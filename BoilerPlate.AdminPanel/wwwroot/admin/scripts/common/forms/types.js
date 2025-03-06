/**
 * Form işlemleri için sabitler
 */

const SwalIcon = {
    SUCCESS: "success",
    WARNING: "warning",
    ERROR: "error",
    INFO: "info"
};

const SwalTitle = {
    SUCCESS: "Başarılı!",
    WARNING: "Uyarı!",
    ERROR: "Hata!",
    INFO: "Bilgi!"
};

/**
 * Form giriş alanları için yapılandırma
 * @typedef {Object} FormFieldConfig
 * @property {string} name - Alan adı
 * @property {string} type - Alan tipi (text, file, multiple-file, editor)
 * @property {Object} [validations] - Doğrulama kuralları
 */

/**
 * Form yapılandırması için tip tanımı
 * @typedef {Object} FormConfig
 * @property {string} formId - Form elementi ID'si
 * @property {string} submitUrl - Form gönderim URL'i
 * @property {string} [redirectUrl] - Başarılı işlem sonrası yönlendirilecek URL
 * @property {FormFieldConfig[]} fields - Form alanları
 * @property {boolean} [validateOnSubmit] - Form gönderiminde doğrulama yapılsın mı?
 * @property {boolean} [includeSeo] - SEO alanları dahil edilsin mi?
 * @property {Function} [onSuccess] - Başarılı işlem callback'i
 * @property {Function} [onError] - Hata durumu callback'i
 * @property {Object} [alert] - Alert yapılandırması
 */

//export enum SwalIcon {
//    Success = "success",
//    Warning = "warning",
//    Error = "error"
//}

//export enum SwalTitle {
//    Success = "Başarılı!",
//    Warning = "Uyarı!",
//    Error = "Hata!"
//}

//export type FormDataInputs = {
//    inputNameArray: string[],
//    fileInputNameArray?: string[],
//    multipleFileInputName?: string,
//}

//export type FormSubmitOptions = {
//    url: string,
//    href?: string,
//    redirect?: boolean,
//    inputs: FormDataInputs,
//    includeSeo?: boolean,
//    alertTitle?: SwalTitle | string,
//    alertIcon?: SwalIcon,
//    alertText?: string,
//    jqueryvalidate?: boolean
//    additionalInputCallBack?: (fdata: FormData) => void
//}