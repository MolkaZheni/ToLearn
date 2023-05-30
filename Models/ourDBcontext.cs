using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace LearnApp.Models
{
    public class ourDBcontext:DbContext
    {
        public DbSet<useraccount> Useraccount { get; set; }
    }
}