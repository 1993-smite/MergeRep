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
               </div>`
});
//file uploader
Vue.component('form-file-upload', {
    props: ['id', 'action', 'dataId'],
    template: `<div class="input-group mb-3">
                    <form type="files" method="POST" :data-id="dataId" :action="action" enctype="multipart/form-data">
                      <label :for="id">
                        <div data-id="dropZone">
                            <span>Для загрузки, перетащите файл сюда.</span>
                            <input type="file" :id="id" style="display: none" name="file" multiple>
                        </div>
                      </label>
                    </form> 
               </div>`
});

Vue.component('button-counter', {
    data: function () {
        return {
            count: 0
        }
    },
    template: '<button v-on:click="count++">Счётчик кликов — {{ count }}</button>'
});