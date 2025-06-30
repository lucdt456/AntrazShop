$(function () {
    pager.numberShowing = 12;
    loadData();
});

var ProductList = [];

var SwitchListView = 2;

function loadData() {
    let Filter = {
        brandIds: FListBrandIds,
        categoryIds: FListCategoryIds,
        searchText: FSearchText,
        minPrice: FPrice.Min,
        maxPrice: FPrice.Max,
        ascendingPrice: null
    };

    window.scrollTo(0, 0);

    pager.numberShowing = $("#numberShowing").val();

    $.ajax({
        url: window.API_URL + `/Shop/Products?page=${pager.currentPage}&size=${pager.numberShowing}`,
        type: 'Post',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        data: JSON.stringify(Filter),
        contentType: 'application/json',
        success: function (response) {
            console.log(response);

            pager.totalPage = response.pagination.totalPage;
            $("#totalItem").text(`Có ${response.pagination.totalItems} sản phẩm`);
            $('#pageNumber li:nth-child(n+3):nth-last-child(n+3)').remove();

            for (let i = response.pagination.endPage; i >= response.pagination.startPage; i--) {
                let active = '';
                if (i == response.pagination.currentPage) {
                    active = 'current';
                }
                $("#pageNumber").children("li:nth-child(2)").after(
                    ` 
                    <li>
                        <a style="cursor: pointer;" class="${active} page-number-link" >${i}</a>
                    </li>
                   `
                )
            }


            ProductList = response.products;
            setPaginationButtonStyle();

            switch (SwitchListView) {
                case 1:
                    loadProducts1();
                    break;
                case 3:
                    loadProducts3();
                    break;
                default:
                    loadProducts2();
                    break;
            }
        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    });
}

function setSwitchListView1() {
    SwitchListView = 1;
    loadData();
}

function setSwitchListView2() {
    SwitchListView = 2;
    loadData();
}

function setSwitchListView3() {
    SwitchListView = 3;
    loadData();
}



function loadProducts2() {
    $("#list-product-2").empty();

    $.each(ProductList, function (index, product) {
       let starRate = '';

        let rate = Math.round(product.rating * 2) / 2;
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

        let min_price_sale = (product.minPrice - product.discountAmount).toLocaleString('vi-VN') + ' đ';
        let min_price = product.minPrice.toLocaleString('vi-VN') + ' đ';

        $("#list-product-2").append(`
            <div class="col">
                <div class="tpproduct p-relative mb-20">
                    <div class="tpproduct__thumb p-relative text-center">
                        <a href="#">
                            <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}" style="max-width: 308px; max-height: 308px; width: auto; height: auto; object-fit: contain;" alt="">
                        </a>
                        <a class="tpproduct__thumb-img" href="/shop/productdetail?id=${product.id}">
                            <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}" style="max-width: 308px; max-height: 308px; width: auto; height: auto; object-fit: contain;" alt="">
                            <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}"  alt="">
                        </a>
                        <div class="tpproduct__info bage">
                            <span class="tpproduct__info-discount bage__discount">-${product.discountAmount}</span>
                            <span class="tpproduct__info-hot bage__hot">HOT</span>
                        </div>
                        <div class="tpproduct__shopping">
                            <a class="tpproduct__shopping-wishlist" href="wishlist.html">
                                <i class="icon-heart icons"></i>
                            </a>
                            <a class="tpproduct__shopping-cart"href="/shop/productdetail?id=${product.id}">
                                <i class="icon-eye"></i>
                            </a>
                        </div>
                    </div>
                    <div class="tpproduct__content">
                        <span class="tpproduct__content-weight">
                            <a href="shop-details-3.html">${product.brand}</a>,
                            <a href="shop-details-3.html">${product.category}</a>
                        </span>
                        <h4 class="tpproduct__title">
                            <a href="shop-details-top-.html">${product.name}</a>
                        </h4>
                        <div class="tpproduct__rating mb-5">
                            ${starRate}
                        </div>
                        <div class="tpproduct__price">
                            <span>${min_price_sale}</span>
                            <del>${min_price}</del>
                        </div>
                    </div>
                    <div class="tpproduct__hover-text">
                        <div class="tpproduct__hover-btn d-flex justify-content-center mb-10">
                            <button class="tp-btn-2" onclick="addToCart(${product.productCCs[0].id})">Thêm vào giỏ</button>
                        </div>
                        <div class="tpproduct__descrip">
                            <ul>
                                <li>Đã bán ${product.soldAmount}</li>
                                <li>Tồn kho: ${product.totalStock} </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        `);
    });
}

function loadProducts1() {
    $("#list-product-1").empty();

    $.each(ProductList, function (index, product) {
        let starRate = '';

        let rate = Math.round(product.rating * 2) / 2;
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

        let mbClass = (index < 4) ? "mb-20" : ""; 

        let min_price_sale = (product.minPrice - product.discountAmount).toLocaleString('vi-VN') + ' đ';
        let min_price = product.minPrice.toLocaleString('vi-VN') + ' đ';

        $("#list-product-1").append(`
            <div class="col target-product">
             <input type="hidden" class="hidden-id" value="${product.id}" />
                <div class="tpproduct p-relative ${mbClass}" style="z-index: ${6 - index};">
                    <div class="tpproduct__thumb p-relative text-center">
                        <a href="#">
                            <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}" alt="">
                        </a>
                        <a class="tpproduct__thumb-img" href="/shop/productdetail?id=${product.id}">
                            <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}" alt="">
                        </a>
                        <div class="tpproduct__info bage">
                            <span class="tpproduct__info-discount bage__discount">-${product.discountAmount}</span>
                            <span class="tpproduct__info-hot bage__hot">HOT</span>
                        </div>
                        <div class="tpproduct__shopping">
                            <a class="tpproduct__shopping-wishlist" href="wishlist.html">
                                <i class="icon-heart icons"></i>
                            </a>
                            <a class="tpproduct__shopping-cart" href="/shop/productdetail?id=${product.id}">
                                <i class="icon-eye"></i>
                            </a>
                        </div>
                    </div>
                    <div class="tpproduct__content">
                        <span class="tpproduct__content-weight">
                            <a href="shop-details-3.html">${product.brand}</a>,
                            <a href="shop-details-3.html">${product.category}</a>
                        </span>
                        <h4 class="tpproduct__title">
                            <a href="shop-details-top-.html">${product.name}</a>
                        </h4>
                        <div class="tpproduct__rating mb-5">
                            ${starRate}
                        </div>
                        <div class="tpproduct__price">
                            <span>${min_price_sale}</span>
                            <del>${min_price}</del>
                        </div>
                    </div>
                    <div class="tpproduct__hover-text" style="z-index: 999;">
                        <div class="tpproduct__hover-btn d-flex justify-content-center mb-10">
                            <button class="tp-btn-2"  onclick="addToCart(${product.productCCs[0].id})">Thêm vào giỏ</button>
                        </div>
                        <div class="tpproduct__descrip">
                            <ul>
                                <li>Đã bán ${product.soldAmount}</li>
                                <li>Tồn kho: ${product.totalStock} </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        `);
    });
}


function loadProducts3() {
    $("#list-product-3").empty();
    $.each(ProductList, function (index, product) {

        let starRate = '';

        let rate = Math.round(product.rating * 2) / 2;
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

        let min_price_sale = (product.minPrice - product.discountAmount).toLocaleString('vi-VN') + ' đ';
        let min_price = product.minPrice.toLocaleString('vi-VN') + ' đ';

        $("#list-product-3").append(`
            <div class="col-lg-12" target-product>
                <div class="tplist__product d-flex align-items-center justify-content-between mb-20">
                 <input type="hidden" class="hidden-id" value="${product.id}" />
                    <div class="tplist__product-img">
                        <a href="#" class="tplist__product-img-one">
                             <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}" alt="" style="max-width: 221px; max-height: 210px; width: auto; height: auto; object-fit: contain;">
                        </a>
                        <a class="tplist__product-img-two" href="/shop/productdetail?id=${product.id}">
                             <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}" alt="" style="max-width: 221px; max-height: 210px; width: auto; height: auto; object-fit: contain;">
                        </a>
                        <div class="tpproduct__info bage">
                            <span class="tpproduct__info-discount bage__discount">-${product.discountAmount}</span>
                            <span class="tpproduct__info-hot bage__hot">HOT</span>
                        </div>
                    </div>
                    <div class="tplist__content">
                        <span>Đã bán ${product.soldAmount}</span>
                        <h4 class="tplist__content-title">
                            <a href="#">${product.name}</a>
                        </h4>
                        <div class="tplist__rating mb-5">
                            ${starRate}
                        </div>
                        <ul class="tplist__content-info">
                            <li>Tồn kho: ${product.totalStock} </li>
                        </ul>
                    </div>
                    <div class="tplist__price justify-content-end">
                        <h4 class="tplist__instock">
                            Availability: <span>92 in stock</span>
                        </h4>
                        <h3 class="tplist__count mb-15">${min_price_sale}</del></h3>
                        <button class="tp-btn-2 mb-10" onclick="addToCart(${product.productCCs[0].id})">Thêm vào giỏ</button>
                        <div class="tplist__shopping">
                            <a href="#"><i class="icon-heart icons"></i> wishlist</a>
                            <a href="#"><i class="icon-layers"></i>Compare</a>
                        </div>
                    </div>
                </div>
            </div>
        `);
    });
}