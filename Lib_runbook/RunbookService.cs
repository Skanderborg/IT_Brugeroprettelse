using System;
using System.Collections.Generic;
using System.Linq;
using Lib_runbook.OrchestratorApiService;
using System.Data.Services.Client;
using System.Net;
using System.Net.Security;
using Lib_runbook.Model;

namespace Lib_runbook
{
    public class RunbookService
    {
        private string username;
        private string password;
        private string runbookId;
        private OrchestratorApi orchestratorApi;

        public RunbookService(string username, string password, string orchestratorApiAddress, string runbookId)
        {
            this.username = username;
            this.password = password;
            this.runbookId = runbookId;
            this.orchestratorApi = new OrchestratorApi(new Uri(orchestratorApiAddress));
            ((DataServiceContext)orchestratorApi).Credentials = new NetworkCredential(username, password);

            //This is used here to ignore certificate errors as you might be using test certificates. You can remove this if you are using trusted certificates.
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                delegate
                {
                    return true;
                });
        }

        public bool CreateAndStartRunbookJob(Runbook_Operation op, out string errorStr)
        {
            //System.IO.File.AppendAllText(@"C:\logs\adusercreation\service_log.txt", DateTime.Now + " - User: " + op.BestillerSamaccount + " called runbook service" + Environment.NewLine);
            OperationParameter operationParameters = CreateRunbookParameters(op);

            return CreateRunbookJob(operationParameters, out errorStr);
        }

        private bool CreateRunbookJob(OperationParameter operationParameters, out string errorStr)
        {
            //System.IO.File.AppendAllText(@"C:\logs\adusercreation\service_log.txt", DateTime.Now + " - Starting runbook job " + Environment.NewLine);
            try
            {
                string uri = string.Concat(orchestratorApi.Runbooks, string.Format("(guid'{0}')/{1}", runbookId, "Start"));
                Uri uriSMA = new Uri(uri, UriKind.Absolute);

                var jobIdValue = orchestratorApi.Execute<Guid>(uriSMA, "POST", true, operationParameters) as QueryOperationResponse<Guid>;
                jobIdValue.Single();
                errorStr = "";
                //System.IO.File.AppendAllText(@"C:\logs\adusercreation\service_log.txt", DateTime.Now + " - finished runbook job " + Environment.NewLine);
                return true;
            }
            catch (Exception ex)
            {
                errorStr = "Fejl: " + ex.InnerException.Message + " - " + ex.Message;
                return false;
            }
        }

