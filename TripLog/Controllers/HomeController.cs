using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TripLog.Models;

namespace TripLog.Controllers
{
    public class HomeController : Controller
    {
        private TripLogContext context { get; set; }
        public HomeController(TripLogContext ctx) => context = ctx;

        public ViewResult Index()
        {
            var trips = context.Trips.OrderBy(t => t.StartDate).ToList();
            return View(trips);
        }

    }
}
