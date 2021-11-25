var calendariojs = {

    initializeEvent: function () {

        $("#btnGrabar").click(function () {
            calendariojs.registrarServicio();
        });

        $("#btnBuscar").click(function () {
            calendariojs.buscarServicio();
        });

        $("#btnLimpiar").click(function () {
            calendariojs.limpiar();
        });

        //$("#btnNewRol").click(function () {
        //    roljs.openModal();
        //});

        calendariojs.buscarServicio();

        CreatePickaDate('txtFecha');
        //CreatePickaTime('txtHoraInicio');
        //CreatePickaTime('txtHoraFin');

        $('#cboEmpleado').change(function () {
            calendariojs.buscarServicio();
            //calendariojs.limpiar();
        });

        $('#txtHoraInicio').change(function () {

            if ($('#txtHoras').val() != "0" && $('#txtHoras').val() != null) {
                calendariojs.calcularFin();
            }
        });

        $('#txtHoras').change(function () {

            if ($('#txtHoras').val() != "0" && $('#txtHoras').val() != null) {
                calendariojs.calcularFin();
            }
        });

    },

    edit: function (idServicio, option) {
        calendariojs.openModal(idServicio, option);
        //$('html, body').animate({ scrollTop: 0 }, 'slow');
    },

    openModal: function (idServicio, option) {
        var url = '/Horario/Calendario/Actualizar';
        $.get(url, {
            idServicio: idServicio, option: option
        }, function (data) {
            createModal(data);
            CreatePickaDate('txtFechaEditar');
            //CreatePickaTime('txtHoraInicioEditar');
            //CreatePickaTime('txtHoraFinEditar');
            //createSelect('cboRegistrationStatusNew');
        });
    },

    registrarServicio: function (obj) {
        var cliente = $("#txtCliente").val();

        if (cliente == null || cliente == '') {
            ModalAlertCancel('Debe Ingresar un Cliente');
            return;
        }


        ModalConfirm('¿Seguro que desea registrar?', 'calendariojs.registrarServicio_callback();');
    },

    registrarServicio_callback: function () {

        var calendarioDTORequest = {
            IdEmpleado: $("#cboEmpleado").val(),
            ClienteOpcional: $("#txtCliente").val(),
            Descripcion: $("#txtDesServicio").val(),
            FechaText: $("#txtFecha").val(),
            HoraInicio: $("#txtHoraInicio").val(),
            HoraFin: $("#txtHoraFin").val()
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                calendarioDTORequest
            },
            url: '/Horario/Calendario/RegistrarServicio',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            //complete: function () {
            //    $('#loading').hide();
            //},
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                    ModalAlert('Registrado Correctamente');
                    calendariojs.buscarServicio();
                    calendariojs.limpiar();
                }
                else
                {
                    ModalAlertCancel('Error al registrar : ' + response);
                    calendariojs.buscarServicio();
                }

/*                calendariojs.buscarServicio();*/
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    buscarServicio: function () {

        var calendarioDTORequest = {
            IdEmpleado: $("#cboEmpleado").val(),
            FechaText: $("#txtFecha").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                calendarioDTORequest
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Horario/Calendario/BuscarServicioXEmpleado',
            success: function (response, textStatus, jqXhr) {

                //var myCalendar = $('#calendar');

                if (response != null) {
                    $("#tbServiciosPorEmpleado > tbody").html("");
                    $('#tbServiciosPorEmpleado').DataTable().clear().destroy();

                    //$("#tbServiciosPorEmpleadoTotales > tbody").html("");
                    //$('#tbServiciosPorEmpleadoTotales').DataTable().clear().destroy();
                    if (response.length > 0) {


                        var indice = 0;

                        var html = "";
/*                        var htmlTotal = "";*/
                        var eje = "";
                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td>';
                            html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                            html += '            <img src="/cssadmin/assets/images/edit.png" onclick="calendariojs.edit(\'' + response[i].idServicio + '\',\'' + 0 + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                            html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                            html += '            <img src="/cssadmin/assets/images/trash.png" onclick="calendariojs.eliminarCalendario(\'' + response[i].idServicio + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
                            html += '                      data-toggle="tooltip" title="Eliminar">';
                            html += '        </div>';
                            html += '    </td>';
                            html += '    <td style="text-align:center;">' + response[i].nombreDia + ' ' + response[i].numeroDia + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].clienteOpcional + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].horaInicio + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].horaFin + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].descripcion + '</td>';
                            html += '</tr>';
                            indice += 1;
                            
                            //var myEvent = {
                            //    title: response[i].clienteOpcional,
                            //    start: response[i].fechaInicio,
                            //    end: response[i].fechaFin
                            //};

                            //myCalendar.fullCalendar('renderEvent', myEvent);

                        }
                        $('#tbServiciosPorEmpleado tbody').append(html);

                        //if (indice > 0) {
                        //    htmlTotal += '<tr>';
                        //    htmlTotal += '    <td style="text-align:center;">' + response[0].totalHorasDia + '</td>';
                        //    htmlTotal += '    <td style="text-align:center;">' + response[0].totalHoraSemana + '</td>';
                        //    htmlTotal += '    <td style="text-align:center;">' + response[0].totalHoraMes + '</td>';
                        //    htmlTotal += '</tr>';
                        //}

                        //$('#tbServiciosPorEmpleadoTotales tbody').append(htmlTotal);
                    }
                    calendariojs.intitDataTable();
                }
            },
            complete: function () {
                calendariojs.buscarServicioCalendario();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    buscarServicioCalendario: function () {

        var calendarioDTORequest = {
            IdEmpleado: $("#cboEmpleado").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                calendarioDTORequest
            },
            url: '/Horario/Calendario/BuscarServicioXEmpleadoCalendario',
            success: function (response, textStatus, jqXhr) {

                var myCalendar = $('#calendar');
                $('#calendar').fullCalendar('removeEvents');
                if (response != null) {
                   

                    if (response.length > 0) {

                        $('#calendar').fullCalendar('removeEvents');

                        for (var i = 0; i < response.length; i++) {


                            var myEvent = {
                                title: response[i].clienteOpcional,
                                start: response[i].fechaInicio,
                                end: response[i].fechaFin
                            };

                            myCalendar.fullCalendar('renderEvent', myEvent);

                        }

                    }

                }
            },
            complete: function () {
                calendariojs.buscarServicioTotal();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    buscarServicioTotal: function () {

        var calendarioDTORequest = {
            IdEmpleado: $("#cboEmpleado").val(),
            FechaText: $("#txtFecha").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                calendarioDTORequest
            },
            url: '/Horario/Calendario/BuscarServicioXEmpleadoTotales',
            success: function (response, textStatus, jqXhr) {

                //var myCalendar = $('#calendar');

                if (response != null) {
                    $("#tbServiciosPorEmpleadoTotales > tbody").html("");
                    $('#tbServiciosPorEmpleadoTotales').DataTable().clear().destroy();

                    if (response.length > 0) {

                        var html = "";
                        var eje = "";
                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td style="text-align:center;">' + response[i].totalHorasDia + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].totalHoraSemana + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].totalHoraMes + '</td>';
                            html += '</tr>';

                        }
                        $('#tbServiciosPorEmpleadoTotales tbody').append(html);
                    }
                }
            },
            complete: function () {
                $('#loading').hide();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    intitDataTable: function () {
        CreateDataTable('tbServiciosPorEmpleado');
    },

    eliminarCalendario: function (obj) {
        ModalConfirm('¿Seguro que desea eliminar el registro?', 'calendariojs.eliminarCalendario_callback(\'' + obj + '\');');
    },

    eliminarCalendario_callback: function (obj) {

        var idServicio = obj;

        $.ajax({
            type: "DELETE",
            data: {
                idServicio
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Horario/Calendario/EliminarServicio',
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                    /*IziToastMessage(0, 'Eliminado Correctamente', 'green', 'topRight', 5000);*/
                    ModalAlert("Eliminado Correctamente");
                    calendariojs.buscarServicio();
                } else {
                    /* IziToastMessage(1, 'Ocurrio un error: ' + response, '', 'topRight', 5000);*/
                    ModalAlert("ERROR");
                    calendariojs.buscarServicio();
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

    actualizarServicio: function (obj) {
        ModalConfirm('¿Seguro que desea actualizar el registro?', 'calendariojs.actualizarServicio_callback();');
    },

    actualizarServicio_callback: function () {

        var calendarioDTORequest = {
            IdServicio: $("#hiddenServicioId").val(),
            ClienteOpcional: $("#txtClienteEditar").val(),
            Descripcion: $("#txtDetalleEditar").val(),
            FechaText: $("#txtFechaEditar").val(),
            HoraInicio: $("#txtHoraInicioEditar").val(),
            HoraFin: $("#txtHoraFinEditar").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                calendarioDTORequest
            },
            url: '/Horario/Calendario/RegistrarServicio',
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
                    calendariojs.buscarServicio();
                }
                else {
                    $('#modal-register').modal('hide');
                    ModalAlert('Error al actualizar : ' + response);
                    calendariojs.buscarServicio();
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

    limpiar: function () {
        $('#txtHoraInicio').val('06:00');
        $('#txtHoraFin').val('06:00');
        $('#txtCliente').val('');
        $('#txtDesServicio').val('');
        $('#txtHoras').val('0');

        calendariojs.buscarServicio();
    },

    calcularFin: function () {

        $.ajax({
            type: "POST",
            data:
            {
                fecha: CurrentDateFormat($('#txtFecha').val()),
                horaInicio: $('#txtHoraInicio').val(),
                horas: $('#txtHoras').val()
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Horario/Calendario/CalcularHoraFin',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $('#txtHoraFin').val(response);
                }
            },
            complete: function () {
                calendariojs.buscarServicioCalendario();
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

    calendariojs.initializeEvent();

});