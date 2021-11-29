var serviciosemanajs = {


    initializeEvent: function () {

        CreatePickaDate('txtFecha');

        $("#btnBuscar").click(function () {
            serviciosemanajs.buscarServicio();
        });

        serviciosemanajs.buscarServicio();

        $('#cboEmpleado').change(function () {
            serviciosemanajs.buscarServicio();
        });


    },

    buscarServicio: function () {

        var calendarioDTORequest = {
            IdEmpleado: $("#cboEmpleado").val(),
            FechaText: $("#txtFecha").val()
        };

        $.ajax({
            type: "POST",
            data:
            {
                calendarioDTORequest
            },
            beforeSend: function () {
                $('#loading').show();
            },
            url: '/Horario/Calendario/BuscarServicioXEmpleado',
            success: function (response, textStatus, jqXhr) {

                //var myCalendar = $('#calendar');

                if (response != null) {
                    $("#tbServiciosPorEmpleado > tbody").html("");
                    $('#tbServiciosPorEmpleado').DataTable().clear().destroy();

                    if (response.length > 0) {

                        var indice = 0;

                        var html = "";
                        var eje = "";
                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td style="text-align:center;">' + response[i].nombreDia + ' ' + response[i].numeroDia + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].clienteOpcional + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].horaInicio + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].horaFin + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].direccion + '</td>';
                            html += '</tr>';
                            indice += 1;


                        }
                        $('#tbServiciosPorEmpleado tbody').append(html);

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
    },

}


$(function () {

    serviciosemanajs.initializeEvent();

});