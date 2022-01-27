function ModalAlert(message) {
    var html = '';

    $("#modalAlert").remove();

    html += '<div class="modal fade" id="modalAlert" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static">';
    html += '<div class="modal-dialog" role="document">';
    html += '<div class="modal-content">';
    html += '<div class="text-center">';
    html += '<img src="../img/confirm.png" style="width: 100px;" />';
    html += '<h4 style="margin-top: 20px; font-weight: 600;">';
    html += message;
    html += '</h4>';
    html += '</div>';
    html += '<div class="modal-footer" style="align-self: center;">';
    html += '<button type="button" id="btnModalAlertClose" class="btn btn-primary btn2" data-dismiss="modal">';
    html += 'OK';
    html += '</button>';
    html += '</div>';
    html += '</div>';
    html += '</div>';
    html += '</div>';

    $("#divSection").append(html);
    $("#modalAlert").modal('show');

    $("#btnModalAlertClose").click(function () {
        document.getElementsByClassName('modal-backdrop fade show')[0].remove();
    });

};

function ModalAlertCancel(message) {
    var html = '';

    $("#modalAlert").remove();

    html += '<div class="modal fade" id="modalAlert" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static">';
    html += '<div class="modal-dialog" role="document">';
    html += '<div class="modal-content">';
    html += '<div class="text-center">';
    html += '<img src="../img/cancel.png" style="width: 100px;" />';
    html += '<h4 style="margin-top: 20px; font-weight: 600;">';
    html += message;
    html += '</h4>';
    html += '</div>';
    html += '<div class="modal-footer" style="align-self: center;">';
    html += '<button type="button" id="btnModalAlertClose" class="btn btn-primary btn2" data-dismiss="modal">';
    html += 'OK';
    html += '</button>';
    html += '</div>';
    html += '</div>';
    html += '</div>';
    html += '</div>';

    $("#divSection").append(html);
    $("#modalAlert").modal('show');

    $("#btnModalAlertClose").click(function () {
        document.getElementsByClassName('modal-backdrop fade show')[0].remove();
    });

};

function ModalConfirm(message, xfunction) {
    var html = '';
    var route = '';
    $("#modalAlert").remove();

    html += '<div class="modal fade" id="modalAlert" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">';
    html += '<div class="modal-dialog" role="document">';
    html += '<div class="modal-content">';
    html += '<div class="text-center">';
    html += '<img src="../img/question.png" style="width: 100px;" />';
    html += '<h4 style="margin-top: 20px; font-weight: 600;">';
    html += message;
    html += '</h4>';
    html += '</div>';
    html += '<div class="modal-footer" style="align-self: center;">';
    html += '<button type="button" class="btn btn-primary btn2" data-dismiss="modal">';
    html += 'NO';
    html += '</button>';
    html += '<button type="button" class="btn btn-primary" onclick="' + xfunction + '" id="ModalConfirm_btn" data-dismiss="modal">';
    html += 'SI';
    html += '</button>';
    html += '</div>';
    html += '</div>';
    html += '</div>';
    html += '</div>';

    $("#divSection").append(html);
    $("#modalAlert").modal();

    $("#ModalConfirm_btn").click(function () {
        $('#modalAlert').modal('hide');
    });
};

function createModal(html, $Size = "") {


    $('#modal-register').remove();
    var newSize = $Size == "" ? "lg" : "xl";
    var divModal = '';
    divModal = divModal + '<div class="modal fade" id="modal-register" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"';
    divModal = divModal + 'aria-hidden="true">';
    divModal = divModal + '<div class="modal-dialog modal-' + newSize + '" role="document" id="modal-register-container">';

    divModal = divModal + html;

    divModal = divModal + '</div>';
    divModal = divModal + '</div>';

    $("#divSection").append(divModal);
    $('#modal-register').modal('show');
}

function createModalSize(html, newSize) {

    $('#modal-register').remove();
    /*var newSize = $Size == "" ? "lg" : "xl";*/
    var divModal = '';
    divModal = divModal + '<div class="modal fade" id="modal-register" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"';
    divModal = divModal + 'aria-hidden="true" data-backdrop="static">';
    divModal = divModal + '<div class="modal-dialog modal-' + newSize + '" role="document" id="modal-register-container">';

    divModal = divModal + html;

    divModal = divModal + '</div>';
    divModal = divModal + '</div>';

    $("#divSection").append(divModal);
    $('#modal-register').modal('show');
}

function CreatePickaDate(id) {
    $('#' + id).datepicker({
        autoclose: true,
        todayHighlight: true,
        monthNames: ["1", "2", "3", "April", "Maj", "Juni", "Juli", "August", "September", "Oktober", "November", "December"],
        weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mier', 'Jue', 'Vie', 'Sab'],
        today: 'Hoy',
        clear: 'Borrar',
        close: 'Cerrar',
        formatSubmit: 'dd/mm/yyyy',
        format: 'dd/mm/yyyy',
        weekStart: 1
    });

}

