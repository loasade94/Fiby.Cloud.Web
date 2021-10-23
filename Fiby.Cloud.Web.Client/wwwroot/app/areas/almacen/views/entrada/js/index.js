var entradajs = {

    initializeEvent: function () {

        $("#tabConsult").click(function () {
            $('#tabAdd').removeClass('first');
            $('#tabAdd').removeClass('current');
            $('#tabAdd').addClass('disabled');

            $('#tabConsult').addClass('first');
            $('#tabConsult').addClass('current');
            $('#tabConsult').removeClass('disabled');

            $('#steps-uid-0-p-1').hide();
            $('#steps-uid-0-p-0').show();
            
        });

        $("#tabAdd").click(function () {
            $('#tabConsult').removeClass('first');
            $('#tabConsult').removeClass('current');
            $('#tabConsult').addClass('disabled');

            $('#tabAdd').addClass('first');
            $('#tabAdd').addClass('current');
            $('#tabAdd').removeClass('disabled');

            $('#steps-uid-0-p-0').hide();
            $('#steps-uid-0-p-1').show();
            
        });

    },

}

$(function () {

    entradajs.initializeEvent();

});