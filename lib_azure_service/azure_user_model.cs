using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace lib_azure_service
{
    public class Azure_user_model
    {

        /// <summary>
        /// Oplysninger om medarbejderen
        /// </summary>

        //Mandatory = $true
        // bestiller samaccount
        [JsonProperty(PropertyName = "BestillerSamaccount", Required = Required.Always)]
        public string BestillerSamaccount { get; set; }

        //Mandatory = $true
        //[string]$Opus_medarbejdernr
        // Vælg medarbejder, der skal have IT bruger: (medarbejdernummer)
        [JsonProperty(PropertyName = "Opus_medarbejdernr", Required = Required.Always)]
        public string Opus_medarbejdernr { get; set; }

        //Mandatory = $true
        //[string]$CPRnummer
        [JsonProperty(PropertyName = "CPRnummer", Required = Required.Always)]
        public string CPRnummer { get; set; }

        //Mandatory = $true
        //[String]$Fornavn
        [JsonProperty(PropertyName = "Fornavn", Required = Required.Always)]
        public string Fornavn { get; set; }

        //Mandatory = $true
        //[String]$Efternavn
        [JsonProperty(PropertyName = "Efternavn", Required = Required.Always)]
        public string Efternavn { get; set; }

        //Mandatory = $true
        //[String]$Vistnavn
        // Medarbejders viste navn: (>e-mail adresse genereres på baggrund af dette, og navn vises i Outlook/Skype.)
        [JsonProperty(PropertyName = "Vistnavn", Required = Required.Always)]
        public string Vistnavn { get; set; }

		//Mandatory = $true
		//[String]$Stillingsbetegnelse
		// Medarbejders stillingsbetegnelse
		[JsonProperty(PropertyName = "Stillingsbetegnelse", Required = Required.Always)]
		public string Stillingsbetegnelse { get; set; }


		//Oplysninger om medarbejderens position i Skanderborg

		//Mandatory = $true
		//[String]$CoworkerSamaccount
		[JsonProperty(PropertyName = "CoworkerSamaccount", Required = Required.Always)]
        public string CoworkerSamaccount { get; set; }

        //Mandatory = $true
        //[Boolean]$IsEmail
        // Skal der oprettes e-mail til medarbejderen?
        //[JsonProperty(PropertyName = "IsEmail", Required = Required.Always)]
        //public bool IsEmail { get; set; }


        //Oplysninger om medarbejderen telefoni

        //Mandatory = $true
        //[Boolean]$IsSkype
        //Skal medarbejderen have et personligt lokalnummer f.x til Skype?
        [JsonProperty(PropertyName = "IsSkype", Required = Required.Always)]
        public bool IsSkype { get; set; }

        //Mandatory = $false
        //[Boolean]$IsRingegruppe
        //Skal medarbejderen tilknyttes ringe- / svargruppe?
        [JsonProperty(PropertyName = "IsRingegruppe", Required = Required.AllowNull)]
        public bool IsRingegruppe { get; set; }

        //Mandatory = $false
        //[String]$RingegruppeNummer
        // Indtast nummer på ringegruppe
        [JsonProperty(PropertyName = "RingegruppeNummer", Required = Required.AllowNull)]
        public string RingegruppeNummer { get; set; }

        //Mandatory = $false
        //[String]$MobilNummer
        //Indtast evt. medarbejderens kontakt mobilnummer: (frivilligt)
        [JsonProperty(PropertyName = "MobilNummer", Required = Required.AllowNull)]
        public string MobilNummer { get; set; }



        /// <summary>
        /// Vælg øvrige rettigheder
        /// </summary>

        //Mandatory = $true
        //[Boolean]$IsDistributionslister
        //Skal medarbejderen tilknyttes distributionslister ud over standard for afdelingen?
        [JsonProperty(PropertyName = "IsDistributionslister", Required = Required.Always)]
        public bool IsDistributionslister { get; set; }

        //Mandatory = $false
        //[string]$DistributionslisterNavne
        [JsonProperty(PropertyName = "DistributionslisterNavne", Required = Required.AllowNull)]
        public string DistributionslisterNavne { get; set; }

        //Mandatory = $true
        //[Boolean]$IsFaellespostkasser
        //Skal medarbejderen have rettigheder til fællespostkasser?
        [JsonProperty(PropertyName = "IsFaellespostkasser", Required = Required.Always)]
        public bool IsFaellespostkasser { get; set; }

        //Mandatory = $false
        //[string]$FaellespostkasserNavne
        [JsonProperty(PropertyName = "FaellespostkasserNavne", Required = Required.AllowNull)]
        public string FaellespostkasserNavne { get; set; }


		//Mandatory = $true
		//[Boolean]$IsMUElev
		//
		[JsonProperty(PropertyName = "IsMUElev", Required = Required.Always)]
		public bool IsMUElev { get; set; }

		//$MUElevSkolekode
		[JsonProperty(PropertyName = "MUElevSkolekode", Required = Required.AllowNull)]
		public string MUElevSkolekode { get; set; }

		//$MUElevRolle
		[JsonProperty(PropertyName = "MUElevRolle", Required = Required.AllowNull)]
		public string MUElevRolle { get; set; }



		/// <summary>
		/// Vælg øvrige programmer
		/// </summary>

		//Mandatory = $true
		//[Boolean]$IsNemID
		//OBS: hvis der bestilles NemID, skal medarbejderen også have en e-mail.
		//[JsonProperty(PropertyName = "IsNemID", Required = Required.Always)]
		//public bool IsNemID { get; set; }

		//Mandatory = $false
		//[string]$Ean
		//Indtast EAN, som betaler for NemID og CuraFMK (er sat hvis der er Nemid eller CuraFMK):
		//[JsonProperty(PropertyName = "Ean", Required = Required.AllowNull)]
		//public string Ean { get; set; }

		//Mandatory = $true
		//[Boolean]$IsCura
		// Skal brugeren have adgang til cura?
		[JsonProperty(PropertyName = "IsCura", Required = Required.Always)]
        public bool IsCura { get; set; }

        //Mandatory = $false
        //[String]$CuraBrugerRolle
        // navnet på curabrugerrolle
        [JsonProperty(PropertyName = "CuraBrugerRolle", Required = Required.AllowNull)]
        public string CuraBrugerRolle { get; set; }

        // cura login org
        // semikolon spereret array, fordi der ikke kan sendes arrays heh
        // mandatory ved cura, men ellers ikke
        [JsonProperty(PropertyName = "CuraLoginORGs", Required = Required.AllowNull)]
        public string CuraLoginORGs { get; set; }

        //Mandatory = $false
        //[Boolean]$IsCuraPlanner
        // skal medarbejder være cura planlæggeR?
        [JsonProperty(PropertyName = "IsCuraPlanner", Required = Required.AllowNull)]
        public bool IsCuraPlanner { get; set; }

        //Mandatory = $false
        //[Boolean]$IsCuraFMK
        //OBS: hvis medarbejderen skal anvende FMK, skal der bestilles et NemID.
        // Skal medarbejderen anvende FMK?
        [JsonProperty(PropertyName = "IsCuraFMK", Required = Required.AllowNull)]
        public bool IsCuraFMK { get; set; }

        //Mandatory = $false
        //[String]$CuraFMKID
        // medarbejderens FKM ID
        [JsonProperty(PropertyName = "CuraFMKID", Required = Required.AllowNull)]
        public string CuraFMKID { get; set; }


        //Mandatory = $true
        //[Boolean]$IsKMDbruger
        // skal medarbejderen have en KMD bruger?
        [JsonProperty(PropertyName = "IsKMDbruger", Required = Required.Always)]
        public bool IsKMDbruger { get; set; }

        //Mandatory = $false
        //[String]$KMDUserProfiles
        // de systemer medarbejderen skal have adgang til (F.eks. Sag, Doc2Archive, Institution)
        [JsonProperty(PropertyName = "KMDUserProfiles", Required = Required.AllowNull)]
        public string KMDUserProfiles { get; set; }


        //Rollebaseret

        //Mandatory = $false
        //[Boolean]$isKmdRollebaseret
        // skal medarbejder have adgang til Opus Rollebaseret?
        [JsonProperty(PropertyName = "IsKmdRollebaseret", Required = Required.AllowNull)]
        public bool IsKmdRollebaseret { get; set; }


        //Mandatory = $false
        //[String]$KMDRollebaseretProfil
        [JsonProperty(PropertyName = "KMDRollebaseretProfil", Required = Required.AllowNull)]
        public string KMDRollebaseretProfil { get; set; }


        //Mandatory = $false
        //[String]$KMDRollebaseretProfitcenteransvar
        [JsonProperty(PropertyName = "KMDRollebaseretProfitcenteransvar", Required = Required.AllowNull)]
        public string KMDRollebaseretProfitcenteransvar { get; set; }


        //Mandatory = $false
        //[String]$KMDRollebaseretLederbogfore
        [JsonProperty(PropertyName = "KMDRollebaseretLederbogfore", Required = Required.AllowNull)]
        public string KMDRollebaseretLederbogfore { get; set; }


        //Mandatory = $false
        //[String]$KmdRollebaseretProfitcenterBogfoer
        [JsonProperty(PropertyName = "KmdRollebaseretProfitcenterBogfoer", Required = Required.AllowNull)]
        public string KmdRollebaseretProfitcenterBogfoer { get; set; }


        //Mandatory = $false
        //[String]$KMDRollebaseretEAN
        [JsonProperty(PropertyName = "KMDRollebaseretEAN", Required = Required.AllowNull)]
        public string KMDRollebaseretEAN { get; set; }


        //Mandatory = $false
        //[String]$KMDRollebaseretOrgEnhed
        [JsonProperty(PropertyName = "KMDRollebaseretOrgEnhed", Required = Required.AllowNull)]
        public string KMDRollebaseretOrgEnhed { get; set; }


        //Mandatory = $false
        //[String]$KMDRollebaseretRettighed
        [JsonProperty(PropertyName = "KMDRollebaseretRettighed", Required = Required.AllowNull)]
        public string KMDRollebaseretRettighed { get; set; }


        //KMD Institution

        //Mandatory = $true
        //[Boolean]$IsKmdInstitution
        // skal medarbejder have adgang til KMD Institution?
        [JsonProperty(PropertyName = "IsKmdInstitution", Required = Required.Always)]
        public bool IsKmdInstitution { get; set; }

        //Mandatory = $false
        //[String]$KmdInstitutionProfil
        [JsonProperty(PropertyName = "KmdInstitutionProfil", Required = Required.AllowNull)]
        public string KmdInstitutionProfil { get; set; }

        //Mandatory = $false
        //[String]$KmdInstitutionInstitutionsnummer
        [JsonProperty(PropertyName = "KmdInstitutionInstitutionsnummer", Required = Required.AllowNull)]
        public string KmdInstitutionInstitutionsnummer { get; set; }


        //$IsTargit = $RequestData.IsTargit
        [JsonProperty(PropertyName = "IsTargit", Required = Required.Always)]
        public bool IsTargit { get; set; }


        //Vejledning til at finde nummer på IsEduca.
        [JsonProperty(PropertyName = "IsEduca", Required = Required.AllowNull)]
        public bool IsEduca { get; set; }

        //$EducaUnilogin(Denne vil jeg gerne finde ud af om kan hentes et sted fra, så bruger ikke skal tage stilling til det)
        [JsonProperty(PropertyName = "EducaUnilogin", Required = Required.AllowNull)]
        public string EducaUnilogin { get; set; }

        //$EducaSkolekode(Denne vil jeg gerne finde ud af om kan hentes et sted fra, så bruger ikke skal tage stilling til det)
        [JsonProperty(PropertyName = "EducaSkolekode", Required = Required.AllowNull)]
        public string EducaSkolekode { get; set; }

        //$EducaRolle
        [JsonProperty(PropertyName = "EducaRolle", Required = Required.AllowNull)]
        public string EducaRolle { get; set; }

		//$IsEHandel
        //•	Forventer en boolean
        //•	Om brugeren skal have e-handel adgang

		[JsonProperty(PropertyName = "IsEHandel", Required = Required.AllowNull)]
        public bool IsEHandel { get; set; }

		//$EHandelBrugerType
		//•	Forventer en string
		//•	Definere hvilken AD gruppe brugeren skal i
		//•	Bruges i switch statement(string like "kan handle", "kan afgive ordre", "konteringsansvarlig")
		//•	Tekst sendes med i mail til 7913 og e-handel

		[JsonProperty(PropertyName = "EHandelBrugerType", Required = Required.AllowNull)]
        public string EHandelBrugerType { get; set; }

		//$EHandelProfitCenter
        //•	Forventer en string
        //•	Tekst sendes med i mail til 7913 og e-handel

		[JsonProperty(PropertyName = "EHandelProfitCenter", Required = Required.AllowNull)]
        public string EHandelProfitCenter { get; set; }

        //$RakatKonteringsansvarlig(Denne vil jeg gerne finde ud af om kan hentes et sted fra, så bruger ikke skal tage stilling til det)
        //[JsonProperty(PropertyName = "RakatKonteringsansvarlig", Required = Required.AllowNull)]
        //public string RakatKonteringsansvarlig { get; set; }

        //$IsKMDelev
        //[JsonProperty(PropertyName = "IsKMDelev", Required = Required.Always)]
        //public bool IsKMDelev { get; set; }

        //$IsKSD
        [JsonProperty(PropertyName = "IsKSD", Required = Required.AllowNull)]
        public bool IsKSD { get; set; }

        //$KSDRolle
        [JsonProperty(PropertyName = "KSDRolle", Required = Required.AllowNull)]
        public string KSDRolle { get; set; }

        //$IsKY
        [JsonProperty(PropertyName = "IsKY", Required = Required.AllowNull)]
        public bool IsKY { get; set; }

        //$KYRolle
        [JsonProperty(PropertyName = "KYRolle", Required = Required.AllowNull)]
        public string KYRolle { get; set; }


        /// <summary>
        /// Bemærkninger
        /// </summary>

        //Mandatory = $false
        //[String]$Bemaerkninger
        //Skriv evt. dine bemærkninger her:
        [JsonProperty(PropertyName = "Bemaerkninger", Required = Required.AllowNull)]
        public string Bemaerkninger { get; set; }


        




        

    }
}
