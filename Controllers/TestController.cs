using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Calculate([Bind] TestClass item)
        {
            ModelState.Clear();
            double discountamount = 0;
            item.qtykecil = item.UOM * item.quantity;

            var splitdisc = item.Discount.Split('+');
            foreach (var disc in splitdisc)
            {
                if (disc.Contains('%'))
                {
                    var discnum = Convert.ToInt32(disc.Remove(disc.Length-1));
                    discountamount += (item.quantity * item.UnitPrice) * discnum / 100;
                }
                else
                {
                    discountamount += Convert.ToInt32(disc);
                }
            }
            item.DiscAmt = discountamount;
            item.Total = (item.quantity * item.UnitPrice) - discountamount;

            return View("~/Views/Test/Index.cshtml", item);
        }
    }
}
