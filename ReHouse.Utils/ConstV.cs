using System;
using System.Collections.Generic;
//using ReHouse.Utils.DataBase.AuxiliaryData;
//using ReHouse.Utils.DataBase.ModelForUI;
//using ReHouse.Utils.DataBase.PriceRules;

namespace ReHouse.Utils
{
    public class ConstV
    {
        //public const Int32 ACategoryId = 1; //пропускаем
        //public const Int32 BCode = 2;
        //public const Int32 CGroup = 3;
        //public const Int32 DArticle = 4;
        //public const Int32 EVendor = 5;
        //public const Int32 FModel = 6;
        //public const Int32 GName = 7;
        //public const Int32 HDescription = 8; //can be empty
        //public const Int32 IPriceUSD = 9;
        //public const Int32 JPriceInd = 10; //can be empty
        //public const Int32 KCategoryName = 11;
        //public const Int32 LBonus = 12; //пропускаем
        //public const Int32 MRecommendedPrice = 13; //can be empty
        //public const Int32 N_DDP = 14; //can be empty
        //public const Int32 OWarranty = 15; //can be empty
        public const Int32 ItemsPerPage = 8; 
        //public const Int32 QNote = 17;  //can be empty
        //public const Int32 RDayDelivery = 18; //0 1 2 3 Days
        //public const Int32 SProductId = 19; //пропускаем
        //public const Int32 TURL = 20;
        
        public const String RoleAdministrator = "Администратор";
        //public const String RoleManager = "Менеджер";
        //public const String RolePartner = "Партнер";
        //public const String RoleClient = "Клиент";

        //public const String OneTimeService = "Единоразовая уcлуга";
        //public const String EveryDayService = "Ежедневная";
        //public const String WeeklyService = "Еженедельная";
        //public const String MonthlyService = "Помесячная";
        //public const String QuarterlyService = "Поквартальная";

        //public const String OurCurrencyName = "Свой курс";
        //public const String CourseExtractCashless = "Курс выписки (б\\н):";
        //public const String CourseEnrollmentCashless = "Курс зачета (б\\н):";
        //public const String CourseCash = "Курс (н):";
        //public const String Provider1CourseExtractCashless = "грн б/н ";
        //public const String Provider1CourseEnrollmentCashless = "грн DDP";
        //public const String Provider1CourseCash = "грн нал";
        //public const String Provider1CurrencyName = "Поставщик 1";
        //public const String Provider2CurrencyName = "Поставщик 2";

        //public const String ChangeCreditLimit = "Изменять кредитный лимит";
        //public const String ChangeRoles = "Изменять роль для контр.агента";
        //public const String IsActive = "Блокировать контр.агентов";

        //public const String PathCarousel = "C:\\inetpub\\wwwroot\\images\\carousel";
        //public const String PathStocks = "C:\\inetpub\\wwwroot\\images\\stocks";
        //public const String PathAppManager = "C:\\inetpub\\wwwroot\\AppManager";
        //public const String UrlCarousel = "http://www.ReHouse.com.ua/images/carousel/";
        //public const String UrlStocks = "http://www.ReHouse.com.ua/images/stocks/";
        //public const String UrlAppManager = "http://www.ReHouse.com.ua/AppManager/";

        //public const String BrainUrlForImages = "";
        //public const String ReHouseUrlForImages = "";
        //public const String PathToSaveImages = "";

