using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.Helpers
{
    public class Helper
    {
        public static bool ChekcActiveCategory(ItFamilyCategory category, int categoryId)
        {
            if (category == null) return false;
            if (category.Id == categoryId) return true;
            if (category.Categories == null) return false;
            if (category.Categories.Any(x => x.Id == categoryId)) return true;
            foreach (var brainCategory in category.Categories)
            {
                if (brainCategory.Categories != null &&
                    brainCategory.Categories.Any(x => x.Id == categoryId))
                    return true;
            }
            return false;
        }

        public static CategoryModel GetActiveCategoryModel(List<ItFamilyCategory> categories, int categoryId)
        {
            if (categories == null || categories.Count == 0) return null;
            CategoryModel catModel = null;
            foreach (var category in categories)
            {
                if (ChekcActiveCategory(category, categoryId))
                {
                    catModel = new CategoryModel
                    {
                        categoryID = category.Id,
                        IsActive = true,
                        name = category.Name,
                        parentID = category.ItFamilyParentId,
                        Categories = category.Categories.Select(x => new CategoryModel
                        {
                            IsActive = ChekcActiveCategory(x, categoryId),
                            categoryID = x.Id,
                            name = x.Name,
                            parentID = x.ItFamilyParentId,
                            Categories = x.Categories.Select(y => new CategoryModel
                            {
                                IsActive = ChekcActiveCategory(y, categoryId),
                                categoryID = y.Id,
                                name = y.Name,
                                parentID = y.ItFamilyParentId
                            }).ToList()
                        }).ToList()
                    };
                }
            }
            return catModel;
        }
        public static String ImageToBase64String(string path)
        {
            var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            var buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            // Преобразование в Base64
            var base64 = Convert.ToBase64String(buffer);
            return base64;
        }

        public static Image DeserializeToImage(string data)
        {
            var ms = new MemoryStream(Convert.FromBase64String(data));
            return Image.FromStream(ms);
        }

        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                var m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidUrl(string urlString)
        {
            Uri uri;
            return Uri.TryCreate(urlString, UriKind.Absolute, out uri)
                && (uri.Scheme == Uri.UriSchemeHttp
                 || uri.Scheme == Uri.UriSchemeHttps
                 || uri.Scheme == Uri.UriSchemeFtp
                 || uri.Scheme == Uri.UriSchemeMailto
                /*...*/);
        }
    }
}