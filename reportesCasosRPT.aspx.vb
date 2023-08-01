Imports System.IO

Public Class reportesCasosRPT
    Inherits System.Web.UI.Page

    Dim clsAdminDb As New adminitradorDB

    Dim bPermiso As Boolean
    Dim sCodModulo As String
    Dim sNombreTabla As String
    Dim sCamposTabla As String
    Dim sCamposINS As String
    Dim sCamposUPD As String
    Dim sLlaves As String
    Dim sTipoDoc As String
    Dim sNumDoc As String
    Dim sNombreUsuario As String
    Dim sNombreFuncionario As String

    Dim FilaHtml As System.Web.UI.HtmlControls.HtmlTableRow
    Dim CeldaHtml As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents linkbutonHtml As System.Web.UI.WebControls.LinkButton
    Protected WithEvents HyperlinkHtml As System.Web.UI.WebControls.HyperLink
    Dim sUsuarioWindows = System.Environment.UserName
    Protected WithEvents linkbutonHtml_Eliminar As System.Web.UI.WebControls.LinkButton


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsAdminDb = New adminitradorDB
        Datos_Modulo()

        If Not IsPostBack Then
            If Session("DatoUsuario" & sCod_Aplicacion) Is Nothing Then
                Session.Abandon()
                Response.Redirect("login.aspx?Err2=001")
            Else
                Dim TestArray() As String = Split(Session("DatoUsuario" & sCod_Aplicacion).ToString, "-")
                lblUsuarioLogueado_Documento.Text = TestArray(0).ToString
                lblUsuarioLogueado_Nombre.Text = TestArray(1).ToString
                txtFechaFinal.AutoPostBack = True
                txtFechaInicial.AutoPostBack = True
                dropTipoAccion.AutoPostBack = True

                Verificar_Perfil()
                CargarDrops()
                limpiarCampos()
                Registro_Procesos("Consultar", clsAdminDb.Mostrar_Consulta)
                Verificar_Perfil()

            End If
        Else
            If lblUsuarioLogueado_Documento.Text = "" Then
                Session.Abandon()
                Response.Redirect("login.aspx")
            End If

        End If

        filtro()
        MostrarMensaje("")
    End Sub

    Private Sub Verificar_Perfil()
        'clsAdminDb = New adminitradorDB
        Dim stabla_Tem As String = _99_T02PERFILxFUNCIONARIOxMODxPERMISO.NombreTabla & "," & _99_T01PERFILES.NombreTabla
        Dim sCAmpos_Tem As String = _99_T02PERFILxFUNCIONARIOxMODxPERMISO.CampoLlave_99_T02CodigoPerfil & "," & _99_T01PERFILES.Campo_99_T01MenuIzquierdo
        Dim sLlaves_Tem As String = _99_T02PERFILxFUNCIONARIOxMODxPERMISO.CampoLlave_99_T02NumDocFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _99_T02PERFILxFUNCIONARIOxMODxPERMISO.CampoLlave_99_T02CodigoPerfil & "=" & _99_T01PERFILES.CampoLlave_99_T01Codigo
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T04LIDERES.CampoLlave_01_T04NumDocLider & "=" & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario

        Dim coleccionDatosPlural As New Collection
        Dim ArregloSingular() As String
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(stabla_Tem, sCAmpos_Tem, sLlaves_Tem,)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                LitIzquierdo.Text = ArregloSingular(1)
            Next
        End If
    End Sub

    Private Sub Datos_Modulo()
        sCodModulo = _02_T01REPORTE_CASOS.CodigoModulo
        sNombreTabla = _02_T01REPORTE_CASOS.NombreTabla
        sCamposTabla = _02_T01REPORTE_CASOS.CamposTabla
        sCamposINS = "'" & clsAdminDb.sRemoverHTML(txt_02_T01Id_Reporte.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01FechaRegistro.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01DocUsuarioRegistra.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01Fecha.Text) & "'" & "," & "'" & txt_02_T01Hora.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_02_T01Id_TipoAccion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01LugarEvento.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01DescripcionEvento.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01SugerenciaEvento.Text) & "'" & "," & "'" & drop_02_T01Id_Area.Text & "'" & "," & "'" & drop_02_T01Id_Sede.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01Dependenia.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01PersonaReporta_Nombre.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T01PersonaReporta_Contacto.Text) & "'"
        sCamposUPD = _02_T01REPORTE_CASOS.Campo_02_T01Fecha & "='" & clsAdminDb.sRemoverHTML(txt_02_T01Fecha.Text) & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01Hora & "='" & txt_02_T01Hora.Text & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01Id_TipoAccion & "='" & clsAdminDb.sRemoverHTML(drop_02_T01Id_TipoAccion.Text) & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01LugarEvento & "='" & clsAdminDb.sRemoverHTML(txt_02_T01LugarEvento.Text) & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01DescripcionEvento & "='" & clsAdminDb.sRemoverHTML(txt_02_T01DescripcionEvento.Text) & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01SugerenciaEvento & "='" & clsAdminDb.sRemoverHTML(txt_02_T01SugerenciaEvento.Text) & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01Id_Area & "='" & drop_02_T01Id_Area.Text & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01Id_Sede & "='" & drop_02_T01Id_Sede.Text & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01Dependenia & "='" & clsAdminDb.sRemoverHTML(txt_02_T01Dependenia.Text) & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01PersonaReporta_Nombre & "='" & clsAdminDb.sRemoverHTML(txt_02_T01PersonaReporta_Nombre.Text) & "'" & "," & _02_T01REPORTE_CASOS.Campo_02_T01PersonaReporta_Contacto & "='" & clsAdminDb.sRemoverHTML(txt_02_T01PersonaReporta_Contacto.Text) & "'"
        sLlaves = _02_T01REPORTE_CASOS.CampoLlave_02_T01Id_Reporte & "=  '" & txt_02_T01Id_Reporte.Text & "'"
    End Sub
    Private Sub CargarDrops()
        clsAdminDb = New adminitradorDB
        With clsAdminDb
            .Lit_CargarLiteral(LitHead, LitSuperior, LitIzquierdo, sCod_Aplicacion)
            Dim stablastem As String = _02_T02TIPO_ACCION.NombreTabla
            Dim sCamposTEm As String = _02_T02TIPO_ACCION.CampoLlave_02_T02Id & "," & _02_T02TIPO_ACCION.Campo_02_T02Nombre
            Dim sllavesTEM As String = _02_T02TIPO_ACCION.Campo_02_T02Activo & "='SI'"
            .drop_CargarCombox(drop_02_T01Id_TipoAccion, stablastem, sCamposTEm, sllavesTEM)
            'If .Mostrar_Error <> """ Then
            'MostrarMensaje(.Mostrar_Error)
            'Exit Sub
            'End If

            '.drop_Cargar_SINO(dropTipoRiesgoQuimicos, False)
            Dim sCampos_tem2 = _02_T20AREA.CampoLlave_02_T20Codigo & "," & _02_T20AREA.Campo_02_T20Nombre
            Dim stables_Tem2 = _02_T20AREA.NombreTabla
            Dim sllaves_Tem2 = _02_T20AREA.Campo_02_T20Activo & "= 'SI' "
            .drop_CargarCombox(drop_02_T01Id_Area, stables_Tem2, sCampos_tem2, sllaves_Tem2, True,, _02_T20AREA.Campo_02_T20Nombre)

            Dim sCampos_tem3 = _02_T21SEDE.CampoLlave_02_T21Codigo & "," & _02_T21SEDE.Campo_02_T21Nombre
            Dim stables_Tem3 = _02_T21SEDE.NombreTabla
            Dim sllaves_Tem3 = _02_T21SEDE.Campo_02_T21Activo & "= 'SI' "
            .drop_CargarCombox(drop_02_T01Id_Sede, stables_Tem3, sCampos_tem3, sllaves_Tem3, True,, _02_T21SEDE.Campo_02_T21Nombre)

            drop_Hora.Items.Add(New ListItem("seleccionar", ""))
            drop_Hora.Items.Add(New ListItem("12 am ", "00"))
            drop_Hora.Items.Add(New ListItem("01 am ", "01"))
            drop_Hora.Items.Add(New ListItem("02 am ", "02"))
            drop_Hora.Items.Add(New ListItem("03 am ", "03"))
            drop_Hora.Items.Add(New ListItem("04 am ", "04"))
            drop_Hora.Items.Add(New ListItem("05 am ", "05"))
            drop_Hora.Items.Add(New ListItem("06 am ", "06"))
            drop_Hora.Items.Add(New ListItem("07 am ", "07"))
            drop_Hora.Items.Add(New ListItem("08 am ", "08"))
            drop_Hora.Items.Add(New ListItem("09 am ", "09"))
            drop_Hora.Items.Add(New ListItem("09 am ", "09"))
            drop_Hora.Items.Add(New ListItem("10 am ", "10"))
            drop_Hora.Items.Add(New ListItem("11 am ", "11"))
            drop_Hora.Items.Add(New ListItem("12 m ", "12"))
            drop_Hora.Items.Add(New ListItem("01 pm ", "13"))
            drop_Hora.Items.Add(New ListItem("03 pm ", "15"))
            drop_Hora.Items.Add(New ListItem("04 pm ", "16"))
            drop_Hora.Items.Add(New ListItem("05 pm ", "17"))
            drop_Hora.Items.Add(New ListItem("06 pm ", "18"))
            drop_Hora.Items.Add(New ListItem("07 pm ", "19"))
            drop_Hora.Items.Add(New ListItem("08 pm ", "20"))
            drop_Hora.Items.Add(New ListItem("09 pm ", "21"))
            drop_Hora.Items.Add(New ListItem("10 pm ", "22"))
            drop_Hora.Items.Add(New ListItem("11 pm ", "23"))

            drop_Minuto.Items.Add(New ListItem("seleccionar", ""))

            For i = 0 To 59
                If i < 10 Then
                    drop_Minuto.Items.Add(New ListItem("0" & i, "0" & i))
                Else
                    drop_Minuto.Items.Add(New ListItem(i, i))
                End If

            Next



        End With
    End Sub


    Private Sub WebForm1_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        clsAdminDb = Nothing
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If bValidarCampos() = False Then Exit Sub
        If bValidarSQL() = False Then Exit Sub
        btnEliminarPopup.Visible = False
        btnGuardarPopup.Visible = True
        Guardar_Registro()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        btnEliminarPopup.Visible = True
        btnGuardarPopup.Visible = False
        lblMensajeGuardarPopup.Text = "Para Eliminar este registro pulse Elimnar"
        ModalPopupGuardar.Show()
    End Sub

    Private Sub Eliminar_Registro()
        'Dim clsAdminDb As New adminitradorDB
        Dim bSqlDelete As Boolean = False

        bPermiso = clsAdminDb.sql_Verificar_Permisos(sCodModulo, adminitradorDB.Tipo_Permiso.Eliminar, sTipoDoc, sNumDoc, sNombreUsuario)
        If bPermiso = False Then
            MostrarMensaje("No Tiene permisos para crear un nuevo registro")
            Exit Sub
        End If
        bSqlDelete = clsAdminDb.sql_Delete(sNombreTabla, sLlaves)
        If bSqlDelete = True Then
            limpiarCampos()
            MostrarMensaje("se elimino correctamente")
            Registro_Procesos("Eliminar", clsAdminDb.Mostrar_Consulta)
            filtro()
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else
                MostrarMensaje("No se encontraron datos ")
            End If
        End If
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        limpiarCampos()
    End Sub

    Private Sub limpiarCampos()

        txt_02_T01Id_Reporte.Text = Nothing
        txt_02_T01FechaRegistro.Text = Now
        txt_02_T01DocUsuarioRegistra.Text = lblUsuarioLogueado_Documento.Text
        txt_02_T01Fecha.Text = Now.Date
        txt_02_T01Hora.Text = Now.TimeOfDay.Hours
        drop_02_T01Id_TipoAccion.Text = Nothing
        txt_02_T01LugarEvento.Text = Nothing
        txt_02_T01DescripcionEvento.Text = Nothing
        txt_02_T01SugerenciaEvento.Text = Nothing
        drop_02_T01Id_Area.Text = Nothing
        drop_02_T01Id_Sede.Text = Nothing
        txt_02_T01Dependenia.Text = Nothing
        txt_02_T01PersonaReporta_Nombre.Text = lblUsuarioLogueado_Documento.Text
        txt_02_T01PersonaReporta_Contacto.Text = Nothing

        lbl_02_T01Id_Reporte.ForeColor = Drawing.Color.Black
        lbl_02_T01FechaRegistro.ForeColor = Drawing.Color.Black
        lbl_02_T01DocUsuarioRegistra.ForeColor = Drawing.Color.Black
        lbl_02_T01Fecha.ForeColor = Drawing.Color.Black
        lbl_02_T01Hora.ForeColor = Drawing.Color.Black
        lbl_02_T01Id_TipoAccion.ForeColor = Drawing.Color.Black
        lbl_02_T01LugarEvento.ForeColor = Drawing.Color.Black
        lbl_02_T01DescripcionEvento.ForeColor = Drawing.Color.Black
        lbl_02_T01SugerenciaEvento.ForeColor = Drawing.Color.Black
        lbl_02_T01Id_Area.ForeColor = Drawing.Color.Black
        lbl_02_T01Id_Sede.ForeColor = Drawing.Color.Black
        lbl_02_T01Dependenia.ForeColor = Drawing.Color.Black
        lbl_02_T01PersonaReporta_Nombre.ForeColor = Drawing.Color.Black
        lbl_02_T01PersonaReporta_Contacto.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing

        MostrarMensaje("")
        bHabilitarControles(True)

        filtro()
    End Sub


    Private Sub bHabilitarControles(ByVal bEstado As Boolean)

        btnEliminar.Visible = False
        btnImprimir.Visible = False
    End Sub

    Private Function bValidarCampos() As Boolean
        bValidarCampos = False

        'lbl_02_T01Id_Reporte.ForeColor = Drawing.Color.Red
        'lbl_02_T01FechaRegistro.ForeColor = Drawing.Color.Red
        lbl_02_T01DocUsuarioRegistra.ForeColor = Drawing.Color.Red
        lbl_02_T01Fecha.ForeColor = Drawing.Color.Red
        lbl_02_T01Hora.ForeColor = Drawing.Color.Red
        lbl_02_T01Id_TipoAccion.ForeColor = Drawing.Color.Red
        lbl_02_T01LugarEvento.ForeColor = Drawing.Color.Red
        lbl_02_T01DescripcionEvento.ForeColor = Drawing.Color.Red
        lbl_02_T01SugerenciaEvento.ForeColor = Drawing.Color.Red
        lbl_02_T01Id_Area.ForeColor = Drawing.Color.Red
        lbl_02_T01Id_Sede.ForeColor = Drawing.Color.Red
        lbl_02_T01Dependenia.ForeColor = Drawing.Color.Red
        lbl_02_T01PersonaReporta_Nombre.ForeColor = Drawing.Color.Red
        lbl_02_T01PersonaReporta_Contacto.ForeColor = Drawing.Color.Red


        'If Trim(txt_02_T01Id_Reporte.Text) = "" Then
        '    txt_02_T01Id_Reporte.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:  01Id_Reporte")
        '    Exit Function
        'Else
        '    lbl_02_T01Id_Reporte.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T01FechaRegistro.Text) = "" Then
        '    txt_02_T01FechaRegistro.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01FechaRegistro")
        '    Exit Function
        'Else
        '    lbl_02_T01FechaRegistro.ForeColor = Drawing.Color.Black
        'End If
        If Trim(txt_02_T01DocUsuarioRegistra.Text) = "" Then
            txt_02_T01DocUsuarioRegistra.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01DocUsuarioRegistra")
            Exit Function
        Else
            lbl_02_T01DocUsuarioRegistra.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T01Fecha.Text) = "" Then
            txt_02_T01Fecha.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Fecha")
            Exit Function
        Else
            lbl_02_T01Fecha.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T01Hora.Text) = "" Then
            txt_02_T01Hora.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Hora")
            Exit Function
        Else
            lbl_02_T01Hora.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T01Id_TipoAccion.Text) = "" Then
            drop_02_T01Id_TipoAccion.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Id_TipoAccion")
            Exit Function
        Else
            lbl_02_T01Id_TipoAccion.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T01LugarEvento.Text) = "" Then
            txt_02_T01LugarEvento.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01LugarEvento")
            Exit Function
        Else
            lbl_02_T01LugarEvento.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T01DescripcionEvento.Text) = "" Then
            txt_02_T01DescripcionEvento.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01DescripcionEvento")
            Exit Function
        Else
            lbl_02_T01DescripcionEvento.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T01SugerenciaEvento.Text) = "" Then
            txt_02_T01SugerenciaEvento.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01SugerenciaEvento")
            Exit Function
        Else
            lbl_02_T01SugerenciaEvento.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T01Id_Area.Text) = "" Then
            drop_02_T01Id_Area.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Id_Area")
            Exit Function
        Else
            lbl_02_T01Id_Area.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T01Id_Sede.Text) = "" Then
            drop_02_T01Id_Sede.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Id_Sede")
            Exit Function
        Else
            lbl_02_T01Id_Sede.ForeColor = Drawing.Color.Black
        End If
        'If Trim(txt_02_T01Dependenia.Text) = "" Then
        '    txt_02_T01Dependenia.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Dependenia")
        '    Exit Function
        'Else
        '    lbl_02_T01Dependenia.ForeColor = Drawing.Color.Black
        'End If
        If Trim(txt_02_T01PersonaReporta_Nombre.Text) = "" Then
            txt_02_T01PersonaReporta_Nombre.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01PersonaReporta_Nombre")
            Exit Function
        Else
            lbl_02_T01PersonaReporta_Nombre.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T01PersonaReporta_Contacto.Text) = "" Then
            txt_02_T01PersonaReporta_Contacto.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01PersonaReporta_Contacto")
            Exit Function
        Else
            lbl_02_T01PersonaReporta_Contacto.ForeColor = Drawing.Color.Black
        End If

        bValidarCampos = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_02_T01Id_Reporte.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01FechaRegistro.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01DocUsuarioRegistra.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01Fecha.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_02_T01Id_TipoAccion.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01LugarEvento.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01DescripcionEvento.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01SugerenciaEvento.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01Dependenia.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01PersonaReporta_Nombre.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T01PersonaReporta_Contacto.Text) Then Exit Function
        bValidarSQL = True
        MostrarMensaje("")
    End Function

    Private Sub MostrarMensaje(ByVal sMensaje As String, Optional ByVal EstadosMensajesT As Integer = EstadosMensajes.SinConsultaBD)

        Panel_Mensaje_Bien.Visible = False
        Panel_Mensaje_Mal.Visible = False
        'img_Mensaje2.ImageUrl = "iconos/MensajeMal.png"
        lblMensaje_Mal.Text = ""
        lblMensaje_Bien.Text = ""
        If EstadosMensajesT = EstadosMensajes.Guardado Then
            Panel_Mensaje_Bien.Visible = True
            lblMensaje_Bien.Text = sMensaje
        ElseIf EstadosMensajesT = EstadosMensajes.Actualizado Then
            lblMensaje_Bien.Text = sMensaje
            Panel_Mensaje_Bien.Visible = True
        ElseIf EstadosMensajesT = EstadosMensajes.Eliminado Then
            lblMensaje_Bien.Text = sMensaje
            Panel_Mensaje_Bien.Visible = True
        ElseIf EstadosMensajesT = EstadosMensajes.Imprimir Then
            lblMensaje_Bien.Text = sMensaje
            Panel_Mensaje_Bien.Visible = True
        ElseIf EstadosMensajesT = EstadosMensajes.ErrorGenerado Then
            lblMensaje_Mal.Text = sMensaje
            Panel_Mensaje_Mal.Visible = True
        ElseIf sMensaje <> "" Then
            lblMensaje_Bien.Text = sMensaje
            Panel_Mensaje_Bien.Visible = True
        ElseIf sMensaje = "" Then
            lblMensaje_Mal.Text = ""
            Panel_Mensaje_Mal.Visible = False
            lblMensaje_Bien.Text = ""
            Panel_Mensaje_Bien.Visible = False

        End If
    End Sub

    Private Sub Cargar_Registro()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object

        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla, sCamposTabla, sLlaves)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            Else
                txt_02_T01Id_Reporte.Text = coleccionDatos(0)
                txt_02_T01FechaRegistro.Text = coleccionDatos(1)
                txt_02_T01DocUsuarioRegistra.Text = coleccionDatos(2)
                txt_02_T01Fecha.Text = coleccionDatos(3)
                txt_02_T01Hora.Text = coleccionDatos(4)
                drop_02_T01Id_TipoAccion.Text = coleccionDatos(5)
                txt_02_T01LugarEvento.Text = coleccionDatos(6)
                txt_02_T01DescripcionEvento.Text = coleccionDatos(7)
                txt_02_T01SugerenciaEvento.Text = coleccionDatos(8)
                drop_02_T01Id_Area.Text = coleccionDatos(9)
                drop_02_T01Id_Sede.Text = coleccionDatos(10)
                txt_02_T01Dependenia.Text = coleccionDatos(11)
                txt_02_T01PersonaReporta_Nombre.Text = coleccionDatos(12)
                txt_02_T01PersonaReporta_Contacto.Text = coleccionDatos(13)
                'If clsAdminDb.sMostrarFecha(coleccionDatos(1)) <> " Then
                'txtFechaIngEmpresa.Text = CDate(coleccionDatos(1))
                'End If
                filtro()
                bHabilitarControles(False)

            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If
    End Sub


    Private Sub Guardar_Registro()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0

        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla, sLlaves)
        If lCantRegistros = 0 Then
            txt_02_T01Id_Reporte.Text = Iconsecutivo()
            Datos_Modulo()
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla, sCamposTabla, sCamposINS)
            If bSqlInsert = True Then
                MostrarMensaje("Se inserto nuevo registro correctamente ", True)
                bHabilitarControles(False)
                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else

            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla, sCamposUPD, sLlaves)
            If bSqlInsert = True Then
                MostrarMensaje("Se actualizo correctamente este registro", True)
                Registro_Procesos("Actualizar", clsAdminDb.Mostrar_Consulta)
                bHabilitarControles(False)
                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If
        End If
    End Sub
    Private Sub Registro_Procesos(ByVal sProceso, sQuery)

    End Sub

    Private Sub cargar_Tabla(Optional ByVal sLlaveTem As String = "")
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection
        'Selec _02_T01Fecha,_02_T01DocUsuarioRegistra, _02_T02Nombre,_02_T01LugarEvento 
        'From _02_T01REPORTE_CASOS, _02_T02TIPO_ACCION
        'Where _02_T02Id = _02_T01Id_TipoAccion


        Dim sCamposTablaLocal As String = "_02_T01Fecha,_02_T01DocUsuarioRegistra, _02_T02Nombre,_02_T01LugarEvento,_02_T01Id_Reporte"
        Dim sLlavesLocal As String = "_02_T02Id = _02_T01Id_TipoAccion and _02_T01Fecha=_01_T22Fecha"
        Dim sTablaLocal As String = "_02_T01REPORTE_CASOS, _02_T02TIPO_ACCION, SERINGTEC.dbo._01_T22AGNOS"
        Dim ArregloSingular() As String
        Dim lfechaInicial As Integer = 0
        Dim lfechaFinal As Integer = 0
        bodytabla.Controls.Clear()
        If txtFechaInicial.Text = "" And txtFechaFinal.Text = "" And dropTipoAccion.Text = "" Then
            Exit Sub
        End If

        Try
            lfechaInicial = Fix(CDate(txtFechaInicial.Text).ToOADate)
        Catch ex As Exception

        End Try
        Try
            lfechaFinal = Fix(CDate(txtFechaFinal.Text).ToOADate)
        Catch ex As Exception

        End Try

        If lfechaInicial <> 0 And lfechaFinal <> 0 Then
            sLlavesLocal = sLlavesLocal & " and  (_01_T22FechaNumero>=" & lfechaInicial & " and _01_T22FechaNumero<=" & lfechaFinal & ")"
        Else
            Exit Sub
        End If


        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
            If Trim("colocar filtro o validaciones") <> "" Then
                sLlavesLocal = sLlavesLocal
            End If
        End If
        Dim j As Integer = 0
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow
                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                'linkbutonHtml.ID = ArregloSingular(0).ToString & "sep" & ArregloSingular(1).ToString & "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = "Eliminar"
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'FilaHtml.Cells.Add(CeldaHtml)
                For i = 0 To ArregloSingular.Count - 3
                    CeldaHtml = New HtmlTableCell
                    linkbutonHtml = New LinkButton
                    'linkbutonHtml.ID = i  & "sep" &  ArregloSingular(0).ToString & "sep" & ArregloSingular(1).ToString & "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                    AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                    linkbutonHtml.Text = ArregloSingular(i)
                    linkbutonHtml.Enabled = False
                    CeldaHtml.Controls.Add(linkbutonHtml)
                    FilaHtml.Cells.Add(CeldaHtml)
                Next
                CeldaHtml = New HtmlTableCell
                HyperlinkHtml = New HyperLink
                HyperlinkHtml.Target = "_blank"
                HyperlinkHtml.ID = j & "zza-sep" & ArregloSingular(0).ToString & "sep" & ArregloSingular(1).ToString & "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                HyperlinkHtml.NavigateUrl = "reportesCasos.aspx?ID=" & ArregloSingular(4)
                HyperlinkHtml.Text = "ver registro"
                HyperlinkHtml.ForeColor = Drawing.Color.White
                CeldaHtml.BgColor = "#E8B617"
                CeldaHtml.BorderColor = "white"
                CeldaHtml.Controls.Add(HyperlinkHtml)
                FilaHtml.Cells.Add(CeldaHtml)


                bodytabla.Controls.Add(FilaHtml)
                j = j + 1

            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)


            End If
        End If
    End Sub

    Private Sub linkbutonHtml_Eliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_Eliminar.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(1).ToString
        Dim sLlaves_Tem = _01_T37TIPOACTIVIDADES.CampoLlave_01_T37Codigo & "='" & sllave1 & "'"


        Dim bSqlDelete = clsAdminDb.sql_Delete(sNombreTabla, sLlaves_Tem)
        If bSqlDelete = True Then
            MostrarMensaje("se elimino correctamente")
            Registro_Procesos("Eliminar", clsAdminDb.Mostrar_Consulta)

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else
                MostrarMensaje("No se encontraron datos ")
            End If
        End If

        cargar_Tabla()
    End Sub


    Private Sub linkbutonHtml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(1).ToString
        'Dim sllave2 As String = TestArray(2).ToString
        'Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        sLlaves = _01_T37TIPOACTIVIDADES.CampoLlave_01_T37Codigo & "='" & sllave1 & "'"
        'sLlaves = sLlaves & " and " & _01_T20TIPODOCENTREGABLES.CampoLlave_01_T20Codigo & "='" & sllave2 & "'"
        Cargar_Registro()
        filtro()
    End Sub

    Private Sub filtro()
        If Trim(txtFiltroBusqueda.Text) <> "" Then
            Dim texto As String = Replace(txtFiltroBusqueda.Text, " ", ".*.")
            Dim TestArray() As String = Split(texto, ".*.")
            Dim sllaveTem As String = ""
            Dim bCargar As Boolean = False
            For i = 0 To TestArray.Length - 1
                If sllaveTem = "" Then
                    sllaveTem = sllaveTem & "("
                    sllaveTem = sllaveTem & _01_T37TIPOACTIVIDADES.CampoLlave_01_T37Codigo & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _01_T37TIPOACTIVIDADES.Campo_01_T37Nombre & " like '" & TestArray(i).ToString & "%'"
                Else
                    sllaveTem = sllaveTem & " Or " & _01_T37TIPOACTIVIDADES.CampoLlave_01_T37Codigo & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _01_T37TIPOACTIVIDADES.Campo_01_T37Nombre & " like '" & TestArray(i).ToString & "%'"
                End If
                bCargar = True
            Next
            sllaveTem = sllaveTem & ")"
            cargar_Tabla(sllaveTem)
        Else
            cargar_Tabla()
        End If
    End Sub

    Private Sub txtFiltroBusqueda_TextChanged(sender As Object, e As EventArgs) Handles txtFiltroBusqueda.TextChanged
        filtro()
    End Sub

    Private Function Iconsecutivo() As Integer
        Dim iVigencia As String = Now.Year
        Dim sConsecutivo As String = ""
        sConsecutivo = clsAdminDb.sql_Max(_02_T01REPORTE_CASOS.NombreTabla, "cast(_02_T01Id_Reporte as integer)", "_02_T01Id_Reporte<>''")
        If sConsecutivo = "0" Then
            sConsecutivo = iVigencia & "1"
        Else
            sConsecutivo = Val(sConsecutivo) + 1
        End If
        Iconsecutivo = sConsecutivo
    End Function

    Private Sub txtFechaInicial_TextChanged(sender As Object, e As EventArgs) Handles txtFechaInicial.TextChanged
        cargar_Tabla()
    End Sub

    Private Sub txtFechaFinal_TextChanged(sender As Object, e As EventArgs) Handles txtFechaFinal.TextChanged
        cargar_Tabla()
    End Sub

    Private Sub dropTipoAccion_TextChanged(sender As Object, e As EventArgs) Handles dropTipoAccion.TextChanged
        cargar_Tabla()
    End Sub
End Class


