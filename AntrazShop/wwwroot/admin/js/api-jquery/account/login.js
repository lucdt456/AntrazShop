$(document).ready(function () {
    $('.form-login').on('submit', function (e) {
        e.preventDefault(); // Ngăn form submit mặc định

        // Lấy giá trị từ các input
        let email = $('#email').val().trim();
        let password = $('#password').val().trim();
        let rememberMe = $('#signed').is(':checked');

        // Kiểm tra dữ liệu (nếu muốn)
        if (!email || !password) {
            alert('Vui lòng nhập đầy đủ email và mật khẩu.');
            return;
        }

        // Gửi dữ liệu (AJAX)
        $.ajax({
            url: 'https://localhost:7092/api/Account/Login',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                email: email,
                password: password
            }),
            success: function (response) {

                swal.fire({
                    title: "Đăng nhập thành công",
                    icon: "success",
                    draggable: true
                }).then(() => {
                    if (response.token) {
                        localStorage.setItem('token', response.token); // Lưu token vào localStorage
                        let decoded = jwt_decode(response.token);
                        if (decoded.IsWorkerAccount == "True") {
                            window.location.href = '/admin/dashboard';
                        }
                        else window.location.href = '/';
                    }
                });
            },
            error: function (xhr, status, error) {

                let errorMessage = 'Đăng nhập thất bại.';

                try {
                    const response = JSON.parse(xhr.responseText);
                    if (response.errors && response.errors.length > 0) {
                        errorMessage = response.errors.join('\n');
                    }
                } catch (e) {
                    console.error("Lỗi phân tích phản hồi:", e);
                }

                swal.fire({
                    icon: "error",
                    title: "oops...",
                    text: errorMessage,
                    footer: '<a href="#">Tại sao tôi gặp lỗi này?</a>'
                });
                console.error(error);
            }
        });
    });
});
