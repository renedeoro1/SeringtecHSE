<%@ Page Debug="true" ValidateRequest="false" Language="vb" AutoEventWireup="false" CodeBehind="RegistrodeTiempo.aspx.vb" Inherits="ONTIME.RegistrodeTiempo_Tem" %>

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
    <%--    <script type="text/javascript">$document.on"ready", function  { $".chzn-select".chosen; $".data-tbl-simple".dataTable; }</script>
    <script languaje="javascript">  </body ></html ></script>--%>
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

        #parent {
            Word-break: break-all;
        }

        #colLeft {
            float: left;
            max-width: 5%;
        }

        #colCenter {
            float: left;
            max-width: 90%;
        }

        #colRight {
            float: right;
            max-width: 5%;
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
    
    <script>
        $(function () {
            $('#TbodyBusquedaEntrgableActividad tr>*').click(function (e) {
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
            $('#TbodyBusquedaEntrgable tr>*').click(function (e) {
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
        #TbodyBusquedaEntrgableActividad a {
            text-decoration: none;
            color: inherit;
        }

        #TbodyBusquedaEntrgableActividad tr:nth-child(odd) {
            background-color: #CBCBCB;
            color: darkblue;
        }

        #TbodyBusquedaEntrgableActividad tr:hover {
            background-color: #F5F5F5;
            color: #1069EE;
        }

        #TbodyBusquedaEntrgableActividad {
            border-spacing: 0;
            border-collapse: collapse;
            border: transparent;
            color: darkblue;
        }
    </style>

        <style>
        #TbodyBusquedaEntrgable a {
            text-decoration: none;
            color: inherit;
        }

        #TbodyBusquedaEntrgable tr:nth-child(odd) {
            background-color: #CBCBCB;
            color: darkblue;
        }

        #TbodyBusquedaEntrgable tr:hover {
            background-color: #F5F5F5;
            color: #1069EE;
        }

        #TbodyBusquedaEntrgable {
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
    <script>

        function myFunctionVer(form) {
            // Get the checkbox
            var formulario = eval(form)
            var checkBox = document.getElementById("chkTodos_Ver");
            // Get the output text
            // If the checkbox is checked, display the output text
            if (checkBox.checked == true) {
                for (var i = 0, len = formulario.elements.length; i < len; i++) {
                    if (formulario.elements[i].type == "checkbox")


                        formulario.elements[i].checked = "checked";
                }
            } else {
                for (var i = 0, len = formulario.elements.length; i < len; i++) {
                    if (formulario.elements[i].type == "checkbox")


                        formulario.elements[i].checked = "";
                }
            }
        }

    </script>
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
            <div class="navbar-inner-Azul">
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
        <asp:Panel ID="Panel5" runat="server" Visible="false">
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
                    <li><a href="aprobacionTiempoAprobadores.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Aprobacion deTiempo</a></li>                    
                    <%--<li><a href="aprobacionTiempoAprobadoresHistorial.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Historial Aprobacion</a></li>--%>                                                                                 
                </ul>
                </li>
                    </asp:Literal>

                    <asp:Literal ID="Lit_Coordinadores" runat="server">
                <li><a href="#"><span class="white-icons shuffle"></span>Coordinadores</a><ul class="acitem">
                    <li><a href="aprobacionTiempoConfirmacion.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Confirmación de Tiempo</a></li>
                    <%--<li><a href="aprobacionTiempoHistorial.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Historial de Confirmación</a></li>--%>
                    
                </ul>
                </li>
                    </asp:Literal>
                    <asp:Literal ID="Lit_Funcionarios" runat="server">
                <li><a href="#"><span class="white-icons documents"></span>FUNCIONARIOS</a><ul class="acitem">
                    <li><a href="RegistrodeTiempo.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Registro Tiempo</a></li>                    
                    <%--<li><a href="RegistrodeTiempoHistorial.aspx"><span class="sidenav-icon"><span class="sidenav-link-color"></span></span>Historial Registro Tiempo</a></li>--%>
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
        </asp:Panel>

        <asp:Literal ID="LitSuperior" runat="server"></asp:Literal>
        <asp:Literal ID="LitIzquierdo" runat="server"></asp:Literal>


        <div id="main-content" class="full-fluid">
            <div class="container-fluid">
