using ReHouse.Utils.BusinessOperations;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReHouse.Utils.BusinessOperations.Emails
{
    public class SendEmailOperation : BaseOperation
    {
        private UserEmailMessage _message { get; set; }
        private int _advertId { get; set; }
        private AdvertsType _type { get; set; }

        public SendEmailOperation(UserEmailMessage message, int advertId = 0, AdvertsType type = 0)
        {
            _message = message;
            _advertId = advertId;
            _type = type;
            RussianName = "Отправка почты по запросы пользователя";
        }

        protected override void InTransaction()
        {
            UserEmailMessage mail = new UserEmailMessage
            {
                Date = DateTime.Now,
                Phone = _message.Phone,
                Message = _message.Message,
                Username = _message.Username,
                Email = _message.Email,
            };
            Context.Emails.Add(mail);
            string viewName = "";
            //string email = "wanted.kaiser228@gmail.com";
            string email = "rehouse.odessa.stars@gmail.com";
            AdvertEmailModel model = null;
            if (_advertId == 0)
            {
                viewName = "CommonUserMailView";
                //email = "rehouse.odessa.stars@gmail.com";
                model = new AdvertEmailModel
                {
                    UserName = _message.Username,
                    UserPhone = _message.Phone,
                    UserMessage = _message.Message,
                    Date = DateTime.Now,
                };
            }
            else
            {
                viewName = "AdvertMailView";
                string url = ConstV.Url;
                string title = "";
                string adress = "";
                string price = "";
                
                if (_type == AdvertsType.NewBuilding)
                {
                    //email = "novostroyrehouseodessa@gmail.com";

                    var advert = Context.NewBuildings.FirstOrDefault(x => !x.Deleted && x.Id == _advertId);
                    if(advert != null)
                    {
                        url += "newbuilding/detail/" + _advertId;
                        title = advert.Name;
                        adress = advert.Adress;
                        price = "от " + advert.Price + " грн/м2";
                    }
                }
                else
                {
                    var advert = Context.Adverts.FirstOrDefault(x => !x.Deleted && x.Id == _advertId);
                    if (advert != null)
                    {
                        //email = advert.User.Email;
                        title = advert.TitleName;
                        adress = advert.Street;

                        if(advert.Type == AdvertsType.Rent)
                        {
                            price = advert.Price + " грн/месяц";
                            url += "rent/detail/" + _advertId;
                        }
                        else
                        {
                            price = advert.Price + " $";
                            url += "sale/detail/" + _advertId;
                        }
                    }
                }

                model = new AdvertEmailModel
                {
                    UserName = _message.Username,
                    UserPhone = _message.Phone,
                    UserMessage = _message.Message,
                    Date = DateTime.Now,
                    Title = title,
                    Url = url,
                    Adress = adress,
                    Price = price,
                    Type = _type,
                };
            }
            Send(viewName, email, model);
        }

        private void Send(string viewName, string email, AdvertEmailModel model)
        {
            SmtpEmailSender mailSender = new SmtpEmailSender("mail.rehouse-realty.com.ua", "postmaster@rehouse-realty.com.ua", "Rehouse123456!");
            var body = SmtpEmailSender.GetHtmlRazor(model, SmtpEmailSender.FormatUrl(viewName));
            mailSender.Send(email, "Новое сообщение", body);
        }
    }
}