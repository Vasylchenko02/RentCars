namespace RentCarsApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string AdditionalInfo { get; set; }
        public Car Car { get; set; }
    }
}
