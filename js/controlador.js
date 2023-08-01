(function ($) {

    if (window.location.hash) {
        var hash = window.location.hash.substring(1); 
        if (hash == "backup") {
            $.ajax({
                url: "index.html",
                async: true,
                success: function (data) {
                    $("#btnBackup").click();
                    window.close();
                }
            });
        }
    }
    /*
    if (window.location.hostname == "localhost") {
        $("#btnGuardar").css('display', 'none');
        $("#btnGuardar2").css('display', 'none');
        $("#btnEliminar").css('display', 'none');
        $("#btnEliminar2").css('display', 'none');
        $("#btnFirmar").css('display', 'none');
        $("#btnFirmar2").css('display', 'none');
        $("#btnUtilidades").css('display', 'none');
        $("#btnAdjuntarArchivo").css('display', 'none');
        $("#btnCartaNoPos").css('display', 'none');
      
        $("#2").css('display', 'none');
        $("#").css('display', 'none');
        $("#2").css('display', 'none');
        


        
    }
    */

    $('#btnUtilidadesLimpiar').removeAttr('disabled');
    //$("#drop_04_T01TipoDocPaciente option[value='RC']").attr("selected", true);
    //$("#txt_04_T01NumDocPaciente").val("12345656");
    //$("#txt_04_T01PrimerNombre").val("a");
    //$("#txt_04_T01SegundoNombre").val("b");
    //$("#txt_04_T01PrimerApellido").val("c");
    //$("#txt_04_T01SegundoApellido").val("d");
    //$("#txt_04_T01FechaNacimiento").val("20/06/2013");
    //$("#txt_04_T01Direccion").val("f");
    //$("#txt_04_T01Telefono").val("g");
    //$("#txt_04_T22NombrePadre").val("h");
    //$("#txt_04_T22EdadPadre").val("i");
    //$("#txt_04_T22ProfesionPadre").val("j");
    //$("#txt_04_T22NombreMadre").val("k");
    //$("#txt_04_T22EdadMadre").val("l");
    //$("#txt_04_T22ProfesionMadre").val("m");
    //$("#drop_04_T01Sexo option[value='M']").attr("selected", true);
    //$("#drop_04_T01Sexo").trigger("liszt:updated");
    //$("#drop_04_T01Escolaridad option[value='01']").attr("selected", true);
    //$("#drop_04_T01Escolaridad").trigger("liszt:updated");
    //$("#drop_04_T01EstadoCivil option[value='01']").attr("selected", true);
    //$("#drop_04_T01EstadoCivil").trigger("liszt:updated");
    //$("#drop_04_T01TipoRegimen option[value='1']").attr("selected", true);
    //$("#drop_04_T01TipoRegimen").trigger("liszt:updated");
    //$("#drop_04_T01CodigoEmpresaLabora option[value='8600788287']").attr("selected", true);
    //$("#drop_04_T01CodigoEmpresaLabora").trigger("liszt:updated");
    //$("#drop_04_T01DeptoMunicipio option[value='25-320']").attr("selected", true);
    //$("#drop_04_T01DeptoMunicipio").trigger("liszt:updated");
    //$("#drop_04_T01TipoZonaResidencia option[value='R']").attr("selected", true);
    //$("#drop_04_T01TipoZonaResidencia").trigger("liszt:updated");
    //$("#drop_04_T01AmbitoRealizacionProced option[value='1']").attr("selected", true);
    //$("#drop_04_T01AmbitoRealizacionProced").trigger("liszt:updated");
    //$("#drop_04_T01FinalidadConsulta option[value='10']").attr("selected", true);
    //$("#drop_04_T01FinalidadConsulta").trigger("liszt:updated");
    //$("#drop_04_T01CausaExterna option[value='13']").attr("selected", true);
    //$("#drop_04_T01CausaExterna").trigger("liszt:updated");
    //$("#drop_04_T02TipoDiagPrincipal option[value='1']").attr("selected", true);
    //$("#drop_04_T02TipoDiagPrincipal").trigger("liszt:updated");
    //$("#drop_04_T01FinalidadProcedimiento option[value='1']").attr("selected", true);
    //$("#drop_04_T01FinalidadProcedimiento").trigger("liszt:updated");
    //$("#txt_04_T01CodigoCUPSConsulta").val("871062 RADIOGRAFIA PANORAMICA DE COLUMNA (GONIOMETRIA U ORTOGRAMA) FORMATO 14 X 17 (NIÑOS) +");
    //$("#txt_04_T02CodigoDiagPrincipal").val("A014 FIEBRE PARATIFOIDEA, NO ESPECIFICADA");
    //$("#txt_04_T01MotivoConsulta").val("MOTIVO");
    //$("#txt_04_T01Antecedentes").val("ANTECEDENTES");
    //$("#txt_04_T01ExamenFisico").val("EXAMEN FISICO");
    //$("#txt_04_T01PlanManejo").val("PLAN MANEJO");
    //$("#txt_04_T01Peso").val("11.8");
    //$("#txt_04_T01Estatura").val("80");
    //$("#txt_04_T01IMC").val("18.43");
    //$("#txt_04_T01EnfActual").val("ENFERMEDAD");
    //$("#txt_04_T22Analisis").val("ANALISIS");
    //$("#txt_04_T22FrecuenciaCardiaca").val("110");
    //$("#txt_04_T22FrecuenciaRespitaratoria").val("28");
    //$("#txt_04_T22Temperatura").val("36");
    //$("#txt_04_T22PerimetrroCefalico").val("48");
    //$("#txt_04_T77FechaInicioIncapacidad").val("19/06/2015");
    //$("#txt_04_T77FechaTerminacionIncapacidad").val("26/06/2015");

    restaFechas = function (f1, f2) {
        var aFecha1 = f1.split('/');
        var aFecha2 = f2.split('/');
        var fFecha1 = Date.UTC(aFecha1[2], aFecha1[1] - 1, aFecha1[0]);
        var fFecha2 = Date.UTC(aFecha2[2], aFecha2[1] - 1, aFecha2[0]);
        var dif = fFecha2 - fFecha1;
        var dias = Math.floor(dif / (1000 * 60 * 60 * 24));
        return dias;
    }
    
    //$("#txt_04_T77FechaInicioIncapacidad, #txt_04_T77FechaTerminacionIncapacidad").on("blur", function () {
    //    calcularDiasIncapacidad();
    //});

    regexDateValidator = function (fecha) {
        return (fecha).match(/([0-9]{2})\/([0-9]{2})\/([0-9]{4})/);
    }

    if ($("#rdb_Tipo_Busqueda").length > 0) {
        
        $("#rdb_Tipo_Busqueda > tbody > tr > td:nth-child(1) > label").attr('style', 'margin-left:-15px !important');
        $("#rdb_Tipo_Busqueda > tbody > tr > td:nth-child(1) > label").attr('style','margin-right:40px !important');
        $("#rdb_Tipo_Busqueda > tbody > tr > td:nth-child(2) > label").attr('style', 'margin-left:-15px !important');
        $("#rdb_Tipo_Busqueda > tbody > tr > td:nth-child(2) > label").attr('style', 'margin-right:10px !important');



    }

    if ($("#txt_04_T22NombrePadre").length > 0 && $("#txt_04_T01NumDocPaciente").val() == "") {
        $("#drop_04_T01EstadoCivil option[value='01']").attr("selected", true);
        $("#drop_04_T01EstadoCivil").trigger("liszt:updated");
    }

    if ($("#drop_04_T01AmbitoRealizacionProced").length > 0) {
        $("#drop_04_T01AmbitoRealizacionProced option[value='1']").attr("selected", true);
        $("#drop_04_T01AmbitoRealizacionProced").trigger("liszt:updated");
    }

    if ($("#drop_04_T01CodigoEmpresaLabora").length > 0) {
        $("#drop_04_T01CodigoEmpresaLabora option[value='01']").attr("selected", true);
        $("#drop_04_T01CodigoEmpresaLabora").trigger("liszt:updated");
    }

    if ($("#drop_HistorialPio").length > 0) {
        /* 
        $("#drop_HistorialPio").attr("multiple", "multiple");
        $("#drop_HistorialPio").attr("size", "3");
        */
    }

    if ($("#menu_componentes").length > 0) {
        switch($('#menu_componentes').val()) {
            case 'GUILLERMO.CAMPO':
                $("#consultaPediatrica").css('display', 'block');
                $("#reporteHistorias").css('display', 'none');
                $("#consultaanexodocumentos").css('display', 'block');
                break;
            case 'JOSE.PARDO':
            case 'JUAN.GUTIERREZ':
            case 'sandra.cespedes':
            case 'SHIRLEY.CELIS':
            case 'YABA':
            case 'LEIDY.LUCUMI':
                $("#agendaMedica").css('display', 'block');
                $("#consultaGinecobstetrica").css('display', 'block');
                $("#reporteHistorias").css('display', 'none');
                $("#reporteRips").css('display', 'none');
                $("#consultaanexodocumentos").css('display', 'block');
                break;
            case 'IVONNE.ALDANA':
            case 'LEIDY.ALONSO':
            case 'PEDRO.PULIDO':
                $("#agendaMedica").css('display', 'block');
                $("#consultaOtorrino").css('display', 'block');
                $("#citasMedicas").css('display', 'block');
                $("#consultaanexodocumentos").css('display', 'block');
                break;
            case 'ALBERTO.MUÑOZ':
            case 'ERNESTO.RUIZ':
                $("#agendaMedica").css('display', 'block');
                $("#consultaOtorrino").css('display', 'block');
                $("#citasMedicas").css('display', 'block');
                $("#entidadFacturacion").css('display', 'block');
                $("#consultaanexodocumentos").css('display', 'block');
                break;
            case 'ANGELA.DIAZ':
            case 'JAVIER.LOPEZ':
            case 'YONEL.JAIMES':
                $("#agendaMedica").css('display', 'block');
                $("#consulta_oftalmologia").css('display', 'block');
                break;
            case 'YOHANA.RENDON':
            case 'SANDRA.GARCIA':
                $("#agendaMedica").css('display', 'block');
                $("#consulta_Optometria").css('display', 'block');
                break;
            case 'KILZA.VARILA':
                $("#reporteHistorias").css('display', 'none');
                $("#reporteRips").css('display', 'none');
                break;
            case 'IVONNE.ALDANA.CAMPO':
                $("#consultaOtorrino").css('display', 'block');
                break;
            case 'gasan57':
                $("#consulta_Ortopedia").css('display', 'block');
                //$("#consulta_Fisiatria").css('display', 'block');
                //$("#consulta_MedicinaLaboral").css('display', 'block');
                break;
            case 'TAZA':
                $("#agendaMedica").css('display', 'block');
                $("#consultaGeneral").css('display', 'block');
                break;
            case 'MARIA.FUERTE':
                $("#agendaMedica").css('display', 'block');
                $("#consultaGeneral").css('display', 'block');
                $("#consultaCardiologia").css('display', 'block');
                $("#consultaPediatrica").css('display', 'block');
                $("#consultanotasEnfermeria").css('display', 'block');
                
                $("#labelLaboratorios").css('display', 'block');
                $("#laboratorioexamenOrdenado").css('display', 'block');
                $("#Laboratorios").css('display', 'block');
                $("#labelOcupacional").css('display', 'block');
                $("#consultasaludOcupacional").css('display', 'block');
                $("#consultasaludocupacional_Auxiliar").css('display', 'block');
                $("#consultaanexodocumentos").css('display', 'block');
                             
                break;
            case 'TATIANA.RENTERIA':
                $("#agendaMedica").css('display', 'block');
                $("#consultaterapiafisicaAdultos").css('display', 'block');
                break;
            case 'DIANA.GUTIERREZ':
                $("#agendaMedica").css('display', 'block');
                $("#consultaterapiafisicaAdultos").css('display', 'block');
                break;
            case 'ALEJANDRA.ALVAREZ':
                $("#agendaMedica").css('display', 'block');
                $("#consultaterapiaOcupacional").css('display', 'block');
                break;
            case 'wendy.rojas':
                $("#agendaMedica").css('display', 'block');
                $("#consultaterapiaRespiratoria").css('display', 'block');
                break;
            case 'MIGUEL.PARRADO':
                $("#agendaMedica").css('display', 'block');
                $("#consultaUrologia").css('display', 'block');
                break;
            case 'MARCELA.RODRIGUEZ':
                $("#citasMedicas").css('display', 'block');                
                $("#consultaUrologia").css('display', 'block');
                $("#consultaGeneral").css('display', 'block');
                break;
            case 'ANGELA.DIAZ':
                $("#citasMedicas").css('display', 'block');
                $("#consultaOftalmologia").css('display', 'none');
                break;
            case 'JAVIER.LOPEZ':
                $("#citasMedicas").css('display', 'block');
                $("#consultaOftalmologia").css('display', 'none');
                break;
            case 'YONEL.JAIMES':
                $("#citasMedicas").css('display', 'block');
                $("#consultaOftalmologia").css('display', 'none');
                break;                
            case 'ELIZABETH.HERNANDEZ':
            case 'OSCAR.ANDRADE':
            case 'DANIEL.SEGURA':
                $("#consultaVascular").css('display', 'block');
                $("#consultaanexodocumentos").css('display', 'block');
                break;
            default:
                $(".span2").css('display', 'block');
                $(".span_label").css('display', 'block');
        }
    }
    /*
    $("#consulta_Ortopedia").css('display', 'block');
    $("#consultaOtorrino").css('display', 'block');
    $("#consulta_Audiometria").css('display', 'block');
    $('#txt_04_T01NumDocPaciente').attr('disabled', 'disabled');
    $("#drop_04_T01TipoDocPaciente").on("change", function () {
        $('#txt_04_T01NumDocPaciente').removeAttr('disabled');
    });

    */

    //javascript: __doPostBack('1sepCCsep40185324', '');

    if ($("#txt_04_T13NumDocPaciente").length > 0) {
        if ($("#drop_04_T13TipoDocPaciente").length > 0) {
            //$("#drop_04_T13TipoDocPaciente option[value='CC']").attr("selected", true);
            //$("#txt_04_T13NumDocPaciente").val("40185324");
            //javascript: __doPostBack('0sepCCsep40185324', '');
        }
    }

    if ($("#txt_04_T01NumDocPaciente").length > 0) {
        if ($("#drop_04_T01TipoDocPaciente").length > 0) {
            //javascript: __doPostBack('11sep897sep63804sepLocal', '');
            //$("#\30 0sep471sep6759sepLocal").click();
           // #\30 0sep471sep6759sepLocal
            //javascript: __doPostBack('01sep471sep6759sepLocal', '');
         
        }
    }


    if ($("#txt_04_T49NumFactura").length > 0) {
        //javascript: __doPostBack('01asep320001332974Local001sep11sep229sepLocalsep54sepEnf03sepConvenio001Local001001sepCCsep1121853181', '');
        //javascript: __doPostBack('02asep320001332974Local001sepsep1sepLocalsep1sepEnf03sepConvenio001Local001001sepCCsep1121853181', '');
        //javascript: __doPostBack('01asep320001332974Local001sepsep1sepLocalsep1sepEnf03sepConvenio001Local001001sepCCsep1121853181', '');
    }


})(jQuery);

