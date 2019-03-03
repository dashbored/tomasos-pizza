// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    // Wire up all of the checkboxes to run markCompleted()
    $('.done-checkbox').on('click', function (e) {
        markCompleted(e.target);
    });

    var wrapCart = $("#cart-order");
    var wrapNav = $("#navbar");


    $(window).on('scroll', function (e) {
        if ($(window).scrollTop() > 264.5) {
            wrapCart.addClass("fix-cart");
            wrapNav.addClass("fixed-top");
        } else {
            wrapCart.removeClass("fix-cart");
            wrapNav.removeClass("fixed-top");
        }
    });

    //$("#drpDishes").change(function() {

    //    this.form.submit();
    //});

});

function markCompleted(checkbox) {
    
    var form = checkbox.closest('form');
    var value = checkbox.value;
    form.submit();
}