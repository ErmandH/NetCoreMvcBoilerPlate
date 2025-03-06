function deleteBlog(id) {
    Swal.fire({
        title: 'Emin misiniz?',
        text: "Bu blog ve tüm resimleri silinecek!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Evet, sil!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/Blog/Delete/${id}`,
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            title: 'Başarılı!',
                            text: response.message,
                            icon: 'success'
                        }).then(() => {
                            window.location.reload();
                        });
                    } else {
                        Swal.fire({
                            title: 'Hata!',
                            text: response.message,
                            icon: 'error'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: 'Hata!',
                        text: 'Silme işlemi sırasında bir hata oluştu.',
                        icon: 'error'
                    });
                }
            });
        }
    });
} 