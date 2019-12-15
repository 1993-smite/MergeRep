// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $(".input-group-append .btn-close").click(function () {
        $("#search-country").val("");
    });
    $(".glyphicon-search", ".input-group-prepend ").click(function () {
        console.log($('#search-country').fias().change());
    });
});