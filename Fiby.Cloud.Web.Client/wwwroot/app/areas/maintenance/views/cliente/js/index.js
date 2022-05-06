var clientejs = {

    initializeEvent: function () {

        $("#btnRegistrar").click(function () {
            clientejs.registrarCliente();
        });

        clientejs.buscarCliente();

    },

    buscarCliente: function () {

        $.ajax({
            type: "POST",
            //data:
            //{
            //    calendarioDTORequest
            //},
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/BuscarClientes',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbCliente > tbody").html("");
                    $('#tbCliente').DataTable().clear().destroy();

                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td>';
                            html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                            html += '            <img src="/cssadmin/assets/images/edit.png" onclick="clientejs.edit(\'' + response[i].idCliente + '\',\'' + 0 + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                            html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                            html += '            <img src="/cssadmin/assets/images/trash.png" onclick="clientejs.eliminarCliente(\'' + response[i].idCliente + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
                            html += '                      data-toggle="tooltip" title="Eliminar">';
                            html += '        </div>';
                            html += '    </td>';
                            html += '    <td style="text-align:center;">' + response[i].idCliente + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].nombres + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].direccion + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].telefono + '</td>';
                            html += '</tr>';

                        }
                        $('#tbCliente tbody').append(html);

                    }
                    clientejs.intitDataTable();
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
        CreateDataTable('tbCliente');
    },

    registrarCliente: function (obj) {
        var cliente = $("#txtNombres").val();

        if (cliente == null || cliente == '') {
            ModalAlertCancel('Debe Ingresar un Cliente');
            return;
        }


        ModalConfirm('¿Seguro que desea registrar?', 'clientejs.registrarCliente_callback();');
    },

    registrarCliente_callback: function () {

        var clienteDTORequest = {
            Nombres: $("#txtNombres").val(),
            Direccion: $("#txtDireccion").val(),
            Telefono: $("#txtTelefono").val(),

            TipoDocumento: $("#cboTipoCliente").val(),
            NumeroDocumento: $("#txtNumeroDocumento").val(),
            NombreCompleto: $("#txtNombreCompleto").val(),
            RazonSocial: $("#txtRazonSocial").val(),
            DepartamentoDireccion: $("#cboDepartamento").val(),
            ProvinciaDireccion: $("#cboProvincia").val(),
            DistritoDireccion: $("#cboDistrito").val(),
            UbigeoDireccion: $("#txtUbigeo").val(),
            FacturacionDireccion: $("#txtDireccionFacturacion").val()
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                clienteDTORequest
            },
            url: '/Maintenance/Cliente/RegistrarCliente',
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
                    clientejs.limpiarCampos();
                    clientejs.buscarCliente();
                    //calendariojs.limpiar();
                }
                else {
                    ModalAlertCancel('Error al registrar : ' + response);
                    clientejs.buscarCliente();
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

    eliminarCliente: function (obj) {
        ModalConfirm('¿Seguro que desea eliminar el registro?', 'clientejs.eliminarCliente_callback(\'' + obj + '\');');
    },

    eliminarCliente_callback: function (obj) {

        var idCliente = obj;

        $.ajax({
            type: "DELETE",
            data: {
                idCliente
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/EliminarCliente',
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                    /*IziToastMessage(0, 'Eliminado Correctamente', 'green', 'topRight', 5000);*/
                    ModalAlert("Eliminado Correctamente");
                    clientejs.buscarCliente();
                } else {
                    /* IziToastMessage(1, 'Ocurrio un error: ' + response, '', 'topRight', 5000);*/
                    ModalAlertCancel("ERROR");
                    clientejs.buscarCliente();
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

    edit: function (idCliente, option) {
        clientejs.openModal(idCliente, option);
        //$('html, body').animate({ scrollTop: 0 }, 'slow');
    },

    openModal: function (idCliente, option) {
        var url = '/Maintenance/Cliente/Actualizar';
        $.get(url, {
            idCliente: idCliente, option: option
        }, function (data) {
            createModal(data);
/*            CreatePickaDate('txtFechaEditar');*/
            //CreatePickaTime('txtHoraInicioEditar');
            //CreatePickaTime('txtHoraFinEditar');
            //createSelect('cboRegistrationStatusNew');
        });
    },

    actualizarCliente: function (obj) {
        var cliente = $("#txtNombreEditar").val();

        if (cliente == null || cliente == '') {
            ModalAlertCancel('Debe Ingresar un Cliente');
            return;
        }


        ModalConfirm('¿Seguro que desea registrar?', 'clientejs.actualizarCliente_callback();');
    },

    actualizarCliente_callback: function () {

        var clienteDTORequest = {
            IdCliente: $("#hiddenClienteId").val(),
            Nombres: $("#txtNombreEditar").val(),
            Telefono: $("#txtTelefonoEditar").val(),
            Direccion: $("#txtDireccionEditar").val(),

            TipoDocumento: $("#cboTipoClienteEditar").val(),
            NumeroDocumento: $("#txtNumeroDocumentoEditar").val(),
            NombreCompleto: $("#txtNombreCompletoEditar").val(),
            RazonSocial: $("#txtRazonSocialEditar").val(),
            DepartamentoDireccion: $("#cboDepartamentoEditar").val(),
            ProvinciaDireccion: $("#cboProvinciaEditar").val(),
            DistritoDireccion: $("#cboDistritoEditar").val(),
            UbigeoDireccion: $("#txtUbigeoEditar").val(),
            FacturacionDireccion: $("#txtFacturacionDireccionEditar").val()
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                clienteDTORequest
            },
            url: '/Maintenance/Cliente/RegistrarCliente',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            //complete: function () {
            //    $('#loading').hide();
            //},
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                    $('#modal-register').modal('hide');
                    ModalAlert("Actualizado correctamente");
                    clientejs.buscarCliente();
                    //calendariojs.limpiar();
                }
                else {
                    $('#modal-register').modal('hide');
                    ModalAlertCancel('Error al registrar : ' + response);
                    clientejs.buscarCliente();
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

    buscarProvinciaPorDept: function () {

        var cboDepartamento = $('#cboDepartamento').val();

        if ($.trim(cboDepartamento) == "") {
            cboDepartamento = "";
        }

        var cboProvincia = $('#cboProvincia');
        var cboDistrito = $('#cboDistrito');

        $.ajax({
            type: "POST",
            data:
            {
                codigoDepartamento: cboDepartamento
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/BuscarProvincia',
            success: function (response, textStatus, jqXhr) {

                cboProvincia.html('');
                cboDistrito.html('');
                $('#txtUbigeo').val('');

                var html = '';

                if (response != null) {

                    html += '<option value="">.:Seleccione:.</option>';

                    for (var i = 0; i < response.length; i++) {
                        html += '<option value="' + response[i].codigoProvincia + '">' + response[i].provinciaDescripcion.toUpperCase()  + '</option>';
                    }

                    cboProvincia.append(html);
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

    buscarDistritoPorDeptProv: function () {

        var cboDepartamento = $('#cboDepartamento').val();
        var cboProvincia = $('#cboProvincia').val();

        if ($.trim(cboDepartamento) == "") {
            cboDepartamento = "";
        }

        if ($.trim(cboProvincia) == "") {
            cboProvincia = "";
        }

        var cboDistrito = $('#cboDistrito');

        $.ajax({
            type: "POST",
            data:
            {
                codigoDepartamento: cboDepartamento,
                codigoProvincia: cboProvincia
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/BuscarDistrito',
            success: function (response, textStatus, jqXhr) {

                cboDistrito.html('');
                $('#txtUbigeo').val('');
                var html = '';

                if (response != null) {

                    html += '<option value="">.:Seleccione:.</option>';

                    for (var i = 0; i < response.length; i++) {
                        //html += '<option onchange="clientejs.setearUbigeo(' + response[i].ubigeo + ');" value="' + response[i].codigoDistrito + '">' + response[i].distritoDescripcion + '</option>';
                        html += '<option id="' + response[i].ubigeo + '" value="' + response[i].codigoDistrito + '">' + response[i].distritoDescripcion.toUpperCase() + '</option>';
                    }

                    cboDistrito.append(html);
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

    setearUbigeo: function (s) {
        var id = s[s.selectedIndex].id;
        $('#txtUbigeo').val(id);
    },

    buscarProvinciaPorDeptEditar: function () {

        var cboDepartamento = $('#cboDepartamentoEditar').val();

        if ($.trim(cboDepartamento) == "") {
            cboDepartamento = "";
        }

        var cboProvincia = $('#cboProvinciaEditar');
        var cboDistrito = $('#cboDistritoEditar');

        $.ajax({
            type: "POST",
            data:
            {
                codigoDepartamento: cboDepartamento
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/BuscarProvincia',
            success: function (response, textStatus, jqXhr) {

                cboProvincia.html('');
                cboDistrito.html('');
                $('#txtUbigeoEditar').val('');

                var html = '';

                if (response != null) {

                    html += '<option value="">.:Seleccione:.</option>';

                    for (var i = 0; i < response.length; i++) {
                        html += '<option value="' + response[i].codigoProvincia + '">' + response[i].provinciaDescripcion.toUpperCase() + '</option>';
                    }

                    cboProvincia.append(html);
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

    buscarDistritoPorDeptProvEditar: function () {

        var cboDepartamento = $('#cboDepartamentoEditar').val();
        var cboProvincia = $('#cboProvinciaEditar').val();

        if ($.trim(cboDepartamento) == "") {
            cboDepartamento = "";
        }

        if ($.trim(cboProvincia) == "") {
            cboProvincia = "";
        }

        var cboDistrito = $('#cboDistritoEditar');

        $.ajax({
            type: "POST",
            data:
            {
                codigoDepartamento: cboDepartamento,
                codigoProvincia: cboProvincia
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/BuscarDistrito',
            success: function (response, textStatus, jqXhr) {

                cboDistrito.html('');
                $('#txtUbigeoEditar').val('');
                var html = '';

                if (response != null) {

                    html += '<option value="">.:Seleccione:.</option>';

                    for (var i = 0; i < response.length; i++) {
                        //html += '<option onchange="clientejs.setearUbigeo(' + response[i].ubigeo + ');" value="' + response[i].codigoDistrito + '">' + response[i].distritoDescripcion + '</option>';
                        html += '<option id="' + response[i].ubigeo + '" value="' + response[i].codigoDistrito + '">' + response[i].distritoDescripcion.toUpperCase() + '</option>';
                    }

                    cboDistrito.append(html);
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

    setearUbigeoEditar: function (s) {
        var id = s[s.selectedIndex].id;
        $('#txtUbigeoEditar').val(id);
    },

    ocultarTipoDocumento: function () {

        var tipo = $('#cboTipoCliente').val();

        if (tipo == "01") {
            $('#dvNombreCompleto').val('');
            $('#dvNombreCompleto').addClass('is-hidden');
            $('#dvRazonSocial').removeClass('is-hidden');
        } else {
            $('#dvRazonSocial').val('');
            $('#dvNombreCompleto').removeClass('is-hidden');
            $('#dvRazonSocial').addClass('is-hidden');
        }

    },

    ocultarTipoDocumentoEditar: function () {

        var tipo = $('#cboTipoClienteEditar').val();

        if (tipo == "01") {
            $('#dvNombreCompletoEditar').val('');
            $('#dvNombreCompletoEditar').addClass('is-hidden');
            $('#dvRazonSocialEditar').removeClass('is-hidden');
        } else {
            $('#dvRazonSocialEditar').val('');
            $('#dvNombreCompletoEditar').removeClass('is-hidden');
            $('#dvRazonSocialEditar').addClass('is-hidden');
        }

    },

    buscarProvinciaPorDeptCodigo: function (ubigeo) {

        var cboDepartamento = ubigeo.substring(0, 2);
        var provincia = ubigeo.substring(2, 4);

        if ($.trim(cboDepartamento) == "") {
            cboDepartamento = "";
        }

        var cboProvincia = $('#cboProvincia');
        var cboDistrito = $('#cboDistrito');

        $.ajax({
            type: "POST",
            data:
            {
                codigoDepartamento: cboDepartamento
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/BuscarProvincia',
            success: function (response, textStatus, jqXhr) {

                cboProvincia.html('');
                cboDistrito.html('');

                var html = '';

                if (response != null) {

                    html += '<option value="">.:Seleccione:.</option>';

                    for (var i = 0; i < response.length; i++) {
                        html += '<option value="' + response[i].codigoProvincia + '">' + response[i].provinciaDescripcion.toUpperCase() + '</option>';
                    }

                    cboProvincia.append(html);

                    cboProvincia.val(provincia);
                }
            },
            complete: function () {
                clientejs.buscarDistritoPorDeptProvCodigo(ubigeo);
                //$('#loading').hide();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    buscarProvinciaPorDeptCodigoEditar: function (ubigeo) {

        var cboDepartamento = ubigeo.substring(0, 2);
        var provincia = ubigeo.substring(2, 4);

        if ($.trim(cboDepartamento) == "") {
            cboDepartamento = "";
        }

        var cboProvincia = $('#cboProvinciaEditar');
        var cboDistrito = $('#cboDistritoEditar');

        $.ajax({
            type: "POST",
            data:
            {
                codigoDepartamento: cboDepartamento
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Cliente/BuscarProvincia',
            success: function (response, textStatus, jqXhr) {

                cboProvincia.html('');
                cboDistrito.html('');

                var html = '';

                if (response != null) {

                    html += '<option value="">.:Seleccione:.</option>';

                    for (var i = 0; i < response.length; i++) {
                        html += '<option value="' + response[i].codigoProvincia + '">' + response[i].provinciaDescripcion.toUpperCase() + '</option>';
                    }

                    cboProvincia.append(html);

                    cboProvincia.val(provincia);
                }
            },
            complete: function () {
                clientejs.buscarDistritoPorDeptProvCodigoEditar(ubigeo);
                //$('#loading').hide();
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    buscarDistritoPorDeptProvCodigo: function (ubigeo) {

        var cboDepartamento = ubigeo.substring(0, 2);
        var cboProvincia = ubigeo.substring(2, 4);
        var cboDistrito = ubigeo.substring(4, 6);;

        var distrito = $('#cboDistrito');

        if ($.trim(cboDepartamento) == "") {
            cboDepartamento = "";
        }

        if ($.trim(cboProvincia) == "") {
            cboProvincia = "";
        }

        $.ajax({
            type: "POST",
            data:
            {
                codigoDepartamento: cboDepartamento,
                codigoProvincia: cboProvincia
            },
            beforeSend: function () {
                /*$('#loading').show();*/
            },
            url: '/Maintenance/Cliente/BuscarDistrito',
            success: function (response, textStatus, jqXhr) {

                distrito.html('');

                var html = '';

                if (response != null) {

                    html += '<option value="">.:Seleccione:.</option>';

                    for (var i = 0; i < response.length; i++) {
                        //html += '<option onchange="clientejs.setearUbigeo(' + response[i].ubigeo + ');" value="' + response[i].codigoDistrito + '">' + response[i].distritoDescripcion + '</option>';
                        html += '<option id="' + response[i].ubigeo + '" value="' + response[i].codigoDistrito + '">' + response[i].distritoDescripcion.toUpperCase() + '</option>';
                    }
                    
                    distrito.append(html);
                    $('#cboDistrito').val(cboDistrito);
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

    buscarDistritoPorDeptProvCodigoEditar: function (ubigeo) {

        var cboDepartamento = ubigeo.substring(0, 2);
        var cboProvincia = ubigeo.substring(2, 4);
        var cboDistrito = ubigeo.substring(4, 6);;

        var distrito = $('#cboDistritoEditar');

        if ($.trim(cboDepartamento) == "") {
            cboDepartamento = "";
        }

        if ($.trim(cboProvincia) == "") {
            cboProvincia = "";
        }

        $.ajax({
            type: "POST",
            data:
            {
                codigoDepartamento: cboDepartamento,
                codigoProvincia: cboProvincia
            },
            beforeSend: function () {
                /*$('#loading').show();*/
            },
            url: '/Maintenance/Cliente/BuscarDistrito',
            success: function (response, textStatus, jqXhr) {

                distrito.html('');

                var html = '';

                if (response != null) {

                    html += '<option value="">.:Seleccione:.</option>';

                    for (var i = 0; i < response.length; i++) {
                        //html += '<option onchange="clientejs.setearUbigeo(' + response[i].ubigeo + ');" value="' + response[i].codigoDistrito + '">' + response[i].distritoDescripcion + '</option>';
                        html += '<option id="' + response[i].ubigeo + '" value="' + response[i].codigoDistrito + '">' + response[i].distritoDescripcion.toUpperCase() + '</option>';
                    }

                    distrito.append(html);
                    $('#cboDistritoEditar').val(cboDistrito);
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

    buscarRUC: function () {

        var ruc = $('#txtNumeroDocumento').val();
        var tipoCliente = $('#cboTipoCliente').val();
        var departamento = $('#cboDepartamento');

        if (tipoCliente == '01' || tipoCliente == '03') {

            if ($.trim(ruc) == "") {
                return;
            }

            $.ajax({
                type: "POST",
                data:
                {
                    ruc: ruc,
                    tipo: tipoCliente
                },
                beforeSend: function () {
                    $('#loading').show();
                },
                url: '/Maintenance/Cliente/BuscarEmpresa',
                success: function (response, textStatus, jqXhr) {

                    if (response != null) {

                        if (tipoCliente == "01") {
                            $('#txtRazonSocial').val(response.nombre);
                            $('#txtUbigeo').val(response.ubigeo);
                            $('#txtDireccionFacturacion').val(response.direccion);

                            var ubigeo = response.ubigeo;
                            var dpto = response.ubigeo.substring(0, 2);
                            departamento.val(dpto);
                            clientejs.buscarProvinciaPorDeptCodigo(ubigeo);
                        } else {
                            $('#txtNombreCompleto').val(response.nombres + " " + response.apellidoPaterno + " " + response.apellidoMaterno);
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
        }

        
    },

    buscarRUCEditar: function () {

        var ruc = $('#txtNumeroDocumentoEditar').val();
        var tipoCliente = $('#cboTipoClienteEditar').val();
        var departamento = $('#cboDepartamentoEditar');

        if (tipoCliente == '01' || tipoCliente == '03') {

            if ($.trim(ruc) == "") {
                return;
            }

            $.ajax({
                type: "POST",
                data:
                {
                    ruc: ruc,
                    tipo: tipoCliente
                },
                beforeSend: function () {
                    $('#loading').show();
                },
                url: '/Maintenance/Cliente/BuscarEmpresa',
                success: function (response, textStatus, jqXhr) {

                    if (response != null) {

                        if (tipoCliente == "01") {
                            $('#txtRazonSocialEditar').val(response.nombre);
                            $('#txtUbigeoEditar').val(response.ubigeo);
                            $('#txtFacturacionDireccionEditar').val(response.direccion);

                            var ubigeo = response.ubigeo;
                            var dpto = response.ubigeo.substring(0, 2);
                            departamento.val(dpto);
                            clientejs.buscarProvinciaPorDeptCodigoEditar(ubigeo);
                        } else {
                            $('#txtNombreCompletoEditar').val(response.nombres + " " + response.apellidoPaterno + " " + response.apellidoMaterno);
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
        }


    },

    limpiarCampos: function () {

        $("#txtNombres").val('');
        $("#txtDireccion").val('');
        $("#txtTelefono").val('');
        $("#cboTipoCliente").val('');
        $("#txtNumeroDocumento").val('');
        $("#txtNombreCompleto").val('');
        $("#txtRazonSocial").val('');
        $("#cboDepartamento").val('');
        $("#cboProvincia").val('');
        $("#cboDistrito").val('');
        $("#txtUbigeo").val('');
        $("#txtDireccionFacturacion").val('');
    }
}

$(function () {

    clientejs.initializeEvent();

});