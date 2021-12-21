$(function () {
    empleadoDashboard.initializeEvent();

});

var empleadoDashboard = {

    initializeEvent: function () {

        empleadoDashboard.buscarAnuncios();
     
    },

    buscarAnuncios: function () {

        //var reporteSemanaDTORequest = {
        //    IdSemana: $('#cboSemana').val()
        //};

        $.ajax({
            type: "POST",
            //data:
            //{
            //    reporteSemanaDTORequest
            //},
            //beforeSend: function () {
            //    $('#loading').show();
            //},
            //complete: function () {
            //    $('#loading').hide();
            //},
            url: '/Reportes/ReporteSemana/BuscarAnunciosParaEmpleados',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#ulAnuncios").html("");

                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            if (i == 0) {
                                html += '<li class="d-flex no-block card-body">';
                                html += '    <i class="mdi mdi-comment-question-outline fs-4 w-30px mt-1"></i>';
                                html += '    <div>';
                                html += '       <a href="#" class="mb-0 font-medium p-0">';
                                html +=             response[i].titulo;
                                html += '       </a>';
                                html += '       <span class="text-muted">' + response[i].detalle + '</span>';
                                html += '    </div>';
                                html += '    <div class="ms-auto">';
                                html += '       <div class="tetx-right">';
                                html += '           <h5 class="text-muted mb-0">' + response[i].dia + '</h5>';
                                html += '           <span class="text-muted font-16">' + response[i].mes + '</span>';
                                html += '       </div>';
                                html += '    </div>';
                                html += '</li>';
                            } else {
                                html += '<li class="d-flex no-block card-body border-top">';
                                html += '    <i class="mdi mdi-comment-question-outline fs-4 w-30px mt-1"></i>';
                                html += '    <div>';
                                html += '       <a href="#" class="mb-0 font-medium p-0">';
                                html +=             response[i].titulo;
                                html += '       </a>';
                                html += '       <span class="text-muted">' + response[i].detalle + '</span>';
                                html += '    </div>';
                                html += '    <div class="ms-auto">';
                                html += '       <div class="tetx-right">';
                                html += '           <h5 class="text-muted mb-0">' + response[i].dia + '</h5>';
                                html += '           <span class="text-muted font-16">' + response[i].mes + '</span>';
                                html += '       </div>';
                                html += '    </div>';
                                html += '</li>';
                            }



                        }
                        $('#ulAnuncios').append(html);

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