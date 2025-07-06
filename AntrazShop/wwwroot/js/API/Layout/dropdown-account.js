function toggleAccountDropdown() {
    const dropdown = document.getElementById("account-dropdown");
    dropdown.style.display = dropdown.style.display === "block" ? "none" : "block";
}

// Đóng dropdown khi click ra ngoài
document.addEventListener('click', function (event) {
    const dropdown = document.getElementById("account-dropdown");
    const userInfo = event.target.closest('#navbar-account');

    if (!userInfo && dropdown.style.display === "block") {
        dropdown.style.display = "none";
    }
});
