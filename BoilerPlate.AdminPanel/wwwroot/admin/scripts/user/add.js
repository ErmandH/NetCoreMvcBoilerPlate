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
            Password:{
                required:true
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
            Password: {
                required: "Lütfen şifreyi giriniz",
            },
            PhoneNumber: {
                required: "Lütfen telefon numarasını giriniz",
            },
            RoleId: {
                required: "Lütfen bir rol seçiniz"
            }
        }
    });

    // Form submit işlemi
    submitForm('form-submit', {
        url: '/appuser/add',
        redirect: true,
        href: '/appuser',
        inputs: {
            inputNameArray: [
                'FirstName',
                'LastName',
                'Email',
                'Password',
                'PhoneNumber',
                'IsActive',
                'RoleId'
            ],
        },
        jqueryvalidate: true
    });
});
