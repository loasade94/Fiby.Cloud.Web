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
                        $('#txtNombrePaciente').val(response.nombrePaciente)
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

}

$(function () {

    citajs.initializeEvent();

});