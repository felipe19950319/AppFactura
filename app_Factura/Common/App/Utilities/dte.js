$(document).ready(function() {
    dteClass = {
        dte: {
            version: '1.0',
            documento: {
                encabezado: {
                    iddoc: {
                        TipoDTE: 0,
                        Folio: 0,
                        FchEmis: null,
                        FmaPago: 0
                    },
                    emisor: {
                        RUTEmisor: null,
                        RznSoc: null,
                        GiroEmis: null,
                        Acteco: null,
                        DirOrigen: null,
                        CmnaOrigen: null,
                        CiudadOrigen: null
                    },
                    receptor: {
                        RUTRecep: null,
                        RznSocRecep: null,
                        GiroRecep: null,
                        DirRecep: null,
                        CmnaRecep: null,
                        CiudadRecep: null
                    },
                    totales: {
                        MntNeto: 0,
                        MntExe: 0,
                        TasaIVA: 0,
                        IVA: 0,
                        MntTotal: 0
                    }
                },
                ID: null
            },
            detalle: [],
            referencia: []
      
        }
    };

    fnGetJsonDte = function () {
        dteClass = {
            dte: {
                version: '1.0',
                TipoOperacion: '',
                documento: {
                    encabezado: {
                        iddoc: {
                            TipoDTE: 0,
                            Folio: 0,
                            FchEmis: null,
                            FmaPago: 0
                        },
                        emisor: {
                            RUTEmisor: null,
                            RznSoc: null,
                            GiroEmis: null,
                            Acteco: null,
                            DirOrigen: null,
                            CmnaOrigen: null,
                            CiudadOrigen: null
                        },
                        receptor: {
                            RUTRecep: null,
                            RznSocRecep: null,
                            GiroRecep: null,
                            DirRecep: null,
                            CmnaRecep: null,
                            CiudadRecep: null
                        },
                        totales: {
                            MntNeto: 0,
                            MntExe: 0,
                            TasaIVA: 0,
                            IVA: 0,
                            MntTotal: 0
                        }
                    },
                    ID: null,
                    detalle: [],
                    referencia: []
                }

            }
        }

        return dteClass;
    }

    Detalle = {
        NroLinDet: 0,
        NmbItem: null,
        DscItem: null,
        QtyItem: 0,
        PrcItem: 0,
        MontoItem: 0
    }

    var Host = 'http://localhost:57530/ws_Sii.svc';

    fnGetPreviewDte = function(ObjDte, callback) {
        $.ajax({
            type: 'POST',
            url: Host + '/GetPreviewDTE',
            data: JSON.stringify(ObjDte),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function(msg) {
                callback(JSON.parse(msg.d));
            },
            error: function(request, status, error) {
                callback(request.responseText);
            }
        });
    }

    fnSaveDocDte = function (ObjDte, callback) {
        $.ajax({
            type: 'POST',
            url: Host + '/SaveDocDte',
            data: JSON.stringify(ObjDte),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                callback(JSON.parse(msg.d));
            },
            error: function (request, status, error) {
                callback(request.responseText);
            }
        });
    }

    fnSavePfx = function (ObjCertificadoDigital, callback) {
        $.ajax({
            type: 'POST',
            url: Host + '/SavePfx',
            data: JSON.stringify(ObjCertificadoDigital),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (msg) {
                callback(JSON.parse(msg.d));
              
            },
            error: function (request, status, error) {
                callback(request.responseText);
            }
        });
    }

    fnAddDetalle = function(dteClass, Detalle) {
        dteClass.dte.detalle.push(Detalle);
    }
});