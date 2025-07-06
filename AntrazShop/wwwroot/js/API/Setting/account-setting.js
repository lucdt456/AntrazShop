//Biến toàn cục cho edit profile
$(function () {
    viewInformation();
});

var selectedEditAvatarFile = null;

//xem trước avatar khi edit profile
$(document).ready(function () {
    $("#edit-avatar-input").on("change", function (event) {
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

            selectedEditAvatarFile = file;
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#edit-avatar-preview").attr("src", e.target.result);
            };
            reader.readAsDataURL(file);
        }
    });
});

//Nút đổi mật khẩu
$("#btn-change-password").click(function () {
    $("#change-password-area").fadeToggle(300);
    $("#btn-change-password").hide();
});

//Nút huỷ đổi mật khẩu
$("#btn-cancle-change-password").click(function () {
    $("#change-password-area").hide();
    $("#btn-change-password").fadeToggle(300);
});

//Nút đổi thông tin
$("#btn-edit-information").click(function () {
    $(".edit-information").attr("disabled", false);
    $("#edit-information-button").fadeToggle(300);
});

//Nút huỷ đổi thông tin
$("#btn-cancle-edit-information").click(function () {
    viewInformation();
    $(".edit-information").attr("disabled", true);
    $("#edit-information-button").fadeToggle(300);
});

//Nút xác nhận thay đổi mật khẩu
$("#btn-save-change-password").click(function () {
    $(".error-message").text("");
    let currentPassword = $("#create-user-password").val();
    let newPassword = $("#new-user-password").val();
    let email = $("#edit-user-email").val();

    var isValid = validateChangePassword();
    if (!isValid) {
        return;
    }

    let changePasswordData = {
        email: email,
        password: currentPassword,
        newPassword: newPassword
    };

    $.ajax({
        url: window.API_URL + '/Account/change-password',
        type: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(changePasswordData),
        success: function (response) {
            Swal.fire({
                title: "Đổi mật khẩu thành công!",
                icon: "success",
                draggable: true
            }).then(() => {
                $("#change-password-area").hide();
                $("#btn-change-password").fadeToggle(300);

                $("#create-user-password").val("");
                $("#new-user-password").val("");
                $("#new-user-repassword").val("");
                $(".error-message").text("");
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi đổi mật khẩu!");
        }
    });

   
});

function validateChangePassword() {
    let currentPassword = $("#create-user-password").val();
    let newPassword = $("#new-user-password").val();
    let confirmPassword = $("#new-user-repassword").val();

    let isValid = true;
    if (!currentPassword.trim()) {
        $("#create-user-password").closest("fieldset").find(".error-message").text("Không được để trống");
        isValid = false;
    }

    if (!newPassword.trim()) {
        $("#new-user-password").closest("fieldset").find(".error-message").text("Không được để trống");
        isValid = false;
    }

    if (!confirmPassword.trim()) {
        $("#new-user-repassword").closest("fieldset").find(".error-message").text("Không được để trống");
        isValid = false;
    }

    if (newPassword !== confirmPassword) {
        $("#new-user-repassword").closest("fieldset").find(".error-message").text("Mật khẩu không khớp");
        isValid = false;
    }
    return isValid;
}

function viewInformation() {
    let userId = localStorage.getItem("id-claim");
    $.ajax({
        url: window.API_URL + `/AccountManager/${userId}`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            $("#edit-user-name").val(response.name);
            $("#edit-user-email").val(response.email);
            $("#edit-user-birthday").val(response.birthday);
            $("#edit-user-gender").val(response.gender);
            $("#edit-user-phone").val(response.phoneNumber);
            $("#edit-user-address").val(response.address);
            $("#edit-user-hometown").val(response.hometown);
            $("#user-list-roles").text(response.roles.map(r => r.name).join(', '));
            $('#edit-avatar-preview').attr('src', `/admin/imgs/avatar/${response.avatar}`);

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

//Nút lưu thông tin mới
$("#btn-save-information").click(function () {
    let userId = localStorage.getItem("id-claim");
    let updateFormData = new FormData();
    updateFormData.append('Name', $("#edit-user-name").val());
    updateFormData.append('Gender', $("#edit-user-gender").val());
    updateFormData.append('Email', $("#edit-user-email").val());
    updateFormData.append('PhoneNumber', $("#edit-user-phone").val());
    updateFormData.append('Birthday', $("#edit-user-birthday").val());
    updateFormData.append('Hometown', $("#edit-user-hometown").val());
    updateFormData.append('Address', $("#edit-user-address").val());

    if (selectedEditAvatarFile) {
        updateFormData.append('Avatar', selectedEditAvatarFile);
    }

    $.ajax({
        url: window.API_URL + `/AccountManager/Edit/${userId}`,
        type: 'PUT',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        data: updateFormData,
        processData: false,
        contentType: false,
        success: function (response) {
            Swal.fire({
                title: "Cập nhật thông tin thành công!",
                icon: "success",
                draggable: true
            }).then(() => {
                selectedEditAvatarFile = null;
                viewInformation();

                $(".edit-information").attr("disabled", true);
                $("#edit-information-button").fadeToggle(300);
            });
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi cập nhật thông tin!");
        }
    });
   
});
