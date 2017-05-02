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
        private Int32 _feedbackId { get; set; }

        public DeleteFeedbackOperation(string tokenHash, int feedbackId)
        {
            _tokenHash = tokenHash;
            _feedbackId = feedbackId;
            RussianName = "Удаление отзыва";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var feedback = Context.UserFeedbacks.FirstOrDefault(x => x.Id == _feedbackId && !x.Deleted);
            if (feedback == null)
                Errors.Add("Id", "Выбранный отзыв не найден.");
            else
            {
                feedback.Deleted = true;
                Context.SaveChanges();
            }
        }
    }
}