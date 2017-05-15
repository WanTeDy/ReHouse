$(document).ready(function () {
    var url = "/cabinet/sale/load/";
    $('#button-more').click(function () {

        var districtID = $('#district').val(),
                price = $('#price').val(),
                category = $('#category').val(),
                trimcondition = $('#trimcondition').val(),
                    managerId = $('#managers').val(),
                isOnlyUser = $('#isonlyuser').val();
        var obj = {
            "DistrictId": districtID,
            "Price": price,
            "CategoryId": category,
            "TrimconditionId": trimcondition,
            "UserId": managerId,
            "IsOnlyUser": isOnlyUser
        }

        lazyload(obj, url, false);
    });

    $('#search').click(function () {
        var districtID = $('#district').val(),
               price = $('#price').val(),
               category = $('#category').val(),
               trimcondition = $('#trimcondition').val(),
                    managerId = $('#managers').val(),
               isOnlyUser = $('#isonlyuser').val();
        var obj = {
            "DistrictId": districtID,
            "Price": price,
            "CategoryId": category,
            "TrimconditionId": trimcondition,
            "UserId": managerId,
            "IsOnlyUser": isOnlyUser
        }

        lazyload(obj, url, true);
    });
});