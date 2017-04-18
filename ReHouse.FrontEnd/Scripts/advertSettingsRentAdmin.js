$(document).ready(function () {
    var url = "/cabinet/rent/load";
    $('#button-more').click(function () {

        var districtID = $('#district').val(),
                price = $('#price').val(),
                category = $('#category').val(),
                trimcondition = $('#trimcondition').val(),
                isOnlyUser = $('#isonlyuser').val();
        var obj = {
            "DistrictId": districtID,
            "Price": price,
            "CategoryId": category,
            "TrimconditionId": trimcondition,
            "IsOnlyUser": isOnlyUser
        }

        lazyload(obj, url, false);
    });

    $('#search').click(function () {
        var districtID = $('#district').val(),
               price = $('#price').val(),
               category = $('#category').val(),
               trimcondition = $('#trimcondition').val(),
               isOnlyUser = $('#isonlyuser').is(':checked');
        
        var obj = {
            "DistrictId": districtID,
            "Price": price,
            "CategoryId": category,
            "TrimconditionId": trimcondition,
            "IsOnlyUser": isOnlyUser
    }

        lazyload(obj, url, true);
    });
});