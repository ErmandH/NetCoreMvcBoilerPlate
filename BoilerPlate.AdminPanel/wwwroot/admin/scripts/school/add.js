$(document).ready(() => {
    validateForm('form-submit', {
        rules: {
            Name: {
                required: true,
                maxlength: 256
            }
        },
        messages: {
            Name: {
                required: "Lütfen okulun ismini giriniz",
                maxlength: jQuery.validator.format("En fazla {0} karakter olmalıdır")
            }
        },
    })

    submitForm('form-submit', {
        url: getPath('/schools/add'),
        redirect: true,
        href: getPath('/schools'),
        inputs: {
            inputNameArray: [
                'Name',
            ],
        },
        jqueryvalidate: true
    })
})


