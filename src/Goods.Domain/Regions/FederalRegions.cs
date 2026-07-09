using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum FederalRegionsENUM
{
    [Display(Name = "Московская область")]
    Moscow = 1,

    [Display(Name = "Тульская область")]
    Tula = 2,

    [Display(Name = "Краснодарский край")]
    KrasnodarskiyKrai = 3,

    [Display(Name = "Ямало-Ненецкий автономный округ")]
    YamaloNenetskiyAvtonomniyOkrug = 4

}