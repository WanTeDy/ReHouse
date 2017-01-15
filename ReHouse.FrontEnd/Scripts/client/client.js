$("document").ready(function () {
    var leftIn = $("#left");
    var rightIn = $("#right");

    moment.locale('ru');
    rome.use(moment);
    rome(left, {
        dateValidator: rome.val.beforeEq(right),
        autoClose: true,
        styles: {
            container: "rd-cont-left",
            date: "rd-date",
            dayBody: "rd-days-body",
            dayBodyElem: "rd-day-body",
            dayConcealed: "rd-day-concealed",
            dayDisabled: "rd-day-disabled",
            dayHead: "rd-days-head",
            dayHeadElem: "rd-day-head",
            dayRow: "rd-days-row",
            dayTable: "rd-days",
            month: "rd-month",
            next: "rd-next",
            positioned: "rd-container-attach",
            selectedDay: "rd-day-selected",
            selectedTime: "rd-time-selected",
            time: "rd-time",
            timeList: "rd-time-list",
            timeOption: "rd-time-option"
        },
        time: false

    }).on('data', function(value) {

        leftIn.attr("value", value);
        var arr = value.split("-");
        var year = arr[0];
        var month = arr[1] - 1;

        console.log(month);
        $(".rd-cont-left .lineDate").html("<div class=\"year\">" + year + "</div>" + "<div class=\"month\">" + moment.months(month) + "</div>").animate("height", 100);
        

        event.preventDefault();
        event.stopImmediatePropagation();
    });

    rome(right, {
        dateValidator: rome.val.afterEq(left),
        autoClose: true,
        styles: {
            container: "rd-cont-right",
            date: "rd-date",
            dayBody: "rd-days-body",
            dayBodyElem: "rd-day-body",
            dayConcealed: "rd-day-concealed",
            dayDisabled: "rd-day-disabled",
            dayHead: "rd-days-head",
            dayHeadElem: "rd-day-head",
            dayRow: "rd-days-row",
            dayTable: "rd-days",
            month: "rd-month",
            next: "rd-next",
            positioned: "rd-container-attach",
            selectedDay: "rd-day-selected",
            selectedTime: "rd-time-selected",
            time: "rd-time",
            timeList: "rd-time-list",
            timeOption: "rd-time-option"
        },
        time: false
    }).on('data', function (value) {

        rightIn.attr("value", value);

        var arr = value.split("-");
        var year = arr[0];
        var month = arr[1] - 1;
        console.log(month);
        $(".rd-cont-right .lineDate").html("<div class=\"year\">" + year + "</div>" + "<div class=\"month\">" + moment.months(month) + "</div>").animate("height", 100);

        event.preventDefault();
        event.stopImmediatePropagation();

    });
    function monthLabelLeftFunc() {


        var monthLabelLeft = $(".rd-cont-left .rd-month-label").css({ "display": "none" });


            var monthStrLeft = monthLabelLeft.text(),
            monthArray = monthStrLeft.split(" "),
            month = monthArray[0],
            year = monthArray[1];
            //console.log(month, year);

        return monthLabelLeft.before("<div class=\"lineDate\"><div class=\"year\"> " + year + " </div>" + "<div class=\"month\"> " + month + "</div></div>").removeClass("rd-month-label").addClass("rendered").animate("opacity", 400);


    };

 

    function monthLabelRightFunc() {

        var monthLabelRight = $(".rd-cont-right .rd-month-label").css({ "display": "none" });

      
            var monthStrRight = monthLabelRight.text(),
                monthArrayRight = monthStrRight.split(" "),
                monthRight = monthArrayRight[0],
                yearRight = monthArrayRight[1];

            return monthLabelRight.before("<div class=\"lineDate\"><div class=\"year\"> " + yearRight + " </div>" + "<div class=\"month\"> " + monthRight + "</div></div>").removeClass("rd-month-label").addClass("rendered").animate("opacity", 400);
            

    };
 
    leftIn.on("click", function() {
        monthLabelLeftFunc();
    });
    rightIn.on("click", function () {
        monthLabelRightFunc();
    });

});
