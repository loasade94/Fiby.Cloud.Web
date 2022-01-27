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
                        swal('Paciente no encontrado', '', 'error');
                    } else {
                        $('#txtNombrePaciente').val(response.nombrePaciente)
                    }

                } else {
                    swal('Paciente no encontrado', '', 'error');
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