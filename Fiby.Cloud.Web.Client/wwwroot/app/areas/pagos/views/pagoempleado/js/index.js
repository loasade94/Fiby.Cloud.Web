var pagoempleadojs = {

    initializeEvent: function () {

        $("#btnBuscar").click(function () {
            pagoempleadojs.buscarPago();
        });

        $("#btnGenerarPDF").click(function () {
            pagoempleadojs.DownloadDocument();
        });

        $("#btnPagar").click(function () {
            pagoempleadojs.registrarPagoEmpleado();
        });

        $('#cboEmpleado').change(function () {
            pagoempleadojs.buscarPago();
            //calendariojs.limpiar();
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
                            html += '    <td style="text-align:center;"> S/. ' + response[i].pago + '</td>';
                            html += '    <td style="text-align:center;">' + formatDateTime(response[i].fecha) + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].descripcionEstado + '</td>';
                            html += '</tr>';

                            

                        }
                        $('#tbPagosXEmpleado tbody').append(html);

                    }

                    if (response.length == 0) {
                        html += '<tr>';
                        html += '    <td colspan="4" style="text-align:center;">NO PAGADO</td>';
                        html += '</tr>';
                        $('#tbPagosXEmpleado tbody').append(html);
                        $('#btnPagar').removeClass("is-hidden");
                        $('#btnGenerarPDF').addClass("is-hidden");

                    } else {
                        $('#btnPagar').addClass("is-hidden");
                        $('#btnGenerarPDF').removeClass("is-hidden");
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

                            if (i == (response.length - 1)) {
                                html += '<tr>';
                                html += '    <td colspan="5" style="text-align:center;">Totales</td>';
                                html += '    <td style="text-align:center;">' + response[i].horas + '</td>';
                                html += '    <td style="text-align:center;"> S/. ' + response[i].subTotal + '</td>';
                                html += '    <td style="text-align:center;"> S/. ' + response[i].pasaje + '</td>';
                                html += '    <td style="text-align:center;"> S/. ' + response[i].pago + '</td>';
                                html += '</tr>';
                            } else {
                                html += '<tr>';
                                html += '    <td>';
                                html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                                html += '            <img src="/cssadmin/assets/images/edit.png" onclick="pagoempleadojs.edit(\'' + response[i].idServicio + '\',\'' + 0 + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                                html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                                html += '        </div>';
                                html += '    </td>';
                                html += '    <td style="text-align:center;">' + response[i].nombreDia + ' ' + response[i].numeroDia + '</td>';
                                html += '    <td style="text-align:center;">' + response[i].cliente + '</td>';
                                html += '    <td style="text-align:center;">' + response[i].horaInicio + '</td>';
                                html += '    <td style="text-align:center;">' + response[i].horaFin + '</td>';
                                html += '    <td style="text-align:center;">' + response[i].horas + '</td>';
                                html += '    <td style="text-align:center;"> S/. ' + response[i].subTotal + '</td>';
                                html += '    <td style="text-align:center;"> S/. ' + response[i].pasaje + '</td>';
                                html += '    <td style="text-align:center;"> S/. ' + response[i].pago + '</td>';
                                html += '</tr>';
                            }

                            

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

    edit: function (idServicio, option) {
        pagoempleadojs.openModal(idServicio, option);
        /*$('html, body').animate({ scrollTop: 0 }, 'slow');*/
    },

    openModal: function (idServicio, option) {
        var url = '/Pagos/PagoEmpleado/ActualizarPasaje';
        $.get(url, {
            idServicio: idServicio, option: option
        }, function (data) {
            createModal(data);
/*            CreatePickaDate('txtFechaEditar');*/
            //CreatePickaTime('txtHoraInicioEditar');
            //CreatePickaTime('txtHoraFinEditar');
            //createSelect('cboRegistrationStatusNew');
        });
    },

    actualizarPasaje: function (obj) {
        ModalConfirm('¿Seguro que desea actualizar el registro?', 'pagoempleadojs.actualizarPasaje_callback();');
    },

    actualizarPasaje_callback: function () {

        var pasaje = $("#txtPasajeEditar").val().replace(",", ".");

        var pagoEmpleadoDTORequest = {
            IdServicio: $("#hiddenServicioId").val(),
            PasajeText: pasaje
        };

        $.ajax({
            type: "POST",
            data:
            {
                pagoEmpleadoDTORequest
            },
            url: '/Pagos/PagoEmpleado/ActualizarPasajeXServicio',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                    $('#modal-register').modal('hide');
                    ModalAlert("Actualizado correctamente");
                    pagoempleadojs.buscarPago();
                }
                else {
                    $('#modal-register').modal('hide');
                    ModalAlert('Error al actualizar : ' + response);
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

    DownloadDocument: function (en) {

        var IdEmpleado = $('#cboEmpleado').val();
        var IdSemana = $('#cboSemana').val();

        var form = document.createElement("FORM");

        var idEmpleadoValue = document.createElement("INPUT");
        idEmpleadoValue.type = "text";
        idEmpleadoValue.value = IdEmpleado;
        idEmpleadoValue.name = "idEmpleado";
        idEmpleadoValue.id = "idEmpleado";

        var idSemanaValue = document.createElement("INPUT");
        idSemanaValue.type = "text";
        idSemanaValue.value = IdSemana;
        idSemanaValue.name = "idSemana";
        idSemanaValue.id = "idSemana";


        form.style.display = "none";
        form.appendChild(idEmpleadoValue);
        form.appendChild(idSemanaValue);
        form.action = "/Pagos/PagoEmpleado/GenerarBoletaPagoPDF";
        form.method = 'POST';
        document.body.appendChild(form);
        form.submit();

    },
}

$(function () {

    pagoempleadojs.initializeEvent();

});