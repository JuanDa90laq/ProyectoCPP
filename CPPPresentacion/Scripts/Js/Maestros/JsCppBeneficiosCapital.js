var lastDepto = null;
var textSeparator = ";";

function OnDeptoChanged(cbDepartamento) {
    if (cbMunicipio.InCallback())
        lastDepto = cbDepartamento.GetValue().toString();
    else
        cbMunicipio.PerformCallback(cbDepartamento.GetValue().toString());
}
function OnEndCallback(s, e) {
    if (lastDepto) {
        cbMunicipio.PerformCallback(lastDepto);
        lastDepto = null;
    }
}

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function OnDropDownCommandButtonClick(s, e) {
    if (e.commandName == "Apply")
        CbActividadP.HideDropDown();
    if (e.commandName == "Close") {
        CbActividadP.HideDropDown();
    }
}

function updateText() {
    var selectedItems = CbActividad.GetSelectedItems();
    CbActividadP.SetText(getSelectedItemsText(selectedItems));
}

function getSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
        texts.push(items[i].text);
    return texts.join(textSeparator);
}

function synchronizeListBoxValues(dropDown, args) {
    CbActividad.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = getValuesByTexts(texts);
    CbActividad.SelectValues(values);
    updateText(); 
}

function getValuesByTexts(texts) {
    var actualValues = [];
    var item;
    for (var i = 0; i < texts.length; i++) {
        item = CbActividad.FindItemByText(texts[i]);
        if (item != null)
            actualValues.push(item.value);
    }
    return actualValues;
}


var postponedCallbackRequired = false;
function OnListBoxIndexChanged(s, e) {
    if (CallbackPanel.InCallback())
        postponedCallbackRequired = true;
    else
        CallbackPanel.PerformCallback();
}

function OnCondicionChanged(CbCondicion) {

    if (CallbackPanel.InCallback())
        postponedCallbackRequired = true;
    else
        CallbackPanel.PerformCallback();   
}
function OnEndCallback(s, e) {
    if (postponedCallbackRequired) {
        CallbackPanel.PerformCallback();
        postponedCallbackRequired = false;
    }
}