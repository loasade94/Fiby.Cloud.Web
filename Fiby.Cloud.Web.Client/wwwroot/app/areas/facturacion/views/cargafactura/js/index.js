var cargafacturajs = {

    initializeEvent: function () {

        CreatePickaDate('txtDateRegisterSale');

        //cargafacturajs.search();

        $('#btnBuscar').on('click', function () {
            cargafacturajs.buscar();
        });

    },


    //search: function () {

    //    var mes = $("#cboTypeDocumentSale").val();

    //    var cargaFacturaDTORequest = {
    //        Mes: mes
    //    };

    //    var html = "";

    //    $.ajax({
    //        type: "POST",
    //        data:
    //        {
    //            cargaFacturaDTORequest
    //        },
    //        url: '/Facturacion/CargaFactura/ConsultaFacturas',
    //        // dataType: "json",
    //        beforeSend: function () {
    //            $('#loading').show();
    //        },
    //        complete: function () {
    //            $('#loading').hide();
    //        },
    //        success: function (response, textStatus, jqXhr) {

    //            if (response != null) {
    //                $("#tbCargaFactura > tbody").html("");
    //                $('#tbCargaFactura').DataTable().clear().destroy();

    //                if (response.length > 0) {

    //                    var html = "";
    //                    var eje = "";
    //                    for (var i = 0; i < response.length; i++) {

    //                        html += '<tr>';
    //                        html += '    <td>';
    //                        html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
    //                        html += '            <img src="/cssadmin/assets/images/edit.png" onclick="roljs.edit(\'' + response[i].rolId + '\',\'' + 0 + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
    //                        html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
    //                        html += '            <img src="/cssadmin/assets/images/trash.png" onclick="roljs.deleteRol(\'' + response[i].rolId + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
    //                        html += '                      data-toggle="tooltip" title="Eliminar">';
    //                        html += '        </div>';
    //                        html += '    </td>';
    //                        html += '    <td style="text-align:center;">' + response[i].fechaEmision + '</td>';
    //                        html += '    <td style="text-align:center;">' + response[i].serie + '-' + response[i].numero + '</td>';
    //                        html += '    <td style="text-align:center;">' + response[i].numeroDocumentoCliente + '</td>';
    //                        html += '    <td style="text-align:center;">' + response[i].cliente + '</td>';
    //                        html += '    <td style="text-align:center;">' + response[i].valorNeto + '</td>';
    //                        html += '    <td style="text-align:center;">' + response[i].igv + '</td>';
    //                        html += '    <td style="text-align:center;">' + response[i].valorTotal + '</td>';
    //                        html += '</tr>';
    //                    }
    //                    $('#tbCargaFactura tbody').append(html);
    //                }
    //                CreateDataTable('tbCargaFactura');
    //            }
    //        },
    //        error: function (xhr, status, errorThrown) {
    //            var err = "Status: " + status + " " + errorThrown;
    //            console.log(err);
    //            $('#loading').hide();
    //        },
    //        async: true,
    //    })
    //},

    //obtenerBuscar: function (pagina, orden, direccion, cambio, div) {
    //    var filtro = {
    //        //TipoTrabajador: $("#Filtro_TipoTrabajador").val(),
    //        AnoPeriodo: $("#Filtro_AnoPeriodo").val(),
    //        CodigoPeriodo: $("#Filtro_CodigoPeriodo").val(),
    //        CodigoUnico: $("#Filtro_CodigoUnico").val()
    //    };

    //    var request = {
    //        url: helperjs.urlBase + "SolicBonoEscolaridad/Buscar",
    //        data: {
    //            filtro: filtro,
    //            pagina: pagina,
    //            orden: orden,
    //            direccion: direccion,
    //            cambio: cambio
    //        },
    //        success: function (data, textStatus, jqXhr) {
    //            $(div).html(data); jsUtil.crearDataTable();
    //        },
    //        error: function (data, textStatus, jqXhr) {
    //            $(div).html("");
    //            $("#MensajeValidacion").html(helperjs.llamarMensajeError(jqXhr));
    //        }
    //    };
    //    helperjs.ajaxSend(request);
    //},

    buscar: function () {

        var mes = $("#cboTypeDocumentSale").val();
        var div = "#divGrid";
        var cargaFacturaDTORequest = {
            Mes: mes
        };

        var html = "";

        $.ajax({
            type: "POST",
            data:
            {
                cargaFacturaDTORequest
            },
            url: '/Facturacion/CargaFactura/ConsultaFacturas',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                $(div).html(response); //jsUtil.crearDataTable();
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

    cargafacturajs.initializeEvent();

});