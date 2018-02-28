using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Features.FeatureA
{
    [Route("api/[controller]")]
    public class WidgetsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Widget> Get()
        {
            return new Widget[]
                   {
                       new Widget("WidgetA", 100),
                       new Widget("WidgetB", 250)
                   };
        }
    }

    public class Widget
    {
        public string Name { get; }
        public decimal Price { get; }

        public Widget(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
