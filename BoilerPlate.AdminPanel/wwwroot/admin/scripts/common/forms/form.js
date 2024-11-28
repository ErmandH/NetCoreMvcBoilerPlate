function getPostData(opt /*: FormDataInputs */) {
    const fdata = new FormData();
    console.table(opt)
    for (let index in opt.inputNameArray) {
        const inputName = opt.inputNameArray[index]
        console.log(getValueFromName(inputName).toString())
        fdata.append(inputName, getValueFromName(inputName).toString());
    }
    if (opt.fileInputNameArray) {
        for (let i in opt.fileInputNameArray) {
            const fileInputName = opt.fileInputNameArray[i]
            const fileData = getFileInputFromName(fileInputName)
            fdata.append(fileInputName, fileData)
        }
    }
    if (opt.multipleFileInputName) {
        const files = getMultipleFileInputFromName('ImageFiles')
        for (let x = 0; x < files.length; x++) {
            fdata.append("ImageFiles", files[x]);
        }
    }
    //for (let i in opt.ckeditorInputs) {
    //    const ckName = opt.ckeditorInputs[i]
    //    fdata.append(ckName, getCkEditorData(ckName).toString());
    //}
    return fdata;
}

function doRequest(opt /*: FormSubmitOptions*/, data /*:FormData*/) {
    console.log(data)
    $.ajax({
        url: opt.url,
        type: 'POST',
        data: data,
        processData: false,
        contentType: false,
        success: () => {
            if (opt.hideAlertOnSuccess && opt.hideAlertOnSuccess === true){
                window.location.href = opt.href
                return true;
            }
            Swal.fire({
                title: opt.alertTitle ? opt.alertTitle : 'Başarıyla eklendi!',
                icon: opt.alertIcon ? opt.alertIcon : 'success',
                text: opt.alertText ? opt.alertText : '',
                confirmButtonText: 'Tamam'
            }).then((result) => {
                if (opt.redirect === true) {
                    if (result.isConfirmed) {
                        window.location.href = opt.href
                    }
                }

            })
        },
        error: (response) => {
            Swal.fire({
                title: "Hata!",
                text: response.responseJSON?.errorMessage ? response.responseJSON.errorMessage : JSON.stringify(response),
                icon: "error",
                confirmButtonText: 'Tamam'
            })
        }
    })
}

function submitForm(formId/*: string*/, opt /*: FormSubmitOptions*/) {
    $(`#${formId}`).submit(function (e) {
        e.preventDefault();
        if (opt.jqueryvalidate == true) {
            if ($(`#${formId}`).valid() === false)
                return;
        }
        const fdata = getPostData({
            fileInputNameArray: opt.inputs.fileInputNameArray,
            inputNameArray: opt.inputs.inputNameArray,
            multipleFileInputName: opt.inputs.multipleFileInputName
        });
        if (opt.includeSeo == true) {
            getSeoInputs(fdata)
        }
        if (opt.additionalInputCallBack) {
            opt.additionalInputCallBack(fdata)
        }
        doRequest(opt, fdata)
    })
}