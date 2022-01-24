using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Core.Enums
{
    public enum Aglomeration
    {
        [Display(Name = "Warszawska")]
        [Description("Warszawska")]
        Warsaw,
        [Display(Name = "Poznańska")]
        [Description("Poznańska")]
        Poznan,
        [Display(Name = "Trójmiejska")]
        [Description("Trójmiejska")]
        Tricity,
        [Display(Name = "Krakowska")]
        [Description("Krakowska")]
        Cracow,
        [Display(Name = "Wrocławska")]
        [Description("Wrocławska")]
        Wroclaw,
        [Display(Name = "Białostocka")]
        [Description("Białostocka")]
        Bialystok,
        [Display(Name = "Rzeszowska")]
        [Description("Rzeszowska")]
        Rzeszów,
        [Display(Name = "Kielecka")]
        [Description("Kielecka")]
        Kielce,
        [Display(Name = "Śląska")]
        [Description("Śląska")]
        Silesia,
        [Display(Name = "Kujawska")]
        [Description("Kujawska")]
        Kuyavia,
    }
}
