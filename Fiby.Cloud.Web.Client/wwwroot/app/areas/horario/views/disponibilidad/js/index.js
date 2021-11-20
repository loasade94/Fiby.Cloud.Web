var disponibilidadjs = {

    initializeEvent: function () {

        $('#cboSemana').change(function () {
            disponibilidadjs.buscarCabeceras();
        });

        disponibilidadjs.buscarCabeceras();
    },

    buscarCabeceras: function () {

        var semanaDTORequest = {
            IdSemana: $('#cboSemana').val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                semanaDTORequest
            },
            url: '/Horario/Disponibilidad/GetListaDiasXSemana',
            beforeSend: function () {
                $('#loading').show();
            },
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbDisponibilidadSemana > thead").html("");

                    if (response.length > 0) {

                        var html = "";

                        html += '<tr>';

                        for (var i = 0; i < response.length; i++) {

                            //html += '    <td style="text-align:center;"><b>' + response[i].horario + '</b></td>';
                            html += '<th style="cursor:pointer;" onclick="disponibilidadjs.agregar_servicio(\'' + formatDateTime(response[i].fecha) + '\');" class="text-center"><b>' + response[i].dia + '</b></th>';
                            
                        }

                        html += '</tr>';
                        $('#tbDisponibilidadSemana thead').append(html);

                    }

                }
            },
            complete: function () {
                disponibilidadjs.buscarDisponibilidad();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },
    
    buscarDisponibilidad: function () {

        var semanaDTORequest = {
            IdSemana: $('#cboSemana').val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                semanaDTORequest
            },
            url: '/Horario/Disponibilidad/GetDisponibilidadSemana',
            //beforeSend: function () {
            //    $('#loading').show();
            //},
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbDisponibilidadSemana > tbody").html("");
                    
                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td style="text-align:center;"><b>' + response[i].horario + '</b></td>';

                            if (response[i].lunes == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:white;text-align:center;">' + response[i].lunes + '</td>';
                            }

                            if (response[i].martes == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:white;text-align:center;">' + response[i].martes + '</td>';
                            }

                            if (response[i].miercoles == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:white;text-align:center;">' + response[i].miercoles + '</td>';
                            }

                            if (response[i].jueves == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:white;text-align:center;">' + response[i].jueves + '</td>';
                            }

                            if (response[i].viernes == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:white;text-align:center;">' + response[i].viernes + '</td>';
                            }

                            if (response[i].sabado == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:white;text-align:center;">' + response[i].sabado + '</td>';
                            }

                            if (response[i].domingo == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:white;text-align:center;">' + response[i].domingo + '</td>';
                            }

                            html += '</tr>';

                        }
                        $('#tbDisponibilidadSemana tbody').append(html);

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

    agregar_servicio: function (fecha) {
        disponibilidadjs.openModal(fecha);
        //$('html, body').animate({ scrollTop: 0 }, 'slow');
    },

    openModal: function (fecha) {
        var url = '/Horario/Disponibilidad/AgregarServicio';
        $.get(url, {
            fecha: fecha
        }, function (data) {
            createModal(data);
            CreatePickaDate('txtFechaAdd');
        });
    },

    registrarServicio: function (obj) {
        var cliente = $("#txtCliente").val();

        if (cliente == null || cliente == '') {
            ModalAlert('Debe Ingresar un Cliente');
            return;
        }


        ModalConfirm('¿Seguro que desea registrar?', 'disponibilidadjs.registrarServicio_callback();');
    },

    registrarServicio_callback: function () {

        var calendarioDTORequest = {
            IdEmpleado: $("#cboEmpleado").val(),
            ClienteOpcional: $("#txtCliente").val(),
            Descripcion: $("#txtDetalle").val(),
            FechaText: $("#txtFechaAdd").val(),
            HoraInicio: $("#cboHoraInicio").val(),
            HoraFin: $("#cboHoraFin").val()
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
                    $('#modal-register').modal('hide');
                    disponibilidadjs.buscarCabeceras();
                }
                else {
                    ModalAlert('Error al registrar : ' + response);
                    $('#modal-register').modal('hide');
                    disponibilidadjs.buscarCabeceras();
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

}

$(function () {

    disponibilidadjs.initializeEvent();

});