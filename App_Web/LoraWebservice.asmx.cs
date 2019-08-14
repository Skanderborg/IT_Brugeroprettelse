using Dal_SOFD.LORA;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using Telerik.Web.UI;

namespace App_Web
{
    /// <summary>
    /// Summary description for LoraService
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class LoraWebservice : System.Web.Services.WebService
    {

        PositionRepo repo = new PositionRepo(Properties.Settings.Default.LORA_Constr);
        [WebMethod]
        public RadComboBoxData GetEmployee(RadComboBoxContext context)
        {
            if (context.Text.Length > 1)
            {
                RadComboBoxData comboData = new RadComboBoxData();
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
                string searchterm = context.Text.ToLower();
                foreach (v_ad_user_creation pos in repo.Query.Where(p => p.User_fk == null && p.Fullname.ToLower().Substring(0, searchterm.Length).Equals(searchterm)))
                {
                    RadComboBoxItemData item = new RadComboBoxItemData();
                    item.Text = pos.Fullname + " -- " + pos.Orgunit + " -- " + pos.Position + " -- MedNR: " + pos.Opus_id;
                    item.Value = pos.Opus_id.ToString();
                    result.Add(item);
                }
                comboData.Items = result.ToArray();
                return comboData;
            }
            return null;
        }

        [WebMethod]
        public RadComboBoxData GetManager(RadComboBoxContext context)
        {
            if (context.Text.Length > 1)
            {
                RadComboBoxData comboData = new RadComboBoxData();
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
                string searchterm = context.Text.ToLower();
                foreach (v_ad_user_creation pos in repo.Query.Where(p => p.Fullname.ToLower().Substring(0, searchterm.Length).Equals(searchterm) && p.Is_Manager && p.User_fk != null))
                {
                    RadComboBoxItemData item = new RadComboBoxItemData();
                    item.Text = pos.Fullname + " -- " + pos.Orgunit + " -- " + pos.Position;
                    item.Value = pos.samaccount;
                    result.Add(item);
                }
                comboData.Items = result.ToArray();
                return comboData;
            }
            return null;
        }

        [WebMethod]
        public RadComboBoxData GetCoworker(RadComboBoxContext context)
        {
            if (context.Text.Length > 1)
            {
                RadComboBoxData comboData = new RadComboBoxData();
                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
                string searchterm = context.Text.ToLower();
                foreach (v_ad_user_creation pos in repo.Query.Where(p => p.Fullname.ToLower().Substring(0, searchterm.Length).Equals(searchterm) && p.User_fk != null))
                {
                    RadComboBoxItemData item = new RadComboBoxItemData();
                    item.Text = pos.Fullname + " -- " + pos.Orgunit + " -- " + pos.Position;
                    item.Value = pos.samaccount;
                    result.Add(item);
                }
                comboData.Items = result.ToArray();
                return comboData;
            }
            return null;
        }
    }
}
