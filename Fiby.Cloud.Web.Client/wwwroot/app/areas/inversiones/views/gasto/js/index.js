var gastojs = {

    initializeEvent: function () {

        $("#btnRegistrar").click(function () {
            gastojs.registrarGasto();
        });

        $("#btnBuscar").click(function () {
            gastojs.buscarGasto();
        });

        gastojs.buscarGasto();

    },

    buscarGasto: function () {

        var gastoDTORequest = {
            IdSemana: $("#cboSemana").val(),
            Descripcion: $("#txtDescripcion").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                gastoDTORequest
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Inversiones/Gasto/BuscarGastos',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbGasto > tbody").html("");
                    $('#tbGasto').DataTable().clear().destroy();

                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td>';
                            html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                            html += '            <img src="/cssadmin/assets/images/edit.png" onclick="gastojs.edit(\'' + response[i].idGasto + '\',\'' + 0 + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                            html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                            html += '            <img src="/cssadmin/assets/images/trash.png" onclick="gastojs.eliminarGasto(\'' + response[i].idGasto + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
                            html += '                      data-toggle="tooltip" title="Eliminar">';
                            html += '        </div>';
                            html += '    </td>';
                            html += '    <td style="text-align:center;">' + response[i].descripcion + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].monto + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].nombreCategoria + '</td>';
                            html += '</tr>';

                        }
                        $('#tbGasto tbody').append(html);

                    }
                    gastojs.intitDataTable();
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
        CreateDataTable('tbGasto');
    },

    registrarGasto: function (obj) {
        var gasto = $("#txtDescripcion").val();

        if (gasto == null || gasto == '') {
            ModalAlertCancel('Debe Ingresar un Gasto');
            return;
        }

        ModalConfirm('¿Seguro que desea registrar?', 'gastojs.registrarGasto_callback();');
    },

    registrarGasto_callback: function () {

        var gastoDTORequest = {
            IdSemana: $("#cboSemana").val(),
            Descripcion: $("#txtDescripcion").val(),
            MontoText: $("#txtMonto").val(),
            Adicional: $("#txtAdicional").val()
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                gastoDTORequest
            },
            url: '/Inversiones/Gasto/RegistrarGasto',
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
                    gastojs.limpiar();
                    gastojs.buscarGasto();
                }
                else {
                    ModalAlertCancel('Error al registrar : ' + response);
                    gastojs.buscarGasto();
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

    eliminarGasto: function (obj) {
        ModalConfirm('¿Seguro que desea eliminar el registro?', 'gastojs.eliminarGasto_callback(\'' + obj + '\');');
    },

    eliminarGasto_callback: function (obj) {

        var idGasto = obj;

        $.ajax({
            type: "DELETE",
            data: {
                idGasto
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Inversiones/Gasto/EliminarGasto',
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                    /*IziToastMessage(0, 'Eliminado Correctamente', 'green', 'topRight', 5000);*/
                    ModalAlert("Eliminado Correctamente");
                    gastojs.buscarGasto();
                } else {
                    /* IziToastMessage(1, 'Ocurrio un error: ' + response, '', 'topRight', 5000);*/
                    ModalAlertCancel("ERROR");
                    gastojs.buscarGasto();
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

    edit: function (idGasto, option) {
        gastojs.openModal(idGasto, option);
        //$('html, body').animate({ scrollTop: 0 }, 'slow');
    },

    openModal: function (idGasto, option) {
        var url = '/Inversiones/Gasto/Actualizar';
        $.get(url, {
            idGasto: idGasto, option: option
        }, function (data) {
            createModal(data);
            /*            CreatePickaDate('txtFechaEditar');*/
            //CreatePickaTime('txtHoraInicioEditar');
            //CreatePickaTime('txtHoraFinEditar');
            //createSelect('cboRegistrationStatusNew');
        });
    },

    actualizarGasto: function (obj) {
        var gasto = $("#txtDescripcionEditar").val();

        if (gasto == null || gasto == '') {
            ModalAlertCancel('Debe Ingresar un Gasto');
            return;
        }


        ModalConfirm('¿Seguro que desea registrar?', 'gastojs.actualizarGasto_callback();');
    },

    actualizarGasto_callback: function () {

        var gastoDTORequest = {
            IdGasto: $("#hiddenGastoId").val(),
            IdSemana: $("#cboSemanaEditar").val(),
            Descripcion: $("#txtDescripcionEditar").val(),
            MontoText: $("#txtMontoEditar").val()
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                gastoDTORequest
            },
            url: '/Inversiones/Gasto/RegistrarGasto',
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
                    gastojs.limpiar();
                    gastojs.buscarGasto();
                    
                }
                else {
                    $('#modal-register').modal('hide');
                    ModalAlertCancel('Error al registrar : ' + response);
                    gastojs.buscarGasto();
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

    limpiar: function () {
        $("#txtDescripcion").val('');
        $("#txtMonto").val('');
    }
}

$(function () {

    gastojs.initializeEvent();

});