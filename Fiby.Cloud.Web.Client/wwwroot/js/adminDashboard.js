$(function () {
    adminDashboard.initializeEvent();
});

var adminDashboard = {

    initializeEvent: function () {

        adminDashboard.GetDataDashBoardAdmin(function (data) {

            adminDashboard.LoadDashBoarAdminDetail(data);
        });

    },

    GetDataDashBoardAdmin: function (callback) {
        //return;
        //var request = {
        //    CodeOrganizationalUnit: codeOrganizationalUnit,
        //    YearPeriod: yearPeriod,
        //    CodePeriod: codePeriod

        //};

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
    },

}