document.addEventListener("DOMContentLoaded", function () {
  const formulario = document.getElementById("formularioContacto");

  formulario.addEventListener("submit", async (e) => {
    e.preventDefault();

    const data = {
      nombre: document.getElementById("nombre").value,
      correo: document.getElementById("correo").value,
      asunto: document.getElementById("asunto").value,
      mensaje: document.getElementById("mensaje").value
    };

    try {
      const response = await fetch("http://localhost:5003/api/Notificacion/enviar-correo", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
      });

      if (!response.ok) throw new Error("No se pudo enviar el correo");

      mostrarModal();
      formulario.reset();
    } catch (error) {
      console.error("Error al enviar correo:", error);
      alert("Hubo un problema al enviar el mensaje.");
    }
  });
});

function mostrarModal() {
  document.getElementById("modalCorreo").classList.remove("hidden");
}

function cerrarModal() {
  document.getElementById("modalCorreo").classList.add("hidden");
}