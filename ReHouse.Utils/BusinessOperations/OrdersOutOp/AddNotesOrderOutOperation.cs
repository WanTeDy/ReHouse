using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersOutOp
{
    public class AddNotesOrderOutOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 OrderOutId { get; set; }
        public String Notes { get; set; }

        public AddNotesOrderOutOperation(string tokenHash, int orderOutId, string notes)
        {
            TokenHash = tokenHash;
            OrderOutId = orderOutId;
            Notes = notes;
        }

        protected override void InTransaction()
        {
            var order = Context.OrderOut.FirstOrDefault(x => x.Id == OrderOutId && !x.Deleted);
            if(order==null)
                throw new ObjectNotFoundException("Обьект заказа не найден. Id = " + OrderOutId);
            order.Notes += "\r\n\n " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\t " + Notes;
            Context.SaveChanges();
        }
    }
}