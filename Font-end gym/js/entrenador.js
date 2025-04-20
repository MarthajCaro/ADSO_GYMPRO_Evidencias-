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


fetch('http://localhost:5003/api/Inscripcion/ObtenerClasesEntrenador', {
  method: 'GET',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`
  }
})
.then(response => {
  if (!response.ok) {
    throw new Error('No autorizado o error en la petición');
  }
  return response.json();
})
.then(data => {
  const tbody = document.querySelector('#tablaEntrenamientos tbody');
  tbody.innerHTML = '';

  if (data.length === 0) {
    const fila = document.createElement('tr');
    fila.innerHTML = `
      <td colspan="6" style="text-align:center; color:gray;">No hay clases disponibles.</td>
    `;
    tbody.appendChild(fila);
    return;
  }

  let enumeracion = 1;  // Inicializamos la numeración

  data.forEach(entrenamiento => {
    const fila = document.createElement('tr');
    fila.innerHTML = `
      <td>${enumeracion}</td>
      <td>${entrenamiento.nombre} ${entrenamiento.apellido}</td>
      <td>${entrenamiento.correo}</td>
      <td>${entrenamiento.edad}</td>
      <td>${entrenamiento.nombreClase}</td>
      <td>${entrenamiento.duracion} minutos</td>
    `;
    tbody.appendChild(fila);
  });
})
.catch(error => {
  const tbody = document.querySelector('#tablaEntrenamientos tbody');
  tbody.innerHTML = '';
  const fila = document.createElement('tr');
  fila.innerHTML = `
    <td colspan="6" style="text-align:center; color:red;">Error al cargar las clases: ${error.message}</td>
  `;
  tbody.appendChild(fila);
});


// Funcion para filtrar la tabla
function filtrarTabla() {
  const input = document.getElementById('buscador');
  const filter = input.value.toLowerCase();  // Convertimos a minúsculas para hacer la búsqueda insensible a mayúsculas/minúsculas
  const rows = document.querySelectorAll('#tablaEntrenamientos tbody tr');
  
  rows.forEach(row => {
    // Accedemos a las celdas de la fila
    const nombreEstudiante= row.cells[1].textContent.toLowerCase();
    const nombreClase  = row.cells[4].textContent.toLowerCase();
    const correo  = row.cells[2].textContent.toLowerCase();
    
    // Si el texto de búsqueda coincide con alguna columna (nombre de la clase o entrenador)
    if (nombreClase.includes(filter) || nombreEstudiante.includes(filter) | correo.includes(filter)) {
      row.style.display = '';  // Mostrar la fila si coincide
    } else {
      row.style.display = 'none';  // Ocultar la fila si no coincide
    }
  });
}