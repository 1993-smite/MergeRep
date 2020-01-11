let svgdom;

let pointColor = "#8baf68";

function loadFilter() {
    //$("#building").val(filter.Building);
    $(`#building option[value=${filter.Building}]`).attr('selected', 'selected');
    $(`#level option[value=${filter.Level}]`).attr('selected', 'selected');
    //$("#level").val(filter.Level);
    console.log(filter);
    initSVG();
    //$(".update-svg").first().change();
}

// инициализируем дом для svg картинки
function initSVG() {
    let svgObject = document.getElementById('svgmap');
    if ('contentDocument' in svgObject) {
        // получаем доступ к объектной модели SVG-файла
        svgdom = $(svgObject.contentDocument);
        // проверяем загрузилось ли изображение
        if ($(`#level-${$("#level").val()}`, svgdom).length > 0) {
            // загрузка интерактивности для svg
            loadSVG();
        }
        else {
            // если не загрузилось пробуем через 100 мс
            setTimeout(() => initSVG(), 100);
        }
    } else {
        setTimeout(() => initSVG(), 100);
    }
}

// привязываем интерактивность к элементам картинки
function loadSVG() {
    for (let item of model) {
        let $point = $(`ellipse[id='${item.id}']`, svgdom);
        $point.attr("data-toggle", "tooltip");
        //console.log(item.id);
        $point.mousemove(function () {
            console.log("move");
            let point = item;
            let el = $(this);
            //el.attr("fill", "#8baf68");
            let position = el.offset();
            $(el).css('cursor', 'pointer');
            $("#point-tooltip").css({ top: position.top + 86, left: position.left + 100 });
            console.log(el.position(), el.offset(), $("#point-tooltip").offset());
            $(".tooltip-inner", "#point-tooltip").text(point.title);
            $("#point-tooltip").show();
        });
        $point.click(function () {
            console.log("click");
            let el = $(this);

            if (el.attr("fill")) {
                $(`.pointer[data-id='${el.attr("id")}']`).removeClass("check");
                $(el).removeAttr("fill");
                return;
            }

            el.attr("fill", pointColor);
            $(el).css('cursor', 'pointer');

            $(".check").each(function (index,el) {
                $(`ellipse[id='${$(el).attr("data-id")}']`, svgdom).removeAttr("fill");
                $(el).removeClass("check");
            });
            $(`.pointer[data-id='${el.attr("id")}']`).addClass("check");
        });
        $point.mouseleave(function () {
            console.log("leave");
            let el = $(this);
            //el.attr("fill", "black");
            $("#point-tooltip").hide();
        });
    }
}

$(document).ready(function () {

    $(".update-svg").change(function () {
        let buildingPath = $("#building").attr("data-path");
        let building = $("#building").val();
        let level = $("#level").val();
        let path = `${buildingPath}/${building}/${level}/plan.svg`;
        console.log("update-svg", path);
        $.get("/SVG/GetPoints",
            {
                building: building,
                level: level
            },
            function (data) {
                model = data;
                vue.$data.points = data;
                console.log(model);
            }
        ).done(function () {
            $("#svgmap").attr("data", path);
            $("#svgmap").html("");
        }).done(function () {
            setTimeout(() => initSVG(), 100);
        });
    });

    $("#tbl-points").on("click","tr",
        function () {
            let id = $(this).attr("data-id");
            $(`ellipse[id='${id}']`, svgdom).click();
    });

    //setTimeout(() => initSVG(), 100);
    loadFilter();
    //initSVG();

});