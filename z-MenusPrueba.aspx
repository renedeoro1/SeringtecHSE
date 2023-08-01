<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="z-MenusPrueba.aspx.vb" Inherits="HSENUEVO.zMenusPrueba" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>SERINGTEC</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="OnTime">
    <meta name="SERINGTEC" content="OnTime">
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/jquery-ui-1.8.16.custom.css" rel="stylesheet">
    <link href="css/jquery.jqplot.css" rel="stylesheet">
    <link href="css/prettify.css" rel="stylesheet">
    <link href="css/elfinder.min.css" rel="stylesheet">
    <link href="css/elfinder.theme.css" rel="stylesheet">
    <link href="css/fullcalendar.css" rel="stylesheet">
    <%--<link href="js/plupupload/jquery.plupload.queue/css/jquery.plupload.queue.css" rel="stylesheet">--%>
    <link href="css/styles.css" rel="stylesheet">
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link href="css/icons-sprite.css" rel="stylesheet">
    <link id="themes" href="css/themes.css" rel="stylesheet">
    <link rel="shortcut icon" href="iconos/favicon.ico">
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="iconos/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="iconos/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="iconos/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="iconos/apple-touch-icon-57-precomposed.png">
    <link rel="stylesheet" href="css/pure-min.css">
    <link href="css/udf/jquery-ui.css" rel="stylesheet" type="text/css">
    <link href="css/udf/ufd-base.css" rel="stylesheet" type="text/css">
    <link href="css/udf/plain/plain.css" rel="stylesheet" type="text/css">
    <script src="js/Script/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="js/Script/jquery-ui-1.8.13.js" type="text/javascript"></script>
    <script src="js/Script/jquery.ui.ufd.js" type="text/javascript"></script>
    <script type="text/javascript">$document.on"ready", function  { $".chzn-select".chosen; $".data-tbl-simple".dataTable; }</script>
    

    <style type="text/css">
        .cpHeader {
            color: white;
            background-color: #000099;
            color: white;
            font: bold 11px auto "Trebuchet MS", Verdana;
            font-size: 12px;
            cursor: pointer;
            width: 100%;
            height: 18px;
            padding: 4px;
        }

        .cpBody {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;
            width: 100%;
            padding: 4px;
            padding-top: 7px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager runat="server" EnableCdn="true" ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" />
        <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
            function endReq(sender, args) {
                $(".chzn-select").chosen();
                $(".data-tbl-simple").dataTable();
            }
        </script>

        <div class="navbar navbar-fixed-top">
            <div class="navbar-inner-Azul top-nav">
                <div class="container-fluid">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div class="logo">
                                    <a href="Plataforma.aspx">
                                        <img src="img/logo6.png" alt="Logo"></a>
                                </div>
                            </td>
                            <td style="width: 85%"></td>
                            <td style="width: 15%">
                                <ul class="nav">
                                    <li class="dropdown">
                                        <i class="nav-icon shuffle"></i>
                                        <asp:Image class="dropdown-toggle" data-toggle="dropdown" ID="Image21" runat="server" ImageUrl="~/iconos/usuario10.png" /><b class="caret"></b></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="CambioClave.aspx">Cambio  Contraseña</a></li>
                                            <li><a href="actualizarsPerfil.aspx">Datos Perfil</a></li>
                                            <li><a href="login.aspx">
                                                <asp:Image ID="Image20" runat="server" ImageUrl="~/iconos/salida.png" Width="40%" Height="40%" ToolTip="Cerrar Sesión" /></a></li>
                                        </ul>
                                    </li>
                                </ul>
                            </td>

                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div class="navbar navbar-fixed-top">
            <div class="navbar-inner-Azul top-nav">
                <div class="container-fluid">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div class="logo"><a href="Plataforma.aspx">
                                    <img src="img/logo6.png" alt="Logo"></a></div>
                            </td>
                            <td style="width: 85%"></td>
                            <td style="width: 15%">
                                <ul class="nav">
                                    <li class="dropdown"><i class="nav-icon shuffle"></i>
                                        <asp:Image class="dropdown-toggle" data-toggle="dropdown" ID="Image30" runat="server" ImageUrl="~/iconos/usuario10.png" /><b class="caret"></b></a><ul class="dropdown-menu">
                                            <li><a href="CambioClave.aspx">Cambio  Contraseña</a></li>
                                            <li><a href="actualizarsPerfil.aspx">Datos Perfil</a></li>
                                            <li><a href="login.aspx">
                                                <asp:Image ID="Image31" runat="server" ImageUrl="~/iconos/salida.png" Width="40%" Height="40%" ToolTip="Cerrar Sesión" /></a></li>
                                        </ul>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                    </div></div>
            </div>
        <div id="sidebar">
            <ul class="side-nav-Azul accordion_mnu collapsible">
                <li><a href="plataforma.aspx"><span class="white-icons computer_imac"></span>Plataforma</a></li>
              <asp:Literal ID="LitIzquierdo" runat="server"></asp:Literal>
                    <li><a href="#"><span class="white-icons cup"></span>Configuración General</a><ul class="acitem">
                        <li><a href="Funcionarios.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Perfiles</a></li>
                        <li><a href="Permisos.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Permisos</a></li>
                        <li><a href="Modulos.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Funcionarios</a></li>
                        <li><a href="Perfiles.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Modulos</a></li>
                        <li><a href="PerfilxModulos.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Perfil xModulo</a></li>
                        <li><a href="PerfilxModulosxFunciionario.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Perfil x Funcionario</a></li>
                        <li><a href="Vigencia.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Vigencia</a></li>
                    </ul>
                    </li>
                    <li><a href="#"><span class="white-icons cup"></span>Configuración Especifica</a><ul class="acitem">
                        <li><a href="tipoaccion.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Tipo Accciones</a></li>
                    </ul>
                    </li>
                    <li><a href="#"><span class="white-icons cup"></span>HSE</a><ul class="acitem">
                        <li><a href="reportesCasos.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>reporte de casos</a></li>
                        <li><a href="InformHSE.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Informe HSE</a></li>
                        <li><a href="InformeMensual.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Informe Mensual</a></li>
                        <li><a href="ATL2.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Atl2 Informe</a></li>
                        <li><a href="Personal.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Personal</a></li>
                        <li><a href="Seguridad_vial.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Seguridad Vial</a></li>
                        <li><a href="Estadistica.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Estadistica Diaria</a></li>
                        <li><a href="RegristroFotografico.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Registro Fotografico</a></li>
                        <li><a href="Atlproces.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Proceso Atl</a></li>
                        <li><a href="AtlOrigenmejora.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Origen Mejora Atl</a></li>
                        <li><a href="AtlAccion.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Accion Atl</a></li>
                        <li><a href="AtlResponsable.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Responsable Atl</a></li>
                        <li><a href="HUB.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>HUB</a></li>
                        <li><a href="itemEstadistica.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Item Estadistica</a></li>
                        <li><a href="Tipovehiculo.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Tipo vehiculo</a></li>
                        <li><a href="Area.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Area</a></li>
                        <li><a href="Sede.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Sede</a></li>
                        <li><a href="CalculosMecanica.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Calculo</a></li>

    
                    </ul>
                    </li>

            </ul>

            <table style="border: thin solid #000080; padding: 3px; width: 100%;">
                <tr>
                    <td style="border: thin solid #252F60; padding: 3px; width: 30px; background-color: darkgray; color: #FFFFFF; font-weight: bold; text-align: center; height: 30PX">Usuario Registrado:
                    </td>
                </tr>
                <tr>
                    <td style="border: thin solid #252F60; padding: 3px;">
                        <asp:Label ID="lblUsuarioLogueado_Nombre" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="lblUsuarioLogueado_Documento" runat="server" Text="Label" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>




        </div>


        <asp:Literal ID="LitSuperior" runat="server"></asp:Literal>
        



        <div id="main-content">

            <div class="container-fluid">
                <ul class="breadcrumb">
                    <asp:Image ID="Image1" runat="server" ImageUrl="iconos/ontimeok.png" Width="150px    " />
                    <asp:Label ID="Label6" runat="server" Text="Bienvenidos al Sistema Web ONTIME" Font-Bold="True" Font-Size="24pt" ForeColor="#000066"></asp:Label>

                </ul>
                   <%--comienza iconos plataforma--%>
                    <div  style ="width :100%; font-size:14px; font-weight:bold ">
                        Configuracion Especifica
                    </div>
                   
                    <div class="span2">
                        <div class="dashboard-wid-wrap">
                            <div class="dashboard-wid-content">
                                <a href="novedades.aspx">
                                    <img alt="" style="width:80px; height:80px"  src="iconos/Novedades1.png"  />                                    
                                    <span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Registro de Novedades</span> </a>
                            </div>
                        </div>
                    </div>

                    <div class="span2">
                        <div class="dashboard-wid-wrap">
                            <div class="dashboard-wid-content">
                                <a href="novedades.aspx">
                                    <img alt="" style="width:80px; height:80px"  src="iconos/Novedades1.png"  />                                    
                                    <span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Registro de Novedades</span> </a>
                            </div>
                        </div>
                    </div>

                    <div class="span2">
                        <div class="dashboard-wid-wrap">
                            <div class="dashboard-wid-content">
                                <a href="novedades.aspx">
                                    <img alt="" style="width:80px; height:80px"  src="iconos/Novedades1.png"  />                                    
                                    <span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Registro de Novedades</span> </a>
                            </div>
                        </div>
                    </div>

                    <div class="span2">
                        <div class="dashboard-wid-wrap">
                            <div class="dashboard-wid-content">
                                <a href="novedades.aspx">
                                    <img alt="" style="width:80px; height:80px"  src="iconos/Novedades1.png"  />                                    
                                    <span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Registro de Novedades</span> </a>
                            </div>
                        </div>
                    </div>
                    <div class="span2">
                        <div class="dashboard-wid-wrap">
                            <div class="dashboard-wid-content">
                                <a href="novedades.aspx">
                                    <img alt="" style="width:80px; height:80px"  src="iconos/Novedades1.png"  />                                    
                                    <span class="dasboard-icon-title" style="font-size: 13px; font-weight: bold">Registro de Novedades</span> </a>
                            </div>
                        </div>
                    </div>

                   <%--termina iconos plataforma--%>          

            </div>
        </div>



        <asp:ModalPopupExtender ID="ModalPopuplogeo" runat="server" BehaviorID="programmaticModalPopupBehavior"
            TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="PanelModalPopulogeo"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="programmaticPopupDragHandle">
        </asp:ModalPopupExtender>
        <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" UseSubmitBehavior="False" />
        <asp:Panel runat="server" Class="span4" ID="PanelModalPopulogeo" Style="display: none; padding: 10px" BorderWidth="3" BorderStyle="Solid" BorderColor="#666666" BackColor="White" DefaultButton="btnVerificarDocumento">
            <asp:Panel runat="Server" ID="programmaticPopupDragHandle" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#666666" ForeColor="White" Font-Bold="True" Font-Size="13">Se requiere realizar una verificacion del documento de identidad! </asp:Panel>

            <asp:Label ID="lblMensajeVerificar_Popup" runat="server" Text="" ForeColor="Red" Font-Bold="true" Width="100%" Font-Size="12px" Style="text-align: center;"></asp:Label>

            <br />

            <div class="control-group">
                <div class="controls" style="text-align: center">
                    <asp:Label ID="lbl_90_T06ClaveLocal" runat="server" Text="Digite N° Documento" Font-Bold="True" Font-Size="10pt" placeholder="Nueva Clave"></asp:Label>
                    <asp:TextBox ID="txt_90_T06ClaveLocal" runat="server" Width="80%" MaxLength="50"></asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="filtered_90_T06ClaveLocal" runat="server" Enabled="True" TargetControlID="txt_90_T06ClaveLocal" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                    <asp:Label ID="lbl_NombreFuncionario" runat="server" Text="" Font-Bold="True" Font-Size="11pt" Visible="false" Width="100%" ForeColor="DarkBlue"></asp:Label>
                    <div style="height: 1px"></div>
                    <asp:Button ID="btnVerificarDocumento" runat="server" Text="Verificar" class="btn btn-primary" Enabled="false" />
                    <asp:Button ID="btnVerificarCancelarPopu" Visible="false" runat="server" Text="Cancelar" class="btn btn-primary-rojo" />

                </div>
            </div>
        </asp:Panel>

        <%--      </ContentTemplate>
            <Triggers>
                
            </Triggers>
        </asp:UpdatePanel>--%>

        <!-- javascript
================================================== -->
        <!-- Placed at the end of the document so the pages load faster -->
        <script src="js/jquery.js"></script>
        <script src="js/jquery-ui-1.8.16.custom.min.js"></script>
        <script src="js/bootstrap.js"></script>
        <script src="js/prettify.js"></script>
        <script src="js/jquery.sparkline.min.js"></script>
        <script src="js/jquery.nicescroll.min.js"></script>
        <script src="js/accordion.jquery.js"></script>
        <script src="js/smart-wizard.jquery.js"></script>
        <script src="js/vaidation.jquery.js"></script>
        <script src="js/jquery-dynamic-form.js"></script>
        <script src="js/fullcalendar.js"></script>
        <script src="js/raty.jquery.js"></script>
        <script src="js/jquery.noty.js"></script>
        <script src="js/jquery.cleditor.min.js"></script>
        <script src="js/data-table.jquery.js"></script>
        <script src="js/TableTools.min.js"></script>
        <script src="js/ColVis.min.js"></script>
        <script src="js/plupload.full.js"></script>
        <script src="js/elfinder/elfinder.min.js"></script>
        <script src="js/chosen.jquery.js"></script>
        <script src="js/uniform.jquery.js"></script>
        <script src="js/jquery.tagsinput.js"></script>
        <script src="js/jquery.colorbox-min.js"></script>
        <script src="js/check-all.jquery.js"></script>
        <script src="js/inputmask.jquery.js"></script>
        <script src="http://bp.yahooapis.com/2.4.21/browserplus-min.js"></script>
        <script src="js/plupupload/jquery.plupload.queue/jquery.plupload.queue.js"></script>
        <script src="js/excanvas.min.js"></script>
        <script src="js/jquery.jqplot.min.js"></script>
        <script src="js/chart/jqplot.highlighter.min.js"></script>
        <script src="js/chart/jqplot.cursor.min.js"></script>
        <script src="js/chart/jqplot.dateAxisRenderer.min.js"></script>
        <script src="js/custom-script.js"></script>
        <!-- html5.js for IE less than 9 -->
        <!--[if lt IE 9]>
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
        <script src="js/respond.min.js"></script>
        <script src="js/ios-orientationchange-fix.js"></script>

    </form>
</body>
</html>
