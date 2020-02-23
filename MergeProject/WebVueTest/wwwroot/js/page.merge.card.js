const searchCountryEl = "#search-country";
const mainFormEl = "#form";
const modelEl = "#user";

console.log(Object.assign(
    model,
    {
        workCountry: "",
        workCity: "",
        workAddress: ""
    }));
var app = new Vue({
    el: modelEl,
    data: Object.assign(
        model,
        {
            workCountry: "",
            workCity: "",
            workAddress: "",
            workFullAddress: false
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
    }
})

$(function () {

    //import counter from './components/counter.vue'
    /*export default {
        components: {
            counter
        }
    }*/

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
            lastName: strValid,
            firstName: strValid,
            middleName: strValid
        },
        messages: {
            lastName: strValidMessage("Фамилия"),
            firstName: strValidMessage("Имя"),
            middleName: strValidMessage("Отчество")
        }
    });
    /* end of jquery validate */

    /************** change of for last val and val ***********/
    var counter = jBlocks.get(document.querySelector('.select.input'));

    // use event to react on what happens during lifecycle
    counter.on('change', function (el) {
        console.log(el);
    });
    /**********************************************************/

    /*********** use observer отслеживание изменений формы ****************/
    console.log($(mainFormEl));
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
    });
    /************************************************************************/

});