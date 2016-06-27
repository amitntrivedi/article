using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linq.Controllers
{
    public class HomeController : Controller
    {
        public class Client
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class Order
        {
            public int Cid { get; set; }
            public int Oid { get; set; }
            public float amount { get; set; }
        }

        public class ClientOrder
        {
            public string ClientName { get; set; }
            public int TotalNoOfOrder { get; set; }
            
        }
      
        public ActionResult Index()
        {

            List<Client> clientList = new List<Client>();
            List<Order> orderList = new List<Order>();

            for (var i = 0; i < 5; i++)
            {
                clientList.Add(new Client { id = i, name = "client " + i.ToString() });
                orderList.Add(new Order { Cid = i, Oid = i, amount = i * 1000 });
            }

            orderList.Add(new Order { Cid=1, Oid=6, amount=3500});
            orderList.Add(new Order { Cid = 4, Oid = 7, amount = 3400 });

            var premiumClient = clientList.Where(x => x.id > 3); // using  lambda expression
            var filteredOrderList = orderList.Where(x => x.Oid > 3); // using  lambda expression

            var simpleQuery = from client in clientList
                              where client.id > 3
                              select client;


            var masterDetailQuery = from myclient in clientList
                                    join myorder in orderList
                                    on myclient.id equals myorder.Cid into ords
                                    select new ClientOrder
                                    {
                                        ClientName = myclient.name,
                                        TotalNoOfOrder= ords.Count()
                                    };

            string goodclient = string.Empty;
            foreach (var i in masterDetailQuery)
            {
                goodclient = goodclient + "   " + i.ClientName + "   " + i.TotalNoOfOrder.ToString();

            }
            ViewBag.ClientOrder = masterDetailQuery;
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}