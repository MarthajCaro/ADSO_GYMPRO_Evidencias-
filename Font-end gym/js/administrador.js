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

let modoFormulario = "registro"; // o "edicion"

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
  obtenerUsuarios();
  cargarOpciones();
  errorLabel.value ="";
  document.getElementById('usuarioContrasena').style.display = "block";
  document.getElementById("contrasena").required = true;
  document.getElementById("confirmar_contrasena").required = true;
  document.getElementById("campoEstado").style.display = "none";
});

const tableBody = document.getElementById("tabla-usuarios");

let todasLasMembresias = []; // <- nueva variable

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
    todosLosUsuarios = data; // <-- aquí guardamos la respuesta
    renderizarTabla(todosLosUsuarios);
  } catch (error) {
    console.error("Error al cargar los datos de usuarios:", error);
  }
}

const searchBox = document.querySelector('.search-box');

searchBox.addEventListener('input', function () {
  const searchTerm = searchBox.value.toLowerCase();

  const resultadosFiltrados = todosLosUsuarios.filter(usuario => 
    usuario.nombreCompleto.toLowerCase().includes(searchTerm) ||
    usuario.correo.toLowerCase().includes(searchTerm) 
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

  data.forEach(usuario => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${usuario.usuario}</td>
      <td>${usuario.nombreCompleto}</td>
      <td>${usuario.correo}</td>
      <td>${usuario.edad}</td>
      <td>${usuario.rol}</td>
      <td style="color: ${usuario.estado ? 'green' : 'red'}">${usuario.estado ? 'Activo' : 'Inactivo'}
      <td><button class="actions" onclick="abrirModalEdicion(${usuario.id})">Editar</button></td>
    `;

    tableBody.appendChild(row);
  });
}

const btnAgregar = document.getElementById('btnAgregarUsuario');
const modal = document.getElementById('modal');
const formMembresia = document.getElementById('formUsuario');

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
  formMembresia.reset();
  modal.style.display = 'flex';

  // Quitar la opción de Cliente si existe
  const clienteOption = document.getElementById("cargo").querySelector('option[value="3"]');
  if (clienteOption) {
    clienteOption.remove();
  }
});

function cerrarModalExito() {
  document.getElementById('modal-exito').style.display = 'none';
}

async function cargarOpciones() {
  const select = document.getElementById("departamento");
  const res = await fetch("http://localhost:5003/api/Departamento");
  const data = await res.json();

  data.forEach(item => {
    const option = document.createElement("option");
    option.value = item.id;
    option.textContent = `${item.nombre}`;
    select.appendChild(option);
  });
}

document.addEventListener("DOMContentLoaded", () => {
  const departamentoSelect = document.getElementById("departamento");
  const municipioSelect = document.getElementById("municipio");

  departamentoSelect.addEventListener("change", async () => {
    const departamentoId = departamentoSelect.value;

    try {
      const response = await fetch("http://localhost:5003/api/Municipio");
      const municipios = await response.json();

      // Limpias el select de municipios antes de llenarlo de nuevo
      municipioSelect.innerHTML = "<option value=''>Selecciona un municipio</option>";

      // Filtras los municipios por departamento
      const municipiosFiltrados = municipios.filter(m => m.id_Departamento == departamentoId);

      // Llenas el select con los municipios filtrados
      municipiosFiltrados.forEach(municipio => {
        const option = document.createElement("option");
        option.value = municipio.id;
        option.textContent = municipio.nombre;
        municipioSelect.appendChild(option);
      });
    } catch (error) {
      console.error("Error al cargar municipios:", error);
    }
  });
});

const form = document.getElementById("formUsuario");

form.addEventListener("submit", function (event) {
  event.preventDefault(); // Evitar recarga

  if (modoFormulario === "registro") {
    // ✅ Validaciones para REGISTRO
    if (!form.checkValidity()) {
      form.reportValidity();
      return;
    }

    const password = document.getElementById("contrasena").value;
    const confirmPassword = document.getElementById("confirmar_contrasena").value;
    if (password !== confirmPassword) {
      document.getElementById("errorLabel").textContent = "Las contraseñas no coinciden.";
      return;
    }

    const usuario = document.getElementById("usuario").value;
    const correo = document.getElementById("correo").value;

    fetch("http://localhost:5003/api/usuarios/validar", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ usuario, correo })
    })
    .then(res => res.json())
    .then(data => {
      if (data.usuarioExistente || data.correoExistente) {
        document.getElementById("errorLabel").textContent =
          data.usuarioExistente ? "El usuario ya existe." : "El correo ya está registrado.";
        return;
      }

      // ✅ Si todo está bien, registramos
      registrarPersona();
    })
    .catch(err => {
      console.error("Error al validar en backend:", err);
      document.getElementById("errorLabel").textContent = "Ocurrió un error. Intenta más tarde.";
    });

  } else if (modoFormulario === "edicion") {
    // ✅ Lógica directa para EDICIÓN, sin validar usuario/contraseña
    editarPersona();
  }
});

async function registrarPersona() {
  const personaData = {
    nombre : document.getElementById("nombre").value,
    apellido : document.getElementById("apellido").value,
    genero : document.getElementById("genero").value,
    fecha_nacimiento : document.getElementById("fecha_nacimiento").value,
    telefono : document.getElementById("telefono").value,
    correo : document.getElementById("correo").value,
    id_municipio : document.getElementById("municipio").value,
    direccion : document.getElementById("direccion").value,
    zip : document.getElementById("zip").value
  };

  try {
    // 1. Registrar persona
    const responsePersona = await fetch("http://localhost:5003/api/Persona", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(personaData)
    });

    const personaId = await responsePersona.json();

    // 2. Registrar usuario con el id de la persona
    const usuarioData = {
      usuario: document.getElementById("usuario").value,
      contrasena: document.getElementById("contrasena").value,
      personaId: personaId,
      rolId : document.getElementById("cargo").value
    };

    const responseUsuario = await fetch("http://localhost:5003/api/Usuarios", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(usuarioData)
    });

    if (responseUsuario.ok) {
      document.getElementById('formUsuario').reset();
      errorLabel.value ="";
      modal.style.display = "none";
      mostrarModal("¡Registro exitoso!");
      obtenerUsuarios();
    } else {
      mostrarModal("Error al registrar usuario.");
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

let idPersonaActual = null;

async function abrirModalEdicion(id) {
  modoFormulario = "edicion";
  idPersonaActual = id; // Guardamos el id actual que vamos a editar

  try {
    // 1. Consultar datos de Persona
    const responsePersona = await fetch(`http://localhost:5003/api/Persona/${id}`, {
      method: "GET",
      headers: {
        "Authorization": `Bearer ${token}`,  // Enviar el token en el encabezado de la solicitud
        "Content-Type": "application/json"
      }
    });
    if (!responsePersona.ok) {
      throw new Error("No se pudo obtener los datos de la persona.");
    }
    const persona = await responsePersona.json();

    // 2. Consultar datos de Usuario
    const responseUsuario = await fetch(`http://localhost:5003/api/Usuarios/porPersona/${id}`, {
      method: "GET",
      headers: {
        "Authorization": `Bearer ${token}`,  // Enviar el token en el encabezado de la solicitud
        "Content-Type": "application/json"
      }
    });
    if (!responseUsuario.ok) {
      throw new Error("No se pudo obtener los datos del usuario.");
    }
    const usuario = await responseUsuario.json();

    // 3. Llenar los campos del formulario
    document.getElementById("nombre").value = persona.nombre;
    document.getElementById("apellido").value = persona.apellido;
    document.getElementById("genero").value = persona.genero;
    // Convertir la fecha (de aaaa-mm-dd a dd/mm/aaaa)
    const fechaNacimiento = persona.fecha_nacimiento;
    const fechaFormateada = new Date(fechaNacimiento);
    const dia = String(fechaFormateada.getDate()).padStart(2, '0');
    const mes = String(fechaFormateada.getMonth() + 1).padStart(2, '0');
    const año = fechaFormateada.getFullYear();
    document.getElementById("fecha_nacimiento").value = `${año}-${mes}-${dia}`;
    document.getElementById("telefono").value = persona.telefono;
    document.getElementById("correo").value = persona.correo;
    document.getElementById("direccion").value = persona.direccion;
    document.getElementById("zip").value = persona.zip;

    // Cargar los departamentos y municipios
    await cargarDepartamentosYMunicipios(persona.id_municipio); 

     // Campos de Usuario
    document.getElementById("usuario").value = usuario.usuario;

     // Si el usuario es "Cliente" y la opción no aparece, la agregamos dinámicamente
     if (!document.getElementById("cargo").querySelector('option[value="3"]')) {
      const optionCliente = document.createElement('option');
      optionCliente.value = "3";
      optionCliente.text = "Cliente";
      document.getElementById("cargo").appendChild(optionCliente);
    }

    document.getElementById("cargo").value = usuario.rolId;

    // Nuevo campo: Estado
    document.getElementById("estado").value = usuario.estado;
    document.getElementById("campoEstado").style.display = "block";

    // Desactivar campo usuario
    document.getElementById("usuario").disabled = true;

    // Ocultar el campo contraseña
    document.getElementById("usuarioContrasena").style.display = "none";
    document.getElementById("contrasena").required = false;
    document.getElementById("confirmar_contrasena").required = false;

    // Mostrar el modal
    document.getElementById("modal").style.display = "block";

  } catch (error) {
    console.error("Error al cargar datos para edición:", error);
  }
}

