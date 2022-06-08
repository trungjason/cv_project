// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict';
    window.addEventListener('load', function () {
        document.title = 'Hội viên - Cơm nhà'

        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                event.preventDefault();
                event.stopPropagation();
                if (form.checkValidity() === false) {
                    form.classList.add('was-validated');
                } else {
                    form.classList.remove('was-validated');
                    if (event.target.id === 'member-password-form') {
                        // Check confirm new password
                        confirmPassword() ?? changePassword(event.target);
                    }
                    if (event.target.id === 'member-discount-form') {
                        displayDiscount(event.target);
                    }
                }
            }, false);
        });
    }, false);
})();

function displayDiscount(form) {
    fetch("https://localhost:5556/Member/" + $("#memberId").val() + "/discount")
        .then(response => response.json())
        .then(response => {
            if (!response['status']) {
                $("#discount-errorMessage").html(response['message'])
                    .stop()
                    .addClass("alert-warning")
                    .fadeIn(1500)
                    .fadeOut(4000);
                form.reset();
                return
            }
            var discount = response['discount'];
            $("#memberDiscount").val(discount*100 + "%")
        })
}

function confirmPassword() {
    if ($("#member-retype-password").val() !== $("#member-new-password").val()) {
        $("#errorMessage").html("Nhập lại mật khẩu không đúng.").addClass("alert-danger").fadeIn('slow').fadeOut(4000)
        return false
    }
    return null
}

function changePassword(form) {
    fetch("https://localhost:5556/Member/" + $("#member-id").val() + "/change-password",
        {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                oldPassword: $("#member-old-password").val(),
                newPassword: $("#member-new-password").val()
            })
        })
        .then(response => response.json())
        .then(response => {
            if (!response['status']) {
                $("#errorMessage").html(response['message'])
                    .stop()
                    .removeClass('alert-success')
                    .addClass("alert-danger")
                    .fadeIn(1500)
                    .fadeOut(4000);
                return
            }
            $("#errorMessage").html(response['message'])
                .stop()
                .removeClass('alert-danger')
                .addClass('alert-success')
                .fadeIn(1500)
                .fadeOut(4000);
            form.reset();
        })
}