using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_ActiveDirectory.Services
{
    public class GroupPrincipalService
    {
        private const string strContextName = "SKB";

        public List<string> GetCuraRoles(string strContextContainer)
        {
            List<string> res = new List<string>();
            using(PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, strContextName, strContextContainer))
            {
                using(GroupPrincipal groupPrincipal = new GroupPrincipal(principalContext))
                {
                    PrincipalSearcher searcher = new PrincipalSearcher(groupPrincipal);
                    foreach(Principal found in searcher.FindAll())
                    {
                        res.Add(found.Name);
                    }
                }
            }
            return res;
        }

        public List<string> GetCuraOrg(string strContextcontainer)
        {
            List<string> res = new List<string>();
            using(PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, strContextName, strContextcontainer))
            {
                using(GroupPrincipal groupPrincipal = new GroupPrincipal(principalContext))
                {
                    PrincipalSearcher searcher = new PrincipalSearcher(groupPrincipal);
                    foreach(Principal found in searcher.FindAll())
                    {
                        res.Add(found.Name);
                    }
                }
            }
            return res;
        }
    }
}
