const ApiMethods = {
    get: 'GET',
    put: 'PUT',
    post: 'POST',
    delete: 'DELETE'
}

class Server {
    constructor() {

    }

    isGet(method = ApiMethods.get) {
        return method.toUpperCase() === ApiMethods.get;
    }

    isPut(method = ApiMethods.put) {
        return method.toUpperCase() === ApiMethods.put;
    }

    isDelete(method = ApiMethods.delete) {
        return method.toUpperCase() === ApiMethods.delete;
    }

    prepare(url, method, params = {}) {
        let query = '';
        if (this.isPut(method) || this.isDelete(method)) {
            url += '/' + params['id']; //'Id=' + params['id'];
        }
        else if (this.isGet(method)) {
            query = Object.keys(params)
                .map(k => esc(k) + '=' + esc(params[k]))
                .join('&');
        }
        if (query.length > 1) {
            url += '?' + query;
        }
        let options = {
            method: method, // *GET, POST, PUT, DELETE, etc.
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            }
        }
        if (!this.isGet(method) && !this.isDelete(method)) {
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
            }
            else if (response.status === 400) {
                let err = await response.json();
                return { status: 400 , valid: err };
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