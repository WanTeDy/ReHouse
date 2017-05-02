using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.Feedback;

namespace ReHouse.Utils.BusinessOperations.Feedbacks
{
    public class UpdateFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private UserFeedback _model { get; set; }
        public UserFeedback _feedback { get; set; }

        public UpdateFeedbackOperation(string tokenHash, UserFeedback model)
        {
            _tokenHash = tokenHash;
            _model = model;
            RussianName = "Изменение отзыва";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _feedback = Context.UserFeedbacks.FirstOrDefault(x => x.Id == _model.Id && !x.Deleted);
            if (_feedback == null)
                Errors.Add("Id", "Выбранный отзыв не найден");
            else
            {
                _feedback.Username = _model.Username;
                _feedback.Description = _model.Description;
                _feedback.IsModerated = _model.IsModerated;
                Context.SaveChanges();
            }
        }
    }
}