//chạy lúc load trang
$(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: window.API_URL + `/Brand`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $("#table-data").empty();

            $.each(response, function (index, brand) {
                //Hiển thị data
                $("#table-data").append(
                    `<tr style="height:60px" class="antraz-table-list">
                        <th class="antraz-table-item">
                            <div class="body-text">${brand.name}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">#${brand.id}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${brand.description}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${brand.productCount}</div>
                        </th> 
                        <th class="antraz-table-item">
                            <div class="list-icon-function">
                                <div class="item edit" onclick="openModalEdit(${brand.id})">
                                    <i class="icon-edit-3"></i>
                                </div>
                                <div class="item trash" onclick="deleteItem(${brand.id})">
                                    <i class="icon-trash-2"></i>
                                </div>
                            </div>
                        </th>   
                    </tr>`
                )
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi load dữ liệu!");
        }
    })
}

function openModalCreate() {
    resetModal();
    $('#create-modal').modal('show');
    $('#title-modal').text('TẠO THƯƠNG HIỆU')
    $("#btn-create").show();
    $("#btn-save").hide();
}

$("#btn-create").on("click", function () {
    let name = $('#form-name').val();
    let description = $('#form-description').val();
    if (!name || !description) {
        alert('Vui lòng nhập đầy đủ thông tin');
        return;
    }

    // Gửi dữ liệu (AJAX)
    $.ajax({
        url: window.API_URL + `/Brand`,
        method: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: JSON.stringify({
            name: name,
            description: description
        }),
        success: function (response) {
            swal.fire({
                title: `Thêm thương hiệu thành công`,
                icon: "success",
                draggable: true
            }).then(() => {
                loadData();
                $('#create-modal').modal('hide');
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi thêm thương hiệu");
        }
    });
});

function resetModal() {
    $('#form-name').val('');
    $('#form-description').val('');
}

var TargetId = 0;
function openModalEdit(id) {
    TargetId = id;
    $('#create-modal').modal('show');
    $('#title-modal').text('SỬA THƯƠNG HIỆU')
    $("#btn-create").hide();
    $("#btn-save").show();

    $.ajax({
        url: window.API_URL + `/Brand/${TargetId}`,
        method: 'Get',
        success: function (response) {
            console.log(response);
            $('#form-name').val(`${response.name}`);
            $('#form-description').val(`${response.description}`);
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi thêm thương hiệu");
        }
    });
}


$("#btn-save").on("click", function () {
    let name = $('#form-name').val();
    let description = $('#form-description').val();
    if (!name || !description) {
        alert('Vui lòng nhập đầy đủ thông tin');
        return;
    }

    // Gửi dữ liệu (AJAX)
    $.ajax({
        url: window.API_URL + `/Brand/${TargetId}`,
        method: 'PUT',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: JSON.stringify({
            name: name,
            description: description
        }),
        success: function (response) {
            swal.fire({
                title: `Sửa thương hiệu thành công`,
                icon: "success",
                draggable: true
            }).then(() => {
                loadData();
                $('#create-modal').modal('hide');
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi sửa thương hiệu");
        }
    });
});

function deleteItem(id) {
    Swal.fire({
        title: "Bạn chắc chắn muốn xoá?",
        text: "Sau khi xoá sẽ không thể khôi phục",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Đồng ý, xoá nó!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: window.API_URL + `/Brand/${id}`,
                method: 'Delete',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                success: function (response) {
                    Swal.fire({
                        title: "Đã xoá!",
                        text: "Bạn đã xoá thành công một thương hiệu",
                        icon: "success"
                    });
                    loadData();
                },
                error: function (xhr, status, error) {
                    handleAjaxError(xhr, status, error, "Lỗi khi xoá thương hiệu");
                }
            });
            
        }
    });
   
}