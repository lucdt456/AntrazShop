//chạy lúc load trang
$(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: window.API_URL + `/Role/1/100`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $("#table-data").empty();

            $.each(response.roles, function (index, role) {
                //Hiển thị data
                $("#table-data").append(
                    `<tr style="height:60px" class="antraz-table-list">
                        <th class="antraz-table-item">
                            <div class="body-text">${role.name}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">#${role.id}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${role.description}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${role.countUser}</div>
                        </th> 
                        <th class="antraz-table-item">
                            <div class="list-icon-function">
                               <a href="/admin/role/EditRole?id=${role.id}" class="item edit">
                                    <i class="icon-edit-3"></i>
                                </a>
                                <div class="item trash" onclick="deleteItem(${role.id})">
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

$("#btn-create").on("click", function () {
    window.location.href = `/admin/role/createrole`;
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
                url: window.API_URL + `/Role/${id}`,
                method: 'Delete',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                success: function (response) {
                    Swal.fire({
                        title: "Đã xoá!",
                        text: "Bạn đã xoá thành công một vai trò",
                        icon: "success"
                    });
                    loadData();
                },
                error: function (xhr, status, error) {
                    handleAjaxError(xhr, status, error, "Lỗi khi xoá vai trò");
                }
            });
            
        }
    });
   
}