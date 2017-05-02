using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.DataBase.Vacancies;

namespace ReHouse.Utils.BusinessOperations.Vacancies
{
    public class UpdateVacancyOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public Vacancy _vacancy { get; set; }

        public UpdateVacancyOperation(string tokenHash, Vacancy vacancy)
        {
            _tokenHash = tokenHash;
            _vacancy = vacancy;
            RussianName = "Изменение вакансии";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var vacancy = Context.Vacancies.FirstOrDefault(x => x.Id == _vacancy.Id && !x.Deleted);
            if (vacancy == null)
            {
                Errors.Add("Id", "*Вакансия не найдена!");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(_vacancy.Title))
                    Errors.Add("Title", "*Укажите заголовок!");
                else
                {
                    if (String.IsNullOrWhiteSpace(_vacancy.Description))
                        Errors.Add("Description", "*Укажите описание!");
                    else
                    {
                        vacancy.Title = _vacancy.Title;
                        vacancy.Description = _vacancy.Description;
                        Context.SaveChanges();
                    }
                }
            }
        }
    }
}