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
/**************************** update select change *********************************************/
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
                element._lastValue = element._curValue;
                element._curValue = element.node.value;

                //user event
                element.emit('change', { Val: element.node.value, lastVal: element._lastValue, el: element });
            }
        },
        ondestroy: function () {
            this._lastValue = null;
            this._curValue = null;
        },
        check: function () {
            this._lastValue = this.node.value;
            this.emit('focus', { element: this });
        }
    }
});
/******************************* tooltip ********************************************/
jBlocks.define('tooltip',
{
    events: {
        'mouseover': 'focus',
    },

    methods: {
        oninit: function () {
            this._title = this.props.title || "please, wait...";
        },
        ondestroy: function () {
            this._title = null
        },
        focus: function () {

            //user event 
            this.emit('getTitle', { element: this, callback: this.show });
        },

        show: function (title) {
            $(this.element.node).attr('data-tooltip', title || this._title);
        }
    }
});
/******************************* single-tab ********************************************/
jBlocks.define('single-tab',
    {
        events: {
            'load': 'loadTab',
            'unload': 'unloadTab'
        },

        methods: {
            oninit: function () {

                this._storage = localStorage;
                this._fMoreOneTab = this.props.fMoreOneTab || function (context) {
                    console.log(`Opened ${context._tabCount} tabs`);
                };
                this._fOnlyOneTab = this.props.fOnlyOneTab || function (context) {
                    console.log("Only one tab");
                };

            },

            loadTab: function () {

                const key = location.pathname;
                let sItem = this._storage.getItem(key);

                if (sItem) {
                    this._tabCount = ++sItem;
                    this._storage.setItem(key, this._tabCount);
                    this._fMoreOneTab(this);
                }
                else {
                    this._storage.setItem(key, 1);
                    this._fOnlyOneTab(this);
                }
            },

            unloadTab: function (title) {
                const key = location.pathname;
                let sItem = this._storage.getItem(key);

                if (sItem && sItem < 2)
                    this._storage.removeItem(key);
                else
                    this._storage.setItem(key, --sItem);
            }
        }
    });
/******************************* toggle ********************************************/
document.addEventListener('click', function (event) {
    if (event.target.dataset.toggle != undefined) {
        let source = event.target.dataset.toggle;
        let els = document.querySelectorAll(source);
        for (let el of els) {
            el.hidden = !el.hidden;
        }
    }
});
/***************************************************************************/