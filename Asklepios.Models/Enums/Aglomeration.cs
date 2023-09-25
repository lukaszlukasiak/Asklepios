using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Core.Enums
{
    public enum Aglomeration
    {
        [Display(Name = "Warszawska")]
        [Description("Aglomeracja warszawska")]
        Warsaw=1,
        [Display(Name = "Poznańska")]
        [Description("Aglomeracja poznańska")]
        Poznan=2,
        [Display(Name = "Trójmiejska")]
        [Description("Aglomeracja trójmiejska")]
        Tricity=3,
        [Display(Name = "Krakowska")]
        [Description("Aglomeracja krakowska")]
        Cracow=4,
        [Display(Name = "Wrocławska")]
        [Description("Aglomeracja wrocławska")]
        Wroclaw=5,
        [Display(Name = "Białostocka")]
        [Description("Aglomeracja białostocka")]
        Bialystok=6,
        [Display(Name = "Rzeszowska")]
        [Description("Aglomeracja rzeszowska")]
        Rzeszów=7,
        [Display(Name = "Kielecka")]
        [Description("Aglomeracja kielecka")]
        Kielce=8,
        [Display(Name = "Śląska")]
        [Description("Aglomeracja śląska")]
        Silesia=9,
        [Display(Name = "Kujawska")]
        [Description("Aglomeracja kujawska")]
        Kuyavia=10,
    }
}