async function cargarDepartamentosYMunicipios(municipioId) {
  try {
    // 1. Obtener los departamentos
    const responseDepartamentos = await fetch("http://localhost:5003/api/Departamento");
    const departamentos = await responseDepartamentos.json();
    const departamentoSelect = document.getElementById("departamento");

    // Limpiar opciones de departamento
    departamentoSelect.innerHTML = "<option value=''>Selecciona un departamento</option>";

    // Agregar departamentos al select
    departamentos.forEach(departamento => {
      const option = document.createElement("option");
      option.value = departamento.id;
      option.textContent = departamento.nombre;
      departamentoSelect.appendChild(option);
    });

    // 2. Obtener los municipios
    const responseMunicipios = await fetch("http://localhost:5003/api/Municipio");
    const municipios = await responseMunicipios.json();
    const municipioSelect = document.getElementById("municipio");

    // Limpiar opciones de municipio
    municipioSelect.innerHTML = "<option value=''>Selecciona un municipio</option>";

    // Agregar municipios al select
    municipios.forEach(municipio => {
      const option = document.createElement("option");
      option.value = municipio.id;
      option.textContent = municipio.nombre;
      municipioSelect.appendChild(option);
    });

    // 3. Seleccionar el municipio y departamento que corresponden
    const municipioSeleccionado = municipios.find(mun => mun.id === municipioId);
    if (municipioSeleccionado) {
      municipioSelect.value = municipioSeleccionado.id;
    }

    if (municipioSeleccionado) {
      // 3. Ahora que tienes el municipio, busca el departamento asociado
      const departamentoSeleccionado = departamentos.find(dep => dep.id === municipioSeleccionado.id_Departamento);
    
      // 4. Si encuentras el departamento, actualiza el valor del select de departamento
      if (departamentoSeleccionado) {
        departamentoSelect.value = departamentoSeleccionado.id;
      } else {
        console.error("No se encontró el departamento para este municipio.");
      }
    }

  } catch (error) {
    console.error("Error al cargar departamentos y municipios:", error);
  }
}

