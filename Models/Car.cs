using System.Collections.Generic;

namespace RentCarsApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string NameProducer { get; set; }
        public string NameModel { get; set; }
        public string Description { get; set; }
        public ushort ProductionYear { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
        public TransmissionType Transmission { get; set; }
        public FuelType Fuel { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
    public enum TransmissionType
    {
        Manual,
        Automatic
    }
    public enum FuelType
    {
        Gasoline,
        Diesel,
        LPG
    }
}
