using Xamarin.Forms;

namespace Formula1.Models
{
    public record ConstructorModel
    {
        public string ConstructorId { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public double Points { get; set; }
        public int Wins { get; set; }
        public ConstructorImageModel Image { get; set; }
        public string Color { get; set; }
    }
}
