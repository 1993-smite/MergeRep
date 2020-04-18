/* строка для ввода данных на форме * /

<template>
    <div class="input-group mb-3">
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
    </div>
</template>

<script> 
    export default {
        data: function (id, label, name, value, autocomplite, type) {
            return {
                id,
                label,
                name,
                value,
                autocomplite,
                type
            }
        },
        filters: {
            defType: function (val) {
                if (val) return val;
                else return "text";
            }
        }
    }
    
</script>