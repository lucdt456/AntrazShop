var urlHosting = 'https://antraz.store'
var urlDevelopment = 'https://localhost:7092';

// Tự động chọn URL theo environment
var isLocal = window.location.hostname === 'localhost';
var baseUrl = isLocal ? urlDevelopment : urlHosting;

window.API_URL = baseUrl + '/api';

// Hàm error handling
function handleAjaxError(xhr, status, error, customTitle = "Lỗi!") {
    console.log("=== FULL ERROR DEBUG ===");
    console.log("xhr.status:", xhr.status);
    console.log("xhr.statusText:", xhr.statusText);
    console.log("xhr.responseText:", xhr.responseText);
    console.log("status:", status);
    console.log("error:", error);
    console.log("xhr object:", xhr);

    let errorMessage = 'Có lỗi xảy ra';
    let detailMessage = '';

    // Xử lý response text chi tiết
    if (xhr.responseText) {
        try {
            const response = JSON.parse(xhr.responseText);
            console.log("Parsed response:", response);
            if (response.errors && response.errors.length > 0) {
                errorMessage = response.errors.join('\n');
            } else if (response.message) {
                errorMessage = response.message;
            } else if (response.error) {
                errorMessage = response.error;
            } else if (response.title) {
                errorMessage = response.title;
                if (response.detail) {
                    detailMessage = response.detail;
                }
            }
        } catch (e) {
            console.log("Cannot parse JSON, raw response:", xhr.responseText);
            errorMessage = xhr.responseText; // Hiển thị raw response
        }
    } else {
        errorMessage = `${xhr.status}: ${xhr.statusText || error}`;
    }

    // Thêm chi tiết cho footer
    let footerText = `HTTP ${xhr.status}`;
    if (detailMessage) {
        footerText += ` | ${detailMessage}`;
    }

    Swal.fire({
        icon: "error",
        title: customTitle,
        html: `<div style="max-height: 250px; overflow-y: auto; white-space: pre-wrap; word-wrap: break-word;">${errorMessage}</div>`,
        footer: `<small>${footerText}</small>`,
    });
}