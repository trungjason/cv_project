$(document).ready(function() {
    if (document.querySelector('.home-slider')) {
        $('.home-slider').slick({
            dots: true,
            infinite: true,
            speed: 500,
            fade: true,
            cssEase: 'linear',
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 3000,
            prevArrow: "<button type='button' class='slick-prev slick-arrow'><i class='fa fa-angle-left' aria-hidden='true'></i></button>",
            nextArrow: "<button type='button' class='slick-next slick-arrow'><i class='fa fa-angle-right' aria-hidden='true'></i></button>",
            responsive: [{
                breakpoint: 576,
                settings: {
                    dots: false,
                    arrows: false,
                }
            }]
        });
    
        $('.home-content__item-food-list').slick({
            dots: false,
            arrows: false,
            infinite: true,
            speed: 500,
            cssEase: 'linear',
            slidesToShow: 4,
            slidesToScroll: 4,
            autoplay: true,
            autoplaySpeed: 3000,
            responsive: [
                {
                    breakpoint: 576,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1,
                    }
                },
            ]
        });
    }
})