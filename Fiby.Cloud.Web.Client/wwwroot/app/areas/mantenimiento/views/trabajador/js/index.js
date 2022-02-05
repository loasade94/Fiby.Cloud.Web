var trabajadorjs = {

    initializeEvent: function () {

        //$("#btnRegistrar").click(function () {
        //    clientejs.registrarCliente();
        //});

        //clientejs.buscarCliente();

        CreateDataTable("tablaTrabajador");

    },

    openModal: function (idTrabajador) {
        var url = '/Mantenimiento/Trabajador/RegisterUpdate';
        $.get(url, {
            idTrabajador: idTrabajador
        }, function (data) {
            createModalSize(data, "xs");
            //createSelect('cboStatusClassificationNew');
        });
    },

    registrarTrabajador: function () {

        var idTrabajador = $("#hiddenCodigoTrabajador").val();

        var txtDni = $("#txtDni").val();
        var txtNombres = $("#txtNombres").val();
        var txtApellidos = $("#txtApellidos").val();
        var cboSeguro = $("#cboSeguro").val();
        var txtTelefono = $("#txtTelefono").val();
        var cboSexo = $("#cboSexo").val();
        var cboEstado = $("#cboEstado").val();

        if (txtDni == null || txtDni == '') {
            swal('Debe ingresar un DNI', '', 'warning');
            return;
        }

        if (txtNombres == null || txtNombres == '') {
            swal('Debe ingresar nombres de trabajador', '', 'warning');
            return;
        }

        if (txtApellidos == null || txtApellidos == '') {
            swal('Debe ingresar apellidos de trabajador', '', 'warning');
            return;
        }

        if (cboSeguro == null || cboSeguro == '') {
            swal('Debe seleccionar si aplica seguro', '', 'warning');
            return;
        }

        if (txtTelefono == null || txtTelefono == '') {
            swal('Debe ingresar un telefono', '', 'warning');
            return;
        }

        if (cboSexo == null || cboSexo == '') {
            swal('Debe ingresar el genero del trabajador', '', 'warning');
            return;
        }

        if (cboEstado == null || cboEstado == '') {
            swal('Debe ingresar el estado del trabajador', '', 'warning');
            return;
        }

        var mensaje = "";

        if (idTrabajador == "0") {
            mensaje = "¿Esta seguro de registrar el trabajador?";
        } else {
            mensaje = "¿Esta seguro de actualizar el trabajador?";
        }

        swal({
            title: mensaje,
            text: "",
            icon: "warning",
            buttons: true
            //dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    trabajadorjs.registrarTrabajador_callback();
                } else {
                    //swal("Your imaginary file is safe!");
                }
            });

    },

    registrarTrabajador_callback: function () {

        var txtDni = $("#txtDni").val();
        var txtNombres = $("#txtNombres").val();
        var txtApellidos = $("#txtApellidos").val();
        var cboSeguro = $("#cboSeguro").val();
        var txtTelefono = $("#txtTelefono").val();
        var cboSexo = $("#cboSexo").val();
        var cboEstado = $("#cboEstado").val();

        var idTrabajador = $("#hiddenCodigoTrabajador").val();

        var trabajadorDTORequest = {
            CodigoTrabajador: idTrabajador,
            DniTrabajador: txtDni,
            NombreTrabajador: txtNombres,
            ApellidoTrabajador: txtApellidos,
            Seguro: cboSeguro,
            Telefono: txtTelefono,
            Sexo: cboSexo,
            SituacionRegistro: cboEstado
        };

        $.ajax({
            type: "POST",
            data:
            {
                trabajadorDTORequest
            },
            url: '/Mantenimiento/Trabajador/GuardarTrabajador',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            //complete: function () {
            //    $('#loading').hide();
            //},
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {

                    if (idTrabajador == "0") {
                        swal('Registrado Correctamente', '', 'success');
                    } else {
                        swal('Actualizado Correctamente', '', 'success');
                    }

                    $('#modal-register').modal('hide');
                    trabajadorjs.buscarTrabajador();
                }
                else {
                    swal('Error al registrar : ' + response, '', 'error');
                    //trabajadorjs.buscarTrabajador();
                }
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    buscarTrabajador: function () {

        //var txtDescriptionClassification = $("#txtDescriptionClassification").val();
        //var cboStatusClassification = $("#cboStatusClassification").val();

        //var classificationDTORequest = {
        //    Description: txtDescriptionClassification,
        //    StatusReg: cboStatusClassification
        //};

        $.ajax({
            type: "POST",
            //data:
            //{
            //    classificationDTORequest
            //},
            url: '/Mantenimiento/Trabajador/GetTrabajadorAll',
            // dataType: "json",
            beforeSend: function () {
                $('#loading-screen').show();
            },
            complete: function () {
                $('#loading-screen').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#dvGrid").html(response);
                }
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading-screen').hide();
            },
            async: true,
        })
    },

    eliminarTrabajador: function (obj) {
        //ModalConfirm('¿Seguro que desea eliminar el registro?', 'anunciojs.eliminarAnuncio_callback(\'' + obj + '\');');
        swal({
            title: "¿Seguro que desea eliminar la trabajador?",
            text: "",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    trabajadorjs.eliminarTrabajador_callback(obj);
                } else {
                    //swal("Your imaginary file is safe!");
                }
            });
    },

    eliminarTrabajador_callback: function (obj) {

        var idTrabajador = obj;

        $.ajax({
            type: "DELETE",
            data: {
                idTrabajador
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Mantenimiento/Trabajador/EliminarTrabajador',
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {

                    //ModalAlert("Eliminado Correctamente");
                    swal('Eliminado Correctamente', '', 'success');
                    trabajadorjs.buscarTrabajador();
                } else {

                    swal('Error al registrar : ' + response, '', 'error');
                    trabajadorjs.buscarTrabajador();
                }

            },
            complete: function () {
                $('#loading').hide();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                IziToastMessage(1, 'Ocurrio un error.', '', 'topRight', 5000);
                $('#loading').hide();

            },
            async: true,
        });


    },

}

$(function () {

    trabajadorjs.initializeEvent();

});