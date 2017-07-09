$(document).ready(function () {
    var url = "/sale/load/";

    function sale_lazyload(search) {
        var districtID = $('#district').val(),
                priceMin = $('#price').val(),
                priceMax = $('#price_max').val(),
                category = $('#category').val(),
                trimcondition = $('#trimcondition').val();
        var obj = {
            "DistrictId": districtID,
            "PriceMin": priceMin,
            "PriceMax": priceMax,
            "CategoryId": category,
            "TrimconditionId": trimcondition
        }

        lazyload(obj, url, search);
    }

    $('#button-more').click(function () {
        sale_lazyload(false);

    });

    $('#search').click(function () {
        sale_lazyload(true);

    });
});