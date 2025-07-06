//Biến toàn cục cho create account
var selectedAvatarFile = null;

//xem trước avatar khi tạo tài khoản
$(document).ready(function () {
    $("#create-avatar-input").on("change", function (event) {
        let file = event.target.files[0];
        if (file) {
            // Validate file type
            if (!file.type.startsWith('image/')) {
                Swal.fire({
                    title: "Lỗi!",
                    text: "Vui lòng chọn file hình ảnh!",
                    icon: "error"
                });
                $(this).val('');
                return;
            }

            // Validate file size (5MB)
            if (file.size > 5 * 1024 * 1024) {
                Swal.fire({
                    title: "Lỗi!",
                    text: "File ảnh quá lớn! Vui lòng chọn file nhỏ hơn 5MB.",
                    icon: "error"
                });
                $(this).val('');
                return;
            }

            selectedAvatarFile = file;
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#create-uploadIcon").hide();
                $("#create-avatar-preview").closest(".img-preview-product").show();
                $("#create-avatar-preview").closest(".image-border").css("border", "none");
                $("#create-avatar-preview").attr("src", e.target.result).show();
            };
            reader.readAsDataURL(file);
        }
    });
});

//reset form create account
function resetCreateForm() {
    // Reset text inputs
    $("#create-user-name").val("");
    $("#create-user-email").val("");
    $("#create-user-gender").val("");
    $("#create-user-birthday").val("");
    $("#create-user-password").val("");
    $("#create-user-repassword").val("");
    $("#create-user-phone").val("");
    $("#create-user-address").val("");
    $("#create-user-hometown").val("");

    // Reset avatar
    $("#create-avatar-input").val("");
    $("#create-avatar-preview").closest(".image-border").removeAttr('style');
    $("#create-uploadIcon").show();
    $("#create-avatar-preview").closest(".img-preview-product").hide();

    // Reset roles về "Tắt"
    $('#list-roles-create input[type="radio"][value="0"]').prop('checked', true);

    // Reset error messages
    $(".error-message").text("");
    $("#create-error-message-image").text('');

    // Reset selected file
    selectedAvatarFile = null;
}


//validate input create account
function validateCreateInput() {
    $(".error-message").text("");
    let isValid = true;

    // Validate required fields
    $(".validate-input").each(function () {
        if ($(this).val().trim() == "") {
            $(this).closest("fieldset").find(".error-message").text("Không được để trống")
            isValid = false;
        }
    });

    // Validate password match
    if ($("#create-user-password").val() !== $("#create-user-repassword").val()) {
        $("#create-user-repassword").closest("fieldset").find(".error-message").text("Mật khẩu không khớp");
        isValid = false;
    }

    // Validate avatar (optional but if selected must be valid)
    if ($("#create-avatar-input").val() !== '' && !selectedAvatarFile) {
        $("#create-error-message-image").text('File ảnh không hợp lệ');
        isValid = false;
    }

    return isValid;
}

//Tạo tài khoản
function createAccount() {
    let isValid = validateCreateInput();

    if (isValid == true) {
        // Lấy selected roles từ modal create
        let selectedRoles = [];
        $('#list-roles-create input[type="radio"]:checked').each(function () {
            let value = $(this).val();
            if (value != "0") {
                selectedRoles.push(parseInt(value));
            }
        });

        let accountFormData = new FormData();

        accountFormData.append('Name', $("#create-user-name").val());
        accountFormData.append('Email', $("#create-user-email").val());
        accountFormData.append('Gender', $("#create-user-gender").val());
        accountFormData.append('Birthday', $("#create-user-birthday").val());
        accountFormData.append('Password', $("#create-user-password").val());
        accountFormData.append('PhoneNumber', $("#create-user-phone").val());
        accountFormData.append('Address', $("#create-user-address").val());
        accountFormData.append('Hometown', $("#create-user-hometown").val());
        accountFormData.append('IsWorkerAccount', true);

        // Thêm avatar nếu có
        if (selectedAvatarFile) {
            accountFormData.append('Avatar', selectedAvatarFile);
        }

        // Thêm roles
        selectedRoles.forEach(function (roleId) {
            accountFormData.append('Roles', roleId);
        });

        console.log(accountFormData);

        $.ajax({
            url: window.API_URL + '/AccountManager/Create',
            type: 'POST',
            headers: {
                'Authorization': 'Bearer ' + token
            },
            data: accountFormData,
            processData: false,
            contentType: false,
            success: function (response) {
                swal.fire({
                    title: "Tạo tài khoản thành công!",
                    icon: "success",
                    draggable: true
                }).then(() => {
                    $('#createAccountModal').modal('hide');
                    loadData(); // Reload table
                    resetCreateForm(); // Reset form
                });
            },
            error: function (xhr, status, error) {
                handleAjaxError(xhr, status, error, "Lỗi khi tạo tài khoản!");
            }
        });
    } else {
        console.log("Lỗi validate");
    }
}
function loadRolesCreate() {
    document.getElementById("table-history-visiable").style.display = "table";

    $.ajax({
        url: window.API_URL + `/Role/1/100`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            $("#list-roles-create").empty();
            console.log(response);

            $.each(response.roles, function (index, role) {
                let idRoleName = 'role-id-create-' + role.id;
                $("#list-roles-create").append(
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
            handleAjaxError(xhr, status, error, "Lỗi khi load vai trò!");
        }
    })
}

// Event khi đóng modal create
$('#createAccountModal').on('hide.bs.modal', function () {
    resetCreateForm();
});