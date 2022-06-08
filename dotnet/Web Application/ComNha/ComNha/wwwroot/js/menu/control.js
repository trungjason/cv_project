$(document).ready(function() {
    function checkItemInCartList() {
        $('.menu-item').each(function() {
            const foodID = $(this).attr('data-food-id');

            if (foodID in cartList['foods']) {
                $(this).addClass('added');
            }
        })
    }

    function setOrderBtnEvent() {
        $('.menu-item__order.menu-item__order--add').click(function () {
            const menuItemElement = $(this).closest('.menu-item');
            const foodID = menuItemElement.attr('data-food-id');

            if (!(foodID in cartList['foods'])) {
                cartList["foods"][foodID] = 1
    
                // Lưu giá trị mới vào bộ nhớ cục bộ
                window.localStorage.setItem("com_nha_cart", JSON.stringify(cartList));
    
                // Cập nhật lại số món ăn trong giỏ hàng
                updateCartNumber();
            }

            menuItemElement.addClass('added');
        });

        checkItemInCartList();
    }

    setOrderBtnEvent();

    // Tải món ăn ứng với danh mục món được chọn
    $('.menu-nav__item').click(function () {
        if (!$(this).hasClass('active')) {
            $('.menu-nav__item.active').removeClass('active');
            $(this).addClass('active');
            
            const categoryID = $(this).attr('data-category-id');
            
            $.get(`https://localhost:5556/Menu/${categoryID}`, function (data) {
                if (data.length != 0) {
                    $('.menu-list-wrapper').children('.menu-item').remove();
                    for (let i = 0; i < data.length; i++) {
                        const food = data[i];

                        $('.menu-list-wrapper').append(`
                            <div 
                                class="col-12 col-md-6 menu-item" 
                                data-food-id="${food.maMonAn}"
                            >
                                <div class="menu-item__wrapper d-flex flex-column align-items-center flex-lg-row">
                                    <a href="/Menu/chitiet/${food.maMonAn}">
                                        <img 
                                            src="/images/food-images/${food.hinhAnh}" 
                                            alt="Img" 
                                            class="menu-item__img"
                                        >
                                    </a>
                                    <div class="menu-item__info d-flex flex-column justify-content-between py-2 px-3">
                                        <div class="d-flex flex-column">
                                            <div class="d-flex align-items-center justify-content-between">
                                                <a href="/Menu/chitiet/${food.maMonAn}" class="menu-item__name">
                                                    ${food.tenMonAn}
                                                </a>
                                                <span class="menu-item__price">
                                                    ${Intl.NumberFormat('vi-VN').format(food.donGia)}đ
                                                </span>
                                            </div>
                                            <p class="menu-item__desc">
                                                ${food.moTa}
                                            </p>
                                        </div>
                                        <button class="menu-item__order menu-item__order--add">
                                            <i class="fas fa-cart-plus"></i>
                                            Order
                                        </button>
                                        <button class="menu-item__order menu-item__order--added">
                                            Đã thêm
                                        </button>
                                    </div>
                                </div>
                            </div>
                        `);
                    };
                    setOrderBtnEvent();                                    
                } else {
                    $('.menu-list-wrapper').children('.menu-item').remove();
                    $('.menu-list-wrapper').append(`
                        <div 
                            class="col-12 menu-item pt-4" 
                        >
                            <h4 class="text-center text-danger">Chưa cập nhật món ăn</h4>
                        </div>
                    `)
                }
            }, "json");
        }

    })

    if (document.querySelector('.detail')) {
        const foodInfoDetailElement = $('.detail-info');
        const foodID = foodInfoDetailElement.attr('data-food-id');

        if (foodID in cartList['foods']) {
            foodInfoDetailElement.addClass('added');
        }
        
        $('.detail-info__order--add').click(function() {
            if (!(foodID in cartList['foods'])) {
                cartList["foods"][foodID] = 1;
    
                // Lưu giá trị mới vào bộ nhớ cục bộ
                window.localStorage.setItem("com_nha_cart", JSON.stringify(cartList));
    
                // Cập nhật lại số món ăn trong giỏ hàng
                updateCartNumber();
            }

            foodInfoDetailElement.addClass('added');
        })

        $(function() {
            $('#imageZoom').imageZoom({
                zoom: 200
            });
        })
    }
})