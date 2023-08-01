<%@ Page Debug="true" ValidateRequest="false" Language="vb" AutoEventWireup="false" CodeBehind="CalculosMecanica.aspx.vb" Inherits="HSENUEVO.CalculosMecanica" %>

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
        .auto-style1 {
            width: 178px;
            text-align: center;
        }
        .auto-style3 {
            width: 510px;
        }
        .auto-style4 {
            width: 288px;
            text-align: center;
        }
        .auto-style5 {
            font-weight: bold;
        }
        .auto-style7 {
            width: 288px;
            text-align: center;
            font-weight: bold;
        }
        .auto-style9 {
            width: 203px;
            height: 25px;
            text-align: center;
        }
        .auto-style10 {
            height: 25px;
        }
        .auto-style11 {
            height: 25px;
            text-align: center;
        }
        .auto-style12 {
            width: 203px;
            text-align: center;
        }
        .auto-style14 {
            width: 100%;
            height: 91px;
        }
        .auto-style16 {
            height: 26px;
            text-align: center;
        }
        .auto-style17 {
            /*   Styles added to the plot target container when there is an error go here.*/
        text-align: center;
            color: #000000;
        }
        .auto-style19 {
            /*   Styles added to the plot target container when there is an error go here.*/
        text-align: center;
            width: 537px;
        }
        .auto-style20 {
            height: 25px;
            text-align: center;
            width: 537px;
        }
        .auto-style23 {
            /*   Styles added to the plot target container when there is an error go here.*/
        text-align: center;
            width: 379px;
        }
        .auto-style24 {
            height: 25px;
            text-align: center;
            width: 379px;
        }
        .auto-style25 {
            /*   Styles added to the plot target container when there is an error go here.*/
        text-align: center;
            width: 260px;
        }
        .auto-style26 {
            height: 25px;
            text-align: center;
            width: 260px;
        }
        .auto-style27 {
            /*   Styles added to the plot target container when there is an error go here.*/
        text-align: center;
            color: #000000;
            width: 114px;
        }
        .auto-style28 {
            /*   Styles added to the plot target container when there is an error go here.*/
        text-align: center;
            width: 114px;
        }
        .auto-style29 {
            height: 25px;
            text-align: center;
            width: 114px;
        }
        .auto-style32 {
            width: 100%;
            zoom: 1;
            height: 2281px;
        }
        .auto-style33 {
            width: 1170px;
            height: 2146px;
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
                <%--                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <div class="auto-style32">
                            <div class="auto-style33">
                                <div class="nonboxy-widget">
                                    <div class="breadcrumb">
                                        <div id="divBotones" runat="server" class="pure-form pure-form-stacked">
                                            <div class="pure-g-r">
                                                <div class="pure-u-1">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 80px">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/iconos/calculo.png" Height="90px" Width="135px" />
                                                            </td>
                                                            <td>
                                                                <h1>Cálculos Mecanica</h1>
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

                                        <div class="widget-box">
                                            <div class="form-horizontal well" style="border: medium double #000000; width: 1030px; height: 2029px; color: #000000;">
                                                <strong>INFORMACIÓN PROYECTO:</strong><table style="width: 100%;">
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Cliente" runat="server" Text="Cliente" Font-Bold="True" Font-Size="10pt" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style4" style="border-style: solid"><strong>IP1</strong></td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                           
                                                            <asp:TextBox ID="txt_01_T02Cliente" runat="server" Width="551px"></asp:TextBox>

                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Cliente" runat="server" Enabled="True" TargetControlID="txt_01_T02Cliente" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02ContratoODS" runat="server" Text="Contrato ODS" Font-Bold="True" Font-Size="10pt" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style4" style="border-style: solid"><strong>IP2</strong></td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                            <asp:TextBox ID="txt_01_T02ContratoODS" runat="server" Width="551px"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02ContratoODS" runat="server" Enabled="True" TargetControlID="txt_01_T02ContratoODS" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Fecha" runat="server" Text="Fecha de calculo" Font-Bold="True" Font-Size="10pt" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style4" style="border-style: solid"><strong>IP3</strong></td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                            <asp:TextBox ID="txt_01_T02Fecha" runat="server" Width="551px" Visible="true"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Fecha" runat="server" Enabled="True" TargetControlID="txt_01_T02Fecha" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                           
                                                        
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_01_T02Fecha" Format="dd/MM/yyyy">
                                                            </asp:CalendarExtender>



                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Diseñador" runat="server" Text="Diseñador" Font-Bold="True" Font-Size="10pt" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style4" style="border-style: solid"><strong>IP4</strong></td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                            <asp:TextBox ID="txt_01_T02Diseñador" runat="server" Width="551px"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Diseñador" runat="server" Enabled="True" TargetControlID="txt_01_T02Diseñador" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>

                                                        </td>
                                                    </tr>
                                                    <caption>
                                                        <tr>
                                                            <td class="auto-style1" style="border-style: solid">
                                                                <asp:Label ID="lbl_01_T02Localizacionr" runat="server" Font-Bold="True" Font-Size="10pt" Text="Localizacion de proyectos" Visible="true"></asp:Label>
                                                            </td>
                                                            <td class="auto-style4" style="border-style: solid"><strong>IP5</strong></td>
                                                            <td class="auto-style3" style="border-style: solid">
                                                                <asp:TextBox ID="txt_01_T02Localizacionr" runat="server" Width="551px"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="filtered_01_T02Localizacion" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T02Localizacionr" ValidChars="'">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </caption>
                                                </table>
                                                <strong>INFORMACIÓN DE LA TRAMPA:</strong><table style="">
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02TAG" runat="server" Font-Bold="True" Font-Size="10pt" Text="TAG" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style7" style="border-style: solid">TAG</td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                            <asp:TextBox ID="txt_01_T02TAG" runat="server" Width="551px"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02TAG" runat="server" Enabled="True" TargetControlID="txt_01_T02TAG" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Tipotrampa" runat="server" Font-Bold="True" Font-Size="10pt" Text="Tipo trampa" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style7" style="border-style: solid">TIPO</td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                            <asp:DropDownList ID="drop_01_T02Tipotrampa" runat="server" Width="558px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Tamañotrampa" runat="server" Font-Bold="True" Font-Size="10pt" Text="Tamaño trampa" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style4" style="border-style: solid">T<span class="auto-style5">AM</span></td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                            <asp:DropDownList ID="drop_01_T02Tamañotrampa" runat="server" Width="558px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                         <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Rating" runat="server" Font-Bold="True" Font-Size="10pt" Text="Rating" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style4" style="border-style: solid">R<span class="auto-style5">AT</span></td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                            <asp:DropDownList ID="drop_01_T02Rating" runat="server" Width="558px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Material" runat="server" Font-Bold="True" Font-Size="10pt" Text="Material" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style4" style="border-style: solid">M<span class="auto-style5">AT</span></td>
                                                        <td class="auto-style3" style="border-style: solid">
                                                            <asp:DropDownList ID="drop_01_T02Material" runat="server" Width="558px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <strong>DATOS DE DISEÑO:</strong><table class="input-xxlargetotal" style="border-style: solid; text-decoration: none;">
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:Label ID="lbl_01_T02CODdiseño" runat="server" Font-Bold="True" Font-Size="10pt" Text="CODdiseño" Visible="true"></asp:Label>
                                                        </strong></td>
                                                        <strong>
                                                            <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Cod</strong></td>
                                                            <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                                <asp:TextBox ID="txt_01_T02CODdiseño" runat="server"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="filtered_01_T02CODdiseño" runat="server" Enabled="True" TargetControlID="txt_01_T02CODdiseño" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                            </strong></td>
                                                            <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong></strong></td>
                                                            <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                        </strong>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Temperatura" runat="server" Font-Bold="True" Font-Size="10pt" Text="Temperatura" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Td</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Temperatura" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Temperatura" runat="server" Enabled="True" TargetControlID="txt_01_T02Temperatura" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>°F</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong></strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:Label ID="lbl_01_T02FactorDiseño" runat="server" Font-Bold="True" Font-Size="10pt" Text="Factor Diseño" Visible="true"></asp:Label>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>F</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong><strong></strong><strong>
                                                            <asp:TextBox ID="txt_01_T02FactorDiseño" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02FactorDiseño" runat="server" Enabled="True" TargetControlID="txt_01_T02FactorDiseño" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong></strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:Label ID="lbl_01_T02SobreCorrosion" runat="server" Font-Bold="True" Font-Size="10pt" Text="Sobreespesor Corrosión" Visible="true"></asp:Label>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>CA</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02SobreCorrosion" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02SobreCorrosion" runat="server" Enabled="True" TargetControlID="txt_01_T02SobreCorrosion" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>in</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:Label ID="lbl_01_T02EficienciaJunta" runat="server" Font-Bold="True" Font-Size="10pt" Text="Eficiencia Junta" Visible="true"></asp:Label>
                                                        </strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;"><strong>E</strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02EficienciaJunta" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02EficienciaJunta" runat="server" Enabled="True" TargetControlID="txt_01_T02EficienciaJunta" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style10" style="border-style: solid; text-decoration: none; color: #000000;"></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:Label ID="lbl_01_T02EespesorbarrilMayor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Espesor barril mayor seleccionado" Visible="true"></asp:Label>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>tn1</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02EespesorbarrilMayor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02EespesorbarrilMayor" runat="server" Enabled="True" TargetControlID="txt_01_T02EespesorbarrilMayor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>in</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:Label ID="lbl_01_T02EespesorbarrilMenor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Espesor barril menor seleccionado" Visible="true"></asp:Label>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>tn2</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02EespesorbarrilMenor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02EespesorbarrilMenor" runat="server" Enabled="True" TargetControlID="txt_01_T02EespesorbarrilMenor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>in </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>

                                                </table>
                                                <strong>PROPIEDADES DEL MATERIAL API 5L X56 PSL1:</strong><table class="input-xxlargetotal" style="border-style: solid; text-decoration: none;">
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Coeficienteexp" runat="server" Font-Bold="True" Font-Size="10pt" Text="Coeficiente de expacion termica" Visible="true"></asp:Label>
                                                        </td>
                                                        <strong>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>α</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Coeficienteexp" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Coeficienteexp" runat="server" Enabled="True" TargetControlID="txt_01_T02Coeficienteexp" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                            </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>in/in/°F</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                        </strong>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02Elasticidad" runat="server" Font-Bold="True" Font-Size="10pt" Text="Elasticidad" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Ey</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Elasticidad" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Elasticidad" runat="server" Enabled="True" TargetControlID="txt_01_T02Elasticidad" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                            
                                                            </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>ksi</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong></strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02RelacionPoisson" runat="server" Font-Bold="True" Font-Size="10pt" Text="Relacion de poisson" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>v</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02RelacionPoisson" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02RelacionPoisson" runat="server" Enabled="True" TargetControlID="txt_01_T02RelacionPoisson" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong></strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02resisfluencia" runat="server" Font-Bold="True" Font-Size="10pt" Text="Minima resistencia de fluencia" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Sy</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02resisfluencia" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02resisfluencia" runat="server" Enabled="True" TargetControlID="txt_01_T02resisfluencia" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>psi</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong></strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style1" style="border-style: solid">
                                                            <asp:Label ID="lbl_01_T02resistension" runat="server" Font-Bold="True" Font-Size="10pt" Text="Minima resistencia de tension" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style16" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Sut</strong></td>
                                                        <td class="auto-style16" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02resistension" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02resistension" runat="server" Enabled="True" TargetControlID="txt_01_T02resistension" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style16" style="border-style: solid; text-decoration: none; color: #000000;"><strong>psi</strong></td>
                                                        <td class="auto-style16" style="border-style: solid; text-decoration: none; color: #000000;"><strong></strong></td>
                                                    </tr>
                                                </table>
                                                <strong>PRECALCULOS:<table class="auto-style14" style="border-style: solid; color: #808080; text-decoration: none;">
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02presion" runat="server" Font-Bold="True" Font-Size="10pt" Text="Presion de diseño" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">Pi</td>
                                                        <td class="auto-style23" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02presion" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02presion" runat="server" Enabled="True" TargetControlID="txt_01_T02presion" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">psig</td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02diametrolinea" runat="server" Font-Bold="True" Font-Size="10pt" Text="Diametro linea" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">NPS1</td>
                                                        <td class="auto-style23" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02diametrolinea" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02diametrolinea" runat="server" Enabled="True" TargetControlID="txt_01_T02diametrolinea" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02diametrobarril" runat="server" Font-Bold="True" Font-Size="10pt" Text="Diametro barril" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">NPS2</td>
                                                        <td class="auto-style23" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02diametrobarril" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02diametrobarril" runat="server" Enabled="True" TargetControlID="txt_01_T02diametrolinea" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style12" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02diametropateo" runat="server" Font-Bold="True" Font-Size="10pt" Text="Diametro linea de pateo" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">NPS3</td>
                                                        <td class="auto-style23" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02diametropateo" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02diametropateo" runat="server" Enabled="True" TargetControlID="txt_01_T02diametropateo" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02diametroexternoBmayor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Diametro externo barril mayor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">D1</td>
                                                        <td class="auto-style24" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02diametroexternoBmayor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02diametroexternoBmayor" runat="server" Enabled="True" TargetControlID="txt_01_T02diametroexternoBmayor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02diametroexternoBmenor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Diametro externo barril menor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">D2</td>
                                                        <td class="auto-style24" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02diametroexternoBmenor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02diametroexternoBmenor" runat="server" Enabled="True" TargetControlID="txt_01_T02diametropateo" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02esfuerzomaterial" runat="server" Font-Bold="True" Font-Size="10pt" Text="Esfuerzo permisible material" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">S</td>
                                                        <td class="auto-style24" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02esfuerzomaterial" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02esfuerzomaterial" runat="server" Enabled="True" TargetControlID="txt_01_T02esfuerzomaterial" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">psi</td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                </table>
                                                INTERPOLACION DE PRESION DE LA TEMPERATURA DE TextBox22:
                                                <br />
                                                <table class="auto-style14" style="border-style: solid; color: #808080; text-decoration: none;">
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02TempInterpolar1" runat="server" Font-Bold="True" Font-Size="10pt" Text="Temperatura interpolar 1" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">T1</td>
                                                        <td class="auto-style19" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02TempInterpolar1" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02TempInterpolar1" runat="server" Enabled="True" TargetControlID="txt_01_T02TempInterpolar1" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">psig</td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02TempInterpolar2" runat="server" Font-Bold="True" Font-Size="10pt" Text="Temperatura interpolar 2" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">T2</td>
                                                        <td class="auto-style19" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02TempInterpolar2" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02TempInterpolar2" runat="server" Enabled="True" TargetControlID="txt_01_T02TempInterpolar2" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Presioninterpolar1" runat="server" Font-Bold="True" Font-Size="10pt" Text="Presion para interpolar 1" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">P1</td>
                                                        <td class="auto-style19" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Presioninterpolar1" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Presioninterpolar1" runat="server" Enabled="True" TargetControlID="txt_01_T02Presioninterpolar1" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Presioninterpolar2" runat="server" Font-Bold="True" Font-Size="10pt" Text="Presion para interpolar 2" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">P2</td>
                                                        <td class="auto-style19" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Presioninterpolar2" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Presioninterpolar2" runat="server" Enabled="True" TargetControlID="txt_01_T02Presioninterpolar2" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02PresioninterpoladaFinal" runat="server" Font-Bold="True" Font-Size="10pt" Text="Presion interpolada a:" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">Px</td>
                                                        <td class="auto-style20" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02PresioninterpoladaFinal" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02PresioninterpoladaFinal" runat="server" Enabled="True" TargetControlID="txt_01_T02PresioninterpoladaFinal" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                </table>
                                                CALCULO DE ESPESOR:<table class="auto-style14" style="border-style: solid; color: #808080; text-decoration: none;">
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Espesordiseñobarrilmayor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Espesor diseño por presión barril mayor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">T1</td>
                                                        <td class="auto-style25" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Espesordiseñobarrilmayor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Espesordiseñobarrilmayor" runat="server" Enabled="True" TargetControlID="txt_01_T02Espesordiseñobarrilmayor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style17" style="border-style: solid; ">In</td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Espesordiseñobarrilmenor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Espesor diseño por presión barril menor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">T2</td>
                                                        <td class="auto-style25" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Espesordiseñobarrilmenor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Espesordiseñobarrilmenor" runat="server" Enabled="True" TargetControlID="txt_01_T02Espesordiseñobarrilmenor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterMode="InvalidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txt_01_T02Presioninterpolar1" ValidChars="'">
                                                            </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Espesorminimotoleranciamayor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Espesor mínimo con tolerancias mayor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">TN1&gt;</td>
                                                        <td class="auto-style25" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Espesorminimotoleranciamayor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Espesorminimotoleranciamayor" runat="server" Enabled="True" TargetControlID="txt_01_T02Espesorminimotoleranciamayor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Espesorminimotoleranciamenor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Espesor mínimo con tolerancias menor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">TN2&gt;</td>
                                                        <td class="auto-style25" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Espesorminimotoleranciamenor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Espesorminimotoleranciamenor" runat="server" Enabled="True" TargetControlID="txt_01_T02Espesorminimotoleranciamenor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;"><strong>In</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Chequeobarrilmayor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Chequeo D/t barril mayor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                        <td class="auto-style26" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Chequeobarrilmayor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Chequeobarrilmayor" runat="server" Enabled="True" TargetControlID="txt_01_T02Chequeobarrilmayor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Chequeobarrilmenor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Chequeo D/t barril menor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                        <td class="auto-style26" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Chequeobarrilmenor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Chequeobarrilmenor" runat="server" Enabled="True" TargetControlID="txt_01_T02Chequeobarrilmenor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                </table>
                                                CALCULO DE PRESION DE PRUEBA DE MTU-ES-08_V1 ( TAMBIEN APLICA MTU-ES-08_V1):<table class="auto-style14" style="border-style: solid; color: #808080; text-decoration: none;">
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Esfuerzopresiondiseño1" runat="server" Font-Bold="True" Font-Size="10pt" Text="Esfuerzo de trabajo a la presión de diseño" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">SH1</td>
                                                        <td class="auto-style25" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Esfuerzopresiondiseño1" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Esfuerzopresiondiseño1" runat="server" Enabled="True" TargetControlID="txt_01_T02Esfuerzopresiondiseño1" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style27" style="border-style: solid; ">Psi</td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Esfuerzopresiondiseño2" runat="server" Font-Bold="True" Font-Size="10pt" Text="Esfuerzo de trabajo a la presión de diseño" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">SH2</td>
                                                        <td class="auto-style25" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Esfuerzopresiondiseño2" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Esfuerzopresiondiseño2" runat="server" Enabled="True" TargetControlID="txt_01_T02Esfuerzopresiondiseño2" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style28" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Psi</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Presionprueba" runat="server" Font-Bold="True" Font-Size="10pt" Text="Presión de prueba" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">Pt2</td>
                                                        <td class="auto-style25" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Presionprueba" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Presionprueba" runat="server" Enabled="True" TargetControlID="txt_01_T02Presionprueba" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style28" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Psi</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Duracionprueba" runat="server" Font-Bold="True" Font-Size="10pt" Text="Duración de la prueba" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style17" style="border-style: solid; ">tt</td>
                                                        <td class="auto-style25" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Duracionprueba" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Duracionprueba" runat="server" Enabled="True" TargetControlID="txt_01_T02Duracionprueba" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style28" style="border-style: solid; text-decoration: none; color: #000000;"><strong>h</strong></td>
                                                        <td class="jqplot-error" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Esfuerzocircunferencialbarrilmayor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Esfuerzo circunferencial de prueba barril mayor" Visible="true"></asp:Label>
                                                        </td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">SH1t</td>
                                                        <td class="auto-style26" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Esfuerzocircunferencialbarrilmayor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Esfuerzocircunferencialbarrilmayor" runat="server" Enabled="True" TargetControlID="txt_01_T02Esfuerzocircunferencialbarrilmayor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style29" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Psi</strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style9" style="border-style: solid; text-decoration: none; color: #000000;">
                                                            <asp:Label ID="lbl_01_T02Esfuerzocircunferencialbarrilmenor" runat="server" Font-Bold="True" Font-Size="10pt" Text="Esfuerzo circunferencial de prueba barril menor" Visible="true"></asp:Label>
                                                            </td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">SHt2</td>
                                                        <td class="auto-style26" style="border-style: solid; text-decoration: none; color: #000000;"><strong>
                                                            <asp:TextBox ID="txt_01_T02Esfuerzocircunferencialbarrilmenor" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="filtered_01_T02Esfuerzocircunferencialbarrilmenor" runat="server" Enabled="True" TargetControlID="txt_01_T02Esfuerzocircunferencialbarrilmenor" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" FilterMode="InvalidChars" ValidChars="'"></asp:FilteredTextBoxExtender>
                                                        </strong></td>
                                                        <td class="auto-style29" style="border-style: solid; text-decoration: none; color: #000000;"><strong>Psi</strong></td>
                                                        <td class="auto-style11" style="border-style: solid; text-decoration: none; color: #000000;">&nbsp;</td>
                                                    </tr>
                                                </table>
                                                   
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>


                            <div class="widget-head" style="height: 64px">
                                <h3>REGISTROS GUARDADOS</h3>
                                <p>
                                    &nbsp;</p>
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
                                    &nbsp;<asp:TextBox ID="txtprueba" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Style="min-width: 250px;">

                    <table style="padding: 5px; border: thin solid #486794; background-color: #E8B617; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%">
                        <thead>
                            <tr>

                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Cliente</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">ContratoODS</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Fecha</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Diseñador</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Localizacion</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">TAG</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Tipotrampa</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Tamañotrampa</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Rating</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Material</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">CODdiseño</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Temperatura</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">FactorDiseño</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">SobreCorrosion</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">EficienciaJunta</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">EespesorbarrilMayor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">EespesorbarrilMenor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Coeficienteexp</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Elasticidad</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">RelacionPoisson</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">resisfluencia</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">resistension</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">presion</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">diametrolinea</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">diametrobarril</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">diametropateo</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">diametroexternoBmayor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">diametroexternoBmenor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">esfuerzomaterial</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">TempInterpolar1</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">TempInterpolar2</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Presioninterpolar1</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Presioninterpolar2</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">PresioninterpoladaFinal</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Espesordiseñobarrilmayor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Espesordiseñobarrilmenor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Espesorminimotoleranciamayor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Espesorminimotoleranciamenor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Chequeobarrilmenor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Chequeobarrilmayor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Esfuerzopresiondiseño1</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Esfuerzopresiondiseño2</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Presionprueba</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Duracionprueba</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Esfuerzocircunferencialbarrilmayor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">Esfuerzocircunferencialbarrilmenor</th>
                                <th style="padding: 5px; border: thin solid #FFFFFF; background-color: #486794; font-weight: bold; color: #FFFFFF; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: center;">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </th>
                            </tr>
                        </thead>
                        <tbody runat="server" id="bodytabla" style="padding: 5px; border: thin solid #486794; background-color: white; color: #486794; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; vertical-align: middle; text-align: left; width: 100%; font-weight: normal; font-size: 11px;">
                        </tbody>
                    </table>
                </asp:Panel>

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

<%--                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
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
