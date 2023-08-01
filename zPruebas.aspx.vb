Public Class zPruebas
    Inherits System.Web.UI.Page

    Dim clsAdminDb As New adminitradorDB

    Dim sCodModulo As String
    Dim sNombreTabla As String
    Dim sCamposTabla As String
    Dim sCamposINS As String
    Dim sCamposUPD As String
    Dim sLlaves As String

    Dim FilaHtml As System.Web.UI.HtmlControls.HtmlTableRow
    Dim CeldaHtml As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents linkbutonHtml As System.Web.UI.WebControls.LinkButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsAdminDb = New adminitradorDB
        Datos_Modulo()

        If Not IsPostBack Then
            CargarDrops()
            txtDocumento.AutoPostBack = True

        End If

        lblMensaje.Text = ""
        cargar_Tabla()
    End Sub

    Private Sub CargarDrops()
        With clsAdminDb

            .drop_Cargar_SINO(drop_Activo, True)

            'Dim sTables_tem As String = _01_T12FUNCIONARIOS.NombreTabla
            'Dim sCampos_tem As String = _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos
            'Dim sLlaves_tem As String = _01_T12FUNCIONARIOS.Campo_01_T12Activo & "='SI'"
            'sLlaves_tem = sLlaves_tem & " and " & _07_Cont_T11_1EMPRESATRABAJAR.CampoLlave_07_Cont_T11_1Usuario & "='" & txtUsuario_Nombre.Text & "'"
            '.drop_CargarCombox(drop_TipoDoc, sTables_tem, sCampos_tem, sLlaves_tem, True)
            .drop_CargarCombox(drop_TipoDoc, "TipoDocumentos", "Codigo,Nombre", "Activo='SI'", True)


        End With
    End Sub

    Private Function bValidarCampos() As Boolean
        bValidarCampos = False


        If Trim(drop_TipoDoc.Text) = "" Then
            drop_TipoDoc.Focus()
            MostrarMensaje("falta campo: Tipo Documento")
            Exit Function
        Else

        End If

        If Trim(txtDocumento.Text) = "" Then
            txtDocumento.Focus()
            MostrarMensaje("falta campo: N° Documento")
            Exit Function
        Else

        End If
        If Trim(txtNombre.Text) = "" Then
            txtNombre.Focus()
            MostrarMensaje("falta campo: Nombre")
            Exit Function
        Else

        End If
        If Trim(txtApellido.Text) = "" Then
            txtApellido.Focus()
            MostrarMensaje("falta campo: Apellido")
            Exit Function
        Else

        End If

        If Trim(drop_Activo.Text) = "" Then
            drop_Activo.Focus()
            MostrarMensaje("falta campo: Activo")
            Exit Function
        Else
            'lbl_01_T37Activo.ForeColor = Drawing.Color.Black
        End If

        bValidarCampos = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txtApellido.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txtNombre.Text) Then Exit Function

        bValidarSQL = True
        MostrarMensaje("")
    End Function

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        drop_TipoDoc.Text = Nothing
        txtDocumento.Text = ""
        txtApellido.Text = Nothing
        txtNombre.Text = Nothing
        drop_Activo.Text = Nothing
        lblMensaje.Text = ""
    End Sub

    Private Sub MostrarMensaje(sMensaje As String)
        lblMensaje.Text = sMensaje
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If bValidarCampos() = False Then Exit Sub
        If bValidarSQL() = False Then Exit Sub
        Guardar_Registro()

    End Sub

    Private Sub Datos_Modulo()
        sCodModulo = ""
        sNombreTabla = "Hijo"
        sCamposTabla = "Nomnbre,Apellido,Cedula,TipoDocumento"
        sCamposINS = "'" & clsAdminDb.sRemoverHTML(txtNombre.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txtApellido.Text) & "'" & "," & "'" & txtDocumento.Text & "'" & "," & "'" & drop_TipoDoc.Text & "'"
        sCamposUPD = "Nomnbre='" & clsAdminDb.sRemoverHTML(txtNombre.Text) & "'" & "," & "Apellido='" & txtApellido.Text & "'"
        sLlaves = "Cedula=  '" & txtDocumento.Text & "'"
    End Sub


    Private Sub Guardar_Registro()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0

        Datos_Modulo()
        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla, sLlaves)
        If lCantRegistros = 0 Then

            Datos_Modulo()
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla, sCamposTabla, sCamposINS)
            If bSqlInsert = True Then
                MostrarMensaje("Se inserto nuevo registro correctamente ")

            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla, sCamposUPD, sLlaves)
            If bSqlInsert = True Then
                MostrarMensaje("Se actualizo correctamente este registro")

            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If
        End If
    End Sub

    Protected Sub txtDocumento_TextChanged(sender As Object, e As EventArgs) Handles txtDocumento.TextChanged
        If txtDocumento.Text <> "" Then
            Cargar_Registro()
        End If

    End Sub

    Private Sub Cargar_Registro()
        'Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object
        'sCamposTabla = "Nomnbre,Apellido,Cedula,TipoDocumento"
        Datos_Modulo()
        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla, sCamposTabla, sLlaves)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            Else
                txtApellido.Text = coleccionDatos(1)
                txtNombre.Text = coleccionDatos(0)
                drop_TipoDoc.Text = coleccionDatos(3)

            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If txtDocumento.Text = "" Then
            MostrarMensaje("Por favor ingrese N° Documento")
            Exit Sub
        End If
        Eliminar_Registro()
    End Sub

    Private Sub Eliminar_Registro()
        'Dim clsAdminDb As New adminitradorDB
        Dim bSqlDelete As Boolean = False

        Datos_Modulo()
        Dim IExiste As Integer = 0
        IExiste = clsAdminDb.sql_Count(sNombreTabla, sLlaves)
        If IExiste <> 0 Then
            bSqlDelete = clsAdminDb.sql_Delete(sNombreTabla, sLlaves)
            If bSqlDelete = True Then
                MostrarMensaje("se elimino correctamente")
                drop_TipoDoc.Text = Nothing
                txtDocumento.Text = ""
                txtApellido.Text = Nothing
                txtNombre.Text = Nothing
                drop_Activo.Text = Nothing

            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If

        End If
    End Sub

    Private Sub cargar_Tabla(Optional ByVal sLlaveTem As String = "")

        Dim coleccionDatosPlural As New Collection
        Dim sCamposTablaLocal As String = "TipoDocumento,Cedula,Nomnbre,Apellido"
        Dim sLlavesLocal As String = "Cedula<>''"
        Dim sTablaLocal As String = sNombreTabla
        Dim ArregloSingular() As String
        bodytabla.Controls.Clear()

        If txtDocumento.Text = "" Then
            'Exit Sub
        Else
            'sLlavesLocal = sLlavesLocal & " AND _01_T20Disciplina='" & drop_01_T20Categoria.Text & "'"
        End If

        Dim i As Integer = 0
        coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta("select TipoDocumento,Cedula,Nomnbre,Apellido from Hijo")

        If (Not coleccionDatosPlural Is Nothing) Then

            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow

                Dim lbl_TipoDocumento As New Label
                lbl_TipoDocumento.Text = ArregloSingular(0)
                CeldaHtml = New HtmlTableCell
                CeldaHtml.Controls.Add(lbl_TipoDocumento)
                FilaHtml.Cells.Add(CeldaHtml)

                Dim lbl_Documento As New Label
                lbl_Documento.Text = ArregloSingular(1)
                CeldaHtml = New HtmlTableCell
                CeldaHtml.Controls.Add(lbl_Documento)
                FilaHtml.Cells.Add(CeldaHtml)

                Dim lbl_Nombre As New Label
                lbl_Nombre.Text = ArregloSingular(2)
                CeldaHtml = New HtmlTableCell
                CeldaHtml.Controls.Add(lbl_Nombre)
                FilaHtml.Cells.Add(CeldaHtml)

                Dim lbl_Apellido As New Label
                lbl_Apellido.Text = ArregloSingular(3)
                CeldaHtml = New HtmlTableCell
                CeldaHtml.Controls.Add(lbl_Apellido)
                FilaHtml.Cells.Add(CeldaHtml)


                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                'linkbutonHtml.ID = i & "d-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = ArregloSingular(3)
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'FilaHtml.Cells.Add(CeldaHtml)

                i = i + 1
                bodytabla.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else

            End If
        End If
    End Sub



End Class