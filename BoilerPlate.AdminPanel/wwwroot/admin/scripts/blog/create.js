$(document).ready(function () {
    // Select2 başlat
    $('#CategoryIds').select2({
        placeholder: 'Kategori seçin...',
        ajax: {
            url: '/blog/get-categories',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    search: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.map(item => ({
                        id: item.id,
                        text: item.name
                    }))
                };
            }
        }
    });

    // Dosya seçildiğinde önizleme göster
    $('#Images').on('change', function () {
        const files = Array.from(this.files);
        const container = $('#selectedImages');
        container.empty();

        files.forEach(file => {
            const reader = new FileReader();
            reader.onload = function (e) {
                container.append(`
                    <div class="col-md-3 mb-2">
                        <div class="card">
                            <img src="${e.target.result}" class="card-img-top" style="height: 150px; object-fit: cover;">
                            <div class="card-body p-2">
                                <small class="text-muted">${file.name}</small>
                            </div>
                        </div>
                    </div>
                `);
            };
            reader.readAsDataURL(file);
        });

        // Custom file input label'ını güncelle
        $(this).next('.custom-file-label').html(files.length > 1 ? `${files.length} dosya seçildi` : files[0].name);
    });

    // Form gönderimi
    const form = new FormManager({
        formId: 'addBlogForm',
        submitUrl: '/blog/add',
        redirectUrl: '/blog',
        validateOnSubmit: true,
        alert: {
            title: 'Blog Eklendi',
            text: 'Blog başarıyla eklendi.'
        }
    });
}); 