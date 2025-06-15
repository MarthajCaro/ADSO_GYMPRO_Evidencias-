document.addEventListener("DOMContentLoaded", () => {
  const usuario = localStorage.getItem("idUsuario");
  const cerrarSesion = document.getElementById("cerrar-sesion");
  const clasesInscritas = document.getElementById("clases-inscritas");
  const progreso = document.getElementById("progreso-personal");

  if (usuario) {
    if (cerrarSesion) cerrarSesion.style.display = "block";
    if (clasesInscritas) clasesInscritas.style.display = "block";
    if (progreso) progreso.style.display = "block";
  } else {
    if (cerrarSesion) cerrarSesion.style.display = "none";
    if (clasesInscritas) clasesInscritas.style.display = "none";
    if (progreso) progreso.style.display = "none";
  }
});

document.getElementById("cerrarSesion").addEventListener("click", function () {
  localStorage.removeItem("token");
  localStorage.removeItem("idUsuario");
  localStorage.removeItem("rol");
  localStorage.removeItem("nombreUsuario");
  window.location.href = "login.html";
});

const token = localStorage.getItem('token');
const usuario = localStorage.getItem('idUsuario');

var idClaseActiva;
// Método para obtener las clases desde el backend
async function obtenerClases() {
  try {
      // Llamada al endpoint para obtener las clases con entrenadores
      const response = await fetch('http://localhost:5003/api/Clase/con-entrenador');
      const clases = await response.json();

      // Contenedor donde agregarás las clases
      const contenedorClases = document.querySelector('.clases-container');

      // Limpiar el contenedor antes de agregar nuevas clases (si es necesario)
      contenedorClases.innerHTML = '';

      // Iterar sobre las clases y construir el HTML dinámicamente
      clases.forEach(clase => {
          // Crear el HTML de cada clase
          const claseHTML = `
              <div class="clase-card">
                  <h3>${clase.nombreClase}</h3>
                  <p><strong>Duración:</strong> ${clase.duracion} minutos</p>
                  <p><strong>Descripción:</strong> ${clase.descripcion}</p>
                  <p><strong>Entrenador:</strong> ${clase.nombreEntrenador}</p>
                  <a onclick="abrirModal(${clase.idClase})" class="boton">Inscribirse</a>
              </div>
          `;

          // Insertar el HTML en el contenedor
          contenedorClases.innerHTML += claseHTML;
      });
  } catch (error) {
      console.error('Error al obtener las clases:', error);
  }
}

// Llamar al método para cargar las clases al cargar la página
document.addEventListener('DOMContentLoaded', obtenerClases);


// Función para abrir la modal
function abrirModal(idClase) {
  const modal = document.getElementById('modalInscripcion');
  const modalBody = document.getElementById('modal-body');
  idClaseActiva = idClase;
  
  if (usuario) {
    // Si está logueado, validamos si tiene un pago reciente
    fetch(`http://localhost:5003/api/Pago/usuario/${usuario}`, {
      method: "GET",
      headers: { 
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
      }
    })
      .then(response => response.json())
      .then(data => {
        const fechaVigencia = new Date(data.fecha_vigencia);
        const hoy = new Date();
        
        if (fechaVigencia >= hoy) {
            informacionClase();
        } else {
          // Si el pago está vencido, pedimos renovación
          modalBody.innerHTML = `
            <p><strong>No cuentas con una membresía activa. Necesitas comprar una membresía para poder inscribirte.</strong></p>
            <button onclick="renovarMembresia()">Renovar Membresía</button>
          `;
        }
      })
      .catch(error => {
        console.error('Error al verificar el pago:', error);
        modalBody.innerHTML = `<p>Error al verificar el pago.</p>`;
      });
  } else {
    // Si no está logueado, mostramos opciones de login o registro
    modalBody.innerHTML = `
      <p><strong>Para inscribirte, debes iniciar sesión o registrarte:</strong></p>
      <button onclick="iniciarSesion()">Iniciar Sesión</button>
      <button onclick="registrarse()">Registrarse</button>
    `;
  }

  // Mostrar la modal
  modal.style.display = "block";
}

// Función para cerrar la modal
function cerrarModal() {
  const modal = document.getElementById('modalInscripcion');
  modal.style.display = "none";
}

// Funciones adicionales (simuladas)
function iniciarSesion() {
  window.location.href = 'login.html';
}

function registrarse() {
  window.location.href = 'register.html';
}

function renovarMembresia() {
  window.location.href = 'pricing.html';
}

function confirmarInscripcion() {
  alert('Inscripción confirmada');
  // Aquí procesamos la inscripción
}

// Detectar el clic en la 'X' para cerrar la modal
document.querySelector('.close').addEventListener('click', cerrarModal);

// Llamamos a la función para abrir la modal al hacer clic en el botón de Inscribirse
const botonInscribir = document.querySelector('.boton');
botonInscribir.addEventListener('click', (e) => {
  e.preventDefault(); // Evitamos que la página recargue
  abrirModal();
});


function  informacionClase(){
  const modalBody = document.getElementById('modal-body');
  fetch(`http://localhost:5003/api/Clase/${idClaseActiva}`, {
    method: "GET",
    headers: { 
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`
    }
  })
  .then(res => res.json())
  .then(clase => {
    // Mostrar info en la modal
    modalBody.innerHTML = `
      <p><strong>Clase seleccionada:</strong> ${clase.nombre}</p>
      <p><strong>Horario:</strong> ${clase.hora}</p>
      <p><strong>Duración:</strong> ${clase.duracion_en_minutos} minutos</p>
      <button onclick="confirmarInscripcion(${idClaseActiva})">Confirmar Inscripción</button>
    `;
  });
}

function confirmarInscripcion(idClaseActiva) {
  const modal = document.getElementById('modalConfirmacion');
  const body = document.getElementById('modalConfirmacionBody');

  // Obtener la fecha de hoy
  const today = new Date();
  const fechaInscripcion = today.toISOString();

    const inscripcion = {
      fecha_inscripcion: fechaInscripcion,
      estado: "Activo",
      id_clase: idClaseActiva,
      id_usuario: usuario
    };

  fetch('http://localhost:5003/api/Inscripcion', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      "Authorization": `Bearer ${token}`
    },
    body: JSON.stringify(inscripcion)
  })
  .then(res => {
    if (res.ok) {
      modal.style.display = 'block';
      body.innerHTML = `<p>✅ ¡Inscripción exitosa!</p>`;
    } else {
      modal.style.display = 'block';
      body.innerHTML = `<p>❌ Ocurrió un error al inscribirse.</p>`;
    }
  })
  .catch(err => {
    console.error('Error al registrar la inscripción:', err);
    body.innerHTML = `<p>❌ Error al registrar la inscripción.</p>`;
  });
}

function cerrarModalConfirmacion() {
  // Cierra la modal de confirmación
  document.getElementById('modalConfirmacion').style.display = 'none';

  // Cierra también la modal de inscripción
  document.getElementById('modalInscripcion').style.display = 'none';
}