// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("div.category-offcanvas").css({ "top": $("header").height() })

$("div.shop-container").css({ "margin-left": $("div.category-offcanvas").width() })