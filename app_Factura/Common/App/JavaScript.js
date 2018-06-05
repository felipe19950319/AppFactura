var x = [
{
    "CodigoDet": "12345",
    "NombreDet": "Pendrive",
    "Descripcion": "Guardar Archivos",
    "UnidadMedida": "UN",
    "PrecioDet": "9990",
    "Iva": "SI",
    "Cantidad": "20",
    "Descuento": "20",
    "TipoDescuento": "+%",
    "Total": "9990"
},
{
    "CodigoDet": "12345",
    "NombreDet": "Pendrive",
    "Descripcion": "Guardar Archivos",
    "UnidadMedida": "UN",
    "PrecioDet": "9990",
    "Iva": "SI",
    "Cantidad": "20",
    "Descuento": "20",
    "TipoDescuento": "+%",
    "Total": "9990"
},
{
    "CodigoDet": "12345",
    "NombreDet": "Pendrive",
    "Descripcion": "Guardar Archivos",
    "UnidadMedida": "UN",
    "PrecioDet": "9990",
    "Iva": "SI",
    "Cantidad": "20",
    "Descuento": "20",
    "TipoDescuento": "+%",
    "Total": "9990"
}
];


$("#TablaDetalles").DataTable({
    "destroy": true,
    "pageLength": 5,
    "lengthChange": false,
    "searching": false,
    "info": false,
    "data": x,
    "bAutoWidth": false,
    "columns": [
        { "data": "CodigoDet", "title": "Codigo" },
        { "data": "NombreDet", "title": "Nombre" },
        { "data": "Descripcion", "title": "Descripcion" },
        { "data": "UnidadMedida", "title": "U.Medida" },
        { "data": "PrecioDet", "title": "Precio" },
        { "data": "Iva", "title": "Iva" },
        { "data": "Cantidad", "title": "Cantidad" },
        { "data": "Descuento", "title": "Descuento" },
        { "data": "Total", "title": "Total" },

          {
              "title": "Accion",
              "mRender": function (data, type, row) {
                  return '<center><a class="btn btn-danger" ><i class="fa fa-trash" style="color:white" aria-hidden="true"></i></a></center>';
              }
          }
    ], "language": {
        "emptyTable": "No hay registros para mostrar.",
        "info": "Mostrando registros _START_ al _END_ de _TOTAL_ registros totales",
        "infoEmpty": "Mostrando 0 registros",
        "infoFiltered": "(filtrados de _MAX_)",
        "infoPostFix": "",
        "thousands": ".",
        "loadingRecords": "Cargando grilla...",
        "processing": "Procesando...",
        "search": "Buscar:",
        "zeroRecords": "No se encontraron registros para la búsqueda",
        "paginate": {
            "first": "Primer",
            "last": "Último",
            "next": "Siguiente",
            "previous": "Anterior"
        }
    }
});




