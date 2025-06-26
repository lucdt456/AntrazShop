//chạy lúc load trang
$(function () {
    initializeData();
    loadData();
    setPaginationButtonStyle();
});

//hàm khởi tạo biến ban đầu
function initializeData() {
    $("#numberShowing").val(10);
    setPagerData();
};

function loadData() {
    setPagerData();
    $.ajax({
        url: `https://localhost:7092/api/AccountManager/Customer/${pager.currentPage}/${pager.numberShowing}?search=${pager.search}`,
        type: 'GET',
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
                                <div class="item eye" data-bs-toggle="modal" data-bs-target="#viewModal" onclick="viewProduct(${user.id})">
                                 <i class="icon-eye"></i>
                                 </div>
                                <div class="item edit" onclick="goToEdit(${user.id})">
                                    <i class="icon-edit-3"></i>
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
            console.error("Lỗi: ", error);
        }
    })
}

