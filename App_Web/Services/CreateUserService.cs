using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dal_SOFD.LORA;
using Lib_runbook.Model;
using Lib_runbook;
using Lib_ActiveDirectory.Services;

namespace App_Web.Services
{
    internal class CreateUserService
    {
        //private CuraRoleRepo rolleRepo = new CuraRoleRepo();
        private GroupPrincipalService adService = new GroupPrincipalService();
        private PositionRepo posRepo = new PositionRepo(Properties.Settings.Default.LORA_Constr);

        /// <summary>
        /// fornavn, efternavn, samlet navn (displayname)
        /// </summary>
        /// <param name="opus_id"></param>
        /// <returns></returns>
        internal Dictionary<string, string> GetEmployeeNames(int opus_id)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();

            v_ad_user_creation pos = posRepo.Query.Where(p => p.Opus_id == opus_id).FirstOrDefault();
            res["Firstname"] = pos.Firstname;
            res["Lastname"] = pos.Lastname;
            res["Fullname"] = pos.Fullname;
            return res;
        }

        private string GetCpr(int opus_id)
        {
            return posRepo.Query.Where(p => p.Opus_id == opus_id).First().Cpr;
        }

        internal bool IsEmployee(int opus_id)
        {
            return posRepo.Query.Where(p => p.Opus_id == opus_id).Count() == 1;
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

                string managerSamaccount,
                string coworkerSamaccount,
                bool isEmail,

                string kontaktTelefonNummer,
                bool isSkype,
                bool isRingegruppe,
                string ringegruppeNummer,
                string mobilNummer,

                bool isDistributionslister,
                string distributionslisterNavne,
                bool isFaellespostkasser,
                string faellespostkasserNavne,

                bool isNemID,
                string ean,

                bool isCura,
                string curaBrugerRolle,
                bool isCuraPlanner,
                bool isCuraFMK,
                string curaFMKID,

                bool isKMDbruger,
                string kMDUserProfiles,

                bool isKMDOpusOkonomiBilagsbehandling,
                string kMDProfitcenterOmkostningssted,
                bool isKMDFaktura,
                string kMDOpusOkonomiEan,
                bool isKMDBudgetOmplacering,
                bool isKMDMitForventedeRegnskab,

                bool isKMDLoenOgPersonale,
                string kMDOrgUnit,

                string bemaerkninger, out string errorStr)
        {
            string bestillerSamaccount = GetCurrentUserADUsername();
            string cpr = GetCpr(int.Parse(opus_medarbejdernr));
            Runbook_Operation ro = new Runbook_Operation()
            {
                BestillerSamaccount = bestillerSamaccount,
                Opus_medarbejdernr = opus_medarbejdernr,
                CPRnummer = cpr,
                Fornavn = fornavn,
                Efternavn = efternavn,
                Vistnavn = vistnavn,

                ManagerSamaccount = managerSamaccount,
                CoworkerSamaccount = coworkerSamaccount,
                IsEmail = isEmail,

                KontaktTelefonNummer = kontaktTelefonNummer,
                IsSkype = isSkype,
                IsRingegruppe = isRingegruppe,
                RingegruppeNummer = ringegruppeNummer,
                MobilNummer = mobilNummer,

                IsDistributionslister = isDistributionslister,
                DistributionslisterNavne = distributionslisterNavne,
                IsFaellespostkasser = isFaellespostkasser,
                FaellespostkasserNavne = faellespostkasserNavne,

                IsNemID = isNemID,
                Ean = ean,

                IsCura = isCura,
                CuraBrugerRolle = curaBrugerRolle,
                IsCuraPlanner = isCuraPlanner,
                IsCuraFMK = isCuraFMK,
                CuraFMKID = curaFMKID,

                IsKMDbruger = isKMDbruger,
                KMDUserProfiles = kMDUserProfiles,

                IsKMDOpusOkonomiBilagsbehandling = isKMDOpusOkonomiBilagsbehandling,
                KMDProfitcenterOmkostningssted = kMDProfitcenterOmkostningssted,
                IsKMDFaktura = isKMDFaktura,
                KMDOpusOkonomiEan = kMDOpusOkonomiEan,
                IsKMDBudgetOmplacering = isKMDBudgetOmplacering,
                IsKMDMitForventedeRegnskab = isKMDMitForventedeRegnskab,

                IsKMDLoenOgPersonale = isKMDLoenOgPersonale,
                KMDOrgUnit = kMDOrgUnit,

                Bemaerkninger = bemaerkninger
            };
            return CreateADUser(ro, out errorStr);
        }
    }
}