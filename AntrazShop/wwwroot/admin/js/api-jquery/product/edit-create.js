$(function () {
    findH3CreateOrUpdate();
});

function findH3CreateOrUpdate() {
    let h3headings = document.querySelectorAll("h3");
    h3headings.forEach(function (item) {
        let textH3 = item.textContent.trim();
        if (textH3 === 'Chỉnh sửa sản phẩm') {
            document.getElementById("btn_create").style.display = "none";
            document.getElementById("btn_update").style.display = "inline";
            let urlParams = new URLSearchParams(window.location.search);
            let id = urlParams.get('id');
            loadEdit(id);
        }
    });
}

//xem trước image
$(document).ready(function () {
    $("#imageView").on("change", function (event) {
        var file = event.target.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {

                $("#imagePreview").attr("src", e.target.result).show();
            };
            reader.readAsDataURL(file);
        }
    });
});

//reset
function ResetData() {
    $("#name").val("");
    $("#price").val("");
    $("#discountAmount").val("");
    $("#categoryid").val("");
    $("#brandid").val("");
    $("#description").val("");
    $("#imageView").val("");
    $("#status").val("");
    $("#stock").val("");
    $("#imagePreview").hide();
    $(".error-message").text("");
}

function validateInput() {
    $(".error-message").text("");
    let isValid = true;

    //name
    if ($("#name").val() === "") {
        $("#name").next(".error-message").text("Vui lòng nhập tên sản phẩm")
        isValid = false;
    }

    //price
    if ($("#price").val() === "") {
        $("#price").next(".error-message").text("Vui lòng nhập giá sản phẩm")
        isValid = false;
    }

    //discountAmount
    if ($("#discountAmount").val() === "") {
        $("#discountAmount").next(".error-message").text("Vui lòng nhập giảm giá")
        isValid = false;
    }

    //category
    if ($("#categoryid").val() === "") {
        $("#categoryid").next(".error-message").text("Vui lòng nhập danh mục")
        isValid = false;
    }

    //brandid
    if ($("#brandid").val() === "") {
        $("#brandid").next(".error-message").text("Vui lòng nhập thương hiệu")
        isValid = false;
    }

    //description
    if ($("#description").val() === "") {
        $("#description").next(".error-message").text("Vui lòng nhập mô tả sản phẩm")
        isValid = false;
    }

    //imagefile
    if ($("#imageProductName") == null) {
        if ($("#imageView").val().split("\\").pop() === "") {
            $("#imageView").next(".error-message").text("Vui lòng nhập file ảnh")
            isValid = false;
        }
    }
    //status
    if ($("#status").val() === "") {
        $("#status").next(".error-message").text("Vui lòng nhập trạng thái sản phẩm")
        isValid = false;
    }

    //stock
    if ($("#stock").val() === "") {
        $("#stock").next(".error-message").text("Vui lòng nhập số lượng tồn kho")
        isValid = false;
    }
    //$(".validate-name").each(function () {
    //    if ($(this).val.trim() === "") {
    //        $(this).next.(".error-message").text("Không được để trống");
    //        isValid = false;
    //    }
    //});
    return isValid;
}

function loadEdit(id) {
    if (id != null) {
        $("#id-product").append(id);
        $.ajax({
            url: `https://localhost:7092/api/Product/${id}`,
            type: 'GET',
            dataType: 'json',
            success: function (product) {
                $("#name").val(product.name);
                $("#price").val(product.price);
                $("#discountAmount").val(product.discountAmount);
                $("#categoryid option").each(function () {
                    if ($(this).text() === product.category) {
                        $("#categoryid").val($(this).val());
                    };
                });
                $("#brandid option").each(function () {
                    if ($(this).text() === product.brand) {
                        $("#brandid").val($(this).val());
                    }
                });
                $("#description").val(product.description);

                $("#imagePreview").attr("src", `/admin/img/product/${product.imageView}`).show();
                $("#imageProductName").val(product.imageView);

                $("#status").val(product.status);
                $("#stock").val(product.stock);

            },
            error: function (xhr, status, error) {
                alert("Lỗi khi lấy sản phẩm: " + xhr.responseText);
                console.error(error);
            }
        })
    }

}

function CreateProduct() {
    let isValid = validateInput();

    if (isValid == true) {
        let product = {
            name: $("#name").val(),
            price: $("#price").val(),
            discountAmount: $("#discountAmount").val(),
            description: $("#description").val(),
            imageView: $("#imageView").val().split("\\").pop(),
            brandId: $("#brandid").val(),
            categoryId: $("#categoryid").val(),
            status: $("#status").val(),
            stock: $("#stock").val()
        }
        productJson = JSON.stringify(product);
        $.ajax({
            url: 'https://localhost:7092/api/Product',
            type: 'post',
            contentType: 'application/json',
            data: productJson,
            success: function (response) {
                swal.fire({
                    title: "Tạo sản phẩm thành công",
                    icon: "success",
                    draggable: true
                }).then(() => {
                    window.location.href = '/admin/product';
                });

            },
            error: function (xhr, status, error) {
                swal.fire({
                    icon: "error",
                    title: "oops...",
                    text: "lỗi không tạo được sản phẩm" + xhr.responsetext,
                    footer: '<a href="#">why do i have this issue?</a>'
                });
                console.error(error);
            }
        });
    } else console.log("Lỗi validate")
}
function saveUpdate() {
    let urlParams = new URLSearchParams(window.location.search);
    let id = urlParams.get('id');
    let isValid = validateInput();
    let imageNameNew = $("#imageView").val().split("\\").pop();

    if (imageNameNew === "") {
        imageNameNew = $("#imageProductName").val();
    }
    if (isValid == true) {
        let product = {
            name: $("#name").val(),
            price: $("#price").val(),
            discountAmount: $("#discountAmount").val(),
            description: $("#description").val(),
            imageView: imageNameNew,
            brandId: $("#brandid").val(),
            categoryId: $("#categoryid").val(),
            status: $("#status").val(),
            stock: $("#stock").val()
        }
        productJson = JSON.stringify(product);
        $.ajax({
            url: `https://localhost:7092/api/Product/${id}`,
            type: 'PUT',
            contentType: 'application/json',
            data: productJson,
            success: function (response) {
                swal.fire({
                    title: "Cập nhật thành công",
                    icon: "success",
                    draggable: true
                }).then(() => {
                    window.location.href = '/admin/product';
                });
            },
            error: function (xhr, status, error) {
                swal.fire({
                    icon: "error",
                    title: "oops...",
                    text: "lỗi không cập nhật được sản phẩm" + xhr.responsetext,
                    footer: '<a href="#">why do i have this issue?</a>'
                });
                console.error(error);
            }
        });
    }
}