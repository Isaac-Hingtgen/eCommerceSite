// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$("div.shop-container").css({ "margin-left": $("div.category-offcanvas").width() })


// add profile and cart partial view to _Layout.cshtml
//$('#profile').load("/Shop/ProfilePartial")
//$('#cart').load("/Shop/CartPartial")


// back to top button
let mybutton = document.getElementById("btn-back-to-top");
window.onscroll = function () {
    scrollFunction();
};

const headerHeight = $("header").height();
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

const USDollar = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
})

function addImageSpinners() {
    $('img').each(function () {
        if (!this.complete) {
            $(this).on('load', function () {
                $(this).parent().find('#spinner').remove()
            })
            $(this).after("<img class='card-img-top' id='spinner' src='../../images/spinner.gif' alt='Loading...' />")
        }
    })
}

addImageSpinners();



//var startApp = function () {
//    gapi.load('auth2', function () {
//        // Retrieve the singleton for the GoogleAuth library and set up the client.
//        auth2 = gapi.auth2.init({
//            client_id: '834917429572-05lbg6eo6u4c2d6jq57u2sopjj68o729.apps.googleusercontent.com',
//            cookiepolicy: 'multi_host_origin',
//            // Request scopes in addition to 'profile' and 'email'
//            //scope: 'additional_scope'
//        });
//        attachSignin(document.getElementById('customBtn'));
//    });
//};

//function attachSignin(element) {
//    console.log(element.id);
//    auth2.attachClickHandler(element, {},
//        function (googleUser) {
//            document.getElementById('name').innerText = "Signed in: " +
//                googleUser.getBasicProfile().getName();
//        }, function (error) {
//            alert(JSON.stringify(error, undefined, 2));
//        });
//}


