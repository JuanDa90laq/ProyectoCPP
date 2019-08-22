var programa = null;
var planPago = null;
var intermediario = null;
var beneficiario = null;
var deptoCompra = null;
var deptoOrigen = null;
var deptoInv = null;

function OnProgramaChange(cbPrograma) {
    if (cbPrograma.InCallback())
        programa = cbPrograma.GetValue().toString();
    else {
        if (cbPrograma.GetValue()) {
            cbPlanPago.PerformCallback(cbPrograma.GetValue().toString());
        }
        else {
            btnAjustarPlan.SetEnabled(false);
            cbPlanPago.SetValue(null);
        }
    }
    txtConvenio.SetEnabled(true);
    txtConvenio.SetText("");
    txtConvenio.SetEnabled(false);
}

function OnEndCallback(s, e) {
    if (programa) {
        cbPlanPago.PerformCallback(programa);
        programa = null;
    }
}

function OnPlanChange(cbPlanPago) {
    if (cbPlanPago.InCallback())
        planPago = cbPlanPago.GetValue().toString();
    else {
        if (cbPlanPago.GetValue()) {
            cbpC.PerformCallback(cbPlanPago.GetValue().toString());
            btnAjustarPlan.SetEnabled(true);
        }
        else {
            txtConvenio.SetEnabled(true);
            txtConvenio.SetText("");
            txtConvenio.SetEnabled(false);
            btnAjustarPlan.SetEnabled(false);
            cbPlanPago.SetValue(null);
        }
    }
    
}

function OnEndCallbackPlan(s, e) {
    if (planPago) {
        cbpC.PerformCallback(planPago);
        planPago = null;
    }
}

function OnChangeIntermediario(cbIntermediarioFinanciero) {
    if (cbIntermediarioFinanciero.InCallback())
        intermediario = cbIntermediarioFinanciero.GetValue().toString();
    else
        cbpIntermediario.PerformCallback(cbIntermediarioFinanciero.GetValue().toString());
}

function OnEndCallbackIntermediario(s, e) {
    if (intermediario) {
        cbpIntermediario.PerformCallback(intermediario);
        intermediario = null;
    }
}

function OnChangeBeneficiario(cbBeneficiario) {
    if (cbBeneficiario.InCallback())
        beneficiario = cbBeneficiario.GetValue().toString();
    else
        cbpBeneficiario.PerformCallback(cbBeneficiario.GetValue().toString());
}

function OnEndCallbackBeneficiario(s, e) {
    if (beneficiario) {
        cbpBeneficiario.PerformCallback(intermediario);
        beneficiario = null;
    }
}

function OnDeptoCompraChanged(cbDeptoCompra) {
    if (cbMunciCompra.InCallback())
        deptoCompra = cbDeptoCompra.GetValue().toString();
    else
        cbMunciCompra.PerformCallback(cbDeptoCompra.GetValue().toString());
}

function OnEndCallbackDeptoCompra(s, e) {
    if (deptoCompra) {
        cbMunciCompra.PerformCallback(deptoCompra);
        deptoCompra = null;
    }
}

function OnDeptoOrigenChanged(cbDeptoOrigen) {
    if (cbMunciOrigen.InCallback())
        deptoOrigen = cbDeptoOrigen.GetValue().toString();
    else
        cbMunciOrigen.PerformCallback(cbDeptoOrigen.GetValue().toString());
}

function OnEndCallbackDeptoOrigen(s, e) {
    if (deptoOrigen) {
        cbMunciOrigen.PerformCallback(deptoOrigen);
        deptoOrigen = null;
    }
}

function OnDeptoInvChanged(cbDeptoInv) {
    if (cbMunciOrigen.InCallback())
        deptoInv = cbDeptoInv.GetValue().toString();
    else
        cbMunciInv.PerformCallback(cbDeptoInv.GetValue().toString());
}

function OnEndCallbackDeptoInv(s, e) {
    if (deptoInv) {
        cbMunciInv.PerformCallback(deptoInv);
        deptoInv = null;
    }
}

function tipoGarantiaChange(cbTipoGarantia) {
    if (cbTipoGarantia.GetValue()) {
        btnAsociarGarantia.SetEnabled(true);
    } else {
        btnAsociarGarantia.SetEnabled(false);
    }
        
}

function onClickGarantia () {

    let tipoGarantia = cbTipoGarantia.GetValue();
    if (tipoGarantia) {
        if (tipoGarantia == 1) {
            pcIdonea.Show();
        } else{
            pcCodeudor.Show();
        }
    }else {
        alert('Por favor seleccione un tipo de garantia')
    }
}

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}