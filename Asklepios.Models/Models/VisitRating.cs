namespace Asklepios.Core.Models
{
    public class VisitRating
    {
        public long Id { get; set; }
        public short GeneralRate { get; set; }
        public short CompetenceRate { get; set; }
        public short AtmosphereRate { get; set; }
        public string ShortDescription { get; set; }
    }
}