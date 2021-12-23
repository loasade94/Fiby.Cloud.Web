var anunciojs = {

    initializeEvent: function () {

        $("#btnRegistrar").click(function () {
            anunciojs.registrarAnuncio();
        });

        $("#btnBuscar").click(function () {
            anunciojs.buscarAnuncio();
        });

        CreatePickaDate('txtFechaInicio');
        CreatePickaDate('txtFechaFin');

        anunciojs.buscarAnuncio();

    },

    buscarAnuncio: function () {

        var anuncioDTORequest = {
            Titulo: $("#txtTitulo").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                anuncioDTORequest
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Anuncio/BuscarAnuncios',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbAnuncio > tbody").html("");
                    $('#tbAnuncio').DataTable().clear().destroy();

                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td>';
                            html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                            html += '            <img src="/cssadmin/assets/images/edit.png" onclick="anunciojs.edit(\'' + response[i].idAnuncio + '\',\'' + 0 + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                            html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                            html += '            <img src="/cssadmin/assets/images/trash.png" onclick="anunciojs.eliminarAnuncio(\'' + response[i].idAnuncio + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
                            html += '                      data-toggle="tooltip" title="Eliminar">';
                            html += '        </div>';
                            html += '    </td>';
                            html += '    <td style="text-align:center;">' + response[i].titulo + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].detalle + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].orden + '</td>';
                            html += '    <td style="text-align:center;">' + formatDateTime(response[i].fechaInicio) + '</td>';
                            html += '    <td style="text-align:center;">' + formatDateTime(response[i].fechaFin) + '</td>';
                            html += '</tr>';

                        }
                        $('#tbAnuncio tbody').append(html);

                    }
                    anunciojs.intitDataTable();
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
        CreateDataTable('tbAnuncio');
    },

    registrarAnuncio: function (obj) {
        var titulo = $("#txtTitulo").val();
        var detalle = $("#txtDetalle").val();
        var orden = $("#txtOrden").val();
        var fechaInicio = $("#txtFechaInicio").val();
        var fechaFin = $("#txtFechaFin").val();

        if (titulo == null || titulo == '') {
            ModalAlertCancel('Debe Ingresar un Titulo');
            return;
        }

        if (detalle == null || detalle == '') {
            ModalAlertCancel('Debe Ingresar un Detalle');
            return;
        }

        if (orden == null || orden == '') {
            ModalAlertCancel('Debe Ingresar un Orden');
            return;
        }

        if (fechaInicio == null || fechaInicio == '') {
            ModalAlertCancel('Debe Ingresar una Fecha de Inicio');
            return;
        }

        if (fechaFin == null || fechaFin == '') {
            ModalAlertCancel('Debe Ingresar una Fecha Fin');
            return;
        }

        ModalConfirm('¿Seguro que desea registrar?', 'anunciojs.registrarAnuncio_callback();');
    },

    registrarAnuncio_callback: function () {

        var titulo = $("#txtTitulo").val();
        var detalle = $("#txtDetalle").val();
        var orden = $("#txtOrden").val();
        var fechaInicio = $("#txtFechaInicio").val();
        var fechaFin = $("#txtFechaFin").val();

        var anuncioDTORequest = {
            IdAnuncio : 0,
            Titulo: titulo,
            Detalle: detalle,
            Orden: orden,
            FechaInicioText: fechaInicio,
            FechaFinText: fechaFin
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                anuncioDTORequest
            },
            url: '/Maintenance/Anuncio/RegistrarAnuncio',
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
                    anunciojs.limpiar();
                    anunciojs.buscarAnuncio();
                }
                else {
                    ModalAlertCancel('Error al registrar : ' + response);
                    anunciojs.buscarAnuncio();
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

    eliminarAnuncio: function (obj) {
        ModalConfirm('¿Seguro que desea eliminar el registro?', 'anunciojs.eliminarAnuncio_callback(\'' + obj + '\');');
    },

    eliminarAnuncio_callback: function (obj) {

        var idAnuncio = obj;

        $.ajax({
            type: "DELETE",
            data: {
                idAnuncio
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Anuncio/EliminarAnuncio',
            success: function (response, textStatus, jqXhr) {

                if (response == "OK") {
                    /*IziToastMessage(0, 'Eliminado Correctamente', 'green', 'topRight', 5000);*/
                    ModalAlert("Eliminado Correctamente");
                    anunciojs.buscarAnuncio();
                } else {
                    /* IziToastMessage(1, 'Ocurrio un error: ' + response, '', 'topRight', 5000);*/
                    ModalAlertCancel("ERROR");
                    anunciojs.buscarAnuncio();
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

    edit: function (idAnuncio, option) {
        anunciojs.openModal(idAnuncio, option);
        //$('html, body').animate({ scrollTop: 0 }, 'slow');
    },

    openModal: function (idAnuncio, option) {
        var url = '/Maintenance/Anuncio/Actualizar';
        $.get(url, {
            idAnuncio: idAnuncio, option: option
        }, function (data) {
            createModal(data);
            CreatePickaDate('txtFechaInicioEditar');
            CreatePickaDate('txtFechaFinEditar');
            //CreatePickaTime('txtHoraInicioEditar');
            //CreatePickaTime('txtHoraFinEditar');
            //createSelect('cboRegistrationStatusNew');
        });
    },

    actualizarAnuncio: function (obj) {
        var titulo = $("#txtTituloEditar").val();
        var detalle = $("#txtDetalleEditar").val();
        var orden = $("#txtOrdenEditar").val();
        var fechaInicio = $("#txtFechaInicioEditar").val();
        var fechaFin = $("#txtFechaFinEditar").val();

        if (titulo == null || titulo == '') {
            ModalAlertCancel('Debe Ingresar un Titulo');
            return;
        }

        if (detalle == null || detalle == '') {
            ModalAlertCancel('Debe Ingresar un Detalle');
            return;
        }

        if (orden == null || orden == '') {
            ModalAlertCancel('Debe Ingresar un Orden');
            return;
        }

        if (fechaInicio == null || fechaInicio == '') {
            ModalAlertCancel('Debe Ingresar una Fecha de Inicio');
            return;
        }

        if (fechaFin == null || fechaFin == '') {
            ModalAlertCancel('Debe Ingresar una Fecha Fin');
            return;
        }


        ModalConfirm('¿Seguro que desea registrar?', 'anunciojs.actualizarAnuncio_callback();');
    },

    actualizarAnuncio_callback: function () {

        var titulo = $("#txtTituloEditar").val();
        var detalle = $("#txtDetalleEditar").val();
        var orden = $("#txtOrdenEditar").val();
        var fechaInicio = $("#txtFechaInicioEditar").val();
        var fechaFin = $("#txtFechaFinEditar").val();

        var anuncioDTORequest = {
            IdAnuncio: $("#hiddenAnuncioId").val(),
            Titulo: titulo,
            Detalle: detalle,
            Orden: orden,
            FechaInicioText: fechaInicio,
            FechaFinText: fechaFin
        };

        /*        var html = "";*/

        $.ajax({
            type: "POST",
            data:
            {
                anuncioDTORequest
            },
            url: '/Maintenance/Anuncio/RegistrarAnuncio',
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
                    anunciojs.limpiar();
                    anunciojs.buscarAnuncio();

                }
                else {
                    $('#modal-register').modal('hide');
                    ModalAlertCancel('Error al registrar : ' + response);
                    anunciojs.buscarAnuncio();
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
        $("#txtTitulo").val('');
        $("#txtDetalle").val('');
        $("#txtOrden").val('');
    }
}

$(function () {

    anunciojs.initializeEvent();

});