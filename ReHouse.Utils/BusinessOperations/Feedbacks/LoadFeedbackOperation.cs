using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.Feedback;

namespace ReHouse.Utils.BusinessOperations.Feedbacks
{
    public class LoadFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _feedbackId { get; set; }
        public UserFeedback _feedback { get; set; }

        public LoadFeedbackOperation(string tokenHash, int feedbackId)
        {
            _tokenHash = tokenHash;
            _feedbackId = feedbackId;
            RussianName = "Получение отзыва";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _feedback = Context.UserFeedbacks.FirstOrDefault(x => !x.Deleted && x.Id == _feedbackId);
        }
    }
}