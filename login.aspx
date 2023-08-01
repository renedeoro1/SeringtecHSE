<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="HSENUEVO.login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Seringtec</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Admin Panel Template" />
    <meta name="author" content="Westilian: Kamrujaman Shohel" />
    <!-- styles -->
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/jquery-ui-1.8.16.custom.css" rel="stylesheet" />
    <link href="css/jquery.jqplot.css" rel="stylesheet" />
    <link href="css/prettify.css" rel="stylesheet" />
    <link href="css/elfinder.min.css" rel="stylesheet" />
    <link href="css/elfinder.theme.css" rel="stylesheet" />
    <link href="css/fullcalendar.css" rel="stylesheet" />
    <link href="js/plupupload/jquery.plupload.queue/css/jquery.plupload.queue.css" rel="stylesheet" />
    <link href="css/styles.css" rel="stylesheet" />
    <link href="css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/icons-sprite.css" rel="stylesheet" />
    <link id="themes" href="css/themes.css" rel="stylesheet" />
    <!--fav and touch icons -->
    <link rel="shortcut icon" href="iconos/favicon.ico" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="iconos/apple-touch-icon-144-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="iconos/apple-touch-icon-114-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="iconos/apple-touch-icon-72-precomposed.png" />
    <link rel="apple-touch-icon-precomposed" href="iconos/apple-touch-icon-57-precomposed.png" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="navbar navbar-fixed-top">
            <div class="navbar-inner-Azul">
                <div class="container-fluid">
                    <div class="branding">
                        <div class="logoGrande" style="height: 110px">
                            <a href="default.aspx">
                                <img src="iconos/tituloHSE.png" alt="Logo" /></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="height: 27px"></div>
        <div id="main-content" class="full-fluid2">
            <div class="container-fluid">
                <div class="breadcrumb">
                    <table class="input-xxlargetotal">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label1" runat="server" Text="Por Favor Ingrese Datos de Identificaciòn" Font-Bold="True" Font-Size="15pt" ForeColor="#333333"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="login-container">
                            <div class="well-login">
                                <div class="control-group">
                                    <div class="controls">
                                        <div style="text-align: center">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="iconos/logoHSG.png" Height="100PX" />
                                        </div>
                                    </div>
                                </div>

                                <div class="control-group">
                                    <div class="controls">
                                        <div>
                                            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Red"></asp:Label>
                                            <asp:TextBox ID="txt_90_T06NombreUsuario" runat="server" placeholder="N° Documento" class="login-input user-name" autocomplete="off"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="txt_90_T06NombreUsuarioFilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_90_T06NombreUsuario"></asp:FilteredTextBoxExtender>
                                            <asp:TextBox ID="txt_90_T06Clave" runat="server" placeholder="Constraseña" class="login-input user-pass" TextMode="Password"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="txt_90_T06ClaveFilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers,LowercaseLetters,UppercaseLetters" TargetControlID="txt_90_T06Clave"></asp:FilteredTextBoxExtender>

                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix">
                                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar" class="btn btn-inverse login-btnGrisOscuro" />
                                </div>



                            </div>
                        </div>




                    </ContentTemplate>
                    <Triggers>

                        <asp:PostBackTrigger ControlID="btnlogin" />
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
