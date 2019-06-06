using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnterpriseClientB2C.Models;
using Microsoft.AspNetCore.Authorization;
using EnterpriseClientB2C.Services;

namespace EnterpriseClientB2C.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsAPIClient apiClient;
        private readonly IReceipeClient receipeAPIClient;
        public HomeController(INewsAPIClient apiClient, IReceipeClient receipeAPIClient)
        {
            this.apiClient = apiClient;
            this.receipeAPIClient = receipeAPIClient;
        }
        public IActionResult Index()
        {
            return View();
        }
       [Authorize]
        public async Task<IActionResult> About()
        {
            var news = await this.apiClient.GetValuesforNews();
          //  var receipe = await this.receipeAPIClient.GetValuesforNews();
            var viewModel = new ViewModel
            {
                NewsArr = news,
         //       ReceipeArr = receipe

            };
            return View(viewModel);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
