let globalsFilter = {
    keys: {
        date: 'date',
        time: 'time'
    },

    dt: function (value, format) {
        // return processed value
        let options = {
            locales: 'ru-ru'
        };
        if (format.includes('date')) {
            options = object.assign({
                day: 'numeric',
                month: 'numeric',
                year: 'numeric'
            }, options);
        }
        if (format.includes('time')) {
            options = object.assign({
                hour: 'numeric',
                minute: 'numeric',
                second: 'numeric'
            }, options);
        }

        return new Intl.DateTimeFormat(options).format(value);
    }
}

// register
Vue.filter('date-ru-format', function (value) {
    // return processed value
    let options = {
        locales: 'ru-ru',

    };
    return globalsFilter.dt(value, globalsFilter.keys.date);
});

// register
Vue.filter('time-ru-format', function (value) {
    // return processed value
    let options = {
        locales: 'ru-ru',

    };
    return globalsFilter.dt(value, globalsFilter.keys.time);
});

// register
Vue.filter('date-time-ru-format', function (value) {
    // return processed value
    let options = {
        locales: 'ru-ru',

    };
    return globalsFilter.dt(value, `${globalsFilter.keys.date}${globalsFilter.keys.time}`);
});