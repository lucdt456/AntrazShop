$(function () {
    loadRevenue();
});

function loadRevenue() {
    $.ajax({
        url: window.API_URL + `/Dashboard/RevenueOverview`,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            console.log(response);
            const formatted = response.map(num => num.toLocaleString('vi-VN'));

            let options = {
                chart: {
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
                colors: ["#10b981"],
                series: [
                    {
                        name: "Doanh thu (Triệu VNĐ)",
                        data: formatted
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
                    categories: [
                        "Tháng 1",
                        "Tháng 2",
                        "Tháng 3",
                        "Tháng 4",
                        "Tháng 5",
                        "Tháng 6",
                        "Tháng 7",
                        "Tháng 8",
                        "Tháng 9",
                        "Tháng 10",
                        "Tháng 11",
                        "Tháng 12",
                    ]
                }
            };
            let lineChart = new ApexCharts(
                document.querySelector("#recent-revenue-chart"),
                options
            );
            lineChart.render();
        },
        error: function (xhr, status, error) {
            handleAjaxError(xhr, status, error, "Lỗi khi load dữ liệu!");
        }
    });
  
}