const dialog = document.getElementById("alertsDialog");
const showButton = document.getElementById("showAlerts");
const closeButton = document.getElementById("closeDialog");

showButton.addEventListener("click", () => {
    dialog.showModal();
});

closeButton.addEventListener("click", () => {
    dialog.close();
});