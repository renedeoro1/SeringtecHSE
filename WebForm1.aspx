<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="HSENUEVO.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:Panel ID="Panel_UtilidadesMedicas" runat="server">
                <div style="height: 10px"></div>
                <div class="pure-g-r">
                    <div class="pure-u-1" style="border-bottom: solid; border-bottom-color: #FFFFFF; border-bottom-width: thin; background-color: #000066; color: #FFFFFF; height: 20px; text-align: center;">
                        <asp:Label ID="Label8" runat="server" Text="X. UTILIDADES MÉDICAS" Font-Bold="True" Font-Size="9pt"></asp:Label>
                    </div>
                </div>
                <div style="height: 10px"></div>
                <div>
                    <asp:Panel ID="pHeaderAdjuntar" runat="server" Class="cpHeader" Width="100%" BorderWidth="1px" BorderColor="DarkBlue">
                        <asp:Label ID="lblTextAdjuntar" runat="server" Width="100%" />
                    </asp:Panel>
                    <asp:Panel ID="pBodyAdjuntar" runat="server" Class="cpBody" ScrollBars="Both" Width="100%">
                        <div class="pure-g-r">
                            <div class="pure-u-1-4">
                                <asp:Label ID="Label388" runat="server" Text="Modulo" Font-Bold="True" Font-Size="10pt" Width="100%"> </asp:Label>
                                <asp:DropDownList ID="drop_04_T28Modulo" runat="server" Width="90%" class="chzn-select" placeholder="_T17TipoDocPaciente"></asp:DropDownList>

                                <asp:Label ID="lbl_04_T28Descripcion" runat="server" Text="Nombre / Descripcion" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                <asp:TextBox ID="txt_04_T28Descripcion" runat="server" Width="90%" MaxLength="200" Visible="false"></asp:TextBox>

                                <asp:Label ID="lbl_04_T28Consecutivo" runat="server" Text="Consecutivo" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                <asp:TextBox ID="txt_04_T28Consecutivo" runat="server" Width="90%" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txt_04_T28Extension" runat="server" Width="90%" MaxLength="10" Visible="false"></asp:TextBox>
                            </div>
                            <div class="pure-u-1-4">
                                <asp:Label ID="lbl_04_T28TipoArchivo" runat="server" Text="Tipo Archivo" Font-Bold="True" Font-Size="10pt" Width="100%"></asp:Label>
                                <asp:DropDownList ID="drop_04_T28TipoArchivo" runat="server" class="chzn-select" Width="90%" placeholder="_T28TipoArchivo"></asp:DropDownList>

                                <asp:DropDownList ID="drop_04_T28TipoGuardado" runat="server" class="chzn-select" Width="90%" placeholder="_T28TipoArchivo" Visible="false" AppendDataBoundItems="true">
                                    <%--<asp:ListItem Value="01">Documento en Fisico</asp:ListItem>--%>
                                    <asp:ListItem Value="02">Documento en Sistema</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txt_04_T28Ruta" runat="server" Width="90%" MaxLength="200" Visible="false"></asp:TextBox>

                            </div>
                            <div class="pure-u-1-4">
                                <asp:Label ID="Label389" runat="server" Text="Seleccionar Archivo" Font-Bold="True" Font-Size="10pt" Width="100%"> </asp:Label>
                                <asp:FileUpload ID="file_04_T28Archivo" runat="server" Class="fileArchivo" />
                            </div>
                            <div class="pure-u-1-6">
                                <asp:Button ID="btnAdjuntarArchivo" runat="server" Text="Adjuntar" class="btn btn-large btn-primary" OnClientClick="bPreguntar = false;" UseSubmitBehavior="false" />

                            </div>
                            <div class="pure-u-1">
                                <table class="data-tbl-simple table table-bordered">

                                    <thead>
                                        <tr>
                                            <th>Tipo Archivo
                                            </th>
                                            <th>Nombre
                                            </th>
                                            <th>Extension
                                            </th>
                                            <th>Imprimir
                                            </th>
                                            <th>Eliminar
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody runat="server" id="bodytablaAnexos" style="border: thin solid #000080; background-color: #FFFFFF;">
                                    </tbody>
                                </table>
                            </div>
                            <div class="pure-u-1">
                                <asp:Label ID="lbl_04_T35_21Anexos_Obs" runat="server" Text="Observaciones" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                <asp:TextBox ID="txt_04_T35_21Anexos_Obs" runat="server" Width="100%" Height="40px" TextMode="MultiLine"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="filtered_04_T35_21Anexos_Obs" runat="server" Enabled="True" TargetControlID="txt_04_T35_21Anexos_Obs" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                            </div>
                        </div>

                    </asp:Panel>
                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender_Adjuntar" runat="server" TargetControlID="pBodyAdjuntar" CollapseControlID="pHeaderAdjuntar" ExpandControlID="pHeaderAdjuntar"
                        Collapsed="true" TextLabelID="lblTextAdjuntar" CollapsedText="ADJUNTAR DOCUMENTOS E IMAGENES" ExpandedText="ADJUNTAR DOCUMENTOS E IMAGENES"
                        CollapsedSize="0">
                    </asp:CollapsiblePanelExtender>
                </div>
                <div style="height: 3px"></div>
                <div>
                    <asp:Panel ID="pHeader5" runat="server" Class="cpHeader" Width="100%" BorderWidth="1px" BorderColor="DarkBlue">
                        <asp:Label ID="lblText4" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pBody5" runat="server" Class="cpBody" Style="width: 100%" ScrollBars="Both">
                        <div id="divContendio" runat="server" class="pure-form pure-form-stacked">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_04_T54TipoUtilidad" runat="server" Text="Tipo Utilidad" Font-Bold="True" Font-Size="10pt" Height="22.5px" Width="90%"></asp:Label>
                                        <asp:DropDownList ID="drop_04_T54TipoUtilidad" runat="server" class="chzn-select" Width="180px" placeholder="_T54TipoUtilidad" ForeColor="Black"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_04_T54CodigoTem" runat="server" Text="Formatos" Font-Bold="True" Font-Size="10pt" Height="22.5px" Width="90%"></asp:Label>
                                        <asp:DropDownList ID="drop_04_T54CodigoTem" runat="server" class="chzn-select" Width="300px" placeholder="_T54CodigoTem" ForeColor="Black"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUtilidades" runat="server" Text="Anexar" class="btn btn-large btn-primary" OnClientClick="bPreguntar = false;" UseSubmitBehavior="false" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUtilidadesLimpiar" runat="server" Text="Limpiar" class="btn btn-large btn-primary" UseSubmitBehavior="false" />

                                        <asp:Button ID="btnUtilidadesImprimirActual" runat="server" Text="Imprimir" class="btn btn-large btn-primary" UseSubmitBehavior="false" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:DropDownList ID="drop_04_T31Nombre" runat="server" class="chzn-select" Style="width: 1000px !important;" Visible="false"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div class="pure-g-r">
                                <div class="pure-u-1">
                                    <asp:Label ID="lbl_04_T55Contenido" runat="server" Text="Observación General" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    <asp:TextBox ID="txt_04_T55Contenido" runat="server" Width="100%" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="filtered_04_T55Contenido" runat="server" Enabled="True" TargetControlID="txt_04_T55Contenido" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>

                                    <asp:Label ID="lbl_04_T55Consecutivo" runat="server" Text="Consecutivo" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txt_04_T55Consecutivo" runat="server" Width="90%" MaxLength="50" Visible="false"></asp:TextBox>


                                    <asp:Label ID="lbl_04_T55CodModulo" runat="server" Text="CodModulo" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txt_04_T55CodModulo" runat="server" Width="90%" MaxLength="30" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lbl_04_T55FechaRegistra" runat="server" Text="FechaRegistra" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txt_04_T55FechaRegistra" runat="server" Width="90%" MaxLength="30" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lbl_04_T55UsuarioRegistra" runat="server" Text="UsuarioRegistra" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txt_04_T55UsuarioRegistra" runat="server" Width="90%" MaxLength="50" Visible="false"></asp:TextBox>



                                </div>
                                <div class="pure-u-1">
                                    <table class="data-tbl-simple table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Consecutivo
                                                </th>
                                                <th>Fecha
                                                </th>
                                                <th>Tipo Formato
                                                </th>
                                                <th>Nombre
                                                </th>
                                                <th>Imprimir
                                                </th>
                                                <th>Eliminar
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody runat="server" id="TbodyUtilidades">
                                        </tbody>
                                    </table>
                                </div>
                                <div style="height: 15px">
                                </div>

                            </div>

                        </div>
                    </asp:Panel>
                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender_Utilidades" runat="server" TargetControlID="pBody5" CollapseControlID="pHeader5" ExpandControlID="pHeader5"
                        Collapsed="true" TextLabelID="lblText4" CollapsedText="UTILIDADES" ExpandedText="UTILIDADES"
                        CollapsedSize="0" SuppressPostBack="true">
                    </asp:CollapsiblePanelExtender>
                </div>
                <div style="height: 3px"></div>
                <div>
                    <asp:Panel ID="pHeader_Actividades" runat="server" Class="cpHeader" Width="100%" BorderWidth="1px" BorderColor="DarkBlue">
                        <asp:Label ID="lbl_Actividades" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pBody5l_Actividades" runat="server" Class="cpBody" Style="width: 100%" ScrollBars="Both">
                        <div id="div5" runat="server" class="pure-form pure-form-stacked">
                            <div style="height: 4px"></div>
                            <div class="pure-g-r">
                                <div class="pure-u-1-8">
                                    <asp:Label ID="lbl_04_T53_4Fecha" runat="server" Text="Fecha" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    <asp:TextBox ID="txt_04_T53_4Fecha" runat="server" Width="90%" MaxLength="30"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txt_04_T53_4Fecha" FilterType="Numbers,Custom" ValidChars="/"></asp:FilteredTextBoxExtender>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_04_T53_4Fecha">
                                    </asp:CalendarExtender>
                                </div>
                                <div class="pure-u-1-8">
                                    <asp:Label ID="lbl_04_T53_4Estado" runat="server" Text="Estado" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    <asp:DropDownList ID="drop_04_T53_4Estado" runat="server" class="chzn-select" Width="90%" placeholder="_T53_4Estado"></asp:DropDownList>
                                </div>
                                <div class="pure-u-1-2">
                                    <asp:Label ID="lbl_04_T53_4Observacion" runat="server" Text="Observaciones" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    <asp:TextBox ID="txt_04_T53_4Observacion" runat="server" Width="90%" MaxLength="30"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="filtered_04_T53_4Observacion" runat="server" Enabled="True" TargetControlID="txt_04_T53_4Observacion" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                </div>
                                <div class="pure-u-1-6">
                                    <asp:Label ID="Label36" runat="server" Text="Registrar Actividad" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                    <asp:Button ID="btnGuardarActividadSeguimiento" runat="server" Text="Agregar" class="btn btn-primary" UseSubmitBehavior="False" />
                                </div>
                                <asp:Label ID="lbl_04_T53_4FucionarioRegistra" runat="server" Text="_4FucionarioRegistra" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                <asp:TextBox ID="txt_04_T53_4FucionarioRegistra" runat="server" Width="90%" MaxLength="200" Visible="false"></asp:TextBox>
                                <asp:Label ID="lbl_04_T53_4FucionarioCierra" runat="server" Text="_4FucionarioCierra" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                <asp:TextBox ID="txt_04_T53_4FucionarioCierra" runat="server" Width="90%" MaxLength="200" Visible="false"></asp:TextBox>
                                <asp:Label ID="lbl_04_T53_4Consecutivo" runat="server" Text="_4Consecutivo" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                <asp:TextBox ID="txt_04_T53_4Consecutivo" runat="server" Width="90%" MaxLength="20" Visible="false"></asp:TextBox>


                                <div class="pure-u-1">
                                    <table class="data-tbl-simple table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Fecha
                                                </th>
                                                <th>Estado
                                                </th>
                                                <th>Observacion
                                                </th>
                                                <th>Usuario Registra
                                                </th>
                                                <th>Usuario Gestiona
                                                </th>
                                                <th>Eliminar
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody runat="server" id="bodytabla_Actividad_Seguimiento">
                                        </tbody>
                                    </table>
                                    <div style="height: 15px">
                                    </div>

                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender_Actividades" runat="server" TargetControlID="pBody5l_Actividades" CollapseControlID="pHeader_Actividades" ExpandControlID="pHeader_Actividades"
                        Collapsed="true" TextLabelID="lbl_Actividades" CollapsedText="ACTIVIDADES DE SEGUIMIENTO" ExpandedText="ACTIVIDADES DE SEGUIMIENTO"
                        CollapsedSize="0" SuppressPostBack="true">
                    </asp:CollapsiblePanelExtender>
                </div>

            </asp:Panel>

            <div id="labelEspecialistasDD" style="width: 100%; font-size: 14px; font-weight: bold">Configuracion Especifica</div>
            <div class="span2">
                <div class="dashboard-wid-wrap">
                    <div class="dashboard-wid-content"><a href="reportesCasos.aspx">
                        <img alt="" style="width: 80px; height: 80px" src="iconos/Reporte_casos.png" /><span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Reporte de Casos</span> </a></div>
                </div>
            </div>
            <div class="span2">
                <div class="dashboard-wid-wrap">
                    <div class="dashboard-wid-content"><a href="InformHSE.aspx">
                        <img alt="" style="width: 80px; height: 80px" src="iconos/InformeHSE.png" /><span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Informe HSE</span> </a></div>
                </div>
            </div>
            <div class="span2">
                <div class="dashboard-wid-wrap">
                    <div class="dashboard-wid-content">
                        <a href="ConsultaInformeHSE.aspx">
                            <img alt="" style="width: 80px; height: 80px" src="iconos/ConsultarInforme.png" /><span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Consultar total Informes HSE</span> </a>
                    </div>
                </div>
            </div>
            <div class="span2">
                <div class="dashboard-wid-wrap">
                    <div class="dashboard-wid-content"><a href="ATL2.aspx">
                        <img alt="" style="width: 80px; height: 80px" src="iconos/ATLInfor.png" /><span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">ALT Informe</span> </a></div>
                </div>
            </div>
            <div class="span2">
                <div class="dashboard-wid-wrap">
                    <div class="dashboard-wid-content"><a href="InformeMensual.aspx">
                        <img alt="" style="width: 80px; height: 80px" src="iconos/mensual.png" /><span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Informe Mensual</span> </a></div>
                </div>
            </div>
        </div>

        <li><a href="#"><span class="white-icons cup"></span>HSE</a><ul class="acitem">
            <li><a href="reportesCasos.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>reporte de casos</a></li>
            <li><a href="InformHSE.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Informe HSE</a></li>
            <li><a href="ConsultaInformeHSE.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Consulta Informe HSE </a></li>
            <li><a href="InformeMensual.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Informe Mensual</a></li>
            <li><a href="ATL2.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Atl2 Informe</a></li>

        </ul>
        </li>


        <li><a href="#"><span class="white-icons cup"></span>HSE</a><ul class="acitem">
            <li><a href="InformHSE.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Informe HSE</a></li>
        </ul>
        </li>

    </form>
</body>
</html>
