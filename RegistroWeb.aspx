<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegistroWeb.aspx.vb" Inherits="REDOC.RegistroWebb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Quicksand:wght@400;600&display=swap" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-iYQeCzEYFbKjA/T2uDLTpkwGzCiq6soy8tYaI1GyVh/UjpbCx/TYkiZhlZB6+fzT" crossorigin="anonymous" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/styleWeb.css" />

    <title>Nuevos Ingresos</title>

</head>
<body>
    <div class="barra">
        <h1>REGISTRO DE DOCUMENTOS</h1>
        <a href="https://www.seringtec.com/">
            <img src="public/logo.png" /></a>
    </div>
    <div class="formulario">
        <form id="form1" runat="server">
            <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1">
                <ControlBundles>
                    <ajaxToolkit:ControlBundle Name="Accordion" />
                </ControlBundles>
            </ajaxToolkit:ToolkitScriptManager>
            <%--    <div class="demoarea">
        <div class="demoheading">Accordion Demonstration</div>--%>

            <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0"
                HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40"
                TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                <Panes>
                    <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                        <Header>DATOS BÁSICOS DE LA PERSONA A CONTRATAR</Header>
                        <Content>
                            <table style="border: thin solid #FFFFFF; width: 100%;">
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066; text-align: center; font-weight: bold;">Seleccione su tipo de documento:</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center; font-weight: bold;">Ingrese su documento de identidad:</td>

                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">
                                        <asp:DropDownList ID="drop_02_T01TipoDocidentidad" Enabled ="false"  runat="server" class="form-select form-select-sm" Width="90%" placeholder="3_T01TipoDocidentidad"></asp:DropDownList></td>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">
                                        <asp:TextBox ID="txt_02_T01NumDoc" runat="server"  Enabled ="false"  Width="90%" class="form-control" MaxLength="30" placeholder="Documento Identidad"></asp:TextBox></asp:DropDownList>    </td>

                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066; text-align: center; font-weight: bold;">Ingrese su nombre completo:</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center; font-weight: bold;">Correo Electr&oacutenico:</td>

                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">
                                        <asp:TextBox ID="txt_02_T01Nombres"  Enabled ="false"  runat="server" Width="90%" class="form-control" MaxLength="200" placeholder="Nombre  - Apellido"></asp:TextBox></td>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">
                                        <asp:TextBox type="email" ID="txt_02_T01CorreoPersonal"  Enabled ="false"  runat="server" Width="90%" class="form-control" MaxLength="300" placeholder="name@example.com"></asp:TextBox>
                                        <asp:DropDownList ID="drop_02_T01Activo" runat="server" class="chzn-select" Width="90%" placeholder="3_T01Activo" Visible="false"></asp:DropDownList>
                                        <asp:DropDownList ID="drop_02_T01EstadoProceso" runat="server" class="chzn-select" Width="90%" placeholder="3_T01Activo" Visible="false"></asp:DropDownList>
                                        
                                    </td>

                                </tr>
                                        <asp:Button ID="btn_IniciarProceso" runat="server" Width="100%" Text="Click para iniciar Proceso" Visible ="false"  />


                            </table>

                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                        <Header>DOCUMENTOS PARA CONTRATACI&OacuteN  LABORAL O DE APRENDIZAJE</Header>
                        <Content>
                            <table style="border: thin solid #FFFFFF; width: 100%;">
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Documento  (Formato .PDF)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Archivo</td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Gestion</td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Hoja de vida:</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_Hojadevida" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_HojadeVida" runat="server">Ver Hoja de Vida Cargada</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_HojadeVida" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_HojadeVida" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Soportes académicos de títulos obtenidos</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_SoporteAcademicos" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_SoporteAcademicos" runat="server">Ver Soportes académicos Cargados</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_SoporteAcademicos" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_SoporteAcademicos" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />

                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Soportes de educaci&oacuten continuada (Cursos, talleres, diplomados, etc.)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_SoporteEduContinua" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_SoporteEduContinua" runat="server">Ver Soportes de educaci&oacuten continuada</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_SoporteEduContinua" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_SoporteEduContinua" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Certificaciones laborales</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_CertificacionesLaborales" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_CertificacionesLaborales" runat="server">Ver Certificaciones laborales Cargadas</asp:LinkButton>
                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CertificacionesLaborales" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_CertificacionesLaborales" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Fotocopia de la tarjeta profesional, si aplica para el cargo</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_FotocopiaTarjetaProfesional" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_FotocopiaTarjetaProfesional" runat="server">Ver Fotocopia de la tarjeta profesional Cargada</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_FotocopiaTarjetaProfesional" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_FotocopiaTarjetaProfesional" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />

                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Fotocopia carnet vacunacion:</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_FotocopiaCarnetVacunacion" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_FotocopiaCarnetVacunacion" runat="server">Ver Fotocopia carnet vacunacion Cargado</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_FotocopiaCarnetVacunacion" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_FotocopiaCarnetVacunacion" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Fotocopia de licencia de conducci&oacuten (En caso de que el cargo requiera conducir)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_FotocopiaLicenciaConduccion" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_FotocopiaLicenciaConduccion" runat="server">Ver licencia de conducci&oacuten Cargada</asp:LinkButton>
                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_FotocopiaLicenciaConduccion" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_FotocopiaLicenciaConduccion" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />

                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Fotocopia de cédula de ciudadanía o tarjeta de identidad (Al 150%)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_FotocopiaDocumentoIdentidad" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_FotocopiaDocumentoIdentidad" runat="server">Ver Fotocopia del Documento Cargado</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_FotocopiaDocumentoIdentidad" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_FotocopiaDocumentoIdentidad" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Fotocopia de certificaci&oacuten bancaria preferiblemente Davivienda (en caso de no tener cuenta de ahorros SERINGTEC S.A.S., entregará la solicitud de apertura de la cuenta)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_CertificacionBancaria" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_CertificacionBancaria" runat="server">Ver certificaci&oacuten bancaria Cargada</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CertificacionBancaria" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_CertificacionBancaria" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />

                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Fotografía 3x4 tipo documento, fondo blanco a color, tomaño de 100KB a 1MB, debe estar nombrada con numero de identificaci&oacuten, sin puntos, ni comas (formato JPEG)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_Fotografia" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_Fotografia" runat="server">Ver Fotografía Cargada</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_Fotografia" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_Fotografia" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />

                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Certificado de afiliaci&oacuten a EPS</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_CertificadoEPS" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_CertificadoEPS" runat="server">Ver Certificado de afiliaci&oacuten a EPS Cargada</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CertificadoEPS" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_CertificadoEPS" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Certificado de afiliaci&oacuten al fondo de pensiones</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_CertificadoPensiones" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_CertificadoPensiones" runat="server">Ver Certificado de afiliaci&oacuten al fondo de pensiones Cargado</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CertificadoPensiones" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_CertificadoPensiones" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />

                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;">Certificado de afiliacion al fondo de cesantias.</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                        <asp:FileUpload ID="fileUp_CertificadoCesantias" runat="server" class="form-control" />
                                        <asp:LinkButton ID="linkVer_CertificadoCesantias" runat="server">Ver Certificado de afiliacion al fondo de cesantias Cargado</asp:LinkButton>

                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CertificadoCesantias" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                        <asp:ImageButton ID="btnEliminar_CertificadoCesantias" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />

                                    </td>
                                </tr>






                            </table>

                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server">
                        <Header>FORMATOS ESPECIFICOS A DILIGENCIAR</Header>
                        <Content>
                            <table style="border: thin solid #FFFFFF; width: 100%;">
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Documento (Formato .PDF y Excel)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Archivo</td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Gestion</td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>SER-GA-FO-038</b> Formato de vinculaci&oacuten de personal.</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_SERGAFO038" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_SERGAFO038" runat="server">Ver SER-GA-FO-038</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_SERGAFO038" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_SERGAFO038" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>GAB-F-214</b> Formato Certificaci&oacuten cumplimiento de perfiles (Es importante adjuntar todas las certificaciones laborales).</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_GABF214" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_GABF214" runat="server">Ver GAB-F-214</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_GABF214" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_GABF214" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>Prueba</b> Psicotecnica.</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_PruebaPsicotecnica" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_PruebaPsicotecnica" runat="server">Ver Prueba Psicotecnica</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_PruebaPsicotecnica" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_PruebaPsicotecnica" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>


                            </table>

                        </Content>
                    </ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server">
                        <Header>DOCUMENTOS PARA AFILIACI&OacuteN A E.P.S.</Header>
                        <Content>
                            <p><b>CERTIFICADO DE AFILIACI&OacuteN A EPS</b><br />.
