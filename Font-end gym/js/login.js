async function iniciarSesion() {
  const Usuario = document.getElementById('usuario').value;
  const Contrasena = document.getElementById('contrasena').value;

  try {
    const response = await fetch('http://localhost:5003/api/Usuarios/login', {
      method: "POST",
      headers: {
          "Content-Type": "application/json"
      },
      body: JSON.stringify({ Usuario, Contrasena })
    });

    if (response.ok) {
      const data = await response.json();
      console.log('Login exitoso:', data);
      localStorage.setItem("token", data.token);
      localStorage.setItem("rol", data.usuario.rolId);
      localStorage.setItem("idUsuario", data.usuario.id);
      localStorage.setItem("nombreUsuario", data.usuario.usuario);

      const rol = localStorage.getItem("rol");

      if (rol === "1") {
        window.location.href = "administrador.html";
      } else if (rol === "2") {
        window.location.href = "entrenador.html";
      } else if (rol === "3") {
        document.getElementById("adminMenu").style.display = "none";
      } else if (rol === "4") {
        window.location.href = "vendedor.html";
      }

    } else {
      const error = await response.text();
      console.error('Error al iniciar sesión:', error);
      const mensaje = "Credenciales inválidas";
      mostrarModal(mensaje);

    }
  } catch (error) {
    console.error('Error de conexión:', error);
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