var reporteloginjs = {

    initializeEvent: function () {

        $('#btnBuscar').on('click', function () {
            reporteloginjs.buscar();
        });

        reporteloginjs.createDataTableReport('tbReporteLogin');

    },

    buscar: function () {

        var idempleado = $("#cboEmpleado").val();

        var div = "#divGrid";
        var request = {
            IdEmpleado: idempleado
        };

        $.ajax({
            type: "POST",
            data:
            {
                request
            },
            url: '/Reportes/ReporteLogin/BuscarReporteLogin',
            beforeSend: function () {
                $('#loading').show();
            },
            complete: function () {
                $('#loading').hide();
            },
            success: function (response, textStatus, jqXhr) {
                $("#tbReporteLogin > tbody").html("");
                $(div).html(response); reporteloginjs.createDataTableReport('tbReporteLogin');
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                $('#loading').hide();
            },
            async: true,
        })
    },

    createDataTableReport: function (value) {

        $('#' + value).DataTable({
            "ordering": false,
            "aoColumnDefs": [
                { "bSortable": false, "aTargets": [0, 1, 2] },
                { "bSearchable": false, "aTargets": [0, 1, 2] }
            ],
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
            }
        });
    }
}


$(function () {

    reporteloginjs.initializeEvent();

});