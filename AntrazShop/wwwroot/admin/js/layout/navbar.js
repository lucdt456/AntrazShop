//chạy lúc load trang
$(function () {
    checkAuth();
    loadNavbar();
});

const token = localStorage.getItem('token');

function logout() {
    localStorage.removeItem('token');
    window.location.href = '/admin/account/login';
}

function checkAuth() {
    if (!token) {
        // Không có token → chuyển về login
        window.location.href = '/admin/account/login';
        return;
    }

    try {
        const decoded = jwt_decode(token);  // Giải mã token

        const now = Math.floor(Date.now() / 1000); // thời gian hiện tại (giây)

        if (decoded.exp < now) {
            // Token hết hạn
            localStorage.removeItem('token');
            window.location.href = '/admin/account/login';
        }
    } catch (error) {
        // Token sai định dạng, hoặc lỗi decode
        console.error('Token không hợp lệ:', error);
        localStorage.removeItem('token');
        window.location.href = '/admin/account/login';
    }
}

function loadNavbar() {
    const decoded = jwt_decode(token); // Giải mã token
    $('#claim-name').text(decoded.Name);
    localStorage.setItem('id-claim', `${decoded.Id}`);
    $('#claim-avatar').attr('src', `/admin/imgs/avatar/${decoded.Avatar}`);

    if (decoded.IsWorkerAccount != "True") {
        logout();
    }
}