function imgError() {
    $("#img_FotoPaciente").attr("src", "iconos/Sinfoto.png");
}

function imgErrorFirma() {
    $("#img_FotoPaciente").attr("src", "iconos/SinFirma.png");
}

function cargarFirma() {
   $("#img_FirmaPaciente").attr("src", "./firmas/" + $("#txt_04_T13NumDocPaciente").val() + ".jpg?" + new Date().getTime());
}

function boton_guardar_popup() {
    $('#btnGuardarPopup').attr('disabled', 'disabled');
}

function boton_guardar() {
    $('#btnGuardar').attr('disabled', 'disabled');
    $('#btnGuardar2').attr('disabled', 'disabled');
}

function captura_firma() {
    window.open("Medical_Paciente_Firma.aspx?c=" + $("#txt_04_T13NumDocPaciente").val() + "&h=" + (window.innerHeight - 300) + "&w=" + (window.innerWidth - 100), "FIRMA", 'height=' + (window.innerHeight) + ',width=' + (window.innerWidth));
}
function captura_firma_Consentimiento_Paciente() {
    window.open("ConsentimientoFirmaPaciente.aspx?c=" + $("#txt_04_T01NumDocPaciente").val() + "&h=" + (window.innerHeight - 300) + "&w=" + (window.innerWidth - 100), "FIRMA", 'height=' + (window.innerHeight) + ',width=' + (window.innerWidth));
}
function captura_firma_Consentimiento_Responsable() {
    window.open("ConsentimientoFirmaResponsable.aspx?c=" + $("#txt_04_T54_3NumDocResponsable").val() + "&h=" + (window.innerHeight - 300) + "&w=" + (window.innerWidth - 100), "FIRMA", 'height=' + (window.innerHeight) + ',width=' + (window.innerWidth));
}
function captura_firma_Cirugia_Paciente_Salida() {
    window.open("pacienteFirma.aspx?c=" + $("#txt_04_T90_12_1NumDocPaciente").val() + "&h=" + (window.innerHeight - 300) + "&w=" + (window.innerWidth - 100), "FIRMA", 'height=' + (window.innerHeight) + ',width=' + (window.innerWidth));
}
function captura_firma_Cirugia_Testigo_Salida() {
    window.open("pacienteFirma.aspx?c=" + $("#txt_04_T90_12_1DocumentoTestigo").val() + "&h=" + (window.innerHeight - 300) + "&w=" + (window.innerWidth - 100), "FIRMA", 'height=' + (window.innerHeight) + ',width=' + (window.innerWidth));
}


