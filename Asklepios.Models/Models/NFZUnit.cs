using Asklepios.Core.Enums;

namespace Asklepios.Core.Models
{
    public class NFZUnit
    {
        public long Id { get; set; }
        public string Description { get; set; }
        
        public string Code { get; set; }
        public VoivodeshipType Voivodeship { get; set; }
    }
}
