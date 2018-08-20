using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedPosition = RouteData?.Values["position"];
            return View(Enum.GetValues(typeof(Position)));
        }
    }
}
