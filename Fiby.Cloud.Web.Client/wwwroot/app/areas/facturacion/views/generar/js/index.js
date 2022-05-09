var generarjs = {

    initializeEvent: function () {

        $("#tabConsult").click(function () {
            $('#tabAdd').removeClass('first');
            $('#tabAdd').removeClass('current');
            $('#tabAdd').addClass('disabled');

            $('#tabConsult').addClass('first');
            $('#tabConsult').addClass('current');
            $('#tabConsult').removeClass('disabled');

            $('#steps-uid-0-p-1').hide();
            $('#steps-uid-0-p-0').show();

        });

        $("#tabAdd").click(function () {
            $('#tabConsult').removeClass('first');
            $('#tabConsult').removeClass('current');
            $('#tabConsult').addClass('disabled');

            $('#tabAdd').addClass('first');
            $('#tabAdd').addClass('current');
            $('#tabAdd').removeClass('disabled');

            $('#steps-uid-0-p-0').hide();
            $('#steps-uid-0-p-1').show();

        });

    },

    buscarDocumento: function () {

        var documento = $('#txtNumeroDocumento').val();

        if ($.trim(documento) == "") {
            return;
        }

        $.ajax({
            type: "POST",
            data:
            {
                documento: documento
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Facturacion/Generar/BuscarDocumento',
            success: function (response, textStatus, jqXhr) {

                if (response.idCliente != 0) {

                    $('#dvDetalleVenta').removeClass('is-hidden');
                    $('#dvTablaDetalleVenta').removeClass('is-hidden');

                    $('#hiddenidCliente').val('');
                    $('#hiddenTipoDocumento').val('');

                    if (response.tipoDocumento == '01') {
                        $('#dvNombreCompleto').addClass('is-hidden');
                        $('#dvRazonSocial').removeClass('is-hidden');
                    } else {
                        $('#dvNombreCompleto').removeClass('is-hidden');
                        $('#dvRazonSocial').addClass('is-hidden');
                    }

                    $('#txtNumeroComprobante').val(response.numeroComprobante);
                    $('#txtTipoDocumento').val(response.tipoDocumentoDescripcion);
                    $('#txtNombreCompleto').val(response.nombreCompleto);
                    $('#txtRazonSocial').val(response.razonSocial);
                    $('#txtDepartamento').val(response.departamentoDireccionDescripcion);
                    $('#txtProvincia').val(response.provinciaDireccionDescripcion);
                    $('#txtDistrito').val(response.distritoDireccionDescripcion);
                    $('#txtDireccion').val(response.facturacionDireccion);
                    $('#txtUbigeo').val(response.ubigeoDireccion);
                    $('#hiddenidCliente').val(response.idCliente);
                    $('#hiddenTipoDocumento').val(response.tipoDocumento);

                } else {
                    $('#dvDetalleVenta').addClass('is-hidden');
                    $('#dvTablaDetalleVenta').addClass('is-hidden');

                    $('#txtNumeroComprobante').val('');
                    $('#txtTipoDocumento').val('');
                    $('#txtNombreCompleto').val('');
                    $('#txtRazonSocial').val('');
                    $('#txtDepartamento').val('');
                    $('#txtProvincia').val('');
                    $('#txtDistrito').val('');
                    $('#txtDireccion').val('');
                    $('#txtUbigeo').val('');
                    $('#hiddenidCliente').val('');
                    $('#hiddenTipoDocumento').val('');

                    $('#sumaSubTotal').text('0.00');
                    $('#sumaIGV').text('0.00');
                    $('#sumaTotal').text('0.00');

                    $('#tbEntradaAgregarData').html('');
                }
            },
            complete: function () {
                //$('#loading').hide();
                generarjs.buscarServicio();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    calcularIGV: function () {

        var monto = $("#txtTotal").val().replace(",", ".");

        var pagoClienteDTORequest = {
            MontoPagoClienteText: monto
        };

        $.ajax({
            type: "POST",
            data:
            {
                pagoClienteDTORequest
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Facturacion/Generar/CalcularIGV',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {

                    $('#txtSubTotal').val(response[0].toString());
                    $('#txtIgv').val(response[1].toString());
                    $('#txtTotal').val(response[2].toString());

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

    agregarDetalle: function () {

        var html = '';
        var codigo = $('#tbEntradaAgregarData tr').length;

        var descripcion = $('#txtDealleDescripcion').val();
        var subTotal = $('#txtSubTotal').val();
        var igv = $('#txtIgv').val();
        var total = $('#txtTotal').val();
        var servicio = $('#cboServicios').val();
        var servicioText = $("#cboServicios option:selected").text();

        html += '<tr>';
        html += '    <td>';
        html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
        html += '            <img src="/cssadmin/assets/images/trash.png" onclick="generarjs.eliminarDetalle(\'' + codigo + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
        html += '                      data-toggle="tooltip" title="Eliminar">';
        html += '        </div>';
        html += '    </td>';
        html += '    <input type="hidden" id="codigo-' + codigo.toString() + '" value="' + codigo.toString() + '" />';
        html += '    <td class="is-hidden" style="text-align:center;">' + codigo.toString() + '</td>';

        html += '    <input type="hidden" id="servicio-' + codigo.toString() + '" value="' + servicio.toString() + '" />';
        html += '    <td style="text-align:center;">' + servicioText.toString() + '</td>';

        html += '    <input type="hidden" id="descripcion-' + codigo.toString() + '" value="' + descripcion.toString() + '" />';
        html += '    <td style="text-align:center;">' + descripcion.toString() + '</td>';

        html += '    <input type="hidden" id="subTotal-' + codigo.toString() + '" value="' + subTotal.toString() + '" />';
        html += '    <td style="text-align:center;">' + subTotal.toString() + '</td>';

        html += '    <input type="hidden" id="igv-' + codigo.toString() + '" value="' + igv.toString() + '" />';
        html += '    <td style="text-align:center;">' + igv.toString() + '</td>';

        html += '    <input type="hidden" id="total-' + codigo.toString() + '" value="' + total.toString() + '" />';
        html += '    <td style="text-align:center;">' + total.toString() + '</td>';

        html += '</tr>';


        $('#tbEntradaAgregar tbody').append(html);


        var valorSubtotal = 0;
        var valorIGV = 0;
        var valorTotal = 0;
        var contador = $('#tbEntradaAgregarData tr').length;

        for (var i = 0; i < contador; i++) {

            //var suma = $("#" + "total-" + i.toString()).val();
            var sumaSubTotal = $("#" + "subTotal-" + i.toString()).val().replace(',','.');
            var sumaIGV = $("#" + "igv-" + i.toString()).val().replace(',', '.');
            var sumaTotal = $("#" + "total-" + i.toString()).val().replace(',', '.');

            valorSubtotal = valorSubtotal + (isNaN(parseFloat(sumaSubTotal)) ? 0 : parseFloat(sumaSubTotal));
            valorIGV = valorIGV + (isNaN(parseFloat(sumaIGV)) ? 0 : parseFloat(sumaIGV));
            valorTotal = valorTotal + (isNaN(parseFloat(sumaTotal)) ? 0 : parseFloat(sumaTotal));

        }

        $('#sumaSubTotal').text(valorSubtotal);
        $('#sumaIGV').text(valorIGV);
        $('#sumaTotal').text(valorTotal);

        $('#txtDealleDescripcion').val('');
        $('#txtSubTotal').val('');
        $('#txtIgv').val('');
        $('#txtTotal').val('');
        $('#cboServicios').val('0');
    },

    buscarServicio: function () {

        var comboServicio = $('#cboServicios');

        var servicioClienteDTORequest = {
            IdCliente: $("#hiddenidCliente").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                servicioClienteDTORequest
            },
            beforeSend: function () {
              /*  $('#loading').show();*/
            },
            url: '/Horario/ServicioCliente/BuscarServicioXCliente',
            success: function (response, textStatus, jqXhr) {

                comboServicio.html('');

                if (response != null) {
                    
                    if (response.length > 0) {

                        var html = "";

                        html += '<option value="0">.::Seleccionar::.</option>';

                        for (var i = 0; i < response.length; i++) {
                            html += '<option value="' + response[i].idServicio + '">' + formatDateTime(response[i].fecha) + ' - ' + response[i].horas  + ' Horas</option>';
                        }

                        comboServicio.append(html);

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

    guardarReserva: function (obj) {

        ModalConfirm('¿Seguro que desea generar el documento?', 'generarjs.guardarReserva_callback();');
    },

    guardarReserva_callback: function () {

        var hiddenidCliente = $('#hiddenidCliente').val();
        var hiddenTipoDocumento = $('#hiddenTipoDocumento').val();
        var contadorProductos = $('#tbEntradaAgregarData tr').length;
        var txtDireccion = $('#txtDireccion').val();
        var txtNumeroDocumento = $('#txtNumeroDocumento').val();
        var txtRazonSocial = $('#txtRazonSocial').val();
        var txtNombreCompleto = $('#txtNombreCompleto').val();
        var txtDepartamento = $('#txtDepartamento').val();
        var txtProvincia = $('#txtProvincia').val();
        var txtDistrito = $('#txtDistrito').val();
        var txtUbigeo = $('#txtUbigeo').val();
        var sumaSubTotal = $('#sumaSubTotal').text();
        var sumaIGV = $('#sumaIGV').text();
        var sumaTotal = $('#sumaTotal').text();

        var name = "";

        if (hiddenTipoDocumento == "01") {
            name = txtRazonSocial;
        } else {
            name = txtNombreCompleto;
        }

        var ventaDTORequest = {
            IdCliente: hiddenidCliente,
            CodigoComprobante: hiddenTipoDocumento,
            contadorProductos: contadorProductos,
/*            CodigoTipoIdentificacion: periodo,*/
            DireccionCliente: txtDireccion,
            EmpresaRUCcliente: txtNumeroDocumento,
            EmpresaRazonsocialCliente: name,
            DptoempresaCliente: txtDepartamento,
            ProvempresaCliente: txtProvincia,
            DistempresaCliente: txtDistrito,
            UbigeoCliente: txtUbigeo,
            TotalIgvTexto: sumaIGV,
            TotSubtotalTexto: sumaSubTotal,
            Monto_totalTexto: sumaTotal,
            Estado: 'R'
        };

        var listDetalleVentaDTORequest = new Array();

        var contador = $('#tbEntradaAgregarData tr').length;

        for (var i = 0; i < contador; i++) {

            var obj = {
                IdServicio: $("#" + "servicio-" + i.toString()).val(),
                Total_a_pagarTexto: $("#" + "subTotal-" + i.toString()).val().replace(',', '.'),
                preciounitarioTexto: $("#" + "total-" + i.toString()).val().replace(',', '.'),
                mtoValorVentaItemTexto: $("#" + "total-" + i.toString()).val().replace(',', '.'),
                porIgvItemTexto: $("#" + "igv-" + i.toString()).val().replace(',', '.'),
                Descripcion: $("#" + "descripcion-" + i.toString()).val()
            };

            listDetalleVentaDTORequest.push(obj);

        }

        $.ajax({
            type: "POST",
            data:
            {
                ventaDTORequest: ventaDTORequest,
                listDetalleVentaDTORequest: listDetalleVentaDTORequest
            },
            url: '/Facturacion/Generar/RegistrarVenta',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response != "") {
                    //window.open("https://localhost:5087/Facturacion/Generar/Imprimir?idVenta=" + response);
                    window.open("https://fibycloud.com/Facturacion/Generar/Imprimir?idVenta=" + response);

                    //var url = "https://localhost:5087/Facturacion/Generar";
                    var url = "https://fibycloud.com/Facturacion/Generar";
                    window.location = url;

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

    consultaFactura: function (idVenta) {
        //window.open("https://localhost:5087/Facturacion/Generar/Imprimir?idVenta=" + idVenta);
        window.open("https://fibycloud.com/Facturacion/Generar/Imprimir?idVenta=" + idVenta);
    },

    bajaFactura: function (idventa) {

        ModalConfirm('¿Seguro que desea dar de baja a la Factura?', 'generarjs.bajaFactura_callback(' + idventa.toString() +');');
    },

    bajaFactura_callback: function (idventa) {

        var ventaDTORequest = {
            IdVenta: idventa
        };

        $.ajax({
            type: "POST",
            data:
            {
                ventaDTORequest: ventaDTORequest
            },
            url: '/Facturacion/Generar/RegistrarBajaFactura',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response == "") {
                    ModalAlert('Anulado correctamente');
                    generarjs.buscarComprobantes();
                } else {
                    ModalAlertCancel('A Ocurrido un Error');
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

    bajaBoleta: function (idventa) {

        ModalConfirm('¿Seguro que desea dar de baja a la Boleta?', 'generarjs.bajaBoleta_callback(' + idventa.toString() + ');');
    },

    bajaBoleta_callback: function (idventa) {

        var ventaDTORequest = {
            IdVenta: idventa
        };

        $.ajax({
            type: "POST",
            data:
            {
                ventaDTORequest: ventaDTORequest
            },
            url: '/Facturacion/Generar/RegistrarBajaBoleta',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response == "") {
                    ModalAlert('Anulado correctamente');
                    generarjs.buscarComprobantes();
                } else {
                    ModalAlertCancel('A Ocurrido un Error');
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

    buscarComprobantes: function () {

        var filtro = {
            /*IdCliente: $("#hiddenidCliente").val()*/
        };

        $.ajax({
            type: "POST",
            data:
            {
                filtro
            },
            beforeSend: function () {
                /*  $('#loading').show();*/
            },
            url: '/Facturacion/Generar/BuscarComprobantes',
            success: function (response, textStatus, jqXhr) {
                $("#divGrid").html(response);
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

    generarjs.initializeEvent();

});