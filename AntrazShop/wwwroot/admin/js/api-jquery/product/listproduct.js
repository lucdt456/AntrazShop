var pager = {
    currentPage: 1,
    totalPage: 2
};

$(function () {
    $("#sizePage").val(10);
    size = $("#sizePage").val();
    loadproducts(1, size);
});

$("#sizePage").change(function () {
    size = $("#sizePage").val();
    loadproducts(1, size);
});

$(document).ready(function () {
    $(document).on('click', '.page-number-link', function () {
        let pageNumber = $(this).text();
        pager.currentPage = pageNumber;
        size = $("#sizePage").val();
        loadproducts(pageNumber, size);
    });
});

$("#firstPageButton").click(function () {
    size = $("#sizePage").val();
    pager.currentPage = 1;
    loadproducts(1, size);
});

$("#endPageButton").click(function () {
    size = $("#sizePage").val();
    pager.currentPage = pager.totalPage;
    loadproducts(pager.totalPage, size);
});

$("#leftPageButton").click(function () {
    pager.currentPage = pager.currentPage - 1;
    size = $("#sizePage").val();
    if (pager.currentPage < 1) {
        pager.currentPage = 1;
    }
    loadproducts(pager.currentPage, size);
});


$("#rightPageButton").click(function () {
    pager.currentPage = pager.currentPage + 1;
    if (pager.currentPage > pager.totalPage) {
        pager.currentPage = pager.totalPage;
    }
    size = $("#sizePage").val();
    loadproducts(pager.currentPage, size);
});

function loadproducts(page, size) {
    $.ajax({
        url: `https://localhost:7092/api/Product?page=${page}&size=${size}`,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $("#product-list").empty();
            $('#pageNumber li:nth-child(n+3):nth-last-child(n+3)').remove();
            
            pager.totalPage = response.pagination.totalPage;
            $("#totalProduct").text(`Có ${response.pagination.totalItems} sản phẩm`);
            for (let i = response.pagination.endPage; i >= response.pagination.startPage; i--) {
                let active = '';
                if (i == response.pagination.currentPage) {
                    active = 'class="active"';
                }
                $("#pageNumber").children("li:nth-child(2)").after(
                    ` 
                    <li ${active}>
                        <a href="#" class="page-number-link" >${i}</a>
                    </li>
                   `
                )
            }
            if (pager.currentPage == 1) {
              
                $("#firstPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
                $("#leftPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
            } else {
               
                $("#firstPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
                $("#leftPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
            }
            
            if (pager.currentPage == pager.totalPage) {
              
                $("#rightPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
                $("#endPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
            } else {
                
                $("#rightPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
                $("#endPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
            }
            $.each(response.products, function (index, product) {
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

                // xử lý tên quá dài
                let nameText = product.name;
                let listnametext = nameText.split(' ');
                let shortcutName = product.name;
                if (listnametext.length > 4) {
                    shortcutName = listnametext.slice(0, 4).join(' ') + ' ...'
                }

                $("#product-list").append(
                    `<tr class="antraz-table-list">
                        <th class="antraz-table-item">
                            <div class="image no-bg">
                                <img style="object-fit: contain; width: 100%;" src="/admin/img/product/${product.imageView}" alt="">
                            </div>
                            
                            <div class="name">
                                <a class="body-title-2 name-shortcut">${shortcutName}</a>
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
                                <div class="item eye" data-bs-toggle="modal" data-bs-target="#viewModal" onclick="viewProduct(${product.id})">
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
                    </tr>`
                )
            })
        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    })
}

function deleteProduct(id) {
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
                url: `https://localhost:7092/api/Product/${id}`,
                type: 'DELETE',
                success: function (response) {
                    swalWithBootstrapButtons.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });
                    loadproducts(pager.currentPage, $("#sizePage").val())
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

function goToEdit(id) {
    window.location.href = '/admin/product/update?id=' + id;
}

function viewProduct(id) {

    $("#exampleModalLabel").text("Chi tiết sản phẩm")
    document.getElementById("btn-save").style.display = "none";
    document.getElementById("btn-edit").style.display = "inline";
    $('#name').prop('disabled', true);
    $('#price').prop('disabled', true);
    $('#discountAmount').prop('disabled', true);
    $('#categoryid').prop('disabled', true);
    $('#brandid').prop('disabled', true);
    $('#description').prop('disabled', true);
    $('#imageProductName').prop('disabled', true);
    $('#status').prop('disabled', true);
    $('#stock').prop('disabled', true);
    if (id != null) {
        $("#id-product").append(id);
        $.ajax({
            url: `https://localhost:7092/api/Product/${id}`,
            type: 'GET',
            dataType: 'json',
            success: function (product) {

                $("#id-product").val('#' + product.id);
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
//btn- edit click
$("#btn-edit").click(function () {
    $("#exampleModalLabel").text("Chỉnh sửa")
    document.getElementById("btn-save").style.display = "inline";
    document.getElementById("btn-edit").style.display = "none";
    $('#name').prop('disabled', false);
    $('#price').prop('disabled', false);
    $('#discountAmount').prop('disabled', false);
    $('#categoryid').prop('disabled', false);
    $('#brandid').prop('disabled', false);
    $('#description').prop('disabled', false);
    $('#imageProductName').prop('disabled', false);
    $('#status').prop('disabled', false);
    $('#stock').prop('disabled', false);
})

//save update
function saveUpdate() {
    let urlParams = new URLSearchParams(window.location.search);
    let id = $("#id-product").val().substring(1);
    console.log(id);
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
                    viewProduct(id);
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
