using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Feedbacks
{
    public class DeleteFeedbackOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _feedbacksId { get; set; }

        public DeleteFeedbackOperation(string tokenHash, int[] feedbacksId)
        {
            _tokenHash = tokenHash;
            _feedbacksId = feedbacksId;
            RussianName = "Удаление отзыва";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_feedbacksId != null && _feedbacksId.Length > 0)
            {
                foreach (var feedbackId in _feedbacksId)
                {
                    var feedback = Context.UserFeedbacks.FirstOrDefault(x => x.Id == feedbackId && !x.Deleted);
                    if(feedback != null)
                        feedback.Deleted = true;
                }
                Context.SaveChanges();
            }
        }
    }
}