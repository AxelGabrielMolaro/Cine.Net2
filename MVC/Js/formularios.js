//seleccionar un combo

$(document).ready(function () {


});



var setearFormularioPeliculas = function ( idGenero) { //combos seleccionados

    $("#comboPeliculaModel option[value=" + idGenero + "]").attr("selected", true);

}


//----------------------------------------Reservas------------------------------------------------------------//
var setearPrecioTotalReserva = function()
{
    
   
    var precio = $("#precioTotalReserva");
    var cantidad = $("#cantidadDeEntradas");
    var precioUnitario = $("#precioUnitario");

    precio.text(cantidad.val() * parseInt(precioUnitario.text()));
}