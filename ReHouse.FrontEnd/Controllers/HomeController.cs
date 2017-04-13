#define CASH

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.Utils.DataBase;
using ReHouse.Utils.BusinessOperations.Home;
using ReHouse.Utils.Helpers;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.BusinessOperations;

namespace ReHouse.FrontEnd.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            ViewBag.Route = RouteEnum.HomeIndex;
            ViewBag.MainPage = true;
                        
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            else
                SessionHelpers.Session("CountProducts", 0);
            var operation = new LoadAdvertsForHomePageOperation(tokenHash);
            operation.ExcecuteTransaction();
            var model = new LoadAdvertsForHomePageModel
            {
                HotAdverts = operation._hotAdverts,
                FlatSaleAdverts = operation._flatSaleAdverts,
                HouseSaleAdverts = operation._houseSaleAdverts,
                NewBuildingAdverts = operation._newBuildingAdverts,
                Articles = operation._articles,
            };
            return View(model);
        }
    }
}






/*
 var context = new DbReHouse();
            List<String> list = new List<String> { "Авангард",
"Александровка",
"Ананьев",
"Арциз",
"Балта",
"Белгород-Днестровский",
"Белино",
"Беляевка",
"Березино",
"Березовка",
"Болград",
"Борщи",
"Бритовка",
"Бурлача балка",
"Великая Михайловка",
"Великодолинское",
"Визирка",
"Вилково",
"Выгода",
"Григоровка",
"Дзинилор",
"Еремеевка",
"Жеребково",
"Загнитков",
"Затишье",
"Затока",
"Ивановка",
"Измаил",
"Ильичевка",
"Исаево",
"Каролино-бугаз",
"Килия",
"Кирнички",
"Кодыма",
"Коминтерновское",
"Котовка",
"Котовск",
"Красноселка",
"Красные Окны",
"Кремидовка",
"Криничное",
"Крыжановка",
"Ланна",
"Ларжанка",
"Лиманское",
"Любашевка",
"Малодолинское",
"Маяки",
"Мизикевича",
"Молодежное",
"Нерубайское",
"Николаевка",
"Новая долина",
"Новая дофиновка",
"Новая некрасовка",
"Новоборисовка",
"Новые беляры",
"Овидиополь",
"Одесса",
"Ониськово",
"Петровка",
"Петродолинское",
"Платоново",
"Прилиманское",
"Радостное",
"Раздельная",
"Рени",
"Саврань",
"Салганы",
"Сарата",
"Сафьяны",
"Сергеевка",
"Словяносербка",
"Старая некрасовка",
"Суворово",
"Сычавка",
"Табаки",
"Таирово",
"Тарутино",
"Татарбунары",
"Теплодар",
"Усатово",
"Фонтанка",
"Фрунзовка",
"Хлебодарское",
"Холодная балка",
"Червоноармейское",
"Черноморск",
"Черноморское",
"Шабо",
"Шевченково",
"Ширяево",
"Южный"
            };
            foreach(var el in list)
            {
                context.Cities.Add(new Utils.DataBase.Geo.City
                {
                    RussianName = el,
                });
            }
            context.SaveChanges();
     */
