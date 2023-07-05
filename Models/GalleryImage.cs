namespace RentCarsApp.Models
{
    public class GalleryImage 
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Car Car { get; set; }
    }
}
