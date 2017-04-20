using System;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.Feedback;

namespace ReHouse.Utils.BusinessOperations.Feedbacks
{
    public class AddFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _username { get; set; }
        private String _comment { get; set; }
        public UserFeedback _userFeedback { get; set; }

        public AddFeedbackOperation(string tokenHash, string username, string comment)
        {
            _tokenHash = tokenHash;
            _username = username;
            _comment = comment;
            RussianName = "Добавление нового коммента";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash && !x.Deleted);

            if (String.IsNullOrWhiteSpace(_username))
                Errors.Add("Username", "*Укажите своё имя!");
            else
            {
                if (String.IsNullOrWhiteSpace(_comment))
                    Errors.Add("Description", "*Укажите свой отзыв!");
                else
                {
                    _userFeedback = new UserFeedback
                    {
                        Username = _username,
                        Description = _comment,
                        Date = DateTime.Now,
                        IsModerated = false,
                        Deleted = false,
                    };
                    Context.UserFeedbacks.Add(_userFeedback);
                    Context.SaveChanges();
                }
            }
        }
    }
}