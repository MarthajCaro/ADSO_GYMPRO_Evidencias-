const API_URL = "http://localhost:8080/api/suplementos";

let editingId = null;

const form = document.getElementById("supplement-form");
const nameInput = document.getElementById("name");
const priceInput = document.getElementById("price");
const stockInput = document.getElementById("stock");
const descriptionInput = document.getElementById("description");
const tableBody = document.getElementById("supplement-table-body");

// Cargar suplementos al iniciar
window.addEventListener("DOMContentLoaded", () => {
  loadSupplements();
});

form.addEventListener("submit", async (e) => {
  e.preventDefault();

  const supplement = {
    name: nameInput.value,
    price: parseFloat(priceInput.value),
    stock: parseInt(stockInput.value),
    description: descriptionInput.value,
    active: true,
  };

  if (editingId) {
    // Actualizar suplemento existente
    await fetch(`${API_URL}/${editingId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(supplement),
    });
    editingId = null;
  } else {
    // Crear nuevo suplemento
    await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(supplement),
    });
  }

  form.reset();
  loadSupplements();
});

async function loadSupplements() {
  const res = await fetch(API_URL);
  const supplements = await res.json();
  renderTable(supplements);
}

function renderTable(supplements) {
  tableBody.innerHTML = "";

  supplements.forEach((supplement) => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${supplement.name}</td>
      <td>${formatCurrency(supplement.price)}</td>
      <td>${supplement.stock}</td>
      <td>${supplement.description}</td>
      <td>${supplement.active ? "Activo" : "Inactivo"}</td>
      <td class="actions">
        <button onclick="editSupplement(${supplement.id})">Editar</button>
        <button onclick="toggleSupplement(${supplement.id}, ${supplement.active})">
          ${supplement.active ? "Inhabilitar" : "Habilitar"}
        </button>
        <button onclick="deleteSupplement(${supplement.id})">Eliminar</button>
      </td>
    `;

    tableBody.appendChild(row);
  });
}

async function editSupplement(id) {
  const res = await fetch(`${API_URL}`);
  const supplements = await res.json();
  const supplement = supplements.find(s => s.id === id);

  nameInput.value = supplement.name;
  priceInput.value = supplement.price;
  stockInput.value = supplement.stock;
  descriptionInput.value = supplement.description;
  editingId = id;
}

async function toggleSupplement(id, currentStatus) {
  const res = await fetch(`${API_URL}`);
  const supplements = await res.json();
  const supplement = supplements.find(s => s.id === id);
  supplement.active = !currentStatus;

  await fetch(`${API_URL}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(supplement),
  });

  loadSupplements();
}

async function deleteSupplement(id) {
  const confirmDelete = confirm("¿Estás seguro de eliminar este suplemento?");
  if (confirmDelete) {
    await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });
    loadSupplements();
  }
}

function formatCurrency(value) {
  return new Intl.NumberFormat("es-CO", {
    style: "currency",
    currency: "COP",
    minimumFractionDigits: 0,
  }).format(value);
}
