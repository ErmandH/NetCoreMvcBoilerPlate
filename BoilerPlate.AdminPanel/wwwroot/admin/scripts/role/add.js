$(document).ready(() => {
    validateForm('form-submit', {
        rules: {
            RoleName: {
                required: true,
                maxlength: 256
            }
        },
        messages: {
            RoleName: {
                required: "Lütfen rol ismini giriniz",
                maxlength: jQuery.validator.format("En fazla {0} karakter olmalıdır")
            }
        },
    })

    submitForm('form-submit', {
        url: '/approle/add',
        redirect: true,
        href: '/approle',
        inputs: {
            inputNameArray: [
                'RoleName',
            ],
        },
        jqueryvalidate: true
    })
})


