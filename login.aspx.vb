

Public Class login
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

    Dim sUsuarioWindows = System.Environment.UserName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsAdminDb = New adminitradorDB
        sDatos_90_T01GENERADOR()

        If Not IsPostBack Then
            Session.Abandon()

            limpiarCampos()
            txt_90_T06Clave.TextMode = TextBoxMode.Password
        End If
    End Sub

    Private Sub WebForm1_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        clsAdminDb = Nothing
    End Sub

    Private Sub sDatos_90_T01GENERADOR()
        sCodModulo = _90_T06USUARIOS.CodigoModulo
        sNombreTabla = _90_T06USUARIOS.NombreTabla
        sCamposTabla = _90_T06USUARIOS.CamposTabla
        sLlaves = _90_T06USUARIOS.CampoLlave_90_T06NombreUsuario & "='" & txt_90_T06NombreUsuario.Text & "'"
    End Sub

    Private Sub MostrarMensaje(ByVal sMensaje As String)
        lblMensaje.Text = sMensaje
    End Sub

    Private Sub limpiarCampos()
        txt_90_T06Clave.Text = Nothing
        txt_90_T06NombreUsuario.Text = ""
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txt_90_T06NombreUsuario.Text = "" Then
            MostrarMensaje("Por favor digite el N° de Documento")
        Else
            If txt_90_T06Clave.Text = "" Then
                MostrarMensaje("Por favor digite Contraseña")
            Else
                Cargar_Registro_Usuario()

            End If

        End If

    End Sub

    Sub Cargar_Registro_Usuario()
        Dim sCadenaConexion_Ontime As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString_Ontime").ConnectionString
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object
        Dim sCodModulo = _01_T12FUNCIONARIOS.CodigoModulo
        Dim sNombreTabla = _01_T12FUNCIONARIOS.NombreTabla
        Dim sCamposTabla = "_01_T12NumDoc,_01_T12Clave,_01_T12Activo,_01_T12NombreApellidos"
        Dim sLlaves = _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "=  '" & txt_90_T06NombreUsuario.Text & "'"

        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla, sCamposTabla, sLlaves,,, sCadenaConexion_Ontime)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("Este Documento no se encuentra registrado")
                    txt_90_T06NombreUsuario.Focus()
                End If
            Else
                If coleccionDatos(2) = "NO" Then
                    MostrarMensaje("Este Documento se encuentra inactivo, por favor comuniquese con el administrador del sistema")
                    txt_90_T06NombreUsuario.Focus()
                    Exit Sub
                End If
                If UCase(txt_90_T06NombreUsuario.Text) <> UCase(coleccionDatos(0)) Then
                    MostrarMensaje("Por favor Digite de Documento  valido")
                    txt_90_T06NombreUsuario.Focus()
                    Exit Sub
                End If

                If txt_90_T06Clave.Text <> coleccionDatos(1) Then
                    MostrarMensaje("Por favor ingrese una contraseña correcta")
                    txt_90_T06Clave.Focus()
                    Exit Sub
                End If
                Dim dFecha As Date
                Dim sFecha As String = ""
                If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                End If

                Session("DatoUsuario" & sCod_Aplicacion) = coleccionDatos(0) & "-" & coleccionDatos(3)
                Response.Redirect("plataforma.aspx")
            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If
    End Sub




    Private Function bValidarCampos() As Boolean
        bValidarCampos = False

        If Trim(txt_90_T06NombreUsuario.Text) = "" Then
            txt_90_T06NombreUsuario.Focus()
            MostrarMensaje("Por favor ingrese un nombre de usuario")
            Exit Function
        Else
            txt_90_T06NombreUsuario.BorderColor = Drawing.Color.LightGray
        End If
        If Trim(txt_90_T06Clave.Text) = "" Then
            txt_90_T06Clave.Focus()
            MostrarMensaje("Por favor ingrese contraseña")
            Exit Function
        End If
        bValidarCampos = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con el proceso")
        If clsAdminDb.bVerificarSQL(txt_90_T06NombreUsuario.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_90_T06Clave.Text) Then Exit Function
        bValidarSQL = True
        MostrarMensaje("")
    End Function


End Class