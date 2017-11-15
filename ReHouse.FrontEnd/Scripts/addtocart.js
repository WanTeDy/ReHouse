function addtocart(id, type, isAdd, element, e) {
    e.preventDefault();
    e.stopPropagation();
    isAdd = $(element).hasClass("like");
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
            if (isAdd) {
                $(element).removeClass("like");
                $(element).addClass("liked");
            } else {
                $(element).removeClass("liked");
                $(element).addClass("like");
            }
        },
        error: function (response) {
            //    $(loadingId).hide();
            //    $("<span>Извините, при обработке запроса произошла ошибка. Пожалуйста обновите страницу</span>").appendTo($(container));
        }
    });
};

function share(element, e) {
    e.preventDefault();
    e.stopPropagation();
    var parent = $(element).parent();
    if (parent.children('.shared_icons').css('display') === 'none') {
        parent.children('div').children('.cost').css('display', 'none');
        parent.children('.shared_icons').css('display', 'flex');
    } else {
        parent.children('div').children('.cost').css('display', 'block');
        parent.children('.shared_icons').css('display', 'none');
    }
    return false;
};