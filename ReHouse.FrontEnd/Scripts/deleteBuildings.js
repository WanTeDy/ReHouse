function deleteChekedProducts() {
    var elements = $('.checkbox:checked');
    var count = elements.length;
    if (count > 0) {
        var values = elements.map(function () {
            return parseInt($(this).val());
        }).get();
        deleteProducts(values);
    }
}

function deleteProducts(ids) {
    if (confirm("Вы действительно хотите удалить объявления?")) {
        var districtID = $('#district').val(),
                    price = $('#price').val(),
                    builderId = $('#builder').val(),
                    expluatationId = $('#date').val(),
                    isOnlyUser = $('#isonlyuser').val();
        var obj = {
            "DistrictId": districtID,
            "Price": price,
            "BuilderId": builderId,
            "ExpluatationDateId": expluatationId,
            "IsOnlyUser": isOnlyUser,        
            "AdvertsId": ids
        }
        
        var json = JSON.stringify(obj);

        $.ajax({
            url: '/cabinet/newbuilding/delete',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: json,
            success: function (response) {
                $("table.added").html(response);
            },
            error: function (response) {
                //    $(loadingId).hide();
                //    $("<span>Извините, при обработке запроса произошла ошибка. Пожалуйста обновите страницу</span>").appendTo($(container));
            }
        });
    }
}