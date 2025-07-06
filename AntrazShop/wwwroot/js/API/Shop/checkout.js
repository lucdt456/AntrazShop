$(function () {
    loadCartItemCart();
});

var token = localStorage.getItem('token');
var userId = localStorage.getItem('id-claim');


function loadCartItemCart() {

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
            $("#check-out-item").empty();

            let totalPrice = 0;
            let totalDiscountAmount = 0;

            $.each(response, function (index, item) {
                totalPrice += item.total;
                totalDiscountAmount += (item.discountAmount * item.quantity);
                let colorCapacity = item.colorName + ' ' + item.capacityValue;

                let price = item.price.toLocaleString('vi-VN') + ' VNĐ';
                let discountAmount = (item.discountAmount || 0).toLocaleString('vi-VN') + ' VNĐ';
                let totalItemPrice = item.total.toLocaleString('vi-VN') + ' VNĐ';

                $("#check-out-item").append(`
                   <tr class="cart_item">
                      <td class="product-name">
                        ${item.productName} <strong class="product-quantity"> × ${item.quantity}</strong>
                       </td>
                       <td class="product-total">
                         <span class="amount">${totalItemPrice}</span>
                       </td>
                  </tr>
               `);
            });

            $("#total-price-cart-items-cart").text(totalPrice.toLocaleString('vi-VN') + ' VNĐ');
            $("#total-discount-amount").text(totalDiscountAmount.toLocaleString('vi-VN') + ' VNĐ');
            $("#total-current-price").text((totalPrice - totalDiscountAmount).toLocaleString('vi-VN') + ' VNĐ');
        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    });
}

function checkOut() {
    let firstName = $('#first-name').val();
    let lastName = $('#last-name').val();
    let phoneNumber = $('#phone-number').val();
    let adress = $('#adress').val();


    if (!firstName || !lastName || !phoneNumber || !adress) {
        alert('Vui lòng nhập đầy đủ thông tin');
        return;
    }

    // Gửi dữ liệu (AJAX)
    $.ajax({
        url: window.API_URL + `/Cart/checkout/${userId}`,
        method: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: JSON.stringify({
            name: firstName + ' ' + lastName,
            phoneNumber: phoneNumber,
            address: adress
        }),
        success: function (response) {
            swal.fire({
                title: `Bạn đã đặt hàng thành công`,
                icon: "success",
                draggable: true
            }).then(() => {
                window.location.href = '/shop';
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi tạo đơn hàng");
        }
    });
}