$(function () {
    findH3CreateOrUpdate();
    loadDataColorCapacity();
});

//Biến toàn cục
var colorCheck = "";
var capacityCheck = "";
var arrProductCCs = [];
var index;


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

//xem trước imageView
$(document).ready(function () {
    $("#imageView").on("change", function (event) {
        let file = event.target.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#uploadIcon").hide();
                $("#imagePreview").closest(".img-preview-product").show();
                $("#imagePreview").closest(".image-border").css("border", "none");
                $("#imagePreview").attr("src", e.target.result).show();

            };
            reader.readAsDataURL(file);
        }
    });

    $("#imageViewCC").on("change", function (event) {
        let fileCC = event.target.files[0];
        if (fileCC) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#uploadIconCC").hide();
                $("#imagePreviewCC").closest(".img-preview-product").show();
                $("#imagePreviewCC").closest(".image-border").css("border", "none");
                $("#imagePreviewCC").attr("src", e.target.result).show();         
            };
            reader.readAsDataURL(fileCC);
        }
    });
});

//reset
function ResetData() {
    $("#name").val("");
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

    $(".validate-input").each(function () {
        if ($(this).val().trim() == "") {
            $(this).closest("fieldset").find(".error-message").text("Không được để trống")
            isValid = false;
        }
    });


    //imagefile
    if ($("#imageProductName").val() === '') {
        if ($("#imageView").val().split("\\").pop() === "") {
            $("#error-message-image").text('Chưa chọn ảnh');
            isValid = false;
        }
    }

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
        let productFormData = new FormData();

        productFormData.append('Name', $("#name").val());
        productFormData.append('Description', $("#description").val());
        productFormData.append('BrandId', $("#brandid").val());
        productFormData.append('CategoryId', $("#categoryid").val());
        productFormData.append('ImageView', $("#imageView")[0].files[0]);
        productFormData.append('DiscountAmount', $("#discountAmount").val());

        arrProductCCs.forEach(function (productCC, index) {
            productFormData.append(`ProductCCDTOs[${index}].colorName`, productCC.colorName);
            productFormData.append(`ProductCCDTOs[${index}].capacityValue`, productCC.capacityValue);
            productFormData.append(`ProductCCDTOs[${index}].stock`, productCC.stock);
            productFormData.append(`ProductCCDTOs[${index}].price`, productCC.price);
            productFormData.append(`ProductCCDTOs[${index}].status`, productCC.status);
            productFormData.append(`ProductCCDTOs[${index}].image`, productCC.image);
        });
         
        console.log(productFormData)
        $.ajax({
            url: 'https://localhost:7092/api/Product/create',
            type: 'POST',
            data: productFormData,
            processData: false,
            contentType: false,

            success: function (response) {
                swal.fire({
                    title: "Tạo sản phẩm thành công!",
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
                    text: "Lỗi không tạo được sản phẩm" + xhr.responseText,
                    footer: '<a href="#">Có lỗi xảy ra?</a>'
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


//Load dữ liệu phân loại CC ra bảng con
function loadDataColorCapacity() {
    $("#productCC-list").empty();
    $.each(arrProductCCs, function (index, productCC) {

        let status = (productCC.status == 1) ? '<div class="block-available">Đang bán</div>' : '<div class="block-pending">Ngừng bán</div>';

        $("#productCC-list").append(
            ` <tr>
                    <th class="antraz-table-item" style="min-width:0px;">
                        <div class="body-text">${index + 1}</div>
                    </th>
                    <th class="antraz-table-item">
                        <div class="body-text">${productCC.colorName}</div>
                    </th>
                    <th class="antraz-table-item">
                        <div class="body-text">${productCC.capacityValue}</div>
                    </th>
                    <th class="antraz-table-item">
                       <div class="body-text">${productCC.price}</div>
                        </th>
                    <th class="antraz-table-item">
                        <div class="body-text">${productCC.stock}</div>
                    </th>
                    <th class="antraz-table-item">
                        <div class="body-text">${status}</div>
                    </th>
                    <th class="antraz-table-item" style="max-width:10%;">
                        <div class="list-icon-function">
                           <div class="item edit" onclick="EditProductCC(this)">
                                <i class="icon-edit-3"></i>
                           </div>                             
                           <div class="item trash" onclick="DeleteProductCC(this)">
                                <i class="icon-trash-2"></i>
                           </div>
                       </div>
                    </th>
               </tr>`
        )
    });
}

// Mở modal CC
$("#modal-cc-open").click(function () {
    ResetFormCC();
    document.getElementById("btn_save_cc").style.display = "none";
    document.getElementById("btn_create_cc").style.display = "inline";
    $('#productCCModal').modal('show');
    $('#modal-cc-title').text('Phân loại sản phẩm');
});




//Validate form nhập Color Capacity
function ValidateFormCC() {
    isValid = true;

    $(".error-message-cc").text("");
    $(".validate-form-cc").each(function () {
        if ($(this).val().trim() == "") {
            $(this).closest("fieldset").find(".error-message-cc").text("Đang trống")
            isValid = false;
        }
    });

    let color = $("#color").val();
    let capacity = $("#capacity").val();

    $("#productCC-list tr").each(function () {
        let existingcolor = $(this).find("th:nth-child(2) .body-text").text();
        let existingcapacity = $(this).find("th:nth-child(3) .body-text").text();
        if (existingcolor === color && existingcapacity === capacity) {
            if (color === colorCheck && capacity === capacityCheck) {

            }
            else {
                $("#error-text-cc-message").text(`Sản phẩm màu ${color} dung lượng ${capacity} đã được thêm trước đó`)
                isValid = false;
            }

        }
    })


    return isValid;
}
//reset Form CC
function ResetFormCC() {
    $("#color").val("");
    $("#capacity").val("");
    $("#price").val("");
    $("#stock").val("");
    $("#status").val("");
    $("#imageViewCC").val("");
    $("#imagePreviewCC").closest(".image-border").removeAttr('style');
    $("#uploadIconCC").show();
    $("#imagePreviewCC").closest(".img-preview-product").hide();
    $(".error-message").text("");

    colorCheck = "";
    capacityCheck = "";
}

//Thêm dữ liệu phân loại vào bảng
function AddProductCC() {
    $("#error-text-cc-message").text("");
    $(".error-message").text("");

    let isValid = ValidateFormCC();
    if (isValid) {
        let productCC = {
            colorName: $("#color").val(),
            capacityValue: $("#capacity").val(),
            price: $("#price").val(),
            stock: $("#stock").val(),
            status: $("#status").val(),
            image: $("#imageViewCC")[0].files[0]
        };

        arrProductCCs.push(productCC);
        loadDataColorCapacity();
        $('#productCCModal').modal('hide');

    }
    else console.log("Lỗi validate");
}

//Xoá dữ liệu bảng phân loại
function DeleteProductCC(button) {
    $(button).closest("tr").remove();
}

//Đưa sản phẩm vào modal
function EditProductCC(button) {
    let currentRow = $(button).closest("tr");

    index = Number(currentRow.find("th:nth-child(1) .body-text").text()) - 1;

    let color = arrProductCCs[index].colorName;
    let capacity = arrProductCCs[index].capacityValue;
    let price = arrProductCCs[index].price;
    let stock = arrProductCCs[index].stock;
    let status = arrProductCCs[index].status;
    let image = arrProductCCs[index].image;

    colorCheck = color;
    capacityCheck = capacity;

    document.getElementById("btn_save_cc").style.display = "inline";
    document.getElementById("btn_create_cc").style.display = "none";

    $('#productCCModal').modal('show');
    $('#modal-cc-title').text('Chỉnh sửa sản phẩm phân loại');

    $("#color").val(color);
    $("#capacity").val(capacity);
    $("#price").val(price);
    $("#stock").val(stock);
    $("#status").val(status);
 /*   let imageURL = URL.createObjectURL(image);*/
    console.log(image.target.result)
   /* $("#imagePreviewCC").attr("src", imageURL).show(); */

  /*  $("#imageViewCC").val(image);*/
}

//SaveEdit CC
function SaveProductCC() {
    $(".error-message").text("");
    let isValid = ValidateFormCC();
    if (isValid) {
        let color = $("#color").val();
        let capacity = $("#capacity").val();
        let price = $("#price").val();
        let stock = $("#stock").val();
        let status = $("#status").val();

        currentRow.find("th:nth-child(1) .body-text").html(`${color}`);
        currentRow.find("th:nth-child(2) .body-text").html(`${capacity}`);
        currentRow.find("th:nth-child(3) .body-text").html(`${price}`);
        currentRow.find("th:nth-child(4) .body-text").html(`${stock}`);
        currentRow.find("th:nth-child(5) .body-text").html(`${status}`);
        $('#productCCModal').modal('hide');
    }
    else console.log("Lỗi validate")

}