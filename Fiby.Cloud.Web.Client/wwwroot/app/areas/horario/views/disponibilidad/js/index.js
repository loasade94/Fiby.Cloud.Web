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

                            if (response[i].lunes == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:green;color:white;text-align:center;">' + response[i].lunes + '</td>';
                            }

                            if (response[i].martes == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:green;color:white;text-align:center;">' + response[i].martes + '</td>';
                            }

                            if (response[i].miercoles == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:green;color:white;text-align:center;">' + response[i].miercoles + '</td>';
                            }

                            if (response[i].jueves == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:green;color:white;text-align:center;">' + response[i].jueves + '</td>';
                            }

                            if (response[i].viernes == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:green;color:white;text-align:center;">' + response[i].viernes + '</td>';
                            }

                            if (response[i].sabado == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:green;color:white;text-align:center;">' + response[i].sabado + '</td>';
                            }

                            if (response[i].domingo == "0") {
                                html += '<td style="background-color:red;color:white;text-align:center;">Ocupado</td>';
                            } else {
                                html += '<td style="background-color:green;color:white;text-align:center;">' + response[i].domingo + '</td>';
                            }

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