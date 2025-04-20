// Datos simulados como si vinieran del backend
const estudiantesInscritos = [
    { id: 1, nombre: "Juan Pérez", edad: 25, correo: "juanp@mail.com", clase: "Crossfit" },
    { id: 2, nombre: "María Gómez", edad: 30, correo: "mariag@mail.com", clase: "Zumba" },
    { id: 3, nombre: "Carlos Ruiz", edad: 28, correo: "carlosr@mail.com", clase: "Funcional" },
    { id: 4, nombre: "Laura Torres", edad: 22, correo: "laurat@mail.com", clase: "Yoga" },
    { id: 5, nombre: "Andrés Mendoza", edad: 34, correo: "andresm@mail.com", clase: "Boxeo" },
    { id: 6, nombre: "Camila Ramírez", edad: 27, correo: "camilar@mail.com", clase: "Funcional" },
    { id: 7, nombre: "Luis Felipe", edad: 29, correo: "luisf@mail.com", clase: "Spinning" },
    { id: 8, nombre: "Valentina López", edad: 24, correo: "valel@mail.com", clase: "Crossfit" },
    { id: 9, nombre: "Esteban Díaz", edad: 31, correo: "esteband@mail.com", clase: "HIIT" },
    { id: 10, nombre: "Daniela Silva", edad: 26, correo: "danis@mail.com", clase: "Yoga" }
  ];
  
  // Referencia a la tabla en el HTML
  const tabla = document.getElementById("students-table-body");
  const buscador = document.getElementById("buscador");

  // Renderizar los datos en la tabla
  function renderTabla(estudiantes) {
    tabla.innerHTML = "";
  
    estudiantes.forEach((estudiante, index) => {
      const fila = document.createElement("tr");
  
      fila.innerHTML = `
        <td>${index + 1}</td>
        <td>${estudiante.nombre}</td>
        <td>${estudiante.edad}</td>
        <td>${estudiante.correo}</td>
        <td>${estudiante.clase}</td>
      `;
  
      tabla.appendChild(fila);
    });
  }
  function filtrarTbla(valor){
    const filtro = valor.toLowerCase();

    const filtrados = estudiantesInscritos.filter(e =>
        e.nombre.toLowerCase().includes(filtro) ||
        e.correo.toLowerCase().includes(filtro) ||
        e.clase.toLowerCase().includes(filtro)
    );

    renderTabla(filtrados);
  }



  // Ejecutar al cargar la página
  window.addEventListener("DOMContentLoaded", () => {
    renderTabla(estudiantesInscritos);

    buscador.addEventListener("input", (e) => {
        filtrarTbla(e.target.value);
    });

  });