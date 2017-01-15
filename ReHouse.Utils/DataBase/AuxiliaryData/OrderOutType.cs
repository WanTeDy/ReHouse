namespace ITfamily.Utils.DataBase.AuxiliaryData
{
    public enum OrderOutType
    {
        All,
        New, //Черновик
        invoice, //Счет
        quotation, //Счет
        reserved, //Бронь
        ordered, //Отгрузка
        prepaid //Отгрузка по предоплате
    }
}