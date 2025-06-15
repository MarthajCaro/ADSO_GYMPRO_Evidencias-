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
  obtenerClases();
});

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


async function obtenerClases() {
  try {
    const response = await fetch(`http://localhost:5003/api/Inscripcion/por-usuario?usuario=${usuario}`, {
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
  tableBody.innerHTML = "";

  data.forEach(clase => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${clase.nombreClase}</td>
      <td>${clase.horario}</td>
      <td>${clase.duracion}</td>
      <td>${clase.descripcion}</td>
      <td>${clase.nombreEntrenador}</td>
    `;

    tableBody.appendChild(row);
  });
}