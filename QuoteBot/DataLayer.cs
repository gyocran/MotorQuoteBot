using Newtonsoft.Json;
using QuoteBot.Helpers;
using QuoteBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuoteBot
{
    public class DataLayer<T>
    {
        private const string WebServiceUrl = "https://activainsurance-test.azurewebsites.net/";  //Activa Test Environment
        public static List<Lookups> coverageLookups;
        public static List<Lookups> usageLookups;
        public static List<Lookups> currencyLookups;
        public static List<Lookups> cubicCapacityLookups;
        public static List<Lookups> registrationTypeLookups;
        public static List<Lookups> coverPeriodLookups;
        public static List<Lookups> yearsNCDLookups;
        public static List<string> coverageOptions;
        public static List<string> usageOptions;
        public static List<string> currencyOptions;
        public static List<string> cubicCapacityOptions;
        public static List<string> registrationTypeOptions;
        public static List<string> coverPeriodOptions;
        public static List<string> yearsNCDOptions;

        public DataLayer()
        {
            //await LoadUserOptions();
        }

        public async Task LoadUserOptions()
        {
            await GetCoverageType();
            await GetUsageOptions();
            await GetCurrencyOptions();
            await GetCubicCapacityOptions();
            await GetRegistrationTypeOptions();
            await GetCoverPeriodOptions();
            await GetNCDYearsOptions();
        }

        public async Task GetCoverageType()
        {
            coverageLookups = await RestClient<List<Lookups>>.GetAsync("popups/getcoveragetype");
            coverageOptions = GenerateList(coverageLookups);
        }

        public async Task GetUsageOptions()
        {
            usageLookups = await RestClient<List<Lookups>>.GetAsync("popups/getusages");
            usageOptions = GenerateList(usageLookups);
        }

        public async Task GetCurrencyOptions()
        {
            currencyLookups = await RestClient<List<Lookups>>.GetAsync("popups/getcurrency");
            currencyOptions = GenerateList(currencyLookups);
        }

        public async Task GetCubicCapacityOptions()
        {
            cubicCapacityLookups = await RestClient<List<Lookups>>.GetAsync("popups/getcubiccapacity");
            cubicCapacityOptions = GenerateList(cubicCapacityLookups);
        }

        public async Task GetRegistrationTypeOptions()
        {
            registrationTypeLookups = await RestClient<List<Lookups>>.GetAsync("popups/getregistrationnumber");
            registrationTypeOptions = GenerateList(registrationTypeLookups);
        }

        public async Task GetCoverPeriodOptions()
        {
            coverPeriodLookups = await RestClient<List<Lookups>>.PostAsync("Motor", "popups/payfrequencybypopsource");
            coverPeriodOptions = GenerateList(coverPeriodLookups);
        }

        public async Task GetNCDYearsOptions()
        {
            yearsNCDLookups = await RestClient<List<Lookups>>.GetAsync("popups/getncd");
            yearsNCDOptions = GenerateList(yearsNCDLookups);
        }

        //public async Task<List<Lookups>> GetAsync(string _action)
        //{
        //    var httpClient = new HttpClient();
        //    var result = new List<Lookups>();
        //    var optionsList = new List<string>();
        //    try
        //    {
        //        //var _bearer = "Bearer " + AppSettings.GetValueOrDefault("access_token", string.Empty);
        //        //httpClient.DefaultRequestHeaders.Add("Authorization", _bearer);

        //        var uri = new Uri(string.Format(WebServiceUrl + _action, string.Empty));
        //        var json = await httpClient.GetAsync(uri);

        //        if (json.IsSuccessStatusCode)
        //        {
        //            var _content = await json.Content.ReadAsStringAsync();
        //            result = JsonConvert.DeserializeObject<List<Lookups>>(_content);
        //        }

        //        //if (result != null)
        //        //{
        //        //    foreach (var option in result)
        //        //    {
        //        //        optionsList.Add(option.Description);
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //Crash reporting to App Center
        //        var properties = new Dictionary<string, string> {
        //            { "Page", "RestClient" },
        //            { "Method", "GetAsync" },
        //            { "URL", _action }
        //        };

        //    }
        //    return result;
        //}

        //public async Task<T> PostAsync(dynamic returnType, string param, string _action)
        //{
        //    var httpClient = new HttpClient();
        //    var result = "";
        //    var optionsList = new List<string>();

        //    try
        //    {
        //        //var _bearer = "Bearer " + AppSettings.GetValueOrDefault("access_token", string.Empty);
        //        //httpClient.DefaultRequestHeaders.Add("Authorization", _bearer);
        //        var json = JsonConvert.SerializeObject(param);
        //        HttpContent httpContent = new StringContent(json);
        //        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        httpClient.DefaultRequestHeaders.Add("Cookie", "auth=ArbitrarySessionToken");

        //        var uri = new Uri(string.Format(WebServiceUrl + _action, string.Empty));
        //        var response = await httpClient.PostAsync(uri, httpContent);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            result = await response.Content.ReadAsStringAsync();
        //        }

        //        //if (result != null)
        //        //{
        //        //    foreach (var option in result)
        //        //    {
        //        //        optionsList.Add(option.Description);
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //Crash reporting to App Center
        //        var properties = new Dictionary<string, string> {
        //            { "Page", "RestClient" },
        //            { "Method", "GetAsync" },
        //            { "URL", _action }
        //        };

        //    }
        //    return JsonConvert.DeserializeObject<T>(result);
        //}

        public List<string> GenerateList(List<Lookups> lookups)
        {
            var optionsList = new List<string>();

            if (lookups != null)
            {
                foreach (var option in lookups)
                {
                    optionsList.Add(option.Description);
                }
            }

            return optionsList;
        }

        public async static Task<decimal> CalculatePremium(VehiclePremium vehicle)
        {
            if (!string.IsNullOrEmpty(vehicle.CoverType))
                vehicle.CoverType = coverageLookups.Find(x => x.Description == vehicle.CoverType.ToString()).Code;

            if (!string.IsNullOrEmpty(vehicle.Usage))
                vehicle.Usage = usageLookups.Find(x => x.Description == vehicle.Usage.ToString()).Code;

            if (!string.IsNullOrEmpty(vehicle.CubicCapacity))
                vehicle.CubicCapacity = cubicCapacityLookups.Find(x => x.Description == vehicle.CubicCapacity.ToString()).Code;

            if (!string.IsNullOrEmpty(vehicle.Currency))
                vehicle.Currency = currencyLookups.Find(x => x.Description == vehicle.Currency.ToString()).Code;

            if (!string.IsNullOrEmpty(vehicle.PayFrequency))
                vehicle.PayFrequency = coverPeriodLookups.Find(x => x.Description == vehicle.PayFrequency.ToString()).Code;

            //if (!string.IsNullOrEmpty(vehicle.SeatingCapacity))
            //    motorPremiumParam.SeatingCapacity = Convert.ToInt32(QuoteData.SeatingCapacityString);

            var prem = await RestClient<VehiclePremium>.PostAsync(vehicle, "insurance/motorpremiumcalc");
            //var result = await restClient.PostAsyncHttpAction(motorPremiumParam, new VehiclePremiumInfo(), "insurance/motorpremiumcalc");
            return (decimal)prem.Premium;
        }

        public async static Task<string> FormatVehicleRegistrationNumber(string regNumber)
        {
            //GenericRestClient<string> _FormatRegNorestClient = new GenericRestClient<string>();
            var registrationDetails = new
            {
                Platform = "ussd",
                VehicleNumber = regNumber
            };
            
            var _formatedRegNoresult = await RestClient<string>.PostAsync(registrationDetails, "insurance/formatvehiclenumber");
            return _formatedRegNoresult;
        }
    }
}
