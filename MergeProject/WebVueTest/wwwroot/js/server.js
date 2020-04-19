﻿class Server {

    constructor() {

    }

    isGet(method = 'GET') {
        return method.toUpperCase() === 'GET';
    }

    prepare(url, method, params = {}) {
        let query = this.isGet(method)
            ? Object.keys(params)
                .map(k => esc(k) + '=' + esc(params[k]))
                .join('&')
            : '';
        if (query.length > 1) {
            url = '?' + query;
        }
        let options = {
            method: method, // *GET, POST, PUT, DELETE, etc.
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            }
        }
        if (!this.isGet(method)) {
            options.body = JSON.stringify(params);
        }
        return {
            url,
            method,
            params,
            options
        };
    }

    requestJSON(url, method, params = {}) {
        this.requestJSONAsync(url, method, params)
            .then(function (result) {
                return result;
            });
    }

    async requestJSONAsync(url, method, params = {}) {
        let { url: ulrQuery, options } = this.prepare(url, method, params);
        try {
            const response = await fetch(ulrQuery, options);
            if (response.status === 200) {
                let json = await response.json();
                return json;
            } else {
                console.warn(`${response.status}`, response);
                throw new Error(`${response.status}`);
            }
        }
        catch (error) {
            console.error(error.message, error);
        }
    }
}