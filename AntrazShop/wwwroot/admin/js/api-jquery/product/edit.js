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

            $("table.antraz-table th:first-child").text("Mã SP");
            $("table.antraz-table th:first-child").css("width", "10%");


            let urlParams = new URLSearchParams(window.location.search);
            let id = urlParams.get('id');
            loadEdit(id);
        }
    });
}

//Load các dữ liệu sản phẩm
function loadEdit(id) {
    if (id != null) {
        $("#id-product").append(id);
        $.ajax({
            url: `https://localhost:7092/api/Product/${id}`,
            type: 'GET',
            dataType: 'json',
            success: function (product) {
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

function LoadProductCC(productCCs) {
    $("#productCC-list").empty();
    $.each(productCCs, function (index, productCC) {

        let status = (productCC.status == 1) ? '<div class="block-available">Đang bán</div>' : '<div class="block-pending">Ngừng bán</div>';

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
    $('#modal-cc-title').text('Phân loại sản phẩm');
});

function loadDataColorCapacity() {
    $("#productCC-list").empty();

}