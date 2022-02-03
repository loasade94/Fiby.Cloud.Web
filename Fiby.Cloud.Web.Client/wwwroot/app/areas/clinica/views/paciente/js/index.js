var pacientejs = {

    initializeEvent: function () {

        //$("#btnRegistrar").click(function () {
        //    clientejs.registrarCliente();
        //});

        //clientejs.buscarCliente();

        CreateDataTable("tablaPaciente");

    },

    openModal: function (idPaciente) {
        var url = '/Clinica/Paciente/RegisterUpdate';
        $.get(url, {
            idPaciente: idPaciente
        }, function (data) {
            createModalSize(data, "xs");
            //createSelect('cboStatusClassificationNew');
        });
    },

    registrarPaciente: function () {

        var idPaciente = $("#hiddenCodigoPaciente").val();

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
            swal('Debe ingresar nombres de paciente', '', 'warning');
            return;
        }

        if (txtApellidos == null || txtApellidos == '') {
            swal('Debe ingresar apellidos de paciente', '', 'warning');
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
            swal('Debe ingresar el genero del paciente', '', 'warning');
            return;
        }

        if (cboEstado == null || cboEstado == '') {
            swal('Debe ingresar el estado del paciente', '', 'warning');
            return;
        }

        var mensaje = "";

        if (idPaciente == "0") {
            mensaje = "¿Esta seguro de registrar el paciente?";
        } else {
            mensaje = "¿Esta seguro de actualizar el paciente?";
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
                    pacientejs.registrarPaciente_callback();
                } else {
                    //swal("Your imaginary file is safe!");
                }
            });

    },

    registrarPaciente_callback: function () {

        var txtDni = $("#txtDni").val();
        var txtNombres = $("#txtNombres").val();
        var txtApellidos = $("#txtApellidos").val();
        var cboSeguro = $("#cboSeguro").val();
        var txtTelefono = $("#txtTelefono").val();
        var cboSexo = $("#cboSexo").val();
        var cboEstado = $("#cboEstado").val();

        var idPaciente = $("#hiddenCodigoPaciente").val();

        var pacienteDTORequest = {
            CodigoPaciente: idPaciente,
            DniPaciente: txtDni,
            NombrePaciente: txtNombres,
            ApellidoPaciente: txtApellidos,
            Seguro: cboSeguro,
            Telefono: txtTelefono,
            Sexo: cboSexo,
            SituacionRegistro: cboEstado
        };

        $.ajax({
            type: "POST",
            data:
            {
                pacienteDTORequest
            },
            url: '/Clinica/Paciente/GuardarPaciente',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            //complete: function () {
            //    $('#loading').hide();
            //},
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {

                    if (idPaciente == "0") {
                        swal('Registrado Correctamente', '', 'success');
                    } else {
                        swal('Actualizado Correctamente', '', 'success');
                    }

                    $('#modal-register').modal('hide');
                    pacientejs.buscarPaciente();
                }
                else {
                    swal('Error al registrar : ' + response, '', 'error');
                    //pacientejs.buscarPaciente();
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

    buscarPaciente: function () {

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
            url: '/Clinica/Paciente/GetPacienteAll',
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

    eliminarPaciente: function (obj) {
        //ModalConfirm('¿Seguro que desea eliminar el registro?', 'anunciojs.eliminarAnuncio_callback(\'' + obj + '\');');
        swal({
            title: "¿Seguro que desea eliminar la paciente?",
            text: "",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    pacientejs.eliminarPaciente_callback(obj);
                } else {
                    //swal("Your imaginary file is safe!");
                }
            });
    },

    eliminarPaciente_callback: function (obj) {

        var idPaciente = obj;

        $.ajax({
            type: "DELETE",
            data: {
                idPaciente
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Clinica/Paciente/EliminarPaciente',
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {

                    //ModalAlert("Eliminado Correctamente");
                    swal('Eliminado Correctamente', '', 'success');
                    pacientejs.buscarPaciente();
                } else {

                    swal('Error al registrar : ' + response, '', 'error');
                    pacientejs.buscarPaciente();
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

    pacientejs.initializeEvent();

});