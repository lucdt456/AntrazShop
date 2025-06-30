//chạy lúc load trang
$(function () {
    setting();
    loadBrandData();
    loadCategoryData();
});

//Biến thông số cho việc lọc dữ liệu
var FPrice = {
    Min: 0,
    Max: 100000000
}

var FListBrandIds = [];
var FListCategoryIds = [];

var FSearchText = '';

function setting() {
    // Khi "Tất cả" của brand được click
    $('#check-all-brand').on('change', function () {
        const brandChecked = $(this).is(':checked');
        $('.brand-check').prop('checked', brandChecked);

        FListBrandIds = [];

        $('.brand-check:checked').each(function () {
            const value = parseInt($(this).val());
            if (!isNaN(value)) {
                FListBrandIds.push(value);
            }
        });
        loadData();
    });

    // Khi một thương hiệu được bỏ tích → bỏ "Tất cả"
    $(document).on('change', '.brand-check', function () {
        if (!$(this).is(':checked')) {
            $('#check-all-brand').prop('checked', false);
        }

        FListBrandIds = [];

        $('.brand-check:checked').each(function () {
            const value = parseInt($(this).val());
            if (!isNaN(value)) {
                FListBrandIds.push(value);
            }
        });
        loadData();
    });

    // Khi "Tất cả" của category được click
    $('#check-all-categtory').on('change', function () {
        const categoryChecked = $(this).is(':checked');
        $('.category-check').prop('checked', categoryChecked);

        FListCategoryIds = [];

        $('.category-check:checked').each(function () {
            const value = parseInt($(this).val());
            if (!isNaN(value)) {
                FListCategoryIds.push(value);
            }
        });
        loadData();
    });

    // Khi một danh mục được bỏ tích → bỏ "Tất cả"
    $(document).on('change', '.category-check', function () {
        if (!$(this).is(':checked')) {
            $('#check-all-categtory').prop('checked', false);
        }

        FListCategoryIds = [];

        $('.category-check:checked').each(function () {
            const value = parseInt($(this).val());
            if (!isNaN(value)) {
                FListCategoryIds.push(value);
            }
        });
        loadData();
    });
}

function loadCategoryData() {
    $.ajax({
        url: window.API_URL + `/Category`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response)
            $("#categories-list").empty();
            FListCategoryIds = [];

            let total = 0;

            $.each(response, function (index, category) {
                total += category.productCount;
                FListCategoryIds.push(category.id);
                //Hiển thị data
                $("#categories-list").append(
                    `
                     <div class="form-check">
                <input class="form-check-input category-check" checked type="checkbox" value="${category.id}" id="category-${category.id}">
                <label class="form-check-label" for="category-${category.id}">
                   ${category.name}
                    <span>(${category.productCount})</span>
                </label>
            </div>
                   `
                )
            });

            $("#tottalProductCategory").text(`(${total})`);
        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    })
}

function loadBrandData() {
    $.ajax({
        url: window.API_URL + `/Brand`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response)
            $("#brands-list").empty();
            FListBrandIds = [];

            let total = 0;

            $.each(response, function (index, brand) {
                total += brand.productCount;
                FListBrandIds.push(brand.id);
                //Hiển thị data
                $("#brands-list").append(
                    `
                     <div class="form-check">
                <input class="form-check-input brand-check" checked type="checkbox" value="${brand.id}" id="brand-${brand.id}">
                <label class="form-check-label" for="brand-${brand.id}">
                   ${brand.name}
                    <span>(${brand.productCount})</span>
                </label>
            </div>
                   `
                )
            });

            $("#tottalProductBrand").text(`(${total})`);
        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    })
}

//Set giá trị cho thanh mức giá
$(function () {
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 100000000,
        values: [FPrice.Min, FPrice.Max],
        slide: function (event, ui) {
            $("#price-filter").val(`${ui.values[0].toLocaleString('vi-VN')} ₫ - ${ui.values[1].toLocaleString('vi-VN')} ₫`);
        }
    });

    // Gán giá trị hiển thị ban đầu cho ô input
    const minVal = $("#slider-range").slider("values", 0);
    const maxVal = $("#slider-range").slider("values", 1);
    $("#price-filter").val(`${minVal.toLocaleString('vi-VN')} ₫ - ${maxVal.toLocaleString('vi-VN')} ₫`);
});

$(function () {
    $('#search-product-form').on('submit', function (e) {
        e.preventDefault();
        FSearchText = $('#search-product').val().trim();
        loadData();
    });
});

//Set giá trị lọc  
function filterPrice() {
    FPrice.Min = $("#slider-range").slider("values", 0);
    FPrice.Max = $("#slider-range").slider("values", 1);
     loadData();
}