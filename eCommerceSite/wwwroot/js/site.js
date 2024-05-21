// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$("div.shop-container").css({ "margin-left": $("div.category-offcanvas").width() })


// add profile and cart partial view to _Layout.cshtml
$('#profile').load("/Shop/ProfilePartial")
$('#cart').load("/Shop/CartPartial")


// back to top button
let mybutton = document.getElementById("btn-back-to-top");
window.onscroll = function () {
    scrollFunction();
};
function scrollFunction() {
    if (
        document.body.scrollTop > headerHeight ||
        document.documentElement.scrollTop > headerHeight
    ) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}
mybutton.addEventListener("click", backToTop);
function backToTop() {
    $("html, body").animate({
        scrollTop: 0
    }, 150, "swing")
}



