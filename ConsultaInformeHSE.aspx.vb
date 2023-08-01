Imports System.Data.SqlClient
Imports System.IO

Public Class ConsultaInformeHSE
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

    Protected WithEvents linkbutonHtml_Editar As System.Web.UI.WebControls.LinkButton

    Protected WithEvents linkbutonHtml_Eliminar As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_Eliminar_Personal As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_Personal As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_Eliminar_SeguridadVial As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_SeguridadVial As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_Eliminar_RegistroFoto As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_ver_RegistroFoto As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_RegistroFoto As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_Eliminar_EstadisticaD As System.Web.UI.WebControls.LinkButton
    Protected WithEvents linkbutonHtml_EstadisticaD As System.Web.UI.WebControls.LinkButton
    Dim rptCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsAdminDb = New adminitradorDB
        Datos_Modulo()
        Datos_Modulo_Personal()
        Datos_Modulo_SeguridadVial()
        Datos_Modulo_RegistroFoto()
        Datos_Modulo_EstadisticaD()

        If Not IsPostBack Then
            drop_02_T04Id_Item.AutoPostBack = True
            drop_02_T03NumCtoMarco.AutoPostBack = True
            drop_02_T03Depto.AutoPostBack = True
            'drop_02_Busqueda.AutoPostBack = True
            'txtfiltro_FechaIFinal.AutoPostBack = True
            'txtfiltro_FechaInicial.AutoPostBack = True
            If Session("DatoUsuario" & sCod_Aplicacion) Is Nothing Then
                Session.Abandon()
                Response.Redirect("login.aspx?Err2=001")
            Else

                txt_02_T04Lunes.AutoPostBack = True
                txt_02_T04Martes.AutoPostBack = True
                txt_02_T04Miercoles.AutoPostBack = True
                txt_02_T04Viernes.AutoPostBack = True
                txt_02_T04Jueves.AutoPostBack = True
                txt_02_T04Sabado.AutoPostBack = True
                txt_02_T04Domingo.AutoPostBack = True
                txt_02_T06KmInicial.AutoPostBack = True
                txt_02_T06KmFinal.AutoPostBack = True
                chkempleadoSeringtec.AutoPostBack = True
                chkempleadoexterno.AutoPostBack = True
                drop_02_T05NombresApellidos.AutoPostBack = True
                drop_02_T05NombresApellidosExterno.AutoPostBack = True

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
    Private Sub VerificarPermisoConsulta()
        clsAdminDb = New adminitradorDB
        bPermiso = clsAdminDb.sql_Verificar_Permisos(sCodModulo, adminitradorDB.Tipo_Permiso.Consultar, sTipoDoc, sNumDoc, sNombreUsuario)
        If bPermiso = False Then
            clsAdminDb = Nothing
            Response.Redirect("sinpermiso.aspx")
            Exit Sub
        End If
    End Sub
    Private Sub Datos_Modulo()
        sCodModulo = _02_T03NFORME_HSE_PROYECTOS.CodigoModulo
        sNombreTabla = _02_T03NFORME_HSE_PROYECTOS.NombreTabla
        sCamposTabla = _02_T03NFORME_HSE_PROYECTOS.CamposTabla
        sCamposINS = "'" & clsAdminDb.sRemoverHTML(txt_02_T03Id_ReporteCasos.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03Id_Informe.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03FechaRegistro.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03usuarioRegistra.Text) & "'" & "," & "'" & drop_02_T03NumCtoMarco.Text & "'" & "," & "'" & drop_02_T03Mes.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_02_T03FechaSemana_Inicio.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_02_T03FechaSemana_Final.Text) & "'" & "," & "'" & drop_02_T03Depto.Text & "'" & "," & "'" & drop_02_T03Municipio.Text & "'" & "," & "'" & drop_02_T03ODS.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_02_T03AreaCliente.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03ActividadesEjecutadas.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03Id_EstadisticaDiaria.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03Id_Personal.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03Id_SeguridadVial.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03Id_RegistroFotografico.Text) & "'" & "," & "'" & txt_02_T03ResponsableInforme.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T03ComentariosFinales.Text) & "'"
        sCamposUPD = _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03FechaRegistro & "='" & clsAdminDb.sRemoverHTML(txt_02_T03FechaRegistro.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03usuarioRegistra & "='" & clsAdminDb.sRemoverHTML(txt_02_T03usuarioRegistra.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03NumCtoMarco & "='" & drop_02_T03NumCtoMarco.Text & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03Mes & "='" & drop_02_T03Mes.Text & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03FechaSemana_Inicio & "='" & clsAdminDb.sRemoverHTML(drop_02_T03FechaSemana_Inicio.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03FechaSemana_Final & "='" & clsAdminDb.sRemoverHTML(drop_02_T03FechaSemana_Final.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03Depto & "='" & drop_02_T03Depto.Text & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03Municipio & "='" & drop_02_T03Municipio.Text & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03ODS & "='" & drop_02_T03ODS.Text & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03AreaCliente & "='" & clsAdminDb.sRemoverHTML(drop_02_T03AreaCliente.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03ActividadesEjecutadas & "='" & clsAdminDb.sRemoverHTML(txt_02_T03ActividadesEjecutadas.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03Id_EstadisticaDiaria & "='" & clsAdminDb.sRemoverHTML(txt_02_T03Id_EstadisticaDiaria.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03Id_Personal & "='" & clsAdminDb.sRemoverHTML(txt_02_T03Id_Personal.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03Id_SeguridadVial & "='" & clsAdminDb.sRemoverHTML(txt_02_T03Id_SeguridadVial.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03Id_RegistroFotografico & "='" & clsAdminDb.sRemoverHTML(txt_02_T03Id_RegistroFotografico.Text) & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03ResponsableInforme & "='" & txt_02_T03ResponsableInforme.Text & "'" & "," & _02_T03NFORME_HSE_PROYECTOS.Campo_02_T03ComentariosFinales & "='" & clsAdminDb.sRemoverHTML(txt_02_T03ComentariosFinales.Text) & "'"
        sLlaves = _02_T03NFORME_HSE_PROYECTOS.CampoLlave_02_T03Id_ReporteCasos & "=  '" & txt_02_T03Id_ReporteCasos.Text & "'" & " AND " & _02_T03NFORME_HSE_PROYECTOS.CampoLlave_02_T03Id_Informe & "=  '" & txt_02_T03Id_Informe.Text & "'"
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


            drop_02_T03Mes.Items.Add(New ListItem("Seleccionar", ""))
            drop_02_T03Mes.Items.Add(New ListItem("Enero", "01"))
            drop_02_T03Mes.Items.Add(New ListItem("Febrero", "02"))
            drop_02_T03Mes.Items.Add(New ListItem("Marzo", "03"))
            drop_02_T03Mes.Items.Add(New ListItem("Abril", "04"))
            drop_02_T03Mes.Items.Add(New ListItem("Mayo", "05"))
            drop_02_T03Mes.Items.Add(New ListItem("Junio", "06"))
            drop_02_T03Mes.Items.Add(New ListItem("Julio", "07"))
            drop_02_T03Mes.Items.Add(New ListItem("Agosto", "08"))
            drop_02_T03Mes.Items.Add(New ListItem("Septiembre", "09"))
            drop_02_T03Mes.Items.Add(New ListItem("Octrube", "10"))
            drop_02_T03Mes.Items.Add(New ListItem("Noviembre", "11"))
            drop_02_T03Mes.Items.Add(New ListItem("Diciembre", "12"))

            drop_02_T03AreaCliente.Items.Add(New ListItem(""))
            drop_02_T03AreaCliente.Items.Add(New ListItem("Seleccionar"))
            drop_02_T03AreaCliente.Items.Add(New ListItem("Operativo"))
            drop_02_T03AreaCliente.Items.Add(New ListItem("Tecnica"))

            Dim sCampos_tem3 = _02_T17ITEMESTADISTICADIARIA.CampoLlave_02_T17Codigo & "," & _02_T17ITEMESTADISTICADIARIA.Campo_02_T17Nombre
            Dim stables_Tem3 = _02_T17ITEMESTADISTICADIARIA.NombreTabla
            Dim sllaves_Tem3 = _02_T17ITEMESTADISTICADIARIA.CampoLlave_02_T17Activo & "= 'SI' "
            .drop_CargarCombox(drop_02_T04Id_Item, stables_Tem3, sCampos_tem3, sllaves_Tem3, True,, "cast(_02_T17Codigo as integer)")

            Dim sCampos_tem4 = _02_T19TIPOVEHICULO.CampoLlave_02_T19Codigo & "," & _02_T19TIPOVEHICULO.Campo_02_T19Nombre
            Dim stables_Tem4 = _02_T19TIPOVEHICULO.NombreTabla
            Dim sllaves_Tem4 = _02_T19TIPOVEHICULO.Campo_02_T19Activo & "= 'SI' "
            .drop_CargarCombox(drop_02_T06TipoVehiculo, stables_Tem4, sCampos_tem4, sllaves_Tem4, True,, _02_T19TIPOVEHICULO.Campo_02_T19Nombre)




            'dropiluminacionInadecuada.Items.Add(New ListItem("texto", "valor"))
            'select  _01_T07Codigo,_01_T07Codigo +' ' + (SELECT TOP 1  _01_T08Nombre from SERINGTEC.dbo._01_T08CLIENTES WHERE _01_T08Nit=_01_T07Cliente),_01_T07Nombre 
            'from SERINGTEC.dbo._01_T07CONTRATOSMARCO
            'where _01_T07Activo='SI'
            Dim sCampos_tem As String = "_01_T07Codigo,_01_T07Codigo +' ' + (SELECT TOP 1  _01_T08Nombre from SERINGTEC.dbo._01_T08CLIENTES WHERE _01_T08Nit=_01_T07Cliente)"
            Dim stables_Tem As String = "SERINGTEC.dbo._01_T07CONTRATOSMARCO"
            Dim sllaves_Tem As String = "_01_T07Activo='SI'"
            .drop_CargarCombox(drop_02_T03NumCtoMarco, stables_Tem, sCampos_tem, sllaves_Tem)



            sCampos_tem = _99_T09DEPARTAMENTOS.CampoLlave_99_T09Codigo & "," & _99_T09DEPARTAMENTOS.Campo_99_T09Nombre
            stables_Tem = _99_T09DEPARTAMENTOS.NombreTabla
            sllaves_Tem = _99_T09DEPARTAMENTOS.Campo_99_T09Activo & "= 'SI' " & " and " & _99_T09DEPARTAMENTOS.CampoLlave_99_T09CodPais & " ='169' "
            .drop_CargarCombox(drop_02_T03Depto, stables_Tem, sCampos_tem, sllaves_Tem,,, _99_T09DEPARTAMENTOS.Campo_99_T09Nombre)

            sCampos_tem = "_01_T12NumDoc,_01_T12NombreApellidos"
            stables_Tem = "SERINGTEC.dbo._01_T12FUNCIONARIOS"
            sllaves_Tem = "_01_T12Activo='SI'"
            .drop_CargarCombox(drop_02_T05NombresApellidos, stables_Tem, sCampos_tem, sllaves_Tem, True,, "_01_T12NombreApellidos")


            sCampos_tem = "_01_T12NumDoc,_01_T12NombreApellidos"
            stables_Tem = "SERINGTEC.dbo._01_T12FUNCIONARIOS," & "_04_HSE_REPORTESDECASOS.dbo._99_T02PERFILxFUNCIONARIOxMODxPERMISO"
            sllaves_Tem = "_01_T12Activo='SI' and _01_T12NumDoc = _99_T02NumDocFuncionario "
            .drop_CargarCombox(drop_02_Busqueda, stables_Tem, sCampos_tem, sllaves_Tem, True,, "_01_T12NombreApellidos")

            Dim sCampos_tem8 = _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1Identificacion & "," & _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1NombresApellidos
            Dim stables_Tem8 = _02_T05_1PERSONAL_EXTERNO.NombreTabla
            Dim sllaves_Tem8 = ""
            .drop_CargarCombox(drop_02_T05NombresApellidosExterno, stables_Tem8, sCampos_tem8, sllaves_Tem8, True,, _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1NombresApellidos)



        End With
    End Sub
    Private Sub Cargar_drop_fechas()
        Dim iDiaSemana As String = Now.DayOfWeek
        'Dim sNombredia As String = Day(iDiaSemana).ToString
        Dim iNumExcelFechaActual As Integer = Fix(Now.ToOADate)
        Dim dFechaActual As Date = Now.Date
        Dim idiaMartes As Integer
        If iDiaSemana = 3 Then ' miercoles
            idiaMartes = iNumExcelFechaActual - 2
        ElseIf iDiaSemana = 4 Then ' jueves
            idiaMartes = iNumExcelFechaActual - 3
        ElseIf iDiaSemana = 5 Then ' viernes
            idiaMartes = iNumExcelFechaActual - 4
        ElseIf iDiaSemana = 6 Then ' sabado
            idiaMartes = iNumExcelFechaActual - 5
        ElseIf iDiaSemana = 0 Then ' domiengo
            idiaMartes = iNumExcelFechaActual - 6
        ElseIf iDiaSemana = 1 Then ' Lunes
            idiaMartes = iNumExcelFechaActual - 7
        ElseIf iDiaSemana = 2 Then ' martes
            idiaMartes = iNumExcelFechaActual - 8
        End If
        drop_02_T03FechaSemana_Inicio.Items.Add(New ListItem("Seleccionar", ""))
        drop_02_T03FechaSemana_Final.Items.Add(New ListItem("Seleccionar", ""))
        For i = (idiaMartes) To iNumExcelFechaActual
            Dim dDate As Date = DateTime.FromOADate(i)
            Dim sFecha As String = ""
            If bValidarFecha(dDate, dDate.Day, dDate.Month, dDate.Year, sFecha) = False Then

            End If
            'drop_02_T03FechaSemana_Inicio.Items.Add(New ListItem(DateTime.FromOADate(i), DateTime.FromOADate(i)))
            'drop_02_T03FechaSemana_Inicio.Items.Add(New ListItem(sFecha, sFecha))
            drop_02_T03FechaSemana_Inicio.Items.Add(New ListItem(sFecha, sFecha))
            drop_02_T03FechaSemana_Final.Items.Add(New ListItem(sFecha, sFecha))

        Next

    End Sub



    Private Sub WebForm1_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        clsAdminDb = Nothing
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If bValidarCampos() = False Then Exit Sub
        If bValidarSQL() = False Then Exit Sub
        Guardar_Registro()
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
        Cargar_drop_fechas()
        cargar_Drops_ODS()
        cargar_drop_Municipio()
        Cargar_Registro_Cargos()
        txt_02_T03Id_ReporteCasos.Text = Nothing
        txt_02_T03Id_Informe.Text = Nothing
        txt_02_T03FechaRegistro.Text = Now
        txt_02_T03usuarioRegistra.Text = lblUsuarioLogueado_Documento.Text
        drop_02_T03NumCtoMarco.Text = Nothing
        drop_02_T03Mes.Text = Nothing
        drop_02_T03FechaSemana_Inicio.Text = Date.Today
        drop_02_T03FechaSemana_Final.Text = Date.Today
        drop_02_T03Depto.Text = Nothing
        drop_02_T03Municipio.Text = Nothing
        drop_02_T03ODS.Text = Nothing
        drop_02_T03AreaCliente.Text = Nothing
        txt_02_T03ActividadesEjecutadas.Text = Nothing
        txt_02_T03ComentariosFinales.Text = Nothing
        txt_02_T03Id_EstadisticaDiaria.Text = Nothing
        txt_02_T03Id_Personal.Text = Nothing
        txt_02_T03Id_SeguridadVial.Text = Nothing
        txt_02_T03Id_RegistroFotografico.Text = Nothing
        txt_02_T03ResponsableInforme.Text = lblUsuarioLogueado_Documento.Text
        lbl_Nombre_Responsable.Text = lblUsuarioLogueado_Nombre.Text



        lbl_02_T03Id_ReporteCasos.ForeColor = Drawing.Color.Black
        lbl_02_T03Id_Informe.ForeColor = Drawing.Color.Black
        lbl_02_T03FechaRegistro.ForeColor = Drawing.Color.Black
        lbl_02_T03usuarioRegistra.ForeColor = Drawing.Color.Black
        lbl_02_T03NumCtoMarco.ForeColor = Drawing.Color.Black
        drop_02_T03Mes.ForeColor = Drawing.Color.Black
        lbl_02_T03FechaSemana_Inicio.ForeColor = Drawing.Color.Black
        lbl_02_T03FechaSemana_Final.ForeColor = Drawing.Color.Black
        lbl_02_T03Depto.ForeColor = Drawing.Color.Black
        lbl_02_T03Municipio.ForeColor = Drawing.Color.Black
        lbl_02_T03ODS.ForeColor = Drawing.Color.Black
        lbl_02_T03AreaCliente.ForeColor = Drawing.Color.Black
        lbl_02_T03ActividadesEjecutadas.ForeColor = Drawing.Color.Black
        lbl_02_T03Id_EstadisticaDiaria.ForeColor = Drawing.Color.Black
        lbl_02_T03Id_Personal.ForeColor = Drawing.Color.Black
        lbl_02_T03Id_SeguridadVial.ForeColor = Drawing.Color.Black
        lbl_02_T03Id_RegistroFotografico.ForeColor = Drawing.Color.Black
        'lbl_02_T03ResponsableInforme.ForeColor = Drawing.Color.Black
        lbl_02_T03ComentariosFinales.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing

        limpiarCampos_Personal()
        limpiarCampos_SeguridadVial()
        limpiarCampos_RegistroFoto()
        limpiarCampos_EstadisticaD()
        MostrarMensaje("")
        bHabilitarControles(True)
        filtro()
    End Sub


    Private Sub bHabilitarControles(ByVal bEstado As Boolean)
        'txt_02_T03Id_ReporteCasos.Enabled = bEstado
        'txt_02_T03Id_Informe.Enabled = bEstado
        'txt_02_T03FechaRegistro.Enabled = bEstado
        'txt_02_T03usuarioRegistra.Enabled = bEstado
        'drop_02_T03NumCtoMarco.Enabled = bEstado
        'drop_02_T03Mes.Enabled = bEstado
        'txt_02_T03FechaSemana_Inicio.Enabled = bEstado
        'txt_02_T03FechaSemana_Final.Enabled = bEstado
        'drop_02_T03Depto.Enabled = bEstado
        'drop_02_T03Municipio.Enabled = bEstado
        'drop_02_T03ODS.Enabled = bEstado
        'txt_02_T03AreaCliente.Enabled = bEstado
        'txt_02_T03ActividadesEjecutadas.Enabled = bEstado
        'txt_02_T03Id_EstadisticaDiaria.Enabled = bEstado
        'txt_02_T03Id_Personal.Enabled = bEstado
        'txt_02_T03Id_SeguridadVial.Enabled = bEstado
        'txt_02_T03Id_RegistroFotografico.Enabled = bEstado
        'txt_02_T03ResponsableInforme.Enabled = bEstado
        'txt_02_T03ComentariosFinales.Enabled = bEstado
        Panel_1.Visible = Not bEstado
        Panel_2.Visible = Not bEstado
        Panel_3.Visible = Not bEstado
        btnGuardar.Visible = False

        btnImprimir.Visible = Not bEstado
    End Sub

    Private Function bValidarCampos() As Boolean
        bValidarCampos = False

        lbl_02_T03Id_ReporteCasos.ForeColor = Drawing.Color.Red
        lbl_02_T03Id_Informe.ForeColor = Drawing.Color.Red
        lbl_02_T03FechaRegistro.ForeColor = Drawing.Color.Red
        lbl_02_T03usuarioRegistra.ForeColor = Drawing.Color.Red
        lbl_02_T03NumCtoMarco.ForeColor = Drawing.Color.Red
        lbl_02_T03Mes.ForeColor = Drawing.Color.Red
        lbl_02_T03FechaSemana_Inicio.ForeColor = Drawing.Color.Red
        lbl_02_T03FechaSemana_Final.ForeColor = Drawing.Color.Red
        lbl_02_T03Depto.ForeColor = Drawing.Color.Red
        lbl_02_T03Municipio.ForeColor = Drawing.Color.Red
        lbl_02_T03ODS.ForeColor = Drawing.Color.Red
        lbl_02_T03AreaCliente.ForeColor = Drawing.Color.Red
        lbl_02_T03ActividadesEjecutadas.ForeColor = Drawing.Color.Red
        lbl_02_T03Id_EstadisticaDiaria.ForeColor = Drawing.Color.Red
        lbl_02_T03Id_Personal.ForeColor = Drawing.Color.Red
        lbl_02_T03Id_SeguridadVial.ForeColor = Drawing.Color.Red
        lbl_02_T03Id_RegistroFotografico.ForeColor = Drawing.Color.Red
        'lbl_02_T03ResponsableInforme.ForeColor = Drawing.Color.Red
        lbl_02_T03ComentariosFinales.ForeColor = Drawing.Color.Red
        'If Trim(txt_02_T03Id_ReporteCasos.Text) = "" Then
        '    txt_02_T03Id_ReporteCasos.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Id_ReporteCasos")
        '    Exit Function
        'Else
        '    lbl_02_T03Id_ReporteCasos.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T03Id_Informe.Text) = "" Then
        '    txt_02_T03Id_Informe.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Id_Informe")
        '    Exit Function
        'Else
        '    lbl_02_T03Id_Informe.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T03FechaRegistro.Text) = "" Then
        '    txt_02_T03FechaRegistro.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03FechaRegistro")
        '    Exit Function
        'Else
        '    lbl_02_T03FechaRegistro.ForeColor = Drawing.Color.Black
        'End If
        If Trim(txt_02_T03usuarioRegistra.Text) = "" Then
            txt_02_T03usuarioRegistra.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03usuarioRegistra")
            Exit Function
        Else
            lbl_02_T03usuarioRegistra.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T03NumCtoMarco.Text) = "" Then
            drop_02_T03NumCtoMarco.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03NumCtoMarco")
            Exit Function
        Else
            lbl_02_T03NumCtoMarco.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T03Mes.Text) = "" Then
            drop_02_T03Mes.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Mes")
            Exit Function
        Else
            lbl_02_T03Mes.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T03FechaSemana_Inicio.Text) = "" Then
            drop_02_T03FechaSemana_Inicio.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03FechaSemana_Inicio")
            Exit Function
        Else
            lbl_02_T03FechaSemana_Inicio.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T03FechaSemana_Final.Text) = "" Then
            drop_02_T03FechaSemana_Final.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03FechaSemana_Final")
            Exit Function
        Else
            lbl_02_T03FechaSemana_Final.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T03Depto.Text) = "" Then
            drop_02_T03Depto.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Depto")
            Exit Function
        Else
            lbl_02_T03Depto.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T03Municipio.Text) = "" Then
            drop_02_T03Municipio.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Municipio")
            Exit Function
        Else
            lbl_02_T03Municipio.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T03AreaCliente.Text) = "" Then
            drop_02_T03AreaCliente.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03AreaCliente")
            Exit Function
        Else
            lbl_02_T03AreaCliente.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_02_T03ODS.Text) = "" Then
            drop_02_T03ODS.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03ODS")
            Exit Function
        Else
            lbl_02_T03ODS.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T03ActividadesEjecutadas.Text) = "" Then
            txt_02_T03ActividadesEjecutadas.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03ActividadesEjecutadas")
            Exit Function
        Else
            lbl_02_T03ActividadesEjecutadas.ForeColor = Drawing.Color.Black
        End If
        'If Trim(txt_02_T03Id_EstadisticaDiaria.Text) = "" Then
        '    txt_02_T03Id_EstadisticaDiaria.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Id_EstadisticaDiaria")
        '    Exit Function
        'Else
        '    lbl_02_T03Id_EstadisticaDiaria.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T03Id_Personal.Text) = "" Then
        '    txt_02_T03Id_Personal.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Id_Personal")
        '    Exit Function
        'Else
        '    lbl_02_T03Id_Personal.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T03Id_SeguridadVial.Text) = "" Then
        '    txt_02_T03Id_SeguridadVial.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Id_SeguridadVial")
        '    Exit Function
        'Else
        '    lbl_02_T03Id_SeguridadVial.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T03Id_RegistroFotografico.Text) = "" Then
        '    txt_02_T03Id_RegistroFotografico.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03Id_RegistroFotografico")
        '    Exit Function
        'Else
        '    lbl_02_T03Id_RegistroFotografico.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(drop_02_T03ResponsableInforme.text) = "" Then
        '    drop_02_T03ResponsableInforme.focus
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03ResponsableInforme")
        '    Exit Function
        'Else
        '    lbl_02_T03ResponsableInforme.ForeColor = Drawing.Color.Black
        'End If
        If Trim(txt_02_T03ComentariosFinales.Text) = "" Then
            txt_02_T03ComentariosFinales.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 03ComentariosFinales")
            Exit Function
        Else
            lbl_02_T03ComentariosFinales.ForeColor = Drawing.Color.Black
        End If


        bValidarCampos = True
        MostrarMensaje("")
    End Function



    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_02_T03Id_ReporteCasos.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03Id_Informe.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03FechaRegistro.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03usuarioRegistra.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_02_T03FechaSemana_Inicio.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_02_T03FechaSemana_Final.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_02_T03AreaCliente.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03ActividadesEjecutadas.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03Id_EstadisticaDiaria.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03Id_Personal.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03Id_SeguridadVial.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03Id_RegistroFotografico.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T03ComentariosFinales.Text) Then Exit Function
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

                txt_02_T03Id_ReporteCasos.Text = coleccionDatos(0)
                txt_02_T03Id_Informe.Text = coleccionDatos(1)
                txt_02_T03FechaRegistro.Text = coleccionDatos(2)
                txt_02_T03usuarioRegistra.Text = coleccionDatos(3)
                drop_02_T03NumCtoMarco.Text = coleccionDatos(4)
                drop_02_T03Mes.Text = coleccionDatos(5)
                drop_02_T03FechaSemana_Inicio.Items.Clear()
                drop_02_T03FechaSemana_Inicio.Items.Add(New ListItem(coleccionDatos(6), coleccionDatos(6)))
                drop_02_T03FechaSemana_Inicio.Text = coleccionDatos(6)
                drop_02_T03FechaSemana_Final.Items.Clear()
                drop_02_T03FechaSemana_Final.Items.Add(New ListItem(coleccionDatos(7), coleccionDatos(7)))
                drop_02_T03FechaSemana_Final.Text = coleccionDatos(7)
                drop_02_T03Depto.Text = coleccionDatos(8)
                cargar_drop_Municipio()
                drop_02_T03Municipio.Text = coleccionDatos(9)
                cargar_Drops_ODS()
                drop_02_T03ODS.Text = coleccionDatos(10)
                drop_02_T03AreaCliente.Text = coleccionDatos(11)
                txt_02_T03ActividadesEjecutadas.Text = coleccionDatos(12)
                txt_02_T03Id_EstadisticaDiaria.Text = coleccionDatos(13)
                txt_02_T03Id_Personal.Text = coleccionDatos(14)
                txt_02_T03Id_SeguridadVial.Text = coleccionDatos(15)
                txt_02_T03Id_RegistroFotografico.Text = coleccionDatos(16)
                txt_02_T03ResponsableInforme.Text = coleccionDatos(17)
                txt_02_T03ComentariosFinales.Text = coleccionDatos(18)
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
        clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0

        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla, sLlaves)
        If lCantRegistros = 0 Then
            txt_02_T03Id_Informe.Text = Iconsecutivo_Informe()
            txt_02_T03Id_ReporteCasos.Text = Iconsecutivo_Rporte_Casos()
            Datos_Modulo()

            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla, sCamposTabla, sCamposINS)
            If bSqlInsert = True Then
                MostrarMensaje("Se inserto nuevo registro correctamente ")
                Registro_Procesos("Guardar", clsAdminDb.Mostrar_Consulta)
                bHabilitarControles(False)
                txt_02_T05Id_Estadistica.Text = txt_02_T03Id_Informe.Text
                filtro()
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
                Registro_Procesos("Actualizar", clsAdminDb.Mostrar_Consulta)
                bHabilitarControles(False)
                txt_02_T05Id_Estadistica.Text = txt_02_T03Id_Informe.Text
                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
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
        Dim sCamposTablaLocal As String
        Dim sLlavesLocal As String
        Dim sTablaLocal As String = _02_T03NFORME_HSE_PROYECTOS.NombreTabla
        Dim ArregloSingular() As String
        Dim iFechainicio As Integer = 0
        Dim iFechaFinal As Integer = 0
        If txtfiltro_FechaIFinal.Text <> "" And txtfiltro_FechaInicial.Text <> "" Then
            iFechainicio = CDate(txtfiltro_FechaInicial.Text).ToOADate
            iFechaFinal = CDate(txtfiltro_FechaIFinal.Text).ToOADate
        Else
            Exit Sub
        End If
        Dim sconsulta As String = ""
        sconsulta = " select _02_T03Id_Informe,_02_T03NumCtoMarco, _02_T03ODS,_02_T03Mes,_02_T03FechaSemana_Inicio,_02_T03FechaSemana_Final, "
        sconsulta += " (select _99_T09Nombre from  _99_T09DEPARTAMENTOS where _99_T09CodPais='169' and _99_T09Codigo=_02_T03Depto), "
        sconsulta += " (select _99_T10Nombre from  _99_T10MUNICIPIOS where _99_T10CodPais='169' and _99_T10CodDepto=_02_T03Depto and _99_T10Codigo=_02_T03Municipio) "
        sconsulta += " from _02_T03NFORME_HSE_PROYECTOS,SERINGTEC.dbo._01_T22AGNOS as fini,SERINGTEC.dbo._01_T22AGNOS as fFIN"
        sconsulta += " where _02_T03usuarioRegistra='" & drop_02_Busqueda.Text & "' "
        sconsulta += " and (fini._01_T22Fecha=_02_T03FechaSemana_Inicio and fini._01_T22FechaNumero >=" & iFechainicio & " and fini._01_T22FechaNumero <=" & iFechaFinal & " "
        sconsulta += " and fFIN._01_T22Fecha=_02_T03FechaSemana_Final and fFIN._01_T22FechaNumero>=" & iFechainicio & " and fFIN._01_T22FechaNumero<=" & iFechaFinal & " )"



        Tabla_Principal.Controls.Clear()

        'If Trim(txtFiltroBusqueda.Text) <> "" Then
        '    sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        'Else
        '    If Trim("colocar filtro o validaciones") <> "" Then
        '        sLlavesLocal = sLlavesLocal
        '    End If
        'End If
        Dim i As Integer = 0
        coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sconsulta)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow
                CeldaHtml = New HtmlTableCell
                linkbutonHtml = New LinkButton
                linkbutonHtml.Enabled = False
                linkbutonHtml.ID = i & "a-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                linkbutonHtml.Text = ArregloSingular(0)
                CeldaHtml.Controls.Add(linkbutonHtml)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml = New LinkButton
                linkbutonHtml.Enabled = False
                linkbutonHtml.ID = i & "b-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                linkbutonHtml.Text = ArregloSingular(1)
                CeldaHtml.Controls.Add(linkbutonHtml)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml = New LinkButton
                linkbutonHtml.Enabled = False
                linkbutonHtml.ID = i & "c-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                linkbutonHtml.Text = ArregloSingular(2)
                CeldaHtml.Controls.Add(linkbutonHtml)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml = New LinkButton
                linkbutonHtml.Enabled = False
                linkbutonHtml.ID = i & "d-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                linkbutonHtml.Text = ArregloSingular(3)
                CeldaHtml.Controls.Add(linkbutonHtml)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml = New LinkButton
                linkbutonHtml.Enabled = False
                linkbutonHtml.ID = i & "e-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                linkbutonHtml.Text = ArregloSingular(4)
                CeldaHtml.Controls.Add(linkbutonHtml)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml = New LinkButton
                linkbutonHtml.Enabled = False
                linkbutonHtml.ID = i & "f-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                linkbutonHtml.Text = ArregloSingular(5)
                CeldaHtml.Controls.Add(linkbutonHtml)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml = New LinkButton
                linkbutonHtml.Enabled = False
                linkbutonHtml.ID = i & "g-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                linkbutonHtml.Text = ArregloSingular(6)
                CeldaHtml.Controls.Add(linkbutonHtml)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml = New LinkButton
                linkbutonHtml.Enabled = False
                linkbutonHtml.ID = i & "h-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
                linkbutonHtml.Text = ArregloSingular(6)
                CeldaHtml.Controls.Add(linkbutonHtml)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Editar = New LinkButton

                linkbutonHtml_Editar.ID = i & "i-sep-" & ArregloSingular(0).ToString '& "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                linkbutonHtml_Editar.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_Editar.ForeColor = Drawing.Color.DarkGreen
                linkbutonHtml_Editar.Font.Bold = True
                linkbutonHtml_Editar.Font.Size = FontUnit.Point(8)
                AddHandler linkbutonHtml_Editar.Click, AddressOf linkbutonHtml_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_Editar.Text = "Ver"
                CeldaHtml.Controls.Add(linkbutonHtml_Editar)
                FilaHtml.Cells.Add(CeldaHtml)


                i = i + 1
                Tabla_Principal.Controls.Add(FilaHtml)
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
        Dim sLlaves_Tem = _02_T03NFORME_HSE_PROYECTOS.CampoLlave_02_T03Id_Informe & "='" & sllave1 & "'"


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
        sLlaves = _02_T03NFORME_HSE_PROYECTOS.CampoLlave_02_T03Id_Informe & "='" & sllave1 & "'"
        'sLlaves = sLlaves & " and " & _01_T20TIPODOCENTREGABLES.CampoLlave_01_T20Codigo & "='" & sllave2 & "'"
        Cargar_Registro()
        filtro()
    End Sub

    Private Sub linkbutonHtml_Editar_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(1).ToString
        'Dim sllave2 As String = TestArray(2).ToString
        'Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        sLlaves = _02_T03NFORME_HSE_PROYECTOS.CampoLlave_02_T03Id_Informe & "='" & sllave1 & "'"
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
            cargar_Tabla_Personal()
            cargar_Tabla_SeguridadVial()
            cargar_Tabla_RegistroFoto()
            cargar_Tabla_EstadisticaD()
        End If
    End Sub

    'Private Sub txtFiltroBusqueda_TextChanged(sender As Object, e As EventArgs) Handles txtFiltroBusqueda.TextChanged
    '    filtro()
    'End Sub


    Private Sub cargar_Drops_ODS()
        drop_02_T03ODS.Items.Clear()
        drop_02_T03ODS.Items.Add(New ListItem("Seleccionar", ""))
        If drop_02_T03NumCtoMarco.Text <> "" Then
            Dim sCampos_tem As String = "_01_T09Codigo,_01_T09NumODS"
            Dim stables_Tem As String = "SERINGTEC.dbo._01_T09ODS"
            Dim sllaves_Tem As String = "_01_T09ContratoMArco='" & drop_02_T03NumCtoMarco.Text & "'"
            sllaves_Tem += " and _01_T09Activo='SI'"
            clsAdminDb.drop_CargarCombox(drop_02_T03ODS, stables_Tem, sCampos_tem, sllaves_Tem)

        End If

    End Sub

    Private Sub drop_02_T03NumCtoMarco_TextChanged(sender As Object, e As EventArgs) Handles drop_02_T03NumCtoMarco.TextChanged
        cargar_Drops_ODS()
    End Sub

    Private Sub drop_02_T03Depto_TextChanged(sender As Object, e As EventArgs) Handles drop_02_T03Depto.TextChanged
        cargar_drop_Municipio()
    End Sub
    Private Sub cargar_drop_Municipio()
        drop_02_T03Municipio.Items.Clear()
        If drop_02_T03Depto.Text <> "" Then
            Dim sCampos_tem As String = "_99_T10Codigo,_99_T10Nombre"
            Dim stables_Tem As String = "_99_T10MUNICIPIOS"
            Dim sllaves_Tem As String = "_99_T10CodDepto='" & drop_02_T03Depto.Text & "' and _99_T10Activo = 'SI' and _99_T10CodPais = '169' "
            clsAdminDb.drop_CargarCombox(drop_02_T03Municipio, stables_Tem, sCampos_tem, sllaves_Tem)

        End If

    End Sub


    Private Sub Cargar_Registro_Cargos()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object
        Dim sCampos_tem As String = "_01_T24Nombre"
        Dim stables_Tem As String = "SERINGTEC.dbo._01_T12FUNCIONARIOS,SERINGTEC.dbo._01_T24CARGOS"
        Dim sllaves_Tem As String = "_01_T12NumDoc='" & lblUsuarioLogueado_Documento.Text & "' AND _01_T12Cargo=_01_T24Codigo"


        coleccionDatos = clsAdminDb.sql_Select(stables_Tem, sCampos_tem, sllaves_Tem)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            Else
                lbl_Cargo_Responsable.Text = coleccionDatos(0)
                'txt_02_T03.Text = coleccionDatos(0)
            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else
                'MostrarMensaje("No se encontraron datos" )
            End If
        End If
    End Sub


    Private Function Iconsecutivo_Informe() As Integer
        Dim iVigencia As String = Now.Year
        Dim sConsecutivo As String = ""
        sConsecutivo = clsAdminDb.sql_Max(_02_T03NFORME_HSE_PROYECTOS.NombreTabla, "cast(" & _02_T03NFORME_HSE_PROYECTOS.CampoLlave_02_T03Id_Informe & " as integer)", "_02_T03Id_Informe<>''")
        If sConsecutivo = "0" Then
            sConsecutivo = iVigencia & "1"
        Else
            sConsecutivo = Val(sConsecutivo) + 1
        End If
        Iconsecutivo_Informe = sConsecutivo
    End Function


    Private Function Iconsecutivo_Rporte_Casos() As Integer
        Dim iVigencia As String = Now.Year
        Dim sConsecutivo As String = ""
        sConsecutivo = clsAdminDb.sql_Max(_02_T03NFORME_HSE_PROYECTOS.NombreTabla, "cast(" & _02_T03NFORME_HSE_PROYECTOS.CampoLlave_02_T03Id_ReporteCasos & " as integer)", "_02_T03Id_Informe<>''")
        If sConsecutivo = "0" Then
            sConsecutivo = iVigencia & "1"
        Else
            sConsecutivo = Val(sConsecutivo) + 1
        End If
        Iconsecutivo_Rporte_Casos = sConsecutivo
    End Function



    '********************** comienza todo lo de PERSONAL

    Dim sCodModulo_Personal As String
    Dim sNombreTabla_Personal As String
    Dim sCamposTabla_Personal As String
    Dim sCamposINS_Personal As String
    Dim sCamposUPD_Personal As String
    Dim sLlaves_Personal As String

    Private Sub Datos_Modulo_Personal()
        sCodModulo_Personal = _02_T05PERSONAL.CodigoModulo
        sNombreTabla_Personal = _02_T05PERSONAL.NombreTabla
        sCamposTabla_Personal = _02_T05PERSONAL.CamposTabla
        sCamposINS_Personal = "'" & clsAdminDb.sRemoverHTML(txt_02_T05Id_Estadistica.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05Id_Personal.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05NombresApellidos.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05Identificacion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05ICargo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05IEmpresa.Text) & "'"
        sCamposUPD_Personal = _02_T05PERSONAL.Campo_02_T05NombresApellidos & "='" & clsAdminDb.sRemoverHTML(txt_02_T05NombresApellidos.Text) & "'" & "," & _02_T05PERSONAL.Campo_02_T05Identificacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T05Identificacion.Text) & "'" & "," & _02_T05PERSONAL.Campo_02_T05ICargo & "='" & clsAdminDb.sRemoverHTML(txt_02_T05ICargo.Text) & "'" & "," & _02_T05PERSONAL.Campo_02_T05IEmpresa & "='" & clsAdminDb.sRemoverHTML(txt_02_T05IEmpresa.Text) & "'"
        sLlaves_Personal = _02_T05PERSONAL.CampoLlave_02_T05Id_Estadistica & "=  '" & txt_02_T05Id_Estadistica.Text & "'" & " AND " & _02_T05PERSONAL.CampoLlave_02_T05Id_Personal & "=  '" & txt_02_T05Id_Personal.Text & "'"
    End Sub

    Private Sub limpiarCampos_Personal()

        txt_02_T05Id_Estadistica.Text = Nothing
        txt_02_T05Id_Personal.Text = Nothing
        txt_02_T05NombresApellidos.Text = Nothing
        txt_02_T05Identificacion.Text = Nothing
        txt_02_T05ICargo.Text = Nothing
        txt_02_T05IEmpresa.Text = Nothing
        drop_02_T05NombresApellidos.Text = ""

        lbl_02_T05Id_Estadistica.ForeColor = Drawing.Color.Black
        'lbl_02_T05Id_Personal.ForeColor = Drawing.Color.Black
        lbl_02_T05NombresApellidos.ForeColor = Drawing.Color.Black
        lbl_02_T05Identificacion.ForeColor = Drawing.Color.Black
        lbl_02_T05ICargo.ForeColor = Drawing.Color.Black
        lbl_02_T05IEmpresa.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing
        Nomotrarcontroles(True)
        Nomotrarcontroles2(True)
        MostrarMensaje("")
        'bHabilitarControles(True)
        filtro()
    End Sub

    Private Function bValidarCampos_Personal() As Boolean
        bValidarCampos_Personal = False

        lbl_02_T05Id_Estadistica.ForeColor = Drawing.Color.Red
        'lbl_02_T05Id_Personal.ForeColor = Drawing.Color.Red
        lbl_02_T05NombresApellidos.ForeColor = Drawing.Color.Red
        lbl_02_T05Identificacion.ForeColor = Drawing.Color.Red
        lbl_02_T05ICargo.ForeColor = Drawing.Color.Red
        lbl_02_T05IEmpresa.ForeColor = Drawing.Color.Red
        If Trim(txt_02_T03Id_Informe.Text) = "" Then
            txt_02_T05Id_Estadistica.Focus()
            MostrarMensaje("falta campo: Estadistica")
            Exit Function
        Else
            lbl_02_T05Id_Estadistica.ForeColor = Drawing.Color.Black
        End If
        'If Trim(txt_02_T05Id_Personal.Text) = "" Then
        '    txt_02_T05Id_Personal.Focus()
        '    MostrarMensaje("falta campo: Personal")
        '    Exit Function
        'Else
        '    lbl_02_T05Id_Personal.ForeColor = Drawing.Color.Black
        'End If
        If Trim(txt_02_T05NombresApellidos.Text) = "" Then
            txt_02_T05NombresApellidos.Focus()
            MostrarMensaje("falta campo: NombresApellidos")
            Exit Function
        Else
            lbl_02_T05NombresApellidos.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T05Identificacion.Text) = "" Then
            txt_02_T05Identificacion.Focus()
            MostrarMensaje("falta campo: Identificacion")
            Exit Function
        Else
            lbl_02_T05Identificacion.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T05ICargo.Text) = "" Then
            txt_02_T05ICargo.Focus()
            MostrarMensaje("falta campo: Cargo")
            Exit Function
        Else
            lbl_02_T05ICargo.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T05IEmpresa.Text) = "" Then
            txt_02_T05IEmpresa.Focus()
            MostrarMensaje("falta campo: Empresa")
            Exit Function
        Else
            lbl_02_T05IEmpresa.ForeColor = Drawing.Color.Black
        End If

        bValidarCampos_Personal = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL_Personal() As Boolean
        bValidarSQL_Personal = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_02_T05Id_Estadistica.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T05Id_Personal.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T05NombresApellidos.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T05Identificacion.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T05ICargo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T05IEmpresa.Text) Then Exit Function
        bValidarSQL_Personal = True
        MostrarMensaje("")
    End Function



    Private Sub Cargar_Registro_Personal()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object

        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla_Personal, sCamposTabla_Personal, sLlaves_Personal)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            Else
                txt_02_T05Id_Estadistica.Text = coleccionDatos(0)
                txt_02_T05Id_Personal.Text = coleccionDatos(1)
                txt_02_T05NombresApellidos.Text = coleccionDatos(2)
                txt_02_T05Identificacion.Text = coleccionDatos(3)
                txt_02_T05ICargo.Text = coleccionDatos(4)
                txt_02_T05IEmpresa.Text = coleccionDatos(5)
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


    Private Sub Guardar_Registro_Personal()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0

        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla_Personal, sLlaves_Personal)
        If lCantRegistros = 0 Then
            Datos_Modulo_Personal()
            txt_02_T05Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            txt_02_T05Id_Personal.Text = Iconsecutivo_Personal()
            Datos_Modulo_Personal()
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla_Personal, sCamposTabla_Personal, sCamposINS_Personal)
            If bSqlInsert = True Then
                Guardar_Registro_Personal_externo()
                MostrarMensaje("Se inserto nuevo registro Personal correctamente ", True)
                bHabilitarControles(False)
                limpiarCampos_Personal()
                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            txt_02_T05Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            Datos_Modulo_Personal()
            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla_Personal, sCamposUPD_Personal, sLlaves_Personal)
            If bSqlInsert = True Then
                Guardar_Registro_Personal_externo()
                MostrarMensaje("Se actualizo correctamente este registro", True)
                Registro_Procesos("Actualizar", clsAdminDb.Mostrar_Consulta)
                bHabilitarControles(False)
                limpiarCampos_Personal()
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


    Private Sub Guardar_Registro_Personal_externo()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0

        Dim sCodModulo_Personal_1 = _02_T05_1PERSONAL_EXTERNO.CodigoModulo
        Dim sNombreTabla_Personal_1 = _02_T05_1PERSONAL_EXTERNO.NombreTabla
        Dim sCamposTabla_Personal_1 = _02_T05_1PERSONAL_EXTERNO.CamposTabla
        Dim sCamposINS_Personal_1 = "'" & clsAdminDb.sRemoverHTML(txt_02_T05Id_Estadistica.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05Id_Personal.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05NombresApellidos.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05Identificacion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05ICargo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T05IEmpresa.Text) & "'"
        Dim sCamposUPD_Personal_1 = _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1NombresApellidos & "='" & clsAdminDb.sRemoverHTML(txt_02_T05NombresApellidos.Text) & "'" & "," & _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1Identificacion & "='" & clsAdminDb.sRemoverHTML(txt_02_T05Identificacion.Text) & "'" & "," & _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1ICargo & "='" & clsAdminDb.sRemoverHTML(txt_02_T05ICargo.Text) & "'" & "," & _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1IEmpresa & "='" & clsAdminDb.sRemoverHTML(txt_02_T05IEmpresa.Text) & "'"
        Dim sLlaves_Personal_1 = _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1Identificacion & "=  '" & txt_02_T05Identificacion.Text & "'"

        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla_Personal_1, sLlaves_Personal_1)
        If lCantRegistros = 0 Then
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla_Personal_1, sCamposTabla_Personal_1, sCamposINS_Personal_1)
            If bSqlInsert = True Then
                drop_02_T05NombresApellidosExterno.Items.Clear()

                Dim sCampos_tem8 = _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1Identificacion & "," & _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1NombresApellidos
                Dim stables_Tem8 = _02_T05_1PERSONAL_EXTERNO.NombreTabla
                Dim sllaves_Tem8 = ""
                clsAdminDb.drop_CargarCombox(drop_02_T05NombresApellidosExterno, stables_Tem8, sCampos_tem8, sllaves_Tem8, True,, _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1NombresApellidos)


            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla_Personal_1, sCamposUPD_Personal_1, sLlaves_Personal_1)
            If bSqlInsert = True Then
                drop_02_T05NombresApellidosExterno.Items.Clear()
                Dim sCampos_tem8 = _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1Identificacion & "," & _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1NombresApellidos
                Dim stables_Tem8 = _02_T05_1PERSONAL_EXTERNO.NombreTabla
                Dim sllaves_Tem8 = ""
                clsAdminDb.drop_CargarCombox(drop_02_T05NombresApellidosExterno, stables_Tem8, sCampos_tem8, sllaves_Tem8, True,, _02_T05_1PERSONAL_EXTERNO.Campo_02_T05_1NombresApellidos)



            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            End If
        End If
    End Sub
    Private Sub cargar_Tabla_Personal(Optional ByVal sLlaveTem As String = "")
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection
        'Selec _02_T01Fecha,_02_T01DocUsuarioRegistra, _02_T02Nombre,_02_T01LugarEvento 
        'From _02_T01REPORTE_CASOS, _02_T02TIPO_ACCION
        'Where _02_T02Id = _02_T01Id_TipoAccion


        Dim sCamposTablaLocal As String = "_02_T05Id_Estadistica,_02_T05Id_Personal,_02_T05NombresApellidos,_02_T05Identificacion,_02_T05ICargo,_02_T05IEmpresa"
        Dim sLlavesLocal As String = _02_T05PERSONAL.CampoLlave_02_T05Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        Dim sTablaLocal As String = _02_T05PERSONAL.NombreTabla
        Dim ArregloSingular() As String
        tabla_Personal.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
            If Trim("colocar filtro o validaciones") <> "" Then
                sLlavesLocal = sLlavesLocal
            End If
        End If
        Dim i As Integer = 0
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Personal = New LinkButton
                linkbutonHtml_Personal.ID = i & "2b-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_Personal.Click, AddressOf linkbutonHtml_Personal_Click
                linkbutonHtml_Personal.Text = ArregloSingular(2)
                CeldaHtml.Controls.Add(linkbutonHtml_Personal)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Personal = New LinkButton
                linkbutonHtml_Personal.ID = i & "2c-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_Personal.Click, AddressOf linkbutonHtml_Personal_Click
                linkbutonHtml_Personal.Text = ArregloSingular(3)
                CeldaHtml.Controls.Add(linkbutonHtml_Personal)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Personal = New LinkButton
                linkbutonHtml_Personal.ID = i & "2d-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_Personal.Click, AddressOf linkbutonHtml_Personal_Click
                linkbutonHtml_Personal.Text = ArregloSingular(4)
                CeldaHtml.Controls.Add(linkbutonHtml_Personal)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Personal = New LinkButton
                linkbutonHtml_Personal.ID = i & "2h-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_Personal.Click, AddressOf linkbutonHtml_Personal_Click
                linkbutonHtml_Personal.Text = ArregloSingular(5)
                CeldaHtml.Controls.Add(linkbutonHtml_Personal)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Eliminar_Personal = New LinkButton
                linkbutonHtml_Eliminar_Personal.ID = i & "2i-sep-" & ArregloSingular(0).ToString & "-sep-" & ArregloSingular(1).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                linkbutonHtml_Eliminar_Personal.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_Eliminar_Personal.ForeColor = Drawing.Color.DarkRed
                linkbutonHtml_Eliminar_Personal.Font.Bold = True
                linkbutonHtml_Eliminar_Personal.Font.Size = FontUnit.Point(8)
                linkbutonHtml_Eliminar_Personal.Enabled = False
                AddHandler linkbutonHtml_Eliminar_Personal.Click, AddressOf linkbutonHtml_Eliminar_Personal_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_Eliminar_Personal.Text = "Eliminar"
                CeldaHtml.Controls.Add(linkbutonHtml_Eliminar_Personal)
                FilaHtml.Cells.Add(CeldaHtml)


                i = i + 1


                tabla_Personal.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)


            End If
        End If
    End Sub

    Private Sub linkbutonHtml_Eliminar_Personal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_Eliminar_Personal.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(2).ToString
        Dim sLlaves_Tem = _02_T05PERSONAL.CampoLlave_02_T05Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _02_T05PERSONAL.CampoLlave_02_T05Id_Personal & "='" & sllave1 & "'"


        Dim bSqlDelete = clsAdminDb.sql_Delete(_02_T05PERSONAL.NombreTabla, sLlaves_Tem)
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

        filtro()
    End Sub


    Private Sub linkbutonHtml_Personal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_Personal.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(2).ToString
        'Dim sllave2 As String = TestArray(2).ToString
        'Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        sLlaves_Personal = _02_T05PERSONAL.CampoLlave_02_T05Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        sLlaves_Personal = sLlaves_Personal & " and " & _02_T05PERSONAL.CampoLlave_02_T05Id_Personal & "='" & sllave1 & "'"
        Cargar_Registro_Personal()
        filtro()
    End Sub

    Private Sub btn_Guardar_Personal_Click(sender As Object, e As EventArgs) Handles btn_Guardar_Personal.Click
        If bValidarCampos_Personal() = False Then Exit Sub
        If bValidarSQL_Personal() = False Then Exit Sub
        Guardar_Registro_Personal()
    End Sub

    Private Function Iconsecutivo_Personal() As Integer
        Dim iVigencia As String = Now.Year
        Dim sConsecutivo As String = ""
        sConsecutivo = clsAdminDb.sql_Max(_02_T05PERSONAL.NombreTabla, "cast(" & _02_T05PERSONAL.CampoLlave_02_T05Id_Personal & " as integer)", _02_T05PERSONAL.CampoLlave_02_T05Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'")
        If sConsecutivo = "0" Then
            sConsecutivo = "1"
        Else
            sConsecutivo = Val(sConsecutivo) + 1
        End If
        Iconsecutivo_Personal = sConsecutivo
    End Function

    Protected Sub chkempleadoSeringtec_CheckedChanged(sender As Object, e As EventArgs) Handles chkempleadoSeringtec.CheckedChanged
        If chkempleadoSeringtec.Checked Then
            chkempleadoexterno.Checked = False
            Nomotrarcontroles2(False)
            Nomotrarcontroles(False)
        Else
            Nomotrarcontroles(True)
        End If
    End Sub
    Protected Sub chkempleadoexterno_CheckedChanged(sender As Object, e As EventArgs) Handles chkempleadoexterno.CheckedChanged
        If chkempleadoexterno.Checked Then
            chkempleadoSeringtec.Checked = False
            Nomotrarcontroles(False)
            Nomotrarcontroles2(False)
        Else
            Nomotrarcontroles2(True)
        End If
    End Sub

    Private Sub Nomotrarcontroles(ByVal bMostrar As Boolean)
        txt_02_T05NombresApellidos.Visible = True
        drop_02_T05NombresApellidos.Visible = False
        drop_02_T05NombresApellidosExterno.Visible = False
        txt_02_T05ICargo.Enabled = True
        txt_02_T05IEmpresa.Enabled = True
        txt_02_T05Identificacion.Enabled = True

        If bMostrar = False Then
            txt_02_T05NombresApellidos.Visible = bMostrar
            drop_02_T05NombresApellidos.Visible = True
            txt_02_T05ICargo.Enabled = bMostrar
            txt_02_T05IEmpresa.Enabled = bMostrar
            txt_02_T05Identificacion.Enabled = bMostrar
        Else
            txt_02_T05NombresApellidos.Visible = True
            drop_02_T05NombresApellidos.Visible = False
            txt_02_T05ICargo.Enabled = True
            txt_02_T05IEmpresa.Enabled = True
            txt_02_T05Identificacion.Enabled = True

        End If

    End Sub
    Private Sub Nomotrarcontroles2(ByVal bMostrar As Boolean)
        txt_02_T05NombresApellidos.Visible = True
        drop_02_T05NombresApellidosExterno.Visible = False
        drop_02_T05NombresApellidos.Visible = False
        txt_02_T05ICargo.Enabled = True
        txt_02_T05IEmpresa.Enabled = True
        txt_02_T05Identificacion.Enabled = True

        If bMostrar = False Then
            txt_02_T05NombresApellidos.Visible = bMostrar
            drop_02_T05NombresApellidosExterno.Visible = True
            txt_02_T05ICargo.Enabled = bMostrar
            txt_02_T05IEmpresa.Enabled = bMostrar
            txt_02_T05Identificacion.Enabled = bMostrar
        Else
            txt_02_T05NombresApellidos.Visible = True
            drop_02_T05NombresApellidosExterno.Visible = False
            txt_02_T05ICargo.Enabled = True
            txt_02_T05IEmpresa.Enabled = True
            txt_02_T05Identificacion.Enabled = True

        End If

    End Sub


    Private Sub drop_02_T05NombresApellidos_TextChanged(sender As Object, e As EventArgs) Handles drop_02_T05NombresApellidos.TextChanged
        CargarDatosFuncionario()
    End Sub



    Private Sub CargarDatosFuncionario()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection



        Dim sCamposTablaLocal As String = _02_T05PERSONAL.CamposTabla
        Dim sLlavesLocal As String = _02_T05PERSONAL.CampoLlave_02_T05Id_Personal & "<>''"
        Dim sTablaLocal As String = _02_T05PERSONAL.NombreTabla
        Dim ArregloSingular() As String
        Dim sCosnulta As String = " select _01_T12NumDoc,_01_T12NombreApellidos,(select top 1 _01_T24Nombre from SERINGTEC.dbo._01_T24CARGOS where _01_T12Cargo=_01_T24Codigo )"
        sCosnulta = sCosnulta & "from SERINGTEC.dbo._01_T12FUNCIONARIOS "
        sCosnulta = sCosnulta & "where _01_T12NumDoc='" & drop_02_T05NombresApellidos.Text & "'"
        sCosnulta = sCosnulta & " and _01_T12Activo='SI'"

        coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                txt_02_T05Identificacion.Text = ArregloSingular(0)
                txt_02_T05NombresApellidos.Text = ArregloSingular(1)
                txt_02_T05ICargo.Text = ArregloSingular(2)
                txt_02_T05IEmpresa.Text = "SERINGTEC"




            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)

            End If
        End If
    End Sub




    Private Sub CargarDatosFuncionario2()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection



        Dim ArregloSingular() As String
        Dim sCosnulta As String = " select _02_T05_1NombresApellidos,_02_T05_1Identificacion,_02_T05_1ICargo,_02_T05_1IEmpresa "
        sCosnulta = sCosnulta & "from _02_T05_1PERSONAL_EXTERNO "
        sCosnulta = sCosnulta & "where _02_T05_1Identificacion='" & drop_02_T05NombresApellidosExterno.Text & "'"


        coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                txt_02_T05Identificacion.Text = ArregloSingular(1)
                txt_02_T05NombresApellidos.Text = ArregloSingular(0)
                txt_02_T05ICargo.Text = ArregloSingular(2)
                txt_02_T05IEmpresa.Text = ArregloSingular(3)




            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)

            End If
        End If
    End Sub

    '********************** TERMINA TODO LO E PERSONAL

    '********************** comienza todo lo de SEGURIDAD VIAL

    Dim sCodModulo_SeguridadVial As String
    Dim sNombreTabla_SeguridadVial As String
    Dim sCamposTabla_SeguridadVial As String
    Dim sCamposINS_SeguridadVial As String
    Dim sCamposUPD_SeguridadVial As String
    Dim sLlaves_SeguridadVial As String

    Private Sub Datos_Modulo_SeguridadVial()
        sCodModulo_SeguridadVial = _02_T06SEGURIDADVIAL.CodigoModulo
        sNombreTabla_SeguridadVial = _02_T06SEGURIDADVIAL.NombreTabla
        sCamposTabla_SeguridadVial = _02_T06SEGURIDADVIAL.CamposTabla
        sCamposINS_SeguridadVial = "'" & clsAdminDb.sRemoverHTML(txt_02_T06Id_Estadistica.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06Id_SeguridadVial.Text) & "'" & "," & "'" & drop_02_T06TipoVehiculo.Text & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06PlacaVehiculo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06KmInicial.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06KmFinal.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06TotalKm.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06EmpresaAffiliacionVehiculo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06FUEC.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06NombreConductor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T06DocumentoConductor.Text) & "'"
        sCamposUPD_SeguridadVial = _02_T06SEGURIDADVIAL.Campo_02_T06TipoVehiculo & "='" & drop_02_T06TipoVehiculo.Text & "'" & "," & _02_T06SEGURIDADVIAL.Campo_02_T06PlacaVehiculo & "='" & clsAdminDb.sRemoverHTML(txt_02_T06PlacaVehiculo.Text) & "'" & "," & _02_T06SEGURIDADVIAL.Campo_02_T06KmInicial & "='" & clsAdminDb.sRemoverHTML(txt_02_T06KmInicial.Text) & "'" & "," & _02_T06SEGURIDADVIAL.Campo_02_T06KmFinal & "='" & clsAdminDb.sRemoverHTML(txt_02_T06KmFinal.Text) & "'" & "," & _02_T06SEGURIDADVIAL.Campo_02_T06TotalKm & "='" & clsAdminDb.sRemoverHTML(txt_02_T06TotalKm.Text) & "'" & "," & _02_T06SEGURIDADVIAL.Campo_02_T06EmpresaAffiliacionVehiculo & "='" & clsAdminDb.sRemoverHTML(txt_02_T06EmpresaAffiliacionVehiculo.Text) & "'" & "," & _02_T06SEGURIDADVIAL.Campo_02_T06FUEC & "='" & clsAdminDb.sRemoverHTML(txt_02_T06FUEC.Text) & "'" & "," & _02_T06SEGURIDADVIAL.Campo_02_T06NombreConductor & "='" & clsAdminDb.sRemoverHTML(txt_02_T06NombreConductor.Text) & "'" & "," & _02_T06SEGURIDADVIAL.Campo_02_T06DocumentoConductor & "='" & clsAdminDb.sRemoverHTML(txt_02_T06DocumentoConductor.Text) & "'"
        sLlaves_SeguridadVial = _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_Estadistica & "=  '" & txt_02_T06Id_Estadistica.Text & "'" & " AND " & _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_SeguridadVial & "=  '" & txt_02_T06Id_SeguridadVial.Text & "'"
    End Sub

    Private Sub limpiarCampos_SeguridadVial()
        txt_02_T06TotalKm.Enabled = False
        txt_02_T06Id_Estadistica.Text = Nothing
        txt_02_T06Id_SeguridadVial.Text = Nothing
        drop_02_T06TipoVehiculo.Text = Nothing
        txt_02_T06PlacaVehiculo.Text = Nothing
        txt_02_T06KmInicial.Text = Nothing
        txt_02_T06KmFinal.Text = Nothing
        txt_02_T06TotalKm.Text = Nothing
        txt_02_T06EmpresaAffiliacionVehiculo.Text = Nothing
        txt_02_T06FUEC.Text = Nothing
        txt_02_T06NombreConductor.Text = Nothing
        txt_02_T06DocumentoConductor.Text = Nothing

        'lbl_02_T06Id_Estadistica.ForeColor = Drawing.Color.Black
        lbl_02_T06Id_SeguridadVial.ForeColor = Drawing.Color.Black
        lbl_02_T06TipoVehiculo.ForeColor = Drawing.Color.Black
        lbl_02_T06PlacaVehiculo.ForeColor = Drawing.Color.Black
        lbl_02_T06KmInicial.ForeColor = Drawing.Color.Black
        lbl_02_T06KmFinal.ForeColor = Drawing.Color.Black
        lbl_02_T06TotalKm.ForeColor = Drawing.Color.Black
        lbl_02_T06EmpresaAffiliacionVehiculo.ForeColor = Drawing.Color.Black
        lbl_02_T06FUEC.ForeColor = Drawing.Color.Black
        lbl_02_T06NombreConductor.ForeColor = Drawing.Color.Black
        lbl_02_T06DocumentoConductor.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing

        MostrarMensaje("")
        'bHabilitarControles(True)
        filtro()

    End Sub

    Private Function bValidarCampos_SeguridadVial() As Boolean
        bValidarCampos_SeguridadVial = False

        'lbl_02_T06Id_Estadistica.ForeColor = Drawing.Color.Red
        lbl_02_T06Id_SeguridadVial.ForeColor = Drawing.Color.Red
        lbl_02_T06TipoVehiculo.ForeColor = Drawing.Color.Red
        lbl_02_T06PlacaVehiculo.ForeColor = Drawing.Color.Red
        lbl_02_T06KmInicial.ForeColor = Drawing.Color.Red
        lbl_02_T06KmFinal.ForeColor = Drawing.Color.Red
        lbl_02_T06TotalKm.ForeColor = Drawing.Color.Red
        lbl_02_T06EmpresaAffiliacionVehiculo.ForeColor = Drawing.Color.Red
        lbl_02_T06FUEC.ForeColor = Drawing.Color.Red
        lbl_02_T06NombreConductor.ForeColor = Drawing.Color.Red
        lbl_02_T06DocumentoConductor.ForeColor = Drawing.Color.Red
        'If Trim(txt_02_T06Id_Estadistica.Text) = "" Then
        '    txt_02_T06Id_Estadistica.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06Id_Estadistica")
        '    Exit Function
        'Else
        '    'lbl_02_T06Id_Estadistica.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T06Id_SeguridadVial.Text) = "" Then
        '    txt_02_T06Id_SeguridadVial.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06Id_SeguridadVial")
        '    Exit Function
        'Else
        '    lbl_02_T06Id_SeguridadVial.ForeColor = Drawing.Color.Black
        'End If
        If Trim(drop_02_T06TipoVehiculo.Text) = "" Then
            drop_02_T06TipoVehiculo.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06TipoVehiculo")
            Exit Function
        Else
            lbl_02_T06TipoVehiculo.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T06PlacaVehiculo.Text) = "" Then
            txt_02_T06PlacaVehiculo.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06PlacaVehiculo")
            Exit Function
        Else
            lbl_02_T06PlacaVehiculo.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T06KmInicial.Text) = "" Then
            txt_02_T06KmInicial.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06KmInicial")
            Exit Function
        Else
            lbl_02_T06KmInicial.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T06KmFinal.Text) = "" Then
            txt_02_T06KmFinal.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06KmFinal")
            Exit Function
        Else
            lbl_02_T06KmFinal.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T06TotalKm.Text) = "" Then
            txt_02_T06TotalKm.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06TotalKm")
            Exit Function
        Else
            lbl_02_T06TotalKm.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T06EmpresaAffiliacionVehiculo.Text) = "" Then
            txt_02_T06EmpresaAffiliacionVehiculo.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06EmpresaAffiliacionVehiculo")
            Exit Function
        Else
            lbl_02_T06EmpresaAffiliacionVehiculo.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T06FUEC.Text) = "" Then
            txt_02_T06FUEC.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06FUEC")
            Exit Function
        Else
            lbl_02_T06FUEC.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T06NombreConductor.Text) = "" Then
            txt_02_T06NombreConductor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06NombreConductor")
            Exit Function
        Else
            lbl_02_T06NombreConductor.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T06DocumentoConductor.Text) = "" Then
            txt_02_T06DocumentoConductor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 06DocumentoConductor")
            Exit Function
        Else
            lbl_02_T06DocumentoConductor.ForeColor = Drawing.Color.Black
        End If

        If Val(txt_02_T06KmInicial.Text) > Val(txt_02_T06KmFinal.Text) Then
            MostrarMensaje("el valor del km inicial debe ser menor al valor del km Final")
            txt_02_T06KmInicial.Focus()
            Exit Function

        End If



        bValidarCampos_SeguridadVial = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL_SeguridadVial() As Boolean
        bValidarSQL_SeguridadVial = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_02_T06Id_Estadistica.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06Id_SeguridadVial.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06PlacaVehiculo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06KmInicial.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06KmFinal.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06TotalKm.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06EmpresaAffiliacionVehiculo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06FUEC.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06NombreConductor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T06DocumentoConductor.Text) Then Exit Function

        bValidarSQL_SeguridadVial = True
        MostrarMensaje("")
    End Function



    Private Sub Cargar_Registro_SeguridadVial()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object

        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla_SeguridadVial, sCamposTabla_SeguridadVial, sLlaves_SeguridadVial)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            Else
                txt_02_T06Id_Estadistica.Text = coleccionDatos(0)
                txt_02_T06Id_SeguridadVial.Text = coleccionDatos(1)
                drop_02_T06TipoVehiculo.Text = coleccionDatos(2)
                txt_02_T06PlacaVehiculo.Text = coleccionDatos(3)
                txt_02_T06KmInicial.Text = coleccionDatos(4)
                txt_02_T06KmFinal.Text = coleccionDatos(5)
                txt_02_T06TotalKm.Text = coleccionDatos(6)
                txt_02_T06EmpresaAffiliacionVehiculo.Text = coleccionDatos(7)
                txt_02_T06FUEC.Text = coleccionDatos(8)
                txt_02_T06NombreConductor.Text = coleccionDatos(9)
                txt_02_T06DocumentoConductor.Text = coleccionDatos(10)
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


    Private Sub Guardar_Registro_SeguridadVial()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0

        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla_SeguridadVial, sLlaves_SeguridadVial)
        If lCantRegistros = 0 Then
            Datos_Modulo_SeguridadVial()
            txt_02_T06Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            txt_02_T06Id_SeguridadVial.Text = Iconsecutivo_SeguridadVial()
            Datos_Modulo_SeguridadVial()
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla_SeguridadVial, sCamposTabla_SeguridadVial, sCamposINS_SeguridadVial)
            If bSqlInsert = True Then
                MostrarMensaje("Se inserto nuevo registro Personal correctamente ", True)
                bHabilitarControles(False)
                limpiarCampos_SeguridadVial()
                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            txt_02_T05Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            Datos_Modulo_SeguridadVial()
            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla_SeguridadVial, sCamposUPD_SeguridadVial, sLlaves_SeguridadVial)
            If bSqlInsert = True Then
                MostrarMensaje("Se actualizo correctamente este registro", True)
                Registro_Procesos("Actualizar", clsAdminDb.Mostrar_Consulta)
                bHabilitarControles(False)
                limpiarCampos_SeguridadVial()
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
    Private Sub cargar_Tabla_SeguridadVial(Optional ByVal sLlaveTem As String = "")
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection
        'Selec _02_T01Fecha,_02_T01DocUsuarioRegistra, _02_T02Nombre,_02_T01LugarEvento 
        'From _02_T01REPORTE_CASOS, _02_T02TIPO_ACCION
        'Where _02_T02Id = _02_T01Id_TipoAccion

        Dim sCamposTablaLocal As String = "(select top 1 _02_T19Nombre from _02_T19TIPOVEHICULO where _02_T19Codigo=_02_T06TipoVehiculo ),_02_T06PlacaVehiculo,_02_T06KmInicial,_02_T06KmFinal,_02_T06TotalKm,_02_T06EmpresaAffiliacionVehiculo,_02_T06FUEC,_02_T06NombreConductor,_02_T06DocumentoConductor,_02_T06Id_Estadistica,_02_T06Id_SeguridadVial"
        '_02_T06TipoVehiculo,_02_T06PlacaVehiculo,_02_T06KmInicial,_02_T06KmFinal,_02_T06TotalKm,_02_T06EmpresaAffiliacionVehiculo,_02_T06FUEC,_02_T06NombreConductor,_02_T06DocumentoConductor
        Dim sLlavesLocal As String = _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        Dim sTablaLocal As String = _02_T06SEGURIDADVIAL.NombreTabla
        Dim ArregloSingular() As String
        tabla_SeguridadVial.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
            If Trim("colocar filtro o validaciones") <> "" Then
                sLlavesLocal = sLlavesLocal
            End If
        End If
        Dim i As Integer = 0
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow
                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3a-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(0)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3b-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(1)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3c-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(2)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3d-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(3)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3e-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(4)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3f-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(5)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3g-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(6)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3h-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(7)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_SeguridadVial = New LinkButton
                linkbutonHtml_SeguridadVial.ID = i & "3j-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_SeguridadVial.Click, AddressOf linkbutonHtml_SeguridadVial_Click
                linkbutonHtml_SeguridadVial.Text = ArregloSingular(8)
                CeldaHtml.Controls.Add(linkbutonHtml_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)



                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Eliminar_SeguridadVial = New LinkButton
                linkbutonHtml_Eliminar_SeguridadVial.ID = i & "3k-sep-" & ArregloSingular(9).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                linkbutonHtml_Eliminar_SeguridadVial.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_Eliminar_SeguridadVial.ForeColor = Drawing.Color.DarkRed
                linkbutonHtml_Eliminar_SeguridadVial.Font.Bold = True
                linkbutonHtml_Eliminar_SeguridadVial.Font.Size = FontUnit.Point(8)
                linkbutonHtml_Eliminar_SeguridadVial.Enabled = False
                AddHandler linkbutonHtml_Eliminar_SeguridadVial.Click, AddressOf linkbutonHtml_Eliminar_SeguridadVial_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_Eliminar_SeguridadVial.Text = "Eliminar"
                CeldaHtml.Controls.Add(linkbutonHtml_Eliminar_SeguridadVial)
                FilaHtml.Cells.Add(CeldaHtml)


                i = i + 1


                tabla_SeguridadVial.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)


            End If
        End If
    End Sub

    Private Sub linkbutonHtml_Eliminar_SeguridadVial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_Eliminar_SeguridadVial.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(2).ToString
        Dim sLlaves_Tem = _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_Estadistica & "='" & TestArray(1).ToString & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_SeguridadVial & "='" & TestArray(2).ToString & "'"


        Dim bSqlDelete = clsAdminDb.sql_Delete(_02_T06SEGURIDADVIAL.NombreTabla, sLlaves_Tem)
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

        filtro()
    End Sub


    Private Sub linkbutonHtml_SeguridadVial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_SeguridadVial.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(2).ToString
        'Dim sllave2 As String = TestArray(2).ToString
        'Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        sLlaves_SeguridadVial = _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        sLlaves_SeguridadVial = sLlaves_SeguridadVial & " and " & _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_SeguridadVial & "='" & sllave1 & "'"
        Cargar_Registro_SeguridadVial()
        filtro()
    End Sub

    Private Sub btn_Guardar_RegistroFoto_Click(sender As Object, e As EventArgs) Handles btn_Guardar_SeguridadVial.Click
        If bValidarCampos_SeguridadVial() = False Then Exit Sub
        If bValidarSQL_SeguridadVial() = False Then Exit Sub
        Guardar_Registro_SeguridadVial()
    End Sub

    Private Function Iconsecutivo_SeguridadVial() As Integer
        Dim iVigencia As String = Now.Year
        Dim sConsecutivo As String = ""
        sConsecutivo = clsAdminDb.sql_Max(_02_T06SEGURIDADVIAL.NombreTabla, "cast(" & _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_SeguridadVial & " as integer)", _02_T06SEGURIDADVIAL.CampoLlave_02_T06Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'")
        If sConsecutivo = "0" Then
            sConsecutivo = "1"
        Else
            sConsecutivo = Val(sConsecutivo) + 1
        End If
        Iconsecutivo_SeguridadVial = sConsecutivo
    End Function

    Private Sub txt_02_T06KmFinal_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T06KmFinal.TextChanged
        CalculoKMTotal()
    End Sub

    Private Sub txt_02_T06KmInicial_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T06KmInicial.TextChanged
        CalculoKMTotal()
    End Sub

    Private Sub CalculoKMTotal()
        If Val(txt_02_T06KmInicial.Text) > Val(txt_02_T06KmFinal.Text) Then
            MostrarMensaje("el valor del km inicial debe ser menor al valor del km Final")
        End If

        txt_02_T06TotalKm.Text = Val(txt_02_T06KmFinal.Text) - Val(txt_02_T06KmInicial.Text)
    End Sub




    '********************** TERMINA TODO LO DE SEGURIDAD VIALL

    '********************** comienza todo lo de Registro fotografico 

    Dim sCodModulo_RegistroFoto As String
    Dim sNombreTabla_RegistroFoto As String
    Dim sCamposTabla_RegistroFoto As String
    Dim sCamposINS_RegistroFoto As String
    Dim sCamposUPD_RegistroFoto As String
    Dim sLlaves_RegistroFoto As String

    Private Sub Datos_Modulo_RegistroFoto()
        sCodModulo_RegistroFoto = _02_T07REGISTROfOTOGRAFICO.CodigoModulo
        sNombreTabla_RegistroFoto = _02_T07REGISTROfOTOGRAFICO.NombreTabla
        sCamposTabla_RegistroFoto = _02_T07REGISTROfOTOGRAFICO.CamposTabla
        sCamposINS_RegistroFoto = "'" & clsAdminDb.sRemoverHTML(txt_02_T07Id_Estadistica.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Id_registroFotografico.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Id_BD.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Id_BD_Item.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T07Observaciones.Text) & "'"
        sCamposUPD_RegistroFoto = _02_T07REGISTROfOTOGRAFICO.Campo_02_T07Id_BD_Item & "='" & clsAdminDb.sRemoverHTML(txt_02_T07Id_BD_Item.Text) & "'" & "," & _02_T07REGISTROfOTOGRAFICO.Campo_02_T07Fecha & "='" & clsAdminDb.sRemoverHTML(txt_02_T07Fecha.Text) & "'" & "," & _02_T07REGISTROfOTOGRAFICO.Campo_02_T07Observaciones & "='" & clsAdminDb.sRemoverHTML(txt_02_T07Observaciones.Text) & "'"
        sLlaves_RegistroFoto = _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_Estadistica & "=  '" & txt_02_T07Id_Estadistica.Text & "'" & " AND " & _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_registroFotografico & "=  '" & txt_02_T07Id_registroFotografico.Text & "'" & " AND " & _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_BD & "=  '" & txt_02_T07Id_BD.Text & "'"
    End Sub

    Private Sub limpiarCampos_RegistroFoto()

        txt_02_T07Id_Estadistica.Text = Nothing
        txt_02_T07Id_registroFotografico.Text = ""
        txt_02_T07Id_BD.Text = "_04_HSE_REPORTESDECASOS_FOTOS_1"
        txt_02_T07Id_BD_Item.Text = "_02_T07REGISTROfOTOGRAFICO_1"
        txt_02_T07Fecha.Text = Nothing
        txt_02_T07Observaciones.Text = Nothing

        'lbl_02_T07Id_Estadistica.ForeColor = Drawing.Color.Black
        lbl_02_T07Id_registroFotografico.ForeColor = Drawing.Color.Black
        lbl_02_T07Id_BD.ForeColor = Drawing.Color.Black
        lbl_02_T07Id_BD_Item.ForeColor = Drawing.Color.Black
        lbl_02_T07Fecha.ForeColor = Drawing.Color.Black
        lbl_02_T07Observaciones.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing

        MostrarMensaje("")
        'bHabilitarControles(True)
        filtro()


    End Sub

    Private Function bValidarCampos_RegistroFoto() As Boolean
        bValidarCampos_RegistroFoto = False

        'lbl_02_T07Id_Estadistica.ForeColor = Drawing.Color.Red
        'lbl_02_T07Id_registroFotografico.ForeColor = Drawing.Color.Red
        lbl_02_T07Id_BD.ForeColor = Drawing.Color.Red
        lbl_02_T07Id_BD_Item.ForeColor = Drawing.Color.Red
        lbl_02_T07Fecha.ForeColor = Drawing.Color.Red
        lbl_02_T07Observaciones.ForeColor = Drawing.Color.Red
        'If Trim(txt_02_T07Id_Estadistica.Text) = "" Then
        '    txt_02_T07Id_Estadistica.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Id_Estadistica")
        '    Exit Function
        'Else
        '    lbl_02_T07Id_Estadistica.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T07Id_registroFotografico.Text) = "" Then
        '    txt_02_T07Id_registroFotografico.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Id_registroFotografico")
        '    Exit Function
        'Else
        '    lbl_02_T07Id_registroFotografico.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T07Id_BD.Text) = "" Then
        '    txt_02_T07Id_BD.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Id_BD")
        '    Exit Function
        'Else
        '    lbl_02_T07Id_BD.ForeColor = Drawing.Color.Black
        'End If
        'If Trim(txt_02_T07Id_BD_Item.Text) = "" Then
        '    txt_02_T07Id_BD_Item.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 07Id_BD_Item")
        '    Exit Function
        'Else
        '    lbl_02_T07Id_BD_Item.ForeColor = Drawing.Color.Black
        'End If
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

        bValidarCampos_RegistroFoto = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL_RegistroFoto() As Boolean
        bValidarSQL_RegistroFoto = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_02_T07Id_Estadistica.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Id_registroFotografico.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Id_BD.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Id_BD_Item.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Fecha.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T07Observaciones.Text) Then Exit Function
        bValidarSQL_RegistroFoto = True
        MostrarMensaje("")

    End Function



    Private Sub Cargar_Registro_RegistroFoto()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object

        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla_RegistroFoto, sCamposTabla_RegistroFoto, sLlaves_RegistroFoto)
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
                filtro()
                bHabilitarControles(False)

            End If
        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)
            Else
                'MostrarMensaje("No se encontraron datos" )
            End If
        End If
    End Sub


    Private Sub Guardar_Registro_RegistroFoto()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0

        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla_RegistroFoto, sLlaves_RegistroFoto)
        If lCantRegistros = 0 Then
            Datos_Modulo_RegistroFoto()
            txt_02_T07Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            txt_02_T07Id_registroFotografico.Text = Iconsecutivo_RegistroFoto()
            Datos_Modulo_RegistroFoto()
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla_RegistroFoto, sCamposTabla_RegistroFoto, sCamposINS_RegistroFoto)
            If bSqlInsert = True Then
                MostrarMensaje("Se inserto nuevo registro Personal correctamente ", True)
                bHabilitarControles(False)
                limpiarCampos_RegistroFoto()
                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            txt_02_T05Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            Datos_Modulo_RegistroFoto()
            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla_RegistroFoto, sCamposUPD_RegistroFoto, sLlaves_RegistroFoto)
            If bSqlInsert = True Then
                MostrarMensaje("Se actualizo correctamente este registro", True)
                Registro_Procesos("Actualizar", clsAdminDb.Mostrar_Consulta)
                bHabilitarControles(False)
                limpiarCampos_RegistroFoto()
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
    Private Sub cargar_Tabla_RegistroFoto(Optional ByVal sLlaveTem As String = "")
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection


        Dim sCamposTablaLocal As String = "_02_T07NombreFoto,_02_T07Fecha,_02_T07Observaciones,_02_T07ExtensionFoto,02_T07Id_Estadistica,_02_T07Id_registroFotografico,_02_T07Id_BD,_02_T07Id_BD_Item"
        Dim sLlavesLocal As String = _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        Dim sTablaLocal As String = _02_T07REGISTROfOTOGRAFICO.NombreTabla
        Dim ArregloSingular() As String
        tabla_RegistroFoto.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
            If Trim("colocar filtro o validaciones") <> "" Then
                sLlavesLocal = sLlavesLocal
            End If
        End If
        Dim i As Integer = 0
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow
                CeldaHtml = New HtmlTableCell
                linkbutonHtml_RegistroFoto = New LinkButton
                linkbutonHtml_RegistroFoto.ID = i & "4a-sep-" & ArregloSingular(4).ToString & "-sep-" & ArregloSingular(5).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_RegistroFoto.Click, AddressOf linkbutonHtml_RegistroFoto_Click
                linkbutonHtml_RegistroFoto.Text = ArregloSingular(0)
                CeldaHtml.Controls.Add(linkbutonHtml_RegistroFoto)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_RegistroFoto = New LinkButton
                linkbutonHtml_RegistroFoto.ID = i & "4b-sep-" & ArregloSingular(4).ToString & "-sep-" & ArregloSingular(5).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_RegistroFoto.Click, AddressOf linkbutonHtml_RegistroFoto_Click
                linkbutonHtml_RegistroFoto.Text = ArregloSingular(1)
                CeldaHtml.Controls.Add(linkbutonHtml_RegistroFoto)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_RegistroFoto = New LinkButton
                linkbutonHtml_RegistroFoto.ID = i & "4c-sep-" & ArregloSingular(4).ToString & "-sep-" & ArregloSingular(5).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_RegistroFoto.Click, AddressOf linkbutonHtml_RegistroFoto_Click
                linkbutonHtml_RegistroFoto.Text = ArregloSingular(2)
                CeldaHtml.Controls.Add(linkbutonHtml_RegistroFoto)
                FilaHtml.Cells.Add(CeldaHtml)


                linkbutonHtml_ver_RegistroFoto = New LinkButton
                linkbutonHtml_ver_RegistroFoto.ID = i & "4g-sep-" & ArregloSingular(4).ToString & "-sep-" & ArregloSingular(5).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                linkbutonHtml_ver_RegistroFoto.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_ver_RegistroFoto.ForeColor = Drawing.Color.Green
                linkbutonHtml_ver_RegistroFoto.Font.Bold = True
                linkbutonHtml_ver_RegistroFoto.Font.Size = FontUnit.Point(8)
                AddHandler linkbutonHtml_ver_RegistroFoto.Click, AddressOf linkbutonHtml_ver_RegistroFoto_Click
                linkbutonHtml_ver_RegistroFoto.Text = "Ver foto"



                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Eliminar_RegistroFoto = New LinkButton
                linkbutonHtml_Eliminar_RegistroFoto.ID = i & "4h-sep-" & ArregloSingular(4).ToString & "-sep-" & ArregloSingular(5).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                linkbutonHtml_Eliminar_RegistroFoto.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_Eliminar_RegistroFoto.ForeColor = Drawing.Color.DarkRed
                linkbutonHtml_Eliminar_RegistroFoto.Enabled = False
                linkbutonHtml_Eliminar_RegistroFoto.Font.Bold = True
                linkbutonHtml_Eliminar_RegistroFoto.Font.Size = FontUnit.Point(8)
                AddHandler linkbutonHtml_Eliminar_RegistroFoto.Click, AddressOf linkbutonHtml_Eliminar_RegistroFoto_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_Eliminar_RegistroFoto.Text = "Eliminar"


                Dim lbltem As New Label
                lbltem.Text = " / "

                CeldaHtml.Controls.AddAt(0, linkbutonHtml_ver_RegistroFoto)
                CeldaHtml.Controls.AddAt(1, lbltem)
                CeldaHtml.Controls.AddAt(2, linkbutonHtml_Eliminar_RegistroFoto)

                FilaHtml.Cells.Add(CeldaHtml)


                i = i + 1


                tabla_RegistroFoto.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)


            End If
        End If
    End Sub

    Private Sub linkbutonHtml_Eliminar_RegistroFoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_Eliminar_SeguridadVial.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(2).ToString
        Dim sLlaves_Tem = _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_registroFotografico & "='" & sllave1 & "'"


        Dim bSqlDelete = clsAdminDb.sql_Delete(_02_T07REGISTROfOTOGRAFICO.NombreTabla, sLlaves_Tem)
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

        filtro()

    End Sub

    Private Sub linkbutonHtml_ver_RegistroFoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_ver_RegistroFoto.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(2).ToString
        Dim sLlaves_Tem = _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_registroFotografico & "='" & sllave1 & "'"


        Dim sNombreTabla_Tem = _02_T07REGISTROfOTOGRAFICO.NombreTabla
        Dim sCamposTabla_Tem = "_02_T07imagenFoto,_02_T07NombreFoto,_02_T07ExtensionFoto"
        'Dim sLlaves_Tem = _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T02NumDocRecu & "=  '" & txt_02_T01NumDoc.Text & "'" & " AND " & _02_T02DOCCONTRATACION.Campo_02_T02IDEnvio & "=  '" & sNumProceso & "'" & " AND " & _02_T02DOCCONTRATACION.CampoLlave_02_T02CodDocumento & "=  '0406'"

        ExportarArchivo(sNombreTabla_Tem, sCamposTabla_Tem, sLlaves_Tem)


        filtro()
    End Sub

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




    Private Sub linkbutonHtml_RegistroFoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_SeguridadVial.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(1).ToString
        'Dim sllave2 As String = TestArray(2).ToString
        'Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        sLlaves_RegistroFoto = _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        sLlaves_RegistroFoto = sLlaves_RegistroFoto & " and " & _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_registroFotografico & "='" & sllave1 & "'"
        Cargar_Registro_RegistroFoto()
        filtro()
    End Sub


    Private Function Iconsecutivo_RegistroFoto() As Integer
        Dim iVigencia As String = Now.Year
        Dim sConsecutivo As String = ""
        sConsecutivo = clsAdminDb.sql_Max(_02_T07REGISTROfOTOGRAFICO.NombreTabla, "cast(" & _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_registroFotografico & " as integer)", _02_T07REGISTROfOTOGRAFICO.CampoLlave_02_T07Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'")
        If sConsecutivo = "0" Then
            sConsecutivo = "1"
        Else
            sConsecutivo = Val(sConsecutivo) + 1
        End If
        Iconsecutivo_RegistroFoto = sConsecutivo
    End Function

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If bValidarCampos_RegistroFoto() = False Then Exit Sub
        If bValidarSQL_RegistroFoto() = False Then Exit Sub
        'Guardar_Registro_RegistroFoto()
        GuuardarArchivo_Foto()
    End Sub


    Private Sub GuuardarArchivo_Foto()
        Dim contentType As String = FileUpload_FOTO.PostedFile.ContentType
        Using fs As Stream = FileUpload_FOTO.PostedFile.InputStream

            Datos_Modulo_RegistroFoto()
            txt_02_T07NombreFoto.Text = FileUpload_FOTO.FileName
            Dim TestArray() As String = Split(txt_02_T07NombreFoto.Text, ".")
            Dim iItems As Integer = TestArray.Length
            'txt_02_T07NombreFoto.Text = TestArray(0)
            txt_02_T07ExtensionFoto.Text = "." & TestArray(iItems - 1).ToString
            txt_02_T07NombreFoto.Text = Replace(txt_02_T07NombreFoto.Text, txt_02_T07ExtensionFoto.Text, "")
            txt_02_T07Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            txt_02_T07Id_registroFotografico.Text = Iconsecutivo_RegistroFoto()
            Datos_Modulo_RegistroFoto()

            Using br As BinaryReader = New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Int32))
                Dim constr As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
                Using con As SqlConnection = New SqlConnection(constr)
                    '_02_T07Id_Estadistica,_02_T07Id_registroFotografico,_02_T07Id_BD,_02_T07Id_BD_Item,_02_T07Fecha,_02_T07Observaciones,_02_T07NombreFoto,_02_T07ExtensionFoto,_02_T07imagenFoto,_02_T07ContentType
                    Dim query As String = "INSERT INTO _02_T07REGISTROfOTOGRAFICO VALUES (@_02_T07Id_Estadistica,@_02_T07Id_registroFotografico,@_02_T07Id_BD,@_02_T07Id_BD_Item, @_02_T07Fecha,@_02_T07Observaciones, @_02_T07NombreFoto, @_02_T07ExtensionFoto, @_02_T07imagenFoto, @_02_T07ContentType)"
                    Using cmd As SqlCommand = New SqlCommand(query)
                        cmd.Connection = con
                        cmd.Parameters.AddWithValue("@_02_T07Id_Estadistica", txt_02_T03Id_Informe.Text)
                        cmd.Parameters.AddWithValue("@_02_T07Id_registroFotografico", txt_02_T07Id_registroFotografico.Text)
                        cmd.Parameters.AddWithValue("@_02_T07Id_BD", txt_02_T07Id_BD.Text)
                        cmd.Parameters.AddWithValue("@_02_T07Id_BD_Item", txt_02_T07Id_BD_Item.Text)
                        cmd.Parameters.AddWithValue("@_02_T07Fecha", txt_02_T07Fecha.Text)
                        cmd.Parameters.AddWithValue("@_02_T07Observaciones", txt_02_T07Observaciones.Text)
                        cmd.Parameters.AddWithValue("@_02_T07NombreFoto", txt_02_T07NombreFoto.Text)
                        cmd.Parameters.AddWithValue("@_02_T07ExtensionFoto", txt_02_T07ExtensionFoto.Text)
                        cmd.Parameters.AddWithValue("@_02_T07imagenFoto", bytes)
                        cmd.Parameters.AddWithValue("@_02_T07ContentType", contentType)

                        MostrarMensaje("")
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
            End Using
        End Using


        'Guardar_RegistroPK__02_T02DOCCONTRATACION(sNombreTabla_TEm, sCodigoDocumento_TEm, sCampoARchivo_TEm, sCamposTabla, sCamposINS_TEm, sLlaves_TEm)




    End Sub




    '********************** TERMINA TODO LO DE Registro fotografico 
    '********************** comienza todo lo de Estadistica Diaria

    Dim sCodModulo_EstadisticaD As String
    Dim sNombreTabla_EstadisticaD As String
    Dim sCamposTabla_EstadisticaD As String
    Dim sCamposINS_EstadisticaD As String
    Dim sCamposUPD_EstadisticaD As String
    Dim sLlaves_EstadisticaD As String

    Private Sub Datos_Modulo_EstadisticaD()
        sCodModulo_EstadisticaD = _02_T04ESTADISTICADIARIA.CodigoModulo
        sNombreTabla_EstadisticaD = _02_T04ESTADISTICADIARIA.NombreTabla
        sCamposTabla_EstadisticaD = _02_T04ESTADISTICADIARIA.CamposTabla
        sCamposINS_EstadisticaD = "'" & clsAdminDb.sRemoverHTML(txt_02_T04Id_Estadistica.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_02_T04Id_Item.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T04Lunes.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T04Martes.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T04Miercoles.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T04Jueves.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T04Viernes.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T04Sabado.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T04Domingo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_02_T04AcomuladoSemanal.Text) & "'"
        sCamposUPD_EstadisticaD = _02_T04ESTADISTICADIARIA.Campo_02_T04Lunes & "='" & clsAdminDb.sRemoverHTML(txt_02_T04Lunes.Text) & "'" & "," & _02_T04ESTADISTICADIARIA.Campo_02_T04Martes & "='" & clsAdminDb.sRemoverHTML(txt_02_T04Martes.Text) & "'" & "," & _02_T04ESTADISTICADIARIA.Campo_02_T04Miercoles & "='" & clsAdminDb.sRemoverHTML(txt_02_T04Miercoles.Text) & "'" & "," & _02_T04ESTADISTICADIARIA.Campo_02_T04Jueves & "='" & clsAdminDb.sRemoverHTML(txt_02_T04Jueves.Text) & "'" & "," & _02_T04ESTADISTICADIARIA.Campo_02_T04Viernes & "='" & clsAdminDb.sRemoverHTML(txt_02_T04Viernes.Text) & "'" & "," & _02_T04ESTADISTICADIARIA.Campo_02_T04Sabado & "='" & clsAdminDb.sRemoverHTML(txt_02_T04Sabado.Text) & "'" & "," & _02_T04ESTADISTICADIARIA.Campo_02_T04Domingo & "='" & clsAdminDb.sRemoverHTML(txt_02_T04Domingo.Text) & "'" & "," & _02_T04ESTADISTICADIARIA.Campo_02_T04AcomuladoSemanal & "='" & clsAdminDb.sRemoverHTML(txt_02_T04AcomuladoSemanal.Text) & "'"
        sLlaves_EstadisticaD = _02_T04ESTADISTICADIARIA.CampoLlave_02_T04Id_Estadistica & "=  '" & txt_02_T04Id_Estadistica.Text & "'" & " AND " & _02_T04ESTADISTICADIARIA.CampoLlave_02_T04Id_Item & "=  '" & drop_02_T04Id_Item.Text & "'"
    End Sub

    Private Sub limpiarCampos_EstadisticaD()
        txt_02_T04AcomuladoSemanal.Enabled = False
        txt_02_T04Id_Estadistica.Text = Nothing
        drop_02_T04Id_Item.Text = Nothing
        txt_02_T04Lunes.Text = "0"
        txt_02_T04Martes.Text = "0"
        txt_02_T04Miercoles.Text = "0"
        txt_02_T04Jueves.Text = "0"
        txt_02_T04Viernes.Text = "0"
        txt_02_T04Sabado.Text = "0"
        txt_02_T04Domingo.Text = "0"
        txt_02_T04AcomuladoSemanal.Text = "0"

        lbl_02_T04Id_Estadistica.ForeColor = Drawing.Color.Black
        lbl_02_T04Id_Item.ForeColor = Drawing.Color.Black
        lbl_02_T04Lunes.ForeColor = Drawing.Color.Black
        lbl_02_T04Martes.ForeColor = Drawing.Color.Black
        lbl_02_T04Miercoles.ForeColor = Drawing.Color.Black
        lbl_02_T04Jueves.ForeColor = Drawing.Color.Black
        lbl_02_T04Viernes.ForeColor = Drawing.Color.Black
        lbl_02_T04Sabado.ForeColor = Drawing.Color.Black
        lbl_02_T04Domingo.ForeColor = Drawing.Color.Black
        lbl_02_T04AcomuladoSemanal.ForeColor = Drawing.Color.Black
        txtFiltroBusqueda.Text = Nothing

        MostrarMensaje("")
        'bHabilitarControles(True)
        filtro()

    End Sub

    Private Function bValidarCampos_EstadisticaD() As Boolean
        bValidarCampos_EstadisticaD = False

        lbl_02_T04Id_Estadistica.ForeColor = Drawing.Color.Red
        lbl_02_T04Id_Item.ForeColor = Drawing.Color.Red
        lbl_02_T04Lunes.ForeColor = Drawing.Color.Red
        lbl_02_T04Martes.ForeColor = Drawing.Color.Red
        lbl_02_T04Miercoles.ForeColor = Drawing.Color.Red
        lbl_02_T04Jueves.ForeColor = Drawing.Color.Red
        lbl_02_T04Viernes.ForeColor = Drawing.Color.Red
        lbl_02_T04Sabado.ForeColor = Drawing.Color.Red
        lbl_02_T04Domingo.ForeColor = Drawing.Color.Red
        lbl_02_T04AcomuladoSemanal.ForeColor = Drawing.Color.Red
        'If Trim(txt_02_T04Id_Estadistica.Text) = "" Then
        '    txt_02_T04Id_Estadistica.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Id_Estadistica")
        '    Exit Function
        'Else
        '    lbl_02_T04Id_Estadistica.ForeColor = Drawing.Color.Black
        'End If
        If Trim(drop_02_T04Id_Item.Text) = "" Then
            drop_02_T04Id_Item.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Id_Item")
            Exit Function
        Else
            lbl_02_T04Id_Item.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T04Lunes.Text) = "" Then
            txt_02_T04Lunes.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Lunes")
            Exit Function
        Else
            lbl_02_T04Lunes.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T04Martes.Text) = "" Then
            txt_02_T04Martes.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Martes")
            Exit Function
        Else
            lbl_02_T04Martes.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T04Miercoles.Text) = "" Then
            txt_02_T04Miercoles.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Miercoles")
            Exit Function
        Else
            lbl_02_T04Miercoles.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T04Jueves.Text) = "" Then
            txt_02_T04Jueves.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Jueves")
            Exit Function
        Else
            lbl_02_T04Jueves.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T04Viernes.Text) = "" Then
            txt_02_T04Viernes.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Viernes")
            Exit Function
        Else
            lbl_02_T04Viernes.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T04Sabado.Text) = "" Then
            txt_02_T04Sabado.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Sabado")
            Exit Function
        Else
            lbl_02_T04Sabado.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T04Domingo.Text) = "" Then
            txt_02_T04Domingo.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04Domingo")
            Exit Function
        Else
            lbl_02_T04Domingo.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_02_T04AcomuladoSemanal.Text) = "" Then
            txt_02_T04AcomuladoSemanal.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 04AcomuladoSemanal")
            Exit Function
        Else
            lbl_02_T04AcomuladoSemanal.ForeColor = Drawing.Color.Black
        End If

        bValidarCampos_EstadisticaD = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL_EstadisticaD() As Boolean
        bValidarSQL_EstadisticaD = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_02_T04Id_Estadistica.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_02_T04Id_Item.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T04Lunes.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T04Martes.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T04Miercoles.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T04Jueves.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T04Viernes.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T04Sabado.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T04Domingo.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_02_T04AcomuladoSemanal.Text) Then Exit Function
        bValidarSQL_EstadisticaD = True
        MostrarMensaje("")
    End Function



    Private Sub Cargar_Registro_EstadisticaD()
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatos As Object

        'Datos_Modulo_EstadisticaD()
        coleccionDatos = clsAdminDb.sql_Select(sNombreTabla_EstadisticaD, sCamposTabla_EstadisticaD, sLlaves_EstadisticaD)
        If Not coleccionDatos Is Nothing Then
            If coleccionDatos.Length = 0 Then
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)
                Else
                    MostrarMensaje("No se encontraron datos ")
                End If
            Else
                txt_02_T04Id_Estadistica.Text = coleccionDatos(0)
                drop_02_T04Id_Item.Text = coleccionDatos(1)
                txt_02_T04Lunes.Text = coleccionDatos(2)
                txt_02_T04Martes.Text = coleccionDatos(3)
                txt_02_T04Miercoles.Text = coleccionDatos(4)
                txt_02_T04Jueves.Text = coleccionDatos(5)
                txt_02_T04Viernes.Text = coleccionDatos(6)
                txt_02_T04Sabado.Text = coleccionDatos(7)
                txt_02_T04Domingo.Text = coleccionDatos(8)
                txt_02_T04AcomuladoSemanal.Text = coleccionDatos(9)
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


    Private Sub Guardar_Registro_EstadisticaD()
        'clsAdminDb = New adminitradorDB
        Dim bSqlInsert As Boolean = False
        Dim lCantRegistros As Long = 0
        txt_02_T04Id_Estadistica.Text = txt_02_T03Id_Informe.Text
        Datos_Modulo_EstadisticaD()
        lCantRegistros = clsAdminDb.sql_Count(sNombreTabla_EstadisticaD, sLlaves_EstadisticaD)
        If lCantRegistros = 0 Then
            Datos_Modulo_EstadisticaD()
            txt_02_T04Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            Datos_Modulo_EstadisticaD()
            bSqlInsert = clsAdminDb.sql_Insert(sNombreTabla_EstadisticaD, sCamposTabla_EstadisticaD, sCamposINS_EstadisticaD)
            If bSqlInsert = True Then
                MostrarMensaje("Se inserto nuevo registro Personal correctamente ", True)
                bHabilitarControles(False)
                limpiarCampos_EstadisticaD()
                filtro()
            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
                Else
                    'MostrarMensaje("Se guardo correctamente este registro")
                End If
            End If
        Else
            txt_02_T05Id_Estadistica.Text = txt_02_T03Id_Informe.Text
            Datos_Modulo_EstadisticaD()
            bSqlInsert = clsAdminDb.sql_Update(sNombreTabla_EstadisticaD, sCamposUPD_EstadisticaD, sLlaves_EstadisticaD)
            If bSqlInsert = True Then
                MostrarMensaje("Se actualizo correctamente este registro", True)
                Registro_Procesos("Actualizar", clsAdminDb.Mostrar_Consulta)
                bHabilitarControles(False)
                limpiarCampos_EstadisticaD()
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
    Private Sub cargar_Tabla_EstadisticaD(Optional ByVal sLlaveTem As String = "")
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection


        Dim sCamposTablaLocal As String = "(select top 1 _02_T17Nombre from _02_T17ITEMESTADISTICADIARIA where _02_T04Id_Item=_02_T17Codigo),_02_T04Id_Item,_02_T04Lunes,_02_T04Martes,_02_T04Miercoles,_02_T04Jueves,_02_T04Viernes,_02_T04Sabado,_02_T04Domingo,_02_T04AcomuladoSemanal,_02_T04Id_Estadistica"

        Dim sLlavesLocal As String = _02_T04ESTADISTICADIARIA.CampoLlave_02_T04Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        Dim sTablaLocal As String = _02_T04ESTADISTICADIARIA.NombreTabla
        Dim ArregloSingular() As String
        tabla_EstadisticaD.Controls.Clear()

        If Trim(txtFiltroBusqueda.Text) <> "" Then
            sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
        Else
            If Trim("colocar filtro o validaciones") <> "" Then
                sLlavesLocal = sLlavesLocal
            End If
        End If
        Dim i As Integer = 0
        coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
        If (Not coleccionDatosPlural Is Nothing) Then
            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                FilaHtml = New HtmlTableRow
                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "5a-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(0)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "5b-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(2)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "5c-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(3)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "5d-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(4)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "5e-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(5)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "5f-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(6)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)

                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "6f-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(7)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "7f-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(8)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)


                CeldaHtml = New HtmlTableCell
                linkbutonHtml_EstadisticaD = New LinkButton
                linkbutonHtml_EstadisticaD.ID = i & "8f-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                AddHandler linkbutonHtml_EstadisticaD.Click, AddressOf linkbutonHtml_EstadisticaD_Click
                linkbutonHtml_EstadisticaD.Text = ArregloSingular(9)
                CeldaHtml.Controls.Add(linkbutonHtml_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)







                CeldaHtml = New HtmlTableCell
                linkbutonHtml_Eliminar_EstadisticaD = New LinkButton
                linkbutonHtml_Eliminar_EstadisticaD.ID = i & "3k-sep-" & ArregloSingular(1).ToString & "-sep-" & ArregloSingular(10).ToString '& "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
                linkbutonHtml_Eliminar_EstadisticaD.OnClientClick = "bPreguntar = false;"
                linkbutonHtml_Eliminar_EstadisticaD.ForeColor = Drawing.Color.DarkRed
                linkbutonHtml_Eliminar_EstadisticaD.Font.Bold = True
                linkbutonHtml_Eliminar_EstadisticaD.Font.Size = FontUnit.Point(8)
                linkbutonHtml_Eliminar_EstadisticaD.Enabled = False
                AddHandler linkbutonHtml_Eliminar_EstadisticaD.Click, AddressOf linkbutonHtml_Eliminar_EstadisticaD_Click
                CeldaHtml.BorderColor = Drawing.Color.DarkBlue.Name
                linkbutonHtml_Eliminar_EstadisticaD.Text = "Eliminar"
                CeldaHtml.Controls.Add(linkbutonHtml_Eliminar_EstadisticaD)
                FilaHtml.Cells.Add(CeldaHtml)


                i = i + 1


                tabla_EstadisticaD.Controls.Add(FilaHtml)
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)


            End If
        End If
    End Sub

    Private Sub linkbutonHtml_Eliminar_EstadisticaD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_Eliminar_EstadisticaD.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(1).ToString
        Dim sLlaves_Tem = _02_T04ESTADISTICADIARIA.CampoLlave_02_T04Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        sLlaves_Tem = sLlaves_Tem & " and " & _02_T04ESTADISTICADIARIA.CampoLlave_02_T04Id_Item & "='" & sllave1 & "'"


        Dim bSqlDelete = clsAdminDb.sql_Delete(_02_T04ESTADISTICADIARIA.NombreTabla, sLlaves_Tem)
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

        filtro()
    End Sub


    Private Sub linkbutonHtml_EstadisticaD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles linkbutonHtml_EstadisticaD.Click
        Dim link_B As LinkButton
        Dim sTexto_ID As String
        link_B = CType(sender, LinkButton)
        sTexto_ID = link_B.ID
        Dim TestArray() As String = Split(sTexto_ID, "-sep-")
        Dim sllave1 As String = TestArray(1).ToString
        'Dim sllave2 As String = TestArray(2).ToString
        'Dim sllave3 As String = TestArray(3).ToString
        'Cargar_Registro()
        sLlaves_EstadisticaD = _02_T04ESTADISTICADIARIA.CampoLlave_02_T04Id_Estadistica & "='" & txt_02_T03Id_Informe.Text & "'"
        sLlaves_EstadisticaD = sLlaves_EstadisticaD & " and " & _02_T04ESTADISTICADIARIA.CampoLlave_02_T04Id_Item & "='" & sllave1 & "'"
        Cargar_Registro_EstadisticaD()
        filtro()
    End Sub

    Private Sub btn_Guardar_EstadisticaD_Click(sender As Object, e As EventArgs) Handles btn_Guardar_EstadisticaD.Click
        If bValidarCampos_EstadisticaD() = False Then Exit Sub
        If bValidarSQL_EstadisticaD() = False Then Exit Sub
        Guardar_Registro_EstadisticaD()
    End Sub
    Private Sub SemanaAcomulado(ByVal SDias As String, ByVal sValorDia As Integer)

        If drop_02_T04Id_Item.Text = "01" Or drop_02_T04Id_Item.Text = "14" Then
            Dim ivalorASTEmporal As Integer = 0
            If Val(txt_02_T04AcomuladoSemanal.Text) <> 0 Then
                ivalorASTEmporal = Val(txt_02_T04AcomuladoSemanal.Text)
            End If

            If ivalorASTEmporal < sValorDia Then
                ivalorASTEmporal = sValorDia
            Else
            End If

            txt_02_T04AcomuladoSemanal.Text = ivalorASTEmporal
        Else
            txt_02_T04AcomuladoSemanal.Text = Val(txt_02_T04Lunes.Text) + Val(txt_02_T04Martes.Text) + Val(txt_02_T04Miercoles.Text) + Val(txt_02_T04Jueves.Text) + Val(txt_02_T04Viernes.Text) + Val(txt_02_T04Sabado.Text) + Val(txt_02_T04Domingo.Text)
        End If


    End Sub


    Private Sub txt_02_T04Lunes_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T04Lunes.TextChanged
        SemanaAcomulado("LU", Val(txt_02_T04Lunes.Text))
    End Sub

    Private Sub txt_02_T04Martes_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T04Martes.TextChanged
        SemanaAcomulado("LU", Val(txt_02_T04Martes.Text))
    End Sub

    Private Sub txt_02_T04Miercoles_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T04Miercoles.TextChanged
        SemanaAcomulado("LU", Val(txt_02_T04Miercoles.Text))
    End Sub

    Private Sub txt_02_T04Jueves_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T04Jueves.TextChanged
        SemanaAcomulado("LU", Val(txt_02_T04Jueves.Text))
    End Sub

    Private Sub txt_02_T04Viernes_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T04Viernes.TextChanged
        SemanaAcomulado("LU", Val(txt_02_T04Viernes.Text))
    End Sub

    Private Sub txt_02_T04Sabado_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T04Sabado.TextChanged
        SemanaAcomulado("LU", Val(txt_02_T04Sabado.Text))
    End Sub

    Private Sub txt_02_T04Domingo_TextChanged(sender As Object, e As EventArgs) Handles txt_02_T04Domingo.TextChanged
        SemanaAcomulado("LU", Val(txt_02_T04Domingo.Text))
    End Sub


    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim clsAdminDb As New adminitradorDB
        Dim sCamposTablaLocal As String = "_02_T03Id_ReporteCasos,_02_T03Id_Informe,_02_T03FechaRegistro,_02_T03usuarioRegistra,_02_T03NumCtoMarco,_02_T03Mes,_02_T03FechaSemana_Inicio,_02_T03FechaSemana_Final,(select top 1 _99_T09Nombre from _99_T09DEPARTAMENTOS where _99_T09Codigo=_02_T03Depto) as _02_T03Depto,(select top 1 _99_T10Nombre from _99_T10MUNICIPIOS where _99_T10CodDepto=_02_T03Depto and _99_T10Codigo=_02_T03Municipio) as _02_T03Municipio,_02_T03ODS,_02_T03AreaCliente,_02_T03ActividadesEjecutadas,_02_T03Id_EstadisticaDiaria,_02_T03Id_Personal,_02_T03Id_SeguridadVial,_02_T03Id_RegistroFotografico,_02_T03ResponsableInforme,_02_T03ComentariosFinales"
        Dim sLlavesLocal As String = _02_T03NFORME_HSE_PROYECTOS.CampoLlave_02_T03Id_Informe & "<>''"
        Dim sTablaLocal As String = _02_T03NFORME_HSE_PROYECTOS.NombreTabla

        Dim sConsulta As String = "select " & sCamposTablaLocal & " from " & sTablaLocal & " where " & sLlavesLocal



        Dim sRutaArchivo As String = "Reportes\rpt_InformeHSE.rpt"



        Try
            rptCrystal = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            rptCrystal = clsAdminDb.sql_Imprimir(sConsulta, sRutaArchivo)


            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
            Else
                MostrarMensaje("Se genero correctamente el reporte")
                rptCrystal.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, True, "Rrpt Informe HSE")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub drop_02_T05NombresApellidosExterno_TextChanged(sender As Object, e As EventArgs) Handles drop_02_T05NombresApellidosExterno.TextChanged
        CargarDatosFuncionario2()
    End Sub

    Private Sub drop_02_T04Id_Item_TextChanged(sender As Object, e As EventArgs) Handles drop_02_T04Id_Item.TextChanged
        txt_02_T04Lunes.Text = "0"
        txt_02_T04Martes.Text = "0"
        txt_02_T04Miercoles.Text = "0"
        txt_02_T04Jueves.Text = "0"
        txt_02_T04Viernes.Text = "0"
        txt_02_T04Sabado.Text = "0"
        txt_02_T04Domingo.Text = "0"
        txt_02_T04AcomuladoSemanal.Text = "0"
    End Sub

    'Private Sub drop_02_Busqueda_TextChanged(sender As Object, e As EventArgs) Handles drop_02_Busqueda.TextChanged
    '    cargar_Tabla()
    'End Sub

    'Private Sub txtfiltro_FechaIFinal_TextChanged(sender As Object, e As EventArgs) Handles txtfiltro_FechaIFinal.TextChanged
    '    cargar_Tabla()
    'End Sub

    'Private Sub txtfiltro_FechaInicial_TextChanged(sender As Object, e As EventArgs) Handles txtfiltro_FechaInicial.TextChanged
    '    cargar_Tabla()
    'End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        cargar_Tabla()
    End Sub
End Class


