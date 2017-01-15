namespace ITfamily.Utils.DataBase.AuxiliaryData
{
    public enum PaymentStatus
    {
        PaidOrder, //Оплочен
        Prepayment, //предоплата/частичная предоплата (аванс)
        NotPayment, //Оплата при получении
        CreditLine //кредитная линия
    }
}