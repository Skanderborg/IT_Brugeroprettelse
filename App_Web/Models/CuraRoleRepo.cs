using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Web.Models
{
    internal class CuraRoleRepo
    {
        private List<CuraRole> roller;

        internal CuraRoleRepo()
        {
            roller = new List<CuraRole>();
            roller.Add(new CuraRole() { System_id = 0, Name = "Administrativ medarbejder", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 1, Name = "Borgerkonsulent", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 2, Name = "Demenskonsulent", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 3, Name = "Diætist", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 4, Name = "Ergoterapeut", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 5, Name = "Forebyggelseskonsulent", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 6, Name = "Fysioterapeut", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 7, Name = "Konsulent", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 8, Name = "Leder", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 9, Name = "Psykolog", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 10, Name = "Ressourceplanlægger", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 11, Name = "Social og Sundhedsassistent", IsPlanner = true, IsFMKuser = true });
            roller.Add(new CuraRole() { System_id = 12, Name = "Social og sundhedshjælper", IsPlanner = true, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 13, Name = "Sygeplejerske", IsPlanner = true, IsFMKuser = true });
            roller.Add(new CuraRole() { System_id = 14, Name = "Systemadministrator", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 15, Name = "Træningsassistent", IsPlanner = false, IsFMKuser = false });
            roller.Add(new CuraRole() { System_id = 16, Name = "Visitator", IsPlanner = false, IsFMKuser = false });
        }

        internal IQueryable<CuraRole> Query
        {
            get
            {
                return roller.AsQueryable();
            }
        }
    }
}