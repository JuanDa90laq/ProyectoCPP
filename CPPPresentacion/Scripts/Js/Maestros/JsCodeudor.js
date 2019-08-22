var lastCountry = null;

function OnCountryChanged(cbDepartamento) {
    if (cbMunicipio.InCallback())
        lastCountry = cbDepartamento.GetValue().toString();
    else
        cbMunicipio.PerformCallback(cbDepartamento.GetValue().toString());
}

function OnEndCallback(s, e) {
    if (lastCountry) {
        cbMunicipio.PerformCallback(lastCountry);
        lastCountry = null;
    }
}

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}