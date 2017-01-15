using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class AnalyzeGoodsFromOrderComesModel
    {
        public Int32 Quantity { get; set; }
        public Decimal AmountSoldPriceUsd { get; set; }
        public Decimal AmountPurchasePrice { get; set; }
        public Int32 ProductId { get; set; }
        public String Articul { get; set; }
        public List<AdditionalDataForAnalyzeGoods> OrderComesData { get; set; }

        public String ProductName { get; set; }
        public String Warranty { get; set; }
        public Int32 CounterVisit { get; set; }
        public FromWhatProvider FromWhat { get; set; }
        public String FromWhatProvider { get; set; }
        public AnalyzeGoodsFromOrderComesModel()
        {
            OrderComesData = new List<AdditionalDataForAnalyzeGoods>();
        }
    }
}