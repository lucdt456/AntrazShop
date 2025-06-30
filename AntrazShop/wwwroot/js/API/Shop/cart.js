$(function () {
    loadCartItem();
});

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
            console.log(response);
            $(".span-cart-item-count").text(response.length);
            $("#list-cart-items").empty();
            let totalPrice = 0;
            $.each(response, function (index, item) {
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