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
                    <label class= "col-md-2" :for="id">{{label}}</label> 
                    <input class="col-md-4 form-control" :id="id" :name="name"
                        :type="type | defType" :list="autocomplite"
                        v-bind:value="value" v-on:input="$emit('input', $event.target.value)" />
                    <slot></slot>
               </div>`
});