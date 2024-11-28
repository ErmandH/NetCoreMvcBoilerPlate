$(document).ready(() => {
    // Form doğrulama kuralları
    validateForm('form-submit', {
        rules: {
            FirstName: {
                required: true,
                maxlength: 64
            },
            LastName: {
                required: true,
                maxlength: 64
            },
            Email: {
                required: true,
                email: true,
                maxlength: 256
            },
            PhoneNumber: {
                required: true
            },
            Detail: {
                required: true,
                maxlength: 256
            },
            RoleId: {
                required: true
            }
        },
        messages: {
            FirstName: {
                required: "Lütfen üyenin adını giriniz",
                maxlength: jQuery.validator.format("En fazla {0} karakter olmalıdır")
            },
            LastName: {
                required: "Lütfen üyenin soyadını giriniz",
                maxlength: jQuery.validator.format("En fazla {0} karakter olmalıdır")
            },
            Email: {
                required: "Lütfen bir email adresi giriniz",
                email: "Geçerli bir email adresi giriniz",
                maxlength: jQuery.validator.format("En fazla {0} karakter olmalıdır")
            },
            PhoneNumber: {
                required: "Lütfen telefon numarasını giriniz",
            },
            SchoolId: {
                required: "Lütfen bir okul seçiniz"
            }
        },
    });

    submitForm('form-submit', {
        url: `/appuser/update`, // Üyenin güncelleneceği endpoint
        redirect: true,
        href: '/appuser', // Başarılı işlemden sonra yönlendirme yapılacak sayfa
        inputs: {
            inputNameArray: [
                'Id',
                'FirstName',
                'LastName',
                'Email',
                'PhoneNumber',
                'Detail',
                'IsActive',
                'RoleId'
            ],
        },
        jqueryvalidate: true,
        alertTitle: "Başarıyla Güncellendi!"
    });
});
