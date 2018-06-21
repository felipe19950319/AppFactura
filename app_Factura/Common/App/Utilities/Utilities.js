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

 

});