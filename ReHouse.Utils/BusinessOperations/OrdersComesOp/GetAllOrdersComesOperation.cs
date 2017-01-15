using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class GetAllOrdersComesOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public List<OrderComesModel> OrderComeses { get; set; }
        public Int32 NowExist { get; set; }
        public GetAllOrdersComesOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Получение списка приходящих заказов";
        }

        protected override void InTransaction()
        {
            Contractor contractor = CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var orders = Context.OrderComes.Where(x => !x.Deleted && x.OrderType != OrderType.Draft).ToList();
            if (contractor.Role.Name == ConstV.RoleManager)
            {
                OrderComeses = orders.Where(x => x.ManagerId == contractor.Id).Select(x => OurMaps.ConvertToModel(x, StatusRole.Manager)).ToList();
                var newOrder = orders.FirstOrDefault(x => !x.Deleted && x.OrderType == OrderType.NewOrder);
                if(newOrder != null)
                    OrderComeses.Add(OurMaps.ConvertToModel(newOrder, StatusRole.Manager));
            }
            else if(contractor.Role.Name == ConstV.RoleAdministrator)
            {
                OrderComeses = orders.Select(x => OurMaps.ConvertToModel(x, StatusRole.Administrator)).ToList();            
            }
            var r = Context.OrderComes.Count(x => x.OrderType == OrderType.NewOrder);
            NowExist = r;
        }
    }
}