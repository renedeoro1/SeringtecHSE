<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sinpermiso.aspx.vb" Inherits="HSENUEVO.sinpermiso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
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
    <script languaje="javascript">  </body ></html ></script>

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
                    <div class="branding">
                        <div class="logo">

                            <a href="Plataforma.aspx">
                                <img src="img/logo6.png" alt="Logo"></a>
                        </div>
                    </div>

                    <button data-target=".nav-collapse" data-toggle="collapse" class="btn btn-navbar" type="button"><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                    <div class="nav-collapse collapse">
                        <ul class="nav">
                            <asp:Literal ID="Lit_Configuracion2" runat="server">
                          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="nav-icon shuffle"></i>Configuración<b class="caret"></b></a><ul class="dropdown-menu">                                
                          
                          
                          <li><a href="PerfilesxUsuarios.aspx">Usuarios  x Perfiles</a></li>
                          <li><a href="ContratosMarco.aspx">Contratos Marco</a></li>
                          <li><a href="Disciplinas.aspx">Disciplinas</a></li>
                          <li><a href="funcionario.aspx">Funcionarios</a></li>
                          <li><a href="Lideres.aspx">Lideres</a></li>
                          <li><a href="ODS.aspx">ODS</a></li>

                            </ul>
                            </li>
                            </asp:Literal>

                            <asp:Literal ID="Lit_Lideres2" runat="server">
                          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="nav-icon shuffle"></i>Lideres<b class="caret"></b></a><ul class="dropdown-menu">                                
                          <li><a href="RecursosxODS.aspx">Asignacion Recursos</a></li>

                            </ul>
                            </li>
                            </asp:Literal>

                            <asp:Literal ID="Lit_Aprobadores2" runat="server">
                          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="nav-icon shuffle"></i>Aprobadores<b class="caret"></b></a><ul class="dropdown-menu">                                
                          <li><a href="aprobacionTiempoAprobadores.aspx">Aprobadores</a></li>

                            </ul>
                            </li>
                            </asp:Literal>

                            <asp:Literal ID="Lit_Coordinadores2" runat="server">
                          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="nav-icon shuffle"></i>Coordinadores<b class="caret"></b></a><ul class="dropdown-menu">                                
                            <li><a href="aprobacionTiempoConfirmacion.aspx">Aprobacion de Tiempo</a></li>

                            </ul>
                            </li>
                            </asp:Literal>

                            <asp:Literal ID="Lit_Funcionarios2" runat="server">
                          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="nav-icon shuffle"></i>Funcionarios<b class="caret"></b></a><ul class="dropdown-menu">                                
                            <li><a href="aprobacionTiempo.aspx">registro de Tiempo</a></li>

                            </ul>
                            </li>
                            </asp:Literal>

                            <asp:Literal ID="Lit_RecursosHumanos2" runat="server">
                          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="nav-icon shuffle"></i>Recursos Humanos<b class="caret"></b></a><ul class="dropdown-menu">                                
                            <li><a href="MatrizRecursosHumnos.aspx">Matriz de Trabjadores</a></li>

                            </ul>
                            </li>
                            </asp:Literal>

                            <asp:Literal ID="Lit_ControlDocumental2" runat="server">
                          <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="nav-icon shuffle"></i>Control Documental<b class="caret"></b></a><ul class="dropdown-menu">                                
                          <li><a href="ListadoEntTipoDocumentos.aspx">Tipos Doc.Entregables</a></li>
                          <li><a href="ListadoDocumentosEntegables.aspx">Listado Doc.Entregables</a></li>


                            </ul>
                            </li>
                            </asp:Literal>









                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="sidebar">
            <ul class="side-nav-Azul accordion_mnu collapsible">
                <li><a href="plataforma.aspx"><span class="white-icons computer_imac"></span>Plataforma</a></li>
                <asp:Literal ID="Lit_Configuracion" runat="server">
                <li><a href="#"><span class="white-icons list"></span>Configuracion</a><ul class="acitem">
                    
                    
                    <li><a href="PerfilesxUsuarios.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Usuarios  x Perfiles</a></li>
                    <li><a href="Clientes.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Clientes</a></li>
                    <li><a href="ContratosMarco.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Contratos Marco</a></li>
                    <li><a href="ODS.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>ODS</a></li>
                    <li><a href="Disciplinas.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Disciplinas</a></li>
                    <%--<li><a href="funcionario.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Funcionarios</a></li>--%>
                    <li><a href="Lideres.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Lideres</a></li>                    
            
                                        
                                                           
                    
                    
                   </ul>
                </li>
                </asp:Literal>
                <asp:Literal ID="Lit_Edicion" runat="server">
                <li><a href="#"><span class="white-icons cup"></span>Edicion Modulos Tiempo</a><ul class="acitem">
                    <li><a href="aprobacionTiempoAprobadoresEdicion.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Edicion  (Aprobadores)</a></li>                                        
                    <li><a href="aprobacionTiempoEdicion.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Edicion  (Coordinadores)</a></li>
                    <li><a href="RegistrodeTiempoEdicion.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Edicion  Tiempo (Funcionarios)</a></li>
                </ul>
                </li>
                </asp:Literal>
                <asp:Literal ID="Lit_Lideres" runat="server">
                <li><a href="#"><span class="white-icons cup"></span>Lideres</a><ul class="acitem">
                    <li><a href="RecursosxODS.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Asignacion Recursos</a></li>                    
                    <li><a href="aprobadores.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Registro Aprobadores</a></li>                    
                    <li><a href="RecursosxAprobador.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Recursos por Aprobador</a></li>                    
                    <%--<li><a href="Novedades.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Novedades</a></li>--%>                    
                </ul>
                </li>
                </asp:Literal>
                <asp:Literal ID="Lit_Aprobadores" runat="server">
                <li><a href="#"><span class="white-icons cup"></span>Aprobadores</a><ul class="acitem">
                    <li><a href="aprobacionTiempoAprobadores.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Aprobacion de Tiempo</a></li>                    
                    <li><a href="aprobacionTiempoAprobadoresHistorial.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Historial Aprobacion</a></li>
                </ul>
                </li>
                </asp:Literal>

                <asp:Literal ID="Lit_Coordinadores" runat="server">
                <li><a href="#"><span class="white-icons shuffle"></span>Coordinadores</a><ul class="acitem">
                    <li><a href="aprobacionTiempoConfirmacion.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Confirmación de Tiempo</a></li>
                    <li><a href="aprobacionTiempoConfirmacionHistorial.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Historial de Confirmación</a></li>
                    
                </ul>
                </li>
                </asp:Literal>
                <asp:Literal ID="Lit_Funcionarios" runat="server">
                <li><a href="#"><span class="white-icons documents"></span>FUNCIONARIOS</a><ul class="acitem">
                    <li><a href="RegistrodeTiempo.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Registro Tiempo</a></li>                    
                    <li><a href="RegistrodeTiempoHistorial.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Historial Registro Tiempo</a></li>
                </ul>
                </li>
                </asp:Literal>
                <asp:Literal ID="Lit_RecursosHumanos" runat="server">
                <li><a href="#"><span class="white-icons documents"></span>RECURSOS HUMANOS</a><ul class="acitem">
                    <li><a href="MatrizTrabajadores.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Matriz Trabajadores</a></li>
                </ul>
                </li>
                </asp:Literal>
                <asp:Literal ID="Lit_ControlProyectos" runat="server">
                <li><a href="#"><span class="white-icons documents"></span>Control Proyectos</a><ul class="acitem">
                    <li><a href="Clientes.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Clientes</a></li>
                    <li><a href="ContratosMarco.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span> Contratos Marco</a></li>
                    <li><a href="ODS.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>ODS</a></li>
                </ul>
                </li>
                </asp:Literal>
                <asp:Literal ID="Lit_ControlDocumentos" runat="server">
                <li><a href="#"><span class="white-icons documents"></span>Control Documentos</a><ul class="acitem">
                    <li><a href="ListadoDocumentosTipos.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Cargue Masivo LME</a></li>

                </ul>
                </li>
                </asp:Literal>

                <asp:Literal ID="Lit_ControlDocumental" runat="server">
                <li><a href="#"><span class="white-icons documents"></span>CONTROL DOCUMENTAL</a><ul class="acitem">

                                        <%--<li><a href="ListadoEntTipoDocumentos.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Tipos Doc.Entregables</a></li>--%>
                    <li><a href="ListadoDocumentosEntegables.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Listado Doc.Entregables</a></li>
