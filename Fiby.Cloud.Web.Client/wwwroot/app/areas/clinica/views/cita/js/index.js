var citajs = {

    initializeEvent: function () {

        //$("#btnRegistrar").click(function () {
        //    clientejs.registrarCliente();
        //});

        //clientejs.buscarCliente();

        CreateDataTable("tablaCita");

    },

    openModal: function (idCita) {
        var url = '/Clinica/Cita/RegisterUpdate';
        $.get(url, {
            idCita: idCita
        }, function (data) {
            createModalSize(data, "xs");
            //createSelect('cboStatusClassificationNew');
        });
    },

    buscarPacienteModal: function () {

        var pacienteDTORequest = {
            DocumentoPaciente: $('#txtDniPaciente').val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                pacienteDTORequest
            },
            url: '/Clinica/Cita/GetPacientePorDocumento',
            // dataType: "json",
            beforeSend: function () {
                $('#loading-screen').show();
            },
            complete: function () {
                $('#loading-screen').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response != null) {

                    if (response.nombrePaciente == null
                        || response.nombrePaciente == "") {
                        swal('Paciente no encontrado', '', 'warning');
                    } else {
                        $('#pCodigoPaciente').val(response.codigoPaciente);
                        $('#txtNombrePaciente').val(response.nombrePaciente);
                    }

                } else {
                    swal('Paciente no encontrado', '', 'warning');
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

    buscarDoctorEspecialidadModal: function () {

        var combo = $('#cboMedicos');

        if ($('#cboEspecialidad').val() == "0") {
            combo.html('');
            return;
        }

        var doctorDTORequest = {
            CodigoEspecialidad: $('#cboEspecialidad').val()
        };

        

        combo.html('');

        $.ajax({
            type: "POST",
            data:
            {
                doctorDTORequest
            },
            url: '/Clinica/Cita/GetDoctorPorEspecialidad',
            // dataType: "json",
            beforeSend: function () {
                $('#loading-screen').show();
            },
            complete: function () {
                $('#loading-screen').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response != null) {

                    if (response.length > 0) {
                        var html = '';
                        for (var i = 0; i < response.length; i++) {
                            html += '<option value="' + response[0].codigoDoctor + '">' + response[0].nombreDoctor  +'</option>';
                            combo.append(html);
                        }

                    } else {
                        swal('No hay doctores para esa especialidad', '', 'warning');
                    }

                } else {
                    swal('No hay doctores para esa especialidad', '', 'warning');
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

    registrarCita: function () {
        
        var idCita = $("#hiddenCitaId").val();

        var fecha = $("#txtFecha").val();
        var txtHora = $("#txtHora").val();
        var paciente = $("#pCodigoPaciente").val();
        var especialidad = $("#cboEspecialidad").val();
        var medico = $("#cboMedicos").val();

        if (fecha == null || fecha == '') {
            swal('Debe Ingresar una Fecha', '', 'warning');
            return;
        }

        if (txtHora == null || txtHora == '') {
            swal('Debe Ingresar una Hora', '', 'warning');
            return;
        }

        if (paciente == null || paciente == '0') {
            swal('Debe Ingresar un Paciente', '', 'warning');
            return;
        }

        if (especialidad == null || especialidad == '0') {
            swal('Debe Ingresar una Especialidad', '', 'warning');
            return;
        }

        if (medico == null || medico == '') {
            swal('Debe Ingresar un Medico', '', 'warning');
            return;
        }

        var mensaje = "";

        if (idCita == "0") {
            mensaje = "¿Esta seguro de registrar la cita?";
        } else {
            mensaje = "¿Esta seguro de actualizar la cita?";
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
                    citajs.registrarCita_callback();
                } else {
                    //swal("Your imaginary file is safe!");
                }
            });

    },

    registrarCita_callback: function () {

        var fecha = formatDateTime($("#txtFecha").val());
        var txtHora = $("#txtHora").val();
        var paciente = $("#pCodigoPaciente").val();
        var especialidad = $("#cboEspecialidad").val();
        var medico = $("#cboMedicos").val();

        var idCita = $("#hiddenCitaId").val();

        var citaDTORequest = {
            IdCita: idCita,
            FechaCitaText: fecha,
            Hora: txtHora,
            CodigoEspecialidad: especialidad,
            CodigoUnicoDoctor: medico,
            CodigoPaciente: paciente
        };

        $.ajax({
            type: "POST",
            data:
            {
                citaDTORequest
            },
            url: '/Clinica/Cita/GuardarCita',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            //complete: function () {
            //    $('#loading').hide();
            //},
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {

                    if (idCita == "0") {
                        swal('Registrado Correctamente', '', 'success');
                    } else {
                        swal('Actualizado Correctamente', '', 'success');
                    }

                    $('#modal-register').modal('hide');
                    citajs.buscarCita();
                }
                else {
                    swal('Error al registrar : ' + response, '', 'error');
                    //citajs.buscarCita();
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

    buscarCita: function () {

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
            url: '/Clinica/Cita/GetCitaAll',
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

    eliminarCita: function (obj) {
        //ModalConfirm('¿Seguro que desea eliminar el registro?', 'anunciojs.eliminarAnuncio_callback(\'' + obj + '\');');
        swal({
            title: "¿Seguro que desea eliminar la cita?",
            text: "",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    citajs.eliminarCita_callback(obj);
                } else {
                    //swal("Your imaginary file is safe!");
                }
            });
    },

    eliminarCita_callback: function (obj) {

        var idCita = obj;

        $.ajax({
            type: "DELETE",
            data: {
                idCita
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Clinica/Cita/EliminarCita',
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                   
                    //ModalAlert("Eliminado Correctamente");
                    swal('Eliminado Correctamente', '', 'success');
                    citajs.buscarCita();
                } else {

                    swal('Error al registrar : ' + response, '', 'error');
                    citajs.buscarCita();
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

    citajs.initializeEvent();

});