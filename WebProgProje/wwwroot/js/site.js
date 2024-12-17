// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    const phoneInput = document.querySelector('input[name="PhoneNumber"]');

    if (phoneInput) {
        phoneInput.addEventListener('keypress', function (event) {
            // Eğer girilen tuş rakam değilse (0-9 arası)
            if (!/^\d$/.test(event.key)) {
                event.preventDefault(); // Yazmayı engelle
            }
        });

        phoneInput.addEventListener('input', function () {
            // Inputa yanlışlıkla yapıştırma gibi durumlarda harfleri sil
            this.value = this.value.replace(/\D/g, '');
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    // Sayfa yüklendiğinde preloader'ı kaldır
    const preloader = document.getElementById("preloader-active");
    setTimeout(() => {
        preloader.style.display = "none";
    }, 1000); // 1 saniye sonra gizle
});


function toggleMenu() {
    const navbarMenu = document.getElementById('navbarMenu');
    navbarMenu.classList.toggle('show');
}
