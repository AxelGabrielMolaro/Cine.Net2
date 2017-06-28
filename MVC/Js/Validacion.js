function preguntar(e) {
    eliminar = confirm("¿Estás seguro que deseas eliminar lo elegido?");
    if (!eliminar) {
        e.preventDefault();
    }
}