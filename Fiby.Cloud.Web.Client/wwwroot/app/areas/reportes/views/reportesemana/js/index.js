var reportesemanajs = {

    initializeEvent: function () {

        $("#btnBuscar").click(function () {
            reportesemanajs.buscarRentabilidad();
        });

        //reportesemanajs.buscarRentabilidad();

        //$("#btnGenerarPDF").click(function () {
        //    pagoempleadojs.DownloadDocument();
        //});

        //$("#btnPagar").click(function () {
        //    pagoempleadojs.registrarPagoEmpleado();
        //});


    },

    buscarRentabilidad: function () {

        var reporteSemanaDTORequest = {
            IdSemana: $('#cboSemana').val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                reporteSemanaDTORequest
            },
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            url: '/Reportes/ReporteSemana/BuscarReporteRentabilidadSemanal',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tblReporteSemana > tbody").html("");

                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            if (i == (response.length - 1)) {
                                html += '<tr>';
                                html += '    <td colspan=2" style="text-align:center;">Totales</td>';
                                html += '    <td style="text-align:center;">' + response[i].horas + '</td>';
                                html += '    <td style="text-align:center;background-color:green;color:white;"> S/. ' + response[i].montoPagoCliente + '</td>';
                                html += '    <td style="text-align:center;background-color:red;color:white;"> S/. ' + response[i].subTotal + '</td>';
                                html += '    <td style="text-align:center;background-color:red;color:white;"> S/. ' + response[i].pasaje + '</td>';
                                html += '    <td style="text-align:center;background-color:red;color:white;"> S/. ' + response[i].pago + '</td>';
                                html += '    <td style="text-align:center;background-color:green;color:white;"> S/. ' + response[i].rentabilidad + '</td>';
                                html += '</tr>';
                            } else {
                                html += '<tr>';
                                html += '    <td style="text-align:center;">' + response[i].nombreDia + ' ' + response[i].numeroDia + '</td>';
                                html += '    <td style="text-align:center;">' + response[i].cliente + '</td>';;
                                html += '    <td style="text-align:center;">' + response[i].horas + '</td>';
                                html += '    <td style="text-align:center;background-color:green;color:white;"> S/. ' + response[i].montoPagoCliente + '</td>';
                                html += '    <td style="text-align:center;background-color:red;color:white;"> S/. ' + response[i].subTotal + '</td>';
                                html += '    <td style="text-align:center;background-color:red;color:white;"> S/. ' + response[i].pasaje + '</td>';
                                html += '    <td style="text-align:center;background-color:red;color:white;"> S/. ' + response[i].pago + '</td>';
                                html += '    <td style="text-align:center;background-color:green;color:white;"> S/. ' + response[i].rentabilidad + '</td>';
                                html += '</tr>';
                            }



                        }
                        $('#tblReporteSemana tbody').append(html);

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

}

$(function () {

    reportesemanajs.initializeEvent();

});