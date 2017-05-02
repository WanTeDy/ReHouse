using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Vacancies
{
    public class DeleteVacancyOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _vacanciesId { get; set; }

        public DeleteVacancyOperation(string tokenHash, int[] vacanciesId)
        {
            _tokenHash = tokenHash;
            _vacanciesId = vacanciesId;
            RussianName = "Удаление вакансии";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_vacanciesId != null && _vacanciesId.Length > 0)
            {
                foreach (var vacanciesId in _vacanciesId)
                {
                    var vacancy = Context.Vacancies.FirstOrDefault(x => x.Id == vacanciesId && !x.Deleted);
                    if (vacancy != null)
                        vacancy.Deleted = true;
                }
                Context.SaveChanges();
            }
        }
    }
}
