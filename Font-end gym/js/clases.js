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
  obtenerClases();
  cargarOpciones();
  document.getElementById("campoEstado").style.display = "none";
});

let modoFormulario = "registro";
let idclaseActual = null;

const tableBody = document.getElementById("tabla-clases");

async function obtenerUsuarios() {
  try {
    const response = await fetch("http://localhost:5003/api/Usuarios/obtener-usuarios", {
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
    return data;
  } catch (error) {
    console.error("Error al cargar los datos de usuarios:", error);
  }
}

let clasesOriginales = [];

async function obtenerClases() {
  try {
    const response = await fetch("http://localhost:5003/api/Clase", {
      method: "GET",
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });

    if (!response.ok) {
      throw new Error("Error al obtener los datos");
    }

    // Obtener todos los usuarios
    const usuarios = await obtenerUsuarios();

    // Crear un objeto para acceder rápidamente a los nombres de los instructores
    const usuariosMap = usuarios.reduce((acc, usuario) => {
      acc[usuario.idUsuario] = usuario.nombreCompleto; // Usamos el id como clave y el nombre como valor
      return acc;
    }, {});

    const data = await response.json();

    // Ahora cruzamos la información entre clases e instructores
    data.forEach(clase => {
      clase.instructorNombre = usuariosMap[clase.id_usuario] || 'Instructor no encontrado';
    });

    clasesOriginales = data; // Guarda la data
    renderizarTabla(data);
  } catch (error) {
    console.error("Error al cargar los datos de las clases:", error);
  }
}

const searchBox = document.querySelector('.search-box');

searchBox.addEventListener('input', function () {
  const searchTerm = searchBox.value.toLowerCase();

  const resultadosFiltrados = clasesOriginales.filter(clase => 
    clase.nombre.toLowerCase().includes(searchTerm) ||
    clase.descripcion.toLowerCase().includes(searchTerm) 
  );

  renderizarTabla(resultadosFiltrados);
});


function renderizarTabla(data) {
  tableBody.innerHTML = "";

  data.forEach(clase => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${clase.nombre}</td>
      <td>${clase.descripcion}</td>
      <td>${clase.duracion_en_minutos}</td>
      <td>${clase.dia}</td>
      <td>${clase.hora}</td>
      <td>${clase.instructorNombre}</td>
      <td style="color: ${clase.estado ? 'green' : 'red'}">${clase.estado ? 'Activo' : 'Inactivo'}
      <td><button class="actions" onclick="abrirModalEdicion(${clase.id})">Editar</button></td>
    `;

    tableBody.appendChild(row);
  });
}

async function cargarOpciones() {
  try {
    // Llamar a obtenerUsuarios para obtener los instructores
    const usuarios = await obtenerUsuarios();

    // Seleccionar el elemento del select donde se agregarán las opciones
    const selectInstructores = document.getElementById("usuario");

    // Limpiar cualquier opción previa (si existe)
    selectInstructores.innerHTML = "";

    // Crear una opción por defecto
    const opcionDefecto = document.createElement("option");
    opcionDefecto.value = "";
    opcionDefecto.textContent = "Selecciona un instructor";
    selectInstructores.appendChild(opcionDefecto);

    // Recorremos los usuarios y agregamos solo los instructores (si es necesario)
    usuarios.forEach(usuario => {
      // Puedes agregar alguna lógica para filtrar solo instructores si es necesario
      if (usuario.rol === "Entrenador") {  // Asumimos que hay un campo 'rol' que indica el rol
        const opcion = document.createElement("option");
        opcion.value = usuario.idUsuario;
        opcion.textContent = usuario.nombreCompleto; // Asumimos que 'nombreCompleto' es el nombre del instructor
        selectInstructores.appendChild(opcion);
      }
    });
  } catch (error) {
    console.error("Error al cargar las opciones de instructores:", error);
  }
}

const btnAgregar = document.getElementById('bntAgregarClase');
const modal = document.getElementById('modal');
const formClase = document.getElementById('formClase');

// Función para cerrar la modal
function closeModal() {
  document.getElementById("modal").style.display = "none";
}

// Para asegurarse de que se cierre la modal al hacer clic fuera de ella
window.onclick = function(event) {
  if (event.target == document.getElementById("modal")) {
      closeModal();
  }
}

btnAgregar.addEventListener('click', async function() {
  formClase.reset();
  modal.style.display = 'flex';
});

const form = document.getElementById("formClase");

form.addEventListener("submit", function (event) {
  event.preventDefault(); // Evitar recarga

  if (modoFormulario === "registro") {
    // ✅ Validaciones para REGISTRO
    if (!form.checkValidity()) {
      form.reportValidity();
      return;
    }
    registrarClase();

  } else if (modoFormulario === "edicion") {
    editarClase();
  }
});

async function registrarClase() {
  const claseData = {
    nombre : document.getElementById("nombre").value,
    descripcion : document.getElementById("descripcion").value,
    dia : document.getElementById("dia").value,
    hora : document.getElementById("hora").value,
    duracion_en_minutos : document.getElementById("duracion").value,
    id_usuario : document.getElementById("usuario").value
  };

  try {
    // 1. Registrar persona
    const response = await fetch("http://localhost:5003/api/Clase", {
      method: "POST",
      headers: {
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(claseData)
    });


    if (response.ok) {
      document.getElementById('formClase').reset();
      modal.style.display = "none";
      mostrarModal("¡Registro exitoso!");
      obtenerClases();
    } else {
      mostrarModal("Error al registrar la clase.");
    }

  } catch (error) {
    console.error("Error:", error);
    alert("Ocurrió un error en el registro.");
  }
}

function mostrarModal(mensaje) {
  const miModal = document.getElementById("miModal");
  const mensajeModal = document.getElementById("mensajeModal");
  const cerrar = document.getElementById("cerrarModal");

  mensajeModal.innerText = mensaje;
  miModal.style.display = "block";

  cerrar.onclick = () => miModal.style.display = "none";
  window.onclick = (event) => {
    if (event.target === miModal) {
      miModal.style.display = "none";
    }
  };
}

async function abrirModalEdicion(id) {
  modoFormulario = "edicion";
  idclaseActual = id; // Guardamos el id actual que vamos a editar

  try {
    // 1. Consultar datos de Persona
    const responsesClase = await fetch(`http://localhost:5003/api/Clase/${id}`, {
      method: "GET",
      headers: {
        "Authorization": `Bearer ${token}`,  // Enviar el token en el encabezado de la solicitud
        "Content-Type": "application/json"
      }
    });
    if (!responsesClase.ok) {
      throw new Error("No se pudo obtener los datos de la clase.");
    }
    const clase = await responsesClase.json();

    // Cargar los usuarios
     await cargarOpciones(); 

    // 3. Llenar los campos del formulario
    document.getElementById("nombre").value = clase.nombre;
    document.getElementById("descripcion").value = clase.descripcion;
    document.getElementById("dia").value = clase.dia;
    document.getElementById("hora").value = clase.hora;
    document.getElementById("duracion").value = clase.duracion_en_minutos;
    document.getElementById("usuario").value = clase.id_usuario;
    document.getElementById("estado").value = clase.estado;

    // Nuevo campo: Estado
    document.getElementById("campoEstado").style.display = "block";

    // Mostrar el modal
    document.getElementById("modal").style.display = "block";

  } catch (error) {
    console.error("Error al cargar datos para edición:", error);
  }
}

