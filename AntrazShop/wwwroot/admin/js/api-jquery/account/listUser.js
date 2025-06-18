//chạy lúc load trang
$(function () {
    $("#sizePage").val(10);
    size = $("#sizePage").val();
    LoadSearchUser(1, size);
});

function LoadSearchUser(page, size) {
    $.ajax({
        url: `https://localhost:7092/api/Product?page=${page}&size=${size}`,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            $("#product-list").empty();
            $('#pageNumber li:nth-child(n+3):nth-last-child(n+3)').remove();
            // Xử lý các số chân trang
            pager.totalPage = response.pagination.totalPage;

            $("#totalProduct").text(`Có ${response.pagination.totalItems} sản phẩm`);
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

            //xử lý chân trang đầu và trang cuối
            if (pager.currentPage == 1) {

                $("#firstPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
                $("#leftPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
            } else {

                $("#firstPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
                $("#leftPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
            }

            if (pager.currentPage == pager.totalPage) {

                $("#rightPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
                $("#endPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
            } else {

                $("#rightPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
                $("#endPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
            }

        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    })
}

