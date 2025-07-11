function addToCart(idProduct) {

    const cartData = {
        "userId": localStorage.getItem('id-claim'),
        "colorCapacityId": idProduct,
        "quantity": 1
    };

    $.ajax({
        url: window.API_URL + `/Cart/add`,
        type: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        data: JSON.stringify(cartData),
        success: function (response) {
            swal.fire({
                title: "Bạn vừa thêm 1 sản phẩm vào giỏ hàng",
                icon: "success",
                draggable: true
            }).then(() => {
                loadCartItem();
            });
        },
        error: function (xhr, status, error) {

            let errorMessage = 'Error';

            try {
                const response = JSON.parse(xhr.responseText);
                if (response.errors && response.errors.length > 0) {
                    errorMessage = response.errors.join('\n');
                }
            } catch (e) {
                console.error("Lỗi phân tích phản hồi:", e);
            }

            swal.fire({
                icon: "error",
                title: "oops...",
                text: errorMessage,
                footer: '<a href="#">Tại sao tôi gặp lỗi này?</a>'
            });
            console.error(error);
        }
    });


}
