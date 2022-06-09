using System;

namespace CarsServer.DA.Model
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Company { get; set; }

        public string Model { get; set; }

        public string FileName { get; set; }
    }
}
