using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

public enum SettlementsTypes
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

public static class SettlementTypeExtention // Блок расширений для значений ENUM
{
    extension(SettlementsTypes type)
    {
        public Boolean CanHaveHistoricalValue =>
            type switch
            {
                SettlementsTypes.City => true,
                SettlementsTypes.Selo => true,
                SettlementsTypes.Derevnya => false,
                SettlementsTypes.PGT => true,
            };

        public Double ScorePoints =>
           type switch
           {
               SettlementsTypes.City => 1.3,
               SettlementsTypes.Selo => 0.5,
               SettlementsTypes.Derevnya => 0.8,
               SettlementsTypes.PGT => 1.0,
           };

    }
}