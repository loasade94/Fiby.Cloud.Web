var pagoempleadojs = {

    initializeEvent: function () {

        $("#btnBuscar").click(function () {
            pagoempleadojs.buscarPago();
        });

        $("#btnPagar").click(function () {
            pagoempleadojs.registrarPagoEmpleado();
        });

        
    },

    buscarPago: function () {

        var pagoEmpleadoDTORequest = {
            IdEmpleado: $('#cboEmpleado').val(),
            IdSemana: $('#cboSemana').val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                pagoEmpleadoDTORequest
            },
            url: '/Pagos/PagoEmpleado/BuscarPagosXEmpleadoSemanaCab',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbPagosXEmpleado > tbody").html("");

                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td style="text-align:center;">' + response[i].horas + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].pago + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].descripcionEstado + '</td>';
                            html += '</tr>';

                        }
                        $('#tbPagosXEmpleado tbody').append(html);

                    }

                }
            },
            complete: function () {
                pagoempleadojs.buscarDetallePago();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    buscarDetallePago: function () {

        var pagoEmpleadoDTORequest = {
            IdEmpleado: $('#cboEmpleado').val(),
            IdSemana: $('#cboSemana').val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                pagoEmpleadoDTORequest
            },
            url: '/Pagos/PagoEmpleado/BuscarPagosXEmpleadoSemana',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbPagosXEmpleadoDetalle > tbody").html("");

                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td style="text-align:center;">' + response[i].nombreDia + ' ' + response[i].numeroDia + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].cliente + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].horaInicio + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].horaFin + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].horas + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].pago + '</td>';
                            html += '</tr>';

                        }
                        $('#tbPagosXEmpleadoDetalle tbody').append(html);

                    }

                }
            },
            complete: function () {

            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    registrarPagoEmpleado: function () {
        ModalConfirm('¿Seguro que desea registrar el pago?', 'pagoempleadojs.registrarPagoEmpleado_callback();');
    },

    registrarPagoEmpleado_callback: function () {

        var pagoEmpleadoDTORequest = {
            IdEmpleado: $("#cboEmpleado").val(),
            IdSemana: $("#cboSemana").val(),
            Estado: 3
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                pagoEmpleadoDTORequest
            },
            url: '/Pagos/PagoEmpleado/RegistrarPagoEmpleado',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                    ModalAlert('Registrado Correctamente');
                    pagoempleadojs.buscarPago();
                    //calendariojs.limpiar();
                }
                else {
                    ModalAlert('Error al registrar : ' + response);
                    pagoempleadojs.buscarPago();
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

}

$(function () {

    pagoempleadojs.initializeEvent();

});