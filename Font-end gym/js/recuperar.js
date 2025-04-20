async function olvidarContrasena() {
  const correo = document.getElementById('correo').value;

  try {
      const response = await fetch('http://localhost:5003/api/Usuarios/olvidar-contrasena', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify({ correo: correo })
      });

      const mensaje = await response.text(); // O .json() si devuelves un objeto
      mostrarModal(mensaje);

  } catch (error) {
      console.error("Error al enviar el correo:", error);
      mostrarModal("Ocurrió un error al enviar el correo.");
  }
}

function mostrarModal(mensaje) {
  document.getElementById('mensajeModal').innerText = mensaje;
  document.getElementById('miModal').style.display = 'block';

  document.getElementById('cerrarModal').onclick = function () {
      document.getElementById('miModal').style.display = 'none';
  }

  window.onclick = function (event) {
      if (event.target == document.getElementById('miModal')) {
          document.getElementById('miModal').style.display = 'none';
      }
  }
}