<%--                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <div class="row-fluid">
                            <div class="span12">
                                <div class="nonboxy-widget">
                                    <div class="breadcrumb">
                                        <div id="divBotones" runat="server" class="pure-form pure-form-stacked">
                                            <div class="pure-g-r">
                                                <div class="pure-u-1">

                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 80px">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/iconos/RegistroTiempo13.PNG" />

                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="20pt" Text="REGISTRO DE TIEMPO" ForeColor="#996600"></asp:Label>

                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:Label ID="Label22" runat="server" Text="usuario: " Width="100%" ForeColor="#000066" Font-Bold="True" Font-Size="10"></asp:Label>
                                                                <asp:Label ID="lblUsuarioLogueado_Nombre2" runat="server" Text="Label" Width="100%" ForeColor="#000066" Font-Bold="True" Font-Size="12"></asp:Label>
                                                                <div style="height: 8px"></div>
                                                                <asp:LinkButton ID="LINKUsuarioLogueado_Nombre2" runat="server" PostBackUrl="~/plataforma.aspx" Font-Size="11" Font-Bold="True" Text="Inicio"></asp:LinkButton> &nbsp;/&nbsp;
                                                                
                                                                
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
                            <div class="span12">
                                <div class="nonboxy-widget">
                                    <div class="widget-content">
                                        <div class="widget-box">
                                            <div class="form-horizontal well">
                                                <fieldset>
                                                    <div id="divContendio" runat="server" class="pure-form pure-form-stacked">

                                                        <div class="pure-g-r">
                                                            <asp:Panel ID="panel_IdAprobacion" runat="server" class="pure-u-1-6">

                                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="10pt" Text="ID Envio" ForeColor="Green"></asp:Label>
                                                                <asp:TextBox ID="txt_IdAprobacionTemporal" runat="server" Width="100%" ReadOnly="true" ForeColor="White" Font-Bold="true" BackColor="Green"></asp:TextBox>
                                                                <asp:TextBox ID="txt_IdAprobacion" runat="server" Width="100%" ReadOnly="true" ForeColor="Black" Visible="false"></asp:TextBox>

                                                            </asp:Panel>
                                                            <asp:Panel ID="Panel4" runat="server" Visible="false">
                                                                <div class="pure-u-1-8">
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="10pt" Text="Version"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownList1" runat="server" class="chzn-select" placeholder="1_T03Entregable" Width="80%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="pure-u-1-8">
                                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="10pt" Text="Horas"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownList2" runat="server" class="chzn-select" placeholder="1_T03Entregable" Width="80%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="pure-u-1-8">
                                                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="10pt" Text="Minutos"></asp:Label>
                                                                    <asp:DropDownList ID="DropDownList3" runat="server" class="chzn-select" placeholder="1_T03Entregable" Width="80%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="10pt" Text="Horas" Visible="false"></asp:Label>
                                                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="10" Visible="false" Width="90%"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03Horas" ValidChars="'">
                                                                </asp:FilteredTextBoxExtender>
                                                                <div class="pure-u-5-8">
                                                                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="10pt" Text="Descripcion Actividad"></asp:Label>
                                                                    <asp:TextBox ID="TextBox2" runat="server" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03DescripcionAcguividad" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T03Observaciones" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Observaciones"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T03Observaciones" runat="server" MaxLength="1000" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T03Observaciones" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03Observaciones" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Aprobado"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T03Aprobado" runat="server" MaxLength="2" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T03Aprobado" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03Aprobado" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Aprobado_Fecha"></asp:Label>
                                                                    <asp:TextBox ID="TextBox3" runat="server" MaxLength="30" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03Aprobado_Fecha" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Aprobado_Hora"></asp:Label>
                                                                    <asp:TextBox ID="TextBox4" runat="server" MaxLength="30" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03Aprobado_Hora" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-2">
                                                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Aprobado_Observacion"></asp:Label>
                                                                    <asp:TextBox ID="TextBox5" runat="server" Height="90px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03Aprobado_Observacion" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-2">
                                                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Aprobado_Observacion_Rechazo"></asp:Label>
                                                                    <asp:TextBox ID="TextBox6" runat="server" Height="90px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03Aprobado_Observacion_Rechazo" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="10pt" Text="3EnviadoAProbacion"></asp:Label>
                                                                    <asp:TextBox ID="TextBox7" runat="server" MaxLength="30" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T03EnviadoAProbacion" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T23Fecha" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Fecha"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T23Fecha" runat="server" MaxLength="30" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T23Fecha" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T23Fecha" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T23Consecutivo" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Consecutivo"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T23Consecutivo" runat="server" MaxLength="10" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T23Consecutivo" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T23Consecutivo" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T23Codigo" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Codigo"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T23Codigo" runat="server" MaxLength="30" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T23Codigo" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T23Codigo" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T23Hora" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Hora"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T23Hora" runat="server" MaxLength="10" Width="90%"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T23Hora" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T23Hora" ValidChars="'">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </div>
                                                            </asp:Panel>


                                                            <asp:Label ID="lbl_01_T03Cliente" runat="server" Text="Cliente" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="drop_01_T03Cliente" runat="server" class="chzn-select" Width="90%" placeholder="1_T03Contrato" Visible="false"></asp:DropDownList>

                                                            <asp:Label ID="lbl_01_T03Contrato" runat="server" Text="Contrato" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="drop_01_T03Contrato" runat="server" class="chzn-select" Width="90%" placeholder="1_T03Contrato" Visible="false"></asp:DropDownList>

                                                            <asp:Label ID="lbl_01_T03Disciplina" runat="server" Text="Disciplina" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="drop_01_T03Disciplina" runat="server" class="chzn-select" Width="90%" placeholder="1_T03ODS" Visible="false"></asp:DropDownList>

                                                            <asp:Label ID="lbl_01_T03Consecutivo" runat="server" Text="3Consecutivo" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txt_01_T03Consecutivo" runat="server" Width="100%" MaxLength="10" Visible="false"></asp:TextBox>

                                                            <asp:Label ID="lbl_01_T03Minutos" runat="server" Text="Minutos" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="drop_01_T03Minutos" runat="server" class="chzn-select" Width="80%" placeholder="1_T03Entregable" Visible="false"></asp:DropDownList>

                                                            <asp:Label ID="lbl_01_T03Horas2" runat="server" Text="Horas" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txt_01_T03Horas" runat="server" Width="90%" MaxLength="10" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="txt_01_T03HorasSolo" runat="server" Width="90%" MaxLength="10" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="txt_01_T03MinutoSolo" runat="server" Width="90%" MaxLength="10" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="txt_01_T03HorasConversion" runat="server" Width="90%" MaxLength="10" Visible="false"></asp:TextBox>

                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T03Horas" runat="server" Enabled="True" TargetControlID="txt_01_T03Horas" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>

                                                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T03TiempoaHoras" runat="server" Text="3Observaciones" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T03TiempoaHoras" runat="server" Width="90%" MaxLength="15"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T03TiempoaHoras" runat="server" Enabled="True" TargetControlID="txt_01_T03TiempoaHoras" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                                </div>

                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T03Aprobado_Fecha" runat="server" Text="3Aprobado_Fecha" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T03Aprobado_Fecha" runat="server" Width="90%" MaxLength="30"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T03Aprobado_Fecha" runat="server" Enabled="True" TargetControlID="txt_01_T03Aprobado_Fecha" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                                </div>

                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T03Aprobado_Hora" runat="server" Text="3Aprobado_Hora" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T03Aprobado_Hora" runat="server" Width="90%" MaxLength="30"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T03Aprobado_Hora" runat="server" Enabled="True" TargetControlID="txt_01_T03Aprobado_Hora" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-2">
                                                                    <asp:Label ID="lbl_01_T03Aprobado_Observacion" runat="server" Text="3Aprobado_Observacion" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T03Aprobado_Observacion" runat="server" Width="90%" TextMode="MultiLine" Height="90px"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T03Aprobado_Observacion" runat="server" Enabled="True" TargetControlID="txt_01_T03Aprobado_Observacion" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-2">
                                                                    <asp:Label ID="lbl_01_T03Aprobado_Observacion_Rechazo" runat="server" Text="3Aprobado_Observacion_Rechazo" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T03Aprobado_Observacion_Rechazo" runat="server" Width="90%" TextMode="MultiLine" Height="90px"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T03Aprobado_Observacion_Rechazo" runat="server" Enabled="True" TargetControlID="txt_01_T03Aprobado_Observacion_Rechazo" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_01_T03EnviadoAProbacion" runat="server" Text="3EnviadoAProbacion" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:TextBox ID="txt_01_T03EnviadoAProbacion" runat="server" Width="90%" MaxLength="30"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_01_T03EnviadoAProbacion" runat="server" Enabled="True" TargetControlID="txt_01_T03EnviadoAProbacion" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                                </div>

                                                            </asp:Panel>


                                                        </div>
                                                        <div style="height: 12px"></div>
                                                        <table style="padding: 5px; border: thin solid #486794; background-color: #E8B617; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                                                            <thead>
                                                                <tr>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 100px">Proyectos 
                                                                            <asp:Label ID="lbl_01_T03ODS" runat="server" Text="Proyectos" Font-Bold="True" Font-Size="10pt" ForeColor="White" Visible="false"></asp:Label>
                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 220px">Entregable Cliente
                                                                            <asp:Label ID="lbl_01_T03Entregable" runat="server" Text="Entregable" Font-Bold="True" Font-Size="10pt" ForeColor="White" Visible="false"></asp:Label>
                                                                        &nbsp;&nbsp;
                                                                        <asp:ImageButton ID="ImageButton_EntreableEco" runat="server" ImageUrl="~/iconos/LUPAOK.png" />
                                                                        
                                                                                                                                            </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 220px">Entregable Seringtec
                                                                            <asp:Label ID="Label29" runat="server" Text="Entregable" Font-Bold="True" Font-Size="10pt" ForeColor="White" Visible="false"></asp:Label>
                                                                        &nbsp;&nbsp;
                                                                        <asp:ImageButton ID="ImageButton_EntreableSering" runat="server" ImageUrl="~/iconos/LUPAOK.png" />
                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Actividades
                                                                            <asp:Label ID="Label20" runat="server" Text="Entregable" Font-Bold="True" Font-Size="10pt" ForeColor="White" Visible="false"></asp:Label>
                                                                        &nbsp;&nbsp;
                                                                        <asp:ImageButton ID="ImageButton_EntreableActividades" runat="server" ImageUrl="~/iconos/LUPAOK.png" />
                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 200px">Observaciones
                                                                            <asp:Label ID="lbl_01_T03DescripcionAcguividad" runat="server" Text="Descripcion Actividad" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 40px">Version
                                                                            <asp:Label ID="lbl_01_T03Version" runat="server" Text="Version" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 60px">Fecha
                                                                            <asp:Label ID="lbl_01_T03Fecha" runat="server" Text="Fecha" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 40px">Horas
                                                                            <asp:Label ID="lbl_01_T03Horas" runat="server" Text="Horas" Font-Bold="True" Font-Size="10pt" Visible="false"></asp:Label>
                                                                    </th>
                                                                    <th style="margin: 0px; padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; vertical-align: middle; text-align: center; width: 57px" rowspan="2">Estado&nbsp; Proceso
                                                                        <asp:Label ID="lbl_01_T03Aprobado" runat="server" Font-Bold="True" Font-Size="10pt" Text="3Aprobado" Visible="false"></asp:Label>
                                                                        <asp:DropDownList ID="drop_01_T03Aprobado" runat="server" ForeColor="Black" placeholder="1_T03Entregable" Visible="False" Width="100%">
                                                                        </asp:DropDownList>
                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: green; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 103px">
                                                                        <asp:Label ID="Label2" runat="server" Text="Gestión"></asp:Label>
                                                                    </th>

                                                                </tr>
                                                                <tr>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 136px">
                                                                        <asp:DropDownList ID="drop_01_T03ODS" runat="server" Width="100%" placeholder="1_T03ODS" ForeColor="Black"></asp:DropDownList>
                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 220px">
                                                                        <asp:DropDownList ID="drop_01_T03Entregable" runat="server" class="chzn-select" Width="95%" placeholder="1_T03Entregable" ForeColor="Black" Font-Size="9"></asp:DropDownList>
                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 220px">
                                                                        <asp:DropDownList ID="drop_01_T03EntregableSeringrtec" runat="server" class="chzn-select" Width="95%" placeholder="1_T03Entregable" ForeColor="Black" Font-Size="8"></asp:DropDownList>
                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; min-width :200px">
                                                                        <asp:DropDownList ID="drop_01_T03Actividad" runat="server" class="chzn-select" Width="95%" placeholder="1_T03Entregable" ForeColor="Black"></asp:DropDownList>
                                                                        <asp:TextBox ID="txt_01_T03Actividad_Texto" runat="server" Width="100%" TextMode="MultiLine" Visible="false" ForeColor="Black"></asp:TextBox>
                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 200px">
                                                                        <asp:TextBox ID="txt_01_T03DescripcionAcguividad" runat="server" Width="100%" TextMode="MultiLine" ForeColor="Black"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="filtered_01_T03DescripcionAcguividad" runat="server" Enabled="True" TargetControlID="txt_01_T03DescripcionAcguividad" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>

                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 60px">

                                                                        <asp:DropDownList ID="drop_01_T03Version" runat="server" Width="100%" placeholder="1_T03Entregable" ForeColor="Black"></asp:DropDownList>

                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 100px">

                                                                        <asp:DropDownList ID="drop_01_T03Fecha" runat="server" Width="100%" placeholder="1_T03Entregable" ForeColor="Black"></asp:DropDownList>
                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: black; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 70px">

                                                                        <asp:DropDownList ID="Drop_01_T03Horas" runat="server" Width="100%" placeholder="1_T03Entregable" ForeColor="Black"></asp:DropDownList>


                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: white; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 40px">
                                                                        <asp:ImageButton ID="btnItem" runat="server" ImageUrl="~/iconos/AddItem.png" />
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody runat="server" id="Tbody1" style="padding: 5px; border: thin solid #486794; background-color: white; color: #486794; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%; font-weight: normal; font-size: 11px;">
                                                            </tbody>

                                                        </table>
                                                        <div style="height: 30px"></div>
                                                        <table style="padding: 5px; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                                                            <thead>
                                                                <tr>
                                                                    <th style="padding: 5px; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 100px"></th>

                                                                    <th style="padding: 5px; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 180px"></th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 190px">
                                                                        <asp:Label ID="Label4" runat="server" Text="Aprobador" Font-Bold="True"></asp:Label>
                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 180px">
                                                                        <asp:Label ID="Label6" runat="server" Text="Fecha" Font-Bold="True" Width="120px"></asp:Label>
                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 50px">
                                                                        <asp:Label ID="Label7" runat="server" Text="Registradas" Font-Bold="True" Font-Size="9"></asp:Label>
                                                                    </th>
                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: green; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 20px">
                                                                        <asp:Label ID="Label8" runat="server" Text="H.N" Width="30px"></asp:Label>
                                                                    </th>

                                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: red; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 20px">
                                                                        <asp:Label ID="Label10" runat="server" Text="H.E" Width="30px"></asp:Label>
                                                                    </th>



                                                                </tr>
                                                                <tr>
                                                                    <th style="padding: 5px; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 100px"></th>

                                                                    <th style="padding: 5px; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 200px"></th>
                                                                    <th style="border: thin solid #FFFFFF; background-color: lightgray; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 140px">
                                                                        <asp:TextBox ID="txt_Cons_Aprobador" runat="server" Width="100%" Height="25" ReadOnly="True" BackColor="White"></asp:TextBox>
                                                                    </th>



                                                                    <th style="border: thin solid #FFFFFF; background-color: lightgray; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 140px">
                                                                        <asp:TextBox ID="txt_Cons_Fecha" runat="server" Width="100%" Height="25" ReadOnly="True" BackColor="White"></asp:TextBox>
                                                                    </th>
                                                                    <th style="border: thin solid #FFFFFF; background-color: lightgray; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 50px">
                                                                        <asp:TextBox ID="txt_Cons_Registradas" runat="server" Width="100%" Height="25" ReadOnly="True" BackColor="White" Style="text-align: center; vertical-align: central" Font-Bold="True"></asp:TextBox>
                                                                    </th>
                                                                    <th style="border: thin solid #FFFFFF; background-color: lightgray; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 40px">
                                                                        <asp:TextBox ID="txt_Cons_Normales" runat="server" Width="100%" Height="25" ReadOnly="True" BackColor="White" Style="text-align: center; vertical-align: central" Font-Bold="True"></asp:TextBox>
                                                                    </th>

                                                                    <th style="border: thin solid #FFFFFF; background-color: lightgray; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 40px">
                                                                        <asp:TextBox ID="txt_Cons_SobreRecargadas" runat="server" Width="100%" Height="25" ReadOnly="True" BackColor="White" Style="text-align: center; vertical-align: central" Font-Bold="True"></asp:TextBox>
                                                                    </th>



                                                                </tr>
                                                            </thead>

                                                        </table>


                                                        <div style="height: 12px"></div>
                                                        <div style="text-align: right">
                                                            <asp:Button ID="btnGuardar" runat="server" Text="" class="botonEnviarAprobacion" Width="60px" Height="55px" UseSubmitBehavior="False" />
                                                            <asp:Button ID="btnImprimir" runat="server" Text="" Width="65px" Height="55px" class="botonImprimir" UseSubmitBehavior="False" />

                                                            <asp:Button ID="btnLimpiar" runat="server" Text="" class="botonLimpiar" Width="65px" Height="55px" UseSubmitBehavior="False" />

                                                            <asp:Button ID="btnEliminar" runat="server" Text="" class="botonEliminar" Width="50px" Height="55px" UseSubmitBehavior="False" />
                                                            <asp:Button ID="btnCerrarSesion" runat="server" Text="" class="botonSalir" Width="65px" Height="55px" UseSubmitBehavior="False" Visible="false" />

                                                        </div>
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
                            <asp:Panel runat="Server" ID="programmaticPopupGuardar" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#003399" ForeColor="White" Font-Bold="True" Font-Size="13">¿ Esta Seguro de Enviar Estas Horas Para Aprobación?</asp:Panel>
                            <div style="height: 2px"></div>
                            <asp:Label ID="lblMensajeGuardarPopup" runat="server" Text="" ForeColor="Red" Font-Bold="true" Width="100%" Font-Size="12px" Style="text-align: center;"></asp:Label>
                            <div style="height: 2px"></div>

                            <div class="control-group">
                                <div class="controls" style="text-align: center">
                                    <asp:Button ID="btnEliminarPopup" runat="server" Text="Eliminar" class="btn btn-primary-rojo" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnGuardarPopup" runat="server" Text="Enviar" class="btn btn-primary" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnGuardarCancelarPopu" runat="server" Text="Cancelar" class="btn btn-primary-rojo" UseSubmitBehavior="False" />
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


                        <asp:Button runat="server" ID="Buttonuuumm1" Style="display: none" UseSubmitBehavior="False" />
                        <asp:ModalPopupExtender runat="server" ID="ModalPopupReenviar" BehaviorID="programmaticReenviar"
                            TargetControlID="Buttonuuumm1" PopupControlID="PanelModalPopupReenviar"
                            BackgroundCssClass="modalBackground" PopupDragHandleControlID="programmaticPopupReenviar">
                        </asp:ModalPopupExtender>
                        <asp:Panel runat="server" Class="span7" ID="PanelModalPopupReenviar" Style="display: none; padding: 10px" BorderWidth="3" BorderStyle="Solid" BorderColor="#417630" BackColor="White">
                            <asp:Panel runat="Server" ID="programmaticPopupReenviar" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#003399" ForeColor="White" Font-Bold="True" Font-Size="13">
                                <table style="text-align: center; width: 100%">
                                    <tr>
                                        <td>¿ Informacion Detalle para Reenviar horas Rechazadas?</td>
                                        <td style="width: 40px">
                                            <asp:Button ID="btnCancelar_Reenviar" runat="server" Text="Cerrar X" UseSubmitBehavior="False" /></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%-- <div style="height: 2px"></div>--%>
                            <asp:Label ID="lblMensajeReenviarPopup" runat="server" Text="" ForeColor="Red" Font-Bold="true" Width="100%" Font-Size="12px" Style="text-align: center;"></asp:Label>
                            <div style="height: 2px"></div>

                            <div class="control-group">
                                <div class="controls" style="text-align: center">
                                    <asp:TextBox ID="txt_Popup_Numdoc_Reenviar" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txt_Tabla_Reenviar" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lbl_Popup_NombreFuncionario_Reenvair" runat="server" Text="" Width="100%" ForeColor="DarkBlue" Font-Bold="true" Font-Size="11"></asp:Label>

                                    <table style="padding: 5px; border: thin solid #486794; background-color: #FFFFFF; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                                        <thead>
                                            <tr>
                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 130px">Fecha
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="txtReenviado_Fecha" runat="server" Width="150px" Enabled="false" ForeColor="Black" BackColor="White"></asp:TextBox></th>

                                            </tr>
                                            <tr>
                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 130px">Horas Registradas
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="txtReenviado_Horas" runat="server" Width="150px" Enabled="false" ForeColor="Black" BackColor="White"></asp:TextBox></th>

                                            </tr>
                                            <tr>
                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 130px">Actividad
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="txtReenviado_Actividad" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" Height="30px"></asp:TextBox></th>

                                            </tr>
                                            <tr>
                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 130px">Estado
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="txtReenviado_Estado" runat="server" Width="150px" Enabled="false" ForeColor="Black" BackColor="White"></asp:TextBox></th>

                                            </tr>
                                            <tr>
                                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 130px">Observaciones
                                                </th>
                                                <th>
                                                    <asp:TextBox ID="txtReenviado_Observacion" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" TextMode="MultiLine" Height="35px"></asp:TextBox>
                                                    <asp:TextBox ID="txtReenviado_Cliente" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtReenviado_Contrato" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtReenviado_Ods" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtReenviado_FEchaId" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtReenviado_Consecutivo" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtReenviado_Entregable" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtReenviado_Version" runat="server" Width="95%" Enabled="false" ForeColor="Black" BackColor="White" Visible="false"></asp:TextBox>


                                                </th>

                                            </tr>

                                        </thead>
                                        <tbody runat="server" id="Tbody33" style="padding: 5px; border: thin solid #486794; background-color: white; color: #486794; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%; font-weight: normal; font-size: 11px;">
                                        </tbody>
                                    </table>

                                    <div style="height: 20px"></div>

                                    <div style="text-align: left; width: 97%">


                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server" Text="Proyecto" Font-Bold="True" Font-Size="10pt" Width="80px"></asp:Label>
                                                </td>


                                                <td>
                                                    <asp:Label ID="Label26" runat="server" Text="Entregable Cliente" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label23" runat="server" Text="Entregable Seringtec" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>


                                                <td>
                                                    <asp:DropDownList ID="drop_Proyecto_Reenviar" runat="server" class="chzn-select" Width="200px"></asp:DropDownList>
                                                    <asp:DropDownList ID="drop_Cliente_Reenviar" runat="server" Visible="false"></asp:DropDownList>
                                                    <asp:DropDownList ID="drop_Contrato_Reenviar" runat="server" Visible="false"></asp:DropDownList>
                                                    <asp:DropDownList ID="drop_Disciplina_Reenviar" runat="server" Visible="false"></asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:DropDownList ID="drop_Entregable_Reenviar" runat="server" class="chzn-select" Width="180px"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="drop_Entregable_ReenviarSeringtec" runat="server" class="chzn-select" Width="180px"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label24" runat="server" Text="Actividades" Font-Bold="True" Font-Size="10pt" Width="80px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" Text="Version" Font-Bold="True" Font-Size="10pt" Width="100px"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label27" runat="server" Text="Tiempo" Font-Bold="True" Font-Size="10pt" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="drop_Actividades_Reenviar" runat="server" class="chzn-select" Width="200px"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="drop_Version_Reenviar" runat="server" Width="60px"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="drop_Tiempo_Reenviar" runat="server" Width="70px"></asp:DropDownList>
                                                </td>
                                            </tr>

                                        </table>
                                        <div style="height: 6px">
                                        </div>
                                        <table style="width: 100%">
                                        </table>
                                        <div style="height: 6px">
                                        </div>
                                        <asp:Label ID="Label32" runat="server" Text="Observaciones Reenvio Horas para Aprobacion" Font-Bold="True" Font-Size="10pt" Width="100%"></asp:Label>
                                        <asp:TextBox ID="txt_Observaciones_Reenviar" runat="server" Width="100%" TextMode="MultiLine" Height="45px" MaxLength="500"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True" TargetControlID="txt_Observaciones_Reenviar" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>

                                    </div>
                                    <asp:Button ID="btnAprobar_Reenviar" runat="server" Text="" class="botonEnviarAprobacion" Width="65px" Height="55px" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnRechazar_Reenviar" runat="server" Text="" class="botonRechazar" Width="65px" Height="55px" UseSubmitBehavior="False" Visible="false" />

                                </div>
                            </div>
                        </asp:Panel>


                        <asp:Button runat="server" ID="Buttonuuu1" Style="display: none" UseSubmitBehavior="False" />
                        <asp:ModalPopupExtender runat="server" ID="ModalPopupVer" BehaviorID="programmaticVer"
                            TargetControlID="Buttonuuu1" PopupControlID="PanelModalPopupVer"
                            BackgroundCssClass="modalBackground" PopupDragHandleControlID="programmaticPopupVer">
                        </asp:ModalPopupExtender>
                        <asp:Panel runat="server" Class="span8" ID="PanelModalPopupVer" Style="display: none; padding: 10px" BorderWidth="3" BorderStyle="Solid" BorderColor="#417630" BackColor="White">
                            <asp:Panel runat="Server" ID="programmaticPopupVer" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#003399" ForeColor="White" Font-Bold="True" Font-Size="13">
                                <table style="text-align: center; width: 100%">
                                    <tr>
                                        <td>Informacion Detalle para Enviar Aprobar horas</td>
                                        <td style="width: 40px">
                                            <asp:Button ID="btnCancelar_Ver" runat="server" Text="Cerrar X" UseSubmitBehavior="False" /></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div style="height: 2px"></div>
                            <asp:Label ID="lblMensajeVerPopup" runat="server" Text="" ForeColor="Red" Font-Bold="true" Width="100%" Font-Size="12px" Style="text-align: center;"></asp:Label>
                            <div style="height: 2px"></div>

                            <div class="control-group">
                                <div class="controls" style="text-align: center">
                                    <asp:TextBox ID="txt_Popup_Numdoc_Ver" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txt_Tabla_Ver" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lbl_Popup_NombreFuncionario_Ver" runat="server" Text="" Width="100%" ForeColor="DarkBlue" Font-Bold="true" Font-Size="11"></asp:Label>
                                    <div style="height: 12px"></div>
                                    <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Width="100%" Height="250px">
                                        <table style="padding: 5px; border: thin solid #486794; background-color: #E8B617; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                                            <thead>
                                                <tr>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 55px">Fecha
                                                    </th>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 120px">
                                                        <asp:DropDownList ID="DropODS_Ver" runat="server" Width="100%" placeholder="1_T03Entregable" ForeColor="Black"></asp:DropDownList>
                                                    </th>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Entregable
                                                    </th>

                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 50px">horas
                                                    </th>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 180px">Observaciones
                                                    </th>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: darkorange; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 30px">Todos
                                                        <asp:CheckBox ID="chkTodos_Ver" runat="server" onclick="myFunctionVer(document.forms[0])" />
                                                    </th>




                                                </tr>
                                            </thead>
                                            <tbody runat="server" id="Tbody2" style="padding: 5px; border: thin solid #486794; background-color: white; color: #486794; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%; font-weight: normal; font-size: 11px;">
                                            </tbody>
                                        </table>


                                    </asp:Panel>
                                    <div style="text-align: left; width: 97%">
                                        <asp:Label ID="Label5" runat="server" Text="Observaciones" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                        <asp:TextBox ID="txt_Observaciones_Ver" runat="server" Width="100%" TextMode="MultiLine" Height="60px" MaxLength="500"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txt_Observaciones_Ver" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>

                                    </div>
                                    <asp:Button ID="btnAprobar_Ver" runat="server" Text="" class="botonEnviarAprobacion" Width="65px" Height="55px" UseSubmitBehavior="False" />
                                    <asp:Button ID="btnRechazar_Ver" runat="server" Text="" class="botonRechazar" Width="65px" Height="55px" UseSubmitBehavior="False" Visible="false" />

                                </div>
                            </div>
                        </asp:Panel>


                        <asp:Button runat="server" ID="Buttonjjjjjjjj1" Style="display: none" UseSubmitBehavior="False" />
                        <asp:ModalPopupExtender runat="server" ID="ModalPopupBuscarEntregable" BehaviorID="programmaticBuscarEntregable"
                            TargetControlID="Buttonjjjjjjjj1" PopupControlID="PanelModalPopupBuscarEntregable"
                            BackgroundCssClass="modalBackground" PopupDragHandleControlID="programmaticPopupBuscarEntregale">
                        </asp:ModalPopupExtender>
                        <asp:Panel runat="server" Class="span8" ID="PanelModalPopupBuscarEntregable" Style="display: none; padding: 10px" BorderWidth="3" BorderStyle="Solid" BorderColor="#417630" BackColor="White">
                            <asp:Panel runat="Server" ID="programmaticPopupBuscarEntregale" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#003399" ForeColor="White" Font-Bold="True" Font-Size="13">
                                <table style="text-align: center; width: 100%">
                                    <tr>
                                        <td>Informacion Busqueda de Entregables</td>
                                        <td style="width: 40px">
                                            <asp:Button ID="btncerrarModalBusquedaEntregable" runat="server" Text="Cerrar X" UseSubmitBehavior="False" /></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div style="height: 2px"></div>
                            <asp:Label ID="Label30" runat="server" Text="" ForeColor="Red" Font-Bold="true" Width="100%" Font-Size="12px" Style="text-align: center;"></asp:Label>
                            <div style="height: 2px"></div>

                            <div class="control-group">
                                <div class="controls" style="text-align: center">
                                    <asp:TextBox ID="txt_ODS_BusquedaEntregable" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txt_Tabla_BusquedaEntregable" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lbl_Popup_Nombre_ODS_BusquedaEntregable" runat="server" Text="" Width="100%" ForeColor="DarkBlue" Font-Bold="true" Font-Size="11"></asp:Label>
                                    <div style="height:8px"></div>
                                    <asp:Panel ID="Panel6" runat="server" ScrollBars="Auto" Width="100%" >
                                        <table style="padding: 5px; border: thin solid #486794; background-color: #E8B617; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                                            <thead>
                                                <tr>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 100px">Codigo Cliente
                                                        <asp:TextBox ID="txt_CodigoBusquedaEntregable" runat="server"  Width ="100px"></asp:TextBox>
                                                    </th>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 100px">Codigo Seringtec
                                                        <asp:TextBox ID="txt_CodigoBusquedaEntregableSeringtec" runat="server"  Width ="100px"></asp:TextBox>
                                                    </th>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Nombre
                                                        <asp:TextBox ID="txt_NombreEntegable" runat="server"  Width ="90%"></asp:TextBox>
                                                    </th>

                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: darkorange; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 30px">Seleccionar
                                                        
                                                    </th>




                                                </tr>
                                            </thead>
                                         </table>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto" Width="100%"  Height="250px">
                                        <table style="padding: 5px; border: thin solid #486794;  margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">                                            
                                            <tbody runat="server" id="TbodyBusquedaEntrgable" style="padding: 5px; border: thin solid #486794; background-color: white; color: #486794; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%; font-weight: normal; font-size: 11px;">
                                            </tbody>
                                        </table>


                                    </asp:Panel>

                                </div>
                            </div>
                        </asp:Panel>


                <asp:Button runat="server" ID="Buttonwwwww1" Style="display: none" UseSubmitBehavior="False" />
                        <asp:ModalPopupExtender runat="server" ID="ModalPopupBuscarEntregableActividad" BehaviorID="programmaticBuscarEntregableActividad"
                            TargetControlID="Buttonwwwww1" PopupControlID="PanelModalPopupBuscarEntregableActividad"
                            BackgroundCssClass="modalBackground" PopupDragHandleControlID="programmaticPopupBuscarEntregaleActividad">
                        </asp:ModalPopupExtender>
                        <asp:Panel runat="server" Class="span8" ID="PanelModalPopupBuscarEntregableActividad" Style="display: none; padding: 10px" BorderWidth="3" BorderStyle="Solid" BorderColor="#417630" BackColor="White">
                            <asp:Panel runat="Server" ID="programmaticPopupBuscarEntregaleActividad" Style="cursor: move; padding: 3px; border: solid 1px Gray; text-align: center;" BackColor="#003399" ForeColor="White" Font-Bold="True" Font-Size="13">
                                <table style="text-align: center; width: 100%">
                                    <tr>
                                        <td>Informacion Busqueda de Actividades</td>
                                        <td style="width: 40px">
                                            <asp:Button ID="btncerrarModalBusquedaEntregableActividad" runat="server" Text="Cerrar X" UseSubmitBehavior="False" /></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div style="height: 2px"></div>
                            <asp:Label ID="Label31" runat="server" Text="" ForeColor="Red" Font-Bold="true" Width="100%" Font-Size="12px" Style="text-align: center;"></asp:Label>
                            <div style="height: 2px"></div>

                            <div class="control-group">
                                <div class="controls" style="text-align: center">
                                    <asp:TextBox ID="txt_ODS_BusquedaEntregableActividad" runat="server" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txt_Tabla_BusquedaEntregableActividad" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lbl_Popup_Nombre_ODS_BusquedaEntregableActividad" runat="server" Text="" Width="100%" ForeColor="DarkBlue" Font-Bold="true" Font-Size="11"></asp:Label>
                                    <div style="height:8px"></div>
                                    <asp:Panel ID="Panel8" runat="server" ScrollBars="Auto" Width="100%" >
                                        <table style="padding: 5px; border: thin solid #486794; background-color: #E8B617; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                                            <thead>
                                                <tr>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 100px">Codigo Actividad
                                                        <asp:TextBox ID="txt_CodigoBusquedaEntregableActividad" runat="server"  Width ="100px"></asp:TextBox>
                                                    </th>
                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Nombre
                                                        <asp:TextBox ID="txt_NombreEntegableActividad" runat="server"  Width ="90%"></asp:TextBox>
                                                    </th>

                                                    <th style="padding: 5px; border: thin solid #FFFFFF; background-color: darkorange; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center; width: 30px">Seleccionar
                                                        
                                                    </th>




                                                </tr>
                                            </thead>
                                         </table>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel9" runat="server" ScrollBars="Auto" Width="100%"  Height="250px">
                                        <table style="padding: 5px; border: thin solid #486794;  margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">                                            
                                            <tbody runat="server" id="TbodyBusquedaEntrgableActividad" style="padding: 5px; border: thin solid #486794; background-color: white; color: #486794; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%; font-weight: normal; font-size: 11px;">
                                            </tbody>
                                        </table>


                                    </asp:Panel>

                                </div>
                            </div>
                        </asp:Panel>



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
                                    <asp:Button ID="btnVerificarDocumento" runat="server" Text="Verificar" class="btn btn-primary" Enabled="false" />
                                    <asp:Button ID="btnVerificarCancelarPopu" Visible="false" runat="server" Text="Cancelar" class="btn btn-primary" />

                                </div>
                            </div>
                        </asp:Panel>

<%--                    </ContentTemplate>
                    <Triggers>

                        <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txtFiltroBusqueda" EventName="TextChanged" />
                        <asp:PostBackTrigger ControlID="btnImprimir" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
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
