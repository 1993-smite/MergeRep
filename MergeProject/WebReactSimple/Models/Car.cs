using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReactSimple.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class CarFactory
    {
        public static Car CreateCar(int index)
        {
            return new Car
            {
                Id = index,
                Name = $"Car {index}"
            };
        }
    }
}
