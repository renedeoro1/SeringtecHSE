<%@ Page Debug="true" ValidateRequest="false" Language="vb" AutoEventWireup="false" CodeBehind="AtLAccion.aspx.vb" Inherits="HSENUEVO.AtLAccion" %>

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


        <div id="sidebar">
            <ul class="side-nav-Azul accordion_mnu collapsible">
                <li><a href="plataforma.aspx"><span class="white-icons computer_imac"></span>Plataforma</a></li>
                <asp:Literal ID="LitIzquierdo" runat="server"></asp:Literal>

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
                                                            <td style="width: 80px">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/iconos/AccionAtl.png" />
                                                            </td>
                                                            <td>
                                                                <h1>Atl Acción</h1>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style="font-size: 10px; font-weight: bold; color: #000080; text-align: right;">
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="Panel_Mensaje_Mal" runat="server" Visible="false" Width="100%" ForeColor="White" BackColor="DarkRed">
                                            <table style="text-align: center; width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMensaje_Mal" runat="server" Text="" Font-Bold="True" Font-Size="11"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel_Mensaje_Bien" runat="server" Visible="false" Width="100%" ForeColor="White" BackColor="#339933" Font-Bold="true" Font-Size="13">
                                            <table style="text-align: center; width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMensaje_Bien" runat="server" Text="" Font-Bold="True" Font-Size="11"></asp:Label>
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
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_B01_T12Codigo" runat="server" Text="Codigo" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:TextBox ID="txt_B01_T12Codigo" runat="server" Width="90%" MaxLength="30"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_B01_T12Codigo" runat="server" Enabled="True" TargetControlID="txt_B01_T12Codigo" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_B01_T12Nombre" runat="server" Text="Nombre" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:TextBox ID="txt_B01_T12Nombre" runat="server" Width="90%" MaxLength="200"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="filtered_B01_T12Nombre" runat="server" Enabled="True" TargetControlID="txt_B01_T12Nombre" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="pure-u-1-6">
                                                                    <asp:Label ID="lbl_B01_T12Activo" runat="server" Text="Activo" Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                    <asp:DropDownList ID="drop_B01_T12Activo" runat="server" class="chzn-select" Width="90%" placeholder="1_T12Activo"></asp:DropDownList>
                                                                </div>

                                                            </div>


                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div> 
                            </div>


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
                                        <th>Nombre
                                        </th>
                                        <th>Acctivo
                                        </th>
                                   

                                    </tr>

                                </thead>
                                <tbody runat="server" id="bodytabla">
                                </tbody>
                            </table>
                                                            <div style="height: 12px"></div>
                                                            <div class="pure-u-1" style="text-align: right">
                                                                <asp:Button ID="btnImprimir" runat="server" Text="" Width="65px" Height="55px" class="botonImprimir" UseSubmitBehavior="False" />
                                                                <asp:Button ID="btnGuardar" runat="server" Text="" class="botonGuardar" Width="65px" Height="55px" UseSubmitBehavior="False" />
                                                                <asp:Button ID="btnLimpiar" runat="server" Text="" class="botonLimpiar" Width="65px" Height="55px" UseSubmitBehavior="False" />

                                                                <asp:Button ID="btnEliminar" runat="server" Text="" class="botonEliminar" Width="65px" Height="55px" UseSubmitBehavior="False" />
                                                                <asp:Button ID="btnCerrarSesion" runat="server" Text="" class="botonSalir" Width="65px" Height="55px" UseSubmitBehavior="False" Visible="false" />

                                                            </div>

                        


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

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txtFiltroBusqueda" EventName="TextChanged" />
                        <asp:PostBackTrigger ControlID="btnImprimir" />
                    </Triggers>
                </asp:UpdatePanel>
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
