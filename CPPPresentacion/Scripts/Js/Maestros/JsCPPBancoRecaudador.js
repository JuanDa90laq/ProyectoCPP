var textSeparator = ";";

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function OnDropDownCommandButtonClick(s, e) {
    if (e.commandName == "Apply")
        cbCuentasContablesC.HideDropDown();
    if (e.commandName == "Close") {
        cbCuentasContablesC.HideDropDown();
    }
}

function updateText() {
    var selectedItems = cbCuentaContable.GetSelectedItems();
    cbCuentasContablesC.SetText(getSelectedItemsText(selectedItems));
}

function getSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
        texts.push(items[i].text);
    return texts.join(textSeparator);
}

function synchronizeListBoxValues(dropDown, args) {
    cbCuentaContable.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = getValuesByTexts(texts);
    cbCuentaContable.SelectValues(values);
    updateText(); 
}

function getValuesByTexts(texts) {
    var actualValues = [];
    var item;
    for (var i = 0; i < texts.length; i++) {
        item = cbCuentaContable.FindItemByText(texts[i]);
        if (item != null)
            actualValues.push(item.value);
    }
    return actualValues;
}