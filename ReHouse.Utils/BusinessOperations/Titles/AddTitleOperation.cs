using System;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Helpers;

namespace ReHouse.Utils.BusinessOperations.Titles
{
    public class AddTitleOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _russianName { get; set; }
        public Title _title { get; set; }

        public AddTitleOperation(string tokenHash, string russianName)
        {
            _tokenHash = tokenHash;
            _russianName = russianName;
            RussianName = "Добавление нового заголовка объявлений";
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