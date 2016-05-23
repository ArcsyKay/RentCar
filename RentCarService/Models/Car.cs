namespace RentCarService.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int ProdYear { get; set; }
        public string CarTransmission { get; set; }
        public string Fuel { get; set; }
        public string PictureLink { get; set; }
        public float Price { get; set; }
    }
}