var trabajadorjs = {

    initializeEvent: function () {

        //$("#btnRegistrar").click(function () {
        //    clientejs.registrarCliente();
        //});

        //clientejs.buscarCliente();

        CreateDataTable("tablaTrabajador");

    },

    openModal: function (idTrabajador) {
        $('#loading-screen').show();
        var url = '/Mantenimiento/Trabajador/RegisterUpdate';
        $.get(url, {
            idTrabajador: idTrabajador
        }, function (data) {
            $('#loading-screen').hide();
            createModalSize(data, "xs");

            if (idTrabajador > 0) {
                if ($('#cboPuesto').val() == $('#hiddenCodigoDoctor').val()) {
                    $('#dvEspecialidad').removeClass('is-hidden');
                } else {
                    $('#dvEspecialidad').addClass('is-hidden');
                    $('#dvEspecialidad').val('');
                }
            }
            //createSelect('cboStatusClassificationNew');
        });
    },

    registrarTrabajador: function () {

        var idTrabajador = $("#hiddenCodigoTrabajador").val();

        var txtDni = $("#txtDni").val();
        var txtNombres = $("#txtNombres").val();
        var txtApellidoPaterno = $("#txtApellidoPaterno").val();
        var txtApellidoMaterno = $("#txtApellidoMaterno").val();
        var cboTipoDocumento = $("#cboTipoDocumento").val();
        var txtTelefono = $("#txtTelefono").val();
        var cboSexo = $("#cboSexo").val();
        var cboEstado = $("#cboEstado").val();
        var cboPuesto = $("#cboPuesto").val();
        var cboEspecialidad = $("#cboEspecialidad").val();

        if (txtDni == null || txtDni == '') {
            swal('Debe ingresar un DNI', '', 'warning');
            return;
        }

        if (txtNombres == null || txtNombres == '') {
            swal('Debe ingresar nombres de trabajador', '', 'warning');
            return;
        }

        if (txtApellidoPaterno == null || txtApellidoPaterno == '') {
            swal('Debe ingresar apellido paterno de trabajador', '', 'warning');
            return;
        }

        if (txtApellidoMaterno == null || txtApellidoMaterno == '') {
            swal('Debe ingresar apellido materno de trabajador', '', 'warning');
            return;
        }

        if (cboTipoDocumento == null || cboTipoDocumento == '') {
            swal('Debe ingresar tipo de documento de trabajador', '', 'warning');
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

        if (cboPuesto == null || cboPuesto == '') {
            swal('Debe ingresar el puesto del trabajador', '', 'warning');
            return;
        }

        if (cboEstado == null || cboEstado == '') {
            swal('Debe ingresar el estado del trabajador', '', 'warning');
            return;
        }

        if (cboPuesto == $('#hiddenCodigoDoctor').val()) {
            if (cboEspecialidad == null || cboEspecialidad == '') {
                swal('Debe ingresar la especialidad del doctor', '', 'warning');
                return;
            }
        }

        var mensaje = "";

        if (idTrabajador == "0") {
            mensaje = "¿Esta seguro de registrar el trabajador?";
        } else {
            mensaje = "¿Esta seguro de actualizar el trabajador?";
        }

        //swal({
        //    title: mensaje,
        //    text: "",
        //    icon: "warning",
        //    buttons: true
        //})
        //    .then((willDelete) => {
        //        if (willDelete) {
        //            trabajadorjs.registrarTrabajador_callback();
        //        } else {
        //        }
        //    });
        swal({
            title: mensaje,
            icon: 'warning',
            buttons: {
                confirm: {
                    text: 'Si, continuar!',
                    className: 'btn btn-success'
                },
                cancel: {
                    visible: true,
                    className: 'btn btn-danger'
                }
            }
        }).then((Delete) => {
            if (Delete) {
                trabajadorjs.registrarTrabajador_callback();
            } else {
                swal.close();
            }
        });

    },

    registrarTrabajador_callback: function () {

        var txtDni = $("#txtDni").val();
        var txtNombres = $("#txtNombres").val();
        var txtApellidoPaterno = $("#txtApellidoPaterno").val();
        var txtApellidoMaterno = $("#txtApellidoMaterno").val();
        var cboTipoDocumento = $("#cboTipoDocumento").val();
        var txtTelefono = $("#txtTelefono").val();
        var cboSexo = $("#cboSexo").val();
        var cboPuesto = $("#cboPuesto").val();
        var cboEstado = $("#cboEstado").val();
        var cboEspecialidad = $("#cboEspecialidad").val();

        var idTrabajador = $("#hiddenCodigoTrabajador").val();

        var trabajadorDTORequest = {
            TipoTrabajador: cboTipoDocumento,
            Nombres: txtNombres,
            ApellidoPaterno: txtApellidoPaterno,
            ApellidoMaterno: txtApellidoMaterno,
            IdPuesto: cboPuesto,
            NumeroDocumento: txtDni,
            SituacionRegistro: cboEstado,
            EspecialidadMedica: cboEspecialidad,
            Telefono: txtTelefono,
            Sexo: cboSexo
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

    activarEspecialidad: function () {
        if ($('#cboPuesto').val() == $('#hiddenCodigoDoctor').val()) {
            $('#dvEspecialidad').removeClass('is-hidden');
        } else {
            $('#dvEspecialidad').addClass('is-hidden');
            $('#dvEspecialidad').val('');
        }
    },

}

$(function () {

    trabajadorjs.initializeEvent();

});