        //public static readonly Dictionary<FromWhatProvider, String> FromWhatProviderToString = new Dictionary<FromWhatProvider, string>
        //{
        //    {FromWhatProvider.OurProduct, "Свой товар"},
        //    {FromWhatProvider.Provider1, "От 1 Поставщика"},
        //    {FromWhatProvider.All, "Все поставщки"},
        //    //{FromWhatProvider.Provider2, "От 2 Поставщика"},            
        //};
        //public static readonly Dictionary<OrderOutType, String> OrderOutTypeToString = new Dictionary<OrderOutType, string>
        //{
        //    {OrderOutType.All, "Все"},
        //    {OrderOutType.New, "Черновик"},
        //    {OrderOutType.invoice, "Счет invoice"},
        //    {OrderOutType.ordered, "Отгрузка"},
        //    {OrderOutType.prepaid, "Отгрузка по предоплате"},
        //    {OrderOutType.quotation, "Счет quotation"},
        //    {OrderOutType.reserved, "Бронь"},
        //};
        //public static readonly Dictionary<String, OrderOutType> OrderOutTypeFromString = new Dictionary<string, OrderOutType>
        //{
        //    //{"Все", OrderOutType.All},
        //    {"Черновик", OrderOutType.New},
        //    {"Счет invoice", OrderOutType.invoice},
        //    {"Отгрузка", OrderOutType.ordered},
        //    {"Отгрузка по предоплате", OrderOutType.prepaid},
        //    {"Счет quotation", OrderOutType.quotation},
        //    {"Бронь", OrderOutType.reserved},
        //};
        //public static readonly Dictionary<string, string> TypeOrders = new Dictionary<string, string>
        //{
        //    {"default","Все"},
        //    {"new", "Черновик"},
        //    {"invoice", "Счет"},
        //    {"quotation", "Счет"},
        //    {"reserved", "Бронь"},
        //    {"ordered", "Отгрузка"},
        //    {"prepaid", "Отгрузка по предоплате"}
        //};
        //public static readonly Dictionary<ProductStatusInStock, String> ProductStatusInStocks = new Dictionary<ProductStatusInStock, string>
        //{
        //    {ProductStatusInStock.InStock, "В наличии"},
        //    {ProductStatusInStock.NotProcessed, "Не обработан"},
        //    {ProductStatusInStock.Sold, "Продан"},
        //    {ProductStatusInStock.PendingDelivery, "В ожидании доставки"},
        //};
        //public static readonly Dictionary<String, ProductStatusInStock> ProductStatusInStocksFromString = new Dictionary<String, ProductStatusInStock>
        //{
        //    {"В наличии", ProductStatusInStock.InStock},
        //    {"Не обработан", ProductStatusInStock.NotProcessed},
        //    {"Продан", ProductStatusInStock.Sold},
        //    {"В ожидании доставки", ProductStatusInStock.PendingDelivery},
        //};
        ////ProductStatusInStocksFromString
        //public static readonly Dictionary<RefreshTime, int> RefreshTimes = new Dictionary<RefreshTime, int>
        //{
        //    {RefreshTime.NotRefresh, -1},
        //    {RefreshTime.In5Second, 5000},
        //    {RefreshTime.In10Second, 10000},
        //    {RefreshTime.In15Second, 15000},
        //    {RefreshTime.In30Second, 30000},
        //    {RefreshTime.In1Minute, 60000},
        //    {RefreshTime.In3Minute, 180000},
        //    {RefreshTime.In5Minute, 300000},
        //};
        //public static readonly Dictionary<RefreshTime, string> RefreshTimesToString = new Dictionary<RefreshTime, string>
        //{
        //    {RefreshTime.NotRefresh, "Не обновлять"},
        //    {RefreshTime.In5Second, "Раз в 5 секунд"},
        //    {RefreshTime.In10Second, "Раз в 10 секунд"},
        //    {RefreshTime.In15Second, "Раз в 15 секунд"},
        //    {RefreshTime.In30Second, "Раз в 30 секунд"},
        //    {RefreshTime.In1Minute, "Раз в 1 минуту"},
        //    {RefreshTime.In3Minute, "Раз в 3 минуты"},
        //    {RefreshTime.In5Minute, "Раз в 5 минут"},
        //};
        //public static readonly Dictionary<SelectedTables, String> SelectTable = new Dictionary<SelectedTables, string>
        //{
        //    {SelectedTables.ImportViewGrid, "Import"},
        //    {SelectedTables.InfoStatesViewGrid, "InfoStates"},
        //    {SelectedTables.LoggersViewGrid, "Loggers"},
        //};

