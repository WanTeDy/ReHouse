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
        private Boolean _isModerated { get; set; }
        private Int32 _feedbackId { get; set; }
        public UserFeedback _feedback { get; set; }

        public UpdateFeedbackOperation(string tokenHash, int feedbackId, bool isModerated)
        {
            _tokenHash = tokenHash;
            _feedbackId = feedbackId;
            _isModerated = isModerated;
            RussianName = "Изменение отзыва";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _feedback = Context.UserFeedbacks.FirstOrDefault(x => x.Id == _feedbackId && !x.Deleted);
            if (_feedback == null)
                Errors.Add("Id", "Выбранный отзыв не найден");
            else
            {
                _feedback.IsModerated = _isModerated;
                Context.SaveChanges();
            }
        }
    }
}