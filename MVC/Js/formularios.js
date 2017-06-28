//seleccionar un combo

$(document).ready(function () {

    //datepicker

    $(function () {
        $(".datepicker").datepicker(
            { dateFormat: 'dd/mm/yy' }
            );
    });
    //
    

    var paso = $("#pasito").text();


    //pasos
    /*cambiar por display none*/

    if (paso == 1) {
    
        $("#paso1").fadeIn();
        $("#paso2").fadeOut();
        $("#paso3").fadeOut();
        $("#paso4").fadeOut();
        $("#progreso").text("25%");
    }
    if (paso == 2) {

        $("#paso2").fadeIn();
        $("#paso1").fadeOut();
        $("#paso3").fadeOut();
        $("#paso4").fadeOut();
        $("#progreso").text("50%");
    }
    if (paso == 3) {
        $("#paso3").fadeIn();
        $("#paso1").fadeOut();
        $("#paso2").fadeOut();
        $("#paso4").fadeOut();
        $("#progreso").text("75%");
    }
    if (paso == 4) {
        $("#paso3").fadeOut();
        $("#paso1").fadeOut();
        $("#paso2").fadeOut();
        $("#paso4").fadeIn();
        $("#progreso").text("90%");

    }

});



var setearFormularioPeliculas = function ( idGenero) { //combos seleccionados

    $("#comboPeliculaModel option[value=" + idGenero + "]").attr("selected", true);

}


//----------------------------------------Reservas------------------------------------------------------------//


var setearPrecioTotalReserva = function ()
{
    
    
    var precio = $("#precioTotalReserva");
    var cantidad = $("#cantidadDeEntradas");
    var precioUnitario = $("#precioUnitario");
    if ($("#precioTotalReserva").text() != "NaN") {
        precio.text(cantidad.val() * parseInt(precioUnitario.text()));
    } else {
        precio.text("Cantidad incorrecta");
    }
    
}



//confirmarReserva

var mostrarCartelDeConfirmacionDeReserva = function ()
{
    
    $("#cartel-confirmacion-reserva").fadeIn();
    $("#mail-cartel").text("Mail : " + $("#mail").val());
    $("#entradas-cartel").text("Cantidad de entradas : " + $("#cantidadDeEntradas").val());
    if ($("#precioTotalReserva").text() != "NaN") {
        $("#precio-cartel").text("Total : $" + $("#precioTotalReserva").text());
    }
    else {
        $("#precio-cartel").text("Verifique cantidad");
    }
}

var ocultarCartelDeConfirmacionDeReserva = function () {
   
        $("#cartel-confirmacion-reserva").fadeOut();
 
}

var enviarFormularioReserva = function()
{
    $("#formularioReserva2").submit();   
}

