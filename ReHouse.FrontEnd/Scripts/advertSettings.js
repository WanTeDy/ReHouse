$(document).ready(function () {
    var url = "/sale/load/";
    $('#button-more').click(function () {

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

        lazyload(obj, url, false);
    });

    $('#search').click(function () {
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

        lazyload(obj, url, true);
    });
});