<%--<li><a href="TipoActividades.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Tipos Actividades</a></li>--%>                    
<%--<li><a href="Actividades.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Actividades</a></li>--%>                    

                </ul>
                </li>
                </asp:Literal>

            </ul>
            <div id="side-accordion-Azul">
                <div class="accordion-group">
                    <div class="accordion-header-Azul"><a class="accordion-toggle" data-toggle="collapse" data-parent="#side-accordion" href="#collapseOne"><i class="nav-icon month_calendar"></i>Informacion del Dia </a></div>
                    <div id="collapseOne" class="collapse in">
                        <div class="accordion-content" style="padding: 10px">

                            <table style="border: thin solid #000080; padding: 3px; width: 100%;">
                                <tr>
                                    <td style="border: thin solid #252F60; padding: 3px; width: 30px; background-color: #252F60; color: #FFFFFF; font-weight: bold; text-align: center;">Usuario Registrado:
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #252F60; padding: 3px;">
                                        <asp:Label ID="lblUsuarioLogueado_Nombre" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="lblUsuarioLogueado_Documento" runat="server" Text="Label" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUsuarioLogueado_Disciplina" runat="server" Text="Label" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div style="height: 10px"></div>
                            <table style="border: thin solid #252F60; padding: 3px; width: 100%;">
                                <tr>
                                    <td style="border: thin solid #666666; padding: 3px; width: 30px; background-color: #666666; color: #FFFFFF; font-weight: bold; width: 100%; text-align: center;" colspan="2">Estadisticas de Gestión
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #CC9900; padding: 3px; width: 30px; background-color: #CC9900; color: #FFFFFF; font-weight: bold width: 100%;" colspan="2">Registro de Tiempo
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #252F60; padding: 3px; width: 30px">Registro Hoy:
                                    </td>
                                    <td style="border: thin solid #252F60; padding: 3px;">
                                        <asp:Label ID="lblEstadisticaRegistroHoy" runat="server" Text="" Width="15px"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #252F60; padding: 3px; width: 30px">Registro Mes:
                                    </td>
                                    <td style="border: thin solid #252F60; padding: 3px;">

                                        <asp:Label ID="lblEstadisticaRegistroMes" runat="server" Text="" Width="15px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #006600; padding: 3px; width: 30px; background-color: #006600; color: #FFFFFF; font-weight: bold; width: 100%;" colspan="2">Aprobacion de Tiempo
                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: thin solid #252F60; padding: 3px; width: 30px">Registro Hoy:
                                    </td>
                                    <td style="border: thin solid #252F60; padding: 3px;">
                                        <asp:Label ID="lblEstadisticaAprobacionHoy" runat="server" Text="" Width="15px"></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin solid #252F60; padding: 3px; width: 30px">Registro Mes:
                                    </td>
                                    <td style="border: thin solid #252F60; padding: 3px;">
                                        <asp:Label ID="lblEstadisticaAprobacionMes" runat="server" Text="" Width="15px"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                </div>

            </div>

        </div>
        
        <div id="main-content">
            <div class="container-fluid">

                <div class="row-fluid">
                    <div class="span12">
                        <div class="nonboxy-widget">
                            <div class="widget-content">
                                <div class="container">
                                    <div class="row error-wrap">
                                        <div class="span3">
                                            <div class="errorcode">
                                                <span>Error</span>
                                            </div>
                                        </div>
                                        <div class="span5">
                                            <div class="error-title">
                                                <span>...UPS!</span>
                                            </div>
                                            <div>
                                                <h3 class="error-message"><span>Lo sentimos, el usuario actualmente logeado no tiene permisos para consultar este modulo.</span> </h3>
                                                <p>
                                                    Realice algunos de los siguientes procedimiento para solucionar su problema:
                                                </p>
                                                <ul class="error-instruction">
                                                    <li>Intente nuevamente ingresar al modulo</li>
                                                    <li>Intente Cerrando todas las sesiones y loguearse de nuevo: <asp:LinkButton ID="btnCerrarSeccion" runat="server">Cerrar Sesiòn</asp:LinkButton></li>
                                                    <li>Informar al adminstrador del sistema </li>
                                                    <li>Generar Ticket de Soporte Tecnico <a href="www.grupoingemeta.com/soporte/"> Ticket de Soporte</a></li>                                                    
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <asp:ModalPopupExtender ID="ModalPopuplogeo" runat="server" BehaviorID="programmaticModalPopupBehavior"
                TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="PanelModalPopulogeo"
                BackgroundCssClass="modalBackground" PopupDragHandleControlID="programmaticPopupDragHandle">
            </asp:ModalPopupExtender>
            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
            <asp:Panel runat="server" Class="span5" ID="PanelModalPopulogeo" Style="display: none; padding: 10px" BorderWidth="3" BorderStyle="Solid" BorderColor="#666666" BackColor="White" defaultbutton ="btnLogin">
                <asp:Panel runat="Server" ID="programmaticPopupDragHandle" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#666666" ForeColor="White" Font-Bold="True" Font-Size="13">Sesion terminada por inactividad ¡ Por favor Ingrese Usuario y Contraseña para Continuar ! </asp:Panel>
                <asp:Label ID="lblMensajeLogeo" runat="server" Text="" ForeColor="Red" Font-Bold="true" Width="100%" Style="text-align: center;"></asp:Label>
                <br />

                <div class="control-group">
                    <div class="controls" style="text-align: center">
                        <asp:TextBox ID="txt_90_T06NombreUsuario" runat="server" placeholder="Nombre Usuario" class="span2"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="txt_90_T06NombreUsuarioFilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"  ValidChars="." TargetControlID="txt_90_T06NombreUsuario"></asp:FilteredTextBoxExtender>
                        <div></div>
                        <asp:TextBox ID="txt_90_T06Clave" runat="server" placeholder="Constraseña" class="span2" TextMode="Password"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="txt_90_T06ClaveFilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers,LowercaseLetters,UppercaseLetters" TargetControlID="txt_90_T06Clave"></asp:FilteredTextBoxExtender>
                        <div style="height: 1px"></div>
                        <asp:Button ID="btnlogin" runat="server" Text="Ingresar" class="btn btn-primary" UseSubmitBehavior="False" />
                        <asp:Button ID="btnloginCancelar" runat="server" Text="Cancelar" class="btn btn-primary" UseSubmitBehavior="False" />
                    </div>
                </div>
            </asp:Panel>

        </div>


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
