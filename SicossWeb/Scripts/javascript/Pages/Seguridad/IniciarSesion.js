class IniciarSesion {
    IniciarSession() {
        var data = [];
        data.push(ItemParaAjax('usuario', $('#txtNombreUsuario').val()));
        data.push(ItemParaAjax('pass', $('#txtPasswordUsuario').val()));
        let urlPeticion = RaizDeAplicacion() + URLIniciarSesion
        LlamarAjax(
            urlPeticion,
            data,
            function (data) {
                console.log(data);
            }
        )
    }
    RegistrarUsuario() {
        let objeto = {};

        if ($('#txtNombreUsuario').val() === "") {
            alert("Debe ingresar un nombre de usuario")
            return;
        }

        if ($('#txtPassword').val() !== $('#txtPasswordConfirmar').val()) {
            alert("las contraseñas no coiciden")
            return;
        }
        objeto.Id = 0;
        objeto.Usuario = $('#txtNombreUsuario').val();
        objeto.Password = $('#txtPassword').val();
        var data = [];
        data.push(ItemParaAjax('modelo', JSON.stringify(objeto)));
        let urlPeticion = RaizDeAplicacion() + URLCrearUsuario
        LlamarAjax(
            urlPeticion,
            data,
            function (data) {
                console.log(data);
            }
        )
    }
}

$(document).ready(function () {
    let objectoIniciarSesion = new IniciarSesion();
    $('#btnAbrirModalRegistro').click(function () {
        $('#mdlUsuario').modal("show");
    });
    $('#btnAgregarUsuario').click(function () {
        objectoIniciarSesion.RegistrarUsuario();
    });
    $('#btnIniciarSesion').click(function () {
        objectoIniciarSesion.IniciarSession();
    });
});