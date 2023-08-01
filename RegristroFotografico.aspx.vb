Imports System.IO

Public Class RegistroFotografico
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
        sCodModulo = _02_T07REGISTROfOTOGRAFICO.CodigoModulo
        sNombreTabla = _02_T07REGISTROfOTOGRAFICO.NombreTabla
        sCamposTabla = _02_T07REGISTROfOTOGRAFICO.CamposTabla
        sCamposINS = "'" & clsAdminDb.sRemoverHTML(txt_02_T07Id_Estadistica.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Id_registroFotografico.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Id_BD.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Id_BD_Item.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Observaciones.Text) & "'"
        sCamposUPD = _02_T07REGISTROfOTOGRAFICO.Campo_02_T07Id_BD_Item & "='" & clsAdminDb.sRemoverHTML(txt_02_T07Id_BD_Item.Text) & "'" & "," & _02_T07REGISTROfOTOGRAFICO.Campo_02_T07Fecha & "='" & clsAdminDb.sRemoverHTML(txt_02_T07Fecha.Text) & "'" & "," & _02_T07REGISTROfOTOGRAFICO.Campo_02_T07Observaciones & "='" & clsAdminDb.sRemoverHTML(txt_02_T07Observaciones.Text) & "'"
        sLlaves = _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_Estadistica & "=  '" & txt_02_T07Id_Estadistica.Text & "'" & " AND " & _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_registroFotografico & "=  '" & txt_02_T07Id_registroFotografico.Text & "'" & " AND " & _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_BD & "=  '" & txt_02_T07Id_BD.Text & "'"
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
        ModalPopupGuardar.Show()
        lblMensajeGuardarPopup.Text = "Para guardar este registro pulse ingresar"
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

        txt_02_T07Id_Estadistica.Text = Nothing
        txt_02_T07Id_registroFotografico.Text = Nothing
        txt_02_T07Id_BD.Text = Nothing
        txt_02_T07Id_BD_Item.Text = Nothing
        txt_02_T07Fecha.Text = Nothing
        txt_02_T07Observaciones.Text = Nothing

        lbl_02_T07Id_Estadistica.ForeColor = Drawing.Color.Black
        lbl_02_T07Id_registroFotografico.ForeColor = Drawing.Color.Black
        lbl_02_T07Id_BD.ForeColor = Drawing.Color.Black
        lbl_02_T07Id_BD_Item.ForeColor = Drawing.Color.Black
        lbl_02_T07Fecha.ForeColor = Drawing.Color.Black
        lbl_02_T07Observaciones.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing

        MostrarMensaje("")
        bHabilitarControles(True)
        filtro()
    End Sub



    Private Sub bHabilitarControles(ByVal bEstado As Boolean)
        txt_02_T07Id_Estadistica.Enabled = bEstado
        txt_02_T07Id_registroFotografico.Enabled = bEstado
        txt_02_T07Id_BD.Enabled = bEstado
        txt_02_T07Id_BD_Item.Enabled = bEstado
        txt_02_T07Fecha.Enabled = bEstado
        txt_02_T07Observaciones.Enabled = bEstado
        btnEliminar.Visible = False
        btnImprimir.Visible = False
    End Sub

    Private Function bValidarCampos() As Boolean
        bValidarCampos = False

        lbl_02_T07Id_Estadistica.ForeColor = Drawing.Color.Red
        lbl_02_T07Id_registroFotografico.ForeColor = Drawing.Color.Red
        lbl_02_T07Id_BD.ForeColor = Drawing.Color.Red
        lbl_02_T07Id_BD_Item.ForeColor = Drawing.Color.Red
        lbl_02_T07Fecha.ForeColor = Drawing.Color.Red
        lbl_02_T07Observaciones.ForeColor = Drawing.Color.Red
        If Trim(txt_02_T07Id_Estadistica.Text) = "" Then
            txt_02_T07Id_Estadistica.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Id_Estadistica")
            Exit Function
        Else
            lbl_02_T07Id_Estadistica.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T07Id_registroFotografico.Text) = "" Then
            txt_02_T07Id_registroFotografico.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Id_registroFotografico")
            Exit Function
        Else
            lbl_02_T07Id_registroFotografico.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T07Id_BD.Text) = "" Then
            txt_02_T07Id_BD.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Id_BD")
            Exit Function
        Else
            lbl_02_T07Id_BD.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T07Id_BD_Item.Text) = "" Then
            txt_02_T07Id_BD_Item.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Id_BD_Item")
            Exit Function
        Else
            lbl_02_T07Id_BD_Item.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T07Fecha.Text) = "" Then
            txt_02_T07Fecha.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Fecha")
            Exit Function
        Else
            lbl_02_T07Fecha.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T07Observaciones.Text) = "" Then
            txt_02_T07Observaciones.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Observaciones")
            Exit Function
        Else
            lbl_02_T07Observaciones.ForeColor = Drawing.Color.Black
        End If

        bValidarCampos = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_02_T07Id_Estadistica.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Id_registroFotografico.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Id_BD.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Id_BD_Item.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Fecha.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Observaciones.Text) Then Exit Function
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
                txt_02_T07Id_Estadistica.Text = coleccionDatos(0)
                txt_02_T07Id_registroFotografico.Text = coleccionDatos(1)
                txt_02_T07Id_BD.Text = coleccionDatos(2)
                txt_02_T07Id_BD_Item.Text = coleccionDatos(3)
                txt_02_T07Fecha.Text = coleccionDatos(4)
                txt_02_T07Observaciones.Text = coleccionDatos(5)
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
        Dim sFecha As String = " "
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = False Then
            MostrarMensaje("Error al guardar registro de procesos")
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
        'Selec _02_T01Fecha,_02_T01DocUsuarioRegistra, _02_T02Nombre,_02_T01LugarEvento 
        'From _02_T01REPORTE_CASOS, _02_T02TIPO_ACCION
        'Where _02_T02Id = _02_T01Id_TipoAccion


        Dim sCamposTablaLocal As String = _02_T07REGISTROfOTOGRAFICO.CamposTabla
        Dim sLlavesLocal As String = _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_registroFotografico & "<>''"
        Dim sTablaLocal As String = _02_T07REGISTROfOTOGRAFICO.NombreTabla
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





End Class


