using System.Linq;
using System.Text;
using ITfamily.Utils.DataBase;

namespace ITfamily.Utils.BusinessOperations.OrdersComesOp
{
    public class FillOrderCitiesOperation : BaseOperation
    {
        protected override void InTransaction()
        {
            System.Net.WebClient web = new System.Net.WebClient {Encoding = UTF8Encoding.UTF8};

            string str = web.DownloadString("http://novaposhta.ua/ru/timetable");
            //var str = File.ReadAllText(@"G:\CurrentDocuments\Desktop\r1.html", Encoding.UTF8);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(str);

            var tds = from li in doc.DocumentNode.Descendants("li")
                      where li.Attributes.Contains("data-value")
                      select li;
            foreach (var htmlNode in tds)
            {
                var orderCity = htmlNode.Attributes["data-value"].Value;
                var city = Context.OrderCities.FirstOrDefault(x => x.Name == orderCity);
                if (city != null) continue;
                var c = new OrderCities {Name = orderCity};
                Context.OrderCities.Add(c);
            }
            Context.SaveChanges();

        }
    }
}