Sí el trabajador a contratar, requiere afiliar beneficiarios, deberá presentar la siguiente 
documentaci&oacuten, en cada caso particular</p>
                            <table style="border: thin solid #FFFFFF; width: 100%;">

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Documento  (Formato .PDF)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Archivo</td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Gestion</td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><B>Hijos menores de 7 años</B> Copia del registro civil de nacimiento. </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_RCNACMenor7" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_RCNACMenor7" runat="server">Ver registro civil de nacimiento</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_RCNACMenor7" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_RCNACMenor7" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><B>Hijos entre 7 y 18 años </B> Copia del registro civil de nacimiento. Copia de la tarjeta de identidad, ampliada al 150%. </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_RCTINinos7y18" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_RCTINinos7y18" runat="server">Ver tarjeta de identidad</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_RCTINinos7y18" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_RCTINinos7y18" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>


                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><B>Hijos estudiantes entre 18 y 25 años </B>Copia del registro civil de nacimiento. Copia del documento de identidad. Certificaci&oacuten original del establecimiento educativo en la cual conste escolaridad, periodo y dedicaci&oacuten académica.</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCNinos18y25" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCNinos18y25" runat="server">Ver Documento de identidad</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCNinos18y25" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCNinos18y25" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>


                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><B>Hijos mayores de 18 años</B> con incapacidad permanente Copia del registro civil de nacimiento. Copia del documento de identidad. Certificaci&oacuten expedida por el médico tratante y convalidado por la EPS donde determine el tipo y el grado de incapacidad.  </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCNinos18Incap" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCNinos18Incap" runat="server">Ver Documento de identidad</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCNinos18Incap" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCNinos18Incap" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>


                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><B>Conyugue o compañero permanente </B>Copia registro civil de matrimonio. Declaraci&oacuten juramentada (formato de la EPS). Copia del documento de identidad del conyugue, ampliada al 150%. </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCConyugue" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCConyugue" runat="server">Ver Documento del Conyugue</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCConyugue" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCConyugue" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>


                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><B>Padres</B> Copia registro civil de nacimiento del cotizante. Copia del documento de identidad de los padres, ampliada al 150%.</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCPadres" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCPadres" runat="server">Ver Documento Padres</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCPadres" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCPadres" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>

                             </table>



                        </Content>
