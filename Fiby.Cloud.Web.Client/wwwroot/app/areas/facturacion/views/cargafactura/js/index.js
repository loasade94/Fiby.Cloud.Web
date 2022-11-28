var cargafacturajs = {

    initializeEvent: function () {

        CreatePickaDate('txtDateRegisterSale');

        //cargafacturajs.search();

        $('#btnBuscar').on('click', function () {
            cargafacturajs.buscar();
        });

    },

    buscar: function () {

        var mes = $("#cboMonthtRegisterSale").val();
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

    DownloadDocument: function (en) {

        var mes = $('#cboMonthtRegisterSale').val();
        var anio = $('#txtYearRegisterSale').val();

        var form = document.createElement("FORM");
        var mesValue = document.createElement("INPUT");
        mesValue.type = "text";
        mesValue.value = mes;
        mesValue.name = "mes";
        mesValue.id = "mes";

        var anioValue = document.createElement("INPUT");
        anioValue.type = "text";
        anioValue.value = anio;
        anioValue.name = "anio";
        anioValue.id = "anio";


        form.style.display = "none";
        form.appendChild(mesValue);
        form.appendChild(anioValue);
        form.action = "/Facturacion/CargaFactura/GetPle0801";
        form.method = 'POST';
        document.body.appendChild(form);
        form.submit();

    },

    eliminarFactura: function (obj) {
        ModalConfirm('¿Seguro que desea eliminar el registro?', 'cargafacturajs.eliminarFactura_callback(\'' + obj + '\');');
    },

    eliminarFactura_callback: function (obj) {

        var idFacturaEmpresa = obj;

        $.ajax({
            type: "DELETE",
            data: {
                idFacturaEmpresa
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Facturacion/CargaFactura/EliminarFactura',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    if (response == "OK") {
                        ModalAlert('Factura eliminada correctamente');
                        cargafacturajs.buscar();
                    } else {
                        ModalAlertCancel('A Ocurrido un Error');
                    }
                } else {
                    ModalAlertCancel('A Ocurrido un Error');
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

    registrarFactura: function (idventa) {

        ModalConfirm('¿Seguro que desea registrar la Factura?', 'cargafacturajs.registrarFactura_callback();');
    },

    registrarFactura_callback: function () {

        var pMes = $('#cboMonthtRegisterSale').val();
        var pAno = $('#txtYearRegisterSale').val();
        var pTipoDocumentoVenta = $('#cboTypeDocumentSale').val();
        var pFechaEmision = $('#txtDateRegisterSale').val();
        var pSerie = $('#txtSerieRegisterSale').val();
        var pNumero = $('#txtNumberSerieRegisterSale').val();
        var pValorNeto = $('#txtNetoProduct').val();
        var pIgv = $('#txtIGVProduct').val();
        var pOtrosCargos = $('#txtOtherChargeProduct').val();
        var pValorTotal = $('#txtBrutoProduct').val();
        var pTipoDocumentoCliente = $('#cboTypeDocumentClientSale').val();
        var pNumeroDocumentoCliente = $('#txtNumberDocumentClient').val();
        var pRazonSocialCliente = $('#txtNameCompleteClient').val();

        var request = {
            Mes: pMes,
            Ano: pAno,
            TipoDocumentoVenta: pTipoDocumentoVenta,
            FechaEmision: pFechaEmision,
            Serie: pSerie,
            Numero: pNumero,
            ValorNeto: pValorNeto,
            Igv: pIgv,
            OtrosCargos: pOtrosCargos,
            ValorTotal: pValorTotal,
            TipoDocumentoCliente: pTipoDocumentoCliente,
            NumeroDocumentoCliente: pNumeroDocumentoCliente,
            RazonSocialCliente: pRazonSocialCliente
        };

        $.ajax({
            type: "POST",
            data:
            {
                request
            },
            url: '/Facturacion/CargaFactura/RegistrarCargaFactura',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    if (response[0] == "0") {
                        ModalAlert('Factura registrada correctamente');
                        cargafacturajs.buscar();
                    } else {
                        ModalAlertCancel('A Ocurrido un Error');
                    }
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
}


$(function () {

    cargafacturajs.initializeEvent();

});