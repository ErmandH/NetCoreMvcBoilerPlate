$(document).ready(() => {
    validateForm('login-form', {
        rules: {
            Email: {
                required: true,
                email: true,
                maxlength: 256
            },
			Password: {
                required: true
            },
        },
        messages: {
            Email: {
                required: "Lütfen bir email adresi giriniz",
                email: "Geçerli bir email adresi giriniz",
                maxlength: jQuery.validator.format("En fazla {0} karakter olmalıdır")
            },
			Password: {
                required: "Lütfen şifreyi giriniz",
            }
        },
    })

    // submitForm('login-form', {
    //     url: '/login',
    //     redirect: true,
    //     href: '/',
    //     inputs: {
    //         inputNameArray: [
    //             'Email',
	// 			'Password'
    //         ],
    //     },
	// 	hideAlertOnSuccess:true,
    //     jqueryvalidate: true
    // })

    $('#login-form').submit(function (e) {
        e.preventDefault();
        if ($(`#login-form`).valid() === false)
            return;
        const payload = {
            Email: $('input[name=Email]').val(),
            Password: $('input[name=Password]').val(),
        }
        loginRequest(payload)
    })
    
    function loginRequest(payload) {
        const urlParams = new URLSearchParams(window.location.search);
        const returnUrl = urlParams.get('ReturnUrl');
        const loginUrl = getPath('/login')
        const baseUrl = getPath('/')
        $.ajax({
            url: loginUrl,
            type: 'POST',
            data: payload,
            success: (response) => {
                window.location.href = returnUrl != null ? returnUrl : baseUrl
            },
            error: (response) => {
                Swal.fire({
                    title: 'Hata!',
                    text: response.responseJSON.errorMessage,
                    icon: 'error',
                    confirmButtonText: 'Tamam'
                })
            }
        })
    }
})




