using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Web.Models
{
    internal class CuraRole
    {
        internal int System_id { get; set; }
        internal string Name { get; set; }
        internal bool IsPlanner { get; set; }
        internal bool IsFMKuser { get; set; }
    }
}