async function editarPersona() {
  const personaData = {
    id: idPersonaActual,
    nombre: document.getElementById("nombre").value,
    apellido: document.getElementById("apellido").value,
    genero: document.getElementById("genero").value,
    fecha_nacimiento: document.getElementById("fecha_nacimiento").value,
    telefono: document.getElementById("telefono").value,
    correo: document.getElementById("correo").value,
    id_municipio: document.getElementById("municipio").value,
    direccion: document.getElementById("direccion").value,
    zip: document.getElementById("zip").value
  };

  const usuarioData = {
    rolId: document.getElementById("cargo").value,
    estado: document.getElementById("estado").value === "true" ? true : false
  };

  try {
    // Actualizar Persona
    await fetch(`http://localhost:5003/api/Persona/${idPersonaActual}`, {
      method: "PUT",
      headers: { "Authorization": `Bearer ${token}`,
                 "Content-Type": "application/json" 
                },
      body: JSON.stringify(personaData)
    });

    // Actualizar Usuario
    await fetch(`http://localhost:5003/api/Usuarios/porPersona/${idPersonaActual}`, {
      method: "PUT",
      headers: { "Authorization": `Bearer ${token}`,
                 "Content-Type": "application/json" 
                },
      body: JSON.stringify(usuarioData)
    });

    mostrarModal("¡Edición exitosa!");
    document.getElementById('formUsuario').reset();
    document.getElementById('modal').style.display = "none";
    obtenerUsuarios();

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