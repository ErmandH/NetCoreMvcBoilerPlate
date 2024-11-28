function validateForm(formId, obj) {
    $(`#${formId}`).validate({
        rules: obj.rules, // end of rules
        messages: obj.messages,
        validClass: "is-valid",
        errorClass: "error-label is-invalid"
    })
}