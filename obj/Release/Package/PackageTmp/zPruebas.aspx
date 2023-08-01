<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="zPruebas.aspx.vb" Inherits="HSENUEVO.zPruebas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 400px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <table class="auto-style1">
                <tr>
                    <td colspan="2" style="border: thin solid #000080">DATOS BASICOS HIJO</td>
                </tr>
                <tr>
                    <td style="border: thin solid #000080">Tipo Documento</td>
                    <td style="border: thin solid #000080">
                        <asp:DropDownList ID="drop_TipoDoc" runat="server" Width="250px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="border: thin solid #000080">Documento</td>
                    <td style="border: thin solid #000080">
                        <asp:TextBox ID="txtDocumento" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border: thin solid #000080">Nombre</td>
                    <td style="border: thin solid #000080">
                        <asp:TextBox ID="txtNombre" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border: thin solid #000080">Apellido</td>
                    <td style="border: thin solid #000080">
                        <asp:TextBox ID="txtApellido" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="border: thin solid #000080">Activo</td>
                    <td style="border: thin solid #000080">
                        <asp:DropDownList ID="drop_Activo" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="border: thin solid #000080" colspan="2">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="#CC0000" Width="100%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border: thin solid #000080" colspan="2">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />
                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" />
                    </td>
                </tr>
                <tr>
                    <td style="border: thin solid #000080" colspan="2">
                        <table class="data-tbl-simple table table-bordered" style="border: thin solid #C0C0C0; width: 100%;">
                            <thead>
                                <tr>
                                    <th>TipoDocumento
                                    </th>
                                    <th>Documento
                                    </th>

                                    <th>Nombre
                                    </th>
                                    <th>-apellido
                                    </th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="bodytabla">
                            </tbody>
                        </table>

                    </td>
                </tr>
            </table>


        </div>
    </form>
</body>
</html>
