<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PerfilesxUsuarios.aspx.vb" Inherits="HSENUEVO.PerfilesxUsuariosVer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <asp:Literal ID="LitHead" runat="server"></asp:Literal>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>SERINGTEC</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="APP Lector de codigo QR">
    <meta name="SERINGTEC" content="APP Lector de codigo QR">
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

    <script>
            $(function () {
                $('#Tbody1 tr>*').click(function (e) {
                    var a = $(this).closest('tr').find('a')
                    e.preventDefault()
                    location.href = a.attr('href')
                })
            })


        $(document).ready(function () {
            $("#hide").on('click', function () {
                $("#element").hide();
                return false;
            });

            $("#show").on('click', function () {
                $("#element").show();
                return false;
            });
        });


    </script>
    <script>
        $(function () {
            $('#Tbody2 tr>*').click(function (e) {
                var a = $(this).closest('tr').find('a')
                e.preventDefault()
                location.href = a.attr('href')
            })
        })


        $(document).ready(function () {
            $("#hide").on('click', function () {
                $("#element").hide();
                return false;
            });

            $("#show").on('click', function () {
                $("#element").show();
                return false;
            });
        });


    </script>
    <style>
        #Tbody1 a {
            text-decoration: none;
            color: inherit;
        }

        #Tbody1 tr:nth-child(odd) {
            background-color: #CBCBCB;
            color: darkblue;
        }

        #Tbody1 tr:hover {
            background-color: #F5F5F5;
            color: #1069EE;
        }

        #Tbody1 {
            border-spacing: 0;
            border-collapse: collapse;
            border: transparent;
            color: darkblue;
        }
    </style>
    <style>
        #Tbody2 a {
            text-decoration: none;
            color: inherit;
        }

        #Tbody2 tr:nth-child(odd) {
            background-color: #CBCBCB;
            color: darkblue;
        }

        #Tbody2 tr:hover {
            background-color: #F5F5F5;
            color: #1069EE;
        }

        #Tbody2 {
            border-spacing: 0;
            border-collapse: collapse;
            border: transparent;
            color: darkblue;
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
                                            <asp:Image class="dropdown-toggle" data-toggle="dropdown" ID="Image21" runat="server" ImageUrl ="~/iconos/usuario10.png" /><b class="caret"></b></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="CambioClave.aspx">Cambio  Contraseña</a></li>
                                            <li><a href="actualizarsPerfil.aspx">Datos Perfil</a></li>
                                            <li><a href="login.aspx"><asp:Image ID="Image20" runat="server" ImageUrl="~/iconos/salida.png"  Width ="40%" Height ="40%" ToolTip="Cerrar Sesión" /></a></li>
                                        </ul>
                                    </li>
                                </ul>
                            </td>

                        </tr>
                    </table>



                    <asp:Panel ID="Panel19" runat="server" Visible="false">
                        <button data-target=".nav-collapse" data-toggle="collapse" class="btn btn-navbar" type="button"><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                        <div class="nav-collapse collapse">

                            <ul class="nav">
                                <asp:Panel ID="Panel18" runat="server" Visible="false">
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
                                </asp:Panel>
                                <li style="min-width: 500px"></li>



                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="nav-icon shuffle"></i>Datos Personales<b class="caret"></b></a>
                                    <ul class="dropdown-menu">
                                        <li><a href="CambioClave.aspx">Cambio de Contraseña</a></li>
                                        <li><a href="actualizarsPerfil.aspx">Actualizar Perfil</a></li>

                                    </ul>
                                </li>
                            </ul>


                        </div>
                    </asp:Panel>


                </div>
            </div>
        </div>
        <div id="sidebar">
            <ul class="side-nav-Azul accordion_mnu collapsible">
                <li><a href="plataforma.aspx"><span class="white-icons computer_imac"></span>Plataforma</a></li>
                <asp:Literal ID="Lit_Configuracion" runat="server">
                <li><a href="#"><span class="white-icons list"></span>Configuracion</a>
                    <ul class="acitem">
                    
                    
                    <li><a href="PerfilesxUsuarios.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Usuarios  x Perfiles</a></li>
                    <li><a href="Clientes.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Clientes</a></li>
                    <li><a href="ContratosMarco.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Contratos Marco</a></li>
                    <li><a href="ODS.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>ODS</a></li>
                    <li><a href="Disciplinas.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Disciplinas</a></li>
                    <%--<li><a href="funcionario.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Funcionarios</a></li>--%>
                    <li><a href="Lideres.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Lideres</a></li>                    
                        <li><a href="Funcionario.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Funcionarios</a></li>                                            
                        <li><a href="Actividades.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Actividades</a></li>                    
                        <li><a href="TipoFEchasEspeciales.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Tipo Fechas Especiales</a></li>                    
                        <li><a href="FechasEspeciales.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Fechas Especiales</a></li>                    
                        <li><a href="TipoNovedades.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Tipos Novedades</a></li>                    
                        <li><a href="ListadoDocumentosEntegables.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>listado Entregables</a></li>
                        <li><a href="Desbloqueo.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Codigo Desbloqueo</a></li>                    
                        <li><a href="ConfiguraOntimeEscritorio.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Sistema de Escritorio</a></li>                    
                        
            
                                        
                                                           
                    
                    
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


        <asp:Literal ID="LitSuperior" runat="server"></asp:Literal>
        <asp:Literal ID="LitIzquierdo" runat="server"></asp:Literal>

        <div id="main-content">

            <div class="container-fluid">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row-fluid">

                            <div class="span12">
                                <div class="nonboxy-widget">
                                    <div class="breadcrumb">
                                        <div id="divBotones" runat="server" class="pure-form pure-form-stacked">
                                            <div class="pure-g-r">
                                                <div class="pure-u-1">

                                                    <table>
                                                        <tr>
                                                            <td style="width: 90px">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/iconos/perfilusuario3.png" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="20pt" Text=" PERFILES DE USUARIO" ForeColor="#996600"></asp:Label>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="font-size: 10px; font-weight: bold; color: #000080; text-align: right;">
                                                    <asp:Label ID="lblUsuario_NombreFuncionario" runat="server" Text="" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="txtUsuario_TipoDoc" runat="server" Width="90%" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtUsuario_NumDoc" runat="server" Width="90%" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtUsuario_Nombre" runat="server" Width="90%" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="Panel_Mensaje_Mal" runat="server" Visible="false" Width="100%" ForeColor="White" BackColor="DarkRed">
                                            <table style="text-align: center; width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMensaje_Mal" runat="server" Text="" Font-Bold="True" Font-Size="11"></asp:Label>
                                                        <asp:Image ID="img_Mensaje_Mal" runat="server" ImageUrl="~/iconos/MensajeMal.png" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel_Mensaje_Bien" runat="server" Visible="false" Width="100%" ForeColor="White" BackColor="#339933" Font-Bold="true" Font-Size="13">
                                            <table style="text-align: center; width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMensaje_Bien" runat="server" Text="" Font-Bold="True" Font-Size="11"></asp:Label>
                                                        <asp:Image ID="img_Mensaje_Bien" runat="server" ImageUrl="~/iconos/MensajeBien.png" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="height: 5px"></div>
                                        </asp:Panel>


                                        <asp:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server" TargetControlID="Panel_Mensaje_Bien" VerticalSide="Bottom" VerticalOffset="20" HorizontalSide="Right" HorizontalOffset="90" ScrollEffectDuration=".1" />
                                        <asp:AlwaysVisibleControlExtender ID="ace" runat="server" TargetControlID="Panel_Mensaje_Mal" VerticalSide="Bottom" VerticalOffset="20" HorizontalSide="Right" HorizontalOffset="90" ScrollEffectDuration=".1" />



                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span7">
                                <div class="nonboxy-widget">
                                    <div class="widget-content">
                                        <div class="widget-box">
                                            <div class="form-horizontal well">
                                                <fieldset>
                                                    <div id="divContendio" runat="server" class="pure-form pure-form-stacked">


                                                        <asp:Label ID="lbl_01_T18Aplicacion" runat="server" Text="Aplicacion" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="drop_01_T18Aplicacion" runat="server" class="chzn-select" Width="90%" placeholder="1_T18Aplicacion" Visible="false"></asp:DropDownList>



                                                        
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 100%; vertical-align: top;">
                                                                    <table style="padding: 5px; border: thin solid #486794; background-color: #E8B617; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;" colspan="4">Listado de Funcionarios</th>

                                                                            </tr>
                                                                            <tr>
                                                                                <th style="padding: 2px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">
                                                                                    <asp:Label ID="lbl_01_T18NumDocFuncionario" runat="server" Text="Funcionario" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                                                    <asp:DropDownList ID="drop_01_T18NumDocFuncionario" runat="server" class="chzn-select" Width="90%" placeholder="1_T18NumDocFuncionario"></asp:DropDownList>

                                                                                </th>
                                                                                <th style="padding: 2px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 70px">
                                                                                    <asp:Label ID="lbl_01_T18Perfil" runat="server" Text="Perfil" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>


                                                                                    <asp:DropDownList ID="drop_01_T18Perfil" runat="server" Width="145px" placeholder="1_T18Perfil"></asp:DropDownList>


                                                                                </th>
                                                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 40px">
                                                                                    <asp:CheckBox ID="chk_Todos" runat="server" /></th>
                                                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: darkgreen; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 40px">
                                                                                    <asp:Label ID="Label2" runat="server" Text="Agregar"></asp:Label>
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody runat="server" id="Tbody1" style="padding: 5px; border: thin solid #486794; background-color: white; color: #486794; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%; font-weight: normal; font-size: 11px;">
                                                                        </tbody>
                                                                        <tbody runat="server" id="Tbody3" style="padding: 5px; text-align: center; width: 100%; font-weight: normal; font-size: 11px; background-color: white; color: #486794">
                                                                            <tr>
                                                                                <td colspan="4" style="height: 15px"></td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <asp:Button ID="btnImprimir" runat="server" Text="" Width="65px" Height="55px" class="botonImprimir" UseSubmitBehavior="False" />
                                                                                    <asp:Button ID="btnGuardar" runat="server" Text="" class="botonGuardar" Width="65px" Height="55px" UseSubmitBehavior="False" />
                                                                                    <asp:Button ID="btnLimpiar" runat="server" Text="" class="botonLimpiar" Width="65px" Height="55px" UseSubmitBehavior="False" />

                                                                                    <asp:Button ID="btnEliminar" runat="server" Text="" class="botonEliminar" Width="65px" Height="55px" UseSubmitBehavior="False" />
                                                                                    <asp:Button ID="btnCerrarSesion" runat="server" Text="" class="botonSalir" Width="65px" Height="55px" UseSubmitBehavior="False" Visible="false" />



                                                                                </td>

                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>


                                                            </tr>
                                                        </table>


                                                    </div>


                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        
                            <div class="span5">
                                <div class="nonboxy-widget">
                                    <div class="widget-content">
                                        <div class="widget-box">
                                            <div class="form-horizontal well">
                                                <fieldset>
                                                    <div id="div1" runat="server" class="pure-form pure-form-stacked">


                                                        <asp:Label ID="Label3" runat="server" Text="Aplicacion" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" class="chzn-select" Width="90%" placeholder="1_T18Aplicacion" Visible="false"></asp:DropDownList>



                                                        
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 100%; vertical-align: top;">
                                                                    <table style="padding: 5px; border: thin solid #486794; background-color: #E8B617; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: darkgreen; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;" colspan="5">Listado de Permisos por Funcionario</th>

                                                                            </tr>
                                                                            <tr>
                                                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: darkgreen; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Recurso </th>
                                                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: darkorange; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 50px">Gestión </th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody runat="server" id="Tbody2" style="padding: 5px; border: thin solid #486794; background-color: white; color: #486794; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%; font-weight: normal; font-size: 11px;">
                                                                        </tbody>
                                                                    </table>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </div>


                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>


                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <div class="widget-head" style="height: 20px">
                                <h3>REGISTROS GUARDADOS</h3>
                            </div>
                            <div style="height: 8px"></div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_11_T04Sector0" runat="server" Font-Bold="True" Font-Size="10pt" Text="Busqueda Rapida"></asp:Label>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFiltroBusqueda" runat="server" class="input-xtipo160 text-tip" placeholder=""></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table class="data-tbl-simple table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Codigo
                                        </th>
                                        <th>Activo
                                        </th>
                                        <th>Activo
                                        </th>
                                    </tr>
                                </thead>
                                <tbody runat="server" id="bodytabla">
                                </tbody>
                            </table>

                        </asp:Panel>


                        <asp:Button runat="server" ID="Button1" Style="display: none" UseSubmitBehavior="False" />
                        <asp:ModalPopupExtender runat="server" ID="ModalPopupGuardar" BehaviorID="programmaticGuardar"
                            TargetControlID="Button1" PopupControlID="PanelModalPopupGuardar"
                            BackgroundCssClass="modalBackground" PopupDragHandleControlID="programmaticPopupGuardar">
                        </asp:ModalPopupExtender>
                        <asp:Panel runat="server" Class="span5" ID="PanelModalPopupGuardar" Style="display: none; padding: 10px" BorderWidth="3" BorderStyle="Solid" BorderColor="#417630" BackColor="White">
                            <asp:Panel runat="Server" ID="programmaticPopupGuardar" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#003399" ForeColor="White" Font-Bold="True" Font-Size="13">¿ Esta Seguro de Realizar esta transaccion ?</asp:Panel>
                            <div style="height: 2px"></div>
                            <asp:Label ID="lblMensajeGuardarPopup" runat="server" Text="" ForeColor="Red" Font-Bold="true" Width="100%" Font-Size="12px" Style="text-align: center;"></asp:Label>
                            <div style="height: 2px"></div>

                            <div class="control-group">
                                <div class="controls" style="text-align: center">
                                    <asp:Button ID="btnEliminarPopup" runat="server" Text="Eliminar" class="btn btn-primary-rojo" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnGuardarPopup" runat="server" Text="Guardar" class="btn btn-primary" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnGuardarCancelarPopu" runat="server" Text="Cancelar" class="btn btn-primary" UseSubmitBehavior="False" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Button runat="server" ID="ButtonSalir" Style="display: none" UseSubmitBehavior="False" />
                        <asp:ModalPopupExtender runat="server" ID="ModalPopupSalir" BehaviorID="programmaticSalir"
                            TargetControlID="ButtonSalir" PopupControlID="PanelModalPopupSalir"
                            BackgroundCssClass="modalBackground" PopupDragHandleControlID="programmaticPopupSalir">
                        </asp:ModalPopupExtender>
                        <asp:Panel runat="server" Class="span5" ID="PanelModalPopupSalir" Style="display: none; padding: 10px" BorderWidth="3" BorderStyle="Solid" BorderColor="#417630" BackColor="White">
                            <asp:Panel runat="Server" ID="programmaticPopupSalir" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#003399" ForeColor="White" Font-Bold="True" Font-Size="13">¿ Esta Seguro de Cerrar el Programa ?</asp:Panel>
                            <div style="height: 2px"></div>
                            <asp:Label ID="lblMensajeSalirPopup" runat="server" Text="¡...Esta Seguro De Salir del Programa...!" ForeColor="Red" Font-Bold="true" Width="100%" Font-Size="12px" Style="text-align: center;"></asp:Label>
                            <div style="height: 2px"></div>

                            <div class="control-group">
                                <div class="controls" style="text-align: center">
                                    <asp:Button ID="btnSalirPopup" runat="server" Text="   Salir   " class="btn btn-primary" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnSalirCancelarPopu" runat="server" Text="Cancelar" class="btn btn-primary" UseSubmitBehavior="False" />
                                </div>
                            </div>
                        </asp:Panel>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txtFiltroBusqueda" EventName="TextChanged" />
                        <asp:PostBackTrigger ControlID="btnImprimir" />
                    </Triggers>
                </asp:UpdatePanel>
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

                        <div style="height: 1px"></div>
                        <asp:Button ID="btnVerificarDocumento" runat="server" Text="Verificar" class="btn btn-primary" Enabled ="false"  />
                        <asp:Button ID="btnVerificarCancelarPopu" Visible="false" runat="server" Text="Cancelar" class="btn btn-primary" />

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
        <%--<script src="http://bp.yahooapis.com/2.4.21/browserplus-min.js"></script>--%>
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
