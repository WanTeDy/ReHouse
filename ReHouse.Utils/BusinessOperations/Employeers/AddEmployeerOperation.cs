using System;
using System.Linq;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Employeers
{
    public class AddEmployeerOperation : BaseOperation
    {
        public Contractor Employeer { get; set; }
        public Int32 RoleId { get; set; }
        public String TokenHash { get; set; }

        public AddEmployeerOperation(Contractor employeer, int roleId, string tokenHash)
        {
            Employeer = employeer;
            RoleId = roleId;
            TokenHash = tokenHash;
            RussianName = "Добавление нового работника";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            
            var role = Context.RoleSet.FirstOrDefault(x => x.Id == RoleId);
            if(role == null)
                throw new ObjectNotFoundException("Должность не найдена! Id = " + RoleId);
            
            Employeer.RoleId = role.Id;
            var cont = Context.Contractors.FirstOrDefault(x => !x.Deleted && x.Email == Employeer.Email);
            if (cont != null)
                throw new ItFamilyException("Такой email уже существует!");
            Context.Contractors.Add(Employeer);
            Context.SaveChanges();
        }
    }
}