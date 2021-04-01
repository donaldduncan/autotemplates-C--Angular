using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Manatee.Trello;
using Newtonsoft.Json.Linq;
using trellobot.Model;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace trellobot.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TrelloDataController : ControllerBase
    {
        private TrelloFactory factory = new TrelloFactory();
        private readonly TrellobotContext _context;

        public TrelloDataController(TrellobotContext context)
        {
            _context = context;
            TrelloAuthorization.Default.AppKey = "817923699038fd73f12bfff3ef01b0ed";
            TrelloAuthorization.Default.UserToken = "32984dd6e9f6d0b6ddb28567e605df6e84e91dfca2b6514bcc1b2bd7a1cb10a6";
            //TrelloConfiguration.ChangeSubmissionTime = TimeSpan.FromMilliseconds(0);
        }

        [HttpHead]
        public IActionResult SetupWebhook()
        {
            return StatusCode(200);
        }

        [HttpGet("/boards/{id}", Name = "GetBoards")]
        public async Task<object> getBoards(string id) {
            var member = factory.Member(id);
            await member.Refresh();
            var boards = member.Boards;
            return (object) boards;
        }

        [HttpPost]
        public async void WebhookJson([FromBody] object jsonIn)
        {
            string strJson = jsonIn.ToString();
            //System.IO.File.WriteAllText("C:/Users/ddunc/Documents/strJson.json", strJson);

            dynamic jsonObj = JObject.Parse(strJson);
            dynamic action = jsonObj.action;

            if (action.type == "emailCard" || action.type == "createCard" ||
               action.type == "copyCard" || action.type == "moveCardToBoard" ||
               action.type == "convertToCardFromCheckItem" ||
               action.type == "moveListToBoard" ||
               (action.type == "updateCard" && action.data.listAfter != null))
            {
                var card = factory.Card((string)action.data.card.id);
                await card.Refresh();

                var template = await getTemplateCardAsync(card);

                await processCardAsync(card, template, action.data);
            }
        }
        private async Task<ICard> getTemplateCardAsync(ICard card)
        {
            bool templateCard = false;
            string templateId = "5bcde9ebc109bf6a97e028a8";

            if (card == null)
            {
                return null;
            }

            ICard template;
            if (templateCard)
            {
                template = factory.Card(templateId);
            }
            else
            {
                var templateList = factory.List(templateId);
                await templateList.Refresh();

                template = templateList.Cards.FirstOrDefault(c => c.Name == getNameWithoutLimit(card.List.Name));
            }
            if (template != null) { await template.Refresh(); };
            return template;
        }
        private async Task processCardAsync(ICard card, ICard template, dynamic data)
        {
            // Don't process template card
            if (template == null || card.Id == template.Id)
            {
                return;
            }

            // Use the description from the template if no current description:
            if (card.Description.Trim() == "" && template.Description.Trim() != "")
            {
                string descriptionWithoutDueDate = Regex.Replace(template.Description, @"(\[\+\d+(\s?)+(weeks|w)\])|((today\+)\d+)", string.Empty).Trim();
                card.Description = descriptionWithoutDueDate;
            }

            await SyncCurrentChecklistsAsync(card, template);
            await AddMissingChecklistsAsync(card, template);
            AddDueDate(card, template, data);
        }

        private void AddDueDate(ICard card, ICard template, dynamic data)
        {
            var today = DateTime.Now;

            // Calculate and add new due date (Default to 0)
            int weeksToAdd = 0;
            string numberFromTemplate = template.Description.Substring(template.Description.IndexOf("[+") + 2, 2);
            
            if (int.TryParse(numberFromTemplate, out weeksToAdd))
            {

                // Add record of superseeded due date in description
                if (card.DueDate != null)
                {
                    if (card.Description != null)
                    {
                        card.Description = $"{card.Description} \n\n";
                    }

                    card.Description = $"{card.Description}> Previous due date: ** {((DateTime)card.DueDate).ToString("dd MMM yyyy")} **[in list _'{data.listBefore.name}'_]\n _(Replaced: {today.ToString("dd MMM yyyy")} )_";
                }

            }
            else {return;}

            card.DueDate = today.AddDays(7 * weeksToAdd);
        }

        private async Task SyncCurrentChecklistsAsync(ICard card, ICard template)
        {
            var matchingChecklists = template.CheckLists.Where(l => card.CheckLists[l.Name] != null);
            foreach (var templateChecklist in matchingChecklists)
            {
                var cardChecklist = card.CheckLists[templateChecklist.Name];
                var missingItems = templateChecklist.CheckItems.Where(i => cardChecklist[i.Name] == null && (int)i.State != 2); // Only unchecked items... (State = 2 is checked (1 is unchecked, 0 unknown))
                foreach (var item in missingItems)
                {
                    await cardChecklist.CheckItems.Add(item.Name);
                }
            }
        }
        private async Task AddMissingChecklistsAsync(ICard card, ICard template)
        {
            var missingChecklists = template.CheckLists.Where(l => card.CheckLists[l.Name] == null);
            foreach (var templateChecklist in missingChecklists)
            {
                // Add checklists
                var cardChecklist = await card.CheckLists.Add(templateChecklist.Name, templateChecklist);

                // Remove template items that are checked
                var extraItems = templateChecklist.CheckItems.Where(i => (int)i.State == 2).ToList(); // Find checked items... (State = 2 is checked (1 is unchecked, 0 unknown))
                foreach (var item in extraItems)
                {
                    await cardChecklist.CheckItems[item.Name].Delete();
                }
            }
        }
        private string getNameWithoutLimit(string name)
        {

            var nameWithoutLimit = name.Trim();

            if (name.Count() > 0 && name.Substring(name.Count() - 1) == "]" && name.Contains("["))
            {
                nameWithoutLimit = name.Substring(0, name.IndexOf("[")).Trim();
            }

            return nameWithoutLimit;

        }
    }
}
/* var auth = new TrelloAuthorization();
List<TrellobotItem> trellobots = _context.TrellobotItems.ToList();
*/
/* var factory = new TrelloFactory();
var board = factory.Board("zwifHvXN");//trellobots[1].BoardId, auth);
await board.Refresh(); */

//var webhook = factory.Webhook(board, "http://7039425b.ngrok.io/api/TrelloData");


//TrelloData notification = TrelloData.FromJson(strJson);



/* var action = factory.Action(change.Action.Id);
Action action = new Action(change.Action.Id);


TrelloAction action = JsonConvert.DeserializeObject<TrelloAction>(jsonIn.ToString());
TrelloProcessor.ProcessNotification(jsonIn.ToString());
*/
