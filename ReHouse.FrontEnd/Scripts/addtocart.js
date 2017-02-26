function addtocart(id, type, isAdd) {

    var obj = {
        "AdvertId": id,
        "Type": type,
        "IsAdd": isAdd
    }

    var json = JSON.stringify(obj);
    //$(buttonId).hide();
    //$(loadingId).show();

    $.ajax({
        url: "/cart/add",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: json,
        success: function (response) {
            //if (response.noElements) {
            //    $(buttonId).fadeOut();
            //    $(loadingId).hide();
            //    return;
            //} else if (search) {
            //    search = false;
            //    $(buttonId).show();
            //    $(loadingId).hide();
            //    $('#data-container').html(response);
            //    return;
            //}
            //appendContests(response);
        },
        error: function (response) {
        //    $(loadingId).hide();
        //    $("<span>Извините, при обработке запроса произошла ошибка. Пожалуйста обновите страницу</span>").appendTo($(container));
        }
    });
};