$("document").ready(function() {
    //var leftE = $("#resultLeft");
    //var rightE =$("#resultRight");
    $(".rd-container").css({
        display: "block",
        position: "relative",
        top: "0",
        left: "0"

    });
    var leftIn = $("#left");
    var rightIn = $("#right");

    moment.locale('ru');
    rome.use(moment);
   rome(left, {
        dateValidator: rome.val.beforeEq(right),
        time: false,
        appendTo: 'parent',
        autoClose: false,
        autoHideOnBlur: false,
        autoHideOnClick: false
    }).on('data', function (value) {

        leftIn.attr("value", value);

        rightIn.trigger("click");

     

        var arr = value.split("-");
        var year = arr[0];
        var month = arr[1] - 1;

        console.log(month);

        $("#leftInline .lineDate").html("<div class=\"year\">" + year + "</div>" + "<div class=\"month\">" + moment.months(month) + "</div>");

        event.preventDefault();
        event.stopImmediatePropagation();
    })
       .show();

  
 
  
   
    rome(right, {
        dateValidator: rome.val.afterEq(left),
        appendTo: 'parent',
        time: false,
        autoClose: false,
        autoHideOnBlur: false,
        autoHideOnClick: false
    }).on('data', function(value) {
        rightIn.attr("value", value);

        leftIn.trigger("click");
        var arr = value.split("-");
        var year = arr[0];
        var month = arr[1] - 1;
        console.log(month);
        $("#rightInline .lineDate").html("<div class=\"year\">" + year + "</div>" + "<div class=\"month\">" + moment.months(month) + "</div>");
        event.preventDefault();
        event.stopImmediatePropagation();

    }).show();

    rightIn.on({
        mouseenter: function (event) {
            event.preventDefault();
            event.stopImmediatePropagation();
            $(this).focus();
        },
        mouseleave: function () {
        
                $(this).blur();
        }
    });
    leftIn.on({
        mouseenter: function (event) {
            event.preventDefault();
            event.stopImmediatePropagation();
            $(this).focus();
        },
        mouseleave: function() {


            $(this).blur();
        }
    });
    leftIn.trigger("click", function () {

        $(".rd-container").css({
            display: "block",
            position: "relative",
            top: "0",
            left: "0"

        });
    });
    rightIn.trigger("click", function () {

       
        $(".rd-container").css({
            display: "block",
            position: "relative",
            top: "0",
            left: "0"

        });

    });
    rightIn.focus(function () {
        $(this).blur();
    });
    leftIn.focus(function () {
        $(this).blur();
    });
        
    var lNext = leftIn.next().appendTo("#leftInline");

   
    var rNext = rightIn.next().appendTo("#rightInline");

    //$("#right").on('change', function () {

    //    var Val = $(this).val();
    //    var arr = Val.split("-");

    //    var year = arr[0];
    //    var month = arr[1];
        
    //    rNext.children(".rd-month-label").html("<div class=\"year\">" + year + "</div>" + "<div class=\"month\">" + moment.months(month) + "</div>");

    //});
    //$("#left").on('change', function () {

    //    var Val = leftIn.val();

    //    var arr = Val.split("-");

    //    console.log(Val);
    //    var year = arr[0];
    //    var month = arr[1];
    //    lNext.children(".rd-month-label").html("<div class=\"year\">" + year + "</div>" + "<div class=\"month\">" + moment.months(month) + "</div>");

    //});
    function monthLabelLeftFunc() {


        var monthLabelLeft = $("#leftInline .rd-month-label").css({ "display": "none" });

        return monthLabelLeft.on("change", function () {

            var monthStrLeft = monthLabelLeft.text(),
            monthArray = monthStrLeft.split(" "),
            month = monthArray[0],
            year = monthArray[1];
            //console.log(month, year);

            return monthLabelLeft.before("<div class=\"lineDate\"><div class=\"year\"> " + year + " </div>" + "<div class=\"month\"> " + month + "</div></div>").removeClass("rd-month-label").addClass("rendered");
        }());


    };

    monthLabelLeftFunc();

    function monthLabelRightFunc() {
        
        var monthLabelRight = $("#rightInline .rd-month-label").css({"display": "none"});

        return monthLabelRight.on("change", function() {
                if (monthLabelRight.hasClass("rendered")) {

                   
                }
                var monthStrRight = monthLabelRight.text(),
                    monthArrayRight = monthStrRight.split(" "),
                    monthRight = monthArrayRight[0],
                    yearRight = monthArrayRight[1];
        
                return monthLabelRight.before("<div class=\"lineDate\"><div class=\"year\"> " + yearRight + " </div>" + "<div class=\"month\"> " + monthRight + "</div></div>").removeClass("rd-month-label").addClass("rendered");

        }());

       
    };

    monthLabelRightFunc();
});
