const token = localStorage.getItem('token');
const modal = document.getElementById("modalFormulario");

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

window.addEventListener("DOMContentLoaded", async () => {
  await loadSupplements(); // con await te aseguras de que lo espere bien
});

let editingId = null;

const previewImg = document.getElementById("preview-img");

const form = document.getElementById("supplement-form");
const nameInput = document.getElementById("nombre");
const priceInput = document.getElementById("precio");
const tipoInput = document.getElementById("tipo");
const stockInput = document.getElementById("stock");
const descriptionInput = document.getElementById("descripcion");
const fotoInput = document.getElementById("foto");
const tableBody = document.getElementById("supplement-table-body");


form.addEventListener("submit", async (e) => {
  e.preventDefault();

  const formData = new FormData();

  formData.append("nombre", nameInput.value);
  formData.append("tipo", tipoInput.value);
  formData.append("descripcion", descriptionInput.value);
  formData.append("stock", stockInput.value);
  formData.append("precio", priceInput.value);
  formData.append("id_usuario", localStorage.getItem("idUsuario"));
  formData.append("Foto", fotoInput.files[0]); // ✅ Aquí capturas el archivo

  if (!editingId && fotoInput.files.length === 0) {
    alert("Por favor, selecciona una imagen para el suplemento.");
    return;
  }

  let response;

  if (editingId) {
    // Editar
    formData.append("Id", editingId);
    response = await fetch(`http://localhost:5003/api/SuplementoDeportivo/${editingId}`, {
      method: "PUT",
      headers: { 
        "Authorization": `Bearer ${token}`
      },
      body: formData,
    });

    if (!response.ok) {
      const errorData = await response.json();
      console.error("Error al actualizar:", errorData);
      alert(`Error al actualizar: ${errorData.message || "Algo salió mal."}`);
      return;
    }

    showSuccessModal("¡Suplemento actualizado correctamente!");
    modal.classList.add("hidden"); 
    editingId = null;

  } else {
    // Crear
    response = await fetch("http://localhost:5003/api/SuplementoDeportivo", {
      method: "POST",
      headers: {
        "Authorization": `Bearer ${token}`
      },
      body: formData,
    });

    if (!response.ok) {
      const errorData = await response.json();
      console.error("Error al crear el suplemento:", errorData);
      alert(`Error al crear suplemento: ${errorData.message || "Algo salió mal."}`);
      return;
    }

    showSuccessModal("¡Suplemento creado exitosamente!");
    modal.classList.add("hidden"); 
  }

  form.reset();
  await loadSupplements();
});

async function loadSupplements() {
  const res = await fetch('http://localhost:5003/api/SuplementoDeportivo/ObtenerSuplementosVendedor', {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    }
  });
  const supplements = await res.json();
  renderTable(supplements);
}

function renderTable(supplements) {
  tableBody.innerHTML = "";
  const urlBase = "http://localhost:5003";

  supplements.forEach((supplement) => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${supplement.nombre}</td>
      <td>${formatCurrency(supplement.precio)}</td>
      <td>${supplement.tipo}</td>
      <td>${supplement.stock}</td>
      <td>${supplement.descripcion}</td>
      <td>${supplement.estado ? "Activo" : "Inactivo"}</td>
      <td><img src="${urlBase + supplement.urlImagen}" width="100" height="100"></td>
      <td class="actions">
        <button onclick="editSupplement(${supplement.id})">Editar</button>
        <button onclick="toggleSupplement(${supplement.id}, ${supplement.estado})">
          ${supplement.estado ? "Inhabilitar" : "Habilitar"}
        </button>
      </td>
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

async function editSupplement(id) {
  res = await fetch(`http://localhost:5003/api/SuplementoDeportivo/${id}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}` // <-- si tu backend lo requiere
    }
  });

  if (!res.ok) {
    alert("Error al obtener los datos del suplemento.");
    return;
  }

  const supplement = await res.json();

  nameInput.value = supplement.nombre;
  priceInput.value = supplement.precio;
  tipoInput.value = supplement.tipo;
  stockInput.value = supplement.stock;
  descriptionInput.value = supplement.descripcion;

  if (supplement.urlImagen) {
    previewImg.src = `http://localhost:5003${supplement.urlImagen}`; // Asegúrate de poner el dominio si es relativo
    previewImg.style.display = "block";
  } else {
    previewImg.style.display = "none";
  }

  editingId = id; // Esto le indica al submit que va a actualizar y no crear
  document.getElementById("tituloModal").textContent = "Editar Suplemento";
  modal.classList.remove("hidden");
}

function toggleSupplement(id, currentStatus) {
  // Cambiar el estado. Si es 'true' (Activo), lo cambiamos a 'false' (Inactivo) y viceversa
  const nuevoEstado = !currentStatus;

  // Llamar a la API para actualizar el estado
  fetch(`http://localhost:5003/api/SuplementoDeportivo/cambiar-estado/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`
    },
    body: JSON.stringify(nuevoEstado)  // Enviamos solo el valor booleano
  })
  .then(response => {
    if (!response.ok) throw new Error("Error al actualizar el estado");
    showSuccessModal("Estado actualizado correctamente.");
    loadSupplements();
  })
}


function showSuccessModal(message = "¡Operación exitosa!") {
  const modal = document.getElementById("successModal");
  const messageElem = document.getElementById("successModalMessage");

  if (messageElem) {
    messageElem.textContent = message;
  }

  modal.classList.remove("hidden");
}

document.getElementById("closeSuccessModal").onclick = () => {
  const modal = document.getElementById("successModal");
  modal.classList.add("hidden");
};

const abrirModalBtn = document.getElementById("abrirModalCrear");
const cerrarModalBtn = document.getElementById("cerrarModal");

abrirModalBtn.addEventListener("click", () => {
  document.getElementById("tituloModal").textContent = "Crear Suplemento";
  form.reset();
  editingId = null;
  modal.classList.remove("hidden");
  previewImg.style.display = "none";
});

cerrarModalBtn.addEventListener("click", () => {
  modal.classList.add("hidden");
});