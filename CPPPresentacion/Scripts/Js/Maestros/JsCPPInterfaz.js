var textSeparator = ";";

function OnDropDownCommandButtonClick(s, e) {
    if (e.commandName == "Apply")
        CbCuetnasContablesP.HideDropDown();
    if (e.commandName == "Close") {
        CbCuetnasContablesP.HideDropDown();
    }
}

function updateText() {
    var selectedItems = CbCuetnasContables.GetSelectedItems();
    CbCuetnasContablesP.SetText(getSelectedItemsText(selectedItems));
    document.getElementById("hdValores").value = getSelectedItemsText(selectedItems);
}

function getSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
        texts.push(items[i].text);
    return texts.join(textSeparator);
}

function synchronizeListBoxValues(dropDown, args) {
    CbCuetnasContables.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = getValuesByTexts(texts);
    CbCuetnasContables.SelectValues(values);
    updateText();
}

function getValuesByTexts(texts) {
    var actualValues = [];
    var item;
    for (var i = 0; i < texts.length; i++) {
        item = CbCuetnasContables.FindItemByText(texts[i]);
        if (item != null)
            actualValues.push(item.value);
    }
    return actualValues;
}


var lastTipoCesion = null;
var lastTipoCuenta = null;
var lastTipoCalificacion = null;

var vcbTipoCesion = null;
var vcbTipoCuenta = null;
var vcbCalificacion = null;        

function OnTipoCesionChanged(cbTipoCesion) {

    vcbTipoCesion = null;
    vcbTipoCuenta = null;
    vcbCalificacion = null;        

    if (udPCargueCuentas.InCallback()) {
        lastTipoCesion = cbTipoCesion.GetValue().toString();
        lastTipoCuenta = cbTipoCuenta.GetValue().toString();
        lastTipoCalificacion = cbCalificacion.GetValue().toString();
    }
    else {

        if (cbTipoCesion.GetValue() != null)
            vcbTipoCesion = cbTipoCesion.GetValue().toString();

        if (cbTipoCuenta.GetValue() != null)
            vcbTipoCuenta = cbTipoCuenta.GetValue().toString();

        if (cbCalificacion.GetValue() != null)
            vcbCalificacion = cbCalificacion.GetValue().toString();

        udPCargueCuentas.PerformCallback(vcbTipoCesion + "," + vcbTipoCuenta + "," + vcbCalificacion);
    }
        
}
function OnTipoCuentaChanged(cbTipoCuenta) {

    vcbTipoCesion = null;
    vcbTipoCuenta = null;
    vcbCalificacion = null;        

    if (udPCargueCuentas.InCallback()) {
        lastTipoCesion = cbTipoCesion.GetValue().toString();
        lastTipoCuenta = cbTipoCuenta.GetValue().toString();
        lastTipoCalificacion = cbCalificacion.GetValue().toString();
    }
    else {

        if (cbTipoCesion.GetValue() != null)
            vcbTipoCesion = cbTipoCesion.GetValue().toString();

        if (cbTipoCuenta.GetValue() != null)
            vcbTipoCuenta = cbTipoCuenta.GetValue().toString();

        if (cbCalificacion.GetValue() != null)
            vcbCalificacion = cbCalificacion.GetValue().toString();

        udPCargueCuentas.PerformCallback(vcbTipoCesion + "," + vcbTipoCuenta + "," + vcbCalificacion);
    }
}
function OnTipoCalificacionChanged(cbCalificacion) {

    vcbTipoCesion = null;
    vcbTipoCuenta = null;
    vcbCalificacion = null;              

    if (udPCargueCuentas.InCallback()) {
        lastTipoCesion = cbTipoCesion.GetValue().toString();
        lastTipoCuenta = cbTipoCuenta.GetValue().toString();
        lastTipoCalificacion = cbCalificacion.GetValue().toString();
    }
    else {

        if (cbTipoCesion.GetValue() != null)
            vcbTipoCesion = cbTipoCesion.GetValue().toString();

        if (cbTipoCuenta.GetValue() != null)
            vcbTipoCuenta = cbTipoCuenta.GetValue().toString();

        if (cbCalificacion.GetValue() != null)
            vcbCalificacion = cbCalificacion.GetValue().toString();

        udPCargueCuentas.PerformCallback(vcbTipoCesion + "," + vcbTipoCuenta + "," + vcbCalificacion);
    }
}

function OnEndCallback(s, e) {    
    if (lastTipoCesion || lastTipoCuenta || lastTipoCalificacion) {
        udPCargueCuentas.PerformCallback(lastTipoCesion + "," + lastTipoCuenta + "," + lastTipoCalificacion);

        var lastTipoCesion = null;
        var lastTipoCuenta = null;
        var lastTipoCalificacion = null;

        }
}