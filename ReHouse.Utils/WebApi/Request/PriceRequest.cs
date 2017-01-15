using System;

namespace ITfamily.Utils.WebApi.Request
{
    public class PriceRequest : BaseRequest
    {
        public Int32 CategoryId { get; set; }
        public Int32 ProductId { get; set; }
        public Decimal PriceFromUsd { get; set; }
        public Decimal PriceToUsd { get; set; }
        /// <summary>
        /// it's can be percent or money in uah for plus to product price_uah
        /// </summary>
        public Decimal ForSaleMin { get; set; }
        /// <summary>
        /// it's can be percent or money in uah for plus to product price_uah
        /// </summary>
        public Decimal ForSaleRetail { get; set; }
    }
}