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
            Nombres: $("#txtNombres").val()
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
            Nombres: $("#txtNombreEditar").val()
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
}

$(function () {

    clientejs.initializeEvent();

});