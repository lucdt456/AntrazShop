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
                $('#list-product-color-capacity').empty();
                $('#list-product-color-capacity')
                    .append('<option value="0">Chọn sản phẩm</option>');

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


                    $('#list-product-color-capacity')
                        .append(`<option value="${productCC.id}">${productCCName}</option>`);

                });
                $('#list-product-color-capacity').niceSelect('update');
                loadReviews(100);
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

    let starRate = formatRatingStar(CURRENTPRODUCT.rating);
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

    if (!token) {
        Swal.fire({
            title: "Bạn chưa đăng nhập?",
            text: "Vui lòng đăng nhập để thêm sản phẩm!",
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

function loadReviews(star) {
    let reviews = [];
    // Thu thập tất cả reviews từ các productCC
    switch (star) {
        case 1: {
            $.each(CURRENTPRODUCT.productCCs, function (index, productCC) {
                $.each(productCC.reviews, function (index, review) {
                    if (review.rating == 1) { 
                        reviews.push(review);
                    }
                });
            });
            break;
        }
        case 2: {
            $.each(CURRENTPRODUCT.productCCs, function (index, productCC) {
                $.each(productCC.reviews, function (index, review) {
                    if (review.rating == 2) {
                        reviews.push(review);
                    }
                });
            });
            break;
        }
        case 3: {
            $.each(CURRENTPRODUCT.productCCs, function (index, productCC) {
                $.each(productCC.reviews, function (index, review) {
                    if (review.rating == 3) {
                        reviews.push(review);
                    }
                });
            });
            break;
        }
        case 4: {
            $.each(CURRENTPRODUCT.productCCs, function (index, productCC) {
                $.each(productCC.reviews, function (index, review) {
                    if (review.rating == 4) {
                        reviews.push(review);
                    }
                });
            });
            break;
        }
        case 5: {
            $.each(CURRENTPRODUCT.productCCs, function (index, productCC) {
                $.each(productCC.reviews, function (index, review) {
                    if (review.rating == 5) {
                        reviews.push(review);
                    }
                });
            });
            break;
        }
        default: {
            $.each(CURRENTPRODUCT.productCCs, function (index, productCC) {
                $.each(productCC.reviews, function (index, review) {
                    reviews.push(review);
                });
            });
            break;
        }
    }

    

    reviews.sort(function (a, b) {
        return new Date(b.createdAt) - new Date(a.createdAt);
    });


    // Xóa danh sách reviews cũ
    $('#list-reviews').empty();

    // Render từng review
    $.each(reviews, function (index, review) {
        let formatCreateTime = formatDateTime(review.createdAt);
        let formatRating = formatRatingStar(review.rating);

        $("#list-reviews").append(`
            <div class="tpreview__comment">
                <div class="tpreview__comment-img mr-20">
                    <img src="/admin/imgs/avatar/lucdt456gmailcom.png" 
                         alt="Avatar" 
                         style="width: 70px; height: 70px; object-fit: cover; border-radius: 50%;">
                </div>
                <div class="tpreview__comment-text">
                    <div class="tpreview__comment-autor-info d-flex align-items-center justify-content-between">
                        <div class="tpreview__comment-author">
                            <span>${review.userName}</span>
                        </div>
                        <div class="tpreview__comment-star">
                            ${formatRating}
                        </div>
                    </div>
                    <span class="date mb-20">--${formatCreateTime}</span>
                    <p>${review.description}</p>
                </div>
            </div>
        `);
    });
}
function formatDateTime(dateString) {
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');

    return `${day}/${month}/${year} ${hours}:${minutes}`;
}

function formatRatingStar(rating) {
    let starRate = '';
    let rate = Math.round(rating * 2) / 2;
    if (Number.isInteger(rate)) {
        // Sao đầy
        for (let i = 1; i <= rate; i++) {
            starRate = starRate + '<a><i class="bi bi-star-fill"></i></a> ';
        }
        // Sao rỗng
        for (let j = 1; j <= (5 - rate); j++) {
            starRate = starRate + '<a><i class="bi bi-star"></i></a> ';
        }
    } else {
        const fullStars = Math.floor(rate); // 4.5 → 4

        // Sao đầy
        for (let i = 1; i <= fullStars; i++) {
            starRate = starRate + '<a><i class="bi bi-star-fill"></i></a> ';
        }
        // Nửa sao
        starRate = starRate + '<a><i class="bi bi-star-half"></i></a> ';
        // Sao rỗng
        for (let j = 1; j <= (5 - fullStars - 1); j++) {
            starRate = starRate + '<a"><i class="bi bi-star"></i></a> ';
        }
    }
    return starRate;
}
var CURRENT_STAR_REVIEW = 5;
// Function chung để set stars
function setStarRating(rating) {
    for (let i = 1; i <= 5; i++) {
        const starId = getStarId(i);
        if (i <= rating) {
            $(starId).html(`<i class="bi bi-star-fill"></i>`);
        } else {
            $(starId).html(`<i class="bi bi-star"></i>`);
        }
    }
    CURRENT_STAR_REVIEW = rating;
}

// Function để get star ID (vì bạn dùng 1st, 2nd, 3th, 4th, 5th)
function getStarId(index) {
    const suffixes = ['', '1st', '2nd', '3th', '4th', '5th'];
    return `#${suffixes[index]}-star`;
}

// Event listeners ngắn gọn
$('#1st-star').on('click', () => setStarRating(1));
$('#2nd-star').on('click', () => setStarRating(2));
$('#3th-star').on('click', () => setStarRating(3));
$('#4th-star').on('click', () => setStarRating(4));
$('#5th-star').on('click', () => setStarRating(5));

function addNewReview() {
    let userId = localStorage.getItem('id-claim');
    let colorCapacityId = $('#list-product-color-capacity').val();
    let description = $('#description-review').val();
    let rating = CURRENT_STAR_REVIEW;

    if (!userId) {
        Swal.fire({
            title: "Chưa đăng nhập",
            text: "Vui lòng đăng nhập để đánh giá!",
            icon: "question"
        });
        return;
    }
    if (colorCapacityId == 0) {
        Swal.fire({
            title: "Chưa chọn phân loại",
            text: "Bạn chưa chọn phân loại nào cả!",
            icon: "question"
        });
        return;
    }
    if (!description) {
        Swal.fire({
            title: "Chưa điền đánh giá",
            text: "Hãy điền nhận xét của bạn",
            icon: "question"
        });
        return;
    }

    $.ajax({
        url: window.API_URL + `/Product/Review`,
        method: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: JSON.stringify({
            userId: userId,
            colorCapacityId: colorCapacityId,
            description: description,
            rating: rating
        }),
        success: function (response) {
            Swal.fire({
                position: "center",
                icon: "success",
                title: "Bạn vừa đánh giá sản phẩm!",
                showConfirmButton: false,
                timer: 1000
            });
            getProduct();
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi tạo đơn hàng");
        }
    });
}

$('#all-star').on('click', function () {
    setActiveStarButton('#all-star');
    loadReviews(100);
});

$('#1-stars').on('click', function () {
    setActiveStarButton('#1-stars');
    loadReviews(1);
});

$('#2-stars').on('click', function () {
    setActiveStarButton('#2-stars');
    loadReviews(2);
});

$('#3-stars').on('click', function () {
    setActiveStarButton('#3-stars');
    loadReviews(3);
});

$('#4-stars').on('click', function () {
    setActiveStarButton('#4-stars');
    loadReviews(4);
});

$('#5-stars').on('click', function () {
    setActiveStarButton('#5-stars');
    loadReviews(5);
});

// Set mặc định "Tất cả" được chọn khi load trang
$(document).ready(function () {
    setActiveStarButton('#all-star');
});

function resetStarButtons() {
    $('#all-star, #1-stars, #2-stars, #3-stars, #4-stars, #5-stars').css({
        'background-color': 'lightgreen',
        'color': 'black'
    }); 
}

function setActiveStarButton(buttonId) {
    resetStarButtons();
    $(buttonId).css({
        'background-color': '#007bff',
        'color': 'white'
    });
}
