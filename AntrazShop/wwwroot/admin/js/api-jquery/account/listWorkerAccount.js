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
                                <div class="item trash" onclick="deleteProduct(${user.id})">
                                    <i class="icon-trash-2"></i>
                                </div>
                            </div>
                        </th>   
                    </tr>`
                )

                setPaginationButtonStyle();
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi tải danh sách người dùng!");
        }
    })
}

function viewUser(id) {
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
                                            <label class="" for="${idRoleName}-off"><span class="body-title-2">Tẳt</span></label>
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
}
function cancleEditRole() {
    $("#list-roles").hide();
    $("#btn-cancle").hide();
    $("#btn-save").hide();
}

//Hàm khi đóng modal
$('#viewModal').on('hide.bs.modal', function () {
    loadData();
    $("#list-roles").hide();
    $("#btn-cancle").hide();
    $("#btn-save").hide();
});