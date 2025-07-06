//chạy lúc load trang
$(function () {
    initializeData();
    loadRoles();
    loadData();
    setPaginationButtonStyle();
});

//hàm khởi tạo biến ban đầu
function initializeData() {
    $("#numberShowing").val(10);
    setPagerData();
};

//Biến toàn cục
var idTarget = 1000;

function loadData() {
    setPagerData();
    $.ajax({
        url: window.API_URL + `/AccountManager/Worker/${pager.currentPage}/${pager.numberShowing}?search=${pager.search}`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $("#table-data").empty();
            $('#pageNumber li:nth-child(n+3):nth-last-child(n+3)').remove();

            pager.totalPage = response.pagination.totalPage;

            $("#totalItem").text(`Có ${response.pagination.totalItems} tài khoản`);

            for (let i = response.pagination.endPage; i >= response.pagination.startPage; i--) {
                let active = '';
                if (i == response.pagination.currentPage) {
                    active = 'class="active"';
                }
                $("#pageNumber").children("li:nth-child(2)").after(
                    ` 
                    <li ${active}>
                        <a class="page-number-link" >${i}</a>
                    </li>
                   `
                )
            }

            pager.currentPage = response.pagination.currentPage;

            $.each(response.users, function (index, user) {
                // xử lý tên quá dài
                let nameText = user.name;
                let listnametext = nameText.split(' ');
                let shortcutName = user.name;
                if (listnametext.length > 4) {
                    shortcutName = listnametext.slice(0, 4).join(' ') + ' ...'
                }

                //Xử lý hiển thị role
                let rolesString = user.roles.slice(0, 2).map(r => r.name).join(', ');
                if (user.roles.length > 2) {
                    rolesString = rolesString + "...";
                }

                let isActive = '';
                switch (user.isActive) {
                    case true:
                        isActive = `<div class="body-text" style="color: #22c55e; font-weight: bold; background-color: #dcfce7; padding: 8px 12px; border-radius: 6px; text-align: center; margin-right: 15px; display: inline-block; min-width: 100px;">Hoạt động</div>`;
                        break;
                    case false:
                        isActive = `<div class="body-text" style="color: #ef4444; font-weight: bold; background-color: #fee2e2; padding: 8px 12px; border-radius: 6px; text-align: center; margin-right: 15px; display: inline-block; min-width: 100px;">Đang khóa</div>`;
                        break;
                    default:
                        isActive = `<div class="body-text" style="color: #6b7280; font-weight: bold; background-color: #f3f4f6; padding: 8px 12px; border-radius: 6px; text-align: center; margin-right: 15px; display: inline-block; min-width: 100px;">Không xác định</div>`;
                        break;
                }

                //Hiển thị data
                $("#table-data").append(
                    `<tr class="antraz-table-list">
                        <th class="antraz-table-item" style="display:flex">
                            <div class="image no-bg">
                                <img style="object-fit: contain; width: 100%;" src="/admin/imgs/avatar/${user.avatar}">
                            </div>
                            
                            <div class="name">
                                <a class="body-title-2 name-shortcut">${shortcutName}</a>
                            </div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">#${user.id}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${user.email}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${rolesString}</div>
                        </th>
                        <th class="antraz-table-item">
                           ${isActive}
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${user.gender}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${user.birthday}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="list-icon-function">
                                <div class="item eye" data-bs-target="#viewModal" onclick="viewUser(${user.id})">
                                 <i class="icon-eye"></i>
                                 </div>
                                <div class="item trash" onclick="deleteAccount(${user.id})">
                                    <i class="icon-trash-2"></i>
                                </div>
                            </div>
                        </th>   
                    </tr>`
                )
            });
            setPaginationButtonStyle();
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi tải danh sách người dùng!");
        }
    })
}

function viewUser(id) {
    $("#a-edit-role").show();
    $("#list-roles").hide();
    $("#table-history-visiable").hide();
    idTarget = id;
    $('#viewModal').modal('show');
    $.ajax({
        url: window.API_URL + `/AccountManager/${idTarget}`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $("#user-email").text(response.email);
            $("#user-name").text(response.name);
            $("#user-birthday").text(response.birthday);
            $("#user-gender").text(response.gender);
            $("#user-address").text(response.address);
            $("#user-hometown").text(response.hometown);
            $("#user-list-roles").text(response.roles.map(r => r.name).join(', '));
            $("#user-create-at").text(response.createAt);
            $("#id-user").text(response.id);
            $('#avatar-image').attr('src', `/admin/imgs/avatar/${response.avatar}`);

            document.getElementById("isActive").checked = response.isActive;

            $.each(response.roles, function (index, role) {
                let rolenameId = 'role-id-' + role.id;
                $(`input[name="${rolenameId}"][value="${role.id}"]`).prop('checked', true);
            });
        
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi load thông tin người dùng!");
        }
    })
}

