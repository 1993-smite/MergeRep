/************************** lazy-input-text-change **********************************************/
jBlocks.define('lazy-input-text-change',
{
    events: {
        'keyup': 'update'
    },

    methods: {
        oninit: function () {
            this._currentValue = this.props.initialValue || "";
            this._timeOut = Number(this.props.timeOut) || 1000;
            this._blockUpdate = false;
        },
        ondestroy: function () {
            this._currentValue = null;
            this._timeOut = null;
            this._blockUpdate = null;
        },
        /**
            * Increases the counter, emits changed event
            */
        update: function () {
            if (!this._blockUpdate) {
                this._blockUpdate = true;
                var component = this;
                setTimeout(function () {
                        component.emit('changed',
                            {
                                value: this._currentValue
                            });
                        component._blockUpdate = false;
                    },
                    this._timeOut);
            }
        }
    }
});
/***************************************************************************************/
jBlocks.define('input-change',
{
    events: {
        'focus': 'check',
    },

    methods: {
        oninit: function () {
            this._lastValue = null;
            this._curValue = null;
            var element = this;
            this.node.onchange = function () {
                console.log("change", element);
                element._lastValue = element._curValue;
                element._curValue = element.node.value;
                element.emit('change', { Val: element.node.value, lastVal: element._lastValue, el: element });
            }
        },
        ondestroy: function () {
            this._lastValue = null;
            this._curValue = null;
        },
        /**
            * Increases the counter, emits changed event
            */
        check: function () {
            this._lastValue = this.node.value;
            this.emit('focus', { element: this });
        }
    }
});