//chạy lúc load trang
$(function () {
    initializeData();
    loadData();
    setPaginationButtonStyle();
});

//hàm khởi tạo biến ban đầu
function initializeData() {
    $("#numberShowing").val(10);
}

var orderTarget;

function setDataSearch() {
    pager.numberShowing = $("#numberShowing").val();
    pager.status = $("#status-order").val();
    pager.search = $("#search").val();
}

var Orders = [];

function loadData() {
    setDataSearch();
    $.ajax({
        url: window.API_URL + `/OrderManager/Orders/${pager.currentPage}/${pager.numberShowing}?ordercode=${pager.search}&status=${pager.status}`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response);

            Orders = response.orders;

            $("#table-data").empty();
            $('#pageNumber li:nth-child(n+3):nth-last-child(n+3)').remove();

            pager.totalPage = response.pagination.totalPage;

            $("#totalItem").text(`Có ${response.pagination.totalItems} đơn hàng`);

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

            $.each(Orders, function (index, order) {
                let dateCreate = new Date(order.createAt).toLocaleDateString('vi-VN');
                let formattedTotal = order.total.toLocaleString('vi-VN');

                let status = getStatusBadge(order.status);

                //Hiển thị data
                $("#table-data").append(
                    `<tr style="height:60px" class="antraz-table-list">
                       <th class="antraz-table-item">
                           <div style="font-weight: bold;" class="body-text">${order.orderCode}</div>
                       </th>
                       <th class="antraz-table-item">
                           <div class="body-text">${order.name}</div>
                       </th>
                       <th class="antraz-table-item">
                           <div class="body-text">${order.email}</div>
                       </th>
                       <th class="antraz-table-item">
                           <div class="body-text">${formattedTotal}</div>
                       </th>
                       <th class="antraz-table-item">
                           <div class="body-text">${dateCreate}</div>
                       </th>
                       <th class="antraz-table-item">
                           ${status}
                       </th>
                       <th class="antraz-table-item">
                           <div class="list-icon-function">
                               <div class="item eye" data-bs-target="#viewModal" onclick="viewModal('${order.orderCode}')">
                                   <i class="icon-eye"></i>
                               </div>
                           </div>
                       </th>   
                   </tr>`
                )
            });

            setPaginationButtonStyle();
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi tải danh sách đơn hàng!");
        }
    })
}

function getStatusBadge(status) {
    const statusStyles = {
        "Chờ xử lý": "color: #f59e0b; background-color: #fef3c7;",
        "Đang giao": "color: #3b82f6; background-color: #dbeafe;",
        "Đã huỷ": "color: #ef4444; background-color: #fee2e2;",
        "Đã giao": "color: #22c55e; background-color: #dcfce7;",
        "Giao thất bại": "color: #8b5cf6; background-color: #ede9fe;",
        "Hoàn thành": "color: #059669; background-color: #a7f3d0;"
    };
    const style = statusStyles[status] || "color: #6b7280; background-color: #f3f4f6;";
    return `<div class="body-text" style="${style} font-weight: bold; padding: 8px 12px; border-radius: 6px; text-align: center;">${status}</div>`;
}