//Lịch sử đăng nhập
function historyLogin() {
    $("#table-history-visiable").show();

    $.ajax({
        url: window.API_URL + `/AccountManager/LoginHistory/${idTarget}`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            $("#table-history").empty();

            $.each(response, function (index, history) {
                //Hiển thị data
                $("#table-history").append(
                    `<tr class="antraz-table-list" style="border:none">
                        <th style="border:none" class="antraz-table-item">
                            <div class="body-text" style="font-size:12px;">${history.time}</div>
                        </th>
                        <th style="border:none" class="antraz-table-item">
                            <div class="body-text" style="font-size:12px;">${history.ipAddress}</div>
                        </th>
                        <th style="border:none" class="antraz-table-item">
                            <div class="body-text" style="font-size:12px;">${history.statusLogin}</div>
                        </th>
                    </tr>`
                )
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi load lịch sử đăng nhập!");
        }
    })
}

function loadRoles() {
    document.getElementById("table-history-visiable").style.display = "table";

    $.ajax({
        url: window.API_URL + `/Role/1/100`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            $("#list-roles").empty();
            console.log(response);

            $.each(response.roles, function (index, role) {
                let idRoleName = 'role-id-' + role.id;
                $("#list-roles").append(
                    ` <fieldset class="col-6 mb-24">
                                    <div class="body-title mb-10">${role.name}</div>
                                    <div class="radio-buttons">
                                        <div class="item">
                                            <input class="" type="radio" value="${role.id}" name="${idRoleName}" id="${idRoleName}-on">
                                            <label class="" for="${idRoleName}-on"><span class="body-title-2">Bật</span></label>
                                        </div>
                                        <div class="item">
                                            <input class="" type="radio" name="${idRoleName}" value="0" id="${idRoleName}-off" checked>
                                            <label class="" for="${idRoleName}-off"><span class="body-title-2">Tắt</span></label>
                                        </div>
                                    </div>
                                </fieldset>`
                )
            });
        },
        error: function (xhr, status, error) {

            handleAjaxError(xhr, status, error, "Lỗi khi load trò!");
        }
    })
}

function saveSetRoles() {
    let selectedRoles = [];
    $('input[type="radio"]:checked').each(function () {
        let value = $(this).val();
        if (value != "0") {
            selectedRoles.push(parseInt(value));
        }
    });

    $.ajax({
        url: window.API_URL + `/AccountManager/EditUserRoles/${idTarget}`,
        type: 'PUT',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        data: JSON.stringify(selectedRoles),
        success: function (response) {
            swal.fire({
                title: "Cập nhật vai trò thành công!",
                icon: "success",
                draggable: true
            }).then(() => {
                viewUser(idTarget);
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi cập nhật vai trò!");
        }
    });
}

//Hàm mở edit roles
function startEditRoles() {
    $("#list-roles").show();
    $("#btn-cancle").show();
    $("#btn-save").show();
    $("#a-edit-role").hide();
}
function cancleEditRole() {
    $("#list-roles").hide();
    $("#btn-cancle").hide();
    $("#btn-save").hide();
    $("#a-edit-role").show();
}

//Hàm khi đóng modal
$('#viewModal').on('hide.bs.modal', function () {
    loadData();
    $('input[type="radio"][value="0"]').prop('checked', true);
    $("#list-roles").hide();
    $("#btn-cancle").hide();
    $("#btn-save").hide();
});


//Xoá tài khoản
function deleteAccount(userId) {
    Swal.fire({
        title: "Chắc chắn xoá?",
        text: "Sau khi xoá không thể khôi phục!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Đồng ý"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: window.API_URL + `/AccountManager/${userId}`,
                type: 'Delete',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                success: function (response) {
                    swal.fire({
                        title: "Xoá thành công",
                        icon: "success",
                        draggable: true
                    }).then(() => {
                        loadData();
                    });
                },
                error: function (xhr, status, error) {
                    handleAjaxError(xhr, status, error, "Lỗi khi xoá tài khoản!");
                }
            });
            Swal.fire({
                title: "Đã xoá!",
                text: "Tài khoản đã được xoá.",
                icon: "success"
            });
        }
    });
}

$("#isActive").on('change', function () {
    let isActiveValue = $(this).is(':checked');
    let alertActive = (isActiveValue)? "Kích hoạt tài khoản thành công" : "Khoá tài khoản thành công!"
    $.ajax({
        url: window.API_URL + `/AccountManager/EditUserAuth/${idTarget}`,
        type: 'PUT',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        contentType: 'application/json',
        data: JSON.stringify(isActiveValue),
        success: function (response) {
            swal.fire({
                title: `${alertActive}`,
                icon: "success",
                draggable: true
            }).then(() => {
                viewUser(idTarget);
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi cập nhật vai trò!");
        }
    });
});

function openModalCreate() {
    $('#createAccountModal').modal('show');
    loadRolesCreate();
}