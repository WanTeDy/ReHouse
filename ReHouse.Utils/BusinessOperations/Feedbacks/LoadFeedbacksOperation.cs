﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.Feedback;

namespace ReHouse.Utils.BusinessOperations.Feedbacks
{
    public class LoadFeedbacksOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Boolean _isAdmin { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public List<UserFeedback> _userFeedbacks { get; set; }

        public LoadFeedbacksOperation(string tokenHash, int page, int count, bool isAdmin = false)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            _isAdmin = isAdmin;
            RussianName = "Получение всех отзывов с фильтром по свежести";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_isAdmin)
            {
                _userFeedbacks = Context.UserFeedbacks.Where(x => !x.Deleted).OrderByDescending(x => x.Date).ToList();
            }
            else
            {
                _userFeedbacks = Context.UserFeedbacks.Where(x => !x.Deleted && x.IsModerated).OrderByDescending(x => x.Date)
                .Skip((_page - 1) * _count).Take(_count).ToList();
            }            
        }
    }
}