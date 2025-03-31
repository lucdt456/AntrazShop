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
                console.log('Đăng nhập thành công');
            },
            error: function (xhr, status, error) {
                // Kiểm tra mã lỗi và thông báo
                if (xhr.status === 401) {
                    const errorMessage = xhr.responseText || 'Đăng nhập thất bại. Vui lòng kiểm tra lại email và mật khẩu.';
                    alert(errorMessage);
                } else {
                    console.error('Lỗi hệ thống: ' + error);
                }
            }
        });
    });
});
