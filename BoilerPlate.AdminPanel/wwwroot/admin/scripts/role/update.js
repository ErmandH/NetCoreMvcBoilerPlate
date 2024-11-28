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


    const id = getValueFromName("Id")
    submitForm('form-submit', {
        url: '/approle/update',
        redirect: true,
        href: '/approle/',
        inputs: {
            inputNameArray: [
                'Id',
                'RoleName',
            ],
        },
        jqueryvalidate: true,
        alertTitle: "Başarıyla Güncellendi!"
    })
})