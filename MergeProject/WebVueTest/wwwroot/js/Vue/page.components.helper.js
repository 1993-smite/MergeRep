//строка для ввода данных на форме
Vue.component('form-tr-input', {
    // camelCase в JavaScript
    props: ['id', 'label', 'name', 'value', 'autocomplite', 'type'],
    filters: {
        defType: function (val) {
            if (val) return val;
            else return "text";
        }
    },
    template: `<div class="input-group mb-3">
                    <div class="col-md-2 form-block-label">
                        <label :for="id">{{label}}</label> <slot name="label"></slot> 
                    </div>
                    <div class="col-md-4 form-block-control">                    
                        <input class="form-control" :id="id" :name="name"
                        :type="type | defType" :list="autocomplite"
                        v-bind:value="value" v-on:input="$emit('input', $event.target.value)" />  
                    </div>
                    <slot></slot>
                    <slot name="validation"></slot>
               </div>`
});
//file uploader
Vue.component('form-file-upload', {
    props: ['id', 'action', 'dataId', 'multi'],
    template: `<div class="input-group mb-3">
                    <form type="files" method="POST" :data-id="dataId" :action="action" enctype="multipart/form-data">
                      <label :for="id">
                        <div data-id="dropZone">
                            <span>Для загрузки, перетащите файл сюда.</span>
                            <input type="file" :id="id" style="display: none" name="file" :multiple="multi">
                        </div>
                      </label>
                    </form> 
               </div>`
});

Vue.component('v-input', {
    props: ['readonly','model', 'validator', 'msg'],
    data: function () {
        return {
            notEdit: this.readonly || true,
            validMsg: this.msg || '',
            valid: this.validator || false,
            mdl: this.model,
        }
    },
    template: `<div>
                    <template v-if="notEdit">
                        {{mdl}}
                    </template>
                    <template v-else>
                        <input type="text" class="input text med"
                                           v-model.lazy.trim="mdl"
                                           v-on:input="onChange">
                        <span class="error" v-show="valid">
                            {{validMsg}}
                        </span>
                    </template>
               </div>`,
    methods: {
        onChange: function () {
            this.$emit('update:model', this.mdl);
        }
    }
});

Vue.component('v-select', {
    props: ['readonly','model', 'source'],
    data: function () {
        return {
            notEdit: this.readonly || false,
            selectIndex: this.model || 1,
            selectName: this.source.find(x => x.Index == this.model).Name || ''
        }
    },
    template: `<div>
                    <template v-if="notEdit">
                        {{selectName}}
                    </template>
                    <template v-else>
                        <select class="input select" v-model="selectIndex" v-on:change="onChange">
                               <option v-for="item in source" v-bind:value="item.Index">{{item.Name}}</option>
                        </select>
                    </template>
               </div>`,
    methods: {
        onChange: function () {
            this.$emit('update:model', this.selectIndex);
        }
    }
});

Vue.component('button-counter', {
    data: function () {
        return {
            count: 0
        }
    },
    template: '<button v-on:click="count++">Счётчик кликов — {{ count }}</button>'
});