using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Flat
{
    public class DeleteFlatOperation : BaseOperation //TODO
    {
        private String _tokenHash { get; set; }
        private Int32 _titleId { get; set; }
        public Title _title { get; set; }

        public DeleteFlatOperation(string tokenHash, int titleId)
        {
            _tokenHash = tokenHash;
            _titleId = titleId;
            RussianName = "Удаление стандартных заголовков объявлений";
        }

        protected override void InTransaction()
        {
            var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _title = Context.Titles.FirstOrDefault(x => x.Id == _titleId && !x.Deleted);
            if (_title == null)
                Errors.Add("Id", "Выбранный заголовок не найден. TitleId = " + _titleId);
            else
            {
                _title.Deleted = true;
                Context.SaveChanges();
            }
        }
    }
}