using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lib_runbook.Model;
using Lib_runbook;
using Lib_ActiveDirectory.Services;
using lib_azure_service;
using App_Web.SofdCoreAPI_WebService;
using DAL.ITBrugeroprettelse.Data;

namespace App_Web.Services
{
    internal class CreateUserService
    {

        private GroupPrincipalService adService = new GroupPrincipalService();   

        /// <summary>
        /// fornavn, efternavn, samlet navn (displayname)
        /// </summary>
        /// <param name="opus_id"></param>
        /// <returns></returns>
        //internal Dictionary<string, string> GetEmployeeNames(int opus_id)
        //{
        //    Dictionary<string, string> res = new Dictionary<string, string>();

        //    v_ad_user_creation pos = posRepo.Query.Where(p => p.Opus_id == opus_id).FirstOrDefault();
        //    res["Firstname"] = pos.Firstname;
        //    res["Lastname"] = pos.Lastname;
        //    res["Fullname"] = pos.Fullname;
        //    return res;
        //}

        /// <summary>
        /// fornavn, efternavn, samlet navn (displayname)
        /// </summary>
        /// <param name="opus_id"></param>
        /// <returns></returns>
        internal Dictionary<string, string> GetEmployeeNamesFromSofdCore(int opus_id, List<EmployeeAffiliationWithoutADUser> employeeList)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();

            EmployeeAffiliationWithoutADUser employee = employeeList.Where(e => e.EmployeeId == opus_id.ToString()).FirstOrDefault();

            //v_ad_user_creation pos = posRepo.Query.Where(p => p.Opus_id == opus_id).FirstOrDefault();
            res["Firstname"] = employee.PersonFirstname;
            res["Lastname"] = employee.PersonSurname;
            res["Fullname"] = employee.PersonFirstname + " " + employee.PersonSurname;
            res["Position"] = employee.AffliationPositionName;
            return res;
        }

		internal Dictionary<string, string> GetEmployeeNamesFromDB(int opus_id, List<AnsatUdenADBruger> employeeList)
		{
			Dictionary<string, string> res = new Dictionary<string, string>();

			AnsatUdenADBruger employee = employeeList.Where(e => e.EmployeeId == opus_id.ToString()).FirstOrDefault();


			res["Firstname"] = employee.PersonFirstname;
			res["Lastname"] = employee.PersonSurname;
			res["Fullname"] = employee.PersonName;
			res["Position"] = employee.AffiliationPositionName;
			return res;
		}


		private string GetCprFromSofdCore(int opus_id)
        {
            string cpr = string.Empty;
            string sofdCoreApiKey = Properties.Settings.Default.SofdCoreApiKey;

            SofdCoreAPI_WebService.SofdCoreAPI_WebService ws = new SofdCoreAPI_WebService.SofdCoreAPI_WebService();
            Person[] personList = ws.GetPersons_FromEmployeeID(opus_id.ToString(), sofdCoreApiKey);

            if (personList.Count() == 1)
            {
                Person person = personList[0];

                cpr = (from a in person.Affiliations
                       where a.EmployeeId == opus_id.ToString()
                       select person.Cpr
                       ).FirstOrDefault();
            }

            return cpr;
        }



        internal bool IsEmployeeInSofdCore(int opus_id, List<EmployeeAffiliationWithoutADUser> employeeList)
        {
            int count = employeeList.Where(e => e.EmployeeId == opus_id.ToString()).ToList().Count;

            return count == 1;
        }

		internal bool IsEmployeeInDB(int opus_id, List<AnsatUdenADBruger> employeeList)
		{
			int count = employeeList.Where(e => e.EmployeeId == opus_id.ToString()).ToList().Count;

			return count == 1;
		}

		internal string GetCurrentUserADUsername()
        {
            string username = string.Empty;

            if (HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.Name.ToString().Length > 4)
            {
                username = HttpContext.Current.User.Identity.Name.ToString().Remove(0, 4);
            }

            return username;
        }

        internal List<string> GetCuraRoles()
        {
            return adService.GetCuraRoles(Properties.Settings.Default.ad_curaroles);
        }

        internal List<string> GetCuraLOrg()
        {
            return adService.GetCuraLOrg(Properties.Settings.Default.ad_curaorg);
        }

        private bool CreateADUser(Runbook_Operation op, out string errorStr)
        {

            //set test eller actual runbook id
            RunbookService runbook = new RunbookService(Properties.Settings.Default.RunbookUsername, Properties.Settings.Default.RunbookPassword, Properties.Settings.Default.orchestratorApiAddress, Properties.Settings.Default.runbookId);
            return runbook.CreateAndStartRunbookJob(op, out errorStr);
        }

