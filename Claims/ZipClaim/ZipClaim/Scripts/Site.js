function ActionConfirm(message) {
    return confirm('Вы действительно хотите ' + message + '?');
}

function DeleteConfirm(name) {
    return confirm('Вы действительно хотите удалить ' + name + '?');
}

function initControlDisableStateElementAndText(chkId, inputId, textOnDisalbe, isChecked, validatorId) {
    if ($('#' + inputId)[0].disabled == true) {
        isChecked = false;
    }

    $('#' + chkId).prop('checked', isChecked);
    $('#' + chkId).on('change', function() {
        //$('#' + inputId).prop('placeholder', textOnDisalbe);
        var checked = $(this).prop('checked');

        $('#' + inputId).prop('disabled', !checked);

        if (!checked) {
            $('#' + inputId).prop('placeholder', textOnDisalbe).val(null);
        } else {
            $('#' + inputId).prop('placeholder', '');//.focus();
        }
        var validator = $('#' + validatorId);
        try {
            ValidatorEnable(validator[0], checked);
        }catch (e){}

    }).change();
}
