Imports System.IO

Public Class InformeMensual
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

    Dim rptCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument()


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
                lblUsuarioLogueado_Nombre2.Text = TestArray(1).ToString
                drop_02_T15Vigencia.AutoPostBack = True
                drop_02_T15ODS.AutoPostBack = True
                drop_02_T15AreaEcopetrolHUB.AutoPostBack = True
                drop_02_T15AreaEcopetrolResponsable.AutoPostBack = True
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
        sCodModulo = _02_T15INFORMEMENSUAL.CodigoModulo
        sNombreTabla = _02_T15INFORMEMENSUAL.NombreTabla
        sCamposTabla = _02_T15INFORMEMENSUAL.CamposTabla
        sCamposINS = "'" & drop_02_T15Vigencia.Text & "'" & "," & "'" & drop_02_T15Mes.Text & "'" & "," & "'" & drop_02_T15ODS.Text & "'" & "," & "'" & drop_02_T15AreaEcopetrolResponsable.Text & "'" & "," & "'" & drop_02_T15AreaEcopetrolHUB.Text & "'" & "," & "'" & drop_02_T15Departamento.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15NumeroTrabajadores.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15HTT.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15HH.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15NumIncidentesOcupacionales.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15NumIncidentesAmbientales.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15NumIncidentesVehiculares.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15NumIncidentesSeguridadProceso.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15AvancePlanHSE.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15AseguramientoComportamiento.text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15KMRecorridos.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15CantidadVehiculos.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15NumCasosEnfermedadDiagnosticada.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15PrevalenciEnfLaboral.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15IncidenciaEnfLaboral.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15Ausentismo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15FechaRegistro.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15NumDocFuncionarioRegistra.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T15Estado.Text) & "'"
        sCamposUPD = _02_T15INFORMEMENSUAL.Campo_02_T15Departamento & "='" & drop_02_T15Departamento.Text & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15NumeroTrabajadores & "='" & clsAdminDb.sRemoverHTML(txt_02_T15NumeroTrabajadores.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15HTT & "='" & clsAdminDb.sRemoverHTML(txt_02_T15HTT.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15HH & "='" & clsAdminDb.sRemoverHTML(txt_02_T15HH.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15NumIncidentesOcupacionales & "='" & clsAdminDb.sRemoverHTML(txt_02_T15NumIncidentesOcupacionales.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15NumIncidentesAmbientales & "='" & clsAdminDb.sRemoverHTML(txt_02_T15NumIncidentesAmbientales.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15NumIncidentesVehiculares & "='" & clsAdminDb.sRemoverHTML(txt_02_T15NumIncidentesVehiculares.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15NumIncidentesSeguridadProceso & "='" & clsAdminDb.sRemoverHTML(txt_02_T15NumIncidentesSeguridadProceso.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15AvancePlanHSE & "='" & clsAdminDb.sRemoverHTML(txt_02_T15AvancePlanHSE.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15AseguramientoComportamiento & "='" & clsAdminDb.sRemoverHTML(txt_02_T15AseguramientoComportamiento.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15KMRecorridos & "='" & clsAdminDb.sRemoverHTML(txt_02_T15KMRecorridos.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15CantidadVehiculos & "='" & clsAdminDb.sRemoverHTML(txt_02_T15CantidadVehiculos.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15NumCasosEnfermedadDiagnosticada & "='" & clsAdminDb.sRemoverHTML(txt_02_T15NumCasosEnfermedadDiagnosticada.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15PrevalenciEnfLaboral & "='" & clsAdminDb.sRemoverHTML(txt_02_T15PrevalenciEnfLaboral.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15IncidenciaEnfLaboral & "='" & clsAdminDb.sRemoverHTML(txt_02_T15IncidenciaEnfLaboral.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15Ausentismo & "='" & clsAdminDb.sRemoverHTML(txt_02_T15Ausentismo.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15FechaRegistro & "='" & clsAdminDb.sRemoverHTML(txt_02_T15FechaRegistro.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15NumDocFuncionarioRegistra & "='" & clsAdminDb.sRemoverHTML(txt_02_T15NumDocFuncionarioRegistra.Text) & "'" & "," & _02_T15INFORMEMENSUAL.Campo_02_T15Estado & "='" & clsAdminDb.sRemoverHTML(txt_02_T15Estado.Text) & "'"
        sLlaves = _02_T15INFORMEMENSUAL.CampoLlave_02_T15Vigencia & "=  '" & drop_02_T15Vigencia.Text & "'" & " AND " & _02_T15INFORMEMENSUAL.CampoLlave_02_T15Mes & "=  '" & drop_02_T15Mes.Text & "'" & " AND " & _02_T15INFORMEMENSUAL.CampoLlave_02_T15ODS & "=  '" & drop_02_T15ODS.Text & "'" & " AND " & _02_T15INFORMEMENSUAL.CampoLlave_02_T15AreaEcopetrolResponsable & "=  '" & drop_02_T15AreaEcopetrolResponsable.Text & "'" & " AND " & _02_T15INFORMEMENSUAL.CampoLlave_02_T15AreaEcopetrolHUB & "=  '" & drop_02_T15AreaEcopetrolHUB.Text & "'"
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

            drop_02_T15Mes.Items.Add(New ListItem("1"))
            drop_02_T15Mes.Items.Add(New ListItem("2"))
            drop_02_T15Mes.Items.Add(New ListItem("3"))
            drop_02_T15Mes.Items.Add(New ListItem("4"))
            drop_02_T15Mes.Items.Add(New ListItem("5"))
            drop_02_T15Mes.Items.Add(New ListItem("6"))
            drop_02_T15Mes.Items.Add(New ListItem("7"))
            drop_02_T15Mes.Items.Add(New ListItem("8"))
            drop_02_T15Mes.Items.Add(New ListItem("9"))
            drop_02_T15Mes.Items.Add(New ListItem("10"))
            drop_02_T15Mes.Items.Add(New ListItem("11"))
            drop_02_T15Mes.Items.Add(New ListItem("12"))


            drop_02_T15Vigencia.Items.Add(New ListItem("seleccionar", ""))
            For vigenciainicial = 2021 To Now.Year
                drop_02_T15Vigencia.Items.Add(New ListItem(vigenciainicial, vigenciainicial))
            Next




            drop_02_T15AreaEcopetrolResponsable.Items.Add(New ListItem("Seleccionar"))
            drop_02_T15AreaEcopetrolResponsable.Items.Add(New ListItem("GERENCIA DE INGENIERIA DE PROYECTOS"))


            Dim sCampos_tem = _99_T09DEPARTAMENTOS.CampoLlave_99_T09Codigo & "," & _99_T09DEPARTAMENTOS.Campo_99_T09Nombre
            Dim stables_Tem = _99_T09DEPARTAMENTOS.NombreTabla
            Dim sllaves_Tem = _99_T09DEPARTAMENTOS.Campo_99_T09Activo & "= 'SI' " & " and " & _99_T09DEPARTAMENTOS.CampoLlave_99_T09CodPais & " ='169' "
            .drop_CargarCombox(drop_02_T15Departamento, stables_Tem, sCampos_tem, sllaves_Tem, True,, _99_T09DEPARTAMENTOS.Campo_99_T09Nombre)

            Dim sCampos_tem2 = _02_T16HUB.CampoLlave_02_T16Codigo & "," & _02_T16HUB.Campo_02_T16Nombre
            Dim stables_Tem2 = _02_T16HUB.NombreTabla
            Dim sllaves_Tem2 = _02_T16HUB.Campo_02_T16Activo & "= 'SI' "
            .drop_CargarCombox(drop_02_T15AreaEcopetrolHUB, stables_Tem2, sCampos_tem2, sllaves_Tem2, True,, _02_T16HUB.Campo_02_T16Nombre)


            sCampos_tem2 = "_01_T09Codigo as A,_01_T09Codigo as B"
            stables_Tem2 = "SERINGTEC.dbo._01_T09ODS"
            sllaves_Tem2 = "_01_T09Activo= 'SI' "
            .drop_CargarCombox(drop_02_T15ODS, stables_Tem2, sCampos_tem2, sllaves_Tem2, True,, "A")


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

        drop_02_T15Vigencia.Text = Nothing
        drop_02_T15Mes.Text = Nothing
        drop_02_T15ODS.Text = Nothing
        drop_02_T15AreaEcopetrolResponsable.Text = Nothing
        drop_02_T15AreaEcopetrolHUB.Text = Nothing
        drop_02_T15Departamento.Text = Nothing
        txt_02_T15NumeroTrabajadores.Text = Nothing
        txt_02_T15HTT.Text = Nothing
        txt_02_T15HH.Text = Nothing
        txt_02_T15NumIncidentesOcupacionales.Text = Nothing
        txt_02_T15NumIncidentesAmbientales.Text = Nothing
        txt_02_T15NumIncidentesVehiculares.Text = Nothing
        txt_02_T15NumIncidentesSeguridadProceso.Text = Nothing
        txt_02_T15AvancePlanHSE.Text = Nothing
        txt_02_T15AseguramientoComportamiento.text = Nothing
        txt_02_T15KMRecorridos.Text = Nothing
        txt_02_T15CantidadVehiculos.Text = Nothing
        txt_02_T15NumCasosEnfermedadDiagnosticada.Text = Nothing
        txt_02_T15PrevalenciEnfLaboral.Text = Nothing
        txt_02_T15IncidenciaEnfLaboral.Text = Nothing
        txt_02_T15Ausentismo.Text = Nothing
        txt_02_T15FechaRegistro.Text = Nothing
        txt_02_T15NumDocFuncionarioRegistra.Text = Nothing
        txt_02_T15Estado.Text = Nothing

        lbl_02_T15Vigencia.ForeColor = Drawing.Color.Black
        lbl_02_T15Mes.ForeColor = Drawing.Color.Black
        lbl_02_T15ODS.ForeColor = Drawing.Color.Black
        lbl_02_T15AreaEcopetrolResponsable.ForeColor = Drawing.Color.Black
        lbl_02_T15AreaEcopetrolHUB.ForeColor = Drawing.Color.Black
        lbl_02_T15Departamento.ForeColor = Drawing.Color.Black
        lbl_02_T15NumeroTrabajadores.ForeColor = Drawing.Color.Black
        lbl_02_T15HTT.ForeColor = Drawing.Color.Black
        lbl_02_T15HH.ForeColor = Drawing.Color.Black
        lbl_02_T15NumIncidentesOcupacionales.ForeColor = Drawing.Color.Black
        lbl_02_T15NumIncidentesAmbientales.ForeColor = Drawing.Color.Black
        lbl_02_T15NumIncidentesVehiculares.ForeColor = Drawing.Color.Black
        lbl_02_T15NumIncidentesSeguridadProceso.ForeColor = Drawing.Color.Black
        lbl_02_T15AvancePlanHSE.ForeColor = Drawing.Color.Black
        lbl_02_T15AseguramientoComportamiento.ForeColor = Drawing.Color.Black
        lbl_02_T15KMRecorridos.ForeColor = Drawing.Color.Black
        lbl_02_T15CantidadVehiculos.ForeColor = Drawing.Color.Black
        lbl_02_T15NumCasosEnfermedadDiagnosticada.ForeColor = Drawing.Color.Black
        lbl_02_T15PrevalenciEnfLaboral.ForeColor = Drawing.Color.Black
        lbl_02_T15IncidenciaEnfLaboral.ForeColor = Drawing.Color.Black
        lbl_02_T15Ausentismo.ForeColor = Drawing.Color.Black
        lbl_02_T15FechaRegistro.ForeColor = Drawing.Color.Black
        lbl_02_T15NumDocFuncionarioRegistra.ForeColor = Drawing.Color.Black
        lbl_02_T15Estado.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing

        MostrarMensaje("")
        bHabilitarControles(True)
        filtro()
    End Sub



    Private Sub bHabilitarControles(ByVal bEstado As Boolean)
        'drop_02_T15Vigencia.Enabled = bEstado
        'drop_02_T15Mes.Enabled = bEstado
        'drop_02_T15ODS.Enabled = bEstado
        'drop_02_T15AreaEcopetrolResponsable.Enabled = bEstado
        'drop_02_T15AreaEcopetrolHUB.Enabled = bEstado
        'drop_02_T15Departamento.Enabled = bEstado
        'txt_02_T15NumeroTrabajadores.Enabled = bEstado
        'txt_02_T15HTT.Enabled = bEstado
        'txt_02_T15HH.Enabled = bEstado
        'txt_02_T15NumIncidentesOcupacionales.Enabled = bEstado
        'txt_02_T15NumIncidentesAmbientales.Enabled = bEstado
        'txt_02_T15NumIncidentesVehiculares.Enabled = bEstado
        'txt_02_T15NumIncidentesSeguridadProceso.Enabled = bEstado
        'txt_02_T15AvancePlanHSE.Enabled = bEstado
        'txt_02_T15AseguramientoComportamiento.Enabled = bEstado
        'txt_02_T15KMRecorridos.Enabled = bEstado
        'txt_02_T15CantidadVehiculos.Enabled = bEstado
        'txt_02_T15NumCasosEnfermedadDiagnosticada.Enabled = bEstado
        'txt_02_T15PrevalenciEnfLaboral.Enabled = bEstado
        'txt_02_T15IncidenciaEnfLaboral.Enabled = bEstado
        'txt_02_T15Ausentismo.Enabled = bEstado
        'txt_02_T15FechaRegistro.Enabled = bEstado
        'txt_02_T15NumDocFuncionarioRegistra.Enabled = bEstado
        'txt_02_T15Estado.Enabled = bEstado

        btnImprimir.Visible = True
        btnEliminar.Visible = False
        'btnImprimir.Visible = False
    End Sub

    Private Function bValidarCampos() As Boolean
        bValidarCampos = False

        lbl_02_T15Vigencia.ForeColor = Drawing.Color.Red
        lbl_02_T15Mes.ForeColor = Drawing.Color.Red
        lbl_02_T15ODS.ForeColor = Drawing.Color.Red
        lbl_02_T15AreaEcopetrolResponsable.ForeColor = Drawing.Color.Red
        lbl_02_T15AreaEcopetrolHUB.ForeColor = Drawing.Color.Red
        lbl_02_T15Departamento.ForeColor = Drawing.Color.Red
        lbl_02_T15NumeroTrabajadores.ForeColor = Drawing.Color.Red
        lbl_02_T15HTT.ForeColor = Drawing.Color.Red
        lbl_02_T15HH.ForeColor = Drawing.Color.Red
        lbl_02_T15NumIncidentesOcupacionales.ForeColor = Drawing.Color.Red
        lbl_02_T15NumIncidentesAmbientales.ForeColor = Drawing.Color.Red
        lbl_02_T15NumIncidentesVehiculares.ForeColor = Drawing.Color.Red
        lbl_02_T15NumIncidentesSeguridadProceso.ForeColor = Drawing.Color.Red
        lbl_02_T15AvancePlanHSE.ForeColor = Drawing.Color.Red
        lbl_02_T15AseguramientoComportamiento.ForeColor = Drawing.Color.Red
        lbl_02_T15KMRecorridos.ForeColor = Drawing.Color.Red
        lbl_02_T15CantidadVehiculos.ForeColor = Drawing.Color.Red
        lbl_02_T15NumCasosEnfermedadDiagnosticada.ForeColor = Drawing.Color.Red
        lbl_02_T15PrevalenciEnfLaboral.ForeColor = Drawing.Color.Red
        lbl_02_T15IncidenciaEnfLaboral.ForeColor = Drawing.Color.Red
        lbl_02_T15Ausentismo.ForeColor = Drawing.Color.Red
        lbl_02_T15FechaRegistro.ForeColor = Drawing.Color.Red
        lbl_02_T15NumDocFuncionarioRegistra.ForeColor = Drawing.Color.Red
        lbl_02_T15Estado.ForeColor = Drawing.Color.Red
        'If Trim(drop_02_T15Vigencia.Text) = "" Then
        '    drop_02_T15Vigencia.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15Vigencia")
        '    Exit Function
        'Else
        '    lbl_02_T15Vigencia.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_02_T15Mes.Text) = "" Then
        '    drop_02_T15Mes.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15Mes")
        '    Exit Function
        'Else
        '    lbl_02_T15Mes.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_02_T15ODS.Text) = "" Then
        '    drop_02_T15ODS.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15ODS")
        '    Exit Function
        'Else
        '    lbl_02_T15ODS.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_02_T15AreaEcopetrolResponsable.Text) = "" Then
        '    drop_02_T15AreaEcopetrolResponsable.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15AreaEcopetrolResponsable")
        '    Exit Function
        'Else
        '    lbl_02_T15AreaEcopetrolResponsable.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_02_T15AreaEcopetrolHUB.Text) = "" Then
        '    drop_02_T15AreaEcopetrolHUB.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15AreaEcopetrolHUB")
        '    Exit Function
        'Else
        '    lbl_02_T15AreaEcopetrolHUB.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_02_T15Departamento.Text) = "" Then
        '    drop_02_T15Departamento.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15Departamento")
        '    Exit Function
        'Else
        '    lbl_02_T15Departamento.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15NumeroTrabajadores.Text) = "" Then
        '    txt_02_T15NumeroTrabajadores.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15NumeroTrabajadores")
        '    Exit Function
        'Else
        '    lbl_02_T15NumeroTrabajadores.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15HTT.Text) = "" Then
        '    txt_02_T15HTT.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15HTT")
        '    Exit Function
        'Else
        '    lbl_02_T15HTT.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15HH.Text) = "" Then
        '    txt_02_T15HH.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15HH")
        '    Exit Function
        'Else
        '    lbl_02_T15HH.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15NumIncidentesOcupacionales.Text) = "" Then
        '    txt_02_T15NumIncidentesOcupacionales.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15NumIncidentesOcupacionales")
        '    Exit Function
        'Else
        '    lbl_02_T15NumIncidentesOcupacionales.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15NumIncidentesAmbientales.Text) = "" Then
        '    txt_02_T15NumIncidentesAmbientales.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15NumIncidentesAmbientales")
        '    Exit Function
        'Else
        '    lbl_02_T15NumIncidentesAmbientales.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15NumIncidentesVehiculares.Text) = "" Then
        '    txt_02_T15NumIncidentesVehiculares.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15NumIncidentesVehiculares")
        '    Exit Function
        'Else
        '    lbl_02_T15NumIncidentesVehiculares.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15NumIncidentesSeguridadProceso.Text) = "" Then
        '    txt_02_T15NumIncidentesSeguridadProceso.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15NumIncidentesSeguridadProceso")
        '    Exit Function
        'Else
        '    lbl_02_T15NumIncidentesSeguridadProceso.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15AvancePlanHSE.Text) = "" Then
        '    txt_02_T15AvancePlanHSE.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15AvancePlanHSE")
        '    Exit Function
        'Else
        '    lbl_02_T15AvancePlanHSE.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15AseguramientoComportamiento.Text) = "" Then
        '    txt_02_T15AseguramientoComportamiento.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15Aseguramiento Comportamiento")
        '    Exit Function
        'Else
        '    lbl_02_T15AseguramientoComportamiento.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15KMRecorridos.Text) = "" Then
        '    txt_02_T15KMRecorridos.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15KMRecorridos")
        '    Exit Function
        'Else
        '    lbl_02_T15KMRecorridos.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15CantidadVehiculos.Text) = "" Then
        '    txt_02_T15CantidadVehiculos.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15CantidadVehiculos")
        '    Exit Function
        'Else
        '    lbl_02_T15CantidadVehiculos.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15NumCasosEnfermedadDiagnosticada.Text) = "" Then
        '    txt_02_T15NumCasosEnfermedadDiagnosticada.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15NumCasosEnfermedadDiagnosticada")
        '    Exit Function
        'Else
        '    lbl_02_T15NumCasosEnfermedadDiagnosticada.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15PrevalenciEnfLaboral.Text) = "" Then
        '    txt_02_T15PrevalenciEnfLaboral.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15PrevalenciEnfLaboral")
        '    Exit Function
        'Else
        '    lbl_02_T15PrevalenciEnfLaboral.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15IncidenciaEnfLaboral.Text) = "" Then
        '    txt_02_T15IncidenciaEnfLaboral.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15IncidenciaEnfLaboral")
        '    Exit Function
        'Else
        '    lbl_02_T15IncidenciaEnfLaboral.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15Ausentismo.Text) = "" Then
        '    txt_02_T15Ausentismo.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15Ausentismo")
        '    Exit Function
        'Else
        '    lbl_02_T15Ausentismo.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15FechaRegistro.Text) = "" Then
        '    txt_02_T15FechaRegistro.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15FechaRegistro")
        '    Exit Function
        'Else
        '    lbl_02_T15FechaRegistro.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15NumDocFuncionarioRegistra.Text) = "" Then
        '    txt_02_T15NumDocFuncionarioRegistra.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15NumDocFuncionarioRegistra")
        '    Exit Function
        'Else
        '    lbl_02_T15NumDocFuncionarioRegistra.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T15Estado.Text) = "" Then
        '    txt_02_T15Estado.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 15Estado")
        '    Exit Function
        'Else
        '    lbl_02_T15Estado.ForeColor = Drawing.Color.Black
        'End If

        bValidarCampos = True
        MostrarMensaje("")
    End Function


    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_02_T15NumeroTrabajadores.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15HTT.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15HH.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15NumIncidentesOcupacionales.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15NumIncidentesAmbientales.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15NumIncidentesVehiculares.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15NumIncidentesSeguridadProceso.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15AvancePlanHSE.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15AseguramientoComportamiento.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15KMRecorridos.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15CantidadVehiculos.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15NumCasosEnfermedadDiagnosticada.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15PrevalenciEnfLaboral.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15IncidenciaEnfLaboral.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15Ausentismo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15FechaRegistro.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15NumDocFuncionarioRegistra.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T15Estado.Text) Then Exit Function
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
                drop_02_T15Vigencia.Text = coleccionDatos(0)
                drop_02_T15Mes.Text = coleccionDatos(1)
                drop_02_T15ODS.Text = coleccionDatos(2)
                drop_02_T15AreaEcopetrolResponsable.Text = coleccionDatos(3)
                drop_02_T15AreaEcopetrolHUB.Text = coleccionDatos(4)
                drop_02_T15Departamento.Text = coleccionDatos(5)
                txt_02_T15NumeroTrabajadores.Text = coleccionDatos(6)
                txt_02_T15HTT.Text = coleccionDatos(7)
                txt_02_T15HH.Text = coleccionDatos(8)
                txt_02_T15NumIncidentesOcupacionales.Text = coleccionDatos(9)
                txt_02_T15NumIncidentesAmbientales.Text = coleccionDatos(10)
                txt_02_T15NumIncidentesVehiculares.Text = coleccionDatos(11)
                txt_02_T15NumIncidentesSeguridadProceso.Text = coleccionDatos(12)
                txt_02_T15AvancePlanHSE.Text = coleccionDatos(13)
                txt_02_T15AseguramientoComportamiento.Text = coleccionDatos(14)
                txt_02_T15KMRecorridos.Text = coleccionDatos(15)
                txt_02_T15CantidadVehiculos.Text = coleccionDatos(16)
                txt_02_T15NumCasosEnfermedadDiagnosticada.Text = coleccionDatos(17)
                txt_02_T15PrevalenciEnfLaboral.Text = coleccionDatos(18)
                txt_02_T15IncidenciaEnfLaboral.Text = coleccionDatos(19)
                txt_02_T15Ausentismo.Text = coleccionDatos(20)
                txt_02_T15FechaRegistro.Text = coleccionDatos(21)
                txt_02_T15NumDocFuncionarioRegistra.Text = coleccionDatos(22)
                txt_02_T15Estado.Text = coleccionDatos(23)
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
        Dim sCamposTablaLocal As String = "_02_T15Vigencia,_02_T15Mes,_02_T15ODS,_02_T15AreaEcopetrolResponsable,(select top 1 _02_T16Nombre from _02_T16HUB where _02_T16Codigo=_02_T15AreaEcopetrolHUB),(select top 1 _99_T09Nombre from _99_T09DEPARTAMENTOS where _99_T09Codigo=_02_T15Departamento),_02_T15NumeroTrabajadores,_02_T15HTT,_02_T15HH,_02_T15NumIncidentesOcupacionales,_02_T15NumIncidentesAmbientales,_02_T15NumIncidentesVehiculares,_02_T15NumIncidentesSeguridadProceso,_02_T15AvancePlanHSE,_02_T15AseguramientoComportamiento,_02_T15KMRecorridos,_02_T15CantidadVehiculos,_02_T15NumCasosEnfermedadDiagnosticada,_02_T15PrevalenciEnfLaboral,_02_T15IncidenciaEnfLaboral,_02_T15Ausentismo,_02_T15FechaRegistro,_02_T15NumDocFuncionarioRegistra,_02_T15Estado"
        Dim sLlavesLocal As String = _02_T15INFORMEMENSUAL.CampoLlave_02_T15Vigencia & "<>''"
        Dim sTablaLocal As String = _02_T15INFORMEMENSUAL.NombreTabla
        Dim ArregloSingular() As String
        bodytabla.Controls.Clear()
        If drop_02_T15Vigencia.Text = "" Then
            Exit Sub
        End If
        If drop_02_T15Vigencia.Text <> "" Then
            sLlavesLocal = sLlavesLocal & " and _02_T15Vigencia='" & drop_02_T15Vigencia.Text & "'"
        End If

        If drop_02_T15AreaEcopetrolHUB.Text <> "" Then
            sLlavesLocal = sLlavesLocal & " and _02_T15AreaEcopetrolHUB='" & drop_02_T15AreaEcopetrolHUB.Text & "'"
        End If

        If drop_02_T15ODS.Text <> "" Then
            sLlavesLocal = sLlavesLocal & " and _02_T15ODS='" & drop_02_T15ODS.Text & "'"
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

    Private Sub drop_02_T15Vigencia_TextChanged(sender As Object, e As EventArgs) Handles drop_02_T15Vigencia.TextChanged
        cargar_Tabla()
    End Sub

    Private Sub drop_02_T15AreaEcopetrolHUB_TextChanged(sender As Object, e As EventArgs) Handles drop_02_T15AreaEcopetrolHUB.TextChanged
        cargar_Tabla()
    End Sub

    Private Sub drop_02_T15ODS_TextChanged(sender As Object, e As EventArgs) Handles drop_02_T15ODS.TextChanged
        cargar_Tabla()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection
        Dim sCamposTablaLocal As String = "_02_T15Vigencia,_02_T15Mes,_02_T15AreaEcopetrolHUB"
        Dim sLlavesLocal As String = _02_T15INFORMEMENSUAL.CampoLlave_02_T15Vigencia & "<>''"
        Dim sTablaLocal As String = _02_T15INFORMEMENSUAL.NombreTabla
        Dim ArregloSingular() As String

        If drop_02_T15Vigencia.Text = "" Then
            Exit Sub
        End If

        sLlavesLocal = sLlavesLocal & " and _02_T15Vigencia='" & drop_02_T15Vigencia.Text & "'"
        sLlavesLocal = sLlavesLocal & " and _02_T15Mes='" & drop_02_T15Mes.Text & "'"

        If drop_02_T15AreaEcopetrolHUB.Text <> "" Then
            sLlavesLocal = sLlavesLocal & " and _02_T15AreaEcopetrolHUB='" & drop_02_T15AreaEcopetrolHUB.Text & "'"
        End If

        clsAdminDb.sql_Coleccion_Consulta("DELETE FROM _02_T15INFORMEMENSUAL_RPT")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('HUB',1,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('DEPTO',2,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('NT',3,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('HTT',4,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('HH',5,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('NIO',6,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('NIA',7,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('NIV',8,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('NISP',9,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('APLANHSE',10,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('ASEGURAMIENTO',11,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('KMRECORRIDOS',12,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('CA',13,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('NCEDX',14,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('PEL',15,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('IEL',16,'','','','','','','','','','') ")
        clsAdminDb.sql_Coleccion_Consulta("INSERT INTO _02_T15INFORMEMENSUAL_RPT values ('AUSENTISMO',17,'','','','','','','','','','') ")

        Dim bContinuar As Boolean = False
        Dim iConsecutivo As Integer = 1
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular

                'Dim sCamposTablaLocal As String = "_02_T15Vigencia,_02_T15Mes,_02_T15AreaEcopetrolHUB"
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT (SELECT TOP 1 _02_T16Nombre  FROM _02_T16HUB WHERE _02_T16Codigo=_02_T15AreaEcopetrolHUB) FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "') WHERE Consecutivo=1 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT (SELECT TOP 1 _99_T09Nombre  FROM _99_T09DEPARTAMENTOS WHERE _99_T09Codigo=_02_T15Departamento) FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "') WHERE Consecutivo=2 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15NumeroTrabajadores FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=3 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15HTT FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=4 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15HH FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=5 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15NumIncidentesOcupacionales FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=6 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15NumIncidentesAmbientales FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=7 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15NumIncidentesVehiculares FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=8 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15NumIncidentesSeguridadProceso FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=9 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15AvancePlanHSE FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=10 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15AseguramientoComportamiento FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=11 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15KMRecorridos FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=12 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15CantidadVehiculos FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=13 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15NumCasosEnfermedadDiagnosticada FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=14 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15PrevalenciEnfLaboral FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=15 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15IncidenciaEnfLaboral FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=16 ")
                clsAdminDb.sql_Coleccion_Consulta(" UPDATE _02_T15INFORMEMENSUAL_RPT SET HUB_" & iConsecutivo & "=(SELECT _02_T15Ausentismo FROM _02_T15INFORMEMENSUAL WHERE _02_T15Vigencia='" & ArregloSingular(0) & "' AND _02_T15Mes='" & ArregloSingular(1) & "' AND _02_T15AreaEcopetrolHUB='" & ArregloSingular(2) & "'   ) WHERE Consecutivo=17 ")


                iConsecutivo = iConsecutivo + 1
                bContinuar = True
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else

            End If
        End If

        If bContinuar = True Then
            Dim sOrderby As String = ""



            Dim sRutaArchivo As String = "Reportes\rpt_InformeMensual.rpt"
            Dim sConsulta As String = " select HUB_1,HUB_2,HUB_3,HUB_4,HUB_5,HUB_6,HUB_7,HUB_8,HUB_9,HUB_10,'" & drop_02_T15Vigencia.Text & "' as vigencia,'" & drop_02_T15Mes.Text & "' as Mes FROM _02_T15INFORMEMENSUAL_RPT ORDER  BY CAST(Consecutivo AS INTEGER)"
            rptCrystal = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            rptCrystal = clsAdminDb.sql_Imprimir(sConsulta, sRutaArchivo)

            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else
                MostrarMensaje("Se genero correctamente el reporte")
                rptCrystal.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, True, "RrptInforme Mensual")
            End If
        End If
    End Sub


End Class




