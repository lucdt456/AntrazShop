//chạy lúc load trang
$(function () {
});

const token = localStorage.getItem('token');

function logout() {
    localStorage.removeItem('token');
    window.location.href = '/admin/account/login';
}