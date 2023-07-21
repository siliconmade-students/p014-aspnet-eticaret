/*!
    * Start Bootstrap - SB Admin v7.0.7 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
// 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

    const confirms = document.querySelectorAll(".confirm");
    confirms.forEach(element => {
        element.addEventListener('click', function (e) {
            e.preventDefault();
            var urlToRedirect = e.currentTarget.getAttribute('href');

            Swal.fire({
                title: 'Emin misiniz?',
                showCancelButton: true,
                confirmButtonText: 'Evet',
                cancelButtonText: `Hayır`,
                confirmButtonColor: '#3085d6',
            }).then((result) => {
                if (result.isConfirmed) {
                    Swal.fire('Silindi!', '', 'success');
                    setTimeout(() => {
                        window.location.href = urlToRedirect;
                    }, 1000);

                }
            })
        })
    });
});
