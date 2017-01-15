using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.BusinessOperations.Feedbacks
{
    public class LoadFeedbacksOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public LoadFeedbacksOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Загрузка сообщений обратной связи";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var feedbacks = Context.Feedbacks.ToList();
            Feedbacks = new List<Feedback>();
            foreach (var feedback in feedbacks)
            {
                Feedbacks.Add(new Feedback
                {
                    Name = feedback.Name,
                    Email = feedback.Email,
                    Id = feedback.Id,
                    Message = feedback.Message,
                    Processed = feedback.Processed,
                    SendDate = feedback.SendDate
                });
            }
        }
    }
}