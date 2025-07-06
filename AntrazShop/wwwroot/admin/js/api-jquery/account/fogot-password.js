$(document).ready(function () {
    $('#form-sent-code').on('submit', function (e) {
        e.preventDefault(); // Ngăn form submit mặc định

        // Lấy giá trị từ các input
        let email = $('#email').val().trim();

        // Kiểm tra dữ liệu (nếu muốn)
        if (!email) {
            alert('Vui lòng nhập đầy đủ email');
            return;
        }

        // Gửi dữ liệu (AJAX)
        $.ajax({
            url: window.API_URL + `/Account/forgot-password`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ email: email }),
            success: function (response) {
                swal.fire({
                    title: `${response}`,
                    icon: "success",
                    draggable: true
                }).then(() => {
                    $('#form-sent-code').hide();
                    $('#form-verify-code').fadeIn();
                });
            },
            error: function (xhr, status, error) {
                handleAjaxError(xhr, status, error, "Lỗi khi gửi mã xác nhận");
            }
        });
    });

    var token = localStorage.getItem('token');
    
    $('#form-verify-code').on('submit', function (e) {
        e.preventDefault(); // Ngăn form submit mặc định

        // Lấy giá trị từ các input
        let email = $('#email').val().trim();
        let code = $('#code').val().trim();

        // Kiểm tra dữ liệu (nếu muốn)
        if (!code) {
            alert('Vui lòng nhập mã xác nhận');
            return;
        }

        // Gửi dữ liệu (AJAX)
        $.ajax({
            url: window.API_URL + `/Account/verify-code`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                email: email,
                code: code
            }),
            success: function (response) {
                swal.fire({
                    title: `${response}`,
                    icon: "success",
                    draggable: true
                }).then(() => {
                    window.location.href = '/admin/account/login';
                });
            },
            error: function (xhr, status, error) {
                handleAjaxError(xhr, status, error, "Lỗi xác nhận");
            }
        });
    });
});
