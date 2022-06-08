let totalCash = 0;

$(document).ready(function () {
    if (Object.keys(cartList["foods"]).length > 0) {
        $('#cart-empty').addClass('d-none');
        $('#cart-not-empty').removeClass('d-none');

        Object.keys(cartList["foods"]).forEach(foodID => {
            const foodQuantity = cartList["foods"][foodID];

            $.ajax({
                async: false,
                type: 'GET',
                url: `https://localhost:5556/Menu/chitiet/${foodID}`,
                success: function (data) {
                    const foodName = data.tenMonAn;
                    const foodImage = data.hinhAnh;
                    const foodPrice = data.donGia
                    const totalPrice = foodPrice * foodQuantity;

                    console.log(data);

                    totalCash = totalCash + totalPrice;

                    $('.cart-list').append(`
                        <li class="cart-item d-flex align-items-md-center" data-food-id=${foodID} data-food-total=${totalPrice} data-food-price=${foodPrice}>
                            <img src="/images/food-images/${foodImage}" alt="Img" class="cart-item__img">
                            <div class="cart-item__wrapper d-flex flex-column flex-md-row align-items-md-center">
                                <div class="cart-item__info">
                                    <div class="cart-item__name">${foodName}</div>
                                    <div><span class="cart-item__price">${Intl.NumberFormat('vi-VN').format(foodPrice)}</span>đ</div>
                                </div>
                                <div class="cart-item__control">
                                    <button class="cart-item__btn cart-item__btn--decrease" onClick="handleDecrease(this)">
                                        <i class="fas fa-minus"></i>
                                    </button>
                                    <input type="number" class="cart-item__num" value="${foodQuantity}" onChange="handleInputChange(this)">
                                    <button class="cart-item__btn cart-item__btn--increase" onClick="handleIncrease(this)">
                                        <i class="fas fa-plus"></i>
                                    </button>
                                </div>
                                <div class="d-none d-md-block mx-2 cart-item__wrap">
                                    Tổng tiền: <span class="cart-item__total">${Intl.NumberFormat('vi-VN').format(totalPrice)}</span>đ
                                </div>
                            </div>
                            <i class="cart-item__delete fas fa-times" onClick="handleDelete(this)"></i>
                        </li>
                    `);
                }
           });
        })

        // Set tổng tiền
        updateCartTotalCash();

        $(".cart-note").val(cartList["message"]);
    } else {
        $('#cart-empty').removeClass('d-none');
        $('#cart-not-empty').addClass('d-none');
    }

})

function updateCartTotalCash() {
    $('.cart-total__cash').html(`${Intl.NumberFormat('vi-VN').format(totalCash)}đ`);
}

function handleDelete(e) {
    const cartItemElement = $(e).closest('.cart-item');
    const foodID = cartItemElement.attr('data-food-id');
    const foodTotalPrice = cartItemElement.attr('data-food-total');
    cartItemElement.remove();

    delete cartList["foods"][foodID];

    // Kiểm tra, nếu không còn món nào thì hiện phần empty cart list
    if (Object.keys(cartList["foods"]).length === 0) {
        $('#cart-empty').removeClass('d-none');
        $('#cart-not-empty').addClass('d-none');
        cartList["message"] = "";
        window.localStorage.setItem("com_nha_cart", JSON.stringify(cartList));
    }

    // Trừ đi số tiền món ăn
    totalCash = totalCash - parseInt(foodTotalPrice);
    updateCartTotalCash();

    // Lưu giá trị mới vào bộ nhớ cục bộ
    window.localStorage.setItem("com_nha_cart", JSON.stringify(cartList));

    // Cập nhật lại số món ăn trong giỏ hàng
    updateCartNumber();
}

// Bắt sự kiện ghi chú
$('.cart-note').change(function () {
    cartList["message"] = $(this).val();
    window.localStorage.setItem("com_nha_cart", JSON.stringify(cartList));
})

// Bắt sự kiện nút Thêm món
$('.cart-add').click(function () {
    window.location.href = "/ComNha/menu";
})

// Bắt sự kiện nút Đặt ship
$('.cart-order').click(function () {
    window.location.href = "/ComNha/checkouts";
})

// Cập nhật lại thông tin của item cart
function updateCartItem(cartItemElement, type, quantity) {
    const cartItemTotalElement = cartItemElement.find('.cart-item__total');
    const cartItemNumElement = cartItemElement.find('.cart-item__num');

    const foodID = cartItemElement.attr('data-food-id');
    const foodPrice = parseInt(cartItemElement.attr('data-food-price'));
    const foodQuantity = parseInt(cartList["foods"][foodID]);

    let newQuantiry = 0;
    if (type === "add") {
        newQuantiry = foodQuantity + 1;
    } else if (type === "sub") {
        newQuantiry = foodQuantity - 1;
    } else if (type === "custom") {
        newQuantiry = quantity;
    }

    if (newQuantiry !== 0) {
        if (type === "add") {
            totalCash = totalCash + foodPrice;
        } else if (type === "sub") {
            totalCash = totalCash - foodPrice;
        } else if (type === "custom") {
            totalCash = totalCash - (foodPrice * foodQuantity);
            totalCash = totalCash + (foodPrice * newQuantiry);
        }

        // Cập nhật lại các số liệu của cart item:
        cartItemNumElement.val(newQuantiry); // Số lượng
        cartItemTotalElement.html(Intl.NumberFormat('vi-VN').format(foodPrice * newQuantiry)); // Tổng tiền của món ăn
        cartItemElement.attr('data-food-total', foodPrice * newQuantiry); // Set lại attr tổng tiền của thẻ

        // Cập nhật lại tổng tiền trong giỏ hàng
        updateCartTotalCash();

        // Cập nhật lại số lượng của món ăn trong localStorage
        cartList["foods"][foodID] = newQuantiry;
        window.localStorage.setItem('com_nha_cart', JSON.stringify(cartList));

        // Cập nhật số lượng món ăn trên giỏ hàng
        updateCartNumber();
    }
}

// Nhấn nút giảm số lượng => Chỉ được giảm tới số 1
function handleDecrease(e) {
    const cartItemElement = $(e).closest('.cart-item');
    updateCartItem(cartItemElement, "sub");
}

// Nhấn nút tăng số lượng
function handleIncrease(e) {
    const cartItemElement = $(e).closest('.cart-item');
    updateCartItem(cartItemElement, "add");
}

// Bắt sự kiện các ô input được thay đổi
function handleInputChange(e) {
    const cartItemElement = $(e).closest('.cart-item');
    let value = parseInt($(e).val());
    if (value < 1) {
        value = 1;
    }
    updateCartItem(cartItemElement, "custom", value);
}

// $('.cart-item__num').change(function () {
//     const cartItemElement = $(this).closest('.cart-item');
//     let value = parseInt($(this).val());
//     if (value < 1) {
//         value = 1;
//     }
//     updateCartItem(cartItemElement, "custom", value);
// })
