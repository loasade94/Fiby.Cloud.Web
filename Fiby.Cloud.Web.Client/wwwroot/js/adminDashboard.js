$(function () {
    adminDashboard.initializeEventDashRent();
    
});

var adminDashboard = {

    initializeEventDashRent: function () {

        adminDashboard.GetDataDashBoardAdmin(function (data) {

            adminDashboard.LoadDashBoarAdminDetail(data);
        });

    },

    initializeEventDashPasajes: function () {

        adminDashboard.GetDataDashPasajesAdmin(function (data) {

            adminDashboard.LoadDashBoarAdminPasajes(data);
        });

    },
    //GANANCIAS INICIO
    GetDataDashBoardAdmin: function (callback) {

        var ListData = {

            ListNombres: [],
            ListMontos: []

        }

        $.ajax({
            type: "POST",
            //data: { request: request },
            url: '/Reportes/ReporteSemana/BuscarRentabilidadGraficoDashboard',
            // dataType: "json",
            beforeSend: function () {


            },
            complete: function () {


            },
            success: function (response, textStatus, jqXhr) {

                console.log('response GetDataDashBoardAdmin');
                console.log(response);

                var ListNombres = [];
                var ListMontos = [];

                if (response != null) {

                    for (var i = 0; i < response.listaNombres.length; i++) {
                        ListNombres[i] = response.listaNombres[i].nombreSemana;
                    }

                    for (var i = 0; i < response.listaMontos.length; i++) {
                        ListMontos[i] = response.listaMontos[i].monto;
                    }
                }

                ListData.ListNombres.push(ListNombres);
                ListData.ListMontos.push(ListMontos);

                callback(ListData);
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                callback(ListData);
            },
            async: true,
        })
    },

    LoadDashBoarAdminDetail: function (data) {

        if (document.getElementById("ControlDashBoardAssistanceDetail") != null) {
            var ctxL = document.getElementById("ControlDashBoardAssistanceDetail").getContext('2d');
            var myLineChart = new Chart(ctxL, {
                type: 'line',
                data: {
                    labels: data.ListNombres[0],
                    datasets: [{
                        label: "Ganancias",
                        data: data.ListMontos[0],
                        backgroundColor: [
                            'rgba(0, 255, 34, 1)',
                        ],
                        borderColor: [
                            'rgba(0, 255, 34, 0.7)',
                        ],
                        borderWidth: 2
                    }
                    //    ,
                    //{
                    //    label: "Tardanzas",
                    //    data: data.ListDataDelay[0],
                    //    backgroundColor: [
                    //        'rgba(0, 137, 132, .2)',
                    //    ],
                    //    borderColor: [
                    //        'rgba(0, 10, 130, .7)',
                    //    ],
                    //    borderWidth: 2
                    //}
                    ]
                },
                options: {
                    responsive: true
                }
            });
        }

        adminDashboard.initializeEventDashPasajes();
    },
    //GANANCIAS FIN

    //PASAJES INICIO
    GetDataDashPasajesAdmin: function (callback) {

        var ListData = {

            ListNombres: [],
            ListMontos1: [],
            ListMontos2: [],
            ListMontos3: []

        }

        $.ajax({
            type: "POST",
            //data: { request: request },
            url: '/Reportes/ReporteSemana/BuscarPasajesEmpleadoDashboard',
            // dataType: "json",
            beforeSend: function () {


            },
            complete: function () {


            },
            success: function (response, textStatus, jqXhr) {

                console.log('response GetDataDashBoardAdmin');
                console.log(response);

                var ListNombres = [];
                var ListMontos1 = [];
                var ListMontos2 = [];
                var ListMontos3 = [];

                if (response != null) {

                    for (var i = 0; i < response.listaNombres.length; i++) {
                        ListNombres[i] = response.listaNombres[i].nombreSemana;
                    }

                    for (var i = 0; i < response.listaMontos1.length; i++) {
                        ListMontos1[i] = response.listaMontos1[i].monto;
                    }

                    for (var i = 0; i < response.listaMontos2.length; i++) {
                        ListMontos2[i] = response.listaMontos2[i].monto;
                    }

                    for (var i = 0; i < response.listaMontos3.length; i++) {
                        ListMontos3[i] = response.listaMontos3[i].monto;
                    }
                }

                ListData.ListNombres.push(ListNombres);
                ListData.ListMontos1.push(ListMontos1);
                ListData.ListMontos2.push(ListMontos2);
                ListData.ListMontos3.push(ListMontos3);

                callback(ListData);
            },
            error: function (xhr, status, errorThrown) {
                var err = "Status: " + status + " " + errorThrown;
                console.log(err);
                callback(ListData);
            },
            async: true,
        })
    },

    LoadDashBoarAdminPasajes: function (data) {

        if (document.getElementById("ControlDashBoardPasajes") != null) {
            var ctxL = document.getElementById("ControlDashBoardPasajes").getContext('2d');
            var myLineChart = new Chart(ctxL, {
                type: 'line',
                data: {
                    labels: data.ListNombres[0],
                    datasets: [{
                        label: "Elsa",
                        data: data.ListMontos1[0],
                        backgroundColor: [
                            'rgba(105, 0, 132, .2)',
                        ],
                        borderColor: [
                            'rgba(200, 99, 132, .7)',
                        ],
                        borderWidth: 2
                    },
                    {
                        label: "Katelin",
                        data: data.ListMontos2[0],
                        backgroundColor: [
                            'rgba(0, 137, 132, .2)',
                        ],
                        borderColor: [
                            'rgba(0, 10, 130, .7)',
                        ],
                        borderWidth: 2
                    },
                    {
                        label: "Brenda",
                        data: data.ListMontos3[0],
                        backgroundColor: [
                            'rgba(249, 105, 14, .1)',
                        ],
                        borderColor: [
                            'rgba(100, 10, 14, .7)',
                        ],
                        borderWidth: 2
                    }
                    ]
                },
                options: {
                    responsive: true
                }
            });
        }
    },

    //PASAJES FIN

}