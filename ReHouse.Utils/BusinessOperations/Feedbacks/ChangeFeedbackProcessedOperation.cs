using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Feedbacks
{
    public class ChangeFeedbackProcessedOperation : BaseOperation
    {
        public Boolean Processed { get; set; }
        public Int32 SelId { get; set; }
        public String TokenHash { get; set; }

        public ChangeFeedbackProcessedOperation(bool processed, int selId, string tokenHash)
        {
            Processed = processed;
            SelId = selId;
            TokenHash = tokenHash;
            RussianName = "Изменение состояния сообщений обратной связи";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var feedback = Context.Feedbacks.FirstOrDefault(x => x.Id == SelId);
            if(feedback == null)
                throw new ObjectNotFoundException("Feedback не найден");
            feedback.Processed = Processed;
            Context.SaveChanges();
        }
    }
}