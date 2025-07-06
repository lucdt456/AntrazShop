$(function () {
    loadData();
});

var ProductList = [];

function loadData() {
    $.ajax({
        url: window.API_URL + `/Product?page=1&size=10`,
        type: 'Get',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            console.log(response);
            ProductList = response.products;
            loadProduct();
        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    });
}

function loadProduct() {
    $("#all-product-list").empty();

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

        //$("#all-product-list").append(`
        //    <div class="col target-product">
        //     <input type="hidden" class="hidden-id" value="${product.id}" />
        //        <div class="tpproduct p-relative ${mbClass}" style="z-index: ${6 - index};">
        //            <div class="tpproduct__thumb p-relative text-center">
        //                <a href="#">
        //                    <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}" alt="">
        //                </a>
        //                <a class="tpproduct__thumb-img" href="/shop/productdetail?id=${product.id}">
        //                    <img src="/admin/imgs/product/${product.folderImage}/${product.imageView}" alt="">
        //                </a>
        //                <div class="tpproduct__info bage">
        //                    <span class="tpproduct__info-discount bage__discount">-${product.discountAmount}</span>
        //                    <span class="tpproduct__info-hot bage__hot">HOT</span>
        //                </div>
        //                <div class="tpproduct__shopping">
        //                    <a class="tpproduct__shopping-wishlist" href="wishlist.html">
        //                        <i class="icon-heart icons"></i>
        //                    </a>
        //                    <a class="tpproduct__shopping-cart" href="/shop/productdetail?id=${product.id}">
        //                        <i class="icon-eye"></i>
        //                    </a>
        //                </div>
        //            </div>
        //            <div class="tpproduct__content">
        //                <span class="tpproduct__content-weight">
        //                    <a href="shop-details-3.html">${product.brand}</a>,
        //                    <a href="shop-details-3.html">${product.category}</a>
        //                </span>
        //                <h4 class="tpproduct__title">
        //                    <a href="shop-details-top-.html">${product.name}</a>
        //                </h4>
        //                <div class="tpproduct__rating mb-5">
        //                    ${starRate}
        //                </div>
        //                <div class="tpproduct__price">
        //                    <span>${min_price_sale}</span>
        //                    <del>${min_price}</del>
        //                </div>
        //            </div>
        //            <div class="tpproduct__hover-text" style="z-index: 999;">
        //                <div class="tpproduct__hover-btn d-flex justify-content-center mb-10">
        //                    <button class="tp-btn-2"  onclick="addToCart(${product.productCCs[0].id})">Thêm vào giỏ</button>
        //                </div>
        //                <div class="tpproduct__descrip">
        //                    <ul>
        //                        <li>Đã bán ${product.soldAmount}</li>
        //                        <li>Tồn kho: ${product.totalStock} </li>
        //                    </ul>
        //                </div>
        //            </div>
        //        </div>
        //    </div>
        //`);
        $("#all-product-list").append(`
   <div class="swiper-slide">
       <input type="hidden" class="hidden-id" value="${product.id}" />
       <div class="tpproduct p-relative">
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
           <div class="tpproduct__hover-text">
               <div class="tpproduct__hover-btn d-flex justify-content-center mb-10">
                   <button class="tp-btn-2" onclick="addToCart(${product.productCCs[0].id})">Thêm vào giỏ</button>
               </div>
               <div class="tpproduct__descrip">
                   <ul>
                       <li>Đã bán ${product.soldAmount}</li>
                       <li>Tồn kho: ${product.totalStock}</li>
                   </ul>
               </div>
           </div>
       </div>
   </div>
`);

    });
}

