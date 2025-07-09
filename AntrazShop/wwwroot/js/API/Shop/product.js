function addToCart(idProduct) {
    if (!token) {
        Swal.fire({
            title: "Bạn chưa đăng nhập?",
            text: "Vui lòng đăng nhập để thêm sản phẩm!",
            icon: "question"
        });
        return;
    }

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
            Swal.fire({
                position: "center",
                icon: "success",
                title: "Thêm sản phẩm vào giỏ hàng thành công!",
                showConfirmButton: false,
                timer: 1000
            });
            loadCartItem();
        },
        error: function (xhr, status, error) {

            handleAjaxError(xhr, status, error, "Lỗi khi thêm sản phẩm vào giỏ hàng");
        }
    });


}
