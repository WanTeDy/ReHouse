$(document).ready(function () {
    var url = "/cabinet/sale/load/";
    $('#button-more').click(function () {

        var districtID = $('#district').val(),
                priceMin = $('#price').val(),
                    priceMax = $('#price_max').val(),
                category = $('#category').val(),
                trimcondition = $('#trimcondition').val(),
                    managerId = $('#managers').val(),
                isOnlyUser = $('#isonlyuser').val();
        var obj = {
            "DistrictId": districtID,
            "PriceMin": priceMin,
            "PriceMax": priceMax,
            "CategoryId": category,
            "TrimconditionId": trimcondition,
            "UserId": managerId,
            "IsOnlyUser": isOnlyUser
        }

        lazyload(obj, url, false);
    });

    $('#search').click(function () {
        var districtID = $('#district').val(),
               priceMin = $('#price').val(),
                    priceMax = $('#price_max').val(),
               category = $('#category').val(),
               trimcondition = $('#trimcondition').val(),
                    managerId = $('#managers').val(),
               isOnlyUser = $('#isonlyuser').val();
        var obj = {
            "DistrictId": districtID,
            "PriceMin": priceMin,
            "PriceMax": priceMax,
            "CategoryId": category,
            "TrimconditionId": trimcondition,
            "UserId": managerId,
            "IsOnlyUser": isOnlyUser
        }

        lazyload(obj, url, true);
    });
});