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


    const id = getValueFromName("Id")
    submitForm('form-submit', {
        url: getPath('/schools/update'),
        redirect: true,
        href: getPath('/schools/'),
        inputs: {
            inputNameArray: [
                'Id',
                'Name',
            ],
        },
        jqueryvalidate: true,
        alertTitle: "Başarıyla Güncellendi!"
    })
})