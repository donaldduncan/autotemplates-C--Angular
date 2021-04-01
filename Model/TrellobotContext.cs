using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Manatee.Trello;

namespace trellobot.Model
{
    public class TrellobotItem
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string BoardId { get; set; }
        public string TemplateId { get; set; }
        public string Token { get; set; }
        public string MemberId { get; set; }
    }


    public class TrellobotItemDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string BoardId { get; set; }
        public string TemplateId { get; set; }
        public string MemberId { get; set; }

    }
    public class TrellobotDTO
    {
        public IMember MemberData { get; set; }
        public List<TrellobotItemDTO> TrellobotItemsForMember { get; set; }
    }

    public class TrellobotContext : DbContext
    {
        public TrellobotContext(DbContextOptions<TrellobotContext> options)
            : base(options)
        {
        }

        public DbSet<TrellobotItem> TrellobotItems { get; set; }
    }
}