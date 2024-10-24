document.addEventListener('DOMContentLoaded', (event) => {
    const themeToggler = document.querySelector(".theme-toggler");

    themeToggler.addEventListener('click', () => {
        document.body.classList.toggle('dark-theme-variables');
    });
});
