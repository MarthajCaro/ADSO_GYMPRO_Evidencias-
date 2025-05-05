document.addEventListener("DOMContentLoaded", () => {
  const usuario = localStorage.getItem("idUsuario");
  const rol = localStorage.getItem("rol");

  const cerrarSesion = document.getElementById("cerrar-sesion");
  const registroDropdown = document.getElementById("registro-dropdown");

  if (usuario) {
    if (cerrarSesion) cerrarSesion.style.display = "block";
    if (registroDropdown) registroDropdown.style.display = "none";
  } else {
    if (cerrarSesion) cerrarSesion.style.display = "none";
  }
});

document.getElementById("cerrarSesion").addEventListener("click", function () {
  localStorage.removeItem("token");
  localStorage.removeItem("idUsuario");
  localStorage.removeItem("rol");
  localStorage.removeItem("nombreUsuario");
  window.location.href = "login.html";
});
