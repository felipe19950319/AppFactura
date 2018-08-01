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
    
    RoundDecimal = function (Number) {
       return Math.floor(Number * 100) / 100;
    }

    FloatTryParse = function (Number)
    {
        var x = parseFloat(Number);
        if (x == "NaN") {
            return 0;
        }
        else {
            return x;
        }
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

    likeRut = function (rut, NumberSplit, dvSplit) {
        rut = rut.replace(/\-/g, '').replace(/\./g, '').replace(/\ /g, '');
        if (rut.length < 2) return rut;
        var nr = "", dig = 0;
        for (var x = rut.length - 2; x >= 0; x--) {
            dig++;
            if (dig > 3) {
                dig = 1;
                nr = NumberSplit + nr;
            }
            nr = rut[x] + nr;
        }
        return nr + dvSplit + rut[rut.length - 1].toLowerCase();
    }

    isNumeric = function (n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    isRut = function (campo) {
        campo = campo.replace(/\-/g, '').replace(/\./g, '').replace(/\ /g, '');
        if (campo.length < 2) return false;
        var rut = campo.substring(0, campo.length - 1), drut = campo.substring(campo.length - 1).toLowerCase(), dvr = '0', mul = 2, suma = 0;
        if (!isNumeric(rut) || (!isNumeric(drut) && drut != "k")) return false;

        for (i = rut.length - 1 ; i >= 0; i--) {
            suma = suma + rut.charAt(i) * mul;
            if (mul == 7)
                mul = 2;
            else
                mul++
        }
        res = suma % 11;
        if (res == 1)
            dvr = 'k';
        else if (res == 0)
            dvr = '0';
        else {
            dvi = 11 - res;
            dvr = dvi + "";
        }

        return dvr == drut;
    }


    GeneratePdf = function (base64,id) {
        var pdfData = atob(base64);

        // Using DocumentInitParameters object to load binary data.
        var loadingTask = PDFJS.getDocument({ data: pdfData });
        loadingTask.promise.then(function (pdf) {
            console.log('PDF loaded');

            // Fetch the first page
            var pageNumber = 1;
            pdf.getPage(pageNumber).then(function (page) {
                console.log('Page loaded');

                var scale = 1.5;
                var viewport = page.getViewport(scale);

                // Prepare canvas using PDF page dimensions
                var canvas = document.getElementById(id);
                var context = canvas.getContext('2d');
                canvas.height = viewport.height;
                canvas.width = viewport.width;

                // Render PDF page into canvas context
                var renderContext = {
                    canvasContext: context,
                    viewport: viewport
                };
                var renderTask = page.render(renderContext);
                renderTask.then(function () {
                    console.log('Page rendered');
                });
            });
        }, function (reason) {
            // PDF loading error
            console.error(reason);
        });
    }

});