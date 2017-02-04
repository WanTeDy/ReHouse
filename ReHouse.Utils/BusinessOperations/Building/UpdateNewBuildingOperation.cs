using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class UpdateNewBuildingOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _russianName { get; set; }
        private Int32 _titleId { get; set; }
        public Title _title { get; set; }


        public UpdateNewBuildingOperation(string tokenHash, int titleId, string russianName)
        {
            _tokenHash = tokenHash;
            _titleId = titleId;
            _russianName = russianName;
            RussianName = "Изменение стандартного заголовка объявлений";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _title = Context.Titles.FirstOrDefault(x => x.Id == _titleId && !x.Deleted);
            if (_title == null)
                Errors.Add("Id", "Выбранный заголовок не найден. TitleId = " + _titleId);            
            else
            {
                if(_title.RussianName.ToLower() == _russianName.ToLower())                
                    Errors.Add("RussianName", "Такой имя заголовока уже существует!");
                else
                {
                    _title.RussianName = _russianName;
                    Context.SaveChanges();
                }
            }
        }
    }
}