let pointColor = "#8baf68";
let defaultPointColor = "#b3b3b3";

function fillPoint(id, pointColor) {
    let $point = $(`[id='${id}']`, svgdom);
    let $pointTr = $(`.pointer[data-id='${id}']`)
    $point.attr("fill", pointColor);
    $point.css({ fill: pointColor });
    $pointTr.addClass("check");
}

function unFillPoint(id, defaultPointColor) {
    let $point = $(`[id='${id}']`, svgdom);
    let $pointTr = $(`.pointer[data-id='${id}']`);
    $point.removeAttr("fill");
    $point.css({ fill: defaultPointColor });
    $pointTr.removeClass("check");
}

//
function fillPoints(point) {
    let el = point.el;
    let scrollToEl = point.scroll;
    console.log("click fill");
    let pointId = $(el).attr("id") || $(el).attr("data-id");
    if ($(el).attr("fill")) {
        unFillPoint(pointId, defaultPointColor);
        return;
    }

    $(".check").each(function (index, el) {
        let pointId = $(el).attr("data-id");
        unFillPoint(pointId, defaultPointColor);
    });

    fillPoint(pointId, pointColor);

    lastPointId = pointId;

    if (scrollToEl) {
        let $pointTr = $(`.pointer[data-id='${pointId}']`);
        $pointTr[0].scrollIntoView({ block: "center", behavior: "smooth" });
    }
}

function getEllipseCenter(el) {
    console.log(el.attr("cx"), el.attr("cy"), el.attr("rx"), el.attr("ry"));
    return {
        x: parseFloat(el.attr("cx")),
        y: parseFloat(el.attr("cy")) - 50,
    }
}

function addLine(firstId, lastId) {
    let first = $(`ellipse[id='${firstId}']`, svgdom);
    let last = $(`ellipse[id='${lastId}']`, svgdom);
    let firstCenter = getEllipseCenter(first);
    let lastCenter = getEllipseCenter(last);
    var newLine = document.createElementNS('http://www.w3.org/2000/svg', 'line');
    newLine.setAttribute('x1', firstCenter.x);
    newLine.setAttribute('y1', firstCenter.y);
    newLine.setAttribute('x2', lastCenter.x);
    newLine.setAttribute('y2', lastCenter.y);
    newLine.setAttribute("stroke", "black")
    $("svg", svgdom).append(newLine);
}

function getDistance(pointFirstId, pointLastId) {
    let first = $(`ellipse[id='${pointFirstId}']`, svgdom);
    let last = $(`ellipse[id='${pointLastId}']`, svgdom);
    let firstCenter = getEllipseCenter(first);
    let lastCenter = getEllipseCenter(last);
    return Math.sqrt(Math.pow(firstCenter.x - lastCenter.x, 2)
        + Math.pow(firstCenter.y - lastCenter.y, 2));
}