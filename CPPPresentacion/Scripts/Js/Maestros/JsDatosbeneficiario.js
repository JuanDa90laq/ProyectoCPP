$(document).ready(function () {

    $('#txMontoActivos').maskMoney();
    $('#txtTelefono').mask('999 9999');
    $('#txtCelular').mask('999 999 9999');
    $("#txtFechaExped").mask("99/99/9999");
    $("#txFechaCorte").mask("99/99/9999");

    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '&#x3c;Ant',
        nextText: 'Sig&#x3e;',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
            'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
        ],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
            'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'
        ],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
        weekHeader: 'Sm',
        dateFormat: 'yy/mm/dd',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };

    $.datepicker.setDefaults($.datepicker.regional['es']);
    
    $("#txtFechaExped").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        showOn: "button",
        buttonImage: "../Imagenes/calendario.png",
        buttonImageOnly: true,
        buttonText: "Select date"
    });

    $("#txFechaCorte").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        showOn: "button",
        buttonImage: "../Imagenes/calendario.png",
        buttonImageOnly: true,
        buttonText: "Select date"
    });

    $("#dvActividades").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }    
        /*height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }*/
    });
});


var prm = Sys.WebForms.PageRequestManager.getInstance();

prm.add_endRequest(function () {

    $('#txMontoActivos').maskMoney();
    $('#txtTelefono').mask('999 9999');
    $('#txtCelular').mask('999 999 9999');
    $("#txtFechaExped").mask("99/99/9999");
    $("#txFechaCorte").mask("99/99/9999");

    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '&#x3c;Ant',
        nextText: 'Sig&#x3e;',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
            'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
        ],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
            'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'
        ],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
        weekHeader: 'Sm',
        dateFormat: 'yy/mm/dd',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };

    $.datepicker.setDefaults($.datepicker.regional['es']);

    $("#txtFechaExped").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        showOn: "button",
        buttonImage: "../Imagenes/calendario.png",
        buttonImageOnly: true,
        buttonText: "Select date"
    });

    $("#txFechaCorte").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd/mm/yy',
        showOn: "button",
        buttonImage: "../Imagenes/calendario.png",
        buttonImageOnly: true,
        buttonText: "Select date"
    });

    $("#dvActividades").dialog({
        autoOpen: false,
        show: {
            duration: 1000
        },
        hide: {
            duration: 1000
        }
        /*height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }*/
    });

});

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function MostrarActividadesAsociadas() {
    $("#dvActividades").dialog("open");
}



/*function activarValidador(objeto, accion) {
    var validator = document.getElementById(objeto);
    ValidatorEnable(validator, accion);
}*/