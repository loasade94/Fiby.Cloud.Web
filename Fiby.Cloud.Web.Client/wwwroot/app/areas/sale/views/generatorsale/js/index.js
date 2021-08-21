var generatesalejs = {

    initializeEvent: function () {

        CreatePickaDate('txtDateRegisterSale');

        $("#btnGenerarArchivo").click(function () {
            generatesalejs.DownloadDocument();
        });


        $("#btnGrabar").click(function () {
            generatesalejs.registrarPle14100();
        });


    },

    DownloadDocument: function (en) {
        var form = document.createElement("FORM");
        //var passwordValue = document.createElement("INPUT");
        //passwordValue.type = "text";
        //passwordValue.value = password;
        //passwordValue.name = "password";
        //passwordValue.id = "password";



        //var emailValue = document.createElement("INPUT");
        //emailValue.type = "text";
        //emailValue.value = usernamemail;
        //emailValue.name = "usernamemail";
        //emailValue.id = "usernamemail";



        form.style.display = "none";
        //form.appendChild(passwordValue);
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

}


$(function () {

    generatesalejs.initializeEvent();

});