namespace ITfamily.Utils.DataBase.AuxiliaryData
{
    //сделать совмещение нескольких состояний enum
    public enum OrderType
    {
        AllOrders,
        Draft,  //Черновик
        NewOrder, //Новый заказ, ожидание обработки менеджером
        PrepaidOrder, //обрабатывается менеджерами
        //VerifyTheValidityOfTheRemoteStock,//проверена актуальность на удаленном складе
        ConfirmedByCustomer,//подтвержден клиентом
        Ordered,//заказан
        ReadyToExtradition,//Готов к выдаче
        Extradited,//Выдан 
        Closed,//Закрыт 
        Canceled//Отменен
    }
}