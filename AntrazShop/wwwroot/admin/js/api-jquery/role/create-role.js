$(function () {
    LoadAllPermission();
});

function LoadAllPermission() {
    $.ajax({
        url: window.API_URL + `/Permission/Groups`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            renderPermissionGroups(response);
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi load danh sách quyền");
        }
    });
}

function renderPermissionGroups(groups) {
    // Xóa nội dung cũ (giữ lại header)
    $("#list-permission-group .flex.flex-column").remove();

    // Render từng nhóm quyền
    $.each(groups, function (index, group) {
        // Skip nhóm ERROR
        if (group.groupName === "ERROR") {
            return true; // continue
        }

        let groupHtml = `
           <ul class="flex flex-column">
               <li class="item gap20">
                   <!-- Tên nhóm -->
                   <div class="body-text" style="font-weight: 600; margin-bottom: 16px; color: #2c3e50; padding-bottom: 8px; border-bottom: 1px solid #eee;">
                       ${group.groupName}
                   </div>
               </li>
               <li class="item gap20" style="display: flex; flex-wrap: wrap; align-items: flex-start;">
                   <!-- Checkbox chọn tất cả -->
                   <div class="flex items-center gap10" style="width: 100%; margin-bottom: 12px;">
                       <input class="group-checkbox" type="checkbox" data-group="${group.idGroups}">
                       <label><div class="body-text" style="color: #007bff; font-weight: 600;">Chọn tất cả</div></label>
                   </div>
       `;

        // Render từng permission trong nhóm - tối đa 4 cột
        $.each(group.permissions, function (permIndex, permission) {
            groupHtml += `
                   <div class="flex items-center gap10" style="width: calc(25% - 15px); min-width: 200px; margin-bottom: 8px; margin-right: 20px;">
                       <input class="permission-checkbox" 
                              type="checkbox" 
                              data-group="${group.idGroups}" 
                              data-permission="${permission.id}" 
                              value="${permission.name}">
                       <label style="word-wrap: break-word;">
                           <div class="body-text">${permission.nameController}</div>
                       </label>
                   </div>
           `;
        });

        groupHtml += `
               </li>
           </ul>
       `;

        // Append vào container
        $("#list-permission-group").append(groupHtml);
    });

    // Thêm event handlers sau khi render xong
    addCheckboxEvents();
}

function addCheckboxEvents() {
    // Event cho checkbox "Chọn tất cả" nhóm
    $('.group-checkbox').off('change').on('change', function () {
        const groupId = $(this).data('group');
        const isChecked = $(this).is(':checked');

        // Check/uncheck tất cả permission trong nhóm
        $(`.permission-checkbox[data-group="${groupId}"]`).prop('checked', isChecked);
    });

    // Event cho checkbox permission riêng lẻ
    $('.permission-checkbox').off('change').on('change', function () {
        const groupId = $(this).data('group');
        const totalPermissions = $(`.permission-checkbox[data-group="${groupId}"]`).length;
        const checkedPermissions = $(`.permission-checkbox[data-group="${groupId}"]:checked`).length;

        // Update trạng thái checkbox "Chọn tất cả" của nhóm
        const groupCheckbox = $(`.group-checkbox[data-group="${groupId}"]`);

        if (checkedPermissions === 0) {
            groupCheckbox.prop('checked', false).prop('indeterminate', false);
        } else if (checkedPermissions === totalPermissions) {
            groupCheckbox.prop('checked', true).prop('indeterminate', false);
        } else {
            groupCheckbox.prop('checked', false).prop('indeterminate', true);
        }
    });
}

function createRole() {
    const permissionIds = getSelectedPermissionIds();
    const roleName = $('#role-name-form').val().trim();
    const roleDescription = $('#role-description-form').val().trim();

    if (!roleName) {
        alert('Vui lòng nhập tên vai trò');
        return;
    }

    if (permissionIds.length === 0) {
        alert('Vui lòng chọn ít nhất một quyền');
        return;
    }

    const roleData = {
        name: roleName,
        description: roleDescription,
        permissionIds: permissionIds
    };

    console.log("Creating role with data:", roleData);

    $.ajax({
        url: window.API_URL + `/Role/Create`,
        type: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(roleData),
        success: function (response) {
            console.log('Success:', response);
            swal.fire({
                title: `Tạo vai trò thành công`,
                icon: "success",
                draggable: true
            }).then(() => {
                window.location.href = '/admin/role';
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi tạo vai trò");
        }
    });
}

function getSelectedPermissionIds() {
    const permissionIds = [];

    $('.permission-checkbox:checked').each(function () {
        const permissionId = $(this).data('permission');
        permissionIds.push(permissionId);
    });

    return permissionIds;
}