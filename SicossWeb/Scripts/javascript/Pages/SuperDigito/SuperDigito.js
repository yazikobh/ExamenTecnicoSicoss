class SuperDigito {
    constructor(_modelUsuario) {
        this.ModelUsuario = JSON.parse(_modelUsuario)
    }

    ObtenerHistorialUsuario() {
        let me = this;
        let data = [];
        data.push(ItemParaAjax('idUsuario', me.ModelUsuario.Id));
        let urlPeticion = RaizDeAplicacion() + URLObtenerHistorialUsuario;
        LlamarAjax(
            urlPeticion,
            data,
            function (data) {
                if (!data.Exito) {
                    alert(data.Mensaje)
                    return;
                }
                console.log(data);
                me.LlenarTabla('#bodyTable', data.Valor);
            }
        )
    }

    CalcularSuperDigito(digito) {
        let me = this;
        let data = [];
        data.push(ItemParaAjax('digito', digito));
        data.push(ItemParaAjax('idUsuario', me.ModelUsuario.Id));
        let urlPeticion = RaizDeAplicacion() + URLCalcularSuperDigito;
        LlamarAjax(
            urlPeticion,
            data,
            function (data) {
                if (!data.Exito) {
                    alert(data.Mensaje)
                    return;
                }
                console.log(data);
                $('#txtResultado').val(data.Valor);
                me.ObtenerHistorialUsuario();
            }
        )
    }

    LlenarTabla(selector, data) { 
        $(selector).empty();
        let inicioTr = "<tr>";
        let finTr = "</tr>";
        let body = "";
        let iniciotd = "<td>";
        let fintd = "</td>";
        for (var i = 0; i < data.length; i++) {
            body = body + inicioTr
                + iniciotd + GenerarBotonesOperaciones(data[i].Id, data[i].Digito) + fintd
                + iniciotd + data[i].Digito + fintd
                + iniciotd + data[i].Resultado + fintd
                + iniciotd + data[i].FechaConsulta + fintd
                + finTr;
        }
        $(selector).append(body);
    }

    ConsultarDigito(Digito) {
        let me = this;
        let data = [];
        data.push(ItemParaAjax('digito', Digito));
        let urlPeticion = RaizDeAplicacion() + URLConsultaPorDigito;
        LlamarAjax(
            urlPeticion,
            data,
            function (data) {
                if (data.Codigo == 0) {
                    $('#txtResultado').val(data.Valor.Resultado);
                    me.ObtenerHistorialUsuario();
                }
                if (data.Codigo == 2) {
                    me.CalcularSuperDigito(Digito)
                }
            }
        )
    }
    EliminarPorId(Id) {
        let me = this;
        let data = [];
        data.push(ItemParaAjax('idRegistro', Id));
        let urlPeticion = RaizDeAplicacion() + URLEliminarHistorialPorId;
        LlamarAjax(
            urlPeticion,
            data,
            function (data) {
                if (data.Codigo == 0) {
                    me.ObtenerHistorialUsuario();
                }
            }
        )
    }

    EliminarPorUsuario() {
        let me = this;
        let data = [];
        data.push(ItemParaAjax('idUsuario', me.ModelUsuario.Id));
        let urlPeticion = RaizDeAplicacion() + URLEliminarHistorialPorUsuario;
        LlamarAjax(
            urlPeticion,
            data,
            function (data) {
                if (data.Codigo == 0) {
                    me.ObtenerHistorialUsuario();
                }
            }
        )
    }

    SuperDigitoDetalle(digito) {
        let me = this;
        let data = [];
        data.push(ItemParaAjax('digito', digito));
        let urlPeticion = RaizDeAplicacion() + URLConsultarDetalleSuperDigito;
        LlamarAjax(
            urlPeticion,
            data,
            function (data) {
                if (data.Codigo == 0) {
                    swal("Detalle", data.Valor);
                }
            }
        )
    }

}

$(document).ready(function () {
    var objecto = new SuperDigito($('#ModelUsuario').val())
    objecto.ObtenerHistorialUsuario();
    $('#btnCalcularSuperDigito').click(function () {
        $('#txtResultado').val('');
        let digito = $('#txtDigito').val();
        objecto.ConsultarDigito(digito);
    });

    $('#bodyTable').on('click', '[id^="eliminar"]', function () {
        if (confirm("Se limpiará el registro, ¿Está seguro?")) {

        }
        let dataset = $(this)[0].dataset;
        objecto.EliminarPorId(dataset.id);
    });

    $('#btnLimpiarHistorial').click(function () {
        if (confirm("Se limpiará el historial, ¿Está seguro?")) {
            objecto.EliminarPorUsuario();
        }
        
    });

    $('#bodyTable').on('click', '[id^="detalle"]', function () {
        let dataset = $(this)[0].dataset;
        objecto.SuperDigitoDetalle(dataset.digito);
    });

});