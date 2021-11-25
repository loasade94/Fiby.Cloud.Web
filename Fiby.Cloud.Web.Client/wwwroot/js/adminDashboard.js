$(function () {
    adminDashboard.initializeEvent();
});

var adminDashboard = {

    initializeEvent: function () {
        //$('.mdb-select').materialSelect({});


        $("body").on("change", "#cboDashboardType", function () {
            var admin = getProfileCode('Admin');
            var supervisor = getProfileCode('Supervisor');
            var Colaborator = getProfileCode('Colaborator');
            var myProfileCode = getProfileCode($('#HiddenUserProfile').val());
            var idUrl = '';
            var url = '';

            if ($(this).val() == supervisor) {
                url = $('#HiddenDashboardPathSupervisor').val();
                if ($(this).val() == myProfileCode)
                    idUrl = '';
                else
                    idUrl = supervisor;

                //window.location.href = url;

            } else if ($(this).val() == Colaborator) {
                var url = '';
                url = $('#HiddenDashboardPathWorker').val();
                if ($(this).val() == myProfileCode)
                    idUrl = '';
                else
                    idUrl = Colaborator;

                //window.location.href = url;

            } else if ($(this).val() == admin) {
                var url = '';
                url = $('#HiddenDashboardPathAdmin').val();
                if ($(this).val() == myProfileCode)
                    idUrl = '';
                else
                    idUrl = admin;

                //window.location.href = url;
            } else {
                var url = '';
                url = $('#HiddenDashboardPathAdmin').val();
                if ($(this).val() == myProfileCode) {
                    idUrl = '';
                }
                else {
                    idUrl = $(this).val();
                }
                //window.location.href = url;
            }

            var strProfile = (idUrl == admin ? 'Admin' :
                (idUrl == supervisor ? 'Supervisor' :
                    (idUrl == Colaborator ? 'Colaborador' : 'Admin')));

            var strProfileId = (idUrl == admin ? '1' :
                (idUrl == supervisor ? '2' :
                    (idUrl == Colaborator ? '3' : '1')));

            $.ajax({
                type: "POST",
                data: { ProfileId: strProfileId, Profile: strProfile },
                url: '/Home/SetClaimValueProfile',
                beforeSend: function () {
                },
                success: function (response, textStatus, jqXhr) {
                    console.log(response);
                },
                complete: function () {
                },
                error: function (xhr, status, errorThrown) {
                    var err = "Status: " + status + " " + errorThrown;
                    console.log(err);
                },
                async: false,
            })

            var form = document.createElement("FORM");
            var idProfile = document.createElement("INPUT");
            idProfile.type = "text";
            idProfile.value = idUrl;
            idProfile.name = "id";
            idProfile.id = "id";

            form.style.display = "none";
            form.appendChild(idProfile);
            form.action = url;
            form.method = 'POST';
            document.body.appendChild(form);
            form.submit();
        });

        adminDashboard.getDashboardCollaboratorNum();

    },

}