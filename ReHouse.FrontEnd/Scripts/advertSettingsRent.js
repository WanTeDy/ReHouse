$(document).ready(function () {
    var url = "/rent/load/";
    $('#button-more').click(function () {

        var districtID = $('#district').val(),
                price = $('#price').val(),
                category = $('#category').val(),
                trimcondition = $('#trimcondition').val();
        var obj = {
            "DistrictId": districtID,
            "Price": price,
            "CategoryId": category,
            "TrimconditionId": trimcondition
        }

        lazyload(obj, url, false);
    });

    $('#search').click(function () {
        var districtID = $('#district').val(),
               price = $('#price').val(),
               category = $('#category').val(),
               trimcondition = $('#trimcondition').val();
        var obj = {
            "DistrictId": districtID,
            "Price": price,
            "CategoryId": category,
            "TrimconditionId": trimcondition
        }

        lazyload(obj, url, true);
    });
});