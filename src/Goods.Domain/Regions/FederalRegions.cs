using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum FederalRegions
{
    [Display(Name = "Город")]
    City = 1,

    [Display(Name = "Село")]
    Selo = 2,

    [Display(Name = "Деревня")]
    Derevnya = 3,

    [Display(Name = "ПГТ")]
    PGT = 4
}