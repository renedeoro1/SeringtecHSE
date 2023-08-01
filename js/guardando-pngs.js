//Nos aseguramos que estén definidas
//algunas funciones básicas

var pos = 0;
var ctx = null;
var cam = null;
var image = null;

var filter_on = false;
var filter_id = 0;

function changeFilter() {
    if (filter_on) {
        filter_id = (filter_id + 1) & 7;
    }
}

function toggleFilter(obj) {
    if (filter_on = !filter_on) {
        obj.parentNode.style.borderColor = "#c00";
    } else {
        obj.parentNode.style.borderColor = "#333";
    }
}

jQuery("#camara").webcam({

    width: 320,
    height: 240,
    mode: "callback",
    swffile: "./js/webcam/jscam_canvas_only.swf",
    
    onTick: function (remain) {

        if (0 == remain) {
            jQuery("#status").text("Cheese!");
        } else {
            jQuery("#status").text(remain + " seconds remaining...");
        }
    },

    onSave: function (data) {
           
        var col = data.split(";");
        var img = image;

        if (false == filter_on) {

            for (var i = 0; i < 320; i++) {
                var tmp = parseInt(col[i]);
                img.data[pos + 0] = (tmp >> 16) & 0xff;
                img.data[pos + 1] = (tmp >> 8) & 0xff;
                img.data[pos + 2] = tmp & 0xff;
                img.data[pos + 3] = 0xff;
                pos += 4;
            }

        } else {

            var id = filter_id;
            var r, g, b;
            var r1 = Math.floor(Math.random() * 255);
            var r2 = Math.floor(Math.random() * 255);
            var r3 = Math.floor(Math.random() * 255);
    
            for (var i = 0; i < 320; i++) {
                var tmp = parseInt(col[i]);

                /* Copied some xcolor methods here to be faster than calling all methods inside of xcolor and to not serve complete library with every req */
        
                if (id == 0) {
                    r = (tmp >> 16) & 0xff;
                    g = 0xff;
                    b = 0xff;
                } else if (id == 1) {
                    r = 0xff;
                    g = (tmp >> 8) & 0xff;
                    b = 0xff;
                } else if (id == 2) {
                    r = 0xff;
                    g = 0xff;
                    b = tmp & 0xff;
                } else if (id == 3) {
                    r = 0xff ^ ((tmp >> 16) & 0xff);
                    g = 0xff ^ ((tmp >> 8) & 0xff);
                    b = 0xff ^ (tmp & 0xff);
                } else if (id == 4) {

                    r = (tmp >> 16) & 0xff;
                    g = (tmp >> 8) & 0xff;
                    b = tmp & 0xff;
                    var v = Math.min(Math.floor(.35 + 13 * (r + g + b) / 60), 255);
                    r = v;
                    g = v;
                    b = v;
                } else if (id == 5) {
                    r = (tmp >> 16) & 0xff;
                    g = (tmp >> 8) & 0xff;
                    b = tmp & 0xff;
                    if ((r += 32) < 0) r = 0;
                    if ((g += 32) < 0) g = 0;
                    if ((b += 32) < 0) b = 0;
                } else if (id == 6) {
                    r = (tmp >> 16) & 0xff;
                    g = (tmp >> 8) & 0xff;
                    b = tmp & 0xff;
                    if ((r -= 32) < 0) r = 0;
                    if ((g -= 32) < 0) g = 0;
                    if ((b -= 32) < 0) b = 0;
                } else if (id == 7) {
                    r = (tmp >> 16) & 0xff;
                    g = (tmp >> 8) & 0xff;
                    b = tmp & 0xff;
                    r = Math.floor(r / 255 * r1);
                    g = Math.floor(g / 255 * r2);
                    b = Math.floor(b / 255 * r3);
                }

                img.data[pos + 0] = r;
                img.data[pos + 1] = g;
                img.data[pos + 2] = b;
                img.data[pos + 3] = 0xff;
                pos += 4;
                
            }
        }
        console.log(pos);
        if (pos >= 4 * 240 * 320) {//0x4B000
            ctx.putImageData(img, 0, 0);
            pos = 0;
        }    

    },

    onCapture: function () {
        webcam.save();
    },

    debug: function (type, string) {
        jQuery("#status").html(type + ": " + string);
    },

    onLoad: function () {


    }
});

window.addEventListener("load", function () {
    
    var canvas = document.getElementById("foto");

    if (canvas.getContext) {
        ctx = document.getElementById("foto").getContext("2d");
        ctx.clearRect(0, 0, 320, 240);
        var img = new Image();
        image = ctx.getImageData(0, 0, 320, 240);
    }
    
}, false);


jQuery('#botonFoto').on('click', function (e) {
    webcam.capture(); changeFilter(); void (0);
});

function UploadPic() {
           
    var Pic = document.getElementById("foto").toDataURL("image/jpg");

    Pic = Pic.replace(/^data:image\/(png|jpg);base64,/, "")

    // Sending the image data to Server
    $.ajax({
        type: 'POST',
        url: 'Save_Picture.aspx/UploadPic',
        data: '{ "imageData" : "' + Pic + '" , "ruta" : "' + $("#ruta").val() + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            window.close();
        }
    });

}
/*

window.URL = window.URL || window.webkitURL;
navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia ||
function () {
    alert('Su navegador no soporta navigator.getUserMedia().');
};

//Este objeto guardará algunos datos sobre la cámara
window.datosVideo = {
    'StreamVideo': null,
    'url': null
}

jQuery('#botonIniciar').on('click', function (e) {

    //Pedimos al navegador que nos da acceso a 
    //algún dispositivo de video (la webcam)
    navigator.getUserMedia({
        'audio': false,
        'video': true
    }, function (streamVideo) {
        datosVideo.StreamVideo = streamVideo;
        datosVideo.url = window.URL.createObjectURL(streamVideo);
        jQuery('#camara').attr('src', datosVideo.url);

    }, function () {
        alert('No fue posible obtener acceso a la cámara.');
    });

});

jQuery('#botonDetener').on('click', function (e) {

    if (datosVideo.StreamVideo) {
        datosVideo.StreamVideo.stop();
        window.URL.revokeObjectURL(datosVideo.url);
    }

});

jQuery('#botonFoto').on('click', function (e) {
    var oCamara, oFoto, oContexto, w, h;

    oCamara = jQuery('#camara');
    oFoto = jQuery('#foto');
    w = oCamara.width();
    h = oCamara.height();
    oFoto.attr({
        'width': w,
        'height': h
    });
    oContexto = oFoto[0].getContext('2d');
    oContexto.drawImage(oCamara[0], 0, 0, w, h);

});

function UploadPic() {

    // generate the image data
    var Pic = document.getElementById("foto").toDataURL("image/jpg");
    Pic = Pic.replace(/^data:image\/(png|jpg);base64,/, "")

    // Sending the image data to Server
    $.ajax({
        type: 'POST',
        url: 'Save_Picture.aspx/UploadPic',
        data: '{ "imageData" : "' + Pic + '" , "ruta" : "' + $("#ruta").val() + '"}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            $("#botonDetener").click();
            window.close();
        }
    });
}
*/