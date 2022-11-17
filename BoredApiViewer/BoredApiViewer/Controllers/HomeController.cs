using BoredApiViewer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BoredApiViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(GetActivityViewModel());
        }

        [HttpPost]
        public IActionResult AddActivity([FromForm] ActivityInput input)
        {
            ActivityViewModel viewModel = GetActivityViewModel();
            var queryStr = GetQueryString(input);
            BoredActivity activity = new BoredActivity();
            Task task = activity.Set(queryStr);

            task.Wait();
            viewModel.activities.Add(activity);
            viewModel.selectedKeyValue = activity.key;
            SetActivityViewModel(viewModel);
            return RedirectToAction("Index");
        }

        private string GetQueryString(ActivityInput input)
        {
            var rtn = "";
            if ((input.type ?? "").Length > 0)
                rtn += $"&type={input.type}";
            if(input.participants > 0)
                rtn += $"&participants={input.participants}";
            if (input.accessibilityMin > 0 || input.accessibilityMax > 0)
                rtn += $"&minaccessibility={input.accessibilityMin}&maxaccessibility={input.accessibilityMax}";
            if (input.priceMin > 0 || input.priceMax > 0)
                rtn += $"&minprice={input.priceMin}&maxprice={input.priceMax}";
            if (rtn.Length > 0)
                rtn = rtn.Remove(0, 1);
            return rtn;
        }

        private ActivityViewModel GetActivityViewModel()
        {
            string value = HttpContext.Session.GetString("ActivityViewModel") ?? "";
            if (value.Length > 0)
                return JsonConvert.DeserializeObject<ActivityViewModel>(value);
            else
                return new ActivityViewModel() { activities = new List<BoredActivity>(), selectedKeyValue = "" };
        }

        private void SetActivityViewModel(ActivityViewModel model)
        {
            HttpContext.Session.SetString("ActivityViewModel", JsonConvert.SerializeObject(model));
        }
    }
}
