using System;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.Helpers;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.DataBase.AdvertParams;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Builders
{
    public class AddBuilderOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public Builder _model { get; set; }
        public Builder _builder { get; set; }

        public AddBuilderOperation(string tokenHash, Builder builder)
        {
            _tokenHash = tokenHash;
            _model = builder;
            RussianName = "Создание застройщика";
        }

        protected override void InTransaction()
        {
            new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_model == null)
                return;

            var builder = Context.Builders.FirstOrDefault(x => x.Name.ToLower() == _model.Name.ToLower());
            if (builder != null)
                Errors.Add("Name", "Такой застройщик уже существует!");
            else
            {
                _builder = new Builder();
                _builder.Name = _model.Name;
                _builder.Url = _model.Url;
                if (_model.Phones != null && _model.Phones.Count > 0)
                {
                    _model.Phones = _model.Phones.Where(x => x.Id > 0 || !String.IsNullOrWhiteSpace(x.TelePhone)).ToList();
                    _model.Phones.ForEach(x => x.TelePhone = x.TelePhone != null ? x.TelePhone.Trim() : "");
                    if (_model.Phones.Count > 0)
                    {
                        foreach (var phone in _model.Phones)
                        {
                            Context.Phones.Add(phone);
                        }
                        _builder.Phones = _model.Phones;
                    }
                }
                Context.Builders.Add(_builder);
                Context.SaveChanges();
            }
        }
    }
}