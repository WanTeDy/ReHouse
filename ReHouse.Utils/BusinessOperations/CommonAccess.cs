using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Windows.Forms;
using ITfamily.Utils.Brain.Facade;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.DataBaseForLog;
using ITfamily.Utils.Except;
using Newtonsoft.Json;

namespace ITfamily.Utils.BusinessOperations
{
    public class CommonAccess
    {
        public static Contractor CheckContractorRoleAuthority(DbItFamily context, string tokenHash, string nameOperation, string russianNameOperation)
        {
            var contractor = context.Contractors.Include("Role").FirstOrDefault(x=>x.TokenHash == tokenHash && !x.Deleted && x.IsActive);
            if (contractor == null)
                throw new ObjectNotFoundException("Incorrect TokenHash");
            //UpdateLastActivityForContractor(contractor, context);
            var role = context.RoleSet.Include("Authorities").FirstOrDefault(x => !x.Deleted && x.Id == contractor.RoleId);
            if (role == null || role.Authorities == null || role.Authorities.Count <= 0)
                throw new ActionNotAllowedException("Недостаточно прав доступа на выполнение операции: " + russianNameOperation);
            var authority = role.Authorities.FirstOrDefault(x => x.NameBusinessOperation == nameOperation);
            if(authority == null)
                throw new ActionNotAllowedException("Недостаточно прав доступа на выполнение операции: " + russianNameOperation);
            return contractor;
        }
        public static bool CheckContractorRoleAuthorityBool(DbItFamily context, string tokenHash, string nameOperation)
        {
            var contractor = context.Contractors.FirstOrDefault(x => x.TokenHash == tokenHash && !x.Deleted && x.IsActive);
            if (contractor == null) return false;
            //UpdateLastActivityForContractor(contractor, context);
            var role = context.RoleSet.Include("Authorities").FirstOrDefault(x => !x.Deleted && x.Id == contractor.RoleId);
            if (role == null || role.Authorities == null || role.Authorities.Count <= 0) return false;
            var authority = role.Authorities.FirstOrDefault(x => x.NameBusinessOperation == nameOperation);
            return authority != null;
        }
        /// <summary>
        /// обновление даты последнего взаимодействия с сервисом
        /// </summary>

       /* private static bool UpdateLastActivityForContractor(Contractor contractor, DbItFamily context)
        {
            
            var orderQueue = context.OrderQueues.FirstOrDefault(x => x.ContractorId == contractor.Id && x.StartWork >= DateTime.Today);
            if (orderQueue == null) return false;
            orderQueue.LastActivity = DateTime.Now;
            context.Entry(orderQueue).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return true;
        } */

        public static void CreateDitectoryIfNotExist()
        {
            var di = new DirectoryInfo(ConstV.PathCarousel);
            if (!di.Exists)
                di.Create();
            di = new DirectoryInfo(ConstV.PathStocks);
            if (!di.Exists)
                di.Create();
        }
        public static void ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                var file = new FileInfo(fileName);
                if (file.Exists)
                {
                    File.SetAttributes(fileName, FileAttributes.Normal);
                    file.Delete();
                }
                // Open file for reading
                var fileStream =
                   new System.IO.FileStream(fileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                fileStream.Write(byteArray, 0, byteArray.Length);

                // close file stream
                fileStream.Close();
            }
            catch (Exception exception)
            {
                // Error
                Logg.Error(JsonConvert.SerializeObject(exception), "Exception caught in process: " + exception.Message, State.Error);
                throw new ActionNotAllowedException("Ошибка при записи файла. Message: " + exception.Message);
            }
        }

        public static DataBase.Currencies.Currency GetOurCourseCurrencies(DbItFamily context)
        {

            var colCur = context.Currencies.Where(x => x.EnumBelongsType == BelongsType.OurCource && x.Name == ConstV.CourseCash && !x.Deleted);
            if (!colCur.Any()) return null;
            var maxDate = colCur.Max(x => x.DateTime);
            return colCur.FirstOrDefault(x => x.DateTime == maxDate);
        }
        public static List<CarouselModel> GetCarouselModels(DbItFamily context)
        {
            var carousel = context.DinamicDatas.Where(x => !x.Deleted && x.TypeData == TypeData.Carousel).ToList();
            if (carousel.Any())
                return carousel.Select(x => new CarouselModel
                {
                    Id = x.Id,
                    UrlPicture = x.UrlPicture,
                    FirstString = x.FirstString,
                    SecondString = x.SecondString,
                    ThirdString = x.ThirdString,
                    UrlHref = x.UrlHref
                }).ToList();
            return null;
        } 

        public static BrainProductFullInfo GetProductFromBrain(string providerLogin1, string providerPassword1, int productId)
        {
            var auth = GetSidToken(providerLogin1, providerPassword1);
            var res = BrainCommonFacade.GetProduct(productId, auth).Result;
            if (res == null || res.result == null)
                return null;
            return res.result;
        }

        public static string GetSidToken(string providerLogin1, string providerPassword1)
        {
            var auth = AuthBrainFacade.Auth(providerLogin1, providerPassword1).Result;
            if (auth == null || auth.status != 1)
                return null;
            return auth.result;
        }
        public static List<BrainProductFullInfo> GetProductsFromBrain(string providerLogin1, string providerPassword1, List<int> productsId)
        {
            try
            {
                if (String.IsNullOrEmpty(providerLogin1) || String.IsNullOrEmpty(providerPassword1))
                    return null;
                var auth = GetSidToken(providerLogin1, providerPassword1);
                var result = new List<BrainProductFullInfo>();
                foreach (var i in productsId)
                {
                    var res = BrainCommonFacade.GetProduct(i, auth).Result;
                    if (res == null || res.result == null || res.status != 1)
                    {
                        auth = GetSidToken(providerLogin1, providerPassword1);
                        res = BrainCommonFacade.GetProduct(i, auth).Result;
                        if (res == null || res.result == null || res.status != 1)
                            continue;
                    }
                    result.Add(res.result);
                }
                return result;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace, "ITFamily");
                Thread.Sleep(60000);
                return GetProductsFromBrain(providerLogin1, providerPassword1, productsId);
            }
        }

        public static Decimal GetOurCourseCash(DbItFamily context)
        {
            Decimal courseCash = 0;
            var curs = context.Currencies.Where(x => x.Name == ConstV.CourseCash && !x.Deleted && x.EnumBelongsType == BelongsType.OurCource);
            if (curs.Any())
            {
                var c = curs.Max(x=>x.DateTime);
                var course = curs.FirstOrDefault(x => x.DateTime == c);
                if(course!=null)
                courseCash = course.Value;
            }
            return courseCash;
        }
    }
}