//seleccionar un combo

$(document).ready(function () {


});



var setearFormularioPeliculas = function ( idGenero) { //combos seleccionados

    $("#comboPeliculaModel option[value=" + idGenero + "]").attr("selected", true);

}