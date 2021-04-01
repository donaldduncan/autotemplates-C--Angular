using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using trellobot.Model;

namespace trellobot.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrellobotController : ControllerBase
    {
        private readonly TrellobotContext _context;
        private TrelloFactory factory = new TrelloFactory();


        public TrellobotController(TrellobotContext context)
        {
            _context = context;

            TrelloAuthorization.Default.AppKey = "817923699038fd73f12bfff3ef01b0ed";
            TrelloAuthorization.Default.UserToken = "32984dd6e9f6d0b6ddb28567e605df6e84e91dfca2b6514bcc1b2bd7a1cb10a6";


            /*             if (_context.TrellobotItems.Count() < 1)
                        {
                            //Create a new TrellobotItem if collection is empty,
                            //which means you can't delete all TrellobotItems.
                            _context.TrellobotItems.Add(new TrellobotItem { Name = "Trellobot 1", AppKey = "817923699038fd73f12bfff3ef01b0ed", Token = "99748fefbc2592a61cf483c25c705f37c08c1ae52fa56e60352055ca2c496101", BoardId = "5bcde8b98be19d75ecb8e356", TemplateId = "5bcde9ebc109bf6a97e028a8", TrelloMemberId = "5a170056d3c2ed417eb120cf" });
                            _context.SaveChanges();
                         }
            */
        }

        [HttpGet]
        public ActionResult<List<TrellobotItem>> GetAll()
        {
            return _context.TrellobotItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTrellobot")]
        public ActionResult<TrellobotItem> GetById(int id)
        {
            var item = _context.TrellobotItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpGet]
        public async Task<ActionResult<string>> GetMembers()
        {
            var DbItems = _context.TrellobotItems.ToList();
            var MemberIDs = DbItems.Select(i => i.MemberId).ToHashSet();
            var DTO = new List<TrellobotDTO>();
            
            Member.DownloadedFields &= ~Member.Fields.Actions & ~Member.Fields.Organizations;

            var Members = new List<IMember>();
            foreach (var id in MemberIDs)
            {
                var Member = factory.Member(id);
                await Member.Refresh();
                Members.Add(Member);
            }

            foreach (var member in Members)
            {
                var DTOitem = new TrellobotDTO();
                DTOitem.MemberData = member;
                DTOitem.TrellobotItemsForMember = DbItems.Where(i => i.MemberId == member.Id)
                                                         .Select(i => new TrellobotItemDTO() {
                                                                ID = i.ID,
                                                                Name = i.Name,
                                                                BoardId = i.BoardId,
                                                                TemplateId = i.TemplateId,
                                                                MemberId = i.MemberId
                                                         }).ToList();

                DTO.Add(DTOitem);
            }

            var errors = new List<string>();

            // return DTO. Mvc chunks the data, which gets interrupted and errors out, so manual serialization is required.
            return JsonConvert.SerializeObject(DTO, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = delegate (object sender, ErrorEventArgs args)
                    {
                        errors.Add(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                    }
                });
        }

        [HttpPost]
        public IActionResult Create(TrellobotItem item)
        {
            _context.TrellobotItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTrellobot", new { id = item.ID }, item);
        }

        [HttpPost]
        public IActionResult Authorize([FromBody] object jsonIn)
        {
            Console.WriteLine(jsonIn);
            return StatusCode(200);
        }

        [HttpPut("{id}", Name = "UpdateTrellobot")]
        public ActionResult<List<TrellobotItem>> Update(int id, TrellobotItem data)
        {
            var item = _context.TrellobotItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item = data;
            _context.TrellobotItems.Update(item);
            _context.SaveChanges();

            return GetAll();
        }

        [HttpDelete("{id}", Name = "DeleteTrellobot")]
        public ActionResult<List<TrellobotItem>> Delete(int id)
        {
            var item = _context.TrellobotItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.TrellobotItems.Remove(item);
            _context.SaveChanges();
            return GetAll();
        }
    }
}