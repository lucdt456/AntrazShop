$(function () {
    getProduct();
});

var CURRENTPRODUCT;

function getProduct() {
    let urlParams = new URLSearchParams(window.location.search);
    let id = urlParams.get('id');

    if (id != null) {
        $("#id-product").append(id);

        $.ajax({
            url: `https://localhost:7092/api/Product/${id}`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            dataType: 'json',
            success: function (product) {
                CURRENTPRODUCT = product;
                console.log(CURRENTPRODUCT);
                loadDataProcduct();

                $("#current-productCC").empty();

                $.each(CURRENTPRODUCT.productCCs, function (index, productCC) {
                    let statusCC = '';

                    switch (productCC.status) {
                        case 0:
                            statusCC = 'Ngừng bán';
                            break;
                        case 1:
                            statusCC = 'Đang bán';
                            break;
                        default:
                            statusCC = "---";
                    }

                    if (productCC.stock == 0) statusCC = 'Hết hàng';

                    let productCCName = productCC.colorName + ' - ' + productCC.capacityValue + ' - ' + statusCC;

                    $("#current-productCC").append(
                        `<div style="border: 1px solid #ddd; padding: 10px; border-radius: 5px; cursor: pointer; text-align: center; min-width: 120px;" onclick="selectOption(this, ${productCC.id})">
                           <small>${productCCName}</small>
                       </div>`
                    );
                });
            },
            error: function (xhr, status, error) {
                alert("Lỗi khi lấy sản phẩm: " + xhr.responseText);
                console.error(error);
            }
        });
    }
}

function selectOption(element, idProductCC) {
    // Reset tất cả options trong phần phiên bản
    const parent = element.parentElement;
    parent.querySelectorAll('div').forEach(div => {
        div.style.border = '1px solid #ddd';
        div.style.backgroundColor = 'white';
        const x = div.querySelector('span');
        if (x) x.remove();
    });

    // Set selected
    element.style.border = '2px solid #007bff';
    element.style.backgroundColor = '#f0f8ff';
    element.innerHTML += '<span style="position: absolute; top: -8px; right: -8px; background: #007bff; color: white; border-radius: 50%; width: 16px; height: 16px; font-size: 12px; display: flex; align-items: center; justify-content: center;">✓</span>';
    ProductCCTargetId = idProductCC;
    loadDataProcductCC();
}

var ProductCCTargetId;
function loadDataProcductCC() {
    let productCC = CURRENTPRODUCT.productCCs.find(p => p.id == ProductCCTargetId);

    $("#current-product-sale-price").text(`${(productCC.price - CURRENTPRODUCT.discountAmount).toLocaleString('vi-VN')} VNĐ`);
    $("#current-product-price").text(`${productCC.price.toLocaleString('vi-VN')} VNĐ`);
    $("#current-product-stock").text(`${productCC.stock} sản phẩm`);
    $("#current-product-soldAmount").text(`${productCC.soldAmount} sản phẩm`);
    $("#current-product-image").attr("src", `/admin/imgs/product/${CURRENTPRODUCT.folderImage}/${productCC.image}`).show();
}

function loadDataProcduct() {
    $("#current-product-sale-price").text(`${(CURRENTPRODUCT.minPrice - CURRENTPRODUCT.discountAmount).toLocaleString('vi-VN')} ~ ${(CURRENTPRODUCT.maxPrice - CURRENTPRODUCT.discountAmount).toLocaleString('vi-VN')} VNĐ`);
    $("#current-product-price").text(`${CURRENTPRODUCT.minPrice.toLocaleString('vi-VN')} ~ ${CURRENTPRODUCT.maxPrice.toLocaleString('vi-VN')} VNĐ`);
    $("#current-product-name").text(CURRENTPRODUCT.name);
    $("#current-product-brand").text(CURRENTPRODUCT.brand);
    $("#current-product-category").text(CURRENTPRODUCT.category);
    $("#current-product-stock").text(`${CURRENTPRODUCT.totalStock} sản phẩm`);
    $("#current-product-soldAmount").text(`${CURRENTPRODUCT.soldAmount} sản phẩm`);

    let totalReview = 0;
    $.each(CURRENTPRODUCT.productCCs, (i, p) => totalReview += p.reviews.length);

    $(".current-product-reviews-count").text(`(${totalReview})`);
    $("#current-product-description").text(CURRENTPRODUCT.description);

    $("#current-product-image").attr("src", `/admin/imgs/product/${CURRENTPRODUCT.folderImage}/${CURRENTPRODUCT.imageView}`).show();

    let starRate = '';
    let rate = Math.round(CURRENTPRODUCT.rating * 2) / 2;
    if (Number.isInteger(rate)) {
        // Sao đầy
        for (let i = 1; i <= rate; i++) {
            starRate = starRate + '<a href="#"><i class="bi bi-star-fill"></i></a> ';
        }
        // Sao rỗng
        for (let j = 1; j <= (5 - rate); j++) {
            starRate = starRate + '<a href="#"><i class="bi bi-star"></i></a> ';
        }
    } else {
        const fullStars = Math.floor(rate); // 4.5 → 4

        // Sao đầy
        for (let i = 1; i <= fullStars; i++) {
            starRate = starRate + '<a href="#"><i class="bi bi-star-fill"></i></a> ';
        }
        // Nửa sao
        starRate = starRate + '<a href="#"><i class="bi bi-star-half"></i></a> ';
        // Sao rỗng
        for (let j = 1; j <= (5 - fullStars - 1); j++) {
            starRate = starRate + '<a href="#"><i class="bi bi-star"></i></a> ';
        }
    }

    $("#current-product-rating").html(starRate);
}

function addProductDetailToCart() {
    if (!ProductCCTargetId) {
        Swal.fire({
            title: "Chưa chọn phân loại?",
            text: "Bạn chưa chọn phân loại nào cả!",
            icon: "question"
        });
        return;
    }
    let cartDataRequest = {
        "userId": localStorage.getItem('id-claim'),
        "colorCapacityId": ProductCCTargetId,
        "quantity": $("#current-productCC-quantity").val()
    };

    $.ajax({
        url: window.API_URL + `/Cart/add`,
        type: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        data: JSON.stringify(cartDataRequest),
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