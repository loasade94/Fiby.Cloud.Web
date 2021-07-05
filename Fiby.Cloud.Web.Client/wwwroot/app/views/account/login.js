var loginjs = {

    initializeEvent: function () {

        $("body").on("click", "#btnIngresarSistema", function () {
            loginjs.validateUser();
        });

        loginjs.ValidateLogin();

    },

    ValidateLogin: function () {
        var error = $('#' + loginjs.componentView.hdLoginError.id).val();
        if (error.length > 0) {

            alert("Usuario o contraseña no valido");

            //var xmessage = "Usuario o contraseña no valido";
            //var xcolor = "red";
            //var xposition = 'topRight';
            //var xtimeout = 3000;
            //IziToastMessage(0, xmessage, xcolor, xposition, xtimeout);
        }
    },

    componentView: {
        hdLoginError: { id: "hdLoginError", class: "" }
    },

    validateUser: function () {

        var txtUserName = $("#txtUserName").val();
        var txtPassword = $("#txtPassword").val();

        var form = document.createElement("FORM");
        var passwordValue = document.createElement("INPUT");
        passwordValue.type = "text";
        passwordValue.value = txtPassword;
        passwordValue.name = "pass";
        passwordValue.id = "pass";

        var emailValue = document.createElement("INPUT");
        emailValue.type = "text";
        emailValue.value = txtUserName;
        emailValue.name = "user";
        emailValue.id = "user";

        form.style.display = "none";
        form.appendChild(passwordValue);
        form.appendChild(emailValue);
        form.action = "/Account/Login";
        form.method = 'POST';
        document.body.appendChild(form);
        form.submit();
    }
}


$(function () {

    loginjs.initializeEvent();

});