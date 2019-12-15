const searchCountryEl = "#search-country";
const mainFormEl = "#form";
const modelEl = "#user";

console.log(model);
var app = new Vue({
    el: modelEl,
    data: model,
    computed: {
        getFIO: function () {
            return `${this.lastName} ${this.firstName} ${this.middleName}`
        },
        getLogin: function () {
            return `${this.lastName}-1119`
        },
        getAge: function () {
            return `${new Date().getFullYear() - moment(this.birthdayStr, "YYYY-MM-DD")._d.getFullYear()}`
        }
    }
})

$(function () {

    //привязка kladr для стран
    $(searchCountryEl).fias({
        type: $.fias.type.city,
        limit: 6,
        openBefore: function () {
            if ($(searchCountryEl).val().length < 4)
                return false;
        }
    });

    $(".input-group-append .btn-close").click(function () {
        $(searchCountryEl).val("");
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