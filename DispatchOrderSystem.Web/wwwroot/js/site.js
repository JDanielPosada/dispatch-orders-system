document.addEventListener("DOMContentLoaded", () => {
    const links = document.querySelectorAll("a:not([target]):not([href^='#'])");

    links.forEach(link => {
        link.addEventListener("click", e => {
            e.preventDefault();
            const href = link.getAttribute("href");
            document.body.classList.add("fade-out");

            setTimeout(() => {
                window.location.href = href;
            }, 200);
        });
    });
});

function showSpinner() {
    document.getElementById('spinner-overlay')?.classList.remove('d-none');
}

function hideSpinner() {
    document.getElementById('spinner-overlay')?.classList.add('d-none');
}

document.addEventListener("DOMContentLoaded", () => {
    const links = document.querySelectorAll("a:not([target]):not([href^='#'])");

    links.forEach(link => {
        link.addEventListener("click", e => {
            e.preventDefault();
            const href = link.getAttribute("href");
            showSpinner();
            setTimeout(() => {
                window.location.href = href;
            }, 200);
        });
    });

    // Spinner in forms
    const forms = document.querySelectorAll("form");
    forms.forEach(form => {
        forms.forEach(form => {
            form.addEventListener("submit", (e) => {

                const isValid = $(form).valid?.() ?? true;

                if (!isValid) {
                    e.preventDefault(); 
                    return;
                }

                showSpinner();
            });
        });
    });
});