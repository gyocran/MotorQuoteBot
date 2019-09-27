using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteBot.Models
{
    public class VehiclePremium
    {
        public string Currency { get; set; }
        public string CurrencyDesc { get; set; }

        [System.ComponentModel.DefaultValue(1)]
        public decimal CurrencyValue { get; set; } = 1;
        public string CoverType { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationNumberType { get; set; }
        public decimal? ValueOfVehicle { get; set; }
        public string Usage { get; set; }
        public string YearOfManufacture { get; set; }
        public string CubicCapacity { get; set; }
        public decimal tppdl { get; set; }
        public int SeatingCapacity { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int NoClaimDiscountPeriod { get; set; }
        public string NoClaimDiscountPeriodDesc { get; set; }

        public decimal FleetDiscount { get; set; }
        //public decimal totalPremium { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? Premium { get; set; }
        // public decimal TotalPremium { get; set; }

        public decimal ExtraCharges { get; set; }
        public decimal NoClaimDiscount { get; set; }
        public int RowNumber { get; set; }
        public string PayFrequency { get; set; }
        public string DiscountCoupon { get; set; }
        public int AppId { get; set; }
        public int CouponId { get; set; }
        public decimal TPPLCharge { get; set; }
        public decimal Own_Damage { get; set; }
        public string DiscountPct { get; set; }
        public object AdditionalPersonalAccident { get; internal set; }
        public string CoverTypeDesc { get; set; }

        // This is to turn loading of Age, Seating, Cubic capcity for premium calcluation. Value will be picked from Database
        public bool EnableLoading { get; set; } = false;
        public decimal additionalcharge { get; set; } = -1.0m;
        public bool EnablethirdpartNCD { get; set; } = true;
        public decimal T1_Basic { get; set; }
        public decimal AdditionalPeril { get; set; }
        public decimal ECOWASCharge { get; set; }
        public decimal NCD { get; set; }
        public decimal PersonalAccident { get; set; }

        //private static decimal additionalPeril = 5.00m;
        //private static decimal ECOWAS = 5.00m;
        //private static decimal PA = 20.00m;
        //private static decimal NIC = 20.00m;
        //private static decimal adminFees = 0.00m;
        //private static decimal stickerfee = 0.00m;
        //private static decimal browncard = 5.00m;
        //private decimal Comprehensive = 0.0m;
        //private decimal ThirdPartyFireTheft = 0.0m;
        //private decimal SeatingCapacityRate = 0.0m;
        //private decimal tppdlRate = 0.02m;
        //private string usegroup = "C";      //this can be changed later to a better option: C-commercial, P - private, M - motor cycle
        //private int maxseating = 5;
        //private int specialNCD = 0;
        //private decimal[] NCD_array = new decimal[5];


    }
}