function captura_firma_Conta_RC() {
    window.open("FirmasDigitales.aspx?TipoFormulario=RC&c=" + $("#txt_NumDoc_Firma").val() + "&h=" + (window.innerHeight - 300) + "&w=" + (window.innerWidth - 100), "FIRMA", 'height=' + (window.innerHeight) + ',width=' + (window.innerWidth));
}

function captura_Cirugia_GraficaRAnestesico() {
    window.open("Cirugia_RegistroAnestesicoGrafica.aspx?c=" + $("#drop_04_T90_1IDPacienteProgramado").val() + "&h=" + (window.innerHeight - 300) + "&w=" + (window.innerWidth - 100), "FIRMA", 'height=' + (window.innerHeight) + ',width=' + (window.innerWidth));
}


function captura_foto_Archivos(sNumDocTem,sCodigoIDRegistro,sModulo) {
    window.open("Medical_Paciente_Foto_Archivos.aspx?IdNumDoc=" + sNumDocTem + "&CodIDR=" + sCodigoIDRegistro + "&CodMod=" + sModulo, "FOTO", 'height=' + (310) + ',width=' + (730));
}

function captura_foto() {
    window.open("Medical_Paciente_Foto.aspx?c=" + $("#txt_04_T13NumDocPaciente").val(), "FOTO", 'height=' + (310) + ',width=' + (730));
}
function captura_foto_Nuevo() {
    window.open("Medical_Paciente_Foto_Nuevo.aspx?sTipoDoc=" + $("#drop_04_T13TipoDocPaciente").val() + "&sNumDoc=" + $("#txt_04_T13NumDocPaciente").val(), "FOTO", 'height=' + (370) + ',width=' + (700));
}
function captura_Huella() {
    window.open("Medical_Paciente_huella.aspx?T=" + $("#drop_04_T13TipoDocPaciente").val() + "&C=" + $("#txt_04_T13NumDocPaciente").val(), "HUELLA", 'height=' + (410) + ',width=' + (800));
}
function captura_Huella_Nuevo() {
    window.open("Medical_Paciente_huella_Nuevo.html?sTipoDoc=" + $("#drop_04_T13TipoDocPaciente").val() + "&sNumDoc=" + $("#txt_04_T13NumDocPaciente").val() + "&sMode=Registrar", "HUELLA", 'width=650,height=520,left=50,top=50,toolbar=no,directories=no, location=no, menubar=no, statusbar=no');
}
function captura_Huella_Verificar() {
    if ($("#txt_04_T13NumDocPaciente").val() == '') {
        window.open("Medical_Paciente_huella_Nuevo.html?sTipoDoc=&sNumDoc=&sMode=Identificar&sPaginaWeb=" + $("#txtPaginaWeb").val(), "HUELLA", 'width=650,height=520,left=50,top=50,toolbar=no,directories=no, location=no, menubar=no, statusbar=no');
    }
    else {
        window.open("Medical_Paciente_huella_Nuevo.html?sTipoDoc=" + $("#drop_04_T13TipoDocPaciente").val() + "&sNumDoc=" + $("#txt_04_T13NumDocPaciente").val() + "&sMode=Verificar&sPaginaWeb=" + $("#txtPaginaWeb").val(), "HUELLA", 'width=650,height=520,left=50,top=50,toolbar=no,directories=no, location=no, menubar=no, statusbar=no');
    }
    
}

