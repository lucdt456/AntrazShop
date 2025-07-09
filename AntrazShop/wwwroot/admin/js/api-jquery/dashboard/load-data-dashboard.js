$(function () {
	loadDashboardStats();
});

function loadDashboardStats() {
	$.ajax({
		url: window.API_URL + `/Dashboard/DashboardStats`,
		type: 'GET',
		headers: {
			'Authorization': 'Bearer ' + token
		},
		dataType: 'json',
		success: function (response) {
			console.log(response);
			$('#total-products-sold').text(response.totalProductsSold);
			$('#total-revenue').text(response.totalRevenue.toLocaleString('vi-VN'));
			$('#total-orders').text(response.totalOrders);
			$('#total-new-customers').text(response.totalNewCustomers);

			let productsSoldGrowthRate = CalculateGrowthRate(response.productsSoldGrowthRate);

			$('#productsSoldGrowthRate').html(productsSoldGrowthRate);

			let ordersGrowthRate = CalculateGrowthRate(response.ordersGrowthRate);

			$('#ordersGrowthRate').html(ordersGrowthRate);

			let newCustomersGrowthRate = CalculateGrowthRate(response.newCustomersGrowthRate);

			$('#newCustomersGrowthRate').html(newCustomersGrowthRate);

			let revenueGrowthRate = CalculateGrowthRate(response.revenueGrowthRate);

			$('#revenueGrowthRate').html(revenueGrowthRate);
		},
		error: function (xhr, status, error) {
			handleAjaxError(xhr, status, error, "Lỗi khi load dữ liệu!");
		}
	});
}
//Hàm chuyển đổi thông số tăng trưởng sang text
function CalculateGrowthRate(growValue) {
	if (growValue > 0) return `<div class="box-icon-trending up">
                        <i class="icon-trending-up"></i>
                        <div class="body-title number">${growValue}%</div>
                    </div>`;
	if (growValue < 0) return ` <div class="box-icon-trending down">
                            <i class="icon-trending-down"></i>
                            <div class="body-title number">${growValue}%</div>
                        </div>`;
	if (growValue = 0) return `<div class="box-icon-trending">
                            <i class="icon-trending-up"></i>
                            <div class="body-title number">${growValue}%</div>
                        </div>`;


}

