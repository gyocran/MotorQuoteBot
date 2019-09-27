using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Logging;
using QuoteBot.Models;

namespace QuoteBot.Bots
{
    public class PremiumBot<T> : ActivityHandler where T : Dialog
    {
        protected readonly Dialog Dialog;
        protected readonly BotState ConversationState;
        protected readonly BotState PremiumState;
        protected readonly ILogger Logger;

        public PremiumBot(ConversationState conversationState, UserState userState, T dialog, ILogger<PremiumBot<T>> logger)
        {
            ConversationState = conversationState;
            PremiumState = userState;
            Dialog = dialog;
            Logger = logger;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occured during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await PremiumState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Running dialog with Message Activity.");

            // Run the Dialog with the new message Activity.
            await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    DataLayer<Lookups> data = new DataLayer<Lookups>();
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello there!"), cancellationToken);
                    await turnContext.SendActivityAsync(MessageFactory.Text($"I will be assisting you in getting a vehicle premium quote with the vehicle information you provide me"), cancellationToken);
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Before we begin, please give me a few seconds while I get ready..."), cancellationToken);
                    await data.LoadUserOptions();
                    await turnContext.SendActivityAsync(MessageFactory.Text($"All set! Let's begin..."), cancellationToken);
                    await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
                }
            }
        }
    }
}
