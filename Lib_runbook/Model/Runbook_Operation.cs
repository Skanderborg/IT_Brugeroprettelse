
namespace Lib_runbook.Model
{
    public class Runbook_Operation
    {
        // bestiller samaccount
        public string BestillerSamaccount { get; set; }

        /// <summary>
        /// Oplysninger om medarbejderen
        /// </summary>

        //Mandatory = $true
        //[string]$Opus_medarbejdernr
        // Vælg medarbejder, der skal have IT bruger: (medarbejdernummer)
        public string Opus_medarbejdernr { get; set; }

        //Mandatory = $true
        //[string]$CPRnummer
        public string CPRnummer { get; set; }

        //Mandatory = $true
        //[String]$Fornavn
        public string Fornavn { get; set; }

        //Mandatory = $true
        //[String]$Efternavn
        public string Efternavn { get; set; }

        //Mandatory = $true
        //[String]$Vistnavn
        // Medarbejders viste navn: (>e-mail adresse genereres på baggrund af dette, og navn vises i Outlook/Skype.)
        public string Vistnavn { get; set; }




        /// <summary>
        /// Oplysninger om medarbejderens position i skanderborg
        /// </summary>

        //Mandatory = $true
        //[String]$ManagerSamaccount
        public string ManagerSamaccount { get; set; }

        //Mandatory = $true
        //[String]$CoworkerSamaccount
        public string CoworkerSamaccount { get; set; }

        //Mandatory = $true
        //[Boolean]$IsEmail
        // Skal der oprettes e-mail til medarbejderen?
        public bool IsEmail { get; set; }




        /// <summary>
        /// Oplysninger om medarbejderens telefoni
        /// </summary>

        //Mandatory = $false
        //[String]$KontaktTelefonNummer
        //Indtast medarbejderens kontakt telefon nummer:
        //Dette felt er frivilligt, men kan f.eks. bruges hvis du ønsker, at genbruge et telefon nummer.
        public string KontaktTelefonNummer { get; set; }


        //Mandatory = $true
        //[Boolean]$IsSkype
        //Skal medarbejderen have et personligt lokalnummer f.x til Skype?
        public bool IsSkype { get; set; }


        //Mandatory = $false
        //[Boolean]$IsRingegruppe
        //Skal medarbejderen tilknyttes ringe- / svargruppe?
        public bool IsRingegruppe { get; set; }

        //Mandatory = $false
        //[String]$RingegruppeNummer
        // Indtast nummer på ringegruppe
        public string RingegruppeNummer { get; set; }

        //Mandatory = $false
        //[String]$MobilNummer
        //Indtast evt. medarbejderens kontakt mobilnummer: (frivilligt)
        public string MobilNummer { get; set; }



        /// <summary>
        /// Vælg øvrige rettigheder
        /// </summary>

        //Mandatory = $true
        //[Boolean]$IsDistributionslister
        //Skal medarbejderen tilknyttes distributionslister ud over standard for afdelingen?
        public bool IsDistributionslister { get; set; }

        //Mandatory = $false
        //[string]$DistributionslisterNavne
        public string DistributionslisterNavne { get; set; }

        //Mandatory = $true
        //[Boolean]$IsFaellespostkasser
        //Skal medarbejderen have rettigheder til fællespostkasser?
        public bool IsFaellespostkasser { get; set; }

        //Mandatory = $false
        //[string]$FaellespostkasserNavne
        public string FaellespostkasserNavne { get; set; }



        /// <summary>
        /// Vælg øvrige programmer
        /// </summary>

        //Mandatory = $true
        //[Boolean]$IsNemID
        //OBS: hvis der bestilles NemID, skal medarbejderen også have en e-mail.
        public bool IsNemID { get; set; }

        //Mandatory = $false
        //[string]$Ean
        //Indtast EAN, som betaler for NemID og CuraFMK (er sat hvis der er Nemid eller CuraFMK):
        public string Ean { get; set; }


        //Mandatory = $true
        //[Boolean]$IsCura
        // Skal brugeren have adgang til cura?
        public bool IsCura { get; set; }

        //Mandatory = $false
        //[String]$CuraBrugerRolle
        // navnet på curabrugerrolle
        public string CuraBrugerRolle { get; set; }

        //Mandatory = $false
        //[Boolean]$IsCuraPlanner
        // skal medarbejder være cura planlæggeR?
        public bool IsCuraPlanner { get; set; }

        //Mandatory = $false
        //[Boolean]$IsCuraFMK
        //OBS: hvis medarbejderen skal anvende FMK, skal der bestilles et NemID.
        // Skal medarbejderen anvende FMK?
        public bool IsCuraFMK { get; set; }

        //Mandatory = $false
        //[String]$CuraFMKID
        // medarbejderens FKM ID
        public string CuraFMKID { get; set; }


        //Mandatory = $true
        //[Boolean]$IsKMDbruger
        // skal medarbejderen have en KMD bruger?
        public bool IsKMDbruger { get; set; }

        //Mandatory = $false
        //[String]$KMDUserProfiles
        // de systemer medarbejderen skal have adgang til (F.eks. Sag, Doc2Archive, Institution)
        public string KMDUserProfiles { get; set; }


        //Mandatory = $false
        //[Boolean]$IsKMDOpusOkonomiBilagsbehandling
        // skal medarbejder have adgang til Opus Økonomi/billagsbehandling?
        public bool IsKMDOpusOkonomiBilagsbehandling { get; set; }

        //Mandatory = $false
        //[String]$KMDUserProfiles
        //Indtast Profitcenter/omkostningssted:
        public string KMDProfitcenterOmkostningssted { get; set; }

        //Mandatory = $false
        //[Boolean]$IsKMDFaktura
        //Skal medarbejderen modtage fakturarer?
        public bool IsKMDFaktura { get; set; }

        //Mandatory = $false
        //[String]$KMDOpusOkonomiEan
        //Indtast EAN nummer:
        //anderledes fra det EAN der betaler for FMK
        public string KMDOpusOkonomiEan { get; set; }

        //Mandatory = $false
        //[Boolean]$IsKMDBudgetOmplacering
        //Skal medarbejderen foretage budget omplaceringer?
        public bool IsKMDBudgetOmplacering { get; set; }

        //Mandatory = $false
        //[Boolean]$IsKMDMitForventedeRegnskab
        //Skal medarbejderen benytte Mit forventede regnskab?
        public bool IsKMDMitForventedeRegnskab { get; set; }


        //Mandatory = $true
        //[Boolean]$IsKMDLoenOgPersonale
        // skal medarbejderen have adgang til OPUS Løn/personale (fravær)?
        public bool IsKMDLoenOgPersonale { get; set; }

        //Mandatory = $false
        //[String]$KMDOrgUnit
        //Indtast Organisationsenhed:
        //Vejledning til at finde nummer på Organisationsenhed.
        public string KMDOrgUnit { get; set; }



        /// <summary>
        /// Bemærkninger
        /// </summary>

        //Mandatory = $false
        //[String]$Bemaerkninger
        //Skriv evt. dine bemærkninger her:
        public string Bemaerkninger { get; set; }


        // cura login org
        // semikolon spereret array, fordi der ikke kan sendes arrays heh
        // mandatory ved cura, men ellers ikke
        public string CuraLoginORGs { get; set; }
    }
}
