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
            url: window.API_URL + '/Account/Login',
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
                handleAjaxError(xhr, status, error, "Đăng nhập không thành công!");
            }
        });
    });
});
