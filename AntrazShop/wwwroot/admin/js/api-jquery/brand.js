$(function () {
    loadproducts();
});

//get
function loadproducts() {
    $.ajax({
        url: 'https://localhost:7092/api/Brand',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            $("#list-brand").empty();
            $.each(response, function (index, product) {

            }
        }
    });
}