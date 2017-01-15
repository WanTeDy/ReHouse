using System;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.BusinessOperations.Feedbacks
{
    public class AddFeedbackOperation : BaseOperation
    {
        public String Email { get; set; }
        public String SendName { get; set; }
        public String Message { get; set; }

        public AddFeedbackOperation(string email, string sendName, string message)
        {
            Email = email;
            SendName = sendName;
            Message = message;
        }

        protected override void InTransaction()
        {
            var feedback = new Feedback
            {
                Email = Email,
                Name = SendName,
                Message = Message,
                SendDate = DateTime.Now,
                Processed = false
            };
            Context.Feedbacks.Add(feedback);
            Context.SaveChanges();
        }
    }
}