function CreatePickaTime(id) {
    $('#' + id).timepicker({
        timeFormat: 'HH:mm',
        template:'modal',
        interval: 30,
        minTime: '6',
        defaultTime: '6',
        maxTime: '10:00pm',
        dynamic: false,
        dropdown: true,
        scrollbar: true,
    });
}

function CreateDataTable(value) {

    $('#' + value).DataTable({
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
}

function exportarGenerico(url, request, parametersModel) {
    
    setPostUrl(url, request, parametersModel);
    return false;
};

function setPostUrl(url, params1, params2, params3) {

    var form = document.createElement("FORM");
    form.action = url;
    form.method = 'POST';
    //IMPORTANTE: No debe tener ningun tipo de serializacion
    var indexQM = url.indexOf("?");
    if (indexQM >= 0) {
        // the URL has parameters => convert them to hidden form inputs
        var params = url.substring(indexQM + 1).split("&");
        for (var i = 0; i < params.length; i++) {
            var keyValuePair = params[i].split("=");
            var input = document.createElement("INPUT");
            input.type = "hidden";
            input.name = keyValuePair[0];
            if (input.name != '__RequestVerificationToken') {
                input.value = keyValuePair[1];
                form.appendChild(input);
            }
        }
    }

    // En caso se envia el array de serializeArray
    if (params1 != undefined)
        for (var i = 0; i < params1.length; i++) {
            var keyValuePair = params1[i];
            var input = document.createElement("INPUT");
            input.type = "hidden";
            input.name = keyValuePair.name;
            if (input.name != '__RequestVerificationToken') {
                input.value = keyValuePair.value;
                form.appendChild(input);
            }
        }

    // En caso se envia el array de serializeArray
    if (params2 != undefined)
        for (var i = 0; i < params2.length; i++) {
            var keyValuePair = params2[i];
            var input = document.createElement("INPUT");
            input.type = "hidden";
            input.name = keyValuePair.name;
            if (input.name != '__RequestVerificationToken') {
                input.value = keyValuePair.value;
                form.appendChild(input);
            }
        }

    // En caso se envia el array de serializeArray
    if (params3 != undefined)
        for (var i = 0; i < params3.length; i++) {
            var keyValuePair = params3[i];
            var input = document.createElement("INPUT");
            input.type = "hidden";
            input.name = keyValuePair.name;
            input.value = keyValuePair.value;
            form.appendChild(input);
        }

    var forgeryToke = $("input[name='__RequestVerificationToken']").val();

    if (forgeryToke != undefined && forgeryToke != null) {
        var input = document.createElement("INPUT");
        input.type = "hidden";
        input.name = '__RequestVerificationToken';
        input.value = forgeryToke;

        form.appendChild(input);
    }

    document.body.appendChild(form);
    //helperjs.showWait();
    form.submit();
};

function CurrentDateFormat(value) {

    if (value == null || value == '') {
        var retorno = '';

        return retorno;

    } else {
        var from = value.split("/")
        var f = new Date(from[2], from[1] - 1, from[0]);

        var today = new Date(f);

        return today.toISOString();
    }


}

function formatDateTime(dateStr) {
    var u = dateStr.substring(0, 10);
    var t = u.split('-');
    var monthNames = ["01", "02", "03", "04", "05", "06",
        "07", "08", "09", "10", "11", "12"];
    var year = t[0];
    var month = monthNames[parseInt(t[1]) - 1];
    var day = t[2];
    return (day + '/' + month + '/' + year);
}


var timeOut = 1440;
var seconds = 0;
var minute = timeOut;
var zeromin = '';
var zeroseg = '';
var x;

function start_cuntdown() {
    seconds = 0;
    minute = timeOut;
    zeromin = '';
    zeroseg = '';
    clearInterval(x);
    x = setInterval(function () {
        start_from_Zero(minute, seconds);
        seconds = seconds % 60;
        //console.log(zeromin + minute + "m " + zeroseg + seconds + "s ");

        if (minute === 0 && seconds === 0) {
            console.log("Tiempo expirado");
            window.location.href = '/Account/Logout';
            clearTimeOut(x);
        }
        if (seconds == 0) {
            minute--;
            seconds += 60;
        }
        seconds--;

    }, 1000);

}

function start_from_Zero(minute, seconds) {
    if (minute < 10) {
        zeromin = '0';

    }
    if (seconds < 10) {
        zeroseg = '0';

    } else {
        zeroseg = '';
    }
    return zeroseg; return zeromin;
}

$(document).ready(function () {
    //start_cuntdown();
    $('body').mousemove(function () {
        //start_cuntdown();
    });
});
