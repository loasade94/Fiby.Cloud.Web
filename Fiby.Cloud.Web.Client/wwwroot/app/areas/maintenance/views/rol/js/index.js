var roljs = {
    RolId: 0,

    initializeEvent: function () {

        $("#btnSearchRol").click(function () {
            roljs.search();
        });

        $("#btnNewRol").click(function () {
            roljs.openModal();
        });

        roljs.search();

    },

    search: function () {

        var txtDescription = $("#txtDescriptionRolSearch").val();

        var rolDTORequest = {
            Description: txtDescription
        };

        var html = "";

        $.ajax({
            type: "POST",
            data:
            {
                rolDTORequest
            },
            url: '/Maintenance/Rol/GetRolAll',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbRolAllMaintenance > tbody").html("");
                    $('#tbRolAllMaintenance').DataTable().clear().destroy();

                    if (response.length > 0) {
  
                        var html = "";
                        var eje = "";
                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td>';
                            html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                            html += '            <img src="/cssadmin/assets/images/edit.png" onclick="roljs.edit(\'' + response[i].rolId + '\',\'' + 0 +  '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                            html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                            html += '            <img src="/cssadmin/assets/images/trash.png" onclick="roljs.deleteRol(\'' + response[i].rolId + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
                            html += '                      data-toggle="tooltip" title="Eliminar">';
                            html += '        </div>';
                            html += '    </td>';
                            html += '    <td style="text-align:center;">' + response[i].rolId + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].description + '</td>';

                            if (response[i].active == true) {
                                html += '<td style="text-align:center;">Activo</td>';
                            } else {
                                html += '<td style="text-align:center;">Inactivo</td>';
                            }

                            html += '</tr>';
                        }
                        $('#tbRolAllMaintenance tbody').append(html);
                    }
                    roljs.intitDataTable();
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

    intitDataTable: function () {

        $('#tbRolAllMaintenance').DataTable({
            "language": {
                "lengthMenu": "Mostrar _MENU_ registros por página",
                "zeroRecords": "No se ha encontrado nada",
                "info": "Mostrando página _PAGE_ de _PAGES_",
                "infoEmpty": "No hay registros disponibles",
                "infoFiltered": "(filtered from _MAX_ total records)",
                //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                "processing": "Procesando...",
                "search": "Buscar:",
                "paginate": {
                    "first": "Primero",
                    "last": "Último",
                    "next": "Siguiente",
                    "previous": "Anterior"
                },
                "aria": {
                    "sortAscending": ": activar para ordenar columnas ascendentemente",
                    "sortDescending": ": activar para ordenar columnas descendentemente"
                }
            }
        });
    },

    openModal: function (rolId, option) {
        var url = '/Maintenance/Rol/RegisterUpdateRol';
        $.get(url, {
            rolId: rolId, option: option
        }, function (data) {
            createModal(data);
            //createSelect('cboRegistrationStatusNew');
        });
    },

    getParametersInput: function () {
        var txtModalDescription = $('#txtModalDescription').val();
        var cboModalActive = $('#cboModalActive').val();

        var request = {
            RolId: $("#hiddenRolId").val(),
            Description: txtModalDescription,
            Active: cboModalActive == 1 ? true : false,
        };

        return request;
    },

    validateForm: function (request) {

        var returns = true;

        if (request.RolId == "") {
            RolId = 0;
        }else if (request.Description == "") {
            ModalAlert("Debe ingresar la descripción.");
            returns = false;
        }

        return returns;
    },

    save: function () {
        var request = roljs.getParametersInput();
        if (roljs.validateForm(request)) {
            $.ajax({
                type: "POST",
                data: { request: request },
                url: '/Maintenance/Rol/RegisterOrUpdateRol',
                beforeSend: function () {
                    $('#loading').show();
                },
                success: function (response, textStatus, jqXhr) {
                    if (response == '1') {
                        //var xmessage = 'Los datos fueron grabados satisfactoriamente';
                        //var xcolor = 'green';
                        //var xposition = 'topRight';
                        //var xtimeout = 2000;
                        //IziToastMessage(0, xmessage, xcolor, xposition, xtimeout);
                        ModalAlert('OK');
                        $('#modal-register').modal('hide');
                    } else {
                        $('#modal-register').modal('hide');
                        ModalAlert(response);
                    }
                    roljs.search();
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

    deleteRol: function (obj) {
        ModalConfirm('¿Seguro que desea eliminar el registro?', 'roljs.deleteRol_callback(\'' + obj + '\');');
    },

    deleteRol_callback: function (obj) {

        var rolId = obj;

        $.ajax({
            type: "DELETE",
            data: {
                rolId
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Rol/DeleteRol',
            success: function (response, textStatus, jqXhr) {

                if (response == "1") {
                    /*IziToastMessage(0, 'Eliminado Correctamente', 'green', 'topRight', 5000);*/
                    ModalAlert("OK");
                    roljs.search();
                } else {
                    /* IziToastMessage(1, 'Ocurrio un error: ' + response, '', 'topRight', 5000);*/
                    ModalAlert("ERROR");
                    $('#loading').hide();
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

    edit: function (rolId, option) {
        roljs.openModal(rolId, option);
        $('html, body').animate({ scrollTop: 0 }, 'slow');
    },

}

$(function () {

    roljs.initializeEvent();

});