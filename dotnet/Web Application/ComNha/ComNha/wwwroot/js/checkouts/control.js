let tempPrice = 0;
let shippingFee = 50000;
let discountFee = 0;
const foods = {};
const orderFood = [];

function renderItemList() {
    if (Object.keys(cartList["foods"]).length > 0) {
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

                    tempPrice = tempPrice + totalPrice;
                    foods[foodID] = {
                        quantity: foodQuantity,
                        price: foodPrice,
                    }
                    orderFood.push({ foodID: foodID, quantity: foodQuantity, price: foodPrice});

                    setPrice();

                    $('.checkouts-list').append(`
                        <li class="checkouts-item d-flex align-items-center mb-4">
                            <div class="checkouts-item__img">
                                <img src="/images/food-images/${foodImage}" alt="Img">
                                <span>${foodQuantity}</span>
                            </div>
                            <div class="checkouts-item__info d-flex flex-column flex-sm-row justify-content-between ml-3">
                                <div class="checkouts-item__name">${foodName}</div>
                                <div class="checkouts-item__price">${Intl.NumberFormat('vi-VN').format(totalPrice)}đ</div>
                            </div>
                        </li>
                    `);
                }
           });
        })

        // Thiết lập các giá tiền
        setPrice();
    } else {
        window.location.href = "/ComNha/menu";
    }

}

function setPrice() {
    $('.checkouts-price').children('span').html(`${Intl.NumberFormat('vi-VN').format(tempPrice)}đ`);

    if (tempPrice > 1000000) {
        shippingFee = 0;
    }

    $('.checkouts-ship').children('span').html(`${Intl.NumberFormat('vi-VN').format(shippingFee)}đ`);
    $('.checkouts-discount').children('span').html(`${Intl.NumberFormat('vi-VN').format(discountFee)}đ`);
    $('.checkouts-total').children('span').html(`${Intl.NumberFormat('vi-VN').format(tempPrice + shippingFee - discountFee)}đ`)
}

renderItemList();

var memberIDValid = '';

$('.checkouts-member-btn').click(function () {
    const memberInputElement = $('#checkouts-member');
    const memberMessElement = $('.checkouts-member-message');
    const memberID = memberInputElement.val();

    if (memberID) {
        $.get(`https://localhost:5556/ComNha/MemberInfo/${memberID}`, function (data) {
            if (data) {
                console.log(data);
                memberIDValid = data['soDienThoai'];
                console.log("Member ID valid", memberIDValid);
                memberInputElement.attr('disabled', true);
                $('#checkouts-phone').val(memberIDValid);
                setDiscount(parseInt(data['tongSoTienThanhToan']));
            } else {
                memberMessElement.removeClass('d-none');

                setTimeout(() => {
                    memberMessElement.addClass('d-none');
                }, 2000);
            }
        }, "json");
    } else {
        memberMessElement.removeClass('d-none');

        setTimeout(() => {
            memberMessElement.addClass('d-none');
        }, 2000);
    }
})

function setDiscount(totalMoney) {
    let discountPercent = 0.02;
    if (totalMoney <= 3000000) {
        discountPercent = 0.02;
    } else if (totalMoney <= 5000000) {
        discountPercent = 0.05;
    } else if (totalMoney <= 10000000) {
        discountPercent = 0.08;
    } else if (totalMoney <= 15000000) {
        discountPercent = 0.1;
    } else if (totalMoney <= 20000000) {
        discountPercent = 0.12;
    } else {
        discountPercent = 0.15;
    }

    discountFee = (tempPrice + shippingFee) * discountPercent;

    if (discountFee > 1500000) {
        discountFee = 1500000;
    }

    $('.checkouts-discount').children('span').html(`${Intl.NumberFormat('vi-VN').format(discountFee)}đ`);
    $('.checkouts-total').children('span').html(`${Intl.NumberFormat('vi-VN').format(tempPrice + shippingFee - discountFee)}đ`)
}

function handelFormCheckoutsSubmit() {
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation.checkouts-form');
    // Loop over them and prevent submission
    var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();
            if (form.checkValidity() === false) {
                form.classList.add('was-validated');
            } else {
                form.classList.remove('was-validated');
                const phone = $('#checkouts-phone').val();
                if (isVietnamesePhoneNumber(phone)) {
                    const name = $('#checkouts-name').val();
                    const email = $('#checkouts-email').val();
                    const address = `${$('#checkouts-address').val()}, ${$('#checkouts-district').val()}, Tp.HCM`;
                    const message = cartList["message"];
                    const method = $('#checkouts-method').find(":selected").text();

                    $.ajax({
                        url: "https://localhost:5556/ComNha/DatMonTrucTuyen",
                        type: "POST",
                        data: JSON.stringify({
                            name,
                            memberID: memberIDValid,
                            phone,
                            email,
                            address,
                            orderFood: orderFood,
                            message,
                            paymentComNhaValid: true,
                            tempPrice,
                            discountFee,
                            shippingFee,
                            totalPrice: tempPrice + shippingFee - discountFee,
                        }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (result) {
                            console.log(result);
                            if (result.status && result.code == 1) {
                                $('.checkouts-payment').children('.checkouts-form').remove();
                                $('.checkouts-payment').append(`
                                    <div class="checkouts-paid">
                                        <div class="checkouts-paid__heading">
                                            <p class="checkouts-paid__heading-title">
                                                <i class="fas fa-check"></i>
                                                Đặt món thành công 
                                            </p>
                                            <p class="checkouts-paid__heading-bill-id">Mã hóa đơn: ${result.billID}</p>
                                            <p>Cảm ơn bạn đã đặt món!</p>
                                        </div>
                                        <div class="checkouts-paid__content">
                                            <p class="checkouts-paid__content-title">
                                                Thông tin đặt món:
                                            </p>
                                            <ul class="checkouts-paid__content-list">
                                                <li class="checkouts-paid__content-item">
                                                    Họ tên: ${name}
                                                </li>
                                                <li class="checkouts-paid__content-item">
                                                    Số điện thoại: ${phone}
                                                </li>
                                                <li class="checkouts-paid__content-item">
                                                    Địa chỉ giao: ${address}
                                                </li>
                                                <li class="checkouts-paid__content-item">
                                                    Phương thức thanh toán: ${method}
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="d-flex justify-content-end mt-4">
                                            <a href="/ComNha/menu">Tiếp tục đặt món</a>
                                        </div>
                                    </div>
                                `);
                                window.localStorage.setItem('com_nha_cart', JSON.stringify({foods: {}, message: ""}));
                                cartList = JSON.parse(window.localStorage.getItem("com_nha_cart"));
                            } else {
                                alert("Lỗi hệ thống, đặt món thất bại!" + result.message);
                            }
                        }
                    });

                } else {
                    $('.checkouts-phone-message').removeClass('d-none');
                    
                    setTimeout(() => {
                        $('.checkouts-phone-message').addClass('d-none');
                    }, 2000);
                }
            }
            
        }, false);
    });
}

function isVietnamesePhoneNumber(number) {
    return /(84|0[3|5|7|8|9])+([0-9]{8})\b/.test(number);
}

window.addEventListener('load', handelFormCheckoutsSubmit, false);
