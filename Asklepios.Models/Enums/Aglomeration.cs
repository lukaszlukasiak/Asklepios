using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Core.Enums
{
    public enum Aglomeration
    {
        [Display(Name = "Warszawska")]
        [Description("Aglomeracja warszawska")]
        Warsaw,
        [Display(Name = "Poznańska")]
        [Description("Aglomeracja poznańska")]
        Poznan,
        [Display(Name = "Trójmiejska")]
        [Description("Aglomeracja trójmiejska")]
        Tricity,
        [Display(Name = "Krakowska")]
        [Description("Aglomeracja krakowska")]
        Cracow,
        [Display(Name = "Wrocławska")]
        [Description("Aglomeracja wrocławska")]
        Wroclaw,
        [Display(Name = "Białostocka")]
        [Description("Aglomeracja białostocka")]
        Bialystok,
        [Display(Name = "Rzeszowska")]
        [Description("Aglomeracja rzeszowska")]
        Rzeszów,
        [Display(Name = "Kielecka")]
        [Description("Aglomeracja kielecka")]
        Kielce,
        [Display(Name = "Śląska")]
        [Description("Aglomeracja śląska")]
        Silesia,
        [Display(Name = "Kujawska")]
        [Description("Aglomeracja kujawska")]
        Kuyavia,
    }
}
