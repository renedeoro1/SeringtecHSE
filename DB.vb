Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection

Public Class adminitradorDB

    'Crear objetos de ADO.NET.
    Private myConn As SqlConnection
    Private myConnTem As SqlConnection
    Private myCmd As SqlCommand
    Private myTrans As SqlTransaction
    Private myReader As SqlDataReader
    Private ColeccionResultado As Collection
    Dim sCadenaConexion As String = ConfigurationManager.ConnectionStrings("SERINGTECConnectionString").ConnectionString
    'Private sCodigoAplicacion As String = ConfigurationManager.ConnectionStrings("SERINGTEGAPLICACION").ConnectionString
    Dim sCod_Aplicacion As String = ConfigurationManager.ConnectionStrings("SERINGTEGAPLICACION").ConnectionString
    Public sRutaAplicacion As String = ConfigurationManager.ConnectionStrings("rutaApliacion").ConnectionString
    Private MostrarError As String
    Private MostrarConsulta As String

    Enum Tipo_Permiso
        Insertar = 1
        Consultar = 2
        Actualizar = 3
        Eliminar = 4
        Imprimir = 5
        Exportar = 6
        Especial1 = 7
        Especial2 = 8
    End Enum

    Public Property Mostrar_Error() As String
        Get
            Return MostrarError
        End Get

        Set(ByVal Value As String)
            MostrarError = Value
        End Set
    End Property

    Public Property Mostrar_Consulta() As String
        Get
            Return MostrarConsulta
        End Get
        Set(ByVal Value As String)
            MostrarConsulta = Value
        End Set
    End Property

    'Crear un objeto Connection.
    Public Sub ConexionCrear(Optional ByVal sCadenaConexionTem As String = "")
        'myConn = New SqlConnection("Initial Catalog=INGEMETA;" & _
        '         "Data Source=MCURVELO\SQLEXPRESS;Integrated Security=SSPI;")
        If sCadenaConexionTem <> "" Then
            sCadenaConexion = sCadenaConexionTem
        End If
        myConn = New SqlConnection(sCadenaConexion)
    End Sub

    Public Sub ConexionEliminar()
        myConn.Dispose()
        myConn = Nothing
    End Sub

    Public Sub ConexionAbrir()
        myConn.Open()
    End Sub

    Public Sub ConexionCerrar()
        If Not IsNothing(myConn) Then
            If myConn.State = ConnectionState.Open Then
                myConn.Close()
            End If
        End If
    End Sub

    Public Sub sql_Transaccion()
        If myConn Is Nothing Then ConexionCrear()
        If myConn.State = ConnectionState.Closed Then ConexionAbrir()
        myTrans = myConn.BeginTransaction()
    End Sub

    Public Sub sql_Transaccion_IComit()
        If Not myTrans Is Nothing Then myTrans.Commit()
    End Sub

    Public Function sql_Transaccion_Estado() As Boolean
        sql_Transaccion_Estado = myTrans.Connection.State = ConnectionState.Open
    End Function

    Public Sub sql_Transaccion_RollBack()
        If Not myTrans Is Nothing Then myTrans.Rollback()
    End Sub

    Public Function sql_sConsulta(ByVal sConsulta As String) As Boolean
        sql_sConsulta = False
        Dim i As Integer = 0

        'Abrir la conexión.
        If sConsulta = "" Then
            MostrarError = "Por favor consulta"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans

            myCmd.CommandText = sConsulta
            myCmd.CommandTimeout = 20000
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            sql_sConsulta = True
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_sConsulta_NonQuery(ByVal sConsulta As String) As Boolean
        sql_sConsulta_NonQuery = False
        Dim i As Integer = 0

        'Abrir la conexión.
        If sConsulta = "" Then
            MostrarError = "Por favor consulta"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans

            myCmd.CommandText = sConsulta
            myCmd.CommandTimeout = 20000
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myCmd.ExecuteNonQuery()
            sql_sConsulta_NonQuery = True
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Imprimir(ByVal sConsulta As String, ByVal sNombreArchivoReportes As String) As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim i As Integer = 0
        Dim dtDatos As New DataTable ' datatable para recibir los datos de la base de datos
        Dim daDatos As New SqlDataAdapter  ' Objeto Adaptador para leer datos de la Base de datos

        'Abrir la conexión.
        If sConsulta = "" Then
            MostrarError = "Por favor consulta"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans

            myCmd.CommandText = sConsulta
            myCmd.CommandTimeout = 20000
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            daDatos = New SqlDataAdapter(myCmd)
            daDatos.Fill(dtDatos)

            sql_Imprimir = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            'sql_Imprimir.Subreports(1).Load(sRutaAplicacion & "rptDescargaEmpresasOK.rpt")
            'sql_Imprimir.Subreports(1).SetDataSource(dtDatos)
            sql_Imprimir.Load(sRutaAplicacion & sNombreArchivoReportes)
            sql_Imprimir.SetDataSource(dtDatos)

        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Function

    Public Function sql_Imprimir_SubReporte(ByVal cr_ReportePrincipal As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal sConsultaSubReporte As String, ByVal sNombreArchivoSubReporte As String) As CrystalDecisions.CrystalReports.Engine.ReportDocument
        sql_Imprimir_SubReporte = Nothing
        If sConsultaSubReporte = "" Or sNombreArchivoSubReporte = "" Then
            MostrarError = "Por favor consulta"
            Exit Function
        End If
        Try
            If sConsultaSubReporte <> "" And sNombreArchivoSubReporte <> "" Then
                Dim dtDatos2 As New DataTable ' datatable para recibir los datos de la base de datos
                Dim daDatos2 As New SqlDataAdapter  ' Objeto Adaptador para leer datos de la Base de datos
                'Abrir la conexión.
                If sConsultaSubReporte = "" Then
                    MostrarError = "Por favor consulta"
                    Exit Function
                End If
                If myConn Is Nothing Then ConexionCrear()
                myCmd = myConn.CreateCommand
                'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
                myCmd.CommandText = sConsultaSubReporte
                myCmd.CommandTimeout = 200000
                MostrarConsulta = myCmd.CommandText.ToString
                Select Case myConn.State
                    Case ConnectionState.Closed
                        ConexionAbrir()
                    Case ConnectionState.Connecting
                    Case ConnectionState.Executing
                    Case ConnectionState.Open
                    Case ConnectionState.Broken
                    Case ConnectionState.Fetching
                End Select
                daDatos2 = New SqlDataAdapter(myCmd)
                daDatos2.Fill(dtDatos2)
                sql_Imprimir_SubReporte = New CrystalDecisions.CrystalReports.Engine.ReportDocument
                sql_Imprimir_SubReporte = cr_ReportePrincipal.Subreports(sNombreArchivoSubReporte)
                sql_Imprimir_SubReporte.SetDataSource(dtDatos2)
            End If

        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Function

    Public Shared Function ObtenerImagenImagenesCrystalReport() As System.Drawing.Image
        Dim assembly As Assembly = System.Reflection.Assembly.GetExecutingAssembly()

        Dim file As Stream = assembly.GetManifestResourceStream("Helpers.intellinet_logo.gif")
        Return System.Drawing.Image.FromStream(file)

    End Function


    Public Function sql__WebService_Select(ByVal sTablas As String, ByVal sCampos As String, ByVal sCondicion As String, Optional ByVal sOrder As String = "", Optional ByVal sAgrupar As String = "", Optional ByVal iTop As Integer = 10) As List(Of String)
        ColeccionResultado = Nothing
        sql__WebService_Select = Nothing
        Dim i As Integer = 0
        Dim sTexto As String = ""
        Dim lista As New List(Of String)

        If sTablas = "" Or sCampos = "" Or sCondicion = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro " & sTablas & ", " & sCampos & ", " & sCondicion & " "
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT TOP " & iTop & " " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT TOP 50 " & iTop & " " & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Group by " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            Do While myReader.Read()
                For i = 0 To myReader.FieldCount - 1
                    If sTexto = "" Then
                        sTexto = myReader.Item(i).ToString
                    Else
                        sTexto = sTexto & " " & myReader.Item(i).ToString
                    End If
                Next
                lista.Add(sTexto)
                sTexto = ""
            Loop
            sql__WebService_Select = lista
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Select_Bytes(ByVal sTablas As String, ByVal sCampos As String, ByVal sCondicion As String, Optional ByVal sOrder As String = "", Optional ByVal sAgrupar As String = "", Optional ByVal sCadenaConexionTem As String = "") As Array
        ColeccionResultado = Nothing
        sql_Select_Bytes = Nothing
        Dim i As Integer = 0
        Dim cargoWeights() As Object

        If sTablas = "" Or sCampos = "" Or sCondicion = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If sCadenaConexionTem <> "" Then
                If myConn Is Nothing Then ConexionCrear(sCadenaConexionTem)
            Else
                If myConn Is Nothing Then ConexionCrear()
            End If

            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT TOP 1  " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT TOP 1  " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Group by " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            Do While myReader.Read()
                ReDim Preserve cargoWeights(myReader.FieldCount)
                For i = 0 To myReader.FieldCount - 1
                    cargoWeights(i) = myReader.Item(i)
                Next
                sql_Select_Bytes = cargoWeights
            Loop
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Select(ByVal sTablas As String, ByVal sCampos As String, ByVal sCondicion As String, Optional ByVal sOrder As String = "", Optional ByVal sAgrupar As String = "", Optional ByVal sCadenaConexionTem As String = "") As Array
        ColeccionResultado = Nothing
        sql_Select = Nothing
        Dim i As Integer = 0
        Dim cargoWeights() As Object

        If sTablas = "" Or sCampos = "" Or sCondicion = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If sCadenaConexionTem <> "" Then
                If myConn Is Nothing Then ConexionCrear(sCadenaConexionTem)
            Else
                If myConn Is Nothing Then ConexionCrear()
            End If

            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT TOP 1  " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT TOP 1  " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Group by " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            Do While myReader.Read()
                ReDim Preserve cargoWeights(myReader.FieldCount)
                For i = 0 To myReader.FieldCount - 1
                    cargoWeights(i) = myReader.Item(i).ToString
                Next
                sql_Select = cargoWeights
            Loop
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Select_Bajo(ByVal sTablas As String, ByVal sCampos As String, ByVal sCondicion As String, Optional ByVal sOrder As String = "", Optional ByVal sAgrupar As String = "", Optional ByVal sCadenaConexionTem As String = "") As Array
        ColeccionResultado = Nothing
        sql_Select_Bajo = Nothing
        Dim i As Integer = 0
        Dim cargoWeights() As Object

        If sTablas = "" Or sCampos = "" Or sCondicion = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If sCadenaConexionTem <> "" Then
                If myConn Is Nothing Then ConexionCrear(sCadenaConexionTem)
            Else
                If myConn Is Nothing Then ConexionCrear()
            End If

            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT TOP 1  " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT TOP 1  " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Group by " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            Do While myReader.Read()
                ReDim Preserve cargoWeights(myReader.FieldCount)
                For i = 0 To myReader.FieldCount - 1
                    cargoWeights(i) = myReader.Item(i)
                Next
                sql_Select_Bajo = cargoWeights
            Loop
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Coleccion_Sincronizacion(ByVal sTablas As String, ByVal sCampos As String, ByVal sCondicion As String, Optional ByVal sOrder As String = "", Optional ByVal sAgrupar As String = "") As Collection
        ColeccionResultado = Nothing
        sql_Coleccion_Sincronizacion = Nothing
        Dim sql_ColeccionTem As Collection
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim cargoWeights() As String

        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " group by " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If
            myCmd.CommandTimeout = 20000
            'bulkCopy.BulkCopyTimeout = 12000
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            sql_ColeccionTem = New Collection
            Do While myReader.Read()
                ReDim Preserve cargoWeights(myReader.FieldCount)
                For i = 0 To myReader.FieldCount - 1
                    cargoWeights(i) = myReader.Item(i).ToString
                Next
                sql_ColeccionTem.Add(cargoWeights, j)
                j = j + 1
            Loop

            sql_Coleccion_Sincronizacion = sql_ColeccionTem
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Coleccion(ByVal sTablas As String, ByVal sCampos As String, ByVal sCondicion As String, Optional ByVal sOrder As String = "", Optional ByVal sAgrupar As String = "", Optional ByVal sCadenaConexionTem As String = "") As Collection
        ColeccionResultado = Nothing
        sql_Coleccion = Nothing
        Dim sql_ColeccionTem As Collection
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim cargoWeights() As String

        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If sCadenaConexionTem <> "" Then
                If myConn Is Nothing Then ConexionCrear(sCadenaConexionTem)
            Else
                If myConn Is Nothing Then ConexionCrear()
            End If

            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " group by " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If
            myCmd.CommandTimeout = 20000
            'bulkCopy.BulkCopyTimeout = 12000
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            sql_ColeccionTem = New Collection
            Do While myReader.Read()
                ReDim Preserve cargoWeights(myReader.FieldCount)
                For i = 0 To myReader.FieldCount - 1
                    cargoWeights(i) = myReader.Item(i).ToString
                Next
                sql_ColeccionTem.Add(cargoWeights, j)
                j = j + 1
            Loop
            sql_Coleccion = sql_ColeccionTem
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Coleccion_Bajo(ByVal sTablas As String, ByVal sCampos As String, ByVal sCondicion As String, Optional ByVal sOrder As String = "", Optional ByVal sAgrupar As String = "", Optional ByVal sCadenaConexionTem As String = "") As Collection
        ColeccionResultado = Nothing
        sql_Coleccion_Bajo = Nothing
        Dim sql_ColeccionTem As Collection
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim cargoWeights() As String

        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If sCadenaConexionTem <> "" Then
                If myConn Is Nothing Then ConexionCrear(sCadenaConexionTem)
            Else
                If myConn Is Nothing Then ConexionCrear()
            End If

            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " group by " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If
            myCmd.CommandTimeout = 20000
            'bulkCopy.BulkCopyTimeout = 12000
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            sql_ColeccionTem = New Collection
            Do While myReader.Read()
                ReDim Preserve cargoWeights(myReader.FieldCount)
                For i = 0 To myReader.FieldCount - 1
                    cargoWeights(i) = myReader.Item(i)
                Next
                sql_ColeccionTem.Add(cargoWeights, j)
                j = j + 1
            Loop
            sql_Coleccion_Bajo = sql_ColeccionTem
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Coleccion_Consulta(ByVal sConsulta As String, Optional ByVal sCadenaConexionTem As String = "") As Collection
        ColeccionResultado = Nothing
        sql_Coleccion_Consulta = Nothing
        Dim sql_ColeccionTem As Collection
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim cargoWeights() As String

        If sConsulta = "" Then
            MostrarError = "Por favor ingrese consulta"
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If sCadenaConexionTem <> "" Then
                ConexionEliminar()
                If myConn Is Nothing Then ConexionCrear(sCadenaConexionTem)
            Else
                If myConn Is Nothing Then ConexionCrear()
            End If

            'If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            myCmd.CommandText = sConsulta
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            sql_ColeccionTem = New Collection
            Do While myReader.Read()
                ReDim Preserve cargoWeights(myReader.FieldCount)
                For i = 0 To myReader.FieldCount - 1
                    cargoWeights(i) = myReader.Item(i).ToString
                Next
                sql_ColeccionTem.Add(cargoWeights, j)
                j = j + 1
            Loop
            sql_Coleccion_Consulta = sql_ColeccionTem
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Insert_Archivo(ByVal sTabla As String, ByVal sCampo As String, ByVal sCondicion As String, ByVal bArchivoBytes As Byte()) As Boolean
        sql_Insert_Archivo = False

        If IsNothing(sql_Insert_Archivo) Or sCampo = "" Or sTabla = "" Then
            MostrarError = "Por favor ingrese Campos, tabla o Valores a ingresar"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            myCmd.CommandText = "INSERT INTO " & sTabla & "(" & sCampo & ") VALUES (@Archivo)"
            myCmd.CommandText = "UPDATE " & sTabla & " SET " & sCampo & "= (@Archivo) WHERE  " & sCondicion
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myCmd.Parameters.AddWithValue("@Archivo", System.Data.SqlDbType.Image).Value = bArchivoBytes
            myReader = myCmd.ExecuteReader()
            sql_Insert_Archivo = True
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Insert(ByVal sTabla As String, ByVal sCampos As String, ByVal sValores As String) As Boolean
        sql_Insert = False

        If sCampos = "" Or sValores = "" Or sTabla = "" Then
            MostrarError = "Por favor ingrese Campos, tabla o Valores a ingresar"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            myCmd.CommandText = "INSERT INTO " & sTabla & "(" & sCampos & ") VALUES (" & sValores & ")"
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            sql_Insert = True
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Update(ByVal sTabla As String, ByVal sValores As String, ByVal sCondicion As String) As Boolean
        sql_Update = False

        If sValores = "" Or sTabla = "" Then
            MostrarError = "Por favor ingrese tabla o Valores para actualizar"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            myCmd.CommandTimeout = 20000
            myCmd.CommandText = "UPDATE " & sTabla & " SET " & sValores & " WHERE " & sCondicion
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            sql_Update = True
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Count(ByVal sTabla As String, ByVal sCondicion As String) As String
        sql_Count = "0"

        If sCondicion = "" Or sTabla = "" Then
            MostrarError = "Por favor ingrese tabla o Valores para Contar"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            myCmd.CommandText = "SELECT COUNT(*) FROM " & sTabla & " WHERE " & sCondicion
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            Do While myReader.Read()
                sql_Count = (myReader.GetValue(0))
            Loop
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Max(ByVal sTabla As String, ByVal sCampo As String, ByVal sCondicion As String) As String
        sql_Max = "0"

        If sCondicion = "" Or sTabla = "" Or sCampo = "" Then
            MostrarError = "Por favor ingrese tabla o Valores para Contar"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            myCmd.CommandText = "SELECT MAX(" & sCampo & ") FROM " & sTabla & " WHERE " & sCondicion
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            Do While myReader.Read()
                sql_Max = Val(myReader.GetValue(0).ToString)
            Loop
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_SUM(ByVal sTabla As String, ByVal sCampo As String, ByVal sCondicion As String) As String
        sql_SUM = "0"

        If sCondicion = "" Or sTabla = "" Or sCampo = "" Then
            MostrarError = "Por favor ingrese tabla o Valores para Contar"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            myCmd.CommandText = "SELECT SUM(" & sCampo & ") FROM " & sTabla & " WHERE " & sCondicion
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            Do While myReader.Read()
                sql_SUM = Val(myReader.GetValue(0).ToString)
            Loop
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Function sql_Delete(ByVal sTabla As String, ByVal sCondicion As String) As Boolean
        sql_Delete = False

        If sCondicion = "" Or sTabla = "" Then
            MostrarError = "Por favor tabla y condicion"
            Exit Function
        End If
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            myCmd.CommandText = "Delete from " & sTabla & " where " & sCondicion
            myCmd.CommandTimeout = 20000
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()
            sql_Delete = True
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    'Public Function sql_Verificar_Permisos(ByVal sCodModulo As String, ByVal TipoPermiso As Tipo_Permiso, ByVal sTipoDocUsuario As String, ByVal sNumDocUsuario As String, ByVal sNombreUsuario As String) As Boolean
    '    sql_Verificar_Permisos = Nothing
    '    Dim sPermiso As String = ""
    '    Dim sTipoPermiso As String = ""
    '    Dim sTablas As String = ""
    '    Dim sCampos As String = ""
    '    Dim sLlave As String = ""

    '    If sCodModulo = "" Or TipoPermiso = 0 Or sTipoDocUsuario = "" Or sNumDocUsuario = "" Or sNombreUsuario = "" Then
    '        MostrarError = "Por favor ingrese sCodModulo, Tipo de Permiso, datos usuario"
    '        Exit Function
    '    End If
    '    'Abrir la conexión.
    '    Try
    '        If myConn Is Nothing Then ConexionCrear()
    '        myCmd = myConn.CreateCommand
    '        'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
    '        sTablas = _90_T06USUARIOS.NombreTabla
    '        sCampos = "dbo.Funcion_Permisos('" & sCod_Aplicacion & "','" & sCodModulo & "'," & sTablas & "." & _90_T06USUARIOS.Campo_90_T06CodPerfil & ",'" & TipoPermiso & "') as Permiso"
    '        sLlave = _90_T06USUARIOS.CampoLlave_90_T06CodAplicacion & "='" & sCod_Aplicacion & "'"
    '        sLlave = sLlave & " AND " & _90_T06USUARIOS.CampoLlave_90_T06TipoDocumento & "='" & sTipoDocUsuario & "'"
    '        sLlave = sLlave & " AND " & _90_T06USUARIOS.CampoLlave_90_T06Documento & "='" & sNumDocUsuario & "'"
    '        sLlave = sLlave & " AND " & _90_T06USUARIOS.CampoLlave_90_T06NombreUsuario & "='" & sNombreUsuario & "'"

    '        myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sLlave
    '        MostrarConsulta = myCmd.CommandText.ToString
    '        Select Case myConn.State
    '            Case ConnectionState.Closed
    '                ConexionAbrir()
    '            Case ConnectionState.Connecting
    '            Case ConnectionState.Executing
    '            Case ConnectionState.Open
    '            Case ConnectionState.Broken
    '            Case ConnectionState.Fetching
    '        End Select
    '        myReader = myCmd.ExecuteReader()

    '        'Concatenar el resultado de la consulta en una cadena.
    '        Do While myReader.Read()
    '            sPermiso = myReader.GetString(0)
    '            If sPermiso = "SI" Then
    '                sql_Verificar_Permisos = True
    '            End If
    '        Loop
    '        myReader.Close()
    '    Catch ex As Exception
    '        MostrarError = " Error ejecutando consulta: " & ex.ToString
    '    End Try
    '    'If myTrans Is Nothing Then ConexionCerrar()

    'End Function

    Public Function sql_Verificar_Permisos(ByVal sCodModulo As String, ByVal TipoPermiso As Tipo_Permiso, ByVal sTipoDocUsuario As String, ByVal sNumDocUsuario As String, ByVal sNombreUsuario As String) As Boolean
        sql_Verificar_Permisos = Nothing
        Dim sPermiso As String = ""
        Dim sTipoPermiso As String = ""
        Dim sTablas As String = ""
        Dim sCampos As String = ""
        Dim sLlave As String = ""

        If sCodModulo = "" Or TipoPermiso = 0 Or sTipoDocUsuario = "" Or sNumDocUsuario = "" Or sNombreUsuario = "" Then
            MostrarError = "Por favor ingrese sCodModulo, Tipo de Permiso, datos usuario"
            Exit Function
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            sTablas = _90_T06USUARIOS.NombreTabla
            sCampos = "dbo.Funcion_Permisos('" & sCod_Aplicacion & "','" & sCodModulo & "'," & sTablas & "." & _90_T06USUARIOS.Campo_90_T06CodPerfil & ",'" & TipoPermiso & "') as Permiso"
            sLlave = _90_T06USUARIOS.CampoLlave_90_T06CodAplicacion & "='" & sCod_Aplicacion & "'"
            sLlave = sLlave & " AND " & _90_T06USUARIOS.CampoLlave_90_T06TipoDocumento & "='" & sTipoDocUsuario & "'"
            sLlave = sLlave & " AND " & _90_T06USUARIOS.CampoLlave_90_T06Documento & "='" & sNumDocUsuario & "'"
            sLlave = sLlave & " AND " & _90_T06USUARIOS.CampoLlave_90_T06NombreUsuario & "='" & sNombreUsuario & "'"

            myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sLlave
            MostrarConsulta = myCmd.CommandText.ToString
            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
                Case ConnectionState.Executing
                Case ConnectionState.Open
                Case ConnectionState.Broken
                Case ConnectionState.Fetching
            End Select
            myReader = myCmd.ExecuteReader()

            'Concatenar el resultado de la consulta en una cadena.
            Do While myReader.Read()
                sPermiso = myReader.GetString(0)
                If sPermiso = "SI" Then
                    sql_Verificar_Permisos = True
                End If
            Loop
            myReader.Close()
        Catch ex As Exception
            MostrarError = " Error ejecutando consulta: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()

    End Function

    Public Sub Lit_CargarLiteral(ByRef LitHead As Literal, ByRef LitMenuSup As Literal, ByRef LitMenuIzq As Literal, ByVal sCodAplicacion As String)
        Dim i As Integer = 0
        Dim sTablas As String = _90_T01APLICACIONES.NombreTabla
        Dim sCampos As String = _90_T01APLICACIONES.Campo_90_T01Head & "," & _90_T01APLICACIONES.Campo_90_T01MenuIzq & "," & _90_T01APLICACIONES.Campo_90_T01MenuSup
        Dim sCondicion As String = _90_T01APLICACIONES.CampoLlave_90_T01Codigo & "='" & sCodAplicacion & "'"

        If sCodAplicacion = "" Then
            MostrarError = "Por favor ingrese codigo de aplicaciòn"
            Exit Sub
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            MostrarConsulta = myCmd.CommandText.ToString
            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            Do While myReader.Read()
                LitHead.Text = myReader.Item(0).ToString
                LitMenuIzq.Text = myReader.Item(1).ToString
                LitMenuSup.Text = myReader.Item(2).ToString
            Loop
            myReader.Close()
        Catch ex As Exception
            Mostrar_Error = " Error ejecutando consulta cargar Literales: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Sub

    Public Sub Lit_CargarLiteral_DescargasConceptos(ByRef LitHead As Literal, ByRef LitMenuSup As Literal, ByRef LitMenuIzq As Literal, ByVal sCodAplicacion As String)
        Dim i As Integer = 0
        Dim sTablas As String = _90_T01APLICACIONES.NombreTabla
        Dim sCampos As String = _90_T01APLICACIONES.Campo_90_T01Head & "," & _90_T01APLICACIONES.Campo_90_T01MenuIzq & "," & _90_T01APLICACIONES.Campo_90_T01MenuSup
        Dim sCondicion As String = _90_T01APLICACIONES.CampoLlave_90_T01Codigo & "='_04_1'"


        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            MostrarConsulta = myCmd.CommandText.ToString
            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            Do While myReader.Read()
                LitHead.Text = myReader.Item(0).ToString
                LitMenuIzq.Text = myReader.Item(1).ToString
                LitMenuSup.Text = myReader.Item(2).ToString
            Loop
            myReader.Close()
        Catch ex As Exception
            Mostrar_Error = " Error ejecutando consulta cargar Literales: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Sub


    '************************** carga rapida drop  *************************
    Public Sub list_CargarListBox(ByRef list As ListBox, ByVal sTablas As String, ByVal sCampos As String, Optional ByVal sCondicion As String = "", Optional ByVal bNulo As Boolean = True, Optional ByVal sAgrupar As String = "", Optional ByVal sOrder As String = "")
        Dim i As Integer = 0
        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Sub
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " GROUP BY " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If

            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            If myReader.HasRows Then
                If bNulo = True Then
                    list.Items.Add(New ListItem("Seleccionar", ""))
                End If
            End If
            Do While myReader.Read()
                Select Case myReader.FieldCount
                    Case 2
                        list.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                    Case 3
                        'drop.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        list.Items.Add(New ListItem(myReader.Item(1).ToString & " - " & myReader.Item(2).ToString, myReader.Item(0).ToString & myReader.Item(2).ToString))
                End Select
            Loop
            myReader.Close()
        Catch ex As Exception
            Mostrar_Error = " Error ejecutando consulta cargar drop: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Sub

    Public Sub chkList_Cargar(ByRef Chklist As CheckBoxList, ByVal sTablas As String, ByVal sCampos As String, Optional ByVal sCondicion As String = "", Optional ByVal bNulo As Boolean = True, Optional ByVal sAgrupar As String = "", Optional ByVal sOrder As String = "")
        Dim i As Integer = 0
        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Sub
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " GROUP BY " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If

            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            If myReader.HasRows Then
                If bNulo = True Then
                    Chklist.Items.Add(New ListItem("Seleccionar", ""))
                End If
            End If
            Do While myReader.Read()
                Select Case myReader.FieldCount
                    Case 2
                        Chklist.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                    Case 3
                        'drop.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        Chklist.Items.Add(New ListItem(myReader.Item(1).ToString & " - " & myReader.Item(2).ToString, myReader.Item(0).ToString & myReader.Item(2).ToString))
                End Select
            Loop
            myReader.Close()
        Catch ex As Exception
            Mostrar_Error = " Error ejecutando consulta cargar drop: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Sub


    '************************** carga rapida drop  *************************
    Public Sub drop_CargarCombox(ByRef drop As DropDownList, ByVal sTablas As String, ByVal sCampos As String, Optional ByVal sCondicion As String = "", Optional ByVal bNulo As Boolean = True, Optional ByVal sAgrupar As String = "", Optional ByVal sOrder As String = "", Optional ByVal sCadenaConexionTem As String = "")
        Dim i As Integer = 0
        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Sub
        End If
        'Abrir la conexión.
        Try
            If sCadenaConexionTem <> "" Then
                If myConn Is Nothing Then ConexionCrear(sCadenaConexionTem)
            Else
                If myConn Is Nothing Then ConexionCrear()
            End If

            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " GROUP BY " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If

            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            If myReader.HasRows Then
                If bNulo = True Then
                    If sTablas = "_90_T36TIPOSANGRE" Then
                        drop.Items.Add(New ListItem("-----", ""))
                    Else
                        drop.Items.Add(New ListItem("Seleccionar", ""))
                    End If
                End If
            End If
            Do While myReader.Read()
                Select Case myReader.FieldCount
                    Case 2
                        drop.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                    Case 3
                        'drop.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        drop.Items.Add(New ListItem(myReader.Item(1).ToString & " - " & myReader.Item(2).ToString, myReader.Item(0).ToString & myReader.Item(2).ToString))
                End Select
            Loop
            myReader.Close()
        Catch ex As Exception
            Mostrar_Error = " Error ejecutando consulta cargar drop: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Sub

    Public Sub drop_CargarCombox_Multiples(ByRef drop As DropDownList, ByVal sTablas As String, ByVal sCampos As String, Optional ByVal sCondicion As String = "", Optional ByVal bNulo As Boolean = True, Optional ByVal sAgrupar As String = "", Optional ByVal sOrder As String = "", Optional ByRef drop1 As DropDownList = Nothing, Optional ByRef drop2 As DropDownList = Nothing, Optional ByRef drop3 As DropDownList = Nothing, Optional ByRef drop4 As DropDownList = Nothing, Optional ByRef drop5 As DropDownList = Nothing, Optional ByRef drop6 As DropDownList = Nothing, Optional ByRef drop7 As DropDownList = Nothing)
        Dim i As Integer = 0
        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Sub
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " GROUP BY " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If

            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            If myReader.HasRows Then
                If bNulo = True Then
                    drop.Items.Add(New ListItem("Seleccionar", ""))
                End If
            End If
            Do While myReader.Read()
                Select Case myReader.FieldCount
                    Case 2
                        drop.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        If Not IsNothing(drop1) Then
                            drop1.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        End If
                        If Not IsNothing(drop2) Then
                            drop2.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        End If
                        If Not IsNothing(drop3) Then
                            drop3.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        End If
                        If Not IsNothing(drop4) Then
                            drop4.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        End If
                        If Not IsNothing(drop5) Then
                            drop5.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        End If
                        If Not IsNothing(drop6) Then
                            drop6.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        End If
                        If Not IsNothing(drop7) Then
                            drop7.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        End If

                    Case 3
                        'drop.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        drop.Items.Add(New ListItem(myReader.Item(1).ToString & " - " & myReader.Item(2).ToString, myReader.Item(0).ToString & myReader.Item(2).ToString))
                End Select
            Loop
            myReader.Close()
        Catch ex As Exception
            Mostrar_Error = " Error ejecutando consulta cargar drop: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Sub

    Public Sub drop_CargarCombox_Ajax(ByRef drop As AjaxControlToolkit.ComboBox, ByVal sTablas As String, ByVal sCampos As String, Optional ByVal sCondicion As String = "", Optional ByVal bNulo As Boolean = True, Optional ByVal sAgrupar As String = "")
        Dim i As Integer = 0
        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Sub
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " GROUP BY " & sAgrupar
            End If
            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            If myReader.HasRows Then
                If bNulo = True Then
                    drop.Items.Add(New ListItem("Seleccionar", ""))
                End If
            End If
            Do While myReader.Read()
                Select Case myReader.FieldCount
                    Case 2
                        drop.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                    Case 3
                        drop.Items.Add(New ListItem(myReader.Item(1).ToString & " - " & myReader.Item(2).ToString, myReader.Item(0).ToString & myReader.Item(2).ToString))
                End Select
            Loop
            myReader.Close()
        Catch ex As Exception
            Mostrar_Error = " Error ejecutando consulta cargar drop: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Sub

    Public Sub ListBox_Cargar(ByRef List As ListBox, ByVal sTablas As String, ByVal sCampos As String, Optional ByVal sCondicion As String = "", Optional ByVal bNulo As Boolean = True, Optional ByVal sAgrupar As String = "", Optional ByVal sOrder As String = "")
        Dim i As Integer = 0
        If sTablas = "" Or sCampos = "" Then
            MostrarError = "Por favor ingrese tablas, campos y condicion para seleccionar el registro"
            Exit Sub
        End If
        'Abrir la conexión.
        Try
            If myConn Is Nothing Then ConexionCrear()
            myCmd = myConn.CreateCommand
            'If Not myTrans Is Nothing Then myCmd.Transaction = myTrans
            If sCondicion <> "" Then
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas & " WHERE " & sCondicion
            Else
                myCmd.CommandText = "SELECT " & sCampos & " FROM " & sTablas
            End If
            If sAgrupar <> "" Then
                myCmd.CommandText = myCmd.CommandText & " GROUP BY " & sAgrupar
            End If
            If sOrder <> "" Then
                myCmd.CommandText = myCmd.CommandText & " Order by " & sOrder
            End If

            MostrarConsulta = myCmd.CommandText.ToString

            Select Case myConn.State
                Case ConnectionState.Closed
                    ConexionAbrir()
                Case ConnectionState.Connecting
            End Select
            myReader = myCmd.ExecuteReader()
            'Concatenar el resultado de la consulta en una cadena.
            If myReader.HasRows Then
                If bNulo = True Then
                    If sTablas = "_90_T36TIPOSANGRE" Then
                        List.Items.Add(New ListItem("-----", ""))
                    Else
                        List.Items.Add(New ListItem("Seleccionar", ""))
                    End If
                End If
            End If
            Do While myReader.Read()
                Select Case myReader.FieldCount
                    Case 2
                        List.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                    Case 3
                        'drop.Items.Add(New ListItem(myReader.Item(1).ToString, myReader.Item(0).ToString))
                        List.Items.Add(New ListItem(myReader.Item(1).ToString & " - " & myReader.Item(2).ToString, myReader.Item(0).ToString & myReader.Item(2).ToString))
                End Select
            Loop
            myReader.Close()
        Catch ex As Exception
            Mostrar_Error = " Error ejecutando consulta cargar drop: " & ex.ToString
        End Try
        'If myTrans Is Nothing Then ConexionCerrar()
    End Sub

    Public Sub drop_Cargar_SINO(ByRef drop As DropDownList, Optional ByVal bNulo As Boolean = True)
        If bNulo = True Then
            drop.Items.Add(New ListItem("", ""))
            drop.Items.Add(New ListItem("SI", "SI"))
            drop.Items.Add(New ListItem("NO", "NO"))
        Else
            drop.Items.Add(New ListItem("SI", "SI"))
            drop.Items.Add(New ListItem("NO", "NO"))
        End If
    End Sub

    Public Sub drop_Cargar_NA(ByRef drop As DropDownList, Optional ByVal bNulo As Boolean = True)
        drop.Items.Add(New ListItem("N", "N"))
        drop.Items.Add(New ListItem("A", "A"))
    End Sub

    Public Sub drop_Cargar_Superficie(ByRef drop As DropDownList, Optional ByVal bNulo As Boolean = True)
        drop.Items.Add(New ListItem("---", ""))
        drop.Items.Add(New ListItem("Vestibular", "V"))
        drop.Items.Add(New ListItem("Palatino", "P"))
        drop.Items.Add(New ListItem("Oclusal", "O"))
        drop.Items.Add(New ListItem("Distal", "D"))
        drop.Items.Add(New ListItem("Mesial", "M"))
    End Sub

    Public Sub drop_Cargar_Diente(ByRef drop As DropDownList, Optional ByVal bNulo As Boolean = True)
        drop.Items.Add(New ListItem("---", ""))
        drop.Items.Add(New ListItem("11", "11"))
        drop.Items.Add(New ListItem("12", "12"))
        drop.Items.Add(New ListItem("13", "13"))
        drop.Items.Add(New ListItem("14", "14"))
        drop.Items.Add(New ListItem("15", "15"))
        drop.Items.Add(New ListItem("16", "16"))
        drop.Items.Add(New ListItem("17", "17"))
        drop.Items.Add(New ListItem("18", "18"))
        drop.Items.Add(New ListItem("21", "21"))
        drop.Items.Add(New ListItem("22", "22"))
        drop.Items.Add(New ListItem("23", "23"))
        drop.Items.Add(New ListItem("24", "24"))
        drop.Items.Add(New ListItem("25", "25"))
        drop.Items.Add(New ListItem("26", "26"))
        drop.Items.Add(New ListItem("27", "27"))
        drop.Items.Add(New ListItem("28", "28"))
        drop.Items.Add(New ListItem("31", "31"))
        drop.Items.Add(New ListItem("32", "32"))
        drop.Items.Add(New ListItem("33", "33"))
        drop.Items.Add(New ListItem("34", "34"))
        drop.Items.Add(New ListItem("35", "35"))
        drop.Items.Add(New ListItem("36", "36"))
        drop.Items.Add(New ListItem("37", "37"))
        drop.Items.Add(New ListItem("38", "38"))
        drop.Items.Add(New ListItem("41", "41"))
        drop.Items.Add(New ListItem("42", "42"))
        drop.Items.Add(New ListItem("43", "43"))
        drop.Items.Add(New ListItem("44", "44"))
        drop.Items.Add(New ListItem("45", "45"))
        drop.Items.Add(New ListItem("46", "46"))
        drop.Items.Add(New ListItem("47", "47"))
        drop.Items.Add(New ListItem("48", "48"))
        drop.Items.Add(New ListItem("51", "51"))
        drop.Items.Add(New ListItem("52", "52"))
        drop.Items.Add(New ListItem("53", "53"))
        drop.Items.Add(New ListItem("54", "54"))
        drop.Items.Add(New ListItem("55", "55"))
        drop.Items.Add(New ListItem("61", "61"))
        drop.Items.Add(New ListItem("62", "62"))
        drop.Items.Add(New ListItem("63", "63"))
        drop.Items.Add(New ListItem("64", "64"))
        drop.Items.Add(New ListItem("65", "65"))
        drop.Items.Add(New ListItem("71", "71"))
        drop.Items.Add(New ListItem("72", "72"))
        drop.Items.Add(New ListItem("73", "73"))
        drop.Items.Add(New ListItem("74", "74"))
        drop.Items.Add(New ListItem("75", "75"))
        drop.Items.Add(New ListItem("81", "81"))
        drop.Items.Add(New ListItem("82", "82"))
        drop.Items.Add(New ListItem("83", "83"))
        drop.Items.Add(New ListItem("84", "84"))
        drop.Items.Add(New ListItem("85", "85"))

    End Sub

    Public Sub drop_Cargar_SINO_AjaX(ByRef drop As AjaxControlToolkit.ComboBox, Optional ByVal bNulo As Boolean = True)
        If bNulo = True Then
            drop.Items.Add(New ListItem("Seleccionar", ""))
            drop.Items.Add(New ListItem("SI", "SI"))
            drop.Items.Add(New ListItem("NO", "NO"))
        Else
            drop.Items.Add(New ListItem("SI", "SI"))
            drop.Items.Add(New ListItem("NO", "NO"))
        End If
    End Sub

    Public Sub drop_Cargar_NormalAnormal_AjaX(ByRef drop As AjaxControlToolkit.ComboBox, Optional ByVal bNulo As Boolean = True)
        If bNulo = True Then
            drop.Items.Add(New ListItem("", ""))
            drop.Items.Add(New ListItem("Nomnal", "N"))
            drop.Items.Add(New ListItem("Anormal", "A"))
        Else
            drop.Items.Add(New ListItem("Nomnal", "N"))
            drop.Items.Add(New ListItem("Anormal", "A"))
        End If
    End Sub

    Public Sub drop_Cargar_Sexo_AjaX(ByRef drop As AjaxControlToolkit.ComboBox, Optional ByVal bNulo As Boolean = True)
        If bNulo = True Then
            drop.Items.Add(New ListItem("Seleccionar", ""))
            drop.Items.Add(New ListItem("Femenino", "F"))
            drop.Items.Add(New ListItem("Masculino", "M"))
        Else
            drop.Items.Add(New ListItem("Femenino", "F"))
            drop.Items.Add(New ListItem("Masculino", "M"))
        End If
    End Sub

    Public Sub drop_Cargar_NormalAnormal(ByRef drop As DropDownList, Optional ByVal bNulo As Boolean = True)
        If bNulo = True Then
            drop.Items.Add(New ListItem("Seleccionar", ""))
            drop.Items.Add(New ListItem("Normal", "N"))
            drop.Items.Add(New ListItem("Anormal", "A"))
        Else
            drop.Items.Add(New ListItem("Normal", "N"))
            drop.Items.Add(New ListItem("Anormal", "A"))
        End If
    End Sub

    Public Sub drop_Cargar_PositivoNegativo(ByRef drop As DropDownList, Optional ByVal bNulo As Boolean = True)
        If bNulo = True Then
            drop.Items.Add(New ListItem("Seleccionar", ""))
            drop.Items.Add(New ListItem("Positivo", "P"))
            drop.Items.Add(New ListItem("Negativo", "N"))
        Else
            drop.Items.Add(New ListItem("Positivo", "P"))
            drop.Items.Add(New ListItem("Negativo", "N"))
        End If
    End Sub

    Public Sub drop_Cargar_RealizadoNoRealizado(ByRef drop As DropDownList, Optional ByVal bNulo As Boolean = True)
        If bNulo = True Then
            drop.Items.Add(New ListItem("Seleccionar", ""))
            drop.Items.Add(New ListItem("No Realizado", "No Realizado"))
            drop.Items.Add(New ListItem("Realizado", "Realizado"))
        Else
            drop.Items.Add(New ListItem("No Realizado", "No Realizado"))
            drop.Items.Add(New ListItem("Realizado", "Realizado"))
        End If
    End Sub

    Public Sub drop_Cargar_Sexo(ByRef drop As DropDownList, Optional ByVal bNulo As Boolean = True)
        If bNulo = True Then
            drop.Items.Add(New ListItem("Seleccionar", ""))
            drop.Items.Add(New ListItem("Femenino", "F"))
            drop.Items.Add(New ListItem("Masculino", "M"))
        Else
            drop.Items.Add(New ListItem("Femenino", "F"))
            drop.Items.Add(New ListItem("Masculino", "M"))
        End If
    End Sub

    Public Function sMostrarFecha(ByVal sFecha As String) As String
        sMostrarFecha = ""
        If sFecha = "01/01/1900 12:00:00 a.m." Then
            sMostrarFecha = ""
        Else
            sMostrarFecha = sFecha
        End If
    End Function

    Public Function sRemoverHTML(ByVal sTexto As String) As String
        Dim RegExp As String = "<[^>]*>"
        Dim r As New Regex(RegExp)
        sRemoverHTML = r.Replace(sTexto, "")
    End Function


    Public Function bVerificarSQL(ByVal sTexto As String) As Boolean
        bVerificarSQL = False
        Dim sTxt As String
        sTxt = UCase(sTexto)
        If bVerificarSQL = InStr(sTxt, "INSERT") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "UPDATE") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "SELECT") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "DELETE") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "DROP") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "CREATE") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "HAVING") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "SUBSTRING") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "ALTER TABLE") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "ALTER") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "INSERT") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "FROM") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "INSERT") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "WHERE") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "EXISTS") Then Exit Function
        If bVerificarSQL = InStr(sTxt, "DISTINCT") Then Exit Function
    End Function


    Public Function letras2(ByVal numero As String) As String
        Dim sNumero, Negativo, decimales As String
        sNumero = Trim(numero)
        Negativo = Mid(numero, 1, 1)
        If Negativo <> "-" Then
            Negativo = ""
        End If
        Dim TestArray() As String = Split(sNumero, ",")
        If TestArray.Length > 2 Then
            decimales = TestArray(1).ToString
        Else
            decimales = ""
        End If
        Dim lCaracteres As Integer = sNumero.Length

        Select Case lCaracteres
            Case 15
            Case 12
            Case 9
            Case 6
            Case 3

        End Select

    End Function
    Private Function Letras_Miles_Millones(ByVal NumCaracteres As Integer)

        Dim sPalabra As String = ""
        Select Case NumCaracteres
            Case 18
                sPalabra = " Mil Billonones "
            Case 15
                sPalabra = " Billon "
            Case 12
                sPalabra = " Mil Millones"
            Case 9
                sPalabra = " Millones "
            Case 6
                sPalabra = " Mil "
            Case 3
                sPalabra = " Pesos "
        End Select
        Letras_Miles_Millones = sPalabra
    End Function

    Private Function Letras_VAlor(ByVal numero As String, ByVal Tipo As String)
        Dim sPalabra As String = ""

        For m = 1 To numero.Length

        Next

        Select Case numero.Length
            Case 1, 2, 3
                For x = 1 To numero.Length
                    Select Case Mid(numero, 1, x)
                        Case "1"
                            sPalabra = sPalabra & " UN" & Letras_Miles_Millones(numero)
                        Case "2"
                            sPalabra = sPalabra & " Dos" & Letras_Miles_Millones(numero)
                        Case "3"
                            sPalabra = sPalabra & " Tres" & Letras_Miles_Millones(numero)
                        Case "4"
                            sPalabra = sPalabra & " Cuatro" & Letras_Miles_Millones(numero)
                        Case "5"
                            sPalabra = sPalabra & " Cinco" & Letras_Miles_Millones(numero)
                        Case "6"
                            sPalabra = sPalabra & " Seis" & Letras_Miles_Millones(numero)
                        Case "7"
                            sPalabra = sPalabra & " Siete" & Letras_Miles_Millones(numero)
                        Case "8"
                            sPalabra = sPalabra & " Ocho" & Letras_Miles_Millones(numero)
                        Case "9"
                            sPalabra = sPalabra & " Nueve" & Letras_Miles_Millones(numero)
                    End Select
                Next
            Case 4, 5, 6
                For x = 1 To numero.Length
                    Select Case Mid(numero, 1, x)
                        Case "1"
                            sPalabra = sPalabra & " Diez" & Letras_Miles_Millones(numero)
                        Case "2"
                            sPalabra = sPalabra & " Veinte" & Letras_Miles_Millones(numero)
                        Case "3"
                            sPalabra = sPalabra & " Treinta" & Letras_Miles_Millones(numero)
                        Case "4"
                            sPalabra = sPalabra & " Cuarenta" & Letras_Miles_Millones(numero)
                        Case "5"
                            sPalabra = sPalabra & " Cincuenta" & Letras_Miles_Millones(numero)
                        Case "6"
                            sPalabra = sPalabra & " Sesenta" & Letras_Miles_Millones(numero)
                        Case "7"
                            sPalabra = sPalabra & " Setenta" & Letras_Miles_Millones(numero)
                        Case "8"
                            sPalabra = sPalabra & " Ochenta" & Letras_Miles_Millones(numero)
                        Case "9"
                            sPalabra = sPalabra & " Noventa" & Letras_Miles_Millones(numero)
                    End Select
                Next

            Case 7, 8, 9
                For x = 1 To numero.Length
                    If x = 1 Then
                        Select Case Mid(numero, x, 1)
                            Case "1"
                                sPalabra = sPalabra & " Cien" & Letras_Miles_Millones(numero)
                            Case "2"
                                sPalabra = sPalabra & " Doscientos" & Letras_Miles_Millones(numero)
                            Case "3"
                                sPalabra = sPalabra & " Trescientos" & Letras_Miles_Millones(numero)
                            Case "4"
                                sPalabra = sPalabra & " Cuatrocientos" & Letras_Miles_Millones(numero)
                            Case "5"
                                sPalabra = sPalabra & " Quinientos" & Letras_Miles_Millones(numero)
                            Case "6"
                                sPalabra = sPalabra & " Seiscientos" & Letras_Miles_Millones(numero)
                            Case "7"
                                sPalabra = sPalabra & " Setecientos" & Letras_Miles_Millones(numero)
                            Case "8"
                                sPalabra = sPalabra & " Ochocientos" & Letras_Miles_Millones(numero)
                            Case "9"
                                sPalabra = sPalabra & " Novecientos" & Letras_Miles_Millones(numero)
                        End Select

                    ElseIf x = 2 Then
                        numero = Mid(numero, x, numero.Length - x)
                        Select Case Mid(numero, 1, 1)
                            Case "1"
                                sPalabra = sPalabra & " Cien" & Letras_Miles_Millones(numero)
                            Case "2"
                                sPalabra = sPalabra & " Doscientos" & Letras_Miles_Millones(numero)
                            Case "3"
                                sPalabra = sPalabra & " Trescientos" & Letras_Miles_Millones(numero)
                            Case "4"
                                sPalabra = sPalabra & " Cuatrocientos" & Letras_Miles_Millones(numero)
                            Case "5"
                                sPalabra = sPalabra & " Quinientos" & Letras_Miles_Millones(numero)
                            Case "6"
                                sPalabra = sPalabra & " Seiscientos" & Letras_Miles_Millones(numero)
                            Case "7"
                                sPalabra = sPalabra & " Setecientos" & Letras_Miles_Millones(numero)
                            Case "8"
                                sPalabra = sPalabra & " Ochocientos" & Letras_Miles_Millones(numero)
                            Case "9"
                                sPalabra = sPalabra & " Novecientos" & Letras_Miles_Millones(numero)
                        End Select
                    ElseIf x = 3 Then

                    End If
                Next
        End Select

        Select Case Tipo
            Case 11
            Case 15
                For x = 1 To numero.Length
                    Select Case Mid(numero, 1, x)
                        Case "1"
                            sPalabra = sPalabra & " UN"
                        Case "2"
                            sPalabra = sPalabra & " Dos"
                        Case "3"
                            sPalabra = sPalabra & " Tres"
                        Case "4"
                            sPalabra = sPalabra & " Cuatro"
                        Case "5"
                            sPalabra = sPalabra & " Cinco"
                        Case "6"
                            sPalabra = sPalabra & " Seis"
                        Case "7"
                            sPalabra = sPalabra & " Siete"
                        Case "8"
                            sPalabra = sPalabra & " Ocho"
                        Case "9"
                            sPalabra = sPalabra & " Nueve"
                    End Select
                Next

            Case 12
                sPalabra = " Mil Millones"
            Case 9
                sPalabra = " Millones "
            Case 6
                sPalabra = " Mil "
            Case 3
                sPalabra = " Pesos "
        End Select

        Letras_VAlor = sPalabra
    End Function

    Public Function Letras(ByVal numero As String) As String
        '********Declara variables de tipo cadena************
        Dim palabras, entero, dec, flag As String

        '********Declara variables de tipo entero***********
        Dim num, x, y As Integer

        flag = "N"

        '**********Número Negativo***********
        If Mid(numero, 1, 1) = "-" Then
            numero = Mid(numero, 2, numero.ToString.Length - 1).ToString
            palabras = "menos "
        End If

        '**********Si tiene ceros a la izquierda*************
        For x = 1 To numero.ToString.Length
            If Mid(numero, 1, 1) = "0" Then
                numero = Trim(Mid(numero, 2, numero.ToString.Length).ToString)
                If Trim(numero.ToString.Length) = 0 Then palabras = ""
            Else
                Exit For
            End If
        Next

        '*********Dividir parte entera y decimal************
        For y = 1 To Len(numero)
            If Mid(numero, y, 1) = "." Then
                flag = "S"
            Else
                If flag = "N" Then
                    entero = entero + Mid(numero, y, 1)
                Else
                    dec = dec + Mid(numero, y, 1)
                End If
            End If
        Next y

        If Len(dec) = 1 Then dec = dec & "0"

        '**********proceso de conversión***********
        flag = "N"

        If Val(numero) <= 999999999 Then
            For y = Len(entero) To 1 Step -1
                num = Len(entero) - (y - 1)
                Select Case y
                    Case 3, 6, 9
                        '**********Asigna las palabras para las centenas***********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" And Mid(entero, num + 2, 1) = "0" Then
                                    palabras = palabras & "cien "
                                Else
                                    palabras = palabras & "ciento "
                                End If
                            Case "2"
                                palabras = palabras & "doscientos "
                            Case "3"
                                palabras = palabras & "trescientos "
                            Case "4"
                                palabras = palabras & "cuatrocientos "
                            Case "5"
                                palabras = palabras & "quinientos "
                            Case "6"
                                palabras = palabras & "seiscientos "
                            Case "7"
                                palabras = palabras & "setecientos "
                            Case "8"
                                palabras = palabras & "ochocientos "
                            Case "9"
                                palabras = palabras & "novecientos "
                        End Select
                    Case 2, 5, 8
                        '*********Asigna las palabras para las decenas************
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    flag = "S"
                                    palabras = palabras & "diez "
                                End If
                                If Mid(entero, num + 1, 1) = "1" Then
                                    flag = "S"
                                    palabras = palabras & "once "
                                End If
                                If Mid(entero, num + 1, 1) = "2" Then
                                    flag = "S"
                                    palabras = palabras & "doce "
                                End If
                                If Mid(entero, num + 1, 1) = "3" Then
                                    flag = "S"
                                    palabras = palabras & "trece "
                                End If
                                If Mid(entero, num + 1, 1) = "4" Then
                                    flag = "S"
                                    palabras = palabras & "catorce "
                                End If
                                If Mid(entero, num + 1, 1) = "5" Then
                                    flag = "S"
                                    palabras = palabras & "quince "
                                End If
                                If Mid(entero, num + 1, 1) > "5" Then
                                    flag = "N"
                                    palabras = palabras & "dieci"
                                End If
                            Case "2"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "veinte "
                                    flag = "S"
                                Else
                                    palabras = palabras & "veinti"
                                    flag = "N"
                                End If
                            Case "3"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "treinta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "treinta y "
                                    flag = "N"
                                End If
                            Case "4"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cuarenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cuarenta y "
                                    flag = "N"
                                End If
                            Case "5"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cincuenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cincuenta y "
                                    flag = "N"
                                End If
                            Case "6"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "sesenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "sesenta y "
                                    flag = "N"
                                End If
                            Case "7"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "setenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "setenta y "
                                    flag = "N"
                                End If
                            Case "8"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "ochenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "ochenta y "
                                    flag = "N"
                                End If
                            Case "9"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "noventa "
                                    flag = "S"
                                Else
                                    palabras = palabras & "noventa y "
                                    flag = "N"
                                End If
                        End Select
                    Case 1, 4, 7
                        '*********Asigna las palabras para las unidades*********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If flag = "N" Then
                                    If y = 1 Then
                                        palabras = palabras & "uno "
                                    Else
                                        palabras = palabras & "un "
                                    End If
                                End If
                            Case "2"
                                If flag = "N" Then palabras = palabras & "dos "
                            Case "3"
                                If flag = "N" Then palabras = palabras & "tres "
                            Case "4"
                                If flag = "N" Then palabras = palabras & "cuatro "
                            Case "5"
                                If flag = "N" Then palabras = palabras & "cinco "
                            Case "6"
                                If flag = "N" Then palabras = palabras & "seis "
                            Case "7"
                                If flag = "N" Then palabras = palabras & "siete "
                            Case "8"
                                If flag = "N" Then palabras = palabras & "ocho "
                            Case "9"
                                If flag = "N" Then palabras = palabras & "nueve "
                        End Select
                End Select

                '***********Asigna la palabra mil***************
                If y = 4 Then
                    If Mid(entero, 6, 1) <> "0" Or Mid(entero, 5, 1) <> "0" Or Mid(entero, 4, 1) <> "0" Or
                    (Mid(entero, 6, 1) = "0" And Mid(entero, 5, 1) = "0" And Mid(entero, 4, 1) = "0" And
                    Len(entero) <= 6) Then palabras = palabras & "mil "
                End If

                '**********Asigna la palabra millón*************
                If y = 7 Then
                    If Len(entero) = 7 And Mid(entero, 1, 1) = "1" Then
                        palabras = palabras & "millón "
                    Else
                        palabras = palabras & "millones "
                    End If
                End If
            Next y

            '**********Une la parte entera y la parte decimal*************
            If dec <> "" Then
                Letras = palabras & "con " & dec
            Else
                Letras = palabras
            End If
        Else
            Letras = ""
        End If
    End Function

    Public Function ValorEnLetras(ByVal value As Double) As String
        Select Case value
            Case 0 : ValorEnLetras = "CERO"
            Case 1 : ValorEnLetras = "UN"
            Case 2 : ValorEnLetras = "DOS"
            Case 3 : ValorEnLetras = "TRES"
            Case 4 : ValorEnLetras = "CUATRO"
            Case 5 : ValorEnLetras = "CINCO"
            Case 6 : ValorEnLetras = "SEIS"
            Case 7 : ValorEnLetras = "SIETE"
            Case 8 : ValorEnLetras = "OCHO"
            Case 9 : ValorEnLetras = "NUEVE"
            Case 10 : ValorEnLetras = "DIEZ"
            Case 11 : ValorEnLetras = "ONCE"
            Case 12 : ValorEnLetras = "DOCE"
            Case 13 : ValorEnLetras = "TRECE"
            Case 14 : ValorEnLetras = "CATORCE"
            Case 15 : ValorEnLetras = "QUINCE"
            Case Is < 20 : ValorEnLetras = "DIECI" & ValorEnLetras(value - 10)
            Case 20 : ValorEnLetras = "VEINTE"
            Case Is < 30 : ValorEnLetras = "VEINTI" & ValorEnLetras(value - 20)
            Case 30 : ValorEnLetras = "TREINTA"
            Case 40 : ValorEnLetras = "CUARENTA"
            Case 50 : ValorEnLetras = "CINCUENTA"
            Case 60 : ValorEnLetras = "SESENTA"
            Case 70 : ValorEnLetras = "SETENTA"
            Case 80 : ValorEnLetras = "OCHENTA"
            Case 90 : ValorEnLetras = "NOVENTA"
            Case Is < 100 : ValorEnLetras = ValorEnLetras(Int(value \ 10) * 10) & " Y " & ValorEnLetras(value Mod 10)
            Case 100 : ValorEnLetras = "CIEN"
            Case Is < 200 : ValorEnLetras = "CIENTO " & ValorEnLetras(value - 100)
            Case 200, 300, 400, 600, 800 : ValorEnLetras = ValorEnLetras(Int(value \ 100)) & "CIENTOS"
            Case 500 : ValorEnLetras = "QUINIENTOS"
            Case 700 : ValorEnLetras = "SETECIENTOS"
            Case 900 : ValorEnLetras = "NOVECIENTOS"
            Case Is < 1000 : ValorEnLetras = ValorEnLetras(Int(value \ 100) * 100) & " " & ValorEnLetras(value Mod 100)
            Case 1000 : ValorEnLetras = "MIL"
            Case Is < 2000 : ValorEnLetras = "MIL " & ValorEnLetras(value Mod 1000)
            Case Is < 1000000 : ValorEnLetras = ValorEnLetras(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then ValorEnLetras = ValorEnLetras & " " & ValorEnLetras(value Mod 1000)
            Case 1000000 : ValorEnLetras = "UN MILLON"
            Case Is < 2000000 : ValorEnLetras = "UN MILLON " & ValorEnLetras(value Mod 1000000)
            Case Is < 1000000000000.0# : ValorEnLetras = ValorEnLetras(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then ValorEnLetras = ValorEnLetras & " " & ValorEnLetras(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : ValorEnLetras = "UN BILLON"
            Case Is < 2000000000000.0# : ValorEnLetras = "UN BILLON " & ValorEnLetras(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : ValorEnLetras = ValorEnLetras(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then ValorEnLetras = ValorEnLetras & " " & ValorEnLetras(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

End Class

Module ModGeneral

    Private Function BuscarMAC(ByVal PhysicalAddress As String) As Boolean
        Dim i As Integer
        Dim MACS_Registradas As String = ""

        MACS_Registradas = MACS_Registradas & "MAC 68A3C4ECD920 - Medico Juan Carlos Gutierres"
        MACS_Registradas = MACS_Registradas & "MAC E06995D115FF - Medico Juan Carlos Gutierres"
        MACS_Registradas = MACS_Registradas & "MAC {7E1417D8-0004-4207-B542-B3C729D19335} {0045255E-7BFE-44DB-8441-5A80D45F95DC} {932B7062-9EB4-4827-B55D-A20F600DBCB3}   Medico Juan Carlos Gutierres"
        MACS_Registradas = MACS_Registradas & "MAC 0025644213D5 00265E1C0423   -  pc 2 alejo"
        MACS_Registradas = MACS_Registradas & "MAC 00155E003904 00155E003903   - Servidor WEB, pc alejo CC52AF0E5F90 - 78E3B54663AD"
        MACS_Registradas = MACS_Registradas & "MAC 00155E003904 00155E003903   - Servidor WEB,"
        MACS_Registradas = MACS_Registradas & "MAC 74D02B471087 - Medico Miguel Angel Curvelo Sanchez cc 86073217,"
        MACS_Registradas = MACS_Registradas & "MAC 3859F9BA7E9E - Yenifer Bermudez,"
        MACS_Registradas = MACS_Registradas & "MAC 70F1A1FA4B6F - B8AC6F7B5DA6 Medico Mauricio Acosta cc 19359786,"
        MACS_Registradas = MACS_Registradas & "MAC 00138F83144A - 10FEED1110AF Medico Mauricio Acosta cc 19359786,"
        MACS_Registradas = MACS_Registradas & "MAC 001E683BF732 - 0021000919BD - Medico Guillermo Campo ,"
        MACS_Registradas = MACS_Registradas & "MAC 0019B973DEE6 - 001E683DF732 - Medico Sandra Ximena Cespedes,"
        MACS_Registradas = MACS_Registradas & "MAC 26DE2BAC94CF - 2C768AD94A2B - 1C3947CA8F90 - 76DFBF37B38D Liga de Lucha Contra el Cancer,"
        MACS_Registradas = MACS_Registradas & "MAC 00238BFABE55 - Centro Vascular de los Llans,"
        MACS_Registradas = MACS_Registradas & "MAC D850E6B884D3 - IPS UniSalud Vital"
        MACS_Registradas = MACS_Registradas & "MAC 0025AB3A7570 - BC8556040C25 - Medico Miguel Parrado"
        MACS_Registradas = MACS_Registradas & "MAC 0025AB3A7570 - E06995E57EFE - F8A9633F3F71 - 18CF5EB9EFC6  Medico Alberto muños"
        MACS_Registradas = MACS_Registradas & "MAC 1AEE65E0E98A - 2025647BE670 -  BSEE65E0E98A Medico Jose Ignacion Pardo"
        MACS_Registradas = MACS_Registradas & "MAC 68A3C4ECD920 - E06995D115FF - {7E1417D8-0004-4207-B542-B3C729D19335} {0045255E-7BFE-44DB-8441-5A80D45F95DC} {932B7062-9EB4-4827-B55D-A20F600DBCB3}   Medico Juan Carlos Gutierres"
        MACS_Registradas = MACS_Registradas & "MAC BSEE652B6C07 - 0025AB59105D - 00235A367A88 - IPS AURIS"
        MACS_Registradas = MACS_Registradas & "MAC BSEE652B6C07 - 28E3470A5516 - 0025AB4E90F4 - IPS MULTIDELTA CASTILLA"
        MACS_Registradas = MACS_Registradas & "MAC BSEE652B6C07 - 1ACF5E267E25 - 18CF5E267E25 - IPS MULTIDELTA CASTILLA PORTATIL"
        MACS_Registradas = MACS_Registradas & "MAC F8A9633931C8 - A4DB309A30BB - IPS MULTIDELTA ACACIAS PORTATIL"
        MACS_Registradas = MACS_Registradas & "MAC 9CD21EBF936D - 28D244642C31 - Medico Gabriel Sanchez"
        MACS_Registradas = MACS_Registradas & "MAC 1E0EC41A7CC3 - EC0EC41A7CC3 - Liga de Lucha Contra el Cancer Villavicencio"
        MACS_Registradas = MACS_Registradas & "MAC C4E9840CD733 - 78E3B54627D3 - Liga de Lucha Contra el Cancer Villavicencio"
        MACS_Registradas = MACS_Registradas & "MAC 18CF5EB61B15 - 0025AB627442 - FCAA140F7477  IPS Previs Pto Gaitan"
        MACS_Registradas = MACS_Registradas & "MAC 40A8F0A9C0CD - 5C93A2C4FBA1 - IPS Prever"

        MACS_Registradas = MACS_Registradas & "MAC 5C93A2FAF2B3 - F0761C1A2D8A - Dr. Cristian Rojas"
        MACS_Registradas = MACS_Registradas & "MAC 5C93A2FAF2B3 - 8056F240B362 - Dr. Ivonne aldana"

        MACS_Registradas = MACS_Registradas & "MAC FCAA140F7B7B - HEALTH WORKERS"

        'MACS_Registradas = MACS_Registradas & "MAC 1E71D99C2405 - Medico Miguel Angel Curvelo Sanchez cc 86073217, "

        i = InStr(MACS_Registradas, PhysicalAddress)
        If i > 0 Then
            BuscarMAC = True
        Else
            BuscarMAC = False
        End If
    End Function

    Function MAC_DireccionFisicaMAC() As Boolean
        MAC_DireccionFisicaMAC = False
        Dim nics As System.Net.NetworkInformation.NetworkInterface() = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
        Dim adapter As System.Net.NetworkInformation.NetworkInterface
        For Each adapter In nics
            If Trim(adapter.GetPhysicalAddress.ToString) = "" Then Exit For
            If BuscarMAC(adapter.GetPhysicalAddress.ToString) = True Then
                MAC_DireccionFisicaMAC = True
                Exit For
            Else
                MAC_DireccionFisicaMAC = False
            End If
            ' adapter.Id & " - " & adapter.Name & " - " & adapter.NetworkInterfaceType.ToString & " - " & adapter.GetPhysicalAddress.ToString
        Next adapter
    End Function

    Public Function bValidarEmail(ByVal sEmail As String) As Boolean
        bValidarEmail = False
        Dim emailRegex As New System.Text.RegularExpressions.Regex("^(?<user>[^@]+)@(?<host>.+)$")
        Dim emailMatch As System.Text.RegularExpressions.Match =
           emailRegex.Match(sEmail)
        Return emailMatch.Success
    End Function

    Public Function sNombreDiaFecha(ByVal dFecha As Date) As String
        sNombreDiaFecha = ""
        If IsDate(dFecha) = False Then Exit Function
        Dim dDate As Date
        dDate = dFecha
        Select Case UCase(dDate.DayOfWeek.ToString)
            Case "MONDAY"
                sNombreDiaFecha = "LUNES" & " " & dDate.Day
            Case "TUESDAY"
                sNombreDiaFecha = "MARTES" & " " & dDate.Day
            Case "WEDNESDAY"
                sNombreDiaFecha = "MIERCOLES" & " " & dDate.Day
            Case "THURSDAY"
                sNombreDiaFecha = "JUEVES" & " " & dDate.Day
            Case "FRIDAY"
                sNombreDiaFecha = "VIERNES" & " " & dDate.Day
            Case "SATURDAY"
                sNombreDiaFecha = "SABADO" & " " & dDate.Day
            Case "SUNDAY"
                sNombreDiaFecha = "DOMINGO" & " " & dDate.Day
            Case Else
                sNombreDiaFecha = UCase(dDate.DayOfWeek.ToString)
        End Select

        Select Case dDate.Month
            Case 1
                sNombreDiaFecha = sNombreDiaFecha & " de ENERO de " & dDate.Year
            Case 2
                sNombreDiaFecha = sNombreDiaFecha & " de FEBRERO de " & dDate.Year
            Case 3
                sNombreDiaFecha = sNombreDiaFecha & " de MARZO de " & dDate.Year
            Case 4
                sNombreDiaFecha = sNombreDiaFecha & " de ABRIL de " & dDate.Year
            Case 5
                sNombreDiaFecha = sNombreDiaFecha & " de MAYO de " & dDate.Year
            Case 6
                sNombreDiaFecha = sNombreDiaFecha & " de JUNIO de " & dDate.Year
            Case 7
                sNombreDiaFecha = sNombreDiaFecha & " de JULIO de " & dDate.Year
            Case 8
                sNombreDiaFecha = sNombreDiaFecha & " de AGOSTO de " & dDate.Year
            Case 9
                sNombreDiaFecha = sNombreDiaFecha & " de SEPTIEMBRE de " & dDate.Year
            Case 10
                sNombreDiaFecha = sNombreDiaFecha & " de OCTUBRE de " & dDate.Year
            Case 11
                sNombreDiaFecha = sNombreDiaFecha & " de NOVIEMBRE de " & dDate.Year
            Case 12
                sNombreDiaFecha = sNombreDiaFecha & " de DICIEMBRE de " & dDate.Year
        End Select


    End Function

    Public Function bValidarFecha(ByRef dFechas As Date, iDia As Integer, ByVal iMes As Integer, iAgno As Integer, Optional ByRef sFechaArmada As String = "") As Boolean
        bValidarFecha = False
        Dim iDiames As Integer
        Dim sDia As String = ""
        Dim sMes As String = ""
        If iDia = 0 Or iMes = 0 Or iAgno = 0 Then Exit Function
        If iMes > 12 Then Exit Function
        iDiames = Date.DaysInMonth(iAgno, iMes)
        If iDia > iDiames Then Exit Function
        Dim dDate = DateSerial(iAgno, iMes, iDia)
        If IsDate(dDate) Then
            dFechas = dDate
            bValidarFecha = True
            Select Case dFechas.Day
                Case 1
                    sDia = "01"
                Case 2
                    sDia = "02"
                Case 3
                    sDia = "03"
                Case 4
                    sDia = "04"
                Case 5
                    sDia = "05"
                Case 6
                    sDia = "06"
                Case 7
                    sDia = "07"
                Case 8
                    sDia = "08"
                Case 9
                    sDia = "09"
                Case Else
                    sDia = dFechas.Day
            End Select
            Select Case dFechas.Month
                Case 1
                    sMes = "01"
                Case 2
                    sMes = "02"
                Case 3
                    sMes = "03"
                Case 4
                    sMes = "04"
                Case 5
                    sMes = "05"
                Case 6
                    sMes = "06"
                Case 7
                    sMes = "07"
                Case 8
                    sMes = "08"
                Case 9
                    sMes = "09"
                Case Else
                    sMes = dFechas.Month
            End Select
            If sMes <> "" And sDia <> "" Then
                sFechaArmada = sDia & "/" & sMes & "/" & dFechas.Year
            End If
        End If
    End Function

    Public Function sHora_AM_PM(ByVal sHora As String, ByVal sMinuto_Actual As String) As String
        Dim sMinuto As String = IIf(Val(sMinuto_Actual) < 10, "0" & Trim(CStr(Val(sMinuto_Actual))), sMinuto_Actual)
        Select Case Val(sHora)
            Case 12
                sHora_AM_PM = "12:" & sMinuto & " m"
            Case 13
                sHora_AM_PM = "01:" & sMinuto & " pm"
            Case 14
                sHora_AM_PM = "02:" & sMinuto & " pm"
            Case 15
                sHora_AM_PM = "03:" & sMinuto & " pm"
            Case 16
                sHora_AM_PM = "04:" & sMinuto & " pm"
            Case 17
                sHora_AM_PM = "05:" & sMinuto & " pm"
            Case 18
                sHora_AM_PM = "06:" & sMinuto & " pm"
            Case 19
                sHora_AM_PM = "07:" & sMinuto & " pm"
            Case 20
                sHora_AM_PM = "08:" & sMinuto & " pm"
            Case 21
                sHora_AM_PM = "09:" & sMinuto & " pm"
            Case 22
                sHora_AM_PM = "10:" & sMinuto & " pm"
            Case 23
                sHora_AM_PM = "11:" & sMinuto & " pm"
            Case 24
                sHora_AM_PM = "12:" & sMinuto & " am"
            Case Else
                sHora_AM_PM = sHora & ":" & sMinuto & " am"
        End Select
    End Function

    Function calcularDigitoVerificacion(ByVal Documento As String) As String
        Dim res As Integer
        If (Integer.TryParse(Documento, res) = True) Then
            Dim Acumulador As Integer = 0
            Dim Vector As Integer() = {0, 3, 7, 13, 17, 19, 23, 29, 37, 41, 43, 47, 53, 59, 67, 71}
            Dim DocumentoArray As Char() = Documento.ToCharArray()
            Dim max As Integer = DocumentoArray.Length
            For Contador As Integer = 0 To Documento.Length - 1
                Dim Temp As Integer = Integer.Parse(Documento.Substring(Contador, 1))
                Acumulador += (Temp * Vector(Documento.Length - Contador))
            Next
            Dim Residuo As Integer = Acumulador Mod 11
            If (Residuo > 1) Then
                Return "" & 11 - Residuo
            Else
                Return Residuo.ToString()
            End If
        Else
            Return ""
        End If

    End Function

    Public Function ConvertirTexto_Moneda(sCadenaNumero_Valor)
        Dim siDecimaloComa As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator

        Dim TestArray() As String = Split(sCadenaNumero_Valor, siDecimaloComa)
        Dim sValorBase_Entera As String = TestArray(0).ToString
        Dim sValorBase_Decimal As String = ""

        Try
            sValorBase_Decimal = TestArray(1).ToString
        Catch ex As Exception
            sValorBase_Decimal = ""
        End Try
        Dim sValorReal As String = ""
        Select Case sValorBase_Entera.Length
            Case 1
                If sValorBase_Decimal <> "" Then
                    sValorReal = sValorBase_Entera & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = sValorBase_Entera
                End If
            Case 2
                If sValorBase_Decimal <> "" Then
                    sValorReal = sValorBase_Entera & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = sValorBase_Entera
                End If
            Case 3
                If sValorBase_Decimal <> "" Then
                    sValorReal = sValorBase_Entera & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = sValorBase_Entera
                End If
            Case 4
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3)
                End If
            Case 5
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3)
                End If
            Case 6
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3)
                End If
            Case 7
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3)
                End If
            Case 8
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3)
                End If

            Case 9
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3)
                End If

            Case 10
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3)
                End If

            Case 11
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3)
                End If

            Case 12
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 9, 3)
                End If

            Case 13
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "," & Mid(sValorBase_Entera, 11, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "," & Mid(sValorBase_Entera, 11, 3)
                End If

            Case 14
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "," & Mid(sValorBase_Entera, 12, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "," & Mid(sValorBase_Entera, 12, 3)
                End If

            Case 15
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 10, 3) & "," & Mid(sValorBase_Entera, 13, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 10, 3) & "," & Mid(sValorBase_Entera, 13, 3)
                End If

            Case 16
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "," & Mid(sValorBase_Entera, 11, 3) & "," & Mid(sValorBase_Entera, 14, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "," & Mid(sValorBase_Entera, 11, 3) & "," & Mid(sValorBase_Entera, 14, 3)
                End If
            Case 17
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "," & Mid(sValorBase_Entera, 12, 3) & "," & Mid(sValorBase_Entera, 15, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "," & Mid(sValorBase_Entera, 12, 3) & "," & Mid(sValorBase_Entera, 15, 3)
                End If

            Case 18
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 10, 3) & "," & Mid(sValorBase_Entera, 13, 3) & "," & Mid(sValorBase_Entera, 16, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 10, 3) & "," & Mid(sValorBase_Entera, 13, 3) & "," & Mid(sValorBase_Entera, 16, 3)
                End If

            Case 19
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "," & Mid(sValorBase_Entera, 11, 3) & "," & Mid(sValorBase_Entera, 14, 3) & "," & Mid(sValorBase_Entera, 17, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "," & Mid(sValorBase_Entera, 11, 3) & "," & Mid(sValorBase_Entera, 14, 3) & "," & Mid(sValorBase_Entera, 17, 3)
                End If

            Case 20
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "," & Mid(sValorBase_Entera, 12, 3) & "," & Mid(sValorBase_Entera, 15, 3) & "," & Mid(sValorBase_Entera, 18, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "," & Mid(sValorBase_Entera, 12, 3) & "," & Mid(sValorBase_Entera, 15, 3) & "," & Mid(sValorBase_Entera, 18, 3)
                End If
            Case 21
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 10, 3) & "," & Mid(sValorBase_Entera, 13, 3) & "," & Mid(sValorBase_Entera, 16, 3) & "," & Mid(sValorBase_Entera, 19, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 10, 3) & "," & Mid(sValorBase_Entera, 13, 3) & "," & Mid(sValorBase_Entera, 16, 3) & "," & Mid(sValorBase_Entera, 19, 3)
                End If
            Case 21
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "," & Mid(sValorBase_Entera, 11, 3) & "," & Mid(sValorBase_Entera, 14, 3) & "," & Mid(sValorBase_Entera, 17, 3) & "," & Mid(sValorBase_Entera, 20, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 1) & "," & Mid(sValorBase_Entera, 2, 3) & "," & Mid(sValorBase_Entera, 5, 3) & "," & Mid(sValorBase_Entera, 8, 3) & "," & Mid(sValorBase_Entera, 11, 3) & "," & Mid(sValorBase_Entera, 14, 3) & "," & Mid(sValorBase_Entera, 17, 3) & "," & Mid(sValorBase_Entera, 20, 3)
                End If
            Case 22
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "," & Mid(sValorBase_Entera, 12, 3) & "," & Mid(sValorBase_Entera, 15, 3) & "," & Mid(sValorBase_Entera, 18, 3) & "," & Mid(sValorBase_Entera, 21, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 2) & "," & Mid(sValorBase_Entera, 3, 3) & "," & Mid(sValorBase_Entera, 6, 3) & "," & Mid(sValorBase_Entera, 9, 3) & "," & Mid(sValorBase_Entera, 12, 3) & "," & Mid(sValorBase_Entera, 15, 3) & "," & Mid(sValorBase_Entera, 18, 3) & "," & Mid(sValorBase_Entera, 21, 3)
                End If
            Case 23
                If sValorBase_Decimal <> "" Then
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 10, 3) & "," & Mid(sValorBase_Entera, 13, 3) & "," & Mid(sValorBase_Entera, 16, 3) & "," & Mid(sValorBase_Entera, 19, 3) & "," & Mid(sValorBase_Entera, 22, 3) & "." & Mid(sValorBase_Decimal, 1, 2)
                Else
                    sValorReal = Mid(sValorBase_Entera, 1, 3) & "," & Mid(sValorBase_Entera, 4, 3) & "," & Mid(sValorBase_Entera, 7, 3) & "," & Mid(sValorBase_Entera, 10, 3) & "," & Mid(sValorBase_Entera, 13, 3) & "," & Mid(sValorBase_Entera, 16, 3) & "," & Mid(sValorBase_Entera, 19, 3) & "," & Mid(sValorBase_Entera, 22, 3)
                End If

        End Select
        ConvertirTexto_Moneda = sValorReal
    End Function
    Structure EstadosMensajes

        Public Const Guardado As Integer = 0
        Public Const Actualizado As Integer = 1
        Public Const Eliminado As Integer = 2
        Public Const Imprimir As Integer = 3

        Public Const SinConsultaBD As Integer = 9
        Public Const ErrorGenerado As Integer = 10
    End Structure
End Module

Module ModTablas
    Public sCod_Aplicacion As String = ConfigurationManager.ConnectionStrings("SERINGTEGAPLICACION").ConnectionString

    Structure _90_T00REGISTROPROCESOS
        Public Const NombreTabla As String = "_90_T00REGISTROPROCESOS"
        Public Const CodigoModulo As String = "_90_T00"
        Public Const CampoLlave_90_T00Consecutivo As String = "_90_T00Consecutivo"
        Public Const Campo_90_T00CodModulo As String = "_90_T00CodModulo"
        Public Const Campo_90_T00Proceso As String = "_90_T00Proceso"
        Public Const Campo_90_T00ConsultaEjecutada As String = "_90_T00ConsultaEjecutada"
        Public Const Campo_90_T00Fecha As String = "_90_T00Fecha"
        Public Const Campo_90_T00Hora As String = "_90_T00Hora"
        Public Const Campo_90_T00Usuario As String = "_90_T00Usuario"
        Public Const Campo_90_T00FechaSincronizacion As String = "_90_T00FechaSincronizacion"

        Public Const CamposTabla As String = "_90_T00Consecutivo,_90_T00CodModulo,_90_T00Proceso,_90_T00ConsultaEjecutada,_90_T00Fecha,_90_T00Hora,_90_T00Usuario,_90_T00FechaSincronizacion"
        Public Const CamposActualizar As String = "_90_T00CodModulo,_90_T00Proceso,_90_T00ConsultaEjecutada,_90_T00Fecha,_90_T00Hora,_90_T00Usuario,_90_T00FechaSincronizacion"
    End Structure

    Structure _90_T00COIDIGOVERIFICACION
        Public Const NombreTabla As String = "_90_T00CODIGOVERIFICACION"
        Public Const CodigoModulo As String = "_90_T00"
        Public Const CampoLlave_90_T00Codigo As String = "_90_T00Codigo"
        Public Const CampoLlave_90_T00CodAplicacion As String = "_90_T00CodAplicacion"
        Public Const Campo_90_T00Fecha As String = "_90_T00Fecha"
        Public Const Campo_90_T00Activo As String = "_90_T00Activo"

        Public Const CamposTabla As String = "_90_T00Codigo,_90_T00CodAplicacion,_90_T00Fecha,_90_T00Activo"
        Public Const CamposActualizar As String = "_90_T00Fecha,_90_T00Activo"
    End Structure


    Structure _90_T01APLICACIONES
        Public Const NombreTabla As String = "_90_T01APLICACIONES"
        Public Const CodigoModulo As String = "_90_T01"
        Public Const CampoLlave_90_T01Codigo As String = "_90_T01Codigo"
        Public Const Campo_90_T01Nombre As String = "_90_T01Nombre"
        Public Const Campo_90_T01Descripcion As String = "_90_T01Descripcion"
        Public Const Campo_90_T01Activo As String = "_90_T01Activo"
        Public Const Campo_90_T01MenuIzq As String = "_90_T01MenuIzq"
        Public Const Campo_90_T01MenuSup As String = "_90_T01MenuSup"
        Public Const Campo_90_T01Head As String = "_90_T01Head"

        Public Const CamposTabla As String = "_90_T01Codigo,_90_T01Nombre,_90_T01Descripcion,_90_T01Activo,_90_T01MenuIzq,_90_T01MenuSup,_90_T01Head"
        Public Const CamposActualizar As String = "_90_T01Nombre,_90_T01Descripcion,_90_T01Activo,_90_T01MenuIzq,_90_T01MenuSup,_90_T01Head"
    End Structure

    Structure _90_T02MODULOSXAPLICACION
        Public Const NombreTabla As String = "_90_T02MODULOSXAPLICACION"
        Public Const CodigoModulo As String = "_90_T02"
        Public Const CampoLlave_90_T02CodApliacion As String = "_90_T02CodApliacion"
        Public Const CampoLlave_90_T02Codigo As String = "_90_T02Codigo"
        Public Const Campo_90_T02Nombre As String = "_90_T02Nombre"
        Public Const Campo_90_T02Activo As String = "_90_T02Activo"
        Public Const Campo_90_T02Descripcion As String = "_90_T02Descripcion"

        Public Const CamposTabla As String = "_90_T02CodApliacion,_90_T02Codigo,_90_T02Nombre,_90_T02Activo,_90_T02Descripcion"
        Public Const CamposActualizar As String = "_90_T02Nombre,_90_T02Activo,_90_T02Descripcion"
    End Structure

    Structure _90_T03PERFILES
        Public Const NombreTabla As String = "_90_T03PERFILES"
        Public Const CodigoModulo As String = "_90_T03"
        Public Const CampoLlave_90_T03Codigo As String = "_90_T03Codigo"
        Public Const Campo_90_T03Nombre As String = "_90_T03Nombre"
        Public Const Campo_90_T03Activo As String = "_90_T03Activo"
        Public Const Campo_90_T03Descripcion As String = "_90_T03Descripcion"
        Public Const CampoLlave_90_T03CodAplicacion As String = "_90_T03CodAplicacion"

        Public Const CamposTabla As String = "_90_T03Codigo,_90_T03Nombre,_90_T03Activo,_90_T03Descripcion,_90_T03CodAplicacion"
        Public Const CamposActualizar As String = "_90_T03Nombre,_90_T03Activo,_90_T03Descripcion"
    End Structure

    Structure _90_T04PERMISOSXPERFIL
        Public Const NombreTabla As String = "_90_T04PERMISOSXPERFIL"
        Public Const CodigoModulo As String = "_90_T04"
        Public Const CampoLlave_90_T04CodAplicacion As String = "_90_T04CodAplicacion"
        Public Const CampoLlave_90_T04CodModulo As String = "_90_T04CodModulo"
        Public Const CampoLlave_90_T04CodPerfil As String = "_90_T04CodPerfil"
        Public Const Campo_90_T04PerGuardar As String = "_90_T04PerGuardar"
        Public Const Campo_90_T04PerConsulta As String = "_90_T04PerConsulta"
        Public Const Campo_90_T04PerActualizar As String = "_90_T04PerActualizar"
        Public Const Campo_90_T04PerEliminar As String = "_90_T04PerEliminar"
        Public Const Campo_90_T04PerImprimir As String = "_90_T04PerImprimir"
        Public Const Campo_90_T04PerExportar As String = "_90_T04PerExportar"
        Public Const Campo_90_T04PerEspecial1 As String = "_90_T04PerEspecial1"
        Public Const Campo_90_T04PerEspecial2 As String = "_90_T04PerEspecial2"

        Public Const CamposTabla As String = "_90_T04CodAplicacion,_90_T04CodModulo,_90_T04CodPerfil,_90_T04PerGuardar,_90_T04PerConsulta,_90_T04PerActualizar,_90_T04PerEliminar,_90_T04PerImprimir,_90_T04PerExportar,_90_T04PerEspecial1,_90_T04PerEspecial2"
        Public Const CamposActualizar As String = "_90_T04PerGuardar,_90_T04PerConsulta,_90_T04PerActualizar,_90_T04PerEliminar,_90_T04PerImprimir,_90_T04PerExportar,_90_T04PerEspecial1,_90_T04PerEspecial2"
    End Structure

    Structure _90_T05TERCEROS
        Public Const NombreTabla As String = "_90_T05TERCEROS"
        Public Const CodigoModulo As String = "_90_T05"
        Public Const CampoLlave_90_T05TipoDocumento As String = "_90_T05TipoDocumento"
        Public Const CampoLlave_90_T05Documento As String = "_90_T05Documento"
        Public Const Campo_90_T05Nombres As String = "_90_T05Nombres"
        Public Const Campo_90_T05Apellidos As String = "_90_T05Apellidos"
        Public Const Campo_90_T05RazonSocial As String = "_90_T05RazonSocial"
        Public Const Campo_90_T05Direccion As String = "_90_T05Direccion"
        Public Const Campo_90_T05TelefonoFijo As String = "_90_T05TelefonoFijo"
        Public Const Campo_90_T05TelefonoCelular As String = "_90_T05TelefonoCelular"
        Public Const Campo_90_T05Activo As String = "_90_T05Activo"

        Public Const CamposTabla As String = "_90_T05TipoDocumento,_90_T05Documento,_90_T05Nombres,_90_T05Apellidos,_90_T05RazonSocial,_90_T05Direccion,_90_T05TelefonoFijo,_90_T05TelefonoCelular,_90_T05Activo"
        Public Const CamposActualizar As String = "_90_T05Nombres,_90_T05Apellidos,_90_T05RazonSocial,_90_T05Direccion,_90_T05TelefonoFijo,_90_T05TelefonoCelular,_90_T05Activo"
    End Structure

    Structure _90_T06USUARIOS
        Public Const NombreTabla As String = "_90_T06USUARIOS"
        Public Const CodigoModulo As String = "_90_T06"
        Public Const CampoLlave_90_T06NombreUsuario As String = "_90_T06NombreUsuario"
        Public Const CampoLlave_90_T06TipoDocumento As String = "_90_T06TipoDocumento"
        Public Const CampoLlave_90_T06Documento As String = "_90_T06Documento"
        Public Const Campo_90_T06Clave As String = "_90_T06Clave"
        Public Const Campo_90_T06Activo As String = "_90_T06Activo"
        Public Const Campo_90_T06Email As String = "_90_T06Email"
        Public Const CampoLlave_90_T06CodAplicacion As String = "_90_T06CodAplicacion"
        Public Const Campo_90_T06CodPerfil As String = "_90_T06CodPerfil"
        Public Const Campo_90_T06Firma As String = "_90_T06Firma"

        Public Const CamposTabla As String = "_90_T06NombreUsuario,_90_T06TipoDocumento,_90_T06Documento,_90_T06Clave,_90_T06Activo,_90_T06Email,_90_T06CodAplicacion,_90_T06CodPerfil,_90_T06Firma"
        Public Const CamposActualizar As String = "_90_T06NombreUsuario,_90_T06Clave,_90_T06Activo,_90_T06Email,_90_T06CodPerfil"
    End Structure

    Structure _90_T06_1USUARIOSEMPRESAS
        Public Const NombreTabla As String = "_90_T06_1USUARIOSEMPRESAS"
        Public Const CodigoModulo As String = "_90_T06_1"
        Public Const CampoLlave_90_T06_1ConsecutivoCliente As String = "_90_T06_1ConsecutivoCliente"
        Public Const CampoLlave_90_T06_1Usuario As String = "_90_T06_1Usuario"
        Public Const Campo_90_T06_1Clave As String = "_90_T06_1Clave"
        Public Const CampoLlave_90_T06_1TipoServidor As String = "_90_T06_1TipoServidor"
        Public Const Campo_90_T06_1Activo As String = "_90_T06_1Activo"
        Public Const Campo_90_T06_1TipoHistoriaDescarga As String = "_90_T06_1TipoHistoriaDescarga"
        Public Const Campo_90_T06_1SincronizadoWeb As String = "_90_T06_1SincronizadoWeb"

        Public Const CamposTabla As String = "_90_T06_1ConsecutivoCliente,_90_T06_1Usuario,_90_T06_1Clave,_90_T06_1TipoServidor,_90_T06_1Activo,_90_T06_1TipoHistoriaDescarga,_90_T06_1SincronizadoWeb"
        Public Const CamposActualizar As String = "_90_T06_1Usuario,_90_T06_1Clave,_90_T06_1Activo,_90_T06_1TipoHistoriaDescarga"
    End Structure

    Structure _90_T06_2USUARIOSSUCURSAL
        Public Const NombreTabla As String = "_90_T06_2USUARIOSSUCURSAL"
        Public Const CodigoModulo As String = "_90_T06_2"
        Public Const CampoLlave_90_T06_2TipoServidor As String = "_90_T06_2TipoServidor"
        Public Const CampoLlave_90_T06_2Usuario As String = "_90_T06_2Usuario"
        Public Const Campo_90_T06_2Fecha As String = "_90_T06_2Fecha"
        Public Const Campo_90_T06_2Hora As String = "_90_T06_2Hora"
        Public Const Campo_90_T06_2Minuto As String = "_90_T06_2Minuto"

        Public Const CamposTabla As String = "_90_T06_2TipoServidor,_90_T06_2Usuario,_90_T06_2Fecha,_90_T06_2Hora,_90_T06_2Minuto"
        Public Const CamposActualizar As String = "_90_T06_2Fecha,_90_T06_2Hora,_90_T06_2Minuto"
    End Structure

    Structure _90_T07USUARIOXPERFIL
        Public Const NombreTabla As String = "_90_T07USUARIOXPERFIL"
        Public Const CodigoModulo As String = "_90_T07"
        Public Const CampoLlave_90_T07CodAplicacion As String = "_90_T07CodAplicacion"
        Public Const CampoLlave_90_T07CodModulo As String = "_90_T07CodModulo"
        Public Const CampoLlave_90_T07CodPerfil As String = "_90_T07CodPerfil"
        Public Const CampoLlave_90_T07NombreUsuario As String = "_90_T07NombreUsuario"

        Public Const CamposTabla As String = "_90_T07CodAplicacion,_90_T07CodModulo,_90_T07CodPerfil,_90_T07NombreUsuario"
        Public Const CamposActualizar As String = ""
    End Structure


    Structure _90_T08TIPODOCIDENTIDAD
        Public Const NombreTabla As String = "_90_T08TIPODOCIDENTIDAD"
        Public Const CodigoModulo As String = "_90_T08"
        Public Const CampoLlave_90_T08Codigo As String = "_90_T08Codigo"
        Public Const Campo_90_T08Nombre As String = "_90_T08Nombre"
        Public Const Campo_90_T08Activo As String = "_90_T08Activo"
        Public Const Campo_90_T08TipoDocFiscal As String = "_90_T08TipoDocFiscal"

        Public Const CamposTabla As String = "_90_T08Codigo,_90_T08Nombre,_90_T08Activo,_90_T08TipoDocFiscal"
        Public Const CamposActualizar As String = "_90_T08Nombre,_90_T08Activo"
    End Structure

    Structure _90_T07PAIS
        Public Const NombreTabla As String = "_90_T07PAIS"
        Public Const CodigoModulo As String = "_90_T07"
        Public Const CampoLlave_07_T07Codigo As String = "_90_T07Codigo"
        Public Const Campo_90_T07Nombre As String = "_90_T07Nombre"
        Public Const Campo_90_T07activo As String = "_90_T07activo"

        Public Const Campo_90_T07Codigoalfa2 As String = "_90_T07Codigoalfa2"
        Public Const Campo_90_T07Codigoalfa3 As String = "_90_T07Codigoalfa3"
        Public Const Campo_90_T07CodigoFactElectronica As String = "_90_T07CodigoFactElectronica"

        Public Const CamposTabla As String = "_90_T07Codigo,_90_T07Nombre,_90_T07activo,_90_T07Codigoalfa2,_90_T07Codigoalfa3,_90_T07CodigoFactElectronica"
        Public Const CamposActualizar As String = "_90_T07Nombre,_90_T07activo"
    End Structure

    Structure _90_T09DEPARTAMENTOS
        Public Const NombreTabla As String = "_90_T09DEPARTAMENTOS"
        Public Const CodigoModulo As String = "_90_T09"
        Public Const CampoLlave_90_T09Codigo As String = "_90_T09Codigo"
        Public Const Campo_90_T09Nombre As String = "_90_T09Nombre"
        Public Const Campo_90_T09activo As String = "_90_T09activo"
        Public Const Campo_90_T09Pais As String = "_90_T09CodigoPais"

        Public Const CamposTabla As String = "_90_T09Codigo,_90_T09Nombre,_90_T09activo,_90_T09CodigoPais"
        Public Const CamposActualizar As String = "_90_T09Nombre,_90_T09activo"
    End Structure

    Structure _90_T10MUNICIPIOS
        Public Const NombreTabla As String = "_90_T10MUNICIPIOS"
        Public Const CodigoModulo As String = "_90_T10"
        Public Const CampoLlave_90_T10CodDepto As String = "_90_T10CodDepto"
        Public Const CampoLlave_90_T10Codigo As String = "_90_T10Codigo"
        Public Const Campo_90_T10Nombre As String = "_90_T10Nombre"
        Public Const Campo_90_T10Activo As String = "_90_T10Activo"
        Public Const Campo_90_T10Pais As String = "_90_T10CodigoPais"

        Public Const CamposTabla As String = "_90_T10CodDepto,_90_T10Codigo,_90_T10Nombre,_90_T10Activo,_90_T10CodigoPais"
        Public Const CamposActualizar As String = "_90_T10Nombre,_90_T10Activo,_90_T10Pais"
    End Structure

    Structure _90_T11BARRIOS
        Public Const NombreTabla As String = "_90_T11BARRIOS"
        Public Const CodigoModulo As String = "_90_T11"
        Public Const CampoLlave_90_T11CodComuna As String = "_90_T11CodComuna"
        Public Const CampoLlave_90_T11Consecutivo As String = "_90_T11Consecutivo"
        Public Const Campo_90_T11Nombre As String = "_90_T11Nombre"
        Public Const Campo_90_T11Activo As String = "_90_T11Activo"
        Public Const Campo_90_T11CodCentroPoblado As String = "_90_T11CodCentroPoblado"

        Public Const CamposTabla As String = "_90_T11CodComuna,_90_T11Consecutivo,_90_T11Nombre,_90_T11Activo,_90_T11CodCentroPoblado"
        Public Const CamposActualizar As String = "_90_T11Nombre,_90_T11Activo,_90_T11CodCentroPoblado"
    End Structure

    Structure _90_T12COMUNA
        Public Const NombreTabla As String = "_90_T12COMUNA"
        Public Const CodigoModulo As String = "_90_T12"
        Public Const CampoLlave_90_T12Codigo As String = "_90_T12Codigo"
        Public Const Campo_90_T12Nombre As String = "_90_T12Nombre"
        Public Const Campo_90_T12Activa As String = "_90_T12Activa"

        Public Const CamposTabla As String = "_90_T12Codigo,_90_T12Nombre,_90_T12Activa"
        Public Const CamposActualizar As String = "_90_T12Nombre,_90_T12Activa"
    End Structure

    Structure _90_T13REGIMENAFILIACION
        Public Const NombreTabla As String = "_90_T13REGIMENAFILIACION"
        Public Const CodigoModulo As String = "_90_T13"
        Public Const CampoLlave_90_T13Codigo As String = "_90_T13Codigo"
        Public Const Campo_90_T13Nombre As String = "_90_T13Nombre"
        Public Const Campo_90_T13Activo As String = "_90_T13Activo"

        Public Const CamposTabla As String = "_90_T13Codigo,_90_T13Nombre,_90_T13Activo"
        Public Const CamposActualizar As String = "_90_T13Nombre,_90_T13Activo"
    End Structure

    Structure _90_T14EPS
        Public Const NombreTabla As String = "_90_T14EPS"
        Public Const CodigoModulo As String = "_90_T14"
        Public Const CampoLlave_90_T14CodRegimenAfiliacion As String = "_90_T14CodRegimenAfiliacion"
        Public Const CampoLlave_90_T14Consecutivo As String = "_90_T14Consecutivo"
        Public Const Campo_90_T14Nombre As String = "_90_T14Nombre"
        Public Const Campo_90_T14Activo As String = "_90_T14Activo"

        Public Const CamposTabla As String = "_90_T14CodRegimenAfiliacion,_90_T14Consecutivo,_90_T14Nombre,_90_T14Activo"
        Public Const CamposActualizar As String = "_90_T14Nombre,_90_T14Activo"
    End Structure

    Structure _90_T15GRUPOSETNICOS
        Public Const NombreTabla As String = "_90_T15GRUPOSETNICOS"
        Public Const CodigoModulo As String = "_90_T15"
        Public Const CampoLlave_90_T15Codigo As String = "_90_T15Codigo"
        Public Const Campo_90_T15Nombre As String = "_90_T15Nombre"
        Public Const Campo_90_T15Activo As String = "_90_T15Activo"

        Public Const CamposTabla As String = "_90_T15Codigo,_90_T15Nombre,_90_T15Activo"
        Public Const CamposActualizar As String = "_90_T15Nombre,_90_T15Activo"
    End Structure

    Structure _90_T16METODOSPLANIFICACION
        Public Const NombreTabla As String = "_90_T16METODOSPLANIFICACION"
        Public Const CodigoModulo As String = "_90_T16"
        Public Const CampoLlave_90_T16Codigo As String = "_90_T16Codigo"
        Public Const Campo_90_T16Nombre As String = "_90_T16Nombre"
        Public Const Campo_90_T16Activo As String = "_90_T16Activo"

        Public Const CamposTabla As String = "_90_T16Codigo,_90_T16Nombre,_90_T16Activo"
        Public Const CamposActualizar As String = "_90_T16Nombre,_90_T16Activo"
    End Structure

    Structure _90_T17CENTROSPOBLADOS
        Public Const NombreTabla As String = "_90_T17CENTROSPOBLADOS"
        Public Const CodigoModulo As String = "_90_T17"
        Public Const CampoLlave_90_T17Codigo As String = "_90_T17Codigo"
        Public Const Campo_90_T17CodDepto As String = "_90_T17CodDepto"
        Public Const Campo_90_T17CodMunicipio As String = "_90_T17CodMunicipio"
        Public Const Campo_90_T17Nombre As String = "_90_T17Nombre"
        Public Const Campo_90_T17CodTipoCentroPoblado As String = "_90_T17CodTipoCentroPoblado"
        Public Const Campo_90_T17Activo As String = "_90_T17Activo"

        Public Const CamposTabla As String = "_90_T17Codigo,_90_T17CodDepto,_90_T17CodMunicipio,_90_T17Nombre,_90_T17CodTipoCentroPoblado,_90_T17Activo"
        Public Const CamposActualizar As String = "_90_T17CodDepto,_90_T17CodMunicipio,_90_T17Nombre,_90_T17CodTipoCentroPoblado,_90_T17Activo"
    End Structure

    Structure _90_T18TIPOCENTOPOBLADO
        Public Const NombreTabla As String = "_90_T18TIPOCENTOPOBLADO"
        Public Const CodigoModulo As String = "_90_T18"
        Public Const CampoLlave_90_T18Codigo As String = "_90_T18Codigo"
        Public Const Campo_90_T18Nombre As String = "_90_T18Nombre"
        Public Const Campo_90_T18Activo As String = "_90_T18Activo"

        Public Const CamposTabla As String = "_90_T18Codigo,_90_T18Nombre,_90_T18Activo"
        Public Const CamposActualizar As String = "_90_T18Nombre,_90_T18Activo"
    End Structure

    Structure _90_T19TIPOSESTRATOS
        Public Const NombreTabla As String = "_90_T19TIPOSESTRATOS"
        Public Const CodigoModulo As String = "_90_T19"
        Public Const CampoLlave_90_T19Codigo As String = "_90_T19Codigo"
        Public Const Campo_90_T19Nombre As String = "_90_T19Nombre"
        Public Const Campo_90_T19Activo As String = "_90_T19Activo"

        Public Const CamposTabla As String = "_90_T19Codigo,_90_T19Nombre,_90_T19Activo"
        Public Const CamposActualizar As String = "_90_T19Nombre,_90_T19Activo"
    End Structure

    Structure _90_T20DEPENDENCIAS
        Public Const NombreTabla As String = "_90_T20DEPENDENCIAS"
        Public Const CodigoModulo As String = "_90_T20"
        Public Const CampoLlave_90_T20Codigo As String = "_90_T20Codigo"
        Public Const Campo_90_T20Nombre As String = "_90_T20Nombre"
        Public Const Campo_90_T20TipoDependencia As String = "_90_T20TipoDependencia"
        Public Const Campo_90_T20Activa As String = "_90_T20Activa"

        Public Const CamposTabla As String = "_90_T20Codigo,_90_T20Nombre,_90_T20TipoDependencia,_90_T20Activa"
        Public Const CamposActualizar As String = "_90_T20Nombre,_90_T20TipoDependencia,_90_T20Activa"
    End Structure

    Structure _90_T21TIPOSDEPENDENCIA
        Public Const NombreTabla As String = "_90_T21TIPOSDEPENDENCIA"
        Public Const CodigoModulo As String = "_90_T21"
        Public Const CampoLlave_90_T21Codigo As String = "_90_T21Codigo"
        Public Const Campo_90_T21Nombre As String = "_90_T21Nombre"
        Public Const Campo_90_T21Observacion As String = "_90_T21Observacion"

        Public Const CamposTabla As String = "_90_T21Codigo,_90_T21Nombre,_90_T21Observacion"
        Public Const CamposActualizar As String = "_90_T21Nombre,_90_T21Observacion"
    End Structure

    Structure _90_T22PLATAFORMA
        Public Const NombreTabla As String = "_90_T22PLATAFORMA"
        Public Const CodigoModulo As String = "_90_T22"
        Public Const CampoLlave_90_T22Codigo As String = "_90_T22Codigo"
        Public Const Campo_90_T22Mensaje As String = "_90_T22Mensaje"
        Public Const Campo_90_T22FechaIni As String = "_90_T22FechaIni"
        Public Const Campo_90_T22FechaFin As String = "_90_T22FechaFin"
        Public Const Campo_90_T22CodAplicacion As String = "_90_T22CodAplicacion"

        Public Const CamposTabla As String = "_90_T22Codigo,_90_T22Mensaje,_90_T22FechaIni,_90_T22FechaFin,_90_T22CodAplicacion"
        Public Const CamposActualizar As String = "_90_T22Mensaje,_90_T22FechaIni,_90_T22FechaFin,_90_T22CodAplicacion"
    End Structure

    Structure _90_T23ESTADOCIVIL
        Public Const NombreTabla As String = "_90_T23ESTADOCIVIL"
        Public Const CodigoModulo As String = "_90_T23"
        Public Const CampoLlave_90_T23Codigo As String = "_90_T23Codigo"
        Public Const Campo_90_T23Nombre As String = "_90_T23Nombre"
        Public Const Campo_90_T23Activo As String = "_90_T23Activo"

        Public Const CamposTabla As String = "_90_T23Codigo,_90_T23Nombre,_90_T23Activo"
        Public Const CamposActualizar As String = "_90_T23Nombre,_90_T23Activo"
    End Structure

    Structure _90_T24ESCOLARIDAD
        Public Const NombreTabla As String = "_90_T24ESCOLARIDAD"
        Public Const CodigoModulo As String = "_90_T24"
        Public Const CampoLlave_90_T24Codigo As String = "_90_T24Codigo"
        Public Const Campo_90_T24Nombre As String = "_90_T24Nombre"
        Public Const Campo_90_T24Activo As String = "_90_T24Activo"

        Public Const CamposTabla As String = "_90_T24Codigo,_90_T24Nombre,_90_T24Activo"
        Public Const CamposActualizar As String = "_90_T24Nombre,_90_T24Activo"
    End Structure

    Structure _90_T25TIPOEDAD
        Public Const NombreTabla As String = "_90_T25TIPOEDAD"
        Public Const CodigoModulo As String = "_90_T25"
        Public Const CampoLlave_90_T25Codigo As String = "_90_T25Codigo"
        Public Const Campo_90_T25Nombre As String = "_90_T25Nombre"
        Public Const Campo_90_T25Activo As String = "_90_T25Activo"

        Public Const CamposTabla As String = "_90_T25Codigo,_90_T25Nombre,_90_T25Activo"
        Public Const CamposActualizar As String = "_90_T25Nombre,_90_T25Activo"
    End Structure

    Structure _90_T26BDCLIENTES
        Public Const NombreTabla As String = "_90_T26BDCLIENTES"
        Public Const CodigoModulo As String = "_90_T26"
        Public Const CampoLlave_90_T26Codigo As String = "_90_T26Codigo"
        Public Const Campo_90_T26NombreCorto As String = "_90_T26NombreCorto"
        Public Const Campo_90_T26NombreLargo As String = "_90_T26NombreLargo"
        Public Const Campo_90_T26Activa As String = "_90_T26Activa"
        Public Const Campo_90_T26TipoDoCliente As String = "_90_T26TipoDoCliente"
        Public Const Campo_90_T26NumDocCliente As String = "_90_T26NumDocCliente"

        Public Const CamposTabla As String = "_90_T26Codigo,_90_T26NombreCorto,_90_T26NombreLargo,_90_T26Activa,_90_T26TipoDoCliente,_90_T26NumDocCliente"
        Public Const CamposActualizar As String = "_90_T26NombreCorto,_90_T26NombreLargo,_90_T26Activa,_90_T26TipoDoCliente,_90_T26NumDocCliente"
    End Structure

    Structure _90_T27BACKUPWEB
        Public Const NombreTabla As String = "_90_T27BACKUPWEB"
        Public Const CodigoModulo As String = "_90_T27"
        Public Const CampoLlave_90_T27Consecutivo As String = "_90_T27Consecutivo"
        Public Const CampoLlave_90_T27Agno As String = "_90_T27Agno"
        Public Const CampoLlave_90_T27Mes As String = "_90_T27Mes"
        Public Const CampoLlave_90_T27Dia As String = "_90_T27Dia"
        Public Const Campo_90_T27Hora As String = "_90_T27Hora"
        Public Const CampoLlave_90_T27CodigoProceso As String = "_90_T27CodigoProceso"
        Public Const Campo_90_T27Pedientes As String = "_90_T27Pedientes"
        Public Const Campo_90_T27Sincronizados As String = "_90_T27Sincronizados"
        Public Const Campo_90_T27Fecha As String = "_90_T27Fecha"


        Public Const CamposTabla As String = "_90_T27Consecutivo,_90_T27Agno,_90_T27Mes,_90_T27Dia,_90_T27Hora,_90_T27CodigoProceso,_90_T27Pedientes,_90_T27Sincronizados,_90_T27Fecha"
        Public Const CamposActualizar As String = "_90_T27Hora"
    End Structure


    Structure _90_T29TIPOSERVIDOR
        Public Const NombreTabla As String = "_90_T29TIPOSERVIDOR"
        Public Const CodigoModulo As String = "_90_T29"
        Public Const CampoLlave_90_T29Codigo As String = "_90_T29Codigo"
        Public Const Campo_90_T29Nombre As String = "_90_T29Nombre"
        Public Const Campo_90_T29Actual As String = "_90_T29Actual"
        Public Const Campo_90_T29CodDepto As String = "_90_T29CodDepto"
        Public Const Campo_90_T29CodMunicipio As String = "_90_T29CodMunicipio"
        Public Const CampoLlave_90_T29CodigoEntidad As String = "_90_T29CodEntidad"
        Public Const Campo_90_T29CodTemporal As String = "_90_T29CodTemporal"
        Public Const Campo_90_T29FactElec_CodSucursal As String = "_90_T29FactElec_CodSucursal"

        Public Const CamposTabla As String = "_90_T29Codigo,_90_T29Nombre,_90_T29Actual,_90_T29CodDepto,_90_T29CodMunicipio,_90_T29CodEntidad,_90_T29CodTemporal,_90_T29FactElec_CodSucursal"
        Public Const CamposActualizar As String = "_90_T29Nombre,_90_T29Actual,_90_T29CodDepto,_90_T29CodMunicipio"
    End Structure

    Structure _90_T31AGNOS
        Public Const NombreTabla As String = "_90_T31AGNOS"
        Public Const CodigoModulo As String = "_90_T31"
        Public Const CampoLlave_90_T31Fecha As String = "_90_T31Fecha"
        Public Const Campo_90_T31FechaNumero As String = "_90_T31FechaNumero"
        Public Const Campo_90_T31Dia As String = "_90_T31Dia"
        Public Const Campo_90_T31Mes As String = "_90_T31Mes"
        Public Const Campo_90_T31Agno As String = "_90_T31Agno"
        Public Const Campo_90_T31NombreLargo As String = "_90_T31NombreLargo"
        Public Const Campo_90_T31NombreDia As String = "_90_T31NombreDia"
        Public Const Campo_90_T31NombreMEs As String = "_90_T31NombreMEs"
        Public Const Campo_90_T31DiaFestivo As String = "_90_T31DiaFestivo"
        Public Const Campo_90_T31Activo As String = "_90_T31Activo"

        Public Const CamposTabla As String = "_90_T31Fecha,_90_T31FechaNumero,_90_T31Dia,_90_T31Mes,_90_T31Agno,_90_T31NombreLargo,_90_T31NombreDia,_90_T31NombreMEs,_90_T31DiaFestivo,_90_T31Activo"
        Public Const CamposActualizar As String = "_90_T31FechaNumero,_90_T31Dia,_90_T31Mes,_90_T31Agno,_90_T31NombreLargo,_90_T31NombreDia,_90_T31NombreMEs,_90_T31DiaFestivo,_90_T31Activo"
    End Structure

    Structure _90_T32EPS
        Public Const NombreTabla As String = "_90_T32EPS"
        Public Const CodigoModulo As String = "_90_T32"
        Public Const CampoLlave_04_T32EPS As String = "_04_T32EPS"
        Public Const Campo_04_T32Nombre As String = "_04_T32Nombre"
        Public Const Campo_04_T32Activo As String = "_04_T32Activo"

        Public Const CamposTabla As String = "_04_T32EPS,_04_T32Nombre,_04_T32Activo"
        Public Const CamposActualizar As String = "_04_T32Nombre,_04_T32Activo"
    End Structure

    Structure _90_T33ARL
        Public Const NombreTabla As String = "_90_T33ARL"
        Public Const CodigoModulo As String = "_90_T33"
        Public Const CampoLlave_90_T33Codigo As String = "_90_T33Codigo"
        Public Const Campo_90_T33Nombre As String = "_90_T33Nombre"
        Public Const Campo_90_T33Activo As String = "_90_T33Activo"

        Public Const CamposTabla As String = "_90_T33Codigo,_90_T33Nombre,_90_T33Activo"
        Public Const CamposActualizar As String = "_90_T33Nombre,_90_T33Activo"
    End Structure

    Structure _90_T34AFP
        Public Const NombreTabla As String = "_90_T34AFP"
        Public Const CodigoModulo As String = "_90_T34"
        Public Const CampoLlave_90_T34Codigo As String = "_90_T34Codigo"
        Public Const Campo_90_T34Nombre As String = "_90_T34Nombre"
        Public Const Campo_90_T34Activo As String = "_90_T34Activo"

        Public Const CamposTabla As String = "_90_T34Codigo,_90_T34Nombre,_90_T34Activo"
        Public Const CamposActualizar As String = "_90_T34Nombre,_90_T34Activo"
    End Structure

    Structure _90_T35PARENTESCOFAMILIAR
        Public Const NombreTabla As String = "_90_T35PARENTESCOFAMILIAR"
        Public Const CodigoModulo As String = "_90_T35"
        Public Const CampoLlave_90_T35Codigo As String = "_90_T35Codigo"
        Public Const Campo_90_T35Nombre As String = "_90_T35Nombre"
        Public Const Campo_90_T35Activo As String = "_90_T35Activo"

        Public Const CamposTabla As String = "_90_T35Codigo,_90_T35Nombre,_90_T35Activo"
        Public Const CamposActualizar As String = "_90_T35Nombre,_90_T35Activo"
    End Structure

    Structure _90_T36TIPOSANGRE
        Public Const NombreTabla As String = "_90_T36TIPOSANGRE"
        Public Const CodigoModulo As String = "_90_T36"
        Public Const CampoLlave_90_T36Codigo As String = "_90_T36Codigo"
        Public Const Campo_90_T36Nombre As String = "_90_T36Nombre"
        Public Const Campo_90_T36Grupo As String = "_90_T36Grupo"
        Public Const Campo_90_T36Hemoclasificacon As String = "_90_T36Hemoclasificacon"
        Public Const Campo_90_T36Activo As String = "_90_T36Activo"

        Public Const CamposTabla As String = "_90_T36Codigo,_90_T36Nombre,_90_T36Grupo,_90_T36Hemoclasificacon,_90_T36Activo"
        Public Const CamposActualizar As String = "_90_T36Nombre,_90_T36Grupo,_90_T36Hemoclasificacon,_90_T36Activo"
    End Structure


    Structure _07_Cont_T11_1EMPRESATRABAJAR
        Public Const NombreTabla As String = "_07_Cont_T11_1EMPRESATRABAJAR"
        Public Const CodigoModulo As String = "_07_Cont_T11_1"
        Public Const CampoLlave_07_Cont_T11_1CodEmpresa As String = "_07_Cont_T11_1CodEmpresa"
        Public Const CampoLlave_07_Cont_T11_1CodSucursal As String = "_07_Cont_T11_1CodSucursal"
        Public Const CampoLlave_07_Cont_T11_1Vigencia As String = "_07_Cont_T11_1Vigencia"
        Public Const Campo_07_Cont_T11_1Observacion As String = "_07_Cont_T11_1Observacion"
        Public Const CampoLlave_07_Cont_T11_1Usuario As String = "_07_Cont_T11_1Usuario"
        Public Const CampoLlave_07_Cont_T11_1Nombre As String = "_07_Cont_T11_1Nombre"
        Public Const CampoLlave_07_Cont_T11_1TipoDocumento As String = "_07_Cont_T11_1TipoDocumento"
        Public Const CampoLlave_07_Cont_T11_1Documento As String = "_07_Cont_T11_1Documento"

        Public Const CamposTabla As String = "_07_Cont_T11_1CodEmpresa,_07_Cont_T11_1CodSucursal,_07_Cont_T11_1Vigencia,_07_Cont_T11_1Observacion,_07_Cont_T11_1Usuario,_07_Cont_T11_1Nombre,_07_Cont_T11_1TipoDocumento,_07_Cont_T11_1Documento"
        Public Const CamposActualizar As String = "_07_Cont_T11_1Observacion"
    End Structure

    Structure _04_T20ENTIDAD
        Public Const NombreTabla As String = "_04_T20ENTIDAD"
        Public Const CodigoModulo As String = "_04_T20"
        Public Const CampoLlave_04_T20Codigo As String = "_04_T20Codigo"
        Public Const Campo_04_T20TipoDoc As String = "_04_T20TipoDoc"
        Public Const Campo_04_T20NumDoc As String = "_04_T20NumDoc"
        Public Const Campo_04_T20RazonSocial As String = "_04_T20RazonSocial"
        Public Const Campo_04_T20Activo As String = "_04_T20Activo"
        Public Const Campo_04_T20CodigoHabilitacion As String = "_04_T20CodigoHabilitacion"
        Public Const Campo_04_T20TipoEntidadPrestadora As String = "_04_T20TipoEntidadPrestadora"
        Public Const Campo_04_T20FechaActWeb As String = "_04_T20FechaActWeb"
        Public Const Campo_04_T20ImagenSuperior As String = "_04_T20ImagenSuperior"
        Public Const Campo_04_T20ImagenInferior As String = "_04_T20ImagenInferior"
        Public Const Campo_04_T20ImagenCentral As String = "_04_T20ImagenCentral"
        Public Const Campo_04_T20ImpresionReceta As String = "_04_T20ImpresionReceta"
        Public Const Campo_04_T20DV As String = "_04_T20DV"
        Public Const Campo_04_T20Telefono As String = "_04_T20Telefono"
        Public Const Campo_04_T20Celular As String = "_04_T20Celular"
        Public Const Campo_04_T20PaginaWeb As String = "_04_T20PaginaWeb"
        Public Const Campo_04_T20Direccion As String = "_04_T20Direccion"
        Public Const Campo_04_T20TipoPuc As String = "_04_T20TipoPuc"
        Public Const Campo_04_T20ReteCrePorcentaj As String = "_04_T20ReteCrePorcentaj"
        Public Const Campo_04_T20ReteCreCuenta As String = "_04_T20ReteCreCuenta"
        Public Const Campo_04_T20ReteFuentePorcentaje As String = "_04_T20ReteFuentePorcentaje"
        Public Const Campo_04_T20ReteFuenteCuenta As String = "_04_T20ReteFuenteCuenta"
        Public Const Campo_04_T20ReteICAPorcentaje As String = "_04_T20ReteICAPorcentaje"
        Public Const Campo_04_T20ReteICACuenta As String = "_04_T20ReteICACuenta"
        Public Const Campo_04_T20ActividadEconomica As String = "_04_T20ActividadEconomica"
        Public Const Campo_04_T20PaisDeptoMunicipio As String = "_04_T20PaisDeptoMunicipio"
        Public Const Campo_04_T20ClaveCostoPromedio As String = "_04_T20ClaveCostoPromedio"
        Public Const Campo_04_T20Moneda As String = "_04_T20Moneda"
        Public Const Campo_04_T20Regimen As String = "_04_T20Regimen"
        Public Const Campo_04_T20Email As String = "_04_T20Email"

        Public Const CamposTabla As String = "_04_T20Codigo,_04_T20TipoDoc,_04_T20NumDoc,_04_T20RazonSocial,_04_T20Activo,_04_T20CodigoHabilitacion,_04_T20TipoEntidadPrestadora,_04_T20FechaActWeb,_04_T20ImagenSuperior,_04_T20ImagenInferior,_04_T20ImagenCentral,_04_T20ImpresionReceta,_04_T20DV,_04_T20Telefono,_04_T20Celular,_04_T20PaginaWeb,_04_T20Direccion,_04_T20TipoPuc,_04_T20ReteCrePorcentaj,_04_T20ReteCreCuenta,_04_T20ReteFuentePorcentaje,_04_T20ReteFuenteCuenta,_04_T20ReteICAPorcentaje,_04_T20ReteICACuenta,_04_T20ActividadEconomica,_04_T20PaisDeptoMunicipio,_04_T20ClaveCostoPromedio,_04_T20Moneda,_04_T20Regimen,_04_T20Email"
        Public Const CamposActualizar As String = "_04_T20TipoDoc,_04_T20NumDoc,_04_T20RazonSocial,_04_T20Activo,_04_T20CodigoHabilitacion,_04_T20TipoEntidadPrestadora,_04_T20FechaActWeb,_04_T20ImagenSuperior,_04_T20ImagenInferior,_04_T20ImagenCentral,_04_T20ImpresionReceta,_04_T20DV,_04_T20Telefono,_04_T20Celular,_04_T20PaginaWeb,_04_T20Direccion,_04_T20TipoPuc,_04_T20ReteCrePorcentaj,_04_T20ReteCreCuenta,_04_T20ReteFuentePorcentaje,_04_T20ReteFuenteCuenta,_04_T20ReteICAPorcentaje,_04_T20ReteICACuenta,_04_T20ActividadEconomica,_04_T20PaisDeptoMunicipio"
    End Structure
    Structure _07_Cont_T12TIPOSISTEMA
        Public Const NombreTabla As String = "_07_Cont_T12TIPOSISTEMA"
        Public Const CodigoModulo As String = "_07_Cont_T12"
        Public Const CampoLlave_07_Cont_T12TipoSistema As String = "_07_Cont_T12TipoSistema"
        Public Const Campo_07_Cont_T12Activo As String = "_07_Cont_T12Activo"

        Public Const CamposTabla As String = "_07_Cont_T12TipoSistema,_07_Cont_T12Activo"
        Public Const CamposActualizar As String = "_07_Cont_T12Activo"
    End Structure




    '***********   COMIENZA TABLAS CHAT ****************************

    Structure _80_T02CONEXIONES_BD
        Public Const NombreTabla As String = "_80_T02CONEXIONES_BD"
        Public Const CodigoModulo As String = "_80_T02"
        Public Const CampoLlave_80_T02Codigo As String = "_80_T02Codigo"
        Public Const Campo_80_T02NombreServidor As String = "_80_T02NombreServidor"
        Public Const Campo_80_T02IP As String = "_80_T02IP"
        Public Const Campo_80_T02SiteDataBaseServer As String = "_80_T02SiteDataBaseServer"
        Public Const CampoLlave_80_T02SiteDataBaseNombre As String = "_80_T02SiteDataBaseNombre"
        Public Const Campo_80_T02Proyecto As String = "_80_T02Proyecto"
        Public Const Campo_80_T02Usuario As String = "_80_T02Usuario"
        Public Const Campo_80_T02Clave As String = "_80_T02Clave"
        Public Const Campo_80_T02Activo As String = "_80_T02Activo"

        Public Const CamposTabla As String = "_80_T02Codigo,_80_T02NombreServidor,_80_T02IP,_80_T02SiteDataBaseServer,_80_T02SiteDataBaseNombre,_80_T02Proyecto,_80_T02Usuario,_80_T02Clave,_80_T02Activo"
        Public Const CamposActualizar As String = "_80_T02NombreServidor,_80_T02IP,_80_T02SiteDataBaseServer,_80_T02Proyecto,_80_T02Usuario,_80_T02Clave,_80_T02Activo"
    End Structure

    Structure _80_T03CODIGO_QR
        Public Const NombreTabla As String = "_80_T03CODIGO_QR"
        Public Const CodigoModulo As String = "_80_T03"
        Public Const Campo_80_T03ConexionDB_Servidor As String = "_80_T03ConexionDB_Servidor"
        Public Const CampoLlave_80_T03CodigoOID As String = "_80_T03CodigoOID"
        Public Const Campo_80_T03IdTemporal As String = "_80_T03IdTemporal"
        Public Const Campo_80_T03ImgenQR As String = "_80_T03ImgenQR"
        Public Const Campo_80_T03Imagen1 As String = "_80_T03Imagen1"
        Public Const Campo_80_T03Imagen2 As String = "_80_T03Imagen2"
        Public Const Campo_80_T03Imagen3 As String = "_80_T03Imagen3"
        Public Const Campo_80_T03Orden As String = "_80_T03Orden"
        Public Const Campo_80_T03Localizacion As String = "_80_T03Localizacion"
        Public Const Campo_80_T03sistema As String = "_80_T03sistema"
        Public Const Campo_80_T03Pipeline As String = "_80_T03Pipeline"
        Public Const Campo_80_T03Piperun As String = "_80_T03Piperun"
        Public Const Campo_80_T03Nombre As String = "_80_T03Nombre"
        Public Const Campo_80_T03NPD As String = "_80_T03NPD"
        Public Const Campo_80_T03TAG As String = "_80_T03TAG"
        Public Const Campo_80_T03Tipo As String = "_80_T03Tipo"
        Public Const Campo_80_T03URLCodigoQR As String = "_80_T03URLCodigoQR"
        Public Const Campo_80_T03ConexionDB_Proyecto As String = "_80_T03ConexionDB_Proyecto"
        Public Const CampoLlave_80_T03ConexionDB_Codigo As String = "_80_T03ConexionDB_Codigo"

        Public Const CamposTabla As String = "_80_T03ConexionDB_Servidor,_80_T03CodigoOID,_80_T03IdTemporal,_80_T03ImgenQR,_80_T03Imagen1,_80_T03Imagen2,_80_T03Imagen3,_80_T03Orden,_80_T03Localizacion,_80_T03sistema,_80_T03Pipeline,_80_T03Piperun,_80_T03Nombre,_80_T03NPD,_80_T03TAG,_80_T03Tipo,_80_T03URLCodigoQR,_80_T03ConexionDB_Proyecto,_80_T03ConexionDB_Codigo"
        Public Const CamposActualizar As String = "_80_T03ConexionDB_Servidor,_80_T03IdTemporal,_80_T03ImgenQR,_80_T03ImagenOID,_80_T03Orden,_80_T03Localizacion,_80_T03sistema,_80_T03Pipeline,_80_T03Piperun,_80_T03Nombre,_80_T03NPD,_80_T03TAG,_80_T03Tipo,_80_T03URLCodigoQR,_80_T03ConexionDB_Proyecto"
    End Structure


    '***********   COMIENZA TABLAS CHAT ****************************

    Structure _01_T01ListadoMaestroEntregablesIng
        Public Const NombreTabla As String = "_01_T01ListadoMaestroEntregablesIng"
        Public Const CodigoModulo As String = "_01_T01"
        Public Const CampoLlave_01_T01Contrato As String = "_01_T01Contrato"
        Public Const CampoLlave_01_T01ODS As String = "_01_T01ODS"
        Public Const CampoLlave_01_T01NombreProyecto As String = "_01_T01NombreProyecto"
        Public Const CampoLlave_01_T01CodigoDocumento As String = "_01_T01CodigoDocumento"
        Public Const Campo_01_T01NombreDocumento As String = "_01_T01NombreDocumento"
        Public Const Campo_01_T01VersionDocumento As String = "_01_T01VersionDocumento"
        Public Const Campo_01_T01FechaDocumento As String = "_01_T01FechaDocumento"
        Public Const Campo_01_T01Area As String = "_01_T01Area"
        Public Const Campo_01_T01Disciplina As String = "_01_T01Disciplina"
        Public Const Campo_01_T01Descripcion As String = "_01_T01Descripcion"
        Public Const Campo_01_T01CodigoEcopetrol As String = "_01_T01CodigoEcopetrol"
        Public Const Campo_01_T01CodigoSeringtec As String = "_01_T01CodigoSeringtec"
        Public Const Campo_01_T01VersionActual As String = "_01_T01VersionActual"
        Public Const Campo_01_T01VersionP As String = "_01_T01VersionP"
        Public Const Campo_01_T01Revisado As String = "_01_T01Revisado"
        Public Const Campo_01_T01Comentado As String = "_01_T01Comentado"
        Public Const Campo_01_T01VersionA As String = "_01_T01VersionA"
        Public Const Campo_01_T01LiberadoA As String = "_01_T01LiberadoA"
        Public Const Campo_01_T01Version0 As String = "_01_T01Version0"
        Public Const Campo_01_T01Liberado0 As String = "_01_T01Liberado0"
        Public Const Campo_01_T01Version1 As String = "_01_T01Version1"
        Public Const Campo_01_T01Liberado1 As String = "_01_T01Liberado1"
        Public Const Campo_01_T01Version2 As String = "_01_T01Version2"
        Public Const Campo_01_T01Liberado2 As String = "_01_T01Liberado2"
        Public Const Campo_01_T01Version3 As String = "_01_T01Version3"
        Public Const Campo_01_T01Liberado3 As String = "_01_T01Liberado3"
        Public Const Campo_01_T01Responsable As String = "_01_T01Responsable"
        Public Const CampoLlave_01_T01FechaRegistro As String = "_01_T01FechaRegistro"
        Public Const CampoLlave_01_T01HoraRegistro As String = "_01_T01HoraRegistro"
        Public Const CampoLlave_01_T01Consecutivo As String = "_01_T01Consecutivo"

        Public Const CamposTabla As String = "_01_T01Contrato,_01_T01ODS,_01_T01NombreProyecto,_01_T01CodigoDocumento,_01_T01NombreDocumento,_01_T01VersionDocumento,_01_T01FechaDocumento,_01_T01Area,_01_T01Disciplina,_01_T01Descripcion,_01_T01CodigoEcopetrol,_01_T01CodigoSeringtec,_01_T01VersionActual,_01_T01VersionP,_01_T01Revisado,_01_T01Comentado,_01_T01VersionA,_01_T01LiberadoA,_01_T01Version0,_01_T01Liberado0,_01_T01Version1,_01_T01Liberado1,_01_T01Version2,_01_T01Liberado2,_01_T01Version3,_01_T01Liberado3,_01_T01Responsable,_01_T01FechaRegistro,_01_T01HoraRegistro,_01_T01Consecutivo"
        Public Const CamposActualizar As String = "_01_T01NombreDocumento,_01_T01VersionDocumento,_01_T01FechaDocumento,_01_T01Area,_01_T01Disciplina,_01_T01Descripcion,_01_T01CodigoEcopetrol,_01_T01CodigoSeringtec,_01_T01VersionActual,_01_T01VersionP,_01_T01Revisado,_01_T01Comentado,_01_T01VersionA,_01_T01LiberadoA,_01_T01Version0,_01_T01Liberado0,_01_T01Version1,_01_T01Liberado1,_01_T01Version2,_01_T01Liberado2,_01_T01Version3,_01_T01Liberado3,_01_T01Responsable"
    End Structure


    Structure _01_T03REGISTROHORAS
        Public Const NombreTabla As String = "_01_T03REGISTROHORAS"
        Public Const CodigoModulo As String = "_01_T03"
        Public Const CampoLlave_01_T03Usuario As String = "_01_T03DocFuncionario"
        Public Const CampoLlave_01_T03Fecha As String = "_01_T03Fecha"
        Public Const CampoLlave_01_T03Contrato As String = "_01_T03Contrato"
        Public Const CampoLlave_01_T03ODS As String = "_01_T03ODS"
        Public Const CampoLlave_01_T03Entregable As String = "_01_T03Entregable"
        Public Const CampoLlave_01_T03Consecutivo As String = "_01_T03Consecutivo"
        Public Const Campo_01_T03Version As String = "_01_T03Version"
        Public Const Campo_01_T03tiempo As String = "_01_T03tiempo"
        Public Const Campo_01_T03DescripcionAcguividad As String = "_01_T03DescripcionActividad"
        Public Const Campo_01_T03Observaciones As String = "_01_T03TiempoaHoras"
        Public Const Campo_01_T03Estado As String = "_01_T03Estado"
        Public Const Campo_01_T03Aprobado_Fecha As String = "_01_T03Aprobado_Fecha"
        Public Const Campo_01_T03Aprobado_Hora As String = "_01_T03Aprobado_Hora"
        Public Const Campo_01_T03Aprobado_IDAprobacion As String = "_01_T03Aprobado_IDAprobacion"
        Public Const Campo_01_T03Aprobado_Rechazo_Obs As String = "_01_T03Aprobado_Rechazo_Obs"
        Public Const Campo_01_T03EnviadoAProbacion As String = "_01_T03EnviadoAProbacion"
        Public Const Campo_01_T03Disciplina As String = "_01_T03Disciplina"
        Public Const Campo_01_T03Horas As String = "_01_T03Horas"
        Public Const Campo_01_T03Minutos As String = "_01_T03Minutos"
        Public Const CampoLlave_01_T03Cliente As String = "_01_T03Cliente"

        Public Const Campo_01_T03Confirmado_Fecha As String = "_01_T03Confirmado_Fecha"
        Public Const Campo_01_T03Confirmado_Hora As String = "_01_T03Confirmado_Hora"
        Public Const Campo_01_T03Confirmado_IDConfirmado As String = "_01_T03Confirmado_IDConfirmado"
        Public Const Campo_01_T03Confirmado_Rechazo_Obs As String = "_01_T03Confirmado_Rechazo_Obs"
        Public Const Campo_01_T03ActividadesCodigo As String = "_01_T03ActividadesCodigo"
        Public Const Campo_01_T03ActividadesDescripcion As String = "_01_T03ActividadesDescripcion"
        Public Const Campo_01_T03EntregableSeringtec As String = "_01_T03EntregableSeringtec"
        Public Const Campo_01_T03fechaRegistro As String = "_01_T03fechaRegistro"
        Public Const Campo_01_T03HoraRegistro As String = "_01_T03HoraRegistro"


        Public Const CamposTabla As String = "_01_T03DocFuncionario,_01_T03Fecha,_01_T03Contrato,_01_T03ODS,_01_T03Entregable,_01_T03Consecutivo,_01_T03Version,_01_T03Tiempo,_01_T03DescripcionActividad,_01_T03TiempoaHoras,_01_T03Estado,_01_T03Aprobado_Fecha,_01_T03Aprobado_Hora,_01_T03Aprobado_IDAprobacion,_01_T03Aprobado_Rechazo_Obs,_01_T03EnviadoAProbacion,_01_T03Disciplina,_01_T03Horas,_01_T03Minutos,_01_T03Cliente,_01_T03Envio_IDEnviado,_01_T03Envio_FechaEnviado,_01_T03Envio_HoraEnviado,_01_T03Envio_Observaciones,_01_T03Confirmado_Fecha,_01_T03Confirmado_Hora,_01_T03Confirmado_IDConfirmado,_01_T03Confirmado_Rechazo_Obs,_01_T03ActividadesCodigo,_01_T03ActividadesDescripcion,_01_T03EntregableSeringtec,_01_T03fechaRegistro,_01_T03HoraRegistro"
        Public Const CamposActualizar As String = "_01_T03Version,_01_T03Horas,_01_T03DescripcionAcguividad,_01_T03Observaciones,_01_T03Aprobado,_01_T03Aprobado_Fecha,_01_T03Aprobado_Hora,_01_T03Aprobado_IDAprobacion,_01_T03Aprobado_IDAprobacion_Rechazo,_01_T03EnviadoAProbacion"
    End Structure

    Structure _01_T03_2REGISTROHORAS_HISTORIAL
        Public Const NombreTabla As String = "_01_T03_2REGISTROHORAS_HISTORIAL"
        Public Const CodigoModulo As String = "_01_T03_2"
        Public Const CampoLlave_01_T03_2Usuario As String = "_01_T03_2DocFuncionario"
        Public Const CampoLlave_01_T03_2Fecha As String = "_01_T03_2Fecha"
        Public Const CampoLlave_01_T03_2Contrato As String = "_01_T03_2Contrato"
        Public Const CampoLlave_01_T03_2ODS As String = "_01_T03_2ODS"
        Public Const CampoLlave_01_T03_2Entregable As String = "_01_T03_2Entregable"
        Public Const CampoLlave_01_T03_2Consecutivo As String = "_01_T03_2Consecutivo"
        Public Const Campo_01_T03_2Version As String = "_01_T03_2Version"
        Public Const Campo_01_T03_2tiempo As String = "_01_T03_2tiempo"
        Public Const Campo_01_T03_2DescripcionAcguividad As String = "_01_T03_2DescripcionActividad"
        Public Const Campo_01_T03_2Observaciones As String = "_01_T03_2TiempoaHoras"
        Public Const Campo_01_T03_2Estado As String = "_01_T03_2Estado"
        Public Const Campo_01_T03_2Aprobado_Fecha As String = "_01_T03_2Aprobado_Fecha"
        Public Const Campo_01_T03_2Aprobado_Hora As String = "_01_T03_2Aprobado_Hora"
        Public Const Campo_01_T03_2Aprobado_IDAprobacion As String = "_01_T03_2Aprobado_IDAprobacion"
        Public Const Campo_01_T03_2Aprobado_Rechazo_Obs As String = "_01_T03_2Aprobado_Rechazo_Obs"
        Public Const Campo_01_T03_2EnviadoAProbacion As String = "_01_T03_2EnviadoAProbacion"
        Public Const Campo_01_T03_2Disciplina As String = "_01_T03_2Disciplina"
        Public Const Campo_01_T03_2Horas As String = "_01_T03_2Horas"
        Public Const Campo_01_T03_2Minutos As String = "_01_T03_2Minutos"
        Public Const CampoLlave_01_T03_2Cliente As String = "_01_T03_2Cliente"

        Public Const Campo_01_T03_2Confirmado_Fecha As String = "_01_T03_2Confirmado_Fecha"
        Public Const Campo_01_T03_2Confirmado_Hora As String = "_01_T03_2Confirmado_Hora"
        Public Const Campo_01_T03_2Confirmado_IDConfirmado As String = "_01_T03_2Confirmado_IDConfirmado"
        Public Const Campo_01_T03_2Confirmado_Rechazo_Obs As String = "_01_T03_2Confirmado_Rechazo_Obs"
        Public Const Campo_01_T03_2ActividadesCodigo As String = "_01_T03_2ActividadesCodigo"
        Public Const Campo_01_T03_2ActividadesDescripcion As String = "_01_T03_2ActividadesDescripcion"
        Public Const Campo_01_T03_2FechaReenviado As String = "_01_T03_2FechaReenviado"
        Public Const Campo_01_T03_2HoraReenviado As String = "_01_T03_2HoraReenviado"
        Public Const Campo_01_T03_2ConsecutivoReenviado As String = "_01_T03_2ConsecutivoReenviado"
        Public Const Campo_01_T03_2ObservacionReenviado As String = "_01_T03_2ObservacionReenviado"

        Public Const CamposTabla As String = "_01_T03_2DocFuncionario,_01_T03_2Fecha,_01_T03_2Contrato,_01_T03_2ODS,_01_T03_2Entregable,_01_T03_2Consecutivo,_01_T03_2Version,_01_T03_2Tiempo,_01_T03_2DescripcionActividad,_01_T03_2TiempoaHoras,_01_T03_2Estado,_01_T03_2Aprobado_Fecha,_01_T03_2Aprobado_Hora,_01_T03_2Aprobado_IDAprobacion,_01_T03_2Aprobado_Rechazo_Obs,_01_T03_2EnviadoAProbacion,_01_T03_2Disciplina,_01_T03_2Horas,_01_T03_2Minutos,_01_T03_2Cliente,_01_T03_2Envio_IDEnviado,_01_T03_2Envio_FechaEnviado,_01_T03_2Envio_HoraEnviado,_01_T03_2Envio_Observaciones,_01_T03_2Confirmado_Fecha,_01_T03_2Confirmado_Hora,_01_T03_2Confirmado_IDConfirmado,_01_T03_2Confirmado_Rechazo_Obs,_01_T03_2ActividadesCodigo,_01_T03_2ActividadesDescripcion,_01_T03_2FechaReenviado,_01_T03_2HoraReenviado,_01_T03_2ConsecutivoReenviado,_01_T03_2ObservacionReenviado"
        Public Const CamposActualizar As String = "_01_T03_2Version,_01_T03_2Horas,_01_T03_2DescripcionAcguividad,_01_T03_2Observaciones,_01_T03_2Aprobado,_01_T03_2Aprobado_Fecha,_01_T03_2Aprobado_Hora,_01_T03_2Aprobado_IDAprobacion,_01_T03_2Aprobado_IDAprobacion_Rechazo,_01_T03_2EnviadoAProbacion"
    End Structure


    Structure _01_T03_1REGISTROHORAS_ENVIO
        Public Const NombreTabla As String = "_01_T03_1REGISTROHORAS_ENVIO"
        Public Const CodigoModulo As String = "_01_T03_1"
        Public Const CampoLlave_01_T03_1NumDocAprobador As String = "_01_T03_1NumDocAprobador"
        Public Const CampoLlave_01_T03_1Disciplina As String = "_01_T03_1Disciplina"
        Public Const CampoLlave_01_T03_1Fecha As String = "_01_T03_1Fecha"
        Public Const CampoLlave_01_T03_1Consecutivo As String = "_01_T03_1Consecutivo"
        Public Const Campo_01_T03_1Codigo As String = "_01_T03_1Codigo"
        Public Const Campo_01_T03_1Cliente As String = "_01_T03_1Cliente"
        Public Const Campo_01_T03_1Contrato As String = "_01_T03_1Contrato"
        Public Const Campo_01_T03_1ODS As String = "_01_T03_1ODS"
        Public Const CampoLlave_01_T03_1Gestion As String = "_01_T03_1Gestion"
        Public Const Campo_01_T03_1Observaciones As String = "_01_T03_1Observaciones"
        Public Const Campo_01_T03_1Hora As String = "_01_T03_1Hora"

        Public Const CamposTabla As String = "_01_T03_1NumDocAprobador,_01_T03_1Disciplina,_01_T03_1Fecha,_01_T03_1Consecutivo,_01_T03_1Codigo,_01_T03_1Cliente,_01_T03_1Contrato,_01_T03_1ODS,_01_T03_1Gestion,_01_T03_1Observaciones,_01_T03_1Hora"
        Public Const CamposActualizar As String = "_01_T03_1Codigo,_01_T03_1Cliente,_01_T03_1Contrato,_01_T03_1ODS,_01_T03_1Observaciones,_01_T03_1Hora"
    End Structure



    Structure _01_T04LIDERES
        Public Const NombreTabla As String = "_01_T04LIDERES"
        Public Const CodigoModulo As String = "_01_T04"
        Public Const CampoLlave_01_T04NumDocLider As String = "_01_T04NumDocLider"
        Public Const CampoLlave_01_T04Disciplinas As String = "_01_T04Disciplinas"
        Public Const Campo_01_T04Observaciones As String = "_01_T04Observaciones"
        Public Const Campo_01_T04Activo As String = "_01_T04Activo"

        Public Const CamposTabla As String = "_01_T04NumDocLider,_01_T04Disciplinas,_01_T04Observaciones,_01_T04Activo"
        Public Const CamposActualizar As String = "_01_T04Activo"
    End Structure

    Structure _01_T06DISCIPLINAS
        Public Const NombreTabla As String = "_01_T06DISCIPLINAS"
        Public Const CodigoModulo As String = "_01_T06"
        Public Const CampoLlave_01_T06Codigo As String = "_01_T06Codigo"
        Public Const Campo_01_T06Nombre As String = "_01_T06Nombre"
        Public Const Campo_01_T06Observaciones As String = "_01_T06Observaciones"
        Public Const Campo_01_T06Activo As String = "_01_T06Activo"
        Public Const Campo_01_T06CodigoTEm As String = "_01_T06CodigoTEm"

        Public Const CamposTabla As String = "_01_T06Codigo,_01_T06Nombre,_01_T06Observaciones,_01_T06Activo,_01_T06CodigoTEm"
        Public Const CamposActualizar As String = "_01_T06Nombre,_01_T06Observaciones,_01_T06Activo"
    End Structure

    Structure _01_T07CONTRATOSMARCO
        Public Const NombreTabla As String = "_01_T07CONTRATOSMARCO"
        Public Const CodigoModulo As String = "_01_T07"
        Public Const CampoLlave_01_T07Codigo As String = "_01_T07Codigo"
        Public Const Campo_01_T07Nombre As String = "_01_T07Nombre"
        Public Const Campo_01_T07Activo As String = "_01_T07Activo"
        Public Const CampoLlave_01_T07Cliente As String = "_01_T07Cliente"
        Public Const Campo_01_T07Abreviacion As String = "_01_T07Abreviacion"

        Public Const CamposTabla As String = "_01_T07Codigo,_01_T07Nombre,_01_T07Activo,_01_T07Cliente,_01_T07Abreviacion"
        Public Const CamposActualizar As String = "_01_T07Nombre,_01_T07Activo,_01_T07Abreviacion"
    End Structure

    Structure _01_T08CLIENTES
        Public Const NombreTabla As String = "_01_T08CLIENTES"
        Public Const CodigoModulo As String = "_01_T08"
        Public Const CampoLlave_01_T08Nit As String = "_01_T08Nit"
        Public Const Campo_01_T08Nombre As String = "_01_T08Nombre"
        Public Const Campo_01_T08Activo As String = "_01_T08Activo"
        Public Const Campo_01_T08CodigoCliente As String = "_01_T08CodigoCliente"
        Public Const Campo_01_T08CodigoSeringtec As String = "_01_T08CodigoSeringtec"

        Public Const CamposTabla As String = "_01_T08Nit,_01_T08Nombre,_01_T08Activo,_01_T08CodigoCliente,_01_T08CodigoSeringtec"
        Public Const CamposActualizar As String = "_01_T08Nombre,_01_T08Activo,_01_T08Codigo"
    End Structure

    Structure _01_T09ODS
        Public Const NombreTabla As String = "_01_T09ODS"
        Public Const CodigoModulo As String = "_01_T09"
        Public Const CampoLlave_01_T09Cliente As String = "_01_T09Cliente"
        Public Const CampoLlave_01_T09ContratoMArco As String = "_01_T09ContratoMArco"
        Public Const CampoLlave_01_T09NumODS As String = "_01_T09NumODS"
        Public Const Campo_01_T09ObjetoODS As String = "_01_T09ObjetoODS"
        Public Const Campo_01_T09FechaInicio As String = "_01_T09FechaInicio"
        Public Const Campo_01_T09FechaFin As String = "_01_T09FechaFin"
        Public Const Campo_01_T09DocumentoCoordinador As String = "_01_T09DocumentoCoordinador"
        Public Const Campo_01_T09Codigo As String = "_01_T09Codigo"
        Public Const Campo_01_T09Activo As String = "_01_T09Activo"

        Public Const CamposTabla As String = "_01_T09Cliente,_01_T09ContratoMArco,_01_T09NumODS,_01_T09ObjetoODS,_01_T09FechaInicio,_01_T09FechaFin,_01_T09DocumentoCoordinador,_01_T09Codigo,_01_T09Activo"
        Public Const CamposActualizar As String = "_01_T09ObjetoODS,_01_T09FechaInicio,_01_T09FechaFin,_01_T09DocumentoCoordinador,_01_T09Codigo,_01_T09Activo"
    End Structure

    Structure _01_T10RECURSOSxODS
        Public Const NombreTabla As String = "_01_T10RECURSOSxODS"
        Public Const CodigoModulo As String = "_01_T10"
        Public Const CampoLlave_01_T10Cliente As String = "_01_T10Cliente"
        Public Const CampoLlave_01_T10Contrato As String = "_01_T10Contrato"
        Public Const CampoLlave_01_T10ODS As String = "_01_T10ODS"
        Public Const CampoLlave_01_T10Disciplina As String = "_01_T10Disciplina"
        Public Const CampoLlave_01_T10DocumentoFuncionario As String = "_01_T10NumDocFuncionario"
        Public Const Campo_01_T10Activo As String = "_01_T10Activo"
        Public Const Campo_01_T10FechaInicial As String = "_T10FechaInicial"
        Public Const Campo_01_T10FechaFinal As String = "_01_T10FechaFinal"
        Public Const Campo_01_T10PorcentajeDedicacion As String = "_01_T10PorcentajeDedicacion"

        Public Const CamposTabla As String = "_01_T10Cliente,_01_T10Contrato,_01_T10ODS,_01_T10Disciplina,_01_T10NumDocFuncionario,_01_T10Activo,_T10FechaInicial,_01_T10FechaFinal,_01_T10PorcentajeDedicacion"
        Public Const CamposActualizar As String = ""
    End Structure

    Structure _01_T10_1RECURSOSxODS_HISTORIAL
        Public Const NombreTabla As String = "_01_T10_1RECURSOSxODS_HISTORIAL"
        Public Const CodigoModulo As String = "_01_T10_1"
        Public Const CampoLlave_01_T10_1ODS As String = "_01_T10_1ODS"
        Public Const CampoLlave_01_T10_1Disciplina As String = "_01_T10_1Disciplina"
        Public Const CampoLlave_01_T10_1DocumentoFuncionario As String = "_01_T10_1NumDocFuncionario"
        Public Const CampoLlave_01_T10_1Consecutivo As String = "_01_T10_1Consecutivo"
        Public Const Campo_01_T10_1FechaInicial As String = "_01_T10_1FechaInicial"
        Public Const Campo_01_T10_1FechaFinal As String = "_01_T10_1FechaFinal"
        Public Const Campo_01_T10_1PorcentajeDedicacion As String = "_01_T10_1PorcentajeDedicacion"

        Public Const CamposTabla As String = "_01_T10_1ODS,_01_T10_1Disciplina,_01_T10_1NumDocFuncionario,_01_T10_1Consecutivo,_01_T10_1FechaInicial,_01_T10_1FechaFinal,_01_T10_1PorcentajeDedicacion"
        Public Const CamposActualizar As String = ""
    End Structure

    Structure _01_T11MATRIZTRABAJADORES
        Public Const NombreTabla As String = "_01_T11MATRIZTRABAJADORES"
        Public Const CodigoModulo As String = "_01_T11"
        Public Const Campo_01_T11VinculoHistorial As String = "_01_T11VinculoHistorial"
        Public Const CampoLlave_01_T11TipoDocumento As String = "_01_T11TipoDocumento"
        Public Const Campo_01_T11NumDocDocumento As String = "_01_T11NumDocDocumento"
        Public Const Campo_01_T11NombresApellidos As String = "_01_T11NombresApellidos"
        Public Const Campo_01_T11DocFechaExp As String = "_01_T11DocFechaExp"
        Public Const Campo_01_T11DocLugarExp As String = "_01_T11DocLugarExp"
        Public Const Campo_01_T11DocLugarNac As String = "_01_T11DocLugarNac"
        Public Const Campo_01_T11FechaNac As String = "_01_T11FechaNac"
        Public Const Campo_01_T11HijosPerACargo As String = "_01_T11HijosPerACargo"
        Public Const Campo_01_T11ContactoEmergerncia As String = "_01_T11ContactoEmergerncia"
        Public Const Campo_01_T11ContactoEmergerncia_Tel As String = "_01_T11ContactoEmergerncia_Tel"
        Public Const Campo_01_T11RH As String = "_01_T11RH"
        Public Const Campo_01_T11NumTarjetaProfesional As String = "_01_T11NumTarjetaProfesional"
        Public Const Campo_01_T11Alergias As String = "_01_T11Alergias"
        Public Const Campo_01_T11EmailPersonal As String = "_01_T11EmailPersonal"
        Public Const Campo_01_T11GradoEscolaridad As String = "_01_T11GradoEscolaridad"
        Public Const Campo_01_T11Direccion As String = "_01_T11Direccion"
        Public Const Campo_01_T11Procedencia As String = "_01_T11Procedencia"
        Public Const Campo_01_T11ComposicionFamiliar As String = "_01_T11ComposicionFamiliar"
        Public Const Campo_01_T11EstratoSocioeconomico As String = "_01_T11EstratoSocioeconomico"
        Public Const Campo_01_T11EstadoCivir As String = "_01_T11EstadoCivir"
        Public Const Campo_01_T11Edad As String = "_01_T11Edad"
        Public Const Campo_01_T11Sexo As String = "_01_T11Sexo"
        Public Const Campo_01_T11TurnoTrabajo As String = "_01_T11TurnoTrabajo"
        Public Const Campo_01_T11Raza As String = "_01_T11Raza"
        Public Const Campo_01_T11Cargo As String = "_01_T11Cargo"
        Public Const Campo_01_T11TipoContrato As String = "_01_T11TipoContrato"
        Public Const Campo_01_T11FechaIngreso As String = "_01_T11FechaIngreso"
        Public Const Campo_01_T11Salario As String = "_01_T11Salario"
        Public Const Campo_01_T11Bono As String = "_01_T11Bono"
        Public Const Campo_01_T11BonoAdicional As String = "_01_T11BonoAdicional"
        Public Const Campo_01_T11DuracionCTO As String = "_01_T11DuracionCTO"
        Public Const Campo_01_T11FechaFinalPerioPrueba As String = "_01_T11FechaFinalPerioPrueba"
        Public Const Campo_01_T11FechaVencimiento As String = "_01_T11FechaVencimiento"
        Public Const Campo_01_T11RegistroNovedadesdePersonal As String = "_01_T11RegistroNovedadesdePersonal"
        Public Const Campo_01_T11ODSContrato As String = "_01_T11ODSContrato"
        Public Const Campo_01_T11ODSDondeEstaLaborando As String = "_01_T11ODSDondeEstaLaborando"
        Public Const Campo_01_T11FechaTerminacionODS As String = "_01_T11FechaTerminacionODS"
        Public Const Campo_01_T11Area As String = "_01_T11Area"
        Public Const Campo_01_T11Jefe As String = "_01_T11Jefe"
        Public Const Campo_01_T11NumContrato As String = "_01_T11NumContrato"
        Public Const Campo_01_T11NumOtroSI As String = "_01_T11NumOtroSI"
        Public Const Campo_01_T11UbicacionSede As String = "_01_T11UbicacionSede"
        Public Const Campo_01_T11EPS As String = "_01_T11EPS"
        Public Const Campo_01_T11EPS_FechaAfiliacion As String = "_01_T11EPS_FechaAfiliacion"
        Public Const Campo_01_T11EPS_EstadoAfiliacion As String = "_01_T11EPS_EstadoAfiliacion"
        Public Const Campo_01_T11ARL As String = "_01_T11ARL"
        Public Const Campo_01_T11ARL_TasaRiesgo As String = "_01_T11ARL_TasaRiesgo"
        Public Const Campo_01_T11ARL_Tarifa As String = "_01_T11ARL_Tarifa"
        Public Const Campo_01_T11ARL_FechaAfiliacion As String = "_01_T11ARL_FechaAfiliacion"
        Public Const Campo_01_T11ARL_EstadoAfiliacion As String = "_01_T11ARL_EstadoAfiliacion"
        Public Const Campo_01_T11FondoPensones As String = "_01_T11FondoPensones"
        Public Const Campo_01_T11FondoPensones_FechaNovedad As String = "_01_T11FondoPensones_FechaNovedad"
        Public Const Campo_01_T11FondoPensones_EstadoAfiliacion As String = "_01_T11FondoPensones_EstadoAfiliacion"
        Public Const Campo_01_T11CCF As String = "_01_T11CCF"
        Public Const Campo_01_T11CCF_FechaAfiliacion As String = "_01_T11CCF_FechaAfiliacion"
        Public Const Campo_01_T11CCF_EstadoAfiliacion As String = "_01_T11CCF_EstadoAfiliacion"
        Public Const Campo_01_T11Cesantias As String = "_01_T11Cesantias"
        Public Const Campo_01_T11Cesantias_FechaAfiliacion As String = "_01_T11Cesantias_FechaAfiliacion"
        Public Const Campo_01_T11Cesantias_EstadoAfiliacion As String = "_01_T11Cesantias_EstadoAfiliacion"
        Public Const Campo_01_T11RequerimientoPersonal As String = "_01_T11RequerimientoPersonal"
        Public Const Campo_01_T11ConceptoMedAptIngreso As String = "_01_T11ConceptoMedAptIngreso"
        Public Const Campo_01_T11PerfilCargo As String = "_01_T11PerfilCargo"
        Public Const Campo_01_T11EntrevistaSeleccion As String = "_01_T11EntrevistaSeleccion"
        Public Const Campo_01_T11VinculacionPersonal As String = "_01_T11VinculacionPersonal"
        Public Const Campo_01_T11InduccionPuestoTrabajo As String = "_01_T11InduccionPuestoTrabajo"
        Public Const Campo_01_T11HojadeVida As String = "_01_T11HojadeVida"
        Public Const Campo_01_T11SoportesAcademicos As String = "_01_T11SoportesAcademicos"
        Public Const Campo_01_T11SoportesEduContinuada As String = "_01_T11SoportesEduContinuada"
        Public Const Campo_01_T11CertificacionesLaborales As String = "_01_T11CertificacionesLaborales"
        Public Const Campo_01_T11TarjetaProfesional As String = "_01_T11TarjetaProfesional"
        Public Const Campo_01_T11CarneVacunacion As String = "_01_T11CarneVacunacion"
        Public Const Campo_01_T11Cedula As String = "_01_T11Cedula"
        Public Const Campo_01_T11CuentaNomina As String = "_01_T11CuentaNomina"
        Public Const Campo_01_T11RUT As String = "_01_T11RUT"
        Public Const Campo_01_T11Fotografia As String = "_01_T11Fotografia"
        Public Const Campo_01_T11CertificacionesEPSyARL As String = "_01_T11CertificacionesEPSyARL"
        Public Const Campo_01_T11Antecedentes As String = "_01_T11Antecedentes"
        Public Const Campo_01_T11SDocumentosPersonasaCargo As String = "_01_T11SDocumentosPersonasaCargo"
        Public Const Campo_01_T11InduccionGH As String = "_01_T11InduccionGH"
        Public Const Campo_01_T11InduccionHSEQ As String = "_01_T11InduccionHSEQ"
        Public Const Campo_01_T11InduccionESpPtoTrabajo As String = "_01_T11InduccionESpPtoTrabajo"
        Public Const Campo_01_T11RegistroEntregaDotacionEPP As String = "_01_T11RegistroEntregaDotacionEPP"
        Public Const Campo_01_T11FechaExamenesMedicos As String = "_01_T11FechaExamenesMedicos"
        Public Const Campo_01_T11ConceptoAptitudExamenPeriodico As String = "_01_T11ConceptoAptitudExamenPeriodico"
        Public Const Campo_01_T11CertificadoTSA As String = "_01_T11CertificadoTSA "
        Public Const Campo_01_T11CertificadoEspaciosConfinados As String = "_01_T11CertificadoEspaciosConfinados"
        Public Const Campo_01_T11Curso50HrasSGSST As String = "_01_T11Curso50HrasSGSST"
        Public Const Campo_01_T11EvaluaciondeDesempeño As String = "_01_T11EvaluaciondeDesempeño"
        Public Const Campo_01_T11ProcesoDisciplinario As String = "_01_T11ProcesoDisciplinario"
        Public Const Campo_01_T11fechaRetiro As String = "_01_T11fechaRetiro"
        Public Const Campo_01_T11fechaRetiroTrabajaroes As String = "_01_T11fechaRetiroTrabajaroes"
        Public Const Campo_01_T11CartaTerminacionCTO As String = "_01_T11CartaTerminacionCTO"
        Public Const Campo_01_T11OrdenExamenMedicoRetiro As String = "_01_T11OrdenExamenMedicoRetiro"
        Public Const Campo_01_T11ConceptoMedicoAptitudRetiro As String = "_01_T11ConceptoMedicoAptitudRetiro"
        Public Const Campo_01_T11EntregaPuestoTrabajo As String = "_01_T11EntregaPuestoTrabajo"
        Public Const Campo_01_T11LiquidacionPrestacionesSociales As String = "_01_T11LiquidacionPrestacionesSociales"
        Public Const Campo_01_T11Item As String = "_01_T11Item"
        Public Const CampoLlave_01_T11Id As String = "_01_T11Id"

        Public Const CamposTabla As String = "_01_T11VinculoHistorial,_01_T11TipoDocumento,_01_T11NumDocDocumento,_01_T11NombresApellidos,_01_T11DocFechaExp,_01_T11DocLugarExp,_01_T11DocLugarNac,_01_T11FechaNac,_01_T11HijosPerACargo,_01_T11ContactoEmergerncia,_01_T11ContactoEmergerncia_Tel,_01_T11RH,_01_T11NumTarjetaProfesional,_01_T11Alergias,_01_T11EmailPersonal,_01_T11GradoEscolaridad,_01_T11Direccion,_01_T11Procedencia,_01_T11ComposicionFamiliar,_01_T11EstratoSocioeconomico,_01_T11EstadoCivir,_01_T11Edad,_01_T11Sexo,_01_T11TurnoTrabajo,_01_T11Raza,_01_T11Cargo,_01_T11TipoContrato,_01_T11FechaIngreso,_01_T11Salario,_01_T11Bono,_01_T11BonoAdicional,_01_T11DuracionCTO,_01_T11FechaFinalPerioPrueba,_01_T11FechaVencimiento,_01_T11RegistroNovedadesdePersonal,_01_T11ODSContrato,_01_T11ODSDondeEstaLaborando,_01_T11FechaTerminacionODS,_01_T11Area,_01_T11Jefe,_01_T11NumContrato,_01_T11NumOtroSI,_01_T11UbicacionSede,_01_T11EPS,_01_T11EPS_FechaAfiliacion,_01_T11EPS_EstadoAfiliacion,_01_T11ARL,_01_T11ARL_TasaRiesgo,_01_T11ARL_Tarifa,_01_T11ARL_FechaAfiliacion,_01_T11ARL_EstadoAfiliacion,_01_T11FondoPensones,_01_T11FondoPensones_FechaNovedad,_01_T11FondoPensones_EstadoAfiliacion,_01_T11CCF,_01_T11CCF_FechaAfiliacion,_01_T11CCF_EstadoAfiliacion,_01_T11Cesantias,_01_T11Cesantias_FechaAfiliacion,_01_T11Cesantias_EstadoAfiliacion,_01_T11RequerimientoPersonal,_01_T11ConceptoMedAptIngreso,_01_T11PerfilCargo,_01_T11EntrevistaSeleccion,_01_T11VinculacionPersonal,_01_T11InduccionPuestoTrabajo,_01_T11HojadeVida,_01_T11SoportesAcademicos,_01_T11SoportesEduContinuada,_01_T11CertificacionesLaborales,_01_T11TarjetaProfesional,_01_T11CarneVacunacion,_01_T11Cedula,_01_T11CuentaNomina,_01_T11RUT,_01_T11Fotografia,_01_T11CertificacionesEPSyARL,_01_T11Antecedentes,_01_T11SDocumentosPersonasaCargo,_01_T11InduccionGH,_01_T11InduccionHSEQ,_01_T11InduccionESpPtoTrabajo,_01_T11RegistroEntregaDotacionEPP,_01_T11FechaExamenesMedicos,_01_T11ConceptoAptitudExamenPeriodico,_01_T11CertificadoTSA ,_01_T11CertificadoEspaciosConfinados,_01_T11Curso50HrasSGSST,_01_T11EvaluaciondeDesempeño,_01_T11ProcesoDisciplinario,_01_T11fechaRetiro,_01_T11fechaRetiroTrabajaroes,_01_T11CartaTerminacionCTO,_01_T11OrdenExamenMedicoRetiro,_01_T11ConceptoMedicoAptitudRetiro,_01_T11EntregaPuestoTrabajo,_01_T11LiquidacionPrestacionesSociales,_01_T11Item,_01_T11Id"
        Public Const CamposActualizar As String = "_01_T11VinculoHistorial,_01_T11NumDocDocumento,_01_T11NombresApellidos,_01_T11DocFechaExp,_01_T11DocLugarExp,_01_T11DocLugarNac,_01_T11FechaNac,_01_T11HijosPerACargo,_01_T11ContactoEmergerncia,_01_T11ContactoEmergerncia_Tel,_01_T11RH,_01_T11NumTarjetaProfesional,_01_T11Alergias,_01_T11EmailPersonal,_01_T11GradoEscolaridad,_01_T11Direccion,_01_T11Procedencia,_01_T11ComposicionFamiliar,_01_T11EstratoSocioeconomico,_01_T11EstadoCivir,_01_T11Edad,_01_T11Sexo,_01_T11TurnoTrabajo,_01_T11Raza,_01_T11Cargo,_01_T11TipoContrato,_01_T11FechaIngreso,_01_T11Salario,_01_T11Bono,_01_T11BonoAdicional,_01_T11DuracionCTO,_01_T11FechaFinalPerioPrueba,_01_T11FechaVencimiento,_01_T11RegistroNovedadesdePersonal,_01_T11ODSContrato,_01_T11ODSDondeEstaLaborando,_01_T11FechaTerminacionODS,_01_T11Area,_01_T11Jefe,_01_T11NumContrato,_01_T11NumOtroSI,_01_T11UbicacionSede,_01_T11EPS,_01_T11EPS_FechaAfiliacion,_01_T11EPS_EstadoAfiliacion,_01_T11ARL,_01_T11ARL_TasaRiesgo,_01_T11ARL_Tarifa,_01_T11ARL_FechaAfiliacion,_01_T11ARL_EstadoAfiliacion,_01_T11FondoPensones,_01_T11FondoPensones_FechaNovedad,_01_T11FondoPensones_EstadoAfiliacion,_01_T11CCF,_01_T11CCF_FechaAfiliacion,_01_T11CCF_EstadoAfiliacion,_01_T11Cesantias,_01_T11Cesantias_FechaAfiliacion,_01_T11Cesantias_EstadoAfiliacion,_01_T11RequerimientoPersonal,_01_T11ConceptoMedAptIngreso,_01_T11PerfilCargo,_01_T11EntrevistaSeleccion,_01_T11VinculacionPersonal,_01_T11InduccionPuestoTrabajo,_01_T11HojadeVida,_01_T11SoportesAcademicos,_01_T11SoportesEduContinuada,_01_T11CertificacionesLaborales,_01_T11TarjetaProfesional,_01_T11CarneVacunacion,_01_T11Cedula,_01_T11CuentaNomina,_01_T11RUT,_01_T11Fotografia,_01_T11CertificacionesEPSyARL,_01_T11Antecedentes,_01_T11SDocumentosPersonasaCargo,_01_T11InduccionGH,_01_T11InduccionHSEQ,_01_T11InduccionESpPtoTrabajo,_01_T11RegistroEntregaDotacionEPP,_01_T11FechaExamenesMedicos,_01_T11ConceptoAptitudExamenPeriodico,_01_T11CertificadoTSA ,_01_T11CertificadoEspaciosConfinados,_01_T11Curso50HrasSGSST,_01_T11EvaluaciondeDesempeño,_01_T11ProcesoDisciplinario,_01_T11fechaRetiro,_01_T11fechaRetiroTrabajaroes,_01_T11CartaTerminacionCTO,_01_T11OrdenExamenMedicoRetiro,_01_T11ConceptoMedicoAptitudRetiro,_01_T11EntregaPuestoTrabajo,_01_T11LiquidacionPrestacionesSociales,_01_T11Item"
    End Structure


    Structure _01_T11_1MATRIZTRABAJADORES
        Public Const NombreTabla As String = "_01_T11_1MATRIZTRABAJADORES"
        Public Const CodigoModulo As String = "_01_T11_1"

        Public Const CampoLlave_01_T11_1NumDocumento As String = "_01_T11_1NumDocumento"
        Public Const Campo_01_T11_1NombresApellidos As String = "_01_T11_1NombresApellidos"
        Public Const Campo_01_T11_1Cargo As String = "_01_T11_1Cargo"
        Public Const Campo_01_T11_1FechaIngreso As String = "_01_T11_1FechaIngreso"
        Public Const Campo_01_T11_1DisciplinaArea As String = "_01_T11_1DisciplinaArea"
        Public Const Campo_01_T11_1NumDocumentoJefe As String = "_01_T11_1NumDocumentoJefe"
        Public Const Campo_01_T11_1NumNombreJefe As String = "_01_T11_1NumNombreJefe"
        Public Const Campo_01_T11_1UbicacionSede As String = "_01_T11_1UbicacionSede"
        Public Const Campo_01_T11_1Consecutivo As String = "_01_T11_1Consecutivo"
        Public Const Campo_01_T11_1FechaHoraCargue As String = "_01_T11_1FechaHoraCargue"
        Public Const Campo_01_T11_1NumDocFuncionarioGesdtiona As String = "_01_T11_1NumDocFuncionarioGesdtiona"
        Public Const Campo_01_T11_1Sincronizado As String = "_01_T11_1Sincronizado"
        Public Const Campo_01_T11_1SincronizadoObservaciones As String = "_01_T11_1SincronizadoObservaciones"

        Public Const CamposTabla As String = "_01_T11_1NumDocumento,_01_T11_1NombresApellidos,_01_T11_1Cargo,_01_T11_1FechaIngreso,_01_T11_1DisciplinaArea,_01_T11_1NumDocumentoJefe,_01_T11_1NumNombreJefe,_01_T11_1UbicacionSede,_01_T11_1Consecutivo,_01_T11_1FechaHoraCargue,_01_T11_1NumDocFuncionarioGesdtiona,_01_T11_1Sincronizado,_01_T11_1SincronizadoObservaciones"
        Public Const CamposActualizar As String = "_01_T11_1NombresApellidos,_01_T11_1Cargo,_01_T11_1FechaIngreso,_01_T11_1DisciplinaArea,_01_T11_1NumDocumentoJefe,_01_T11_1NumNombreJefe,_01_T11_1UbicacionSede,_01_T11_1Consecutivo,_01_T11_1FechaHoraCargue,_01_T11_1NumDocFuncionarioGesdtiona,_01_T11_1Sincronizado,_01_T11_1SincronizadoObservaciones"
    End Structure


    Structure _01_T12FUNCIONARIOS
        Public Const NombreTabla As String = "_01_T12FUNCIONARIOS"
        Public Const CodigoModulo As String = "_01_T12"
        Public Const CampoLlave_01_T12NumDoc As String = "_01_T12NumDoc"
        Public Const Campo_01_T12NombreApellidos As String = "_01_T12NombreApellidos"
        Public Const Campo_01_T12ConctactoCAsoEmergencia As String = "_01_T12ConctactoCAsoEmergencia"
        Public Const Campo_01_T12ConctactoCAsoEmergencia_Tel As String = "_01_T12ConctactoCAsoEmergencia_Tel"
        Public Const Campo_01_T12Telefono As String = "_01_T12Telefono"
        Public Const Campo_01_T12CorreoPersonal As String = "_01_T12CorreoPersonal"
        Public Const Campo_01_T12CorreoEntidad As String = "_01_T12CorreoEntidad"
        Public Const Campo_01_T12GradoEscolaridad As String = "_01_T12GradoEscolaridad"
        Public Const Campo_01_T12Direccion As String = "_01_T12Direccion"
        Public Const Campo_01_T12Sexo As String = "_01_T12Sexo"
        Public Const Campo_01_T12TurnoTrabajo As String = "_01_T12TurnoTrabajo"
        Public Const Campo_01_T12Cargo As String = "_01_T12Cargo"
        Public Const Campo_01_T12TipoContrato As String = "_01_T12TipoContrato"
        Public Const Campo_01_T12FechaIngreso As String = "_01_T12FechaIngreso"
        Public Const Campo_01_T12DuracionContrato As String = "_01_T12DuracionContrato"
        Public Const Campo_01_T12ODSContrato As String = "_01_T12ODSContrato"
        Public Const Campo_01_T12ODSDondeTrabaja As String = "_01_T12ODSDondeTrabaja"
        Public Const Campo_01_T12Disciplina As String = "_01_T12Disciplina"
        Public Const Campo_01_T12Jefe As String = "_01_T12Jefe"
        Public Const Campo_01_T12NumContrato As String = "_01_T12NumContrato"
        Public Const Campo_01_T12UbicacionSede As String = "_01_T12UbicacionSede"
        Public Const Campo_01_T12FechaRetiro As String = "_01_T12FechaRetiro"
        Public Const Campo_01_T12Activo As String = "_01_T12Activo"
        Public Const Campo_01_T12Contrasena As String = "_01_T12Clave"

        Public Const CamposTabla As String = "_01_T12NumDoc,_01_T12NombreApellidos,_01_T12ConctactoCAsoEmergencia,_01_T12ConctactoCAsoEmergencia_Tel,_01_T12Telefono,_01_T12CorreoPersonal,_01_T12CorreoEntidad,_01_T12GradoEscolaridad,_01_T12Direccion,_01_T12Sexo,_01_T12TurnoTrabajo,_01_T12Cargo,_01_T12TipoContrato,_01_T12FechaIngreso,_01_T12DuracionContrato,_01_T12ODSContrato,_01_T12ODSDondeTrabaja,_01_T12Disciplina,_01_T12Jefe,_01_T12NumContrato,_01_T12UbicacionSede,_01_T12FechaRetiro,_01_T12Activo,_01_T12Clave"
        Public Const CamposActualizar As String = "_01_T12NombreApellidos,_01_T12ConctactoCAsoEmergencia,_01_T12ConctactoCAsoEmergencia_Tel,_01_T12Telefono,_01_T12CorreoPersonal,_01_T12CorreoEntidad,_01_T12GradoEscolaridad,_01_T12Direccion,_01_T12Sexoq,_01_T12TurnoTrabajo,_01_T12Cargo,_01_T12TipoContrato,_01_T12FechaIngreso,_01_T12DuracionContrato,_01_T12ODSContrato,_01_T12ODSDondeTrabaja,_01_T12Disciplina,_01_T12Jefe,_01_T12NumContrato,_01_T12UbicacionSede,_01_T12FechaRetiro,_01_T12Activo,_01_T12Clave"
    End Structure

    Structure _01_T16APLICACIONES
        Public Const NombreTabla As String = "_01_T16APLICACIONES"
        Public Const CodigoModulo As String = "_01_T16"
        Public Const CampoLlave_01_T16Codigo As String = "_01_T16Codigo"
        Public Const Campo_01_T16Nombre As String = "_01_T16Nombre"
        Public Const Campo_01_T16Activo As String = "_01_T16Activo"

        Public Const CamposTabla As String = "_01_T16Codigo,_01_T16Nombre,_01_T16Activo"
        Public Const CamposActualizar As String = "_01_T16Nombre,_01_T16Activo"
    End Structure

    Structure _01_T17PERFILESxAPLICACION
        Public Const NombreTabla As String = "_01_T17PERFILESxAPLICACION"
        Public Const CodigoModulo As String = "_01_T17"
        Public Const CampoLlave_01_T17Aplicacion As String = "_01_T17Aplicacion"
        Public Const CampoLlave_01_T17Codigo As String = "_01_T17Codigo"
        Public Const Campo_01_T17Nombre As String = "_01_T17Nombre"
        Public Const Campo_01_T17Activo As String = "_01_T17Activo"

        Public Const CamposTabla As String = "_01_T17Aplicacion,_01_T17Codigo,_01_T17Nombre,_01_T17Activo"
        Public Const CamposActualizar As String = "_01_T17Nombre,_01_T17Activo"
    End Structure

    Structure _01_T18PERFILESxAPLICACIONxFUNCIONARIO
        Public Const NombreTabla As String = "_01_T18PERFILESxAPLICACIONxFUNCIONARIO"
        Public Const CodigoModulo As String = "_01_T18"
        Public Const CampoLlave_01_T18Aplicacion As String = "_01_T18Aplicacion"
        Public Const CampoLlave_01_T18Perfil As String = "_01_T18Perfil"
        Public Const CampoLlave_01_T18NumDocFuncionario As String = "_01_T18NumDocFuncionario"

        Public Const CamposTabla As String = "_01_T18Aplicacion,_01_T18Perfil,_01_T18NumDocFuncionario"
        Public Const CamposActualizar As String = ""
    End Structure


    Structure _01_T19CATDOCENTREGABLES
        Public Const NombreTabla As String = "_01_T19CATDOCENTREGABLES"
        Public Const CodigoModulo As String = "_01_T19"
        Public Const CampoLlave_01_T19Codigo As String = "_01_T19Codigo"
        Public Const Campo_01_T19Nombre As String = "_01_T19Nombre"
        Public Const Campo_01_T19Activo As String = "_01_T19Activo"

        Public Const CamposTabla As String = "_01_T19Codigo,_01_T19Nombre,_01_T19Activo"
        Public Const CamposActualizar As String = "_01_T19Nombre,_01_T19Activo"
    End Structure

    Structure _01_T20TIPODOCENTREGABLES
        Public Const NombreTabla As String = "_01_T20TIPODOCENTREGABLES"
        Public Const CodigoModulo As String = "_01_T20"
        Public Const CampoLlave_01_T20Disciplina As String = "_01_T20Disciplina"
        Public Const CampoLlave_01_T20Codigo As String = "_01_T20Codigo"
        Public Const Campo_01_T20Nombre As String = "_01_T20Nombre"
        Public Const Campo_01_T20Activo As String = "_01_T20Activo"

        Public Const CamposTabla As String = "_01_T20Disciplina,_01_T20Codigo,_01_T20Nombre,_01_T20Activo"
        Public Const CamposActualizar As String = "_01_T20Nombre,_01_T20Activo"
    End Structure

    Structure _01_T21LISTADODOCENTREGABLES
        Public Const NombreTabla As String = "_01_T21LISTADODOCENTREGABLES"
        Public Const CodigoModulo As String = "_01_T21"
        Public Const CampoLlave_01_T21Cliente As String = "_01_T21Cliente"
        Public Const CampoLlave_01_T21Contrato As String = "_01_T21Contrato"
        Public Const CampoLlave_01_T21ODS As String = "_01_T21ODS"
        Public Const CampoLlave_01_T21Categoria As String = "_01_T21Disciplina"
        Public Const CampoLlave_01_T21TipoDocumento As String = "_01_T21TipoDocumento"
        Public Const CampoLlave_01_T21Consecutivo As String = "_01_T21Consecutivo"
        Public Const Campo_01_T21CodigoEcopetrol As String = "_01_T21CodigoEcopetrol"
        Public Const Campo_01_T21CodigoSeringtec As String = "_01_T21CodigoSeringtec"
        Public Const Campo_01_T21Nombre As String = "_01_T21Nombre"
        Public Const Campo_01_T21VersionActual As String = "_01_T21VersionActual"
        Public Const Campo_01_T21VersionP As String = "_01_T21VersionP"
        Public Const Campo_01_T21VersionPRevisado As String = "_01_T21VersionPRevisado"
        Public Const Campo_01_T21VersionPComentado As String = "_01_T21VersionPComentado"
        Public Const Campo_01_T21VersionA As String = "_01_T21VersionA"
        Public Const Campo_01_T21VersionALiberado As String = "_01_T21VersionALiberado"
        Public Const Campo_01_T21VersionO As String = "_01_T21VersionO"
        Public Const Campo_01_T21VersionOLiberado As String = "_01_T21VersionOLiberado"
        Public Const Campo_01_T21Version1 As String = "_01_T21Version1"
        Public Const Campo_01_T21Version1Liberado As String = "_01_T21Version1Liberado"
        Public Const Campo_01_T21Version2 As String = "_01_T21Version2"
        Public Const Campo_01_T21Version2Liberado As String = "_01_T21Version2Liberado"
        Public Const Campo_01_T21Version3 As String = "_01_T21Version3"
        Public Const Campo_01_T21Version3Liberado As String = "_01_T21Version3Liberado"
        Public Const Campo_01_T21Responsable As String = "_01_T21Responsable"
        Public Const Campo_01_T21Activo As String = "_01_T21Activo"

        Public Const CamposTabla As String = "_01_T21Cliente,_01_T21Contrato,_01_T21ODS,_01_T21Disciplina,_01_T21TipoDocumento,_01_T21Consecutivo,_01_T21CodigoEcopetrol,_01_T21CodigoSeringtec,_01_T21Nombre,_01_T21VersionActual,_01_T21VersionP,_01_T21VersionPRevisado,_01_T21VersionPComentado,_01_T21VersionA,_01_T21VersionALiberado,_01_T21VersionO,_01_T21VersionOLiberado,_01_T21Version1,_01_T21Version1Liberado,_01_T21Version2,_01_T21Version2Liberado,_01_T21Version3,_01_T21Version3Liberado,_01_T21Responsable,_01_T21Activo"
        Public Const CamposActualizar As String = "_01_T21CodigoEcopetrol,_01_T21CodigoSeringtec,_01_T21Nombre,_01_T21VersionActual,_01_T21VersionP,_01_T21VersionPRevisado,_01_T21VersionPComentado,_01_T21VersionA,_01_T21VersionALiberado,_01_T21VersionO,_01_T21VersionOLiberado,_01_T21Version1,_01_T21Version1Liberado,_01_T21Version2,_01_T21Version2Liberado,_01_T21Version3,_01_T21Version3Liberado,_01_T21Responsable,_01_T21Activo"
    End Structure

    Structure _01_T21_01LISTADO_TIPOPLANTILLAS
        Public Const NombreTabla As String = "_01_T21_01LISTADO_TIPOPLANTILLAS"
        Public Const CodigoModulo As String = "_01_T21_01"
        Public Const CampoLlave_01_T21_01Codigo As String = "_01_T21_01Codigo"
        Public Const Campo_01_T21_01Nombre As String = "_01_T21_01Nombre"
        Public Const Campo_01_T21_01Activo As String = "_01_T21_01Activo"
        Public Const Campo_01_T21_01Cliente As String = "_01_T21_01Cliente"
        Public Const Campo_01_T21_01NumContratoMarco As String = "_01_T21_01NumContratoMarco"
        Public Const Campo_01_T21_01LinkDescargaPlantilla As String = "_01_T21_01LinkDescargaPlantilla"
        Public Const Campo_01_T21_01DireccionWebCargue As String = "_01_T21_01DireccionWebCargue"
        Public Const Campo_01_T21_01DireccionWebDescargue As String = "_01_T21_01DireccionWebDescargue"

        Public Const CamposTabla As String = "_01_T21_01Codigo,_01_T21_01Nombre,_01_T21_01Activo,_01_T21_01Cliente,_01_T21_01NumContratoMarco,_01_T21_01LinkDescargaPlantilla,_01_T21_01DireccionWebCargue,_01_T21_01DireccionWebDescargue"
        Public Const CamposActualizar As String = "_01_T21_01Nombre,_01_T21_01Activo,_01_T21_01Cliente,_01_T21_01NumContratoMarco,_01_T21_01LinkDescargaPlantilla,_01_T21_01DireccionWebCargue,_01_T21_01DireccionWebDescargue"
    End Structure

    Structure _01_T21_02LISTADO_DETALLE_01
        Public Const NombreTabla As String = "_01_T21_02LISTADO_DETALLE_01"
        Public Const CodigoModulo As String = "_01_T21_02"
        Public Const Campo_01_T21_02Loc As String = "_01_T21_02Loc"
        Public Const Campo_01_T21_02Dis As String = "_01_T21_02Dis"
        Public Const Campo_01_T21_02Descripcion As String = "_01_T21_02Descripcion"
        Public Const Campo_01_T21_02CodigoCliente As String = "_01_T21_02CodigoCliente"
        Public Const Campo_01_T21_02CodigoSeringtec As String = "_01_T21_02CodigoSeringtec"
        Public Const Campo_01_T21_02VersionActual As String = "_01_T21_02VersionActual"

        Public Const CamposTabla As String = "_01_T21_02Loc,_01_T21_02Dis,_01_T21_02Descripcion,_01_T21_02CodigoCliente,_01_T21_02CodigoSeringtec,_01_T21_02VersionActual"
        Public Const CamposActualizar As String = "_01_T21_02Loc,_01_T21_02Dis,_01_T21_02Descripcion,_01_T21_02CodigoCliente,_01_T21_02CodigoSeringtec,_01_T21_02VersionActual"
    End Structure


    Structure _01_T22AGNOS
        Public Const NombreTabla As String = "_01_T22AGNOS"
        Public Const CodigoModulo As String = "_01_T20"
        Public Const CampoLlave_01_T22Fecha As String = "_01_T22Fecha"
        Public Const Campo_01_T22FechaNumero As String = "_01_T22FechaNumero"
        Public Const Campo_01_T22Dia As String = "_01_T22Dia"
        Public Const Campo_01_T22Mes As String = "_01_T22Mes"
        Public Const Campo_01_T22Agno As String = "_01_T22Agno"
        Public Const Campo_01_T22NombreLargo As String = "_01_T22NombreLargo"
        Public Const Campo_01_T22NombreDia As String = "_01_T22NombreDia"
        Public Const Campo_01_T22NombreMEs As String = "_01_T22NombreMEs"
        Public Const Campo_01_T22DiaFestivo As String = "_01_T22DiaFestivo"
        Public Const Campo_01_T22Activo As String = "_01_T22Activo"


        Public Const CamposTabla As String = "_01_T22Fecha,_01_T22FechaNumero,_01_T22Dia,_01_T22Mes,_01_T22Agno,_01_T22NombreLargo,_01_T22NombreDia,_01_T22NombreMEs,_01_T22DiaFestivo,_01_T22Activo"
        Public Const CamposActualizar As String = "_01_T22FechaNumero,_01_T22Dia,_01_T22Mes,_01_T22Agno,_01_T22NombreLargo,_01_T22NombreDia,_01_T22NombreMEs,_01_T22DiaFestivo,_01_T22Activo"
    End Structure

    Structure _01_T23APROBACIONTIEMPO
        Public Const NombreTabla As String = "_01_T23APROBACIONTIEMPO"
        Public Const CodigoModulo As String = "_01_T23"
        Public Const CampoLlave_01_T23NumDocAprobador As String = "_01_T23NumDocAprobador"
        Public Const CampoLlave_01_T23Disciplina As String = "_01_T23Disciplina"
        Public Const CampoLlave_01_T23Fecha As String = "_01_T23Fecha"
        Public Const CampoLlave_01_T23Consecutivo As String = "_01_T23Consecutivo"
        Public Const Campo_01_T23Codigo As String = "_01_T23Codigo"
        Public Const Campo_01_T23Cliente As String = "_01_T23Cliente"
        Public Const Campo_01_T23Contrato As String = "_01_T23Contrato"
        Public Const Campo_01_T23ODS As String = "_01_T23ODS"
        Public Const CampoLlave_01_T23Gestion As String = "_01_T23Gestion"
        Public Const Campo_01_T23Observaciones As String = "_01_T23Observaciones"
        Public Const Campo_01_T23Hora As String = "_01_T23Hora"

        Public Const CamposTabla As String = "_01_T23NumDocAprobador,_01_T23Disciplina,_01_T23Fecha,_01_T23Consecutivo,_01_T23Codigo,_01_T23Cliente,_01_T23Contrato,_01_T23ODS,_01_T23Gestion,_01_T23Observaciones,_01_T23Hora"
        Public Const CamposActualizar As String = "_01_T23Codigo,_01_T23Cliente,_01_T23Contrato,_01_T23ODS,_01_T23Observaciones,_01_T23Hora"
    End Structure

    Structure _01_T24CARGOS
        Public Const NombreTabla As String = "_01_T24CARGOS"
        Public Const CodigoModulo As String = "_01_T24"
        Public Const CampoLlave_01_T24Codigo As String = "_01_T24Codigo"
        Public Const Campo_01_T24Nombre As String = "_01_T24Nombre"
        Public Const Campo_01_T24Activo As String = "_01_T24Activo"

        Public Const CamposTabla As String = "_01_T24Codigo,_01_T24Nombre,_01_T24Activo"
        Public Const CamposActualizar As String = "_01_T24Nombre,_01_T24Activo"
    End Structure

    Structure _01_T25SEDES
        Public Const NombreTabla As String = "_01_T25SEDES"
        Public Const CodigoModulo As String = "_01_T25"
        Public Const CampoLlave_01_T25Codigo As String = "_01_T25Codigo"
        Public Const Campo_01_T25Nombre As String = "_01_T25Nombre"
        Public Const Campo_01_T25Activo As String = "_01_T25Activo"

        Public Const CamposTabla As String = "_01_T25Codigo,_01_T25Nombre,_01_T25Activo"
        Public Const CamposActualizar As String = "_01_T25Nombre,_01_T25Activo"
    End Structure

    Structure _01_T26TURNOSTRABAJO
        Public Const NombreTabla As String = "_01_T26TURNOSTRABAJO"
        Public Const CodigoModulo As String = "_01_T26"
        Public Const CampoLlave_01_T26Codigo As String = "_01_T26Codigo"
        Public Const Campo_01_T26Nombre As String = "_01_T26Nombre"
        Public Const Campo_01_T26Activo As String = "_01_T26Activo"

        Public Const CamposTabla As String = "_01_T26Codigo,_01_T26Nombre,_01_T26Activo"
        Public Const CamposActualizar As String = "_01_T26Nombre,_01_T26Activo"
    End Structure

    Structure _01_T27TIPOSCONTRATOS
        Public Const NombreTabla As String = "_01_T27TIPOSCONTRATOS"
        Public Const CodigoModulo As String = "_01_T27"
        Public Const CampoLlave_01_T27Codigo As String = "_01_T27Codigo"
        Public Const Campo_01_T27Nombre As String = "_01_T27Nombre"
        Public Const Campo_01_T27Activo As String = "_01_T27Activo"

        Public Const CamposTabla As String = "_01_T27Codigo,_01_T27Nombre,_01_T27Activo"
        Public Const CamposActualizar As String = "_01_T27Nombre,_01_T27Activo"
    End Structure

    Structure _01_T28TIPONOVEDADES
        Public Const NombreTabla As String = "_01_T28TIPONOVEDADES"
        Public Const CodigoModulo As String = "_01_T28"
        Public Const CampoLlave_01_T28Codigo As String = "_01_T28Codigo"
        Public Const Campo_01_T28Nombre As String = "_01_T28Nombre"
        Public Const Campo_01_T28Activo As String = "_01_T28Activo"
        Public Const Campo_01_T28DiasActivo As String = "_01_T28DiasActivo"

        Public Const CamposTabla As String = "_01_T28Codigo,_01_T28Nombre,_01_T28Activo,_01_T28DiasActivo"
        Public Const CamposActualizar As String = "_01_T28Nombre,_01_T28Activo"
    End Structure

    Structure _01_T29NOVEDADES
        Public Const NombreTabla As String = "_01_T29NOVEDADES"
        Public Const CodigoModulo As String = "_01_T29"
        Public Const CampoLlave_01_T29Contrato As String = "_01_T29Contrato"
        Public Const CampoLlave_01_T29Cliente As String = "_01_T29Cliente"
        Public Const CampoLlave_01_T29ODS As String = "_01_T29ODS"
        Public Const CampoLlave_01_T29Disciplina As String = "_01_T29Disciplina"
        Public Const CampoLlave_01_T29NumDocCoordinador As String = "_01_T29NumDocCoordinador"
        Public Const CampoLlave_01_T29Consecutivo As String = "_01_T29Consecutivo"
        Public Const Campo_01_T29FechaRegistro As String = "_01_T29FechaRegistro"
        Public Const Campo_01_T29FechaInicial As String = "_01_T29FechaInicial"
        Public Const Campo_01_T29FechaFinal As String = "_01_T29FechaFinal"
        Public Const Campo_01_T29Observaciones As String = "_01_T29Observaciones"
        Public Const Campo_01_T29Activo As String = "_01_T29Activo"
        Public Const CampoLlave_01_T29NumDocFuncionario As String = "_01_T29NumDocFuncionario"
        Public Const CampoLlave_01_T29TipoNovedad As String = "_01_T29TipoNovedad"
        Public Const Campo_01_T29IdNovedad As String = "_01_T29IdNovedad"
        Public Const Campo_01_T29IdVigencia As String = "_01_T29IdVigencia"
        Public Const Campo_01_T29IdConsecutivo As String = "_01_T29IdConsecutivo"

        Public Const CamposTabla As String = "_01_T29Contrato,_01_T29Cliente,_01_T29ODS,_01_T29Disciplina,_01_T29NumDocCoordinador,_01_T29Consecutivo,_01_T29FechaRegistro,_01_T29FechaInicial,_01_T29FechaFinal,_01_T29Observaciones,_01_T29Activo,_01_T29NumDocFuncionario,_01_T29TipoNovedad,_01_T29IdNovedad,_01_T29IdVigencia,_01_T29IdConsecutivo"
        Public Const CamposActualizar As String = "_01_T29FechaRegistro,_01_T29FechaInicial,_01_T29FechaFinal,_01_T29Observaciones,_01_T29Activo"
    End Structure


    Structure _01_T30TIPOFECHAESPECIAL
        Public Const NombreTabla As String = "_01_T30TIPOFECHAESPECIAL"
        Public Const CodigoModulo As String = "_01_T30"
        Public Const CampoLlave_01_T30Codigo As String = "_01_T30Codigo"
        Public Const Campo_01_T30Nombre As String = "_01_T30Nombre"
        Public Const Campo_01_T30Activo As String = "_01_T30Activo"

        Public Const CamposTabla As String = "_01_T30Codigo,_01_T30Nombre,_01_T30Activo"
        Public Const CamposActualizar As String = "_01_T30Nombre,_01_T30Activo"
    End Structure

    Structure _01_T31FECHASESPECIALES
        Public Const NombreTabla As String = "_01_T31FECHASESPECIALES"
        Public Const CodigoModulo As String = "_01_T31"
        Public Const CampoLlave_01_T31TipoFechaEspecial As String = "_01_T31TipoFechaEspecial"
        Public Const CampoLlave_01_T31Fecha As String = "_01_T31Fecha"
        Public Const Campo_01_T31Activo As String = "_01_T31Activo"

        Public Const CamposTabla As String = "_01_T31TipoFechaEspecial,_01_T31Fecha,_01_T31Activo"
        Public Const CamposActualizar As String = "_01_T31Activo"
    End Structure

    Structure _01_T32ESCOLARIDAD
        Public Const NombreTabla As String = "_01_T32ESCOLARIDAD"
        Public Const CodigoModulo As String = "_01_T32"
        Public Const CampoLlave_01_T32Codigo As String = "_01_T32Codigo"
        Public Const Campo_01_T32Nombre As String = "_01_T32Nombre"
        Public Const Campo_01_T32Activo As String = "_01_T32Activo"

        Public Const CamposTabla As String = "_01_T32Codigo,_01_T32Nombre,_01_T32Activo"
        Public Const CamposActualizar As String = "_01_T32Nombre,_01_T32Activo"
    End Structure

    Structure _01_T33APROBADORESTIEMPO
        Public Const NombreTabla As String = "_01_T33APROBADORESTIEMPO"
        Public Const CodigoModulo As String = "_01_T33"
        Public Const CampoLlave_01_T33DocumentoLider As String = "_01_T33DocumentoLider"
        Public Const CampoLlave_01_T33DocumentoFncionario As String = "_01_T33DocumentoFuncionario"
        Public Const Campo_01_T33Disciplina As String = "_01_T33Disciplina"
        Public Const Campo_01_T33Observaciones As String = "_01_T33Observaciones"
        Public Const Campo_01_T33Activo As String = "_01_T33Activo"

        Public Const CamposTabla As String = "_01_T33DocumentoLider,_01_T33DocumentoFuncionario,_01_T33Disciplina,_01_T33Observaciones,_01_T33Activo"
        Public Const CamposActualizar As String = "_01_T33Disciplina,_01_T33Observaciones,_01_T33Activo"
    End Structure

    Structure _01_T34RECURSOSxAPROBADORES
        Public Const NombreTabla As String = "_01_T34RECURSOSxAPROBADORES"
        Public Const CodigoModulo As String = "_01_T34"
        Public Const CampoLlave_01_T34DocumentoAprobador As String = "_01_T34DocumentoAprobador"
        Public Const CampoLlave_01_T34Cliente As String = "_01_T34Cliente"
        Public Const CampoLlave_01_T34Contrato As String = "_01_T34Contrato"
        Public Const CampoLlave_01_T34ODS As String = "_01_T34ODS"
        Public Const CampoLlave_01_T34Recurso As String = "_01_T34NumDocRecurso"
        Public Const Campo_01_T34Disciplina As String = "_01_T34Disciplina"
        Public Const Campo_01_T34Activo As String = "_01_T34Activo"
        Public Const CampoLlave_01_T34NumDocJefe As String = "_01_T34NumDocJefe"

        Public Const CamposTabla As String = "_01_T34DocumentoAprobador,_01_T34Cliente,_01_T34Contrato,_01_T34ODS,_01_T34NumDocRecurso,_01_T34Disciplina,_01_T34Activo,_01_T34NumDocJefe"
        Public Const CamposActualizar As String = "_01_T34Disciplina,_01_T34Activo"
    End Structure

    Structure _01_T35CONFIRMACIONTIEMPO
        Public Const NombreTabla As String = "_01_T35CONFIRMACIONTIEMPO"
        Public Const CodigoModulo As String = "_01_T35"
        Public Const CampoLlave_01_T35NumDocAprobador As String = "_01_T35NumDocAprobador"
        Public Const CampoLlave_01_T35Disciplina As String = "_01_T35Disciplina"
        Public Const CampoLlave_01_T35Fecha As String = "_01_T35Fecha"
        Public Const CampoLlave_01_T35Consecutivo As String = "_01_T35Consecutivo"
        Public Const Campo_01_T35Codigo As String = "_01_T35Codigo"
        Public Const Campo_01_T35Cliente As String = "_01_T35Cliente"
        Public Const Campo_01_T35Contrato As String = "_01_T35Contrato"
        Public Const Campo_01_T35ODS As String = "_01_T35ODS"
        Public Const CampoLlave_01_T35Gestion As String = "_01_T35Gestion"
        Public Const Campo_01_T35Observaciones As String = "_01_T35Observaciones"
        Public Const Campo_01_T35Hora As String = "_01_T35Hora"

        Public Const CamposTabla As String = "_01_T35NumDocAprobador,_01_T35Disciplina,_01_T35Fecha,_01_T35Consecutivo,_01_T35Codigo,_01_T35Cliente,_01_T35Contrato,_01_T35ODS,_01_T35Gestion,_01_T35Observaciones,_01_T35Hora"
        Public Const CamposActualizar As String = "_01_T35Codigo,_01_T35Cliente,_01_T35Contrato,_01_T35ODS,_01_T35Observaciones,_01_T35Hora"
    End Structure

    Structure _01_T36SSESIONUSUARIO
        Public Const NombreTabla As String = "_01_T36SSESIONUSUARIO"
        Public Const CodigoModulo As String = "_01_T36"
        Public Const CampoLlave_01_T36Aplicacion As String = "_01_T36Aplicacion"
        Public Const CampoLlave_01_T36DocFuncionario As String = "_01_T36DocFuncionario"
        Public Const CampoLlave_01_T36EmailCorporativo As String = "_01_T36EmailCorporativo"
        Public Const Campo_01_T36Fecha As String = "_01_T36Fecha"
        Public Const Campo_01_T36Hora As String = "_01_T36Hora"
        Public Const Campo_01_T36PerfilAdmin As String = "_01_T36PerfilAdmin"
        Public Const Campo_01_T36PerfilLider As String = "_01_T36PerfilLider"
        Public Const Campo_01_T36PerfilCoordinador As String = "_01_T36PerfilCoordinador"
        Public Const Campo_01_T36PerfilAprobador As String = "_01_T36PerfilAprobador"
        Public Const Campo_01_T36PerfilFuncionario As String = "_01_T36PerfilFuncionario"
        Public Const Campo_01_T36PerfilRecursosHumanos As String = "_01_T36PerfilRecursosHumanos"
        Public Const Campo_01_T36PerfilControlProyectos As String = "_01_T36PerfilControlProyectos"
        Public Const Campo_01_T36PerfilControlDocumentos As String = "_01_T36PerfilControlDocumentos"
        'Public Const Campo_01_T36PerfilFuncionario As String = "_01_T36PerfilFuncionario"

        Public Const CamposTabla As String = "_01_T36Aplicacion,_01_T36DocFuncionario,_01_T36EmailCorporativo,_01_T36Fecha,_01_T36Hora,_01_T36PerfilAdmin,_01_T36PerfilLider,_01_T36PerfilCoordinador,_01_T36PerfilAprobador,_01_T36PerfilFuncionario,_01_T36PerfilRecursosHumanos,_01_T36PerfilControlProyectos,_01_T36PerfilControlDocumentos"
        Public Const CamposActualizar As String = "_01_T36Fecha,_01_T36Hora,_01_T36PerfilAdmin,_01_T36PerfilLider,_01_T36PerfilCoordinador,_01_T36PerfilAprobador,_01_T36PerfilFuncionario"
    End Structure

    Structure _01_T37TIPOACTIVIDADES
        Public Const NombreTabla As String = "_01_T37TIPOACTIVIDADES"
        Public Const CodigoModulo As String = "_01_T37"
        Public Const CampoLlave_01_T37Codigo As String = "_01_T37Codigo"
        Public Const Campo_01_T37Nombre As String = "_01_T37Nombre"
        Public Const Campo_01_T37Activo As String = "_01_T37Activo"

        Public Const CamposTabla As String = "_01_T37Codigo,_01_T37Nombre,_01_T37Activo"
        Public Const CamposActualizar As String = "_01_T37Nombre,_01_T37Activo"
    End Structure


    Structure _01_T38ACTIVIDADES
        Public Const NombreTabla As String = "_01_T38ACTIVIDADES"
        Public Const CodigoModulo As String = "_01_T38"
        Public Const CampoLlave_01_T38TipoActividada As String = "_01_T38TipoActividada"
        Public Const CampoLlave_01_T38CodigoActividada As String = "_01_T38CodigoActividada"
        Public Const Campo_01_T38Descripcion As String = "_01_T38Descripcion"
        Public Const Campo_01_T38ID As String = "_01_T38ID"
        Public Const Campo_01_T38Activo As String = "_01_T38Activo"

        Public Const CamposTabla As String = "_01_T38TipoActividada,_01_T38CodigoActividada,_01_T38Descripcion,_01_T38ID,_01_T38Activo"
        Public Const CamposActualizar As String = "_01_T38Descripcion,_01_T38ID"
    End Structure

    Structure _01_T40DESBLOQUEO
        Public Const NombreTabla As String = "_01_T40DESBLOQUEO"
        Public Const CodigoModulo As String = "_01_T40"
        Public Const CampoLlave_01_T40Codigo As String = "_01_T40Codigo"
        Public Const CampoLlave_01_T40Fecha As String = "_01_T40Fecha"
        Public Const Campo_01_T40DocFuncionario As String = "_01_T40DocFuncionario"
        Public Const Campo_01_T40Activo As String = "_01_T40Activo"

        Public Const CamposTabla As String = "_01_T40Codigo,_01_T40Fecha,_01_T40DocFuncionario,_01_T40Activo"
        Public Const CamposActualizar As String = "_01_T40DocFuncionario,_01_T40Activo"
    End Structure

    Structure _01_T41ONTIMEESCRITORIO
        Public Const NombreTabla As String = "_01_T41ONTIMEESCRITORIO"
        Public Const CodigoModulo As String = "_01_T41"
        Public Const CampoLlave_01_T41FechaInicio As String = "_01_T41FechaInicio"
        Public Const Campo_01_T41Intervalo As String = "_01_T41Intervalo"
        Public Const Campo_01_T41TiempoAumento As String = "_01_T41TiempoAumento"
        Public Const Campo_01_T41TActivo As String = "_01_T41TActivo"
        Public Const Campo_01_T41IntervaloPopupHoras As String = "_01_T41IntervaloPopupHoras"
        Public Const Campo_01_T41Horas_Aumento As String = "_01_T41Horas_Aumento"
        Public Const Campo_01_T41IntervaloAprobar As String = "_01_T41IntervaloAprobar"
        Public Const Campo_01_T41Aprobar_Aumento As String = "_01_T41Aprobar_Aumento"
        Public Const Campo_01_T41IntervaloConfirmar As String = "_01_T41IntervaloConfirmar"
        Public Const Campo_01_T41Confirmar_Aumento As String = "_01_T41Confirmar_Aumento"


        Public Const CamposTabla As String = "_01_T41FechaInicio,_01_T41Intervalo,_01_T41TiempoAumento,_01_T41TActivo,_01_T41IntervaloPopupHoras,_01_T41Horas_Aumento,_01_T41IntervaloAprobar,_01_T41Aprobar_Aumento,_01_T41IntervaloConfirmar,_01_T41Confirmar_Aumento"
        Public Const CamposActualizar As String = "_01_T41Intervalo,_01_T41TiempoAumento,_01_T41TActivo,_01_T41IntervaloPopup"
    End Structure

    Structure _01_T41ONTIMEESCRITORIO_SINBLOQUEO
        Public Const NombreTabla As String = "_01_T41ONTIMEESCRITORIO_SINBLOQUEO"
        Public Const CodigoModulo As String = "_01_T41"
        Public Const CampoLlaveNumDocFuncionario As String = "_01_T41NumDocFuncionario"

        Public Const CamposTabla As String = "_01_T41NumDocFuncionario"
        Public Const CamposActualizar As String = "_01_T41Intervalo,_01_T41TiempoAumento,_01_T41TActivo,_01_T41IntervaloPopup"
    End Structure

    '*************** comienza HSE  ***********************************

    Structure _02_T02TIPO_ACCION
        Public Const NombreTabla As String = "_02_T02TIPO_ACCION"
        Public Const CodigoModulo As String = "_02_T02"
        Public Const CampoLlave_02_T02Id As String = "_02_T02Id"
        Public Const Campo_02_T02Nombre As String = "_02_T02Nombre"
        Public Const Campo_02_T02Activo As String = "_02_T02Activo"

        Public Const CamposTabla As String = "_02_T02Id,_02_T02Nombre,_02_T02Activo"
        Public Const CamposActualizar As String = "_02_T02Nombre,_02_T02Activo"
    End Structure


    Structure _02_T01REPORTE_CASOS
        Public Const NombreTabla As String = "_02_T01REPORTE_CASOS"
        Public Const CodigoModulo As String = "_02_T01"
        Public Const CampoLlave_02_T01Id_Reporte As String = "_02_T01Id_Reporte"
        Public Const Campo_02_T01FechaRegistro As String = "_02_T01FechaRegistro"
        Public Const Campo_02_T01DocUsuarioRegistra As String = "_02_T01DocUsuarioRegistra"
        Public Const Campo_02_T01Fecha As String = "_02_T01Fecha"
        Public Const Campo_02_T01Hora As String = "_02_T01Hora"
        Public Const Campo_02_T01Id_TipoAccion As String = "_02_T01Id_TipoAccion"
        Public Const Campo_02_T01LugarEvento As String = "_02_T01LugarEvento"
        Public Const Campo_02_T01DescripcionEvento As String = "_02_T01DescripcionEvento"
        Public Const Campo_02_T01SugerenciaEvento As String = "_02_T01SugerenciaEvento"
        Public Const Campo_02_T01Id_Area As String = "_02_T01Id_Area"
        Public Const Campo_02_T01Id_Sede As String = "_02_T01Id_Sede"
        Public Const Campo_02_T01Dependenia As String = "_02_T01Dependenia"
        Public Const Campo_02_T01PersonaReporta_Nombre As String = "_02_T01PersonaReporta_Nombre"
        Public Const Campo_02_T01PersonaReporta_Contacto As String = "_02_T01PersonaReporta_Contacto"
        Public Const CamposTabla As String = "_02_T01Id_Reporte,_02_T01FechaRegistro,_02_T01DocUsuarioRegistra,_02_T01Fecha,_02_T01Hora,_02_T01Id_TipoAccion,_02_T01LugarEvento,_02_T01DescripcionEvento,_02_T01SugerenciaEvento,_02_T01Id_Area,_02_T01Id_Sede,_02_T01Dependenia,_02_T01PersonaReporta_Nombre,_02_T01PersonaReporta_Contacto"
        Public Const CamposActualizar As String = "_02_T01FechaRegistro,_02_T01DocUsuarioRegistra,_02_T01Fecha,_02_T01Hora,_02_T01Id_TipoAccion,_02_T01LugarEvento,_02_T01DescripcionEvento,_02_T01SugerenciaEvento,_02_T01Id_Area,_02_T01Id_Sede,_02_T01Dependenia,_02_T01PersonaReporta_Nombre,_02_T01PersonaReporta_Contacto"
    End Structure

    Structure _02_T03NFORME_HSE_PROYECTOS
        Public Const NombreTabla As String = "_02_T03NFORME_HSE_PROYECTOS"
        Public Const CodigoModulo As String = "_02_T03"
        Public Const CampoLlave_02_T03Id_ReporteCasos As String = "_02_T03Id_ReporteCasos"
        Public Const CampoLlave_02_T03Id_Informe As String = "_02_T03Id_Informe"
        Public Const Campo_02_T03FechaRegistro As String = "_02_T03FechaRegistro"
        Public Const Campo_02_T03usuarioRegistra As String = "_02_T03usuarioRegistra"
        Public Const Campo_02_T03NumCtoMarco As String = "_02_T03NumCtoMarco"
        Public Const Campo_02_T03Mes As String = "_02_T03Mes"
        Public Const Campo_02_T03FechaSemana_Inicio As String = "_02_T03FechaSemana_Inicio"
        Public Const Campo_02_T03FechaSemana_Final As String = "_02_T03FechaSemana_Final"
        Public Const Campo_02_T03Depto As String = "_02_T03Depto"
        Public Const Campo_02_T03Municipio As String = "_02_T03Municipio"
        Public Const Campo_02_T03ODS As String = "_02_T03ODS"
        Public Const Campo_02_T03AreaCliente As String = "_02_T03AreaCliente"
        Public Const Campo_02_T03ActividadesEjecutadas As String = "_02_T03ActividadesEjecutadas"
        Public Const Campo_02_T03Id_EstadisticaDiaria As String = "_02_T03Id_EstadisticaDiaria"
        Public Const Campo_02_T03Id_Personal As String = "_02_T03Id_Personal"
        Public Const Campo_02_T03Id_SeguridadVial As String = "_02_T03Id_SeguridadVial"
        Public Const Campo_02_T03Id_RegistroFotografico As String = "_02_T03Id_RegistroFotografico"
        Public Const Campo_02_T03ResponsableInforme As String = "_02_T03ResponsableInforme"
        Public Const Campo_02_T03ComentariosFinales As String = "_02_T03ComentariosFinales"

        Public Const CamposTabla As String = "_02_T03Id_ReporteCasos,_02_T03Id_Informe,_02_T03FechaRegistro,_02_T03usuarioRegistra,_02_T03NumCtoMarco,_02_T03Mes,_02_T03FechaSemana_Inicio,_02_T03FechaSemana_Final,_02_T03Depto,_02_T03Municipio,_02_T03ODS,_02_T03AreaCliente,_02_T03ActividadesEjecutadas,_02_T03Id_EstadisticaDiaria,_02_T03Id_Personal,_02_T03Id_SeguridadVial,_02_T03Id_RegistroFotografico,_02_T03ResponsableInforme,_02_T03ComentariosFinales"
        Public Const CamposActualizar As String = "_02_T03FechaRegistro,_02_T03usuarioRegistra,_02_T03NumCtoMarco,_02_T03Mes,_02_T03FechaSemana_Inicio,_02_T03FechaSemana_Final,_02_T03Depto,_02_T03Municipio,_02_T03ODS,_02_T03AreaCliente,_02_T03ActividadesEjecutadas,_02_T03Id_EstadisticaDiaria,_02_T03Id_Personal,_02_T03Id_SeguridadVial,_02_T03Id_RegistroFotografico,_02_T03ResponsableInforme,_02_T03ComentariosFinales"
    End Structure
    Structure _02_T21SEDE
        Public Const NombreTabla As String = "_02_T21SEDE"
        Public Const CodigoModulo As String = "_02_T21"
        Public Const CampoLlave_02_T21Codigo As String = "_02_T21Codigo"
        Public Const Campo_02_T21Nombre As String = "_02_T21Nombre"
        Public Const Campo_02_T21Activo As String = "_02_T21Activo"

        Public Const CamposTabla As String = "_02_T21Codigo,_02_T21Nombre,_02_T21Activo"
        Public Const CamposActualizar As String = "_02_T21Nombre,_02_T21Activo"
    End Structure

    Structure _02_T20AREA
        Public Const NombreTabla As String = "_02_T20AREA"
        Public Const CodigoModulo As String = "_02_T20"
        Public Const CampoLlave_02_T20Codigo As String = "_02_T20Codigo"
        Public Const Campo_02_T20Nombre As String = "_02_T20Nombre"
        Public Const Campo_02_T20Activo As String = "_02_T20Activo"
        Public Const CamposTabla As String = "_02_T20Codigo,_02_T20Nombre,_02_T20Activo"
        Public Const CamposActualizar As String = "_02_T20Nombre,_02_T20Activo"
    End Structure

    Structure _02_T19TIPOVEHICULO
        Public Const NombreTabla As String = "_02_T19TIPOVEHICULO"
        Public Const CodigoModulo As String = "_02_T19"
        Public Const CampoLlave_02_T19Codigo As String = "_02_T19Codigo"
        Public Const Campo_02_T19Nombre As String = "_02_T19Nombre"
        Public Const Campo_02_T19Activo As String = "_02_T19Activo"

        Public Const CamposTabla As String = "_02_T19Codigo,_02_T19Nombre,_02_T19Activo"
        Public Const CamposActualizar As String = "_02_T19Nombre,_02_T19Activo"
    End Structure

    Structure _02_T04ESTADISTICADIARIA
        Public Const NombreTabla As String = "_02_T04ESTADISTICADIARIA"
        Public Const CodigoModulo As String = "_02_T04"
        Public Const CampoLlave_02_T04Id_Estadistica As String = "_02_T04Id_Estadistica"
        Public Const CampoLlave_02_T04Id_Item As String = "_02_T04Id_Item"
        Public Const Campo_02_T04Lunes As String = "_02_T04Lunes"
        Public Const Campo_02_T04Martes As String = "_02_T04Martes"
        Public Const Campo_02_T04Miercoles As String = "_02_T04Miercoles"
        Public Const Campo_02_T04Jueves As String = "_02_T04Jueves"
        Public Const Campo_02_T04Viernes As String = "_02_T04Viernes"
        Public Const Campo_02_T04Sabado As String = "_02_T04Sabado"
        Public Const Campo_02_T04Domingo As String = "_02_T04Domingo"
        Public Const Campo_02_T04AcomuladoSemanal As String = "_02_T04AcomuladoSemanal"

        Public Const CamposTabla As String = "_02_T04Id_Estadistica,_02_T04Id_Item,_02_T04Lunes,_02_T04Martes,_02_T04Miercoles,_02_T04Jueves,_02_T04Viernes,_02_T04Sabado,_02_T04Domingo,_02_T04AcomuladoSemanal"
        Public Const CamposActualizar As String = "_02_T04Lunes,_02_T04Martes,_02_T04Miercoles,_02_T04Jueves,_02_T04Viernes,_02_T04Sabado,_02_T04Domingo,_02_T04AcomuladoSemanal"
    End Structure
    Structure _02_T05PERSONAL
        Public Const NombreTabla As String = "_02_T05PERSONAL"
        Public Const CodigoModulo As String = "_02_T05"
        Public Const CampoLlave_02_T05Id_Estadistica As String = "_02_T05Id_Estadistica"
        Public Const CampoLlave_02_T05Id_Personal As String = "_02_T05Id_Personal"
        Public Const Campo_02_T05NombresApellidos As String = "_02_T05NombresApellidos"
        Public Const Campo_02_T05Identificacion As String = "_02_T05Identificacion"
        Public Const Campo_02_T05ICargo As String = "_02_T05ICargo"
        Public Const Campo_02_T05IEmpresa As String = "_02_T05IEmpresa"

        Public Const CamposTabla As String = "_02_T05Id_Estadistica,_02_T05Id_Personal,_02_T05NombresApellidos,_02_T05Identificacion,_02_T05ICargo,_02_T05IEmpresa"
        Public Const CamposActualizar As String = "_02_T05NombresApellidos,_02_T05Identificacion,_02_T05ICargo,_02_T05IEmpresa"
    End Structure

    Structure _02_T05_1PERSONAL_EXTERNO
        Public Const NombreTabla As String = "_02_T05_1PERSONAL_EXTERNO"
        Public Const CodigoModulo As String = "_02_T05_1"
        Public Const CampoLlave_02_T05_1Id_Estadistica As String = "_02_T05_1Id_Estadistica"
        Public Const CampoLlave_02_T05_1Id_Personal As String = "_02_T05_1Id_Personal"
        Public Const Campo_02_T05_1NombresApellidos As String = "_02_T05_1NombresApellidos"
        Public Const Campo_02_T05_1Identificacion As String = "_02_T05_1Identificacion"
        Public Const Campo_02_T05_1ICargo As String = "_02_T05_1ICargo"
        Public Const Campo_02_T05_1IEmpresa As String = "_02_T05_1IEmpresa"

        Public Const CamposTabla As String = "_02_T05_1Id_Estadistica,_02_T05_1Id_Personal,_02_T05_1NombresApellidos,_02_T05_1Identificacion,_02_T05_1ICargo,_02_T05_1IEmpresa"
        Public Const CamposActualizar As String = "_02_T05_1NombresApellidos,_02_T05_1Identificacion,_02_T05_1ICargo,_02_T05_1IEmpresa"
    End Structure

    Structure _02_T06SEGURIDADVIAL
        Public Const NombreTabla As String = "_02_T06SEGURIDADVIAL"
        Public Const CodigoModulo As String = "_02_T06"
        Public Const CampoLlave_02_T06Id_Estadistica As String = "_02_T06Id_Estadistica"
        Public Const CampoLlave_02_T06Id_SeguridadVial As String = "_02_T06Id_SeguridadVial"
        Public Const Campo_02_T06TipoVehiculo As String = "_02_T06TipoVehiculo"
        Public Const Campo_02_T06PlacaVehiculo As String = "_02_T06PlacaVehiculo"
        Public Const Campo_02_T06KmInicial As String = "_02_T06KmInicial"
        Public Const Campo_02_T06KmFinal As String = "_02_T06KmFinal"
        Public Const Campo_02_T06TotalKm As String = "_02_T06TotalKm"
        Public Const Campo_02_T06EmpresaAffiliacionVehiculo As String = "_02_T06EmpresaAffiliacionVehiculo"
        Public Const Campo_02_T06FUEC As String = "_02_T06FUEC"
        Public Const Campo_02_T06NombreConductor As String = "_02_T06NombreConductor"
        Public Const Campo_02_T06DocumentoConductor As String = "_02_T06DocumentoConductor"

        Public Const CamposTabla As String = "_02_T06Id_Estadistica,_02_T06Id_SeguridadVial,_02_T06TipoVehiculo,_02_T06PlacaVehiculo,_02_T06KmInicial,_02_T06KmFinal,_02_T06TotalKm,_02_T06EmpresaAffiliacionVehiculo,_02_T06FUEC,_02_T06NombreConductor,_02_T06DocumentoConductor"
        Public Const CamposActualizar As String = "_02_T06TipoVehiculo,_02_T06PlacaVehiculo,_02_T06KmInicial,_02_T06KmFinal,_02_T06TotalKm,_02_T06EmpresaAffiliacionVehiculo,_02_T06FUEC,_02_T06NombreConductor,_02_T06DocumentoConductor"
    End Structure


    Structure _02_T07REGISTROfOTOGRAFICO
        Public Const NombreTabla As String = "_02_T07REGISTROfOTOGRAFICO"
        Public Const CodigoModulo As String = "_02_T07"
        Public Const CampoLlave_02_T07Id_Estadistica As String = "_02_T07Id_Estadistica"
        Public Const CampoLlave_02_T07Id_registroFotografico As String = "_02_T07Id_registroFotografico"
        Public Const CampoLlave_02_T07Id_BD As String = "_02_T07Id_BD"
        Public Const Campo_02_T07Id_BD_Item As String = "_02_T07Id_BD_Item"
        Public Const Campo_02_T07Fecha As String = "_02_T07Fecha"
        Public Const Campo_02_T07Observaciones As String = "_02_T07Observaciones"
        Public Const Campo_02_T07NombreFoto As String = "_02_T07NombreFoto"
        Public Const Campo_02_T07ExtensionFoto As String = "_02_T07ExtensionFoto"
        Public Const Campo_02_T07imagenFoto As String = "_02_T07imagenFoto"
        Public Const Campo_02_T07ContentType As String = "_02_T07ContentType"


        Public Const CamposTabla As String = "_02_T07Id_Estadistica,_02_T07Id_registroFotografico,_02_T07Id_BD,_02_T07Id_BD_Item,_02_T07Fecha,_02_T07Observaciones,_02_T07NombreFoto,_02_T07ExtensionFoto,_02_T07imagenFoto,_02_T07ContentType"
        Public Const CamposActualizar As String = "_02_T07Id_BD_Item,_02_T07Fecha,_02_T07Observaciones"
    End Structure

    Structure _04_T11ATL_PROCESO
        Public Const NombreTabla As String = "_04_T11ATL_PROCESO"
        Public Const CodigoModulo As String = "_04_T11"
        Public Const CampoLlave_04_T11Codigo As String = "_04_T11Codigo"
        Public Const Campo_04_T11Nombre As String = "_04_T11Nombre"
        Public Const Campo_04_T11Activo As String = "_04_T11Activo"

        Public Const CamposTabla As String = "_04_T11Codigo,_04_T11Nombre,_04_T11Activo"
        Public Const CamposActualizar As String = "_04_T11Nombre,_04_T11Activo"
    End Structure

    Structure _04_T13ATL_ORIGENMEJORA
        Public Const NombreTabla As String = "_04_T13ATL_ORIGENMEJORA"
        Public Const CodigoModulo As String = "_04_T13"
        Public Const CampoLlave_04_T13Codigo As String = "_04_T13Codigo"
        Public Const Campo_04_T13Nombre As String = "_04_T13Nombre"
        Public Const Campo_04_T13Activo As String = "_04_T13Activo"

        Public Const CamposTabla As String = "_04_T13Codigo,_04_T13Nombre,_04_T13Activo"
        Public Const CamposActualizar As String = "_04_T13Nombre,_04_T13Activo"
    End Structure


    Structure _B01_T12ATL_TIPOACCION
        Public Const NombreTabla As String = "_B01_T12ATL_TIPOACCION"
        Public Const CodigoModulo As String = "_B01_T12"
        Public Const CampoLlave_B01_T12Codigo As String = "_B01_T12Codigo"
        Public Const Campo_B01_T12Nombre As String = "_B01_T12Nombre"
        Public Const Campo_B01_T12Activo As String = "_B01_T12Activo"

        Public Const CamposTabla As String = "_B01_T12Codigo,_B01_T12Nombre,_B01_T12Activo"
        Public Const CamposActualizar As String = "_B01_T12Nombre,_B01_T12Activo"
    End Structure


    Structure _04_T14ATL_RESPONSABLE_ACCION
        Public Const NombreTabla As String = "_04_T14ATL_RESPONSABLE_ACCION"
        Public Const CodigoModulo As String = "_04_T14"
        Public Const CampoLlave_04_T14Codigo As String = "_04_T14Codigo"
        Public Const Campo_04_T14Nombre As String = "_04_T14Nombre"
        Public Const Campo_04_T14Activo As String = "_04_T14Activo"

        Public Const CamposTabla As String = "_04_T14Codigo,_04_T14Nombre,_04_T14Activo"
        Public Const CamposActualizar As String = "_04_T14Nombre,_04_T14Activo"
    End Structure

    Structure _04_T10ATL
        Public Const NombreTabla As String = "_04_T10ATL"
        Public Const CodigoModulo As String = "_04_T10"
        Public Const CampoLlave_04_T10Proceso As String = "_04_T10Proceso"
        Public Const CampoLlave_04_T10Consecutivo As String = "_04_T10Consecutivo"
        Public Const Campo_04_T10Fecha As String = "_04_T10Fecha"
        Public Const Campo_04_T10TipoAccion As String = "_04_T10TipoAccion"
        Public Const Campo_04_T10OrigenMejora As String = "_04_T10OrigenMejora"
        Public Const Campo_04_T10DescripcionHallazgo As String = "_04_T10DescripcionHallazgo"
        Public Const Campo_04_T10REsponsableAccion As String = "_04_T10REsponsableAccion"
        Public Const Campo_04_T10Causas As String = "_04_T10Causas"
        Public Const Campo_04_T10AccionesCorrectivas As String = "_04_T10AccionesCorrectivas"
        Public Const Campo_04_T10AccionesPreventivas As String = "_04_T10AccionesPreventivas"
        Public Const Campo_04_T10FechaInicio As String = "_04_T10FechaInicio"
        Public Const Campo_04_T10FechaCierre As String = "_04_T10FechaCierre"
        Public Const Campo_04_T10FechaVerificacion As String = "_04_T10FechaVerificacion"
        Public Const Campo_04_T10SeCierraAccion As String = "_04_T10SeCierraAccion"
        Public Const Campo_04_T10FechaRealCierre As String = "_04_T10FechaRealCierre"
        Public Const Campo_04_T10DiasAtraso As String = "_04_T10DiasAtraso"
        Public Const Campo_04_T10Estado As String = "_04_T10Estado"
        Public Const Campo_04_T10Observaciones As String = "_04_T10Observaciones"
        Public Const Campo_04_T10FechaRegistro As String = "_04_T10FechaRegistro"
        Public Const Campo_04_T10NumDocReggistra As String = "_04_T10NumDocReggistra"

        Public Const CamposTabla As String = "_04_T10Proceso,_04_T10Consecutivo,_04_T10Fecha,_04_T10TipoAccion,_04_T10OrigenMejora,_04_T10DescripcionHallazgo,_04_T10REsponsableAccion,_04_T10Causas,_04_T10AccionesCorrectivas,_04_T10AccionesPreventivas,_04_T10FechaInicio,_04_T10FechaCierre,_04_T10FechaVerificacion,_04_T10SeCierraAccion,_04_T10FechaRealCierre,_04_T10DiasAtraso,_04_T10Estado,_04_T10Observaciones,_04_T10FechaRegistro,_04_T10NumDocReggistra"
        Public Const CamposActualizar As String = "_04_T10Fecha,_04_T10TipoAccion,_04_T10OrigenMejora,_04_T10DescripcionHallazgo,_04_T10REsponsableAccion,_04_T10Causas,_04_T10AccionesCorrectivas,_04_T10AccionesPreventivas,_04_T10FechaInicio,_04_T10FechaCierre,_04_T10FechaVerificacion,_04_T10SeCierraAccion,_04_T10FechaRealCierre,_04_T10DiasAtraso,_04_T10Estado,_04_T10Observaciones,_04_T10FechaRegistro,_04_T10NumDocReggistra"
    End Structure


    Structure _02_T17ITEMESTADISTICADIARIA
        Public Const NombreTabla As String = "_02_T17ITEMESTADISTICADIARIA"
        Public Const CodigoModulo As String = "_02_T17"
        Public Const CampoLlave_02_T17Codigo As String = "_02_T17Codigo"
        Public Const Campo_02_T17Nombre As String = "_02_T17Nombre"
        Public Const CampoLlave_02_T17Activo As String = "_02_T17Activo"

        Public Const CamposTabla As String = "_02_T17Codigo,_02_T17Nombre,_02_T17Activo"
        Public Const CamposActualizar As String = "_02_T17Nombre"
    End Structure


    Structure _02_T15INFORMEMENSUAL
        Public Const NombreTabla As String = "_02_T15INFORMEMENSUAL"
        Public Const CodigoModulo As String = "_02_T15"
        Public Const CampoLlave_02_T15Vigencia As String = "_02_T15Vigencia"
        Public Const CampoLlave_02_T15Mes As String = "_02_T15Mes"
        Public Const CampoLlave_02_T15ODS As String = "_02_T15ODS"
        Public Const CampoLlave_02_T15AreaEcopetrolResponsable As String = "_02_T15AreaEcopetrolResponsable"
        Public Const CampoLlave_02_T15AreaEcopetrolHUB As String = "_02_T15AreaEcopetrolHUB"
        Public Const Campo_02_T15Departamento As String = "_02_T15Departamento"
        Public Const Campo_02_T15NumeroTrabajadores As String = "_02_T15NumeroTrabajadores"
        Public Const Campo_02_T15HTT As String = "_02_T15HTT"
        Public Const Campo_02_T15HH As String = "_02_T15HH"
        Public Const Campo_02_T15NumIncidentesOcupacionales As String = "_02_T15NumIncidentesOcupacionales"
        Public Const Campo_02_T15NumIncidentesAmbientales As String = "_02_T15NumIncidentesAmbientales"
        Public Const Campo_02_T15NumIncidentesVehiculares As String = "_02_T15NumIncidentesVehiculares"
        Public Const Campo_02_T15NumIncidentesSeguridadProceso As String = "_02_T15NumIncidentesSeguridadProceso"
        Public Const Campo_02_T15AvancePlanHSE As String = "_02_T15AvancePlanHSE"
        Public Const Campo_02_T15AseguramientoComportamiento As String = "_02_T15AseguramientoComportamiento"
        Public Const Campo_02_T15KMRecorridos As String = "_02_T15KMRecorridos"
        Public Const Campo_02_T15CantidadVehiculos As String = "_02_T15CantidadVehiculos"
        Public Const Campo_02_T15NumCasosEnfermedadDiagnosticada As String = "_02_T15NumCasosEnfermedadDiagnosticada"
        Public Const Campo_02_T15PrevalenciEnfLaboral As String = "_02_T15PrevalenciEnfLaboral"
        Public Const Campo_02_T15IncidenciaEnfLaboral As String = "_02_T15IncidenciaEnfLaboral"
        Public Const Campo_02_T15Ausentismo As String = "_02_T15Ausentismo"
        Public Const Campo_02_T15FechaRegistro As String = "_02_T15FechaRegistro"
        Public Const Campo_02_T15NumDocFuncionarioRegistra As String = "_02_T15NumDocFuncionarioRegistra"
        Public Const Campo_02_T15Estado As String = "_02_T15Estado"
        Public Const CamposTabla As String = "_02_T15Vigencia,_02_T15Mes,_02_T15ODS,_02_T15AreaEcopetrolResponsable,_02_T15AreaEcopetrolHUB,_02_T15Departamento,_02_T15NumeroTrabajadores,_02_T15HTT,_02_T15HH,_02_T15NumIncidentesOcupacionales,_02_T15NumIncidentesAmbientales,_02_T15NumIncidentesVehiculares,_02_T15NumIncidentesSeguridadProceso,_02_T15AvancePlanHSE,_02_T15AseguramientoComportamiento,_02_T15KMRecorridos,_02_T15CantidadVehiculos,_02_T15NumCasosEnfermedadDiagnosticada,_02_T15PrevalenciEnfLaboral,_02_T15IncidenciaEnfLaboral,_02_T15Ausentismo,_02_T15FechaRegistro,_02_T15NumDocFuncionarioRegistra,_02_T15Estado"
        Public Const CamposActualizar As String = "_02_T15Departamento,_02_T15NumeroTrabajadores,_02_T15HTT,_02_T15HH,_02_T15NumIncidentesOcupacionales,_02_T15NumIncidentesAmbientales,_02_T15NumIncidentesVehiculares,_02_T15NumIncidentesSeguridadProceso,_02_T15AvancePlanHSE,_02_T15Aseguramiento Comportamiento,_02_T15KMRecorridos,_02_T15CantidadVehiculos,_02_T15NumCasosEnfermedadDiagnosticada,_02_T15PrevalenciEnfLaboral,_02_T15IncidenciaEnfLaboral,_02_T15Ausentismo,_02_T15FechaRegistro,_02_T15NumDocFuncionarioRegistra,_02_T15Estado"
    End Structure



    Structure _02_T16HUB
        Public Const NombreTabla As String = "_02_T16HUB"
        Public Const CodigoModulo As String = "_02_T16"
        Public Const CampoLlave_02_T16Codigo As String = "_02_T16Codigo"
        Public Const Campo_02_T16Nombre As String = "_02_T16Nombre"
        Public Const Campo_02_T16Activo As String = "_02_T16Activo"
        Public Const CamposTabla As String = "_02_T16Codigo,_02_T16Nombre,_02_T16Activo"
        Public Const CamposActualizar As String = "_02_T16Nombre,_02_T16Activo"
    End Structure

    '*************** TERMINA HSE  ***********************************

    '*************** COMIENZA Mecanica ***********************************
    Structure _01_T02MECANICA
        Public Const NombreTabla As String = "_01_T02MECANICA"
        Public Const CodigoModulo As String = "_01_T02"
        Public Const CampoLlave_01_T02Cliente As String = "_01_T02Cliente"
        Public Const Campo_01_T02ContratoODS As String = "_01_T02ContratoODS"
        Public Const Campo_01_T02Fecha As String = "_01_T02Fecha"
        Public Const Campo_01_T02Diseñador As String = "_01_T02Diseñador"
        Public Const Campo_01_T02Localizacion As String = "_01_T02Localizacion"
        Public Const Campo_01_T02TAG As String = "_01_T02TAG"
        Public Const Campo_01_T02Tipotrampa As String = "_01_T02Tipotrampa"
        Public Const Campo_01_T02Tamañotrampa As String = "_01_T02Tamañotrampa"
        Public Const Campo_01_T02Rating As String = "_01_T02Rating"
        Public Const Campo_01_T02Material As String = "_01_T02Material"
        Public Const Campo_01_T02CODdiseño As String = "_01_T02CODdiseño"
        Public Const Campo_01_T02Temperatura As String = "_01_T02Temperatura"
        Public Const Campo_01_T02FactorDiseño As String = "_01_T02FactorDiseño"
        Public Const Campo_01_T02SobreCorrosion As String = "_01_T02SobreCorrosion"
        Public Const Campo_01_T02EficienciaJunta As String = "_01_T02EficienciaJunta"
        Public Const Campo_01_T02EespesorbarrilMayor As String = "_01_T02EespesorbarrilMayor"
        Public Const Campo_01_T02EespesorbarrilMenor As String = "_01_T02EespesorbarrilMenor"
        Public Const Campo_01_T02Coeficienteexp As String = "_01_T02Coeficienteexp"
        Public Const Campo_01_T02Elasticidad As String = "_01_T02Elasticidad"
        Public Const Campo_01_T02RelacionPoisson As String = "_01_T02RelacionPoisson"
        Public Const Campo_01_T02resisfluencia As String = "_01_T02resisfluencia"
        Public Const Campo_01_T02resistension As String = "_01_T02resistension"
        Public Const Campo_01_T02presion As String = "_01_T02presion"
        Public Const Campo_01_T02diametrolinea As String = "_01_T02diametrolinea"
        Public Const Campo_01_T02diametrobarril As String = "_01_T02diametrobarril"
        Public Const Campo_01_T02diametropateo As String = "_01_T02diametropateo"
        Public Const Campo_01_T02diametroexternoBmayor As String = "_01_T02diametroexternoBmayor"
        Public Const Campo_01_T02diametroexternoBmenor As String = "_01_T02diametroexternoBmenor"
        Public Const Campo_01_T02esfuerzomaterial As String = "_01_T02esfuerzomaterial"
        Public Const Campo_01_T02TempInterpolar1 As String = "_01_T02TempInterpolar1"
        Public Const Campo_01_T02TempInterpolar2 As String = "_01_T02TempInterpolar2"
        Public Const Campo_01_T02Presioninterpolar1 As String = "_01_T02Presioninterpolar1"
        Public Const Campo_01_T02Presioninterpolar2 As String = "_01_T02Presioninterpolar2"
        Public Const Campo_01_T02Presioninterpolada As String = "_01_T02PresioninterpoladaFinal"
        Public Const Campo_01_T02Espesorbarrilmayor As String = "_01_T02Espesordiseñobarrilmayor"
        Public Const Campo_01_T02Espesorbarrilmenor As String = "_01_T02Espesordiseñobarrilmenor"
        Public Const Campo_01_T02Espesorminimotoleranciamayor As String = "_01_T02Espesorminimotoleranciamayor"
        Public Const Campo_01_T02Espesorminimotoleranciamenor As String = "_01_T02Espesorminimotoleranciamenor"
        Public Const Campo_01_T02Chequeobarrilmenor As String = "_01_T02Chequeobarrilmenor"
        Public Const Campo_01_T02Chequeobarrilmayor As String = "_01_T02Chequeobarrilmayor"
        Public Const Campo_01_T02Esfuerzopresiondiseño1 As String = "_01_T02Esfuerzopresiondiseño1"
        Public Const Campo_01_T02Esfuerzopresiondiseño2 As String = "_01_T02Esfuerzopresiondiseño2"
        Public Const Campo_01_T02Presionprueba As String = "_01_T02Presionprueba"
        Public Const Campo_01_T02Duracionprueba As String = "_01_T02Duracionprueba"
        Public Const Campo_01_T02Esfuerzocircunferencialbarrilmayor As String = "_01_T02Esfuerzocircunferencialbarrilmayor "
        Public Const Campo_01_T02Esfuerzocircunferencialbarrilmenor As String = "_01_T02Esfuerzocircunferencialbarrilmenor "


        Public Const CamposTabla As String = "_01_T02Cliente,_01_T02ContratoODS,_01_T02Fecha,_01_T02Diseñador,_01_T02Localizacion,_01_T02TAG,_01_T02Tipotrampa,_01_T02Tamañotrampa,_01_T02Rating,_01_T02Material,_01_T02CODdiseño,_01_T02Temperatura,_01_T02FactorDiseño,_01_T02SobreCorrosion,_01_T02EficienciaJunta,_01_T02EespesorbarrilMayor,_01_T02EespesorbarrilMenor,_01_T02Coeficienteexp,_01_T02Elasticidad,_01_T02RelacionPoisson,_01_T02resisfluencia,_01_T02resistension,_01_T02presion,_01_T02diametrolinea,_01_T02diametrobarril,_01_T02diametropateo,_01_T02diametroexternoBmayor,_01_T02diametroexternoBmenor,_01_T02esfuerzomaterial,_01_T02TempInterpolar1,_01_T02TempInterpolar2,_01_T02Presioninterpolar1,_01_T02Presioninterpolar2,_01_T02PresioninterpoladaFinal,_01_T02Espesordiseñobarrilmayor,_01_T02Espesordiseñobarrilmenor,_01_T02Espesorminimotoleranciamayor,_01_T02Espesorminimotoleranciamenor,_01_T02Chequeobarrilmenor,_01_T02Chequeobarrilmayor,_01_T02Esfuerzopresiondiseño1,_01_T02Esfuerzopresiondiseño2,_01_T02Presionprueba,_01_T02Duracionprueba,_01_T02Esfuerzocircunferencialbarrilmayor,_01_T02Esfuerzocircunferencialbarrilmenor"
        Public Const CamposActualizar As String = "_01_T02Cliente,_01_T02ContratoODS,_01_T02Fecha,_01_T02Diseñador,_01_T02Localizacion,_01_T02TAG,_01_T02Tipotrampa,_01_T02Tamañotrampa,_01_T02Rating,_01_T02Material,_01_T02CODdiseño,_01_T02Temperatura,_01_T02FactorDiseño,_01_T02SobreCorrosion,_01_T02EficienciaJunta,_01_T02EespesorbarrilMayor,_01_T02EespesorbarrilMenor,_01_T02Coeficienteexp,_01_T02Elasticidad,_01_T02RelacionPoisson,_01_T02resisfluencia,_01_T02resistension,_01_T02presion,_01_T02diametrolinea,_01_T02diametrobarril,_01_T02diametropateo,_01_T02diametroexternoBmayor,_01_T02diametroexternoBmenor,_01_T02esfuerzomaterial,_01_T02TempInterpolar1,_01_T02TempInterpolar2,_01_T02Presioninterpolar1,_01_T02Presioninterpolar2,_01_T02PresioninterpoladaFinal,_01_T02Espesordiseñobarrilmayor,_01_T02Espesordiseñobarrilmenor,_01_T02Espesorminimotoleranciamayor,_01_T02Espesorminimotoleranciamenor,_01_T02Chequeobarrilmenor,_01_T02Chequeobarrilmayor,_01_T02Esfuerzopresiondiseño1,_01_T02Esfuerzopresiondiseño2,_01_T02Presionprueba,_01_T02Duracionprueba,_01_T02Esfuerzocircunferencialbarrilmayor,_01_T02Esfuerzocircunferencialbarrilmenor"
    End Structure
    '*************** TERMINA Mecanica  ***********************************

    '*************** COMIENZA BASICOS ***********************************
    Structure _99_T00REGISTROPROCESOS
        Public Const NombreTabla As String = "_99_T00REGISTROPROCESOS"
        Public Const CodigoModulo As String = "_99_T00"
        Public Const CampoLlave_99_T00Consecutivo As String = "_99_T00Consecutivo"
        Public Const Campo_99_T00NumDocFuncionarioRegistra As String = "_99_T00NumDocFuncionarioRegistra"
        Public Const Campo_99_T00NombreFuncionarioRegistra As String = "_99_T00NombreFuncionarioRegistra"
        Public Const Campo_99_T00FechaHoraCompleta As String = "_99_T00FechaHoraCompleta"
        Public Const Campo_99_T00Fecha As String = "_99_T00Fecha"
        Public Const Campo_99_T00Hora As String = "_99_T00Hora"
        Public Const Campo_99_T00CodigoModulo As String = "_99_T00CodigoModulo"
        Public Const Campo_99_T00PermisoEjecutado As String = "_99_T00PermisoEjecutado"
        Public Const Campo_99_T00QueryEjecutado As String = "_99_T00QueryEjecutado"

        Public Const CamposTabla As String = "_99_T00Consecutivo,_99_T00NumDocFuncionarioRegistra,_99_T00NombreFuncionarioRegistra,_99_T00FechaHoraCompleta,_99_T00Fecha,_99_T00Hora,_99_T00CodigoModulo,_99_T00PermisoEjecutado,_99_T00QueryEjecutado"
        Public Const CamposActualizar As String = "_99_T00NumDocFuncionarioRegistra,_99_T00NombreFuncionarioRegistra,_99_T00FechaHoraCompleta,_99_T00Fecha,_99_T00Hora,_99_T00CodigoModulo,_99_T00PermisoEjecutado,_99_T00QueryEjecutado"
    End Structure

    Structure _99_T01PERFILES
        Public Const NombreTabla As String = "_99_T01PERFILES"
        Public Const CodigoModulo As String = "_99_T01"
        Public Const CampoLlave_99_T01Codigo As String = "_99_T01Codigo"
        Public Const Campo_99_T01Nombre As String = "_99_T01Nombre"
        Public Const Campo_99_T01MenuSuperior As String = "_99_T01MenuSuperior"
        Public Const Campo_99_T01MenuIzquierdo As String = "_99_T01MenuIzquierdo"
        Public Const Campo_99_T01IconosPlataforma As String = "_99_T01IconosPlataforma"
        Public Const Campo_99_T01MenuAdicional1 As String = "_99_T01MenuAdicional1"
        Public Const Campo_99_T01MenuAdicional2 As String = "_99_T01MenuAdicional2"
        Public Const Campo_99_T01Activo As String = "_99_T01Activo"

        Public Const CamposTabla As String = "_99_T01Codigo,_99_T01Nombre,_99_T01MenuSuperior,_99_T01MenuIzquierdo,_99_T01IconosPlataforma,_99_T01MenuAdicional1,_99_T01MenuAdicional2,_99_T01Activo"
        Public Const CamposActualizar As String = "_99_T01Nombre,_99_T01MenuSuperior,_99_T01MenuIzquierdo,_99_T01IconosPlataforma,_99_T01MenuAdicional1,_99_T01MenuAdicional2,_99_T01Activo"
    End Structure

    Structure _99_T02PERFILxFUNCIONARIOxMODxPERMISO
        Public Const NombreTabla As String = "_99_T02PERFILxFUNCIONARIOxMODxPERMISO"
        Public Const CodigoModulo As String = "_99_T02"
        Public Const CampoLlave_99_T02CodigoPerfil As String = "_99_T02CodigoPerfil"
        Public Const CampoLlave_99_T02NumDocFuncionario As String = "_99_T02NumDocFuncionario"
        Public Const CampoLlave_99_T02CodigoModulo As String = "_99_T02CodigoModulo"
        Public Const CampoLlave_99_T02CodigoPermiso As String = "_99_T02CodigoPermiso"

        Public Const CamposTabla As String = "_99_T02CodigoPerfil,_99_T02NumDocFuncionario,_99_T02CodigoModulo,_99_T02CodigoPermiso"
        Public Const CamposActualizar As String = ""
    End Structure

    Structure _99_T03MODULOS
        Public Const NombreTabla As String = "_99_T03MODULOS"
        Public Const CodigoModulo As String = "_99_T03"
        Public Const CampoLlave_99_T03Codigo As String = "_99_T03Codigo"
        Public Const Campo_99_T03Nombre As String = "_99_T03Nombre"
        Public Const Campo_99_T03Activo As String = "_99_T03Activo"

        Public Const CamposTabla As String = "_99_T03Codigo,_99_T03Nombre,_99_T03Activo"
        Public Const CamposActualizar As String = "_99_T03Nombre,_99_T03Activo"
    End Structure

    Structure _99_T04PERFILxMODULOS
        Public Const NombreTabla As String = "_99_T04PERFILxMODULOS"
        Public Const CodigoModulo As String = "_99_T04"
        Public Const CampoLlave_99_T04CodigoPerfil As String = "_99_T04CodigoPerfil"
        Public Const CampoLlave_99_T04CodigoModulo As String = "_99_T04CodigoModulo"

        Public Const CamposTabla As String = "_99_T04CodigoPerfil,_99_T04CodigoModulo"
        Public Const CamposActualizar As String = ""
    End Structure

    Structure _99_T05PERMISOS
        Public Const NombreTabla As String = "_99_T05PERMISOS"
        Public Const CodigoModulo As String = "_99_T05"
        Public Const CampoLlave_99_T05Codigo As String = "_99_T05Codigo"
        Public Const Campo_99_T05Nombre As String = "_99_T05Nombre"
        Public Const Campo_99_T05Activo As String = "_99_T05Activo"

        Public Const CamposTabla As String = "_99_T05Codigo,_99_T05Nombre,_99_T05Activo"
        Public Const CamposActualizar As String = "_99_T05Nombre,_99_T05Activo"
    End Structure

    Structure _99_T06FUNCIONARIOS
        Public Const NombreTabla As String = "_99_T06FUNCIONARIOS"
        Public Const CodigoModulo As String = "_99_T06"
        Public Const Campo_99_T06TipoDocIdentidad As String = "_99_T06TipoDocIdentidad"
        Public Const CampoLlave_99_T06NumDocIdentidad As String = "_99_T06NumDocIdentidad"
        Public Const Campo_99_T06NombreApeliidos As String = "_99_T06NombreApeliidos"
        Public Const Campo_99_T06Cargo As String = "_99_T06Cargo"
        Public Const Campo_99_T06Disciplina As String = "_99_T06Disciplina"
        Public Const Campo_99_T06Area As String = "_99_T06Area"

        Public Const CamposTabla As String = "_99_T06TipoDocIdentidad,_99_T06NumDocIdentidad,_99_T06NombreApeliidos,_99_T06Cargo,_99_T06Disciplina,_99_T06Area"
        Public Const CamposActualizar As String = "_99_T06TipoDocIdentidad,_99_T06NombreApeliidos,_99_T06Cargo,_99_T06Disciplina,_99_T06Area"
    End Structure

    Structure _99_T07VIGENCIA
        Public Const NombreTabla As String = "_99_T07VIGENCIA"
        Public Const CodigoModulo As String = "_99_T07"
        Public Const CampoLlave_99_T07Fecha As String = "_99_T07Fecha"
        Public Const CampoLlave_99_T07FechaNumero As String = "_99_T07FechaNumero"
        Public Const Campo_99_T07Dia As String = "_99_T07Dia"
        Public Const Campo_99_T07Mes As String = "_99_T07Mes"
        Public Const Campo_99_T07Agno As String = "_99_T07Agno"
        Public Const Campo_99_T07NombreLargo As String = "_99_T07NombreLargo"
        Public Const Campo_99_T07NombreDia As String = "_99_T07NombreDia"
        Public Const Campo_99_T07NombreMes As String = "_99_T07NombreMes"
        Public Const Campo_99_T07Festivo As String = "_99_T07Festivo"
        Public Const Campo_99_T07Activo As String = "_99_T07Activo"

        Public Const CamposTabla As String = "_99_T07Fecha,_99_T07FechaNumero,_99_T07Dia,_99_T07Mes,_99_T07Agno,_99_T07NombreLargo,_99_T07NombreDia,_99_T07NombreMes,_99_T07Festivo,_99_T07Activo"
        Public Const CamposActualizar As String = "_99_T07Dia,_99_T07Mes,_99_T07Agno,_99_T07NombreLargo,_99_T07NombreDia,_99_T07NombreMes,_99_T07Festivo,_99_T07Activo"
    End Structure

    Structure _99_T08PAIS
        Public Const NombreTabla As String = "_99_T08PAIS"
        Public Const CodigoModulo As String = "_99_T08"
        Public Const CampoLlave_99_T08Codigo As String = "_99_T08Codigo"
        Public Const Campo_99_T08Nombre As String = "_99_T08Nombre"
        Public Const Campo_99_T08Activo As String = "_99_T08Activo"

        Public Const CamposTabla As String = "_99_T08Codigo,_99_T08Nombre,_99_T08Activo"
        Public Const CamposActualizar As String = "_99_T08Nombre,_99_T08Activo"
    End Structure


    Structure _99_T10MUNICIPIOS
        Public Const NombreTabla As String = "_99_T10MUNICIPIOS"
        Public Const CodigoModulo As String = "_99_T10"
        Public Const CampoLlave_99_T10CodPais As String = "_99_T10CodPais"
        Public Const CampoLlave_99_T10CodDepto As String = "_99_T10CodDepto"
        Public Const CampoLlave_99_T10Codigo As String = "_99_T10Codigo"
        Public Const Campo_99_T10Nombre As String = "_99_T10Nombre"
        Public Const Campo_99_T10Activo As String = "_99_T10Activo"

        Public Const CamposTabla As String = "_99_T10CodPais,_99_T10CodDepto,_99_T10Codigo,_99_T10Nombre,_99_T10Activo"
        Public Const CamposActualizar As String = "_99_T10Nombre,_99_T10Activo"
    End Structure


    Structure _99_T09DEPARTAMENTOS
        Public Const NombreTabla As String = "_99_T09DEPARTAMENTOS"
        Public Const CodigoModulo As String = "_99_T09"
        Public Const CampoLlave_99_T09CodPais As String = "_99_T09CodPais"
        Public Const CampoLlave_99_T09Codigo As String = "_99_T09Codigo"
        Public Const Campo_99_T09Nombre As String = "_99_T09Nombre"
        Public Const Campo_99_T09Activo As String = "_99_T09Activo"

        Public Const CamposTabla As String = "_99_T09CodPais,_99_T09Codigo,_99_T09Nombre,_99_T09Activo"
        Public Const CamposActualizar As String = "_99_T09Nombre,_99_T09Activo"
    End Structure



    '*************** TERMINA BASICOS ***********************************


End Module

Public Class RecibeElement
    Private _NumeroDocumento As String
    Private _Fecha As String
    Private _Imagen As String

    Public Property NumeroDocumento As String
        Get
            Return _NumeroDocumento
        End Get
        Set(ByVal value As String)
            _NumeroDocumento = value
        End Set
    End Property


    Public Property Fecha As String
        Get
            Return _Fecha
        End Get
        Set(ByVal value As String)
            _Fecha = value
        End Set
    End Property


    Public Property Imagen As String
        Get
            Return _Imagen
        End Get
        Set(ByVal value As String)
            _Imagen = value
        End Set
    End Property
End Class
