var generatesalejs = {

    initializeEvent: function () {

        CreatePickaDate('txtDateRegisterSale');

        $("#btnGenerarArchivo").click(function () {
            generatesalejs.DownloadDocument();
        });


        $("#btnGrabar").click(function () {
            generatesalejs.registrarPle14100();
        });

        $("#btnBuscar").click(function () {
            generatesalejs.searchPle14100(0);
        });

        generatesalejs.searchPle14100(1);
    },

    intitDataTable: function () {
        CreateDataTable('tbSalesAll');
    },

    DownloadDocument: function (en) {

        var mes = $('#cboTypeDocumentSale').val();

        var form = document.createElement("FORM");
        var mesValue = document.createElement("INPUT");
        mesValue.type = "text";
        mesValue.value = mes;
        mesValue.name = "mes";
        mesValue.id = "mes";



        //var emailValue = document.createElement("INPUT");
        //emailValue.type = "text";
        //emailValue.value = usernamemail;
        //emailValue.name = "usernamemail";
        //emailValue.id = "usernamemail";



        form.style.display = "none";
        form.appendChild(mesValue);
        //form.appendChild(emailValue);
        form.action = "/Sale/GeneratorSale/GetPleAll";
        form.method = 'POST';
        document.body.appendChild(form);
        form.submit();

    },

    generatePle: function () {

        var txtSequenceCUO = 1;

        var pleDTORequest = {
            SequenceCUO: txtSequenceCUO,
            TipoDocumento: $("#cboTypeDocumentSale").val(),
            FechaEmision: $("#txtDateRegisterSale").val(),
            Serie: $("#txtSerieRegisterSale").val(),
            NumeroSerie: $("#txtNumberSerieRegisterSale").val(),
            ValorNeto: $("#txtNetoProduct").val(),
            ValorIgv: $("#txtIGVProduct").val(),
            ValorTotal: $("#txtBrutoProduct").val(),
            TipoDocumentoCliente: $("#cboTypeDocumentClientSale").val(),
            NumeroDocumentoCliente: $("#txtNumberDocumentClient").val(),
            NombresDocumentoCliente: $("#txtNameCompleteClient").val()
        };

/*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                pleDTORequest
            },
            url: '/Sale/GeneratorSale/GetPleAll',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {


            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    registrarPle14100: function () {

        var pLE14100DTORequest = {
            TIPO_DOC_COMPROBANTE: $("#cboTypeDocumentSale").val(),
            FECHA_EMISION: $("#txtDateRegisterSale").val(),
            SERIE: $("#txtSerieRegisterSale").val(),
            NUMERO: $("#txtNumberSerieRegisterSale").val(),
            BASE_IMPONIBLE_GRAV: $("#txtNetoProduct").val(),
            IGV_GRAV: $("#txtIGVProduct").val(),
            IMPORTE_TOTAL: $("#txtBrutoProduct").val(),
            TIPO_DOC_CLIENTE: $("#cboTypeDocumentClientSale").val(),
            NUMERO_DOC_CLIENTE: $("#txtNumberDocumentClient").val(),
            NOMBRE_CLIENTE: $("#txtNameCompleteClient").val()
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                pLE14100DTORequest
            },
            url: '/Sale/GeneratorSale/RegistrarPle14100DPorMes',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {


            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    searchPle14100: function (indicadorInicio) {

        var pLE14100DTORequest = {
            MesLista: $("#txtDateRegisterSale").val(),
        };

        $.ajax({
            type: "POST",
            data:
            {
                pLE14100DTORequest,
                indicadorInicio
            },
            url: '/Sale/GeneratorSale/BuscarPle14100DPorMes',
            // dataType: "json",
            //beforeSend: function () {
            //    $('#loading').show();
            //},
            //complete: function () {
            //    $('#loading').hide();
            //},
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbSalesAll > tbody").html("");
                    $('#tbSalesAll').DataTable().clear().destroy();
                    if (response.length > 0) {

                        var html = "";
                        var eje = "";
                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            //html += '    <td>';
                            //html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                            //html += '            <img src="/admin-profile/imgadmin/pen.png" onclick="classificationjs.edit(\'' + response[i].classificationId + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                            //html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                            //html += '            <img src="/admin-profile/imgadmin/trash.png" onclick="classificationjs.deleteConfirm(\'' + response[i].classificationId + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
                            //html += '                      data-toggle="tooltip" title="Eliminar">';
                            //html += '        </div>';
                            //html += '    </td>';
                            html += '    <td style="text-align:center;">' + response[i].base_imponible_grav + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].igv_grav + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].importe_total + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].serie + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].numero + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].numero_doc_cliente + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].nombre_cliente + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].fecha_emision + '</td>';
                            html += '</tr>';
                        }
                        $('#tbSalesAll tbody').append(html);
                    }
                    generatesalejs.intitDataTable();
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

    generatesalejs.initializeEvent();

});