function viewModal(orderCode) {
    $('#viewModal').modal('show');
    orderTarget = Orders.find(o => o.orderCode === orderCode);

    console.log(orderTarget);
    // Set basic info
    $("#order-code").text(orderTarget.orderCode);
    $("#email-user").text(orderTarget.email);
    $("#name-user").text(orderTarget.name);
    $("#phone-number").text(orderTarget.phoneNumber);
    $("#address").text(orderTarget.address);
    $("#status").text(orderTarget.status);

    // Handle buttons and status styling
    handleOrderStatusUI(orderTarget.status);

    $("#create-at").text(orderTarget.createAt);
    $("#last-update-time").text(orderTarget.lastStatusDate);

    // Thêm CSS nếu chưa có
    if (!$('#history-status-css').length) {
        $('head').append(`
        <style id="history-status-css">
            .history-item {
                display: flex;
                align-items: flex-start;
                padding: 16px;
                margin-bottom: 12px;
                background: #f8f9fa;
                border-radius: 8px;
                border-left: 4px solid #007bff;
                transition: all 0.3s ease;
            }
            .history-item:hover {
                background: #e3f2fd;
                transform: translateX(4px);
                box-shadow: 0 4px 12px rgba(0,123,255,0.15);
            }
            .history-item:last-child {
                margin-bottom: 0;
            }
            .history-icon {
                width: 40px;
                height: 40px;
                border-radius: 50%;
                background: #007bff;
                display: flex;
                align-items: center;
                justify-content: center;
                color: white;
                font-weight: bold;
                font-size: 14px;
                margin-right: 16px;
                flex-shrink: 0;
            }
            .history-content {
                flex: 1;
            }
            .history-status {
                font-weight: 600;
                font-size: 16px;
                color: #2c3e50;
                margin-bottom: 4px;
            }
            .history-time {
                font-size: 14px;
                color: #6c757d;
            }
        </style>
    `);
    }

    // Format thời gian
    function formatDateTime(dateString) {
        const date = new Date(dateString);
        const day = date.getDate().toString().padStart(2, '0');
        const month = (date.getMonth() + 1).toString().padStart(2, '0');
        const year = date.getFullYear();
        const hours = date.getHours().toString().padStart(2, '0');
        const minutes = date.getMinutes().toString().padStart(2, '0');

        return `${day}/${month}/${year} ${hours}:${minutes}`;
    }


    displayOrderDetails();
    $("#history-status").empty();
    $.each(orderTarget.orderStatusLogVMs, function (index, history) {
        console.log(history);

        const formattedTime = formatDateTime(history.createAt);
        const statusIcon = index + 1;

        // Xác định màu dựa trên status bằng switch case
        let borderColor = '#6b7280';
        let iconColor = '#6b7280';

        switch (true) {
            case history.status.includes('Chờ xử lý'):
                borderColor = '#f59e0b';
                iconColor = '#f59e0b';
                break;
            case history.status.includes('Đang giao'):
                borderColor = '#3b82f6';
                iconColor = '#3b82f6';
                break;
            case history.status.includes('Đã giao'):
                borderColor = '#22c55e';
                iconColor = '#22c55e';
                break;
            case history.status.includes('Hoàn thành'):
                borderColor = '#059669';
                iconColor = '#059669';
                break;
            case history.status.includes('Đã huỷ'):
                borderColor = '#ef4444';
                iconColor = '#ef4444';
                break;
            case history.status.includes('Giao thất bại'):
                borderColor = '#8b5cf6';
                iconColor = '#8b5cf6';
                break;
            case history.status.includes('khởi tạo'):
                borderColor = '#6b7280';
                iconColor = '#6b7280';
                break;
            default:
                borderColor = '#007bff';
                iconColor = '#007bff';
        }

        $("#history-status").append(`
   <div class="history-item" style="border-left: 4px solid ${borderColor};">
       <div class="history-icon" style="background: ${iconColor};">
           ${statusIcon}
       </div>
       <div class="history-content">
           <div class="history-status">${history.status}</div>
           <div class="history-time">${formattedTime}</div>
       </div>
   </div>
`);
    });

}

function handleOrderStatusUI(status) {
    const statusConfig = {
        'Chờ xử lý': {
            buttons: { shipping: true, reject: true, complete: false, failed: false, reactivate: false },
            css: { 'color': '#f59e0b', 'font-weight': 'bold' }
        },
        'Đang giao': {
            buttons: { shipping: false, reject: false, complete: true, failed: true, reactivate: false },
            css: { 'color': '#3b82f6', 'font-weight': 'bold' }
        },
        'Đã huỷ': {
            buttons: { shipping: false, reject: false, complete: false, failed: false, reactivate: true },
            css: { 'color': '#ef4444', 'font-weight': 'bold' }
        },
        'Đã giao': {
            buttons: { shipping: false, reject: false, complete: false, failed: false, reactivate: false },
            css: { 'color': '#22c55e', 'font-weight': 'bold' }
        },
        'Giao thất bại': {
            buttons: { shipping: true, reject: true, complete: false, failed: false, reactivate: false },
            css: { 'color': '#ef4444', 'font-weight': 'bold' }
        },
        'Hoàn thành': {
            buttons: { shipping: false, reject: false, complete: false, failed: false, reactivate: false },
            css: { 'color': '#22c55e', 'font-weight': 'bold' }
        },
        'Giao thất bại': {
            buttons: { shipping: false, reject: false, complete: false, failed: false, reactivate: false },
            css: { 'color': '#8b5cf6', 'font-weight': 'bold' }
        }
    };

    const config = statusConfig[status] || {
        buttons: { shipping: false, reject: false, complete: false, reactivate: false },
        css: { 'color': '#6b7280', 'background-color': '#f3f4f6' }
    };

    // Show/hide buttons
    $("#btn-shipping").toggle(config.buttons.shipping);
    $("#btn-reject").toggle(config.buttons.reject);
    $("#btn-complete").toggle(config.buttons.complete);
    $("#btn-failed").toggle(config.buttons.failed);
    $("#btn-reactivate").toggle(config.buttons.reactivate);

    // Apply CSS
    $("#status").css(config.css);
}

