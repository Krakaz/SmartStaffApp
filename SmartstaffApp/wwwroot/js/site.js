// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SaveSelectToLocalStorage(selectId) {
    var select = document.getElementById(selectId);
    var value = select.options[select.selectedIndex].value;
    localStorage.setItem(selectId, value);
}
function LoadSelectFromLocalStorage(selectId, defaultValue) {
    var item = localStorage.getItem(selectId);
    var select = document.getElementById(selectId);
    if (item !== null) {
        select.value = item;
    } else {
        select.value = defaultValue;
    }
}