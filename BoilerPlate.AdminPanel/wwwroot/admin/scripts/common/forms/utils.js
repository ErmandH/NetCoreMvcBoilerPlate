/**
 * Form işlemleri için yardımcı fonksiyonlar
 */

class FormUtils {
    /**
     * Form alanından değer alır
     * @param {string} inputName - Alan adı
     * @returns {string} Alan değeri
     */
    static getFieldValue(inputName) {
        const field = $(`[name=${inputName}]`);
        if (!field.length) {
            console.warn(`${inputName} adında bir form alanı bulunamadı`);
            return null;
        }
        return field.val();
    }

    /**
     * Dosya alanından tek dosya alır
     * @param {string} inputName - Alan adı
     * @returns {File|null} Dosya
     */
    static getSingleFile(inputName) {
        const fileInput = $(`input[name=${inputName}]`)[0];
        return fileInput?.files?.[0] || null;
    }

    /**
     * Dosya alanından çoklu dosya alır
     * @param {string} inputName - Alan adı
     * @returns {File[]} Dosya listesi
     */
    static getMultipleFiles(inputName) {
        const fileInputs = document.querySelectorAll(`input[name=${inputName}]`);
        const files = [];

        fileInputs.forEach(input => {
            if (input.files) {
                Array.from(input.files).forEach(file => files.push(file));
            }
        });

        return files;
    }

    /**
     * Form verilerini FormData nesnesine dönüştürür
     * @param {JQuery} formElement - Form elementi
     * @param {Object} [additionalData] - Ekstra eklenecek veriler
     * @returns {FormData} Form verisi
     */
    static createFormData(formElement, additionalData = null) {
        const formData = new FormData();
        
        // Form verilerini serialize ederek al
        const serializedArray = formElement.serializeArray();
        serializedArray.forEach(item => {
            formData.append(item.name, item.value);
        });

        // Dosya alanlarını ekle
        const fileInputs = formElement.find('input[type="file"]');
        fileInputs.each((_, input) => {
            const files = input.files;
            if (files && files.length > 0) {
                if (input.multiple) {
                    Array.from(files).forEach(file => {
                        formData.append(input.name, file);
                    });
                } else {
                    formData.append(input.name, files[0]);
                }
            }
        });

        // CKEditor alanlarını ekle
        if (typeof CKEDITOR !== 'undefined') {
            for (let instanceName in CKEDITOR.instances) {
                const editorContent = CKEDITOR.instances[instanceName].getData();
                formData.append(instanceName, editorContent);
            }
        }

        // Ekstra verileri ekle
        if (additionalData) {
            Object.keys(additionalData).forEach(key => {
                formData.append(key, additionalData[key]);
            });
        }

        return formData;
    }
}