function ModalAlert(message) {
    var html = '';

    $("#modalAlert").remove();

    html += '<div class="modal fade" id="modalAlert" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static">';
    html += '<div class="modal-dialog" role="document">';
    html += '<div class="modal-content">';
    html += '<div class="text-center">';
    html += '<img src="/../../admin-profile/imgadmin/warning.png" style="width: 100px;" />';
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
    html += '<img src="/../../admin-profile/imgadmin/modal.png" style="width: 100px;" />';
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
    divModal = divModal + '<div class="modal fade" id="modal-register" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"';
    divModal = divModal + 'aria-hidden="true" data-backdrop="static">';
    divModal = divModal + '<div class="modal-dialog modal-' + newSize + '" role="document" id="modal-register-container">';

    divModal = divModal + html;

    divModal = divModal + '</div>';
    divModal = divModal + '</div>';

    $("#divSection").append(divModal);
    $('#modal-register').modal('show');
}