
let VueOptions = {
    silent: true, //логи
    devtools: false,
    errorHandler: function (err, vm, info) {
        
    },
    performance: true,
    productionTip: true
};

for (let prop in VueOptions) {
    Vue.config[prop] = VueOptions[prop];
}