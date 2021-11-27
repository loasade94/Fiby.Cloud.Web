var calendariojs = {
    valorCliente : '',

    initializeCliente: function () {

        $.widget("custom.combobox", {
            _create: function () {
                this.wrapper = $("<span>")
                    .addClass("custom-combobox")
                    .insertAfter(this.element);

                this.element.hide();
                this._createAutocomplete();
                this._createShowAllButton();
            },

            _createAutocomplete: function () {
                var selected = this.element.children(":selected"),
                    value = selected.val() ? selected.text() : "";

                

                this.input = $("<input>")
                    .appendTo(this.wrapper)
                    .val(value)
                    .attr("title", "")
                    .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                    .autocomplete({
                        delay: 0,
                        minLength: 0,
                        clearButton: true,
                        source: this._source.bind(this),
                        change: function () {
                            calendariojs.buscarClientePorCodigo()
                        }      
                    })
                    .tooltip({
                        classes: {
                            "ui-tooltip": "ui-state-highlight"
                        }
                    });

                this._on(this.input, {
                    autocompleteselect: function (event, ui) {
                        ui.item.option.selected = true;
                        this._trigger("select", event, {
                            item: ui.item.option
                        });
                    },

                    autocompletechange: "_removeIfInvalid"
                });
            },

            _createShowAllButton: function () {
                var input = this.input,
                    wasOpen = false;

                $("<a>")
                    .attr("tabIndex", -1)
                    .attr("title", "Show All Items")
                    .tooltip()
                    .appendTo(this.wrapper)
                    .button({
                        icons: {
                            primary: "ui-icon-triangle-1-s"
                        },
                        text: false
                    })
                    .removeClass("ui-corner-all")
                    .addClass("custom-combobox-toggle ui-corner-right")
                    .on("mousedown", function () {
                        wasOpen = input.autocomplete("widget").is(":visible");
                    })
                    .on("click", function () {
                        input.trigger("focus");

                        // Close if already visible
                        if (wasOpen) {
                            return;
                        }

                        // Pass empty string as value to search for, displaying all results
                        input.autocomplete("search", "");
                    });
            },

            _source: function (request, response) {
                var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                response(this.element.children("option").map(function () {
                    var text = $(this).text();
                    if (this.value && (!request.term || matcher.test(text)))
                        return {
                            label: text,
                            value: text,
                            option: this
                        };
                }));
            },

            _removeIfInvalid: function (event, ui) {

                // Selected an item, nothing to do
                if (ui.item) {
                    return;
                }

                // Search for a match (case-insensitive)
                var value = this.input.val(),
                    valueLowerCase = value.toLowerCase(),
                    valid = false;
                this.element.children("option").each(function () {
                    if ($(this).text().toLowerCase() === valueLowerCase) {
                        this.selected = valid = true;
                        return false;
                    }
                });

                // Found a match, nothing to do
                if (valid) {
                    calendariojs.valorCliente = value;
                    return;
                } else {
                 /*   $('#originalValue').attr('value', '0').html('');*/
                    //$('#originalValue').attr('value', '0').html(value);
                    calendariojs.valorCliente = value;
                    $('#combobox').val('0');
                }

                // Remove invalid value
                //this.input
                //    .val("")
                //    .attr("title", value + " didn't match any item")
                //    .tooltip("open");
                //this.eleme    nt.val("");
                //this._delay(function () {
                //    this.input.tooltip("close").attr("title", "");
                //}, 2500);
                this.input.autocomplete("instance").term = "";
                
            },

            _destroy: function () {
                this.wrapper.remove();
                this.element.show();
            }
        });

        $("#combobox").combobox();
        $("#toggle").on("click", function () {
            $("#combobox").toggle();
        });

        $("#btnBorrarCliente").addClass("is-hidden");

    },

    initializeEvent: function () {

        $("#btnGrabar").click(function () {
            calendariojs.registrarServicio_ValidacionRecurrente();
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

    registrarServicio_ValidacionRecurrente: function() {
        var recurrente = 0;

        var checkboxElem = $('#chkRecurrente')[0].checked;
        if (checkboxElem == true) {
            recurrente = 1;
        } else {
            recurrente = 0;
        }

        if (recurrente == 1) {
            ModalConfirm('¿Esta creando un servicio recurrente , esa seguro?', 'calendariojs.registrarServicio();');
        } else {
            calendariojs.registrarServicio();
        }
    },

    registrarServicio: function (obj) {
        var cliente = '';
        var Valuecliente = $('#combobox').val();

        if (Valuecliente == '0') {
            cliente = calendariojs.valorCliente;
        } else {
            cliente = $("#combobox option:selected").text();
        }

        //$("#combobox option:selected").text();

        if (cliente == null || cliente == '') {
            ModalAlertCancel('Debe Ingresar un Cliente');
            return;
        }

        ModalConfirm('¿Seguro que desea registrar?', 'calendariojs.registrarServicio_callback();');
    },

    registrarServicio_callback: function () {

        var recurrente = 0;

        var checkboxElem = $('#chkRecurrente')[0].checked;
        if (checkboxElem == true) {
            recurrente = 1;
        } else {
            recurrente = 0;
        }

        var cliente = '';
        var Valuecliente = $('#combobox').val();

        if (Valuecliente == '0') {
            cliente = calendariojs.valorCliente;
        } else {
            cliente = $("#combobox option:selected").text();
        }

        var calendarioDTORequest = {
            IdEmpleado: $("#cboEmpleado").val(),
            IdCliente: Valuecliente,
            ClienteOpcional: cliente,
            ClienteTelefono: $("#txtTelefonoCliente").val(),
            ClienteDireccion: $("#txtDireccionCliente").val(),
            Descripcion: $("#txtDesServicio").val(),
            FechaText: $("#txtFecha").val(),
            HoraInicio: $("#txtHoraInicio").val(),
            HoraFin: $("#txtHoraFin").val(),
            Recurrente: recurrente
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

                if (response != null) {
                    if (response[1] == "OK") {
                        ModalAlert('Registrado Correctamente');

                        if (Valuecliente == '0') {
                            calendariojs.buscarServicioXCodigo(response[0]);
                        }

                        calendariojs.buscarServicio();
                        calendariojs.limpiar();
                    }
                    else {
                        ModalAlertCancel('Error al registrar : ' + response[1]);
                        calendariojs.buscarServicio();
                    }
                } else {
                    ModalAlertCancel("Ocurrio un error");
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

    buscarServicioXCodigo: function (valuex) {

        var calendarioDTORequest = {
            IdServicio: valuex
        };

        $.ajax({
            type: "POST",
            data:
            {
                calendarioDTORequest
            },
            //beforeSend: function () {
            //    $('#loading').show();
            //},
            url: '/Horario/Calendario/BuscarServicioXCodigo',
            success: function (response, textStatus, jqXhr) {

                var select = document.getElementById('combobox');

                if (response != null) {
                    for (var i = 0; i < 1; i++) {
                        var opt = document.createElement('option');
                        opt.value = response.idCliente;
                        opt.innerHTML = response.clienteOpcional;
                        select.appendChild(opt);
                    }
                }

            },
            //complete: function () {
            //    calendariojs.buscarServicioCalendario();
            //},
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: false,
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
                            html += '    <td style="text-align:center;">' + response[i].direccion + '</td>';
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
        $('#combobox').val('0');
        calendariojs.valorCliente = '';
        $("#btnBorrarCliente").trigger("click");
        $('#txtDesServicio').val('');
        $('#txtHoras').val('0');
        $('#txtDireccionCliente').val('');
        $('#txtTelefonoCliente').val('');

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

    buscarClientePorCodigo: function () {

        var cod = $('#combobox').val();

        if (cod == "0") {
            $('#txtDireccionCliente').val('');
            $('#txtTelefonoCliente').val('');
            return;
        }

        $.ajax({
            type: "POST",
            data:
            {
                codigo: cod
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/BuscarClientesCodio',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $('#txtDireccionCliente').val(response.direccion);
                    $('#txtTelefonoCliente').val(response.telefono);
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

}

$(function () {

    calendariojs.initializeEvent();
    calendariojs.initializeCliente();

});