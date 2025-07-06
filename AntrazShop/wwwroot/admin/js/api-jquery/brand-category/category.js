//chạy lúc load trang
$(function () {
    loadData();
});


function loadData() {
    $.ajax({
        url: window.API_URL + `/Category`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $("#table-data").empty();

            $.each(response, function (index, category) {
                //Hiển thị data
                $("#table-data").append(
                    `<tr style="height:60px" class="antraz-table-list">
                        <th class="antraz-table-item">
                            <div class="body-text">${category.name}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">#${category.id}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${category.description}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${category.productCount}</div>
                        </th> 
                        <th class="antraz-table-item">
                            <div class="list-icon-function">
                                <div class="item edit" onclick="openModalEdit(${category.id})">
                                    <i class="icon-edit-3"></i>
                                </div>
                                <div class="item trash" onclick="deleteItem(${category.id})">
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
    $('#title-modal').text('TẠO DANH MỤC')
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
        url: window.API_URL + `/Category`,
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
                title: `Thêm danh mục thành công`,
                icon: "success",
                draggable: true
            }).then(() => {
                loadData();
                $('#create-modal').modal('hide');
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi thêm danh mục");
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
    $('#title-modal').text('SỬA DANH MỤC')
    $("#btn-create").hide();
    $("#btn-save").show();

    $.ajax({
        url: window.API_URL + `/Category/${TargetId}`,
        method: 'Get',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            console.log(response);
            $('#form-name').val(`${response.name}`);
            $('#form-description').val(`${response.description}`);
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi thêm danh mục");
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
        url: window.API_URL + `/Category/${TargetId}`,
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
                title: `Sửa danh mục thành công`,
                icon: "success",
                draggable: true
            }).then(() => {
                loadData();
                $('#create-modal').modal('hide');
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi sửa danh mục");
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
                url: window.API_URL + `/Category/${id}`,
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                method: 'Delete',
                success: function (response) {
                    Swal.fire({
                        title: "Đã xoá!",
                        text: "Bạn đã xoá thành công một danh mục",
                        icon: "success"
                    });
                    loadData();
                },
                error: function (xhr, status, error) {
                    handleAjaxError(xhr, status, error, "Lỗi khi xoá danh mục");
                }
            });

        }
    });

}