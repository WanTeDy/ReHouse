$(document).ready(function () {
    var url = "/cabinet/sale/load/";

    function sale_lazyload(search) {
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

        lazyload(obj, url, search);
    }

    $('#button-more').click(function () {
        sale_lazyload(false);
    });

    $('#search').click(function () {
        
        sale_lazyload(true);

    });
});