
Public Class PerfilesxUsuariosVer
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
    Protected WithEvents linkbutonHtml_Eliminar As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_Asignar As System.Web.UI.WebControls.LinkButton

    Dim rptCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

    Dim sUsuarioWindows = System.Environment.UserName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsAdminDb = New adminitradorDB
        Datos_Modulo()
        If Session("DatoUsuario" & sCod_Aplicacion) Is Nothing Then
            Session.Abandon()
            Response.Redirect("login.aspx?Err2=001")
        Else
            sUsuarioWindows = Session("DatoUsuario" & sCod_Aplicacion)
            bVerficarLogin()
        End If

        If Not IsPostBack Then
            drop_01_T18NumDocFuncionario.AutoPostBack = True
            drop_01_T18Perfil.AutoPostBack = True
            drop_01_T18Perfil.AutoPostBack = True

            txtFiltroBusqueda.AutoPostBack = True
            bVerficarLogin()
            'VerificarPermisoConsulta()

            CargarDrops()
            limpiarCampos()
            Registro_Procesos("Consultar", clsAdminDb.Mostrar_Consulta)
        Else
            bVerficarLogin()
            'VerificarPermisoConsulta()

        End If

        cargar_Tabla_Usuarios()
        cargar_Tabla()
        MostrarMensaje("")
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
                If coleccionDatos(5) = "01" Then
                Else
                    Response.Redirect("sinpermiso.aspx")
                End If
                'VerificarPermisoConsulta()
            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then

            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If


    End Sub

    Private Sub bVerficarClave(ByVal sTipoDocTem As String, ByVal sNumDocTem As String)
        Dim sUsuarioWindows = System.Environment.UserName




        'Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object

        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0
        Dim sNombreTablaTem As String = _90_T06USUARIOS.NombreTabla
        Dim sCamposTablaTem As String = _90_T06USUARIOS.CamposTabla
        Dim sCamposUPDTem As String = _90_T06USUARIOS.Campo_90_T06Clave & "='" & clsAdminDb.sRemoverHTML(txt_90_T06ClaveLocal.Text)
        Dim sLlavesTem As String = _90_T06USUARIOS.CampoLlave_90_T06TipoDocumento & "=  '" & sTipoDocTem & "'" & " AND " & _90_T06USUARIOS.CampoLlave_90_T06Documento & "=  '" & sNumDocTem & "'" & " AND " & _90_T06USUARIOS.CampoLlave_90_T06CodAplicacion & "=  '" & sCod_Aplicacion & "'"

        coleccionDatos = clsAdminDb.sql_Select(sNombreTablaTem, sCamposTablaTem, sLlavesTem)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
            Else
                If coleccionDatos(2) = coleccionDatos(3) Or "12345" = coleccionDatos(3) Then
                    ModalPopuplogeo.Show()
                Else
                    ModalPopuplogeo.Hide()
                End If
            End If
        Else
        End If

    End Sub

    Private Sub Guardar_Clave()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0
        Dim sNombreTablaTem As String = _01_T12FUNCIONARIOS.NombreTabla
        Dim sCamposTablaTem As String = _01_T12FUNCIONARIOS.CamposTabla
        Dim sCamposUPDTem As String = _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & clsAdminDb.sRemoverHTML(txt_90_T06ClaveLocal.Text) & "'"
        Dim sLlavesTem As String = _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "=  '" & sTipoDoc & "'" & " AND " & _90_T06USUARIOS.CampoLlave_90_T06Documento & "=  '" & sNumDoc & "'" & " AND " & _90_T06USUARIOS.CampoLlave_90_T06CodAplicacion & "=  '" & sCod_Aplicacion & "'"

        Dim lExisteFuncionario As Integer = 0
        lExisteFuncionario = clsAdminDb.sql_Count(_01_T12FUNCIONARIOS.NombreTabla, _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & sUsuarioWindows & "@sereingtec.com'")

        If lExisteFuncionario <> 0 Then
            'Dim clsAdminDb As New adminitradorDB
            Dim coleccionDatos As Object
            sNombreTablaTem = _01_T12FUNCIONARIOS.NombreTabla
            sCamposTablaTem = _01_T12FUNCIONARIOS.Campo_01_T12Activo & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
            sLlavesTem = _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & sUsuarioWindows & "@sereingtec.com'"

            coleccionDatos = clsAdminDb.sql_Select(sNombreTablaTem, sCamposTablaTem, sLlavesTem)
            If Not coleccionDatos Is Nothing Then
                If coleccionDatos.Length = 0 Then
                    If clsAdminDb.Mostrar_Error <> "" Then
                        MostrarMensaje_verificar(clsAdminDb.Mostrar_Error)
                    Else
                        'MostrarMensaje_Login("Este usuario no se encuentra registrado")
                    End If
                Else
                    If coleccionDatos(0) = "NO" Then
                        MostrarMensaje_verificar("Este usuario se encuentra inactivo, por favor comuniquese con el administrador del sistema")
                        Exit Sub
                    End If
                    If coleccionDatos(1) <> txt_90_T06ClaveLocal.Text Then
                        MostrarMensaje_verificar("Este  numeo de documento no corresdponde al regisdtrado en el sistema para este usuario")
                        Exit Sub
                    End If
                    'Session("DatoUsuario" & sCod_Aplicacion) = sCod_Aplicacion & "-" & coleccionDatos(1) & "-" & coleccionDatos(2) & "-" & coleccionDatos(0) & "-" & coleccionDatos(6)
                    ModalPopuplogeo.Hide()

                End If
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje_verificar(clsAdminDb.Mostrar_Error)
                Else
                    'MostrarMensaje("No se encontraron datos" 
                End If
            End If

        Else
            'Dim clsAdminDb As New adminitradorDB
            Dim coleccionDatos As Object
            sNombreTablaTem = _01_T12FUNCIONARIOS.NombreTabla
            sCamposTablaTem = _01_T12FUNCIONARIOS.Campo_01_T12Activo & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "," & _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad
            sLlavesTem = _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & txt_90_T06ClaveLocal.Text & "'" ' AND " & _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "=''"

            coleccionDatos = clsAdminDb.sql_Select(sNombreTablaTem, sCamposTablaTem, sLlavesTem)
            If Not coleccionDatos Is Nothing Then
                If coleccionDatos.Length = 0 Then
                    If clsAdminDb.Mostrar_Error <> "" Then
                        MostrarMensaje_verificar(clsAdminDb.Mostrar_Error)
                    Else
                        'MostrarMensaje_Login("Este usuario no se encuentra registrado")
                    End If
                Else
                    If coleccionDatos(0) = "NO" Then
                        MostrarMensaje_verificar("Este usuario se encuentra inactivo, por favor comuniquese con el administrador del sistema")
                        Exit Sub
                    End If
                    If coleccionDatos(2) <> "" Then
                        If coleccionDatos(2) <> sUsuarioWindows & "@seringtec.com'" Then
                            MostrarMensaje_verificar("Este  numero de documento ya tiene asignado u n usuario por favor comunicarse con el administrador del sistema")
                            Exit Sub
                        End If
                    Else
                        bSqlInsert = clsAdminDb.sql_Update(_01_T12FUNCIONARIOS.NombreTabla, _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & sUsuarioWindows & "@seringtec.com'", _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & txt_90_T06ClaveLocal.Text & "'")
                        If bSqlInsert = True Then
                            ModalPopuplogeo.Hide()
                        Else
                            If clsAdminDb.Mostrar_Error <> "" Then
                            Else
                                ModalPopuplogeo.Hide()
                            End If
                        End If
                    End If


                End If
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje_verificar(clsAdminDb.Mostrar_Error)
                Else
                    'MostrarMensaje("No se encontraron datos" 
                End If
            End If
        End If


    End Sub


    Private Sub Datos_Modulo()
        sCodModulo = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CodigoModulo
        sNombreTabla = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla
        sCamposTabla = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CamposTabla
        sCamposINS = "'" & drop_01_T18Aplicacion.Text & "'" & "," & "'" & drop_01_T18Perfil.Text & "'" & "," & "'" & drop_01_T18NumDocFuncionario.Text & "'"
        sCamposUPD = ""
        sLlaves = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Aplicacion & "=  '" & drop_01_T18Aplicacion.Text & "'" & " AND " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "=  '" & drop_01_T18Perfil.Text & "'" & " AND " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "=  '" & drop_01_T18NumDocFuncionario.Text & "'"

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
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
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

            Lit_RecursosHumanos.Visible = True
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

    Private Sub CargarDrops()
        'clsAdminDb = New adminitradorDB
        With clsAdminDb
            '.Lit_CargarLiteral(LitHead, LitSuperior, LitIzquierdo, sCod_Aplicacion)


            '.drop_Cargar_SINO(drop_01_T10Activo, False)
            drop_01_T18Aplicacion.Items.Add(New ListItem("Ontime", "01"))
            'Dim sTables_tem As String = _07_Cont_T11_1EMPRESATRABAJAR.NombreTabla & "," & _04_T20ENTIDAD.NombreTabla
            'Dim sCampos_tem As String = _04_T20ENTIDAD.Campo_04_T20TipoPuc & "," & _07_Cont_T11_1EMPRESATRABAJAR.CampoLlave_07_Cont_T11_1CodEmpresa & "," & _04_T20ENTIDAD.Campo_04_T20NumDoc
            'Dim sLlaves_tem As String = _04_T20ENTIDAD.CampoLlave_04_T20Codigo & "=" & _07_Cont_T11_1EMPRESATRABAJAR.CampoLlave_07_Cont_T11_1CodEmpresa
            'Dim sLlaves_tem As String = _04_T20ENTIDAD.CampoLlave_04_T20Codigo & "=" & _07_Cont_T11_1EMPRESATRABAJAR.CampoLlave_07_Cont_T11_1CodEmpresa
            'sLlaves_tem = sLlaves_tem & " and " & _07_Cont_T11_1EMPRESATRABAJAR.CampoLlave_07_Cont_T11_1Usuario & "='" & txtUsuario_Nombre.Text & "'"
            drop_01_T18NumDocFuncionario.Items.Add(New ListItem("Seleccione Funcionario", ""))
            .drop_CargarCombox(drop_01_T18NumDocFuncionario, _01_T12FUNCIONARIOS.NombreTabla, _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos, _01_T12FUNCIONARIOS.Campo_01_T12Activo & "='SI' AND  _01_T12NumDoc<>'ADMINISTRADOR'", False)
            drop_01_T18Perfil.Items.Add(New ListItem("Seleccione Perfil", ""))
            .drop_CargarCombox(drop_01_T18Perfil, _01_T17PERFILESxAPLICACION.NombreTabla, _01_T17PERFILESxAPLICACION.CampoLlave_01_T17Codigo & "," & _01_T17PERFILESxAPLICACION.Campo_01_T17Nombre, _01_T17PERFILESxAPLICACION.Campo_01_T17Activo & "='SI' and " & _01_T17PERFILESxAPLICACION.CampoLlave_01_T17Aplicacion & "='" & drop_01_T18Aplicacion.Text & "'", False)
            '.drop_CargarCombox(drop_01_T18NumDocFuncionario, _01_T12FUNCIONARIOS.NombreTabla, _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos, _01_T12FUNCIONARIOS.Campo_01_T12Activo & "='SI'", True)



        End With
    End Sub


    Private Sub WebForm1_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        clsAdminDb = Nothing
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Guardar_Multiple()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        btnEliminarPopup.Visible = True
        btnGuardarPopup.Visible = False
        lblMensajeGuardarPopup.Text = "Para Eliminar este registro pulse Elimnar"
        ModalPopupGuardar.Show()
    End Sub

    Private Sub Elminar_Registro()
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
        drop_01_T18Aplicacion.Text = Nothing
        drop_01_T18Perfil.Text = Nothing
        drop_01_T18NumDocFuncionario.Text = Nothing

        lbl_01_T18Aplicacion.ForeColor = Drawing.Color.Black
        lbl_01_T18Perfil.ForeColor = Drawing.Color.Black
        lbl_01_T18NumDocFuncionario.ForeColor = Drawing.Color.Black

        txtFiltroBusqueda.Text = Nothing

        MostrarMensaje("")
        bHabilitarControles(True)

        filtro()
    End Sub

    Private Sub bHabilitarControles(ByVal bEstado As Boolean)
        btnEliminar.Visible = False
        'btnImprimir.Visible = False
        btnLimpiar.Visible = False

    End Sub

    Private Function bValidarCampos(Optional ByVal sNumFuncionaio As String = "") As Boolean
        bValidarCampos = False
        lbl_01_T18Aplicacion.ForeColor = Drawing.Color.Red
        lbl_01_T18Perfil.ForeColor = Drawing.Color.Red
        lbl_01_T18NumDocFuncionario.ForeColor = Drawing.Color.Red
        If Trim(drop_01_T18Aplicacion.Text) = "" Then
            drop_01_T18Aplicacion.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: Aplicacion")
            Exit Function
        Else
            lbl_01_T18Aplicacion.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_01_T18Perfil.Text) = "" Then
            drop_01_T18Perfil.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: Perfil del funcionario: " & sNumFuncionaio)
            Exit Function
        Else
            lbl_01_T18Perfil.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_01_T18NumDocFuncionario.Text) = "" Then
            drop_01_T18NumDocFuncionario.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 18NumDocFuncionario")
            Exit Function
        Else
            lbl_01_T18NumDocFuncionario.ForeColor = Drawing.Color.Black
        End If


        bValidarCampos = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
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
    Private Sub btnGuardarPopup_Click(sender As Object, e As EventArgs) Handles btnGuardarPopup.Click
        Guardar_Registro()
    End Sub

    Private Sub btnEliminarPopup_Click(sender As Object, e As EventArgs) Handles btnEliminarPopup.Click
        Elminar_Registro()
    End Sub


    Private Sub btnGuardarCancelarPopu_Click(sender As Object, e As EventArgs) Handles btnGuardarCancelarPopu.Click
        ModalPopupGuardar.Hide()
    End Sub


    Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub


    Private Sub Habilitar_BotonGuardar(ByVal bHabilitar As Boolean)
        btnGuardar.Enabled = bHabilitar
    End Sub

    Private Sub Cargar_Registro()
        'Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object

        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla, sCamposTabla, sLlaves)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            Else
                drop_01_T18Aplicacion.Text = coleccionDatos(0)
                drop_01_T18Perfil.Text = coleccionDatos(1)
                drop_01_T18NumDocFuncionario.Text = coleccionDatos(2)

                filtro()
                bHabilitarControles(False)

            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
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
                MostrarMensaje("Se agrego Perfil correctamente ", True)
                Registro_Procesos("Guardar", clsAdminDb.Mostrar_Consulta)
                Guardar_Clave(drop_01_T18NumDocFuncionario.Text)
                drop_01_T18NumDocFuncionario.Text = ""
                bHabilitarControles(False)
                'drop_01_T18NumDocFuncionario.Text = ""
                'drop_01_T18Perfil.Text = ""
                bHabilitarControles(True)


            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            'bSqlInsert = clsAdminDb.sql_Update(sNombreTabla, sCamposUPD, sLlaves)
            If Val(lCantRegistros) <> 0 Then
                Guardar_Clave(drop_01_T18NumDocFuncionario.Text)
                MostrarMensaje("Se agrego Perfil correctamente ", True)
                'Registro_Procesos("Actualizar", clsAdminDb.Mostrar_Consulta)
                ' bHabilitarControles(False)
                'drop_01_T18NumDocFuncionario.Text = ""
                ' bHabilitarControles(False)
                'drop_01_T18NumDocFuncionario.Text = ""
                'drop_01_T18Perfil.Text = ""

                'bHabilitarControles(True)

                'filtro()
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
        'clsAdminDb = New adminitradorDB
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
        'Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection
        Dim sCamposTablaLocal As String = "_01_T12NombreApellidos,_01_T17Nombre,_01_T18Aplicacion,_01_T18Perfil,_01_T18NumDocFuncionario,(select top 1 _01_T24Nombre from _01_T24CARGOS where _01_T12Cargo=_01_T24Codigo)" '
        Dim sLlavesLocal As String = "_01_T12NumDoc=_01_T18NumDocFuncionario AND _01_T18Aplicacion=_01_T17Aplicacion  and _01_T17Codigo=_01_T18Perfil  and _01_T12NumDoc<>'ADMINISTRADOR'"
        Dim sTablaLocal As String = "_01_T17PERFILESxAPLICACION,_01_T18PERFILESxAPLICACIONxFUNCIONARIO,_01_T12FUNCIONARIOS"


        Dim ArregloSingular() As String
        Tbody2.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal
        Else
            If drop_01_T18Aplicacion.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Aplicacion & "='" & drop_01_T18Aplicacion.Text & "'"
            End If

            If drop_01_T18Perfil.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "='" & drop_01_T18Perfil.Text & "'"
            End If
            If drop_01_T18NumDocFuncionario.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "='" & drop_01_T18NumDocFuncionario.Text & "'"
            End If

        End If
        Dim i As Integer = 0
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow

                CeldaHtml = New HtmlTableCell
                Dim lit_Tem = New LiteralControl

                Dim sTexto As String = "<b>Nombre: </b> " & UCase(ArregloSingular(0)) '& "</br><b>  Inicio: </b>" & ArregloSingular(3) & "<b> Fin: </b>" & ArregloSingular(4) & "<b>  Dedicacion: </b>" & ArregloSingular(5) & "%"
                sTexto = sTexto & "</br><b>Cargo: </b> " & ArregloSingular(5)
                sTexto = sTexto & "</br><b>Perfil: </b> <u>" & UCase(ArregloSingular(1)) & "</u>"
                lit_Tem.Text = sTexto
                CeldaHtml.Controls.Add(lit_Tem)

                FilaHtml.Cells.Add(CeldaHtml)



                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Eliminar = New LinkButton
                'CeldaHtml.BorderColor = "YELLOW"
                linkbutonHtml_Eliminar.ID = i & "ii-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(4).ToString
                'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar)
                linkbutonHtml_Eliminar.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_Eliminar.ForeColor = Drawing.Color.DarkRed
                linkbutonHtml_Eliminar.Font.Bold = True
                linkbutonHtml_Eliminar.Font.Size = FontUnit.Point(8)
                AddHandler linkbutonHtml_Eliminar.Click, AddressOf linkbutonHtml_Eliminar_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_Eliminar.Text = "Eliminar"
                'linkbutonHtml_Eliminar.Enabled = False
                CeldaHtml.Controls.Add(linkbutonHtml_Eliminar)
                FilaHtml.Cells.Add(CeldaHtml)

                i = i + 1
                Tbody2.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
    End Sub

    Private Sub cargar_Tabla_Usuarios(Optional ByVal sPerfil As String = "")
        'Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection
        Dim sCamposTablaLocal As String = "_01_T12NombreApellidos,_01_T12NumDoc,(select top 1 _01_T24Nombre from _01_T24CARGOS where _01_T12Cargo=_01_T24Codigo)" '
        Dim sLlavesLocal As String = _01_T12FUNCIONARIOS.Campo_01_T12Activo & "='SI'  and _01_T12NumDoc<>'ADMINISTRADOR'"
        Dim sTablaLocal As String = "_01_T12FUNCIONARIOS"
        Dim ArregloSingular() As String
        Tbody1.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal
        Else
            If drop_01_T18NumDocFuncionario.Text = "" Then

            Else
                sLlavesLocal = sLlavesLocal & " And " & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & drop_01_T18NumDocFuncionario.Text & "'"
            End If
            'If drop_01_T10ODS.Text = "" Then
            '    Exit Sub
            'Else
            '    sLlavesLocal = sLlavesLocal & " And " & _01_T12FUNCIONARIOS.Campo_01_T12Disciplina & "='" & drop_01_T10Disciplina.Text & "'"
            'End If

            'sLlavesLocal = sLlavesLocal & "  And  not exists ( select *  from  _01_T10RECURSOSxODS where _01_T10NumDocFuncionario=_01_T12NumDoc and _01_T10ODS='" & drop_01_T10ODS.Text & "' and _01_T12Disciplina=_01_T10Disciplina)"
        End If
        Dim i As Integer = 0
        Dim lbl_Tem As Label
        Dim txt_FEchaInic As TextBox
        Dim txt_FEchaFin As TextBox
        Dim txt_Dedicacion As TextBox
        Dim filBox1 As New AjaxControlToolkit.FilteredTextBoxExtender
        Dim filBox2 As New AjaxControlToolkit.CalendarExtender
        Dim chk_tem As CheckBox

        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow


                CeldaHtml = New HtmlTableCell
                Dim lit_Tem = New LiteralControl

                Dim sTexto As String = "<b>Nombre: </b> " & UCase(ArregloSingular(0)) '& "</br><b>  Inicio: </b>" & ArregloSingular(3) & "<b> Fin: </b>" & ArregloSingular(4) & "<b>  Dedicacion: </b>" & ArregloSingular(5) & "%"
                sTexto = sTexto & "</br><b>Cargo: </b> " & ArregloSingular(2)
                lit_Tem.Text = sTexto
                CeldaHtml.Controls.Add(lit_Tem)

                FilaHtml.Cells.Add(CeldaHtml)





                CeldaHtml = New HtmlTableCell
                Dim drop_Tem = New DropDownList
                drop_Tem.ID = i & "BB-sep-" & ArregloSingular(1).ToString '& "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(7).ToString & "-sep-" & ArregloSingular(8).ToString
                'CeldaHtml.Align = "Center"
                For Each item As ListItem In drop_01_T18Perfil.Items
                    drop_Tem.Items.Add(New ListItem(item.Text, item.Value))
                Next
                If sPerfil <> "" Then
                    drop_Tem.Text = sPerfil

                End If
                drop_Tem.Width = Unit.Point(110)
                drop_Tem.Height = Unit.Point(20)
                CeldaHtml.Controls.AddAt(0, drop_Tem)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                chk_tem = New CheckBox
                chk_tem.ID = i & "FF-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(7).ToString & "-sep-" & ArregloSingular(8).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'chk_tem.Text = ArregloSingular(9)
                CeldaHtml.Controls.Add(chk_tem)
                CeldaHtml.Align = "Center"
                FilaHtml.Cells.Add(CeldaHtml)

                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                'linkbutonHtml.ID = i & "g-sep-" & ArregloSingular(5).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(7).ToString & "-sep-" & ArregloSingular(8).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = ArregloSingular(10)
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'FilaHtml.Cells.Add(CeldaHtml)

                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                'linkbutonHtml.ID = i & "h-sep-" & ArregloSingular(5).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(7).ToString & "-sep-" & ArregloSingular(8).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = ArregloSingular(11)
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Asignar = New LinkButton
                'CeldaHtml.BorderColor = "YELLOW"
                linkbutonHtml_Asignar.ID = i & "EE-sep-" & ArregloSingular(1).ToString '& "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(7).ToString & "-sep-" & ArregloSingular(8).ToString
                'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Asignar)
                linkbutonHtml_Asignar.OnClientClick = "bPreguntar = False;"
                linkbutonHtml_Asignar.ForeColor = Drawing.Color.DarkGreen
                linkbutonHtml_Asignar.Font.Bold = True
                linkbutonHtml_Asignar.Font.Size = FontUnit.Point(8)
                AddHandler linkbutonHtml_Asignar.Click, AddressOf linkbutonHtml_Asignar_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_Asignar.Text = "Agregar"
                'linkbutonHtml_Eliminar.Enabled = False
                CeldaHtml.Controls.Add(linkbutonHtml_Asignar)
                CeldaHtml.Align = "Center"
                FilaHtml.Cells.Add(CeldaHtml)

                i = i + 1
                Tbody1.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
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
        'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T01Descripcion,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
        Dim sllave1 As String = TestArray(1).ToString
        Dim sllave2 As String = TestArray(2).ToString
        Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        Dim sLlaves_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Aplicacion & "='" & sllave1 & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "='" & sllave2 & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "='" & sllave3 & "'"

        Dim bSqlDelete = clsAdminDb.sql_Delete(sNombreTabla, sLlaves_Tem)
        If bSqlDelete = True Then
            MostrarMensaje("se Elimino correctamente", True)


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
        Dim sllave2 As String = TestArray(2).ToString
        Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        sLlaves = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Aplicacion & "='" & sllave1 & "'"
        sLlaves = sLlaves & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "='" & sllave2 & "'"
        sLlaves = sLlaves & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "='" & sllave3 & "'"

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
                    sllaveTem = sllaveTem & _01_T10RECURSOSxODS.CampoLlave_01_T10ODS & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _01_T06DISCIPLINAS.Campo_01_T06Nombre & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _01_T11MATRIZTRABAJADORES.Campo_01_T11NombresApellidos & " like '" & TestArray(i).ToString & "%'"
                Else
                    sllaveTem = sllaveTem & " Or " & _01_T10RECURSOSxODS.CampoLlave_01_T10ODS & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _01_T06DISCIPLINAS.Campo_01_T06Nombre & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _01_T11MATRIZTRABAJADORES.Campo_01_T11NombresApellidos & " like '" & TestArray(i).ToString & "%'"


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

    '***************************************************************************************
    '*********** comienza logueo **********************************************************
    Private Sub MostrarMensaje_verificar(ByVal sMensaje As String)
        lblMensajeVerificar_Popup.Text = sMensaje
    End Sub


    Private Sub btnVerificarDocumento_Click(sender As Object, e As EventArgs) Handles btnVerificarDocumento.Click
        If txt_90_T06ClaveLocal.Text = "" Then
            MostrarMensaje_verificar("Por favor digite un numero de documento")
            ModalPopuplogeo.Show()
            Exit Sub
        End If
        Guardar_Clave()
    End Sub

    Private Sub btnVerificarCancelarPopu_Click(sender As Object, e As EventArgs) Handles btnVerificarCancelarPopu.Click
        clsAdminDb = Nothing
        Response.Redirect("login.aspx")
    End Sub


    Private Sub drop_01_T18NumDocFuncionario_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T18NumDocFuncionario.TextChanged
        cargar_Tabla()
        cargar_Tabla_Usuarios()
    End Sub

    Private Sub linkbutonHtml_Asignar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_Asignar.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T01Descripcion,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
        Dim sllave1 As String = TestArray(1).ToString

        'Cargar_Registro()
        Guardar_Multiple(sllave1)
        cargar_Tabla()
    End Sub

    Private Sub Guardar_Multiple(Optional ByVal sNumRecurso As String = "")
        Dim bGuardar_FactTem As Boolean = False
        Dim FilaHtml = New HtmlTableRow
        Dim sNumRecurso_TEm As String = ""

        For Each FilaHtml In Tbody1.Controls
            Dim sObjeto1 As New Object
            Dim sObjeto As New Object
            Dim lbl_Funcionrio As New Label
            Dim drop_Perfil_Tem As New DropDownList
            Dim txt_FechaFin_Tem As New TextBox
            Dim txt_Dedicacion_Tem As New TextBox
            Dim chk As New CheckBox
            chk = FilaHtml.Controls(2).Controls(0)

            If chk.Checked = True And sNumRecurso = "" Then
                Dim TestArray() As String = Split(chk.ID, "-sep-")
                drop_01_T18NumDocFuncionario.Text = TestArray(1).ToString
                drop_Perfil_Tem = FilaHtml.Controls(1).Controls(0)
                drop_01_T18Perfil.Text = drop_Perfil_Tem.Text

                Datos_Modulo()
                If bValidarCampos(TestArray(2).ToString) = False Then Exit Sub
                If bValidarSQL() = False Then Exit Sub
                Guardar_Registro()
            ElseIf sNumRecurso <> "" Then
                Dim TestArray() As String = Split(chk.ID, "-sep-")
                sNumRecurso_TEm = TestArray(1).ToString
                drop_01_T18NumDocFuncionario.Text = sNumRecurso
                drop_Perfil_Tem = FilaHtml.Controls(1).Controls(0)
                drop_01_T18Perfil.Text = drop_Perfil_Tem.Text
                If sNumRecurso_TEm = sNumRecurso Then
                    Datos_Modulo()
                    If bValidarCampos(TestArray(2).ToString) = False Then Exit Sub
                    If bValidarSQL() = False Then Exit Sub
                    Guardar_Registro()
                    Exit Sub
                End If


            End If
            'tran.Complete()
        Next

        cargar_Tabla()
        cargar_Tabla_Usuarios()

    End Sub

    Private Sub drop_01_T18Perfil_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T18Perfil.TextChanged
        'drop_01_T18Perfil.Text = drop_01_T18Perfil.Text
        cargar_Tabla_Usuarios(drop_01_T18Perfil.Text)
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim sOrderby As String = ""
        Dim coleccionDatosPlural As New Collection

        Dim sCamposTablaLocal As String = "_01_T12NombreApellidos,_01_T17Nombre,_01_T16Nombre as _01_T18Aplicacion,_01_T18Perfil,_01_T18NumDocFuncionario,_01_T12Cargo" '
        Dim sLlavesLocal As String = "_01_T12NumDoc=_01_T18NumDocFuncionario AND _01_T18Aplicacion=_01_T17Aplicacion  and _01_T17Codigo=_01_T18Perfil and _01_T16Codigo=_01_T18Aplicacion  and _01_T16Codigo=_01_T17Aplicacion"
        Dim sTablaLocal As String = "_01_T17PERFILESxAPLICACION,_01_T18PERFILESxAPLICACIONxFUNCIONARIO,_01_T12FUNCIONARIOS,_01_T16APLICACIONES"

        If drop_01_T18Aplicacion.Text <> "" Then
            sLlavesLocal = sLlavesLocal & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Aplicacion & "='" & drop_01_T18Aplicacion.Text & "'"
        End If

        If drop_01_T18Perfil.Text <> "" Then
            sLlavesLocal = sLlavesLocal & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "='" & drop_01_T18Perfil.Text & "'"
        End If
        If drop_01_T18NumDocFuncionario.Text <> "" Then
            sLlavesLocal = sLlavesLocal & " and " & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "='" & drop_01_T18NumDocFuncionario.Text & "'"
        End If


        Dim sRutaArchivo As String = "\Reportes\Ontime\Perfiles\rpt_ListadoUsuariosxPerfil.rpt"
        Dim sConsulta As String = " select " & sCamposTablaLocal & " from " & sTablaLocal & " where " & sLlavesLocal
        rptCrystal = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        rptCrystal = clsAdminDb.sql_Imprimir(sConsulta, sRutaArchivo)

        If clsAdminDb.Mostrar_Error <> "" Then
            MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
        Else
            MostrarMensaje("Se genero correctamente el reporte")
            rptCrystal.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, True, "rpt_LitadoUsuriosxPerfil")
        End If
    End Sub


    Private Sub Guardar_Clave(ByVal sNumDocFuncionario As String)
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0
        Dim sNombreTablaTem As String = _01_T36SSESIONUSUARIO.NombreTabla
        Dim sCamposTablaTem As String = _01_T36SSESIONUSUARIO.CamposTabla
        Dim sCamposUPDTem As String = _01_T36SSESIONUSUARIO.CampoLlave_01_T36DocFuncionario & "='" & clsAdminDb.sRemoverHTML(sNumDocFuncionario) & "'"
        Dim dFecha As Date
        Dim sFecha As String = ""
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
        Else
        End If
        Dim sLlavesTem As String = _01_T36SSESIONUSUARIO.CampoLlave_01_T36DocFuncionario & "=  '" & sNumDocFuncionario & "'" & " AND " & _01_T36SSESIONUSUARIO.CampoLlave_01_T36Aplicacion & "=  '" & sCod_Aplicacion & "'" & " AND " & _01_T36SSESIONUSUARIO.Campo_01_T36Fecha & "=  '" & sFecha & "'"

        Dim coleccionDatos As Object
        sNombreTablaTem = _01_T12FUNCIONARIOS.NombreTabla
        sLlavesTem = _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & sNumDocFuncionario & "'" ' AND " & _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "=''"
        Dim sPerfilAdmin As String = "(select _01_T18Perfil  from _01_T18PERFILESxAPLICACIONxFUNCIONARIO where _01_T18NumDocFuncionario='" & sNumDocFuncionario & "' and _01_T18Perfil='01' and _01_T18Aplicacion='" & sCod_Aplicacion & "' and _01_T18NumDocFuncionario=_01_T12NumDoc)"
        Dim sPerfilCoordinador As String = "(select top 1 _01_T18Perfil  from _01_T18PERFILESxAPLICACIONxFUNCIONARIO where _01_T18NumDocFuncionario='" & sNumDocFuncionario & "' and _01_T18Perfil='03' and _01_T18Aplicacion='" & sCod_Aplicacion & "' and _01_T18NumDocFuncionario=_01_T12NumDoc)"
        Dim sPerfilLider As String = "(select top 1  _01_T18Perfil  from _01_T18PERFILESxAPLICACIONxFUNCIONARIO where _01_T18NumDocFuncionario='" & sNumDocFuncionario & "' and _01_T18Perfil='02' and _01_T18Aplicacion='" & sCod_Aplicacion & "' and _01_T18NumDocFuncionario=_01_T12NumDoc)"
        Dim sPerfiCoordinador As String = "(select top 1  _01_T18Perfil  from _01_T18PERFILESxAPLICACIONxFUNCIONARIO where _01_T18NumDocFuncionario='" & sNumDocFuncionario & "' and _01_T18Perfil='02' and _01_T18Aplicacion='" & sCod_Aplicacion & "' and _01_T18NumDocFuncionario=_01_T12NumDoc)"
        Dim sPerfiFuncinario As String = "(select top 1 _01_T18Perfil  from _01_T18PERFILESxAPLICACIONxFUNCIONARIO where _01_T18NumDocFuncionario='" & sNumDocFuncionario & "' and _01_T18Perfil='04' and _01_T18Aplicacion='" & sCod_Aplicacion & "' and _01_T18NumDocFuncionario=_01_T12NumDoc)"
        Dim sPerfiAprobador As String = "(select top 1 _01_T18Perfil  from _01_T18PERFILESxAPLICACIONxFUNCIONARIO where _01_T18NumDocFuncionario='" & sNumDocFuncionario & "' and _01_T18Perfil='06' and _01_T18Aplicacion='" & sCod_Aplicacion & "' and _01_T18NumDocFuncionario=_01_T12NumDoc)"
        Dim sPerfiRecursosHumanos As String = "(select top 1 _01_T18Perfil  from _01_T18PERFILESxAPLICACIONxFUNCIONARIO where _01_T18NumDocFuncionario='" & sNumDocFuncionario & "' and _01_T18Perfil='05' and _01_T18Aplicacion='" & sCod_Aplicacion & "' and _01_T18NumDocFuncionario=_01_T12NumDoc)"

        sCamposTablaTem = _01_T12FUNCIONARIOS.Campo_01_T12Activo & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "," & _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "," & sPerfilAdmin & "," & sPerfilLider & "," & sPerfilCoordinador & "," & sPerfiAprobador & "," & sPerfiFuncinario & "," & _01_T12FUNCIONARIOS.Campo_01_T12Contrasena & "," & sPerfiRecursosHumanos
        coleccionDatos = clsAdminDb.sql_Select(sNombreTablaTem, sCamposTablaTem, sLlavesTem)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
            Else
                If coleccionDatos(0) = "NO" Then
                    Exit Sub
                End If

                bSqlInsert = clsAdminDb.sql_Update(_01_T12FUNCIONARIOS.NombreTabla, _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & sNumDocFuncionario & "@seringtec.com'", _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & sNumDocFuncionario & "'")
                If bSqlInsert = True Then
                    Dim sCamposINS_TEm = "'" & sCod_Aplicacion & "'" & "," & "'" & sNumDocFuncionario & "'" & "," & "'" & sNumDocFuncionario & "'" & "," & "'" & sFecha & "'" & "," & "'" & Now.Hour & "'" & "," & "'" & coleccionDatos(3) & "'" & "," & "'" & coleccionDatos(4) & "'" & "," & "'" & coleccionDatos(5) & "'" & "," & "'" & coleccionDatos(6) & "'" & "," & "'" & coleccionDatos(7) & "'" & "," & "'" & coleccionDatos(9) & "'"
                    Dim bGuardarSeseion As Boolean = False
                    clsAdminDb.sql_Delete(_01_T36SSESIONUSUARIO.NombreTabla, _01_T36SSESIONUSUARIO.CampoLlave_01_T36DocFuncionario & "='" & sNumDocFuncionario & "' and " & _01_T36SSESIONUSUARIO.CampoLlave_01_T36Aplicacion & "='" & sCod_Aplicacion & "'")
                    bGuardarSeseion = clsAdminDb.sql_Insert(_01_T36SSESIONUSUARIO.NombreTabla, _01_T36SSESIONUSUARIO.CamposTabla, sCamposINS_TEm)
                    'Session("DatoUsuario" & sCod_Aplicacion) = coleccionDatos(1)
                    'Session("DatoUsuario" & sCod_Aplicacion) = sCod_Aplicacion & "-" & coleccionDatos(3)
                    If bGuardarSeseion = True Then
                        'ModalPopuplogeo.Hide()
                        'Response.Redirect("plataforma.aspx")
                    Else
                        'ModalPopuplogeo.Show()
                    End If

                Else
                    If clsAdminDb.Mostrar_Error <> "" Then
                    Else
                        'ModalPopuplogeo.Hide()
                    End If
                End If


            End If
        Else
        End If


    End Sub


End Class


