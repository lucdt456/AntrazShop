$(function () {
    loadproducts();
});

//get
function loadproducts() {
    $.ajax({
        url: window.API_URL + `/Brand`,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            $("#list-brand").empty();
            $.each(response, function (index, product) {

            }
        }
    });
}