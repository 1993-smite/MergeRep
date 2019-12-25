using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ViewTestProject.Controllers
{
    public class Point
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public static class PointFactory
    {
        public static Point CreatePoint(int index)
        {
            return new Point
            {
                Id = index,
                Name = $"Name №{index}",
                Title = $"Title №{index}"
            };
        }

        public static List<Point> GetPoints(Filter filter)
        {
            var points = new List<Point>();
            int initRoomId = filter.Level * 100;
            for (int i = initRoomId; i < initRoomId + 50; i++)
            {
                points.Add(PointFactory.CreatePoint(i));
            }
            return points;
        }
    }

    public class Filter
    {
        public string Building { get; set; }
        public int Level { get; set; }

        public Filter(string building, int level)
        {
            Building = building;
            Level = level;
        }
        public Filter()
        {
            Building = "AK-2";
            Level = 1;
        }
    }

    public static class Extension
    {
        public static string ObjectToJSON(this Filter filter)
        {
            return JsonConvert.SerializeObject(filter);
        }

        public static T JSONToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public class SVGController : Controller
    {
        public static readonly string filterKey = "filter";
        public IActionResult Index()
        {
            var filter = HttpContext.Session
                .GetString(filterKey)?.JSONToObject<Filter>() 
                ?? new Filter();
            ViewData[filterKey] = filter;
            return View(PointFactory.GetPoints(filter));
        }

        public IActionResult GetPoints(string building, int level)
        {
            HttpContext.Session.SetString(filterKey,new Filter(building,level).ObjectToJSON());
            var points = new List<Point>();
            for (int i = level*100; i < level * 100 + 50; i++)
            {
                points.Add(PointFactory.CreatePoint(i));
            }
            return Json(points);
        }
    }
}