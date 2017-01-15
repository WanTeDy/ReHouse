using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class AddNotesOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public Int32 SelectedId { get; set; }
        public String Notes { get; set; }

        public AddNotesOperation(string tokenHash, int selectedId, string notes)
        {
            TokenHash = tokenHash;
            SelectedId = selectedId;
            Notes = notes;
            RussianName = "Добавление примечаний к приходящему заказу";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var order = Context.OrderComes.FirstOrDefault(x => x.Id == SelectedId && !x.Deleted);
            if(order == null)
                throw new ObjectNotFoundException("Приходящий заказ Id: " + SelectedId + " не найден.");
            order.Notes += "\r\n\n " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") +"\t " + Notes;
            Context.SaveChanges();
        }
    }
}