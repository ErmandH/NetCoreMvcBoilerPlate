$(document).ready(() => {
    $.uploadPreview({
		input_field: "#image-upload",   // Default: .image-upload
		preview_box: "#image-preview",  // Default: .image-preview
		label_field: "#image-label",    // Default: .image-label
		label_default: "Dosya Seç",   // Default: Choose File
		label_selected: "Dosyayı Değiştir",  // Default: Change File
		no_label: false,                // Default: false
		success_callback: null          // Default: null
	});


    validateForm('form-submit', {
        rules: {
            Title: {
                required: true,
                maxlength: 256
            },
            MemberId: {
                required: true
            }
        },
        messages: {
            Title: {
                required: "Lütfen başlığı giriniz",
                maxlength: jQuery.validator.format("En fazla {0} karakter olmalıdır")
            },
        },
    })

    function appendCkEditorData(fdata) {
        const ckData = CKEDITOR.instances.Description.getData();
        fdata.append('Description', ckData)
    }

    const id = getValueFromName("Id")
    submitForm('form-submit', {
        url: getPath('/blogs/update'),
        redirect: true,
        href: getPath('/blogs/'),
        inputs: {
            inputNameArray: [
                'Id',
                'Title',
                'MemberId',
                'IsActive'
            ],
            fileInputNameArray: ['Image']
        },
        jqueryvalidate: true,
        additionalInputCallBack: appendCkEditorData,
        alertTitle: "Başarıyla Güncellendi!"
    })
})