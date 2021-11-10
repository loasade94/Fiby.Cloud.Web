$(document).ready(function () {

    //$('#calendar').fullCalendar({
    //    header: {
    //        left: 'prev,next today',
    //        center: 'title',
    //        right: 'month,basicWeek,basicDay'
    //    },
    //    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    //    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
    //    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
    //    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
    //    defaultDate: new Date(),
    //    defaultView: 'basicWeek',
    //    hiddenDays: [0],
    //    navLinks: false, // can click day/week names to navigate views
    //    editable: true,
    //    eventLimit: true // allow "more" link when too many events
    //});

    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,basicWeek,basicDay'
        },
        firstDay: 1,
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
        defaultDate: new Date(),
        buttonText: {
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Día'
        },
        navLinks: true, // can click day/week names to navigate views
        editable: true,
        eventLimit: true // allow "more" link when too many events
        //events: [
        //    {
        //        title: 'All Day Event',
        //        start: '2016-12-01'
        //    },
        //    {
        //        title: 'Long Event',
        //        start: '2016-12-07',
        //        end: '2016-12-10'
        //    },
        //    {
        //        id: 999,
        //        title: 'Repeating Event',
        //        start: '2016-12-09T16:00:00'
        //    },
        //    {
        //        id: 999,
        //        title: 'Repeating Event',
        //        start: '2016-12-16T16:00:00'
        //    },
        //    {
        //        title: 'Conference',
        //        start: '2016-12-11',
        //        end: '2016-12-13'
        //    },
        //    {
        //        title: 'Meeting',
        //        start: '2016-12-12T10:30:00',
        //        end: '2016-12-12T12:30:00'
        //    },
        //    {
        //        title: 'Lunch',
        //        start: '2016-12-12T12:00:00'
        //    },
        //    {
        //        title: 'Meeting',
        //        start: '2016-12-12T14:30:00'
        //    },
        //    {
        //        title: 'Happy Hour',
        //        start: '2016-12-12T17:30:00'
        //    },
        //    {
        //        title: 'Dinner',
        //        start: '2016-12-12T20:00:00'
        //    },
        //    {
        //        title: 'Birthday Party',
        //        start: '2016-12-13T07:00:00'
        //    },
        //    {
        //        title: 'Click for Google',
        //        url: 'https://google.com/',
        //        start: '2016-12-28'
        //    }
        //]
    });

});