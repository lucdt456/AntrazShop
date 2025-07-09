$(function () {
    loadRecentOrder();
    loadNewOrder();
});

var DataOrderOverView;
function loadOrder() {
    var options = {
        chart: {
            type: 'donut',  
            height: 350
        },
        series: [DataOrderOverView.completedOrderCount, DataOrderOverView.failedOrdersCount, DataOrderOverView.processOrderCount],
        labels: ['Đơn hoàn thành', 'Đơn thất bại', 'Đơn đang xử lý'],
        colors: ['#10b981', '#ef4444', '#3b82f6'], 
        gradients: [
            'linear-gradient(135deg, #10b981, #059669)', 
            'linear-gradient(135deg, #ef4444, #dc2626)', 
            'linear-gradient(135deg, #3b82f6, #2563eb)'  
        ],
        plotOptions: {
            pie: {
                donut: {
                    size: '50%',
                    labels: {
                        show: true,
                        total: {
                            show: true,
                            showAlways: true,
                            label: 'Tổng',
                            fontSize: '16px',
                            fontWeight: 600,
                            color: '#374151',
                            formatter: function (w) {
                                return w.globals.seriesTotals.reduce((a, b) => a + b, 0)
                            }
                        }
                    }
                }
            }
        },

        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val.toFixed(1) + "%"
            },
            style: {
                fontSize: '12px',
                fontWeight: 'bold',
                colors: ['#fff']
            }
        },

        legend: {
            position: 'bottom',
            offsetY: 10,
            fontSize: '14px',
            markers: {
                width: 12,
                height: 12,
                radius: 2 
            },
            itemMargin: {
                horizontal: 15,
                vertical: 5
            }
        }
    };

    var chart = new ApexCharts(document.querySelector("#order-chart"), options);
    chart.render();
}

function loadRecentOrder() {
    let dayCount = $('#recent-order-count-day').val();
    let urlAPI = window.API_URL + `/Dashboard/OrderOverView/${dayCount}`
    console.log(urlAPI);

    $.ajax({
        url: urlAPI,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response);
            DataOrderOverView = response;

            const nameDays = [];
            const today = new Date();

            for (let i = dayCount -1; i >= 0; i--) {
                const date = new Date(today);
                date.setDate(today.getDate() - i);

                const dayname = date.getDate();
                const month = date.getMonth() + 1;
                nameDays.push(`${dayname}/${month}`);
            }

            let options = {
                chart: {
                    height: 291,
                    type: "area",
                    zoom: {
                        enabled: false
                    },
                    toolbar: {
                        show: false,
                    },
                },
                dataLabels: {
                    enabled: false
                },
                colors: ["#2275fc"],
                series: [
                    {
                        name: "Số đơn",
                        data: response.newOrdersCountDetails
                    }
                ],
                fill: {
                    type: "gradient",
                    gradient: {
                        shadeIntensity: 1,
                        opacityFrom: 0.3,
                        opacityTo: 0.9,
                        stops: [0, 90, 100]
                    }
                },
                yaxis: {
                    show: false,
                },
                xaxis: {
                    labels: {
                        style: {
                            colors: '#95989D',
                        },
                    },
                    categories: nameDays
                }
            };
            let lineChart = new ApexCharts(
                document.querySelector("#recent-order-chart"),
                options
            );
            lineChart.render();
            loadOrder();
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi load dữ liệu!");
        }
    });
    
    
}

function loadNewOrder() {
    $.ajax({
        url: window.API_URL + `/OrderManager/Orders/1/100?status=Ch%E1%BB%9D%20x%E1%BB%AD%20l%C3%BD`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        dataType: 'json',
        success: function (response) {
            console.log(response.orders);
            $("#new-orders-list").empty();

            $.each(response.orders, function (index, order) {
                let dateCreate = formatDateTime(order.createAt);
                let formattedTotal = order.total.toLocaleString('vi-VN');

                let firstOrderDetail = order.orderDetails[0];
                //let status = getStatusBadge(order.status);

                //Hiển thị data
                $("#new-orders-list").append(
                    ` <li class="product-item gap14">
                            <div class="image small">
                            <img  src="/admin/imgs/product/${firstOrderDetail.folderImage}/${firstOrderDetail.productImage}" alt=""  style="max-width: 40px; max-height: 40px; object-fit: contain;">
                            </div>
                            <div class="flex items-center justify-between flex-grow gap10">
                                <div class="name">
                                    <a href="/admin/order" class="body-text">${order.orderCode}</a>
                                </div>
                                <div class="body-text">${formattedTotal}</div>
                                <div class="body-text">${dateCreate}</div>
                            </div>
                        </li>`
                )
            });

            setPaginationButtonStyle();
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi tải danh sách đơn hàng!");
        }
    })
}

function formatDateTime(dateString) {
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');

    return `${day}/${month}/${year} ${hours}:${minutes}`;
}