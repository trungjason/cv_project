$(document).ready(function () {
    function handleFormContactSubmit() {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.querySelectorAll('.needs-validation.contact-form');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                    form.classList.add('was-validated');
                } else {
                    event.preventDefault();
                    event.stopPropagation();
                    form.classList.remove('was-validated');
                    console.log("Gửi thành công");
                }
            }, false);
        });
    }

    window.addEventListener('load', handleFormContactSubmit, false);
})