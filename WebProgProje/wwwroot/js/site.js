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


$(document).ready(function () {
    $('.slider').slick({
        slidesToShow: 3,   // Show 3 images at once
        slidesToScroll: 1, // Scroll 1 image at a time
        autoplay: true,    // Enable autoplay
        autoplaySpeed: 2000, // Speed of autoplay (2 seconds)
        dots: true,        // Enable navigation dots
        arrows: false,     // Disable arrows (you can enable them if needed)
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2,  // Show 2 images on tablets
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 1,  // Show 1 image on mobile
                }
            }
        ]
    });
    });
