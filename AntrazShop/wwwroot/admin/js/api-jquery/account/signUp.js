function createUser() {
    isValid = validateInput();
    if (isValid == true) {

        let user = {
            "name": $("#name").val(),
            "gender": $("#gender").val(),
            "email": $("#email").val(),
            "password": $("#password").val(),
            "phoneNumber": $("#phoneNumber").val(),
            "address": $("#address").val(),
            "birthday": $("#birthday").val()
        }
        userJson = JSON.stringify(user);
        $.ajax({
            url: `https://localhost:7092/api/Account/Register`,
            type: 'POST',
            contentType: 'application/json',
            data: userJson,
            success: function (response) {
                swal.fire({
                    title: "Tạo tài khoản thành công",
                    icon: "success",
                    draggable: true
                }).then(() => {
                    window.location.href = '/admin/account/login';
                });
            },
            error: function (xhr, status, error) {
                swal.fire({
                    icon: "error",
                    title: "oops...",
                    text: "lỗi không tạo được tài khoản" + xhr.responseText,
                    footer: '<a href="#">why do i have this issue?</a>'
                });
                console.error(error);
            }
        });
    }
    else console.log("Lỗi validate");
}

function validateInput() {
    $(".error-message").text("");
    let isValid = true;

    $(".validate-input").each(function () {
        if ($(this).val().trim() == "") {
            $(this).closest("fieldset").find(".error-message").text("Không được để trống")
            isValid = false;
        }
    });

    if ($("#password").val() != $("#repassword").val()) {
        $("#repassword").closest("fieldset").find(".error-message").text("Mật khẩu nhập lại không chính xác")
        isValid = false;
    }
    return isValid;
}

$("#repassword").change(function () {
    if ($("#password").val() != $("#repassword").val()) {
        $("#repassword").closest("fieldset").find(".error-message").text("Mật khẩu nhập lại không chính xác")
    }
    else {
        $("#repassword").closest("fieldset").find(".error-message").text("");
    }
});