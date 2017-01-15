using System;
using System.IO;
using System.Linq;
using ITfamily.Utils.BusinessOperations.UpdateData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.DataBaseForLog;
using ITfamily.Utils.Except;
using Newtonsoft.Json;

namespace ITfamily.Utils.BusinessOperations.ChangeCarousel
{
    public class AddOrUpdateCarouselOperation : BaseOperation
    {
        //public byte[] Bytes { get; set; }
        //public String NameFile { get; set; }
        //public byte[] HashMD5 { get; set; }
        //public String TokenHash { get; set; }
        //public String FirstString { get; set; }
        //public String SecondGreenString { get; set; }
        //public String ThirdString { get; set; }
        //private const String _path = "C:\\inetpub\\wwwroot\\images\\carousel";
        //
        //public AddOrUpdateCarouselOperation(byte[] bytes, string nameFile, byte[] hashMd5, string tokenHash)
        //{
        //    Bytes = bytes;
        //    NameFile = nameFile;
        //    HashMD5 = hashMd5;
        //    TokenHash = tokenHash;
        //}
        //
        //protected override void InTransaction()
        //{
        //    var emp = Context.Contractors.Include("Role").FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
        //    if (emp == null)
        //        throw new ObjectNotFoundException("Incorrect TokenHash");
        //    if (emp.Role != null && emp.Role.Name != ConstV.RoleAdministrator)
        //        throw new ActionNotAllowedException("Недостаточно прав доступа!");
        //
        //    var fileName = _path + "\\" + NameFile;
        //    var file = new FileInfo(fileName);
        //    if (file.Exists)
        //    {
        //        var md51 = HashHelper.GetMd5FromFile(fileName);
        //        if (md51 != null && HashHelper.ByteArrayCompareWithSimplest(HashMD5, md51))
        //        {
        //            ByteArrayToFile(fileName, HashMD5);
        //        }
        //        else
        //        {
        //            ByteArrayToFile(fileName, HashMD5);
        //        }
        //    }
        //    
        //}
        //
        //public bool ByteArrayToFile(string fileName, byte[] byteArray)
        //{
        //    try
        //    {
        //        var file = new FileInfo(fileName);
        //        if (file.Exists)
        //        {
        //            File.SetAttributes(fileName, FileAttributes.Normal);
        //            file.Delete();
        //        }
        //        // Open file for reading
        //        var fileStream =
        //           new System.IO.FileStream(fileName, System.IO.FileMode.Create,
        //                                    System.IO.FileAccess.Write);
        //        // Writes a block of bytes to this stream using data from
        //        // a byte array.
        //        fileStream.Write(byteArray, 0, byteArray.Length);
        //
        //        // close file stream
        //        fileStream.Close();
        //
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        // Error
        //        Logg.Error(JsonConvert.SerializeObject(exception), "Exception caught in process: " + exception.Message, State.Error);
        //        throw new ActionNotAllowedException("Ошибка при записи файла. Message: " + exception.Message);
        //    }
        //
        //    // error occured, return false
        //    return false;
        //}
    }
}