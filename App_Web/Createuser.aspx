<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Createuser.aspx.cs" Inherits="App_Web.Createuser" %>

<html>
<head runat="server">
  <title>Bestil IT-Bruger</title>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
  <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" />
  <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
  <style type="text/css">
    body {
      background: #e6ece5;
      padding: 15px;
    }
    .requiredfield{
      color: red;
      padding-left: 5px;
      font-weight: 800;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      <Scripts>
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
      </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server"></telerik:RadAjaxLoadingPanel>


    <div class="container">
      <h3>Formular til oprettelse af IT bruger til ny medarbejder.</h3>
      <asp:Panel runat="server" ID="form_panel">
      <asp:Label runat="server" Visible="false" ID="lblTotaleFatale" />
      <h4>Oplysninger om medarbejderen</h4>
      <div class="form-group">
        <label for="CbNaermesteleder">Vælg medarbejder, der skal have IT bruger:</label><label class="requiredfield">*</label>
        <small id="emphelp2" class="form-text text-danger">OBS: Du kan kun oprette en medarbejder der har været oprettet i lønsystemet i minimum et døgn, og ikke i forvejen har et It-brugernavn.</small>
        <telerik:RadComboBox ID="CBOpus_medarbejdernr" runat="server" Height="150" EmptyMessage="Indtast medarbejderens navn" EnableLoadOnDemand="true" MarkFirstMatch="true" Width="100%" OnSelectedIndexChanged="CBOpus_medarbejdernr_SelectedIndexChanged" AutoPostBack="true">
          <WebServiceSettings Method="GetEmployee" Path="LoraWebservice.asmx" />
        </telerik:RadComboBox>
        <small id="emphelp" class="form-text text-muted">Søg på medarbejderens navn, og vælg den korrekte stilling og ansættelsessted.</small>
      </div>
      <div class="form-group">
        <div class="row">
          <div class="col-3">
            <label for="TxbFornavn">Medarbejderens fornavn:</label>
            <telerik:RadTextBox runat="server" ID="TxbFornavn" Enabled="false" Width="100%" BackColor="LightGray" EmptyMessage="Fornavn"/>
          </div>
          <div class="col-3">
            <label for="TxbEfternavn">Medarbejderens efternavn:</label>
            <telerik:RadTextBox runat="server" ID="TxbEfternavn" Enabled="false" Width="100%" BackColor="LightGray" EmptyMessage="Efternavn" />
          </div>
          <div class="col-6">
            <label for="TxbVistnavn">Medarbejderens viste navn:</label><label class="requiredfield">*</label>
            <telerik:RadTextBox runat="server" ID="TxbVistnavn" Width="100%" EmptyMessage="Vist navn" />
            <small id="vistnanvhelp" class="form-text text-muted">E-mail adresse genereres på baggrund af dette, og navn vises i Outlook/Skype.</small>
          </div>
        </div>
      </div>

      <h4>Oplysninger om medarbejderens position i Skanderborg Kommune</h4>
      <div class="form-group">
        <label for="CbManagerSamaccount">Vælg medarbejderens nærmeste leder:</label><label class="requiredfield">*</label>
        <telerik:RadComboBox ID="CbManagerSamaccount" runat="server" Height="150" EmptyMessage="Indtast leders navn" EnableLoadOnDemand="true" MarkFirstMatch="true" Width="100%">
          <WebServiceSettings Method="GetManager" Path="LoraWebservice.asmx" />
        </telerik:RadComboBox>
        <small id="lederhelp" class="form-text text-muted">Søg på lederens navn.</small>
        <small id="lederhelp2" class="form-text text-muted">Medarbejderens IT-bruger får tilknyttet den valgte leder som værende sin nærmeste leder.</small>
      </div>
      <div class="form-group">
        <label for="CbCoworkerSamaccount">Vælg medarbejderens nærmeste kollega, med lignende funktion:</label><label class="requiredfield">*</label>
        <telerik:RadComboBox ID="CbCoworkerSamaccount" runat="server" Height="150" EmptyMessage="Indtast kollegas navn" EnableLoadOnDemand="true" MarkFirstMatch="true" Width="100%">
          <WebServiceSettings Method="GetCoworker" Path="LoraWebservice.asmx" />
        </telerik:RadComboBox>
        <small id="ligmedhelp" class="form-text text-muted">Søg på medarbejderens navn.</small>
        <small id="ligmedhelp2" class="form-text text-muted">Find en eksisterende kollega, der er ansat i samme afdeling som medarbejderen.</small>
      </div>
      <div class="form-group">
        <asp:label runat="server" ID="ErrEmail" for="RBIsEmail">Skal der oprettes e-mail til medarbejderen?</asp:label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RBIsEmail" runat="server" Width="100" RepeatDirection="Horizontal">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
        <small id="emailhelp" class="form-text text-muted">OBS: Hvis der skal bestilles et NemID til brugeren (f.eks. i forbindelse med CuraFMK) skal der oprettes en e-mail.</small>
      </div>

      <h4>Oplysninger om medarbejderens telefoni</h4>
      <div class="form-group">
        <label for="TxbKontaktTelefonNummer">Indtast medarbejderens kontakt telefon nummer:</label>
        <telerik:RadTextBox runat="server" ID="TxbKontaktTelefonNummer" Width="100%" EmptyMessage="Telefonnummer (Ikke til mobil)" />
        <small id="TxbKontaktTelefonNummerhelp" class="form-text text-muted">Angives hvis medarbejderen træffes på et eksisterende telefonnummer fx et fællesnummer. Ellers efterlades feltet tomt.</small>
      </div>
      <div class="form-group">
        <asp:label runat="server" ID="ErrSkype" for="RbIsSkype">Skal medarbejderen have et personligt lokalnummer f.x til Skype?</asp:label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RbIsSkype" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsSkype_SelectedIndexChanged" AutoPostBack="true">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <asp:Panel ID="panelSkype" Visible="false" runat="server">
        <div class="form-group">
          <asp:label runat="server" ID="errRingGruppe" for="RbIsRingegruppe">Skal medarbejderen tilknyttes ringe-/svargruppe?</asp:label><label class="requiredfield">*</label>
          <asp:RadioButtonList ID="RbIsRingegruppe" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsRingegruppe_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="Ja"></asp:ListItem>
            <asp:ListItem Text="Nej"></asp:ListItem>
          </asp:RadioButtonList>
        </div>
        <asp:Panel ID="panelRingeGrupppe" Visible="false" runat="server">
          <div class="form-group">
            <label for="TxbRingegruppeNummer">Indtast nummer på ringe-/svargruppen:</label><label class="requiredfield">*</label>
            <telerik:RadTextBox runat="server" ID="TxbRingegruppeNummer" Width="100%" EmptyMessage="Indtast nummer" />
          </div>
        </asp:Panel>
      </asp:Panel>
      <div class="form-group">
        <label for="TxbMobilNummer">Indtast evt. medarbejderens kontakt mobilnummer:</label>
        <telerik:RadTextBox runat="server" ID="TxbMobilNummer" Width="100%" EmptyMessage="Indtast Mobilnummer" />
        <small id="mobilehelp" class="form-text text-muted">Nummeret kan fremsøges i Outlook/Skype</small>
      </div>

      <h4>Vælg øvrige rettigheder</h4>
      <div class="form-group">
        <asp:label runat="server" ID="errDistributionslister" for="RbIsDistributionslister">Skal medarbejderen tilknyttes interne post-/distributionslister ud over standard for afdelingen?</asp:label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RbIsDistributionslister" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsDistributionslister_SelectedIndexChanged" AutoPostBack="true">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <asp:Panel runat="server" Visible="false" ID="panelDistributionslister">
        <div class="form-group">
          <label for="TxbDistributionslisterNavne">Angiv præcis navn på post-/distributionslister:</label><label class="requiredfield">*</label>
          <telerik:RadTextBox runat="server" ID="TxbDistributionslisterNavne" Width="100%" EmptyMessage="Indtast distributionslister, adskil med komma" />
        </div>
      </asp:Panel>
      <div class="form-group">
        <asp:label runat="server" ID="errFallespostkasser" for="RbIsFaellespostkasser">Skal medarbejderen have rettigheder til fælles-/offentlige postkasser?</asp:label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RbIsFaellespostkasser" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsFaellespostkasser_SelectedIndexChanged" AutoPostBack="true">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <asp:Panel runat="server" Visible="false" ID="panelFallespostkasser">
        <div class="form-group">
          <label for="TxbFaellespostkasserNavne">Angiv præcis navn på fælles-/offentlige postkasser:</label><label class="requiredfield">*</label>
          <telerik:RadTextBox runat="server" ID="TxbFaellespostkasserNavne" Width="100%" EmptyMessage="Indtast fællespostkasser, adskil med komma" />
          <small id="fallespostkasserhelp" class="form-text text-muted">Bemærk at medarbejderen selv skal tilknytte postkasser manuelt til Outlook.</small>
        </div>
      </asp:Panel>

      <h4>Vælg øvrige programmer</h4>
      <div class="form-group">
        <asp:label runat="server" ID="errNemId" for="RbIsNemID">NemID</asp:label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RbIsNemID" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsNemID_SelectedIndexChanged" AutoPostBack="true">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
        <small id="nemidhelp" class="form-text text-muted">OBS: hvis der bestilles NemID, skal medarbejderen også have en e-mail.</small>
      </div>
      <asp:Panel ID="panelNemId" runat="server" Visible="false">
        <div class="form-group">
          <label for="TxbEan">Indtast EAN nummer, som betaler for NemID:</label><label class="requiredfield">*</label>
          <telerik:RadTextBox runat="server" ID="TxbEan" Width="100%" />
        </div>
      </asp:Panel>

      <div class="form-group">
        <asp:Label runat="server" id="errCura">Cura</asp:Label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RbIsCura" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsCura_SelectedIndexChanged" AutoPostBack="true">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <asp:Panel ID="panelCura" runat="server" Visible="false">
        <div class="form-group">
          <label for="DDLCuraRolle">Vælg Cura Brugerrolle:</label><label class="requiredfield">*</label>
          <telerik:RadDropDownList runat="server" ID="DdlCuraBrugerRolle" Width="100%" />
         </div>
         <asp:Panel ID="panelCuraPlanner" runat="server" Visible="false">
            <div class="form-group">
              <asp:label runat="server" ID="errCuraPlaner">Er personen ressourceplanlægger?</asp:label><label class="requiredfield">*</label>
              <asp:RadioButtonList ID="RbIsCuraPlanner" runat="server" Width="100" RepeatDirection="Horizontal">
                <asp:ListItem Text="Ja"></asp:ListItem>
                <asp:ListItem Text="Nej"></asp:ListItem>
              </asp:RadioButtonList>
            </div>
        </asp:Panel>
        <asp:Panel ID="panelCuraFMK" runat="server" Visible="false">
          <div class="form-group">
            <asp:label runat="server" ID="errRbIsCuraFMK">Skal medarbejderen anvende FMK?</asp:label><label class="requiredfield">*</label>
            <asp:RadioButtonList ID="RbIsCuraFMK" runat="server" Width="100" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsCuraFMK_SelectedIndexChanged">
              <asp:ListItem Text="Ja"></asp:ListItem>
              <asp:ListItem Text="Nej"></asp:ListItem>
            </asp:RadioButtonList>
            <small id="fkmhelp" class="form-text text-muted">OBS: hvis medarbejderen skal anvende FMK, skal der bestilles et NemID.</small>
          </div>
          <asp:Panel ID="panelCuraFMKID" runat="server" Visible="false">
            <div class="form-group">
              <label for="TxbCuraFMKID">Indtast FMK ID:</label><label class="requiredfield">*</label>
              <telerik:RadTextBox runat="server" ID="TxbCuraFMKID" Width="100%" />
            </div>
          </asp:Panel>
        </asp:Panel> 
        <asp:Panel runat="server" ID="panelCuraORGLOG" Visible="false">
          <div class="form-group">
            <asp:label id="errCuraLOrg" runat="server">Vælg Cura login organisationer</asp:label><label class="requiredfield">*</label>
            <div class="row">
              <div class="col-11">
              <telerik:RadDropDownList runat="server" ID="ddl_curaLOrg" Width="100%" />
              </div>
              <div class="col-1">
              <telerik:RadButton runat="server" ID="btn_add_curaLOrg" Text="Tilføj" OnClick="btn_add_curaLOrg_Click" AutoPostBack="true"/>
                </div>
            </div>
            <small id="curalorghelp" class="form-text text-muted">OBS: der skal vælges minimum én login organisation.</small>
          </div>
          <div class="form-group">
            <label>Valgte login organisationer:</label>
            <telerik:RadGrid runat="server" ID="grid_curaLOrg" RenderMode="Lightweight" AllowPaging="false" AutoGenerateColumns="false" AllowMultiRowEdit="false"
              OnItemCommand="grid_curaLOrg_ItemCommand">
              <MasterTableView ShowHeader="false" ShowFooter="false" DataKeyNames="curaLOrg">
                <Columns>
                  <telerik:GridBoundColumn UniqueName="curaLOrg" DataField="curaLOrg" />
                  <telerik:GridTemplateColumn UniqueName="delete" HeaderStyle-Width="5%">
                    <ItemTemplate>
                      <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Fjern" OnClientClick="javascript:if(!confirm('Vil du fjerne login organisationen?')){return false};"
                        ImageUrl="delete.png" CommandName="delete" />
                    </ItemTemplate>
                  </telerik:GridTemplateColumn>
                </Columns>
              </MasterTableView>
            </telerik:RadGrid>
          </div>
        </asp:Panel>
      </asp:Panel>

      <div class="form-group">
        <asp:label runat="server" ID="errKMDBruger" for="RbIsKMDbruger">KMD bruger</asp:label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RbIsKMDbruger" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsKMDbruger_SelectedIndexChanged" AutoPostBack="true">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <asp:Panel ID="panelKMD" runat="server" Visible="false">
        <div class="form-group">
          <label for="TxbKMDUserProfiles">Indtast de KMD systemer medarbejderen skal have adgang til:</label><label class="requiredfield">*</label>
          <telerik:RadTextBox runat="server" ID="TxbKMDUserProfiles" EmptyMessage="Indtast systemer, adskild med komma" width="100%" />
          <small id="kmdbrugerprofilerhelp" class="form-text text-muted">Skriv de KMD Systemer som medarbejderen skal have adgang til, f.eks. Sag, Doc2Archive, Vagtplan mv..</small>
        </div>
      </asp:Panel>
      
      <div class="form-group">
        <asp:label runat="server" ID="errOpusOko" for="RbIsKMDOpusOkonomiBilagsbehandling">Opus Økonomi/bilagsbehandling?</asp:label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RbIsKMDOpusOkonomiBilagsbehandling" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsKMDOpusOkonomiBilagsbehandling_SelectedIndexChanged" AutoPostBack="true">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <asp:Panel runat="server" ID="panelOpusOko" Visible="false">
        <div class="form-group">
          <label for="TxbKMDProfitcenterOmkostningssted">Indtast Profitcenter/omkostningssted:</label><label class="requiredfield">*</label>
          <telerik:RadTextBox runat="server" ID="TxbKMDProfitcenterOmkostningssted" Width="100%" />
          <small id="profitcenterhelp" class="form-text text-muted"><a href="https://skbkom.sharepoint.com/:b:/g/EeDKq2swZu5Evgfi8d65J7EB-mXD7r24VzSEGk5tQiq2_w" target="_blank">Vejledning til at finde nummer på Profitcenter.</a></small>
        </div>
        <div class="form-group">
          <asp:label runat="server" ID="errFaktura" for="RbIsKMDFaktura">Skal medarbejderen modtage fakturarer?</asp:label><label class="requiredfield">*</label>
          <asp:RadioButtonList ID="RbIsKMDFaktura" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsKMDFaktura_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="Ja"></asp:ListItem>
            <asp:ListItem Text="Nej"></asp:ListItem>
          </asp:RadioButtonList>
        </div>
        <asp:Panel runat="server" ID="panelFaktura" Visible="false">
          <div class="form-group">
            <label for="TxbKMDOpusOkonomiEan">Indtast EAN nummer:</label><label class="requiredfield">*</label>
            <telerik:RadTextBox runat="server" ID="TxbKMDOpusOkonomiEan" Width="100%" />
            <small id="fakturahelp" class="form-text text-muted"><a href="https://www.skanderborg.dk/erhverv/leverandoer-til-kommunen/kommunens-ean-numre" target="_blank">Oversigt over kommunens EAN numre</a></small>
          </div>
        </asp:Panel>
        <div class="form-group">
          <asp:label runat="server" ID="errBudgetOmplacering" for="RbIsKMDBudgetOmplacering">Skal medarbejderen foretage budget omplaceringer?</asp:label><label class="requiredfield">*</label>
          <asp:RadioButtonList ID="RbIsKMDBudgetOmplacering" runat="server" Width="100" RepeatDirection="Horizontal">
            <asp:ListItem Text="Ja"></asp:ListItem>
            <asp:ListItem Text="Nej"></asp:ListItem>
          </asp:RadioButtonList>
        </div>
        <div class="form-group">
          <asp:label runat="server" ID="errMitForventedeRegnskab" for="RbIsKMDMitForventedeRegnskab">Skal medarbejderen benytte Mit forventede regnskab?</asp:label><label class="requiredfield">*</label>
          <asp:RadioButtonList ID="RbIsKMDMitForventedeRegnskab" runat="server" Width="100" RepeatDirection="Horizontal">
            <asp:ListItem Text="Ja"></asp:ListItem>
            <asp:ListItem Text="Nej"></asp:ListItem>
          </asp:RadioButtonList>
        </div>
      </asp:Panel>
      <div class="form-group">
        <asp:label runat="server" ID="errOpusLonPersonale" for="RbIsKMDLoenOgPersonale">OPUS Løn/personale (fravær)?</asp:label><label class="requiredfield">*</label>
        <asp:RadioButtonList ID="RbIsKMDLoenOgPersonale" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsKMDLoenOgPersonale_SelectedIndexChanged" AutoPostBack="true">
          <asp:ListItem Text="Ja"></asp:ListItem>
          <asp:ListItem Text="Nej"></asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <asp:Panel runat="server" ID="panelOpusLonPersonale" Visible="false">
        <div class="form-group">
          <label for="TxbKMDOrgUnit">Indtast Organisationsenhed:</label><label class="requiredfield">*</label>
          <telerik:RadTextBox runat="server" ID="TxbKMDOrgUnit" Width="100%" />
          <small id="orgunithelp" class="form-text text-muted"><a href="https://skbkom.sharepoint.com/:b:/g/EXTqoMTRAGlFnrTzh6zgPEoBjOrq9T5Ec6Lwha1BXdfAxg" target="_blank">Vejledning til at finde nummer på Organisationsenhed.</a></small>
        </div>
      </asp:Panel>
      
      
      <div class="form-group">
        <label for="TxbBemaerkninger">Skriv evt. dine bemærkninger her:</label>
        <telerik:RadTextBox runat="server" ID="TxbBemaerkninger" TextMode="MultiLine" Width="100%" Height="100px" />
        <small id="bemarkhelp" class="form-text text-muted">Findes programmer ikke på listen ovenfor, kan man skrive i bemærkninger her.</small>
        <small id="bemarkhelp2" class="form-text text-muted">Ønskes specialprogrammer og rettigheder som fx Educa, Targit mv, bestilles disse via 7913-systemet under Bestil/Godkend brugeradgange > Special programmer eller Brugeradministration.</small>
      </div>
      </asp:Panel>
      <telerik:RadButton runat="server" AutoPostBack="true" ID="Button_submit" ButtonType="StandardButton" Text="Opret bruger" OnClick="Button_submit_Click" />
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
      <AjaxSettings> 
        <telerik:AjaxSetting AjaxControlID="Button_submit">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="form_panel" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="CBOpus_medarbejdernr">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="TxbFornavn" LoadingPanelID="RadAjaxLoadingPanel1" />
            <telerik:AjaxUpdatedControl ControlID="TxbEfternavn" LoadingPanelID="RadAjaxLoadingPanel1" />
            <telerik:AjaxUpdatedControl ControlID="TxbVistnavn" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsSkype">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelSkype" LoadingPanelID="RadAjaxLoadingPanel1" />
            <telerik:AjaxUpdatedControl ControlID="panelRingeGrupppe" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsRingegruppe">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelRingeGrupppe" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsDistributionslister">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelDistributionslister" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsFaellespostkasser">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelFallespostkasser" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsNemID">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelNemId" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsCura">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelCura" LoadingPanelID="RadAjaxLoadingPanel1" />
            <telerik:AjaxUpdatedControl ControlID="panelCuraORGLOG" LoadingPanelID="RadAjaxLoadingPanel1" />
            <telerik:AjaxUpdatedControl ControlID="panelCuraPlanner" LoadingPanelID="RadAjaxLoadingPanel1" />
            <telerik:AjaxUpdatedControl ControlID="panelCuraFMK" LoadingPanelID="RadAjaxLoadingPanel1" />
            <telerik:AjaxUpdatedControl ControlID="panelCuraFMKID" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsCuraFMK">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelCuraFMKID" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="BtnAddCuraORGLOG">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="grid_curaLOrg" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>


        <telerik:AjaxSetting AjaxControlID="RbIsKMDbruger">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelKMD" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsKMDOpusOkonomiBilagsbehandling">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelOpusOko" LoadingPanelID="RadAjaxLoadingPanel1" />  
            <telerik:AjaxUpdatedControl ControlID="panelFaktura" LoadingPanelID="RadAjaxLoadingPanel1" />
            <telerik:AjaxUpdatedControl ControlID="panelOpusLonPersonale" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsKMDFaktura">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelFaktura" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="RbIsKMDLoenOgPersonale">
          <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="panelOpusLonPersonale" LoadingPanelID="RadAjaxLoadingPanel1" />
          </UpdatedControls>
        </telerik:AjaxSetting>
      </AjaxSettings>
    </telerik:RadAjaxManager>
  </form>
</body>
</html>
