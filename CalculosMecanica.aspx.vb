Imports System.IO

Public Class CalculosMecanica
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
    Protected WithEvents linkbutonHtml_Editar As System.Web.UI.WebControls.LinkButton

    Dim rptCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsAdminDb = New adminitradorDB
        Datos_Modulo()

        If Not IsPostBack Then
            drop_01_T02Tipotrampa.AutoPostBack = True
            drop_01_T02Tamañotrampa.AutoPostBack = True
            txt_01_T02EficienciaJunta.AutoPostBack = True
            txt_01_T02FactorDiseño.AutoPostBack = True
            txt_01_T02SobreCorrosion.AutoPostBack = True
            txt_01_T02Temperatura.AutoPostBack = True
            drop_01_T02Rating.AutoPostBack = True
            drop_01_T02Material.AutoPostBack = True
            txt_01_T02EespesorbarrilMayor.AutoPostBack = True
            txt_01_T02EespesorbarrilMenor.AutoPostBack = True


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
        sCodModulo = _01_T02MECANICA.CodigoModulo
        sNombreTabla = _01_T02MECANICA.NombreTabla
        sCamposTabla = _01_T02MECANICA.CamposTabla
        sCamposINS = "'" & clsAdminDb.sRemoverHTML(txt_01_T02Cliente.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02ContratoODS.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Fecha.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Diseñador.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Localizacionr.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02TAG.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T02Tipotrampa.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T02Tamañotrampa.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T02Rating.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(drop_01_T02Material.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02CODdiseño.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Temperatura.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02FactorDiseño.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02SobreCorrosion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02EficienciaJunta.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02EespesorbarrilMayor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02EespesorbarrilMenor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Coeficienteexp.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Elasticidad.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02RelacionPoisson.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02resisfluencia.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02resistension.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02presion.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02diametrolinea.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02diametrobarril.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02diametropateo.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02diametroexternoBmayor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02diametroexternoBmenor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02esfuerzomaterial.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02TempInterpolar1.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02TempInterpolar2.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Presioninterpolar1.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Presioninterpolar2.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02PresioninterpoladaFinal.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Espesordiseñobarrilmayor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Espesordiseñobarrilmenor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Espesorminimotoleranciamayor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Espesorminimotoleranciamenor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Chequeobarrilmayor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Chequeobarrilmenor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Esfuerzopresiondiseño1.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Esfuerzopresiondiseño2.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Presionprueba.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Duracionprueba.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Esfuerzocircunferencialbarrilmayor.Text) & "'" & "," & "'" & clsAdminDb.sRemoverHTML(txt_01_T02Esfuerzocircunferencialbarrilmenor.Text) & "'"
        sLlaves = _01_T02MECANICA.CampoLlave_01_T02Cliente & "=  '" & txt_01_T02Cliente.Text & "'"
    End Sub
    Private Sub CargarDrops()
        clsAdminDb = New adminitradorDB
        With clsAdminDb
            .Lit_CargarLiteral(LitHead, LitSuperior, LitIzquierdo, sCod_Aplicacion)
            'If .Mostrar_Error <> """ Then
            'MostrarMensaje(.Mostrar_Error)
            'Exit Sub
            'End If

            '.drop_Cargar_SINO(dropTipoRiesgoQuimicos, False)




            'Dim sCampos_tem = _99_T09DEPARTAMENTOS.CampoLlave_99_T09Codigo & "," & _99_T09DEPARTAMENTOS.Campo_99_T09Nombre
            'Dim stables_Tem = _99_T09DEPARTAMENTOS.NombreTabla
            'Dim sllaves_Tem = _99_T09DEPARTAMENTOS.Campo_99_T09Activo & "= 'SI' " & " and " & _99_T09DEPARTAMENTOS.CampoLlave_99_T09CodPais & " ='169' "
            '.drop_CargarCombox(drop_02_T01Id_Area, stables_Tem, sCampos_tem, sllaves_Tem,,, _99_T09DEPARTAMENTOS.Campo_99_T09Nombre)

            'drop_02_T01Id_Area.Items.Add(New ListItem("Otros"))

            drop_01_T02Tipotrampa.Items.Add(New ListItem("Seleccionar", ""))
            drop_01_T02Tipotrampa.Items.Add(New ListItem("Recibo", "Recibo"))
            drop_01_T02Tipotrampa.Items.Add(New ListItem("Bidireccional", "Bidireccional"))
            drop_01_T02Tipotrampa.Items.Add(New ListItem("Despacho", "Despacho"))


            drop_01_T02Tamañotrampa.Items.Add(New ListItem("8in x 6in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("10in x 8in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("12in x 10in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("16in x 12in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("18in x 14in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("20in x 16in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("24in x 18in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("24in x 20in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("30in x 24in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("36in x 30in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("42in x 36in"))
            drop_01_T02Tamañotrampa.Items.Add(New ListItem("48in x 42in"))

            drop_01_T02Rating.Items.Add(New ListItem("150"))
            drop_01_T02Rating.Items.Add(New ListItem("300"))
            drop_01_T02Rating.Items.Add(New ListItem("400"))
            drop_01_T02Rating.Items.Add(New ListItem("600"))
            drop_01_T02Rating.Items.Add(New ListItem("900"))
            drop_01_T02Rating.Items.Add(New ListItem("1500"))
            drop_01_T02Rating.Items.Add(New ListItem("2500"))

            'drop_01_T02Material.Items.Add(New ListItem("API 5L B, SMLS, PSL2", "API 5L B, SMLS, PSL2"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L A25, FBW, PSL2", "API 5L A25, FBW, PSL2"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L, B, PSL2"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L X42 PSL1"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L X46 PSL1"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L X52 PSL0"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L X56 PSL1"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L X60 PSL2"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L X65 PSL2"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L X70 PSL2"))
            'drop_01_T02Material.Items.Add(New ListItem("API 5L X80 PSL2"))
            'drop_01_T02Material.Items.Add(New ListItem("ASTM A53, S, Grade A"))
            'drop_01_T02Material.Items.Add(New ListItem("ASTM A53, S, Grade B"))
            'drop_01_T02Material.Items.Add(New ListItem("ASTM A53, F, Grade A"))
            'drop_01_T02Material.Items.Add(New ListItem("ASTM A53, E, Grade A"))
            'drop_01_T02Material.Items.Add(New ListItem("ASTM A53, E, Grade B"))



            .drop_CargarCombox(drop_01_T02Material, "_99_T13_MATERIALES_MECANICA", "_99_T13Id,_especificacion", "_99_T13Id<>''", True,, )

















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

        txt_01_T02Cliente.Text = Nothing
        txt_01_T02ContratoODS.Text = Nothing
        txt_01_T02Fecha.Text = Nothing
        txt_01_T02Diseñador.Text = Nothing
        txt_01_T02Localizacionr.Text = Nothing
        txt_01_T02TAG.Text = Nothing
        drop_01_T02Tipotrampa.Text = Nothing
        drop_01_T02Tamañotrampa.Text = Nothing
        drop_01_T02Rating.Text = Nothing
        drop_01_T02Material.Text = Nothing
        txt_01_T02CODdiseño.Text = Nothing
        txt_01_T02Temperatura.Text = Nothing
        txt_01_T02FactorDiseño.Text = Nothing
        txt_01_T02SobreCorrosion.Text = Nothing
        txt_01_T02EficienciaJunta.Text = Nothing
        txt_01_T02EespesorbarrilMayor.Text = 0
        txt_01_T02EespesorbarrilMenor.Text = 0
        txt_01_T02Coeficienteexp.Text = Nothing
        txt_01_T02Elasticidad.Text = Nothing
        txt_01_T02RelacionPoisson.Text = Nothing
        txt_01_T02resisfluencia.Text = Nothing
        txt_01_T02resistension.Text = Nothing
        txt_01_T02presion.Text = Nothing
        txt_01_T02diametrolinea.Text = Nothing
        txt_01_T02diametrobarril.Text = Nothing
        txt_01_T02diametropateo.Text = Nothing
        txt_01_T02diametroexternoBmayor.Text = Nothing
        txt_01_T02diametroexternoBmenor.Text = Nothing
        txt_01_T02esfuerzomaterial.Text = Nothing
        txt_01_T02TempInterpolar1.Text = Nothing
        txt_01_T02TempInterpolar2.Text = Nothing
        txt_01_T02Presioninterpolar1.Text = Nothing
        txt_01_T02Presioninterpolar2.Text = Nothing
        txt_01_T02PresioninterpoladaFinal.Text = Nothing
        txt_01_T02Espesordiseñobarrilmayor.Text = Nothing
        txt_01_T02Espesordiseñobarrilmenor.Text = Nothing
        txt_01_T02Espesorminimotoleranciamayor.Text = Nothing
        txt_01_T02Espesorminimotoleranciamenor.Text = Nothing
        txt_01_T02Chequeobarrilmayor.Text = Nothing
        txt_01_T02Chequeobarrilmenor.Text = Nothing
        txt_01_T02Esfuerzopresiondiseño1.Text = Nothing
        txt_01_T02Esfuerzopresiondiseño2.Text = Nothing
        txt_01_T02Presionprueba.Text = Nothing
        txt_01_T02Duracionprueba.Text = Nothing
        txt_01_T02Esfuerzocircunferencialbarrilmayor.Text = Nothing
        txt_01_T02Esfuerzocircunferencialbarrilmenor.Text = Nothing



        lbl_01_T02Cliente.ForeColor = Drawing.Color.Black
        lbl_01_T02ContratoODS.ForeColor = Drawing.Color.Black
        lbl_01_T02Fecha.ForeColor = Drawing.Color.Black
        lbl_01_T02Diseñador.ForeColor = Drawing.Color.Black
        lbl_01_T02Localizacionr.ForeColor = Drawing.Color.Black
        lbl_01_T02TAG.ForeColor = Drawing.Color.Black
        lbl_01_T02Tipotrampa.ForeColor = Drawing.Color.Black
        lbl_01_T02Tamañotrampa.ForeColor = Drawing.Color.Black
        lbl_01_T02Rating.ForeColor = Drawing.Color.Black
        lbl_01_T02Material.ForeColor = Drawing.Color.Black
        lbl_01_T02CODdiseño.ForeColor = Drawing.Color.Black
        lbl_01_T02Temperatura.ForeColor = Drawing.Color.Black
        lbl_01_T02FactorDiseño.ForeColor = Drawing.Color.Black
        lbl_01_T02SobreCorrosion.ForeColor = Drawing.Color.Black
        lbl_01_T02EficienciaJunta.ForeColor = Drawing.Color.Black
        lbl_01_T02EespesorbarrilMayor.ForeColor = Drawing.Color.Black
        lbl_01_T02EespesorbarrilMenor.ForeColor = Drawing.Color.Black
        lbl_01_T02Coeficienteexp.ForeColor = Drawing.Color.Black
        lbl_01_T02Elasticidad.ForeColor = Drawing.Color.Black
        lbl_01_T02RelacionPoisson.ForeColor = Drawing.Color.Black
        lbl_01_T02resisfluencia.ForeColor = Drawing.Color.Black
        lbl_01_T02resistension.ForeColor = Drawing.Color.Black
        lbl_01_T02presion.ForeColor = Drawing.Color.Black
        lbl_01_T02diametrolinea.ForeColor = Drawing.Color.Black
        lbl_01_T02diametrobarril.ForeColor = Drawing.Color.Black
        lbl_01_T02diametropateo.ForeColor = Drawing.Color.Black
        lbl_01_T02diametroexternoBmayor.ForeColor = Drawing.Color.Black
        lbl_01_T02diametroexternoBmenor.ForeColor = Drawing.Color.Black
        lbl_01_T02esfuerzomaterial.ForeColor = Drawing.Color.Black
        lbl_01_T02TempInterpolar1.ForeColor = Drawing.Color.Black
        lbl_01_T02TempInterpolar2.ForeColor = Drawing.Color.Black
        lbl_01_T02Presioninterpolar1.ForeColor = Drawing.Color.Black
        lbl_01_T02Presioninterpolar2.ForeColor = Drawing.Color.Black
        lbl_01_T02PresioninterpoladaFinal.ForeColor = Drawing.Color.Black
        lbl_01_T02Espesordiseñobarrilmayor.ForeColor = Drawing.Color.Black
        lbl_01_T02Espesordiseñobarrilmenor.ForeColor = Drawing.Color.Black
        lbl_01_T02Espesorminimotoleranciamayor.ForeColor = Drawing.Color.Black
        lbl_01_T02Espesorminimotoleranciamenor.ForeColor = Drawing.Color.Black
        lbl_01_T02Chequeobarrilmayor.ForeColor = Drawing.Color.Black
        lbl_01_T02Chequeobarrilmenor.ForeColor = Drawing.Color.Black
        lbl_01_T02Esfuerzopresiondiseño1.ForeColor = Drawing.Color.Black
        lbl_01_T02Esfuerzopresiondiseño2.ForeColor = Drawing.Color.Black
        lbl_01_T02Presionprueba.ForeColor = Drawing.Color.Black
        lbl_01_T02Duracionprueba.ForeColor = Drawing.Color.Black
        lbl_01_T02Esfuerzocircunferencialbarrilmayor.ForeColor = Drawing.Color.Black
        lbl_01_T02Esfuerzocircunferencialbarrilmenor.ForeColor = Drawing.Color.Black

        txtFiltroBusqueda.Text = Nothing


        MostrarMensaje("")
        bHabilitarControles(True)
        filtro()
    End Sub


    Private Sub bHabilitarControles(ByVal bEstado As Boolean)

        btnEliminar.Visible = False
        btnImprimir.Visible = Not (bEstado)
    End Sub

    Private Function bValidarCampos() As Boolean
        bValidarCampos = False
        lbl_01_T02Cliente.ForeColor = Drawing.Color.Red
        lbl_01_T02ContratoODS.ForeColor = Drawing.Color.Red
        lbl_01_T02Fecha.ForeColor = Drawing.Color.Red
        lbl_01_T02Diseñador.ForeColor = Drawing.Color.Red
        lbl_01_T02Localizacionr.ForeColor = Drawing.Color.Red
        lbl_01_T02TAG.ForeColor = Drawing.Color.Red
        lbl_01_T02Tipotrampa.ForeColor = Drawing.Color.Red
        lbl_01_T02Tamañotrampa.ForeColor = Drawing.Color.Red
        lbl_01_T02Rating.ForeColor = Drawing.Color.Red
        lbl_01_T02Material.ForeColor = Drawing.Color.Red
        lbl_01_T02CODdiseño.ForeColor = Drawing.Color.Red
        lbl_01_T02Temperatura.ForeColor = Drawing.Color.Red
        lbl_01_T02FactorDiseño.ForeColor = Drawing.Color.Red
        lbl_01_T02SobreCorrosion.ForeColor = Drawing.Color.Red
        lbl_01_T02EficienciaJunta.ForeColor = Drawing.Color.Red
        lbl_01_T02EespesorbarrilMayor.ForeColor = Drawing.Color.Red
        lbl_01_T02EespesorbarrilMenor.ForeColor = Drawing.Color.Red
        lbl_01_T02Coeficienteexp.ForeColor = Drawing.Color.Red
        lbl_01_T02Elasticidad.ForeColor = Drawing.Color.Red
        lbl_01_T02RelacionPoisson.ForeColor = Drawing.Color.Red
        lbl_01_T02resisfluencia.ForeColor = Drawing.Color.Red
        lbl_01_T02resistension.ForeColor = Drawing.Color.Red
        lbl_01_T02presion.ForeColor = Drawing.Color.Red
        lbl_01_T02diametrolinea.ForeColor = Drawing.Color.Red
        lbl_01_T02diametrobarril.ForeColor = Drawing.Color.Red
        lbl_01_T02diametropateo.ForeColor = Drawing.Color.Red
        lbl_01_T02diametroexternoBmayor.ForeColor = Drawing.Color.Red
        lbl_01_T02diametroexternoBmenor.ForeColor = Drawing.Color.Red
        lbl_01_T02esfuerzomaterial.ForeColor = Drawing.Color.Red
        lbl_01_T02TempInterpolar1.ForeColor = Drawing.Color.Red
        lbl_01_T02TempInterpolar2.ForeColor = Drawing.Color.Red
        lbl_01_T02Presioninterpolar1.ForeColor = Drawing.Color.Red
        lbl_01_T02Presioninterpolar2.ForeColor = Drawing.Color.Red
        lbl_01_T02PresioninterpoladaFinal.ForeColor = Drawing.Color.Red
        lbl_01_T02Espesordiseñobarrilmayor.ForeColor = Drawing.Color.Red
        lbl_01_T02Espesordiseñobarrilmenor.ForeColor = Drawing.Color.Red
        lbl_01_T02Espesorminimotoleranciamayor.ForeColor = Drawing.Color.Red
        lbl_01_T02Espesorminimotoleranciamenor.ForeColor = Drawing.Color.Red
        lbl_01_T02Chequeobarrilmayor.ForeColor = Drawing.Color.Red
        lbl_01_T02Chequeobarrilmenor.ForeColor = Drawing.Color.Red
        lbl_01_T02Esfuerzopresiondiseño1.ForeColor = Drawing.Color.Red
        lbl_01_T02Esfuerzopresiondiseño2.ForeColor = Drawing.Color.Red
        lbl_01_T02Presionprueba.ForeColor = Drawing.Color.Red
        lbl_01_T02Duracionprueba.ForeColor = Drawing.Color.Red
        lbl_01_T02Esfuerzocircunferencialbarrilmayor.ForeColor = Drawing.Color.Red
        lbl_01_T02Esfuerzocircunferencialbarrilmenor.ForeColor = Drawing.Color.Red


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
        If Trim(txt_01_T02Cliente.Text) = "" Then
            txt_01_T02Cliente.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Cliente")
            Exit Function
        Else
            lbl_01_T02Cliente.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02ContratoODS.Text) = "" Then
            txt_01_T02ContratoODS.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Contrato")
            Exit Function
        Else
            lbl_01_T02ContratoODS.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Fecha.Text) = "" Then
            txt_01_T02Fecha.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Fecha")
            Exit Function
        Else
            lbl_01_T02Fecha.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Diseñador.Text) = "" Then
            txt_01_T02Diseñador.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Diseñador")
            Exit Function
        Else
            lbl_01_T02Diseñador.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Localizacionr.Text) = "" Then

            txt_01_T02Localizacionr.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Localizacionr")
            Exit Function
        Else
            lbl_01_T02Localizacionr.ForeColor = Drawing.Color.Black
        End If


        If Trim(txt_01_T02TAG.Text) = "" Then
            txt_01_T02TAG.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: TAG")
            Exit Function
        Else
            lbl_01_T02TAG.ForeColor = Drawing.Color.Black
        End If

        If Trim(drop_01_T02Tipotrampa.Text) = "" Then
            drop_01_T02Tipotrampa.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:01TipoTrampa")
            Exit Function
        Else
            lbl_01_T02Tipotrampa.ForeColor = Drawing.Color.Black
        End If

        If Trim(drop_01_T02Tamañotrampa.Text) = "" Then
            drop_01_T02Tamañotrampa.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Tamañotrampa")
            Exit Function
        Else
            lbl_01_T02Tamañotrampa.ForeColor = Drawing.Color.Black
        End If
        If Trim(drop_01_T02Rating.Text) = "" Then
            drop_01_T02Rating.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Rating")
            Exit Function
        Else
            lbl_01_T02Rating.ForeColor = Drawing.Color.Black
        End If
        'If Trim(txt_02_T01Dependenia.Text) = "" Then
        '    txt_02_T01Dependenia.Focus()
        '    MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Dependenia")
        '    Exit Function
        'Else
        '    lbl_02_T01Dependenia.ForeColor = Drawing.Color.Black
        'End If
        If Trim(drop_01_T02Material.Text) = "" Then
            drop_01_T02Material.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Material")
            Exit Function
        Else
            lbl_01_T02Material.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Temperatura.Text) = "" Then
            txt_01_T02Temperatura.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Temperatura")
            Exit Function
        Else
            lbl_01_T02Temperatura.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Coeficienteexp.Text) = "" Then
            txt_01_T02Coeficienteexp.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Coeficienteexp")
            Exit Function
        Else
            lbl_01_T02Coeficienteexp.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Elasticidad.Text) = "" Then
            txt_01_T02Elasticidad.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Elasticidad")
            Exit Function
        Else
            lbl_01_T02Elasticidad.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02resisfluencia.Text) = "" Then
            txt_01_T02resisfluencia.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Resisfluencia")
            Exit Function
        Else
            lbl_01_T02resisfluencia.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02resistension.Text) = "" Then
            txt_01_T02resistension.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Resistension")
            Exit Function
        Else
            lbl_01_T02resistension.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02presion.Text) = "" Then
            txt_01_T02presion.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Presion")
            Exit Function
        Else
            lbl_01_T02presion.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02diametrolinea.Text) = "" Then
            txt_01_T02Temperatura.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Diametrolinea")
            Exit Function
        Else
            lbl_01_T02diametrolinea.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02diametrobarril.Text) = "" Then
            txt_01_T02diametrobarril.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Diametrobarril")
            Exit Function
        Else
            lbl_01_T02diametrobarril.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02diametropateo.Text) = "" Then
            txt_01_T02diametropateo.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01DiametroPateo")
            Exit Function
        Else
            lbl_01_T02diametropateo.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02diametroexternoBmayor.Text) = "" Then
            txt_01_T02diametroexternoBmayor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Diametroexternomayor")
            Exit Function
        Else
            lbl_01_T02diametroexternoBmayor.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02diametroexternoBmenor.Text) = "" Then
            txt_01_T02diametroexternoBmenor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Diametroexternomenor")
            Exit Function
        Else
            lbl_01_T02diametroexternoBmenor.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02esfuerzomaterial.Text) = "" Then
            txt_01_T02esfuerzomaterial.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Esfuerzomaterial")
            Exit Function
        Else
            lbl_01_T02esfuerzomaterial.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02TempInterpolar1.Text) = "" Then
            txt_01_T02TempInterpolar1.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Tempinterpolar1")
            Exit Function
        Else
            lbl_01_T02TempInterpolar1.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02TempInterpolar2.Text) = "" Then
            txt_01_T02TempInterpolar2.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Tempinterpolar2")
            Exit Function
        Else
            lbl_01_T02TempInterpolar2.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Presioninterpolar1.Text) = "" Then
            txt_01_T02Presioninterpolar1.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Presioninterpolar1")
            Exit Function
        Else
            lbl_01_T02Presioninterpolar1.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Presioninterpolar2.Text) = "" Then
            txt_01_T02Presioninterpolar2.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Presioninterpolar2")
            Exit Function
        Else
            lbl_01_T02Presioninterpolar2.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02PresioninterpoladaFinal.Text) = "" Then
            txt_01_T02PresioninterpoladaFinal.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Presioninterpolarfinal")
            Exit Function
        Else
            lbl_01_T02PresioninterpoladaFinal.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Espesordiseñobarrilmayor.Text) = "" Then
            txt_01_T02Espesordiseñobarrilmayor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Espesorbarrilmayor")
            Exit Function
        Else
            lbl_01_T02Espesordiseñobarrilmayor.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Espesordiseñobarrilmenor.Text) = "" Then
            filtered_01_T02Espesordiseñobarrilmenor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Espesorbarrilmenor")
            Exit Function
        Else
            lbl_01_T02Espesordiseñobarrilmenor.ForeColor = Drawing.Color.Black
        End If


        If Trim(txt_01_T02Espesorminimotoleranciamayor.Text) = "" Then
            txt_01_T02Espesorminimotoleranciamayor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Espesorminimotoleranciamayor")
            Exit Function
        Else
            lbl_01_T02Espesorminimotoleranciamayor.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Espesorminimotoleranciamenor.Text) = "" Then
            txt_01_T02Espesorminimotoleranciamenor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Espesorminimotoleranciamenor")
            Exit Function
        Else
            lbl_01_T02Espesorminimotoleranciamenor.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Chequeobarrilmayor.Text) = "" Then
            txt_01_T02Chequeobarrilmayor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Chequeobarrilmayor")
            Exit Function
        Else
            lbl_01_T02Chequeobarrilmayor.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02Chequeobarrilmenor.Text) = "" Then
            txt_01_T02Chequeobarrilmenor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Chequeobarrilmenor")
            Exit Function
        Else
            lbl_01_T02Chequeobarrilmenor.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Esfuerzopresiondiseño1.Text) = "" Then
            txt_01_T02Esfuerzopresiondiseño1.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Esfuerzopresiondiseño1")
            Exit Function
        Else
            lbl_01_T02Esfuerzopresiondiseño1.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Esfuerzopresiondiseño2.Text) = "" Then
            txt_01_T02Esfuerzopresiondiseño2.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Esfuerzopresiondiseño2")
            Exit Function
        Else
            lbl_01_T02Esfuerzopresiondiseño2.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Presionprueba.Text) = "" Then
            txt_01_T02Presionprueba.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Presionprueba")
            Exit Function
        Else
            lbl_01_T02Presionprueba.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Duracionprueba.Text) = "" Then
            txt_01_T02Duracionprueba.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Duracionprueba")
            Exit Function
        Else
            lbl_01_T02Duracionprueba.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Esfuerzocircunferencialbarrilmayor.Text) = "" Then
            txt_01_T02Esfuerzocircunferencialbarrilmayor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Esfuerzocircunferenciabarrilmayor")
            Exit Function
        Else
            lbl_01_T02Esfuerzocircunferencialbarrilmayor.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02Esfuerzocircunferencialbarrilmenor.Text) = "" Then
            txt_01_T02Esfuerzocircunferencialbarrilmenor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo: 01Esfuerzocircunferenciabarrilmenor")
            Exit Function
        Else
            lbl_01_T02Esfuerzocircunferencialbarrilmenor.ForeColor = Drawing.Color.Black
        End If

        If Trim(txt_01_T02CODdiseño.Text) = "" Then
            txt_01_T02CODdiseño.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:T02CODdiseño")
            Exit Function
        Else
            lbl_01_T02CODdiseño.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02FactorDiseño.Text) = "" Then
            txt_01_T02FactorDiseño.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:T02FactorDiseño")
            Exit Function
        Else
            lbl_01_T02FactorDiseño.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02FactorDiseño.Text) = "" Then
            txt_01_T02FactorDiseño.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:T02FactorDiseño")
            Exit Function
        Else
            lbl_01_T02FactorDiseño.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02SobreCorrosion.Text) = "" Then
            txt_01_T02SobreCorrosion.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:T02SobreCorrosion")
            Exit Function
        Else
            lbl_01_T02SobreCorrosion.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02EficienciaJunta.Text) = "" Then
            txt_01_T02EficienciaJunta.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:T02EficienciaJunta")
            Exit Function
        Else
            lbl_01_T02EficienciaJunta.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02EespesorbarrilMayor.Text) = "" Then
            filtered_01_T02EespesorbarrilMayor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:T02Espesorbarrilmayor")
            Exit Function
        Else
            lbl_01_T02EespesorbarrilMayor.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02EespesorbarrilMenor.Text) = "" Then
            txt_01_T02EespesorbarrilMenor.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:T02Espesorbarrilmenor")
            Exit Function
        Else
            lbl_01_T02EespesorbarrilMenor.ForeColor = Drawing.Color.Black
        End If
        If Trim(txt_01_T02RelacionPoisson.Text) = "" Then
            txt_01_T02RelacionPoisson.Focus()
            MostrarMensaje("Por favor ingrese datos en los campos obligatorios marcados en rojo - falta campo:T02RelacionPoisson")
            Exit Function
        Else
            lbl_01_T02RelacionPoisson.ForeColor = Drawing.Color.Black
        End If


        bValidarCampos = True
        MostrarMensaje("")
    End Function

    Private Function bValidarSQL() As Boolean
        bValidarSQL = False

        MostrarMensaje("Existe codigo SQL malicioso que quiere ingresar, por favor borrelo para continuar con proceso")
        If clsAdminDb.bVerificarSQL(txt_01_T02Cliente.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02ContratoODS.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Fecha.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Diseñador.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Localizacionr.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02TAG.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_01_T02Tipotrampa.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_01_T02Tamañotrampa.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_01_T02Rating.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(drop_01_T02Material.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02CODdiseño.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Temperatura.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02FactorDiseño.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02SobreCorrosion.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02EficienciaJunta.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Espesordiseñobarrilmayor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Espesordiseñobarrilmenor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Coeficienteexp.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Elasticidad.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02RelacionPoisson.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02resisfluencia.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02presion.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02presion.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02diametrobarril.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02diametrobarril.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02diametroexternoBmayor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02diametroexternoBmenor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02esfuerzomaterial.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02TempInterpolar1.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02TempInterpolar2.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Presioninterpolar1.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Presioninterpolar2.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02PresioninterpoladaFinal.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Espesordiseñobarrilmayor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Espesordiseñobarrilmenor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Espesorminimotoleranciamenor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Chequeobarrilmayor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Chequeobarrilmenor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Esfuerzopresiondiseño1.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Esfuerzopresiondiseño2.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Presionprueba.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Duracionprueba.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Esfuerzocircunferencialbarrilmayor.Text) Then Exit Function
        If clsAdminDb.bVerificarSQL(txt_01_T02Esfuerzocircunferencialbarrilmenor.Text) Then Exit Function






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

                txt_01_T02Cliente.ForeColor = coleccionDatos(0)
                txt_01_T02ContratoODS.ForeColor = coleccionDatos(1)
                txt_01_T02Fecha.ForeColor = coleccionDatos(2)
                txt_01_T02Diseñador.ForeColor = coleccionDatos(3)
                txt_01_T02Localizacionr.ForeColor = coleccionDatos(4)
                txt_01_T02TAG.ForeColor = coleccionDatos(5)
                drop_01_T02Tipotrampa.ForeColor = coleccionDatos(6)
                drop_01_T02Tamañotrampa.ForeColor = coleccionDatos(7)
                drop_01_T02Rating.ForeColor = coleccionDatos(8)
                drop_01_T02Material.ForeColor = coleccionDatos(9)
                txt_01_T02CODdiseño.ForeColor = coleccionDatos(10)
                txt_01_T02Temperatura.ForeColor = coleccionDatos(11)
                txt_01_T02FactorDiseño.ForeColor = coleccionDatos(12)
                txt_01_T02SobreCorrosion.ForeColor = coleccionDatos(13)
                txt_01_T02EficienciaJunta.ForeColor = coleccionDatos(14)
                txt_01_T02Espesordiseñobarrilmayor.ForeColor = coleccionDatos(15)
                txt_01_T02Espesordiseñobarrilmenor.ForeColor = coleccionDatos(16)
                txt_01_T02Coeficienteexp.ForeColor = coleccionDatos(17)
                txt_01_T02Elasticidad.ForeColor = coleccionDatos(18)
                txt_01_T02RelacionPoisson.ForeColor = coleccionDatos(19)
                txt_01_T02resisfluencia.ForeColor = coleccionDatos(20)
                txt_01_T02presion.ForeColor = coleccionDatos(21)
                txt_01_T02presion.ForeColor = coleccionDatos(22)
                txt_01_T02diametrobarril.ForeColor = coleccionDatos(23)
                txt_01_T02diametrobarril.ForeColor = coleccionDatos(24)
                txt_01_T02diametroexternoBmayor.ForeColor = coleccionDatos(25)
                txt_01_T02diametroexternoBmenor.ForeColor = coleccionDatos(26)
                txt_01_T02esfuerzomaterial.ForeColor = coleccionDatos(27)
                txt_01_T02TempInterpolar1.ForeColor = coleccionDatos(28)
                txt_01_T02TempInterpolar2.ForeColor = coleccionDatos(29)
                txt_01_T02Presioninterpolar1.ForeColor = coleccionDatos(30)
                txt_01_T02Presioninterpolar2.ForeColor = coleccionDatos(31)
                txt_01_T02PresioninterpoladaFinal.ForeColor = coleccionDatos(32)
                txt_01_T02Espesordiseñobarrilmayor.ForeColor = coleccionDatos(33)
                txt_01_T02Espesordiseñobarrilmenor.ForeColor = coleccionDatos(34)
                txt_01_T02Espesorminimotoleranciamenor.ForeColor = coleccionDatos(35)
                txt_01_T02Chequeobarrilmayor.ForeColor = coleccionDatos(36)
                txt_01_T02Chequeobarrilmenor.ForeColor = coleccionDatos(37)
                txt_01_T02Esfuerzopresiondiseño1.ForeColor = coleccionDatos(38)
                txt_01_T02Esfuerzopresiondiseño2.ForeColor = coleccionDatos(39)
                txt_01_T02Presionprueba.ForeColor = coleccionDatos(40)
                txt_01_T02Duracionprueba.ForeColor = coleccionDatos(41)
                txt_01_T02Esfuerzocircunferencialbarrilmayor.ForeColor = coleccionDatos(42)
                txt_01_T02Esfuerzocircunferencialbarrilmenor.ForeColor = coleccionDatos(43)




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
        
    End Sub

    'Private Sub cargar_Tabla(Optional ByVal sLlaveTem As String = "")
    '    Dim clsAdminDb As New adminitradorDB
    '    Dim coleccionDatosPlural As New Collection
    '    'Selec _02_T01Fecha,_02_T01DocUsuarioRegistra, _02_T02Nombre,_02_T01LugarEvento 
    '    'From _02_T01REPORTE_CASOS, _02_T02TIPO_ACCION
    '    'Where _02_T02Id = _02_T01Id_TipoAccion


    '    Dim sCamposTablaLocal As String = _01_T02MECANICA.CamposTabla
    '    Dim sLlavesLocal As String = _01_T02MECANICA.CampoLlave_01_T02Cliente & "<>''"
    '    Dim sTablaLocal As String = _01_T02MECANICA.NombreTabla
    '    Dim ArregloSingular() As String
    '    bodytabla.Controls.Clear()

    '    If Trim(txtFiltroBusqueda.Text) <> "" Then
    '        sLlavesLocal = sLlavesLocal & " And " & sLlaveTem
    '    Else
    '        If Trim("colocar filtro o validaciones") <> "" Then
    '            sLlavesLocal = sLlavesLocal
    '        End If
    '    End If

    '    coleccionDatosPlural = clsAdminDb.sql_Coleccion(sTablaLocal, sCamposTablaLocal, sLlavesLocal)
    '    If (Not coleccionDatosPlural Is Nothing) Then
    '        For Each ColeccionSingular In coleccionDatosPlural
    '            ArregloSingular = ColeccionSingular
    '            FilaHtml = New HtmlTableRow
    '            'CeldaHtml = New HtmlTableCell
    '            'linkbutonHtml = New LinkButton
    '            'linkbutonHtml.ID = ArregloSingular(0).ToString & "sep" & ArregloSingular(1).ToString & "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
    '            'AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
    '            'linkbutonHtml.Text = "Eliminar"
    '            'CeldaHtml.Controls.Add(linkbutonHtml)
    '            'FilaHtml.Cells.Add(CeldaHtml)
    '            For i = 0 To ArregloSingular.Count - 1
    '                CeldaHtml = New HtmlTableCell
    '                linkbutonHtml = New LinkButton
    '                linkbutonHtml.ID = i & "-sep-" & ArregloSingular(0).ToString & "-sep-" '& ArregloSingular(1).ToString & "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
    '                AddHandler linkbutonHtml.Click, AddressOf linkbutonHtml_Click
    '                linkbutonHtml.Text = ArregloSingular(i)
    '                CeldaHtml.Controls.Add(linkbutonHtml)
    '                FilaHtml.Cells.Add(CeldaHtml)
    '            Next

    '            bodytabla.Controls.Add(FilaHtml)
    '        Next

    '    Else
    '        If clsAdminDb.Mostrar_Error <> "" Then
    '            MostrarMensaje(clsAdminDb.Mostrar_Error)


    '        End If
    '    End If
    'End Sub
    Private Sub cargar_Tabla(Optional ByVal sLlaveTem As String = "")
        Dim clsAdminDb As New adminitradorDB
        Dim coleccionDatosPlural As New Collection
        'Selec _02_T01Fecha,_02_T01DocUsuarioRegistra, _02_T02Nombre,_02_T01LugarEvento 
        'From _02_T01REPORTE_CASOS, _02_T02TIPO_ACCION
        'Where _02_T02Id = _02_T01Id_TipoAccion


        Dim sCamposTablaLocal As String = _01_T02MECANICA.CamposTabla
        Dim sLlavesLocal As String = _01_T02MECANICA.CampoLlave_01_T02Cliente & "<>''"
        Dim sTablaLocal As String = _01_T02MECANICA.NombreTabla
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
                For i = 0 To ArregloSingular.Count - 1
                    CeldaHtml = New HtmlTableCell
                    linkbutonHtml = New LinkButton
                    linkbutonHtml.ID = i & "-sep-" & ArregloSingular(0).ToString & "-sep-" '& ArregloSingular(1).ToString & "sep" & ArregloSingular(2).ToString & "sep" & ArregloSingular(4).ToString
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
        Dim sLlaves_Tem = _01_T02MECANICA.CampoLlave_01_T02Cliente & "='" & sllave1 & "'"


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
        sLlaves = _01_T02MECANICA.CampoLlave_01_T02Cliente & "='" & sllave1 & "'"
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
                    'sllaveTem = sllaveTem & _01_T37TIPOACTIVIDADES.CampoLlave_01_T37Codigo & " like '" & TestArray(i).ToString & "%'"
                    'sllaveTem = sllaveTem & " Or " & _01_T37TIPOACTIVIDADES.Campo_01_T37Nombre & " like '" & TestArray(i).ToString & "%'"
                Else
                    'sllaveTem = sllaveTem & " Or " & _01_T37TIPOACTIVIDADES.CampoLlave_01_T37Codigo & " like '" & TestArray(i).ToString & "%'"
                    'sllaveTem = sllaveTem & " Or " & _01_T37TIPOACTIVIDADES.Campo_01_T37Nombre & " like '" & TestArray(i).ToString & "%'"
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
        sConsecutivo = clsAdminDb.sql_Max(_01_T02MECANICA.NombreTabla, "cast(_02_T01Id_Reporte as integer)", "_02_T01Id_Reporte<>''")
        If sConsecutivo = "0" Then
            sConsecutivo = iVigencia & "1"
        Else
            sConsecutivo = Val(sConsecutivo) + 1
        End If
        Iconsecutivo = sConsecutivo
    End Function

    Private Sub formulas(ByVal Coddiseño As String)
        If Coddiseño <> "" Then
            Dim ArregloSingular() As String
            Dim coleccionDatosPlural As New Collection

            Dim sCosnulta As String = ""
            sCosnulta += "SELECT _especificacion,_Eksi,_Sypsi,_Sutpsi,_in_in_f,_junta,_E,_99_T13id FROM _04_HSE_REPORTESDECASOS.dbo._99_T13_MATERIALES_MECANICA "
            sCosnulta = sCosnulta & "where _99_T13id ='" & Coddiseño & "'"
            coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta)
            If (Not coleccionDatosPlural Is Nothing) Then
                For Each ColeccionSingular In coleccionDatosPlural
                    ArregloSingular = ColeccionSingular
                    txt_01_T02Coeficienteexp.Text = ArregloSingular(4)
                    txt_01_T02Elasticidad.Text = ArregloSingular(1)
                    txt_01_T02RelacionPoisson.Text = 0.3
                    txt_01_T02resisfluencia.Text = ArregloSingular(2)
                    txt_01_T02resistension.Text = ArregloSingular(3)
                Next

            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)

                End If
            End If

        End If


    End Sub
    Private Sub formulas2(ByVal tipotrampa As String, ByVal tamañotramp As String)
        'txt_01_T02diametrolinea.Text = ""
        'txt_01_T02diametrobarril.Text = ""
        'txt_01_T02diametropateo.Text = ""


        If tipotrampa <> "" Then
            Dim ArregloSingular() As String
            Dim coleccionDatosPlural As New Collection



            If tipotrampa = "Recibo" Then
                Dim sCosnulta1 As String = ""
                sCosnulta1 += "SELECT  _Tamaño,_Lineaprincipal,_Barrilmayor,_REFINADO,_CRUDO   FROM _04_HSE_REPORTESDECASOS.dbo.T15_TRAMPASRECIBO_MECANICA "
                sCosnulta1 = sCosnulta1 & "where _Tamaño ='" & drop_01_T02Tamañotrampa.Text & "'"
                coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta1)
                If (Not coleccionDatosPlural Is Nothing) Then
                    For Each ColeccionSingular In coleccionDatosPlural
                        ArregloSingular = ColeccionSingular
                        txt_01_T02diametrolinea.Text = ArregloSingular(2)
                        txt_01_T02diametrobarril.Text = ArregloSingular(1)
                        txt_01_T02diametropateo.Text = ArregloSingular(3)
                    Next

                Else
                    If clsAdminDb.Mostrar_Error <> "" Then
                        MostrarMensaje(clsAdminDb.Mostrar_Error)

                    End If
                End If
            ElseIf tipotrampa = "Bidireccional" Then
                Dim sCosnulta1 As String = ""
                sCosnulta1 += "SELECT  _Tamaño,_Lineaprincipal,_Barrilmayor,_REFINADO,_CRUDO   FROM _04_HSE_REPORTESDECASOS.dbo.T15_TRAMPASBIDIRECCIONAL_MECANICA "
                sCosnulta1 = sCosnulta1 & "where _Tamaño ='" & drop_01_T02Tamañotrampa.Text & "'"
                coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta1)
                If (Not coleccionDatosPlural Is Nothing) Then
                    For Each ColeccionSingular In coleccionDatosPlural
                        ArregloSingular = ColeccionSingular
                        txt_01_T02diametrolinea.Text = ArregloSingular(2)
                        txt_01_T02diametrobarril.Text = ArregloSingular(1)
                        txt_01_T02diametropateo.Text = ArregloSingular(3)

                    Next

                Else
                    If clsAdminDb.Mostrar_Error <> "" Then
                        MostrarMensaje(clsAdminDb.Mostrar_Error)

                    End If
                End If
            ElseIf tipotrampa = "Despacho" Then
                Dim sCosnulta1 As String = ""
                sCosnulta1 += "SELECT  _Tamaño,_Lineaprincipal,_Barrilmayor,_REFINADO,_CRUDO   FROM _04_HSE_REPORTESDECASOS.dbo.T15_TRAMPASDEDESPACHO_MECANICA "
                sCosnulta1 = sCosnulta1 & "where _Tamaño ='" & drop_01_T02Tamañotrampa.Text & "'"
                coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta1)
                If (Not coleccionDatosPlural Is Nothing) Then
                    For Each ColeccionSingular In coleccionDatosPlural
                        ArregloSingular = ColeccionSingular
                        txt_01_T02diametrolinea.Text = ArregloSingular(2)
                        txt_01_T02diametrobarril.Text = ArregloSingular(1)
                        txt_01_T02diametropateo.Text = ArregloSingular(3)

                    Next

                Else
                    If clsAdminDb.Mostrar_Error <> "" Then
                        MostrarMensaje(clsAdminDb.Mostrar_Error)

                    End If
                End If




            End If
            'Exit Sub
            Dim sCosnulta2 As String = ""
            sCosnulta2 += "SELECT  *  FROM _04_HSE_REPORTESDECASOS.dbo.T15_TUBERIA_MECANICA "
            sCosnulta2 = sCosnulta2 & "where _NPS ='" & txt_01_T02diametrolinea.Text & "'"
            coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta2)
            If (Not coleccionDatosPlural Is Nothing) Then
                For Each ColeccionSingular In coleccionDatosPlural
                    ArregloSingular = ColeccionSingular
                    txt_01_T02diametroexternoBmayor.Text = ArregloSingular(1)
                Next

            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)

                End If
            End If


            Dim sCosnulta3 As String = ""
            sCosnulta3 += "SELECT  *  FROM _04_HSE_REPORTESDECASOS.dbo.T15_TUBERIA_MECANICA "
            sCosnulta3 = sCosnulta3 & "where _NPS ='" & txt_01_T02diametrobarril.Text & "'"
            coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta3)
            If (Not coleccionDatosPlural Is Nothing) Then
                For Each ColeccionSingular In coleccionDatosPlural
                    ArregloSingular = ColeccionSingular
                    txt_01_T02diametroexternoBmenor.Text = ArregloSingular(1)
                Next

            Else
                If clsAdminDb.Mostrar_Error <> "" Then
                    MostrarMensaje(clsAdminDb.Mostrar_Error)

                End If
            End If


            txt_01_T02esfuerzomaterial.Text = Val(txt_01_T02resisfluencia.Text) * Val(txt_01_T02FactorDiseño.Text) * Val(txt_01_T02EficienciaJunta.Text)
            txt_01_T02presion.Text = txt_01_T02PresioninterpoladaFinal.Text
            '----------------------------------------------------------

        End If
    End Sub
    Private Sub formulas3()
        Dim ArregloSingular() As String
        Dim coleccionDatosPlural As New Collection

        If txt_01_T02Temperatura.Text = "" Then
            Exit Sub
        ElseIf txt_01_T02Temperatura.Text <= 100 Then
            txt_01_T02TempInterpolar1.Text = 0
        ElseIf txt_01_T02Temperatura.Text > 100 And txt_01_T02Temperatura.Text <= 200 Then
            txt_01_T02TempInterpolar1.Text = 100
        ElseIf txt_01_T02Temperatura.Text > 200 And txt_01_T02Temperatura.Text <= 300 Then
            txt_01_T02TempInterpolar1.Text = 200
        End If


        If txt_01_T02Temperatura.Text = "" Then
            Exit Sub
        ElseIf txt_01_T02Temperatura.Text <= 100 Then
            txt_01_T02TempInterpolar2.Text = 0
        ElseIf txt_01_T02Temperatura.Text > 100 And txt_01_T02Temperatura.Text <= 200 Then
            txt_01_T02TempInterpolar2.Text = 200
        ElseIf txt_01_T02Temperatura.Text > 200 And txt_01_T02Temperatura.Text <= 300 Then
            txt_01_T02TempInterpolar2.Text = 300
        End If
        '----------------------------------------------------------
        Dim sCosnulta4 As String = ""
        sCosnulta4 += "SELECT  T" + drop_01_T02Rating.Text
        sCosnulta4 += " FROM _04_HSE_REPORTESDECASOS.dbo._T14_TEMPERATURA_MECANICA "
        sCosnulta4 += "where Temp='" & txt_01_T02TempInterpolar1.Text & "'"
        coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta4)
        If (Not coleccionDatosPlural Is Nothing) Then

            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                txt_01_T02Presioninterpolar1.Text = ArregloSingular(0)
                'txt_01_T02Presioninterpolar2.Text = ArregloSingular()
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)

            End If
        End If



        '----------------------------------------------------------------------
        Dim sCosnulta5 As String = ""
        sCosnulta5 += "SELECT  T" + drop_01_T02Rating.Text
        sCosnulta5 += " FROM _04_HSE_REPORTESDECASOS.dbo._T14_TEMPERATURA_MECANICA "
        sCosnulta5 += "where Temp='" & txt_01_T02TempInterpolar2.Text & "'"
        coleccionDatosPlural = clsAdminDb.sql_Coleccion_Consulta(sCosnulta5)
        If (Not coleccionDatosPlural Is Nothing) Then

            For Each ColeccionSingular In coleccionDatosPlural
                ArregloSingular = ColeccionSingular
                txt_01_T02Presioninterpolar2.Text = ArregloSingular(0)
                'txt_01_T02Presioninterpolar2.Text = ArregloSingular()
            Next

        Else
            If clsAdminDb.Mostrar_Error <> "" Then
                MostrarMensaje(clsAdminDb.Mostrar_Error)

            End If
        End If

        formulas4()
        formulas5()
    End Sub
    Private Sub formulas4()
        txt_01_T02PresioninterpoladaFinal.Text = Val(txt_01_T02Presioninterpolar2.Text) - (((Val(txt_01_T02TempInterpolar2.Text - txt_01_T02Temperatura.Text) * Val(txt_01_T02Presioninterpolar2.Text - txt_01_T02Presioninterpolar1.Text))) / Val(txt_01_T02TempInterpolar2.Text - txt_01_T02TempInterpolar1.Text))
        txt_01_T02presion.Text = txt_01_T02PresioninterpoladaFinal.Text
        '-------------------------------------------------------


    End Sub

    Private Sub formulas5()
        If drop_01_T02Tipotrampa.Text <> "" Then


            txt_01_T02Espesordiseñobarrilmayor.Text = txt_01_T02presion.Text * txt_01_T02diametroexternoBmayor.Text / 2 / (txt_01_T02esfuerzomaterial.Text)
            txt_01_T02Espesordiseñobarrilmenor.Text = txt_01_T02presion.Text * txt_01_T02diametroexternoBmenor.Text / 2 / (txt_01_T02esfuerzomaterial.Text)

            txt_01_T02Espesorminimotoleranciamayor.Text = txt_01_T02Espesordiseñobarrilmayor.Text + (txt_01_T02SobreCorrosion.Text)
            txt_01_T02Espesorminimotoleranciamenor.Text = txt_01_T02Espesordiseñobarrilmenor.Text + (txt_01_T02SobreCorrosion.Text)

            txt_01_T02Chequeobarrilmayor.Text = txt_01_T02diametroexternoBmayor.Text / txt_01_T02EespesorbarrilMayor.Text
            txt_01_T02Chequeobarrilmenor.Text = txt_01_T02diametroexternoBmenor.Text / txt_01_T02EespesorbarrilMenor.Text


            txt_01_T02Esfuerzopresiondiseño1.Text = (txt_01_T02presion.Text * txt_01_T02diametroexternoBmayor.Text) / 2 / txt_01_T02EespesorbarrilMayor.Text
            txt_01_T02Esfuerzopresiondiseño2.Text = (txt_01_T02presion.Text * txt_01_T02diametroexternoBmenor.Text) / 2 / txt_01_T02EespesorbarrilMenor.Text

            txt_01_T02Presionprueba.Text = 1.25 * txt_01_T02presion.Text

            txt_01_T02Esfuerzocircunferencialbarrilmayor.Text = (txt_01_T02Presionprueba.Text * txt_01_T02diametroexternoBmayor.Text) / 2 / txt_01_T02EespesorbarrilMayor.Text
            txt_01_T02Esfuerzocircunferencialbarrilmenor.Text = (txt_01_T02Presionprueba.Text * txt_01_T02diametroexternoBmenor.Text) / 2 / txt_01_T02EespesorbarrilMenor.Text

        End If


        If txt_01_T02Esfuerzopresiondiseño1.Text <> "" Then

            If (txt_01_T02Esfuerzopresiondiseño1.Text / txt_01_T02resisfluencia.Text > 0.2) Then
                txt_01_T02Duracionprueba.Text = 4
            ElseIf (txt_01_T02Esfuerzopresiondiseño2.Text / txt_01_T02resisfluencia.Text > 0.2) Then
                txt_01_T02Duracionprueba.Text = 4
            Else
                txt_01_T02Duracionprueba.Text = 1
            End If
        End If








    End Sub






    Private Sub drop_01_T02Material_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T02Material.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()
    End Sub

    Protected Sub drop_01_T02Tipotrampa_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T02Tipotrampa.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()

    End Sub

    Private Sub drop_01_T02Tamañotrampa_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T02Tamañotrampa.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()
    End Sub

    Protected Sub txt_01_T02Temperatura_TextChanged(sender As Object, e As EventArgs) Handles txt_01_T02Temperatura.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()
    End Sub

    Protected Sub txt_01_T02SobreCorrosion_TextChanged(sender As Object, e As EventArgs) Handles txt_01_T02SobreCorrosion.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()
    End Sub

    Protected Sub txt_01_T02FactorDiseño_TextChanged(sender As Object, e As EventArgs) Handles txt_01_T02FactorDiseño.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()
    End Sub

    Protected Sub txt_01_T02EficienciaJunta_TextChanged(sender As Object, e As EventArgs) Handles txt_01_T02EficienciaJunta.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()
    End Sub

    Protected Sub txt_01_T02EespesorbarrilMayor_TextChanged(sender As Object, e As EventArgs) Handles txt_01_T02EespesorbarrilMayor.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()

    End Sub

    Protected Sub txt_01_T02EespesorbarrilMenor_TextChanged(sender As Object, e As EventArgs) Handles txt_01_T02EespesorbarrilMenor.TextChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()


    End Sub

    Protected Sub drop_01_T02Rating_TextChanged(sender As Object, e As EventArgs) Handles drop_01_T02Rating.SelectedIndexChanged
        formulas(drop_01_T02Material.Text)
        formulas2(drop_01_T02Tipotrampa.Text, drop_01_T02Tamañotrampa.Text)
        formulas3()
    End Sub














    'Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
    '    Dim clsAdminDb As New adminitradorDB
    '    Dim sCamposTablaLocal As String = _01_T02MECANICA.CamposTabla
    '    Dim sLlavesLocal As String = _01_T02MECANICA.CampoLlave_01_T02Cliente "<>''"
    '    Dim sTablaLocal As String = _01_T02MECANICA.NombreTabla

    '    Dim sConsulta As String = "select " & sCamposTablaLocal & " from " & sTablaLocal & " where " & sLlavesLocal

    '    Dim sRutaArchivo As String = "Reportes\rpt_Reporte_Casos.rpt"

    '    Try
    '        rptCrystal = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
    '        rptCrystal = clsAdminDb.sql_Imprimir(sConsulta, sRutaArchivo)


    '        If clsAdminDb.Mostrar_Error <> "" Then
    '            MostrarMensaje(clsAdminDb.Mostrar_Error, EstadosMensajes.ErrorGenerado)
    '        Else
    '            MostrarMensaje("Se genero correctamente el reporte")
    '            rptCrystal.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, True, "RrptInforme Mensual")
    '        End If

    '    Catch ex As Exception

    '    End Try







    'End Sub
End Class


