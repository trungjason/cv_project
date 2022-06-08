function getMinDay() {
    var today = new Date();
    today.setDate(today.getDate() + 1);
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0 so need to add 1 to make it 1!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }

    return today = yyyy + '-' + mm + '-' + dd;
}

$('#book-date').attr('min', getMinDay());

const bookAlertElement = $(".book-alert");

function handleFormBookSubmit() {
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation');
    // Loop over them and prevent submission
    var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === false) {
                form.classList.add('was-validated');
            } else {
                form.classList.remove('was-validated');
                const phone = $("#book-phone").val();
                const isValidPhone = isVietnamesePhoneNumber(phone);
                if (isValidPhone) {
                    const name = $("#book-name").val();
                    const quantity = $("#book-quantity").val();
                    const date = $("#book-date").val();
                    const time = $("#book-time").val();
                    const note = $("#book-note").val();

                    $.ajax({
                        url: "https://localhost:5556/Book/DatBan",
                        type: "POST",
                        data: JSON.stringify({
                            name,
                            phone,
                            quantity,
                            date,
                            time,
                            note,
                        }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (result) {
                            console.log(result);
                            if (!result.status) {
                                setBookAlert('aleart', result.message);
                            } else {
                                setBookAlert('success', result.message);
                            }
                            $('.book-form').trigger("reset");
                        }
                    });
                } else {
                    $('#book-phone').focus();
                    setBookAlert('aleart', 'Số điện thoại không hợp lệ');
                }
            }
        }, false);
    });
}

function setBookAlert(type, message) {
    switch (type) {
        case 'success':
            bookAlertElement.removeClass('alert-danger');
            bookAlertElement.addClass('alert-success');
            break;
        default:
            bookAlertElement.removeClass('alert-success');
            bookAlertElement.addClass('alert-danger');
    }

    bookAlertElement.removeClass('d-none');
    bookAlertElement.html(message);
    bookAlertElement.show();

    setTimeout(function () {
        bookAlertElement.addClass('d-none');
    }, 2000);
}

function isVietnamesePhoneNumber(number) {
    return /(84|0[3|5|7|8|9])+([0-9]{8})\b/.test(number);
}

window.addEventListener('load', handleFormBookSubmit, false);
