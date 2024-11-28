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
            },
            Image: {
                required: true,
            },
        },
        messages: {
            Title: {
                required: "Lütfen başlığı giriniz",
                maxlength: jQuery.validator.format("En fazla {0} karakter olmalıdır")
            },
        },
    })


    function appendCkEditorData(fdata /*: FormData*/) {
        const ckData = CKEDITOR.instances.Description.getData();
        fdata.append('Description', ckData)
    }

    submitForm('form-submit', {
        url: getPath('/blogs/add'),
        redirect: true,
        href: getPath('/blogs'),
        inputs: {
            inputNameArray: [
                'Title',
                'MemberId',
                'IsActive'
            ],
            fileInputNameArray: ['Image']
        },
        jqueryvalidate: true,
        additionalInputCallBack: appendCkEditorData
    })
})