        //public static readonly Dictionary<OrderType, String> OrderTypes = new Dictionary<OrderType, string>
        //{
        //    {OrderType.AllOrders, "Все заказы"},
        //    {OrderType.Canceled, "Отменен"},
        //    {OrderType.Closed, "Закрыт"},
        //    {OrderType.ConfirmedByCustomer, "Подтвержден клиентом"},
        //    {OrderType.Extradited, "Выдан"},
        //    {OrderType.Draft, "Черновик"}, //
        //    {OrderType.NewOrder, "Новый заказ"},
        //    {OrderType.Ordered, "Заказан"},
        //    {OrderType.PrepaidOrder, "Обрабатывается менеджером"},
        //    {OrderType.ReadyToExtradition, "Готов к выдаче"},
        //};

        //public static readonly Dictionary<PaymentStatus, String> PaymentStatuses = new Dictionary<PaymentStatus, string>
        //{
        //    {PaymentStatus.NotPayment, "Не оплочен"},
        //    {PaymentStatus.CreditLine, "Кредитная линия"},
        //    {PaymentStatus.PaidOrder, "Оплочен"},
        //    {PaymentStatus.Prepayment, "Предоплата/частичная предоплата (аванс)"},
        //};

        //public static readonly Dictionary<PaymentMethod, String> PaymentMethods = new Dictionary<PaymentMethod, string>
        //{
        //    {PaymentMethod.CreditLine, "Кредитная линия"},
        //    {PaymentMethod.FullPayment, "Предоплата"},
        //    {PaymentMethod.PaymentOnDelivery, "Наложный платеж"},
        //};
        //public static readonly Dictionary<String, PaymentMethod> PaymentMethodsFromString = new Dictionary<string, PaymentMethod>
        //{
        //    {"Кредитная линия", PaymentMethod.CreditLine},
        //    {"Предоплата", PaymentMethod.FullPayment},
        //    {"Наложный платеж", PaymentMethod.PaymentOnDelivery},
        //};

        //public static readonly Dictionary<DeliveryType, String> DeliveryTypes = new Dictionary<DeliveryType, string>
        //{
        //    {DeliveryType.PickupFromStock, "Cамовывоз"},
        //    {DeliveryType.SendingNovaPoshta, "Нова почта"},
        //};
        //public static readonly Dictionary<String, DeliveryType> DeliveryTypesFromString = new Dictionary<string, DeliveryType>
        //{
        //    {"Cамовывоз", DeliveryType.PickupFromStock},
        //    {"Нова почта", DeliveryType.SendingNovaPoshta},
        //};

        //public static readonly Dictionary<TypeRule, String> TypesRule = new Dictionary<TypeRule, string>
        //{
        //    //{TypeRule.AddUah, "+ UAH"},
        //    {TypeRule.AddUsd, "+ USD"},
        //    {TypeRule.MultiplePercent, "* %"},
        //};

        //public static readonly Dictionary<String, TypeRule> TypesRuleStringToType = new Dictionary<string, TypeRule>
        //{
        //    //{"+ UAH",TypeRule.AddUah},
        //    {"+ USD",TypeRule.AddUsd},
        //    {"* %", TypeRule.MultiplePercent},
        //};

        //public static readonly Dictionary<ForWhom, String> ForWhoms = new Dictionary<ForWhom, string>
        //{
        //    {ForWhom.Clients, "Для клиентов"},
        //    {ForWhom.OtherPartner, "Для других партнеров"},
        //    {ForWhom.OurPartner, "Для своих партнеров"},
        //};

        //public static readonly Dictionary<CurrencyType, String> CurrencyTypes = new Dictionary<CurrencyType, string>
        //{
        //    {CurrencyType.USD, "USD"},    
        //    {CurrencyType.UAH, "UAH"},
        //};

        //public static readonly Dictionary<String, ForWhom> ForWhomStringToType = new Dictionary<String, ForWhom>
        //{
        //    {"Для клиентов",ForWhom.Clients},
        //    {"Для других партнеров",ForWhom.OtherPartner},
        //    {"Для своих партнеров", ForWhom.OurPartner},
        //};