function captura_Huella_Cirugia_Salida_Paciente() {
    window.open("pacienteHuella.aspx?T=" + $("#drop_04_T90_12_1TipoDocPaciente").val() + "&C=" + $("#txt_04_T90_12_1NumDocPaciente").val(), "HUELLA", 'height=' + (410) + ',width=' + (800));
}
function captura_Huella_Cirugia_Salida_Testigo() {
    window.open("pacienteHuella.aspx?T=" + $("#drop_04_T90_12_1TipoDocPacienteTestigo").val() + "&C=" + $("#txt_04_T90_12_1DocumentoTestigo").val(), "HUELLA", 'height=' + (410) + ',width=' + (800));
}

function captura_Huella_Cirugia_Consentimientos_Paciente() {
    window.open("pacienteHuella.aspx?T=" + $("#drop_04_T01TipoDocPaciente").val() + "&C=" + $("#txt_04_T01NumDocPaciente").val(), "HUELLA", 'height=' + (410) + ',width=' + (800));
}
function captura_Huella_Cirugia_Consentimientos_Testigo() {
    window.open("pacienteHuella.aspx?T=" + $("#drop_04_T54_3TipoDocResponsable").val() + "&C=" + $("#txt_04_T54_3NumDocResponsable").val(), "HUELLA", 'height=' + (410) + ',width=' + (800));
}


