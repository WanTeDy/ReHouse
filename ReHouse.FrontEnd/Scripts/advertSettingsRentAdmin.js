$(document).ready(function () {
    var url = "/cabinet/rent/load";

    function rent_lazyload(search) {
        var districtID = $('#district').val(),
                priceMin = $('#price').val(),
                    priceMax = $('#price_max').val(),
                rentPeriodType = $('#rent_type').val(),
                category = $('#category').val(),
                trimcondition = $('#trimcondition').val(),
                    managerId = $('#managers').val(),
                isOnlyUser = $('#isonlyuser').val();
        var obj = {
            "DistrictId": districtID,
            "PriceMin": priceMin,
            "PriceMax": priceMax,
            "RentPeriodType": rentPeriodType,
            "CategoryId": category,
            "TrimconditionId": trimcondition,
            "UserId": managerId,
            "IsOnlyUser": isOnlyUser
        }

        lazyload(obj, url, search);
    }

    $('#button-more').click(function () {
        rent_lazyload(false);

        
    });

    $('#search').click(function () {
        rent_lazyload(true);

    });
});