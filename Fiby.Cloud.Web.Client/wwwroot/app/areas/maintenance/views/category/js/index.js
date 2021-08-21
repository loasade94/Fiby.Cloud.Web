var categoryjs = {
    CategoryId: 0,

    initializeEvent: function () {

        $("#btnSearchCategory").click(function () {
            categoryjs.search();
        });

        $("#btnNewCategory").click(function () {
            categoryjs.openModal();
        });

        categoryjs.search();

    },

    search: function () {

        var txtDescription = $("#txtDescriptionCategorySearch").val();

        var categoryDTORequest = {
            Description: txtDescription
        };

        var html = "";

        $.ajax({
            type: "POST",
            data:
            {
                categoryDTORequest
            },
            url: '/Maintenance/Category/GetCategoryAll',
            // dataType: "json",
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbCategoryAllMaintenance > tbody").html("");
                    $('#tbCategoryAllMaintenance').DataTable().clear().destroy();

                    if (response.length > 0) {

                        var html = "";
                        var eje = "";
                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td>';
                            html += '        <div class="ctn-btn-tabla ctn-btn-tabla-mx">';
                            html += '            <img src="/cssadmin/assets/images/edit.png" onclick="categoryjs.edit(\'' + response[i].categoryId + '\',\'' + 0 + '\');" alt="Editar" data-toggle="tooltip" title="Editar" ';
                            html += '                      class="material-tooltip-main" data-id-ctn="_editar-consulta">';
                            html += '            <img src="/cssadmin/assets/images/trash.png" onclick="categoryjs.deleteCategory(\'' + response[i].categoryId + '\');" class="material-tooltip-main eliminar-movimiento" alt="Eliminar" ';
                            html += '                      data-toggle="tooltip" title="Eliminar">';
                            html += '        </div>';
                            html += '    </td>';
                            html += '    <td style="text-align:center;">' + response[i].categoryId + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].description + '</td>';

                            if (response[i].active == true) {
                                html += '<td style="text-align:center;">Activo</td>';
                            } else {
                                html += '<td style="text-align:center;">Inactivo</td>';
                            }

                            html += '</tr>';
                        }
                        $('#tbCategoryAllMaintenance tbody').append(html);
                    }
                    CreateDataTable('tbCategoryAllMaintenance');
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

    openModal: function (categoryId, option) {
        var url = '/Maintenance/Category/RegisterUpdateCategory';
        $.get(url, {
            categoryId: categoryId, option: option
        }, function (data) {
            createModal(data);
            //createSelect('cboRegistrationStatusNew');
        });
    },

    getParametersInput: function () {
        var txtModalDescription = $('#txtModalDescription').val();
        var cboModalActive = $('#cboModalActive').val();

        var request = {
            CategoryId: $("#hiddenCategoryId").val(),
            Description: txtModalDescription,
            Active: cboModalActive == 1 ? true : false,
        };

        return request;
    },

    validateForm: function (request) {

        var returns = true;

        if (request.CategoryId == "") {
            CategoryId = 0;
        } else if (request.Description == "") {
            ModalAlert("Debe ingresar la descripción.");
            returns = false;
        }

        return returns;
    },

    save: function () {
        var request = categoryjs.getParametersInput();
        if (categoryjs.validateForm(request)) {
            $.ajax({
                type: "POST",
                data: { request: request },
                url: '/Maintenance/Category/RegisterOrUpdateCategory',
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
                    categoryjs.search();
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

    deleteCategory: function (obj) {
        ModalConfirm('¿Seguro que desea eliminar el registro?', 'categoryjs.deleteCategory_callback(\'' + obj + '\');');
    },

    deleteCategory_callback: function (obj) {

        var categoryId = obj;

        $.ajax({
            type: "DELETE",
            data: {
                categoryId
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Maintenance/Category/DeleteCategory',
            success: function (response, textStatus, jqXhr) {

                if (response == "1") {
                    /*IziToastMessage(0, 'Eliminado Correctamente', 'green', 'topRight', 5000);*/
                    ModalAlert("OK");
                    categoryjs.search();
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
        categoryjs.openModal(rolId, option);
        $('html, body').animate({ scrollTop: 0 }, 'slow');
    },

}

$(function () {

    categoryjs.initializeEvent();

});