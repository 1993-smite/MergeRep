using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ViewTestProject.Controllers
{
    public class Point
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public static class PointFactory
    {
        public static Point CreatePoint(int index)
        {
            return new Point
            {
                Id = index,
                Title = $"Title №{index}"
            };
        }
    }

    public class SVGController : Controller
    {
        

        public IActionResult Index()
        {
            var points = new List<Point>();
            for(int i = 0; i < 20; i++)
            {
                points.Add(PointFactory.CreatePoint(i));
            }

            return View(points);
        }
    }
}