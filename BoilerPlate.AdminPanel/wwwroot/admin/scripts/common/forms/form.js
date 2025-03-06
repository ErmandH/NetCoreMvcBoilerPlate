/**
 * Form yönetimi için ana sınıf
 */
class FormManager {
    /**
     * @param {Object} config - Form yapılandırması
     * @param {string} config.formId - Form elementi ID'si
     * @param {string} config.submitUrl - Form gönderim URL'i
     * @param {string} [config.redirectUrl] - Başarılı işlem sonrası yönlendirilecek URL
     * @param {boolean} [config.validateOnSubmit=false] - Form gönderiminde jQuery validate kontrolü yapılsın mı?
     * @param {Object} [config.additionalData] - FormData'ya eklenecek ekstra veriler
     * @param {Object} [config.alert] - Alert yapılandırması
     * @param {Function} [config.onSuccess] - Başarılı işlem callback'i
     * @param {Function} [config.onError] - Hata durumu callback'i
     */
    constructor(config) {
        this.config = {
            validateOnSubmit: true,
            ...config
        };
        
        this.formElement = $(`#${config.formId}`);
        if (!this.formElement.length) {
            throw new Error(`${config.formId} ID'li form bulunamadı`);
        }

        this.initialize();
    }

    /**
     * Form olaylarını başlatır
     */
    initialize() {
        this.formElement.on('submit', (e) => this.handleSubmit(e));
    }

    /**
     * Form gönderimini işler
     * @param {Event} e - Form submit olayı
     */
    async handleSubmit(e) {
        e.preventDefault();

        if (this.config.validateOnSubmit && !this.formElement.valid()) {
            return;
        }

        try {
            const formData = FormUtils.createFormData(this.formElement, this.config.additionalData);
            await this.submitForm(formData);
        } catch (error) {
            this.handleError(error);
        }
    }

    /**
     * Form verilerini sunucuya gönderir
     * @param {FormData} formData - Form verisi
     */
    submitForm(formData) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: this.config.submitUrl,
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: (response) => {
                    this.handleSuccess(response);
                    resolve(response);
                },
                error: (error) => {
                    reject(error);
                }
            });
        });
    }

    /**
     * Başarılı form gönderimini işler
     * @param {Object} response - Sunucu yanıtı
     */
    handleSuccess(response) {
        if (this.config.onSuccess) {
            this.config.onSuccess(response);
            return;
        }

        const alert = this.config.alert || {};
        Swal.fire({
            title: alert.title || SwalTitle.SUCCESS,
            icon: alert.icon || SwalIcon.SUCCESS,
            text: alert.text || '',
            confirmButtonText: 'Tamam'
        }).then((result) => {
            if (result.isConfirmed && this.config.redirectUrl) {
                window.location.href = this.config.redirectUrl;
            }
        });
    }

    /**
     * Form hatalarını işler
     * @param {Object} error - Hata nesnesi
     */
    handleError(error) {
        if (this.config.onError) {
            this.config.onError(error);
            return;
        }

        Swal.fire({
            title: SwalTitle.ERROR,
            text: error.responseJSON?.errorMessage || 'Bir hata oluştu',
            icon: SwalIcon.ERROR,
            confirmButtonText: 'Tamam'
        });
    }
}