        internal bool CreateUser(string opus_medarbejdernr,
                string fornavn,
                string efternavn,
                string vistnavn,
                string stillingsbetegnelse,

                string coworkerSamaccount,

                bool isSkype,
                bool isRingegruppe,
                string ringegruppeNummer,
                string mobilNummer,

                bool isDistributionslister,
                string distributionslisterNavne,
                bool isFaellespostkasser,
                string faellespostkasserNavne,

                bool isMUElev,
                string mUElevSkolekode,
                string mUElevRolle,

				bool isCura,
                string curaBrugerRolle,
                bool isCuraPlanner,
                bool isCuraFMK,
                string curaFMKID,

                bool isKMDbruger,
                string kMDUserProfiles,

                bool isKmdRollebaseret,
                string kMDRollebaseretProfil,
                string kMDRollebaseretProfitcenteransvar,
                string kMDRollebaseretLederbogfore,
                string kmdRollebaseretProfitcenterBogfoer,
                string kMDRollebaseretEAN,
                string kMDRollebaseretOrgEnhed,
                string kMDRollebaseretRettighed,

                bool isKmdInstitution,
                string kmdInstitutionProfil,
                string kmdInstitutionInstitutionsnummer,

                bool isTargit,

                bool isEduca,
                string educaUnilogin,
                string educaSkolekode,
                string educaRolle,

                bool isEhandel,
                string eHandelBrugerType,
                string eHandelProfitCenter,

                string bemaerkninger,

                string curaLoginORGs,
                out string errorStr)
        {
            string bestillerSamaccount = GetCurrentUserADUsername();
            string cpr = GetCprFromSofdCore(int.Parse(opus_medarbejdernr));


            JsonService js = new JsonService();
            Azure_user_model json_model = new Azure_user_model()
            {
                BestillerSamaccount = bestillerSamaccount,
                Opus_medarbejdernr = opus_medarbejdernr,
                CPRnummer = cpr,
                Fornavn = fornavn,
                Efternavn = efternavn,
                Vistnavn = vistnavn,
                Stillingsbetegnelse = stillingsbetegnelse,

                CoworkerSamaccount = coworkerSamaccount,
                //IsEmail = true,

                IsSkype = isSkype,
                IsRingegruppe = isRingegruppe,
                RingegruppeNummer = ringegruppeNummer,
                MobilNummer = mobilNummer,

                IsDistributionslister = isDistributionslister,
                DistributionslisterNavne = distributionslisterNavne,
                IsFaellespostkasser = isFaellespostkasser,
                FaellespostkasserNavne = faellespostkasserNavne,

                IsMUElev = isMUElev,
                MUElevSkolekode = mUElevSkolekode,
                MUElevRolle = mUElevRolle,

                IsCura = isCura,
                CuraBrugerRolle = curaBrugerRolle,
                IsCuraPlanner = isCuraPlanner,
                IsCuraFMK = isCuraFMK,
                CuraFMKID = curaFMKID,
                CuraLoginORGs = curaLoginORGs,

                IsKMDbruger = isKMDbruger,
                KMDUserProfiles = kMDUserProfiles,

                IsKmdRollebaseret = isKmdRollebaseret,
                KMDRollebaseretProfil = kMDRollebaseretProfil,
                KMDRollebaseretProfitcenteransvar = kMDRollebaseretProfitcenteransvar,
                KMDRollebaseretLederbogfore = kMDRollebaseretLederbogfore,
                KmdRollebaseretProfitcenterBogfoer = kmdRollebaseretProfitcenterBogfoer,
                KMDRollebaseretEAN = kMDRollebaseretEAN,
                KMDRollebaseretOrgEnhed = kMDRollebaseretOrgEnhed,
                KMDRollebaseretRettighed = kMDRollebaseretRettighed,

                IsKmdInstitution = isKmdInstitution,
                KmdInstitutionProfil = kmdInstitutionProfil,
                KmdInstitutionInstitutionsnummer = kmdInstitutionInstitutionsnummer,

                IsTargit = isTargit,

                IsEduca = isEduca,
                EducaUnilogin = educaUnilogin,
                EducaSkolekode = educaSkolekode,
                EducaRolle = educaRolle,

				IsEHandel = isEhandel,
				EHandelBrugerType = eHandelBrugerType,
				EHandelProfitCenter = eHandelProfitCenter,

                Bemaerkninger = bemaerkninger

            };
            js.PostJson(Properties.Settings.Default.azure_service, "apikey", json_model);

            errorStr = "";
            return true;
        }





    }
}