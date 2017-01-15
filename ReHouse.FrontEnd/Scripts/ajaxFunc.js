
    function LoginOnComplete(data, resul, xhr) {
        if (data.url) {
            // if the server returned a JSON object containing an url 
            // property we redirect the browser to that url
            formHide();
            window.location.href = data.url;
        }
    }
    function RemindOnComplete(data, resul, xhr) {
        if (data.url) {
             window.location.href = data.url;
        }
        //var url = xhr.getResponseHeader('Url');
        //if (url != "") {
        //    console.log(url);
        //    window.location.href = url;
        //}
    }
    function RegisterComplete(data, resul, xhr) {
        if (data.url) {
            // if the server returned a JSON object containing an url 
            // property we redirect the browser to that url
            formHide3();
            window.location.href = data.url;
        }
    }
    function triggerModal(message, type) {
        $(document).ready(function () {

            $("#inline-popups a").trigger("click");
            var textPopUp = $("#test-popup");
            if (type == "error") {
                textPopUp.html("<button title=\"Close (Esc)\" type=\"button\" class=\"mfp-close\">×</button>" + "<b style=\"color:red\">" + message + "</b>");
            } else {
                textPopUp.html("<button title=\"Close (Esc)\" type=\"button\" class=\"mfp-close\">×</button>" + "<b style=\"color:green\">" + message + "</b>");

            }
        });
    }

    var formHide = function() {
        $(document).ready(function () {


            Foundation.libs.dropdown.close($('#login-modal'));
        });
    };

    var formHide3 = function () {
        $(document).ready(function () {

            Foundation.libs.dropdown.close($('#register-modal'));
        });
    };

    function ChangeClientsData(data, result, xhr) {
        if (data.Status != "Success") {
            triggerModal(data.Status, 'error');
            $(document).ready(function() {
                $('#form0').find('.button.cancel').trigger('click');
            });
            
        } else {
            triggerModal("Ваши даные успешно изменены!");
            formHide2();
        }
    }

    var formHide2 = function() {
        $(document).ready(function() {

            if ($("#user-profile-modal").hasClass("open")) {
                $("#user-profile-modal").removeClass("open");
            }
        });
    };

    function RegisterCorporate(data, result, xhr) {
        $(document).ready(function() {
            $('select.styler').styler();
        });
        if (xhr != "") {
            var resComplite = xhr.getResponseHeader('AddLegalEntitySuccess');
            if (resComplite == "Success") {

                triggerModal("БЛАГОДАРИМ ЗА РЕГИСТРАЦИЮ!\n\nВ ближайшее время с Вами свяжется наш менеджер, для уточнения деталей и подтверждения регистрации!\n\nПриятных покупок,\nс уважением,\nкоманда ITfamily.", resComplite);
                
            } else {
                console.log(xhr.responseText);
                console.log(resComplite);
                triggerModal("Возникла ошибка! Пожалуйста введите еще раз данные или свяжитесь с нами!", resComplite = "error");

            }
            HandlingResponse(xhr);
        }
    }

 

    function OrderGoodsFunction(data, result, xhr) {
        if (xhr != "") {
            var resComplite = xhr.getResponseHeader('OrderGoods');
            if (resComplite == "Success") {
              
                triggerModal("БЛАГОДАРИМ ЗА ПОКУПКУ!\n\nВ ближайшее время с Вами свяжется наш менеджер, для уточнения деталей и подтверждения покупки!\n\nПриятных покупок,\nс уважением,\nкоманда ITfamily.", resComplite);
               
            } else {

                console.log("erorr");
             
                triggerModal("Возникла ошибка! Пожалуйста введите еще раз данные!", resComplite = "error");
                
                $(document).ready(function() {
                    $('#orderGoods select').styler();
                    $(".button-add-comment").click(function() {

                        var comment = $("#Comment");
                        if (comment.hasClass("active")) {
                            comment.removeClass("active");
                            $(this).text("").html("<span class=\"fa fa-plus\"></span>" + "Добавить комментарий к заказу").css({ "font-weight": "normal" });
                        } else {
                            comment.addClass("active");
                            $(this).text("").html("<span class=\"fa fa-minus\"></span>" + "Комментарий к заказу").css({ "font-weight": "bolder" });
                        }
                    });

                    $(".button-add-shipment").click(function() {

                            var block = $(".additional-shipment");
                            if (block.hasClass("active")) {
                                block.removeClass("active");
                                $(this).text("").html("<span class=\"fa fa-plus-circle\"></span>" + "Доставка").css({ "font-weight": "bold" });
                                $("#basket-adress").removeAttr("required");
                            } else {
                                block.addClass("active");
                                $(this).text("").html("<span class=\"fa fa-minus-circle\"></span>" + "Доставка").css({ "font-weight": "bolder" });
                                $("#basket-adress").attr("required", "required");
                            }


                        }
                    );
                });
            }

            HandlingResponse(xhr);
        }
    }


    function FeedbackResponse(data, result, xhr) {
        if (xhr != "") {
            var resComplite = xhr.getResponseHeader('FeedbackResponse');
            if (resComplite == "Success") {
                triggerModal("БЛАГОДАРИМ ЗА ОТПРАВЛЕНОЕ СООБЩЕНИЕ!\n\nВ ближайшее время наши менеджеры получат уведомление!\n\nПриятных покупок,\nс уважением,\nкоманда ITfamily.", resComplite);
                $(document).ready(function () {

                    var feedbackBlock = $("#feedback-modal");
                    var userProfileExisits = $("#user-profile-modal").length;
                    if (userProfileExisits !== 0) {
                        feedbackBlock.find("textarea").val(null);

                    } else {
                        feedbackBlock.find("input").val(null);
                        feedbackBlock.find("textarea").val(null);

                    }
                     Foundation.libs.dropdown.close(feedbackBlock);
                });
            } else {
                Foundation.libs.dropdown.close(feedbackBlock);

                triggerModal("Возникла ошибка! Пожалуйста введите еще раз данные!", resComplite = "error");

            }
            HandlingResponse(xhr);
        }
    }

    function HandlingResponse(xhr) {
        var error = xhr.getResponseHeader('Error');
        if (error != "" && error != null) {
            triggerModal("Воникла ошибка" + "<br/>" + error, "error");
        }
        var url = xhr.getResponseHeader('Url');
        if (url != "" && url != null) {
            window.location.href = url;
        }
    }