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
  localStorage.removeItem("nombreUsuario");
  localStorage.removeItem("rol");

  localStorage.clear();
  // Redirige al login
  window.location.href = "login.html";
});

document.addEventListener("DOMContentLoaded", function () {
  obtenerAvanceClientes();
});

const tableBody = document.getElementById("tabla-usuarios");

async function obtenerAvanceClientes() {
  try {
    const response = await fetch("http://localhost:5003/api/ProgresoFisico/comparacion-todos", {
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
    graficarProgreso(data);
    graficarResumenUsuarios(data);
  } catch (error) {
    console.error("Error al cargar los datos de clientes:", error);
  }
}

function renderizarTabla(data) {
  const urlBase = "http://localhost:5003";
  tableBody.innerHTML = "";

  data.forEach(usuarios => {
    const row = document.createElement("tr");
    const fechaFormateada = usuarios.ultimaFechaRegistro.split("T")[0];
    const pesoAnterior = usuarios.pesoAnterior ?? "Sin datos previos";
    
    const varPeso = usuarios.variacionPeso;
    const varCintura = usuarios.variacionMedidaCintura;
    const varPecho = usuarios.variacionMedidaPecho;

    const textoPeso = (varPeso !== null && !isNaN(varPeso)) ? `${varPeso.toFixed(2)} kg` : "Sin datos previos";
    const textoCintura = (varCintura !== null && !isNaN(varCintura)) ? `${varCintura.toFixed(2)} cm` : "Sin datos previos";
    const textoPecho = (varPecho !== null && !isNaN(varPecho)) ? `${varPecho.toFixed(2)} cm` : "Sin datos previos";

    const colorPeso = (varPeso !== null && !isNaN(varPeso)) ? (varPeso <= 0 ? "green" : "red") : "";
    const colorCintura = (varCintura !== null && !isNaN(varCintura)) ? (varCintura <= 0 ? "green" : "red") : "";
    const colorPecho = (varPecho !== null && !isNaN(varPecho)) ? (varPecho <= 0 ? "green" : "red") : "";

    row.innerHTML = `
        <tr>
          <td>${usuarios.nombreUsuario}</td>
          <td>${usuarios.pesoActual}</td>
          <td>${pesoAnterior}</td>
          <td style="color:${colorPeso}; font-weight:bold">${textoPeso}</td>
          <td style="color:${colorCintura}; font-weight:bold">${textoCintura}</td>
          <td style="color:${colorPecho}; font-weight:bold">${textoPecho}</td>
          <td>${fechaFormateada}</td>
          <td><img src="${urlBase + usuarios.foto}" width="100" height="100"></td>
        </tr>`;

    tableBody.appendChild(row);
  });
}

document.getElementById("descargar-excel").addEventListener("click", () => {
  const tabla = document.querySelector("table"); // o usa el ID si es más seguro
  const ws = XLSX.utils.table_to_sheet(tabla); // incluye headers del <thead>
  const wb = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(wb, ws, "Avance_usuarios");
  XLSX.writeFile(wb, "Avance_usuarios.xlsx");
});

function graficarProgreso(registros) {
  const fechas = registros.map(r => new Date(r.ultimaFechaRegistro).toISOString().split('T')[0]);

  const pesos = registros.map(r => r.variacionPeso);
  const cinturas = registros.map(r => r.variacionMedidaCintura);
  const pechos = registros.map(r => r.variacionMedidaPecho);

  const ctx = document.getElementById('graficaProgreso').getContext('2d');

  new Chart(ctx, {
    type: 'line',
    data: {
      labels: fechas,
      datasets: [
        {
          label: 'Peso (kg)',
          data: pesos,
          borderColor: 'yellow',
          fill: false
        },
        {
          label: 'Cintura (cm)',
          data: cinturas,
          borderColor: 'blue',
          fill: false
        },
        {
          label: 'Pecho (cm)',
          data: pechos,
          borderColor: 'red',
          fill: false
        }
      ]
    }
  });
}

function graficarResumenUsuarios(registros) {
  const nombres = registros.map(r => r.nombreUsuario.replace('_', ' '));
  const pesos = registros.map(r => r.variacionPeso ?? 0);
  const cinturas = registros.map(r => r.variacionMedidaCintura ?? 0);
  const pechos = registros.map(r => r.variacionMedidaPecho ?? 0);

  const ctx = document.getElementById('graficaProgresoUsuario').getContext('2d');

  new Chart(ctx, {
    type: 'bar',
    data: {
      labels: nombres, // Aquí se muestra el nombre del usuario
      datasets: [
        {
          label: 'Variación Peso (kg)',
          data: pesos,
          backgroundColor: 'orange'
        },
        {
          label: 'Variación Cintura (cm)',
          data: cinturas,
          backgroundColor: 'blue'
        },
        {
          label: 'Variación Pecho (cm)',
          data: pechos,
          backgroundColor: 'red'
        }
      ]
    },
    options: {
      responsive: true,
      plugins: {
        legend: {
          position: 'top'
        },
        tooltip: {
          callbacks: {
            title: (tooltipItems) => {
              const index = tooltipItems[0].dataIndex;
              return `Usuario: ${nombres[index]}`;
            }
          }
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          title: {
            display: true,
            text: 'Variación'
          }
        }
      }
    }
  });
}