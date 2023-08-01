<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ZZZ.aspx.vb" Inherits="HSENUEVO.ZZZ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div>

                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="10pt" Text="Rango Fechas"></asp:Label>
                                <asp:TextBox ID="txtfiltro_FechaInicial" runat="server" Width="120px" MaxLength="30" Visible="true"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtfiltro_FechaInicial" FilterType="Numbers,Custom" ValidChars="/"></asp:FilteredTextBoxExtender>

                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfiltro_FechaInicial" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>


        </div>
    </form>
</body>
</html>
