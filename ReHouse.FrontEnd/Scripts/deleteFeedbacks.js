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
    if (confirm("Вы действительно хотите удалить отзывы?")) {
        
        var obj = {            
            "feedbacksId": ids
        }
        
        var json = JSON.stringify(obj);

        $.ajax({
            url: '/cabinet/feedbacks/delete',
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