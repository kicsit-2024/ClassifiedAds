using ClassifiedAds.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedAds.Controllers
{
    public class TestController : Controller
    {
        public IActionResult UniqueTokens()
        {
            List<string> tokens = new List<string>();   
            DateTime startTime = DateTime.Now;
            for (int i = 0; i<10000; i++)
            {
                tokens.Add(CoreHelper.GetUniqueToken());
            }

            var endTime = DateTime.Now;


            return Json(new { Total = tokens.Count, Unique = tokens.Distinct().Count(), Samples = tokens.Take(10), Time = (endTime - startTime).Milliseconds });
        }
    }
}
