/**
 * @author Shaumik Daityari
 * @copyright Copyright © 2013 All rights reserved.
 */

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
    page += 1;
};




//var lazyload = lazyload || {};

//(function ($, lazyload) {

//    "use strict";

//    var page = 1,
//        buttonId = "#button-more",
//        loadingId = "#loading-div",
//        container = "#data-container";

//    lazyload.load = function () {

//        var districtID = $('#district').val(),
//            price = $('#price').val(),
//            builderId = $('#builder').val(),
//            expluatationId = $('#date').val();        
//        if (search) {
//            page = 1;
//        } else {
//            page = page + 1;
//        }
//        var obj = {
//            "PageNumber": page,
//            "DistrictId": districtID,
//            "Price": price,
//            "BuilderId": builderId,
//            "ExpluatationDateId": expluatationId
//        }

//        var json = JSON.stringify(obj);
//        $(buttonId).hide();
//        $(loadingId).show();

//        $.ajax({
//            url: url,
//            type: "POST",
//            contentType: 'application/json; charset=utf-8',
//            data: json,
//            success: function (response) {
//                if (response.noElements) {
//                    $(buttonId).fadeOut();
//                    $(loadingId).hide();
//                    if (page == 1) {
//                        $('#data-container').html("По вашему запросу не были найдены объявления");
//                    }
//                    return;
//                } else if (search) {
//                    search = false;
//                    $(buttonId).show();
//                    $(loadingId).hide();
//                    $('#data-container').html(response);
//                    return;
//                }
//                appendContests(response);
//            },
//            error: function (response) {
//                $(loadingId).text("Извините, при обработке запроса произошла ошибка. Пожалуйста обновите страницу.");
//            }
//        });
//    };

//    var appendContests = function (response) {
//        var id = $(buttonId);

//        $(buttonId).show();
//        $(loadingId).hide();

//        $(response).appendTo($(container));
//        page += 1;
//    };


//})(jQuery, lazyload);