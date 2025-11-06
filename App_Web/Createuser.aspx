<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Createuser.aspx.cs" Inherits="App_Web.Createuser" %>

<html>
<head runat="server">
    <title>Bestil IT-Bruger</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Styles/StyleSheet_ITBrugeroprettelse.css" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
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
            <h1>Oprettelse af IT bruger til ny medarbejder.</h1>
            <asp:Panel runat="server" ID="form_panel">
                <asp:Label runat="server" Visible="false" ID="lblTotaleFatale" />
                <h5>Oplysninger om medarbejderen</h5>
                <div class="form-group">
                    <%--<label for="CbNaermesteleder">Vælg medarbejder, der skal have IT bruger:</label>--%>
                    <div for="CbNaermesteleder" class="inputLabel">Vælg medarbejder, der skal have IT bruger:</div>
                    <div class="requiredfield">*</div>
                    <small id="emphelp2" class="form-text text-danger">OBS: Du kan kun oprette en medarbejder der har været oprettet i lønsystemet i minimum et døgn, og ikke i forvejen har et It-brugernavn.</small>
                    <telerik:RadComboBox ID="CBOpus_medarbejdernr" runat="server" Height="150" EmptyMessage="Indtast medarbejderens navn eller cpr" EnableLoadOnDemand="true" MarkFirstMatch="true" Width="100%" OnSelectedIndexChanged="CBOpus_medarbejdernr_SelectedIndexChanged" AutoPostBack="true">
                        <WebServiceSettings Method="GetEmployeeWithoutADUser" Path="EmployeeWebservice.asmx" />
                    </telerik:RadComboBox>
                    <div class="smallLabel">Søg på medarbejderens navn eller cpr, og vælg den korrekte stilling og ansættelsessted.</div>
                    <%--<small id="emphelp" class="form-text text-muted">Søg på medarbejderens navn eller cpr, og vælg den korrekte stilling og ansættelsessted.</small>--%>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-3">
                            <div for="TxbFornavn" class="inputLabel">Medarbejderens fornavn:</div>
                            <telerik:RadTextBox runat="server" ID="TxbFornavn" Enabled="false" Width="100%" BackColor="LightGray" EmptyMessage="Fornavn" />
                        </div>
                        <div class="col-3">
                            <div for="TxbEfternavn" class="inputLabel">Medarbejderens efternavn:</div>
                            <telerik:RadTextBox runat="server" ID="TxbEfternavn" Enabled="false" Width="100%" BackColor="LightGray" EmptyMessage="Efternavn" />
                        </div>
                        <div class="col-6">
                            <div for="TxbVistnavn" class="inputLabel">Medarbejderens viste navn som også danner e-mail adresse:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbVistnavn" Width="100%" EmptyMessage="Vist navn" autocomplete="off" />
                            <telerik:RadLabel runat="server" ID="LblStilling" Visible="false"></telerik:RadLabel>
                            <div class="smallLabel">E-mail adresse genereres på baggrund af dette, og navn vises i Outlook/Skype.</div>
                            <%--<small id="vistnanvhelp" class="form-text text-muted">E-mail adresse genereres på baggrund af dette, og navn vises i Outlook/Skype.</small>--%>
                        </div>
                    </div>
                </div>


                <h5>Oplysninger om medarbejderens position i Skanderborg Kommune</h5>
                <div class="form-group">
                    <div for="CbCoworkerSamaccount" class="inputLabel">Vælg medarbejderens nærmeste kollega i samme afdeling og med samme Q-drev:</div>
                    <div class="requiredfield">*</div>
                    <telerik:RadComboBox ID="CbCoworkerSamaccount" runat="server" Height="150" EmptyMessage="Indtast kollegas navn" EnableLoadOnDemand="true" MarkFirstMatch="true" Width="100%">
                        <WebServiceSettings Method="GetCoworkerFromAD" Path="EmployeeWebservice.asmx" />
                    </telerik:RadComboBox>
                    <div class="smallLabel">Find en eksisterende kollega, der er ansat i samme afdeling som medarbejderen (Søg på navn).</div>
                    <%--                    <small id="ligmedhelp" class="form-text text-muted">Søg på medarbejderens navn.</small>
                    <small id="ligmedhelp2" class="form-text text-muted">Find en eksisterende kollega, der er ansat i samme afdeling som medarbejderen.</small>--%>
                </div>



                <h5>Oplysninger om medarbejderens telefoni</h5>
                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorSkype">Skal medarbejderen have et personligt lokalnummer?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                </div>

                <div class="form-group">
                    <asp:RadioButtonList ID="RbIsSkype" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsSkype_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="smallLabel">Bruger kan godt deltage i online møder samt ringe internt uden et lokalnummer.</div>

                </div>
                <asp:Panel ID="panelSkype" Visible="false" runat="server" CssClass="leftIndent">
                    <div class="radioButtonHeader">
                        <telerik:RadLabel runat="server" ID="ErrorRingGruppe">Skal medarbejderen tilknyttes ringe-/svargruppe?</telerik:RadLabel>
                        <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                    </div>

                    <div class="form-group">
                        <%--<asp:Label runat="server" ID="errRingGruppe" for="RbIsRingegruppe">Skal medarbejderen tilknyttes ringe-/svargruppe?</asp:Label><label class="requiredfield">*</label>--%>
                        <asp:RadioButtonList ID="RbIsRingegruppe" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsRingegruppe_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Ja"></asp:ListItem>
                            <asp:ListItem Text="Nej"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <asp:Panel ID="panelRingeGrupppe" Visible="false" runat="server">
                        <div class="form-group">
                            <div for="TxbRingegruppeNummer" class="inputLabel">Indtast nummer på ringe-/svargruppen:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRingegruppeNummer" Width="100%" EmptyMessage="Indtast nummer" />
                        </div>
                    </asp:Panel>
                </asp:Panel>
                <div class="form-group">
                    <div for="TxbMobilNummer" class="inputLabel">Indtast evt. medarbejderens kontakt mobilnummer:</div>
                    <telerik:RadTextBox runat="server" ID="TxbMobilNummer" Width="100%" EmptyMessage="Indtast Mobilnummer" />
                    <div class="smallLabel">Nummeret kan fremsøges i Outlook/Teams.</div>

                </div>


                <h5>Oplysninger om distributionslister og postkasser</h5>

                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorDistributionslister">Skal medarbejderen tilknyttes interne post-/distributionslister ud over standard for afdelingen?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <%--<asp:Label runat="server" ID="errDistributionslister" for="RbIsDistributionslister">Skal medarbejderen tilknyttes interne post-/distributionslister ud over standard for afdelingen?</asp:Label><label class="requiredfield">*</label>--%>
                    <asp:RadioButtonList ID="RbIsDistributionslister" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsDistributionslister_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <asp:Panel runat="server" Visible="false" ID="panelDistributionslister" CssClass="leftIndent">
                    <div class="form-group">
                        <%--<label for="TxbDistributionslisterNavne">Angiv præcis navn på post-/distributionslister:</label><label class="requiredfield">*</label>--%>
                        <div for="TxbDistributionslisterNavne" class="inputLabel">Angiv præcist navn på post-/distributionslister:</div>
                        <div class="requiredfield">*</div>
                        <telerik:RadTextBox runat="server" ID="TxbDistributionslisterNavne" Width="100%" EmptyMessage="Indtast distributionslister, adskil med komma" />
                    </div>
                </asp:Panel>
                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorFallespostkasser">Skal medarbejderen have rettigheder til fælles-/offentlige postkasser?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <%--<asp:Label runat="server" ID="errFallespostkasser" for="RbIsFaellespostkasser">Skal medarbejderen have rettigheder til fælles-/offentlige postkasser?</asp:Label><label class="requiredfield">*</label>--%>
                    <asp:RadioButtonList ID="RbIsFaellespostkasser" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsFaellespostkasser_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <asp:Panel runat="server" Visible="false" ID="panelFallespostkasser" CssClass="leftIndent">
                    <div class="form-group">
                        <%--<label for="TxbFaellespostkasserNavne">Angiv præcis navn på fælles-/offentlige postkasser:</label><label class="requiredfield">*</label>--%>
                        <div for="TxbFaellespostkasserNavne" class="inputLabel">Angiv præcist navn på fælles-/offentlige postkasser:</div>
                        <div class="requiredfield">*</div>
                        <telerik:RadTextBox runat="server" ID="TxbFaellespostkasserNavne" Width="100%" EmptyMessage="Indtast fællespostkasser, adskil med komma" />
                        <div class="smallLabel">Bemærk at medarbejderen selv skal tilknytte postkasser manuelt til Outlook.</div>
                        <%--<small id="fallespostkasserhelp" class="form-text text-muted">Bemærk at medarbejderen selv skal tilknytte postkasser manuelt til Outlook.</small>--%>
                    </div>
                </asp:Panel>



                <h5>Vælg øvrige programmer</h5>

                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorCura">Cura</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <%--<asp:Label runat="server" ID="errCura">Cura</asp:Label><label class="requiredfield">*</label>--%>

                    <asp:RadioButtonList ID="RbIsCura" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsCura_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>


                <asp:Panel ID="panelCura" runat="server" Visible="false" CssClass="leftIndent">
                    <div class="radioButtonHeader">
                        <telerik:RadLabel runat="server" ID="ErrorCuraRole">Vælg Cura Brugerroller:</telerik:RadLabel>
                        <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                    </div>
                    <div class="form-group">
                        <%--<asp:Label runat="server" ID="errCuraRole">Vælg Cura Brugerroller:</asp:Label><label class="requiredfield">*</label>--%>

                        <div class="row">
                            <div class="col-11">
                                <telerik:RadDropDownList runat="server" ID="DdlCuraBrugerRolle" Width="100%" />
                            </div>
                            <div class="col-1">
                                <telerik:RadButton runat="server" ID="btn_add_cura_rolle" Text="Tilføj" OnClick="btn_add_cura_rolle_Click" AutoPostBack="true" />
                            </div>
                        </div>
                        <div class="smallLabel">OBS: der skal vælges minimum én rolle.</div>
                        <%--<small id="curalrolehelp" class="form-text text-muted">OBS: der skal vælges minimum én login organisation.</small>--%>
                    </div>
                    <div class="form-group">
                        <%--<label>Valgte curaroller:</label>--%>
                        <div style="padding-left: 5px; padding-bottom: 0px; margin-bottom: 0px;">Valgte curaroller:</div>
                        <telerik:RadGrid runat="server" ID="grid_curaRoles" RenderMode="Lightweight" AllowPaging="false" AutoGenerateColumns="false" AllowMultiRowEdit="false"
                            OnItemCommand="grid_curaRoles_ItemCommand">
                            <MasterTableView ShowHeader="false" ShowFooter="false" DataKeyNames="curaRole">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="curaRole" DataField="curaRole" />
                                    <telerik:GridTemplateColumn UniqueName="delete" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="Fjern" OnClientClick="javascript:if(!confirm('Vil du fjerne login organisationen?')){return false};"
                                                ImageUrl="delete.png" CommandName="delete" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <asp:Panel ID="panelCuraPlanner" runat="server" Visible="false">
                        <div class="radioButtonHeader">
                            <telerik:RadLabel runat="server" ID="ErrorCuraPlaner">Er personen ressourceplanlægger?</telerik:RadLabel>
                            <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                        </div>
                        <div class="form-group">
                            <%--<asp:Label runat="server" ID="errCuraPlaner">Er personen ressourceplanlægger?</asp:Label><label class="requiredfield">*</label>--%>
                            <asp:RadioButtonList ID="RbIsCuraPlanner" runat="server" Width="100" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Ja"></asp:ListItem>
                                <asp:ListItem Text="Nej"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panelCuraFMK" runat="server" Visible="false">
                        <div class="radioButtonHeader">
                            <telerik:RadLabel runat="server" ID="ErrorRbIsCuraFMK">Skal medarbejderen anvende FMK?</telerik:RadLabel>
                            <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                        </div>
                        <div class="form-group">
                            <%--<asp:Label runat="server" ID="errRbIsCuraFMK">Skal medarbejderen anvende FMK?</asp:Label><label class="requiredfield">*</label>--%>
                            <asp:RadioButtonList ID="RbIsCuraFMK" runat="server" Width="100" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsCuraFMK_SelectedIndexChanged">
                                <asp:ListItem Text="Ja"></asp:ListItem>
                                <asp:ListItem Text="Nej"></asp:ListItem>
                            </asp:RadioButtonList>
                            <div class="smallLabel">OBS: hvis medarbejderen skal anvende FMK, skal der bestilles et NemID.</div>
                            <%--<small id="fkmhelp" class="form-text text-muted">OBS: hvis medarbejderen skal anvende FMK, skal der bestilles et NemID.</small>--%>
                        </div>
                        <asp:Panel ID="panelCuraFMKID" runat="server" Visible="false">
                            <div class="form-group">
                                <%--<label for="TxbCuraFMKID">Indtast FMK autorisationsnummer:</label><label class="requiredfield">*</label>--%>
                                <div for="TxbCuraFMKID" class="inputLabel">Indtast FMK autorisationsnummer:</div>
                                <div class="requiredfield">*</div>
                                <telerik:RadTextBox runat="server" ID="TxbCuraFMKID" Width="100%" />
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panelCuraORGLOG" Visible="false">
                        <div class="radioButtonHeader">
                            <telerik:RadLabel runat="server" ID="ErrorCuraLOrg">Vælg Cura login organisationer</telerik:RadLabel>
                            <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                        </div>
                        <div class="form-group">
                            <%--<asp:Label ID="errCuraLOrg" runat="server">Vælg Cura login organisationer</asp:Label><label class="requiredfield">*</label>--%>
                            <%--<div for="TxbCuraFMKID" class="inputLabel">Vælg Cura login organisationer</div><div class="requiredfield">*</div>--%>
                            <div class="row">
                                <div class="col-11">
                                    <telerik:RadDropDownList runat="server" ID="ddl_curaLOrg" Width="100%" />
                                </div>
                                <div class="col-1">
                                    <telerik:RadButton runat="server" ID="btn_add_curaLOrg" Text="Tilføj" OnClick="btn_add_curaLOrg_Click" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="smallLabel">OBS: der skal vælges minimum én login organisation.</div>
                            <%--<small id="curalorghelp" class="form-text text-muted">OBS: der skal vælges minimum én login organisation.</small>--%>
                        </div>
                        <div class="form-group">
                            <%--<label>Valgte login organisationer:</label>--%>
                            <div style="padding-left: 5px; padding-bottom: 0px; margin-bottom: 0px;">Valgte login organisationer:</div>
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





                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorRollebaseret">KMD OPUS Rollebaseret indgang</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <%--<asp:Label runat="server" ID="errOpusOko" for="RbIsKMDOpusOkonomiBilagsbehandling">Opus Rollebaseret indgang</asp:Label><label class="requiredfield">*</label>--%>
                    <asp:RadioButtonList ID="RbIsRollebaseret" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsKMDOpusOkonomiBilagsbehandling_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <asp:Panel runat="server" ID="PanelRollebaseret" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <%--<label for="ComboRollebaseret">Vælg profil for brugeren der skal oprettes</label><label class="requiredfield">*</label>--%>
                            <div for="ComboRollebaseret" class="inputLabel">Vælg profil for brugeren der skal oprettes</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadComboBox runat="server" ID="ComboRollebaseret" Skin="Bootstrap" Width="100%" EmptyMessage="Vælg profil"
                                EnableLoadOnDemand="true"
                                ShowToggleImage="true" AllowCustomText="false" AutoPostBack="true"
                                CheckBoxes="false" OnSelectedIndexChanged="ComboRollebaseret_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Leder med personaleansvar" Value="LEDER" />
                                    <telerik:RadComboBoxItem Text="Administrativ medarbejder" Value="ADMINISTRATIV" />
                                    <telerik:RadComboBoxItem Text="Andet (Fx Revisor, Projektleder anlæg, Central Økonomi, Løn/HR medarbejder)" Value="ANDET" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelRollebaseret_Leder" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <h6>OPUS Økonomi</h6>
                            <%--<label for="TxbRollebaseretProfitcenter_Leder">Angiv det profitcenter/knude som leder er ansvarlig for:</label><label class="requiredfield">*</label>--%>
                            <div for="TxbRollebaseretProfitcenterAnsvar" class="inputLabel">Angiv det profitcenter som leder er ansvarlig for:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRollebaseretProfitcenterAnsvar" Width="100%" />
                            <small id="ProfitcenterHelp_Leder" class="form-text text-muted" style="margin-top: 0px; padding-top: 0px; padding-left: 5px;"><a href="https://intranet.skanderborg.dk/opus-rollebaseret-indgang?iswebpart=0" target="_blank">Se oversigt over profitcenter/profitcenterknuder.</a></small>
                        </div>

                        <div class="radioButtonHeader">
                            <telerik:RadLabel runat="server" ID="ErrorRollebaseretLeder">Kvittering eller bogføring af faktura/bilag?</telerik:RadLabel>
                            <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                        </div>
                        <div class="form-group">
                            <%--<asp:Label runat="server" ID="Label1" for="RbIsKMDFaktura">Skal medarbejderen modtage fakturarer?</asp:Label><label class="requiredfield">*</label>--%>
                            <asp:RadioButtonList ID="RbRollebaseretFaktura_Leder" runat="server" Width="100%" RepeatDirection="Vertical" OnSelectedIndexChanged="RbRollebaseretFaktura_Leder_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Leder skal selv bogføre eller kunne modtage fakturaer" Value="BOGFOER"></asp:ListItem>
                                <asp:ListItem Text="Leder skal ikke automatisk modtage fakturaer, men kun kvittere" Value="KVITTER"></asp:ListItem>
                                <asp:ListItem Text="Leder skal hverken modtage/kvittere eller bogføre fakturaer" Value="INTET"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelRollebaseret_LederBogfoering" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <%--<label for="TxbRollebaseretProfitcenter_LederBogfoering">Angiv Profitcenterknude/omkostningssted:</label><label class="requiredfield">*</label>--%>
                            <div for="TxbRollebaseretProfitcenter_LederBogfoering" class="inputLabel">Angiv Profitcenter:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRollebaseretProfitcenter_LederBogfoering" Width="100%" />
                            <small id="ProfitcenterHelp_LederBogfoering" class="form-text text-muted" style="margin-top: 0px; padding-top: 0px; padding-left: 5px;"><a href="https://intranet.skanderborg.dk/opus-rollebaseret-indgang?iswebpart=0" target="_blank">Se oversigt over profitcenter/profitcenterknuder.</a></small>
                        </div>
                        <div class="form-group">
                            <%--<label for="TxbRollebaseret_LederBogfoeringEAN">Angiv EAN-nummer der skal modtages fakturaer på:</label><label class="requiredfield">*</label>--%>
                            <div for="TxbRollebaseret_LederBogfoeringEAN" class="inputLabel">Angiv EAN-nummer der skal modtages fakturaer på:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRollebaseret_LederBogfoeringEAN" Width="100%" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelLoenPersonale" Visible="false">
                    <div class="leftIndent">
                        <h6>OPUS Løn og Personale</h6>
                        <%--<asp:Label runat="server">Leder oprettes som standard til at se egne medarbejdere.</asp:Label>--%>

                        <div class="radioButtonHeader">
                            <telerik:RadLabel runat="server" ID="ErrorRollebaseretLederSeeOther">Skal leder se andre medarbejdere udover egne?</telerik:RadLabel>
                            <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                        </div>
                        <div class="form-group">
                            <%--<asp:Label runat="server" ID="Label1" for="RbIsLeaderAbleToSeeOther">Skal leder se andre medarbejdere udover egne?</asp:Label><label class="requiredfield">*</label>--%>
                            <asp:RadioButtonList ID="RbIsLeaderAbleToSeeOther" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsLeaderAbleToSeeOther_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Ja"></asp:ListItem>
                                <asp:ListItem Text="Nej"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelRollebaseretSeAndre" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <%--<label for="TxbRollebaseret_SeAndre_OrgEnhed">Angiv organisationsenhed eller navn:</label><label class="requiredfield">*</label>--%>
                            <div for="TxbRollebaseret_SeAndre_OrgEnhed" class="inputLabel">Angiv organisationsenhed:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRollebaseret_SeAndre_OrgEnhed" Width="100%" />
                            <small id="OrgEnhedHelp_Leder" class="form-text text-muted" style="margin-top: 0px; padding-top: 0px; padding-left: 5px;"><a href="https://intranet.skanderborg.dk/opus-rollebaseret-indgang?iswebpart=0" target="_blank">Se oversigt over organisatoriske enheder.</a></small>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PanelAdministrativMedarbejder" Visible="false">
                    <div class="leftIndent">
                        <div class="radioButtonHeader">
                            <telerik:RadLabel runat="server" ID="ErrorRollebaseretAdministrativOpg">Vælg én eller flere opgaver:</telerik:RadLabel>
                            <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                        </div>
                        <div class="form-group">
                            <telerik:RadButton runat="server" ID="BtnRollebaseretAdmMedarb_OekonomiOpg" ButtonType="ToggleButton"
                                ToggleType="CheckBox" Skin="Bootstrap" Checked="false"
                                Value="" OnCheckedChanged="BtnRollebaseretAdmMedarb_OekonomiOpg_CheckedChanged">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="OPUS Økonomi opgaver" PrimaryIconUrl="Images/Checked_16px.png" PrimaryIconTop="3" PrimaryIconLeft="5" />
                                    <telerik:RadButtonToggleState Text="OPUS Økonomi opgaver" PrimaryIconUrl="Images/Unchecked_16px.png" PrimaryIconTop="3" PrimaryIconLeft="5" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>

                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelOpusOpg_Administrativ" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <%--<label for="TxbOpusOpg_Administrativ_Profitcenter">Angiv Profitcenterknude/omkostningssted:</label><label class="requiredfield">*</label>--%>
                            <div for="TxbRollebaseretAdmMedarb_EAN" class="inputLabel">Angiv Profitcenter:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbOpusOpg_Administrativ_Profitcenter" Width="100%" />
                            <small id="ProfitcenterHelp_AdmMedarbejder" class="form-text text-muted" style="margin-top: 0px; padding-top: 0px; padding-left: 5px;"><a href="https://intranet.skanderborg.dk/opus-rollebaseret-indgang?iswebpart=0" target="_blank">Se oversigt over profitcenter/profitcenterknuder.</a></small>
                        </div>
                        <div class="form-group">
                            <div for="TxbRollebaseretAdmMedarb_EAN_" class="inputLabel">Angiv EAN-nummer der skal modtages fakturaer på:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRollebaseretAdmMedarb_EAN" Width="100%" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelRollebaseretAdmMedarb_LoenPersonale" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <telerik:RadButton runat="server" ID="BtnRollebaseretAdmMedarb_LoenPersonale" ButtonType="ToggleButton"
                                ToggleType="CheckBox" Skin="Bootstrap" Checked="false"
                                Value="" OnCheckedChanged="BtnRollebaseretAdmMedarb_LoenPersonale_CheckedChanged">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="OPUS Løn og Personale opgaver" PrimaryIconUrl="Images/Checked_16px.png" PrimaryIconTop="3" PrimaryIconLeft="5" />
                                    <telerik:RadButtonToggleState Text="OPUS Løn og Personale opgaver" PrimaryIconUrl="Images/Unchecked_16px.png" PrimaryIconTop="3" PrimaryIconLeft="5" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelRollebaseretAdmMedarb_OrgEnh" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <div for="TxbRollebaseretAdmMedarb_OrgEnh" class="inputLabel">Angiv Organisationsenhed:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRollebaseretAdmMedarb_OrgEnh" Width="100%" />
                            <small id="OrgEnhedHelp_AdmMedarbejder" class="form-text text-muted" style="margin-top: 0px; padding-top: 0px; padding-left: 5px;"><a href="https://intranet.skanderborg.dk/opus-rollebaseret-indgang?iswebpart=0" target="_blank">Se oversigt over organisatoriske enheder.</a></small>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelRollebaseretAdmMedarb_Andet" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <telerik:RadButton runat="server" ID="BtnRollebaseretAdmMedarb_Andet" ButtonType="ToggleButton"
                                ToggleType="CheckBox" Skin="Bootstrap" Checked="false"
                                Value="" OnCheckedChanged="BtnRollebaseretAdmMedarb_Andet_CheckedChanged">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Andet" PrimaryIconUrl="Images/Checked_16px.png" PrimaryIconTop="3" PrimaryIconLeft="5" />
                                    <telerik:RadButtonToggleState Text="Andet" PrimaryIconUrl="Images/Unchecked_16px.png" PrimaryIconTop="3" PrimaryIconLeft="5" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelRollebaseretAdmMedarb_Rettigheder" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <div for="TxbRollebaseretAdmMedarb_Andet" class="inputLabel">Hvilke andre rettigheder i OPUS skal der tildeles:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRollebaseretAdmMedarb_Andet" Width="100%" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelRollebaseretAndet" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <%--<label for="TxbRollebaseretAdmMedarb_Andet">Hvilke rettigheder i OPUS skal tildeles:</label><label class="requiredfield">*</label>--%>
                            <div for="TxbRollebaseretAndet" class="inputLabel">Hvilke andre rettigheder i OPUS skal der tildeles:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxbRollebaseretAndet" Width="100%" />
                        </div>
                    </div>
                </asp:Panel>

                <%--       <asp:Panel runat="server" ID="panelOpusOko" Visible="false">--%>
                <%--                    <div class="form-group">
                        <label for="TxbKMDProfitcenterOmkostningssted">Indtast Profitcenter/omkostningssted:</label><label class="requiredfield">*</label>
                        <telerik:RadTextBox runat="server" ID="TxbKMDProfitcenterOmkostningssted" Width="100%" />
                        <small id="profitcenterhelp_3" class="form-text text-muted"><a href="https://skbkom.sharepoint.com/:b:/g/EeDKq2swZu5Evgfi8d65J7EB-mXD7r24VzSEGk5tQiq2_w" target="_blank">Vejledning til at finde nummer på Profitcenter.</a></small>
                    </div>--%>
                <%--                    <div class="form-group">
                        <asp:Label runat="server" ID="errFaktura" for="RbIsKMDFaktura">Skal medarbejderen modtage fakturarer?</asp:Label><label class="requiredfield">*</label>
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
                    </asp:Panel>--%>
                <%--                    <div class="form-group">
                        <asp:Label runat="server" ID="errBudgetOmplacering" for="RbIsKMDBudgetOmplacering">Skal medarbejderen foretage budget omplaceringer?</asp:Label><label class="requiredfield">*</label>
                        <asp:RadioButtonList ID="RbIsKMDBudgetOmplacering" runat="server" Width="100" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Ja"></asp:ListItem>
                            <asp:ListItem Text="Nej"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>--%>
                <%--                    <div class="form-group">
                        <asp:Label runat="server" ID="errMitForventedeRegnskab" for="RbIsKMDMitForventedeRegnskab">Skal medarbejderen benytte Mit forventede regnskab?</asp:Label><label class="requiredfield">*</label>
                        <asp:RadioButtonList ID="RbIsKMDMitForventedeRegnskab" runat="server" Width="100" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Ja"></asp:ListItem>
                            <asp:ListItem Text="Nej"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>--%>
                <%--     </asp:Panel>--%>
                <%--                <div class="form-group">
                    <asp:Label runat="server" ID="errOpusLonPersonale" for="RbIsKMDLoenOgPersonale">OPUS Løn/personale (fravær)?</asp:Label><label class="requiredfield">*</label>
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
                </asp:Panel>--%>

                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorKMDInstitution">KMD Institution I2?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RbIsKmdInstitution" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsKmdInstitution_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <asp:Panel ID="PanelKmdInstitution" runat="server" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">
                            <div for="ComboKmdInstitution" class="inputLabel">Funktionsrolle:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadComboBox runat="server" ID="ComboKmdInstitution" Skin="Bootstrap" Width="100%" EmptyMessage="Vælg funktionsrolle"
                                EnableLoadOnDemand="true"
                                ShowToggleImage="true" AllowCustomText="false" AutoPostBack="true"
                                CheckBoxes="false" OnSelectedIndexChanged="ComboKmdInstitution_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Institutionsleder og Administrativ i Dagtilbud og Skoler" Value="DAGTILBUD" />
                                    <telerik:RadComboBoxItem Text="Administrator og Pladsanviser" Value="ADMINISTRATOR" />
                                    <telerik:RadComboBoxItem Text="Dagpleje, morgenvisitering" Value="DAGPLEJE" />
                                    <telerik:RadComboBoxItem Text="Dagplejeleder" Value="DAGPLEJELEDER" />
                                    <telerik:RadComboBoxItem Text="Læseadgang" Value="LAESEADGANG" />
                                </Items>
                            </telerik:RadComboBox>
                            <a href="https://intranet.skanderborg.dk/kmd-institution-I2?item=2679758" class="smallLink" target="_blank" style="font-size:11px;">Se Børn og Unges side vedr. KMD Institution</a>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PanelKmdInsttution_Institutionsnummer" Visible="false">
                    <div class="leftIndent">
                        <div class="form-group">

                            <div for="TxtBoxKmdInsttution_Institutionsnummer" class="inputLabel">Angiv institutions ID:</div>
                            <div class="requiredfield">*</div>
                            <telerik:RadTextBox runat="server" ID="TxtBoxKmdInsttution_Institutionsnummer" Width="100%" />
                            <div class="smallLabel"><a href="https://intranet.skanderborg.dk/systemer-og-programmer?item=2679758" target="_blank">Se oversigt over Institutions ID’er.</a></div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorKMDBruger">KMD bruger, andre programmer?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RbIsKMDbruger" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsKMDbruger_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <asp:Panel ID="panelKMD" runat="server" Visible="false" CssClass="leftIndent">
                    <div class="form-group">
                        <%--<label for="TxbKMDUserProfiles">Indtast de KMD systemer medarbejderen skal have adgang til:</label><label class="requiredfield">*</label>--%>
                        <div for="TxbKMDUserProfiles" class="inputLabel">Indtast de KMD systemer medarbejderen skal have adgang til:</div>
                        <div class="requiredfield">*</div>
                        <telerik:RadTextBox runat="server" ID="TxbKMDUserProfiles" EmptyMessage="Indtast systemer, adskil med komma" Width="100%" />
                        <div class="smallLabel">Skriv de KMD Systemer som medarbejderen skal have adgang til - fx. Doc2Archive, Indkomst, CICS, Vagtplan mv.</div>
                        <%--<small id="kmdbrugerprofilerhelp" class="form-text text-muted">Skriv de KMD Systemer som medarbejderen skal have adgang til, f.eks. Sag, Doc2Archive, Vagtplan mv..</small>--%>
                    </div>
                </asp:Panel>



                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorTargit">Targit adgang?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <%--<asp:Label runat="server" ID="errTargit" for="RbIsTargit">Targit adgang?</asp:Label><label class="requiredfield">*</label>--%>
                    <asp:RadioButtonList ID="RbIsTargit" runat="server" Width="100" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                    <%--<small id="targithelp" class="form-text text-muted">Oprettelse videresendes automatisk til Budget- og Analyseteam.</small>--%>
                    <div class="smallLabel">Oprettelse videresendes automatisk til Budget- og Analyseteam.</div>
                </div>

                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorEducaPersonale">Educa Personale?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <%--<asp:Label runat="server" ID="errEducaPersonale" for="RbIsEducaPersonale">Educa Personale?</asp:Label><label class="requiredfield">*</label>--%>
                    <asp:RadioButtonList ID="RbIsEducaPersonale" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbIsEducaPersonale_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <asp:Panel ID="panel_educapersonale" runat="server" Visible="false" CssClass="leftIndent">
                    <div class="form-group">
                        <%--<label for="TxbEducaPersonale_unilogin">Unilogin:</label><label class="requiredfield">*</label>--%>
                        <div for="TxbEducaPersonale_unilogin" class="inputLabel">Uni ID (Findes i Aula eller på Kodeskift):</div>
                        <div class="requiredfield">*</div>
                        <telerik:RadTextBox runat="server" ID="TxbEducaPersonale_unilogin" Width="100%" />
                    </div>
                    <div class="form-group">
                        <div for="TxbEducaPersonale_skolekode" class="inputLabel">Skolekode:</div>
                        <div class="requiredfield">*</div>
                        <%--<label for="TxbEducaPersonale_skolekode">Skolekode:</label><label class="requiredfield">*</label>--%>
                        <telerik:RadTextBox runat="server" ID="TxbEducaPersonale_skolekode" Width="100%" />




                    </div>


                    <div class="radioButtonHeader">
                        <telerik:RadLabel runat="server" ID="ErrorEducaPersonale_Role">Rolle:</telerik:RadLabel>
                        <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                    </div>
                    <div class="form-group">
                        <%--<asp:Label runat="server" ID="errEducaPersonale_Role" for="RbEducaPersonale_Role">Rolle:</asp:Label><label class="requiredfield">*</label>--%>
                        <asp:RadioButtonList ID="RbEducaPersonale_Role" runat="server" Width="500px" RepeatDirection="Vertical">
                            <asp:ListItem Text="Skoleadministrator" Value="Skoleadministrator"></asp:ListItem>
                            <asp:ListItem Text="Vikar" Value="Vikar"></asp:ListItem>
                            <asp:ListItem Text="Læs-bruger" Value="Læs-bruger"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>


                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorMUElev">MU Elev og Min Uddannelse?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="18" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RblIsMUElev" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RblIsMUElev_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>



                <asp:Panel ID="PanelMUElev" runat="server" Visible="false" CssClass="leftIndent">

                    <div class="form-group">
                        <div for="TxtBoxMUElev_Skolekode" class="inputLabel">Skole:</div>
                        <div class="requiredfield">*</div>
                        <%--<label for="TxbEducaPersonale_skolekode">Skolekode:</label><label class="requiredfield">*</label>--%>
                       <%-- <telerik:RadTextBox runat="server" ID="TxtBoxMUElev_Skolekode" Width="100%" />--%>

                        <telerik:RadComboBox runat="server" ID="ComboMUElev_Skolekode" Skin="Bootstrap" Width="100%" EmptyMessage="Angiv skole"
                            EnableLoadOnDemand="true"
                            ShowToggleImage="true" AllowCustomText="false"
                            CheckBoxes="false" DropDownAutoWidth="Enabled">
                        </telerik:RadComboBox>
                    </div>


                    <div class="radioButtonHeader">
                        <telerik:RadLabel runat="server" ID="ErrorMUElev_Rolle">Rolle:</telerik:RadLabel>
                        <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                    </div>
                    <div class="form-group">
                        <%--<asp:Label runat="server" ID="errEducaPersonale_Role" for="RbEducaPersonale_Role">Rolle:</asp:Label><label class="requiredfield">*</label>--%>
                        <asp:RadioButtonList ID="RblMUElev_Rolle" runat="server" Width="100%" RepeatDirection="Vertical">

                            <asp:ListItem Value="Administrativt Personale (MU-Elev)">Administrativt Personale (MU-Elev) 
                                <div><label class="smallLabel">Tildeles til Administrativt personale eller ledelse på skolen. Giver adgang til at administrere den enkelte skole ift. indskrivning, stamdata opsætning af fravær mm.</label></div>
                            </asp:ListItem>

                            <asp:ListItem Value="Skoleadministrator (MinUddannelse)">Skoleadministrator (MinUddannelse) 
                                <div><label class="smallLabel">Tildeles til Administrativt personale eller ledelse på skolen. Giver adgang til at administrere den enkelte skole ift. opsætning af læringsplatformen.</label></div>
                            </asp:ListItem>
                            
                            <asp:ListItem Value="Skoleleder (MU-Elev og MinUddannelse)">Skoleleder (MU-Elev og MinUddannelse) 
                                <div><label class="smallLabel">Tildeles til Skoleleder. Giver adgang til ledelsesinformation på skole niveau i MinUdddannelse samt adgang til at administrere den enkelte skole i MU-Elev</label></div>                     
                            </asp:ListItem>

                        </asp:RadioButtonList>
                        <a href="https://intranet.skanderborg.dk/mu-elev-og-minuddannelse?item=2915268" class="smallLink" target="_blank" style="font-size:11px;">Se Børn og Unges side vedr. MU Elev og Min Uddannelse</a>

                    </div>
                    
                </asp:Panel>


                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorRBIsRakat">E-handelssystem?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <%--<asp:Label runat="server" ID="errRBIsRakat" for="RBIsRakat">E-handelssystem?</asp:Label><label class="requiredfield">*</label>--%>
                    <asp:RadioButtonList ID="RBisEhandel" runat="server" Width="100" RepeatDirection="Horizontal" OnSelectedIndexChanged="RBIsRakat_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                    <%--<small id="RBIsRakathelptext" class="form-text text-muted">Oprettelse videresendes automatisk til systemadministrator.</small>--%>
                    <div class="smallLabel">Oprettelse videresendes automatisk til systemadministrator.</div>
                </div>
                <asp:Panel ID="panel_Rakat" runat="server" Visible="false" CssClass="leftIndent">
                    <div class="radioButtonHeader">
                        <telerik:RadLabel runat="server" ID="ErrorRbRakatRole">Rolle:</telerik:RadLabel>
                        <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                    </div>
                    <div class="form-group">
                        <%--<asp:Label runat="server" ID="errRbRakatRole" for="RbRakatRole">Rolle:</asp:Label><label class="requiredfield">*</label>--%>
                        <asp:RadioButtonList ID="RbEHandelBrugerType" runat="server" Width="600px" RepeatDirection="Vertical" OnSelectedIndexChanged="RbRakatRole_SelectedIndexChanged" AutoPostBack="true">
                            <%--<asp:ListItem Text="Bruger A - Kigge adgang" Value="A"></asp:ListItem>--%>
                            <asp:ListItem Text="Indkøber der kan handle og kontere sin ordre" Value="B"></asp:ListItem>
                            <asp:ListItem Text="Indkøber der kan afgive ordre, men ikke kontere den" Value="C"></asp:ListItem>
                            <asp:ListItem Text="Indkøber der også er Konteringsansvarlig for Indkøber der ikke kan kontere" Value="D"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_rakat_profitomkoststed" runat="server" Visible="false" CssClass="leftIndent">
                    <div class="form-group">
                        <div for="TxbEHandelProfitCenter" class="inputLabel">Angiv Profitcenter:</div>
                        <div class="requiredfield">*</div>
                        <telerik:RadTextBox runat="server" ID="TxbEHandelProfitCenter" EmptyMessage="" Width="100%" />
                        <small id="Txb_rakat_profitomkoststed_help" class="form-text text-muted" style="margin-top: 0px; padding-top: 0px; padding-left: 5px;"><a href="https://intranet.skanderborg.dk/opus-rollebaseret-indgang?iswebpart=0" target="_blank">Se oversigt over profitcenter/profitcenterknuder.</a></small>
                        <%--<small id="Txb_rakat_profitomkoststed_help" class="form-text text-muted">Angiv det OPUS profitcenter der skal konteres på.</small>--%>
                    </div>
                </asp:Panel>
                <%--                <asp:Panel ID="panel_rakat_konteringsansvarlig" runat="server" Visible="false" CssClass="leftIndent">
                    <div class="form-group">
                        <telerik:RadTextBox runat="server" ID="Txb_rakat_konteringsansvarlig" EmptyMessage="Angiv konteringsansvarlig" Width="100%" />
                        <small id="Txb_rakat_konteringsansvarlig_help" class="form-text text-muted">Angiv den konteringsansvarlige.</small>
                    </div>
                </asp:Panel>--%>

                <%--                <div class="radioButtonHeader">
                    <telerik:RadLabel runat="server" ID="ErrorKMDelev">KMD Elev?</telerik:RadLabel>
                    <telerik:RadLabel runat="server" ForeColor="Red" Font-Size="16" Text="*"></telerik:RadLabel>
                </div>
                <div class="form-group">
                    <asp:RadioButtonList ID="RbIsKMDElev" runat="server" Width="100" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Ja"></asp:ListItem>
                        <asp:ListItem Text="Nej"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>--%>

                <div class="form-group">
                    <%--<label for="TxbBemaerkninger">Skriv evt. dine bemærkninger her:</label>--%>
                    <div for="TxbBemaerkninger" class="inputLabel">Skriv evt. dine bemærkninger her:</div>
                    <telerik:RadTextBox runat="server" ID="TxbBemaerkninger" TextMode="MultiLine" Width="100%" Height="50px" />
                    <%--<small id="bemarkhelp" class="form-text text-muted">Findes programmer ikke på listen ovenfor, kan man skrive i bemærkninger her.</small>--%>
                    <div class="smallLabel">Findes programmer ikke på listen ovenfor, kan man skrive i bemærkninger her.</div>
                    <%--<small id="bemarkhelp2" class="form-text text-muted">Ønskes specialprogrammer og rettigheder som fx Educa, Targit mv, bestilles disse via 7913-systemet under Bestil/Godkend brugeradgange > Special programmer eller Brugeradministration.</small>--%>
                    <div class="smallLabel">Ønskes specialprogrammer og rettigheder som fx Educa, Targit mv, bestilles disse via 7913-systemet under Bestil/Godkend brugeradgange > Special programmer eller Brugeradministration.</div>
                </div>
            </asp:Panel>
            <telerik:RadButton runat="server" AutoPostBack="true" ID="Button_submit" ButtonType="StandardButton" Text="OPRET BRUGER" OnClick="Button_submit_Click" Enabled="true"
                Font-Bold="true" Font-Size="20" Width="300" Height="35" SingleClick="true" SingleClickText="Sender..." />
        </div>


        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Button_submit">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="form_panel" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="Button_submit" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseret" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ComboRollebaseret">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseret_Leder" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelLoenPersonale" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseret_LederBogfoering" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretSeAndre" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelAdministrativMedarbejder" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelOpusOpg_Administrativ" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretAdmMedarb_OrgEnh" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretAdmMedarb_LoenPersonale" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretAdmMedarb_Rettigheder" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretAdmMedarb_Andet" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretAndet" LoadingPanelID="RadAjaxLoadingPanel1" />

                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RbRollebaseretFaktura_Leder">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseret_LederBogfoering" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RbIsLeaderAbleToSeeOther">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretSeAndre" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnRollebaseretAdmMedarb_OekonomiOpg">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelOpusOpg_Administrativ" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnRollebaseretAdmMedarb_LoenPersonale">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretAdmMedarb_OrgEnh" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="BtnRollebaseretAdmMedarb_Andet">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelRollebaseretAdmMedarb_Rettigheder" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                <telerik:AjaxSetting AjaxControlID="RbIsEducaPersonale">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="panel_educapersonale" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RBIsRakat">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="panel_Rakat" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panel_rakat_konteringsansvarlig" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panel_rakat_profitomkoststed" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RbRakatRole">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="panel_rakat_konteringsansvarlig" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="panel_rakat_profitomkoststed" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    </form>
</body>
</html>
