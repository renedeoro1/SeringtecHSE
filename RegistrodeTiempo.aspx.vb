Imports System.IO

Public Class RegistrodeTiempo_Tem
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
    Protected WithEvents linkbutonHtml_Eliminar_Editar As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_CodigoEntregale_Busqueda As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_CodigoEntregale_BusquedaActividad As System.Web.UI.WebControls.LinkButton
    Dim sUsuarioWindows = System.Environment.UserName

    Dim rptCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsAdminDb = New adminitradorDB

        'ImageButton_EntreableEco.Visible = True
        Try
            If Not IsNothing(Request.QueryString("CodTmpOK")) Then
                Dim sNumDocFuncionarioRegistrado_TEm As String = Request.QueryString("CodTmpOK").ToString
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "nrWE", "-*-")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "UYFh", "/")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "AxRA", "0")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "vGHt", "1")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "FBGu", "2")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "Mnyz", "3")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "pLIj", "4")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "XVUR", "5")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "wQZX", "6")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "EdYH", "7")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "ZXrY", "8")
                sNumDocFuncionarioRegistrado_TEm = Replace(sNumDocFuncionarioRegistrado_TEm, "QWST", "9")
                Dim TestArray2() As String = Split(sNumDocFuncionarioRegistrado_TEm, "-*-")
                Dim sFechaTEm = TestArray2(0).ToString
                Dim sDocTEm = TestArray2(1).ToString

                Dim dFecha As Date
                Dim sFecha As String = ""
                If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
                Else
                End If
                If sFecha <> sFechaTEm Then
                Else
                    Session("DatoUsuario" & sCod_Aplicacion) = sDocTEm
                    Guardar_Clave(sDocTEm)
                End If
            End If
        Catch ex As Exception

        End Try


        If Session("DatoUsuario" & sCod_Aplicacion) Is Nothing Then
            Session.Abandon()
            Response.Redirect("login.aspx?Err2=001")
        Else
            sUsuarioWindows = Session("DatoUsuario" & sCod_Aplicacion)
            bVerficarLogin()
        End If
        Datos_Modulo()
        'txt_01_T03Usuario.Text = sUsuarioWindows
        If Not IsPostBack Then
            drop_Proyecto_Reenviar.AutoPostBack = True
            drop_Entregable_Reenviar.AutoPostBack = True
            drop_Actividades_Reenviar.AutoPostBack = True
            drop_01_T03Cliente.AutoPostBack = True
            drop_01_T03Contrato.AutoPostBack = True
            drop_01_T03ODS.AutoPostBack = True
            drop_01_T03Disciplina.AutoPostBack = True
            drop_01_T03Entregable.AutoPostBack = True
            drop_01_T03EntregableSeringrtec.AutoPostBack = True
            drop_01_T03Version.AutoPostBack = True
            drop_01_T03Fecha.AutoPostBack = True
            Drop_01_T03Horas.AutoPostBack = True
            drop_01_T03Aprobado.AutoPostBack = True
            DropODS_Ver.AutoPostBack = True
            txt_CodigoBusquedaEntregable.AutoPostBack = True
            txt_NombreEntegable.AutoPostBack = True
            txt_CodigoBusquedaEntregable.AutoPostBack = True
            txt_NombreEntegable.AutoPostBack = True
            txt_CodigoBusquedaEntregableActividad.AutoPostBack = True
            txt_NombreEntegableActividad.AutoPostBack = True

            txtFiltroBusqueda.AutoPostBack = True

            bVerficarLogin()
            CargarDrops()

            limpiarCampos()
            Cargar_ODS()
            Registro_Procesos("Consultar", clsAdminDb.Mostrar_Consulta)
        Else
            bVerficarLogin()
            'CargarDrops()

        End If
        cargar_Tabla_ver()
        cargar_Tabla_BusquedaEntregable()
        cargar_Tabla_BusquedaEntregableActividades()
        filtro()
        MostrarMensaje("")
    End Sub

    Private Sub bVerficarLogin()
        Dim dFecha As Date
        Dim sFecha As String = ""
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
        Else
        End If
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
                'If coleccionDatos(5) = "01" Or coleccionDatos(9) = "04" Then
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


    Private Sub Datos_Modulo()
        Dim dFecha As Date
        Dim sFecha As String = ""
        Dim sHora As String = ""
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
        Else
        End If
        sHora = Now.Hour & ":" & Now.Minute

        sCodModulo = _01_T03REGISTROHORAS.CodigoModulo
        sNombreTabla = _01_T03REGISTROHORAS.NombreTabla
        sCamposTabla = _01_T03REGISTROHORAS.CamposTabla
        sCamposINS = "'" & clsAdminDb.sRemoverHTML(lblUsuarioLogueado_Documento.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Fecha.Text) & "'" & "," & "'" & drop_01_T03Contrato.Text & "'" & "," & "'" & drop_01_T03ODS.Text & "'" & "," & "'" & drop_01_T03Entregable.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Consecutivo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Version.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Horas.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03DescripcionAcguividad.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03TiempoaHoras.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Aprobado.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Hora.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Observacion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Observacion_Rechazo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03EnviadoAProbacion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Disciplina.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03HorasSolo.Text) & "'" & "," & "'" & txt_01_T03MinutoSolo.Text & "'" & "," & "'" & drop_01_T03Cliente.Text & "','','','','','','','','','" & drop_01_T03Actividad.Text & "','" & txt_01_T03Actividad_Texto.Text & "','" & drop_01_T03EntregableSeringrtec.Text & "','" & sFecha & "','" & sHora & "'"
        sCamposUPD = _01_T03REGISTROHORAS.Campo_01_T03Version & "='" & clsAdminDb.sRemoverHTML(drop_01_T03Version.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03tiempo & "='" & clsAdminDb.sRemoverHTML(txt_01_T03Horas.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03DescripcionAcguividad & "='" & clsAdminDb.sRemoverHTML(txt_01_T03DescripcionAcguividad.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Observaciones & "='" & clsAdminDb.sRemoverHTML(txt_01_T03TiempoaHoras.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Estado & "='" & clsAdminDb.sRemoverHTML(drop_01_T03Aprobado.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Aprobado_Fecha & "='" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Fecha.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Aprobado_Hora & "='" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Hora.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Aprobado_IDAprobacion & "='" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Observacion.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Aprobado_Rechazo_Obs & "='" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Observacion_Rechazo.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03EnviadoAProbacion & "='" & clsAdminDb.sRemoverHTML(txt_01_T03EnviadoAProbacion.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Disciplina & "='" & clsAdminDb.sRemoverHTML(drop_01_T03Disciplina.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Horas & "='" & clsAdminDb.sRemoverHTML(txt_01_T03HorasSolo.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03Minutos & "='" & clsAdminDb.sRemoverHTML(txt_01_T03MinutoSolo.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03ActividadesCodigo & "='" & clsAdminDb.sRemoverHTML(drop_01_T03Actividad.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03ActividadesDescripcion & "='" & clsAdminDb.sRemoverHTML(txt_01_T03Actividad_Texto.Text) & "'" & "," & _01_T03REGISTROHORAS.Campo_01_T03EntregableSeringtec & "='" & drop_01_T03EntregableSeringrtec.Text & "'"
        sLlaves = _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "=  '" & lblUsuarioLogueado_Documento.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Fecha & "=  '" & drop_01_T03Fecha.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Contrato & "=  '" & drop_01_T03Contrato.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03ODS & "=  '" & drop_01_T03ODS.Text & "'" & " AND " & _01_T03REGISTROHORAS.Campo_01_T03ActividadesCodigo & "=  '" & drop_01_T03Actividad.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Consecutivo & "=  '" & txt_01_T03Consecutivo.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Cliente & "=  '" & drop_01_T03Cliente.Text & "'"
    End Sub

    Private Sub VerificarPermisoConsulta(ByVal sPerfilAdmin As String, ByVal sPerfilLIDER As String, ByVal sPerfilCoordinador As String, ByVal sPerfilAprobador As String, ByVal sPerfilFuncionario As String, ByVal sPerfilRecursosHumanos As String, ByVal sPerfilControlProyectos As String, ByVal sPerfilControlDocumentos As String)

        'clsAdminDb = New adminitradorDB
        Dim stabla_Tem As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
        Dim sCAmpos_Tem As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        Dim sLlaves_Tem As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & " AND " & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & sUsuarioWindows & "'"
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

        'stabla_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla & "," & _01_T12FUNCIONARIOS.Campo_01_T12Disciplina
        sCAmpos_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "," & _01_T12FUNCIONARIOS.Campo_01_T12Disciplina
        'sLlaves_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & " AND " & _01_T12FUNCIONARIOS.Campo_01_T12CorreoEntidad & "='" & sUsuarioWindows & "@SERINGTEC.COM" & "'"
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "=" & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario

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
                lblUsuarioLogueado_Nombre2.Text = coleccionDatos(1)
                lblUsuarioLogueado_Disciplina.Text = coleccionDatos(3)
                lblUsuarioLogueado_Documento.Text = coleccionDatos(2)
                drop_01_T03Disciplina.Text = lblUsuarioLogueado_Disciplina.Text
                'Cargar_ODS()
            End If
        Else

        End If



        stabla_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla & "," & _01_T10RECURSOSxODS.NombreTabla
        sCAmpos_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "," & _01_T10RECURSOSxODS.CampoLlave_01_T10Disciplina
        sLlaves_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & " AND " & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & sUsuarioWindows & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        sLlaves_Tem = sLlaves_Tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "=" & _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"
        'sLlaves_Tem = sLlaves_Tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10Disciplina & "='" & lblUsuarioLogueado_Disciplina.Text & "'"


        coleccionDatosPlural = clsAdminDb.sql_Coleccion(stabla_Tem, sCAmpos_Tem, sLlaves_Tem)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular

                lblUsuarioLogueado_Nombre.Text = ArregloSingular(1)
                'lblUsuarioLogueado_Disciplina.Text = ArregloSingular(3)
                lblUsuarioLogueado_Documento.Text = ArregloSingular(2)
                'drop_01_T03Disciplina.Items.Clear()
                'drop_01_T03Disciplina.Items.Add(New ListItem(lblUsuarioLogueado_Disciplina.Text, lblUsuarioLogueado_Disciplina.Text))
                'drop_01_T03Disciplina.Text = lblUsuarioLogueado_Disciplina.Text



            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then

            Else

            End If
        End If

        stabla_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
        sCAmpos_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil & "," & _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos & "," & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc
        sLlaves_Tem = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "=" & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & " AND " & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & sUsuarioWindows & "@SERINGTEC.COM" & "'"
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


            '.drop_Cargar_SINO(drop_80_T02Activo, False)
            'dropiluminacionInadecuada.Items.Add(New ListItem(", "))

            drop_Contrato_Reenviar.Items.Add(New ListItem("Seleccionar", ""))
            drop_Entregable_Reenviar.Items.Add(New ListItem("Seleccionar", ""))
            drop_Proyecto_Reenviar.Items.Add(New ListItem("Seleccionar", ""))
            'drop_01_T03Aprobado.Items.Add(New ListItem("Registrado", "Registrado"))
            'drop_01_T03Aprobado.Items.Add(New ListItem("", ""))
            drop_Version_Reenviar.Items.Add(New ListItem("", ""))


            drop_01_T03Contrato.Items.Add(New ListItem("Seleccionar", ""))
            drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))
            drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
            drop_01_T03ODS.Items.Add(New ListItem("Seleccionar", ""))
            drop_01_T03Aprobado.Items.Add(New ListItem("Registrado", "Registrado"))
            'drop_01_T03Aprobado.Items.Add(New ListItem("", ""))
            drop_01_T03Version.Items.Add(New ListItem("", ""))

            'Dim sTables_tem As String = _07_Cont_T11_1EMPRESATRABAJAR.NombreTabla & "," & _04_T20ENTIDAD.NombreTabla
            'Dim sCampos_tem As String = _04_T20ENTIDAD.Campo_04_T20TipoPuc & "," & _07_Cont_T11_1EMPRESATRABAJAR.CampoLlave_07_Cont_T11_1CodEmpresa & "," & _04_T20ENTIDAD.Campo_04_T20NumDoc
            'Dim sLlaves_tem As String = _04_T20ENTIDAD.CampoLlave_04_T20Codigo & "=" & _07_Cont_T11_1EMPRESATRABAJAR.CampoLlave_07_Cont_T11_1CodEmpresa
            'sLlaves_tem = sLlaves_tem & " and " & _07_Cont_T11_1EMPRESATRABAJAR.CampoLlave_07_Cont_T11_1Usuario & "='" & txtUsuario_Nombre.Text & "'"

            '.drop_CargarCombox(drop_01_T03Disciplina, _01_T06DISCIPLINAS.NombreTabla, _01_T06DISCIPLINAS.CampoLlave_01_T06Codigo & "," & _01_T06DISCIPLINAS.Campo_01_T06Nombre, _01_T06DISCIPLINAS.Campo_01_T06Activo & "='SI'", True)
            Drop_01_T03Horas.Items.Add(New ListItem("", ""))
            Drop_01_T03Horas.Items.Add(New ListItem("00:15", "00:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("00:30", "00:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("00:45", "00:45"))
            Drop_01_T03Horas.Items.Add(New ListItem("01:00", "01:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("01:15", "01:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("01:30", "01:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("01:45", "01:45"))
            Drop_01_T03Horas.Items.Add(New ListItem("02:00", "02:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("02:15", "02:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("02:30", "02:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("02:45", "02:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("03:00", "03:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("03:15", "03:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("03:30", "03:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("03:45", "03:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("04:00", "04:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("04:15", "04:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("04:30", "04:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("04:45", "04:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("05:00", "05:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("05:15", "05:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("05:30", "05:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("05:45", "05:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("06:00", "06:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("06:15", "06:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("06:30", "06:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("06:45", "06:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("07:00", "07:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("07:15", "07:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("07:30", "07:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("07:45", "07:45"))


            Drop_01_T03Horas.Items.Add(New ListItem("08:00", "08:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("08:15", "08:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("08:30", "08:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("08:45", "08:45"))


            Drop_01_T03Horas.Items.Add(New ListItem("09:00", "09:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("09:15", "09:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("09:30", "09:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("09:45", "09:45"))


            Drop_01_T03Horas.Items.Add(New ListItem("10:00", "10:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("10:15", "10:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("10:30", "10:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("10:45", "10:45"))


            Drop_01_T03Horas.Items.Add(New ListItem("11:00", "11:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("11:15", "11:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("11:30", "11:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("11:45", "11:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("12:00", "12:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("12:15", "12:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("12:30", "12:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("12:45", "12:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("13:00", "13:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("13:15", "13:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("13:30", "13:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("13:45", "13:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("14:00", "14:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("14:15", "14:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("14:30", "14:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("14:45", "14:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("15:00", "15:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("15:15", "15:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("15:30", "15:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("15:45", "15:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("16:00", "16:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("16:15", "16:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("16:30", "16:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("16:45", "16:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("17:00", "17:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("17:15", "17:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("17:30", "17:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("17:45", "17:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("18:00", "18:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("18:15", "18:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("18:30", "18:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("18:45", "18:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("19:00", "19:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("19:15", "19:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("19:30", "19:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("19:45", "19:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("20:00", "20:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("20:15", "20:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("20:30", "20:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("20:45", "20:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("21:00", "21:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("21:15", "21:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("21:30", "21:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("21:45", "21:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("22:00", "22:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("22:15", "22:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("22:30", "22:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("22:45", "22:45"))

            Drop_01_T03Horas.Items.Add(New ListItem("23:00", "23:00"))
            Drop_01_T03Horas.Items.Add(New ListItem("23:15", "23:15"))
            Drop_01_T03Horas.Items.Add(New ListItem("23:30", "23:30"))
            Drop_01_T03Horas.Items.Add(New ListItem("23:45", "23:45"))

            For Each aItem As ListItem In Drop_01_T03Horas.Items
                drop_Tiempo_Reenviar.Items.Add(New ListItem(aItem.Text, aItem.Value))
            Next


            CargarDrops_FEchas()

        End With
    End Sub

    Private Sub CargarDrops_FEchas()
        drop_01_T03Fecha.Items.Clear()
        'clsAdminDb = New adminitradorDB
        With clsAdminDb
            Dim coleccionDatosPlural As New Collection
            Dim ArregloSingular() As String

            Dim lFinal As Integer = Fix(CDate(Now.Date).ToOADate)
            Dim lInicial As Integer = lFinal - 1
            Dim iNumDiadeSemana As Integer = Now.DayOfWeek
            Dim iNumDiaTEmporal As Integer

            drop_01_T03Fecha.Items.Add(New ListItem("", ""))
            If iNumDiadeSemana = 1 Then
                iNumDiaTEmporal = lFinal - 3
                coleccionDatosPlural = clsAdminDb.sql_Coleccion(_01_T22AGNOS.NombreTabla, "_01_T22Fecha", "_01_T22FechaNumero=" & iNumDiaTEmporal)
                If (Not coleccionDatosPlural Is Nothing) Then
                    For Each ColeccionSingular In coleccionDatosPlural
                        ArregloSingular = ColeccionSingular
                        drop_01_T03Fecha.Items.Add(New ListItem(ArregloSingular(0) & " (viernes)", ArregloSingular(0)))
                    Next
                End If
                iNumDiaTEmporal = lFinal - 2
                coleccionDatosPlural = clsAdminDb.sql_Coleccion(_01_T22AGNOS.NombreTabla, "_01_T22Fecha", "_01_T22FechaNumero=" & iNumDiaTEmporal)
                If (Not coleccionDatosPlural Is Nothing) Then
                    For Each ColeccionSingular In coleccionDatosPlural
                        ArregloSingular = ColeccionSingular
                        drop_01_T03Fecha.Items.Add(New ListItem(ArregloSingular(0) & " (Sabado)", ArregloSingular(0)))
                    Next
                End If

            ElseIf iNumDiadeSemana = 2 Then
                iNumDiaTEmporal = lFinal - 4
                Dim iEsFEstivo As Integer = 0
                iEsFEstivo = clsAdminDb.sql_Count(_01_T22AGNOS.NombreTabla, _01_T22AGNOS.Campo_01_T22FechaNumero & "=" & lFinal & " and _01_T22DiaFestivo='SI'")
                If iEsFEstivo <> 0 Then
                    coleccionDatosPlural = clsAdminDb.sql_Coleccion(_01_T22AGNOS.NombreTabla, "_01_T22Fecha,_01_T22DiaFestivo", "_01_T22FechaNumero=" & iNumDiaTEmporal)
                    If (Not coleccionDatosPlural Is Nothing) Then
                        For Each ColeccionSingular In coleccionDatosPlural
                            ArregloSingular = ColeccionSingular
                            drop_01_T03Fecha.Items.Add(New ListItem(ArregloSingular(0) & " (viernes)", ArregloSingular(0)))
                        Next
                    End If
                    iNumDiaTEmporal = lFinal - 3
                    coleccionDatosPlural = clsAdminDb.sql_Coleccion(_01_T22AGNOS.NombreTabla, "_01_T22Fecha,_01_T22DiaFestivo", "_01_T22FechaNumero=" & iNumDiaTEmporal)
                    If (Not coleccionDatosPlural Is Nothing) Then
                        For Each ColeccionSingular In coleccionDatosPlural
                            ArregloSingular = ColeccionSingular
                            drop_01_T03Fecha.Items.Add(New ListItem(ArregloSingular(0) & " (Sabado)", ArregloSingular(0)))
                        Next
                    End If

                End If

            End If



            coleccionDatosPlural = clsAdminDb.sql_Coleccion(_01_T22AGNOS.NombreTabla, "_01_T22Fecha", "_01_T22FechaNumero>=" & lInicial & "and _01_T22FechaNumero<=" & lFinal, "_01_T22FechaNumero asc")
            If (Not coleccionDatosPlural Is Nothing) Then
                For Each ColeccionSingular In coleccionDatosPlural
                    ArregloSingular = ColeccionSingular
                    Dim IExisteFechaEspeciales As Integer = 0
                    IExisteFechaEspeciales = clsAdminDb.sql_Count(_01_T31FECHASESPECIALES.NombreTabla, _01_T31FECHASESPECIALES.CampoLlave_01_T31Fecha & "='" & ArregloSingular(0) & "'")
                    If IExisteFechaEspeciales <> 0 Then
                    Else
                        drop_01_T03Fecha.Items.Add(New ListItem(ArregloSingular(0), ArregloSingular(0)))
                    End If

                Next
            Else

            End If

            Dim sTablesTem As String = _01_T22AGNOS.NombreTabla & "," & _01_T29NOVEDADES.NombreTabla & "," & _01_T28TIPONOVEDADES.NombreTabla
            Dim sLevesTEm As String = _01_T29NOVEDADES.CampoLlave_01_T29NumDocFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"
            sLevesTEm = sLevesTEm & " and " & _01_T29NOVEDADES.CampoLlave_01_T29TipoNovedad & "=" & _01_T28TIPONOVEDADES.CampoLlave_01_T28Codigo
            sLevesTEm = sLevesTEm & " and " & _01_T29NOVEDADES.Campo_01_T29FechaRegistro & "=" & _01_T22AGNOS.CampoLlave_01_T22Fecha
            Dim lFechaActual As Integer = Fix(Now.ToOADate)
            sLevesTEm = sLevesTEm & " and ((cast(_01_T28DiasActivo  As Integer) + _01_T22FechaNumero) >=" & lFechaActual & ")"
            Dim sCamposTem As String = _01_T29NOVEDADES.Campo_01_T29FechaInicial & "," & _01_T29NOVEDADES.Campo_01_T29FechaFinal & "," & _01_T28TIPONOVEDADES.Campo_01_T28Nombre & "," & _01_T28TIPONOVEDADES.Campo_01_T28DiasActivo

            coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablesTem, sCamposTem, sLevesTEm, "_01_T22FechaNumero asc")
            If (Not coleccionDatosPlural Is Nothing) Then
                For Each ColeccionSingular In coleccionDatosPlural
                    ArregloSingular = ColeccionSingular
                    Dim lfechaInicial As Integer = Fix(CDate(ArregloSingular(0)).ToOADate)
                    Dim lfechaFinal As Integer = Fix(CDate(ArregloSingular(1)).ToOADate)
                    For i = lfechaInicial To lfechaFinal

                        Dim dFecha As Date = Nothing
                        'Dim sFechaInicial As String = ""
                        dFecha = DateTime.FromOADate(i)
                        Dim sFechaInicial As String = Nothing
                        sFechaInicial = ""
                        If bValidarFecha(dFecha, dFecha.Day, dFecha.Month, dFecha.Year, sFechaInicial) = True Then
                            drop_01_T03Fecha.Items.Add(New ListItem(sFechaInicial & " - (" & ArregloSingular(2) & " - Plazo " & ArregloSingular(3) & " dias)", sFechaInicial))
                        Else
                        End If



                    Next

                Next
            Else

            End If


        End With
    End Sub

    Private Sub WebForm1_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        'Suma_Horas()
        clsAdminDb = Nothing
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'btnEliminarPopup.Visible = False
        'btnGuardarPopup.Visible = True
        'ModalPopupGuardar.Show()
        'lblMensajeGuardarPopup.Text = "Para Enviar estas Horas para Aprobacion  dar click en Enviar"
        chkTodos_Ver.Checked = False

        txt_Popup_Numdoc_Ver.Text = lblUsuarioLogueado_Documento.Text
        Dim coleccionDatos As Object
        coleccionDatos = clsAdminDb.sql_Select(_01_T12FUNCIONARIOS.NombreTabla, _01_T12FUNCIONARIOS.Campo_01_T12NombreApellidos, _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & txt_Popup_Numdoc_Ver.Text & "'")
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
            Else
                lbl_Popup_NombreFuncionario_Ver.Text = coleccionDatos(0) & " Por Enviar "
            End If

        End If
        If drop_01_T03ODS.Text <> "" Then
            DropODS_Ver.Text = drop_01_T03ODS.Text
        End If

        ModalPopupVer.Show()
        lblMensajeGuardarPopup.Text = "Informacion de Horas Registradas"
        'Exit Sub
        cargar_Tabla_ver()


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
        Exit Sub
        bSqlDelete = clsAdminDb.sql_Delete(sNombreTabla, sLlaves)
        If bSqlDelete = True Then
            //Guardar_Transaccion(sCod_Aplicacion, sCodModulo, lblUsuarioLogueado_Documento.Text, lblUsuarioLogueado_Nombre.Text, "03", clsAdminDb.Mostrar_Consulta)
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
        drop_01_T03Actividad.Items.Clear()
        drop_01_T03Entregable.Items.Clear()
        drop_01_T03EntregableSeringrtec.Items.Clear()
        'drop_01_T03ODS.Items.Clear()
        drop_01_T03Version.Items.Clear()
        drop_01_T03Version.Items.Add(New ListItem("", ""))
        drop_01_T03Actividad.Items.Add(New ListItem("Seleccionar", ""))

        txt_01_T03Actividad_Texto.Text = ""
        drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03ODS.Text = ""
        'drop_01_T03ODS.Items.Add(New ListItem("Seleccionar", ""))

        'txt_01_T03Usuario.Text = txtUsuario_NumDoc.Text

        Dim dFecha As Date
        Dim sFecha As String = ""
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
            'drop_01_T03Fecha.Text = sFecha
            txt_01_T23Fecha.Text = sFecha
        Else
            drop_01_T03Fecha.Text = Nothing
        End If



        drop_01_T03Contrato.Text = Nothing
        drop_01_T03ODS.Text = Nothing
        Try
            drop_01_T03Entregable.Text = Nothing
        Catch ex As Exception

        End Try
        Try
            drop_01_T03EntregableSeringrtec.Text = Nothing
        Catch ex As Exception

        End Try


        txt_01_T03Consecutivo.Text = Nothing
        drop_01_T03Version.Text = Nothing
        txt_01_T03Horas.Text = Nothing
        txt_01_T03DescripcionAcguividad.Text = Nothing
        txt_01_T03TiempoaHoras.Text = Nothing
        drop_01_T03Aprobado.Text = Nothing
        txt_01_T03Aprobado_Fecha.Text = Nothing
        txt_01_T03Aprobado_Hora.Text = Nothing
        txt_01_T03Aprobado_Observacion.Text = Nothing
        txt_01_T03Aprobado_Observacion_Rechazo.Text = Nothing
        txt_01_T03EnviadoAProbacion.Text = Nothing
        Drop_01_T03Horas.Text = Nothing
        txt_01_T03MinutoSolo.Text = Nothing

        'lbl_01_T03Usuario.ForeColor = Drawing.Color.Black
        lbl_01_T03Fecha.ForeColor = Drawing.Color.Black
        lbl_01_T03Contrato.ForeColor = Drawing.Color.Black
        lbl_01_T03ODS.ForeColor = Drawing.Color.Black
        lbl_01_T03Entregable.ForeColor = Drawing.Color.Black
        lbl_01_T03Consecutivo.ForeColor = Drawing.Color.Black
        lbl_01_T03Version.ForeColor = Drawing.Color.Black
        lbl_01_T03Horas.ForeColor = Drawing.Color.Black
        lbl_01_T03DescripcionAcguividad.ForeColor = Drawing.Color.Black
        lbl_01_T03TiempoaHoras.ForeColor = Drawing.Color.Black
        lbl_01_T03Aprobado.ForeColor = Drawing.Color.Black
        lbl_01_T03Aprobado_Fecha.ForeColor = Drawing.Color.Black
        lbl_01_T03Aprobado_Hora.ForeColor = Drawing.Color.Black
        lbl_01_T03Aprobado_Observacion.ForeColor = Drawing.Color.Black
        lbl_01_T03Aprobado_Observacion_Rechazo.ForeColor = Drawing.Color.Black
        lbl_01_T03EnviadoAProbacion.ForeColor = Drawing.Color.Black
        btnGuardar.Visible = True

        txtFiltroBusqueda.Text = Nothing
        drop_01_T03Fecha.Text = Nothing
        MostrarMensaje("")
        bHabilitarControles(True, False, False, False, False, False, False, False)
        Mostrar_panel_IdAprobacion(False)
        filtro()
    End Sub

    Private Sub bHabilitarControles(ByVal bODS As Boolean, ByVal bENTEEGABLE As Boolean, ByVal bOBSERVACIONES As Boolean, ByVal bVERSION As Boolean, ByVal bFECHA As Boolean, ByVal bHORAS As Boolean, ByVal bESTADO As Boolean, ByVal bGuardar As Boolean)
        drop_01_T03ODS.Enabled = bODS
        drop_01_T03Entregable.Enabled = bENTEEGABLE
        ImageButton_EntreableEco.Visible = bENTEEGABLE
        ImageButton_EntreableSering.Visible = bENTEEGABLE
        drop_01_T03EntregableSeringrtec.Enabled = bENTEEGABLE
        If bENTEEGABLE = True Then
            If drop_01_T03Entregable.Text = "N/A" And drop_01_T03EntregableSeringrtec.Text = "N/A" Then
                ImageButton_EntreableActividades.Visible = True
                ImageButton_EntreableEco.Visible = False
                ImageButton_EntreableSering.Visible = False

            Else
                ImageButton_EntreableEco.Visible = True
                ImageButton_EntreableSering.Visible = True

                ImageButton_EntreableActividades.Visible = False
            End If
        Else
            ImageButton_EntreableEco.Visible = bENTEEGABLE
            ImageButton_EntreableSering.Visible = bENTEEGABLE
            ImageButton_EntreableActividades.Visible = False
        End If

        txt_01_T03DescripcionAcguividad.Enabled = bOBSERVACIONES
        drop_01_T03Version.Enabled = bOBSERVACIONES
        drop_01_T03Fecha.Enabled = bFECHA
        Drop_01_T03Horas.Enabled = bHORAS
        drop_01_T03Aprobado.Visible = False
        If bGuardar = False Then
            btnItem.ImageUrl = "~/iconos/AddItem2.png"
        Else
            btnItem.ImageUrl = "~/iconos/AddItem.png"
        End If

        btnItem.Enabled = bGuardar

        'btnLimpiar.Visible = False
        btnEliminar.Visible = False
        'btnGuardar.Visible = False
    End Sub

    Private Function bValidarCampos(Optional ByVal bReenvio As Boolean = False) As Boolean
        bValidarCampos = False


        lbl_01_T03Fecha.ForeColor = Drawing.Color.Red
        lbl_01_T03Contrato.ForeColor = Drawing.Color.Red
        lbl_01_T03ODS.ForeColor = Drawing.Color.Red
        lbl_01_T03Entregable.ForeColor = Drawing.Color.Red
        lbl_01_T03Consecutivo.ForeColor = Drawing.Color.Red
        lbl_01_T03Version.ForeColor = Drawing.Color.Red
        lbl_01_T03Horas.ForeColor = Drawing.Color.Red
        'lbl_01_T03DescripcionAcguividad.ForeColor = Drawing.Color.Red
        lbl_01_T03TiempoaHoras.ForeColor = Drawing.Color.Red
        lbl_01_T03Aprobado.ForeColor = Drawing.Color.Red
        lbl_01_T03Aprobado_Fecha.ForeColor = Drawing.Color.Red
        lbl_01_T03Aprobado_Hora.ForeColor = Drawing.Color.Red
        lbl_01_T03Aprobado_Observacion.ForeColor = Drawing.Color.Red
        lbl_01_T03Aprobado_Observacion_Rechazo.ForeColor = Drawing.Color.Red
        lbl_01_T03EnviadoAProbacion.ForeColor = Drawing.Color.Red
        If Trim(lblUsuarioLogueado_Documento.Text) = "" Then

            MostrarMensaje("falta campo: Usuario")
            filtro()
            Exit Function
        Else

        End If
        If Trim(drop_01_T03Fecha.Text) = "" Then
            drop_01_T03Fecha.Focus()
            MostrarMensaje("falta campo: Fecha")
            filtro()
            Exit Function
        Else
            lbl_01_T03Fecha.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_01_T03Contrato.Text) = "" Then
            drop_01_T03Contrato.Focus()
            MostrarMensaje("falta campo: Contrato")
            filtro()
            Exit Function
        Else
            lbl_01_T03Contrato.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_01_T03ODS.Text) = "" Then
            drop_01_T03ODS.Focus()
            MostrarMensaje("falta campo: ODS")
            filtro()
            Exit Function
        Else
            lbl_01_T03ODS.ForeColor = Drawing.Color.Black
        End If

        If Trim(drop_01_T03Actividad.Text) = "" Then
            drop_01_T03Actividad.Focus()
            MostrarMensaje("falta campo: Actividad")
            filtro()
            Exit Function
        Else
            lbl_01_T03Entregable.ForeColor = Drawing.Color.Black
        End If

        If Trim(drop_01_T03Entregable.Text) = "" And Trim(drop_01_T03EntregableSeringrtec.Text) = "" Then
            drop_01_T03Entregable.Focus()
            MostrarMensaje("falta campo: Entregable Cliente o de SEringtec")
            filtro()
            Exit Function
        Else
            lbl_01_T03Entregable.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_01_T03Disciplina.Text) = "" Then
            drop_01_T03Disciplina.Focus()
            MostrarMensaje("falta campo: Disciplina")
            filtro()
            Exit Function
        Else
            lbl_01_T03Disciplina.ForeColor = Drawing.Color.Black
        End If


        'If Trim(txt_01_T03Consecutivo.Text) = "" Then
        '    txt_01_T03Consecutivo.Focus()
        '    MostrarMensaje("falta campo: 03Consecutivo")
        '    Exit Function
        'Else
        '    lbl_01_T03Consecutivo.ForeColor = Drawing.Color.Black
        'End If
        If Trim(drop_01_T03Version.Text) = "" Then
            drop_01_T03Version.Focus()
            MostrarMensaje("falta campo: Version")
            filtro()
            Exit Function
        Else
            lbl_01_T03Version.ForeColor = Drawing.Color.Black
        End If
        If Trim(Drop_01_T03Horas.Text) = "" Then
            Drop_01_T03Horas.Focus()
            MostrarMensaje("falta campo: Horas")
            filtro()
            Exit Function
        Else
            lbl_01_T03Horas.ForeColor = Drawing.Color.Black
        End If


        'If Trim(txt_01_T03DescripcionAcguividad.Text) = "" Then
        '    txt_01_T03DescripcionAcguividad.Focus()
        '    MostrarMensaje("falta campo: 03DescripcionAcguividad")
        '    Exit Function
        'Else
        '    lbl_01_T03DescripcionAcguividad.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_01_T03TiempoaHoras.Text) = "" Then
        '    txt_01_T03TiempoaHoras.Focus()
        '    MostrarMensaje("falta campo: 03Observaciones")
        '    Exit Function
        'Else
        '    lbl_01_T03Observaciones.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_01_T03Aprobado.Text) = "" Then
        '    drop_01_T03Aprobado.Focus()
        '    MostrarMensaje("falta campo: 03Aprobado")
        '    Exit Function
        'Else
        '    lbl_01_T03Aprobado.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_01_T03Aprobado_Fecha.Text) = "" Then
        '    txt_01_T03Aprobado_Fecha.Focus()
        '    MostrarMensaje("falta campo: 03Aprobado_Fecha")
        '    Exit Function
        'Else
        '    lbl_01_T03Aprobado_Fecha.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_01_T03Aprobado_Hora.Text) = "" Then
        '    txt_01_T03Aprobado_Hora.Focus()
        '    MostrarMensaje("falta campo: 03Aprobado_Hora")
        '    Exit Function
        'Else
        '    lbl_01_T03Aprobado_Hora.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_01_T03Aprobado_Observacion.Text) = "" Then
        '    txt_01_T03Aprobado_Observacion.Focus()
        '    MostrarMensaje("falta campo: 03Aprobado_Observacion")
        '    Exit Function
        'Else
        '    lbl_01_T03Aprobado_Observacion.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_01_T03Aprobado_Observacion_Rechazo.Text) = "" Then
        '    txt_01_T03Aprobado_Observacion_Rechazo.Focus()
        '    MostrarMensaje("falta campo: 03Aprobado_Observacion_Rechazo")
        '    Exit Function
        'Else
        '    lbl_01_T03Aprobado_Observacion_Rechazo.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_01_T03EnviadoAProbacion.Text) = "" Then
        '    txt_01_T03EnviadoAProbacion.Focus()
        '    MostrarMensaje("falta campo: 03EnviadoAProbacion")
        '    Exit Function
        'Else
        '    lbl_01_T03EnviadoAProbacion.ForeColor = Drawing.Color.Black
        'End 

        If bReenvio = False Then
            Dim lCantidad_Registrada As Decimal = 0
            Dim HorasTotal As TimeSpan = TimeSpan.Parse("0:0")
            Dim SUMAA As String = Suma_Horas()
            Dim HorasTotalBD As TimeSpan = TimeSpan.Parse(SUMAA)
            Dim Horasitem As TimeSpan = TimeSpan.Parse(Drop_01_T03Horas.Text)
            HorasTotal = HorasTotalBD + Horasitem

            If HorasTotal.TotalHours > 24 Then
                txt_01_T03EnviadoAProbacion.Focus()
                MostrarMensaje("No Podra Registrar mas horas para este Fecha Supera las 24 Horas del dia")
                filtro()
                Exit Function
            Else
                lbl_01_T03EnviadoAProbacion.ForeColor = Drawing.Color.Black
            End If

        Else

        End If


        bValidarCampos = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")

        If clsAdminDb.bVerificarSQL(drop_01_T03Fecha.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03Consecutivo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_01_T03Version.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03Horas.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03DescripcionAcguividad.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03TiempoaHoras.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_01_T03Aprobado.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03Aprobado_Fecha.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03Aprobado_Hora.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03Aprobado_Observacion.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03Aprobado_Observacion_Rechazo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T03EnviadoAProbacion.Text) Then Exit Function

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
        Dim dFecha As Date
        Dim sFecha As String = ""
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
            sFecha = sFecha & " " & Now.ToShortTimeString
        Else
            'drop_01_T03Fecha.Text = Nothing
        End If
        Dim sNombreTabla_TEm = _01_T03REGISTROHORAS.NombreTabla
        Dim sCamposTabla_TEm = _01_T03REGISTROHORAS.CamposTabla
        Dim sCamposINS_TEm = "'" & clsAdminDb.sRemoverHTML(lblUsuarioLogueado_Documento.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Fecha.Text) & "'" & "," & "'" & drop_01_T03Contrato.Text & "'" & "," & "'" & drop_01_T03ODS.Text & "'" & "," & "'" & drop_01_T03Entregable.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Consecutivo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Version.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Horas.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03DescripcionAcguividad.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03TiempoaHoras.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Aprobado.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Hora.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Observacion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Observacion_Rechazo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03EnviadoAProbacion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Disciplina.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03HorasSolo.Text) & "'" & "," & "'" & txt_01_T03MinutoSolo.Text & "'" & "," & "'" & drop_01_T03Cliente.Text & "'"
        Dim sCamposUPD_TEm = _01_T03REGISTROHORAS.Campo_01_T03Estado & "='Enviado', " & _01_T03REGISTROHORAS.Campo_01_T03EnviadoAProbacion & "='" & sFecha & "'"
        Dim sLlaves_tem = _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "=  '" & lblUsuarioLogueado_Documento.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Fecha & "=  '" & drop_01_T03Fecha.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Contrato & "=  '" & drop_01_T03Contrato.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03ODS & "=  '" & drop_01_T03ODS.Text & "' AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Cliente & "=  '" & drop_01_T03Cliente.Text & "' and " & _01_T03REGISTROHORAS.Campo_01_T03Estado & "='Registrado'"
        Dim bSqlInsert As Boolean = clsAdminDb.sql_Update(sNombreTabla_TEm, sCamposUPD_TEm, sLlaves_tem)
        If bSqlInsert = True Then
            If bSqlInsert = True Then
                MostrarMensaje("Se envio correctamente estasa horas para Aprobación", True)
                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If
        End If
        ModalPopupGuardar.Hide()
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
                'txt_01_T03Usuario.Text = coleccionDatos(0)
                Dim bNoExisteDia As Boolean = False
                For Each item As ListItem In drop_01_T03Fecha.Items
                    If coleccionDatos(1) = item.Text Then
                        bNoExisteDia = True
                    End If
                Next
                If bNoExisteDia = False Then
                    drop_01_T03Fecha.Items.Add(New ListItem(coleccionDatos(1), coleccionDatos(1)))
                    drop_01_T03Fecha.Text = coleccionDatos(1)
                Else
                    drop_01_T03Fecha.Text = coleccionDatos(1)
                End If


                drop_01_T03ODS.Text = coleccionDatos(3)
                drop_01_T03ODS_TextChanged(Nothing, Nothing)
                drop_01_T03Contrato.Text = coleccionDatos(2)
                'Cargar_Disciplina()
                'drop_01_T03Disciplina.Text = coleccionDatos(16)
                Cargar_Entregable()
                Try
                    drop_01_T03Entregable.Text = coleccionDatos(4)
                Catch ex As Exception

                End Try

                Cargar_Version()
                txt_01_T03Consecutivo.Text = coleccionDatos(5)
                Try
                    drop_01_T03Version.Text = coleccionDatos(6)
                Catch ex As Exception

                End Try

                txt_01_T03Horas.Text = coleccionDatos(7)
                txt_01_T03DescripcionAcguividad.Text = coleccionDatos(8)
                txt_01_T03TiempoaHoras.Text = coleccionDatos(9)
                Try
                    drop_01_T03Aprobado.Text = coleccionDatos(10)
                Catch ex As Exception

                End Try

                txt_01_T03Aprobado_Fecha.Text = coleccionDatos(11)
                txt_01_T03Aprobado_Hora.Text = coleccionDatos(12)
                txt_01_T03Aprobado_Observacion.Text = coleccionDatos(13)
                txt_01_T03Aprobado_Observacion_Rechazo.Text = coleccionDatos(14)
                txt_01_T03EnviadoAProbacion.Text = coleccionDatos(15)
                Drop_01_T03Horas.Text = coleccionDatos(7)
                txt_01_T03MinutoSolo.Text = coleccionDatos(18)
                Cargar_Actividades()
                Try
                    drop_01_T03Actividad.Text = coleccionDatos(28)
                Catch ex As Exception

                End Try
                Try
                    drop_01_T03EntregableSeringrtec.Text = coleccionDatos(30)
                Catch ex As Exception

                End Try

                'txt_01_T03MinutoSolo.Text = coleccionDatos(8)
                filtro()
                bHabilitarControles(True, True, True, True, True, True, True, True)


            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else
                'MostrarMensaje("No se encontraron datos" 
            End If
        End If
    End Sub

    Private Sub Cargar_Registro_Reenviar()
        'Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object
        Dim sNombreTabla2 = _01_T03REGISTROHORAS.NombreTabla
        Dim sCamposTabla2 = _01_T03REGISTROHORAS.CamposTabla

        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla2, sCamposTabla2, sLlaves)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            Else
                'txt_01_T03Usuario.Text = coleccionDatos(0)
                txtReenviado_Estado.Text = coleccionDatos(10)
                txtReenviado_Fecha.Text = coleccionDatos(1)
                txtReenviado_Horas.Text = coleccionDatos(7)
                txtReenviado_Actividad.Text = coleccionDatos(29)
                txtReenviado_Observacion.Text = coleccionDatos(14)
                txtReenviado_Observacion.Text = txtReenviado_Observacion.Text & vbCrLf & coleccionDatos(27)
                'drop_01_T03Actividad.Text = coleccionDatos(28)
                txtReenviado_Entregable.Text = coleccionDatos(28)
                txtReenviado_Ods.Text = coleccionDatos(3)
                'drop_01_T03ODS_TextChanged(Nothing, Nothing)
                txtReenviado_Contrato.Text = coleccionDatos(2)
                'Cargar_Disciplina()
                'drop_01_T03Disciplina.Text = coleccionDatos(16)
                'Cargar_Entregable()
                'drop_01_T03Entregable.Text = coleccionDatos(4)
                'Cargar_Version()
                txtReenviado_FEchaId.Text = coleccionDatos(1)
                txtReenviado_Consecutivo.Text = coleccionDatos(5)
                txtReenviado_Cliente.Text = coleccionDatos(19)
'                drop_01_T03Version.Text = coleccionDatos(6)
                txt_01_T03Horas.Text = coleccionDatos(7)
                txt_01_T03DescripcionAcguividad.Text = coleccionDatos(8)
                drop_Proyecto_Reenviar.Text = coleccionDatos(3)

                drop_Proyecto_Reenviar_TextChanged(Nothing, Nothing)

                'drop_01_T03Contrato.Text = coleccionDatos(2)
                'Cargar_Disciplina()
                'drop_01_T03Disciplina.Text = coleccionDatos(16)
                'Cargar_Entregable()
                Try
                    drop_Entregable_Reenviar.Text = coleccionDatos(4)
                Catch ex As Exception
                End Try

                Try
                    drop_Entregable_ReenviarSeringtec.Text = coleccionDatos(30)
                Catch ex As Exception
                End Try

                Cargar_Version_Reenviar()
                'txt_01_T03Consecutivo.Text = coleccionDatos(5)
                Try
                    drop_Version_Reenviar.Text = coleccionDatos(6)
                Catch ex As Exception

                End Try

                'txt_01_T03Horas.Text = coleccionDatos(7)
                'txt_01_T03DescripcionAcguividad.Text = coleccionDatos(8)
                'txt_01_T03TiempoaHoras.Text = coleccionDatos(9)
                Try
                    'drop_01_T03Aprobado.Text = coleccionDatos(10)
                Catch ex As Exception

                End Try

                'txt_01_T03Aprobado_Fecha.Text = coleccionDatos(11)
                'txt_01_T03Aprobado_Hora.Text = coleccionDatos(12)
                'txt_01_T03Aprobado_Observacion.Text = coleccionDatos(13)
                'txt_01_T03Aprobado_Observacion_Rechazo.Text = coleccionDatos(14)
                'txt_01_T03EnviadoAProbacion.Text = coleccionDatos(15)
                drop_Tiempo_Reenviar.Text = coleccionDatos(7)

                'txt_01_T03MinutoSolo.Text = coleccionDatos(18)
                Cargar_Actividades_Reenviar()
                Try
                    drop_Actividades_Reenviar.Text = coleccionDatos(28)
                Catch ex As Exception

                End Try

                Try
                    'drop_Entregable_ReenviarSeringtecc.Text = coleccionDatos(30)
                Catch ex As Exception

                End Try

                'txt_01_T03MinutoSolo.Text = coleccionDatos(8)


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


            txt_01_T03Consecutivo.Text = iConsecutivo()
            txt_01_T03Horas.Text = Drop_01_T03Horas.Text
            txt_01_T03HorasSolo.Text = Mid((Drop_01_T03Horas.Text), 1, 2)
            txt_01_T03MinutoSolo.Text = Mid((Drop_01_T03Horas.Text), 4, 2)
            Dim Horas As Integer = txt_01_T03HorasSolo.Text
            Dim Minutos As Integer = txt_01_T03MinutoSolo.Text
            Dim HorasMinutos As Integer
            Dim HorasSegundos As Integer
            If Horas >= 2 Then
                HorasMinutos = Horas * 60
            Else
                HorasMinutos = 60
            End If
            HorasSegundos = (HorasMinutos * 3600)


            Dim MinSegundos As Integer = (Minutos * 60)
            Dim Total_Horas_Segundos As Integer = HorasSegundos
            Dim Total_Horas_Horas = HorasSegundos / 60
            Total_Horas_Horas = Total_Horas_Horas / 60
            Total_Horas_Horas = Total_Horas_Horas / 60

            'Dim MinSegundos As Integer
            MinSegundos = (Minutos * 60)
            Dim Total_Segundos As Integer = MinSegundos
            Dim Total_SegundosOK = Total_Segundos / 60
            Total_SegundosOK = Total_SegundosOK / 60
            'Total_SegundosOK = Total_SegundosOK / 60
            txt_01_T03TiempoaHoras.Text = Total_SegundosOK + Total_Horas_Horas
            txt_01_T03TiempoaHoras.Text = Replace(txt_01_T03TiempoaHoras.Text, ",", ".")

            'Dim HorasTotal As TimeSpan = TimeSpan.Parse("0:0")
            'Dim SUMAA As String = Suma_Horas()
            'Dim HorasTotalBD As TimeSpan = TimeSpan.Parse(SUMAA)
            Dim Horasitem As TimeSpan = TimeSpan.Parse(Drop_01_T03Horas.Text)
            'HorasTotal = HorasTotalBD + Horasitem
            txt_01_T03TiempoaHoras.Text = Horasitem.TotalHours


            txt_01_T03Actividad_Texto.Text = drop_01_T03Actividad.SelectedItem.Text
            Datos_Modulo()
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla, sCamposTabla, sCamposINS)
            If bSqlInsert = True Then
                Guardar_Transaccion(sCod_Aplicacion, sCodModulo, lblUsuarioLogueado_Documento.Text, lblUsuarioLogueado_Nombre.Text, "01", clsAdminDb.Mostrar_Consulta)
                MostrarMensaje("Se inserto nuevo registro correctamente ", True)
                'Registro_Procesos("Guardar", clsAdminDb.Mostrar_Consulta)

                txt_01_T03Consecutivo.Text = ""
                'txt_01_T03DescripcionAcguividad.Text = ""
                'Drop_01_T03Horas.Text = ""
                'txt_01_T03MinutoSolo.Text = ""
                Try
                    'drop_01_T03Entregable.Text = ""
                Catch ex As Exception

                End Try


                'drop_01_T03EntregableSeringrtec.Items.Clear()
                'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))

                'drop_01_T03Actividad.Items.Clear()
                'drop_01_T03Actividad.Items.Add(New ListItem("Seleccionar", ""))
                'txt_01_T03Actividad_Texto.Text = ""
                'drop_01_T03Version.Items.Clear()
                Drop_01_T03Horas.Text = ""
                drop_01_T03Fecha.Text = ""
                Drop_01_T03Horas.Text = ""
                'txt_01_T03DescripcionAcguividad.Text = ""
                'drop_01_T03ODS.Text = ""

                'bHabilitarControles(True, False, False, False, False, False, False, False)
                'txt_Cons_Aprobador.Text = ""

                'CargarDrops_FEchas()
                btnItem.ImageUrl = "~/iconos/AddItem2.png"
                btnItem.Enabled = False
                'btnItem.ImageUrl = "~/iconos/AddItem.png"


                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            txt_01_T03Horas.Text = Drop_01_T03Horas.Text
            txt_01_T03HorasSolo.Text = Mid((Drop_01_T03Horas.Text), 1, 2)
            txt_01_T03MinutoSolo.Text = Mid((Drop_01_T03Horas.Text), 4, 2)
            Dim Horas As Integer = txt_01_T03HorasSolo.Text
            Dim Minutos As Integer = txt_01_T03MinutoSolo.Text
            Dim HorasMinutos As Integer
            Dim HorasSegundos As Integer
            If Horas >= 2 Then
                HorasMinutos = Horas * 60
            Else
                HorasMinutos = 60
            End If
            HorasSegundos = (HorasMinutos * 3600)


            Dim MinSegundos As Integer = (Minutos * 60)
            Dim Total_Horas_Segundos As Integer = HorasSegundos
            Dim Total_Horas_Horas = HorasSegundos / 60
            Total_Horas_Horas = Total_Horas_Horas / 60
            Total_Horas_Horas = Total_Horas_Horas / 60

            'Dim MinSegundos As Integer
            MinSegundos = (Minutos * 60)
            Dim Total_Segundos As Integer = MinSegundos
            Dim Total_SegundosOK = Total_Segundos / 60
            Total_SegundosOK = Total_SegundosOK / 60
            'Total_SegundosOK = Total_SegundosOK / 60
            txt_01_T03TiempoaHoras.Text = Total_SegundosOK + Total_Horas_Horas

            Dim Horasitem As TimeSpan = TimeSpan.Parse(Drop_01_T03Horas.Text)
            'HorasTotal = HorasTotalBD + Horasitem
            txt_01_T03TiempoaHoras.Text = Horasitem.TotalHours


            txt_01_T03Actividad_Texto.Text = drop_01_T03Actividad.SelectedItem.Text
            Datos_Modulo()

            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla, sCamposUPD, sLlaves)
            If bSqlInsert = True Then
                Guardar_Transaccion(sCod_Aplicacion, sCodModulo, lblUsuarioLogueado_Documento.Text, lblUsuarioLogueado_Nombre.Text, "02", clsAdminDb.Mostrar_Consulta)
                MostrarMensaje("Se actualizo correctamente este registro", True)
                txt_01_T03Consecutivo.Text = ""
                'txt_01_T03DescripcionAcguividad.Text = ""
                'Drop_01_T03Horas.Text = ""
                'txt_01_T03MinutoSolo.Text = ""
                'drop_01_T03Entregable.Text = ""
                'drop_01_T03EntregableSeringrtec.Items.Clear()
                'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))


                'drop_01_T03Actividad.Items.Clear()
                'drop_01_T03Actividad.Items.Add(New ListItem("Seleccionar", ""))
                Drop_01_T03Horas.Text = ""
                'drop_01_T03Version.Items.Clear()
                Drop_01_T03Horas.Text = ""
                'txt_01_T03DescripcionAcguividad.Text = ""
                'drop_01_T03ODS.Text = ""
                drop_01_T03Fecha.Text = ""
                'drop_01_T03Actividad.Text = ""
                'txt_01_T03Actividad_Texto.Text = ""
                'bHabilitarControles(True, False, False, False, False, False, False, False)
                'txt_Cons_Aprobador.Text = ""
                'CargarDrops_FEchas()
                btnItem.ImageUrl = "~/iconos/AddItem2.png"
                btnItem.Enabled = False

                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If
        End If
        'Suma_Horas()
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
        Tbody1.Controls.Clear()
        Dim coleccionDatosPlural As New Collection
        Dim sHoras As String = "sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo)))/60/60 as horas"
        Dim sMinutes As String = "(sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo))) - (sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo)))/60/60*60*60))/60 as minutos"
        Dim sSegundos As String = "sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo))) - (sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo)))/60/60*60*60) - ((sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo))) - (sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo)))/60/60*60*60))/60*60) as segundos"
        Dim sTotalSegundos As String = "sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo))) as totalensegundos"

        Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,REPLACE(REPLACE(REPLACE(_01_T03ActividadesCodigo,CHAR(9),''),CHAR(10),''),CHAR(13),''),_01_T03DescripcionActividad,_01_T03Version,_01_T03Fecha,_01_T03tiempo,_01_T03EnviadoAProbacion,_01_T03Estado,_01_T03Consecutivo,_01_T03DocFuncionario,'',_01_T03ActividadesDescripcion,_01_T03TiempoaHoras,_01_T03ActividadesCodigo,_01_T03ActividadesCodigo,_01_T03ActividadesDescripcion,_01_T03EntregableSeringtec,_01_T03Entregable" '
        Dim sLlavesLocal As String = _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "='" & lblUsuarioLogueado_Documento.Text & "'" ' and  _01_T21ODS=_01_T09Codigo and _01_T09Cliente=_01_T21Cliente and _01_T21Contrato=_01_T09ContratoMArco  "
        sLlavesLocal = sLlavesLocal & " and (_01_T03Estado='Registrado' or _01_T03Estado='Rechazado')"
        'sLlavesLocal = sLlavesLocal & " and _01_T03ODS=_01_T09Codigo and _01_T09Cliente=_01_T03Cliente and _01_T03Contrato=_01_T09ContratoMArco "
        'sLlavesLocal = sLlavesLocal & " and _01_T03ODS=_01_T21ODS and _01_T21Cliente=_01_T03Cliente and _01_T03Contrato=_01_T21Contrato "
        'sLlavesLocal = sLlavesLocal & " and " & _01_T03REGISTROHORAS.Campo_01_T03Disciplina & "=" & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Categoria
        'sLlavesLocal = sLlavesLocal & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Entregable & "=" & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol
        If drop_01_T03ODS.Text = "" Then
            If drop_01_T03Fecha.Text <> "" Then

                sLlavesLocal = sLlavesLocal & " and _01_T03Fecha='" & drop_01_T03Fecha.Text & "'"

            Else
                'sLlavesLocal = sLlavesLocal & " and _01_T03Fecha='" & txt_01_T23Fecha.Text & "'"
            End If
            If drop_01_T03Disciplina.Text <> "" Then
                'sLlavesLocal = sLlavesLocal & " and _01_T03Disciplina='" & drop_01_T03Disciplina.Text & "'"
            End If
        Else

            'If drop_01_T03ODS.Text <> "" Then
            '    sLlavesLocal = sLlavesLocal & " and _01_T03ODS='" & drop_01_T03ODS.Text & "'"
            '    'sLlavesLocal = sLlavesLocal & " and _01_T03Contrato='" & drop_01_T03Contrato.Text & "'"
            '    'sLlavesLocal = sLlavesLocal & " and _01_T03Cliente='" & drop_01_T03Cliente.Text & "'"
            'End If
            If drop_01_T03Disciplina.Text <> "" Then
                'sLlavesLocal = sLlavesLocal & " and _01_T03Disciplina='" & drop_01_T03Disciplina.Text & "'"
            End If
            'If drop_01_T03Entregable.Text <> "" Then
            '    sLlavesLocal = sLlavesLocal & " and _01_T03Entregable='" & drop_01_T03Entregable.Text & "'"
            'End If
            If drop_01_T03Fecha.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and _01_T03Fecha='" & drop_01_T03Fecha.Text & "'"
            Else
                'sLlavesLocal = sLlavesLocal & " and _01_T03Fecha='" & txt_01_T23Fecha.Text & "'"

            End If

        End If
        sLlavesLocal = sLlavesLocal & " and _01_T03Fecha=_01_T22Fecha"
        Dim sTablaLocal As String = _01_T03REGISTROHORAS.NombreTabla & "," & _01_T22AGNOS.NombreTabla '& "," & _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim ArregloSingular() As String


        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
        End If
        Dim i As Integer = 1
        Dim lbl_Tem2 As Label
        'Dim dTotaloras As Decimal = 0.0
        Dim iHorasEnviadas As Integer = 0
        Dim iHoraPorEnviar As Integer = 0
        Dim dTotaloras As TimeSpan = TimeSpan.Parse("0:0")
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal, "_01_T22FechaNumero desc")
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow

                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                ''Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                'linkbutonHtml.ID = i & "A-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = ArregloSingular(0)
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                'FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                lbl_Tem2.ID = i & "B-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(1)
                'lbl_Tem2.Font.Bold = True
                lbl_Tem2.ForeColor = Drawing.Color.FromName("#423F3F")
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(lbl_Tem2)
                CeldaHtml.Align = "Center"
                FilaHtml.Cells.Add(CeldaHtml)

                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                ''Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                'linkbutonHtml.ID = i & "C-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = ArregloSingular(12)
                'CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                lbl_Tem2.ID = i & "D-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(15) '& " " & ArregloSingular(13)
                'lbl_Tem2.Font.Bold = True
                lbl_Tem2.ForeColor = Drawing.Color.FromName("#423F3F")
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(lbl_Tem2)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                lbl_Tem2.ID = i & "Ddeff-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(18) '& " " & ArregloSingular(13)
                'lbl_Tem2.Font.Bold = True
                lbl_Tem2.ForeColor = Drawing.Color.FromName("#423F3F")
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(lbl_Tem2)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                lbl_Tem2.ID = i & "Dde-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(17) '& " " & ArregloSingular(13)
                'lbl_Tem2.Font.Bold = True
                lbl_Tem2.ForeColor = Drawing.Color.FromName("#423F3F")
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(lbl_Tem2)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                lbl_Tem2.ID = i & "E-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(4)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(lbl_Tem2)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                lbl_Tem2.ID = i & "F-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(5)
                'lbl_Tem2.Font.Bold = True
                lbl_Tem2.ForeColor = Drawing.Color.FromName("#423F3F")
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(lbl_Tem2)
                CeldaHtml.Align = "Center"
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                lbl_Tem2.ID = i & "G-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(6)
                'lbl_Tem2.Font.Bold = True
                lbl_Tem2.ForeColor = Drawing.Color.FromName("#423F3F")
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(lbl_Tem2)
                CeldaHtml.Align = "Center"
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                lbl_Tem2.ID = i & "H-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(7)
                'lbl_Tem2.Font.Bold = True
                lbl_Tem2.ForeColor = Drawing.Color.FromName("#423F3F")
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(lbl_Tem2)
                CeldaHtml.Align = "Center"
                FilaHtml.Cells.Add(CeldaHtml)

                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                ''Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                'linkbutonHtml.ID = i & "I-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = ArregloSingular(8)
                'CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                lbl_Tem2 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                'linkbutonHtml.ID = i & "J-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                lbl_Tem2.Text = ArregloSingular(9)
                'lbl_Tem2.Font.Bold = True
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                lbl_Tem2.ForeColor = Drawing.Color.FromName("#423F3F")
                CeldaHtml.Controls.Add(lbl_Tem2)
                CeldaHtml.Align = "Center"
                FilaHtml.Cells.Add(CeldaHtml)

                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                ''Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                'linkbutonHtml.ID = i & "K-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = ArregloSingular(10)
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'FilaHtml.Cells.Add(CeldaHtml)

                'CeldaHtml = New HtmlTableCell
                'linkbutonHtml = New LinkButton
                ''Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                'linkbutonHtml.ID = i & "L-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'linkbutonHtml.Text = ArregloSingular(11)
                'CeldaHtml.Controls.Add(linkbutonHtml)
                'FilaHtml.Cells.Add(CeldaHtml)

                If ArregloSingular(9) = "Registrado" Then
                    CeldaHtml = New HtmlTableCell
                    linkbutonHtml = New LinkButton
                    'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                    'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03ActividadesCodigo,_01_T03DescripcionActividad,_01_T03Version,_01_T03Fecha,_01_T03tiempo,_01_T03EnviadoAProbacion,_01_T03Estado,_01_T03Consecutivo,_01_T03DocFuncionario,'',_01_T03ActividadesDescripcion,_01_T03TiempoaHoras,_01_T03ActividadesCodigo,_01_T03ActividadesCodigo,_01_T03ActividadesDescripcion,_01_T03EntregableSeringtec" '
                    linkbutonHtml.ID = i & "Jjggk-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                    AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                    linkbutonHtml.ForeColor = Drawing.Color.Navy
                    linkbutonHtml.Font.Bold = True
                    linkbutonHtml.Font.Size = FontUnit.Point(8)
                    linkbutonHtml.ToolTip = "Click para Editar"

                    linkbutonHtml.Text = "Editar"
                    CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                    CeldaHtml.Align = "Center"

                    linkbutonHtml_Eliminar = New LinkButton
                    linkbutonHtml_Eliminar.ID = i & "kkggm13-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                    'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar)
                    'linkbutonHtml_Eliminar.OnClientClick = "bPreguntar = false;"
                    linkbutonHtml_Eliminar.ForeColor = Drawing.Color.DarkRed
                    linkbutonHtml_Eliminar.Font.Bold = True
                    linkbutonHtml_Eliminar.Font.Size = FontUnit.Point(8)
                    AddHandler linkbutonHtml_Eliminar.Click, AddressOf linkbutonHtml_Eliminar_Click
                    CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                    linkbutonHtml_Eliminar.Text = "Eliminar"
                    linkbutonHtml_Eliminar.ToolTip = "Click para Eliminar"
                    'CeldaHtml.Controls.AddAt(0, linkbutonHtml)
                    Dim lblEm As New Label
                    lblEm.Text = "  /  "
                    CeldaHtml.Controls.AddAt(0, linkbutonHtml)
                    CeldaHtml.Controls.AddAt(1, lblEm)
                    CeldaHtml.Controls.AddAt(2, linkbutonHtml_Eliminar)

                    FilaHtml.Cells.Add(CeldaHtml)

                ElseIf ArregloSingular(9) = "Enviado" Then
                    CeldaHtml = New HtmlTableCell
                    linkbutonHtml_Eliminar = New LinkButton
                    'CeldaHtml.BorderColor = "YELLOW"
                    linkbutonHtml_Eliminar.ID = i & "kk13-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                    'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar)
                    linkbutonHtml_Eliminar.OnClientClick = "bPreguntar = false;"
                    linkbutonHtml_Eliminar.ForeColor = Drawing.Color.DarkGoldenrod
                    linkbutonHtml_Eliminar.Font.Bold = True
                    linkbutonHtml_Eliminar.Font.Size = FontUnit.Point(8)
                    linkbutonHtml_Eliminar.Enabled = False
                    AddHandler linkbutonHtml_Eliminar.Click, AddressOf linkbutonHtml_Eliminar_Click
                    CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                    linkbutonHtml_Eliminar.Text = "Enviado"
                    CeldaHtml.Align = "Center"
                    'linkbutonHtml_Eliminar.Enabled = False
                    CeldaHtml.Controls.Add(linkbutonHtml_Eliminar)
                    FilaHtml.Cells.Add(CeldaHtml)
                ElseIf ArregloSingular(9) = "Aprobado" Then
                    CeldaHtml = New HtmlTableCell
                    linkbutonHtml_Eliminar = New LinkButton
                    'CeldaHtml.BorderColor = "YELLOW"
                    linkbutonHtml_Eliminar.ID = i & "kk13-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                    'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar)
                    linkbutonHtml_Eliminar.OnClientClick = "bPreguntar = false;"
                    linkbutonHtml_Eliminar.ForeColor = Drawing.Color.DarkGreen
                    linkbutonHtml_Eliminar.Font.Bold = True
                    linkbutonHtml_Eliminar.Font.Size = FontUnit.Point(8)
                    linkbutonHtml_Eliminar.Enabled = False
                    AddHandler linkbutonHtml_Eliminar.Click, AddressOf linkbutonHtml_Eliminar_Click
                    CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                    linkbutonHtml_Eliminar.Text = "Aprobado"
                    CeldaHtml.Align = "Center"
                    'linkbutonHtml_Eliminar.Enabled = False
                    CeldaHtml.Controls.Add(linkbutonHtml_Eliminar)
                    FilaHtml.Cells.Add(CeldaHtml)
                ElseIf ArregloSingular(9) = "Confirmado" Then
                    CeldaHtml = New HtmlTableCell
                    linkbutonHtml_Eliminar = New LinkButton
                    'CeldaHtml.BorderColor = "YELLOW"
                    linkbutonHtml_Eliminar.ID = i & "kk13-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                    'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar)
                    linkbutonHtml_Eliminar.OnClientClick = "bPreguntar = false;"
                    linkbutonHtml_Eliminar.ForeColor = Drawing.Color.DarkGreen
                    linkbutonHtml_Eliminar.Font.Bold = True
                    linkbutonHtml_Eliminar.Font.Size = FontUnit.Point(8)
                    linkbutonHtml_Eliminar.Enabled = False
                    AddHandler linkbutonHtml_Eliminar.Click, AddressOf linkbutonHtml_Eliminar_Click
                    CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                    linkbutonHtml_Eliminar.Text = "Confirmado"
                    CeldaHtml.Align = "Center"
                    'linkbutonHtml_Eliminar.Enabled = False
                    CeldaHtml.Controls.Add(linkbutonHtml_Eliminar)
                    FilaHtml.Cells.Add(CeldaHtml)
                ElseIf ArregloSingular(9) = "Rechazado" Then
                    CeldaHtml = New HtmlTableCell
                    linkbutonHtml_Eliminar = New LinkButton
                    'CeldaHtml.BorderColor = "YELLOW"
                    linkbutonHtml_Eliminar.ID = i & "kkmm13-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                    'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar)
                    linkbutonHtml_Eliminar.OnClientClick = "bPreguntar = false;"
                    linkbutonHtml_Eliminar.ForeColor = Drawing.Color.DarkGreen
                    linkbutonHtml_Eliminar.Font.Bold = True
                    linkbutonHtml_Eliminar.Font.Size = FontUnit.Point(8)
                    linkbutonHtml_Eliminar.Enabled = False
                    linkbutonHtml_Eliminar.Text = "Rechazado"
                    AddHandler linkbutonHtml_Eliminar.Click, AddressOf linkbutonHtml_Eliminar_Click

                    linkbutonHtml_Eliminar_Editar = New LinkButton
                    'CeldaHtml.BorderColor = "YELLOW"
                    'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,REPLACE(REPLACE(REPLACE(_01_T03ActividadesCodigo,CHAR(9),''),CHAR(10),''),CHAR(13),''),_01_T03DescripcionActividad,_01_T03Version,_01_T03Fecha,_01_T03tiempo,_01_T03EnviadoAProbacion,_01_T03Estado,_01_T03Consecutivo,_01_T03DocFuncionario,'',_01_T03ActividadesDescripcion,_01_T03TiempoaHoras,_01_T03ActividadesCodigo,_01_T03ActividadesCodigo,_01_T03ActividadesDescripcion,_01_T03EntregableSeringtec,_01_T03Entregable" '
                    linkbutonHtml_Eliminar_Editar.ID = i & "kksmn13-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString & "-sep-" & ArregloSingular(5).ToString & "-sep-" & Replace(ArregloSingular(7).ToString, ":", "-ampm-") & "-sep-" & ArregloSingular(19).ToString
                    'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar_Editar)
                    linkbutonHtml_Eliminar_Editar.OnClientClick = "bPreguntar = false;"
                    linkbutonHtml_Eliminar_Editar.ForeColor = Drawing.Color.DarkBlue
                    linkbutonHtml_Eliminar_Editar.ForeColor = Drawing.Color.DarkBlue
                    linkbutonHtml_Eliminar_Editar.Font.Bold = True
                    linkbutonHtml_Eliminar_Editar.Font.Size = FontUnit.Point(8)
                    'linkbutonHtml_Eliminar_Editar.Enabled = False
                    linkbutonHtml_Eliminar_Editar.Text = "Reenviar"
                    AddHandler linkbutonHtml_Eliminar_Editar.Click, AddressOf linkbutonHtml_Eliminar_Editar_Click


                    CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                    CeldaHtml.Align = "Center"
                    'linkbutonHtml_Eliminar.Enabled = False
                    Dim lblEm As New Label
                    lblEm.Text = "  /  "
                    CeldaHtml.Controls.AddAt(0, linkbutonHtml_Eliminar)
                    CeldaHtml.Controls.AddAt(1, lblEm)
                    CeldaHtml.Controls.AddAt(2, linkbutonHtml_Eliminar_Editar)

                    FilaHtml.Cells.Add(CeldaHtml)


                End If
                If ArregloSingular(9).ToString = "Registrado" Then
                    iHoraPorEnviar = iHoraPorEnviar + 1

                    'btnGuardar.Visible = True
                End If

                If ArregloSingular(9).ToString = "Enviado" Then
                    iHorasEnviadas = iHorasEnviadas + 1


                End If
                'Dim sTiempo As String = ArregloSingular(7).ToString '& ":" & ArregloSingular(1).ToString
                'Dim dTotaloras2 As TimeSpan = TimeSpan.Parse(sTiempo)
                'dTotaloras = dTotaloras + dTotaloras2
                'txt_Cons_Registradas.Text = dTotaloras.ToString
                'If (dTotaloras.TotalMinutes / 60) < 9 Then
                '    txt_Cons_Normales.Text = dTotaloras.ToString
                '    'txt_Cons_Recargadas.Text = "0"
                '    txt_Cons_SobreRecargadas.Text = "0"
                'Else
                '    txt_Cons_Normales.Text = "09:00"
                '    If (dTotaloras.TotalMinutes / 60) > 9 Then
                '        Dim sTiempo3 As String = dTotaloras.ToString  '& ":" & ArregloSingular(1).ToString
                '        Dim dTotaloras3 As TimeSpan = TimeSpan.Parse(sTiempo3)
                '        Dim sTiempo4 As String = "09:00"
                '        Dim dTotaloras4 As TimeSpan = TimeSpan.Parse(sTiempo4)
                '        Dim dTotaloras5 As TimeSpan = dTotaloras3 - dTotaloras4
                '        txt_Cons_SobreRecargadas.Text = dTotaloras5.ToString
                '        txt_Cons_SobreRecargadas.Text = dTotaloras5.ToString

                '        'txt_Cons_Recargadas.Text = dTotaloras - 8
                '        'txt_Cons_SobreRecargadas.Text = "0"
                '    Else
                '        'txt_Cons_Recargadas.Text = dTotaloras - txt_Cons_Normales.Text
                '        txt_Cons_SobreRecargadas.Text = "0"

                '    End If
                '    Try
                '        txt_Cons_Fecha.Text = CDate(drop_01_T03Fecha.Text).ToLongDateString
                '    Catch ex As Exception

                '    End Try


                'End If

                'dTotaloras = dTotaloras + ArregloSingular(14).ToString
                i = i + 1
                Tbody1.Controls.Add(FilaHtml)
            Next
            '    txt_Cons_Registradas.Text = dTotaloras
            '    If dTotaloras <8 Then
            '        txt_Cons_Normales.Text= dTotaloras
            '        'txt_Cons_Recargadas.Text = "0"
            'txt_Cons_SobreRecargadas.Text = "0"
            'Else
            '    txt_Cons_Normales.Text = "9"
            '    If dTotaloras > 9 Then
            '        'txt_Cons_Recargadas.Text = "8" 'dTotaloras - txt_Cons_Normales.Text
            '        txt_Cons_SobreRecargadas.Text = dTotaloras - txt_Cons_Normales.Text

            '        'txt_Cons_Recargadas.Text = dTotaloras - 8
            '        'txt_Cons_SobreRecargadas.Text = "0"
            '    Else
            '        'txt_Cons_Recargadas.Text = dTotaloras - txt_Cons_Normales.Text
            '        txt_Cons_SobreRecargadas.Text = "0"

            '    End If
            '    txt_Cons_Fecha.Text = CDate(drop_01_T03Fecha.Text).ToLongDateString

            'End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
        'Suma_Horas()
    End Sub




    Private Sub linkbutonHtml_Eliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_Eliminar.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T01Descripcion,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim ___01_T03Contrato As String = TestArray(1).ToString ' 
        Dim ___01_T03ODS As String = TestArray(2).ToString ' 
        Dim ___01_T03Disciplina As String = TestArray(3).ToString ' 
        Dim ___01_T03Entregable As String = TestArray(4).ToString ' 
        Dim ___01_T03Fecha As String = TestArray(5).ToString ' 
        Dim ___01_T03Consecutivo As String = TestArray(6).ToString ' 
        Dim ___01_T03Usuario As String = TestArray(7).ToString ' 
        'Dim clsAdminDb As New adminitradorDB
        Dim bSqlDelete As Boolean = False
        Dim sLlaves_Tem As String = "_01_T03Contrato='" & ___01_T03Contrato & "'"
        sLlaves_Tem = sLlaves_Tem & " and _01_T03ODS='" & ___01_T03ODS & "'"
        sLlaves_Tem = sLlaves_Tem & " and _01_T03Disciplina='" & ___01_T03Disciplina & "'"
        sLlaves = sLlaves & " and REPLACE(REPLACE(REPLACE(_01_T03ActividadesCodigo,CHAR(9),''),CHAR(10),''),CHAR(13),'')='" & ___01_T03Entregable & "'"
        sLlaves_Tem = sLlaves_Tem & " and _01_T03Fecha='" & ___01_T03Fecha & "'"
        sLlaves_Tem = sLlaves_Tem & " and _01_T03Consecutivo='" & ___01_T03Consecutivo & "'"
        sLlaves_Tem = sLlaves_Tem & " and _01_T03DocFuncionario='" & ___01_T03Usuario & "'"
        sLlaves_Tem = sLlaves_Tem & " and _01_T03Estado='Registrado'"
        If ___01_T03Contrato = "" Or ___01_T03ODS = "" Or ___01_T03Disciplina = "" Or ___01_T03Fecha = "" Or ___01_T03Consecutivo = "" Or ___01_T03Usuario = "" Then
            Exit Sub
        End If


        bSqlDelete = clsAdminDb.sql_Delete(sNombreTabla, sLlaves_Tem)
        If bSqlDelete = True Then
            MostrarMensaje("se elimino correctamente este registro")
            'Registro_Procesos("Eliminar", clsAdminDb.Mostrar_Consulta)
            Guardar_Transaccion(sCod_Aplicacion, sCodModulo, lblUsuarioLogueado_Documento.Text, lblUsuarioLogueado_Nombre.Text, "03", clsAdminDb.Mostrar_Consulta)
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
        'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T01Descripcion,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '

        Dim ___01_T03Contrato As String = TestArray(1).ToString ' 
        Dim ___01_T03ODS As String = TestArray(2).ToString
        Dim ___01_T03Disciplina As String = TestArray(3).ToString ' 
        Dim ___01_T03Entregable As String = TestArray(4).ToString ' 
        Dim ___01_T03Fecha As String = TestArray(5).ToString
        Dim ___01_T03Consecutivo As String = TestArray(6).ToString ' 
        Dim ___01_T03Usuario As String = TestArray(7).ToString ' 
        sLlaves = "_01_T03Contrato='" & ___01_T03Contrato & "'"
        sLlaves = sLlaves & " and _01_T03ODS='" & ___01_T03ODS & "'"
        sLlaves = sLlaves & " and _01_T03Disciplina='" & ___01_T03Disciplina & "'"
        sLlaves = sLlaves & " and REPLACE(REPLACE(REPLACE(_01_T03ActividadesCodigo,CHAR(9),''),CHAR(10),''),CHAR(13),'')='" & ___01_T03Entregable & "'"
        sLlaves = sLlaves & " and _01_T03Fecha='" & ___01_T03Fecha & "'"
        sLlaves = sLlaves & " and _01_T03Consecutivo='" & ___01_T03Consecutivo & "'"
        sLlaves = sLlaves & " and _01_T03DocFuncionario='" & ___01_T03Usuario & "'"

        Cargar_Registro()
        cargar_Tabla()
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
                    sllaveTem = sllaveTem & _80_T02CONEXIONES_BD.CampoLlave_80_T02Codigo & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.Campo_80_T02IP & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.Campo_80_T02NombreServidor & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.Campo_80_T02SiteDataBaseServer & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.CampoLlave_80_T02SiteDataBaseNombre & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.Campo_80_T02Proyecto & " like '" & TestArray(i).ToString & "%'"
                Else
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.CampoLlave_80_T02Codigo & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.Campo_80_T02IP & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.Campo_80_T02NombreServidor & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.Campo_80_T02SiteDataBaseServer & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.CampoLlave_80_T02SiteDataBaseNombre & " like '" & TestArray(i).ToString & "%'"
                    sllaveTem = sllaveTem & " Or " & _80_T02CONEXIONES_BD.Campo_80_T02Proyecto & " like '" & TestArray(i).ToString & "%'"
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

    End Sub

    '*********** Termina logueo **********************************************************
    '***************************************************************************************

    Private Sub btnVerificarCancelarPopu_Click(sender As Object, e As EventArgs) Handles btnVerificarCancelarPopu.Click
        clsAdminDb = Nothing
        Response.Redirect("login.aspx")
    End Sub



    Private Sub drop_01_T03Contrato_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T03Contrato.TextChanged
        Cargar_ODS()
    End Sub
    Private Sub Cargar_ODS()
        drop_01_T03Actividad.Items.Clear()
        drop_01_T03Entregable.Items.Clear()
        drop_01_T03EntregableSeringrtec.Items.Clear()
        drop_01_T03ODS.Items.Clear()
        drop_01_T03Version.Items.Clear()
        drop_01_T03Version.Items.Add(New ListItem("", ""))
        drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03Actividad.Items.Add(New ListItem("Seleccionar", ""))

        'drop_01_T03Disciplina.Text = lblUsuarioLogueado_Disciplina.Text

        drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03ODS.Items.Add(New ListItem("Seleccionar", ""))
        With clsAdminDb
            Dim iFechaActual As Integer = Fix(Now.ToOADate)
            Dim sTables_tem As String = _01_T09ODS.NombreTabla & "," & _01_T10RECURSOSxODS.NombreTabla & "," & _01_T22AGNOS.NombreTabla
            Dim sCampos_tem As String = _01_T09ODS.Campo_01_T09Codigo & "," & _01_T09ODS.Campo_01_T09Codigo
            Dim sLlaves_tem As String = _01_T09ODS.CampoLlave_01_T09Cliente & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Cliente & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Contrato
            sLlaves_tem = sLlaves_tem & " and _01_T10ODS=_01_T09Codigo"
            sLlaves_tem = sLlaves_tem & " and _01_T22Fecha=_01_T10FechaFinal "
            'sLlaves_tem = sLlaves_tem & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "='" & drop_01_T03Contrato.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T09ODS.Campo_01_T09Activo & "='SI'"
            .drop_CargarCombox(drop_Proyecto_Reenviar, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
            'sLlaves_tem = sLlaves_tem & "  and _01_T22FechaNumero>=" & iFechaActual
            .drop_CargarCombox(drop_01_T03ODS, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
            .drop_CargarCombox(drop_Proyecto_Reenviar, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
            DropODS_Ver.Items.Clear()
            DropODS_Ver.Items.Add(New ListItem("Filtro Por ODS", ""))
            .drop_CargarCombox(DropODS_Ver, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)


            sTables_tem = _01_T09ODS.NombreTabla '& "," & _01_T10RECURSOSxODS.NombreTabla
            sCampos_tem = _01_T09ODS.Campo_01_T09Codigo & "," & _01_T09ODS.Campo_01_T09ObjetoODS
            'sLlaves_tem = _01_T09ODS.CampoLlave_01_T09Cliente & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Cliente & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Contrato
            sLlaves_tem = " _01_T09Codigo ='ADMINISTRATIVO'"
            'sLlaves_tem = sLlaves_tem & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "='" & drop_01_T03Contrato.Text & "'"
            'sLlaves_tem = sLlaves_tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"

            .drop_CargarCombox(drop_01_T03ODS, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
            .drop_CargarCombox(drop_Proyecto_Reenviar, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
            .drop_CargarCombox(DropODS_Ver, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)


            Dim coleccionDatosPlural As New Collection
            Dim sCamposTablaLocal As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18Perfil   '& "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & "," & _01_T38ACTIVIDADES.Campo_01_T38Descripcion & "," & _01_T38ACTIVIDADES.Campo_01_T38Activo & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada
            Dim sLlavesLocal As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.CampoLlave_01_T18NumDocFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"
            Dim sTablaLocal As String = _01_T18PERFILESxAPLICACIONxFUNCIONARIO.NombreTabla
            Dim ArregloSingular() As String

            coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
            If (Not coleccionDatosPlural Is Nothing) Then
                For Each ColeccionSingular In coleccionDatosPlural
                    ArregloSingular = ColeccionSingular
                    If ArregloSingular(0) <> "" Then
                        Select Case ArregloSingular(0)

                            Case "02" 'lider

                                sTables_tem = _01_T09ODS.NombreTabla
                                sCampos_tem = _01_T09ODS.Campo_01_T09Codigo & "," & _01_T09ODS.Campo_01_T09Codigo
                                sLlaves_tem = _01_T09ODS.Campo_01_T09Activo & "='SI'" '& _01_T10RECURSOSxODS.CampoLlave_01_T10Cliente & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Contrato
                                'sLlaves_tem = sLlaves_tem & " and _01_T10ODS=_01_T09Codigo"
                                'sLlaves_tem = sLlaves_tem & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "='" & drop_01_T03Contrato.Text & "'"
                                'sLlaves_tem = sLlaves_tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"
                                drop_01_T03ODS.Items.Clear()
                                drop_01_T03ODS.Items.Add(New ListItem("Seleccionar", ""))
                                drop_Proyecto_Reenviar.Items.Clear()
                                drop_Proyecto_Reenviar.Items.Add(New ListItem("Seleccionar", ""))
                                DropODS_Ver.Items.Clear()
                                DropODS_Ver.Items.Add(New ListItem("Seleccionar", ""))

                                Dim coleccionDatosPlural_ODS As New Collection
                                Dim ArregloSingular_ODS() As String
                                coleccionDatosPlural_ODS = clsAdminDb.sql_Coleccion(sTables_tem, sCampos_tem, sLlaves_tem)
                                If (Not coleccionDatosPlural_ODS Is Nothing) Then
                                    For Each ColeccionSingular_ODS In coleccionDatosPlural_ODS
                                        ArregloSingular_ODS = ColeccionSingular_ODS
                                        drop_01_T03ODS.Items.Add(New ListItem(ArregloSingular_ODS(0), ArregloSingular_ODS(0)))
                                        drop_Proyecto_Reenviar.Items.Add(New ListItem(ArregloSingular_ODS(0), ArregloSingular_ODS(0)))
                                        DropODS_Ver.Items.Add(New ListItem(ArregloSingular_ODS(0), ArregloSingular_ODS(0)))

                                    Next
                                    Exit Sub 
                                End If



                            Case "03" 'Coordinador

                                sTables_tem = _01_T09ODS.NombreTabla
                                sCampos_tem = _01_T09ODS.Campo_01_T09Codigo & "," & _01_T09ODS.Campo_01_T09Codigo
                                sLlaves_tem = _01_T09ODS.Campo_01_T09Activo & "='SI'" '& _01_T10RECURSOSxODS.CampoLlave_01_T10Cliente & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Contrato
                                sLlaves_tem = _01_T09ODS.Campo_01_T09DocumentoCoordinador & "='" & lblUsuarioLogueado_Documento.Text & "'"
                                'sLlaves_tem = sLlaves_tem & " and _01_T10ODS=_01_T09Codigo"
                                'sLlaves_tem = sLlaves_tem & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "='" & drop_01_T03Contrato.Text & "'"
                                'sLlaves_tem = sLlaves_tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"
                                'drop_01_T03ODS.Items.Clear()
                                'drop_01_T03ODS.Items.Add(New ListItem("Seleccionar", ""))
                                'drop_Proyecto_Reenviar.Items.Clear()
                                'drop_Proyecto_Reenviar.Items.Add(New ListItem("Seleccionar", ""))
                                'DropODS_Ver.Items.Clear()
                                'DropODS_Ver.Items.Add(New ListItem("Seleccionar", ""))

                                Dim coleccionDatosPlural_ODS As New Collection
                                Dim ArregloSingular_ODS() As String
                                Dim droODS_TEm As New DropDownList
                                Dim droODS_TEm_2 As New DropDownList
                                For Each item As ListItem In drop_01_T03ODS.Items
                                    droODS_TEm.Items.Add(New ListItem(item.Text, item.Value))
                                Next
                                coleccionDatosPlural_ODS = clsAdminDb.sql_Coleccion(sTables_tem, sCampos_tem, sLlaves_tem)
                                If (Not coleccionDatosPlural_ODS Is Nothing) Then
                                    Try
                                        For Each ColeccionSingular_ODS In coleccionDatosPlural_ODS
                                            ArregloSingular_ODS = ColeccionSingular_ODS
                                            Dim bNoExiste As Boolean = False
                                            For Each item As ListItem In droODS_TEm.Items
                                                If item.Value = ArregloSingular_ODS(0).ToString Then
                                                    bNoExiste = True
                                                    Exit For
                                                Else
                                                End If

                                            Next

                                            If bNoExiste = True Then
                                            Else
                                                drop_01_T03ODS.Items.Add(New ListItem(ArregloSingular_ODS(0), ArregloSingular_ODS(0)))
                                                drop_Proyecto_Reenviar.Items.Add(New ListItem(ArregloSingular_ODS(0), ArregloSingular_ODS(0)))
                                                DropODS_Ver.Items.Add(New ListItem(ArregloSingular_ODS(0), ArregloSingular_ODS(0)))

                                            End If
                                        Next

                                    Catch ex As Exception

                                    End Try
                                End If
                                'sTables_tem = _01_T09ODS.NombreTabla '& "," & _01_T10RECURSOSxODS.NombreTabla
                                'sCampos_tem = _01_T09ODS.Campo_01_T09Codigo & "," & _01_T09ODS.Campo_01_T09ObjetoODS
                                ''sLlaves_tem = _01_T09ODS.CampoLlave_01_T09Cliente & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Cliente & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Contrato
                                'sLlaves_tem = " _01_T09Codigo ='ADMINISTRATIVO'"
                                ''sLlaves_tem = sLlaves_tem & " and " & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "='" & drop_01_T03Contrato.Text & "'"
                                ''sLlaves_tem = sLlaves_tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"

                                '.drop_CargarCombox(drop_01_T03ODS, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
                                '.drop_CargarCombox(drop_Proyecto_Reenviar, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
                                '.drop_CargarCombox(DropODS_Ver, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)



                            Case "06" 'Aprobador


                        End Select
                    End If
                Next

            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else

                End If
            End If


        End With
    End Sub

    Private Sub Cargar_Entregable()
        drop_01_T03Entregable.Items.Clear()
        drop_01_T03EntregableSeringrtec.Items.Clear()
        'drop_01_T03Version.Items.Clear()
        'drop_01_T03Version.Items.Add(New ListItem("Seleccionar", ""))

        drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03Entregable.Items.Add(New ListItem("N/A", "N/A"))

        drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("N/A", "N/A"))

        Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21Nombre & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec
        Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
        'sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Categoria & "='" & drop_01_T03Disciplina.Text & "'"
        'drop_CargarCombox(drop_01_T03Entregable, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)



        Dim coleccionDatosPlural As New Collection


        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTables_tem, sCampos_tem, sLlaves_tem,,)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                Dim sCodTEm As String = ""
                If ColeccionSingular(0).ToString.Length > 11 Then
                    sCodTEm = ColeccionSingular(0) ' Mid(ColeccionSingular(0), 1, ColeccionSingular(0).ToString.Length - 11)
                    'sCodTEm = sCodTEm & " " & Mid(ColeccionSingular(0), ColeccionSingular(0).ToString.Length - 9, 13)
                    'sCodTEm = sCodTEm & " " & Replace(Mid(ColeccionSingular(0), ColeccionSingular(0).ToString.Length - 9, 13), "-", " ")
                End If

                drop_01_T03Entregable.Items.Add(New ListItem(sCodTEm, ColeccionSingular(0)))
                drop_01_T03EntregableSeringrtec.Items.Add(New ListItem(ColeccionSingular(2), ColeccionSingular(2)))
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
    End Sub

    Private Sub Cargar_Entregable_TEm()
        'drop_01_T03Entregable.Items.Clear()
        drop_01_T03EntregableSeringrtec.Items.Clear()
        'drop_01_T03Version.Items.Clear()
        'drop_01_T03Version.Items.Add(New ListItem("Seleccionar", ""))

        'drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))
        'drop_01_T03Entregable.Items.Add(New ListItem("N/A", "N/A"))

        drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("N/A", "N/A"))

        Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21Nombre & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec
        Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
        'sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Categoria & "='" & drop_01_T03Disciplina.Text & "'"
        'drop_CargarCombox(drop_01_T03Entregable, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)



        Dim coleccionDatosPlural As New Collection


        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTables_tem, sCampos_tem, sLlaves_tem,,)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                Dim sCodTEm As String = ""
                If ColeccionSingular(0).ToString.Length > 11 Then
                    sCodTEm = ColeccionSingular(0) ' Mid(ColeccionSingular(0), 1, ColeccionSingular(0).ToString.Length - 11)
                    'sCodTEm = sCodTEm & " " & Mid(ColeccionSingular(0), ColeccionSingular(0).ToString.Length - 9, 13)
                    'sCodTEm = sCodTEm & " " & Replace(Mid(ColeccionSingular(0), ColeccionSingular(0).ToString.Length - 9, 13), "-", " ")
                End If

                'drop_01_T03Entregable.Items.Add(New ListItem(sCodTEm, ColeccionSingular(0)))
                drop_01_T03EntregableSeringrtec.Items.Add(New ListItem(ColeccionSingular(2), ColeccionSingular(2)))
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
    End Sub

    Private Sub Cargar_Entregable_Reenviar()
        drop_Entregable_Reenviar.Items.Clear()
        'drop_01_T03Version.Items.Clear()
        'drop_01_T03Version.Items.Add(New ListItem("Seleccionar", ""))

        drop_Entregable_Reenviar.Items.Add(New ListItem("Seleccionar", ""))
        drop_Entregable_Reenviar.Items.Add(New ListItem("N/A", "N/A"))

        Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21Nombre & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec
        Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_Cliente_Reenviar.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_Contrato_Reenviar.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_Proyecto_Reenviar.Text & "'"

        Dim coleccionDatosPlural As New Collection


        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTables_tem, sCampos_tem, sLlaves_tem,,)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                Dim sCodTEm As String = ""
                If ColeccionSingular(0).ToString.Length > 11 Then
                    sCodTEm = Mid(ColeccionSingular(0), 1, ColeccionSingular(0).ToString.Length - 11)
                    sCodTEm = sCodTEm & " " & Mid(ColeccionSingular(0), ColeccionSingular(0).ToString.Length - 9, 13)
                    'sCodTEm = sCodTEm & " " & Replace(Mid(ColeccionSingular(0), ColeccionSingular(0).ToString.Length - 9, 13), "-", " ")
                End If

                drop_Entregable_Reenviar.Items.Add(New ListItem(sCodTEm, ColeccionSingular(0)))

            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
    End Sub

    Private Sub Cargar_Entregable_Reenviar_Seringtec()
        drop_Entregable_ReenviarSeringtec.Items.Clear()
        drop_Entregable_ReenviarSeringtec.Items.Clear()
        'drop_01_T03Version.Items.Clear()
        'drop_01_T03Version.Items.Add(New ListItem("Seleccionar", ""))

        drop_Entregable_ReenviarSeringtec.Items.Add(New ListItem("Seleccionar", ""))
        drop_Entregable_ReenviarSeringtec.Items.Add(New ListItem("N/A", "N/A"))

        Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21Nombre & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec
        Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_Cliente_Reenviar.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_Contrato_Reenviar.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_Proyecto_Reenviar.Text & "'"

        Dim coleccionDatosPlural As New Collection


        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTables_tem, sCampos_tem, sLlaves_tem,,)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                Dim sCodTEm As String = ""
                If ColeccionSingular(0).ToString.Length > 11 Then
                    sCodTEm = Mid(ColeccionSingular(0), 1, ColeccionSingular(0).ToString.Length - 11)
                    sCodTEm = sCodTEm & " " & Mid(ColeccionSingular(0), ColeccionSingular(0).ToString.Length - 9, 13)
                    'sCodTEm = sCodTEm & " " & Replace(Mid(ColeccionSingular(0), ColeccionSingular(0).ToString.Length - 9, 13), "-", " ")
                End If

                drop_Entregable_ReenviarSeringtec.Items.Add(New ListItem(sCodTEm, ColeccionSingular(0)))

            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
    End Sub


    Private Sub Cargar_Actividades()
        drop_01_T03Actividad.Items.Clear()



        'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("N/A", "N/A"))




        Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21Nombre
        Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "='" & drop_01_T03Entregable.Text & "'"

        If (drop_01_T03Entregable.Text = "N/A" Or drop_01_T03EntregableSeringrtec.Text = "N/A") And drop_01_T03Cliente.Text = "900335275" And drop_01_T03ODS.Text <> "" Then
            'Cargar_Entregable_TEm()
            sTables_tem = _01_T38ACTIVIDADES.NombreTabla
            sCampos_tem = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & " + ' - ' + " & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
            sLlaves_tem = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & " in ('01','03')"
            clsAdminDb.drop_CargarCombox(drop_01_T03Actividad, sTables_tem, sCampos_tem, sLlaves_tem, True)
            drop_01_T03EntregableSeringrtec.Items.Clear()
            drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("N/A", "N/A"))
            'drop_01_T03EntregableSeringrtec.Text = "N/A"

        ElseIf (drop_01_T03Entregable.Text = "N/A" Or drop_01_T03EntregableSeringrtec.Text = "N/A") And drop_01_T03ODS.Text = "ADMINISTRATIVO" And drop_01_T03ODS.Text <> "" Then
            sTables_tem = _01_T38ACTIVIDADES.NombreTabla
            sCampos_tem = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & " + ' - ' + " & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
            sLlaves_tem = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & "='01'"
            clsAdminDb.drop_CargarCombox(drop_01_T03Actividad, sTables_tem, sCampos_tem, sLlaves_tem, True)
            drop_01_T03Entregable.Text = "N/A"
            Try
                drop_01_T03EntregableSeringrtec.Text = "N/A"
            Catch ex As Exception

            End Try

        ElseIf drop_01_T03Entregable.Text = "N/A" And drop_01_T03ODS.Text <> "ADMINISTRATIVO" And drop_01_T03ODS.Text <> "" Then
            'Cargar_Entregable_TEm()
            sTables_tem = _01_T38ACTIVIDADES.NombreTabla
            sCampos_tem = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & " + ' - ' + " & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
            sLlaves_tem = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & " in ('02','03')"
            clsAdminDb.drop_CargarCombox(drop_01_T03Actividad, sTables_tem, sCampos_tem, sLlaves_tem, True)
            drop_01_T03EntregableSeringrtec.Items.Clear()
            drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("N/A", "N/A"))
            'drop_01_T03EntregableSeringrtec.Text = "N/A"
        ElseIf drop_01_T03Entregable.Text <> "N/A" And drop_01_T03ODS.Text <> "ADMINISTRATIVO" And drop_01_T03ODS.Text <> "" Then
            clsAdminDb.drop_CargarCombox(drop_01_T03Actividad, sTables_tem, sCampos_tem, sLlaves_tem, False)

            sTables_tem = _01_T21LISTADODOCENTREGABLES.NombreTabla
            sCampos_tem = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec
            sLlaves_tem = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "='" & drop_01_T03Entregable.Text & "'"
            drop_01_T03EntregableSeringrtec.Items.Clear()
            clsAdminDb.drop_CargarCombox(drop_01_T03EntregableSeringrtec, sTables_tem, sCampos_tem, sLlaves_tem, False)
        End If
    End Sub

    Private Sub Cargar_Actividades_TEm()
        drop_01_T03Actividad.Items.Clear()



        'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("N/A", "N/A"))




        Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21Nombre
        Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec & "='" & drop_01_T03EntregableSeringrtec.Text & "'"

        If (drop_01_T03Entregable.Text = "N/A" Or drop_01_T03EntregableSeringrtec.Text = "N/A") And drop_01_T03ODS.Text = "ADMINISTRATIVO" And drop_01_T03ODS.Text <> "" Then
            sTables_tem = _01_T38ACTIVIDADES.NombreTabla
            sCampos_tem = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & " + ' - ' + " & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
            sLlaves_tem = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & "='01'"
            clsAdminDb.drop_CargarCombox(drop_01_T03Actividad, sTables_tem, sCampos_tem, sLlaves_tem, True)
            drop_01_T03Entregable.Text = "N/A"
            Try
                drop_01_T03EntregableSeringrtec.Text = "N/A"
            Catch ex As Exception

            End Try

        ElseIf drop_01_T03EntregableSeringrtec.Text = "N/A" And drop_01_T03ODS.Text <> "ADMINISTRATIVO" And drop_01_T03ODS.Text <> "" Then
            Cargar_Entregable_TEm()
            sTables_tem = _01_T38ACTIVIDADES.NombreTabla
            sCampos_tem = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & " + ' - ' + " & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
            sLlaves_tem = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & " in ('02','03')"
            clsAdminDb.drop_CargarCombox(drop_01_T03Actividad, sTables_tem, sCampos_tem, sLlaves_tem, True)
            Try
                drop_01_T03Entregable.Text = "N/A"
            Catch ex As Exception
                drop_01_T03Entregable.Items.Clear()
                drop_01_T03Entregable.Items.Add(New ListItem("N/A", "N/A"))
            End Try

            Try
                drop_01_T03EntregableSeringrtec.Text = "N/A"
            Catch ex As Exception

            End Try

        ElseIf drop_01_T03EntregableSeringrtec.Text <> "N/A" And drop_01_T03ODS.Text <> "ADMINISTRATIVO" And drop_01_T03ODS.Text <> "" Then
            clsAdminDb.drop_CargarCombox(drop_01_T03Actividad, sTables_tem, sCampos_tem, sLlaves_tem, False)

            sTables_tem = _01_T21LISTADODOCENTREGABLES.NombreTabla
            sCampos_tem = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol
            sLlaves_tem = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec & "='" & drop_01_T03EntregableSeringrtec.Text & "'"
            drop_01_T03Entregable.Items.Clear()

            Dim coleccionDatosPlural_ODS As New Collection
            Dim ArregloSingular_ODS() As String
            Dim BVAcio As Boolean = False
            coleccionDatosPlural_ODS = clsAdminDb.sql_Coleccion(sTables_tem, sCampos_tem, sLlaves_tem)
            If (Not coleccionDatosPlural_ODS Is Nothing) Then
                For Each ColeccionSingular_ODS In coleccionDatosPlural_ODS
                    ArregloSingular_ODS = ColeccionSingular_ODS

                    If ArregloSingular_ODS(0) = "" Then
                        BVAcio = True
                    Else
                        'drop_01_T03Entregable.Text = ArregloSingular_ODS(0)
                        Try
                            drop_01_T03Entregable.Items.Add(New ListItem(ArregloSingular_ODS(0), ArregloSingular_ODS(0)))
                        Catch ex As Exception

                        End Try

                    End If
                Next
            End If

            'sclsAdminDb.drop_CargarCombox(drop_01_T03Entregable, sTables_tem, sCampos_tem, sLlaves_tem, False)
            If BVAcio = True Then
                drop_01_T03Entregable.Items.Add(New ListItem("No Creado", "N/C"))
                Try
                    drop_01_T03Entregable.Text = "N/C"

                Catch ex As Exception

                End Try
            End If
            If drop_01_T03EntregableSeringrtec.Text = "" Then
                drop_01_T03Actividad.Items.Clear()
                'drop_01_T03EntregableSeringrtec.Items.Clear()
                drop_01_T03Actividad.Items.Add(New ListItem("Seleccionar", ""))
                sTables_tem = _01_T21LISTADODOCENTREGABLES.NombreTabla
                sCampos_tem = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol
                sLlaves_tem = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
                sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
                sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
                'sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "='" & drop_01_T03Entregable.Text & "'"
                drop_01_T03Entregable.Items.Clear()
                drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))
                drop_01_T03Entregable.Items.Add(New ListItem("N/A", "N/A"))
                clsAdminDb.drop_CargarCombox(drop_01_T03Entregable, sTables_tem, sCampos_tem, sLlaves_tem, False)


            End If

        End If

    End Sub


    Private Sub Cargar_Actividades_Reenviar()

        drop_Actividades_Reenviar.Items.Clear()



        'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("N/A", "N/A"))




        Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21Nombre
        Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_Cliente_Reenviar.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_Contrato_Reenviar.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_Proyecto_Reenviar.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "='" & drop_Entregable_Reenviar.Text & "'"

        If (drop_Entregable_Reenviar.Text = "N/A" Or drop_Entregable_ReenviarSeringtec.Text = "N/A") And drop_Proyecto_Reenviar.Text = "ADMINISTRATIVO" And drop_Proyecto_Reenviar.Text <> "" Then
            sTables_tem = _01_T38ACTIVIDADES.NombreTabla
            sCampos_tem = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & " + ' - ' + " & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
            sLlaves_tem = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & "='01'"
            clsAdminDb.drop_CargarCombox(drop_Actividades_Reenviar, sTables_tem, sCampos_tem, sLlaves_tem, True)
            drop_Entregable_Reenviar.Text = "N/A"
            drop_Entregable_ReenviarSeringtec.Text = "N/A"
        ElseIf drop_Entregable_Reenviar.Text = "N/A" And drop_Proyecto_Reenviar.Text <> "ADMINISTRATIVO" And drop_Proyecto_Reenviar.Text <> "" Then
            'Cargar_Entregable_TEm()
            sTables_tem = _01_T38ACTIVIDADES.NombreTabla
            sCampos_tem = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & " + ' - ' + " & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
            sLlaves_tem = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & " in ('02','03')"
            clsAdminDb.drop_CargarCombox(drop_Actividades_Reenviar, sTables_tem, sCampos_tem, sLlaves_tem, True)
            drop_Entregable_ReenviarSeringtec.Items.Clear()
            drop_Entregable_ReenviarSeringtec.Items.Add(New ListItem("N/A", "N/A"))
            'drop_01_T03EntregableSeringrtec.Text = "N/A"
        ElseIf drop_Entregable_Reenviar.Text <> "N/A" And drop_Proyecto_Reenviar.Text <> "ADMINISTRATIVO" And drop_Proyecto_Reenviar.Text <> "" Then
            clsAdminDb.drop_CargarCombox(drop_Actividades_Reenviar, sTables_tem, sCampos_tem, sLlaves_tem, False)

            sTables_tem = _01_T21LISTADODOCENTREGABLES.NombreTabla
            sCampos_tem = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec
            sLlaves_tem = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_Cliente_Reenviar.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_Contrato_Reenviar.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_Proyecto_Reenviar.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "='" & drop_Entregable_Reenviar.Text & "'"
            drop_Entregable_ReenviarSeringtec.Items.Clear()
            clsAdminDb.drop_CargarCombox(drop_Entregable_ReenviarSeringtec, sTables_tem, sCampos_tem, sLlaves_tem, False)
        End If
    End Sub



    Private Sub Cargar_Version()
        drop_01_T03Version.Items.Clear()
        drop_01_T03Version.Items.Add(New ListItem("", ""))
        'drop_01_T03Version.Items.Add(New ListItem("", ""))
        'drop_01_T03Version.Items.Add(New ListItem("SIN", "SIN"))
        'drop_01_T03Version.Items.Add(New ListItem("P", "P"))
        'drop_01_T03Version.Items.Add(New ListItem("A", "A"))
        'drop_01_T03Version.Items.Add(New ListItem("B", "B"))
        'drop_01_T03Version.Items.Add(New ListItem("0", "0"))
        'drop_01_T03Version.Items.Add(New ListItem("1", "1"))


        'Exit Sub

        With clsAdminDb
            Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
            Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21VersionActual & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21VersionActual
            Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
            'sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Categoria & "='" & drop_01_T03Disciplina.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "='" & drop_01_T03Entregable.Text & "'"

            'Dim clsAdminDb As New adminitradorDB
            Dim coleccionDatos As Object

            coleccionDatos = clsAdminDb.sql_Select(sTables_tem, sCampos_tem, sLlaves_tem)
            If Not coleccionDatos Is Nothing Then
                If coleccionDatos.Length = 0 Then
                    drop_01_T03Version.Items.Clear()
                    drop_01_T03Version.Items.Add(New ListItem("", ""))
                    drop_01_T03Version.Items.Add(New ListItem("N/A", "N/A"))
                    drop_01_T03Version_TextChanged(Nothing, Nothing)
                Else
                    If drop_01_T03Entregable.Text = "N/A" Or drop_01_T03EntregableSeringrtec.Text = "N/A" Then
                        drop_01_T03Version.Items.Clear()
                        drop_01_T03Version.Items.Add(New ListItem("", ""))
                        drop_01_T03Version.Items.Add(New ListItem("N/A", "N/A"))
                    Else
                        If coleccionDatos(0) = "SIN" Then
                            drop_01_T03Version.Items.Clear()
                            drop_01_T03Version.Items.Add(New ListItem("", ""))
                            drop_01_T03Version.Items.Add(New ListItem("P", "P"))
                            drop_01_T03Version.Items.Add(New ListItem("A", "A"))
                            drop_01_T03Version.Items.Add(New ListItem("B", "B"))
                            drop_01_T03Version.Items.Add(New ListItem("B1", "B1"))
                            drop_01_T03Version.Items.Add(New ListItem("B2", "B2"))
                            drop_01_T03Version.Items.Add(New ListItem("B3", "B3"))
                            drop_01_T03Version.Items.Add(New ListItem("C", "C"))
                            drop_01_T03Version.Items.Add(New ListItem("D", "D"))

                            drop_01_T03Version.Items.Add(New ListItem("0", "0"))
                            drop_01_T03Version.Items.Add(New ListItem("1", "1"))
                            drop_01_T03Version.Items.Add(New ListItem("2", "2"))
                            drop_01_T03Version.Items.Add(New ListItem("3", "3"))
                            drop_01_T03Version.Items.Add(New ListItem("4", "4"))
                            drop_01_T03Version.Items.Add(New ListItem("5", "5"))
                            drop_01_T03Version.Items.Add(New ListItem("6", "6"))
                            drop_01_T03Version.Items.Add(New ListItem("7", "7"))
                            drop_01_T03Version.Items.Add(New ListItem("8", "8"))
                            drop_01_T03Version.Items.Add(New ListItem("9", "9"))
                            drop_01_T03Version.Items.Add(New ListItem("10", "10"))
                            drop_01_T03Version.Items.Add(New ListItem("11", "11"))


                            drop_01_T03Version.Items.Add(New ListItem("SIN", "SIN"))

                        Else
                            drop_01_T03Version.Items.Clear()
                            drop_01_T03Version.Items.Add(New ListItem("", ""))
                            drop_01_T03Version.Items.Add(New ListItem("P", "P"))
                            drop_01_T03Version.Items.Add(New ListItem("A", "A"))
                            drop_01_T03Version.Items.Add(New ListItem("B", "B"))
                            drop_01_T03Version.Items.Add(New ListItem("B1", "B1"))
                            drop_01_T03Version.Items.Add(New ListItem("B2", "B2"))
                            drop_01_T03Version.Items.Add(New ListItem("B3", "B3"))
                            drop_01_T03Version.Items.Add(New ListItem("C", "C"))
                            drop_01_T03Version.Items.Add(New ListItem("D", "D"))

                            drop_01_T03Version.Items.Add(New ListItem("0", "0"))
                            drop_01_T03Version.Items.Add(New ListItem("1", "1"))
                            drop_01_T03Version.Items.Add(New ListItem("2", "2"))
                            drop_01_T03Version.Items.Add(New ListItem("3", "3"))
                            drop_01_T03Version.Items.Add(New ListItem("4", "4"))
                            drop_01_T03Version.Items.Add(New ListItem("5", "5"))
                            drop_01_T03Version.Items.Add(New ListItem("6", "6"))
                            drop_01_T03Version.Items.Add(New ListItem("7", "7"))
                            drop_01_T03Version.Items.Add(New ListItem("8", "8"))
                            drop_01_T03Version.Items.Add(New ListItem("9", "9"))
                            drop_01_T03Version.Items.Add(New ListItem("10", "10"))
                            drop_01_T03Version.Items.Add(New ListItem("11", "11"))

                            drop_01_T03Version.Items.Add(New ListItem("SIN", "SIN"))
                            Try
                                Dim dropTemporal As New DropDownList
                                .drop_CargarCombox(dropTemporal, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
                                If dropTemporal.Text <> "" Then
                                    drop_01_T03Version.Text = dropTemporal.Text
                                End If
                            Catch ex As Exception

                            End Try

                        End If
                    End If
                End If
            Else
                drop_01_T03Version.Items.Clear()
                drop_01_T03Version.Items.Add(New ListItem("", ""))
                drop_01_T03Version.Items.Add(New ListItem("N/A", "N/A"))
                drop_01_T03Version_TextChanged(Nothing, Nothing)
                If clsAdminDb.Mostrar_Error <> "" Then
                    'MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("No se encontraron datos" 
                End If
            End If


        End With
    End Sub

    Private Sub Cargar_Version_Reenviar()
        drop_Version_Reenviar.Items.Clear()
        drop_Version_Reenviar.Items.Add(New ListItem("", ""))
        'drop_Version_Reenviar.Items.Add(New ListItem("", ""))
        'drop_Version_Reenviar.Items.Add(New ListItem("SIN", "SIN"))
        'drop_Version_Reenviar.Items.Add(New ListItem("P", "P"))
        'drop_Version_Reenviar.Items.Add(New ListItem("A", "A"))
        'drop_Version_Reenviar.Items.Add(New ListItem("B", "B"))
        'drop_Version_Reenviar.Items.Add(New ListItem("0", "0"))
        'drop_Version_Reenviar.Items.Add(New ListItem("1", "1"))


        'Exit Sub

        With clsAdminDb
            Dim sTables_tem As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
            Dim sCampos_tem As String = _01_T21LISTADODOCENTREGABLES.Campo_01_T21VersionActual & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21VersionActual
            Dim sLlaves_tem As String = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_Cliente_Reenviar.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_Contrato_Reenviar.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_Proyecto_Reenviar.Text & "'"
            'sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Categoria & "='" & drop_01_T03Disciplina.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "='" & drop_Entregable_Reenviar.Text & "'"

            'Dim clsAdminDb As New adminitradorDB
            Dim coleccionDatos As Object

            coleccionDatos = clsAdminDb.sql_Select(sTables_tem, sCampos_tem, sLlaves_tem)
            If Not coleccionDatos Is Nothing Then
                If coleccionDatos.Length = 0 Then
                    drop_Version_Reenviar.Items.Clear()
                    drop_Version_Reenviar.Items.Add(New ListItem("", ""))
                    drop_Version_Reenviar.Items.Add(New ListItem("N/A", "N/A"))
                    drop_Version_Reenviar_TextChanged(Nothing, Nothing)
                Else
                    If coleccionDatos(0) = "SIN" Then
                        drop_Version_Reenviar.Items.Clear()
                        drop_Version_Reenviar.Items.Add(New ListItem("", ""))
                        drop_Version_Reenviar.Items.Add(New ListItem("P", "P"))
                        drop_Version_Reenviar.Items.Add(New ListItem("A", "A"))
                        drop_Version_Reenviar.Items.Add(New ListItem("B", "B"))
                        drop_Version_Reenviar.Items.Add(New ListItem("B1", "B1"))
                        drop_Version_Reenviar.Items.Add(New ListItem("B2", "B2"))
                        drop_Version_Reenviar.Items.Add(New ListItem("B3", "B3"))
                        drop_Version_Reenviar.Items.Add(New ListItem("C", "C"))
                        drop_Version_Reenviar.Items.Add(New ListItem("D", "D"))

                        drop_Version_Reenviar.Items.Add(New ListItem("0", "0"))
                        drop_Version_Reenviar.Items.Add(New ListItem("1", "1"))
                        drop_Version_Reenviar.Items.Add(New ListItem("2", "2"))
                        drop_Version_Reenviar.Items.Add(New ListItem("3", "3"))
                        drop_Version_Reenviar.Items.Add(New ListItem("4", "4"))
                        drop_Version_Reenviar.Items.Add(New ListItem("5", "5"))
                        drop_Version_Reenviar.Items.Add(New ListItem("6", "6"))
                        drop_Version_Reenviar.Items.Add(New ListItem("7", "7"))
                        drop_Version_Reenviar.Items.Add(New ListItem("8", "8"))
                        drop_Version_Reenviar.Items.Add(New ListItem("9", "9"))
                        drop_Version_Reenviar.Items.Add(New ListItem("10", "10"))
                        drop_Version_Reenviar.Items.Add(New ListItem("11", "11"))


                        drop_Version_Reenviar.Items.Add(New ListItem("SIN", "SIN"))

                    Else
                        drop_Version_Reenviar.Items.Clear()
                        drop_Version_Reenviar.Items.Add(New ListItem("", ""))
                        drop_Version_Reenviar.Items.Add(New ListItem("P", "P"))
                        drop_Version_Reenviar.Items.Add(New ListItem("A", "A"))
                        drop_Version_Reenviar.Items.Add(New ListItem("B", "B"))
                        drop_Version_Reenviar.Items.Add(New ListItem("B1", "B1"))
                        drop_Version_Reenviar.Items.Add(New ListItem("B2", "B2"))
                        drop_Version_Reenviar.Items.Add(New ListItem("B3", "B3"))
                        drop_Version_Reenviar.Items.Add(New ListItem("C", "C"))
                        drop_Version_Reenviar.Items.Add(New ListItem("D", "D"))

                        drop_Version_Reenviar.Items.Add(New ListItem("0", "0"))
                        drop_Version_Reenviar.Items.Add(New ListItem("1", "1"))
                        drop_Version_Reenviar.Items.Add(New ListItem("2", "2"))
                        drop_Version_Reenviar.Items.Add(New ListItem("3", "3"))
                        drop_Version_Reenviar.Items.Add(New ListItem("4", "4"))
                        drop_Version_Reenviar.Items.Add(New ListItem("5", "5"))
                        drop_Version_Reenviar.Items.Add(New ListItem("6", "6"))
                        drop_Version_Reenviar.Items.Add(New ListItem("7", "7"))
                        drop_Version_Reenviar.Items.Add(New ListItem("8", "8"))
                        drop_Version_Reenviar.Items.Add(New ListItem("9", "9"))
                        drop_Version_Reenviar.Items.Add(New ListItem("10", "10"))
                        drop_Version_Reenviar.Items.Add(New ListItem("11", "11"))

                        drop_Version_Reenviar.Items.Add(New ListItem("SIN", "SIN"))
                        Try
                            Dim dropTemporal As New DropDownList
                            .drop_CargarCombox(dropTemporal, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)
                            If dropTemporal.Text <> "" Then
                                drop_Version_Reenviar.Text = dropTemporal.Text
                            End If
                        Catch ex As Exception

                        End Try

                    End If

                End If
            Else
                drop_Version_Reenviar.Items.Clear()
                drop_Version_Reenviar.Items.Add(New ListItem("", ""))
                drop_Version_Reenviar.Items.Add(New ListItem("N/A", "N/A"))
                drop_Version_Reenviar_TextChanged(Nothing, Nothing)
                If clsAdminDb.Mostrar_Error <> "" Then
                    'MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("No se encontraron datos" 
                End If
            End If


        End With
    End Sub

    Private Sub drop_01_T03ODS_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T03ODS.TextChanged
        drop_01_T03Actividad.Items.Clear()
        drop_01_T03Actividad.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03Entregable.Items.Clear()
        drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03Version.Items.Clear()
        Drop_01_T03Horas.Text = ""
        txt_01_T03DescripcionAcguividad.Text = ""
        txt_Cons_Aprobador.Text = ""
        Mostrar_panel_IdAprobacion(False)
        Drop_01_T03Horas.Text = ""
        Dim coleccionDatos As Object
        Dim sAprobador As String = "(select top 1 _01_T12NombreApellidos  from _01_T12FUNCIONARIOS where _01_T12NumDoc=_01_T09DocumentoCoordinador)"
        coleccionDatos = clsAdminDb.sql_Select(_01_T09ODS.NombreTabla, _01_T09ODS.CampoLlave_01_T09Cliente & "," & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "," & sAprobador, _01_T09ODS.Campo_01_T09Codigo & "='" & drop_01_T03ODS.Text & "'")
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
            Else
                'drop_01_T10Cliente.Text = coleccionDatos(0)
                cargar_Cliente(coleccionDatos(0))
                cargar_Contrato(coleccionDatos(0), coleccionDatos(1))
                Cargar_Disciplina()
                txt_Cons_Aprobador.Text = coleccionDatos(2)

            End If
        Else
        End If
        If drop_01_T03ODS.Text <> "" Then
            bHabilitarControles(True, True, False, False, False, False, False, False)
        Else
            bHabilitarControles(True, False, False, False, False, False, False, False)
        End If



        cargar_Tabla()
    End Sub

    Private Sub cargar_Contrato(ByVal sCliente As String, ByVal sContratoMarco As String)
        drop_01_T03Contrato.Items.Clear()
        'drop_01_T10Contrato.Items.Add(New ListItem("Seleccionar", ""))

        clsAdminDb.drop_CargarCombox(drop_01_T03Contrato, _01_T07CONTRATOSMARCO.NombreTabla, _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo & "," & _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo, _01_T07CONTRATOSMARCO.CampoLlave_01_T07Cliente & "='" & drop_01_T03Cliente.Text & "' AND " & _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo & "='" & sContratoMarco & "'", False)
    End Sub

    Private Sub cargar_Contrato_Reenviar(ByVal sCliente As String, ByVal sContratoMarco As String)
        drop_Contrato_Reenviar.Items.Clear()
        'drop_01_T10Contrato.Items.Add(New ListItem("Seleccionar", ""))

        clsAdminDb.drop_CargarCombox(drop_Contrato_Reenviar, _01_T07CONTRATOSMARCO.NombreTabla, _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo & "," & _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo, _01_T07CONTRATOSMARCO.CampoLlave_01_T07Cliente & "='" & drop_Cliente_Reenviar.Text & "' AND " & _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo & "='" & sContratoMarco & "'", False)
    End Sub

    Private Sub cargar_Cliente(ByVal sCliente As String)
        drop_01_T03Cliente.Items.Clear()
        'drop_01_T10Cliente.Items.Add(New ListItem("Seleccionar", ""))

        clsAdminDb.drop_CargarCombox(drop_01_T03Cliente, _01_T08CLIENTES.NombreTabla, _01_T08CLIENTES.CampoLlave_01_T08Nit & "," & _01_T08CLIENTES.Campo_01_T08Nombre, _01_T08CLIENTES.CampoLlave_01_T08Nit & "='" & sCliente & "'", False)
    End Sub

    Private Sub cargar_Cliente_Reenviar(ByVal sCliente As String)
        drop_Cliente_Reenviar.Items.Clear()
        'drop_01_T10Cliente.Items.Add(New ListItem("Seleccionar", ""))

        clsAdminDb.drop_CargarCombox(drop_Cliente_Reenviar, _01_T08CLIENTES.NombreTabla, _01_T08CLIENTES.CampoLlave_01_T08Nit & "," & _01_T08CLIENTES.Campo_01_T08Nombre, _01_T08CLIENTES.CampoLlave_01_T08Nit & "='" & sCliente & "'", False)
    End Sub




    Private Sub drop_01_T03Entregable_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T03Entregable.TextChanged
        Mostrar_panel_IdAprobacion(False)
        drop_01_T03Fecha.Text = ""
        Drop_01_T03Horas.Text = ""
        Cargar_Version()
        If drop_01_T03Entregable.Text <> "" Then
            Cargar_Actividades()
        Else
            drop_01_T03Actividad.Items.Clear()
            drop_01_T03EntregableSeringrtec.Items.Clear()
            drop_01_T03Actividad.Items.Add(New ListItem("Seleccionar", ""))
            Dim sTables_tem = _01_T21LISTADODOCENTREGABLES.NombreTabla
            Dim sCampos_tem = _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec & "," & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoSeringtec
            Dim sLlaves_tem = _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Cliente & "='" & drop_01_T03Cliente.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Contrato & "='" & drop_01_T03Contrato.Text & "'"
            sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21ODS & "='" & drop_01_T03ODS.Text & "'"
            'sLlaves_tem = sLlaves_tem & " and " & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol & "='" & drop_01_T03Entregable.Text & "'"
            drop_01_T03EntregableSeringrtec.Items.Clear()
            drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
            drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("N/A", "N/A"))
            clsAdminDb.drop_CargarCombox(drop_01_T03EntregableSeringrtec, sTables_tem, sCampos_tem, sLlaves_tem, False)


            'drop_01_T03EntregableSeringrtec.Items.Add(New ListItem("Seleccionar", ""))
        End If
        If drop_01_T03Entregable.Text <> "" Then
            bHabilitarControles(True, True, True, False, False, False, False, False)
        Else
            bHabilitarControles(True, True, False, False, False, False, False, False)
        End If
        Cargar_Version()
    End Sub

    Private Sub drop_01_T03EntregableSeringrtec_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T03EntregableSeringrtec.TextChanged
        Mostrar_panel_IdAprobacion(False)
        drop_01_T03Fecha.Text = ""
        Drop_01_T03Horas.Text = ""
        Cargar_Version()
        Cargar_Actividades_TEm()
        If drop_01_T03EntregableSeringrtec.Text <> "" Then
            bHabilitarControles(True, True, True, False, False, False, False, False)
        Else
            bHabilitarControles(True, True, False, False, False, False, False, False)
        End If
        Cargar_Version()
    End Sub


    Private Sub drop_01_T03Disciplina_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T03Disciplina.TextChanged
        Cargar_Entregable()
    End Sub

    '*********** Termina logueo **********************************************************
    '***************************************************************************************

    Function iConsecutivo() As Integer
        iConsecutivo = 0
        Dim iCons As Integer = 0
        Dim lCantidad As Integer = 0
        iCons = clsAdminDb.sql_Max(_01_T03REGISTROHORAS.NombreTabla, "cast(" & _01_T03REGISTROHORAS.CampoLlave_01_T03Consecutivo & " as bigint) ", _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "=  '" & lblUsuarioLogueado_Documento.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Contrato & "=  '" & drop_01_T03Contrato.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03ODS & "=  '" & drop_01_T03ODS.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Entregable & "='" & drop_01_T03Entregable.Text & "' and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Fecha & "='" & drop_01_T03Fecha.Text & "'")
        'iCons = clsAdminDb.sql_Max(_04_T49FACTURACIONDETALLE.NombreTabla, "cast(" & _04_T49FACTURACIONDETALLE.CampoLlave_04_T49Consecutivo & " as bigint) ", _04_T49FACTURACIONDETALLE.CampoLlave_04_T49TipoServidor & "=  '" & drop_04_T44TipoServidor.Text & "'" & " AND " & _04_T49FACTURACIONDETALLE.CampoLlave_04_T49CodEntidadPrestadora & "=  '" & drop_07_Cont_T10EmpresaContable.Text & "'" & " AND " & _04_T49FACTURACIONDETALLE.Campo_04_T49TarifaConvenioCto & "=  '" & drop_04_T49Servicios.Text & "'")
        If iCons = 0 Then
            iConsecutivo = 1
        Else
            iConsecutivo = iCons + 1
        End If
    End Function

    Function iConsecutivo_ReenvioTEm() As Integer
        iConsecutivo_ReenvioTEm = 0
        Dim iCons As Integer = 0
        Dim lCantidad As Integer = 0
        iCons = clsAdminDb.sql_Max(_01_T03REGISTROHORAS.NombreTabla, "cast(" & _01_T03REGISTROHORAS.CampoLlave_01_T03Consecutivo & " as bigint) ", _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "=  '" & lblUsuarioLogueado_Documento.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Contrato & "=  '" & drop_Contrato_Reenviar.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03ODS & "=  '" & drop_Proyecto_Reenviar.Text & "'" & " AND " & _01_T03REGISTROHORAS.CampoLlave_01_T03Fecha & "='" & txtReenviado_Fecha.Text & "'")
        'iCons = clsAdminDb.sql_Max(_04_T49FACTURACIONDETALLE.NombreTabla, "cast(" & _04_T49FACTURACIONDETALLE.CampoLlave_04_T49Consecutivo & " as bigint) ", _04_T49FACTURACIONDETALLE.CampoLlave_04_T49TipoServidor & "=  '" & drop_04_T44TipoServidor.Text & "'" & " AND " & _04_T49FACTURACIONDETALLE.CampoLlave_04_T49CodEntidadPrestadora & "=  '" & drop_07_Cont_T10EmpresaContable.Text & "'" & " AND " & _04_T49FACTURACIONDETALLE.Campo_04_T49TarifaConvenioCto & "=  '" & drop_04_T49Servicios.Text & "'")
        If iCons = 0 Then
            iConsecutivo_ReenvioTEm = 1
        Else
            iConsecutivo_ReenvioTEm = iCons + 1
        End If
    End Function


    Function iConsecutivoRenvio() As Integer
        iConsecutivoRenvio = 0
        Dim iCons As Integer = 0
        Dim lCantidad As Integer = 0
        iCons = clsAdminDb.sql_Max(_01_T03_2REGISTROHORAS_HISTORIAL.NombreTabla, "cast(" & _01_T03_2REGISTROHORAS_HISTORIAL.Campo_01_T03_2ConsecutivoReenviado & " as bigint) ", _01_T03_2REGISTROHORAS_HISTORIAL.CampoLlave_01_T03_2Usuario & "=  '" & lblUsuarioLogueado_Documento.Text & "'")
        'iCons = clsAdminDb.sql_Max(_04_T49FACTURACIONDETALLE.NombreTabla, "cast(" & _04_T49FACTURACIONDETALLE.CampoLlave_04_T49Consecutivo & " as bigint) ", _04_T49FACTURACIONDETALLE.CampoLlave_04_T49TipoServidor & "=  '" & drop_04_T44TipoServidor.Text & "'" & " AND " & _04_T49FACTURACIONDETALLE.CampoLlave_04_T49CodEntidadPrestadora & "=  '" & drop_07_Cont_T10EmpresaContable.Text & "'" & " AND " & _04_T49FACTURACIONDETALLE.Campo_04_T49TarifaConvenioCto & "=  '" & drop_04_T49Servicios.Text & "'")
        If iCons = 0 Then
            iConsecutivoRenvio = 1
        Else
            iConsecutivoRenvio = iCons + 1
        End If
    End Function

    Private Sub drop_01_T03Cliente_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T03Cliente.TextChanged
        Cargar_Contrato()
    End Sub

    Private Sub Cargar_Contrato()
        drop_01_T03Contrato.Items.Clear()
        drop_01_T03Contrato.Items.Add(New ListItem("Seleccionar", ""))
        drop_01_T03ODS.Items.Clear()
        drop_01_T03ODS.Items.Add(New ListItem("Seleccionar", ""))

        Dim sLlaves_tem As String = _01_T07CONTRATOSMARCO.CampoLlave_01_T07Cliente & "=" & _01_T10RECURSOSxODS.CampoLlave_01_T10Cliente
        sLlaves_tem = sLlaves_tem & " and " & _01_T07CONTRATOSMARCO.CampoLlave_01_T07Cliente & "='" & drop_01_T03Cliente.Text & "'"
        sLlaves_tem = sLlaves_tem & " and " & _01_T10RECURSOSxODS.CampoLlave_01_T10DocumentoFuncionario & "='" & lblUsuarioLogueado_Documento.Text & "'"
        Dim sTables As String = _01_T10RECURSOSxODS.NombreTabla & "," & _01_T07CONTRATOSMARCO.NombreTabla


        clsAdminDb.drop_CargarCombox(drop_01_T03Contrato, sTables, _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo & "," & _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo, sLlaves_tem, False, _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo & "," & _01_T07CONTRATOSMARCO.CampoLlave_01_T07Codigo)
    End Sub


    Private Sub Cargar_Disciplina()
        drop_01_T03Entregable.Items.Clear()
        'drop_01_T03Disciplina.Items.Clear()
        'drop_01_T03Version.Items.Clear()
        'drop_01_T03Version.Items.Add(New ListItem("Seleccionar", ""))


        drop_01_T03Entregable.Items.Add(New ListItem("Seleccionar", ""))

        With clsAdminDb
            Dim sTables_tem As String = _01_T06DISCIPLINAS.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
            Dim sCampos_tem As String = _01_T06DISCIPLINAS.CampoLlave_01_T06Codigo & "," & _01_T06DISCIPLINAS.Campo_01_T06Nombre
            Dim sLlaves_tem As String = _01_T06DISCIPLINAS.CampoLlave_01_T06Codigo & "=" & _01_T12FUNCIONARIOS.Campo_01_T12Disciplina
            sLlaves_tem = sLlaves_tem & " and " & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & lblUsuarioLogueado_Documento.Text & "'"

            .drop_CargarCombox(drop_01_T03Disciplina, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)

        End With
        Cargar_Entregable()
        'drop_01_T03Disciplina.Text = lblUsuarioLogueado_Disciplina.Text
    End Sub

    Private Sub Cargar_Disciplina_Reenviar()
        drop_Entregable_Reenviar.Items.Clear()
        'drop_01_T03Disciplina.Items.Clear()
        'drop_01_T03Version.Items.Clear()
        'drop_01_T03Version.Items.Add(New ListItem("Seleccionar", ""))


        drop_Entregable_Reenviar.Items.Add(New ListItem("Seleccionar", ""))

        With clsAdminDb
            Dim sTables_tem As String = _01_T06DISCIPLINAS.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
            Dim sCampos_tem As String = _01_T06DISCIPLINAS.CampoLlave_01_T06Codigo & "," & _01_T06DISCIPLINAS.Campo_01_T06Nombre
            Dim sLlaves_tem As String = _01_T06DISCIPLINAS.CampoLlave_01_T06Codigo & "=" & _01_T12FUNCIONARIOS.Campo_01_T12Disciplina
            sLlaves_tem = sLlaves_tem & " and " & _01_T12FUNCIONARIOS.CampoLlave_01_T12NumDoc & "='" & lblUsuarioLogueado_Documento.Text & "'"

            .drop_CargarCombox(drop_Disciplina_Reenviar, sTables_tem, sCampos_tem, sLlaves_tem, False, sCampos_tem)

        End With
        Cargar_Entregable_Reenviar()
        Cargar_Entregable_Reenviar_Seringtec()
        'drop_01_T03Disciplina.Text = lblUsuarioLogueado_Disciplina.Text
    End Sub

    Private Sub drop_01_T03Version_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T03Version.TextChanged
        'drop_01_T03Fecha.Text = ""
        Drop_01_T03Horas.Text = ""
        If drop_01_T03Version.Text <> "" Then
            bHabilitarControles(True, True, True, True, True, True, False, False)
        Else
            bHabilitarControles(True, True, True, True, False, False, False, False)
        End If
    End Sub

    Private Sub drop_01_T03Fecha_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T03Fecha.TextChanged
        Mostrar_panel_IdAprobacion(False)
        Drop_01_T03Horas.Text = ""
        If drop_01_T03Version.Text <> "" Then
            bHabilitarControles(True, True, True, True, True, True, False, False)
        Else
            bHabilitarControles(True, True, True, True, True, False, False, False)
        End If
    End Sub

    Private Sub Drop_01_T03Horas_TextChanged(sender As Object, e As EventArgs) Handles Drop_01_T03Horas.TextChanged
        Mostrar_panel_IdAprobacion(False)
        If Drop_01_T03Horas.Text <> "" Then
            bHabilitarControles(True, True, True, True, True, True, True, True)
        Else
            bHabilitarControles(True, True, True, True, True, True, False, False)
        End If

    End Sub

    Protected Sub btnItem_Click(sender As Object, e As ImageClickEventArgs) Handles btnItem.Click
        If bValidarCampos() = False Then Exit Sub
        If bValidarSQL() = False Then Exit Sub
        Guardar_Registro()
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim sOrderby As String = ""
        Dim coleccionDatosPlural As New Collection

        Dim sTablaLocal As String = _01_T03REGISTROHORAS.NombreTabla '& "," & _01_T06DISCIPLINAS.NombreTabla
        Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03ActividadesCodigo as _01_T03Entregable,_01_T03DescripcionActividad,_01_T03Version,_01_T03Fecha,_01_T03tiempo,_01_T03EnviadoAProbacion,_01_T03Estado,_01_T03Consecutivo,'" & lblUsuarioLogueado_Nombre.Text & "- del " & drop_01_T03Fecha.Text & "' as _01_T03DocFuncionario,'' as_01_T06Nombre,_01_T03ActividadesDescripcion,_01_T03TiempoaHoras,_01_T03Aprobado_Fecha,_01_T03Aprobado_Hora,_01_T03Aprobado_IDAprobacion,_01_T03Aprobado_Rechazo_Obs,'" & txt_IdAprobacionTemporal.Text & "'  as _01_T03Envio_IDEnviado,_01_T03Envio_FechaEnviado,_01_T03Envio_HoraEnviado,_01_T03Envio_Observaciones"
        Dim sLlavesLocal As String = _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & " ='" & lblUsuarioLogueado_Documento.Text & "'  "
        'sLlavesLocal = sLlavesLocal & " and " & _01_T03REGISTROHORAS.Campo_01_T03Disciplina & "=" & _01_T06DISCIPLINAS.CampoLlave_01_T06Codigo

        If panel_IdAprobacion.Visible = False Or txt_IdAprobacion.Text = "" Then
            Exit Sub
        Else

            If drop_01_T03ODS.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and _01_T03ODS='" & drop_01_T03ODS.Text & "'"
                sLlavesLocal = sLlavesLocal & " and _01_T03Contrato='" & drop_01_T03Contrato.Text & "'"
                sLlavesLocal = sLlavesLocal & " and _01_T03Cliente='" & drop_01_T03Cliente.Text & "'"
            End If
            If drop_01_T03Disciplina.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and _01_T03Disciplina='" & drop_01_T03Disciplina.Text & "'"
            End If
            If drop_01_T03Entregable.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and _01_T03Entregable='" & drop_01_T03Entregable.Text & "'"
            End If
            If drop_01_T03Fecha.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and _01_T03Fecha='" & drop_01_T03Fecha.Text & "'"
            End If
            If txt_IdAprobacion.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and _01_T03Envio_IDEnviado='" & txt_IdAprobacion.Text & "'"
            End If


        End If
        Dim sRutaArchivo As String = "\Reportes\Ontime\RegistroTiempo\rpt_RegistroTiempo.rpt"
        Dim sConsulta As String = " select " & sCamposTablaLocal & " from " & sTablaLocal & " where " & sLlavesLocal
        rptCrystal = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        rptCrystal = clsAdminDb.sql_Imprimir(sConsulta, sRutaArchivo)

        If clsAdminDb.Mostrar_Error <> "" Then
            MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
        Else
            MostrarMensaje("Se genero correctamente el reporte")
            rptCrystal.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, True, "rpt_RecursosAsignados_ODS_" & drop_01_T03ODS.Text & "_" & lblUsuarioLogueado_Documento.Text)
        End If

    End Sub

    Private Sub cargar_Tabla_ver(Optional ByVal sLlaveTem As String = "")
        'Dim clsAdminDb As New adminitradorDB

        If txt_Tabla_Ver.Text = "" Then
        Else
            Exit Sub
        End If
        Dim coleccionDatosPlural As New Collection
        Dim sGroupby As String = "_01_T03Fecha,CAST  (_01_T03ActividadesDescripcion AS VARCHAR(3000)),_01_T03tiempo,_01_T03DescripcionActividad,_01_T03Cliente,_01_T03Contrato,_01_T03ODS,_01_T03Entregable,_01_T03Fecha,_01_T03Consecutivo,_01_T03DocFuncionario"
        Dim sCamposTablaLocal As String = "_01_T03Fecha,CAST  (_01_T03ActividadesDescripcion AS VARCHAR(3000)),_01_T03tiempo,_01_T03DescripcionActividad,_01_T03Cliente,_01_T03Contrato,_01_T03ODS,_01_T03ActividadesCodigo,_01_T03Fecha,_01_T03Consecutivo,_01_T03DocFuncionario" '
        Dim sLlavesLocal As String = "_01_T12NumDoc=_01_T03DocFuncionario "
        sLlavesLocal = sLlavesLocal & " And _01_T03Fecha=_01_T22Fecha"
        'sLlavesLocal = sLlavesLocal & " And _01_T21ODS=_01_T09Codigo And _01_T09Cliente=_01_T21Cliente And _01_T21Contrato=_01_T09ContratoMArco "
        'sLlavesLocal = sLlavesLocal & " And _01_T21ODS = _01_T09Codigo And _01_T09Cliente = _01_T21Cliente And _01_T21Contrato = _01_T09ContratoMArco "
        'sLlavesLocal = sLlavesLocal & " And _01_T03ODS=_01_T09Codigo And _01_T09Cliente=_01_T03Cliente And _01_T03Contrato=_01_T09ContratoMArco "
        'sLlavesLocal = sLlavesLocal & " And _01_T03ODS=_01_T21ODS And _01_T21Cliente=_01_T03Cliente And _01_T03Contrato=_01_T21Contrato "
        'sLlavesLocal = sLlavesLocal & " And _01_T21Disciplina=_01_T03Disciplina"
        'sLlavesLocal = sLlavesLocal & " And _01_T21CodigoEcopetrol = _01_T03Entregable"
        sLlavesLocal = sLlavesLocal & " And _01_T03Estado='Registrado'"
        'sLlavesLocal = sLlavesLocal & " and " & _01_T03REGISTROHORAS.Campo_01_T03Estado & "='Registrado'"
        sLlavesLocal = sLlavesLocal & " and _01_T03DocFuncionario='" & txt_Popup_Numdoc_Ver.Text & "'"
        If txt_Popup_Numdoc_Ver.Text = "" Then
            Exit Sub
        Else
            'sLlavesLocal = sLlavesLocal & " and _01_T03Contrato='" & drop_01_T03Contrato.Text & "'"
            If DropODS_Ver.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and _01_T03ODS='" & DropODS_Ver.Text & "'"
            End If
            If drop_01_T03Disciplina.Text <> "" Then
                sLlavesLocal = sLlavesLocal & " and _01_T03Disciplina='" & drop_01_T03Disciplina.Text & "'"
            End If
            'If drop_01_T03Entregable.Text <> "" Then
            '    sLlavesLocal = sLlavesLocal & " and _01_T03Entregable='" & drop_01_T03Entregable.Text & "'"
            'End If
            sLlavesLocal = sLlavesLocal & " and _01_T03DocFuncionario='" & txt_Popup_Numdoc_Ver.Text & "'"



        End If

        Dim sTablaLocal As String = _01_T03REGISTROHORAS.NombreTabla & "," & _01_T22AGNOS.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla '& "," & _01_T21LISTADODOCENTREGABLES.NombreTabla
        'Dim sTablaLocal As String = _01_T03REGISTROHORAS.NombreTabla & "," & _01_T06DISCIPLINAS.NombreTabla & "," & _01_T09ODS.NombreTabla & "," & _01_T21LISTADODOCENTREGABLES.NombreTabla & "," & _01_T22AGNOS.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
        Dim ArregloSingular() As String
        Tbody2.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
        End If
        Dim i As Integer = 1
        'Dim chk_Tem As CheckBox
        Dim llTem1 As Label
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal,,)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow

                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"

                llTem1 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "yyuC-sep-" '& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(0)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"

                llTem1 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "mmyyuC-sep-" '& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(6)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"

                llTem1 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "yyyuD-sep-" ''& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(1)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)

                'CeldaHtml = New HtmlTableCell
                'llTem1 = New Label
                ''Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                'llTem1.ID = i & "yyyyE-sep-" ''& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                ''AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'llTem1.Text = ArregloSingular(2)
                'CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                'CeldaHtml.Controls.Add(llTem1)
                'FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                llTem1 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "Veruy-sep-" ''& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(2)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)
                CeldaHtml = New HtmlTableCell
                llTem1 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                'linkbutonHtml.ID = i & "yzzyyyE-sep-" ''& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(3)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)


                Dim chk_Tem As CheckBox
                CeldaHtml = New HtmlTableCell
                chk_Tem = New CheckBox
                'CeldaHtml.BorderColor = "YELLOW"
                chk_Tem.ID = i & "mmuk13-sep-" & ArregloSingular(4).ToString & "-sep-" & ArregloSingular(5).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(7).ToString & "-sep-" & ArregloSingular(8).ToString & "-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString
                'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(chk_Tem)
                'chk_Tem.OnClientClick = "bPreguntar = false;"
                chk_Tem.ForeColor = Drawing.Color.DarkGreen
                chk_Tem.Font.Bold = True
                chk_Tem.ValidationGroup = "checkbox"
                chk_Tem.Font.Size = FontUnit.Point(8)
                'AddHandler linkbutonHtml_Aprobar.Click, AddressOf linkbutonHtml_Aprobar_Click
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"
                CeldaHtml.Controls.Add(chk_Tem)
                FilaHtml.Cells.Add(CeldaHtml)



                'CeldaHtml = New HtmlTableCell
                'CeldaHtml.Align = "Center"
                ''CeldaHtml.BgColor = "DarkBlue"
                'CeldaHtml.BorderColor = "DarkBlue"

                'llTem1 = New Label
                ''Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                ''linkbutonHtml.ID = i & "mmmH-sep-" ''& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                ''AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                'llTem1.Text = ArregloSingular(5)
                'CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                'CeldaHtml.Controls.Add(llTem1)
                'FilaHtml.Cells.Add(CeldaHtml)



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

    Private Sub btnAprobar_Ver_Click(sender As Object, e As EventArgs) Handles btnAprobar_Ver.Click
        ModalPopupVer.Show()
        If bValidar_Multiple_Ver() = False Then
            Mostrar_panel_IdAprobacion(False)
            Exit Sub
        End If

        If Guardar_Registro_Envio() = True Then

            If Guardar_Multiple_Ver("", "Enviado") = True Then
                txt_01_T23Consecutivo.Text = ""
                'txt_Observaciones.Text = ""
                txt_01_T23Codigo.Text = ""
                txt_Observaciones_Ver.Text = ""
                txt_Popup_Numdoc_Ver.Text = ""
                txt_Popup_Numdoc_Ver.Text = ""
                drop_01_T03ODS.Text = ""
                Try
                    drop_01_T03Entregable.Text = ""
                Catch ex As Exception

                End Try

                drop_01_T03Version.Text = ""
                drop_01_T03Fecha.Text = ""
                Drop_01_T03Horas.Text = ""
                txt_01_T03Observaciones.Text = ""
                bHabilitarControles(True, False, False, False, False, False, False, False)
                ModalPopupVer.Hide()
                cargar_Tabla()
            End If

        End If


    End Sub
    Private Function Guardar_Multiple_Ver(Optional ByVal sNumRecurso As String = "", Optional ByVal sRechazoAprobado As String = "") As Boolean
        Guardar_Multiple_Ver = False
        Dim bGuardar_FactTem As Boolean = False
        Dim FilaHtml = New HtmlTableRow
        Dim sNumRecurso_TEm As String = ""

        For Each FilaHtml In Tbody2.Controls
            Dim sObjeto1 As New Object
            Dim sObjeto As New Object
            Dim lbl_Funcionrio As New Label
            Dim chk As New CheckBox
            chk = FilaHtml.Controls(5).Controls(0)

            If chk.Checked = True And sNumRecurso = "" Then
                Dim TestArray() As String = Split(chk.ID, "-sep-")
                Dim sLlaves_TEm As String = TestArray(1).ToString
                sLlaves_TEm = _01_T03REGISTROHORAS.CampoLlave_01_T03Cliente & "='" & TestArray(1).ToString & "'"
                sLlaves_TEm = sLlaves_TEm & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Contrato & "='" & TestArray(2).ToString & "'"
                sLlaves_TEm = sLlaves_TEm & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03ODS & "='" & TestArray(3).ToString & "'"
                sLlaves_TEm = sLlaves_TEm & " and " & _01_T03REGISTROHORAS.Campo_01_T03ActividadesCodigo & "='" & TestArray(4).ToString & "'"
                sLlaves_TEm = sLlaves_TEm & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Fecha & "='" & TestArray(5).ToString & "'"
                sLlaves_TEm = sLlaves_TEm & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Consecutivo & "='" & TestArray(6).ToString & "'"
                sLlaves_TEm = sLlaves_TEm & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "='" & TestArray(7).ToString & "'"

                Guardar_Multiple_Ver = Guardar_ActulizarRegistroHoras(sLlaves_TEm, "", "", sRechazoAprobado, "")

            ElseIf sNumRecurso <> "" Then
                Dim TestArray() As String = Split(chk.ID, "-sep-")
                sNumRecurso_TEm = TestArray(1).ToString
                If sNumRecurso_TEm = sNumRecurso Then
                    Datos_Modulo()
                    Guardar_Registro()
                    Exit Function
                End If


            End If
            'tran.Complete()
        Next

    End Function

    Private Function Guardar_ActulizarRegistroHoras(ByVal sGuardarActualizar_Table_Ver As String, ByVal sGuardarActualizar_Table As String, ByVal sGuardarActualizar_Link As String, ByVal sRechazoAprobado As String, ByVal sGuardarReenvio As String) As Boolean
        Dim sLlsavesTem As String = "_01_T03Fecha=_01_T22Fecha"
        'sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Contrato & "='" & drop_01_T03Contrato.Text & "'"
        'sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03ODS & "='" & drop_01_T03ODS.Text & "'"
        'sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.Campo_01_T03Disciplina & "='" & drop_01_T03Disciplina.Text & "'"
        'sLlsavesTem = sLlsavesTem & " and _01_T03Fecha=_01_T22Fecha"
        Guardar_ActulizarRegistroHoras = False
        If sGuardarActualizar_Link <> "" Then
            sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "='" & txt_Popup_Numdoc_Ver.Text & "'"
            'sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Entregable & "='" & txt_Popup_Entregable.Text & "'"
            sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.Campo_01_T03Estado & "='Registrado'"
            'sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Fecha & "='" & txt_Popup_Fecha.Text & "'"
            'sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Consecutivo & "='" & txt_Popup_Consecutivo.Text & "'"
            If txt_Popup_Numdoc_Ver.Text = "" Then
                MostrarMensaje("Error Interno no se realizo el proceso, por favor intentelo nuevamente")
                Exit Function
            End If
            Dim bSqlInsert As Boolean = clsAdminDb.sql_sConsulta("UPDATE _01_T03REGISTROHORAS SET _01_T03Estado='Enviado',_01_T03Envio_FechaEnviado='" & txt_01_T23Fecha.Text & "',_01_T03Envio_HoraEnviado='" & Now.Hour & ":" & Now.Minute & "',_01_T03Envio_IDEnviado='" & txt_01_T23Codigo.Text & "',_01_T03Envio_Observaciones='" & "" & "' from _01_T22AGNOS, _01_T03REGISTROHORAS where " & sLlsavesTem)
            If bSqlInsert = True Then
                If sRechazoAprobado = "Rechazado" Then
                    MostrarMensaje("Se RECHAZO Correctamente el Tiempo para:  " & lblUsuarioLogueado_Nombre.Text, True)
                Else
                    MostrarMensaje("Se ENVIO Correctamente el Tiempo para Aprobacion", True)
                End If

                'txt_01_T23Consecutivo.Text = ""
                'txt_Observaciones.Text = ""
                'txt_01_T23Codigo.Text = ""
                'txt_Popup_Entregable.Text = ""
                'txt_Popup_Fecha.Text = ""
                'txt_Popup_Consecutivo.Text = ""
                Guardar_ActulizarRegistroHoras = True
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If

        ElseIf sGuardarActualizar_Table_Ver <> "" Then
            sLlsavesTem = sLlsavesTem & " and " & sGuardarActualizar_Table_Ver
            sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.Campo_01_T03Estado & "='Registrado'"
            Dim bSqlInsert As Boolean = clsAdminDb.sql_sConsulta("UPDATE _01_T03REGISTROHORAS SET _01_T03Estado='Enviado',_01_T03Envio_FechaEnviado='" & txt_01_T23Fecha.Text & "',_01_T03Envio_HoraEnviado='" & Now.Hour & ":" & Now.Minute & "',_01_T03Envio_IDEnviado='" & txt_01_T23Codigo.Text & "',_01_T03Envio_Observaciones='" & txt_Observaciones_Ver.Text & "' from _01_T22AGNOS, _01_T03REGISTROHORAS where " & sLlsavesTem)
            'Dim bSqlInsert As Boolean = clsAdminDb.sql_sConsulta("UPDATE _01_T03REGISTROHORAS SET _01_T03Estado='" & sRechazoAprobado & "',_01_T03Aprobado_Fecha='" & txt_01_T23Fecha.Text & "',_01_T03Aprobado_Hora='" & Now.Hour & ":" & Now.Minute & "',_01_T03Aprobado_IDAprobacion='" & txt_01_T23Codigo.Text & "' from _01_T22AGNOS, _01_T03REGISTROHORAS where " & sLlsavesTem)
            If bSqlInsert = True Then
                If sRechazoAprobado = "Rechazado" Then
                    MostrarMensaje("Se RECHAZO Correctamente el Tiempo ", True)
                Else
                    MostrarMensaje("Se Enviado Correctamente el Tiempo ", True)
                End If

                Guardar_ActulizarRegistroHoras = True
            Else

                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If

        ElseIf sGuardarReenvio <> "" Then
            sLlsavesTem = sLlsavesTem & " and " & sGuardarReenvio
            sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.Campo_01_T03Estado & "='Rechazado'"
            Dim sConsecutivo = iConsecutivo_ReenvioTEm()
            Dim Horasitem As TimeSpan = TimeSpan.Parse(drop_Tiempo_Reenviar.Text)
            'HorasTotal = HorasTotalBD + Horasitem
            Dim txt_01_T03TiempoaHoras_TEm = Horasitem.TotalHours

            'sConsulta = "update _01_T03REGISTROHORAS set _01_T03Estado='Enviado',_01_T03ActividadesDescripcion ='" & drop_Actividades_Reenviar.SelectedItem.Text & "',_01_T03ActividadesCodigo ='" & drop_Actividades_Reenviar.Text & "',_01_T03ODS ='" & drop_Proyecto_Reenviar.Text & "',_01_T03Contrato ='" & drop_Contrato_Reenviar.Text & "',_01_T03Cliente ='" & drop_Cliente_Reenviar.Text & "',_01_T03Consecutivo='" & sConsecutivo & "',_01_T03Aprobado_Fecha='',_01_T03Aprobado_Hora='',_01_T03Aprobado_IDAprobacion='',_01_T03Aprobado_Rechazo_Obs='',_01_T03Aprobado_Rechazo_Obs='',_01_T03Envio_IDEnviado='',_01_T03Envio_FechaEnviado='',_01_T03Envio_HoraEnviado='',_01_T03Envio_Observaciones='',_01_T03Confirmado_Fecha='',_01_T03Confirmado_Hora='',_01_T03Confirmado_IDConfirmado='',_01_T03Confirmado_Rechazo_Obs=''"


            Dim bSqlInsert As Boolean = clsAdminDb.sql_sConsulta("UPDATE _01_T03REGISTROHORAS SET _01_T03Estado='Enviado',_01_T03Envio_FechaEnviado='" & txt_01_T23Fecha.Text & "',_01_T03Envio_HoraEnviado='" & Now.Hour & ":" & Now.Minute & "',_01_T03Envio_IDEnviado='" & txt_01_T23Codigo.Text & "',_01_T03Envio_Observaciones='" & txtReenviado_Observacion.Text & "',_01_T03Tiempo='" & drop_Tiempo_Reenviar.Text & "',_01_T03TiempoaHoras='" & txt_01_T03TiempoaHoras_TEm & "',_01_T03ActividadesDescripcion ='" & drop_Actividades_Reenviar.SelectedItem.Text & "',_01_T03ActividadesCodigo ='" & drop_Actividades_Reenviar.Text & "',_01_T03ODS ='" & drop_Proyecto_Reenviar.Text & "',_01_T03Contrato ='" & drop_Contrato_Reenviar.Text & "',_01_T03Cliente ='" & drop_Cliente_Reenviar.Text & "',_01_T03Entregable='" & drop_Entregable_Reenviar.Text & "',_01_T03EntregableSeringtec='" & drop_Entregable_ReenviarSeringtec.Text & "',_01_T03Version='" & drop_Version_Reenviar.Text & "',_01_T03Consecutivo='" & sConsecutivo & "',_01_T03Aprobado_Rechazo_Obs='',_01_T03Confirmado_Rechazo_Obs='' from _01_T22AGNOS, _01_T03REGISTROHORAS where " & sLlsavesTem)
            'Dim bSqlInsert As Boolean = clsAdminDb.sql_sConsulta("UPDATE _01_T03REGISTROHORAS SET _01_T03Estado='Enviado',_01_T03Envio_FechaEnviado='" & txt_01_T23Fecha.Text & "',_01_T03Envio_HoraEnviado='" & Now.Hour & ":" & Now.Minute & "',_01_T03Envio_IDEnviado='" & txt_01_T23Codigo.Text & "',_01_T03Envio_Observaciones='" & txtReenviado_Observacion.Text & "',_01_T03Tiempo='" & drop_Tiempo_Reenviar.Text & "',_01_T03ActividadesDescripcion ='" & drop_Actividades_Reenviar.SelectedItem.Text & "',_01_T03ActividadesCodigo ='" & drop_Actividades_Reenviar.Text & "',_01_T03ODS ='" & drop_Proyecto_Reenviar.Text & "',_01_T03Contrato ='" & drop_Contrato_Reenviar.Text & "',_01_T03Cliente ='" & drop_Cliente_Reenviar.Text & "',_01_T03Entregable='" & drop_Entregable_Reenviar.Text & "',_01_T03EntregableSeringtec='" & drop_Entregable_ReenviarSeringtec.Text & "',_01_T03Version='" & drop_Version_Reenviar.Text & "',_01_T03Consecutivo='" & sConsecutivo & "',_01_T03Aprobado_Rechazo_Obs='',_01_T03Confirmado_Rechazo_Obs=''   from _01_T22AGNOS, _01_T03REGISTROHORAS where " & sLlsavesTem)
            'Dim bSqlInsert As Boolean = clsAdminDb.sql_sConsulta("UPDATE _01_T03REGISTROHORAS SET _01_T03Estado='" & sRechazoAprobado & "',_01_T03Aprobado_Fecha='" & txt_01_T23Fecha.Text & "',_01_T03Aprobado_Hora='" & Now.Hour & ":" & Now.Minute & "',_01_T03Aprobado_IDAprobacion='" & txt_01_T23Codigo.Text & "' from _01_T22AGNOS, _01_T03REGISTROHORAS where " & sLlsavesTem)
            If bSqlInsert = True Then
                If sRechazoAprobado = "Rechazado" Then
                    MostrarMensaje("Se RECHAZO Correctamente el Tiempo ", True)
                Else
                    MostrarMensaje("Se Enviado Correctamente el Tiempo ", True)
                End If

                Guardar_ActulizarRegistroHoras = True
            Else

                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If

        ElseIf sGuardarActualizar_Table <> "" Then
            sLlsavesTem = sLlsavesTem & " and " & sGuardarActualizar_Table
            sLlsavesTem = sLlsavesTem & " and " & _01_T03REGISTROHORAS.Campo_01_T03Estado & "='Enviado'"
            Dim bSqlInsert As Boolean = clsAdminDb.sql_sConsulta("UPDATE _01_T03REGISTROHORAS SET _01_T03Estado='" & sRechazoAprobado & "',_01_T03Aprobado_Fecha='" & txt_01_T23Fecha.Text & "',_01_T03Aprobado_Hora='" & Now.Hour & ":" & Now.Minute & "',_01_T03Aprobado_IDAprobacion='" & txt_01_T23Codigo.Text & "' from _01_T22AGNOS, _01_T03REGISTROHORAS where " & sLlsavesTem)
            If bSqlInsert = True Then
                If sRechazoAprobado = "Rechazado" Then
                    MostrarMensaje("Se RECHAZO Correctamente el Tiempo ", True)
                Else
                    MostrarMensaje("Se APROBO Correctamente el Tiempo ", True)
                End If

                Guardar_ActulizarRegistroHoras = True
            Else

                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If

        End If
        Guardar_Transaccion(sCod_Aplicacion, sCodModulo, lblUsuarioLogueado_Documento.Text, lblUsuarioLogueado_Nombre.Text, "02", clsAdminDb.Mostrar_Consulta)

    End Function

    Private Function bValidar_Multiple_Ver(Optional ByVal sNumRecurso As String = "", Optional ByVal sRechazoAprobado As String = "") As Boolean
        bValidar_Multiple_Ver = False
        Dim bGuardar_FactTem As Boolean = False
        Dim FilaHtml = New HtmlTableRow
        Dim sNumRecurso_TEm As String = ""
        Dim bSinChek As Boolean = False
        For Each FilaHtml In Tbody2.Controls
            Dim sObjeto1 As New Object
            Dim sObjeto As New Object
            Dim lbl_Funcionrio As New Label
            Dim drop_Perfil_Tem As New DropDownList
            Dim txt_FechaFin_Tem As New TextBox
            Dim txt_Dedicacion_Tem As New TextBox
            Dim chk As New CheckBox
            chk = FilaHtml.Controls(5).Controls(0)

            If chk.Checked = True And sNumRecurso = "" Then
                bValidar_Multiple_Ver = True
                bSinChek = True
            Else

            End If

        Next
        If bSinChek = False Then
            lblMensajeVerPopup.Text = "Por  favor checkear un registro para realizar este proceso"
            MostrarMensaje("Por  favor checkear un registro para realizar este proceso")
        End If

    End Function

    Private Sub Mostrar_panel_IdAprobacion(ByVal bMostrar As Boolean)
        panel_IdAprobacion.Visible = bMostrar
        btnImprimir.Visible = bMostrar
        If bMostrar = False Then
            txt_IdAprobacion.Text = ""
            txt_IdAprobacionTemporal.Text = ""
        End If
    End Sub

    Function Guardar_Registro_Envio() As Boolean
        Guardar_Registro_Envio = False
        Mostrar_panel_IdAprobacion(False)
        txt_IdAprobacion.Text = ""
        txt_IdAprobacionTemporal.Text = ""

        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0
        Dim sLlaves_TEm = _01_T03_1REGISTROHORAS_ENVIO.CampoLlave_01_T03_1NumDocAprobador & "=  '" & lblUsuarioLogueado_Documento.Text & "'" & " AND " & _01_T03_1REGISTROHORAS_ENVIO.CampoLlave_01_T03_1Disciplina & "=  '" & drop_01_T03Disciplina.Text & "'" & " AND " & _01_T03_1REGISTROHORAS_ENVIO.CampoLlave_01_T03_1Fecha & "=  '" & txt_01_T23Fecha.Text & "'" & " AND " & _01_T03_1REGISTROHORAS_ENVIO.CampoLlave_01_T03_1Consecutivo & "=  '" & txt_01_T23Consecutivo.Text & "'" '& " AND " & _01_T03_1REGISTROHORAS_ENVIO.CampoLlave_01_T03_1Gestion & "=  '""'"
        Dim sCamposINS_TEm = "'" & clsAdminDb.sRemoverHTML(lblUsuarioLogueado_Documento.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Disciplina.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Consecutivo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Codigo.Text) & "'" & "," & "'" & drop_01_T03Cliente.Text & "'" & "," & "'" & drop_01_T03Contrato.Text & "'" & "," & "'" & drop_01_T03ODS.Text & "'" & "," & "'""'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_Observaciones_Ver.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(Now.Hour & ":" & Now.Minute) & "'"
        Dim sCamposUPD_TEm = _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Codigo & "='" & clsAdminDb.sRemoverHTML(txt_01_T23Codigo.Text) & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Cliente & "='" & drop_01_T03Cliente.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Contrato & "='" & drop_01_T03Contrato.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1ODS & "='" & drop_01_T03ODS.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Observaciones & "='" & clsAdminDb.sRemoverHTML(txt_Observaciones_Ver.Text) & "'"
        Dim sCamposTabla_Tem = _01_T03_1REGISTROHORAS_ENVIO.CamposTabla
        lCantRegistros = clsAdminDb.sql_Count(_01_T03_1REGISTROHORAS_ENVIO.NombreTabla, sLlaves_TEm)
        If lCantRegistros = 0 Then


            txt_01_T23Consecutivo.Text = iConsecutivo_Envio()
            txt_01_T23Codigo.Text = lblUsuarioLogueado_Documento.Text & "-.-" & lblUsuarioLogueado_Disciplina.Text & "-.-" & txt_01_T23Fecha.Text & "-.-" & txt_01_T23Consecutivo.Text


            sCamposINS_TEm = "'" & clsAdminDb.sRemoverHTML(lblUsuarioLogueado_Documento.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(lblUsuarioLogueado_Disciplina.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Consecutivo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Codigo.Text) & "'" & "," & "'" & drop_01_T03Cliente.Text & "'" & "," & "'" & drop_01_T03Contrato.Text & "'" & "," & "'" & drop_01_T03ODS.Text & "'" & "," & "'""'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_Observaciones_Ver.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(Now.Hour & ":" & Now.Minute) & "'"
            sCamposUPD_TEm = _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Codigo & "='" & clsAdminDb.sRemoverHTML(txt_01_T23Codigo.Text) & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Cliente & "='" & drop_01_T03Cliente.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Contrato & "='" & drop_01_T03Contrato.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1ODS & "='" & drop_01_T03ODS.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Observaciones & "='" & clsAdminDb.sRemoverHTML(txt_Observaciones_Ver.Text) & "'"

            'Datos_Modulo()
            bSqlInsert = clsAdminDb.sql_Insert(_01_T03_1REGISTROHORAS_ENVIO.NombreTabla, sCamposTabla_Tem, sCamposINS_TEm)
            If bSqlInsert = True Then
                MostrarMensaje("Se inserto nuevo registro correctamente ", True)
                Guardar_Transaccion(sCod_Aplicacion, sCodModulo, lblUsuarioLogueado_Documento.Text, lblUsuarioLogueado_Nombre.Text, "01", clsAdminDb.Mostrar_Consulta)
                'Registro_Procesos("Guardar", clsAdminDb.Mostrar_Consulta)
                Guardar_Registro_Envio = True
                Mostrar_panel_IdAprobacion(True)
                Dim TestArray() As String = Split(txt_01_T23Codigo.Text, "-.-")
                txt_IdAprobacionTemporal.Text = TestArray(1).ToString & "-" & TestArray(2).ToString & "-" & TestArray(3).ToString
                txt_IdAprobacion.Text = txt_01_T23Codigo.Text

            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else

            sCamposINS_TEm = "'" & clsAdminDb.sRemoverHTML(lblUsuarioLogueado_Documento.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Disciplina.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Consecutivo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T23Codigo.Text) & "'" & "," & "'" & drop_01_T03Cliente.Text & "'" & "," & "'" & drop_01_T03Contrato.Text & "'" & "," & "'" & drop_01_T03ODS.Text & "'" & "," & "'""'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_Observaciones_Ver.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(Now.Hour & ":" & Now.Minute) & "'"
            sCamposUPD_TEm = _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Codigo & "='" & clsAdminDb.sRemoverHTML(txt_01_T23Codigo.Text) & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Cliente & "='" & drop_01_T03Cliente.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Contrato & "='" & drop_01_T03Contrato.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1ODS & "='" & drop_01_T03ODS.Text & "'" & "," & _01_T03_1REGISTROHORAS_ENVIO.Campo_01_T03_1Observaciones & "='" & clsAdminDb.sRemoverHTML(txt_Observaciones_Ver.Text) & "'"


            bSqlInsert = clsAdminDb.sql_Update(_01_T03_1REGISTROHORAS_ENVIO.NombreTabla, sCamposUPD_TEm, sLlaves_TEm)
            If bSqlInsert = True Then
                Guardar_Transaccion(sCod_Aplicacion, sCodModulo, lblUsuarioLogueado_Documento.Text, lblUsuarioLogueado_Nombre.Text, "02", clsAdminDb.Mostrar_Consulta)
                MostrarMensaje("Se actualizo correctamente este registro", True)
                'Registro_Procesos("Actualizar", clsAdminDb.Mostrar_Consulta)
                Guardar_Registro_Envio = True
                Mostrar_panel_IdAprobacion(True)
                txt_IdAprobacion.Text = txt_01_T23Codigo.Text
                txt_IdAprobacionTemporal.Text = txt_01_T23Codigo.Text
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If
        End If
    End Function

    Function iConsecutivo_Envio() As Integer
        iConsecutivo_Envio = 0
        Dim iCons As Integer = 0
        Dim lCantidad As Integer = 0
        iCons = clsAdminDb.sql_Max(_01_T03_1REGISTROHORAS_ENVIO.NombreTabla, "cast(" & _01_T03_1REGISTROHORAS_ENVIO.CampoLlave_01_T03_1Consecutivo & " as bigint) ", _01_T03_1REGISTROHORAS_ENVIO.CampoLlave_01_T03_1NumDocAprobador & "=  '" & lblUsuarioLogueado_Documento.Text & "'" & " AND " & _01_T03_1REGISTROHORAS_ENVIO.CampoLlave_01_T03_1Fecha & "='" & txt_01_T23Fecha.Text & "'")
        'iCons = clsAdminDb.sql_Max(_04_T49FACTURACIONDETALLE.NombreTabla, "cast(" & _04_T49FACTURACIONDETALLE.CampoLlave_04_T49Consecutivo & " as bigint) ", _04_T49FACTURACIONDETALLE.CampoLlave_04_T49TipoServidor & "=  '" & drop_04_T44TipoServidor.Text & "'" & " AND " & _04_T49FACTURACIONDETALLE.CampoLlave_04_T49CodEntidadPrestadora & "=  '" & drop_07_Cont_T10EmpresaContable.Text & "'" & " AND " & _04_T49FACTURACIONDETALLE.Campo_04_T49TarifaConvenioCto & "=  '" & drop_04_T49Servicios.Text & "'")
        If iCons = 0 Then
            iConsecutivo_Envio = 1
        Else
            iConsecutivo_Envio = iCons + 1
        End If
    End Function

    Private Sub DropODS_Ver_TextChanged(sender As Object, e As EventArgs) Handles DropODS_Ver.TextChanged
        ModalPopupVer.Show()
        cargar_Tabla_ver()
    End Sub


    Private Function Suma_Horas() As String
        Suma_Horas = "0:0"
        'Dim clsAdminDb As New adminitradorDB
        'btnGuardar.Visible = False
        'Tbody1.Controls.Clear()
        Dim coleccionDatosPlural As New Collection
        Dim sHoras As String = "sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo)))/60/60 as horas"
        Dim sMinutes As String = "(sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo))) - (sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo)))/60/60*60*60))/60 as minutos"
        Dim sSegundos As String = "sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo))) - (sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo)))/60/60*60*60) - ((sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo))) - (sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo)))/60/60*60*60))/60*60) as segundos"
        Dim sTotalSegundos As String = "sum((DATEPART(hh, CONVERT(time, _01_T03tiempo))*60 + DATEPART(mi, CONVERT(time, _01_T03tiempo)))*60+ DATEPART(SS, CONVERT(time, _01_T03tiempo))) as totalensegundos"

        Dim sCamposTablaLocal As String = ""
        sCamposTablaLocal = sHoras & "," & sMinutes & "," & sSegundos & "," & sTotalSegundos
        Dim sLlavesLocal As String = _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "='" & lblUsuarioLogueado_Documento.Text & "'  "
        'sLlavesLocal = sLlavesLocal & " and " & _01_T03REGISTROHORAS.Campo_01_T03Disciplina & "=" & _01_T06DISCIPLINAS.CampoLlave_01_T06Codigo
        'sLlavesLocal = sLlavesLocal & " and " & _01_T03REGISTROHORAS.Campo_01_T03Disciplina & "=" & _01_T21LISTADODOCENTREGABLES.CampoLlave_01_T21Categoria
        'sLlavesLocal = sLlavesLocal & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Entregable & "=" & _01_T21LISTADODOCENTREGABLES.Campo_01_T21CodigoEcopetrol
        'sLlavesLocal = sLlavesLocal & " and " & _01_T03REGISTROHORAS.CampoLlave_01_T03Usuario & "='" & lblUsuarioLogueado_Documento.Text & "'"
        If drop_01_T03Fecha.Text <> "" Then

            sLlavesLocal = sLlavesLocal & " and _01_T03Fecha='" & drop_01_T03Fecha.Text & "'"

        Else
            txt_Cons_Registradas.Text = ""
            txt_Cons_Normales.Text = ""
            txt_Cons_Fecha.Text = ""
            txt_Cons_SobreRecargadas.Text = ""
            txt_Cons_Aprobador.Text = ""
            Exit Function
            sLlavesLocal = sLlavesLocal & " and _01_T03Fecha='" & txt_01_T23Fecha.Text & "'"
        End If
        If drop_01_T03Disciplina.Text <> "" Then
            sLlavesLocal = sLlavesLocal & " and _01_T03Disciplina='" & drop_01_T03Disciplina.Text & "'"
        End If


        Dim sTablaLocal As String = _01_T03REGISTROHORAS.NombreTabla '& "," & _01_T09ODS.NombreTabla & "," & _01_T21LISTADODOCENTREGABLES.NombreTabla
        Dim ArregloSingular() As String


        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal
        Else
        End If
        Dim i As Integer = 1
        Dim lbl_Tem2 As Label
        Dim dTotaloras As TimeSpan = TimeSpan.Parse("0:0")
        Dim iHorasEnviadas As Integer = 0
        Dim iHoraPorEnviar As Integer = 0

        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                Try
                    If ArregloSingular(0).ToString <> "" Then
                        Dim sTiempo As String = ArregloSingular(0).ToString & ":" & ArregloSingular(1).ToString
                        Dim dTotaloras2 As TimeSpan = TimeSpan.Parse(sTiempo)
                        dTotaloras = dTotaloras + dTotaloras2
                        Suma_Horas = dTotaloras.ToString
                        txt_Cons_Registradas.Text = dTotaloras2.ToString
                        If dTotaloras.TotalHours <= 9 Then
                            txt_Cons_Normales.Text = txt_Cons_Registradas.Text
                            'txt_Cons_Recargadas.Text = "0"
                            txt_Cons_SobreRecargadas.Text = "0"
                        Else
                            txt_Cons_Normales.Text = "9"
                            If dTotaloras.TotalHours > 9 Then
                                txt_Cons_Normales.Text = "9" 'dTotaloras - txt_Cons_Normales.Text
                                txt_Cons_SobreRecargadas.Text = dTotaloras.TotalHours - txt_Cons_Normales.Text

                                'txt_Cons_Recargadas.Text = dTotaloras - 8
                                'txt_Cons_SobreRecargadas.Text = "0"
                            Else
                                'txt_Cons_Recargadas.Text = dTotaloras - txt_Cons_Normales.Text
                                txt_Cons_SobreRecargadas.Text = "0"

                            End If
                            txt_Cons_Fecha.Text = CDate(drop_01_T03Fecha.Text).ToLongDateString

                        End If
                    Else
                        txt_Cons_Registradas.Text = ""
                        txt_Cons_Normales.Text = ""
                        txt_Cons_Fecha.Text = ""
                        txt_Cons_SobreRecargadas.Text = ""
                        txt_Cons_Aprobador.Text = ""
                    End If
                Catch ex As Exception

                End Try

            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
    End Function

    Private Sub RegistrodeTiempoVer_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Suma_Horas()
    End Sub

    Private Sub btnCancelar_Ver_Click(sender As Object, e As EventArgs) Handles btnCancelar_Ver.Click
        DropODS_Ver.Text = ""
    End Sub

    Private Sub linkbutonHtml_Eliminar_Editar_Click(sender As Object, e As EventArgs) Handles linkbutonHtml_Eliminar_Editar.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T01Descripcion,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '

        Dim ___01_T03Contrato As String = TestArray(1).ToString ' 
        Dim ___01_T03ODS As String = TestArray(2).ToString ' 
        Dim ___01_T03Disciplina As String = TestArray(3).ToString ' 
        Dim ___01_T03Entregable As String = TestArray(4).ToString ' 
        Dim ___01_T03Fecha As String = TestArray(5).ToString ' 
        Dim ___01_T03Consecutivo As String = TestArray(6).ToString ' 
        Dim ___01_T03Usuario As String = TestArray(7).ToString ' 
        Dim ___01_T03Version As String = TestArray(8).ToString ' 
        Dim ___01_T03Tiempo As String = Replace(TestArray(9).ToString, "-ampm-", ":") '
        Dim ___01_T03Entregable2 As String = TestArray(10).ToString ' 
        sLlaves = "_01_T03Contrato='" & ___01_T03Contrato & "'"
        sLlaves = sLlaves & " and _01_T03ODS='" & ___01_T03ODS & "'"
        sLlaves = sLlaves & " and _01_T03Disciplina='" & ___01_T03Disciplina & "'"
        sLlaves = sLlaves & " and REPLACE(REPLACE(REPLACE(_01_T03ActividadesCodigo,CHAR(9),''),CHAR(10),''),CHAR(13),'')='" & ___01_T03Entregable & "'"
        sLlaves = sLlaves & " and _01_T03Fecha='" & ___01_T03Fecha & "'"
        sLlaves = sLlaves & " and _01_T03Consecutivo='" & ___01_T03Consecutivo & "'"
        sLlaves = sLlaves & " and _01_T03DocFuncionario='" & ___01_T03Usuario & "'"
        ModalPopupReenviar.Show()
        txt_Observaciones_Reenviar.Text = ""
        Dim bContinuarTEm As Boolean = True
        Cargar_Registro_Reenviar()
        If bContinuarTEm = False Then
            ModalPopupReenviar.Hide()
            Exit Sub
        End If
        ModalPopupReenviar.Show()
        txt_Observaciones_Reenviar.Text = ""
        Cargar_Registro_Reenviar()

        'drop_Proyecto_Reenviar.Text = ___01_T03ODS
        'drop_Proyecto_Reenviar_TextChanged(Nothing, Nothing)
        'drop_Entregable_Reenviar.Text = ___01_T03Entregable
        'drop_Entregable_Reenviar_TextChanged(Nothing, Nothing)
        'drop_Entregable_ReenviarSeringtec.Text = __
        'drop_Actividades_Reenviar.Text = ___01_T03Entregable
        Try
            drop_Version_Reenviar.Text = ___01_T03Version
        Catch ex As Exception

        End Try

        drop_Tiempo_Reenviar.Text = ___01_T03Tiempo


        cargar_Tabla()
    End Sub

    Private Sub btnAprobar_Reenviar_Click(sender As Object, e As EventArgs) Handles btnAprobar_Reenviar.Click
        ModalPopupReenviar.Show()
        'If bValidarCampos(True) = False Then Exit Sub
        If bValidarSQL() = False Then Exit Sub

        If txtReenviado_Consecutivo.Text = "" Or txtReenviado_Cliente.Text = "" Or txtReenviado_Contrato.Text = "" Or txtReenviado_Ods.Text = "" Or txtReenviado_Entregable.Text = "" Then
            lblMensajeReenviarPopup.Text = "por Favor rgue nuevamente este registro, no se cargaron los IDs correctamente"
            Exit Sub
        End If


        If drop_Tiempo_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione una hora"
            Exit Sub
        End If
        If drop_Entregable_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione un Entregable Cliente"
            Exit Sub
        End If
        If drop_Actividades_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione Actividad"
            Exit Sub
        End If
        If drop_Entregable_ReenviarSeringtec.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione un Entregable Seringtec"
            Exit Sub
        End If

        If drop_Version_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione una Version"
            Exit Sub
        End If
        If drop_Proyecto_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione un Proyecto"
            Exit Sub
        End If
        If drop_Cliente_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione un Cliente -Error Soporte"
            Exit Sub
        End If
        If drop_Contrato_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione un Contrato -Error Soporte"
            Exit Sub
        End If
        If drop_Version_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor seleccione Version"
            Exit Sub
        End If




        If txt_Observaciones_Reenviar.Text = "" Then
            lblMensajeReenviarPopup.Text = "Por favor Digite una Observacion para el reenvio de las horas"
            Exit Sub
        End If

        Dim dFecha As Date
        Dim sFecha As String = ""
        If bValidarFecha(dFecha, Now.Day, Now.Month, Now.Year, sFecha) = True Then
        Else
        End If

        Dim bSqlInsert As Boolean = False

        Dim iConsecutivoTEm As Integer = iConsecutivoRenvio()

        Dim sCodModulo_Tem As String = _01_T03_2REGISTROHORAS_HISTORIAL.CodigoModulo
        Dim sNombreTabla_Tem As String = _01_T03_2REGISTROHORAS_HISTORIAL.NombreTabla
        Dim sCamposTabla_Tem As String = _01_T03_2REGISTROHORAS_HISTORIAL.CamposTabla
        Dim sCamposINS_Tem As String = "'" & clsAdminDb.sRemoverHTML(lblUsuarioLogueado_Documento.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Fecha.Text) & "'" & "," & "'" & drop_01_T03Contrato.Text & "'" & "," & "'" & drop_01_T03ODS.Text & "'" & "," & "'" & drop_01_T03Entregable.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Consecutivo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Version.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Horas.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03DescripcionAcguividad.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03TiempoaHoras.Text) & "'" & "," & "'Rechazado'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Hora.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Observacion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03Aprobado_Observacion_Rechazo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03EnviadoAProbacion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T03Disciplina.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T03HorasSolo.Text) & "'" & "," & "'" & txt_01_T03MinutoSolo.Text & "'" & "," & "'" & drop_01_T03Cliente.Text & "','','','','','','','','','" & drop_01_T03Actividad.Text & "','" & txt_01_T03Actividad_Texto.Text & "','" & sFecha & "','" & Now.Hour & "','" & iConsecutivoTEm & "','" & txt_Observaciones_Reenviar.Text & "'"
        Dim sCamposUPD_Tem As String = ""
        Dim sLlaves_Tem As String = _01_T03_2REGISTROHORAS_HISTORIAL.CampoLlave_01_T03_2Usuario & "=  '" & lblUsuarioLogueado_Documento.Text & "'"
        Dim sConsulta As String = ""

        sConsulta = " INSERT INTO _01_T03_2REGISTROHORAS_HISTORIAL "
        sConsulta = sConsulta & " Select  _01_T03DocFuncionario,_01_T03Fecha,_01_T03Contrato,_01_T03ODS,_01_T03Entregable,_01_T03Consecutivo,_01_T03Version,_01_T03Tiempo,_01_T03DescripcionActividad,_01_T03TiempoaHoras,_01_T03Estado,_01_T03Aprobado_Fecha,_01_T03Aprobado_Hora,_01_T03Aprobado_IDAprobacion,_01_T03Aprobado_Rechazo_Obs,_01_T03EnviadoAProbacion,_01_T03Disciplina,_01_T03Horas,_01_T03Minutos,_01_T03Cliente,_01_T03Envio_IDEnviado,_01_T03Envio_FechaEnviado,_01_T03Envio_HoraEnviado,_01_T03Envio_Observaciones,_01_T03Confirmado_Fecha,_01_T03Confirmado_Hora,_01_T03Confirmado_IDConfirmado,_01_T03Confirmado_Rechazo_Obs,_01_T03ActividadesCodigo,_01_T03ActividadesDescripcion,'" & sFecha & "','" & Now.Hour & ":" & Now.Minute & "','" & iConsecutivoTEm & "','" & txt_Observaciones_Reenviar.Text & "' FROM _01_T03REGISTROHORAS "
        sConsulta = sConsulta & " WHERE _01_T03DocFuncionario ='" & lblUsuarioLogueado_Documento.Text & "' "
        sConsulta = sConsulta & " And _01_T03Fecha ='" & drop_01_T03Fecha.Text & "'"
        sConsulta = sConsulta & " And _01_T03Contrato ='" & drop_01_T03Contrato.Text & "'"
        sConsulta = sConsulta & " And _01_T03ODS ='" & drop_01_T03ODS.Text & "'"
        sConsulta = sConsulta & " And _01_T03ActividadesCodigo ='" & drop_01_T03Actividad.Text & "'"
        sConsulta = sConsulta & " And _01_T03Consecutivo ='" & txt_01_T03Consecutivo.Text & "'"
        sConsulta = sConsulta & " And _01_T03Cliente ='" & drop_01_T03Cliente.Text & "'"

        bSqlInsert = clsAdminDb.sql_sConsulta(sConsulta)
        If bSqlInsert = True Then
            ModalPopupReenviar.Hide()
            'Dim sConsecutivo = iConsecutivo_ReenvioTEm()
            'sConsulta = "update _01_T03REGISTROHORAS set _01_T03Estado='Enviado',_01_T03ActividadesDescripcion ='" & drop_Actividades_Reenviar.SelectedItem.Text & "',_01_T03ActividadesCodigo ='" & drop_Actividades_Reenviar.Text & "',_01_T03ODS ='" & drop_Proyecto_Reenviar.Text & "',_01_T03Contrato ='" & drop_Contrato_Reenviar.Text & "',_01_T03Cliente ='" & drop_Cliente_Reenviar.Text & "',_01_T03Consecutivo='" & sConsecutivo & "',_01_T03Aprobado_Fecha='',_01_T03Aprobado_Hora='',_01_T03Aprobado_IDAprobacion='',_01_T03Aprobado_Rechazo_Obs='',_01_T03Aprobado_Rechazo_Obs='',_01_T03Envio_IDEnviado='',_01_T03Envio_FechaEnviado='',_01_T03Envio_HoraEnviado='',_01_T03Envio_Observaciones='',_01_T03Confirmado_Fecha='',_01_T03Confirmado_Hora='',_01_T03Confirmado_IDConfirmado='',_01_T03Confirmado_Rechazo_Obs=''"

            If Guardar_Registro_Envio() = True Then
                sLlaves_Tem = " _01_T03DocFuncionario ='" & lblUsuarioLogueado_Documento.Text & "' "
                sLlaves_Tem = sLlaves_Tem & " And _01_T03Fecha ='" & txtReenviado_FEchaId.Text & "'"
                sLlaves_Tem = sLlaves_Tem & " And _01_T03Contrato ='" & txtReenviado_Contrato.Text & "'"
                sLlaves_Tem = sLlaves_Tem & " And _01_T03ODS ='" & txtReenviado_Ods.Text & "'"
                sLlaves_Tem = sLlaves_Tem & " And _01_T03ActividadesCodigo ='" & txtReenviado_Entregable.Text & "'"
                sLlaves_Tem = sLlaves_Tem & " And _01_T03Consecutivo ='" & txtReenviado_Consecutivo.Text & "'"
                sLlaves_Tem = sLlaves_Tem & " And _01_T03Cliente ='" & txtReenviado_Cliente.Text & "'"



                Dim Guardar_Multiple_Ver As Boolean = Guardar_ActulizarRegistroHoras("", "", "", "Enviado", sLlaves_Tem)

                If Guardar_Multiple_Ver = True Then
                    txt_01_T23Consecutivo.Text = ""
                    MostrarMensaje("Se ha Reenviado Correctamente estas Horas para Aprobacion", True)
                    'txt_Observaciones.Text = ""
                    txt_01_T23Codigo.Text = ""
                    txt_Observaciones_Ver.Text = ""
                    txt_Popup_Numdoc_Ver.Text = ""
                    txt_Popup_Numdoc_Ver.Text = ""
                    drop_01_T03ODS.Text = ""
                    Try
                        drop_01_T03Entregable.Text = ""
                    Catch ex As Exception

                    End Try

                    drop_01_T03Version.Text = ""
                    drop_01_T03Fecha.Text = ""
                    Drop_01_T03Horas.Text = ""
                    txt_01_T03Observaciones.Text = ""
                    bHabilitarControles(True, False, False, False, False, False, False, False)
                    ModalPopupVer.Hide()
                    cargar_Tabla()
                End If
            Else
                txt_01_T23Consecutivo.Text = ""
                'txt_Observaciones.Text = ""
                txt_01_T23Codigo.Text = ""

            End If


        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
        filtro()
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

    Private Sub drop_Actividades_Reenviar_TextChanged(sender As Object, e As EventArgs) Handles drop_Actividades_Reenviar.TextChanged
        ModalPopupReenviar.Show()
    End Sub

    Private Sub drop_Entregable_Reenviar_TextChanged(sender As Object, e As EventArgs) Handles drop_Entregable_Reenviar.TextChanged
        ModalPopupReenviar.Show()

        drop_Tiempo_Reenviar.Text = ""
        Cargar_Version_Reenviar()
        If drop_01_T03Entregable.Text <> "" Then
            bHabilitarControles(True, True, True, False, False, False, False, False)
        Else
            bHabilitarControles(True, True, False, False, False, False, False, False)
        End If
        Cargar_Actividades_Reenviar()

    End Sub

    Private Sub drop_Proyecto_Reenviar_TextChanged(sender As Object, e As EventArgs) Handles drop_Proyecto_Reenviar.TextChanged
        ModalPopupReenviar.Show()
        drop_Actividades_Reenviar.Items.Clear()
        drop_Actividades_Reenviar.Items.Add(New ListItem("Seleccionar", ""))
        drop_Entregable_Reenviar.Items.Clear()
        drop_Entregable_Reenviar.Items.Add(New ListItem("Seleccionar", ""))
        drop_Version_Reenviar.Items.Clear()


        Dim coleccionDatos As Object
        Dim sAprobador As String = "(select top 1 _01_T12NombreApellidos  from _01_T12FUNCIONARIOS where _01_T12NumDoc=_01_T09DocumentoCoordinador)"
        coleccionDatos = clsAdminDb.sql_Select(_01_T09ODS.NombreTabla, _01_T09ODS.CampoLlave_01_T09Cliente & "," & _01_T09ODS.CampoLlave_01_T09ContratoMArco & "," & sAprobador, _01_T09ODS.Campo_01_T09Codigo & "='" & drop_Proyecto_Reenviar.Text & "'")
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
            Else
                'drop_01_T10Cliente.Text = coleccionDatos(0)
                cargar_Cliente_Reenviar(coleccionDatos(0))
                cargar_Contrato_Reenviar(coleccionDatos(0), coleccionDatos(1))
                Cargar_Disciplina_Reenviar()

            End If
        Else
        End If
    End Sub

    Private Sub drop_Version_Reenviar_TextChanged(sender As Object, e As EventArgs) Handles drop_Version_Reenviar.TextChanged
        'drop_01_T03Fecha.Text = ""
        'drop_Tiempo_Reenviar.Text = ""
        If drop_Version_Reenviar.Text <> "" Then

        Else

        End If
    End Sub

    Private Sub ImageButton_EntreableEco_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton_EntreableEco.Click
        ModalPopupBuscarEntregable.Show()
        txt_ODS_BusquedaEntregable.Text = drop_01_T03ODS.Text
        lbl_Popup_Nombre_ODS_BusquedaEntregable.Text = "Proyecto: " & drop_01_T03ODS.Text
    End Sub
    Private Sub ImageButton_EntreableSering_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton_EntreableSering.Click
        ModalPopupBuscarEntregable.Show()
        txt_ODS_BusquedaEntregable.Text = drop_01_T03ODS.Text
        lbl_Popup_Nombre_ODS_BusquedaEntregable.Text = "Proyecto: " & drop_01_T03ODS.Text
    End Sub


    Private Sub cargar_Tabla_BusquedaEntregable(Optional ByVal sLlaveTem As String = "")
        'Dim clsAdminDb As New adminitradorDB
        If txt_ODS_BusquedaEntregable.Text = "" Then
            Exit Sub
        End If
        Dim coleccionDatosPlural As New Collection
        Dim sGroupby As String = "_01_T21CodigoEcopetrol,_01_T21CodigoSeringtec,_01_T21Nombre,_01_T21ODS"
        Dim sCamposTablaLocal As String = "_01_T21CodigoEcopetrol,_01_T21CodigoSeringtec,_01_T21Nombre,_01_T21ODS" '
        Dim sLlavesLocal As String = "_01_T21ODS='" & txt_ODS_BusquedaEntregable.Text & "'"
        'sLlavesLocal = sLlavesLocal & " and _01_T03Contrato='" & drop_01_T03Contrato.Text & "'"
        If Trim(txt_CodigoBusquedaEntregable.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " and _01_T21CodigoEcopetrol like '%" & txt_CodigoBusquedaEntregable.Text & "%'"
        End If
        If Trim(txt_CodigoBusquedaEntregableSeringtec.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " and _01_T21CodigoSeringtec like '%" & txt_CodigoBusquedaEntregableSeringtec.Text & "%'"
        End If

        If Trim(txt_NombreEntegable.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " and _01_T21Nombre like '%" & txt_NombreEntegable.Text & "%'"
        End If



        Dim sTablaLocal As String = _01_T21LISTADODOCENTREGABLES.NombreTabla
        'Dim sTablaLocal As String = _01_T03REGISTROHORAS.NombreTabla & "," & _01_T06DISCIPLINAS.NombreTabla & "," & _01_T09ODS.NombreTabla & "," & _01_T21LISTADODOCENTREGABLES.NombreTabla & "," & _01_T22AGNOS.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
        Dim ArregloSingular() As String
        TbodyBusquedaEntrgable.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
        End If
        Dim i As Integer = 1
        'Dim chk_Tem As CheckBox
        Dim llTem1 As Label
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal,,)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow

                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"
                llTem1 = New Label
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "yyuC11-sep-" '& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(0)
                llTem1.Width = Unit.Pixel(100)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"

                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"
                llTem1 = New Label
                llTem1.Width = Unit.Pixel(100)
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "yBByuC12-sep-" '& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(1)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"
                llTem1 = New Label
                llTem1.Width = Unit.Percentage(100)
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "yBByuC13-sep-" '& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(2)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"
                linkbutonHtml_CodigoEntregale_Busqueda = New LinkButton
                linkbutonHtml_CodigoEntregale_Busqueda.Width = Unit.Pixel(100)
                linkbutonHtml_CodigoEntregale_Busqueda.ID = i & "kkcdvvwbggm113-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString ' & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar)
                'linkbutonHtml_Eliminar.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_CodigoEntregale_Busqueda.ForeColor = Drawing.Color.DarkRed
                linkbutonHtml_CodigoEntregale_Busqueda.Font.Bold = True
                linkbutonHtml_CodigoEntregale_Busqueda.Font.Size = FontUnit.Point(8)
                AddHandler linkbutonHtml_CodigoEntregale_Busqueda.Click, AddressOf linkbutonHtml_CodigoEntregale_Busqueda_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_CodigoEntregale_Busqueda.Text = "Seleccionar"
                linkbutonHtml_CodigoEntregale_Busqueda.ToolTip = "Click para Eliminar"
                CeldaHtml.Controls.Add(linkbutonHtml_CodigoEntregale_Busqueda)
                FilaHtml.Cells.Add(CeldaHtml)

                i = i + 1
                TbodyBusquedaEntrgable.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
    End Sub

    Private Sub linkbutonHtml_CodigoEntregale_Busqueda_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_CodigoEntregale_Busqueda.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T01Descripcion,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim ___CodigoCliente As String = TestArray(1).ToString ' 
        Dim ___CodigoSeringtec As String = TestArray(2).ToString ' 
        If ___CodigoCliente <> "" Then
            Try
                drop_01_T03Entregable.Text = ___CodigoCliente
            Catch ex As Exception

            End Try

            drop_01_T03Entregable_TextChanged(Nothing, Nothing)
        Else
            If ___CodigoSeringtec <> "" Then
                Try
                    drop_01_T03EntregableSeringrtec.Text = ___CodigoSeringtec
                Catch ex As Exception

                End Try

                drop_01_T03EntregableSeringrtec_TextChanged(Nothing, Nothing)
            End If
        End If

        ModalPopupBuscarEntregable.Hide()
        txt_CodigoBusquedaEntregable.Text = ""
        txt_NombreEntegable.Text = ""
        txt_ODS_BusquedaEntregable.Text = ""

        cargar_Tabla()
    End Sub

    Private Sub txt_NombreEntegable_TextChanged(sender As Object, e As EventArgs) Handles txt_NombreEntegable.TextChanged
        ModalPopupBuscarEntregable.Show()
        cargar_Tabla_BusquedaEntregable()
    End Sub

    Private Sub txt_CodigoBusquedaEntregable_TextChanged(sender As Object, e As EventArgs) Handles txt_CodigoBusquedaEntregable.TextChanged
        ModalPopupBuscarEntregable.Show()
        cargar_Tabla_BusquedaEntregable()
    End Sub

    Private Sub btncerrarModalBusquedaEntregable_Click(sender As Object, e As EventArgs) Handles btncerrarModalBusquedaEntregable.Click
        ModalPopupBuscarEntregable.Hide()
        txt_CodigoBusquedaEntregable.Text = ""
        txt_NombreEntegable.Text = ""
        txt_ODS_BusquedaEntregable.Text = ""
    End Sub



    'kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk

    Private Sub ImageButton_EntreableActividades_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton_EntreableActividades.Click
        ModalPopupBuscarEntregableActividad.Show()
        txt_ODS_BusquedaEntregableActividad.Text = drop_01_T03ODS.Text
        lbl_Popup_Nombre_ODS_BusquedaEntregableActividad.Text = "Proyecto: " & drop_01_T03ODS.Text
    End Sub


    Private Sub cargar_Tabla_BusquedaEntregableActividades(Optional ByVal sLlaveTem As String = "")
        'Dim clsAdminDb As New adminitradorDB
        If txt_ODS_BusquedaEntregableActividad.Text = "" Then
            Exit Sub
        End If
        Dim coleccionDatosPlural As New Collection
        Dim sGroupby As String = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & "," & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
        Dim sCamposTablaLocal As String = _01_T38ACTIVIDADES.Campo_01_T38ID & "," & _01_T38ACTIVIDADES.CampoLlave_01_T38CodigoActividada & "," & _01_T38ACTIVIDADES.Campo_01_T38Descripcion
        Dim sLlavesLocal As String = "_01_T21ODS='" & txt_ODS_BusquedaEntregableActividad.Text & "'"
        If (drop_01_T03Entregable.Text = "N/A" Or drop_01_T03EntregableSeringrtec.Text = "N/A") And drop_01_T03ODS.Text = "ADMINISTRATIVO" And drop_01_T03ODS.Text <> "" Then
            sLlavesLocal = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & "='01'"
        ElseIf drop_01_T03Entregable.Text = "N/A" And drop_01_T03ODS.Text <> "ADMINISTRATIVO" And drop_01_T03ODS.Text <> "" Then
            sLlavesLocal = _01_T38ACTIVIDADES.CampoLlave_01_T38TipoActividada & " in ('02','03')"
        End If

        'sLlavesLocal = sLlavesLocal & " and _01_T03Contrato='" & drop_01_T03Contrato.Text & "'"
        If Trim(txt_CodigoBusquedaEntregableActividad.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " and _01_T38CodigoActividada like '%" & txt_CodigoBusquedaEntregableActividad.Text & "%'"
        End If
        If Trim(txt_NombreEntegableActividad.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " and _01_T38Descripcion like '%" & txt_NombreEntegableActividad.Text & "%'"
        End If

        Dim sTablaLocal As String = _01_T38ACTIVIDADES.NombreTabla

        'Dim sTablaLocal As String = _01_T03REGISTROHORAS.NombreTabla & "," & _01_T06DISCIPLINAS.NombreTabla & "," & _01_T09ODS.NombreTabla & "," & _01_T21LISTADODOCENTREGABLES.NombreTabla & "," & _01_T22AGNOS.NombreTabla & "," & _01_T12FUNCIONARIOS.NombreTabla
        Dim ArregloSingular() As String
        TbodyBusquedaEntrgableActividad.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
        End If
        Dim i As Integer = 1
        'Dim chk_Tem As CheckBox
        Dim llTem1 As Label
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal,,)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow

                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"

                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"
                llTem1 = New Label
                llTem1.Width = Unit.Pixel(100)
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "jjyBByuC12-sep-" '& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(1)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"
                llTem1 = New Label
                llTem1.Width = Unit.Percentage(100)
                'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
                llTem1.ID = i & "jjyBByuC13-sep-" '& ArregloSingular(5).ToString '& "-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                llTem1.Text = ArregloSingular(2)
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                CeldaHtml.Controls.Add(llTem1)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                CeldaHtml.Align = "Center"
                'CeldaHtml.BgColor = "DarkBlue"
                CeldaHtml.BorderColor = "DarkBlue"
                linkbutonHtml_CodigoEntregale_BusquedaActividad = New LinkButton
                linkbutonHtml_CodigoEntregale_BusquedaActividad.Width = Unit.Pixel(100)
                linkbutonHtml_CodigoEntregale_BusquedaActividad.ID = i & "kkcdvvwbggm113-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString & "-sep-" '& ArregloSingular(2).ToString & "-sep-" & ArregloSingular(3).ToString ' & "-sep-" & ArregloSingular(6).ToString & "-sep-" & ArregloSingular(10).ToString & "-sep-" & ArregloSingular(11).ToString
                'ScriptManager1.GetCurrent(Page).RegisterPostBackControl(linkbutonHtml_Eliminar)
                'linkbutonHtml_Eliminar.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_CodigoEntregale_BusquedaActividad.ForeColor = Drawing.Color.DarkRed
                linkbutonHtml_CodigoEntregale_BusquedaActividad.Font.Bold = True
                linkbutonHtml_CodigoEntregale_BusquedaActividad.Font.Size = FontUnit.Point(8)
                AddHandler linkbutonHtml_CodigoEntregale_BusquedaActividad.Click, AddressOf linkbutonHtml_CodigoEntregale_BusquedaActividad_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_CodigoEntregale_BusquedaActividad.Text = "Seleccionar"
                linkbutonHtml_CodigoEntregale_BusquedaActividad.ToolTip = "Click para Eliminar"
                CeldaHtml.Controls.Add(linkbutonHtml_CodigoEntregale_BusquedaActividad)
                FilaHtml.Cells.Add(CeldaHtml)

                i = i + 1
                TbodyBusquedaEntrgableActividad.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else

            End If
        End If
    End Sub

    Private Sub linkbutonHtml_CodigoEntregale_BusquedaActividad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_CodigoEntregale_BusquedaActividad.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        'Dim sCamposTablaLocal As String = "_01_T03Contrato,_01_T03ODS,_01_T03Disciplina,_01_T03Entregable,_01_T01Descripcion,_01_T03Version,_01_T03Fecha,_01_T03Horas,_01_T03EnviadoAProbacion,_01_T03Aprobado,_01_T03Consecutivo,_01_T03Usuario" '
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim ___CodigoCliente As String = TestArray(1).ToString ' 
        Dim ___CodigoSeringtec As String = TestArray(2).ToString ' 
        If ___CodigoCliente <> "" Then
            drop_01_T03Actividad.Text = ___CodigoCliente
            'drop_01_T03Actividad(Nothing, Nothing)
        Else

        End If

        ModalPopupBuscarEntregableActividad.Hide()
        txt_CodigoBusquedaEntregableActividad.Text = ""
        txt_NombreEntegableActividad.Text = ""
        txt_ODS_BusquedaEntregableActividad.Text = ""
        cargar_Tabla()
    End Sub

    Private Sub txt_NombreEntegableActividad_TextChanged(sender As Object, e As EventArgs) Handles txt_NombreEntegableActividad.TextChanged
        ModalPopupBuscarEntregableActividad.Show()
        cargar_Tabla_BusquedaEntregableActividades()
    End Sub

    Private Sub txt_CodigoBusquedaEntregableActividad_TextChanged(sender As Object, e As EventArgs) Handles txt_CodigoBusquedaEntregableActividad.TextChanged
        ModalPopupBuscarEntregableActividad.Show()
        cargar_Tabla_BusquedaEntregableActividades()
    End Sub

    Private Sub btncerrarModalBusquedaEntregableActividad_Click(sender As Object, e As EventArgs) Handles btncerrarModalBusquedaEntregableActividad.Click
        ModalPopupBuscarEntregableActividad.Hide()
        txt_CodigoBusquedaEntregableActividad.Text = ""
        txt_NombreEntegableActividad.Text = ""
        txt_ODS_BusquedaEntregableActividad.Text = ""
    End Sub



End Class


