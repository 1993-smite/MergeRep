// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//common site scrips
// prototype for replace all
String.prototype.replaceAll = function (search, replace) {
    return this.split(search).join(replace);
}

function changeObjects(first, second) {
    return [second, first];
}

function initObject(name,value) {
    this.name = name;
    this.value = value;
}

$(function () {
    //ready
})