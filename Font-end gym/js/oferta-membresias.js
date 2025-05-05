document.addEventListener("DOMContentLoaded", () => {
  const usuario = localStorage.getItem("idUsuario");
  const cerrarSesion = document.getElementById("cerrar-sesion");

  if (usuario) {
    if (cerrarSesion) cerrarSesion.style.display = "block";
  } else {
    if (cerrarSesion) cerrarSesion.style.display = "none";
  }
});

document.getElementById("cerrarSesion").addEventListener("click", function () {
  localStorage.removeItem("token");
  localStorage.removeItem("idUsuario");
  localStorage.removeItem("rol");
  localStorage.removeItem("nombreUsuario");
  window.location.href = "login.html";
});

const usuario = localStorage.getItem("idUsuario");

const token = localStorage.getItem("token");

const nombreUsuario = localStorage.getItem("nombreUsuario");

const contenedor = document.getElementById("membresias-container");

let membresiasGuardadas = [];

fetch("http://localhost:5003/api/Membresia/publico/membresias")
  .then(response => response.json())
  .then(membresias => {
    membresias.forEach(m => {
      membresiasGuardadas = membresias;
      const col = document.createElement("div");
      col.className = "col-md-3";

      col.innerHTML = `
        <ul class="price1">
          <h4>${m.duracionMeses} meses de membresía</h4>
          <h2 class="m_25">$${m.precio.toLocaleString()}<small></small></h2>
          <ul class="price_list">
            <p>
              <h4>${m.tipoMembresiaNombre}</h4>
            </p>
            <p>
              <img src="images/tick.png" width="16" height="14"/>
              <a>${m.descripcion}</a>
            </p>
            <a class="popup-with-zoom-anim comprar" href="#">
              <li class="price_but">COMPRA AHORA</li>
            </a>
            <div class="clear"></div>
          </ul>
        </ul>
      `;

      contenedor.appendChild(col);

      if(token != null || token != ""){
        llenarSelectMembresias(membresias);

        const usuarioInput = document.getElementById("usuario");

        if (nombreUsuario) {
          usuarioInput.value = nombreUsuario;
          usuarioInput.readOnly = true; // Lo hace no editable
        }
      }

      // 🔸 Aquí agregas el listener para mostrar monto y vencimiento
      const selectMembresia = document.getElementById("tipo_membresia");
      const inputMonto = document.getElementById("monto");
      const inputVencimiento = document.getElementById("vencimiento");

      selectMembresia.addEventListener("change", () => {
        const idSeleccionado = parseInt(selectMembresia.value);
        const seleccionada = membresiasGuardadas.find(m => m.id === idSeleccionado);

        if (seleccionada) {
          // Mostrar monto
          inputMonto.value = seleccionada.precio;

          // Calcular vencimiento sumando los meses
          const hoy = new Date();
          hoy.setMonth(hoy.getMonth() + seleccionada.duracionMeses);
          const fechaFormateada = hoy.toISOString().split("T")[0];
          inputVencimiento.value = fechaFormateada;
          inputVencimiento.readOnly = true; 
          inputMonto.readOnly = true; 
        }
      });
    });
  });


  document.addEventListener("click", function (e) {
    if (e.target && e.target.closest(".comprar")) {
      e.preventDefault();  
      if (!usuario) {
        document.getElementById("modal-inicio-registro").style.display = "block";
      } else {
         // Verificar si el usuario ya tiene una membresía vigente
         fetch(`http://localhost:5003/api/Pago/usuario/${usuario}`, {
          method: "GET",
          headers: { 
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
          }
        })
        .then(response => {
          if (!response.ok) throw new Error('No se pudo obtener el pago');
          return response.json();
        })
        .then(data => {
          const fechaVigencia = new Date(data.fecha_vigencia);
          const hoy = new Date();

          if (fechaVigencia > hoy) {
            // Tiene membresía vigente
            document.getElementById("modal-membresia-vigente").style.display = "block";
          } else {
            // No tiene membresía vigente
            document.getElementById("modal-pago").style.display = "block";
          }
        })
        .catch(error => {
          console.error("Error al verificar la membresía:", error);
          // Si hay error, aún puedes decidir mostrar la modal de pago
          document.getElementById("modal-pago").style.display = "block";
        });
      }
    }
  });

  document.querySelectorAll('.cerrar').forEach(btn => {
    btn.onclick = () => {
      btn.parentElement.parentElement.style.display = 'none';
    };
  });
  
  window.onclick = function(event) {
    document.querySelectorAll('.modal').forEach(modal => {
      if (event.target == modal) {
        modal.style.display = "none";
      }
    });
  }


  fetch('http://localhost:5003/api/MetodoPago', {
    method: "GET",
    headers: {
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  })
  .then(res => res.json())
  .then(data => {
    const select = document.getElementById('metodo_pago');
    select.innerHTML = ''; // Limpiar opciones anteriores

     // Opción por defecto
     const defaultOption = document.createElement('option');
     defaultOption.value = '';
     defaultOption.textContent = 'Seleccione un método de pago';
     defaultOption.disabled = true;
     defaultOption.selected = true;
     select.appendChild(defaultOption);

    data.forEach(metodo => {
      const option = document.createElement('option');
      option.value = metodo.id; // o metodo.nombre, depende de tu estructura
      option.textContent = metodo.nombre;
      select.appendChild(option);
    });
  });

  function llenarSelectMembresias(membresias) {
    const select = document.getElementById('tipo_membresia');
    select.innerHTML = '';
  
    const defaultOption = document.createElement('option');
    defaultOption.value = '';
    defaultOption.textContent = 'Seleccione una opción';
    defaultOption.disabled = true;
    defaultOption.selected = true;
    select.appendChild(defaultOption);
  
    membresias.forEach(m => {
      const option = document.createElement('option');
      option.value = m.id;
      option.textContent = m.tipoMembresiaNombre;
      select.appendChild(option);
    });
  }

  document.getElementById('metodo_pago').addEventListener('change', function() {
    var metodoPagoSelect = document.getElementById('metodo_pago');
    var metodoPagoTexto = metodoPagoSelect.options[metodoPagoSelect.selectedIndex].text;
    var opcionesTarjeta = document.getElementById('opciones_tarjeta');
    var opcionesPse = document.getElementById('opciones_pse');
    
    // Mostrar u ocultar las opciones de tarjeta
    if (metodoPagoTexto === 'Tarjeta credito o debito') {
      opcionesTarjeta.style.display = 'block';  // Mostrar opciones de tipo de tarjeta
      opcionesPse.style.display = 'none';      // Ocultar opciones de PSE
    } else if (metodoPagoTexto === 'PSE') {
      opcionesTarjeta.style.display = 'none';  // Ocultar opciones de tipo de tarjeta
      opcionesPse.style.display = 'block';     // Mostrar opciones de PSE
    } else {
      opcionesTarjeta.style.display = 'none';  // Ocultar opciones de tipo de tarjeta
      opcionesPse.style.display = 'none';      // Ocultar opciones de PSE
    }
  });

  const boton = document.getElementById('botonPagar');
  
  boton.addEventListener("click", function () {
    obtenerPago(); // o tu lógica de pagos
  });


  async function obtenerPago() {
    // Obtener los valores del formulario
    const tipoMembresia = document.getElementById('tipo_membresia').value;
    const precio = document.getElementById('monto').value;
    const fechaVigencia = document.getElementById('vencimiento').value;
    const metodoPago = document.getElementById('metodo_pago').value;
  
    // Obtener la fecha de hoy
    const today = new Date();
    const fechaPago = today.toISOString();
  
    // Crear el objeto pago
    const pago = {
      precio: precio,
      fechaPago: fechaPago,
      fechaVigencia: fechaVigencia,
      idUsuario: usuario,
      idMetodoPago: metodoPago,
      membresia_id: tipoMembresia
    };

    try {
      const response = await fetch("http://localhost:5003/api/Pago", {
        method: "POST",
        headers: {
          "Authorization": `Bearer ${token}`,
          "Content-Type": "application/json"
        },
        body: JSON.stringify(pago),
      });
  
      if (!response.ok) {
        throw new Error("Error al obtener los datos");
      }
      // Mostrar el modal de éxito
      document.getElementById('modal-exito').style.display = 'block';
    } catch (error) {
      console.error("Error al cargar los datos de membresia:", error);
      // Mostrar el modal de error
      document.getElementById('modal-error').style.display = 'block';
    }
  }
  
  const btnCerrarExito = document.getElementById('cerrar-modal-activo');
  const modalExito = document.getElementById('modal-exito');
  const modalError = document.getElementById('modal-error');

  btnCerrarExito.addEventListener('click', () => {
    modalExito.style.display = 'none';
    window.location.href = 'classes.html';
  });

  // También podrías cerrar haciendo clic fuera del contenido
  window.addEventListener('click', (event) => {
    if (event.target === modalExito) {
      modalExito.style.display = 'none';
    }
    if (event.target === modalError) {
      modalError.style.display = 'none';
    }
  });

  function cerrarModalMembresia() {
    document.getElementById("modal-membresia-vigente").style.display = "none";
  }