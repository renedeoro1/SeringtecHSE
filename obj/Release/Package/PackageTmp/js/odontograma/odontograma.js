jQuery(function(){
var index = 0;
//alert(window.innerWidth );
	function drawDiente(svg, parentGroup, diente){
		if(!diente) throw new Error('Error no se ha especificado el diente.');
		
		var x = diente.x || 0,
			y = diente.y || 0;
		
		var defaultPolygon = { fill: 'white', stroke: 'navy', strokeWidth: 0.5};
		var dienteGroup = svg.group(parentGroup, {transform: 'translate(' + x + ',' + y + ')'});
                              
		var caraSuperior = svg.polygon(dienteGroup,
			[[0,0],[40,0],[27,20],[13,20]],  
		    $.extend(defaultPolygon, {id: diente.id.toString() + "_S"}));
	    caraSuperior = $(caraSuperior).data('cara', 'S');
		
		var caraInferior =  svg.polygon(dienteGroup,
			[[13,40],[27,40],[40,60],[0,60]], 
		    $.extend(defaultPolygon, { id: diente.id.toString() + "_I" }));
		caraInferior = $(caraInferior).data('cara', 'I');

		var caraDerecha = svg.polygon(dienteGroup,
			[[27,20],[40,0],[40,60],[27,40]],    
		    $.extend(defaultPolygon, { id: diente.id.toString() + "_D" }));
	    caraDerecha = $(caraDerecha).data('cara', 'D');
		
		var caraIzquierda = svg.polygon(dienteGroup,
			[[0,0],[13,20],[13,40],[0,60]],  
		    $.extend(defaultPolygon, { id: diente.id.toString() + "_Z" }));
		caraIzquierda = $(caraIzquierda).data('cara', 'Z');		    
		
		var caraCentral = svg.polygon(dienteGroup,
			[[13,20],[27,20],[27,40],[13,40]],
		    $.extend(defaultPolygon, { id: diente.id.toString() + "_C" }));
		caraCentral = $(caraCentral).data('cara', 'C');		    
	    
	    var caraCompleto = svg.text(dienteGroup, 16, 70, diente.id.toString(), 
	    	{fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: 'font-size: 6pt;font-weight:normal'});
    	caraCompleto = $(caraCompleto).data('cara', 'X');
    	
		//Busco los tratamientos aplicados al diente
		var tratamientosAplicadosAlDiente = ko.utils.arrayFilter(vm.tratamientosAplicados(), function(t){
			return t.diente.id == diente.id;
		});
        
        //alert(tratamiento.nombre);
	    //alert($("#caracteristica").val());
        
		var caras = [];
		caras['S'] = caraSuperior;
		caras['I'] = caraInferior;
		caras['C'] = caraCentral;
		caras['X'] = caraCompleto;
		caras['Z'] = caraIzquierda;
		caras['D'] = caraDerecha;

		for (var i = tratamientosAplicadosAlDiente.length - 1; i >= 0; i--) {
			var t = tratamientosAplicadosAlDiente[i];
			//caras[t.cara].attr('fill', "url(#image)");
            //alert($("#caracteristica option[value=" + t.tratamiento + "]").data('estilo'));
            //alert(t.tratamiento);
			//$("#" + t.diente.id + "_S").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('prueba') );
			switch(t.cara) {
			    case 'S':
					svg.text(dienteGroup, 15, 15, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo') });
					break;
			    case 'C':
					svg.text(dienteGroup, 15, 33, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
			        break;
			    case 'I':
					svg.text(dienteGroup, 16, 55, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
			        break;
			    case 'X':
					svg.text(dienteGroup, 15, 15, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
					svg.text(dienteGroup, 16, 33, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
					svg.text(dienteGroup, 16, 55, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
					svg.text(dienteGroup, 2, 33, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
					svg.text(dienteGroup, 29, 33, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
			        break;
			    case 'Z':
					svg.text(dienteGroup, 2, 33, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
			        break;
			    case 'D':
					svg.text(dienteGroup, 29, 33, $("#caracteristica option[value=" + t.tratamiento + "]").data('texto'), {fill: 'navy', stroke: 'navy', strokeWidth: 0.1, style: $("#caracteristica option[value=" + t.tratamiento + "]").data('estilo')});
			        break;
			}
            //alert($("#caracteristica option[value=" + t.tratamiento + "]").data('texto'));
			switch (t.tratamiento) {
			    case '2':
			    case '19':
			    case '20':
                    switch (t.cara) {
			            case 'S':
			                $("#" + t.diente.id + "_S").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
			                break;
			            case 'C':
			                $("#" + t.diente.id + "_C").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
			                break;
			            case 'I':
			                $("#" + t.diente.id + "_I").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
			                break;
			            case 'X':
			                $("#" + t.diente.id + "_S").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
			                $("#" + t.diente.id + "_C").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
                            $("#" + t.diente.id + "_I").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
                            $("#" + t.diente.id + "_Z").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));            
                            $("#" + t.diente.id + "_D").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
			                break;
			            case 'Z':
			                $("#" + t.diente.id + "_Z").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
			                break;
			            case 'D':
			                $("#" + t.diente.id + "_D").attr('fill', $("#caracteristica option[value=" + t.tratamiento + "]").data('fill'));
			                break;
			        }
                    break;
			}
			//alert(t.diente.id);
			console.log(t);
////$('#caracteristica option:selected')
//$("#caracteristica option[value=" + t.tratamiento + "]")
//caras[t.cara].attr('fill', "#F5F5DC");
//caras[t.cara].css("background-color", "yellow");
//caras[t.cara].css("color", "yellow");
//caras[t.cara].css("background-color", "yellow");
//caras[t.cara].attr('style', "svg: url(#clipPath)");
//caras[t.cara].attr('fill', "red");
//caras[t.cara].attr('style', "clip-path: url(#clipPath5)");

		};

		$.each([caraCentral, caraIzquierda, caraDerecha, caraInferior, caraSuperior, caraCompleto], function(index, value){
	    	value.click(function(){
	    		var me = $(this);
	    		var cara = me.data('cara');
				
				if(!vm.tratamientoSeleccionado()){
					alert('Debe seleccionar un tratamiento previamente.');	
					return false;
				}

				//Validaciones
				//Validamos el tratamiento
	var tratamiento = vm.tratamientoSeleccionado();
				/*
				var tratamiento = vm.tratamientoSeleccionado();
//alert(tratamiento.nombre);
				if(cara == 'X' && !tratamiento.aplicaDiente){
					alert('El tratamiento seleccionado no se puede aplicar a toda la pieza.');
					return false;
				}
				if(cara != 'X' && !tratamiento.aplicaCara){
					alert('El tratamiento seleccionado no se puede aplicar a una cara.');
					return false;
				}*/
				//TODO: Validaciones de si la cara tiene tratamiento o no, etc...
                
				vm.tratamientosAplicados.push({diente: diente, cara: cara, tratamiento: tratamiento});
				vm.tratamientoSeleccionado(null);
				
				//Actualizo el SVG
				renderSvg();
	    	}).mouseenter(function(){
	    		var me = $(this);
	    		me.data('oldFill', me.attr('fill'));
	    		me.attr('fill', 'yellow');
	    	}).mouseleave(function(){
	    		var me = $(this);
	    		me.attr('fill', me.data('oldFill'));
	    	});			
		});
	};

	function renderSvg(){
		console.log('update render');

		var svg = $('#odontograma').svg('get').clear();
		var parentGroup = svg.group({transform: 'scale(1.5)'});
		var dientes = vm.dientes();
		for (var i =  dientes.length - 1; i >= 0; i--) {
			var diente =  dientes[i];
			var dienteUnwrapped = ko.utils.unwrapObservable(diente); 
			drawDiente(svg, parentGroup, dienteUnwrapped);
		};
	}

    //View Models
	function DienteModel(id, x, y){
		var self = this;

		self.id = id;	
		self.x = x;
		self.y = y;		
	};

	function ViewModel(){
		var self = this;

		self.tratamientosPosibles = ko.observableArray([]);
		self.tratamientoSeleccionado = ko.observable(null);
		self.tratamientosAplicados = ko.observableArray([]);

		self.quitarTratamiento = function(tratamiento){
		    //self.tratamientosAplicados.remove();
		    ///alert(tratamiento);
		    console.log(vm.tratamientosAplicados());

		    self.tratamientosAplicados.remove(tratamiento);

            /*
		    var tratamientosAplicadosAlDiente = ko.utils.arrayFilter(vm.tratamientosAplicados(), function (t) {
		     
		    });

		    for (var i = tratamientosAplicadosAlDiente.length - 1; i >= 0; i--) {
		        self.tratamientosAplicados.remove(tratamientosAplicadosAlDiente[i]);
                
		    }*/
		    //self.tratamientosAplicados.remove(vm.tratamientosAplicados());
			index = index - 1;
			renderSvg();
		}

self.cssNumber = function() {
    return (index % 2);
}

self.itemNumber = function() {
	index = index + 1;
    return index;
}
		//Cargo los dientes
		var dientes = [];
		//Dientes izquierdos
		for(var i = 0; i < 8; i++){
			dientes.push(new DienteModel(18 - i, i * 65, 0));	
		}
		
		for(var i = 3; i < 8; i++){
			dientes.push(new DienteModel(55 - (i-3), i * 65, 1 * 80));	
		}
		for(var i = 3; i < 8; i++){
			dientes.push(new DienteModel(85 - (i-3), i * 65, 2 * 80));	
		}
		for(var i = 0; i < 8; i++){
			dientes.push(new DienteModel(48 - i, i * 65, 3 * 80));	
		}
		//Dientes derechos
		
		for(var i = 0; i < 8; i++){
			dientes.push(new DienteModel(21 + i, i * 65 + 510, 0));	
		}
		for(var i = 0; i < 5; i++){
			dientes.push(new DienteModel(61 + i, i * 65 + 510, 1 * 80));	
		}
		for(var i = 0; i < 5; i++){
			dientes.push(new DienteModel(71 + i, i * 65 + 510, 2 * 80));	
		}
		for(var i = 0; i < 8; i++){
			dientes.push(new DienteModel(31 + i, i * 65 + 510, 3 * 80));	
		}

		self.dientes = ko.observableArray(dientes);
	};

	vm = new ViewModel();
	
	//Inicializo SVG
    $('#odontograma').svg({
        settings: { width: '1508px', height: '467px', id: 'alejandro' }
    });
    //, style: 'background-color: green;color: green;', fill: 'green'
	ko.applyBindings(vm);

	//TODO: Cargo el estado del odontograma
	renderSvg();


    //Cargo los tratamientos
    /*
	$.getJSON('tratamientos.js', function(d){
		for (var i = d.length - 1; i >= 0; i--) {
			var tratamiento = d[i];
			//vm.tratamientosPosibles.push(tratamiento);
		};		
	});
    */
   
function prueba(){
 
}


});


jQuery('#botonFoto').on('click', function (e) {
    
/*
      var canvas = document.getElementById("foto");
          var context = canvas.getContext("2d");
 
          // no argument defaults to image/png; image/jpeg, etc also work on some
          // implementations -- image/png is the only one that must be supported per spec.
          window.open(canvas.toDataURL("image/png"));
*/
    
});
jQuery('#downloader').on('click', function (e) {
    var oSerializer = new XMLSerializer();
    var sXML = oSerializer.serializeToString(document.getElementById("alejandro"));
    canvg(document.getElementById('foto'), sXML,{ ignoreMouse: true, ignoreAnimation: true })
    document.getElementById("downloader").download = "odontograma.jpg";
    document.getElementById("downloader").href = document.getElementById("foto").toDataURL("image/jpg").replace(/^data:image\/[^;]/, 'data:application/octet-stream');
});

function limpiarodontograma() {
    while ($("#tratamiento > div:nth-child(1) > a").length > 0) {
        $("#tratamiento > div:nth-child(1) > a").click();
    }
}