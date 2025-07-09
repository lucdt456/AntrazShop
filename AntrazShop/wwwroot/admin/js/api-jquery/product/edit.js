$(function () {
    findH3CreateOrUpdate();
});
var productEdit = {
    imageView: '',
    folderImage: ''
}

//Ần hiện các phím
function findH3CreateOrUpdate() {
    let h3headings = document.querySelectorAll("h3");
    h3headings.forEach(function (item) {
        let textH3 = item.textContent.trim();
        if (textH3 === 'Chỉnh sửa sản phẩm') {
            document.getElementById("btn_create").style.display = "none";
            document.getElementById("btn_update").style.display = "inline";
            document.getElementById("btn_reset").style.display = "none";

            $("table.antraz-table th:first-child").text("Mã SP");
            $("table.antraz-table th:first-child").css("width", "10%");


            let urlParams = new URLSearchParams(window.location.search);
            let id = urlParams.get('id');
            loadEdit(id);
        }
    });
}

//biến toàn cục chứa thông tin sản phẩm lấy để chỉnh sửa
var productX;

//Lưu update sản phẩm
function saveUpdate() {
    Swal.fire({
        title: "Do you want to save the changes?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Save",
        denyButtonText: `Don't save`
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            let urlParams = new URLSearchParams(window.location.search);
            let id = urlParams.get('id');


            let productFormData = new FormData();
            productFormData.append('Name', $("#name").val());
            productFormData.append('Description', $("#description").val());
            productFormData.append('BrandId', $("#brandid").val());
            productFormData.append('CategoryId', $("#categoryid").val());
            productFormData.append('DiscountAmount', $("#discountAmount").val());

            if ($("#imageView").val() == '') {
                productFormData.append('ImageView', null);
            }
            else {
                productFormData.append('ImageView', $("#imageView")[0].files[0]);
            }

            $.ajax({
                url: window.API_URL + `/Product/${id}`,
                type: 'PUT',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                data: productFormData,
                processData: false,
                contentType: false,

                success: function (response) {
                    swal.fire({
                        title: "Thành công!",
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



            Swal.fire("Saved!", "", "success");
        } else if (result.isDenied) {
            Swal.fire("Changes are not saved", "", "info");
        }
    });
}

//Load các dữ liệu sản phẩm
function loadEdit(id) {
    if (id != null) {
        $("#id-product").append(id);
        $.ajax({
            url: window.API_URL + `/Product/${id}`,
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            dataType: 'json',
            success: function (product) {
                productX = product;
                console.log(product);
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

                //Xử lý ảnh hiển thị
                $("#uploadIcon").hide();
                $("#imagePreview").closest(".img-preview-product").show();
                $("#imagePreview").closest(".image-border").css("border", "none");

                productEdit.folderImage = product.folderImage;
                $("#imagePreview    ").attr("src", `/admin/imgs/product/${product.folderImage}/${product.imageView}`).show();

                productEdit.imageView = product.imageView; //Lưu tên file tạm thời
                $("#imageProductName").val(product.imageView);
                LoadProductCC(product.productCCs);

            },
            error: function (xhr, status, error) {
                alert("Lỗi khi lấy sản phẩm: " + xhr.responseText);
                console.error(error);
            }
        })
    }
}


//load phân loại sản phẩm
function LoadProductCC(productCCs) {
    $("#productCC-list").empty();
    $.each(productCCs, function (index, productCC) {
        statusInt = productCC.status;
        if (productCC.stock == 0) statusInt = 2;
        let status;
        switch (statusInt) {
            case 0:
                status = '<div class="block-pending">Ngừng bán</div>';
                break;
            case 1:
                status = '<div class="block-available">Đang bán</div>';
                break;
            case 2:
                status = '<div class="block-not-available">Hết hàng</div>';
                break;
            default:
                status = "---";
        }

        $("#productCC-list").append(
            ` <tr class="antraz-table-list">
                    <th class="antraz-table-item" style="min-width:0px;">
                        <div class="body-text">${productCC.id}</div>
                    </th>
                    <th class="antraz-table-item">
                        <div class="image no-bg">
                             <img style="object-fit: contain; width: 100%;" src="/admin/imgs/product/${productEdit.folderImage}/${productCC.image}" alt="">
                        </div>
                    </th> 
                    <th class="antraz-table-item">
                        <div class="body-text">${productCC.colorName}</div>
                    </th>
                    <th class="antraz-table-item">
                        <div class="body-text">${productCC.capacityValue}</div>
                    </th>
                    <th class="antraz-table-item">
                       <div class="body-text">${productCC.price.toLocaleString('vi-VN')}</div>
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

//Reset Form CC
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


// Mở modal CC
$("#modal-cc-open").click(function () {
    ResetFormCC()
    document.getElementById("btn_save_cc").style.display = "none";
    document.getElementById("btn_create_cc").style.display = "inline";
    $('#productCCModal').modal('show');
    $('#modal-cc-title').text('Thêm phân loại sản phẩm');
    $("#idCCDiv").html("");
});

function AddProductCC() {
    let isValid = ValidateFormCC();
    if ($("#imageViewCC").val() == '') {
        $("#error-message-imageCC").text('Chọn ảnh');
        isValid = false;
    };
    if (isValid) {
        Swal.fire({
            title: "Do you want to save the changes?",
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: "Save",
            denyButtonText: `Don't save`
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {

                let productCCFormData = new FormData();
                productCCFormData.append('ColorName', $("#color").val());
                productCCFormData.append('CapacityValue', $("#capacity").val());
                productCCFormData.append('Stock', $("#stock").val());
                productCCFormData.append('Price', $("#price").val());
                productCCFormData.append('Status', $("#status").val());
                productCCFormData.append('Image', $("#imageViewCC")[0].files[0]);

                let urlParams = new URLSearchParams(window.location.search);
                let idProduct = urlParams.get('id');
                $.ajax({
                    url: window.API_URL + `/Product/${idProduct}/${productX.folderImage}`,
                    type: 'POST',
                    headers: {
                        'Authorization': 'Bearer ' + token
                    },
                    data: productCCFormData,
                    processData: false,
                    contentType: false,

                    success: function (response) {
                        Swal.fire({
                            position: "center",
                            icon: "success",
                            title: "Thêm thành công!",
                            showConfirmButton: false,
                            timer: 1000
                        });
                        loadEdit(idProduct);
                        $('#productCCModal').modal('hide');

                    },
                    error: function (xhr, status, error) {
                        swal.fire({
                            icon: "error",
                            title: "oops...",
                            text: "Lỗi không cập nhật được phân loại sản phẩm" + xhr.responseText,
                            footer: '<a href="#">Có lỗi xảy ra?</a>'
                        });
                        console.error(error);
                    }
                });

            } else if (result.isDenied) {
                Swal.fire("Changes are not saved", "", "info");
            }
        });
    } else {
        console.log("Lỗi validate");
    }

}

function EditProductCC(button) {
    ResetFormCC()
    $("#error-message-imageCC").text('');

    document.getElementById("btn_save_cc").style.display = "inline";
    document.getElementById("btn_create_cc").style.display = "none";

    $('#productCCModal').modal('show');
    $('#modal-cc-title').text('Chỉnh sửa phân loại sản phẩm');

    let currentRow = $(button).closest("tr");
    let idProductCC = currentRow.find("th:nth-child(1) .body-text").text();

    $("#idCCDiv").html(
        `
        <label class="body-title mb-10">Mã phân loại</label>
        <input style="background-color: #E2E8F0" disabled class="mb-10 validate-form-cc" type="number" value="${idProductCC}" placeholder="" id="idCC" tabindex="0">
        `
    );

    let productCC = productX.productCCs.find(p => p.id == idProductCC);

    $("#color").val(productCC.colorName);
    $("#capacity").val(productCC.capacityValue);
    $("#price").val(productCC.price);
    $("#stock").val(productCC.stock);
    $("#status").val(productCC.status);

    //Hiển thị ảnh
    $("#uploadIconCC").hide();
    $("#imagePreviewCC").closest(".img-preview-product").show();
    $("#imagePreviewCC").closest(".image-border").css("border", "none");
    $("#imagePreviewCC").attr("src", `/admin/imgs/product/${productX.folderImage}/${productCC.image}`).show();
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

function SaveProductCC() {
    let isValid = ValidateFormCC();
    if (isValid) {
        Swal.fire({
            title: "Do you want to save the changes?",
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: "Save",
            denyButtonText: `Don't save`
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {

                let productCCFormData = new FormData();
                productCCFormData.append('ColorName', $("#color").val());
                productCCFormData.append('CapacityValue', $("#capacity").val());
                productCCFormData.append('Stock', $("#stock").val());
                productCCFormData.append('Price', $("#price").val());
                productCCFormData.append('Status', $("#status").val());

                if ($("#imageViewCC").val() == '') {
                    productCCFormData.append('Image', null);
                }
                else {
                    productCCFormData.append('Image', $("#imageViewCC")[0].files[0]);
                }
                let idProductCC = $("#idCC").val();
                $.ajax({
                    url: window.API_URL + `/Product/${productX.folderImage}/${idProductCC}`,
                    type: 'PUT',
                    headers: {
                        'Authorization': 'Bearer ' + token
                    },
                    data: productCCFormData,
                    processData: false,
                    contentType: false,

                    success: function (response) {
                        Swal.fire({
                            position: "center",
                            icon: "success",
                            title: "Cập nhật thành công!",
                            showConfirmButton: false,
                            timer: 1000
                        });
                        let urlParams = new URLSearchParams(window.location.search);
                        let id = urlParams.get('id');
                        loadEdit(id);
                        $('#productCCModal').modal('hide');

                    },
                    error: function (xhr, status, error) {
                        swal.fire({
                            icon: "error",
                            title: "oops...",
                            text: "Lỗi không cập nhật được phân loại sản phẩm" + xhr.responseText,
                            footer: '<a href="#">Có lỗi xảy ra?</a>'
                        });
                        console.error(error);
                    }
                });

            } else if (result.isDenied) {
                Swal.fire("Changes are not saved", "", "info");
            }
        });
    } else {
        console.log("Lỗi validate");
    }

}

function DeleteProductCC(button) {

    let currentRow = $(button).closest("tr");
    let idProductCC = currentRow.find("th:nth-child(1) .body-text").text();

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Bạn chắc chắn xoá?",
        text: "Sau khi xoá không thể khôi phục!",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "Không",
        confirmButtonText: "Đồng ý",

        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: window.API_URL + `/Product/${idProductCC}/${productX.folderImage}`,
                type: 'DELETE',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                success: function (response) {
                    Swal.fire({
                        position: "center",
                        icon: "success",
                        title: "Xoá thành công!",
                        showConfirmButton: false,
                        timer: 1000
                    });
                    let urlParams = new URLSearchParams(window.location.search);
                    let id = urlParams.get('id');
                    loadEdit(id);
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

function ValidateFormCC() {
    isValid = true;

    $(".error-message-cc").text("");
    $(".validate-form-cc").each(function () {
        if ($(this).val().trim() == "") {
            $(this).closest("fieldset").find(".error-message-cc").text("Đang trống")
            isValid = false;
        }
    });
    return isValid;
}