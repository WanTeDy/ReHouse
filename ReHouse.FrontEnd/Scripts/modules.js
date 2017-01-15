/*jslint unparam: true, browser: true, indent: 2 */

; (function ($, window, document, undefined) {
    'use strict';

    Foundation.libs.dropdown = {
        name: 'dropdown',

        version: '4.3.2',

        settings: {
            activeClass: 'open',
            is_hover: false,
            opened: function () { },
            closed: function () { }
        },

        init: function (scope, method, options) {
            this.scope = scope || this.scope;
            Foundation.inherit(this, 'throttle scrollLeft data_options');

            if (typeof method === 'object') {
                $.extend(true, this.settings, method);
            }

            if (typeof method !== 'string') {

                if (!this.settings.init) {
                    this.events();
                }

                return this.settings.init;
            } else {
                return this[method].call(this, options);
            }
        },

        events: function () {
            var self = this;

            $(this.scope)
              .on('click.fndtn.dropdown', '[data-dropdown]', function (e) {
                  var settings = $.extend({}, self.settings, self.data_options($(this)));
                  e.preventDefault();

                  if (!settings.is_hover) self.toggle($(this));
              })
              .on('mouseenter', '[data-dropdown]', function (e) {
                  var settings = $.extend({}, self.settings, self.data_options($(this)));
                  if (settings.is_hover) self.toggle($(this));
              })
              .on('mouseleave', '[data-dropdown-content]', function (e) {
                  var target = $('[data-dropdown="' + $(this).attr('id') + '"]'),
                      settings = $.extend({}, self.settings, self.data_options(target));
                  if (settings.is_hover) self.close.call(self, $(this));
              })
              .on('opened.fndtn.dropdown', '[data-dropdown-content]', this.settings.opened)
              .on('closed.fndtn.dropdown', '[data-dropdown-content]', this.settings.closed);

            $(document).on('click.fndtn.dropdown', function (e) {
                var parent = $(e.target).closest('[data-dropdown-content]');

                if ($(e.target).data('dropdown') || $(e.target).parent().data('dropdown')) {
                    return;
                }
                if (!($(e.target).data('revealId')) &&
                  (parent.length > 0 && ($(e.target).is('[data-dropdown-content]') ||
                    $.contains(parent.first()[0], e.target)))) {
                    e.stopPropagation();
                    return;
                }

                self.close.call(self, $('[data-dropdown-content]'));
            });

            $(window).on('resize.fndtn.dropdown', self.throttle(function () {
                self.resize.call(self);
            }, 50)).trigger('resize');

            this.settings.init = true;
        },

        close: function (dropdown) {
            var self = this;
            dropdown.each(function () {
                if ($(this).hasClass(self.settings.activeClass)) {
                    $(this)
                      .css(Foundation.rtl ? 'right' : 'left', '-99999px')
                      .removeClass(self.settings.activeClass);
                    $(this).trigger('closed');
                }
            });
        },

        open: function (dropdown, target) {
            this
              .css(dropdown
                .addClass(this.settings.activeClass), target);
            dropdown.trigger('opened');
        },

        toggle: function (target) {
            var dropdown = $('#' + target.data('dropdown'));
            if (dropdown.length === 0) {
                // No dropdown found, not continuing
                return;
            }

            this.close.call(this, $('[data-dropdown-content]').not(dropdown));

            if (dropdown.hasClass(this.settings.activeClass)) {
                this.close.call(this, dropdown);
            } else {
                this.close.call(this, $('[data-dropdown-content]'))
                this.open.call(this, dropdown, target);
            }
        },

        resize: function () {
            var dropdown = $('[data-dropdown-content].open'),
                target = $("[data-dropdown='" + dropdown.attr('id') + "']");

            if (dropdown.length && target.length) {
                this.css(dropdown, target);
            }
        },

        css: function (dropdown, target) {
            var offset_parent = dropdown.offsetParent();
            // if (offset_parent.length > 0 && /body/i.test(dropdown.offsetParent()[0].nodeName)) {
            var position = target.offset();
            position.top -= offset_parent.offset().top;
            position.left -= offset_parent.offset().left;
            // } else {
            //   var position = target.position();
            // }

            if (this.small()) {
                dropdown.css({
                    position: 'absolute',
                    width: '95%',
                    'max-width': 'none',
                    top: position.top + this.outerHeight(target)
                });
                dropdown.css(Foundation.rtl ? 'right' : 'left', '2.5%');
            } else {
                if (!Foundation.rtl && $(window).width() > this.outerWidth(dropdown) + target.offset().left && !this.data_options(target).align_right) {
                    var left = position.left;
                    if (dropdown.hasClass('right')) {
                        dropdown.removeClass('right');
                    }
                } else {
                    if (!dropdown.hasClass('right')) {
                        dropdown.addClass('right');
                    }
                    var left = position.left - (this.outerWidth(dropdown) - this.outerWidth(target));
                }

                dropdown.attr('style', '').css({
                    position: 'absolute',
                    top: position.top + this.outerHeight(target),
                    left: left
                });
            }

            return dropdown;
        },

        small: function () {
            return $(window).width() < 768 || $('html').hasClass('lt-ie9');
        },

        off: function () {
            $(this.scope).off('.fndtn.dropdown');
            $('html, body').off('.fndtn.dropdown');
            $(window).off('.fndtn.dropdown');
            $('[data-dropdown-content]').off('.fndtn.dropdown');
            this.settings.init = false;
        },

        reflow: function () { }
    };
}(Foundation.zj, this, this.document));


