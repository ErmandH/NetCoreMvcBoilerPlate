function getValueFromName(inputName) {
    const value = $(`[name=${inputName}]`).val()
    console.log(`${inputName}: ${value}`)
    return value
}

function getFileInputFromName(inputName) {
    const fileInput = $(`input[name=${inputName}]`)[0] ;
    const file = fileInput.files[0];
    return file
}


function getMultipleFileInputFromName(inputName) {
    const fileInputs = document.querySelectorAll(`input[name=${inputName}]`);
    const files = [];

    fileInputs.forEach((fileInput) => {
        const fileList = fileInput.files;
        if (fileList) {
            for (let i = 0; i < fileList.length; i++) {
                files.push(fileList[i]);
            }
        }
    });

    return files;
}

function getSeoInputs(fdata) {
    $('[name^="seo"]').each(function (x, y) {
        fdata.append($(y).attr("name"), $(y).val().toString());
    });
    return fdata
}

function getCkEditorData(inputName) {
    const ckData = CKEDITOR.instances.Description.getData();

    console.log(ckData)
    return ckData
}