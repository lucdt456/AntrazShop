//chạy lúc load trang
$(function () {
    loadData();
});

var orderTarget;

function setDataSearch() {
    pager.numberShowing = $("#numberShowing").val();
    pager.status = $("#status-order").val();
    pager.search = $("#search").val();
}

var Orders = [];

function loadData() {
    var userId = localStorage.getItem('id-claim');
    $.ajax({
        url: window.API_URL + `/OrderManager/${userId}`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            Orders = response;
            $("#list-orders-user").empty();
            console.log(Orders);
            $.each(Orders, function (index, order) {
                let firstOrderDetail = order.orderDetails[0];
                let statusColor = '';

                switch (order.status) {
                    case 'Chờ xử lý':
                        statusColor = '#ffc107'; 
                        break;
                    case 'Đang giao':
                        statusColor = '#007bff'; 
                        break;
                    case 'Đã hủy': 
                        statusColor = '#dc3545'; 
                        break;
                    case 'Đã giao':
                        statusColor = '#28a745'; 
                        break;
                    case 'Hoàn thành': 
                        statusColor = '#28a745'; 
                        break;
                    case 'Giao thất bại':
                        statusColor = '#fd7e14';
                        break;
                    default:
                        statusColor = '#6c757d';
                }

                $("#list-orders-user").append(`
                  <div style="cursor: pointer;" onclick="viewOrderDetail('${order.orderCode}')" class="tplocation__item d-flex align-items-center">
                       <div class="tplocation__img mr-20">
                          <img  src="/admin/imgs/product/${firstOrderDetail.folderImage}/${firstOrderDetail.productImage}" alt=""  style="max-width: 180px; max-height: 180px; object-fit: contain;">
                       </div>
                       <div class="tplocation__text">
                           <h4 class="tplocation__text-title">Mã đơn hàng: ${order.orderCode}</h4>
                           <div class="tplocation__content tplocation__content-two">
                               <ul>
                                   <li>
                                       Địa chỉ: ${order.address}
                                   </li>
                                   <li>
                                       SĐT: ${order.phoneNumber}
                                   </li>
                                   <li>
                                       Tạo lúc: ${order.createAt}
                                   </li>
                                   <li>
                                       Trạng thái: <span style="color: ${statusColor}; font-weight: bold;">${order.status}</span>
                                   </li>
                               </ul>
                           </div>
                       </div>
                   </div>
              `);
            });
            viewOrderDetail(orderTarget.orderCode);
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi tải danh sách đơn hàng!");
        }
    })
}

function viewOrderDetail(orderCode) {
    $("#order-information").show();
    orderTarget = Orders.find(o => o.orderCode === orderCode);
    if (orderTarget.status == "Đã giao") {
        $('#btn-complete-order').show();
    }
    else {
        $('#btn-complete-order').hide();
    }
    console.log(orderTarget)
    $("#list-item-details").empty();
    let totalPrice = 0;
    let totalDiscountAmount = 0;

    $('#order-code-details').text(`Mã đơn hàng: ${orderCode}`);
    $.each(orderTarget.orderDetails, function (index, item) {
        totalPrice += (item.price * item.quantity);
        totalDiscountAmount += (item.discountAmount * item.quantity);
        const price = (item.price * item.quantity).toLocaleString('vi-VN') + ' VNĐ';
        let colorCapacity = item.colorName + ' ' + item.capacityValue;
        $("#list-item-details").append(`  
                   <tr>
                      <td class="product-thumbnail" style="display: flex; align-items: center; gap: 15px;">
                          <img src="/admin/imgs/product/${item.folderImage}/${item.productImage}" alt="" style="width: 80px; height: 80px; object-fit: contain;">
                          <div>
                              <a href="/shop/productdetail?id=${item.productId}" style="font-weight: bold; text-decoration: none;">${item.productName}</a>
                          </div>
                      </td>
                      <td class="product-name">
                          <span class="amount">${colorCapacity}</span>
                      </td>
                      <td class="product-quantity">
                         <span class="amount">${item.quantity}</span>
                      </td>
                      <td class="product-subtotal">
                          <span class="amount">${price}</span>
                      </td>
                  </tr>
      `);
    });

    $("#details-total-price").text(totalPrice.toLocaleString('vi-VN') + ' VNĐ');
    $("#details-discount-amount").text(totalDiscountAmount.toLocaleString('vi-VN') + ' VNĐ');
    $("#details-total-price-sale").text(orderTarget.total.toLocaleString('vi-VN') + ' VNĐ');
    $("#details-total-price-sale").text(orderTarget.total.toLocaleString('vi-VN') + ' VNĐ');
    $("#details-discount-name").text(orderTarget.name);
    $("#details-discount-phonenumber").text(orderTarget.phoneNumber);
    $("#details-discount-address").text(orderTarget.address);

    $("#details-discount-status").text(orderTarget.status);
    $("#details-discount-time").text(orderTarget.lastStatusDate);

    let statusColor = '';

    switch (orderTarget.status) {
        case 'Chờ xử lý':
            statusColor = '#ffc107';
            break;
        case 'Đang giao':
            statusColor = '#007bff';
            break;
        case 'Đã hủy':
            statusColor = '#dc3545';
            break;
        case 'Đã giao':
            statusColor = '#28a745';
            break;
        case 'Hoàn thành':
            statusColor = '#28a745';
            break;
        case 'Giao thất bại':
            break;
        default:
            statusColor = '#6c757d';
    }
    $("#details-discount-status").css({
        'color': statusColor,
        'font-weight': 'bold'
    });


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
$('#btn-complete-order').on('click', function () {
    Swal.fire({
        title: "Bạn chắc chứ?",
        text: `Xác nhận đã nhận được hàng!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Huỷ"
    }).then((result) => {
        if (result.isConfirmed) {
            let status = 'Hoàn thành';
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
                        title: "Đơn hàng đã hoàn thành!",
                        showConfirmButton: false,
                        timer: 1000
                    });
                    loadData();
                    
                },
                error: function (xhr, status, error) {
                    handleAjaxError(xhr, status, error, "Lỗi khi cập nhật đơn hàng");
                }
            });
        }
    });
});