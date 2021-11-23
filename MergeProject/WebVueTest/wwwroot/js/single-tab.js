function MoreOneTab() {
    $("#double-tab").show();
}

function OnlyOneTab() {
    $("#double-tab").hide();
}

let formUser = document.querySelector('#user');
var singleTab = jBlocks.get(formUser);
singleTab._fMoreOneTab = function () {
    $("#double-tab").show();
};
singleTab._fOnlyOneTab = function () {
    $("#double-tab").hide();
};


$(function () {

    //const href = location.href;
    //const key = location.pathname;

    //const storage = localStorage;

    //let sItem = storage.getItem(key);

    //console.log(location, sItem);

    //if (sItem) {
    //    storage.setItem(key, ++sItem);
    //    $("#double-tab").show();
    //}
    //else {
    //    storage.setItem(key, 1);
    //    $("#double-tab").hide();
    //}

    //var windowObjectReference = window.open(href, 'Test');
    //console.log(windowObjectReference, href);

    //window.addEventListener("unload", function () {
    //    let sItem = storage.getItem(key);

    //    if (sItem && sItem < 2)
    //        storage.removeItem(key);
    //    else 
    //        storage.setItem(key, --sItem);
    //});
    //let formUser = document.querySelector('#user');
    //formUser.setAttribute('data-props', `{
    //    "fMoreOneTab": MoreOneTab()
    //}`);
    //$(formUser).attr('data-props', `{
    //    fed: 2123
    //}`);
    //var singleTab = jBlocks.get(formUser);
    //singleTab._fMoreOneTab = function () {
    //    $("#double-tab").show();
    //};
    //singleTab._fOnlyOneTab = function () {
    //    $("#double-tab").hide();
    //};

    window.addEventListener("load", function () {
        singleTab.loadTab(event);
    });

    window.addEventListener("unload", function () {
        singleTab.unloadTab(event);
    });

    //$('#user').jBlocks('single-tab');

});