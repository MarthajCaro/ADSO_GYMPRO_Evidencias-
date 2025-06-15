document.addEventListener("DOMContentLoaded", () => {
  const idUsuario = localStorage.getItem("idUsuario");

  const cerrarSesion = document.getElementById("cerrar-sesion");

  const clasesInscritas = document.getElementById("clases-inscritas");

  const progreso = document.getElementById("progreso-personal");

  if (idUsuario) {
    if (cerrarSesion) cerrarSesion.style.display = "block";
    if (clasesInscritas) clasesInscritas.style.display = "block";
    if (progreso) progreso.style.display = "block";
    
  } else {
    if (cerrarSesion) cerrarSesion.style.display = "none";
    if (clasesInscritas) clasesInscritas.style.display = "none";
    if (progreso) progreso.style.display = "none";
  }
  obtenerProgreso();
});

// Obtener la fecha actual en formato YYYY-MM-DD
const hoy = new Date().toISOString().split('T')[0];
document.getElementById('fecha').value = hoy;

const token = localStorage.getItem('token');

// Si no hay token, redirige al login
if (!token) {
  window.location.href = 'login.html'; // cambia esto por tu ruta al login
}

document.getElementById("cerrarSesion").addEventListener("click", function () {
  // Borra todo del localStorage (o solo lo necesario)
  localStorage.removeItem("token");
  localStorage.removeItem("usuario");
  localStorage.removeItem("rol");

  localStorage.clear();
  // Redirige al login
  window.location.href = "login.html";
});

const tableBody = document.getElementById("supplement-table-body");
const usuario = localStorage.getItem("idUsuario");

// Enviar datos al endpoint
  document.getElementById('formProgreso').addEventListener('submit', async function (event) {
    event.preventDefault(); // Evita recarga del formulario

    const form = event.target;
    const formData = new FormData(form);

    formData.append("UsuarioId", usuario); // En caso de que el backend lo necesite

    try {
      const response = await fetch("http://localhost:5003/api/ProgresoFisico/crear-con-imagen", {
        method: "POST",
        body: formData,
        headers: {
          // No pongas 'Content-Type' aquí: fetch lo hace automáticamente con FormData
          "Authorization": `Bearer ${token}` // Si usas token
        }
      });

      if (!response.ok) {
        throw new Error("Error al guardar el progreso");
      }

      const resultado = await response.json();
      alert("Progreso registrado correctamente");
      console.log(resultado);
      form.reset();
      document.getElementById('fecha').value = hoy; // volver a poner fecha del día

    } catch (error) {
      console.error("Error:", error);
      alert("Ocurrió un error al registrar el progreso");
    }
  });


async function obtenerProgreso() {
  try {
    const response = await fetch(`http://localhost:5003/api/ProgresoFisico/por-usuario?usuarioId=${usuario}`, {
      method: "GET",
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });

    if (!response.ok) {
      throw new Error("Error al obtener los datos");
    }

    const data = await response.json();
    renderizarTabla(data);
  } catch (error) {
    console.error("Error al cargar los datos de clientes:", error);
  }
}

function renderizarTabla(data) {
  const urlBase = "http://localhost:5003";
  tableBody.innerHTML = "";

  data.forEach(progreso => {
    const row = document.createElement("tr");
    const fechaFormateada = progreso.fechaRegistro.split("T")[0];

    row.innerHTML = `
      <td>${fechaFormateada}</td>
      <td>${progreso.peso} kg <span style="color: ${progreso.variacionPeso > 0 ? 'red' : 'green'}">(${progreso.variacionPeso >= 0 ? '+' : ''}${progreso.variacionPeso.toFixed(1)})</span></td>
      <td>${progreso.medidaCintura} cm <span style="color: ${progreso.variacionCintura > 0 ? 'red' : 'green'}">(${progreso.variacionCintura >= 0 ? '+' : ''}${progreso.variacionCintura.toFixed(1)})</span></td>
      <td>${progreso.medidaPecho} cm <span style="color: ${progreso.variacionPecho > 0 ? 'red' : 'green'}">(${progreso.variacionPecho >= 0 ? '+' : ''}${progreso.variacionPecho.toFixed(1)})</span></td>
      <td>${progreso.observaciones}</td>
      <td><img src="${urlBase + progreso.urlFoto}" width="100" height="100"></td>
    `;

    tableBody.appendChild(row);
  });
}