function displayOrderDetails() {
    $("#list-item-checkout").empty();
    let totalPrice = 0;
    let totalDiscountAmount = 0;

    $.each(orderTarget.orderDetails, function (index, item) {
        totalPrice += (item.price * item.quantity);
        totalDiscountAmount += (item.discountAmount * item.quantity);

        const colorCapacity = item.colorName + ' ' + item.capacityValue;
        const price = (item.price * item.quantity).toLocaleString('vi-VN') + ' VNĐ';

        $("#list-item-checkout").append(`
           <li class="product-item gap14">
               <div class="image no-bg">
                   <img src="/admin/imgs/product/${item.folderImage}/${item.productImage}" alt="">
               </div>
               <div class="flex items-center justify-between gap40 flex-grow">
                   <div class="name">
                       <div class="text-tiny mb-1">${colorCapacity}</div>
                       <a href="product-list.html" class="body-title-2">${item.productName}</a>
                   </div>
                   <div class="name">
                       <div class="text-tiny mb-1">Số lượng</div>
                       <div class="body-title-2">${item.quantity}</div>
                   </div>
                   <div class="name">
                       <div class="text-tiny mb-1">Tổng tiền</div>
                       <div class="body-title-2">${price}</div>
                   </div>
               </div>
           </li>       
       `);
    });

    // Update totals
    $("#total-price").text(totalPrice.toLocaleString('vi-VN') + ' VNĐ');
    $("#total-discount-amount").text(totalDiscountAmount.toLocaleString('vi-VN') + ' VNĐ');
    $(".total-price-last").text(orderTarget.total.toLocaleString('vi-VN') + ' VNĐ');
}

$("#btn-shipping").click(function () {
    updateOrderStatus('Đang giao');
});

$("#btn-reject").click(function () {
    updateOrderStatus('Đã huỷ');
});

$("#btn-complete").click(function () {
    updateOrderStatus('Đã giao');
});

$("#btn-reactivate").click(function () {
    updateOrderStatus('Chờ xử lý');
});
$("#btn-failed").click(function () {
    updateOrderStatus('Giao thất bại');
});

function updateOrderStatus(status) {
    Swal.fire({
        title: "Bạn chắc chứ?",
        text: `Chuyển sang trạng thái ${status}!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Huỷ"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: window.API_URL + `/OrderManager/Order?orderCode=${orderTarget.orderCode}&status=${status}`,
                method: 'PUT',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
                success: function (response) {
                    Swal.fire({
                        position: "center",
                        icon: "success",
                        title: "Cập nhật thành công!",
                        showConfirmButton: false,
                        timer: 1500
                    });
                    $("#status").text(status);
                    handleOrderStatusUI(status);
                    (async () => {
                        $('#status-order').val('');
                        loadData();
                        setTimeout(() => {
                            viewModal(orderTarget.orderCode);
                        }, 500);
                    })();
                },
                error: function (xhr, status, error) {
                    handleAjaxError(xhr, status, error, "Lỗi khi cập nhật đơn hàng");
                }
            });
        }
    });

}

//Hàm khi đóng modal
$('#viewModal').on('hide.bs.modal', function () {
    loadData();
});
