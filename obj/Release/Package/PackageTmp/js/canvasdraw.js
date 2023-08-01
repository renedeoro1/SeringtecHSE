// by Chtiwi Malek ===> CODICODE.COM

var mousePressed = false;
var lastX, lastY;
var ctx;
var maxX = 0;
var maxY = 0;

var minX = window.innerWidth;
var minY = window.innerHeight;

function InitThis() {
    ctx = document.getElementById('myCanvas').getContext("2d");
    $('#myCanvas').mousedown(function (e) {
        mousePressed = true;
        Draw(e.pageX - $(this).offset().left, e.pageY - $(this).offset().top, false);
    });

    $('#myCanvas').mousemove(function (e) {
        if (mousePressed) {
            Draw(e.pageX - $(this).offset().left, e.pageY - $(this).offset().top, true);
        }
    });

    $('#myCanvas').mouseup(function (e) {
        if (mousePressed) {
            mousePressed = false;
            cPush();
        }
    });

    $('#myCanvas').mouseleave(function (e) {
        if (mousePressed) {
            mousePressed = false;
            cPush();
        }
    });
    drawImage();
}

function drawImage() {
  
    
    var image = new Image();
    image.src = 'img/MyPictureBlanco.png';
    $(image).load(function () {
        ctx.drawImage(image, 0, 0, $("#myCanvas").width(), $("#myCanvas").height());
        cPush();
    });
    /*
    ctx.clearRect(0, 0, $("#myCanvas").width(), $("#myCanvas").height());
    var image = new Image();
    image.src = 'img/MyPictureBlanco.png';
    $(image).load(function () {
        ctx.drawImage(image, 0, 0, $("#myCanvas").width(), $("#myCanvas").height());
        cPush();
    });
    /*
    var image2 = new Image();
    image2.src = 'img/MyPicture.png';
    $(image2).load(function () {
        ctx.drawImage(image2, 0, 0, $("#myCanvas").width(), $("#myCanvas").height());
        cPush();
    });*/
}

function Draw(x, y, isDown) {

    if (isDown) {
        ctx.beginPath();
        ctx.strokeStyle = $('#selColor').val();
        ctx.lineWidth = $('#selWidth').val();
        ctx.lineJoin = "round";
        ctx.moveTo(lastX, lastY);
        ctx.lineTo(x, y);
        ctx.closePath();
        ctx.stroke();
    }

    lastX = x;
    lastY = y;

    if (maxX < x) {
        maxX = x;
    }
    if (maxY < y) {
        maxY = y;
    }

    if (minX > x) {
        minX = x;
    }

    if (minY > y) {
        minY = y;
    }

}

var cPushArray = new Array();
var cStep = -1;

function cPush() {
    cStep++;
    if (cStep < cPushArray.length) { cPushArray.length = cStep; }
    cPushArray.push(document.getElementById('myCanvas').toDataURL());
    //document.title = cStep + ":" + cPushArray.length;
}
function cUndo() {
    if (cStep > 0) {
        cStep--;
        var canvasPic = new Image();
        canvasPic.src = cPushArray[cStep];
        canvasPic.onload = function () { ctx.drawImage(canvasPic, 0, 0); }
        document.title = cStep + ":" + cPushArray.length;
    }
}
function cRedo() {
    if (cStep < cPushArray.length-1) {
        cStep++;
        var canvasPic = new Image();
        canvasPic.src = cPushArray[cStep];
        canvasPic.onload = function () { ctx.drawImage(canvasPic, 0, 0); }
        document.title = cStep + ":" + cPushArray.length;
    }
}

function DrawPic() {
    // Get the canvas element and its 2d context
    var Cnv = document.getElementById('myCanvas');
    image.src = 'MyPicture.png';
    $(image).load(function () {
        Cnv.drawImage(image, 80, 80, 150, 150);
    });
    
    var Cntx = Cnv.getContext('2d');

    // Create gradient
    var Grd = Cntx.createRadialGradient(100, 100, 20, 140, 100, 230);
    // Grd.addColorStop(0, 'rgba(31,0,0,' + opacity + ')');
     Grd.addColorStop(0, "white");
    // Grd.addColorStop(1, "white");


    // Fill with gradient

    var d = new Date(); 

    Cntx.fillStyle = Grd;
    Cntx.fillRect(80, 80, 150, 150);
    Cntx.fillStyle = "black";
    Cntx.font = "20px Verdana";
    Cntx.fillText(d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds(), 20, 40);
}

function UploadPic() {

    // generate the image data
    //document.getElementById('myCanvas').width = "500px";

    /*document.getElementById("myCanvas").width = "300";
    ctx.strokeRect(0, 0, document.getElementById("myCanvas").width, document.getElementById("myCanvas").height);
    */


    var oldCanvas = document.getElementById("myCanvas").toDataURL("image/jpg");
    var img = new Image();
    img.src = oldCanvas;
    img.onload = function () {
        // document.getElementById("myCanvas").width = maxX + 5;
        // document.getElementById("myCanvas").height = maxY + 5;

        document.getElementById("myCanvas").width = maxX - (minX) + 10;
        document.getElementById("myCanvas").height = maxY - (minY) + 10;

        ctx.drawImage(img, -(minX - 5), -(minY - 5));

        //document.getElementById("myCanvas").width = maxX + 5 - minX;
        //ctx.drawImage(img, minX, 0);

        //alert(minX);


        var Pic = document.getElementById("myCanvas").toDataURL("image/jpg");
        Pic = Pic.replace(/^data:image\/(png|jpg);base64,/, "");

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

    //ctx.strokeRect(0, 0, "300", document.getElementById("myCanvas").height);
    
}

function UploadPicOdontograma() {

    var oSerializer = new XMLSerializer();
    var sXML = oSerializer.serializeToString(document.getElementById("alejandro"));
    canvg(document.getElementById('foto'), sXML, { ignoreMouse: true, ignoreAnimation: true })
    document.getElementById("downloader").download = "odontograma.jpg";
    document.getElementById("downloader").href = document.getElementById("foto").toDataURL("image/jpg").replace(/^data:image\/[^;]/, 'data:application/octet-stream');

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
            window.close();
        }
    });
}