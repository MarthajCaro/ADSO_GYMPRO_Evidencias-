document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("formCambio");

    form.addEventListener("submit", async (event) => {
      event.preventDefault();

      const usuario = document.getElementById("usuario").value;
      const nueva = document.getElementById("new-password").value;
      const confirmar = document.getElementById("confirm-password").value;

      if (!usuario || !nueva || !confirmar) {
        mostrarModal("Todos los campos son obligatorios.");
        return;
      }

      if (nueva.length < 8) {
        mostrarModal("La nueva contraseña debe tener al menos 8 caracteres.");
        return;
      }

      if (nueva !== confirmar) {
        mostrarModal("Las contraseñas no coinciden.");
        return;
      }

      try {
        const response = await fetch("http://localhost:5003/api/Usuarios/cambiar-contrasena", {
          method: "PUT",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify({
            usuario: usuario,
            nuevaContrasena: nueva
          })
        });

        if (response.ok) {
          mostrarModal("¡Contraseña cambiada exitosamente!");
          document.getElementById("formCambio").reset(); // resetea el formulario si tienes uno
        } else {
          const error = await response.text();
          mostrarModal(`Error: ${error}`);
        }

      } catch (error) {
        console.error("Error:", error);
        mostrarModal("Ocurrió un error al intentar cambiar la contraseña.");
      }
  });
});

function mostrarModal(mensaje) {
  document.getElementById("mensajeModal").innerText = mensaje;
  document.getElementById("modalMensaje").style.display = "block";
}

function cerrarModal() {
  document.getElementById("modalMensaje").style.display = "none";
}
