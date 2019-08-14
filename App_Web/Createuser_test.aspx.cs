using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Web
{
    public partial class Createuser_test : System.Web.UI.Page
    {
        public string state_cb = "collapse";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_submit_Click(object sender, EventArgs e)
        {
            bool test = cb_toggletest.Checked;
            if (test)
            {
                state_cb = "expand";
                cb_toggletest.Checked = true;
            }
            else
                state_cb = "collapse";
        }
    }
}