//Biến toàn cục
var colorCheck = ""; //2 biến check validate product phân loại
var capacityCheck = "";

var arrProductCCs = [];//Mảng chứa phân loại

var imageProductCCUrl = ''; //Url ảnh phân loại

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
                imageProductCCUrl = e.target.result;
                $("#imagePreviewCC").attr("src", imageProductCCUrl).show();

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

    if (arrProductCCs.length == 0) {
        isValid = false;
        Swal.fire({
            icon: "error",
            title: "Lỗi...",
            text: "Vui lòng thêm ít nhất 1 phân loại!!",
            footer: '<a href="#">Why do I have this issue?</a>'
        });
    }

    //imagefile
    if ($("#imageProductName").val() === '') {
        if ($("#imageView").val().split("\\").pop() === "") {
            $("#error-message-image").text('Chưa chọn ảnh');
            isValid = false;
        }
    }

    return isValid;
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
            url: window.API_URL + '/Product/create',
            type: 'POST',
            headers: {
                'Authorization': 'Bearer ' + token
            },
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

                let errorMessage = 'Đăng nhập thất bại.';

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
    } else console.log("Lỗi validate")
}

//Load dữ liệu phân loại CC ra bảng con
function loadDataColorCapacity() {
    $("#productCC-list").empty();
    $.each(arrProductCCs, function (index, productCC) {

        let status = (productCC.status == 1) ? '<div class="block-available">Đang bán</div>' : '<div class="block-pending">Ngừng bán</div>';

        $("#productCC-list").append(
            ` <tr class="antraz-table-list">
                    <th class="antraz-table-item" style="min-width:0px;">
                        <div class="body-text">${index + 1}</div>
                    </th>
                    <th class="antraz-table-item">
                        <div class="image no-bg">
                             <img style="object-fit: contain; width: 100%;" src="${productCC.imageUrl}" alt="">
                        </div>
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
    $(".error-message-cc").text("");
    $("#error-message-imageCC").text('');

    colorCheck = "";
    capacityCheck = "";
}

//Thêm dữ liệu phân loại vào bảng

function AddProductCC() {
    $("#error-text-cc-message").text("");
    $(".error-message").text("");

    let isValid = ValidateFormCC();
    if ($("#imageViewCC").val() == '') {
        $("#error-message-imageCC").text('Chọn ảnh');
        isValid = false;
    };

    if (isValid) {

        let productCC = {
            colorName: $("#color").val(),
            capacityValue: $("#capacity").val(),
            price: $("#price").val(),
            stock: $("#stock").val(),
            status: $("#status").val(),
            image: $("#imageViewCC")[0].files[0],
            imageUrl: imageProductCCUrl
        };

        arrProductCCs.push(productCC);
        loadDataColorCapacity();
        $('#productCCModal').modal('hide');
    } else {
        console.log("Lỗi validate");
    }
}

//Xoá dữ liệu bảng phân loại
function DeleteProductCC(button) {
    let currentRow = $(button).closest("tr");
    let indexDelete = Number(currentRow.find("th:nth-child(1) .body-text").text()) - 1;
    Swal.fire({
        title: "Xác nhận xoá?",
        text: "Sau khi xoá sẽ không thể khôi phục!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Đồng ý"
    }).then((result) => {
        if (result.isConfirmed) {
            arrProductCCs.splice(indexDelete, 1);
            loadDataColorCapacity();
            Swal.fire({
                title: "Đã xoá",
                text: "Xoá thành công sản phẩm phân loại khỏi danh sách",
                icon: "success"
            });
        }
    });
}


//Biến toàn cục đưa sản phẩm vào modal edit
var indexEdit;

//Đưa sản phẩm vào modal để edit
function EditProductCC(button) {
    $("#error-message-imageCC").text('');
    let currentRow = $(button).closest("tr");
    indexEdit = Number(currentRow.find("th:nth-child(1) .body-text").text()) - 1;

    let color = arrProductCCs[indexEdit].colorName;
    let capacity = arrProductCCs[indexEdit].capacityValue;

    $("#imageViewCC").val('');

    //Lấy biến để validate
    colorCheck = color;
    capacityCheck = capacity;

    document.getElementById("btn_save_cc").style.display = "inline";
    document.getElementById("btn_create_cc").style.display = "none";

    $('#productCCModal').modal('show');
    $('#modal-cc-title').text('Chỉnh sửa sản phẩm phân loại');

    $("#color").val(color);
    $("#capacity").val(capacity);
    $("#price").val(arrProductCCs[indexEdit].price);
    $("#stock").val(arrProductCCs[indexEdit].stock);
    $("#status").val(arrProductCCs[indexEdit].status);

    //Hiển thị ảnh
    $("#uploadIconCC").hide();
    $("#imagePreviewCC").closest(".img-preview-product").show();
    $("#imagePreviewCC").closest(".image-border").css("border", "none");
    $("#imagePreviewCC").attr("src", arrProductCCs[indexEdit].imageUrl).show();
}

//SaveEdit CC
function SaveProductCC() {
    Swal.fire({
        title: "Xác nhận lưu?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Lưu",
        denyButtonText: `Huỷ`
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $(".error-message-cc").text("");
            $("#error-text-cc-message").text("");
            let isValid = ValidateFormCC();
            if (isValid) {

                arrProductCCs[indexEdit] = {
                    colorName: $("#color").val(),
                    capacityValue: $("#capacity").val(),
                    price: $("#price").val(),
                    stock: $("#stock").val(),
                    status: $("#status").val()
                };

                if ($("#imageViewCC").val() != '') {
                    arrProductCCs[indexEdit].image = $("#imageViewCC")[0].files[0];
                    arrProductCCs[indexEdit].imageUrl = imageProductCCUrl;
                }
                $('#productCCModal').modal('hide');
                loadDataColorCapacity();
            }
            else console.log("Lỗi validate")
            Swal.fire("Đã lưu", "", "success");
        } else if (result.isDenied) {
            Swal.fire("Lưu thất bại", "", "info");
        }
    });
}