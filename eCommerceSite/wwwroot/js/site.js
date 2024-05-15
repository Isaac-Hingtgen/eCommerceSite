// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const headerHeight = $("header").height();
const categoryOffcanvasHeight = window.innerHeight;
$("div.category-offcanvas").css({ "height": categoryOffcanvasHeight })

//$("div.shop-container").css({ "margin-left": $("div.category-offcanvas").width() })



// add profile partial view to index.cshtml
$('#profile').load("/Shop/ProfilePartial")



// back to top button
let mybutton = document.getElementById("btn-back-to-top");
window.onscroll = function () {
    scrollFunction();
};
function scrollFunction() {
    if (
        document.body.scrollTop > 20 ||
        document.documentElement.scrollTop > 20
    ) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}
mybutton.addEventListener("click", backToTop);
function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}