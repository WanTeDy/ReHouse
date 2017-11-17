using System;
using System.Linq;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.AdminFeedbacks
{
    public class DeleteAdminFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _feedbacksId { get; set; }

        public DeleteAdminFeedbackOperation(string tokenHash, int[] feedbacksId)
        {
            _feedbacksId = feedbacksId;
            _tokenHash = tokenHash;
            RussianName = "Удаление админского отзыва";
        }

        protected override void InTransaction()
        {
            //if (_feedbacksId != null && _feedbacksId.Length > 0)
            //{
            //    foreach (var feedbackId in _feedbacksId)
            //    {
            //        var feedback = Context.AdminFeedbacks.FirstOrDefault(x => x.Id == feedbackId && !x.Deleted);
            //        if (feedback != null)
            //            feedback.Deleted = true;
            //    }
            //    Context.SaveChanges();
            //}
        }
    }
}