async function editarClase() {
  const claseData = {
    id: idclaseActual,
    nombre: document.getElementById("nombre").value,
    descripcion: document.getElementById("descripcion").value,
    dia: document.getElementById("dia").value,
    hora: document.getElementById("hora").value,
    duracion_en_minutos: document.getElementById("duracion").value,
    id_usuario: document.getElementById("usuario").value,
    estado: document.getElementById("estado").value === "true"
  };

  try {
    // Actualizar Persona
    const responseEClase =await fetch(`http://localhost:5003/api/Clase/${idclaseActual}`, {
      method: "PUT",
      headers: { "Authorization": `Bearer ${token}`,
                 "Content-Type": "application/json" 
                },
      body: JSON.stringify(claseData)
    });

    if (!responseEClase.ok) {
      throw new Error("No se pudo obtener los datos de la persona.");
    }

    mostrarModal("¡Edición exitosa!");
    document.getElementById('formClase').reset();
    document.getElementById('modal').style.display = "none";
    obtenerClases();

  } catch (error) {
    console.error("Error al editar:", error);
  
    // Aquí verificas si hay un error 400 en la respuesta
    if (error.response && error.response.status === 400) {
      alert("Error en la solicitud: los datos enviados son incorrectos.");
    } else {
      alert("Ocurrió un error al editar.");
    }
  }
}