using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadPicturesUrlsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<String> ImageUrls { get; set; }
        private Int32 ProductId { get; set; }

        public LoadPicturesUrlsOperation(string tokenHash, int productId)
        {
            TokenHash = tokenHash;
            ProductId = productId;
        }

        protected override void InTransaction()
        {
            ImageUrls = new List<string>();
            var contr = Context.Contractors.Include("Role").FirstOrDefault(x => x.IsActive && !x.Deleted && x.TokenHash == TokenHash);
            if (contr == null)
                throw new ObjectNotFoundException("Данный токен не найден, попробуйте выйти и зайти еще раз в приложение.");
            if (contr.Role.Name == ConstV.RoleAdministrator || contr.Role.Name == ConstV.RoleManager)
            {
                var prod = Context.Products.Include("PathImageses").FirstOrDefault(x => x.productID == ProductId);
                if (prod != null)
                {
                    if (!String.IsNullOrEmpty(prod.MainImage))
                        ImageUrls.Add(prod.MainImage);
                    if (prod.PathImageses != null && prod.PathImageses.Any())
                    {
                        foreach (var pathImagese in prod.PathImageses)
                        {
                            ImageUrls.Add(pathImagese.BigImage);
                        }
                    }
                }
            }
            else
                throw new ActionNotAllowedException("Недостаточно полномочий для выполнения данной операции.");

        }
    }
}