$(document).ready(function() {


    $(".form-drop").on('opened.fndtn.dropdown', function() {

        var that = $(this);             
        that.find(".button-close").click(function (e) {

            Foundation.libs.dropdown.close($("#"+ that.attr("id")));

                //$(".f").has("data-dropdown", ).trigger("click");
            }
        );        

    });

});
var inintHeaderDrops = function() {
    $(".button-add-comment").click(function () {

        var comment = $("#Comment");
        if (comment.hasClass("active")) {
            comment.removeClass("active");
            $(this).text("").html("<span class=\"fa fa-plus\"></span>" + "Добавить комментарий к заказу").css({ "font-weight": "normal" });
        }
        else {
            comment.addClass("active");
            $(this).text("").html("<span class=\"fa fa-minus\"></span>" + "Комментарий к заказу").css({ "font-weight": "bolder" });
        }



    });

    $(".button-add-shipment").click(function () {

        var block = $(".additional-shipment");
        if (block.hasClass("active")) {
            block.removeClass("active");
            $(this).text("").html("<span class=\"fa fa-plus-circle\"></span>" + "Доставка").css({ "font-weight": "bold" });
            $("#basket-adress").removeAttr("required");
        }
        else {
            block.addClass("active");
            $(this).text("").html("<span class=\"fa fa-minus-circle\"></span>" + "Доставка").css({ "font-weight": "bolder" });
            $("#basket-adress").attr("required", "required");
        }



    });
}
inintHeaderDrops();
(function ($) {
    $(function () {

 
        $("#main-sidebar-button").click(function (ev) {
            ev.preventDefault();
            var block = $("#main-sidebar");
            var subblock = $("#category-sidebar");
            if (block.hasClass("active")) {
                block.removeClass("active");
                subblock.css({ "opacity": 1 });

                $(this).text("").html(" <span class=\"catalog-title sub-catalog\">КАТАЛОГ ТОВАРОВ  <i ></i></span>").css({ "font-weight": "bold" });
                $(this).removeClass("active");
            }
            else {
                block.addClass("active");
                subblock.css({"opacity" : 0});

                
                $(this).text("").html(" <span class=\"catalog-title active sub-catalog\">КАТАЛОГ ТОВАРОВ  <i></i></span>").css({ "font-weight": "bolder" });
                $(this).addClass("active");
            }



        });

        $(".button-add-contact").click(function () {

            var block = $(".row-contact");
            if (block.hasClass("active")) {
                block.removeClass("active");
                $(this).text("").html("<span class=\"fa fa-plus\"></span>" + "Добавить контактное лицо").css({ "font-weight": "bold" });
            }
            else {
                block.addClass("active");
                $(this).text("").html("<span class=\"fa fa-minus\"></span>" + "Добавить контактное лицо").css({ "font-weight": "bolder" });
            }



        });

        $(".button-add-filial").click(function () {

            var block = $(".row-add-filial");
            if (block.hasClass("active")) {
                block.removeClass("active");
                $(this).text("").html("<span class=\"fa fa-plus-circle\"></span>" + "Добавить филиал компании").css({ "font-weight": "bold" });
            }
            else {
                block.addClass("active");
                $(this).text("").html("<span class=\"fa fa-minus-circle\"></span>" + "Добавить филиал компании").css({ "font-weight": "bolder" });
            }



        });

       

    });
})(jQuery);
/*!
 * Modernizr v2.8.3
 * www.modernizr.com
 *
 * Copyright (c) Faruk Ates, Paul Irish, Alex Sexton
 * Available under the BSD and MIT licenses: www.modernizr.com/license/
 */
