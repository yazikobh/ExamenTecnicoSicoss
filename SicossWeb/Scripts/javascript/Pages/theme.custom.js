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

function GenerarBotonesOperaciones(id,digito) {
    let elementoDivInicio = '<div class="botones" style="display:flex;flex-direction: row;justify-content: center;gap:1rem">'
    let elementoDivFin = '</div>'
    let botonEditar = '<button id="detalle_' + id + '" data-id="' + id + '" data-digito="' + digito+'" class="btn btn-primary"> Detalle</button>';
    let botonEliminar = '<button id="eliminar' + id + '" data-id="' + id + '" class="btn btn-danger"> Eliminar</button>';
    let resultado = elementoDivInicio + botonEditar + botonEliminar + elementoDivFin;
    return resultado;
}