function captura_odontograma() {
    var datos = $("#txt_04_T01NumDocPaciente").val() + "_" + $("#txt_04_T01Fecha").val();
    datos = datos.replace(/\//g,"");
    datos = datos.replace(/:/g, "");
    datos = datos.replace(/ /g, "");
    window.open("pacienteOdontograma.aspx?c=" + datos, "ODONTOGRAMA", 'height=' + (window.innerHeight) + ',width=' + (window.innerWidth));
}

function descargar_odontograma() {
    var datos = $("#txt_04_T01NumDocPaciente").val() + "_" + $("#txt_04_T01Fecha").val();
    datos = datos.replace(/\//g, "");
    datos = datos.replace(/:/g, "");
    datos = datos.replace(/ /g, "");
    $.ajax({
        url: "./odontogramas/" + datos + ".jpg", 
        success: function (data) {
       
            var link = document.createElement('a');
            link.href = "./odontogramas/" + datos + ".jpg";
            link.download = datos + ".jpg";
            document.body.appendChild(link);
            link.click();

        },
        error: function (data) {
            alert('DEBE CREAR PRIMERO EL ODONTOGRAMA');
        },
    });
}

function prueba() {
    alert("texto");
}

function cargar_loading() {
    $('.loading').css('display', 'block');
}

function cerrar_loading() {
    $('.loading').css('display', 'none');
}

function cargardato(nombre) {
    $("#nombrePaciente").val(nombre);
}


function Nuevo_Medicamento(campo, proceso) {

    var p = $("#" + campo);

    if (campo == "txt_04_T87Exa_OtrosExamenes") {
        if ($("#txt_04_T87Exa_OtrosExamenes2").length > 0) {
            p = $("#txt_04_T87Exa_OtrosExamenes2");
        }
    }

    var position = p.position();
    $("#PanelModalPopup_NuevoMedicamento").css('top', position.top);
    
    $("#txt_04_T58Campo").val(campo);
    $("#txt_04_T58Descripcion").val("");
    $("#drop_04_T58Medicamentos option[value='']").attr("selected", true);
    $("#drop_04_T58Medicamentos option[value!='0']").hide();
    $("#drop_04_T58Medicamentos option[value^='" + campo + "']").show();
    $("#drop_04_T58Medicamentos option[value='']").show();
    $("#drop_04_T58Medicamentos").trigger("liszt:updated");
    document.getElementById("txt_04_T58Descripcion").focus();

    if (proceso == 0) {
        $find("programmatic_NuevoMedicamento").show();
    } else {
    }
    //$("#txt_04_T87_2Contenido").val($("#txt_04_T87_2Contenido").val());

}

function Nuevo_Medicamento_Opcion(value, texto) {
    if (value != "") {
        $("#txt_04_T58Descripcion").val(texto);
    }
}

function Nuevo_Medicamento_seleccion(value, contenido, proceso) {

    if (value != "") {

        var campo = value.split("-", 1);

        if (campo == "txt_04_T87Exa_OtrosExamenes") {
            $("#txt_04_T87Exa_OtrosExamenes2").val(contenido + " " + $("#txt_04_T87Exa_OtrosExamenes2").val());
        }


        $("#" + campo).val(contenido + " " + $("#" + campo).val());

        if (proceso == 0) {
            $find("programmatic_NuevoMedicamento").hide();
        } else {
        }
        //console.log(campo[0]);

        if (campo == "txt_04_T87Exa_OtrosExamenes") {
            if ($("#txt_04_T87Exa_OtrosExamenes2").length > 0) {
                document.getElementById("txt_04_T87Exa_OtrosExamenes2").focus();
            } else {
                document.getElementById(campo[0]).focus();
            }
        } else {
            document.getElementById(campo[0]).focus();
        }



        //$("#" + value.split("-", 1)).val($("#" + value.split("-", 1)).val());

    }

}

function Nuevo_Medicamento_cerrar() {
    var campo = $("#txt_04_T58Campo").val();
        //
    /*

    if ($("#programmaticPredeterminado").valid()) {
        
    }

    */
    $find("programmatic_NuevoMedicamento").hide();
    document.getElementById(campo).focus();





    //$("#" + campo).val($("#" + campo).val());
}


function Lector_Cedula(campo, proceso) {

    var p = $("#" + campo);

    var position = p.position();
    $("#PanelModalPopup_LectorCedula").css('top', position.top);
       
    document.getElementById("txt_04_TLector_Documento").focus();

    if (proceso == 0) {
        $find("programmatic_LectorCedula").show();
    } else {
    }
    document.getElementById("txt_04_TLector_Documento").focus();

}


function Lecto_Cedula_cerrar() {
    var campo = $("#txt_04_T13NumDocPaciente").val();
    $find("programmatic_LectorCedula").hide();
    document.getElementById(campo).focus();
}

function Paciente_Basico(campo, proceso) {

    var p = $("#" + campo);

    var position = p.position();
    $("#PanelModalPopup_PacienteBasico").css('top', position.top);

    document.getElementById("txt_Paciente_Documento").focus();

    if (proceso == 0) {
        $find("programmatic_PacienteBasico").show();
    } else {
    }
    document.getElementById("txt_Paciente_Documento").focus();

}


function Paciente_Basico_cerrar() {
    var campo = $("#txt_04_T17DocPaciente").val();
    $find("programmatic_PacienteBasico").hide();
    document.getElementById(campo).focus();
}



function predeterminada(campo, proceso) {

    var p = $("#" + campo);
    
    if (campo == "txt_04_T87Exa_OtrosExamenes") {
        if ($("#txt_04_T87Exa_OtrosExamenes2").length > 0) {
            p = $("#txt_04_T87Exa_OtrosExamenes2");
        }
    }

    var position = p.position();
    $("#PanelModalPopupPredeterminado").css('top', position.top);
    //alert(position.top);
    
    $("#txt_04_T87_2Campo").val(campo);
    $("#txt_04_T87_2Contenido").val("");
    $("#drop_04_T87_2Contenido option[value='']").attr("selected", true);
    $("#drop_04_T87_2Contenido option[value!='0']").hide();
    $("#drop_04_T87_2Contenido option[value^='" + campo + "']").show();
    $("#drop_04_T87_2Contenido option[value='']").show();
    $("#drop_04_T87_2Contenido").trigger("liszt:updated");
    document.getElementById("txt_04_T87_2Contenido").focus();
    
    if (proceso == 0) {
        $find("programmaticPredeterminado").show();
    } else {
    }
    //$("#txt_04_T87_2Contenido").val($("#txt_04_T87_2Contenido").val());

}

function predeterminada_opcion(value, texto) {
    if (value != "") {
        $("#txt_04_T87_2Contenido").val(texto);
    }
}

function predeterminada_seleccion(value, contenido, proceso) {
    
    if (value != "") {

        var campo = value.split("-", 1);
        
        if (campo == "txt_04_T87Exa_OtrosExamenes") {
            $("#txt_04_T87Exa_OtrosExamenes2").val(contenido + " " + $("#txt_04_T87Exa_OtrosExamenes2").val());
        }


        $("#" + campo).val(contenido + " " + $("#" + campo).val());

        if (proceso == 0) {
            $find("programmaticPredeterminado").hide();
        } else {
        }
        //console.log(campo[0]);

        if (campo == "txt_04_T87Exa_OtrosExamenes") {
            if ($("#txt_04_T87Exa_OtrosExamenes2").length > 0) {
                document.getElementById("txt_04_T87Exa_OtrosExamenes2").focus();
            } else {
                document.getElementById(campo[0]).focus();
            }
        } else {
            document.getElementById(campo[0]).focus();
        }


        
        //$("#" + value.split("-", 1)).val($("#" + value.split("-", 1)).val());

    }
   
}

function predeterminada_cerrar() {
    var campo = $("#txt_04_T87_2Campo").val();
    //
    /*

    if ($("#programmaticPredeterminado").valid()) {
        
    }

    */
    $find("programmaticPredeterminado").hide();
    document.getElementById(campo).focus();


     


    //$("#" + campo).val($("#" + campo).val());
}

function colapsar() {
    
    //$("#pHeader5").click();
    //$("#Panel_UtilidadesMedicas > div:nth-child(4) > div:nth-child(4) > div:nth-child(2)").click();
    //$("#Panel_UtilidadesMedicas > div:nth-child(4) > div:nth-child(4) > div:nth-child(2)").css("height", "auto");
    //$("#Panel_UtilidadesMedicas > div:nth-child(4) > div:nth-child(4) > div:nth-child(2)").css("display", "block");
    //$("#Panel_UtilidadesMedicas > div:nth-child(4) > div:nth-child(4) > div:nth-child(2)").css("height", "auto");
    //$("#Panel_UtilidadesMedicas > div:nth-child(4) > div:nth-child(4) > div:nth-child(2)").css("display", "block");
    //$("#Panel_UtilidadesMedicas > div:nth-child(4) > div:nth-child(4) > div:nth-child(2)").css({ "height": "auto", "display": "block" });
    //$("#Panel_UtilidadesMedicas > div:nth-child(4) > div:nth-child(4) > div:nth-child(2)").css({ "display": "block", "height": "auto" });


}

function calcularDiasIncapacidad(inicio, terminacion) {
    accept = regexDateValidator($("#" + inicio).val());
    if (!accept) {
        $("#" + inicio).val('');
    }
    accept2 = regexDateValidator($("#" + terminacion).val());
    if (!accept2) {
        $("#" + terminacion).val('');
    }
    if (accept && accept2) {
        $("#diasIncapacidad").val(restaFechas($("#" + inicio).val(), $("#" + terminacion).val()));
    } else {
        $("#diasIncapacidad").val("");
    }
}

function calcularDiasIncapacidad_Utilidad_Procedimientos(inicio, terminacion) {
    accept = regexDateValidator($("#" + inicio).val());
    if (!accept) {
        $("#" + inicio).val('');
    }
    accept2 = regexDateValidator($("#" + terminacion).val());
    if (!accept2) {
        $("#" + terminacion).val('');
    }
    if (accept && accept2) {
        $("#txt_04_T90_55_3Duracion").val(restaFechas($("#" + inicio).val(), $("#" + terminacion).val()));
    } else {
        $("#txt_04_T90_55_3Duracion").val("");
    }
}


function laboratorio(texto) {
    $("#txt_04_T55Contenido").val("- " + texto + "\n" + $("#txt_04_T55Contenido").val());
}
function Procedimiento(texto) {
    $("#txt_04_T55Contenido").val("- " + texto + "\n" + $("#txt_04_T55Contenido").val());
}

function calcularImc() {
    if ($("#txt_04_T14Peso").val() != "" && $("#txt_04_T01Estatura").val() != "") {
        if ($("#txt_04_T14Peso").length > 0) {
            peso = $("#txt_04_T14Peso").val();
        } else {
            peso = $("#txt_04_T01Peso").val();
        }
        if (!isNaN(peso) && !isNaN($("#txt_04_T01Estatura").val())) {
            var total = 0;
            talla = $("#txt_04_T01Estatura").val();
            total = (peso / (talla * talla));
            $("#txt_04_T01IMC").val(total.toFixed(2));
        } else {
            $("#txt_04_T01IMC").val("0");
        }
    } else {
        $("#txt_04_T01IMC").val("0");
    }
}

function calcularImc_ValorAnestesica() {
    if ($("#txt_04_T90_7EF_Peso").val() != "" && $("#txt_04_T90_7EF_Talla").val() != "") {
        if ($("#txt_04_T90_7EF_Peso").length > 0) {
            peso = $("#txt_04_T90_7EF_Peso").val();
        } else {
            peso = $("#txt_04_T90_7EF_Peso").val();
        }
        if (!isNaN(peso) && !isNaN($("#txt_04_T90_7EF_Talla").val())) {
            var total = 0;
            talla = $("#txt_04_T90_7EF_Talla").val();
            total = (peso / (talla * talla));
            $("#txt_04_T90_7EF_IMC").val(total.toFixed(2));
        } else {
            $("#txt_04_T90_7EF_IMC").val("0");
        }
    } else {
        $("#txt_04_T90_7EF_IMC").val("0");
    }
}

function toxico() {
    if ($("#txt_04_T70Alergicos").val() != "") {
        $("#drop_04_T70Alergia option[value='SI']").attr("selected", true);
    } else {
        $("#drop_04_T70Alergia option[value='NO']").attr("selected", true);
    }
    $("#drop_04_T70Alergia").trigger("liszt:updated");
}

function toxicos() {
    if ($("#drop_04_T70Alergia").val() == "NO") {
        $("#txt_04_T70Alergicos").val("");
    }
}

function ocultarDiv() {
    /*

    $("#Panel_DatosPersonales").css('display', 'none');
    $("#UpdatePanel1 > div:nth-child(1)").css('display', 'none');
    $("#UpdatePanel1 > div:nth-child(3) > div.span2").css('display', 'none');
    $("#UpdatePanel1 > div:nth-child(2) > div").css('display', 'none');
    $("#UpdatePanel1 > div:nth-child(3) > div.span10").css('display', 'none');
    $("#Panel3").css('display', 'none');
    $("#panel_Ginecobstetrica").css('display', 'none');
    $("#panel_Ginecobstetrica_EscalaBiosicosocial").css('display', 'none');
    $("#panel_Ginecobstetrica_ExamenesMaternos").css('display', 'none');
    $("#Panel_ExamenFisico_Normal").css('display', 'none');
    $("#Panel_Ginecobstetrica_ExamenFisico").css('display', 'none');
    $("#Panel_Evolucion").css('display', 'none');
    $("#Panel_FinalidadConsulta").css('display', 'none');
    $("#pHeader").click();
    $("#Panel_Graficas").click();
    $("#lblText").click();
    */

    

    
}

function cambiar_todos() {
    if ($("#citasTodos").is(':checked')) {
        $('input[type="checkbox"]').attr('checked', true);
    } else {
        $('input[type="checkbox"]').attr('checked', false);
    }
}

function cambiar_estado(value) {
    $("select.opcionEstado").val(value);
}

