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
            $("#cart-table-row").empty();

            if (!response || response.length === 0) {
                $("#cart-table-row").append('<tr><td colspan="8" class="text-center">Giỏ hàng trống</td></tr>');
                $("#total-price-cart-items-cart").text('0 VNĐ');
                return;
            }

            let totalPrice = 0;
            let totalDiscountAmount = 0;

            $.each(response, function (index, item) {
                totalPrice += item.total;
                totalDiscountAmount += (item.discountAmount * item.quantity);
                let colorCapacity = item.colorName + ' ' + item.capacityValue;

                let price = item.price.toLocaleString('vi-VN') + ' VNĐ';
                let discountAmount = ((item.discountAmount * item.quantity) || 0).toLocaleString('vi-VN') + ' VNĐ';
                let totalItemPrice = (item.total - (item.discountAmount * item.quantity)).toLocaleString('vi-VN') + ' VNĐ';

                $("#cart-table-row").append(`
                   <tr>
                       <td class="product-thumbnail">
                           <a href="/shop/productdetail?id=${item.productId}">
                               <img src="/admin/imgs/product/${item.folderImage}/${item.productImage}" alt="" style="width: 80px; height: 80px; object-fit: contain;">
                           </a>
                       </td>
                       <td class="product-name">
                           <a href="/shop/productdetail?id=${item.productId}">${item.productName}</a>
                       </td>
                       <td class="product-price">
                           <span class="amount">${colorCapacity}</span>
                       </td>
                       <td class="product-price">
                           <span class="amount">${price}</span>
                       </td>
                       <td class="product-price">
                           <span class="amount">${discountAmount}</span>
                       </td>
                       <td class="product-quantity">
                           <span class="cart-minus" onclick="decreaseQuantity(${item.colorCapacityId},${item.quantity})" style="cursor: pointer; padding: 5px;">-</span>
                           <input class="cart-input" type="text" value="${item.quantity}" onchange="setQuantity(${item.colorCapacityId}, this.value)" style="width: 50px; text-align: center;">
                           <span class="cart-plus" onclick="increaseQuantity(${item.colorCapacityId},${item.quantity})" style="cursor: pointer; padding: 5px;">+</span>
                       </td>
                       <td class="product-subtotal">
                           <span class="amount">${totalItemPrice}</span>
                       </td>
                       <td class="product-remove">
                           <a href="#" onclick="removeFromCart(${item.colorCapacityId})" style="color: red;"><i class="fa fa-times"></i></a>
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

function decreaseQuantity(ccId, currentQuantity) {
    currentQuantity--;
    setQuantity(ccId, currentQuantity);
}

function increaseQuantity(ccId, currentQuantity) {
    currentQuantity++;
    setQuantity(ccId, currentQuantity);
}


function setQuantity(ccId, quantity) {
    $.ajax({
        url: window.API_URL + `/Cart/update`,
        method: 'PUT',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: JSON.stringify({
            userId: userId,
            colorCapacityId: ccId,
            quantity: quantity
        }),
        success: function (response) {
            loadCartItemCart();
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi cập nhật giỏ hàng");
        }
    });
}

function removeFromCart(ccId) {
    $.ajax({
        url: window.API_URL + `/Cart/${userId}/${ccId}`,
        method: 'DELETE',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            loadCartItemCart();
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi xoá sản phẩm khỏi giỏ hàng");
        }
    });
}