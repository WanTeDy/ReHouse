using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Feedback;

namespace ReHouse.Utils.BusinessOperations.AdminFeedbacks
{
    public class LoadAdminFeedbacksOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public List<AdminFeedback> _feedbacks { get; set; }

        public LoadAdminFeedbacksOperation(string tokenHash, int page, int count)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            RussianName = "Загрузка списка пользователей";
        }

        protected override void InTransaction()
        {
            _feedbacks = Context.AdminFeedbacks
                        .Where(x => !x.Deleted)
                        .OrderByDescending(x => x.CreationDate)
                        .Skip((_page - 1) * _count).Take(_count).ToList();
        }
    }
}