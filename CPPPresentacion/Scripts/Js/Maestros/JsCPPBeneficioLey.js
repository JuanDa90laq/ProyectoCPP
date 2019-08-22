var depto = null;
var seguroVida = null;
var seguroVidaP2 = null;
var beneficioInteres = null;
var beneficioInteresP2 = null;
var otrosBeneficios = null;
var otrosBeneficiosP2 = null;
var colors = [];

function OnDeptoChanged(cbDepartamento) {
    if (cbMunicipio.InCallback())
        depto = cbDepartamento.GetValue().toString();
    else
        cbMunicipio.PerformCallback(cbDepartamento.GetValue().toString());
}

function OnEndCallbackDepto(s, e) {
    if (depto) {
        cbMunicipio.PerformCallback(depto);
        depto = null;
    }
}

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function onClick(s, e) {
    if (ASPxClientEdit.ValidateEditorsInContainer(FormLayout.GetMainElement(), null, true))
        console.log('Verdadero');
    else {
        console.log('falso');
        e.processOnServer = false;
    }
}

function OnPagareChange(cbPagares) {
    if (cbPagares.GetValue()) {
        console.log(cbPagares.GetValue().toString());
        cbpBeneficio.PerformCallback(cbPagares.GetValue().toString());
    }
    else {
        cbpBeneficio.PerformCallback('1');
    }
}

function OnPeriodoMuertoChange(cbPeriodoMuerto) {
    if (cbPeriodoMuerto.GetValue()) {
        console.log(cbPeriodoMuerto.GetValue().toString());
        cbpPeriodoMuerto.PerformCallback(cbPeriodoMuerto.GetValue().toString());
    }
    else {
        cbpPeriodoMuerto.PerformCallback('1');
    }
}

function OnPeriodoMuertoP2Change(cbPeriodoMuertoP2) {
    if (cbPeriodoMuertoP2.GetValue()) {
        console.log(cbPeriodoMuertoP2.GetValue().toString());
        cbpPeriodoMuertoP2.PerformCallback(cbPeriodoMuertoP2.GetValue().toString());
    }
    else {
        cbpPeriodoMuertoP2.PerformCallback('1');
    }
}

function OnPeriodoGraciaChange(cbPeriodoGracia) {
    if (cbPeriodoGracia.GetValue()) {
        console.log(cbPeriodoGracia.GetValue().toString());
        cbpPeriodoGracia.PerformCallback(cbPeriodoGracia.GetValue().toString());
    }
    else {
        cbpPeriodoGracia.PerformCallback('1');
    }
}

function OnPeriodoGraciaP2Change(cbPeriodoGraciaP2) {
    if (cbPeriodoGraciaP2.GetValue()) {
        console.log(cbPeriodoGraciaP2.GetValue().toString());
        cbpPeriodoGraciaP2.PerformCallback(cbPeriodoGraciaP2.GetValue().toString());
    }
    else {
        cbpPeriodoGraciaP2.PerformCallback('1');
    }
}

function OnSeguroVidaChange(cbBeneficioSeguroVida) {
    if (cbBeneficioSeguroVida.GetValue()) {
        seguroVida = cbBeneficioSeguroVida.GetValue();
        console.log(seguroVida);
        cbpFechaInicioSeguroV.PerformCallback(seguroVida);

    }
    else {
        cbpFechaInicioSeguroV.PerformCallback('1');
        cbpFechaFinSeguroV.PerformCallback('1');
    }
}

function OnEndCallbackSeguroVidaChange(s, e) {
    cbpFechaFinSeguroV.PerformCallback(seguroVida);
    seguroVida = null;
}

function OnSeguroVidaP2Change(cbBeneficioSeguroVidaP2) {
    if (cbBeneficioSeguroVidaP2.GetValue()) {
        seguroVidaP2 = cbBeneficioSeguroVidaP2.GetValue();
        console.log(seguroVidaP2);
        cbpFechaInicioSeguroVP2.PerformCallback(seguroVidaP2);

    }
    else {
        cbpFechaInicioSeguroVP2.PerformCallback('1');
        cbpFechaFinSeguroVP2.PerformCallback('1');
    }
}

