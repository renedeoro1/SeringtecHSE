Imports System.IO

Public Class ATL2
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
        sCodModulo = _04_T10ATL.CodigoModulo
        sNombreTabla = _04_T10ATL.NombreTabla
        sCamposTabla = _04_T10ATL.CamposTabla
        sCamposINS = "'" & drop_04_T10Proceso.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10Consecutivo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10Fecha.Text) & "'" & "," & "'" & drop_04_T10TipoAccion.Text & "'" & "," & "'" & drop_04_T10OrigenMejora.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10DescripcionHallazgo.Text) & "'" & "," & "'" & drop_04_T10REsponsableAccion.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10Causas.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10AccionesCorrectivas.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10AccionesPreventivas.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10FechaInicio.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10FechaCierre.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10FechaVerificacion.Text) & "'" & "," & "'" & drop_04_T10SeCierraAccion.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10FechaRealCierre.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10DiasAtraso.Text) & "'" & "," & "'" & drop_04_T10Estado.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10Observaciones.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10FechaRegistro.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_04_T10NumDocReggistra.Text) & "'"
        sCamposUPD = _04_T10ATL.Campo_04_T10Fecha & "='" & clsAdminDb.sRemoverHTML(txt_04_T10Fecha.Text) & "'" & "," & _04_T10ATL.Campo_04_T10TipoAccion & "='" & drop_04_T10TipoAccion.Text & "'" & "," & _04_T10ATL.Campo_04_T10OrigenMejora & "='" & drop_04_T10OrigenMejora.Text & "'" & "," & _04_T10ATL.Campo_04_T10DescripcionHallazgo & "='" & clsAdminDb.sRemoverHTML(txt_04_T10DescripcionHallazgo.Text) & "'" & "," & _04_T10ATL.Campo_04_T10REsponsableAccion & "='" & drop_04_T10REsponsableAccion.Text & "'" & "," & _04_T10ATL.Campo_04_T10Causas & "='" & clsAdminDb.sRemoverHTML(txt_04_T10Causas.Text) & "'" & "," & _04_T10ATL.Campo_04_T10AccionesCorrectivas & "='" & clsAdminDb.sRemoverHTML(txt_04_T10AccionesCorrectivas.Text) & "'" & "," & _04_T10ATL.Campo_04_T10AccionesPreventivas & "='" & clsAdminDb.sRemoverHTML(txt_04_T10AccionesPreventivas.Text) & "'" & "," & _04_T10ATL.Campo_04_T10FechaInicio & "='" & clsAdminDb.sRemoverHTML(txt_04_T10FechaInicio.Text) & "'" & "," & _04_T10ATL.Campo_04_T10FechaCierre & "='" & clsAdminDb.sRemoverHTML(txt_04_T10FechaCierre.Text) & "'" & "," & _04_T10ATL.Campo_04_T10FechaVerificacion & "='" & clsAdminDb.sRemoverHTML(txt_04_T10FechaVerificacion.Text) & "'" & "," & _04_T10ATL.Campo_04_T10SeCierraAccion & "='" & drop_04_T10SeCierraAccion.Text & "'" & "," & _04_T10ATL.Campo_04_T10FechaRealCierre & "='" & clsAdminDb.sRemoverHTML(txt_04_T10FechaRealCierre.Text) & "'" & "," & _04_T10ATL.Campo_04_T10DiasAtraso & "='" & clsAdminDb.sRemoverHTML(txt_04_T10DiasAtraso.Text) & "'" & "," & _04_T10ATL.Campo_04_T10Estado & "='" & drop_04_T10Estado.Text & "'" & "," & _04_T10ATL.Campo_04_T10Observaciones & "='" & clsAdminDb.sRemoverHTML(txt_04_T10Observaciones.Text) & "'" & "," & _04_T10ATL.Campo_04_T10FechaRegistro & "='" & clsAdminDb.sRemoverHTML(txt_04_T10FechaRegistro.Text) & "'" & "," & _04_T10ATL.Campo_04_T10NumDocReggistra & "='" & clsAdminDb.sRemoverHTML(txt_04_T10NumDocReggistra.Text) & "'"
        sLlaves = _04_T10ATL.CampoLlave_04_T10Proceso & "=  '" & drop_04_T10Proceso.Text & "'" & " AND " & _04_T10ATL.CampoLlave_04_T10Consecutivo & "=  '" & txt_04_T10Consecutivo.Text & "'"
    End Sub



    Private Sub CargarDrops()
        clsAdminDb = New adminitradorDB
        With clsAdminDb
            .Lit_CargarLiteral(LitHead, LitSuperior, LitIzquierdo, sCod_Aplicacion)
            '.drop_CargarCombox(dropEjemplo, "NombreTabla","CampoCodigo" & "CampoNombre", True)
            'If .Mostrar_Error <> """ Then
            'MostrarMensaje(.Mostrar_Error)
            'Exit Sub
            'End If

            '.drop_Cargar_SINO(dropTipoRiesgoQuimicos, False)
            'dropiluminacionInadecuada.Items.Add(New ListItem(", "))
            'dropiluminacionInadecuada.Items.Add(New ListItem("texto", "valor"))


            Dim sCampos_tem3 = _04_T13ATL_ORIGENMEJORA.CampoLlave_04_T13Codigo & "," & _04_T13ATL_ORIGENMEJORA.Campo_04_T13Nombre
            Dim stables_Tem3 = _04_T13ATL_ORIGENMEJORA.NombreTabla
            Dim sllaves_Tem3 = "_04_T13Activo='Si'"
            .drop_CargarCombox(drop_04_T10OrigenMejora, stables_Tem3, sCampos_tem3, sllaves_Tem3)



            drop_04_T10Estado.Items.Add(New ListItem("Seleccionar"))
            drop_04_T10Estado.Items.Add(New ListItem("Abierto"))
            drop_04_T10Estado.Items.Add(New ListItem("Cerrado"))
            drop_04_T10Estado.Items.Add(New ListItem("Vacías"))

            Dim sCampos_tem1 = _04_T11ATL_PROCESO.CampoLlave_04_T11Codigo & "," & _04_T11ATL_PROCESO.Campo_04_T11Nombre
            Dim stables_Tem1 = _04_T11ATL_PROCESO.NombreTabla
            Dim sllaves_Tem1 = "_04_T11Activo='Si'"
            .drop_CargarCombox(drop_04_T10Proceso, stables_Tem1, sCampos_tem1, sllaves_Tem1)

            Dim sCampos_tem2 = _04_T14ATL_RESPONSABLE_ACCION.CampoLlave_04_T14Codigo & "," & _04_T14ATL_RESPONSABLE_ACCION.Campo_04_T14Nombre
            Dim stables_Tem2 = _04_T14ATL_RESPONSABLE_ACCION.NombreTabla
            Dim sllaves_Tem2 As String = "_04_T14Activo='Si'"
            .drop_CargarCombox(drop_04_T10REsponsableAccion, stables_Tem2, sCampos_tem2, sllaves_Tem2)

            drop_04_T10SeCierraAccion.Items.Add(New ListItem("Seleccionar"))
            drop_04_T10SeCierraAccion.Items.Add(New ListItem("Si"))
            drop_04_T10SeCierraAccion.Items.Add(New ListItem("No"))
            drop_04_T10SeCierraAccion.Items.Add(New ListItem("Vacías"))


            drop_04_T10TipoAccion.Items.Add(New ListItem("Seleccionar"))
            drop_04_T10TipoAccion.Items.Add(New ListItem("Acción correctiva"))
            drop_04_T10TipoAccion.Items.Add(New ListItem("Acción de mejora"))
            drop_04_T10TipoAccion.Items.Add(New ListItem("Acción preventiva"))
            drop_04_T10TipoAccion.Items.Add(New ListItem("Vacías"))


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

    Private Sub Elminar_Registro()
        Dim clsAdminDb As New adminitradorDB
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
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else
                MostrarMensaje("No se encontraron datos ")
            End If
        End If
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        limpiarCampos()
    End Sub


    Private Sub limpiarCampos()

        drop_04_T10Proceso.Text = Nothing
        txt_04_T10Consecutivo.Text = Nothing
        txt_04_T10Fecha.Text = Nothing
        drop_04_T10TipoAccion.Text = Nothing
        drop_04_T10OrigenMejora.Text = Nothing
        txt_04_T10DescripcionHallazgo.Text = Nothing
        drop_04_T10REsponsableAccion.Text = Nothing
        txt_04_T10Causas.Text = Nothing
        txt_04_T10AccionesCorrectivas.Text = Nothing
        txt_04_T10AccionesPreventivas.Text = Nothing
        txt_04_T10FechaInicio.Text = Nothing
        txt_04_T10FechaCierre.Text = Nothing
        txt_04_T10FechaVerificacion.Text = Nothing
        drop_04_T10SeCierraAccion.Text = Nothing
        txt_04_T10FechaRealCierre.Text = Nothing
        txt_04_T10DiasAtraso.Text = Nothing
        drop_04_T10Estado.Text = Nothing
        txt_04_T10Observaciones.Text = Nothing
        txt_04_T10FechaRegistro.Text = Nothing
        txt_04_T10NumDocReggistra.Text = Nothing

        lbl_04_T10Proceso.ForeColor = Drawing.Color.Black
        lbl_04_T10Consecutivo.ForeColor = Drawing.Color.Black
        lbl_04_T10Fecha.ForeColor = Drawing.Color.Black
        lbl_04_T10TipoAccion.ForeColor = Drawing.Color.Black
        lbl_04_T10OrigenMejora.ForeColor = Drawing.Color.Black
        lbl_04_T10DescripcionHallazgo.ForeColor = Drawing.Color.Black
        lbl_04_T10REsponsableAccion.ForeColor = Drawing.Color.Black
        lbl_04_T10Causas.ForeColor = Drawing.Color.Black
        lbl_04_T10AccionesCorrectivas.ForeColor = Drawing.Color.Black
        lbl_04_T10AccionesPreventivas.ForeColor = Drawing.Color.Black
        lbl_04_T10FechaInicio.ForeColor = Drawing.Color.Black
        lbl_04_T10FechaCierre.ForeColor = Drawing.Color.Black
        lbl_04_T10FechaVerificacion.ForeColor = Drawing.Color.Black
        lbl_04_T10SeCierraAccion.ForeColor = Drawing.Color.Black
        lbl_04_T10FechaRealCierre.ForeColor = Drawing.Color.Black
        lbl_04_T10DiasAtraso.ForeColor = Drawing.Color.Black
        lbl_04_T10Estado.ForeColor = Drawing.Color.Black
        lbl_04_T10Observaciones.ForeColor = Drawing.Color.Black
        lbl_04_T10FechaRegistro.ForeColor = Drawing.Color.Black
        lbl_04_T10NumDocReggistra.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing

        MostrarMensaje("")
        bHabilitarControles(True)
        filtro()
    End Sub


    Private Sub bHabilitarControles(ByVal bEstado As Boolean)
        drop_04_T10Proceso.Enabled = bEstado
        txt_04_T10Consecutivo.Enabled = bEstado
        txt_04_T10Fecha.Enabled = bEstado
        drop_04_T10TipoAccion.Enabled = bEstado
        drop_04_T10OrigenMejora.Enabled = bEstado
        txt_04_T10DescripcionHallazgo.Enabled = bEstado
        drop_04_T10REsponsableAccion.Enabled = bEstado
        txt_04_T10Causas.Enabled = bEstado
        txt_04_T10AccionesCorrectivas.Enabled = bEstado
        txt_04_T10AccionesPreventivas.Enabled = bEstado
        txt_04_T10FechaInicio.Enabled = bEstado
        txt_04_T10FechaCierre.Enabled = bEstado
        txt_04_T10FechaVerificacion.Enabled = bEstado
        drop_04_T10SeCierraAccion.Enabled = bEstado
        txt_04_T10FechaRealCierre.Enabled = bEstado
        txt_04_T10DiasAtraso.Enabled = bEstado
        drop_04_T10Estado.Enabled = bEstado
        txt_04_T10Observaciones.Enabled = bEstado
        txt_04_T10FechaRegistro.Enabled = bEstado
        txt_04_T10NumDocReggistra.Enabled = bEstado
        btnEliminar.Visible = False
        btnImprimir.Visible = False
    End Sub

    Private Function bValidarCampos() As Boolean
        bValidarCampos = False

        lbl_04_T10Proceso.ForeColor = Drawing.Color.Red
        lbl_04_T10Consecutivo.ForeColor = Drawing.Color.Red
        lbl_04_T10Fecha.ForeColor = Drawing.Color.Red
        lbl_04_T10TipoAccion.ForeColor = Drawing.Color.Red
        lbl_04_T10OrigenMejora.ForeColor = Drawing.Color.Red
        lbl_04_T10DescripcionHallazgo.ForeColor = Drawing.Color.Red
        lbl_04_T10REsponsableAccion.ForeColor = Drawing.Color.Red
        lbl_04_T10Causas.ForeColor = Drawing.Color.Red
        lbl_04_T10AccionesCorrectivas.ForeColor = Drawing.Color.Red
        lbl_04_T10AccionesPreventivas.ForeColor = Drawing.Color.Red
        lbl_04_T10FechaInicio.ForeColor = Drawing.Color.Red
        lbl_04_T10FechaCierre.ForeColor = Drawing.Color.Red
        lbl_04_T10FechaVerificacion.ForeColor = Drawing.Color.Red
        lbl_04_T10SeCierraAccion.ForeColor = Drawing.Color.Red
        lbl_04_T10FechaRealCierre.ForeColor = Drawing.Color.Red
        lbl_04_T10DiasAtraso.ForeColor = Drawing.Color.Red
        lbl_04_T10Estado.ForeColor = Drawing.Color.Red
        lbl_04_T10Observaciones.ForeColor = Drawing.Color.Red
        lbl_04_T10FechaRegistro.ForeColor = Drawing.Color.Red
        lbl_04_T10NumDocReggistra.ForeColor = Drawing.Color.Red
        'If Trim(drop_04_T10Proceso.Text) = "" Then
        '    drop_04_T10Proceso.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10Proceso")
        '    Exit Function
        'Else
        '    lbl_04_T10Proceso.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10Consecutivo.Text) = "" Then
        '    txt_04_T10Consecutivo.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10Consecutivo")
        '    Exit Function
        'Else
        '    lbl_04_T10Consecutivo.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10Fecha.Text) = "" Then
        '    txt_04_T10Fecha.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10Fecha")
        '    Exit Function
        'Else
        '    lbl_04_T10Fecha.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_04_T10TipoAccion.Text) = "" Then
        '    drop_04_T10TipoAccion.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10TipoAccion")
        '    Exit Function
        'Else
        '    lbl_04_T10TipoAccion.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_04_T10OrigenMejora.Text) = "" Then
        '    drop_04_T10OrigenMejora.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10OrigenMejora")
        '    Exit Function
        'Else
        '    lbl_04_T10OrigenMejora.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10DescripcionHallazgo.Text) = "" Then
        '    txt_04_T10DescripcionHallazgo.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10DescripcionHallazgo")
        '    Exit Function
        'Else
        '    lbl_04_T10DescripcionHallazgo.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_04_T10REsponsableAccion.Text) = "" Then
        '    drop_04_T10REsponsableAccion.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10REsponsableAccion")
        '    Exit Function
        'Else
        '    lbl_04_T10REsponsableAccion.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10Causas.Text) = "" Then
        '    txt_04_T10Causas.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10Causas")
        '    Exit Function
        'Else
        '    lbl_04_T10Causas.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10AccionesCorrectivas.Text) = "" Then
        '    txt_04_T10AccionesCorrectivas.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10AccionesCorrectivas")
        '    Exit Function
        'Else
        '    lbl_04_T10AccionesCorrectivas.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10AccionesPreventivas.Text) = "" Then
        '    txt_04_T10AccionesPreventivas.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10AccionesPreventivas")
        '    Exit Function
        'Else
        '    lbl_04_T10AccionesPreventivas.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10FechaInicio.Text) = "" Then
        '    txt_04_T10FechaInicio.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10FechaInicio")
        '    Exit Function
        'Else
        '    lbl_04_T10FechaInicio.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10FechaCierre.Text) = "" Then
        '    txt_04_T10FechaCierre.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10FechaCierre")
        '    Exit Function
        'Else
        '    lbl_04_T10FechaCierre.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10FechaVerificacion.Text) = "" Then
        '    txt_04_T10FechaVerificacion.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10FechaVerificacion")
        '    Exit Function
        'Else
        '    lbl_04_T10FechaVerificacion.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_04_T10SeCierraAccion.Text) = "" Then
        '    drop_04_T10SeCierraAccion.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10SeCierraAccion")
        '    Exit Function
        'Else
        '    lbl_04_T10SeCierraAccion.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10FechaRealCierre.Text) = "" Then
        '    txt_04_T10FechaRealCierre.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10FechaRealCierre")
        '    Exit Function
        'Else
        '    lbl_04_T10FechaRealCierre.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10DiasAtraso.Text) = "" Then
        '    txt_04_T10DiasAtraso.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10DiasAtraso")
        '    Exit Function
        'Else
        '    lbl_04_T10DiasAtraso.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_04_T10Estado.Text) = "" Then
        '    drop_04_T10Estado.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10Estado")
        '    Exit Function
        'Else
        '    lbl_04_T10Estado.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10Observaciones.Text) = "" Then
        '    txt_04_T10Observaciones.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10Observaciones")
        '    Exit Function
        'Else
        '    lbl_04_T10Observaciones.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10FechaRegistro.Text) = "" Then
        '    txt_04_T10FechaRegistro.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10FechaRegistro")
        '    Exit Function
        'Else
        '    lbl_04_T10FechaRegistro.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_04_T10NumDocReggistra.Text) = "" Then
        '    txt_04_T10NumDocReggistra.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 10NumDocReggistra")
        '    Exit Function
        'Else
        '    lbl_04_T10NumDocReggistra.ForeColor = Drawing.Color.Black
        'End If

        bValidarCampos = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_04_T10Consecutivo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10Fecha.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10DescripcionHallazgo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10Causas.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10AccionesCorrectivas.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10AccionesPreventivas.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10FechaInicio.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10FechaCierre.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10FechaVerificacion.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10FechaRealCierre.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10DiasAtraso.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10Observaciones.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10FechaRegistro.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_04_T10NumDocReggistra.Text) Then Exit Function
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
                drop_04_T10Proceso.Text = coleccionDatos(0)
                txt_04_T10Consecutivo.Text = coleccionDatos(1)
                txt_04_T10Fecha.Text = coleccionDatos(2)
                drop_04_T10TipoAccion.Text = coleccionDatos(3)
                drop_04_T10OrigenMejora.Text = coleccionDatos(4)
                txt_04_T10DescripcionHallazgo.Text = coleccionDatos(5)
                drop_04_T10REsponsableAccion.Text = coleccionDatos(6)
                txt_04_T10Causas.Text = coleccionDatos(7)
                txt_04_T10AccionesCorrectivas.Text = coleccionDatos(8)
                txt_04_T10AccionesPreventivas.Text = coleccionDatos(9)
                txt_04_T10FechaInicio.Text = coleccionDatos(10)
                txt_04_T10FechaCierre.Text = coleccionDatos(11)
                txt_04_T10FechaVerificacion.Text = coleccionDatos(12)
                drop_04_T10SeCierraAccion.Text = coleccionDatos(13)
                txt_04_T10FechaRealCierre.Text = coleccionDatos(14)
                txt_04_T10DiasAtraso.Text = coleccionDatos(15)
                drop_04_T10Estado.Text = coleccionDatos(16)
                txt_04_T10Observaciones.Text = coleccionDatos(17)
                txt_04_T10FechaRegistro.Text = coleccionDatos(18)
                txt_04_T10NumDocReggistra.Text = coleccionDatos(19)
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
        clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim dFecha As Date
        Dim sFecha As String = ""
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = False Then
            MostrarMensaje("Error al guardar registro de procesos")
            Exit Sub
        End If
        sQuery = Replace(sQuery, "'", """")
        sQuery = Replace(sQuery, sCodModulo, "")
        Dim sNombreTabla_Procesos = sCod_Aplicacion & "T00REGISTROPROCESOS"
        Dim sCamposTabla_Procesos = sCod_Aplicacion & "T00CodModulo," & sCod_Aplicacion & "T00Proceso," & sCod_Aplicacion & "T00ConsultaEjecutada," & sCod_Aplicacion & "T00Fecha," & sCod_Aplicacion & "T00Hora," & sCod_Aplicacion & "T00Usuario"
        Dim sCamposINS_Procesos = "'" & sCodModulo & "','" & sProceso & "','" & sQuery & "','" & sFecha & "','" & Now.Hour & ":" & Now.Minute & "',' Documento:" & sTipoDoc & " " & sNumDoc & " - Usuario: " & sNombreUsuario & "'"

        bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla_Procesos, sCamposTabla_Procesos, sCamposINS_Procesos)
        If bSqlInsert = True Then

        End If
    End Sub

    Private Sub cargar_Tabla(Optional ByVal sLlaveTem As String = "")
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection
        Dim sCamposTablaLocal As String = "_04_T10Proceso,_04_T10Fecha,_04_T10TipoAccion,_04_T10OrigenMejora,_04_T10DescripcionHallazgo,_04_T10REsponsableAccion,_04_T10Causas,_04_T10AccionesCorrectivas,_04_T10AccionesPreventivas,_04_T10FechaInicio,_04_T10FechaCierre,_04_T10FechaVerificacion,_04_T10SeCierraAccion,_04_T10FechaRealCierre,_04_T10DiasAtraso,_04_T10Estado,_04_T10Observaciones"
        Dim sLlavesLocal As String = _04_T10ATL.CampoLlave_04_T10Proceso & "<>''"
        Dim sTablaLocal As String = _04_T10ATL.NombreTabla
        Dim ArregloSingular() As String
        bodytabla.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
            If Trim("colocar filtro o validaciones") <> "" Then
                sLlavesLocal = sLlavesLocal
            End If
        End If

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
                For i = 0 To ArregloSingular.Count - 2
                    CeldaHtml = New HtmlTableCell
                    linkbutonHtml = New LinkButton
                    'linkbutonHtml.ID = i  & "sep" &  ArregloSingular(0).ToString & "sep" & ArregloSingular(1).ToString & "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                    AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                    linkbutonHtml.Text = ArregloSingular(i)
                    CeldaHtml.Controls.Add(linkbutonHtml)
                    FilaHtml.Cells.Add(CeldaHtml)
                Next
                bodytabla.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else

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
        Dim TestArray() As String = Split(sTexto_ID, "sep")
        Dim sllave1 As String = TestArray(1).ToString
        'Dim sllave2 As String = TestArray(2).ToString
        'Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        sLlaves = "NombreCampoLlave" & "='" & sllave1 & "'"
        'sLlaves = sLlaves & " and " &   "NombreCampoLlave" & "='" & sllave2 & "'"
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
                    'sllaveTem = sllaveTem & "("
                    'sllaveTem = sllaveTem & _90_T01APLICACIONES.CampoLlave_90_T01Codigo & " like '" & TestArray(i).ToString & "%'"
                    'sllaveTem = sllaveTem & " Or " & _90_T01APLICACIONES.Campo_90_T01Nombre & " like '" & TestArray(i).ToString & "%'"
                Else
                    'sllaveTem = sllaveTem & _90_T01APLICACIONES.CampoLlave_90_T01Codigo & " like '" & TestArray(i).ToString & "%'"
                    'sllaveTem = sllaveTem & " Or " & _90_T01APLICACIONES.Campo_90_T01Nombre & " like '" & TestArray(i).ToString & "%'"
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


End Class


