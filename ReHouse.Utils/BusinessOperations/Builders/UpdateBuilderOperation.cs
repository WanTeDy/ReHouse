using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.News;
using System.Web;
using System.IO;
using ImageResizer;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Except;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Builders
{
    public class UpdateBuilderOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Builder _model { get; set; }
        public Builder _builder { get; set; }

        public UpdateBuilderOperation(string tokenHash, Builder builder)
        {
            _tokenHash = tokenHash;
            _model = builder;
            RussianName = "Изменение застройщика";
        }

        protected override void InTransaction()
        {
            //new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _builder = Context.Builders.FirstOrDefault(x => x.Id == _model.Id && !x.Deleted);
            if (_builder == null)
                Errors.Add("Id", "Выбранный застройщик не найден");
            else
            {
                var builder = Context.Builders.FirstOrDefault(x => x.Name.ToLower() == _model.Name.ToLower());
                if (builder != null)
                    Errors.Add("Name", "Такой застройщик уже существует!");
                else
                {
                    _builder.Name = _model.Name;
                    _builder.Url = _model.Url;
                    if (_model.Phones != null && _model.Phones.Count > 0)
                    {
                        _model.Phones = _model.Phones.Where(x => x.Id > 0 || !String.IsNullOrWhiteSpace(x.TelePhone)).ToList();
                        _model.Phones.ForEach(x => x.TelePhone = x.TelePhone != null ? x.TelePhone.Trim() : "");
                        List<Phone> newPhones = _model.Phones.Where(x => x.Id == 0).ToList();
                        List<Phone> oldPhones = _model.Phones.Where(x => x.Id > 0).ToList();
                        if (oldPhones.Count > 0)
                        {
                            foreach (var phone in oldPhones)
                            {
                                var exPhone = Context.Phones.FirstOrDefault(x => x.Id == phone.Id);
                                if (exPhone != null)
                                {
                                    if (String.IsNullOrWhiteSpace(phone.TelePhone))
                                        Context.Phones.Remove(exPhone);
                                    else
                                        exPhone.TelePhone = phone.TelePhone;
                                }
                            }
                        }
                        if (newPhones.Count > 0)
                        {
                            foreach (var phone in newPhones)
                            {
                                Context.Phones.Add(phone);
                            }
                            if (_builder.Phones != null)
                                _builder.Phones.AddRange(newPhones);
                            else
                                _builder.Phones = newPhones;
                        }
                    }
                    Context.SaveChanges();
                }
            }
        }
    }
}