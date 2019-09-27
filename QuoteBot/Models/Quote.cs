using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteBot.Models
{
    public class Quote
    {
        public string Action { get; set; }
        public string AppType { get; set; }
        public string AppTypeDesc { get; set; }
        public bool? SendSms { get; set; }
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string PhoneNumber { get; set; }
        public long? QuoteId { get; set; }
        public long? ResultId { get; set; }

        // Travel Details
        public string Occupation { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string destinationCountryDesc { get; set; }// did not delete because may break application elsewhere
        public string DestinationCountryDesc { get; set; }
        public string[] Destinations { get; set; }
        public string[] DestinationsDesc { get; set; }
        public string Regions { get; set; }
        public string Area { get; set; }

        // Vehicle Details
        public string Usage { get; set; }
        public string UsageDesc { get; set; }
        [System.ComponentModel.DefaultValue(1)]
        public string Currency { get; set; }
        public string ValueOfVehicle { get; set; }
        public string CubicCapacity { get; set; }
        public string CubicCapacityDesc { get; set; }
        public string RegistrationNumber { get; set; }
        public string YearOfManufacture { get; set; }
        public int? SeatingCapacity { get; set; }
        public string SeatingCapacityString { get; set; }
        public int? PolicyPeriod { get; set; } //How long is policy needed 
        public int? InsuranceWithoutYearOfClaims { get; set; } // How many years of buying insurance without claims
        public string CoverageType { get; set; }
        public string CoverType { get; set; }
        public decimal? ExtraCharges { get; set; }
        public decimal? Premium { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? NoClaimDiscount { get; set; }
        public string RegistrationNumberType { get; set; }
        public int? NoClaimDiscountPeriod { get; set; }
        public string CompName { get; set; }
        public string CompEmail { get; set; }
        public string CompPhone { get; set; }
        public string VehicleType { get; set; }
        public int? NumberOfVehicles { get; set; }
        public Byte? ExcelUpload { get; set; }

        //Funeral Details
        public string FunCoverageType { get; set; } //added 'Fun' prefix because of ambiguity
        public string Optionalbenefit { get; set; }
        public int? principalAge { get; set; }
        public int? childAge { get; set; }
        public int? otherAge { get; set; }
        public int? rateType { get; set; }
        public int? spouseAge { get; set; }

        public decimal? Child { get; set; }
        public decimal? Other { get; set; }
        public decimal? Principal { get; set; }
        public decimal? Spouse { get; set; }
        public decimal? FunTotalPremium { get; set; } //added 'Fun' prefix because of ambiguity
        public string PaymentFrequency { get; set; }
        public string BurialPlan1 { get; set; }
        public string BurialPlan2 { get; set; }
        public string BurialPlan3 { get; set; }
        public string BurialPlan4 { get; set; }
        public DateTime? FunDateOfBirth { get; set; } //added 'Fun' prefix because of ambiguity
        public string PlanType { get; set; }
        public string Plantype { get; set; }
        public string Relationship1 { get; set; }
        public string Relationship2 { get; set; }
        public string Relationship3 { get; set; }
        public string Triptype { get; set; }
        public decimal? TotalPremium { get; set; }
        public string Under19 { get; set; }
        public string Btn19_65 { get; set; }
        public string Btn65_75 { get; set; }
        public string Btn76_80 { get; set; }
        public string Above_81 { get; set; }
        public decimal? DiscountAmount { get; set; }


        public string NumOfdays { get; set; }
        public string InsureType { get; set; }
        public string Make { get; set; }
        public string ExcelUploadPath { get; set; }
        public decimal? BasicSumAssured { get; set; }
        public decimal? DreadSumAssured { get; set; }
        public string PaymentFrequencyDesc { get; set; }
        public decimal? TotalSumAssured { get; set; }
        public decimal? BasicPremium { get; set; }
        public decimal? DreadPremium { get; set; }

        #region Additional Fields for Funeral 
        //Additional Fields for Funeral 
        public string AgeofRelationship3 { get; set; }
        public string AgeofRelationship2 { get; set; }
        public string AgeofRelationship1 { get; set; }

        public DateTime? DateOfBirth1 { get; set; }
        public DateTime? DateOfBirth2 { get; set; }
        public DateTime? DateOfBirth3 { get; set; }
        public decimal? InitialLumpSum { get; set; }
        public decimal? ContributionAmount { get; set; }
        public string PeriodOfSavings { get; set; }
        public decimal? InterestRate { get; set; }
        public string FirePolicyType { get; set; }
        public string BuildingLocation { get; set; }
        public string BuildingValueCurrency { get; set; }
        public decimal? BuildingValue { get; set; }
        public string FirePeriod { get; set; }
        public byte[] Image { get; set; }
        #endregion

        #region Fire and Allied Perils Quote
        //Fire and Allied Perils Quote
        public string PropertyToInsure1 { get; set; }
        public string PropertyToInsure2 { get; set; }
        public string PropertyToInsure3 { get; set; }
        public string PropertyToInsure4 { get; set; }
        public string PropertyToInsure5 { get; set; }
        public string CurrencyValue1 { get; set; }
        public string CurrencyValue2 { get; set; }
        public string CurrencyValue3 { get; set; }
        public string CurrencyValue4 { get; set; }
        public string CurrencyValue5 { get; set; }
        public string ValueOfBuilding1 { get; set; }
        public string ValueOfBuilding2 { get; set; }
        public string ValueOfBuilding3 { get; set; }
        public string ValueOfBuilding4 { get; set; }
        public string ValueOfBuilding5 { get; set; }
        public string LocationOfProperty { get; set; }
        public string DetailsOfProperty { get; set; }
        public string ExtraPerils1 { get; set; }
        public string ExtraPerils2 { get; set; }
        public string ExtraPerils3 { get; set; }
        public string ExtraPerils4 { get; set; }
        public string ExtraPerils5 { get; set; }
        public string ExtraPerils6 { get; set; }
        public string ExtraPerils7 { get; set; }
        public string InsurePeriod { get; set; }
        //Home Inasurance Quote
        public string HomePolicyType { get; set; }
        public string HomeLocation { get; set; }
        public string HomeCurrencyValue { get; set; }
        public string HomeValuePrice { get; set; }
        public string HomeInsurePeriod { get; set; }
        #endregion

        #region Personal Accident Insurance
        //Personal Accident Insurance 
        public bool? DeathYN { get; set; }
        public bool? PermanentDisabilityYN { get; set; }
        public bool? TemporaryDisabilityYN { get; set; }
        public bool? MedicalExpenseYN { get; set; }

        public decimal? Death { get; set; }
        public decimal? PermanentDisability { get; set; }
        public decimal? TemporaryDisability { get; set; }
        public decimal? MedicalExpense { get; set; }
        #endregion

        //Marine Insurance 
        public string DescriptionCargo { get; set; }
        public string ValueOfGoods { get; set; }
        public string Packaging { get; set; }
        public string CurrencyDesc { get; set; }
        public string MarineCargo { get; set; }
        public DateTime? DateOfMarineReturn { get; set; }
        public DateTime? DateofMarineDepature { get; set; }
        public string MarineDestinationCountry { get; set; }
        public string MarineDepartureCountry { get; set; }
        public string CoverageTypeDesc { get; set; }
        public string FamilyPackage { get; set; }
        public decimal OccupationRate { get; set; }
        public string GroupOption { get; set; }
        public decimal FamilyPackageCheckSum { get; set; }
    }
}
