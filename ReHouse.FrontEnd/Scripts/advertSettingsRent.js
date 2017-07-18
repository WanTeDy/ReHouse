$(document).ready(function () {
    var url = "/rent/load/";
    
    function rent_lazyload(search) {
        var districtID = $('#district').val(),                
                rentPeriodType = $('#rent_type').val(),
                category = $('#category').val(),
                priceMin = $('#price').val(),
                priceMax = $('#price_max').val(),
                trimcondition = $('#trimcondition').val();

        if (rentPeriodType == 2) {
            priceMin = $('#price2').val();
            priceMax = $('#price_max2').val();
        }
        
        var obj = {
            "DistrictId": districtID,
            "PriceMin": priceMin,
            "PriceMax": priceMax,
            "RentPeriodType": rentPeriodType,
            "CategoryId": category,
            "TrimconditionId": trimcondition
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