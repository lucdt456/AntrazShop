//chạy lúc load trang
$(function () {
    loadNavbar();
    loadCartItem();
});


var CART_ITEMS = [];
var token = localStorage.getItem('token');

function loadNavbar() {
    try {
        const decoded = jwt_decode(token); // Giải mã token
        console.log(decoded);
        localStorage.setItem('id-claim', decoded.Id);

        const now = Math.floor(Date.now() / 1000); // thời gian hiện tại (giây)

        if (decoded.exp < now) {
            document.getElementById('navbar-login').style.display = 'block';
            document.getElementById('navbar-account').style.display = 'none';
        }
        else {
            $('#claim-avatar').attr('src', `/admin/imgs/avatar/${decoded.Avatar}`);
            document.getElementById('navbar-login').style.display = 'none';
            document.getElementById('navbar-account').style.display = 'block';
        }
    } catch (error) {
        document.getElementById('navbar-login').style.display = 'block';
        document.getElementById('navbar-account').style.display = 'none';
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

function loadCartItem() {
    let token = localStorage.getItem('token');
    let userId = localStorage.getItem('id-claim');
    if (userId == null) return;
    $.ajax({
        url: window.API_URL + `/Cart/${userId}`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            CART_ITEMS = response;
            $(".span-cart-item-count").text(response.length);
            $("#list-cart-items").empty();
            let totalPrice = 0;
            $.each(CART_ITEMS, function (index, item) {
                totalPrice += item.total;

                let price = item.price.toLocaleString('vi-VN') + ' VNĐ';
                $("#list-cart-items").append(
                    `
                    <li>
                         <div class="tpcart__item">
                                <div class="tpcart__img">
                                    <img src="/admin/imgs/product/${item.folderImage}/${item.productImage}" alt="">
                                    <div class="tpcart__del">
                                        <a href="#"><i class="icon-x-circle"></i></a>
                                    </div>
                                </div>
                                <div class="tpcart__content">
                                    <span class="tpcart__content-title">
                                        <a href="shop-details.html">${item.productName}</a>
                                    </span>
                                    <div class="tpcart__cart-price">
                                        <span class="quantity">${item.quantity}x</span>
                                        <span class="new-price">${price}</span>
                                    </div>
                                </div>
                            </div>
                        </li>
                    `
                )
                $("#total-price-cart-items").text(totalPrice.toLocaleString('vi-VN') + ' VNĐ');
            });
        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    })
}