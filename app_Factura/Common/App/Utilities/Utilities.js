$(document).ready(function () {
     StringTob64=function(str) {
        return window.btoa(unescape(encodeURIComponent(str)));
    }

    ServerSide = function (page, method, jsonObject, callback) {
       // console.log(jsonObject);
        var json64 = StringTob64(JSON.stringify(jsonObject));

        $.ajax({
            type: 'POST',
            url: page + '/' + method,
            async:false,
            data:JSON.stringify( {json:json64}),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                callback(msg);
            },
            error: function (request, status, error) {
                callback(request.responseText);
            }
        });
    }

    Date.prototype.yyyymmdd = function (separator) {
        var mm = this.getMonth() + 1; // getMonth() is zero-based
        var dd = this.getDate();

        return [this.getFullYear(),
                (mm > 9 ? '' : '0') + mm,
                (dd > 9 ? '' : '0') + dd
        ].join(separator);
    };


    MakeTable = function (pageLength,data,ColumnsDefs,Id) {
        var table = $(Id).DataTable({
            "destroy": true,
            "pageLength": pageLength,
            "lengthChange": false,
            "searching": false,
            "responsive":true,
            "info": false,
            "data": data,
            "bAutoWidth": false,
            "columns": ColumnsDefs,
            "language": {
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

       return table;

    }

    GetCellIndexByName = function (table,IndexName) {
        return table.column(':contains(' + IndexName+')')[0][0];
    } 

    SetCellValue = function(table,RowIndex,ColIndex,value) {
        table.cell({ row: RowIndex, column: ColIndex }).data(value).draw();
    }

});