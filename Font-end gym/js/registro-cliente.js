document.addEventListener("DOMContentLoaded", () => {
  cargarOpciones();
});

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


const form = document.getElementById("formularioRegistro");

form.addEventListener("submit", function (event) {
  event.preventDefault(); // Siempre lo mantenemos para evitar recargar la página

  // ✅ 1. Validación HTML5 de campos obligatorios
  if (!form.checkValidity()) {
    form.reportValidity(); // Muestra errores nativos si faltan campos
    return;
  }

  // ✅ 2. Validación de contraseñas
  const password = document.getElementById("contrasena").value;
  const confirmPassword = document.getElementById("confirmar_contrasena").value;
  if (password !== confirmPassword) {
    document.getElementById("errorLabel").textContent = "Las contraseñas no coinciden.";
    return;
  }

  // ✅ 3. Validación en backend (usuario y correo)
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
    registrarCliente();
  })
  .catch(err => {
    console.error("Error al validar en backend:", err);
    document.getElementById("errorLabel").textContent = "Ocurrió un error. Intenta más tarde.";
  });
});

async function registrarCliente() {
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
      rolId : 3
    };

    const responseUsuario = await fetch("http://localhost:5003/api/Usuarios", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(usuarioData)
    });

    if (responseUsuario.ok) {
      document.getElementById('formularioRegistro').reset();
      mostrarModal("¡Registro exitoso!");
    } else {
      mostrarModal("Error al registrar usuario.");
    }

  } catch (error) {
    console.error("Error:", error);
    alert("Ocurrió un error en el registro.");
  }
}

function mostrarModal(mensaje) {
  const modal = document.getElementById("miModal");
  const mensajeModal = document.getElementById("mensajeModal");
  const cerrar = document.getElementById("cerrarModal");

  mensajeModal.innerText = mensaje;
  modal.style.display = "block";

  cerrar.onclick = () => modal.style.display = "none";
  window.onclick = (event) => {
    if (event.target === modal) {
      modal.style.display = "none";
    }
  };
}