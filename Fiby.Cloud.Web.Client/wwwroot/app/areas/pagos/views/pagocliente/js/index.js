var pagoclientejs = {

    initializeEvent: function () {

        $("#btnBuscar").click(function () {
            pagoclientejs.buscarDetallePagoCliente();
        });

        $('#cboEmpleado').change(function () {
            pagoclientejs.buscarDetallePagoCliente();
            //calendariojs.limpiar();
        });

    },

    buscarDetallePagoCliente: function () {

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
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            url: '/Pagos/PagoEmpleado/BuscarPagosXEmpleadoSemana',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbPagosXCliente > tbody").html("");

                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            if (i == (response.length - 1)) {
                            } else {
                                html += '<tr>';
                                html += '    <td>';
                                html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                                html += '            <img src="/cssadmin/assets/images/edit.png" onclick="pagoclientejs.edit(\'' + response[i].idServicio + '\',\'' + 0 + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                                html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                                html += '        </div>';
                                html += '    </td>';
                                html += '    <td style="text-align:center;">' + response[i].nombreDia + ' ' + response[i].numeroDia + '</td>';
                                html += '    <td style="text-align:center;">' + response[i].cliente + '</td>';
                                html += '    <td style="text-align:center;">' + response[i].horas + '</td>';
                                html += '    <td style="text-align:center;">' + response[i].montoPagoCliente + '</td>';
                                html += '</tr>';
                            }



                        }
                        $('#tbPagosXCliente tbody').append(html);

                    } else {
                        ModalAlert("No se encontraron registros para la semana especififcada");
                    }

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
        pagoclientejs.openModal(idServicio, option);
     /*   $('html, body').animate({ scrollTop: 0 }, 'slow');*/
    },

    openModal: function (idServicio, option) {
        var url = '/Pagos/PagoCliente/ActualizarPagoCliente';
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

    actualizarPagoCliente: function (obj) {
        ModalConfirm('¿Seguro que desea actualizar el registro?', 'pagoclientejs.actualizarPagoCliente_callback();');
    },

    actualizarPagoCliente_callback: function () {

        var monto = $("#txtPagoClienteEditar").val().replace(",", ".");

        var pagoClienteDTORequest = {
            IdServicio: $("#hiddenServicioId").val(),
            MontoPagoClienteText: monto
        };

        $.ajax({
            type: "POST",
            data:
            {
                pagoClienteDTORequest
            },
            url: '/Pagos/PagoCliente/ActualizarPagoClienteXServicio',
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
                    pagoclientejs.buscarDetallePagoCliente();
                }
                else {
                    $('#modal-register').modal('hide');
                    ModalAlert('Error al actualizar : ' + response);
                    pagoclientejs.buscarDetallePagoCliente();
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

    pagoclientejs.initializeEvent();

});