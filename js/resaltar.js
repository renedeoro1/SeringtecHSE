function ResaltarFila(id_tabla){
    if (id_tabla == undefined)      // si no se le pasa parametro
        // recupera todas las filas de todas las tablas
        var filas = document.getElementsByTagName("tr");
    else{
        // recupera todas las filas de la tabla indicada en el parametro
        var tabla = document.getElementById(id_tabla);
        var filas = tabla.getElementsByTagName("tr");
    }
    // recorre cada una de las filas
    for(var i in filas) {
    	// cuando el puntero esta encima de la fila asigna la regla css: resaltar
        filas[i].onmouseover = function() {
            this.className = "resaltar";
        }
        // cuando el puntero salga de la fila asigna ninguna regla
        filas[i].onmouseout = function() {
            this.className = null;
        }
    }
}

function noResaltarFila(id_tabla){
    if (id_tabla == undefined)
        var filas = document.getElementsByTagName("tr");
    else{
        var tabla = document.getElementById(id_tabla);
        var filas = tabla.getElementsByTagName("tr");
    }
    for(var i in filas) {
        filas[i].onmouseover = function() {
            this.className = null;
        }
        filas[i].onmouseout = function() {
            this.className = null;
        }
    }
}