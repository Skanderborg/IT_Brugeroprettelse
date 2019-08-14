using App_Web.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Telerik.Web.UI;

namespace App_Web
{
    public partial class Createuser : System.Web.UI.Page
    {
        private CreateUserService service = new CreateUserService();
        private System.Drawing.Color colErr = System.Drawing.Color.Red;
        private System.Drawing.Color colNoErr = System.Drawing.Color.LightGray;
        private System.Drawing.Color colNoErrLabel = System.Drawing.Color.Black;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCuraRoles();
                LoadCuraLOrg();
            }
        }

        protected void LoadCuraRoles()
        {
            DdlCuraBrugerRolle.Items.Clear();
            DdlCuraBrugerRolle.Items.Add(new Telerik.Web.UI.DropDownListItem() { Value = "X", Text = "Vælg rolle" });
            int count = 1;
            foreach (string role in service.GetCuraRoles())
            {
                DdlCuraBrugerRolle.Items.Add(new Telerik.Web.UI.DropDownListItem() { Value = count.ToString(), Text = role });
                count++;
            }
        }

        protected void LoadCuraLOrg()
        {
            ddl_curaLOrg.Items.Clear();
            ddl_curaLOrg.Items.Add(new Telerik.Web.UI.DropDownListItem() { Value = "X", Text = "Vælg Login Organisation" });
            int count = 1;
            foreach (string lorg in service.GetCuraLOrg())
            {
                ddl_curaLOrg.Items.Add(new Telerik.Web.UI.DropDownListItem() { Value = count.ToString(), Text = lorg });
                count++;
            }
        }

        protected void CBOpus_medarbejdernr_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int opus_id;
            if (int.TryParse(CBOpus_medarbejdernr.SelectedValue, out opus_id))
            {
                Dictionary<string, string> names = service.GetEmployeeNames(opus_id);
                TxbFornavn.Text = names["Firstname"];
                TxbEfternavn.Text = names["Lastname"];
                TxbVistnavn.Text = names["Fullname"];
            }
        }

        protected void Button_submit_Click(object sender, EventArgs e)
        {
            if (IsPageValid())
            {
                string errMessage;
                bool result = service.CreateUser(CBOpus_medarbejdernr.SelectedValue,
                                                TxbFornavn.Text,
                                                TxbEfternavn.Text,
                                                TxbVistnavn.Text,
                                                CbManagerSamaccount.SelectedValue,
                                                CbCoworkerSamaccount.SelectedValue,
                                                RBIsEmail.SelectedIndex == 0,
                                                TxbKontaktTelefonNummer.Text,
                                                RbIsSkype.SelectedIndex == 0,
                                                RbIsRingegruppe.SelectedIndex == 0,
                                                TxbRingegruppeNummer.Text,
                                                TxbMobilNummer.Text,
                                                RbIsDistributionslister.SelectedIndex == 0,
                                                TxbDistributionslisterNavne.Text,
                                                RbIsFaellespostkasser.SelectedIndex == 0,
                                                TxbFaellespostkasserNavne.Text,
                                                RbIsNemID.SelectedIndex == 0,
                                                TxbEan.Text,
                                                RbIsCura.SelectedIndex == 0,
                                                DdlCuraBrugerRolle.SelectedText,
                                                RbIsCuraPlanner.SelectedIndex == 0,
                                                RbIsCuraFMK.SelectedIndex == 0,
                                                TxbCuraFMKID.Text,
                                                RbIsKMDbruger.SelectedIndex == 0,
                                                TxbKMDUserProfiles.Text,
                                                RbIsKMDOpusOkonomiBilagsbehandling.SelectedIndex == 0,
                                                TxbKMDProfitcenterOmkostningssted.Text,
                                                RbIsKMDFaktura.SelectedIndex == 0,
                                                TxbKMDOpusOkonomiEan.Text,
                                                RbIsKMDBudgetOmplacering.SelectedIndex == 0,
                                                RbIsKMDMitForventedeRegnskab.SelectedIndex == 0,
                                                RbIsKMDLoenOgPersonale.SelectedIndex == 0,
                                                TxbKMDOrgUnit.Text,
                                                TxbBemaerkninger.Text, out errMessage);
                if (result)
                {
                    Response.Redirect("Success.aspx");
                }
                else
                {
                    lblTotaleFatale.Text = errMessage;
                    lblTotaleFatale.ForeColor = colErr;
                    lblTotaleFatale.Visible = true;
                }
            }
        }

        protected bool IsPageValid()
        {
            bool oplysninger_om_medarbejder = Oplysninger_om_medarbejder_is_valid();
            bool oplysninger_om_medarbejderens_position = Oplysninger_om_medarbejderens_position_is_valid();
            bool oplysninger_om_medarbejderens_telefoni = Oplysninger_om_medarbejderens_telefoni_is_valid();
            bool ovrige_rettigheder = Ovrige_rettigheder_is_valid();
            bool ovrige_programmer = Ovrige_programmer_is_valid();
            return oplysninger_om_medarbejder && oplysninger_om_medarbejderens_position && oplysninger_om_medarbejderens_telefoni && ovrige_rettigheder && ovrige_programmer;
        }

        /// <summary>
        /// Tjekker Oplysninger om medarbejderen
        /// </summary>
        /// <returns></returns>
        protected bool Oplysninger_om_medarbejder_is_valid()
        {
            bool res = true;
            int opus_id;
            if (int.TryParse(CBOpus_medarbejdernr.SelectedValue, out opus_id))
            {
                if (service.IsEmployee(opus_id))
                {
                    CBOpus_medarbejdernr.BorderColor = colNoErr;
                }
                else
                {
                    CBOpus_medarbejdernr.BorderColor = colErr;
                    res = false;
                }
            }
            else
            {
                CBOpus_medarbejdernr.BorderWidth = 1;
                CBOpus_medarbejdernr.BorderColor = colErr;
                res = false;
            }

            // vist navn skal tjekkes:
            // skal muligvis tjekkes - på displayname
            if (TxbVistnavn.Text.Length > 5)
            {
                TxbVistnavn.BorderColor = colNoErr;
            }
            else
            {
                TxbVistnavn.BorderColor = colErr;
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Tjekker Oplysninger om medarbejderens position i Skanderborg Kommune
        /// </summary>
        /// <returns></returns>
        protected bool Oplysninger_om_medarbejderens_position_is_valid()
        {
            // er der valgt en gyldig leder?
            bool res = true;
            if (CbManagerSamaccount.SelectedValue.Length == 6)
            {
                CbManagerSamaccount.BorderColor = colNoErr;
            }
            else
            {
                CbManagerSamaccount.BorderWidth = 1;
                CbManagerSamaccount.BorderColor = colErr;
                res = false;
            }

            // er der valgt en gyldig coworker?
            if (CbCoworkerSamaccount.SelectedValue.Length == 6)
            {
                CbCoworkerSamaccount.BorderColor = colNoErr;
            }
            else
            {
                CbCoworkerSamaccount.BorderWidth = 1;
                CbCoworkerSamaccount.BorderColor = colErr;
                res = false;
            }

            // er der taget stilling til email?
            if (RBIsEmail.SelectedIndex == -1)
            {
                ErrEmail.ForeColor = colErr;
                res = false;
            }
            else
            {
                ErrEmail.ForeColor = colNoErrLabel;
            }
            return res;
        }

        protected bool Oplysninger_om_medarbejderens_telefoni_is_valid()
        {
            bool res = true;

            // er der taget stilling til skype?
            if (RbIsSkype.SelectedIndex == -1)
            {
                ErrSkype.ForeColor = colErr;
                res = false;
            }
            else
            {
                ErrSkype.ForeColor = colNoErrLabel;
                // hvis ja
                if (RbIsSkype.SelectedIndex == 0)
                {

                    // men er der så taget højde for ringe/svargruppe
                    if (RbIsRingegruppe.SelectedIndex == -1)
                    {
                        errRingGruppe.ForeColor = colErr;
                        res = false;
                    }
                    else
                    {
                        errRingGruppe.ForeColor = colNoErrLabel;
                        // men hvis ringegruppe er ja, er der så et nummer?
                        if (RbIsRingegruppe.SelectedIndex == 0)
                        {
                            if (TxbRingegruppeNummer.Text.Length > 1)
                            {
                                TxbRingegruppeNummer.BorderColor = colNoErr;
                            }
                            else
                            {
                                TxbRingegruppeNummer.BorderColor = colErr;
                                res = false;
                            }
                        }
                    }
                }
            }

            // er der kontakt nummer, og er det så et tal?
            if (TxbKontaktTelefonNummer.Text.Length > 0)
            {
                string input = TxbKontaktTelefonNummer.Text;
                bool isValid = input.Length == 8 && input.All(char.IsDigit);
                if (isValid)
                {
                    TxbKontaktTelefonNummer.BorderColor = colNoErr;
                }
                else
                {
                    TxbKontaktTelefonNummer.BorderColor = colErr;
                    res = false;
                }
            }
            else
            {
                TxbKontaktTelefonNummer.BorderColor = colNoErr;
            }

            // er der mobil nummer, og er det så et tal?
            if (TxbMobilNummer.Text.Length > 0)
            {
                string input = TxbMobilNummer.Text;
                bool isValid = input.All(c => char.IsDigit(c) || c.Equals('+'));
                if (isValid)
                {
                    TxbMobilNummer.BorderColor = colNoErr;
                }
                else
                {
                    TxbMobilNummer.BorderColor = colErr;
                    res = false;
                }
            }
            else
            {
                TxbMobilNummer.BorderColor = colNoErr;
            }

            return res;
        }
        protected bool Ovrige_rettigheder_is_valid()
        {
            bool res = true;

            // er der valgt ja/nej distributionsliste
            if (RbIsDistributionslister.SelectedIndex == -1)
            {
                errDistributionslister.ForeColor = colErr;
                res = false;
            }
            else
            {
                errDistributionslister.ForeColor = colNoErrLabel;
                // hvis ja, er der så indtastet nogle distributionslister
                if (RbIsDistributionslister.SelectedIndex == 0)
                {
                    if (TxbDistributionslisterNavne.Text.Length > 0)
                    {
                        TxbDistributionslisterNavne.BorderColor = colNoErr;
                    }
                    else
                    {
                        TxbDistributionslisterNavne.BorderColor = colErr;
                        res = false;
                    }
                }
            }

            // er der taget stilling til fællespostkasser? ja/nej
            if (RbIsFaellespostkasser.SelectedIndex == -1)
            {
                errFallespostkasser.ForeColor = colErr;
                res = false;
            }
            else
            {
                errFallespostkasser.ForeColor = colNoErrLabel;
                // hvis ja, er der så indtastet nogle fæælespostkasseR?
                if (RbIsFaellespostkasser.SelectedIndex == 0)
                {
                    if (TxbFaellespostkasserNavne.Text.Length > 0)
                    {
                        TxbFaellespostkasserNavne.BorderColor = colNoErr;
                    }
                    else
                    {
                        TxbFaellespostkasserNavne.BorderColor = colErr;
                        res = false;
                    }
                }
            }
            return res;
        }

        // øvrige programmer er splittet op fordi forretningslogikken er lidt vild i dem
        protected bool Ovrige_programmer_is_valid()
        {
            bool nemid_is_valid = Nemid_is_valid();
            bool cura_is_valid = Cura_is_valid();
            bool KMD = KMD_is_valid();
            bool opus_okonomi_billagsbehandling = Opus_okonomi_billagsbehandling();
            bool opus_lon_personale = Opus_lon_personale();
            return nemid_is_valid && cura_is_valid && KMD && opus_okonomi_billagsbehandling && opus_lon_personale;
        }

        protected bool Nemid_is_valid()
        {
            bool res = true;
            // er der taget stilling til nemid?
            if (RbIsNemID.SelectedIndex == -1)
            {
                errNemId.ForeColor = colErr;
                res = false;
            }
            else
            {
                errNemId.ForeColor = colNoErrLabel;
                // hvis der bestilles nemid skal medarbejderen også have en email
                if (RbIsNemID.SelectedIndex == 0)
                {
                    if (RBIsEmail.SelectedIndex != 0)
                    {
                        ErrEmail.Text = "Skal der oprettes e-mail til medarbejderen? - kræves når der bestilles nemid";
                        ErrEmail.ForeColor = colErr;
                        res = false;
                    }
                    else
                    {
                        ErrEmail.Text = "Skal der oprettes e-mail til medarbejderen?";
                        ErrEmail.ForeColor = colNoErrLabel;
                    }

                    // der skal også indtastes et ean
                    if (TxbEan.Text.Length > 5)
                    {
                        TxbEan.BorderColor = colNoErr;
                    }
                    else
                    {
                        TxbEan.BorderColor = colErr;
                        res = false;
                    }
                }
            }
            return res;
        }

        protected bool Cura_is_valid()
        {
            bool res = true;

            // er der taget stilling til cura?
            if (RbIsCura.SelectedIndex == -1)
            {
                errCura.ForeColor = colErr;
                res = false;
            }
            else
            {
                errCura.ForeColor = colNoErrLabel;
                // hvis ja er der taget stilling til cura rolle?
                if (RbIsCura.SelectedIndex == 0)
                {
                    if (DdlCuraBrugerRolle.SelectedValue.Equals("X"))
                    {
                        DdlCuraBrugerRolle.BorderColor = colErr;
                        DdlCuraBrugerRolle.BorderWidth = 1;
                        res = false;
                    }
                    else
                    {
                        DdlCuraBrugerRolle.BorderColor = colNoErr;
                    }

                    // er der taget stilling til cura planlægger?
                    if (RbIsCuraPlanner.SelectedIndex == -1)
                    {
                        errCuraPlaner.ForeColor = colErr;
                        RbIsCuraPlanner.ForeColor = colErr;
                        res = false;
                    }
                    else
                    {
                        errCuraPlaner.ForeColor = colNoErrLabel;
                        RbIsCuraPlanner.ForeColor = colNoErr;
                    }


                    // er der taget stilling til FMK?
                    if (RbIsCuraFMK.SelectedIndex == -1)
                    {
                        errRbIsCuraFMK.ForeColor = colErr;
                        RbIsCuraFMK.ForeColor = colErr;
                        res = false;
                    }
                    else
                    {
                        errRbIsCuraFMK.ForeColor = colNoErr;
                        RbIsCuraFMK.ForeColor = colNoErrLabel;
                        // hvis brugeren skal have FMK, skal FMK authentication indtastes
                        // der skal også bestilles nemid
                        if (RbIsCuraFMK.SelectedIndex == 0)
                        {
                            // authentication er indtastet
                            if (TxbCuraFMKID.Text.Length > 0)
                            {
                                TxbCuraFMKID.BorderColor = colNoErr;
                            }
                            else
                            {
                                TxbCuraFMKID.BorderColor = colErr;
                                res = false;
                            }

                            //nemid er valgt
                            if (RbIsNemID.SelectedIndex == 0)
                            {
                                errNemId.Text = "NemID";
                                errNemId.ForeColor = colNoErrLabel;
                            }
                            else
                            {
                                errNemId.Text = "NemID - krævet når der bestilles FMK";
                                errNemId.ForeColor = colErr;
                                res = false;
                            }
                        }
                    }

                }

                // er der valgt cura login organisaitoner?
                if(grid_curaLOrg.Items.Count > 0)
                {
                    errCuraLOrg.ForeColor = colNoErrLabel;
                    ddl_curaLOrg.BorderColor = colNoErr;
                }
                else
                {
                    errCuraLOrg.ForeColor = colErr;
                    ddl_curaLOrg.BorderColor = colErr;
                    ddl_curaLOrg.BorderWidth = 1;
                    res = false;
                }
            }
            return res;
        }

        protected bool KMD_is_valid()
        {
            bool res = true;
            // er der taget stilling til KMD brugere?
            if (RbIsKMDbruger.SelectedIndex == -1)
            {
                errKMDBruger.ForeColor = colErr;
                res = false;
            }
            else
            {
                errKMDBruger.ForeColor = colNoErrLabel;
                // hvis ja, skal der indtastes kmd systemer
                if (RbIsKMDbruger.SelectedIndex == 0)
                {
                    if (TxbKMDUserProfiles.Text.Length > 0)
                    {
                        TxbKMDUserProfiles.BorderColor = colNoErr;
                    }
                    else
                    {
                        TxbKMDUserProfiles.BorderColor = colErr;
                        res = false;
                    }
                }
            }
            return res;
        }

        protected bool Opus_okonomi_billagsbehandling()
        {
            bool res = true;
            // er der taget stilling til opus økonomi?
            if (RbIsKMDOpusOkonomiBilagsbehandling.SelectedIndex == -1)
            {
                errOpusOko.ForeColor = colErr;
                res = false;
            }
            else
            {
                errOpusOko.ForeColor = colNoErrLabel;
                // er der sagt ja?
                if (RbIsKMDOpusOkonomiBilagsbehandling.SelectedIndex == 0)
                {
                    // er der indtastet profit center?
                    if (TxbKMDProfitcenterOmkostningssted.Text.Length > 0)
                    {
                        TxbKMDProfitcenterOmkostningssted.BorderColor = colNoErr;
                    }
                    else
                    {
                        TxbKMDProfitcenterOmkostningssted.BorderColor = colErr;
                        res = false;
                    }

                    // er der taget stilling til faktura? RBtnFaktura
                    if (RbIsKMDFaktura.SelectedIndex == -1)
                    {
                        errFaktura.ForeColor = colErr;
                        res = false;
                    }
                    else
                    {
                        errFaktura.ForeColor = colNoErrLabel;
                        // er der sagt ja til faktura?
                        if (RbIsKMDFaktura.SelectedIndex == 0)
                        {
                            // er der indtastet ean?
                            if (TxbKMDOpusOkonomiEan.Text.Length > 0)
                            {
                                TxbKMDOpusOkonomiEan.BorderColor = colNoErr;
                            }
                            else
                            {
                                TxbKMDOpusOkonomiEan.BorderColor = colErr;
                                res = false;
                            }
                        }
                    }

                    // er der taget stilling til budget og omplacering?
                    if (RbIsKMDBudgetOmplacering.SelectedIndex == -1)
                    {
                        errBudgetOmplacering.ForeColor = colErr;
                        res = false;
                    }
                    else
                    {
                        errBudgetOmplacering.ForeColor = colNoErrLabel;
                    }

                    // er der taget stilling til mit forventede regnskab
                    if (RbIsKMDMitForventedeRegnskab.SelectedIndex == -1)
                    {
                        errMitForventedeRegnskab.ForeColor = colErr;
                        res = false;
                    }
                    else
                    {
                        errMitForventedeRegnskab.ForeColor = colNoErrLabel;
                    }
                }
            }
            return res;
        }

        protected bool Opus_lon_personale()
        {
            bool res = true;

            // er der taget stilling til opus løn og personalle (fravær)?
            if (RbIsKMDLoenOgPersonale.SelectedIndex == -1)
            {
                errOpusLonPersonale.ForeColor = colErr;
                res = false;
            }
            else
            {
                errOpusLonPersonale.ForeColor = colNoErrLabel;
                // hvis ja
                if (RbIsKMDLoenOgPersonale.SelectedIndex == 0)
                {
                    // er der udfyldt orgunit?
                    if (TxbKMDOrgUnit.Text.Length > 0)
                    {
                        TxbKMDOrgUnit.BorderColor = colNoErr;
                    }
                    else
                    {
                        TxbKMDOrgUnit.BorderColor = colErr;
                        res = false;
                    }
                }
            }
            return res;
        }


        protected void RbIsSkype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsSkype.SelectedIndex == 0)
                panelSkype.Visible = true;
            else
            {
                panelSkype.Visible = false;
                RbIsRingegruppe.SelectedIndex = -1;
                TxbRingegruppeNummer.Text = "";
            }
        }

        protected void RbIsRingegruppe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsRingegruppe.SelectedIndex == 0)
                panelRingeGrupppe.Visible = true;
            else
            {
                panelRingeGrupppe.Visible = false;
                TxbRingegruppeNummer.Text = "";
            }
        }

        protected void RbIsDistributionslister_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsDistributionslister.SelectedIndex == 0)
                panelDistributionslister.Visible = true;
            else
            {
                panelDistributionslister.Visible = false;
                TxbDistributionslisterNavne.Text = "";
            }
        }

        protected void RbIsFaellespostkasser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsFaellespostkasser.SelectedIndex == 0)
                panelFallespostkasser.Visible = true;
            else
            {
                panelFallespostkasser.Visible = false;
                TxbFaellespostkasserNavne.Text = "";
            }
        }

        protected void RbIsNemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsNemID.SelectedIndex == 0)
                panelNemId.Visible = true;
            else
            {
                panelNemId.Visible = false;
                TxbEan.Text = "";
            }
        }

        protected void RbIsCura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsCura.SelectedIndex == 0)
            {
                panelCura.Visible = true;
                panelCuraORGLOG.Visible = true;
                panelCuraPlanner.Visible = true;
                panelCuraFMK.Visible = true;
            }
            else
            {
                panelCura.Visible = false;
                panelCuraORGLOG.Visible = false;
                DdlCuraBrugerRolle.SelectedIndex = -1;
                RbIsCuraPlanner.SelectedIndex = -1;
                RbIsCuraFMK.SelectedIndex = -1;
                TxbCuraFMKID.Text = "";
                panelCuraPlanner.Visible = false;
                panelCuraFMK.Visible = false;
            }
        }

        protected void RbIsCuraFMK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsCuraFMK.SelectedIndex == 0)
                panelCuraFMKID.Visible = true;
            else
            {
                TxbCuraFMKID.Text = "";
                panelCuraFMKID.Visible = false;
            }
        }

        protected void RbIsKMDbruger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsKMDbruger.SelectedIndex == 0)
                panelKMD.Visible = true;
            else
            {
                TxbKMDUserProfiles.Text = "";
                panelKMD.Visible = false;
            }
        }

        protected void RbIsKMDOpusOkonomiBilagsbehandling_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsKMDOpusOkonomiBilagsbehandling.SelectedIndex == 0)
                panelOpusOko.Visible = true;
            else
            {
                TxbKMDProfitcenterOmkostningssted.Text = "";
                TxbKMDOpusOkonomiEan.Text = "";
                RbIsKMDFaktura.SelectedIndex = -1;
                RbIsKMDBudgetOmplacering.SelectedIndex = -1;
                RbIsKMDMitForventedeRegnskab.SelectedIndex = -1;
                panelOpusOko.Visible = false;
                panelFaktura.Visible = false;
            }
        }

        protected void RbIsKMDFaktura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsKMDFaktura.SelectedIndex == 0)
                panelFaktura.Visible = true;
            else
            {
                TxbKMDOpusOkonomiEan.Text = "";
                panelFaktura.Visible = false;
            }
        }

        protected void RbIsKMDLoenOgPersonale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbIsKMDLoenOgPersonale.SelectedIndex == 0)
                panelOpusLonPersonale.Visible = true;
            else
            {
                TxbKMDOrgUnit.Text = "";
                panelOpusLonPersonale.Visible = false;
            }
        }

        protected void btn_add_curaLOrg_Click(object sender, EventArgs e)
        {
            if (ddl_curaLOrg.SelectedIndex != -1 && ddl_curaLOrg.SelectedValue != "X")
            {
                List<string> curaLOrgs = new List<string>();
                foreach (GridDataItem item in grid_curaLOrg.Items)
                {
                    curaLOrgs.Add(item.GetDataKeyValue("curaLOrg").ToString());
                }
                if (!curaLOrgs.Contains(ddl_curaLOrg.SelectedText))
                {
                    curaLOrgs.Add(ddl_curaLOrg.SelectedText);
                }
                LoadCuraGrid(curaLOrgs);
            }
        }

        protected void grid_curaLOrg_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                List<string> curaLOGORGs = new List<string>();
                foreach (GridDataItem item in grid_curaLOrg.Items)
                {
                    curaLOGORGs.Add(item.GetDataKeyValue("curaLOrg").ToString());
                }
                GridDataItem currentItem = (GridDataItem)e.Item;
                curaLOGORGs.Remove(currentItem.GetDataKeyValue("curaLOrg").ToString());
                LoadCuraGrid(curaLOGORGs);
            }
        }

        private void LoadCuraGrid(List<string> curaLOrgs)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("curaLOrg", typeof(string));
            foreach (string logorg in curaLOrgs)
                dt.Rows.Add(logorg);
            grid_curaLOrg.DataSource = dt;
            grid_curaLOrg.Rebind();
        }
    }
}