        private OperationParameter CreateRunbookParameters(Runbook_Operation op)
        {
            var runbookParams = new List<NameValuePair>();

            //Fordi microsoft har "glemt" type casting, caster SMA serveren selv værdierne - følgende er et hack til at komme uden om buggen
            string strHack = "DefaultString:";

            runbookParams.Add(new NameValuePair() { Name = "BestillerSamaccount", Value = strHack + op.BestillerSamaccount });
            runbookParams.Add(new NameValuePair() { Name = "Opus_medarbejdernr", Value = strHack + op.Opus_medarbejdernr });
            runbookParams.Add(new NameValuePair() { Name = "CPRnummer", Value = strHack + op.CPRnummer });
            runbookParams.Add(new NameValuePair() { Name = "Fornavn", Value = strHack + op.Fornavn });
            runbookParams.Add(new NameValuePair() { Name = "Efternavn", Value = strHack + op.Efternavn });
            runbookParams.Add(new NameValuePair() { Name = "Vistnavn", Value = strHack + op.Vistnavn });
            runbookParams.Add(new NameValuePair() { Name = "ManagerSamaccount", Value = strHack + op.ManagerSamaccount });
            runbookParams.Add(new NameValuePair() { Name = "CoworkerSamaccount", Value = strHack + op.CoworkerSamaccount });
            runbookParams.Add(new NameValuePair() { Name = "IsEmail", Value = strHack + op.IsEmail });
            runbookParams.Add(new NameValuePair() { Name = "KontaktTelefonNummer", Value = strHack + op.KontaktTelefonNummer });
            runbookParams.Add(new NameValuePair() { Name = "IsSkype", Value = strHack + op.IsSkype });
            runbookParams.Add(new NameValuePair() { Name = "IsRingegruppe", Value = strHack + op.IsRingegruppe });
            runbookParams.Add(new NameValuePair() { Name = "RingegruppeNummer", Value = strHack + op.RingegruppeNummer });
            runbookParams.Add(new NameValuePair() { Name = "MobilNummer", Value = strHack + op.MobilNummer });
            runbookParams.Add(new NameValuePair() { Name = "IsDistributionslister", Value = strHack + op.IsDistributionslister });
            runbookParams.Add(new NameValuePair() { Name = "DistributionslisterNavne", Value = strHack + op.DistributionslisterNavne });
            runbookParams.Add(new NameValuePair() { Name = "IsFaellespostkasser", Value = strHack + op.IsFaellespostkasser });
            runbookParams.Add(new NameValuePair() { Name = "FaellespostkasserNavne", Value = strHack + op.FaellespostkasserNavne });
            runbookParams.Add(new NameValuePair() { Name = "IsNemID", Value = strHack + op.IsNemID });
            runbookParams.Add(new NameValuePair() { Name = "Ean", Value = strHack + op.Ean });
            runbookParams.Add(new NameValuePair() { Name = "IsCura", Value = strHack + op.IsCura });
            runbookParams.Add(new NameValuePair() { Name = "CuraBrugerRolle", Value = strHack + op.CuraBrugerRolle });
            runbookParams.Add(new NameValuePair() { Name = "IsCuraPlanner", Value = strHack + op.IsCuraPlanner });
            runbookParams.Add(new NameValuePair() { Name = "IsCuraFMK", Value = strHack + op.IsCuraFMK });
            runbookParams.Add(new NameValuePair() { Name = "CuraFMKID", Value = strHack + op.CuraFMKID });
            runbookParams.Add(new NameValuePair() { Name = "IsKMDbruger", Value = strHack + op.IsKMDbruger });
            runbookParams.Add(new NameValuePair() { Name = "KMDUserProfiles", Value = strHack + op.KMDUserProfiles });
            runbookParams.Add(new NameValuePair() { Name = "IsKMDOpusOkonomiBilagsbehandling", Value = strHack + op.IsKMDOpusOkonomiBilagsbehandling });
            runbookParams.Add(new NameValuePair() { Name = "KMDProfitcenterOmkostningssted", Value = strHack + op.KMDProfitcenterOmkostningssted });
            runbookParams.Add(new NameValuePair() { Name = "IsKMDFaktura", Value = strHack + op.IsKMDFaktura });
            runbookParams.Add(new NameValuePair() { Name = "KMDOpusOkonomiEan", Value = strHack + op.KMDOpusOkonomiEan });
            runbookParams.Add(new NameValuePair() { Name = "IsKMDBudgetOmplacering", Value = strHack + op.IsKMDBudgetOmplacering });
            runbookParams.Add(new NameValuePair() { Name = "IsKMDMitForventedeRegnskab", Value = strHack + op.IsKMDMitForventedeRegnskab });
            runbookParams.Add(new NameValuePair() { Name = "IsKMDLoenOgPersonale", Value = strHack + op.IsKMDLoenOgPersonale });
            runbookParams.Add(new NameValuePair() { Name = "KMDOrgUnit", Value = strHack + op.KMDOrgUnit });
            runbookParams.Add(new NameValuePair() { Name = "Bemaerkninger", Value = strHack + op.Bemaerkninger });
                runbookParams.Add(new NameValuePair() { Name = "CuraLoginORGs", Value = strHack + op.CuraLoginORGs });
            return new BodyOperationParameter("parameters", runbookParams);
        }

        private Runbook GetRunbookByName(string runbookName)
        {
            return orchestratorApi.Runbooks.Where(r => r.RunbookName == runbookName).First();
        }
    }
}
