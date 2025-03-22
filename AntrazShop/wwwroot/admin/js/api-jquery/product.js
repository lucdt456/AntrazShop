
$(function () {
    loadproducts();
    findH3CreateOrUpdate();
});

//Get
function loadproducts() {
    $.ajax({
        url: 'https://localhost:7092/api/Product',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            $("#product-list").empty();
            $("#product-list2").empty();
            $.each(response, function (index, product) {
                let status;
                switch (product.status) {
                    case 1:
                        status = "Hiện";
                        break;
                    case 2:
                        status = "Ẩn";
                        break;
                    case 3:
                        status = "Còn hàng";
                        break;
                    case 4:
                        status = "Hết hàng";
                        break;
                    default:
                        status = "---";
                }
                $("#product-list").append(
                    `<li class="product-item gap14">
                        <div class="image no-bg">
                            <img src="/admin/img/product/${product.imageView}" alt="">
                        </div>
                        <div class="flex items-center justify-between gap20 flex-grow">
                            <div class="name">
                                <a class="body-title-2">${product.name}</a>
                            </div>
                            <div class="body-text">#${product.id}</div>
                            <div class="body-text">$${product.price}</div>
                            
                            <div class="body-text">${product.stock}</div>
                            <div class="body-text">${product.discountAmount}</div>
                            <div>
                                <div class="block-available">${status}</div>
                            </div>
                            <div class="body-text">${product.brand}</div>

                            <div class="body-text">${product.category}</div>
                            <div class="list-icon-function">
                                <div class="item eye" onclick="goToEdit(${product.id})">
                                    <i class="icon-eye"></i>
                                </div>
                                <div class="item edit" onclick="goToEdit(${product.id})">
                                    <i class="icon-edit-3"></i>
                                </div>
                                <div class="item trash" onclick="deleteProduct(${product.id})">
                                    <i class="icon-trash-2"></i>
                                </div>
                            </div>
                        </div>
                    </li>`
                )

                $("#product-list2").append(
                    `<tr class="antraz-table-list">
                        <th class="antraz-table-item">
                            <div class="image no-bg">
                                <img src="/admin/img/product/${product.imageView}" alt="">
                            </div>
                            
                            <div class="name">
                                <a class="body-title-2">${product.name}</a>
                            </div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">#${product.id}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">$${product.price}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${product.stock}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${product.discountAmount}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="block-available">${status}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${product.brand}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${product.category}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="list-icon-function">
                                <div class="item eye" onclick="goToEdit(${product.id})">
                                    <i class="icon-eye"></i>
                                </div>
                                <div class="item edit" onclick="goToEdit(${product.id})">
                                    <i class="icon-edit-3"></i>
                                </div>
                                <div class="item trash" onclick="deleteProduct(${product.id})">
                                    <i class="icon-trash-2"></i>
                                </div>
                            </div>
                        </th>
                    </tr>
                    `
                )
            })
        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    })
}

//Create
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

//goToEdit
function goToEdit(id) {
    window.location.href = '/admin/product/update?id=' + id;

}
//load product update
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

//save update
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

//delete
function deleteProduct(id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "No",
        confirmButtonText: "Yes",

        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `https://localhost:7092/api/Product/${id}`,
                type: 'DELETE',
                success: function (response) {
                    swalWithBootstrapButtons.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });
                    loadproducts()
                },
                error: function (xhr, status, error) {
                    alert("Lỗi khi xoá: " + xhr.responseText);
                    console.error(error);
                }
            })
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                text: "Your imaginary file is safe :)",
                icon: "error"
            });
        }
    });
}

//validate
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