using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using QuoteBot.Models;

namespace QuoteBot.Dialogs
{
    public class PremiumQuoteDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<VehiclePremium> _premInfoAccessor;
        private DataLayer<Lookups> data;
        public static string retryPromptChoices = "Sorry, you chose a wrong option. Please select one of the options available";
        public static string currencyDesc = string.Empty;

        public PremiumQuoteDialog(UserState userState)
            : base(nameof(PremiumQuoteDialog))
        {
            _premInfoAccessor = userState.CreateProperty<VehiclePremium>("PremiumInfo");
            data = new DataLayer<Lookups>();

            // This array defines how the Waterfall will execute.
            var waterfallSteps = new WaterfallStep[]
            {
                CoverTypeStepAsync,
                UsageStepAsync,
                CurrencyTypeStepAsync,
                VehicleValueStepAsync,
                CubicCapacityStepAsync,
                RegistrationTypeStepAsync,
                RegistrationNumberStepAsync,
                YearManufactureStepAsync,
                SeatingCapacityStepAsync,
                CoverPeriodStepAsync,
                NCDStepAsync,
                SummaryStepAsync,
                CalculatePremiumStepAsync
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt("RegistrationNumber", RegistrationNumberValidatorAsync));
            AddDialog(new NumberPrompt<int>("YearManufacture", YearManufactureValidatorAsync));
            AddDialog(new NumberPrompt<int>("SeatingCapacity", SeatingCapacityValidatorAsync));
            AddDialog(new NumberPrompt<decimal>("VehicleValue", VehicleValueValidatorAsync));
            AddDialog(new ChoicePrompt("Usage"));
            AddDialog(new ChoicePrompt("CoverType"));
            AddDialog(new ChoicePrompt("CurrencyType"));
            AddDialog(new ChoicePrompt("CubicCapacity"));
            AddDialog(new ChoicePrompt("RegistrationType"));
            AddDialog(new ChoicePrompt("CoverPeriod"));
            AddDialog(new ChoicePrompt("NCD"));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private static async Task<DialogTurnResult> CoverTypeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            return await stepContext.PromptAsync("CoverType",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What cover type would you want for your vehicle?"),
                    Choices = ChoiceFactory.ToChoices(DataLayer<Lookups>.coverageOptions),
                    RetryPrompt = MessageFactory.Text(retryPromptChoices),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> UsageStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["coverType"] = ((FoundChoice)stepContext.Result).Value;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Got it"), cancellationToken);
            return await stepContext.PromptAsync("Usage",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What is your vehicle primarily used for?"),
                    Choices = ChoiceFactory.ToChoices(DataLayer<Lookups>.usageOptions),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> CurrencyTypeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["usageType"] = ((FoundChoice)stepContext.Result).Value;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("OK"), cancellationToken);
            return await stepContext.PromptAsync("CurrencyType",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What currency did you purchase your vehicle with?"),
                    RetryPrompt = MessageFactory.Text(retryPromptChoices),
                    Choices = ChoiceFactory.ToChoices(DataLayer<Lookups>.currencyOptions),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> VehicleValueStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["currencyType"] = ((FoundChoice)stepContext.Result).Value;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Alright"), cancellationToken);
            return await stepContext.PromptAsync("VehicleValue", 
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What is your vehicle's current value in the currency you purchased it with?"),
                    RetryPrompt = MessageFactory.Text("Sorry but your vehicle must cost something. Could you please try again?"),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> CubicCapacityStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["vehicleValue"] = (decimal)stepContext.Result;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Good"), cancellationToken);
            return await stepContext.PromptAsync("CubicCapacity",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What is the vehicle's engine capacity?"),
                    RetryPrompt = MessageFactory.Text(retryPromptChoices),
                    Choices = ChoiceFactory.ToChoices(DataLayer<Lookups>.cubicCapacityOptions),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> RegistrationTypeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["cubicCapacity"] = ((FoundChoice)stepContext.Result).Value;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Noted"), cancellationToken);
            return await stepContext.PromptAsync("RegistrationType",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text(string.Format("What type of registration number does your vehicle have?")),
                    RetryPrompt = MessageFactory.Text(retryPromptChoices),
                    Choices = ChoiceFactory.ToChoices(DataLayer<Lookups>.registrationTypeOptions),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> RegistrationNumberStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["registrationType"] = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync("RegistrationNumber", 
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Could you please tell me the vehicle number registration?"),
                    RetryPrompt = MessageFactory.Text(string.Format("Sorry. Could you please state in XX XXXX-YY format if you have a standard registration?"))
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> YearManufactureStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["registrationNumber"] = (string)stepContext.Result;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("I'll remember that"), cancellationToken);
            return await stepContext.PromptAsync("YearManufacture", 
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What year was your vehicle manufactured in?"),
                    RetryPrompt = MessageFactory.Text("Sorry but I can only work with years from 1970 till this year. Can you please try again?")
                }, 
                cancellationToken);
        }

        private static async Task<DialogTurnResult> SeatingCapacityStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["yearManufacture"] = (int)stepContext.Result;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Saved"), cancellationToken);
            return await stepContext.PromptAsync("SeatingCapacity", 
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("How many people can sit in your vehicle at a time?"),
                    RetryPrompt = MessageFactory.Text("I don't mean to be rude but your vehicle should fit at least one person in. Can you please try again?")
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> CoverPeriodStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["seatingCapacity"] = (int)stepContext.Result;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Alright"), cancellationToken);
            return await stepContext.PromptAsync("CoverPeriod",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text(string.Format("What period would you want this insurance to cover?")),
                    RetryPrompt = MessageFactory.Text(retryPromptChoices),
                    Choices = ChoiceFactory.ToChoices(DataLayer<Lookups>.coverPeriodOptions),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> NCDStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["coverPeriod"] = ((FoundChoice)stepContext.Result).Value;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Got it"), cancellationToken);
            return await stepContext.PromptAsync("NCD",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text(string.Format("How many years has it been since you last made a claim with us?")),
                    RetryPrompt = MessageFactory.Text(retryPromptChoices),
                    Choices = ChoiceFactory.ToChoices(DataLayer<Lookups>.yearsNCDOptions),
                }, cancellationToken);
        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["NCDPeriod"] = ((FoundChoice)stepContext.Result).Value;

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Excellent"), cancellationToken);
            // Get the current profile object from user state.
            var userProfile = await _premInfoAccessor.GetAsync(stepContext.Context, () => new VehiclePremium(), cancellationToken);

            userProfile.CoverTypeDesc = (string)stepContext.Values["coverType"];
            userProfile.Usage = (string)stepContext.Values["usageType"];
            userProfile.Currency = (string)stepContext.Values["currencyType"];
            currencyDesc = (string)stepContext.Values["currencyType"];
            userProfile.ValueOfVehicle = Convert.ToDecimal(stepContext.Values["vehicleValue"]);
            userProfile.CubicCapacity = (string)stepContext.Values["cubicCapacity"];
            userProfile.RegistrationNumberType = (string)stepContext.Values["registrationType"];
            userProfile.RegistrationNumber = (string)stepContext.Values["registrationNumber"];
            userProfile.YearOfManufacture = stepContext.Values["yearManufacture"].ToString();
            userProfile.SeatingCapacity = (int)stepContext.Values["seatingCapacity"];
            userProfile.PayFrequency = (string)stepContext.Values["coverPeriod"];
            userProfile.NoClaimDiscountPeriodDesc = (string)stepContext.Values["NCDPeriod"];

            var msg = $"OK... so I have your details as:\n";
            msg += $"Your preferred cover type is {userProfile.CoverTypeDesc}.\n";
            msg += $"Vehicle usage is {userProfile.Usage}.\n";
            msg += $"Current value of the vehicle as {userProfile.Currency} {userProfile.ValueOfVehicle}.\n";
            msg += $"The engine capacity as {userProfile.CubicCapacity}.\n";
            msg += $"Vehicle's registration number is {userProfile.RegistrationNumber}.\n";
            msg += $"The manufactured year of the vehicle as {userProfile.YearOfManufacture}.\n";
            msg += $"Seating capacity as {userProfile.SeatingCapacity}.\n";
            msg += $"Preferred cover period as {userProfile.PayFrequency}.\n";
            msg += $"And finally, the number of years without a claim as {userProfile.NoClaimDiscountPeriodDesc}.\n";

            await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);

            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = MessageFactory.Text("Is this correct?") }, cancellationToken);

            // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
            //return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private async Task<DialogTurnResult> CalculatePremiumStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((bool)stepContext.Result)
            {
                var userProfile = await _premInfoAccessor.GetAsync(stepContext.Context, () => new VehiclePremium(), cancellationToken);

                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Great! Please wait a little while as I retrieve your premium..."), cancellationToken);
                var prem = await DataLayer<Lookups>.CalculatePremium(userProfile);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Your premium is {currencyDesc} {prem.ToString("0.##")}."), cancellationToken);
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"It's been my pleasure working with you. See you again soon!"), cancellationToken);
            }

            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

        private static Task<bool> YearManufactureValidatorAsync(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            // This condition is our validation rule. You can also change the value at this point.
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value >= 1970 && promptContext.Recognized.Value <= DateTime.Today.Year);
        }

        private static async Task<bool> RegistrationNumberValidatorAsync(PromptValidatorContext<string> promptContext, CancellationToken cancellationToken)
        {
            var formattedNumber = await DataLayer<Lookups>.FormatVehicleRegistrationNumber(promptContext.Recognized.Value);
            return await Task.FromResult(promptContext.Recognized.Succeeded && formattedNumber != "WRONG REGISTRATION NUMBER");
        }

        private static Task<bool> SeatingCapacityValidatorAsync(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 0);
        }

        private static Task<bool> VehicleValueValidatorAsync(PromptValidatorContext<decimal> promptContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 0);
        }
    }
}
