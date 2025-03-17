$(document).ready(function () {
    let url = window.location.pathname;
    document.querySelectorAll(".menu-item-active").forEach(item => {
        let urlItem = item.getAttribute("href");
        if (url === urlItem) {
            

            let menuItem = item.closest(".menu-item")
            let subMenu = menuItem.querySelector('.sub-menu');
            menuItem.classList.add("active");
            item.classList.add("active");
            if (subMenu != null) {
                subMenu.style.display = 'block';
                subMenu.classList.add("active");
            }
        }
    });
});