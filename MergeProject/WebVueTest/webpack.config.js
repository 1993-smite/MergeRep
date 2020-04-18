'use strict';

const fs = require('fs');
const path = require('path');
const glob = require("glob");
const webpack = require("webpack");

const spawn = require('child_process').spawnSync;

const webpackConfigPartName = 'webpack.config.part.js';

// В этой папке будет произведен поиск всех файлов webpack.config.part.js.
const solutionDir = path.join('..', '..', '..');

/**
 * Модули, необходимые для сборки частей конфигурации.
 */
const requirements = {
    path: path,
    glob: glob
}

/**
 * Получение конфигурации сборки
 * @returns {string} "production" либо "development".
 */
const getEnvironmentVariable = () => {
    for (let argument of process.argv)
        if (argument.startsWith("-p"))
            return "production";
    return "development";
}

const nodeModulesPath = path.join(__dirname, "node_modules");
const configurationTemplate = {
    devtool: 'source-maps',
    module: {
        loaders: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'ts-loader'.
            {
                test: /\.tsx?$/,
                loader: "ts-loader"
            }
        ],

        preLoaders: [
            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            {
                test: /\.js$/,
                loader: "source-map-loader"
            }
        ]
    },

    plugins: [
        errorWriter,
        new webpack.DefinePlugin({
            'process.env': {
                'NODE_ENV': JSON.stringify(getEnvironmentVariable())
            }
        })
    ],

    // Оверрайд настроек из tsconfig.json
    ts: {
        compilerOptions: {
            module: "amd",
            noEmit: false,
            target: "es5"
        }
    },

    resolve: {
        // Пусть к node_modules для резолва import'ов
        root: path.resolve(nodeModulesPath),

        // Нужно уметь резолвить модули из .ts и .tsx
        extensions: ["", ".ts", ".tsx", ".js"]
    },

    resolveLoader: {
        // Пусть к node_modules для резолва loader'ов webpack'а
        root: path.resolve(nodeModulesPath)
    }
};

/**
 * Получение target-проекта из аргумента
 * --project=_<путь к папке проекта>_
 * @returns {string} Путь к target-проекту.
 */
const tryGetTargetProject = () => {
    for (let argument of process.argv)
        if (argument.startsWith("--project="))
            return path.resolve(argument.substring("--project=".length));

    return null;
}

/**
 * Получение списка путей проектов, в которых присутствует файл частичной конфигурации.
 * @returns {Array<string>} Массив путей к проектам.
 */
const getProjects = () => {
    const files = glob
        .sync(
            solutionDir + '/**/' + webpackConfigPartName, {
                // Не интересно, что лежит в node_modules и в результатах деплоя msbuild'а
                ignore: solutionDir + '/**/+(obj|node_modules)/**'
            })
        .map(path.dirname);

    const result = [];
    for (let file of files)
        result.push(path.resolve(file));

    return result;
}

/**
 * Получение части конфигурации webpack'а.
 * @param {string} basePath - Путь к папке с проектом.
 * @returns {{}} Конфигурация webpack'а.
 */
const getConfigPart = function (basePath) {
    // Нельзя юзать =>, т.к. сломаются arguments.

    const configPartBuilder = require(path.join(basePath, webpackConfigPartName));
    const configPart = configPartBuilder(basePath, requirements);

    // Новый объект настроек. Возьмем кусок конфига проекта и расширим им стандартные настройки.
    const extendedConfigPart = Object.assign({}, configurationTemplate, configPart);

    return extendedConfigPart;
}

/**
 * Загрузка npm-модулей из файла package.json.
 * @param {string} basePath - Путь к папке с проектом.
 */
const installPackages = (projectPath) => {
    console.info(`\x1b[32m Running 'npm install' in folder "${projectPath}"...\x1b[0m`);

    const packageJsonPath = path.join(projectPath, 'package.json');
    if (!fs.existsSync(packageJsonPath)) {
        console.warn(`File ${packageJsonPath} not found.`);
        return;
    }

    const installPackages = spawn('npm.cmd', ['install'], {
        cwd: projectPath
    });

    if (installPackages.stderr) {
        console.warn(`${installPackages.stderr.toString()}`);
    }

    if (installPackages.error) {
        console.error(`\x1b[1m\x1b[31m Error during 'npm install' in project '${projectPath}': ${installPackages.error.toString()}.\x1b[0m`);
        process.exit(1);
    }
}

/**
 * Создание конфигурации webpack'а. В случае, передан аргумент
 * --project=_<путь к папке проекта>_
 * , создаст конфигурацию только для него.
 * @returns {Array<{}>} Массив конфигураций webpack'а.
 */
const createConfig = () => {
    const projectPaths = getProjects();
    const targetProject = tryGetTargetProject();

    if (targetProject != null) {
        if (projectPaths.indexOf(targetProject) < 0)
            throw new Error(
                `Folder of target-project "${targetProject}" does not exist or "${webpackConfigPartName}" file is missing`);

        installPackages(targetProject);

        console.info(`Webpack config found in project "${targetProject}"...`);
        return [getConfigPart(targetProject)];
    }

    console.info(`Building TypeScript in projects:`);
    for (let project of projectPaths)
        console.info(` — ${project}`);

    console.info(`Installing packages:`);
    for (let project of projectPaths)
        installPackages(project);

    return projectPaths.map(project => getConfigPart(project));
}

module.exports = createConfig();