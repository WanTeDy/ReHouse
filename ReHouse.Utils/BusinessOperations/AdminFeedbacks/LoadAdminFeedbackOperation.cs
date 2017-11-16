using System;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Feedback;

namespace ReHouse.Utils.BusinessOperations.AdminFeedbacks
{
    public class LoadAdminFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _feedbackId { get; set; }
        public AdminFeedback _feedback { get; set; }

        public LoadAdminFeedbackOperation(string tokenHash, int feedbackId)
        {
            _tokenHash = tokenHash;
            _feedbackId = feedbackId;
            RussianName = "Просмотр отзыва";
        }

        protected override void InTransaction()
        {
            //var _feedback = Context.AdminFeedbacks.FirstOrDefault(x => x.Id == _feedbackId && !x.Deleted);
        }
    }
}