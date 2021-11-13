var disponibilidadjs = {

    initializeEvent: function () {

        //$("#btnBuscar").click(function () {
        //    calendariojs.buscarServicio();
        //});

        disponibilidadjs.buscarDisponibilidad();
    },
    
    buscarDisponibilidad: function () {

        var semanaDTORequest = {
            IdSemana: 1
        };

        $.ajax({
            type: "POST",
            data:
            {
                semanaDTORequest
            },
            url: '/Horario/Disponibilidad/GetDisponibilidadSemana',
            success: function (response, textStatus, jqXhr) {

                if (response != null) {
                    $("#tbDisponibilidadSemana > tbody").html("");
                    
                    if (response.length > 0) {

                        var html = "";

                        for (var i = 0; i < response.length; i++) {

                            html += '<tr>';
                            html += '    <td style="text-align:center;">' + response[i].horario + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].lunes + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].martes + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].miercoles + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].jueves + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].viernes + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].sabado + '</td>';
                            html += '    <td style="text-align:center;">' + response[i].domingo + '</td>';
                            html += '</tr>';

                        }
                        $('#tbDisponibilidadSemana tbody').append(html);

                    }

                }
            },
            complete: function () {

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

    disponibilidadjs.initializeEvent();

});