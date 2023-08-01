Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI.WebControls
Imports Ionic.Zip

Public Class RegistroWebb
    Inherits System.Web.UI.Page

    Dim clsAdminDb As New adminitradorDB

    Private des As New TripleDESCryptoServiceProvider 'Algorithmo TripleDES
    Private hashmd5 As New MD5CryptoServiceProvider 'objeto md5
    Private myKey As String = "MyKey2012" 'Clave secreta(puede alterarse)


    Dim sCodModulo As String
    Dim sNombreTabla As String
    Dim sCamposTabla As String
    Dim sCamposINS As String
    Dim sCamposUPD As String
    Dim sLlaves As String

    Dim sContenido_TEm As String
    Dim sTipoEmail As String
    Dim sNumDocRecurso As String
    Dim sNumProceso As String
    Dim sNumProcesoOK As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Datos_Modulo()
            txt_02_T01NumDoc.AutoPostBack = True
            CargarDrops()
            Estado_Controles()

        End If
        Try
            If Not IsNothing(Request.QueryString("CodTmpOK")) Then
                sContenido_TEm = Request.QueryString("CodTmpOK").ToString
                sContenido_TEm = Replace(sContenido_TEm, " ", "+")
                sContenido_TEm = Desencriptar(sContenido_TEm)
                Dim TestArray() As String = Split(sContenido_TEm, "-00-")
                sTipoEmail = TestArray(0).ToString
                sNumDocRecurso = TestArray(1).ToString
                sNumProceso = TestArray(2).ToString
                sNumProcesoOK = TestArray(3).ToString
                txt_02_T01NumDoc.Text = sNumDocRecurso
                Cargar_Registro_Recurso()
            End If
        Catch ex As Exception
            txt_02_T01NumDoc.Text = ""
        End Try
        If txt_02_T01NumDoc.Text = "" Then
            btnCargar_HojadeVida.Visible = False
            btnEliminar_HojadeVida.Visible = False


            btnCargar_SoporteAcademicos.Visible = False
            btnEliminar_SoporteAcademicos.Visible = False

            btnCargar_SoporteEduContinua.Visible = False
            btnEliminar_SoporteEduContinua.Visible = False

            btnCargar_CertificacionesLaborales.Visible = False
            btnEliminar_CertificacionesLaborales.Visible = False

            btnCargar_FotocopiaTarjetaProfesional.Visible = False
            btnEliminar_FotocopiaTarjetaProfesional.Visible = False

            btnCargar_FotocopiaCarnetVacunacion.Visible = False
            btnEliminar_FotocopiaCarnetVacunacion.Visible = False

            btnCargar_FotocopiaLicenciaConduccion.Visible = False
            btnEliminar_FotocopiaLicenciaConduccion.Visible = False

            btnCargar_FotocopiaDocumentoIdentidad.Visible = False
            btnEliminar_FotocopiaDocumentoIdentidad.Visible = False

            btnCargar_CertificacionBancaria.Visible = False
            btnEliminar_CertificacionBancaria.Visible = False

            btnCargar_Fotografia.Visible = False
            btnEliminar_Fotografia.Visible = False

            btnCargar_CertificadoEPS.Visible = False
            btnEliminar_CertificadoEPS.Visible = False

            btnCargar_CertificadoPensiones.Visible = False
            btnEliminar_CertificadoPensiones.Visible = False

            btnCargar_CertificadoCesantias.Visible = False
            btnEliminar_CertificadoCesantias.Visible = False

            '22222222222
            btnCargar_SERGAFO038.Visible = False
            btnEliminar_SERGAFO038.Visible = False

            btnCargar_GABF214.Visible = False
            btnEliminar_GABF214.Visible = False

            btnCargar_PruebaPsicotecnica.Visible = False
            btnEliminar_PruebaPsicotecnica.Visible = False

            '33333333
            btnCargar_RCNACMenor7.Visible = False
            btnEliminar_RCNACMenor7.Visible = False

            btnCargar_RCTINinos7y18.Visible = False
            btnEliminar_RCTINinos7y18.Visible = False

            btnCargar_CCNinos18y25.Visible = False
            btnEliminar_CCNinos18y25.Visible = False

            btnCargar_CCNinos18Incap.Visible = False
            btnEliminar_CCNinos18Incap.Visible = False

            btnCargar_CCConyugue.Visible = False
            btnEliminar_CCConyugue.Visible = False

            btnCargar_CCPadres.Visible = False
            btnEliminar_CCPadres.Visible = False

            '44444444
            btnCargar_CCFMenor7.Visible = False
            btnEliminar_CCFMenor7.Visible = False

            btnCargar_CCFMenor7y12.Visible = False
            btnEliminar_CCFMenor7y12.Visible = False

            btnCargar_CCFhijo12y18.Visible = False
            btnEliminar_CCFhijo12y18.Visible = False
            btnCargar_CCFPadres.Visible = False
            btnEliminar_CCFPadres.Visible = False

            btnCargar_CCFHijos18Incapa.Visible = False
            btnEliminar_CCFHijos18Incapa.Visible = False

            btnCargar_CCFConyuge.Visible = False
            btnEliminar_CCFConyuge.Visible = False

            btnCargar_CCFMenor.Visible = False
            btnEliminar_CCFMenor.Visible = False



            btn_IniciarProceso.Visible = False
            botonEnviar.Visible = False

        End If

        Lit_Mensaje.Text = ""
        Lit_Mensaje.Visible = False
        If sNumProcesoOK = "OK1" Then
            Lit_Mensaje.Text += "<div id='visor_Mensajes' class='lblMensaje'>"
            Lit_Mensaje.Text += "SE HA ENVIADO Y TERMINADO CORRECTAMENTE  ESTE PROCESO</div>"
            Lit_Mensaje.Visible = True
        End If


    End Sub

    Private Sub CargarDrops()

        With clsAdminDb
            .drop_Cargar_SINO(drop_02_T01Activo, False)
            drop_02_T01Activo.Text = "SI"

            drop_02_T01TipoDocidentidad.Items.Add(New ListItem("Selecccionar", ""))
            drop_02_T01TipoDocidentidad.Items.Add(New ListItem("Cedula de Ciudadania", "CC"))
            drop_02_T01TipoDocidentidad.Items.Add(New ListItem("Cedula de Extranjeria", "CE"))
            drop_02_T01TipoDocidentidad.Items.Add(New ListItem("Tarjeta de Identidad", "TI"))

            drop_02_T01EstadoProceso.Items.Add(New ListItem("Iniciado", "Iniciado"))
            drop_02_T01EstadoProceso.Items.Add(New ListItem("Recibido", "Recibido"))
            drop_02_T01EstadoProceso.Items.Add(New ListItem("En Gestion", "En Gestion"))
            drop_02_T01EstadoProceso.Items.Add(New ListItem("Gestionado", "Gestionado"))
            drop_02_T01EstadoProceso.Items.Add(New ListItem("Entregado", "Entregado"))
            drop_02_T01EstadoProceso.Items.Add(New ListItem("Admitido", "Admitido"))
            drop_02_T01EstadoProceso.Items.Add(New ListItem("Cerrado", "Cerrado"))



        End With
    End Sub

    Private Function bValidarCampos() As Boolean
        bValidarCampos = False
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(drop_02_T01TipoDocidentidad.Text) = "" Then
            drop_02_T01TipoDocidentidad.Focus()
            Lit_Mensaje.Text += "falta campo: Tipo Documento de identidad </div>"
            Exit Function
        Else

        End If
        If Trim(txt_02_T01NumDoc.Text) = "" Then
            txt_02_T01NumDoc.Focus()
            Lit_Mensaje.Text += "falta campo: N° Documento de identidad </div>"
            Exit Function
        Else

        End If
        If Trim(txt_02_T01Nombres.Text) = "" Then
            txt_02_T01Nombres.Focus()
            Lit_Mensaje.Text += "falta campo: Nombre Completo </div>"
            Exit Function
        Else

        End If
        If Trim(txt_02_T01CorreoPersonal.Text) = "" Then
            txt_02_T01CorreoPersonal.Focus()
            Lit_Mensaje.Text += "falta campo: Email </div>"
            Exit Function
        Else

        End If
        If Trim(drop_02_T01Activo.Text) = "" Then
            drop_02_T01Activo.Focus()
            Lit_Mensaje.Text += "falta campo: Activo </div>"
            Exit Function
        Else

        End If

        bValidarCampos = True
        'Lit_Mensaje.Text = ""
        'Lit_Mensaje.Visible = False
    End Function


    Protected Sub botonEnviar_Click(sender As Object, e As EventArgs) Handles botonEnviar.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim coleccionDatosPlural As New Collection
        Dim sCamposTablaLocal As String = ""
        Dim sLlavesLocal As String = ""
        Dim sTablaLocal As String = ""
        Dim ArregloSingular() As String

        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                If Val(ArregloSingular(4)) <> 1 And ArregloSingular(0) = "0105" Then
                    If Val(ArregloSingular(3)) = 0 Then
                        Lit_Mensaje.Text = ""
                        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
                        Lit_Mensaje.Text += "Faltan Documentos  obligatorios - " & ArregloSingular(1) & "</div>"
                        Exit Sub
                    End If
                Else
                    If Val(ArregloSingular(3)) = 0 Then
                        Lit_Mensaje.Text = ""
                        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
                        Lit_Mensaje.Text += "Faltan Documentos  obligatorios por cargar - " & ArregloSingular(1) & "</div>"
                        Exit Sub
                    End If

                End If

            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                'MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If



        Dim dFecha As Date
        Dim sFecha As String = ""
        Dim iHora As String = Now.Hour
        Dim iMinute As String = Now.Minute

        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
            If Val(iHora) < 10 Then
                iHora = "0" & iHora
            End If
            If Val(iMinute) < 10 Then
                iMinute = "0" & iMinute
            End If

        End If
        Dim bActualizado As Boolean = False
        Dim sNombreTabla_Tem = _02_T01RECURSO.NombreTabla
        Dim sLlaves_Tem = _02_T01RECURSO.CampoLlave_02_T01NumDoc & "=  '" & txt_02_T01NumDoc.Text & "'"
        drop_02_T01EstadoProceso.Text = "Entregado"
        Dim sUpdate_TEm As String = _02_T01RECURSO.Campo_02_T01EstadoProceso & "='" & drop_02_T01EstadoProceso.Text & "'"
        bActualizado = clsAdminDb.sql_Update(sNombreTabla_Tem, sUpdate_TEm, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bActualizado = True Then
            bActualizado = False
            sNombreTabla_Tem = _02_T04PROCESO.NombreTabla
            sLlaves_Tem = _02_T04PROCESO.CampoLlave_02_T04NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "' and " & _02_T04PROCESO.CampoLlave_02_T04Consecutivo & "='" & sNumProceso & "'"
            sUpdate_TEm = _02_T04PROCESO.Campo_02_T04FechaEntregado & "='" & sFecha & "'," & _02_T04PROCESO.Campo_02_T04HoraEntregado & "='" & iHora & ":" & iMinute & "'," & _02_T04PROCESO.Campo_02_T04Estado & "='" & drop_02_T01EstadoProceso.Text & "'"
            bActualizado = clsAdminDb.sql_Update(sNombreTabla_Tem, sUpdate_TEm, sLlaves_Tem)

            Lit_Mensaje.Text += "Se Termino y Envio Correctamente el Proceso</div>"
            Estado_Controles()
            If drop_02_T01EstadoProceso.Text = "Entregado" Then
                Dim coleccionDatos As Object
                coleccionDatos = clsAdminDb.sql_Select(_02_T05CONFIGURACION_EMAIL.NombreTabla, _02_T05CONFIGURACION_EMAIL.Campo_02_T05dirWebEnvio, _02_T05CONFIGURACION_EMAIL.CampoLlave_02_T05TipoEmail & "='02'",,, )
                Dim sNombreRecurso As String = "02" & "-00-" & txt_02_T01NumDoc.Text & "-00-" & sNumProceso & "-00-" & "OK"
                sNombreRecurso = Encriptar(sNombreRecurso)
                Response.Redirect(coleccionDatos(0) & "?CodTmpOK=" & sNombreRecurso)

                'Response.Redirect(coleccionDatos(0) & "?CodTmpOK=" & sNombreRecurso)
            End If

            'Exit Sub
        Else
        End If

    End Sub




    Private Sub Estado_Controles()

        fileUp_Hojadevida.Visible = True
        linkVer_HojadeVida.Visible = False
        btnCargar_HojadeVida.Visible = True
        btnEliminar_HojadeVida.Visible = False

        fileUp_SoporteAcademicos.Visible = True
        linkVer_SoporteAcademicos.Visible = False
        btnCargar_SoporteAcademicos.Visible = True
        btnEliminar_SoporteAcademicos.Visible = False

        fileUp_SoporteEduContinua.Visible = True
        linkVer_SoporteEduContinua.Visible = False
        btnCargar_SoporteEduContinua.Visible = True
        btnEliminar_SoporteEduContinua.Visible = False

        fileUp_CertificacionesLaborales.Visible = True
        linkVer_CertificacionesLaborales.Visible = False
        btnCargar_CertificacionesLaborales.Visible = True
        btnEliminar_CertificacionesLaborales.Visible = False

        fileUp_FotocopiaTarjetaProfesional.Visible = True
        linkVer_FotocopiaTarjetaProfesional.Visible = False
        btnCargar_FotocopiaTarjetaProfesional.Visible = True
        btnEliminar_FotocopiaTarjetaProfesional.Visible = False

        fileUp_FotocopiaCarnetVacunacion.Visible = True
        linkVer_FotocopiaCarnetVacunacion.Visible = False
        btnCargar_FotocopiaCarnetVacunacion.Visible = True
        btnEliminar_FotocopiaCarnetVacunacion.Visible = False

        fileUp_FotocopiaLicenciaConduccion.Visible = True
        linkVer_FotocopiaLicenciaConduccion.Visible = False
        btnCargar_FotocopiaLicenciaConduccion.Visible = True
        btnEliminar_FotocopiaLicenciaConduccion.Visible = False


        fileUp_FotocopiaDocumentoIdentidad.Visible = True
        linkVer_FotocopiaDocumentoIdentidad.Visible = False
        btnCargar_FotocopiaDocumentoIdentidad.Visible = True
        btnEliminar_FotocopiaDocumentoIdentidad.Visible = False

        fileUp_CertificacionBancaria.Visible = True
        linkVer_CertificacionBancaria.Visible = False
        btnCargar_CertificacionBancaria.Visible = True
        btnEliminar_CertificacionBancaria.Visible = False

        fileUp_Fotografia.Visible = True
        linkVer_Fotografia.Visible = False
        btnCargar_Fotografia.Visible = True
        btnEliminar_Fotografia.Visible = False


        fileUp_CertificadoEPS.Visible = True
        linkVer_CertificadoEPS.Visible = False
        btnCargar_CertificadoEPS.Visible = True
        btnEliminar_CertificadoEPS.Visible = False

        fileUp_CertificadoPensiones.Visible = True
        linkVer_CertificadoPensiones.Visible = False
        btnCargar_CertificadoPensiones.Visible = True
        btnEliminar_CertificadoPensiones.Visible = False

        fileUp_CertificadoCesantias.Visible = True
        linkVer_CertificadoCesantias.Visible = False
        btnCargar_CertificadoCesantias.Visible = True
        btnEliminar_CertificadoCesantias.Visible = False

        ' 222222222222222

        fileUp_SERGAFO038.Visible = True
        linkVer_SERGAFO038.Visible = False
        btnCargar_SERGAFO038.Visible = True
        btnEliminar_SERGAFO038.Visible = False

        fileUp_GABF214.Visible = True
        linkVer_GABF214.Visible = False
        btnCargar_GABF214.Visible = True
        btnEliminar_GABF214.Visible = False

        fileUp_PruebaPsicotecnica.Visible = True
        linkVer_PruebaPsicotecnica.Visible = False
        btnCargar_PruebaPsicotecnica.Visible = True
        btnEliminar_PruebaPsicotecnica.Visible = False


        fileUp_RCNACMenor7.Visible = True
        linkVer_RCNACMenor7.Visible = False
        btnCargar_RCNACMenor7.Visible = True
        btnEliminar_RCNACMenor7.Visible = False

        fileUp_RCTINinos7y18.Visible = True
        linkVer_RCTINinos7y18.Visible = False
        btnCargar_RCTINinos7y18.Visible = True
        btnEliminar_RCTINinos7y18.Visible = False

        fileUp_CCNinos18y25.Visible = True
        linkVer_CCNinos18y25.Visible = False
        btnCargar_CCNinos18y25.Visible = True
        btnEliminar_CCNinos18y25.Visible = False

        fileUp_CCNinos18Incap.Visible = True
        linkVer_CCNinos18Incap.Visible = False
        btnCargar_CCNinos18Incap.Visible = True
        btnEliminar_CCNinos18Incap.Visible = False

        fileUp_CCConyugue.Visible = True
        linkVer_CCConyugue.Visible = False
        btnCargar_CCConyugue.Visible = True
        btnEliminar_CCConyugue.Visible = False

        fileUp_CCPadres.Visible = True
        linkVer_CCPadres.Visible = False
        btnCargar_CCPadres.Visible = True
        btnEliminar_CCPadres.Visible = False

        '44444

        fileUp_CCFMenor7.Visible = True
        linkVer_CCFMenor7.Visible = False
        btnCargar_CCFMenor7.Visible = True
        btnEliminar_CCFMenor7.Visible = False


        fileUp_CCFMenor7y12.Visible = True
        linkVer_CCFMenor7y12.Visible = False
        btnCargar_CCFMenor7y12.Visible = True
        btnEliminar_CCFMenor7y12.Visible = False


        fileUp_CCFhijo12y18.Visible = True
        linkVer_CCFhijo12y18.Visible = False
        btnCargar_CCFhijo12y18.Visible = True
        btnEliminar_CCFhijo12y18.Visible = False

        fileUp_CCFPadres.Visible = True
        linkVer_CCFPadres.Visible = False
        btnCargar_CCFPadres.Visible = True
        btnEliminar_CCFPadres.Visible = False

        fileUp_CCFHijos18Incapa.Visible = True
        linkVer_CCFHijos18Incapa.Visible = False
        btnCargar_CCFHijos18Incapa.Visible = True
        btnEliminar_CCFHijos18Incapa.Visible = False

        fileUp_CCFConyuge.Visible = True
        linkVer_CCFConyuge.Visible = False
        btnCargar_CCFConyuge.Visible = True
        btnEliminar_CCFConyuge.Visible = False

        fileUp_CCFMenor.Visible = True
        linkVer_CCFMenor.Visible = False
        btnCargar_CCFMenor.Visible = True
        btnEliminar_CCFMenor.Visible = False






        Dim iExisteRecurso As Integer = 0
        Dim sWhere As String = _02_T01RECURSO.CampoLlave_02_T01NumDoc & "='" & txt_02_T01NumDoc.Text & "'"
        sWhere = sWhere & " and " & _02_T01RECURSO.CampoLlave_02_T01NumDoc & "=" & _02_T04PROCESO.CampoLlave_02_T04NumDocRecurso
        sWhere = sWhere & " and " & _02_T04PROCESO.CampoLlave_02_T04Consecutivo & "='" & sNumProceso & "'"
        iExisteRecurso = clsAdminDb.sql_Count(_02_T01RECURSO.NombreTabla & "," & _02_T04PROCESO.NombreTabla, sWhere)
        If iExisteRecurso <> 0 Then

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            Dim iExisteDocumento As Integer = 0
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0101'"
            'Documento 0101  Hoja de Vida
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_Hojadevida.Visible = False
                linkVer_HojadeVida.Visible = True
                btnCargar_HojadeVida.Visible = False
                btnEliminar_HojadeVida.Visible = True
            End If


            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0102'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_SoporteAcademicos.Visible = False
                linkVer_SoporteAcademicos.Visible = True
                btnCargar_SoporteAcademicos.Visible = False
                btnEliminar_SoporteAcademicos.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0103'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_SoporteEduContinua.Visible = False

                linkVer_SoporteEduContinua.Visible = True
                btnCargar_SoporteEduContinua.Visible = False
                btnEliminar_SoporteEduContinua.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0104'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CertificacionesLaborales.Visible = False
                linkVer_CertificacionesLaborales.Visible = True
                btnCargar_CertificacionesLaborales.Visible = False
                btnEliminar_CertificacionesLaborales.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0105'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_FotocopiaTarjetaProfesional.Visible = False
                linkVer_FotocopiaTarjetaProfesional.Visible = True
                btnCargar_FotocopiaTarjetaProfesional.Visible = False
                btnEliminar_FotocopiaTarjetaProfesional.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0106'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_FotocopiaCarnetVacunacion.Visible = False
                linkVer_FotocopiaCarnetVacunacion.Visible = True
                btnCargar_FotocopiaCarnetVacunacion.Visible = False
                btnEliminar_FotocopiaCarnetVacunacion.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0107'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_FotocopiaLicenciaConduccion.Visible = False
                linkVer_FotocopiaLicenciaConduccion.Visible = True
                btnCargar_FotocopiaLicenciaConduccion.Visible = False
                btnEliminar_FotocopiaLicenciaConduccion.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0108'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_FotocopiaDocumentoIdentidad.Visible = False
                linkVer_FotocopiaDocumentoIdentidad.Visible = True
                btnCargar_FotocopiaDocumentoIdentidad.Visible = False
                btnEliminar_FotocopiaDocumentoIdentidad.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0109'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CertificacionBancaria.Visible = False

                linkVer_CertificacionBancaria.Visible = True
                btnCargar_CertificacionBancaria.Visible = False
                btnEliminar_CertificacionBancaria.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0110'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_Fotografia.Visible = False
                linkVer_Fotografia.Visible = True
                btnCargar_Fotografia.Visible = False
                btnEliminar_Fotografia.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0111'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CertificadoEPS.Visible = False
                linkVer_CertificadoEPS.Visible = True
                btnCargar_CertificadoEPS.Visible = False
                btnEliminar_CertificadoEPS.Visible = True
            End If
            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0112'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CertificadoPensiones.Visible = False

                linkVer_CertificadoPensiones.Visible = True
                btnCargar_CertificadoPensiones.Visible = False
                btnEliminar_CertificadoPensiones.Visible = True
            End If
            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0113'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CertificadoCesantias.Visible = False
                linkVer_CertificadoCesantias.Visible = True
                btnCargar_CertificadoCesantias.Visible = False
                btnEliminar_CertificadoCesantias.Visible = True
            End If

            '2222222222222
            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0201'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_SERGAFO038.Visible = False
                linkVer_SERGAFO038.Visible = True
                btnCargar_SERGAFO038.Visible = False
                btnEliminar_SERGAFO038.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0202'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_GABF214.Visible = False
                linkVer_GABF214.Visible = True
                btnCargar_GABF214.Visible = False
                btnEliminar_GABF214.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0203'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_PruebaPsicotecnica.Visible = False
                linkVer_PruebaPsicotecnica.Visible = True
                btnCargar_PruebaPsicotecnica.Visible = False
                btnEliminar_PruebaPsicotecnica.Visible = True
            End If

            '333333333333
            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0301'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_RCNACMenor7.Visible = False
                linkVer_RCNACMenor7.Visible = True
                btnCargar_RCNACMenor7.Visible = False
                btnEliminar_RCNACMenor7.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0302'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_RCTINinos7y18.Visible = False
                linkVer_RCTINinos7y18.Visible = True
                btnCargar_RCTINinos7y18.Visible = False
                btnEliminar_RCTINinos7y18.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0303'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCNinos18y25.Visible = False
                linkVer_CCNinos18y25.Visible = True
                btnCargar_CCNinos18y25.Visible = False
                btnEliminar_CCNinos18y25.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0304'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCNinos18Incap.Visible = False
                linkVer_CCNinos18Incap.Visible = True
                btnCargar_CCNinos18Incap.Visible = False
                btnEliminar_CCNinos18Incap.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0305'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCConyugue.Visible = False
                linkVer_CCConyugue.Visible = True
                btnCargar_CCConyugue.Visible = False
                btnEliminar_CCConyugue.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0306'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCPadres.Visible = False
                linkVer_CCPadres.Visible = True
                btnCargar_CCPadres.Visible = False
                btnEliminar_CCPadres.Visible = True
            End If

            '444444
            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0401'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCFMenor7.Visible = False
                linkVer_CCFMenor7.Visible = True
                btnCargar_CCFMenor7.Visible = False
                btnEliminar_CCFMenor7.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0402'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCFMenor7y12.Visible = False
                linkVer_CCFMenor7y12.Visible = True
                btnCargar_CCFMenor7y12.Visible = False
                btnEliminar_CCFMenor7y12.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0403'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCFhijo12y18.Visible = False
                linkVer_CCFhijo12y18.Visible = True
                btnCargar_CCFhijo12y18.Visible = False
                btnEliminar_CCFhijo12y18.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0404'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCFPadres.Visible = False
                linkVer_CCFPadres.Visible = True
                btnCargar_CCFPadres.Visible = False
                btnEliminar_CCFPadres.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0405'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCFHijos18Incapa.Visible = False
                linkVer_CCFHijos18Incapa.Visible = True
                btnCargar_CCFHijos18Incapa.Visible = False
                btnEliminar_CCFHijos18Incapa.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0406'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCFConyuge.Visible = False
                linkVer_CCFConyuge.Visible = True
                btnCargar_CCFConyuge.Visible = False
                btnEliminar_CCFConyuge.Visible = True
            End If

            'se verifica todos los documentos de la tabla _02_T02DOCCONTRATACION
            sWhere = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "='" & txt_02_T01NumDoc.Text & "' and " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "='" & sNumProceso & "' and " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "='0407'"
            'Documento 0102 Soportes Academicos
            iExisteDocumento = clsAdminDb.sql_Count(_02_T02DOCCONTRATACION.NombreTabla, sWhere)
            If iExisteDocumento <> 0 Then
                fileUp_CCFMenor.Visible = False
                linkVer_CCFMenor.Visible = True
                btnCargar_CCFMenor.Visible = False
                btnEliminar_CCFMenor.Visible = True
            End If


        End If


        If drop_02_T01EstadoProceso.Text = "Entregado" Then
            btnCargar_HojadeVida.Visible = False
            btnEliminar_HojadeVida.Visible = False


            btnCargar_SoporteAcademicos.Visible = False
            btnEliminar_SoporteAcademicos.Visible = False

            btnCargar_SoporteEduContinua.Visible = False
            btnEliminar_SoporteEduContinua.Visible = False

            btnCargar_CertificacionesLaborales.Visible = False
            btnEliminar_CertificacionesLaborales.Visible = False

            btnCargar_FotocopiaTarjetaProfesional.Visible = False
            btnEliminar_FotocopiaTarjetaProfesional.Visible = False

            btnCargar_FotocopiaCarnetVacunacion.Visible = False
            btnEliminar_FotocopiaCarnetVacunacion.Visible = False

            btnCargar_FotocopiaLicenciaConduccion.Visible = False
            btnEliminar_FotocopiaLicenciaConduccion.Visible = False

            btnCargar_FotocopiaDocumentoIdentidad.Visible = False
            btnEliminar_FotocopiaDocumentoIdentidad.Visible = False

            btnCargar_CertificacionBancaria.Visible = False
            btnEliminar_CertificacionBancaria.Visible = False

            btnCargar_Fotografia.Visible = False
            btnEliminar_Fotografia.Visible = False

            btnCargar_CertificadoEPS.Visible = False
            btnEliminar_CertificadoEPS.Visible = False

            btnCargar_CertificadoPensiones.Visible = False
            btnEliminar_CertificadoPensiones.Visible = False

            btnCargar_CertificadoCesantias.Visible = False
            btnEliminar_CertificadoCesantias.Visible = False

            '22222222222
            btnCargar_SERGAFO038.Visible = False
            btnEliminar_SERGAFO038.Visible = False

            btnCargar_GABF214.Visible = False
            btnEliminar_GABF214.Visible = False

            btnCargar_PruebaPsicotecnica.Visible = False
            btnEliminar_PruebaPsicotecnica.Visible = False

            '33333333
            btnCargar_RCNACMenor7.Visible = False
            btnEliminar_RCNACMenor7.Visible = False

            btnCargar_RCTINinos7y18.Visible = False
            btnEliminar_RCTINinos7y18.Visible = False

            btnCargar_CCNinos18y25.Visible = False
            btnEliminar_CCNinos18y25.Visible = False

            btnCargar_CCNinos18Incap.Visible = False
            btnEliminar_CCNinos18Incap.Visible = False

            btnCargar_CCConyugue.Visible = False
            btnEliminar_CCConyugue.Visible = False

            btnCargar_CCPadres.Visible = False
            btnEliminar_CCPadres.Visible = False

            '44444444
            btnCargar_CCFMenor7.Visible = False
            btnEliminar_CCFMenor7.Visible = False

            btnCargar_CCFMenor7y12.Visible = False
            btnEliminar_CCFMenor7y12.Visible = False

            btnCargar_CCFhijo12y18.Visible = False
            btnEliminar_CCFhijo12y18.Visible = False
            btnCargar_CCFPadres.Visible = False
            btnEliminar_CCFPadres.Visible = False

            btnCargar_CCFHijos18Incapa.Visible = False
            btnEliminar_CCFHijos18Incapa.Visible = False

            btnCargar_CCFConyuge.Visible = False
            btnEliminar_CCFConyuge.Visible = False

            btnCargar_CCFMenor.Visible = False
            btnEliminar_CCFMenor.Visible = False



            btn_IniciarProceso.Visible = False
            botonEnviar.Visible = False

        End If

    End Sub

    Private Sub btn_IniciarProceso_Click(sender As Object, e As EventArgs) Handles btn_IniciarProceso.Click
        If bValidarCampos() = False Then
            Exit Sub
        Else
            Guardar_Registro()
        End If
    End Sub

    Private Sub Guardar_Registro()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla, sLlaves)
        If lCantRegistros = 0 Then

            Datos_Modulo()
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla, sCamposTabla, sCamposINS)
            If bSqlInsert = True Then
                Lit_Mensaje.Text += "Se Inicio Proceso Correctamente </div>"
                drop_02_T01EstadoProceso.Text = "Iniciado"
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    Lit_Mensaje.Text += clsAdminDb.Mostrar_Error & "</div>"

                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla, sCamposUPD, sLlaves)
            If bSqlInsert = True Then
                Lit_Mensaje.Text += "Se actualizo Inicio Proceso Correctamente </div>"
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    Lit_Mensaje.Text += clsAdminDb.Mostrar_Error & "</div>"
                Else

                End If
            End If
        End If
    End Sub

    Private Sub Cargar_Registro_Recurso()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object
        Datos_Modulo()
        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla, sCamposTabla, sLlaves)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    'MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    'MostrarMensaje("No se encontraron datos ")
                End If
            Else
                drop_02_T01TipoDocidentidad.Text = coleccionDatos(0)
                txt_02_T01NumDoc.Text = coleccionDatos(1)
                txt_02_T01Nombres.Text = coleccionDatos(2)
                txt_02_T01CorreoPersonal.Text = coleccionDatos(3)
                drop_02_T01Activo.Text = coleccionDatos(4)
                drop_02_T01EstadoProceso.Text = coleccionDatos(5)
                If drop_02_T01EstadoProceso.Text = "Iniciado" Then
                    Dim dFecha As Date
                    Dim sFecha As String = ""
                    Dim iHora As String = Now.Hour
                    Dim iMinute As String = Now.Minute

                    If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                        If Val(iHora) < 10 Then
                            iHora = "0" & iHora
                        End If
                        If Val(iMinute) < 10 Then
                            iMinute = "0" & iMinute
                        End If

                    End If
                    Dim bActualizado As Boolean = False
                    Dim sNombreTabla_Tem = _02_T01RECURSO.NombreTabla
                    Dim sLlaves_Tem = _02_T01RECURSO.CampoLlave_02_T01NumDoc & "=  '" & txt_02_T01NumDoc.Text & "'"
                    drop_02_T01EstadoProceso.Text = "Recibido"
                    Dim sUpdate_TEm As String = _02_T01RECURSO.Campo_02_T01EstadoProceso & "='" & drop_02_T01EstadoProceso.Text & "'"
                    bActualizado = clsAdminDb.sql_Update(sNombreTabla_Tem, sUpdate_TEm, sLlaves_Tem)

                    If bActualizado = True Then
                        bActualizado = False
                        sNombreTabla_Tem = _02_T04PROCESO.NombreTabla
                        sLlaves_Tem = _02_T04PROCESO.CampoLlave_02_T04NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "' and " & _02_T04PROCESO.CampoLlave_02_T04Consecutivo & "='" & sNumProceso & "'"
                        sUpdate_TEm = _02_T04PROCESO.Campo_02_T04FechaRecibido & "='" & sFecha & "'," & _02_T04PROCESO.Campo_02_T04HoraRecibido & "='" & iHora & ":" & iMinute & "'," & _02_T04PROCESO.Campo_02_T04Estado & "='" & drop_02_T01EstadoProceso.Text & "'"
                        bActualizado = clsAdminDb.sql_Update(sNombreTabla_Tem, sUpdate_TEm, sLlaves_Tem)
                    Else
                    End If
                End If



                Estado_Controles()
            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                'MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If
    End Sub


    Private Sub Datos_Modulo()
        sCodModulo = _02_T01RECURSO.CodigoModulo
        sNombreTabla = _02_T01RECURSO.NombreTabla
        sCamposTabla = _02_T01RECURSO.CamposTabla
        sCamposINS = "'" & drop_02_T01TipoDocidentidad.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01Nombres.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01CorreoPersonal.Text) & "'" & "," & "'" & drop_02_T01Activo.Text & "','Iniciado'"
        sCamposUPD = _02_T01RECURSO.Campo_02_T01Nombres & "='" & clsAdminDb.sRemoverHTML(txt_02_T01Nombres.Text) & "'" & "," & _02_T01RECURSO.Campo_02_T01CorreoPersonal & "='" & clsAdminDb.sRemoverHTML(txt_02_T01CorreoPersonal.Text) & "'" & "," & _02_T01RECURSO.Campo_02_T01Activo & "='" & drop_02_T01Activo.Text & "'"
        sLlaves = _02_T01RECURSO.CampoLlave_02_T01NumDoc & "=  '" & txt_02_T01NumDoc.Text & "'"
    End Sub

    Private Sub txt_02_T01NumDoc_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T01NumDoc.TextChanged
        If txt_02_T01NumDoc.Text <> "" Then
            Cargar_Registro_Recurso()
        End If
    End Sub

    Private Sub Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_Tem, sCodigoDocumento_TEm, sCAmpoArchivo_TEm, sCamposTabla_Tem, sCamposINS_TEm, sLlaves_Tem)
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla_Tem, sLlaves_Tem)
        If lCantRegistros = 0 Then


            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla_Tem, sCamposTabla_Tem, sCamposINS_TEm)
            If bSqlInsert = True Then
                Lit_Mensaje.Text += "Se Guardo Correctamente este archivo</div>"
                drop_02_T01EstadoProceso.Text = "En Gestion"
                Dim dFecha As Date
                Dim sFecha As String = ""
                Dim iHora As String = Now.Hour
                Dim iMinute As String = Now.Minute

                If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                    If Val(iHora) < 10 Then
                        iHora = "0" & iHora
                    End If
                    If Val(iMinute) < 10 Then
                        iMinute = "0" & iMinute
                    End If

                End If
                Dim bActualizado As Boolean = False
                Dim sUpdate_TEm As String = _02_T01RECURSO.Campo_02_T01EstadoProceso & "='" & drop_02_T01EstadoProceso.Text & "'"
                bActualizado = clsAdminDb.sql_Update(sNombreTabla_Tem, sUpdate_TEm, sLlaves_Tem)
                If bActualizado = True Then
                    bActualizado = False
                    sNombreTabla_Tem = _02_T04PROCESO.NombreTabla
                    sLlaves_Tem = _02_T04PROCESO.CampoLlave_02_T04NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "' and " & _02_T04PROCESO.CampoLlave_02_T04Consecutivo & "='" & sNumProceso & "'"
                    sUpdate_TEm = _02_T04PROCESO.Campo_02_T04FechaGestionado & "='" & sFecha & "'," & _02_T04PROCESO.Campo_02_T04HoraGestionado & "='" & iHora & ":" & iMinute & "'," & _02_T04PROCESO.Campo_02_T04Estado & "='" & drop_02_T01EstadoProceso.Text & "',"
                    bActualizado = clsAdminDb.sql_Update(sNombreTabla_Tem, sUpdate_TEm, sLlaves_Tem)

                End If
                bGuardarArchivos(sNombreTabla_Tem, sCodigoDocumento_TEm, sCAmpoArchivo_TEm, sLlaves_Tem)
                Estado_Controles()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    Lit_Mensaje.Text += clsAdminDb.Mostrar_Error & "</div>"

                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
        End If
    End Sub

    Private Function bGuardarArchivos(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoArchivo_TEm, sLlaves_Tem) As Boolean
        bGuardarArchivos = False
        'clsAdminDb = New adminitradorDB
        Dim bContinuar As Boolean = False
        Dim imageInferior As Byte()
        Select Case sCodigoDocumento_TEm
            Case "0101" 'Hoja de Vida
                Using reader As New System.IO.BinaryReader(fileUp_Hojadevida.PostedFile.InputStream)
                    imageInferior = reader.ReadBytes(fileUp_Hojadevida.PostedFile.ContentLength)
                End Using
            Case "0102"
                Using reader As New System.IO.BinaryReader(fileUp_SoporteAcademicos.PostedFile.InputStream)
                    imageInferior = reader.ReadBytes(fileUp_SoporteAcademicos.PostedFile.ContentLength)
                End Using


        End Select
        bContinuar = False
        bContinuar = clsAdminDb.sql_Insert_Archivo_Varbinary(sNombreTabla_TEm, sCampoArchivo_TEm, sLlaves_Tem, imageInferior)
        If bContinuar = True Then
            bGuardarArchivos = True
        Else
            If clsAdminDb.Mostrar_Error <> "" Then

            Else

            End If
        End If

    End Function

    Private Sub ExportarArchivo(sNombreTabla_TEm, sCampos_TEm, sLlaves_Tem)
        'Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object

        coleccionDatos = clsAdminDb.sql_Select_Bytes(sNombreTabla_TEm, sCampos_TEm, sLlaves_Tem)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    'MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    'MostrarMensaje("No se encontraron datos ")
                End If
            Else
                Dim byImagen() As Byte = Nothing
                byImagen = CType(coleccionDatos(0), Byte())
                'oMemoryStream = New MemoryStream(byImagen)

                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", coleccionDatos(1) & coleccionDatos(2)))
                Select Case UCase(coleccionDatos(2))
                    Case ".DOC"
                        Response.ContentType = "application/msword"
                    Case ".DOCX"
                        Response.ContentType = "application/msword"
                    Case ".XLS"
                        Response.ContentType = "application/ms-excel"
                    Case ".XLSX"
                        Response.ContentType = "application/ms-excel"
                    Case ".JPEG"
                        Response.ContentType = "image/jpeg"
                    Case ".JPG"
                        Response.ContentType = "image/jpg"
                    Case ".TIFF"
                        Response.ContentType = "image/tiff"
                    Case ".BMP"
                        Response.ContentType = "image/bmp"
                    Case ".PDF"
                        Response.ContentType = "application/pdf"
                    Case ".PLAIN"
                        Response.ContentType = "text/plain"
                    Case ".TXT"
                        Response.ContentType = "text/plain"
                    Case ".PPT"
                        Response.ContentType = "application/vnd.ms-powerpoint"
                    Case ".ZIP"
                        Response.ContentType = "application/zip"
                    Case Else
                        Response.ContentType = "application/octet-stream"
                End Select

                Response.BinaryWrite(byImagen)
                Response.End()


            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                'MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If
    End Sub


    Private Sub btnCargar_HojadeVida_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_HojadeVida.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_Hojadevida.HasFile) = False Then
            fileUp_Hojadevida.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_Hojadevida.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_Hojadevida.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0101"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"
            'Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_Hojadevida.PostedFile.ContentType
            Using fs As Stream = fileUp_Hojadevida.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Hoja de Vida" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", ContentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""

                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)
        End If

        Estado_Controles()
    End Sub

    'Private Sub btnCargar_HojadeVida_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_HojadeVida.Click
    '    If bValidarCampos() = False Then
    '        Exit Sub
    '    End If
    '    Lit_Mensaje.Text = ""
    '    Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
    '    Lit_Mensaje.Visible = True

    '    If Trim(fileUp_Hojadevida.HasFile) = False Then
    '        fileUp_Hojadevida.Focus()
    '        Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
    '        Exit Sub
    '    Else

    '        Dim fileName As String = Server.HtmlEncode(fileUp_Hojadevida.FileName)
    '        Dim extension As String = System.IO.Path.GetExtension(fileName)
    '        fileName = Replace(fileName, extension, "")
    '        If UCase(extension) = ".PDF" Then
    '        Else
    '            Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
    '            Exit Sub
    '        End If
    '        Dim lTamano As Integer = 0
    '        lTamano = ((fileUp_Hojadevida.FileContent.Length / 1024) / 1024)
    '        If lTamano > 30 Then
    '            Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
    '            Exit Sub
    '        End If

    '        Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
    '        Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
    '        Dim sCodigoDocumento_TEm As String = "0101"
    '        Dim dFecha As Date
    '        Dim sFecha As String = ""
    '        Dim iHora As String = Now.Hour
    '        Dim iMinute As String = Now.Minute
    '        Dim sHoraMinuto As String = ""
    '        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
    '            If Val(iHora) < 10 Then
    '                iHora = "0" & iHora
    '            End If

    '            If Val(iMinute) < 10 Then
    '                iMinute = "0" & iMinute
    '            End If
    '            sHoraMinuto = iHora & ":" & iMinute
    '        End If

    '        Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
    '        Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
    '        Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"
    '        'Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

    '        Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

    '        Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)
    '    End If

    'End Sub


    Private Sub linkVer_HojadeVida_Click(sender As Object, e As EventArgs) Handles linkVer_HojadeVida.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0101'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_HojadeVida_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_HojadeVida.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0101'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_Hojadevida.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente la Hoja de vida</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub


    Private Sub btnCargar_SoporteAcademicos_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_SoporteAcademicos.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_SoporteAcademicos.HasFile) = False Then
            fileUp_SoporteAcademicos.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_SoporteAcademicos.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            Dim contentType As String = fileUp_SoporteAcademicos.PostedFile.ContentType
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_SoporteAcademicos.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0102"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "','" & "Hoja de Vida" & extension & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo
            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            'Dim contentType As String = fileUp_Hojadevida.PostedFile.ContentType
            Using fs As Stream = fileUp_SoporteAcademicos.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Soportes Academicos" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            Estado_Controles()
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_SoporteAcademicos_Click(sender As Object, e As EventArgs) Handles linkVer_SoporteAcademicos.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0102'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_SoporteAcademicos_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_SoporteAcademicos.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0102'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_SoporteAcademicos.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente la Hoja de vida</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_SoporteEduContinua_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_SoporteEduContinua.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_SoporteEduContinua.HasFile) = False Then
            fileUp_SoporteEduContinua.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_SoporteEduContinua.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_SoporteEduContinua.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0103"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo
            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_SoporteEduContinua.PostedFile.ContentType
            Using fs As Stream = fileUp_SoporteEduContinua.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Soportes Educacion Continuada" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_SoporteEduContinua_Click(sender As Object, e As EventArgs) Handles linkVer_SoporteEduContinua.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0103'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_SoporteEduContinua_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_SoporteEduContinua.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0103'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_SoporteEduContinua.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente Soporte Educación Continua</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CertificacionesLaborales_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CertificacionesLaborales.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CertificacionesLaborales.HasFile) = False Then
            fileUp_CertificacionesLaborales.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CertificacionesLaborales.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CertificacionesLaborales.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0104"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo
            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CertificacionesLaborales.PostedFile.ContentType
            Using fs As Stream = fileUp_CertificacionesLaborales.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Certificaciones Laborales" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_CertificacionesLaborales_Click(sender As Object, e As EventArgs) Handles linkVer_CertificacionesLaborales.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0104'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CertificacionesLaborales_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CertificacionesLaborales.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0104'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CertificacionesLaborales.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente las Certificaciones Laborales</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_FotocopiaTarjetaProfesional_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_FotocopiaTarjetaProfesional.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_FotocopiaTarjetaProfesional.HasFile) = False Then
            fileUp_FotocopiaTarjetaProfesional.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_FotocopiaTarjetaProfesional.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_FotocopiaTarjetaProfesional.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0105"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_FotocopiaTarjetaProfesional.PostedFile.ContentType
            Using fs As Stream = fileUp_FotocopiaTarjetaProfesional.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Tarjeta Profesional" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_FotocopiaTarjetaProfesional_Click(sender As Object, e As EventArgs) Handles linkVer_FotocopiaTarjetaProfesional.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0105'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_FotocopiaTarjetaProfesional_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_FotocopiaTarjetaProfesional.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0105'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_FotocopiaTarjetaProfesional.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente la Tarjeta Profesional</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_FotocopiaCarnetVacunacion_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_FotocopiaCarnetVacunacion.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_FotocopiaCarnetVacunacion.HasFile) = False Then
            fileUp_FotocopiaCarnetVacunacion.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_FotocopiaCarnetVacunacion.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_FotocopiaCarnetVacunacion.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0106"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_FotocopiaCarnetVacunacion.PostedFile.ContentType
            Using fs As Stream = fileUp_FotocopiaCarnetVacunacion.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Carnet de Vacunacion" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_FotocopiaCarnetVacunacion_Click(sender As Object, e As EventArgs) Handles linkVer_FotocopiaCarnetVacunacion.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0106'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_FotocopiaCarnetVacunacion_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_FotocopiaCarnetVacunacion.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0106'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_FotocopiaCarnetVacunacion.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Carnet Vacunacion</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_FotocopiaLicenciaConduccion_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_FotocopiaLicenciaConduccion.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_FotocopiaLicenciaConduccion.HasFile) = False Then
            fileUp_FotocopiaLicenciaConduccion.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_FotocopiaLicenciaConduccion.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_FotocopiaLicenciaConduccion.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0107"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_FotocopiaLicenciaConduccion.PostedFile.ContentType
            Using fs As Stream = fileUp_FotocopiaLicenciaConduccion.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Licencia de Conduccion" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_FotocopiaLicenciaConduccion_Click(sender As Object, e As EventArgs) Handles linkVer_FotocopiaLicenciaConduccion.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0107'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_FotocopiaLicenciaConduccion_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_FotocopiaLicenciaConduccion.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0107'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_FotocopiaLicenciaConduccion.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente la Licencia Conduccion</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_FotocopiaDocumentoIdentidad_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_FotocopiaDocumentoIdentidad.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_FotocopiaDocumentoIdentidad.HasFile) = False Then
            fileUp_FotocopiaDocumentoIdentidad.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_FotocopiaDocumentoIdentidad.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_FotocopiaDocumentoIdentidad.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0108"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_FotocopiaDocumentoIdentidad.PostedFile.ContentType
            Using fs As Stream = fileUp_FotocopiaDocumentoIdentidad.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Documento de Identidad" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_FotocopiaDocumentoIdentidad_Click(sender As Object, e As EventArgs) Handles linkVer_FotocopiaDocumentoIdentidad.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0108'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_FotocopiaDocumentoIdentidad_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_FotocopiaDocumentoIdentidad.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0108'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_FotocopiaDocumentoIdentidad.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Documento de Identidad</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CertificacionBancaria_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CertificacionBancaria.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CertificacionBancaria.HasFile) = False Then
            fileUp_CertificacionBancaria.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CertificacionBancaria.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CertificacionBancaria.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0109"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CertificacionBancaria.PostedFile.ContentType
            Using fs As Stream = fileUp_CertificacionBancaria.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Certificacion Bancaria" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_CertificacionBancaria_Click(sender As Object, e As EventArgs) Handles linkVer_CertificacionBancaria.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0109'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CertificacionBancaria_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CertificacionBancaria.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0109'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CertificacionBancaria.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente Certificacion Bancaria</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_Fotografia_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_Fotografia.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_Fotografia.HasFile) = False Then
            fileUp_Fotografia.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_Fotografia.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".JPG" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .JPG</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_Fotografia.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0110"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_Fotografia.PostedFile.ContentType
            Using fs As Stream = fileUp_Fotografia.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Fotografia 3x4" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_Fotografia_Click(sender As Object, e As EventArgs) Handles linkVer_Fotografia.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0110'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_Fotografia_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_Fotografia.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0110'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_Fotografia.Focus()
            Lit_Mensaje.Text += "Se Elimino Fotografia</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CertificadoEPS_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CertificadoEPS.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CertificadoEPS.HasFile) = False Then
            fileUp_CertificadoEPS.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CertificadoEPS.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CertificadoEPS.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0111"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CertificadoEPS.PostedFile.ContentType
            Using fs As Stream = fileUp_CertificadoEPS.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Certificado de Afiliacion EPS" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_CertificadoEPS_Click(sender As Object, e As EventArgs) Handles linkVer_CertificadoEPS.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0111'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CertificadoEPS_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CertificadoEPS.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0111'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CertificadoEPS.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliación a EPS</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CertificadoPensiones_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CertificadoPensiones.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CertificadoPensiones.HasFile) = False Then
            fileUp_CertificadoPensiones.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CertificadoPensiones.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CertificadoPensiones.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0112"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CertificadoPensiones.PostedFile.ContentType
            Using fs As Stream = fileUp_CertificadoPensiones.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Certificado Afiliacion Fondo de Pensiones" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_CertificadoPensiones_Click(sender As Object, e As EventArgs) Handles linkVer_CertificadoPensiones.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0112'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CertificadoPensiones_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CertificadoPensiones.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0112'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CertificadoPensiones.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliación al fondo de pensiones</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CertificadoCesantias_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CertificadoCesantias.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CertificadoCesantias.HasFile) = False Then
            fileUp_CertificadoCesantias.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CertificadoCesantias.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CertificadoCesantias.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0113"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "01" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CertificadoCesantias.PostedFile.ContentType
            Using fs As Stream = fileUp_CertificadoCesantias.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Certificado de Afiliacion al Fondo de  Cesantias" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using

            'Response.Redirect(Request.Url.AbsoluteUri)
            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_CertificadoCesantias_Click(sender As Object, e As EventArgs) Handles linkVer_CertificadoCesantias.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0113'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CertificadoCesantias_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CertificadoCesantias.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0113'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CertificadoCesantias.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    '2222222
    Private Sub btnCargar_SERGAFO038_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_SERGAFO038.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_SERGAFO038.HasFile) = False Then
            fileUp_SERGAFO038.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_SERGAFO038.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".XLS" Or UCase(extension) = ".XLSX" Or UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF, XLS, XLSX</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_SERGAFO038.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0201"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "02" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_SERGAFO038.PostedFile.ContentType
            Using fs As Stream = fileUp_SERGAFO038.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "02")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "SER-GA-FO-038 Formato de vinculacion de personal." & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""

                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_SERGAFO038_Click(sender As Object, e As EventArgs) Handles linkVer_SERGAFO038.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0201'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_SERGAFO038_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_SERGAFO038.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0201'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_SERGAFO038.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_GABF214_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_GABF214.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_GABF214.HasFile) = False Then
            fileUp_GABF214.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_GABF214.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".XLS" Or UCase(extension) = ".XLSX" Or UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF, XLS, XLSX</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_GABF214.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0202"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "02" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_GABF214.PostedFile.ContentType
            Using fs As Stream = fileUp_GABF214.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "02")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "GAB-F-214 Formato Certificacion cumplimiento de perfiles" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""

                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_GABF214_Click(sender As Object, e As EventArgs) Handles linkVer_GABF214.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0202'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_GABF214_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_GABF214.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0202'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_GABF214.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_PruebaPsicotecnica_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_PruebaPsicotecnica.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_PruebaPsicotecnica.HasFile) = False Then
            fileUp_PruebaPsicotecnica.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_PruebaPsicotecnica.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".XLS" Or UCase(extension) = ".XLSX" Or UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF, XLS, XLSX</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_PruebaPsicotecnica.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0203"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "02" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_PruebaPsicotecnica.PostedFile.ContentType
            Using fs As Stream = fileUp_PruebaPsicotecnica.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "02")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Prueba Psicotecnica" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""

                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_PruebaPsicotecnica_Click(sender As Object, e As EventArgs) Handles linkVer_PruebaPsicotecnica.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0202'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_PruebaPsicotecnica_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_PruebaPsicotecnica.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0202'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_PruebaPsicotecnica.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente Prueba Psicotecnicas</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub


    Private Sub btnCargar_RCNACMenor7_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_RCNACMenor7.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_RCNACMenor7.HasFile) = False Then
            fileUp_RCNACMenor7.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_RCNACMenor7.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_RCNACMenor7.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0301"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "03" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCFMenor7.PostedFile.ContentType
            Using fs As Stream = fileUp_CCFMenor7.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "03")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Hijos menores de 7 años Copia del registro civil de nacimiento" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""

                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_RCNACMenor7_Click(sender As Object, e As EventArgs) Handles linkVer_RCNACMenor7.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0301'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_RCNACMenor7_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_RCNACMenor7.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0301'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_RCNACMenor7.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_RCTINinos7y18_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_RCTINinos7y18.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_RCTINinos7y18.HasFile) = False Then
            fileUp_RCTINinos7y18.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_RCTINinos7y18.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_RCTINinos7y18.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0302"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "03" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_RCTINinos7y18.PostedFile.ContentType
            Using fs As Stream = fileUp_RCTINinos7y18.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "03")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Hijos entre 7 y 18 años Copia Documebto de identidad" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""

                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_RCTINinos7y18_Click(sender As Object, e As EventArgs) Handles linkVer_RCTINinos7y18.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0302'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_RCTINinos7y18_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_RCTINinos7y18.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0302'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_RCTINinos7y18.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCNinos18y25_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCNinos18y25.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCNinos18y25.HasFile) = False Then
            fileUp_CCNinos18y25.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCNinos18y25.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCNinos18y25.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0303"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "03" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            Dim contentType As String = fileUp_CCNinos18y25.PostedFile.ContentType
            Using fs As Stream = fileUp_CCNinos18y25.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Documento Identidad Hijos estudiantes entre 18 y 25 años" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()




    End Sub

    Private Sub linkVer_CCNinos18y25_Click(sender As Object, e As EventArgs) Handles linkVer_CCNinos18y25.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0303'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCNinos18y25_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCNinos18y25.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0303'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCNinos18y25.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCNinos18Incap_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCNinos18Incap.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCNinos18Incap.HasFile) = False Then
            fileUp_CCNinos18Incap.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCNinos18Incap.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCNinos18Incap.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0304"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "03" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCNinos18Incap.PostedFile.ContentType
            Using fs As Stream = fileUp_CCNinos18Incap.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Documento de Identidad Hijos mayores de 18 años con incapacidad permanente" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()


    End Sub

    Private Sub linkVer_CCNinos18Incap_Click(sender As Object, e As EventArgs) Handles linkVer_CCNinos18Incap.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0304'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCNinos18Incap_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCNinos18Incap.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0304'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCNinos18Incap.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCConyugue_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCConyugue.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCConyugue.HasFile) = False Then
            fileUp_CCConyugue.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCConyugue.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCConyugue.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0305"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "03" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCConyugue.PostedFile.ContentType
            Using fs As Stream = fileUp_CCConyugue.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Documento onyugue o compañero permanente Copia registro civil de matrimonio" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()


    End Sub

    Private Sub linkVer_CCConyugue_Click(sender As Object, e As EventArgs) Handles linkVer_CCConyugue.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0305'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCConyugue_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCConyugue.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0305'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCConyugue.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCPadres_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCPadres.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCPadres.HasFile) = False Then
            fileUp_CCPadres.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCPadres.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCPadres.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0306"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "03" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCPadres.PostedFile.ContentType
            Using fs As Stream = fileUp_CCPadres.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "Documento registro civil de nacimiento del cotizante" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()

    End Sub

    Private Sub linkVer_CCPadres_Click(sender As Object, e As EventArgs) Handles linkVer_CCPadres.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0306'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCPadres_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCPadres.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0306'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCPadres.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    '444444444444444
    Private Sub btnCargar_CCFMenor7_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCFMenor7.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCFMenor7.HasFile) = False Then
            fileUp_CCFMenor7.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCFMenor7.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCFMenor7.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0401"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "04" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCFMenor7.PostedFile.ContentType
            Using fs As Stream = fileUp_CCFMenor7.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "CCF Hijos menores de 7 años" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)
            Estado_Controles()
        End If

    End Sub

    Private Sub linkVer_CCFMenor7_Click(sender As Object, e As EventArgs) Handles linkVer_CCFMenor7.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0401'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCFMenor7_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCFMenor7.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0401'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCFMenor7.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCFMenor7y12_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCFMenor7y12.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCFMenor7y12.HasFile) = False Then
            fileUp_CCFMenor7y12.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCFMenor7y12.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCFMenor7y12.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0402"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "04" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCFMenor7y12.PostedFile.ContentType
            Using fs As Stream = fileUp_CCFMenor7y12.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "CCF Hijos entre 7 y 12 años" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_CCFMenor7y12_Click(sender As Object, e As EventArgs) Handles linkVer_CCFMenor7y12.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0402'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCFMenor7y12_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCFMenor7y12.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0402'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCFMenor7y12.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCFhijo12y18_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCFhijo12y18.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCFhijo12y18.HasFile) = False Then
            fileUp_CCFhijo12y18.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCFhijo12y18.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCFhijo12y18.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0403"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "04" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCFhijo12y18.PostedFile.ContentType
            Using fs As Stream = fileUp_CCFhijo12y18.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "CCF Hijos estudiantes entre 12 y 18 años" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()
    End Sub

    Private Sub linkVer_CCFhijo12y18_Click(sender As Object, e As EventArgs) Handles linkVer_CCFhijo12y18.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0403'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCFhijo12y18_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCFhijo12y18.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0403'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCFhijo12y18.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCFPadres_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCFPadres.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCFPadres.HasFile) = False Then
            fileUp_CCFPadres.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCFPadres.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCFPadres.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0404"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "04" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCFPadres.PostedFile.ContentType
            Using fs As Stream = fileUp_CCFPadres.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "CCF Padres (Mayores de 60 años)" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()


    End Sub

    Private Sub linkVer_CCFPadres_Click(sender As Object, e As EventArgs) Handles linkVer_CCFPadres.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0404'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCFPadres_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCFPadres.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0404'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCFPadres.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCFHijos18Incapa_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCFHijos18Incapa.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCFHijos18Incapa.HasFile) = False Then
            fileUp_CCFHijos18Incapa.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCFHijos18Incapa.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCFHijos18Incapa.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0405"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "04" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCFHijos18Incapa.PostedFile.ContentType
            Using fs As Stream = fileUp_CCFHijos18Incapa.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "CCF Hijos mayores de 18 años con incapacidad permanente" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()

    End Sub

    Private Sub linkVer_CCFHijos18Incapa_Click(sender As Object, e As EventArgs) Handles linkVer_CCFHijos18Incapa.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0405'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCFHijos18Incapa_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCFHijos18Incapa.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0405'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCFHijos18Incapa.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCFConyuge_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCFConyuge.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCFConyuge.HasFile) = False Then
            fileUp_CCFConyuge.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCFConyuge.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCFConyuge.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0406"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "04" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCFConyuge.PostedFile.ContentType
            Using fs As Stream = fileUp_CCFConyuge.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "CCF Conyugue o compañero permanente" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()


    End Sub

    Private Sub linkVer_CCFConyuge_Click(sender As Object, e As EventArgs) Handles linkVer_CCFConyuge.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0406'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCFConyuge_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCFConyuge.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0406'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCFConyuge.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Sub btnCargar_CCFMenor_Click(sender As Object, e As ImageClickEventArgs) Handles btnCargar_CCFMenor.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If Trim(fileUp_CCFMenor.HasFile) = False Then
            fileUp_CCFMenor.Focus()
            Lit_Mensaje.Text += "Por favor seleccione un archivo a cargar</div>"
            Exit Sub
        Else

            Dim fileName As String = Server.HtmlEncode(fileUp_CCFMenor.FileName)
            Dim extension As String = System.IO.Path.GetExtension(fileName)
            fileName = Replace(fileName, extension, "")
            If UCase(extension) = ".PDF" Then
            Else
                Lit_Mensaje.Text += " Solo se permiten arhivos con extensiones .PDF</div>"
                Exit Sub
            End If
            Dim lTamano As Integer = 0
            lTamano = ((fileUp_CCFMenor.FileContent.Length / 1024) / 1024)
            If lTamano > 30 Then
                Lit_Mensaje.Text += " El tamaño MAXIMO permitido del archivo es de 30 mb, el archivo que desea adjuntar tiene un tamaño de " & lTamano & " MB </div>"
                Exit Sub
            End If

            Dim sNombreTabla_TEm = _02_T02DOCCONTRATACION.NombreTabla
            Dim sCamposTabla = _02_T02DOCCONTRATACION.CamposTabla
            Dim sCodigoDocumento_TEm As String = "0407"
            Dim dFecha As Date
            Dim sFecha As String = ""
            Dim iHora As String = Now.Hour
            Dim iMinute As String = Now.Minute
            Dim sHoraMinuto As String = ""
            If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                If Val(iHora) < 10 Then
                    iHora = "0" & iHora
                End If

                If Val(iMinute) < 10 Then
                    iMinute = "0" & iMinute
                End If
                sHoraMinuto = iHora & ":" & iMinute
            End If

            Dim sCamposINS_TEm = "'" & "04" & "'" & "," & "'" & sCodigoDocumento_TEm & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01NumDoc.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sFecha) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(sHoraMinuto) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(fileName) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(extension) & "'" & "," & "''" & "," & "'" & clsAdminDb.sRemoverHTML(sNumProceso) & "'"
            Dim sCamposUPD_TEm = "" '03_T02_01DOCCONTRATACION.Campo_02_T02_01Aplica & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Aplica.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Observacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Observacion.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01Archivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01Archivo.text) & "'" & "," & _02_T02DOCCONTRATACION.Campo_02_T02_01ExtensionArchivo & "='" & clsAdminDb.sRemoverHTML(txt_02_T02_01ExtensionArchivo.text) & "'"
            Dim sLlaves_TEm = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '" & sCodigoDocumento_TEm & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'"

            Dim sCampoARchivo_TEm As String = _02_T02DOCCONTRATACION.Campo_02_T02ImageArchivo

            'Dim filename2 As String = Path.GetFileName(fileUp_Hojadevida.PostedFile.FileName)
            Dim contentType As String = fileUp_CCFMenor.PostedFile.ContentType
            Using fs As Stream = fileUp_CCFMenor.PostedFile.InputStream
                Using br As BinaryReader = New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                    Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                    Using con As SqlConnection = New SqlConnection(constr)
                        Dim query As String = "INSERT INTO _02_T02DOCCONTRATACION VALUES (@_02_T02TipoDocumento,@_02_T02CodDocumento,@_02_T02NumDocRecurso,@_02_T02FechaRegistro, @_02_T02HoraRegistro, @_02_T02NombreArchivo, @_02_T02ExtensionArchivo, @_02_T02ImageArchivo, @_02_T02IDEnvio,@_02_T02ContentType)"
                        Using cmd As SqlCommand = New SqlCommand(query)
                            cmd.Connection = con
                            cmd.Parameters.AddWithValue("@_02_T02TipoDocumento", "01")
                            cmd.Parameters.AddWithValue("@_02_T02CodDocumento", sCodigoDocumento_TEm)
                            cmd.Parameters.AddWithValue("@_02_T02NumDocRecurso", txt_02_T01NumDoc.Text)
                            cmd.Parameters.AddWithValue("@_02_T02FechaRegistro", sFecha)
                            cmd.Parameters.AddWithValue("@_02_T02HoraRegistro", sHoraMinuto)
                            cmd.Parameters.AddWithValue("@_02_T02NombreArchivo", "CCF Menor de Edad" & extension)
                            cmd.Parameters.AddWithValue("@_02_T02ExtensionArchivo", extension)
                            cmd.Parameters.AddWithValue("@_02_T02ImageArchivo", bytes)
                            cmd.Parameters.AddWithValue("@_02_T02IDEnvio", sNumProceso)
                            cmd.Parameters.AddWithValue("@_02_T02ContentType", contentType)

                            'cmd.Parameters.AddWithValue("@Fecha", Now.Date)
                            'cmd.Parameters.AddWithValue("@Hora", Now.Hour)
                            'cmd.Parameters.AddWithValue("@UsuarioGestiona", "86073217")
                            'cmd.Parameters.AddWithValue("@ContentType", contentType)
                            Lit_Mensaje.Text = ""
                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using
                End Using
            End Using


            'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)

        End If
        Estado_Controles()

    End Sub

    Private Sub linkVer_CCFMenor_Click(sender As Object, e As EventArgs) Handles linkVer_CCFMenor.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If

        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sCamposTabla_Tem = "_02_T02ImageArchivo,_02_T02NombreArchivo,_02_T02ExtensionArchivo"
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0407'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)
    End Sub

    Private Sub btnEliminar_CCFMenor_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminar_CCFMenor.Click
        If bValidarCampos() = False Then
            Exit Sub
        End If
        Dim bEliminado As Boolean = False
        Dim sNombreTabla_Tem = _02_T02DOCCONTRATACION.NombreTabla
        Dim sLlaves_Tem = _02_T02DOCCONTRATACION.CampoLlave_02_T02NumDocRecurso & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0407'"
        bEliminado = clsAdminDb.sql_Delete(sNombreTabla_Tem, sLlaves_Tem)
        Lit_Mensaje.Text = ""
        Lit_Mensaje.Text = "<div id='visor_Mensajes' class='lblMensaje'>"
        Lit_Mensaje.Visible = True

        If bEliminado = True Then
            fileUp_CCFMenor.Focus()
            Lit_Mensaje.Text += "Se Elimino Correctamente el Certificado de afiliacion al fondo de cesantias</div>"
            Estado_Controles()
            Exit Sub
        Else
        End If

    End Sub

    Private Function Encriptar(ByVal texto As String) As String

        If Trim(texto) = "" Then
            Encriptar = ""
        Else
            des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(myKey))
            des.Mode = CipherMode.ECB
            Dim encrypt As ICryptoTransform = des.CreateEncryptor()
            Dim buff() As Byte = UnicodeEncoding.ASCII.GetBytes(texto)
            Encriptar = Convert.ToBase64String(encrypt.TransformFinalBlock(buff, 0, buff.Length))
        End If
        Return Encriptar
    End Function


    'Funcion para el Desencriptado de Cadenas de Texto
    Private Function Desencriptar(ByVal texto As String) As String
        If Trim(texto) = "" Then
            Desencriptar = ""
        Else
            des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(myKey))
            des.Mode = CipherMode.ECB
            Dim desencrypta As ICryptoTransform = des.CreateDecryptor()
            Dim buff() As Byte = Convert.FromBase64String(texto)
            Desencriptar = UnicodeEncoding.ASCII.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length))
        End If
        Return Desencriptar
    End Function


End Class
