
Public Class sinpermiso
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

    Dim FilaHtml As System.Web.UI.HtmlControls.HtmlTableRow
    Dim CeldaHtml As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents linkbutonHtml As System.Web.UI.WebControls.LinkButton

    Dim sUsuarioWindows = System.Environment.UserName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'txtClave.TextMode = TextBoxMode.Password
        If Not IsPostBack Then
            If Session("DatoUsuario" & sCod_Aplicacion) Is Nothing Then
                Session.Abandon()
                Response.Redirect("login.aspx?Err2=001")
            Else
                sUsuarioWindows = Session("DatoUsuario" & sCod_Aplicacion)
                bVerficarLogin()
            End If
            CargarDrops()
        End If
        Datos_Modulo()
        bVerficarLogin()
    End Sub

    Private Sub CargarDrops()
        clsAdminDb = New adminitradorDB
        With clsAdminDb
            '.Lit_CargarLiteral(LitHead, LitSuperior, LitIzquierdo, sCod_Aplicacion)
        End With
    End Sub

    Private Sub bVerficarLogin()
        Dim dFecha As Date
        Dim sFecha As String = ""
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
        Else
        End If
        'Dim lExisteFuncionario As Integer = 0
        'lExisteFuncionario = clsAdminDb.sql_Count(_01_T36SSESIONUSUARIO.NombreTabla, )

        'Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object
        Dim sLlavesTem As String = _01_T36SSESIONUSUARIO.CampoLlave_01_T36EmailCorporativo & "='" & sUsuarioWindows & "'"
        sLlavesTem = sLlavesTem & " and " & _01_T36SSESIONUSUARIO.CampoLlave_01_T36Aplicacion & "='" & sCod_Aplicacion & "'"
        sLlavesTem = sLlavesTem & " and " & _01_T36SSESIONUSUARIO.Campo_01_T36Fecha & "='" & sFecha & "'"

        coleccionDatos = clsAdminDb.sql_Select(_01_T36SSESIONUSUARIO.NombreTabla, _01_T36SSESIONUSUARIO.CamposTabla, sLlavesTem)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                Else
                End If
            Else
                'VerificarPermisoConsulta(coleccionDatos(5), coleccionDatos(6), coleccionDatos(7), coleccionDatos(8), coleccionDatos(9), coleccionDatos(10), coleccionDatos(11), coleccionDatos(12))
                'Private Sub VerificarPermisoConsulta(ByVal sPerfilAdmin As String, ByVal sPerfilLIDER As String, ByVal sPerfilCoordinador As String, ByVal sPerfilAprobador As String, ByVal sPerfilFuncionario As String, ByVal sPerfilRecursosHumanos As String, ByVal sPerfilControlProyectos As String, ByVal sPerfilControlDocumentos As String)
                VerificarPermisoConsulta(coleccionDatos(5), coleccionDatos(6), coleccionDatos(7), coleccionDatos(8), coleccionDatos(9), coleccionDatos(10), coleccionDatos(11), coleccionDatos(12))
                'If coleccionDatos(5) = "01" Then
                'Else
                '    Response.Redirect("sinpermiso.aspx")
                'End If
                'VerificarPermisoConsulta()
            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then

            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If


    End Sub

    Private Sub Datos_Modulo()
    End Sub

    Private Sub VerificarPermisoConsulta(ByVal sPerfilAdmin As String, ByVal sPerfilLIDER As String, ByVal sPerfilCoordinador As String, ByVal sPerfilAprobador As String, ByVal sPerfilFuncionario As String, ByVal sPerfilRecursosHumanos As String, ByVal sPerfilControlProyectos As String, ByVal sPerfilControlDocumentos As String)
        'clsAdminDb = New adminitradorDB
        Dim stabla_Tem As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
        Dim sCAmpos_Tem As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        Dim sLlaves_Tem As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & " AND " & _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & sUsuarioWindows & "@SERINGTEC.COM" & "'"
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T04LIDERES.CampoLlave_01_T04NumDocLider & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T04LIDERES.CampoLlave_01_T04NumDocLider & "=" & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario

        lblUsuarioLogueado_Nombre.Text = ""
        lblUsuarioLogueado_Disciplina.Text = ""
        lblUsuarioLogueado_Documento.Text = ""

        Dim coleccionDatosPlural As New Collection
        Dim ArregloSingular() As String
        Lit_Configuracion.Visible = False
        Lit_Coordinadores.Visible = False
        Lit_Funcionarios.Visible = True
        Lit_Lideres.Visible = False
        Lit_RecursosHumanos.Visible = False
        Lit_ControlDocumental.Visible = False
        Lit_Configuracion2.Visible = False
        Lit_Coordinadores2.Visible = False
        Lit_Funcionarios2.Visible = False
        Lit_Lideres2.Visible = False
        Lit_Aprobadores.Visible = False
        Lit_Aprobadores2.Visible = False
        Lit_RecursosHumanos2.Visible = False
        Lit_ControlDocumental2.Visible = False
        Lit_Edicion.Visible = False

        Dim coleccionDatos As Object

        stabla_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
        sCAmpos_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "," & _01_T12FUNCIONARIOS.Campo_01_T12Disciplina
        sLlaves_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & " AND " & _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & sUsuarioWindows & "@SERINGTEC.COM" & "'"
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T33APROBADORESTIEMPO.CampoLlave_01_T33DocumentoFncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T33APROBADORESTIEMPO.CampoLlave_01_T33DocumentoFncionario & "=" & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario

        coleccionDatos = clsAdminDb.sql_Select(stabla_Tem, sCAmpos_Tem, sLlaves_Tem)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    'MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("No se encontraron datos ")
                End If
            Else
                lblUsuarioLogueado_Nombre.Text = coleccionDatos(1)
                lblUsuarioLogueado_Disciplina.Text = coleccionDatos(3)
                lblUsuarioLogueado_Documento.Text = coleccionDatos(2)
                'drop_01_T03Disciplina.Text = lblUsuarioLogueado_Disciplina.Text
                'Cargar_ODS()
            End If
        Else

        End If



        stabla_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
        sCAmpos_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        sLlaves_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & " AND " & _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & sUsuarioWindows & "@SERINGTEC.COM" & "'"
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T04LIDERES.CampoLlave_01_T04NumDocLider & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T04LIDERES.CampoLlave_01_T04NumDocLider & "=" & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario

        If sPerfilAdmin = "01" Then
            Lit_Configuracion.Visible = True
            Lit_Coordinadores.Visible = True
            Lit_Funcionarios.Visible = True
            Lit_Lideres.Visible = True
            Lit_RecursosHumanos.Visible = False
            'Lit_ControlDocumental.Visible = True
            Lit_Configuracion2.Visible = True
            Lit_Coordinadores2.Visible = True
            Lit_Funcionarios2.Visible = False
            Lit_Lideres2.Visible = True
            Lit_Aprobadores.Visible = True
            Lit_Aprobadores2.Visible = True
            Lit_RecursosHumanos.Visible = True
            'Lit_ControlDocumental2.Visible = False
            Lit_Edicion.Visible = False

        End If
        If sPerfilLIDER = "02" Then
            Lit_Lideres.Visible = True
            Lit_Lideres2.Visible = True

        End If
        If sPerfilCoordinador = "03" Then

            Lit_Coordinadores.Visible = True
            Lit_Coordinadores2.Visible = True

        End If
        If sPerfilFuncionario = "04" Then 'Funcionarios
            Lit_Funcionarios.Visible = True
            Lit_Funcionarios2.Visible = False

        End If
        If sPerfilRecursosHumanos = "05" Then 'recursos humanos

            Lit_RecursosHumanos.Visible = False
            Lit_RecursosHumanos2.Visible = True
        End If
        If sPerfilAprobador = "06" Then 'recursos humanos
            Lit_Aprobadores.Visible = True
            Lit_Aprobadores2.Visible = True

        ElseIf "101" = "07" Then 'CONTROL DOCUMENTAL

            Lit_ControlDocumental.Visible = True
            Lit_ControlDocumental2.Visible = False
        End If

    End Sub

    Private Sub WebForm1_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        clsAdminDb = Nothing
    End Sub


    '***************************************************************************************
    '*********** comienza logueo **********************************************************

    Private Sub MostrarMensaje_Login(ByVal sMensaje As String)
        lblMensajeLogeo.Text = sMensaje
    End Sub

    Private Sub limpiarCampos_Login()
        txt_90_T06Clave.Text = Nothing
        txt_90_T06NombreUsuario.Text = Nothing
        MostrarMensaje_Login("")
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        If bValidarCampos_Login() = False Then Exit Sub
        If bValidarSQL_Login() = False Then Exit Sub
        Cargar_Registro_Usuario_Login()
    End Sub

    Private Sub btnloginCancelar_Click(sender As Object, e As EventArgs) Handles btnloginCancelar.Click
        ModalPopuplogeo.Hide()
        Session.Abandon()
        clsAdminDb = Nothing
        Response.Redirect("login.aspx")
    End Sub

    Sub Cargar_Registro_Usuario_Login()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object
        Dim sCodModulo = _90_T06USUARIOS.CodigoModulo
        Dim sNombreTabla = _90_T06USUARIOS.NombreTabla & ", " & _90_T05TERCEROS.NombreTabla
        Dim sCamposTabla = _90_T06USUARIOS.CamposTabla & ", " & _90_T05TERCEROS.Campo_90_T05Nombres & " + ' ' + " & _90_T05TERCEROS.Campo_90_T05Apellidos
        Dim sLlaves = _90_T06USUARIOS.CampoLlave_90_T06NombreUsuario & "=  '" & txt_90_T06NombreUsuario.Text & "'"
        sLlaves = sLlaves & " And " & _90_T06USUARIOS.CampoLlave_90_T06TipoDocumento & " = " & _90_T05TERCEROS.CampoLlave_90_T05TipoDocumento
        sLlaves = sLlaves & " And " & _90_T06USUARIOS.CampoLlave_90_T06Documento & " = " & _90_T05TERCEROS.CampoLlave_90_T05Documento

        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla, sCamposTabla, sLlaves)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje_Login(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje_Login("Este usuario no se encuentra registrado")
                End If
            Else
                If coleccionDatos(4) = "NO" Then
                    MostrarMensaje_Login("Este usuario se encuentra inactivo, por favor comuniquese con el administrador del sistema")
                    Exit Sub
                End If
                If UCase(txt_90_T06NombreUsuario.Text) <> UCase(coleccionDatos(0)) Then
                    MostrarMensaje_Login("Por favor Digite un nombre de usuario valido")
                    Exit Sub
                End If
                If txt_90_T06Clave.Text <> coleccionDatos(3) Then
                    MostrarMensaje_Login("Por favor ingrese una contraseña correcta")
                    Exit Sub
                End If
                Session("DatoUsuario" & sCod_Aplicacion) = sCod_Aplicacion & "-" & coleccionDatos(1) & "-" & coleccionDatos(2) & "-" & coleccionDatos(0) & "-" & coleccionDatos(6)
                'Session("NumDoc" & sCod_Aplicacion) = coleccionDatos(2)
                'Session("NombreUsuario" & sCod_Aplicacion) = coleccionDatos(6)
                ModalPopuplogeo.Hide()
                limpiarCampos_Login()
            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje_Login(clsAdminDb.Mostrar_Error)
            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If
    End Sub

    Private Function bValidarCampos_Login() As Boolean
        bValidarCampos_Login = False
        If Trim(txt_90_T06NombreUsuario.Text) = "" Then
            MostrarMensaje_Login("Por favor ingrese un nombre de usuario")
            Exit Function
        Else
            txt_90_T06NombreUsuario.BorderColor = Drawing.Color.LightGray
        End If
        If Trim(txt_90_T06Clave.Text) = "" Then
            MostrarMensaje_Login("Por favor ingrese contraseña")
            Exit Function
        End If
        bValidarCampos_Login = True
        MostrarMensaje_Login("")
    End Function

    Private Function bValidarSQL_Login() As Boolean
        bValidarSQL_Login = False

        MostrarMensaje_Login("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con el proceso")
        If clsAdminDb.bVerificarSQL(txt_90_T06NombreUsuario.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_90_T06Clave.Text) Then Exit Function
        bValidarSQL_Login = True
        MostrarMensaje_Login("")
    End Function

    '*********** Termina logueo **********************************************************
    '***************************************************************************************

    Private Sub btnCerrarSeccion_Click(sender As Object, e As EventArgs) Handles btnCerrarSeccion.Click
        Session.Abandon()
        clsAdminDb = Nothing
        Response.Redirect("login.aspx")
    End Sub
End Class