        //public static readonly Dictionary<int, string> BrainErrors = new Dictionary<int, string>
        //{
        //    {1,"Не указан логин"},
        //    {2, "Не указан пароль"},
        //    {3, "Не указан идентификатор сессии (SID)"},
        //    {4, "Неправильный идентификатор сессии (SID)"},
        //    {5, "Окончилось время жизни сессии"},
        //    {6, "Неправильный логин или пароль"},
        //    {7, "Пользователь заблокирован"},
        //    {8, "Неправильный идентификатор категории (categoryID)"},
        //    {9, "Неправильный идентификатор фильтра (filterID)"},
        //    {10, "Неправильное значение параметра limit"},
        //    {11, "Неправильное значение параметра max_price"},
        //    {12, "Неправильное значение параметра min_price"},
        //    {13, "Неправильное значение параметра offset"},
        //    {14, "Неправильное значение параметра order"},
        //    {15, "Неправильный идентификатор товара (productID)"},
        //    {16, "Неправильная строка поиска"},
        //    {17, "Неправильное значение параметра sortby"},
        //    {18, "Неправильный идентификатор склада (stockIDs)"},
        //    {19, "Неправильный идентификатор производителя (vendorID)"},
        //    {20, "Превышено максимальное значение параметра limit"},
        //    {21, "Не существует категории с указанным идентификатором (categoryID)"},
        //    {22, "Не существует времени получения указанного товара на указанном складе"},
        //    {23, "Не существует товара с указанным идентификатором (productID)"},
        //    {24, "В указанной категории не существует указанного фильтра"},
        //    {25, "В указанной категории не существует указанного производителя"},
        //    {26, "Не существует склада с указанным идентификатором (stockIDs)"},
        //    {27, "Неправильный формат JSON"},
        //    {28, "Отсутствуют обязательные параметры товара"},
        //    {29, "Неправильные параметры товара"},
        //    {30, "Не указаны данные"},
        //    {31, "Неправильные параметры товара"},
        //    {32, "Неправильный код товара (product_code)"},
        //    {33, "Неправильный артикул товара (articul)"},
        //    {34, "Недостаточно прав. Обратитесь к администратору."},
        //    {35, "Неправильная (некорректная) дата"},
        //    {36, "Превышен максимальный временной диапазон (в днях)"},
        //    {37, "Не удалось загрузить данные"},
        //    {38, "Не указана валюта"},
        //    {39, "Неправильная валюта"},
        //    {40, "Не указан идентификатор пункта выдачи / службы доставки"},
        //    {41, "Неправильный идентификатор пункта выдачи / службы доставки"},
        //    {42, "Для данного типа отгрузки заказа обязательно нужно указать адрес доставки"},
        //    {43, "Неправильный идентификатор адреса доставки"},
        //    {44, "Неправильная привязка адреса к службе доставки"},
        //    {45, "Произошла ошибка. Пожалуйста, свяжитесь с администратором"},
        //    {46, "Отсутствует список товаров"},
        //    {47, "Заказ не найден"},
        //    {48, "Операция недоступна"},
        //    {49, "Не указана дата отгрузки"},
        //    {50, "Неправильная (некорректная) дата отгрузки"},
        //    {51, "Неправильный идентификатор филиала"},
        //    {52, "Неправильный тип заказа"},
        //    {53, "Не существует товара с указанными параметрами"},
        //    {54, "Превышение частоты запроса для данного типа отчета"},
        //    {55, "Неправильный идентификатор контактного лица"},
        //    {56, "Неправильный код города или оператора"},
        //    {57, "Недопустимые символы в номере телефона"},
        //    {58, "Неправильный email, телефон или пароль"},
        //    {59, "Некорректный параметр языка"},
        //    {60, "Некорректный формат прайслиста"},
        //    {61, "Некорректный формат прайслиста"}
        //};


        //Description	PriceUSD	Price_ind	CategoryName	
        //Bonus	RecommendedPrice	DDP	Warranty	Stock	Note	DayDelivery	ProductID	URL
    }
}