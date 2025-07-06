//chạy lúc load trang
$(function () {
    loadBrandData();
    loadCategoryData();
});
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
            var selectElement = document.getElementById("categoryid");
            selectElement.innerHTML = '';

            var defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Chọn danh mục";
            selectElement.appendChild(defaultOption);

            $.each(response, function (index, category) {
                var option = document.createElement("option");
                option.value = category.id;
                option.textContent = category.name;
                selectElement.appendChild(option);
            });

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
            var selectElement = document.getElementById("brandid");
            selectElement.innerHTML = '';

            var defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Chọn thương hiệu";
            selectElement.appendChild(defaultOption);

            $.each(response, function (index, brand) {
                var option = document.createElement("option");
                option.value = brand.id;
                option.textContent = brand.name;
                selectElement.appendChild(option);
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi load dữ liệu!");
        }
    })
}