</ajaxToolkit:AccordionPane>
                    <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server">
                        <Header>DOCUMENTOS PARA AFILIACI&oacuteN A CAJA DE COMPENSACI&OacuteN FAMILIAR </Header>
                        <Content>
                            <table style="border: thin solid #FFFFFF; width: 100%;">

                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Documento (Formato .PDF)</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Archivo</td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #FFFFFF; text-align: center; background-color: #C0C0C0; font-weight: bold;">Gestion</td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>Hijos menores de 7 años </b>Copia del registro civil de nacimiento. Certificaci&oacuten laboral del (padre/madre) en el cual conste si recibe o no cuota monetaria y el valor de su salario mensual. Si no labora (padre/madre) debe diligenciar la declaraci&oacuten juramentada (Formato CCF).</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCFMenor7" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCFMenor7" runat="server">Ver Documento</asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCFMenor7" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCFMenor7" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>Hijos entre 7 y 12 años </b>Copia del registro civil de nacimiento. Copia de la tarjeta de identidad, ampliada al 150%. Certificaci&oacuten laboral del (padre/madre) en el cual conste si recibe o no cuota monetaria y el valor de su salario mensual. Si no labora (padre/madre) debe diligenciar la declaraci&oacuten juramentada (Formato CCF).</td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCFMenor7y12" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCFMenor7y12" runat="server">Ver Documento </asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCFMenor7y12" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCFMenor7y12" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>Hijos estudiantes entre 12 y 18 años </b>Copia del registro civil de nacimiento. Copia del documento de identidad. Certificaci&oacuten original del establecimiento educativo en la cual conste escolaridad, periodo y dedicaci&oacuten académica. Certificaci&oacuten laboral del (padre/madre) en el cual conste si recibe o no cuota monetaria y el valor de su salario mensual. Si no labora (padre/madre) debe diligenciar la declaraci&oacuten juramentada (Formato CCF). </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCFhijo12y18" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCFhijo12y18" runat="server">Ver Documento </asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCFhijo12y18" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCFhijo12y18" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>Padres (Mayores de 60 años)</b> Copia registro civil de nacimiento del afiliado Copia del documento de identidad de los padres, ampliada al 150% Declaraci&oacuten juramentada (Formato CCF). </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCFPadres" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCFPadres" runat="server">Ver Documento </asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCFPadres" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCFPadres" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>Hijos mayores de 18 años con incapacidad permanente</b> Copia del registro civil de nacimiento. Copia del documento de identidad. Certificaci&oacuten expedida por el médico tratante y convalidado por la EPS donde determine el tipo y el grado de incapacidad. Certificaci&oacuten laboral del (padre/madre) en el cual conste si recibe o no cuota monetaria y el valor de su salario mensual. Si no labora (padre/madre) debe diligenciar la declaraci&oacuten juramentada (Formato CCF). </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCFHijos18Incapa" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCFHijos18Incapa" runat="server">Ver Documento </asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCFHijos18Incapa" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCFHijos18Incapa" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>Conyugue o compañero permanente</b> Copia registro civil de matrimonio. Declaraci&oacuten juramentada (Formato CCF). Copia del documento de identidad del conyugue, ampliada al 150%. </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCFConyuge" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCFConyuge" runat="server">Ver Documento </asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCFConyuge" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCFConyuge" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 2px solid #FFFFFF; width: 300px; color: #000066;"><b>Hijos menores de 7 años</b> Copia del registro civil de nacimiento. Certificaci&oacuten laboral del (padre/madre) en el cual conste si recibe o no cuota monetaria y el valor de su salario mensual. Si no labora (padre/madre) debe diligenciar la declaraci&oacuten juramentada (Formato CCF). </td>
                                    <td style="border: 2px solid #FFFFFF; width: 250px; color: #000066; text-align: center;">
                                          <asp:FileUpload ID="fileUp_CCFMenor" runat="server" class="form-control" />
                                         <asp:LinkButton ID="linkVer_CCFMenor" runat="server">Ver Documento </asp:LinkButton>


                                    </td>
                                    <td style="border: 2px solid #FFFFFF; width: 80px; color: #000066; text-align: center;">
                                        <asp:ImageButton ID="btnCargar_CCFMenor" runat="server" ImageUrl="~/public/btnCargar_Archivo.png" />
                                      <asp:ImageButton ID="btnEliminar_CCFMenor" runat="server" ImageUrl="~/public/btnEliminar_Archivo.png" />
   
                                    </td>
                                </tr>



                            </table>



                        </Content>

                    </ajaxToolkit:AccordionPane>
                </Panes>
            </ajaxToolkit:Accordion>

            <asp:Literal ID="Lit_Mensaje" runat="server"></asp:Literal>
            <%--<asp:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server" TargetControlID="Lit_Mensaje" VerticalSide="Bottom" VerticalOffset="20" HorizontalSide="Right" HorizontalOffset="90" ScrollEffectDuration=".1" />--%>
            <asp:Button ID="botonEnviar" class="botonEnviar" runat="server" Text="TERMINAR y ENVIAR" />


        </form>
    </div>


    <div class="footer">
        <footer>

            <p>@ 2022 Seringtec s.a.s, Todos los derechos reservados </p>
        </footer>
    </div>
    <script src="../js/main.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-u1OknCvxWvY5kfmNBILK2hRnQC3Pr17a+RTT6rIHI7NnikvbZlHgTPOOmMi466C8" crossorigin="anonymous"></script>
</body>
</html>
