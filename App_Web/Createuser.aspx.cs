using App_Web.Services;
using App_Web.SofdCoreAPI_WebService;
using DAL.ITBrugeroprettelse.Data;
using DAL.RPA_CuraBrugere.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace App_Web
{
	public partial class Createuser : System.Web.UI.Page
	{
		private CreateUserService service = new CreateUserService();
		private System.Drawing.Color colErr = System.Drawing.Color.Red;
		private System.Drawing.Color colNoErr = System.Drawing.Color.LightGray;
		private System.Drawing.Color colNoErrLabel = System.Drawing.Color.Black;

		private DAL.ITBrugeroprettelse.Data.IRepo<Skolekode> skolekodeRepo = new SkolekodeRepo(Properties.Settings.Default.ConnectionStringITBrugeroprettelse);
		private DAL.ITBrugeroprettelse.Data.IRepo<AnsatUdenADBruger> ansatUdenADBrugerRepo = new AnsatUdenADBrugerRepo(Properties.Settings.Default.ConnectionStringITBrugeroprettelse);
		private DAL.RPA_CuraBrugere.Data.IRepo<DisabledGroupName> disabledGroupNameRepo = new DisabledGroupNamesRepo(Properties.Settings.Default.ConnectionStringCuraBrugere);

		private static List<AnsatUdenADBruger> employeesWithoutADUser;
		//private static List<EmployeeAffiliationWithoutADUser> employeesWithoutADUser;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadCuraRoles();
				LoadCuraLOrg();
				LoadComboSkole(ComboMUElev_Skolekode);

				List<AnsatUdenADBruger> employeeList = LoadUserWithoutADUserList();
				//List<EmployeeAffiliationWithoutADUser> employeeList = LoadUserWithoutADUserList();
				Application["Employee"] = employeeList;
				employeesWithoutADUser = employeeList;
			}
		}

		private List<AnsatUdenADBruger> LoadUserWithoutADUserList()
		{


			List<AnsatUdenADBruger> empList = ansatUdenADBrugerRepo.List.ToList();

			empList = empList.OrderBy(emp => emp.PersonFirstname).ThenBy(em => em.PersonSurname).ToList();

			//List<EmployeeAffiliationWithoutADUser> employeeList = ws.GetPersonsWithoutADUsers(sofdCoreApiKey).OrderBy(e => e.PersonFirstname).ThenBy(em => em.PersonSurname).ToList();

			return empList;
		}

		private List<EmployeeAffiliationWithoutADUser> LoadUserWithoutADUserList_SofdService()
		{
			string sofdCoreApiKey = Properties.Settings.Default.SofdCoreApiKey;
			SofdCoreAPI_WebService.SofdCoreAPI_WebService ws = new SofdCoreAPI_WebService.SofdCoreAPI_WebService();

			List<EmployeeAffiliationWithoutADUser> empList = ws.GetPersonsWithoutADUsers(sofdCoreApiKey).ToList();

			empList = empList.OrderBy(emp => emp.PersonFirstname).ThenBy(em => em.PersonSurname).ToList();

			//List<EmployeeAffiliationWithoutADUser> employeeList = ws.GetPersonsWithoutADUsers(sofdCoreApiKey).OrderBy(e => e.PersonFirstname).ThenBy(em => em.PersonSurname).ToList();

			return empList;
		}

		private List<string> GetDisabledCuraRoleNameList()
		{
			string search = "CuraRoller";
			List<DisabledGroupName> disabledGroupList = disabledGroupNameRepo.List.Where(a => a.ADGroupName.StartsWith(search)).ToList();

			List<string> disabledList = new List<string>();

			foreach (DisabledGroupName disabledGroup in disabledGroupList)
			{
				string name = disabledGroup.ADGroupName.Replace(search, "");
				name = name.Trim();

				disabledList.Add(name);
			}

			return disabledList;
		}

		public void LoadComboSkole(RadComboBox combo)
		{
			combo.Items.Clear();
			List<Skolekode> list = GetSkolekodeList();

			foreach (Skolekode skolekode in list)
			{
				RadComboBoxItem comboItem = new RadComboBoxItem();
				comboItem.Value = skolekode.Insttutionsnummer;
				comboItem.Text = skolekode.Skole;

				combo.Items.Add(comboItem);
			}
		}

		private List<Skolekode> GetSkolekodeList()
		{
			List<Skolekode> skolekodeList = skolekodeRepo.List.Where(s => s.Deleted == false).OrderBy(sk => sk.Skole).ToList();
			return skolekodeList;
		}

		protected void LoadCuraRoles()
		{
			List<string> disabledList = GetDisabledCuraRoleNameList();

			DdlCuraBrugerRolle.Items.Clear();
			DdlCuraBrugerRolle.Items.Add(new Telerik.Web.UI.DropDownListItem() { Value = "X", Text = "Vælg rolle" });
			int count = 1;
			foreach (string role in service.GetCuraRoles())
			{
				if (!role.Equals("Ressourceplanlægger"))
				{
					if (!disabledList.Contains(role, StringComparer.OrdinalIgnoreCase))
					{
						DdlCuraBrugerRolle.Items.Add(new Telerik.Web.UI.DropDownListItem() { Value = count.ToString(), Text = role });
						count++;
					}
				}
			}
		}

		private List<string> GetDisabledCuraLOGNameList()
		{
			string search = "CURALOG";
			List<DisabledGroupName> disabledGroupList = disabledGroupNameRepo.List.Where(a => a.ADGroupName.StartsWith(search)).ToList();

			List<string> disabledList = new List<string>();

			foreach (DisabledGroupName disabledGroup in disabledGroupList)
			{
				string name = disabledGroup.ADGroupName.Replace(search, "");
				name = name.Trim();

				disabledList.Add(name);
			}

			return disabledList;
		}

		protected void LoadCuraLOrg()
		{
			List<string> disabledList = GetDisabledCuraLOGNameList();

			ddl_curaLOrg.Items.Clear();
			ddl_curaLOrg.Items.Add(new Telerik.Web.UI.DropDownListItem() { Value = "X", Text = "Vælg Login Organisation" });
			int count = 1;
			foreach (string lorg in service.GetCuraLOrg())
			{
				if (!disabledList.Contains(lorg, StringComparer.OrdinalIgnoreCase))
				{
					ddl_curaLOrg.Items.Add(new Telerik.Web.UI.DropDownListItem() { Value = count.ToString(), Text = lorg });
					count++;
				}
			}
		}

		private void LoadEmployeesWithNoADUser()
		{

			RadComboBox combo = CBOpus_medarbejdernr;
			string sofdCoreApiKey = Properties.Settings.Default.SofdCoreApiKey;

			SofdCoreAPI_WebService.SofdCoreAPI_WebService ws = new SofdCoreAPI_WebService.SofdCoreAPI_WebService();

			List<EmployeeAffiliationWithoutADUser> employeeList = ws.GetPersonsWithoutADUsers(sofdCoreApiKey).OrderBy(e => e.PersonFirstname).ThenBy(e => e.PersonSurname).ToList();

			foreach (EmployeeAffiliationWithoutADUser employee in employeeList)
			{
				string employeeName = employee.PersonFirstname + " " + employee.PersonSurname;
				string employeeID = employee.EmployeeId;
				string positionName = employee.AffliationPositionName;
				string orgUnitName = employee.OrgUnitName;

				RadComboBoxItem item = new RadComboBoxItem();

				item.Text = employeeName + " -- " + orgUnitName + " -- " + positionName + " -- MedNR: " + employeeID;
				item.Value = employeeID;

				combo.Items.Add(item);
			}
		}

		protected void CBOpus_medarbejdernr_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			int opus_id;
			if (int.TryParse(CBOpus_medarbejdernr.SelectedValue, out opus_id))
			{
				Dictionary<string, string> names = service.GetEmployeeNamesFromDB(opus_id, employeesWithoutADUser); //GetEmployeeNames(opus_id);
				TxbFornavn.Text = names["Firstname"];
				TxbEfternavn.Text = names["Lastname"];
				TxbVistnavn.Text = names["Fullname"];
				LblStilling.Text = names["Position"];
			}
		}

		private string GetKmdRollebaseretProfil()
		{
			string profil = "";
			if (ComboRollebaseret.SelectedItem != null)
			{
				profil = ComboRollebaseret.Text;
			}
			return profil;
		}

		private string GetKmdRollebaseretLederBogfoer()
		{
			string lederBogfoering = "";

			if (RbRollebaseretFaktura_Leder.SelectedItem != null)
			{
				lederBogfoering = RbRollebaseretFaktura_Leder.SelectedItem.Text;
			}

			return lederBogfoering;
		}

		private string GetKmdRollebaseretProfitcenterBogfoer()
		{
			string profitcenter = "";

			if (ComboRollebaseret.SelectedValue == "LEDER")
			{
				profitcenter = TxbRollebaseretProfitcenter_LederBogfoering.Text;
			}
			else if (ComboRollebaseret.SelectedValue == "ADMINISTRATIV")
			{
				profitcenter = TxbOpusOpg_Administrativ_Profitcenter.Text;
			}

			return profitcenter;
		}

		private string GetRollebaseretEAN()
		{
			string ean = "";

			if (ComboRollebaseret.SelectedValue == "LEDER")
			{
				ean = TxbRollebaseret_LederBogfoeringEAN.Text;
			}
			else if (ComboRollebaseret.SelectedValue == "ADMINISTRATIV")
			{
				ean = TxbRollebaseretAdmMedarb_EAN.Text;
			}

			return ean;
		}

		private string GetRollebaseretOrgEnhed()
		{
			string orgEnhed = "";

			if (ComboRollebaseret.SelectedValue == "LEDER")
			{
				orgEnhed = TxbRollebaseret_SeAndre_OrgEnhed.Text;
			}
			else if (ComboRollebaseret.SelectedValue == "ADMINISTRATIV")
			{
				orgEnhed = TxbRollebaseretAdmMedarb_OrgEnh.Text;
			}

			return orgEnhed;
		}

		private string GetRollebaseretRettighed()
		{
			string rettighed = "";

			if (ComboRollebaseret.SelectedValue == "ADMINISTRATIV")
			{
				rettighed = TxbRollebaseretAdmMedarb_Andet.Text;
			}
			else if (ComboRollebaseret.SelectedValue == "ANDET")
			{
				rettighed = TxbRollebaseretAndet.Text;
			}
			return rettighed;
		}

		protected void Button_submit_Click(object sender, EventArgs e)
		{
			//TEST
			//int index = RblIsMUElev.SelectedIndex;
			//string skole = TxtBoxMUElev_Skolekode.Text;
			//string value = RblMUElev_Rolle.SelectedValue;
			//string pos = LblStilling.Text;
			//string val = RbEHandelBrugerType.SelectedValue;
			//string op = GetEhandelRoleSelectionText();
			string val = ComboMUElev_Skolekode.SelectedValue;

			if (IsPageValid())
			{
				string errMessage;
				string curaLoginORGs = GetCuraLoginORGs();
				string curaRoller = GetCuraRoles();


				bool result = service.CreateUser(CBOpus_medarbejdernr.SelectedValue,
												TxbFornavn.Text,
												TxbEfternavn.Text,
												TxbVistnavn.Text,
												LblStilling.Text,
												CbCoworkerSamaccount.SelectedValue,
												RbIsSkype.SelectedIndex == 0,
												RbIsRingegruppe.SelectedIndex == 0,
												TxbRingegruppeNummer.Text,
												TxbMobilNummer.Text,
												RbIsDistributionslister.SelectedIndex == 0,
												TxbDistributionslisterNavne.Text,
												RbIsFaellespostkasser.SelectedIndex == 0,
												TxbFaellespostkasserNavne.Text,

												RblIsMUElev.SelectedIndex == 0,
												ComboMUElev_Skolekode.SelectedValue,
												//TxtBoxMUElev_Skolekode.Text,
												RblMUElev_Rolle.SelectedValue,

												RbIsCura.SelectedIndex == 0,
												curaRoller,
												RbIsCuraPlanner.SelectedIndex == 0,
												RbIsCuraFMK.SelectedIndex == 0,
												TxbCuraFMKID.Text,
												RbIsKMDbruger.SelectedIndex == 0,
												TxbKMDUserProfiles.Text,

												RbIsRollebaseret.SelectedIndex == 0,
												GetKmdRollebaseretProfil(),
												TxbRollebaseretProfitcenterAnsvar.Text,
												GetKmdRollebaseretLederBogfoer(),
												GetKmdRollebaseretProfitcenterBogfoer(),
												GetRollebaseretEAN(),
												GetRollebaseretOrgEnhed(),
												GetRollebaseretRettighed(),

												RbIsKmdInstitution.SelectedIndex == 0,
												GetKmdInstitutionProfil(),
												TxtBoxKmdInsttution_Institutionsnummer.Text,

												RbIsTargit.SelectedIndex == 0,
												RbIsEducaPersonale.SelectedIndex == 0,
												TxbEducaPersonale_unilogin.Text,
												TxbEducaPersonale_skolekode.Text,
												RbEducaPersonale_Role.SelectedValue,
												RBisEhandel.SelectedIndex == 0,
												GetEhandelRoleSelectionText(),
												TxbEHandelProfitCenter.Text,

												TxbBemaerkninger.Text,
												curaLoginORGs,
												out errMessage); ;
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

		private string GetEhandelRoleSelectionText()
		{
			string role = "";

			if (RbEHandelBrugerType.SelectedItem != null)
			{
				role = RbEHandelBrugerType.SelectedItem.Text;
			}
			return role;
		}

		private string GetCuraLoginORGs()
		{
			string fakeArr = "";
			foreach (GridDataItem item in grid_curaLOrg.Items)
			{
				fakeArr += item.GetDataKeyValue("curaLOrg").ToString() + ";";
			}
			return fakeArr;
		}

		private string GetCuraRoles()
		{
			string fakeArr = "";
			foreach (GridDataItem item in grid_curaRoles.Items)
			{
				fakeArr += item.GetDataKeyValue("curaRole").ToString() + ";";
			}
			return fakeArr;
		}

		protected bool IsPageValid()
		{
			bool oplysninger_om_medarbejder = Oplysninger_om_medarbejder_is_valid();
			bool oplysninger_om_medarbejderens_position = Oplysninger_om_medarbejderens_position_is_valid();
			bool oplysninger_om_medarbejderens_telefoni = Oplysninger_om_medarbejderens_telefoni_is_valid();
			bool ovrige_rettigheder = Ovrige_rettigheder_is_valid();
			bool ovrige_programmer = Ovrige_programmer_is_valid();
			return oplysninger_om_medarbejder && oplysninger_om_medarbejderens_position && oplysninger_om_medarbejderens_telefoni
				&& ovrige_rettigheder && ovrige_programmer;
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
				if (service.IsEmployeeInDB(opus_id, employeesWithoutADUser))
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
			if (TxbVistnavn.Text.Length > 5 && !TxbVistnavn.Text.Contains("@"))
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
			//if (CbManagerSamaccount.SelectedValue.Length == 6)
			//{
			//    CbManagerSamaccount.BorderColor = colNoErr;
			//}
			//else
			//{
			//    CbManagerSamaccount.BorderWidth = 1;
			//    CbManagerSamaccount.BorderColor = colErr;
			//    res = false;
			//}

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
			//if (RBIsEmail.SelectedIndex == -1)
			//{
			//    ErrEmail.ForeColor = colErr;
			//    res = false;
			//}
			//else
			//{
			//    ErrEmail.ForeColor = colNoErrLabel;
			//}

			return res;
		}

		protected bool Oplysninger_om_medarbejderens_telefoni_is_valid()
		{
			bool res = true;

			// er der taget stilling til skype?
			if (RbIsSkype.SelectedIndex == -1)
			{
				ErrorSkype.ForeColor = colErr;
				//ErrorSkype.Style.Add("ForeColor", "Red");
				res = false;
			}
			else
			{
				ErrorSkype.ForeColor = colNoErrLabel;
				//ErrorSkype.Style.Add("ForeColor", "Black");
				// hvis ja
				if (RbIsSkype.SelectedIndex == 0)
				{

					// men er der så taget højde for ringe/svargruppe
					if (RbIsRingegruppe.SelectedIndex == -1)
					{
						ErrorRingGruppe.ForeColor = colErr;
						res = false;
					}
					else
					{
						ErrorRingGruppe.ForeColor = colNoErrLabel;
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
			//if (TxbKontaktTelefonNummer.Text.Length > 0)
			//{
			//    string input = TxbKontaktTelefonNummer.Text;
			//    bool isValid = input.Length == 8 && input.All(char.IsDigit);
			//    if (isValid)
			//    {
			//        TxbKontaktTelefonNummer.BorderColor = colNoErr;
			//    }
			//    else
			//    {
			//        TxbKontaktTelefonNummer.BorderColor = colErr;
			//        res = false;
			//    }
			//}
			//else
			//{
			//    TxbKontaktTelefonNummer.BorderColor = colNoErr;
			//}

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
				ErrorDistributionslister.ForeColor = colErr;
				res = false;
			}
			else
			{
				ErrorDistributionslister.ForeColor = colNoErrLabel;
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
				ErrorFallespostkasser.ForeColor = colErr;
				res = false;
			}
			else
			{
				ErrorFallespostkasser.ForeColor = colNoErrLabel;
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

			//ER der taget stilling til MU Elev
			if (RblIsMUElev.SelectedIndex == -1)
			{
				ErrorMUElev.ForeColor = colErr;

			}
			else
			{
				ErrorMUElev.ForeColor = colNoErrLabel;

				if (RblIsMUElev.SelectedIndex == 0)
				{
					if (ComboMUElev_Skolekode.SelectedValue != string.Empty)
					{
						ComboMUElev_Skolekode.BorderColor = colNoErr;
					}
					else
					{
						ComboMUElev_Skolekode.BorderWidth = 1;
						ComboMUElev_Skolekode.BorderColor = colErr;
						res = false;
					}

					if (RblMUElev_Rolle.SelectedIndex == -1)
					{
						ErrorMUElev_Rolle.ForeColor = colErr;
						res = false;
					}
					else
					{
						ErrorMUElev_Rolle.ForeColor = colNoErrLabel;
					}
				}
			}

			return res;
		}

		// øvrige programmer er splittet op fordi forretningslogikken er lidt vild i dem
		protected bool Ovrige_programmer_is_valid()
		{
			//bool nemid_is_valid = Nemid_is_valid();
			bool cura_is_valid = Cura_is_valid();
			bool KMD = KMD_is_valid();
			bool opus_okonomi_billagsbehandling = Opus_okonomi_billagsbehandling();
			//bool opus_lon_personale = Opus_lon_personale();
			bool targit_is_valid = Targit_is_valid();
			//bool kmdelev_is_valid = KMDelev_is_valid();
			bool educaPersonale_isValid = EducaPersonale_isValid();
			bool rakat_isValid = Rakat_isValid();
			bool rollebaseret_IsValid = Rollebaseret_isValid();
			bool kmdInstitution_IsValid = KmdInstitutionIsValid();
			return cura_is_valid && KMD && opus_okonomi_billagsbehandling //&& opus_lon_personale
				&& targit_is_valid && educaPersonale_isValid && rakat_isValid
				&& rollebaseret_IsValid && kmdInstitution_IsValid;
		}

		//protected bool Nemid_is_valid()
		//{
		//    bool res = true;
		//    // er der taget stilling til nemid?
		//    if (RbIsNemID.SelectedIndex == -1)
		//    {
		//        ErrorNemId.ForeColor = colErr;
		//        res = false;
		//    }
		//    else
		//    {
		//        ErrorNemId.ForeColor = colNoErrLabel;
		//        // hvis der bestilles nemid skal medarbejderen også have en email
		//        if (RbIsNemID.SelectedIndex == 0)
		//        {
		//            //if (RBIsEmail.SelectedIndex != 0)
		//            //{
		//            //    ErrEmail.Text = "Skal der oprettes e-mail til medarbejderen? - kræves når der bestilles nemid";
		//            //    ErrEmail.ForeColor = colErr;
		//            //    res = false;
		//            //}
		//            //else
		//            //{
		//            //    ErrEmail.Text = "Skal der oprettes e-mail til medarbejderen?";
		//            //    ErrEmail.ForeColor = colNoErrLabel;
		//            //}

		//            // der skal også indtastes et ean
		//            if (TxbEan.Text.Length > 5)
		//            {
		//                TxbEan.BorderColor = colNoErr;
		//            }
		//            else
		//            {
		//                TxbEan.BorderColor = colErr;
		//                res = false;
		//            }
		//        }
		//    }
		//    return res;
		//}

		protected bool Cura_is_valid()
		{
			bool res = true;

			// er der taget stilling til cura?
			if (RbIsCura.SelectedIndex == -1)
			{
				ErrorCura.ForeColor = colErr;
				res = false;
			}
			else
			{
				ErrorCura.ForeColor = colNoErrLabel;
				// hvis ja er der taget stilling til cura rolle?
				if (RbIsCura.SelectedIndex == 0)
				{
					// er der valgt curaroller?
					if (grid_curaRoles.Items.Count > 0)
					{
						ErrorCuraRole.ForeColor = colNoErrLabel;
						DdlCuraBrugerRolle.BorderColor = colNoErr;
					}
					else
					{
						ErrorCuraRole.ForeColor = colErr;
						DdlCuraBrugerRolle.BorderColor = colErr;
						DdlCuraBrugerRolle.BorderWidth = 1;
						res = false;
					}

					// er der taget stilling til cura planlægger?
					if (RbIsCuraPlanner.SelectedIndex == -1)
					{
						ErrorCuraPlaner.ForeColor = colErr;
						RbIsCuraPlanner.ForeColor = colErr;
						res = false;
					}
					else
					{
						ErrorCuraPlaner.ForeColor = colNoErrLabel;
						RbIsCuraPlanner.ForeColor = colNoErr;
					}


					// er der taget stilling til FMK?
					if (RbIsCuraFMK.SelectedIndex == -1)
					{
						ErrorRbIsCuraFMK.ForeColor = colErr;
						RbIsCuraFMK.ForeColor = colErr;
						res = false;
					}
					else
					{
						ErrorRbIsCuraFMK.ForeColor = colNoErr;
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
							//if (RbIsNemID.SelectedIndex == 0)
							//{
							//    ErrorNemId.Text = "NemID";
							//    ErrorNemId.ForeColor = colNoErrLabel;
							//}
							//else
							//{
							//    ErrorNemId.Text = "NemID - krævet når der bestilles FMK";
							//    ErrorNemId.ForeColor = colErr;
							//    res = false;
							//}
						}
					}
					// er der valgt cura login organisaitoner?
					if (grid_curaLOrg.Items.Count > 0)
					{
						ErrorCuraLOrg.ForeColor = colNoErrLabel;
						ddl_curaLOrg.BorderColor = colNoErr;
					}
					else
					{
						ErrorCuraLOrg.ForeColor = colErr;
						ddl_curaLOrg.BorderColor = colErr;
						ddl_curaLOrg.BorderWidth = 1;
						res = false;
					}
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
				ErrorKMDBruger.ForeColor = colErr;
				res = false;
			}
			else
			{
				ErrorKMDBruger.ForeColor = colNoErrLabel;
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
			if (RbIsRollebaseret.SelectedIndex == -1)
			{
				ErrorRollebaseret.ForeColor = colErr;
				res = false;
			}
			else
			{
				ErrorRollebaseret.ForeColor = colNoErrLabel;
				// er der sagt ja?
				if (RbIsRollebaseret.SelectedIndex == 0)
				{

					//// er der taget stilling til faktura? RBtnFaktura
					//if (RbIsKMDFaktura.SelectedIndex == -1)
					//{
					//    errFaktura.ForeColor = colErr;
					//    res = false;
					//}
					//else
					//{
					//    errFaktura.ForeColor = colNoErrLabel;
					//    // er der sagt ja til faktura?
					//    if (RbIsKMDFaktura.SelectedIndex == 0)
					//    {
					//        // er der indtastet ean?
					//        if (TxbKMDOpusOkonomiEan.Text.Length > 0)
					//        {
					//            TxbKMDOpusOkonomiEan.BorderColor = colNoErr;
					//        }
					//        else
					//        {
					//            TxbKMDOpusOkonomiEan.BorderColor = colErr;
					//            res = false;
					//        }
					//    }
					//}

					// er der taget stilling til budget og omplacering?
					//if (RbIsKMDBudgetOmplacering.SelectedIndex == -1)
					//{
					//    errBudgetOmplacering.ForeColor = colErr;
					//    res = false;
					//}
					//else
					//{
					//    errBudgetOmplacering.ForeColor = colNoErrLabel;
					//}

					// er der taget stilling til mit forventede regnskab
					//if (RbIsKMDMitForventedeRegnskab.SelectedIndex == -1)
					//{
					//    errMitForventedeRegnskab.ForeColor = colErr;
					//    res = false;
					//}
					//else
					//{
					//    errMitForventedeRegnskab.ForeColor = colNoErrLabel;
					//}
				}
			}
			return res;
		}

		protected bool Rollebaseret_isValid()
		{
			bool result = true;

			// er der taget stilling Rollebaseret?
			if (RbIsRollebaseret.SelectedIndex == -1)
			{
				ErrorRollebaseret.ForeColor = colErr;
				result = false;
			}
			else
			{
				ErrorRollebaseret.ForeColor = colNoErrLabel;
				// er der sagt ja?
				if (RbIsRollebaseret.SelectedIndex == 0)
				{
					// er der valgt en profil?
					if (ComboRollebaseret.SelectedItem != null)
					{
						ComboRollebaseret.BorderColor = colNoErr;

						if (ComboRollebaseret.SelectedValue == "LEDER")
						{
							result = Rollebaseret_Leder_Isvalid();
						}
						else if (ComboRollebaseret.SelectedValue == "ADMINISTRATIV")
						{
							result = Rollebaseret_Administrativ_Isvalid();
						}
						else if (ComboRollebaseret.SelectedValue == "ANDET")
						{
							result = Rollebaseret_Andet_Isvalid();
						}
					}
					else
					{
						ComboRollebaseret.BorderWidth = 1;
						ComboRollebaseret.BorderColor = colErr;
						result = false;
					}
				}
			}

			return result;
		}

		protected bool KmdInstitutionIsValid()
		{
			bool result = true;

			if (RbIsKmdInstitution.SelectedIndex == -1)
			{
				ErrorKMDInstitution.ForeColor = colErr;
				result = false;
			}
			else
			{
				ErrorKMDInstitution.ForeColor = colNoErrLabel;

				if (RbIsKmdInstitution.SelectedIndex == 0)
				{

					if (ComboKmdInstitution.SelectedItem != null)
					{
						ComboKmdInstitution.BorderColor = colNoErr;

						if (ComboKmdInstitution.SelectedValue == "DAGTILBUD")
						{
							if (TxtBoxKmdInsttution_Institutionsnummer.Text.Length > 0)
							{
								TxtBoxKmdInsttution_Institutionsnummer.BorderColor = colNoErr;
							}
							else
							{
								TxtBoxKmdInsttution_Institutionsnummer.BorderColor = colErr;
								result = false;
							}
						}
					}
					else
					{
						ComboKmdInstitution.BorderWidth = 1;
						ComboKmdInstitution.BorderColor = colErr;
						result = false;
					}
				}

			}

			return result;
		}

		private bool Rollebaseret_Leder_Isvalid()
		{
			bool result = true;

			// er der indtastet Profitcenter for leder?
			if (TxbRollebaseretProfitcenterAnsvar.Text.Length > 0)
			{
				TxbRollebaseretProfitcenterAnsvar.BorderColor = colNoErr;
			}
			else
			{
				TxbRollebaseretProfitcenterAnsvar.BorderColor = colErr;
				result = false;
			}

			// er der taget stilling til kvittering / bogføring?
			if (RbRollebaseretFaktura_Leder.SelectedIndex == -1)
			{
				ErrorRollebaseretLeder.ForeColor = colErr;
				result = false;
			}
			else
			{
				ErrorRollebaseretLeder.ForeColor = colNoErrLabel;
			}

			// er der taget stilling til skal leder se andre?
			if (RbIsLeaderAbleToSeeOther.SelectedIndex == -1)
			{
				ErrorRollebaseretLederSeeOther.ForeColor = colErr;
				result = false;




			}
			else
			{
				ErrorRollebaseretLederSeeOther.ForeColor = colNoErrLabel;

				if (RbIsLeaderAbleToSeeOther.SelectedIndex == 0)
				{
					// er der indtastet ?
					if (TxbRollebaseret_SeAndre_OrgEnhed.Text.Length > 0)
					{
						TxbRollebaseret_SeAndre_OrgEnhed.BorderColor = colNoErr;
					}
					else
					{
						TxbRollebaseret_SeAndre_OrgEnhed.BorderColor = colErr;
						result = false;
					}
				}
			}

			return result;
		}

		private bool Rollebaseret_Administrativ_Isvalid()
		{
			bool result = true;

			if (BtnRollebaseretAdmMedarb_OekonomiOpg.Checked == false && BtnRollebaseretAdmMedarb_LoenPersonale.Checked == false && BtnRollebaseretAdmMedarb_Andet.Checked == false)
			{
				ErrorRollebaseretAdministrativOpg.ForeColor = colErr;
				result = false;
			}
			else
			{
				ErrorRollebaseretAdministrativOpg.ForeColor = colNoErrLabel;

				if (BtnRollebaseretAdmMedarb_OekonomiOpg.Checked)
				{
					// er der indtastet profitcenter?
					if (TxbOpusOpg_Administrativ_Profitcenter.Text.Length > 0)
					{
						TxbOpusOpg_Administrativ_Profitcenter.BorderColor = colNoErr;
					}
					else
					{
						TxbOpusOpg_Administrativ_Profitcenter.BorderColor = colErr;
						result = false;
					}

					if (TxbRollebaseretAdmMedarb_EAN.Text.Length > 0)
					{
						TxbRollebaseretAdmMedarb_EAN.BorderColor = colNoErr;
					}
					else
					{
						TxbRollebaseretAdmMedarb_EAN.BorderColor = colErr;
						result = false;
					}
				}

				if (BtnRollebaseretAdmMedarb_LoenPersonale.Checked)
				{
					// er der indtastet profitcenter?
					if (TxbRollebaseretAdmMedarb_OrgEnh.Text.Length > 0)
					{
						TxbRollebaseretAdmMedarb_OrgEnh.BorderColor = colNoErr;
					}
					else
					{
						TxbRollebaseretAdmMedarb_OrgEnh.BorderColor = colErr;
						result = false;
					}
				}

				if (BtnRollebaseretAdmMedarb_Andet.Checked)
				{
					// er der indtastet profitcenter?
					if (TxbRollebaseretAdmMedarb_Andet.Text.Length > 0)
					{
						TxbRollebaseretAdmMedarb_Andet.BorderColor = colNoErr;
					}
					else
					{
						TxbRollebaseretAdmMedarb_Andet.BorderColor = colErr;
						result = false;
					}
				}
			}


			return result;
		}

		private bool Rollebaseret_Andet_Isvalid()
		{
			bool result = true;


			// er der indtastet andet?
			if (TxbRollebaseretAndet.Text.Length > 0)
			{
				TxbRollebaseretAndet.BorderColor = colNoErr;
			}
			else
			{
				TxbRollebaseretAndet.BorderColor = colErr;
				result = false;
			}



			return result;
		}

		//protected bool Opus_lon_personale()
		//{
		//    bool res = true;

		//    // er der taget stilling til opus løn og personalle (fravær)?
		//    if (RbIsKMDLoenOgPersonale.SelectedIndex == -1)
		//    {
		//        errOpusLonPersonale.ForeColor = colErr;
		//        res = false;
		//    }
		//    else
		//    {
		//        errOpusLonPersonale.ForeColor = colNoErrLabel;
		//        // hvis ja
		//        if (RbIsKMDLoenOgPersonale.SelectedIndex == 0)
		//        {
		//            // er der udfyldt orgunit?
		//            if (TxbKMDOrgUnit.Text.Length > 0)
		//            {
		//                TxbKMDOrgUnit.BorderColor = colNoErr;
		//            }
		//            else
		//            {
		//                TxbKMDOrgUnit.BorderColor = colErr;
		//                res = false;
		//            }
		//        }
		//    }
		//    return res;
		//}

		protected bool Targit_is_valid()
		{
			bool res = true;
			// er der taget stilling til nemid?
			if (RbIsTargit.SelectedIndex == -1)
			{
				ErrorTargit.ForeColor = colErr;
				res = false;
			}
			else
			{
				ErrorTargit.ForeColor = colNoErrLabel;
			}
			return res;
		}

		//protected bool KMDelev_is_valid()
		//{
		//    bool res = true;
		//    // er der taget stilling til nemid?
		//    if (RbIsKMDElev.SelectedIndex == -1)
		//    {
		//        ErrorKMDelev.ForeColor = colErr;
		//        res = false;
		//    }
		//    else
		//    {
		//        ErrorKMDelev.ForeColor = colNoErrLabel;
		//    }
		//    return res;
		//}


		protected bool EducaPersonale_isValid()
		{

			bool res = true;
			// er der taget stilling til educapersonale?
			if (RbIsEducaPersonale.SelectedIndex == -1)
			{
				ErrorEducaPersonale.ForeColor = colErr;
				res = false;
			}
			else
			{
				if (RbIsEducaPersonale.SelectedIndex == 0)
				{
					ErrorEducaPersonale.ForeColor = colNoErrLabel;
					if (TxbEducaPersonale_unilogin.Text.Length > 0)
					{
						TxbEducaPersonale_unilogin.BorderColor = colNoErr;

					}
					else
					{
						TxbEducaPersonale_unilogin.BorderColor = colErr;
						res = false;
					}
					if (TxbEducaPersonale_skolekode.Text.Length > 0)
					{
						TxbEducaPersonale_skolekode.BorderColor = colNoErr;
					}
					else
					{
						TxbEducaPersonale_skolekode.BorderColor = colErr;
						res = false;
					}
					if (RbEducaPersonale_Role.SelectedIndex == -1)
					{
						ErrorEducaPersonale_Role.ForeColor = colErr;
						res = false;
					}
					else
					{
						ErrorEducaPersonale_Role.ForeColor = colNoErrLabel;
					}
				}
			}
			return res;
		}

		protected bool Rakat_isValid()
		{
			bool res = true;
			if (RBisEhandel.SelectedIndex == -1)
			{
				ErrorRBIsRakat.ForeColor = colErr;
				res = false;
			}
			else
			{
				ErrorRBIsRakat.ForeColor = colNoErrLabel;
				if (RBisEhandel.SelectedIndex == 0)
				{
					if (RbEHandelBrugerType.SelectedIndex == -1)
					{
						ErrorRbRakatRole.ForeColor = colErr;
						res = false;
					}
					else
					{
						ErrorRbRakatRole.ForeColor = colNoErrLabel;
						//if (RBIsEmail.SelectedIndex != 0)
						//{
						//    ErrEmail.Text = "Skal der oprettes e-mail til medarbejderen? - kræves når der bestilles RAKAT";
						//    ErrEmail.ForeColor = colErr;
						//    res = false;
						//}
						//else
						//{
						//    ErrEmail.Text = "Skal der oprettes e-mail til medarbejderen?";
						//    ErrEmail.ForeColor = colNoErrLabel;
						//}

						if (RbEHandelBrugerType.SelectedValue == "B" || RbEHandelBrugerType.SelectedValue == "D")
						{
							if (TxbEHandelProfitCenter.Text.Length > 0)
							{
								TxbEHandelProfitCenter.BorderColor = colNoErr;
							}
							else
							{
								res = false;
								TxbEHandelProfitCenter.BorderColor = colErr;
							}
						}
						if (RbEHandelBrugerType.SelectedValue == "C")
						{
							//    if (Txb_rakat_konteringsansvarlig.Text.Length > 0)
							//    {
							//        Txb_rakat_konteringsansvarlig.BorderColor = colNoErr;
							//    }
							//    else
							//    {
							//        res = false;
							//        Txb_rakat_konteringsansvarlig.BorderColor = colErr;
							//    }
						}
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

		//protected void RbIsNemID_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    if (RbIsNemID.SelectedIndex == 0)
		//        panelNemId.Visible = true;
		//    else
		//    {
		//        panelNemId.Visible = false;
		//        TxbEan.Text = "";
		//    }
		//}

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
			if (RbIsRollebaseret.SelectedIndex == 0)
			{
				//panelOpusOko.Visible = true;
				PanelRollebaseret.Visible = true;
			}

			else
			{
				//TxbKMDProfitcenterOmkostningssted.Text = "";
				//TxbKMDOpusOkonomiEan.Text = "";
				//RbIsKMDFaktura.SelectedIndex = -1;
				//RbIsKMDBudgetOmplacering.SelectedIndex = -1;
				//RbIsKMDMitForventedeRegnskab.SelectedIndex = -1;
				//panelOpusOko.Visible = false;
				//panelFaktura.Visible = false;


				PanelRollebaseret.Visible = false;
				ComboRollebaseret.ClearSelection();

				HideAndResetRollebaseret_Leder();
				HideAndResetRollebaseret_Administrativ();
				HideAndResetRollebaseret_Andet();

				//PanelRollebaseret_Leder.Visible = false;
				//PanelRollebaseret_LederBogfoering.Visible = false;
				//TxbRollebaseretProfitcenter_Leder.Text = "";
				//RbRollebaseretFaktura_Leder.ClearSelection();
				//PanelRollebaseret_LederBogfoering.Visible = false;
				//TxbRollebaseretProfitcenter_LederBogfoering.Text = "";
				//TxbRollebaseret_LederBogfoeringEAN.Text = "";
				//PanelLoenPersonale.Visible = false;
				//RbIsLeaderAbleToSeeOther.ClearSelection();
				//TxbRollebaseret_SeAndre_OrgEnhed.Text = "";
				//PanelRollebaseretSeAndre.Visible = false;
				//PanelOpusOpg_Administrativ.Visible = false;
				//TxbOpusOpg_Administrativ_Profitcenter.Text = "";
				//PanelAdministrativMedarbejder.Visible = false;
				//BtnRollebaseretAdmMedarb_OekonomiOpg.Checked = false;
				//PanelRollebaseretAdmMedarb_LoenPersonale.Visible= false;
				//BtnRollebaseretAdmMedarb_LoenPersonale.Checked = false;
				//PanelRollebaseretAdmMedarb_EAN.Visible = false;
				//TxbRollebaseretAdmMedarb_EAN.Text = "";
				//PanelRollebaseretAdmMedarb_Andet.Visible = false;
				//BtnRollebaseretAdmMedarb_Andet.Checked = false;
				//PanelRollebaseretAdmMedarb_Rettigheder.Visible = false;
				//TxbRollebaseretAdmMedarb_Andet.Text = "";
				//PanelRollebaseretAndet.Visible = false;
				//TxbRollebaseretAndet.Text = "";
			}
		}

		//protected void RbIsKMDFaktura_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    if (RbIsKMDFaktura.SelectedIndex == 0)
		//        panelFaktura.Visible = true;
		//    else
		//    {
		//        TxbKMDOpusOkonomiEan.Text = "";
		//        panelFaktura.Visible = false;
		//    }
		//}

		//protected void RbIsKMDLoenOgPersonale_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    if (RbIsKMDLoenOgPersonale.SelectedIndex == 0)
		//        panelOpusLonPersonale.Visible = true;
		//    else
		//    {
		//        TxbKMDOrgUnit.Text = "";
		//        panelOpusLonPersonale.Visible = false;
		//    }
		//}

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

		protected void btn_add_cura_rolle_Click(object sender, EventArgs e)
		{
			if (DdlCuraBrugerRolle.SelectedIndex != -1 && DdlCuraBrugerRolle.SelectedValue != "X")
			{
				List<string> list_of_selected_cura_roles = new List<string>();
				foreach (GridDataItem item in grid_curaRoles.Items)
				{
					list_of_selected_cura_roles.Add(item.GetDataKeyValue("curaRole").ToString());
				}
				if (!list_of_selected_cura_roles.Contains(DdlCuraBrugerRolle.SelectedText))
				{
					list_of_selected_cura_roles.Add(DdlCuraBrugerRolle.SelectedText);
				}
				LoadCuraRoleGrid(list_of_selected_cura_roles);
			}
		}

		protected void grid_curaRoles_ItemCommand(object sender, GridCommandEventArgs e)
		{
			if (e.CommandName == "delete")
			{
				List<string> list_of_selected_roles = new List<string>();
				foreach (GridDataItem item in grid_curaRoles.Items)
				{
					list_of_selected_roles.Add(item.GetDataKeyValue("curaRole").ToString());
				}
				GridDataItem currentItem = (GridDataItem)e.Item;
				list_of_selected_roles.Remove(currentItem.GetDataKeyValue("curaRole").ToString());
				LoadCuraRoleGrid(list_of_selected_roles);
			}
		}

		private void LoadCuraRoleGrid(List<string> curaroles)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("CuraRole", typeof(string));
			foreach (string role in curaroles)
			{
				dt.Rows.Add(role);
			}
			grid_curaRoles.DataSource = dt;
			grid_curaRoles.Rebind();
		}

		protected void RbIsEducaPersonale_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (RbIsEducaPersonale.SelectedIndex == 0)
			{
				panel_educapersonale.Visible = true;
			}
			else
			{
				TxbEducaPersonale_unilogin.Text = "";
				TxbEducaPersonale_skolekode.Text = "";
				RbEducaPersonale_Role.SelectedIndex = -1;
				panel_educapersonale.Visible = false;
			}
		}

		protected void RBIsRakat_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (RBisEhandel.SelectedIndex == 0)
			{
				panel_Rakat.Visible = true;
			}
			else
			{
				//Txb_rakat_konteringsansvarlig.Text = "";
				TxbEHandelProfitCenter.Text = "";
				RbEHandelBrugerType.SelectedIndex = -1;
				panel_Rakat.Visible = false;
				panel_rakat_profitomkoststed.Visible = false;
				//panel_rakat_konteringsansvarlig.Visible = false;
			}
		}

		protected void RbRakatRole_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (RbEHandelBrugerType.SelectedValue == "B" || RbEHandelBrugerType.SelectedValue == "D")
			{
				panel_rakat_profitomkoststed.Visible = true;
				//panel_rakat_konteringsansvarlig.Visible = false;
				//Txb_rakat_konteringsansvarlig.Text = "";
			}
			else if (RbEHandelBrugerType.SelectedValue == "C")
			{
				panel_rakat_profitomkoststed.Visible = false;
				//panel_rakat_konteringsansvarlig.Visible = true;
				TxbEHandelProfitCenter.Text = "";
			}
			else
			{
				//Txb_rakat_konteringsansvarlig.Text = "";
				TxbEHandelProfitCenter.Text = "";
				panel_rakat_profitomkoststed.Visible = false;
				//panel_rakat_konteringsansvarlig.Visible = false;
			}
		}

		protected void ComboRollebaseret_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if (ComboRollebaseret.SelectedValue == "LEDER")
			{
				PanelRollebaseret_Leder.Visible = true;
				PanelLoenPersonale.Visible = true;

				HideAndResetRollebaseret_Administrativ();
				HideAndResetRollebaseret_Andet();
			}
			else if (ComboRollebaseret.SelectedValue == "ADMINISTRATIV")
			{

				PanelAdministrativMedarbejder.Visible = true;
				PanelRollebaseretAdmMedarb_LoenPersonale.Visible = true;
				PanelRollebaseretAdmMedarb_Andet.Visible = true;

				HideAndResetRollebaseret_Leder();
				HideAndResetRollebaseret_Andet();
			}
			else if (ComboRollebaseret.SelectedValue == "ANDET")
			{
				PanelRollebaseretAndet.Visible = true;

				HideAndResetRollebaseret_Leder();
				HideAndResetRollebaseret_Administrativ();
			}
		}


		private void HideAndResetRollebaseret_Leder()
		{
			PanelRollebaseret_Leder.Visible = false;
			PanelLoenPersonale.Visible = false;

			TxbRollebaseretProfitcenterAnsvar.Text = "";
			RbRollebaseretFaktura_Leder.ClearSelection();
			PanelRollebaseret_LederBogfoering.Visible = false;
			TxbRollebaseretProfitcenter_LederBogfoering.Text = "";
			TxbRollebaseret_LederBogfoeringEAN.Text = "";

			PanelLoenPersonale.Visible = false;
			RbIsLeaderAbleToSeeOther.ClearSelection();
			TxbRollebaseret_SeAndre_OrgEnhed.Text = "";
			PanelRollebaseretSeAndre.Visible = false;
		}
		private void HideAndResetRollebaseret_Administrativ()
		{
			PanelAdministrativMedarbejder.Visible = false;
			PanelRollebaseretAdmMedarb_LoenPersonale.Visible = false;
			PanelRollebaseretAdmMedarb_Andet.Visible = false;

			PanelOpusOpg_Administrativ.Visible = false;
			TxbOpusOpg_Administrativ_Profitcenter.Text = "";
			TxbRollebaseretAdmMedarb_EAN.Text = "";
			BtnRollebaseretAdmMedarb_OekonomiOpg.Checked = false;
			BtnRollebaseretAdmMedarb_LoenPersonale.Checked = false;
			PanelRollebaseretAdmMedarb_OrgEnh.Visible = false;
			TxbRollebaseretAdmMedarb_OrgEnh.Text = "";
			BtnRollebaseretAdmMedarb_Andet.Checked = false;
			PanelRollebaseretAdmMedarb_Rettigheder.Visible = false;
			TxbRollebaseretAdmMedarb_Andet.Text = "";
		}
		private void HideAndResetRollebaseret_Andet()
		{
			PanelRollebaseretAndet.Visible = false;
			TxbRollebaseretAndet.Text = "";
		}






		protected void RbRollebaseretFaktura_Leder_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (RbRollebaseretFaktura_Leder.SelectedValue == "BOGFOER")
			{
				PanelRollebaseret_LederBogfoering.Visible = true;
			}
			else
			{
				PanelRollebaseret_LederBogfoering.Visible = false;
			}
		}

		protected void RbIsLeaderAbleToSeeOther_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (RbIsLeaderAbleToSeeOther.SelectedIndex == 0)
			{
				PanelRollebaseretSeAndre.Visible = true;
			}
			else
			{
				PanelRollebaseretSeAndre.Visible = false;
				TxbRollebaseret_SeAndre_OrgEnhed.Text = "";
			}
		}

		//protected void RbOpusOpg_Administrativ_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    if (RbOpusOpg_Administrativ.SelectedIndex == 0)
		//    {
		//        PanelOpusOpg_Administrativ.Visible = true;
		//    }
		//    else
		//    {
		//        PanelOpusOpg_Administrativ.Visible= false;
		//        TxbOpusOpg_Administrativ_Profitcenter.Text = "";
		//    }
		//}


		protected void BtnRollebaseretAdmMedarb_LoenPersonale_CheckedChanged(object sender, EventArgs e)
		{
			if (BtnRollebaseretAdmMedarb_LoenPersonale.Checked)
			{
				PanelRollebaseretAdmMedarb_OrgEnh.Visible = true;
			}
			else
			{
				PanelRollebaseretAdmMedarb_OrgEnh.Visible = false;
				TxbRollebaseretAdmMedarb_OrgEnh.Text = "";
			}
		}

		protected void BtnRollebaseretAdmMedarb_OekonomiOpg_CheckedChanged(object sender, EventArgs e)
		{
			if (BtnRollebaseretAdmMedarb_OekonomiOpg.Checked)
			{
				PanelOpusOpg_Administrativ.Visible = true;
			}
			else
			{
				PanelOpusOpg_Administrativ.Visible = false;
				TxbOpusOpg_Administrativ_Profitcenter.Text = "";
				TxbRollebaseretAdmMedarb_EAN.Text = "";
			}
		}

		protected void BtnRollebaseretAdmMedarb_Andet_CheckedChanged(object sender, EventArgs e)
		{
			if (BtnRollebaseretAdmMedarb_Andet.Checked)
			{
				PanelRollebaseretAdmMedarb_Rettigheder.Visible = true;
			}
			else
			{
				PanelRollebaseretAdmMedarb_Rettigheder.Visible = false;
				TxbRollebaseretAdmMedarb_Andet.Text = "";
			}
		}

		protected void RbIsKmdInstitution_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (RbIsKmdInstitution.SelectedIndex == 0)
			{
				PanelKmdInstitution.Visible = true;
			}
			else
			{
				PanelKmdInstitution.Visible = false;
				PanelKmdInsttution_Institutionsnummer.Visible = false;
				ComboKmdInstitution.ClearSelection();
				TxtBoxKmdInsttution_Institutionsnummer.Text = string.Empty;
			}

		}

		protected void ComboKmdInstitution_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if (ComboKmdInstitution.SelectedValue == "DAGTILBUD")
			{
				PanelKmdInsttution_Institutionsnummer.Visible = true;
			}
			else
			{
				PanelKmdInsttution_Institutionsnummer.Visible = false;
			}
		}

		private string GetKmdInstitutionProfil()
		{
			string profil = "";
			if (ComboKmdInstitution.SelectedItem != null)
			{
				profil = ComboKmdInstitution.Text;
			}
			return profil;
		}

		protected void RblIsMUElev_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (RblIsMUElev.SelectedIndex == 0)
			{
				PanelMUElev.Visible = true;
			}
			else
			{
				ComboMUElev_Skolekode.ClearSelection();
				RblMUElev_Rolle.SelectedIndex = -1;
				PanelMUElev.Visible = false;
			}
		}
	}
}