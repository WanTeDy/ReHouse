var page = 1,
        buttonId = "#button-more",
        loadingId = "#loading-div",
        container = "#data-container";

function lazyload(obj, url, search) {

    if (search) {
        page = 1;
    } else {
        page = page + 1;
    }
    obj.PageNumber = page;

    var json = JSON.stringify(obj);
    $(buttonId).hide();
    $(loadingId).show();

    $.ajax({
        url: url,
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: json,
        success: function (response) {
            if (response.noElements) {
                $(buttonId).fadeOut();
                $(loadingId).hide();
                if (page == 1) {
                    $('#data-container').html("По вашему запросу не были найдены объявления");
                }
                return;
            } else if (search) {
                search = false;
                $(buttonId).show();
                $(loadingId).hide();
                $('#data-container').html(response);
                return;
            }
            appendContests(response);
        },
        error: function (response) {
            $(loadingId).hide();
            $("<span>Извините, при обработке запроса произошла ошибка. Пожалуйста обновите страницу</span>").appendTo($(container));
        }
    });
};

var appendContests = function (response) {
    var id = $(buttonId);

    $(buttonId).show();
    $(loadingId).hide();

    $(response).appendTo($(container));
    //page += 1;
};