function OnEndCallbackSeguroVidaP2Change(s, e) {
    cbpFechaFinSeguroVP2.PerformCallback(seguroVidaP2);
    seguroVidaP2 = null;
}

function OnBeneficioInteresChange(cbBeneficioInteres) {
    if (cbBeneficioInteres.GetValue()) {
        beneficioInteres = cbBeneficioInteres.GetValue();
        console.log(beneficioInteres);
        cbpFechaInicioBeneficioI.PerformCallback(beneficioInteres);
    }
    else {
        cbpFechaInicioBeneficioI.PerformCallback('1');
        cbpFechaFinBeneficioI.PerformCallback('1');
    }
}

function OnEndCallbackBeneficioInteresChange(s, e) {
    cbpFechaFinBeneficioI.PerformCallback(beneficioInteres);
    beneficioInteres = null;
}

function OnBeneficioInteresP2Change(cbBeneficioInteresP2) {
    if (cbBeneficioInteresP2.GetValue()) {
        beneficioInteresP2 = cbBeneficioInteresP2.GetValue();
        console.log(beneficioInteresP2);
        cbpFechaInicioBeneficioIP2.PerformCallback(beneficioInteresP2);

    }
    else {
        cbpFechaInicioBeneficioIP2.PerformCallback('1');
        cbpFechaFinBeneficioIP2.PerformCallback('1');
    }
}

function OnEndCallbackBeneficioInteresP2Change(s, e) {
    cbpFechaFinBeneficioIP2.PerformCallback(beneficioInteresP2);
    beneficioInteresP2 = null;
}

function OnOtrosBeneficioChange(cbOtrosBeneficios) {
    if (cbOtrosBeneficios.GetValue()) {
        otrosBeneficios = cbOtrosBeneficios.GetValue();
        console.log(otrosBeneficios);
        cbpFechaInicioOtroB.PerformCallback(otrosBeneficios);

    }
    else {
        cbpFechaInicioOtroB.PerformCallback('1');
        cbpFechaFinOtroBeneficio.PerformCallback('1');
    }
}

function OnEndCallbackOtroBeneficioChange(s, e) {
    cbpFechaFinOtroBeneficio.PerformCallback(otrosBeneficios);
    otrosBeneficios = null;
}

function OnOtrosBeneficioP2Change(cbOtrosBeneficiosP2) {
    if (cbOtrosBeneficiosP2.GetValue()) {
        otrosBeneficiosP2 = cbOtrosBeneficiosP2.GetValue();
        console.log(otrosBeneficiosP2);
        cbpFechaInicioOtrosBeneficiosP2.PerformCallback(otrosBeneficiosP2);

    }
    else {
        cbpFechaInicioOtrosBeneficiosP2.PerformCallback('1');
        cbpFechaFinOtrosBeneficiosP2.PerformCallback('1');
    }
}

function OnEndCallbackOtrosBeneficiosP2Change(s, e) {
    cbpFechaFinOtrosBeneficiosP2.PerformCallback(otrosBeneficiosP2);
    otrosBeneficiosP2 = null;
}

function onInit(s, e) {
    var array = document.getElementsByClassName("myTab");
    for (var i = 0; i < array.length; i++) {
        colors.push(array[i].style.backgroundColor);
    }
}

function onValidation(s, e) {
    if (!e.isValid) {
        var currenTab = pageControl.GetTabByName(s.cpTab);
        var tabElement = pageControl.GetTabElement(currenTab.index);
        tabElement.style.backgroundColor = "#ff000047";
        // for active tab
        tabElement = pageControl.GetTabElement(currenTab.index, true);
        tabElement.style.backgroundColor = "lightpink";
    }
}

function onValidationClick(s, e) {
    var array = document.getElementsByClassName("myTab");
    for (var i = 0; i < array.length; i++) {
        array[i].style.backgroundColor = colors[i];
    }

    var group2 = ASPxClientEdit.ValidateGroup("0_2", true);
    var group1 = ASPxClientEdit.ValidateGroup("0_1", true);
    var group0 = ASPxClientEdit.ValidateGroup("0_0", true);
    e.processOnServer = (group0 && group1 && group2);
}