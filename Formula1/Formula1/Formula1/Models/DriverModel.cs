namespace Formula1.Models
{
    public record DriverModel
    {
        public string DriverId { get; set; }
        public DriverImageModel Image { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Nationality { get; set; }
        public int PermanentNumber { get; set; }
        public string Code { get; set; }
        public string DateOfBirth { get; set; }
    }
}
