using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.JournalOrder
{
    public class GetJournalOrdersOperation : BaseOperation
    {
        public List<OrderComesModel> OrderComesModel { get; set; }
        private String TokenHash { get; set; }
        private OrderType OrderType { get; set; }
        private DateTime From { get; set; }
        private DateTime To { get; set; }
        public GetJournalOrdersOperation(string tokenHash, OrderType orderType)
        {
            TokenHash = tokenHash;
            OrderType = orderType;
            From = DateTime.MinValue;
            To = DateTime.MinValue;
            RussianName = "Получение журнала покупок (клиенты или парнеры)";
        }

        public GetJournalOrdersOperation(string tokenHash, OrderType orderType, DateTime @from, DateTime to)
        {
            TokenHash = tokenHash;
            OrderType = orderType;
            From = @from;
            To = to.AddDays(1).AddMinutes(-1);
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var contractor =
                Context.Contractors.FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            if(contractor==null)
                throw new ObjectNotFoundException("Объект не найдет. Операция: Получение журнала покупок ");
            List<OrderComes> orders = null;

            if (OrderType != OrderType.AllOrders)
            {
                if(From == DateTime.MinValue && To == DateTime.MinValue)
                    orders = Context.OrderComes.Where(x => x.OrderType == OrderType && x.ContractorId == contractor.Id && !x.Deleted).ToList();
                else
                {
                    orders = Context.OrderComes.Where(x => x.OrderType == OrderType && x.ShipingDate.HasValue 
                        && x.ShipingDate.Value >= From 
                        && x.ShipingDate.Value <= To
                        && x.ContractorId == contractor.Id && !x.Deleted).ToList();
                }
            }
            else
            {
                if (From == DateTime.MinValue && To == DateTime.MinValue)
                    orders = Context.OrderComes.Where(x => x.ContractorId == contractor.Id && !x.Deleted).ToList();
                else
                {
                    orders = Context.OrderComes.Where(x => x.ShipingDate.HasValue 
                        && x.ShipingDate.Value >= From 
                        && x.ShipingDate.Value <= To
                        && x.ContractorId == contractor.Id && !x.Deleted).ToList();
                }
            }
            //if (orders.Count == 0)
            //    throw new OrderException("Заказы отсуствуют!");
            if (contractor.Role.Name == ConstV.RolePartner)
                OrderComesModel = orders.Select(x => OurMaps.ConvertToModel(x, StatusRole.Partner)).ToList();
            else if (contractor.Role.Name == ConstV.RoleAdministrator)
                OrderComesModel = orders.Select(x => OurMaps.ConvertToModel(x, StatusRole.Administrator)).ToList();
            else if (contractor.Role.Name == ConstV.RoleClient)
                OrderComesModel = orders.Select(x => OurMaps.ConvertToModel(x, StatusRole.Client)).ToList();
            else if (contractor.Role.Name == ConstV.RoleManager)
                OrderComesModel = orders.Select(x => OurMaps.ConvertToModel(x, StatusRole.Manager)).ToList();
        }
    }
}