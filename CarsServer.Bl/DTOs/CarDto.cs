using System;

namespace CarsServer.Bl.DTOs
{
    public class CarDto : CarBase
    {
        public Guid Id { get; set; }  
        
        public string Path { get; set; }
    }
}
