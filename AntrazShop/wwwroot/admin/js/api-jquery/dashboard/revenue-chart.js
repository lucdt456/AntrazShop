$(function () {
    loadRevenue();
});

function loadRevenue() {
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
                name: "$",
                data: [45, 52, 38, 45, 49, 43, 40, 45, 52, 38, 45, 190]
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
                "Jan",
                "Feb",
                "Mar",
                "Apr",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Oct",
                "Nov",
                "Dec",
            ]
        }
    };
    let lineChart = new ApexCharts(
        document.querySelector("#recent-revenue-chart"),
        options
    );
    lineChart.render();
}