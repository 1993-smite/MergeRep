const searchCountryEl = "#search-country";
const mainFormEl = "#form";
const modelEl = "#user";
const fixAlertEl = "#fix-alert";

console.log(model);
var app = new Vue({
    el: modelEl,
    data: Object.assign(
        model,
        {
            workCountry: "",
            workCity: "",
            workAddress: "",
            workFullAddress: false,
            uploadFiles: false,
            comments: false,
            validate: false
        }),
    computed: {
        getFIO: function () {
            return `${this.lastName} ${this.firstName} ${this.middleName}`
        },
        getLogin: function () {
            return `${this.lastName}-${moment(this.birthdayStr, "YYYY-MM-DD")._d.getFullYear()}`
        },
        getAge: function () {
            return `${new Date().getFullYear() - moment(this.birthdayStr, "YYYY-MM-DD")._d.getFullYear()}`
        },
        getAgeToDeath: function () {
            return `${moment(this.deathStr, "DD.MM.YYYY")._d.getFullYear() - new Date().getFullYear()}`
        },
        getWorkAddress: function () {
            return `${this.workCountry} ${this.workCity} ${this.workAddress}`;
        }
    },
    validations: {
        lastName: {
            required: appValidators.required,
            minLength: appValidators.minLength(3)
        },
        firstName: {
            required: appValidators.required,
            minLength: appValidators.minLength(3)
        }
    }
})

var appFix = new Vue({
    el: fixAlertEl,
    data: {
        unreadMsg: 0
    },
    methods: {
        addUnreadMsg: function (count = 1) {
            this.unreadMsg += count;
        },
        subUnreadMsg: function (count = 1) {
            this.unreadMsg -= count;
        },
        clrUnreadMsg: function () {
            this.unreadMsg = 0;
        }
    }
})

$(function () {
    //привязка kladr для стран
    $(searchCountryEl).fias({
        type: $.fias.type.city,
        limit: 6,
        openBefore: function () {
            if ($(searchCountryEl).val().length < 2)
                return false;
        }
    });

    $(".input-group-append .btn-close").click(function () {
        $(searchCountryEl).val("");
    });

    //связка датапикера и вью
    $("#birthDay, #deathDay").datepicker({
        dateFormat: 'dd.mm.yy',
        changeYear: true,
        onSelect: function (dateText) {
            //формируем эвент ввода данных в поле, чтобы вью подхватил
            $(this)[0].dispatchEvent(new Event('input', { 'bubbles': true }))
        }
    });

    /*
     * валидация jquery
     */
    //правило для заполнения текстового поля
    let strValid = {
        required: true,
        minlength: 2
    };

    //сообщения для валидации обязательных полей 
    let strValidMessage = function (fieldName) {
        return {
            required: `Поле '${fieldName}' обязательно к заполнению`,
            minlength: `Введите не менее 2-х символов в поле '${fieldName}'`
        }
    };

    $(mainFormEl).validate({
        rules: {
            LastName: strValid,
            FirstName: strValid,
            MiddleName: strValid
        },
        messages: {
            LastName: strValidMessage("Фамилия"),
            FirstName: strValidMessage("Имя"),
            MiddleName: strValidMessage("Отчество")
        }
    });
    /* end of jquery validate */

    /*********** use observer отслеживание изменений формы ****************/
    /*console.log($(mainFormEl));
    let server = new Observer(form, null, function (mutationsList, observerEl) {
        var text = "";
        for (let mutation of mutationsList) {
            if (mutation.type === 'childList') {
                text += "A child node has been added or removed."
            } else if (mutation.type === 'attributes') {
                text += `The ${mutation.attributeName} attribute was modified.`;
            }
            text += "<br/>";
        }
        console.log(text.replaceAll("<br/>", ""));
        $("#alert-title").html(text);
        $('#alert-container').addClass("show");
    });*/
    /************************************************************************/

});