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
  obtenerClientesConMembresia();
});

let clientesOriginales = [];

document.getElementById("aplicar-filtros").addEventListener("click", aplicarFiltros);

const tableBody = document.getElementById("tabla-pagos");

async function obtenerClientesConMembresia() {
  try {
    const response = await fetch("http://localhost:5003/api/Pago/clientes-membresia", {
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

  const filtrados = clientesOriginales.filter(cliente => {
    const coincideEstado = !estado || cliente.estado === estado;
    return coincideEstado;
  });

  renderizarTabla(filtrados);
}

function renderizarTabla(data) {
  tableBody.innerHTML = "";

  data.forEach(cliente => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${cliente.nombreCliente}</td>
      <td>${cliente.membresia}</td>
      <td>${cliente.descripcionMembresia}</td>
      <td>${new Date(cliente.fechaVencimiento).toLocaleDateString()}</td>
      <td>${formatCurrency(cliente.precio)}</td>
      <td style="color: ${cliente.estado === "Activo" ? "green" : "red"}">${cliente.estado}</td>
    `;

    tableBody.appendChild(row);
  });
}

function renderizarTabla(data) {
  tableBody.innerHTML = "";

  data.forEach(cliente => {
    const row = document.createElement("tr");

    row.innerHTML = `
        <tr>
          <td>${cliente.nombreCliente}</td>
          <td>${cliente.membresia}</td>
          <td>${cliente.descripcionMembresia}</td>
          <td>${new Date(cliente.fechaVencimiento).toLocaleDateString()}</td>
          <td>${formatCurrency(cliente.precio)}</td>
          <td style="color: ${cliente.estado === "Activo" ? "green" : "red"}">
            ${cliente.estado}
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
  XLSX.writeFile(wb, "clientes_membresia.xlsx");
});