Vue.use(window.vuelidate.default);

let appValidators = {
    required: window.validators.required,
    minLength: window.validators.minLength,
    maxLength: window.validators.maxLength,
    numeric: window.validators.numeric,
    between: window.validators.between,
    email: window.validators.email,
    requiredIf: window.validators.requiredIf,
    ipAddress: window.validators.ipAddress,
    macAddress: window.validators.macAddress,
    url: window.validators.url
}