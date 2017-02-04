using System;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class AddNewBuildingOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _russianName { get; set; }
        public Title _title { get; set; }

        public AddNewBuildingOperation(string tokenHash, string russianName)
        {
            _tokenHash = tokenHash;
            _russianName = russianName;
            RussianName = "Добавление нового объявления по новостройкам";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var title = Context.Titles.FirstOrDefault(x => x.RussianName.ToLower() == _russianName.ToLower());
            if (title != null)
                Errors.Add("RussianName", "Такая роль уже существует!");
            else
            {
                _title = new Title
                {                    
                    RussianName = _russianName,
                };
                Context.Titles.Add(title);
                Context.SaveChanges();
            }            
        }
    }
}