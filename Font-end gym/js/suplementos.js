const token = localStorage.getItem('token');

// Si no hay token, redirige al login
if (!token) {
  window.location.href = 'login.html'; // cambia esto por tu ruta al login
}

 // Cargar nombre del usuario desde localStorage
 const nombreGuardado = localStorage.getItem("nombreUsuario");
 if (nombreGuardado) {
   document.getElementById("nombreUsuario").textContent = nombreGuardado;
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

document.addEventListener("DOMContentLoaded", function () {
  obtenerSuplementos();
});

let clientesOriginales = [];

document.getElementById("aplicar-filtros").addEventListener("click", aplicarFiltros);

const tableBody = document.getElementById("supplement-table-body");

async function obtenerSuplementos() {
  try {
    const response = await fetch("http://localhost:5003/api/SuplementoDeportivo", {
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
    clientesOriginales = data; // Guarda la data
    renderizarTabla(data);
  } catch (error) {
    console.error("Error al cargar los datos de clientes:", error);
  }
}

function aplicarFiltros() {
  const estado = document.getElementById("filtro-estado").value;

  const filtrados = clientesOriginales.filter(suplemento => {
    if (!estado) return true; // si no hay filtro, deja pasar todo

    const estadoEsperado = estado === "Activo"; // convierte a booleano
    return suplemento.estado === estadoEsperado;
  });

  renderizarTabla(filtrados);
}

function renderizarTabla(data) {
  tableBody.innerHTML = "";

  data.forEach(suplemento => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${suplemento.nombre}</td>
      <td>${suplemento.tipo}</td>
      <td>${suplemento.descripcion}</td>
      <td>${formatCurrency(suplemento.precio)}</td>
      <td>${suplemento.stock}</td>
      <td style="color: ${suplemento.estado ? 'green' : 'red'}">
        ${suplemento.estado ? 'Activo' : 'Inactivo'}
      </td>
    `;

    tableBody.appendChild(row);
  });
}

function renderizarTabla(data) {
  tableBody.innerHTML = "";

  data.forEach(suplemento => {
    const row = document.createElement("tr");

    row.innerHTML = `
        <tr>
          <td>${suplemento.nombre}</td>
          <td>${suplemento.tipo}</td>
          <td>${suplemento.descripcion}</td>
          <td>${formatCurrency(suplemento.precio)}</td>
          <td>${suplemento.stock}</td>
          <td style="color: ${suplemento.estado ? 'green' : 'red'}">
            ${suplemento.estado ? 'Activo' : 'Inactivo'}
          </td>
        </tr>`;

    tableBody.appendChild(row);
  });
}

function formatCurrency(value) {
  return new Intl.NumberFormat("es-CO", {
    style: "currency",
    currency: "COP",
    minimumFractionDigits: 0,
  }).format(value);
}

document.getElementById("descargar-excel").addEventListener("click", () => {
  const tabla = document.querySelector("table"); // o usa el ID si es más seguro
  const ws = XLSX.utils.table_to_sheet(tabla); // incluye headers del <thead>
  const wb = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(wb, ws, "Clientes");
  XLSX.writeFile(wb, "suplementos.xlsx");
});