window.Modernizr=function(a,b,c){function d(a){t.cssText=a}function e(a,b){return d(x.join(a+";")+(b||""))}function f(a,b){return typeof a===b}function g(a,b){return!!~(""+a).indexOf(b)}function h(a,b){for(var d in a){var e=a[d];if(!g(e,"-")&&t[e]!==c)return"pfx"==b?e:!0}return!1}function i(a,b,d){for(var e in a){var g=b[a[e]];if(g!==c)return d===!1?a[e]:f(g,"function")?g.bind(d||b):g}return!1}function j(a,b,c){var d=a.charAt(0).toUpperCase()+a.slice(1),e=(a+" "+z.join(d+" ")+d).split(" ");return f(b,"string")||f(b,"undefined")?h(e,b):(e=(a+" "+A.join(d+" ")+d).split(" "),i(e,b,c))}function k(){o.input=function(c){for(var d=0,e=c.length;e>d;d++)E[c[d]]=!!(c[d]in u);return E.list&&(E.list=!(!b.createElement("datalist")||!a.HTMLDataListElement)),E}("autocomplete autofocus list placeholder max min multiple pattern required step".split(" ")),o.inputtypes=function(a){for(var d,e,f,g=0,h=a.length;h>g;g++)u.setAttribute("type",e=a[g]),d="text"!==u.type,d&&(u.value=v,u.style.cssText="position:absolute;visibility:hidden;",/^range$/.test(e)&&u.style.WebkitAppearance!==c?(q.appendChild(u),f=b.defaultView,d=f.getComputedStyle&&"textfield"!==f.getComputedStyle(u,null).WebkitAppearance&&0!==u.offsetHeight,q.removeChild(u)):/^(search|tel)$/.test(e)||(d=/^(url|email)$/.test(e)?u.checkValidity&&u.checkValidity()===!1:u.value!=v)),D[a[g]]=!!d;return D}("search tel url email datetime date month week time datetime-local number range color".split(" "))}var l,m,n="2.8.3",o={},p=!0,q=b.documentElement,r="modernizr",s=b.createElement(r),t=s.style,u=b.createElement("input"),v=":)",w={}.toString,x=" -webkit- -moz- -o- -ms- ".split(" "),y="Webkit Moz O ms",z=y.split(" "),A=y.toLowerCase().split(" "),B={svg:"http://www.w3.org/2000/svg"},C={},D={},E={},F=[],G=F.slice,H=function(a,c,d,e){var f,g,h,i,j=b.createElement("div"),k=b.body,l=k||b.createElement("body");if(parseInt(d,10))for(;d--;)h=b.createElement("div"),h.id=e?e[d]:r+(d+1),j.appendChild(h);return f=["&#173;",'<style id="s',r,'">',a,"</style>"].join(""),j.id=r,(k?j:l).innerHTML+=f,l.appendChild(j),k||(l.style.background="",l.style.overflow="hidden",i=q.style.overflow,q.style.overflow="hidden",q.appendChild(l)),g=c(j,a),k?j.parentNode.removeChild(j):(l.parentNode.removeChild(l),q.style.overflow=i),!!g},I=function(b){var c=a.matchMedia||a.msMatchMedia;if(c)return c(b)&&c(b).matches||!1;var d;return H("@media "+b+" { #"+r+" { position: absolute; } }",function(b){d="absolute"==(a.getComputedStyle?getComputedStyle(b,null):b.currentStyle).position}),d},J=function(){function a(a,e){e=e||b.createElement(d[a]||"div"),a="on"+a;var g=a in e;return g||(e.setAttribute||(e=b.createElement("div")),e.setAttribute&&e.removeAttribute&&(e.setAttribute(a,""),g=f(e[a],"function"),f(e[a],"undefined")||(e[a]=c),e.removeAttribute(a))),e=null,g}var d={select:"input",change:"input",submit:"form",reset:"form",error:"img",load:"img",abort:"img"};return a}(),K={}.hasOwnProperty;m=f(K,"undefined")||f(K.call,"undefined")?function(a,b){return b in a&&f(a.constructor.prototype[b],"undefined")}:function(a,b){return K.call(a,b)},Function.prototype.bind||(Function.prototype.bind=function(a){var b=this;if("function"!=typeof b)throw new TypeError;var c=G.call(arguments,1),d=function(){if(this instanceof d){var e=function(){};e.prototype=b.prototype;var f=new e,g=b.apply(f,c.concat(G.call(arguments)));return Object(g)===g?g:f}return b.apply(a,c.concat(G.call(arguments)))};return d}),C.flexbox=function(){return j("flexWrap")},C.flexboxlegacy=function(){return j("boxDirection")},C.canvas=function(){var a=b.createElement("canvas");return!(!a.getContext||!a.getContext("2d"))},C.canvastext=function(){return!(!o.canvas||!f(b.createElement("canvas").getContext("2d").fillText,"function"))},C.webgl=function(){return!!a.WebGLRenderingContext},C.touch=function(){var c;return"ontouchstart"in a||a.DocumentTouch&&b instanceof DocumentTouch?c=!0:H(["@media (",x.join("touch-enabled),("),r,")","{#modernizr{top:9px;position:absolute}}"].join(""),function(a){c=9===a.offsetTop}),c},C.geolocation=function(){return"geolocation"in navigator},C.postmessage=function(){return!!a.postMessage},C.websqldatabase=function(){return!!a.openDatabase},C.indexedDB=function(){return!!j("indexedDB",a)},C.hashchange=function(){return J("hashchange",a)&&(b.documentMode===c||b.documentMode>7)},C.history=function(){return!(!a.history||!history.pushState)},C.draganddrop=function(){var a=b.createElement("div");return"draggable"in a||"ondragstart"in a&&"ondrop"in a},C.websockets=function(){return"WebSocket"in a||"MozWebSocket"in a},C.rgba=function(){return d("background-color:rgba(150,255,150,.5)"),g(t.backgroundColor,"rgba")},C.hsla=function(){return d("background-color:hsla(120,40%,100%,.5)"),g(t.backgroundColor,"rgba")||g(t.backgroundColor,"hsla")},C.multiplebgs=function(){return d("background:url(https://),url(https://),red url(https://)"),/(url\s*\(.*?){3}/.test(t.background)},C.backgroundsize=function(){return j("backgroundSize")},C.borderimage=function(){return j("borderImage")},C.borderradius=function(){return j("borderRadius")},C.boxshadow=function(){return j("boxShadow")},C.textshadow=function(){return""===b.createElement("div").style.textShadow},C.opacity=function(){return e("opacity:.55"),/^0.55$/.test(t.opacity)},C.cssanimations=function(){return j("animationName")},C.csscolumns=function(){return j("columnCount")},C.cssgradients=function(){var a="background-image:",b="gradient(linear,left top,right bottom,from(#9f9),to(white));",c="linear-gradient(left top,#9f9, white);";return d((a+"-webkit- ".split(" ").join(b+a)+x.join(c+a)).slice(0,-a.length)),g(t.backgroundImage,"gradient")},C.cssreflections=function(){return j("boxReflect")},C.csstransforms=function(){return!!j("transform")},C.csstransforms3d=function(){var a=!!j("perspective");return a&&"webkitPerspective"in q.style&&H("@media (transform-3d),(-webkit-transform-3d){#modernizr{left:9px;position:absolute;height:3px;}}",function(b,c){a=9===b.offsetLeft&&3===b.offsetHeight}),a},C.csstransitions=function(){return j("transition")},C.fontface=function(){var a;return H('@font-face {font-family:"font";src:url("https://")}',function(c,d){var e=b.getElementById("smodernizr"),f=e.sheet||e.styleSheet,g=f?f.cssRules&&f.cssRules[0]?f.cssRules[0].cssText:f.cssText||"":"";a=/src/i.test(g)&&0===g.indexOf(d.split(" ")[0])}),a},C.generatedcontent=function(){var a;return H(["#",r,"{font:0/0 a}#",r,':after{content:"',v,'";visibility:hidden;font:3px/1 a}'].join(""),function(b){a=b.offsetHeight>=3}),a},C.video=function(){var a=b.createElement("video"),c=!1;try{(c=!!a.canPlayType)&&(c=new Boolean(c),c.ogg=a.canPlayType('video/ogg; codecs="theora"').replace(/^no$/,""),c.h264=a.canPlayType('video/mp4; codecs="avc1.42E01E"').replace(/^no$/,""),c.webm=a.canPlayType('video/webm; codecs="vp8, vorbis"').replace(/^no$/,""))}catch(d){}return c},C.audio=function(){var a=b.createElement("audio"),c=!1;try{(c=!!a.canPlayType)&&(c=new Boolean(c),c.ogg=a.canPlayType('audio/ogg; codecs="vorbis"').replace(/^no$/,""),c.mp3=a.canPlayType("audio/mpeg;").replace(/^no$/,""),c.wav=a.canPlayType('audio/wav; codecs="1"').replace(/^no$/,""),c.m4a=(a.canPlayType("audio/x-m4a;")||a.canPlayType("audio/aac;")).replace(/^no$/,""))}catch(d){}return c},C.localstorage=function(){try{return localStorage.setItem(r,r),localStorage.removeItem(r),!0}catch(a){return!1}},C.sessionstorage=function(){try{return sessionStorage.setItem(r,r),sessionStorage.removeItem(r),!0}catch(a){return!1}},C.webworkers=function(){return!!a.Worker},C.applicationcache=function(){return!!a.applicationCache},C.svg=function(){return!!b.createElementNS&&!!b.createElementNS(B.svg,"svg").createSVGRect},C.inlinesvg=function(){var a=b.createElement("div");return a.innerHTML="<svg/>",(a.firstChild&&a.firstChild.namespaceURI)==B.svg},C.smil=function(){return!!b.createElementNS&&/SVGAnimate/.test(w.call(b.createElementNS(B.svg,"animate")))},C.svgclippaths=function(){return!!b.createElementNS&&/SVGClipPath/.test(w.call(b.createElementNS(B.svg,"clipPath")))};for(var L in C)m(C,L)&&(l=L.toLowerCase(),o[l]=C[L](),F.push((o[l]?"":"no-")+l));return o.input||k(),o.addTest=function(a,b){if("object"==typeof a)for(var d in a)m(a,d)&&o.addTest(d,a[d]);else{if(a=a.toLowerCase(),o[a]!==c)return o;b="function"==typeof b?b():b,"undefined"!=typeof p&&p&&(q.className+=" "+(b?"":"no-")+a),o[a]=b}return o},d(""),s=u=null,function(a,b){function c(a,b){var c=a.createElement("p"),d=a.getElementsByTagName("head")[0]||a.documentElement;return c.innerHTML="x<style>"+b+"</style>",d.insertBefore(c.lastChild,d.firstChild)}function d(){var a=s.elements;return"string"==typeof a?a.split(" "):a}function e(a){var b=r[a[p]];return b||(b={},q++,a[p]=q,r[q]=b),b}function f(a,c,d){if(c||(c=b),k)return c.createElement(a);d||(d=e(c));var f;return f=d.cache[a]?d.cache[a].cloneNode():o.test(a)?(d.cache[a]=d.createElem(a)).cloneNode():d.createElem(a),!f.canHaveChildren||n.test(a)||f.tagUrn?f:d.frag.appendChild(f)}function g(a,c){if(a||(a=b),k)return a.createDocumentFragment();c=c||e(a);for(var f=c.frag.cloneNode(),g=0,h=d(),i=h.length;i>g;g++)f.createElement(h[g]);return f}function h(a,b){b.cache||(b.cache={},b.createElem=a.createElement,b.createFrag=a.createDocumentFragment,b.frag=b.createFrag()),a.createElement=function(c){return s.shivMethods?f(c,a,b):b.createElem(c)},a.createDocumentFragment=Function("h,f","return function(){var n=f.cloneNode(),c=n.createElement;h.shivMethods&&("+d().join().replace(/[\w\-]+/g,function(a){return b.createElem(a),b.frag.createElement(a),'c("'+a+'")'})+");return n}")(s,b.frag)}function i(a){a||(a=b);var d=e(a);return!s.shivCSS||j||d.hasCSS||(d.hasCSS=!!c(a,"article,aside,dialog,figcaption,figure,footer,header,hgroup,main,nav,section{display:block}mark{background:#FF0;color:#000}template{display:none}")),k||h(a,d),a}var j,k,l="3.7.0",m=a.html5||{},n=/^<|^(?:button|map|select|textarea|object|iframe|option|optgroup)$/i,o=/^(?:a|b|code|div|fieldset|h1|h2|h3|h4|h5|h6|i|label|li|ol|p|q|span|strong|style|table|tbody|td|th|tr|ul)$/i,p="_html5shiv",q=0,r={};!function(){try{var a=b.createElement("a");a.innerHTML="<xyz></xyz>",j="hidden"in a,k=1==a.childNodes.length||function(){b.createElement("a");var a=b.createDocumentFragment();return"undefined"==typeof a.cloneNode||"undefined"==typeof a.createDocumentFragment||"undefined"==typeof a.createElement}()}catch(c){j=!0,k=!0}}();var s={elements:m.elements||"abbr article aside audio bdi canvas data datalist details dialog figcaption figure footer header hgroup main mark meter nav output progress section summary template time video",version:l,shivCSS:m.shivCSS!==!1,supportsUnknownElements:k,shivMethods:m.shivMethods!==!1,type:"default",shivDocument:i,createElement:f,createDocumentFragment:g};a.html5=s,i(b)}(this,b),o._version=n,o._prefixes=x,o._domPrefixes=A,o._cssomPrefixes=z,o.mq=I,o.hasEvent=J,o.testProp=function(a){return h([a])},o.testAllProps=j,o.testStyles=H,o.prefixed=function(a,b,c){return b?j(a,b,c):j(a,"pfx")},q.className=q.className.replace(/(^|\s)no-js(\s|$)/,"$1$2")+(p?" js "+F.join(" "):""),o}(this,this.document);

/*! http://mths.be/placeholder v2.0.9 by @mathias */
!function(a){"function"==typeof define&&define.amd?define(["jquery"],a):a(jQuery)}(function(a){function b(b){var c={},d=/^jQuery\d+$/;return a.each(b.attributes,function(a,b){b.specified&&!d.test(b.name)&&(c[b.name]=b.value)}),c}function c(b,c){var d=this,f=a(d);if(d.value==f.attr("placeholder")&&f.hasClass("placeholder"))if(f.data("placeholder-password")){if(f=f.hide().nextAll('input[type="password"]:first').show().attr("id",f.removeAttr("id").data("placeholder-id")),b===!0)return f[0].value=c;f.focus()}else d.value="",f.removeClass("placeholder"),d==e()&&d.select()}function d(){var d,e=this,f=a(e),g=this.id;if(""===e.value){if("password"===e.type){if(!f.data("placeholder-textinput")){try{d=f.clone().attr({type:"text"})}catch(h){d=a("<input>").attr(a.extend(b(this),{type:"text"}))}d.removeAttr("name").data({"placeholder-password":f,"placeholder-id":g}).bind("focus.placeholder",c),f.data({"placeholder-textinput":d,"placeholder-id":g}).before(d)}f=f.removeAttr("id").hide().prevAll('input[type="text"]:first').attr("id",g).show()}f.addClass("placeholder"),f[0].value=f.attr("placeholder")}else f.removeClass("placeholder")}function e(){try{return document.activeElement}catch(a){}}var f,g,h="[object OperaMini]"==Object.prototype.toString.call(window.operamini),i="placeholder"in document.createElement("input")&&!h,j="placeholder"in document.createElement("textarea")&&!h,k=a.valHooks,l=a.propHooks;i&&j?(g=a.fn.placeholder=function(){return this},g.input=g.textarea=!0):(g=a.fn.placeholder=function(){var a=this;return a.filter((i?"textarea":":input")+"[placeholder]").not(".placeholder").bind({"focus.placeholder":c,"blur.placeholder":d}).data("placeholder-enabled",!0).trigger("blur.placeholder"),a},g.input=i,g.textarea=j,f={get:function(b){var c=a(b),d=c.data("placeholder-password");return d?d[0].value:c.data("placeholder-enabled")&&c.hasClass("placeholder")?"":b.value},set:function(b,f){var g=a(b),h=g.data("placeholder-password");return h?h[0].value=f:g.data("placeholder-enabled")?(""===f?(b.value=f,b!=e()&&d.call(b)):g.hasClass("placeholder")?c.call(b,!0,f)||(b.value=f):b.value=f,g):b.value=f}},i||(k.input=f,l.value=f),j||(k.textarea=f,l.value=f),a(function(){a(document).delegate("form","submit.placeholder",function(){var b=a(".placeholder",this).each(c);setTimeout(function(){b.each(d)},10)})}),a(window).bind("beforeunload.placeholder",function(){a(".placeholder").each(function(){this.value=""})}))});

// JavaScript source code

(function ($) {
    $(function() {
        setTimeout(function() {
            $('input, select').styler();
        }, 100);
        $(".button-add-shipment").on("click", function() {

            $(".jq-selectbox").trigger('refresh');

        });
        $('form').validate({
            valid: function() {

                $('input, select').trigger('refresh');

            },
            invalid: function() {

                $('input, select').trigger('refresh');

            }

        });
        //    var breadBlock = $('.bread-block');
        //    var breadcrumbs = $('.breadcrumbs');
        //    console.log(breadBlock.siblings());
        //    if ($('.jqselect').is('visible')) {

        //        breadcrumbs.css({ "width": "90%" });


//    }
        //    else breadcrumbs.css({ "width": "100%" });

    });
})(jQuery);
!(function ($) {
    $(function () {

        $('ul.menu').on('click', 'li:not(.current)', function () {
            $(this).addClass('current').siblings().removeClass('current')
                .parents('div.wrapper').find('div.box').removeClass('visible').eq($(this).index()).addClass('visible');
            window.location.hash = $(this).data('hash');
            $('input').blur();
        });
        hash = window.location.hash.replace(/#(.+)/, '$1');
        if (hash !== '') {
            $('ul.menu li[data-hash=' + hash + ']').click();
        }

        $.fn.toggleDisabled = function () {
            return this.each(function () {
                this.disabled = !this.disabled;
            });
        };

        $.fn.toggleChecked = function () {
            return this.each(function () {
                this.checked = !this.checked;
            });
        };

        $('button.add1').click(function (e) {
            var inputs = '';
            for (i = 1; i <= 5; i++) {
                inputs += '<label><input type="checkbox" name="checkbox" /> checkbox ' + i + '</label><br />';
            }
            $(this).parents('div.section').append('<div>' + inputs + '</div>');
            $('input:checkbox').styler();
            e.preventDefault();
        });

        $('button.add2').click(function (e) {
            var inputs = '';
            for (i = 1; i <= 5; i++) {
                inputs += '<label><input type="radio" name="radio" /> radio ' + i + '</label><br />';
            }
            $(this).parents('div.section').append('<div>' + inputs + '</div>');
            $('input:radio').styler();
            e.preventDefault();
        });

        $('button.add3').click(function (e) {
            $(this).parents('div.section').append('<br /><br /><select><option>-- �������� --</option><option>����� 1</option><option>����� 2</option><option>����� 3</option><option>����� 4</option><option>����� 5</option></select>');
            $(this).parents('div.section').find('select').styler();
            e.preventDefault();
        });

        $('button.add4').click(function (e) {
            var options = '';
            for (i = 1; i <= 5; i++) {
                options += '<option>Option ' + i + '</option>';
            }
            $(this).parents('div.section').find('select').each(function () {
                $(this).append(options);
            });
            $(this).parents('div.section').find('select').trigger('refresh');
            e.preventDefault();
        });

        $('button.add5').click(function (e) {
            $(this).parents('div.section').append('<div><input type="file" name="" /></div>');
            $('input:file').styler();
            e.preventDefault();
        });

        $('button.add6').click(function (e) {
            $(this).parents('div.section').append('<br /><br /><select multiple><option>-- �������� --</option><option>����� 1</option><option>����� 2</option><option>����� 3</option><option>����� 4</option><option>����� 5</option></select>');
            $(this).parents('div.section').find('select').styler();
            e.preventDefault();
        });

        $('button.check').click(function () {
            $(this).parents('div.section').find('input').toggleChecked().trigger('refresh');
            return false;
        });

        $('button.dis').click(function (e) {
            $(this).parents('div.section').find('input').toggleDisabled().trigger('refresh');
            e.preventDefault();
        });

        $('button.dis2').click(function (e) {
            $(this).parents('div.section').find('select').toggleDisabled().trigger('refresh');
            e.preventDefault();
        });

        $('button.dis3').click(function (e) {
            $(this).parents('div.section').find('option').toggleDisabled().trigger('refresh');
            e.preventDefault();
        });

    });
})(jQuery);
$(document).ready(function () {


    // Initialize navgoco with default options
    $("#category-sidebar").navgoco({
        caretHtml: "",
        accordion: true,
        openClass: "active",
        save: false,
        cookie: false,
        //    {
        //    name: "itfamily.sidebar",
        //    expires: false,
        //    path: "/"
        //},
        slide: {
            duration: 300,
            easing: "swing"
        },
        // Add Active class to clicked menu item
        onClickAfter: function (e, submenu) {
            $("#category-sidebar").find("li").removeClass("active");
            var li = $(this).parent();
            var lis = li.parents("li");
            li.addClass("active");
            lis.addClass("active");
        }
    });
    var body = $('body');
    $(".m-side").superfish({
        delay: 500,
        speed: "fast", // speed of the opening animation. Equivalent to second parameter of jQuery’s .animate() method
        speedOut: "fast",
        animation: {
            width: "show"
        },
        autoArrows: true,
        cssArrows: true,
        hoverClass: "hover",
        popUpSelector: "ul,.m-side-l-i, .m-side-second"//,
    
    });

    $(document).ready(function () {

        $(".m-side-l-i.m-side-second-l").on({
            mouseenter: function () {
                var mSideSecond = $(".m-side-second");
                mSideSecond.css({ "width": "640px" });
                var hoveredBlock = $(this).find(".m-side-link").not(".m-side-link-arrow");
                hoveredBlock.append($("<span/>", {
                    class: "no-includes",
                    text: 'перейти'
                })
                    .css({
                        "position": "absolute",
                        "right": "10px",
                        "top": "0",
                        "color": "white",
                        "font-weight": "normal",
                        "font-size": "14px"
                    })
                    );
            },
            mouseleave: function () {

                var hoveredBlock = $(this).find(".m-side-link").not(".m-side-link-arrow");


                hoveredBlock.find("span.no-includes").remove();

                $(".m-side-second").css({ "width": "286px" });


            }

        });
        var initFirstHide = function(obj, secondRow) {

            if (obj.length > 12) {
                obj.each(function(index) {
                    if (index < 12) {

                        $(this).css({ "display": "block" });
                
                    } else {

                        $(this).css({ "display": "none" });

                    }
                });
                secondRow.append($("<li/>", {
                    "class": "m-side-l-i m-side-second-l btn",
                    "html": "<button class=\"m-side-link button\" is-new=\"new\" level=\"level2\">просмотореть еще в категории</a><i class=\"fa fa-arrow-circle-o-down\"></i>"
                })).css({ "display": "block" });

            };
        };
        var initSecondHide = function (obj, secondRow) {

            obj.each(function (index) {

                if (index < 12) {

                    $(this).css({ "display": "none" });

                } else {

                    $(this).css({ "display": "block" });

                }
            });

            secondRow.find("li .m-side-link.button").text("перейти назад");
          

        };
        $(".m-side-second").on({
            mouseenter: function () {
                var secndRow = $(this).find("ul.m-side-subl");

                var secondRowItems = $(this).find("ul.m-side-subl li.m-side-l-i.m-side-second-l");

                $(".m-side-second").find("li.btn").remove();

                initFirstHide(secondRowItems, secndRow);

                var button = $(this).find("button.m-side-link.button");

                button.click(function () {

                    initSecondHide(secondRowItems, secndRow);

                    $(this).click(function () {
                        secondRowItems.each(function (index) {

                            $(this).css({ "display": "none" });

                        });
                        $(".m-side-second").find("li.btn").remove();

                        secndRow.trigger("mouseenter");

                    });
                });
          

            },
            mouseleave: function() {
            
            }
        });
        });
        
   
    $(".m-side-l-i.m-side-second-l").on({
       mouseenter: function () {
           var mSideSecond = $(".m-side-second");
           mSideSecond.css({ "width": "640px" });
           var hoveredBlock = $(this).find(".m-side-link").not(".m-side-link-arrow");
           hoveredBlock.append($("<span/>", {
               class: "no-includes",
               text: 'перейти'
           })
               .css({
                   "position": "absolute",
                   "right": "10px",
                   "top": "0",
                   "color": "white",
                   "font-weight": "normal",
                   "font-size": "14px"
               })
               );
       },
       mouseleave: function () {

           var hoveredBlock = $(this).find(".m-side-link").not(".m-side-link-arrow");


           hoveredBlock.find("span.no-includes").remove();

            $(".m-side-second").css({ "width": "286px" });


       }

    });
   
    var toolbox = $('.m-side-scroll'),
         height = toolbox.height(),
         scrollHeight = toolbox.get(0).scrollHeight;

    toolbox.bind('mousewheel', function (e, d) {
        if ((this.scrollTop === (scrollHeight - height) && d < 0) || (this.scrollTop === 0 && d > 0)) {
            e.preventDefault();
        }
    });

   
});
// JavaScript source code
$(document).ready(function () {

    $('.tooltip-bottom').tooltipster
    ({
        animation: 'fade',
        delay: 200,
        theme: 'tooltipster-light',
        touchDevices: true,
        trigger: 'hover',
        position: "bottom"
    });

    $('.tooltip-left').tooltipster
    ({
        animation: 'fade',
        delay: 200,
        theme: 'tooltipster-light',
        touchDevices: true,
        trigger: 'hover',
        position: "left"
    });
    $('.tooltip-right').tooltipster
  ({
      animation: 'fade',
      delay: 200,
      theme: 'tooltipster-light',
      touchDevices: true,
      trigger: 'hover',
      position: "right"
  });


    $('.tooltip').tooltipster({

        animation: 'fade',
        delay: 200,
        theme: 'tooltipster-light',
        touchDevices: true,
        trigger: 'hover'

    });
});
