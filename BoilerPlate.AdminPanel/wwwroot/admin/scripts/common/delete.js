
//    type DeleteRequestOptions = {
//    id: string,
//    url: string,
//    href?: string,
//    redirect?: boolean,
//    alertTitle?: SwalTitle,
//    alertIcon?: SwalIcon,
//    alertText?: string
//}


function askForDeleteAlert(opt /* DeleteRequestOptions */) {
    Swal.fire({
        title: 'Silmek istediğinize emin misiniz?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Evet!',
        cancelButtonText: 'Hayır',
    }).then((result) => {
        if (result.isConfirmed) {
            executeDeleteRequest(opt)
        }
    })
}

function executeDeleteRequest(opt /* DeleteRequestOptions */) {
    $.ajax({
        url: opt.url + opt.id,
        type: 'POST',
        success: (res) => {
            Swal.fire({
                title: opt.alertTitle ? opt.alertTitle : 'Başarıyla Silindi!',
                icon: opt.alertIcon ? opt.alertIcon : 'success',
                confirmButtonText: 'Tamam'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = opt.href
                }
            })
        },
        error: (res) => {
            Swal.fire({
                title: 'Silme işlemi sırasında bir hata oluştu!',
                icon: 'error',
                confirmButtonText: 'Tamam'
            })
        }
    })
}

function deleteRequest(opt /* DeleteRequestOptions */) {
    askForDeleteAlert(opt);
}