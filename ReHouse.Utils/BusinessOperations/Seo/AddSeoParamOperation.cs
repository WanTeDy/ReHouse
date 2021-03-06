﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class AddSeoParamOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public SeoParam _seoParam { get; set; }

        public AddSeoParamOperation(string tokenHash, SeoParam seoParam)
        {
            _tokenHash = tokenHash;
            _seoParam = seoParam;
            RussianName = "Добавление сео параметров";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _seoParam.ActionName = _seoParam.ActionName.ToLower();
            _seoParam.ControllerName = _seoParam.ControllerName.ToLower();
            _seoParam.FullUrl = _seoParam.FullUrl.ToLower();
            _seoParam.Id = 0;
            Context.SeoParams.Add(_seoParam);
            Context.SaveChanges();
        }
    }
}