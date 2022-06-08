$(document).ready(function () {
    updateCartNumber();

    /** Header */
    const menuBtn = document.querySelector('.header-mobile__menu-btn');
    const menuList = document.querySelector('.header-mobile__navbar');
    const menuOverlay = document.querySelector('.header-mobile__overlay');

    if (menuBtn) {
        menuBtn.onclick = function () {
            this.classList.toggle('active');
            menuList.classList.toggle('active');
        }
    }

    if (menuOverlay) {
        menuOverlay.onclick = function () {
            menuBtn.classList.toggle('active');
            menuList.classList.toggle('active');
        }
    }

    /** Go to top */
    function handleScroll() {
        if (window.scrollY >= 300) {
            $('.go-to-top').addClass('active');
        } else {
            $('.go-to-top').removeClass('active');
        }
    }

    window.addEventListener('scroll', handleScroll);
    $('.go-to-top').click(function () {
        window.scrollTo(0, 0);
    })

    $('.fa-circle-minus').on('click', function () {
        $(this).parent().css("transform", "translateX(-150%)");
    })
})

let cartList = window.localStorage.getItem("com_nha_cart") ? JSON.parse(window.localStorage.getItem("com_nha_cart")) : { foods: {}, message: "" };

// Cập nhật số lượng món trong giỏ hàng
function updateCartNumber() {
    let num = Object.keys(cartList["foods"]).reduce(function (acc, curr) {
        return acc + cartList["foods"][curr];
    }, 0);

    if (num > 9) {
        $('.header-cart-number').html("9+");
    } else {
        $('.header-cart-number').html(num);
    }
}
