function ItemParaAjax(nombre, valor) {
    return { name: nombre, value: valor }
}

function LlamarAjax(url, data, funcionExito) {
    //data.push(hsVariableSessionEncryptada());
    $.ajax({
        url: url
        , data: data
        , type: 'POST'
        , dataType: "json"
        , beforeSend: function () {
            MostrarImagenCargado();
        }
        , complete: function () {
            OcultarImagenCargado();
        }
        , success: function (data) {
            funcionExito(data);
        }
        , error: function (xhr, ajaxOptions, thrownError) {
            //ErrorDeAjax(xhr, ajaxOptions, thrownError);
        }
    });
}

function RaizDeAplicacion() {
    return $("#layoutRutaServidor").val();
}

function MostrarImagenCargado() {
    $("#contenedorLoading").prop("hidden", false);
}

function OcultarImagenCargado() {
    $("#contenedorLoading").prop("hidden", true);
}