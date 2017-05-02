using System;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.DataBase.Vacancies;

namespace ReHouse.Utils.BusinessOperations.Vacancies
{
    public class AddVacancyOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public Vacancy _vacancy { get; set; }

        public AddVacancyOperation(string tokenHash, Vacancy vacancy)
        {
            _tokenHash = tokenHash;
            _vacancy = vacancy;
            RussianName = "Добавление вакансии";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            if (String.IsNullOrWhiteSpace(_vacancy.Title))
                Errors.Add("Title", "*Укажите заголовок!");
            else
            {
                if (String.IsNullOrWhiteSpace(_vacancy.Description))
                    Errors.Add("Description", "*Укажите описание!");
                else
                {
                    _vacancy.Date = DateTime.Now;
                    _vacancy.Deleted = false;         
                    Context.Vacancies.Add(_vacancy);
                    Context.SaveChanges();
                }
            }
        }
    }
}