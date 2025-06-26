var pager = {
    numberShowing: 10,
    totalPage: 1,
    currentPage: 1,
    search: ''
};

function setPagerData() {
    pager.numberShowing = $("#numberShowing").val();
    pager.search = $("#search").val();
}

function setPaginationViewData() {

};

//Hàm loadData phải có sau khi ấn hàm loaddata xong thì set lại giá trị của
//pager: currentPage, totalItem, totalPage

//Trang đầu
$("#firstPageButton").click(function () {
    pager.currentPage = 1;
    loadData();
});

//Trang cuối
$("#lastPageButton").click(function () {
    pager.currentPage = pager.totalPage;
    loadData();
});

//Trang sau
$("#nextPageButton").click(function () {
    pager.currentPage++;
    loadData();
});

//Trang trước
$("#prevPageButton").click(function () {
    pager.currentPage--;
    loadData();
});

function setPaginationButtonStyle() {
    if (pager.currentPage == 1) {

        $("#firstPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
        $("#prevPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
    } else {

        $("#firstPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
        $("#prevPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
    }

    if (pager.currentPage == pager.totalPage) {

        $("#nextPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
        $("#lastPageButton").parent().css({ "pointer-events": "none", "opacity": "0.2" });
    } else {

        $("#nextPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
        $("#lastPageButton").parent().css({ "pointer-events": "auto", "opacity": "1" });
    }
}

// Chuyển trang bằng cách ấn chân trang
$(document).ready(function () {
    $(document).on('click', '.page-number-link', function () {

        pager.currentPage = $(this).text();;
        loadData();
    });

    $(".form-search").submit(function (event) {
        pager.currentPage = 1;
        event.preventDefault();
        loadData();
    });
});

//Hàm lúc thay đổi số lượng item trong 1 trang
$("#numberShowing").change(function () {
    pager.currentPage = 1;
    loadData();
});
