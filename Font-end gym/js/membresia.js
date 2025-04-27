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

const tableBody = document.getElementById("tabla-membresias");

let todasLasMembresias = []; // <- nueva variable

async function obtenerClientesConMembresia() {
  try {
    const response = await fetch("http://localhost:5003/api/Membresia/consultar-membresias", {
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
    todasLasMembresias = data; // <-- aquí guardamos la respuesta
    renderizarTabla(todasLasMembresias);
  } catch (error) {
    console.error("Error al cargar los datos de membresia:", error);
  }
}

const searchBox = document.querySelector('.search-box');

searchBox.addEventListener('input', function () {
  const searchTerm = searchBox.value.toLowerCase();

  const resultadosFiltrados = todasLasMembresias.filter(membresia => 
    membresia.tipoMembresiaNombre.toLowerCase().includes(searchTerm) ||
    membresia.descripcion.toLowerCase().includes(searchTerm) ||
    membresia.estado.toLowerCase().includes(searchTerm)
  );

  renderizarTabla(resultadosFiltrados);
});

function renderizarTabla(data) {
  tableBody.innerHTML = "";

  if (data.length === 0) {
    tableBody.innerHTML = `
      <tr>
        <td colspan="6" style="text-align:center;">No se encontraron resultados</td>
      </tr>
    `;
    return;
  }

  data.forEach(membresia => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${membresia.id}</td>
      <td>${membresia.tipoMembresiaNombre}</td>
      <td>${membresia.descripcion}</td>
      <td>${formatCurrency(membresia.precio)}</td>
      <td>${membresia.duracionMeses}</td>
      <td style="color: ${membresia.estado === "Activo" ? "green" : "red"} !important">${membresia.estado}</td>
      <td><button class="actions" onclick="editarMembresia(${membresia.id})">Editar</button></td>
    `;

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

const btnAgregar = document.getElementById('btnAgregarMembresia');
const modal = document.getElementById('modalMembresia');
const btnCerrar = document.getElementById('btnCerrarModal');
const formMembresia = document.getElementById('formMembresia');

let idEditando = null; // <- para saber si estamos creando o editando

btnAgregar.addEventListener('click', async function() {
  idEditando = null;
  document.getElementById('modalTitulo').innerText = 'Crear Membresía';
  formMembresia.reset();
  await cargarTiposMembresia();
  modal.style.display = 'flex';
});

btnCerrar.addEventListener('click', function() {
  modal.style.display = 'none';
});

async function cargarTiposMembresia() {
  try {
    const response = await fetch('http://localhost:5003/api/TipoMembresia', { // cambia la ruta si es otra
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });

    if (!response.ok) {
      throw new Error("Error al obtener los tipos de membresía");
    }

    const tipos = await response.json();
    const select = document.getElementById('tipoMembresiaSelect');
    
    select.innerHTML = ''; // limpiar antes de agregar

    tipos.forEach(tipo => {
      const option = document.createElement('option');
      option.classList.add("option");
      option.value = tipo.id;
      option.textContent = tipo.nombre; // o como se llame el campo
      select.appendChild(option);
    });

  } catch (error) {
    console.error(error);
  }
}

formMembresia.addEventListener('submit', async function(event) {
  event.preventDefault();

  const nuevaMembresia = {
    id_tipo_membresia: document.getElementById('tipoMembresiaSelect').value,
    descripcion: document.getElementById('descripcionInput').value,
    precio: document.getElementById('precioInput').value,
    duracion_membresia_en_meses: document.getElementById('duracionInput').value
  };

  // Si estamos editando, añadimos el id
  if (idEditando) {
    nuevaMembresia.id = idEditando;
  }


  const url = idEditando 
    ? `http://localhost:5003/api/Membresia/${idEditando}` // si editamos
    : 'http://localhost:5003/api/Membresia'; // si creamos nueva

  const method = idEditando ? 'PUT' : 'POST';

  try {
    const response = await fetch(url, {
      method,
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(nuevaMembresia)
    });

    if (!response.ok) {
      alert('Ocurrió un error al guardar.');
    }

    document.getElementById('modalMembresia').style.display = 'none';
    document.getElementById('formMembresia').reset();
    mostrarModalExito(idEditando ? '¡Membresía editada exitosamente!' : '¡Membresía creada exitosamente!');


    obtenerClientesConMembresia(); // recargar la tabla

  } catch (error) {
    console.error(error);
  }
});

function editarMembresia(id) {
  const membresia = todasLasMembresias.find(m => m.id === id);

  if (!membresia) return;

  idEditando = id;
  document.getElementById('modalTitulo').innerText = 'Editar Membresía';
  
  document.getElementById('descripcionInput').value = membresia.descripcion;
  document.getElementById('precioInput').value = membresia.precio;
  document.getElementById('duracionInput').value = membresia.duracionMeses;

  cargarTiposMembresia().then(() => {
    document.getElementById('tipoMembresiaSelect').value = membresia.tipoMembresiaId;
  });

  modal.style.display = 'flex';
}

function mostrarModalExito(mensaje) {
  const modalExito = document.getElementById('modal-exito');
  const mensajeExito = document.getElementById('mensaje-exito');
  
  mensajeExito.textContent = mensaje;
  modalExito.style.display = 'block';
}

function cerrarModalExito() {
  document.getElementById('modal-exito').style.display = 'none';
}