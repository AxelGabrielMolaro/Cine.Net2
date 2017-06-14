function preguntar(e) {
    eliminar = confirm("¿Deseas eliminar este registro?");
    if (!eliminar) {
        e.preventDefault();
    }
}