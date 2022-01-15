var clientehorariojs = {
    valorCliente: '',

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
                            /*calendariojs.buscarClientePorCodigo()*/
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
                    clientehorariojs.valorCliente = value;
                    return;
                } else {
                    /*   $('#originalValue').attr('value', '0').html('');*/
                    //$('#originalValue').attr('value', '0').html(value);
                    clientehorariojs.valorCliente = value;
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

        $("#btnBuscar").click(function () {

            var cliente = $('#combobox').val();

            if (cliente == "0") {
                ModalAlertCancel("Debe ingresar un Cliente");
            } else {
                clientehorariojs.buscarServicio();
            }

            
        });

        $("#btnLimpiar").click(function () {
            clientehorariojs.limpiar();
        });

    },

    buscarServicio: function () {

        var servicioClienteDTORequest = {
            IdSemana: $("#cboSemana").val(),
            IdCliente: $("#combobox").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                servicioClienteDTORequest
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Horario/ServicioCliente/BuscarServicioXCliente',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbServiciosPorCliente > tbody").html("");
                    $('#tbServiciosPorCliente').DataTable().clear().destroy();

                    if (response.length > 0) {


                        var indice = 0;

                        var html = "";
                        var eje = "";
                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td style="text-align:center;">' + formatDateTime(response[i].fecha) + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].horario + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].pasaje + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].montoPagoCliente + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].nombreEmpleado + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].direccion + '</td>';
                            html += '</tr>';
                            indice += 1;

                        }
                        $('#tbServiciosPorCliente tbody').append(html);

                    }
                    clientehorariojs.intitDataTable();
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
        //CreateDataTable('tbServiciosPorCliente');
        $('#tbServiciosPorCliente').DataTable({
            "language": {
                "lengthMenu": "Mostrar _MENU_ registros por página",
                "zeroRecords": "No se ha encontrado nada",
                "info": "Mostrando página _PAGE_ de _PAGES_",
                "infoEmpty": "No hay registros disponibles",
                "infoFiltered": "(filtered from _MAX_ total records)",
                //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                "processing": "Procesando...",
                "search": "Buscar:",
                "paginate": {
                    "first": "Primero",
                    "last": "Último",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }

                //"aria": {
                //    "sortAscending": ": activar para ordenar columnas ascendentemente",
                //    "sortDescending": ": activar para ordenar columnas descendentemente"
                //}
            },
            "columnDefs": [{ "orderable": false, "targets": 0 }],
            "ordering": false
        });
    },

    limpiar: function () {

        $('#combobox').val('0');
        $('#cboSemana').val('0');
        clientehorariojs.valorCliente = '';
        $("#btnBorrarCliente").trigger("click");
        
        //clientehorariojs.buscarServicio();
    },

}


$(function () {

    clientehorariojs.initializeEvent();
    clientehorariojs.initializeCliente();

});