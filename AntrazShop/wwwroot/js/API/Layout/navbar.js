//chạy lúc load trang
$(function () {
    loadNavbar();
});

const token = localStorage.getItem('token');

function loadNavbar() {
    try {
        const decoded = jwt_decode(token); // Giải mã token
        console.log(decoded);
        localStorage.setItem('id-claim', decoded.Id);

        const now = Math.floor(Date.now() / 1000); // thời gian hiện tại (giây)

        if (decoded.exp < now) {
            document.getElementById('navbar-login').style.display = 'block';
            document.getElementById('navbar-account').style.display = 'none';
            document.getElementById('navbar-logout').style.display = 'none';
        }
        else {
            $('#claim-avatar').attr('src', `/admin/imgs/avatar/${decoded.Avatar}`);
            document.getElementById('navbar-login').style.display = 'none';
            document.getElementById('navbar-account').style.display = 'block';
            document.getElementById('navbar-logout').style.display = 'block';
        }
    } catch (error) {
        document.getElementById('navbar-login').style.display = 'block';
        document.getElementById('navbar-account').style.display = 'none';
        document.getElementById('navbar-logout').style.display = 'none';
    }
}

function logout() {
    localStorage.removeItem('token');
    window.location.href = '/admin/account/login';
}

function gotoAccount() {
    try {
        const decoded = jwt_decode(token); // Giải mã token
        const now = Math.floor(Date.now() / 1000); // thời gian hiện tại (giây)

        if (decoded.exp < now) {
            window.location.href = '/admin/account/login';
        }
        else {
            window.location.href = '/account/';        
        }
    } catch (error) {
        window.location.href = '/admin/account/login';
    }
}