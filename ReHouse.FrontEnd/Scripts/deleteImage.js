function deleteImage(id) {    
    var obj = {
        "imageId": id
    }


    var json = JSON.stringify(obj);
    //$(buttonId).hide();
    //$(loadingId).show();
    if (confirm("Вы действительно хотите удалить изображение?")) {
        $.ajax({
            url: "/cabinet/image/delete",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: json,
            success: function (response) {
                if (response.IsSuccess)
                    $("#image_" + id).hide();
            },
            error: function (response) {
                //    $(loadingId).hide();
                //    $("<span>Извините, при обработке запроса произошла ошибка. Пожалуйста обновите страницу</span>").appendTo($(container));
            }
        });
    }
};

function deletePlanImage(id) {
    var obj = {
        "imageId": id
    }


    var json = JSON.stringify(obj);
    //$(buttonId).hide();
    //$(loadingId).show();
    if (confirm("Вы действительно хотите удалить изображение?")) {
        $.ajax({
            url: "/cabinet/image/deleteplan",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: json,
            success: function (response) {
                if (response.IsSuccess)
                    $("#planimage_" + id).hide();
            },
            error: function (response) {
                //    $(loadingId).hide();
                //    $("<span>Извините, при обработке запроса произошла ошибка. Пожалуйста обновите страницу</span>").appendTo($(container));
            }
        });
    }
};


