/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ function(module, exports, __webpack_require__) {

	/* WEBPACK VAR INJECTION */(function(global) {global.jBlocks = __webpack_require__(1);

	/* WEBPACK VAR INJECTION */}.call(exports, (function() { return this; }())))

/***/ },
/* 1 */
/***/ function(module, exports, __webpack_require__) {

	var instances = {};
	var declarations = {};
	var gid = 0;
	var noop = function() {};

	/**
	 * @see https://github.com/oleggromov/true-pubsub
	 * @type {Function}
	 */
	var PubSub = __webpack_require__(2);

	/**
	 * Returns constuctor for component with the given name.
	 * Implement the right chain of prototypes:
	 * instance of the component -> methods from decl -> base component methods
	 * @param  {String} name
	 * @return {Function}
	 */
	var getComponentConstructor = function(name) {
	    var F = function() {
	        Component.apply(this, arguments);
	    };
	    var decl = declarations[name] || {};
	    var methods = decl.methods || {};

	    methods.oninit = methods.oninit || noop;
	    methods.ondestroy = methods.ondestroy || noop;

	    F.prototype = Object.create(Component.prototype);
	    F.prototype.constuctor = Component;

	    for (var method in methods) {
	        if (methods.hasOwnProperty(method)) {
	            F.prototype[method] = methods[method];
	        }
	    }
	    return F;
	};

	/**
	 * @namespace
	 * @name jBlocks
	 * @description
	 * Methods to define components
	 * using declaration and create new instances.
	 * Also helps to find and destroy components.
	 */
	var jBlocks = {};

	/**
	 * Destroy instance binded to the node
	 * @memberof jBlocks
	 * @param  {HTMLElement} node
	 * @return {jBlocks}
	 */
	jBlocks.destroy = function(node) {
	    this.get(node).destroy();
	    return this;
	};

	/**
	 * Define a new component
	 * @memberof jBlocks
	 * @param  {String} name
	 * @param  {Object} declaration
	 * @return {jBlocks}
	 */
	jBlocks.define = function(name, declaration) {
	    if (declarations[name]) {
	        throw new Error(name + ' has already been declared');
	    }
	    declarations[name] = declaration || {};
	    return this;
	},

	/**
	 * Remove declaration from cache
	 * @memberof jBlocks
	 * @param  {String} name name of component
	 * @return {jBlocks}
	 */
	jBlocks.forget = function(name) {
	    declarations[name] = null;
	    return this;
	},

	/**
	 * Create and return a new instance of component
	 * @memberof jBlocks
	 * @param  {HTMLElement}  node
	 * @return {jBlocks.Component} a new instance
	 */
	jBlocks.get = function(node) {
	    if (!node) {
	        throw new Error('invalid node');
	    }
	    var name = node.getAttribute('data-component');
	    if (!name) {
	        throw new Error('data-component attribute is missing')
	    }
	    var instanceId = node.getAttribute('data-instance');
	    if (!instanceId) {
	        try {
	            var props = JSON.parse(node.getAttribute('data-props'));
	        } catch (e) {
	            throw e;
	        }
	        var Component = getComponentConstructor(name);
	        var instance = new Component(node, name, props);
	        var instanceId = instance.__id;

	        if (!instances[name]) {
	            instances[name] = {};
	        }
	        instances[name][instanceId] = instance;
	    }
	    return instances[name][instanceId];
	};

	/**
	 * @class
	 * @memberof jBlocks
	 * @param {HTMLElement} node
	 * @param {String}      name
	 * @param {Object}      props
	 */
	var Component = function(node, name, props) {
	    /**
	     * Name of the components used in decl
	     * @type {String}
	     */
	    this.name = name;
	    /**
	     * Node which component is binded with
	     * @type {HTMLElement}
	     */
	    this.node = node;
	    /**
	     * Props of the component gained from data-props
	     * @type {Object}
	     */
	    this.props = props || {};

	    this.__decl = declarations[this.name] || {};
	    this.__id = ++gid;
	    this.__events = {};
	    this.__handlerDomEvents = this.__handlerDomEvents.bind(this);
	    this.__bindDomEvents();
	    this.__pubsub = new PubSub();

	    this.oninit();
	};
	jBlocks.Component = Component;

	Component.prototype =
	/**
	 * @lends jBlocks.Component
	 */
	{
	    /**
	     * Attach an event handler function for the given event
	     * @param  {String}   event
	     * @param  {Function} callback
	     * @return {jBlocks.Component}
	     */
	    on: function(event, callback) {
	        this.__pubsub.on(event, callback);
	        return this;
	    },

	    /**
	     * Remove an event handler function for the given event
	     * @param  {String}   event
	     * @param  {Function} callback
	     * @return {jBlocks.Component}
	     */
	    off: function(event, callback) {
	        this.__pubsub.off(event, callback);
	        return this;
	    },

	    /**
	     * Execute all handlers attached for the given event
	     * @param  {String} event
	     * @param  {*} data
	     * @return {jBlocks.Component}
	     */
	    emit: function(event, data) {
	        this.__pubsub.emit(event, data);
	        return this;
	    },

	    /**
	     * Attach an event handler function for the given event
	     * which will be called only once
	     * @param  {String}   event
	     * @param  {Function} callback
	     * @return {jBlocks.Component}
	     */
	    once: function(event, callback) {
	        this.__pubsub.once(event, callback);
	        return this;
	    },

	    /**
	     * Destroy the instance
	     * @return {jBlocks.Component}
	     */
	    destroy: function() {
	        instances[this.name][this.__id] = null;
	        this.__unbindDomEvents();
	        this.__events = null;
	        this.ondestroy();
	        return null;
	    },

	    /**
	     * Bind DOM Events from decl
	     * @private
	     * @return {jBlocks.Component}
	     */
	    __bindDomEvents: function() {
	        return this.__forEachEvent(function(event) {
	            this.node.addEventListener(event, this.__handlerDomEvents);
	        });
	    },
	    /**
	     * Unbind DOM Events from decl
	     * @private
	     * @return {jBlocks.Component}
	     */
	    __unbindDomEvents: function() {
	        return this.__forEachEvent(function(event) {
	            this.node.removeEventListener(event, this.__handlerDomEvents);
	        });
	    },

	    /**
	     * Iterate for each event from decl
	     * @private
	     * @param  {Function} callback
	     * @return {jBlocks.Component}
	     */
	    __forEachEvent: function(callback) {
	        var events = this.__decl.events || {};

	        for (var name in events) {
	            if (events.hasOwnProperty(name)) {
	                var parts = name.split(' ', 2);
	                var event = parts[0];
	                var selector = parts[1];
	                var callbackName = events[name];
                    console.log(event);
	                callback.call(this, event, selector, callbackName);
	            }
	        }
	        return this;
	    },

	    /**
	     * Handler for each distinc event from decl
	     * @private
	     * @param  {Event} e
	     * @return {jBlocks.Component}
	     */
	    __handlerDomEvents: function(e) {
	        this.__forEachEvent(function(event, selector, callbackName) {
	            if (selector) {
	                var node = this.node.querySelector(selector);
	                if (this.__contains(node, e.target)) {
	                    this.__tryCall(callbackName);
	                }
	            } else {
	                this.__tryCall(callbackName);
	            }
	        });
	    },

	    /**
	     * Safely try to call method of component
	     * @private
	     * @param  {String} method
	     * @return {*}
	     */
	    __tryCall: function(method) {
	        try {
	            return this[method]();
	        } catch (e) {
	            throw new Error(e.message + '. Check out ' + method);
	        }
	    },

	    /**
	     * Check is one element down from another in DOM
	     * @private
	     * @param  {HTMLElement} root
	     * @param  {HTMLElement} child
	     * @return {Boolean}
	     */
	    __contains: function(root, child) {
	        return root === child || root.contains(child);
	    }
	};

	module.exports = jBlocks;


/***/ },
/* 2 */
/***/ function(module, exports) {

	function PubSub () {
	    this._events = {};
	}

	PubSub.prototype = {
	    on: function (event, callback) {
	        if (!this._events[event]) {
	            this._events[event] = [];
	        }

	        this._events[event].push(callback);
	    },

	    once: function (event, callback) {
	        var cb = (function () {
	            callback.apply(undefined, arguments);
	            this.off(event, cb);
	        }).bind(this);

	        this.on(event, cb);
	    },

	    off: function (event, callback) {
	        if (this._events[event]) {
	            var index = this._events[event].indexOf(callback);
	            this._events[event][index] = undefined;
	        }
	    },

	    emit: function (event) {
	        var args = Array.prototype.slice.call(arguments, 1);

	        if (this._events[event]) {
	            this._events[event].forEach(function (callback) {
	                if (typeof callback === 'function') {
	                    callback.apply(undefined, args);
	                }
	            });
	        }
	    }
	};

	module.exports = PubSub;


/***/ }
/******/ ]);