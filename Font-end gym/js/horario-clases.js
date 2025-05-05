let clasesData = []; 

const url = "http://localhost:5003/api/Clase/publico/clases";

// Hacer la petición al backend
fetch(url)
  .then(response => {
    if (!response.ok) {
      throw new Error('No podemos mostrar las clases disponibles en este momento');
    }
    return response.json();
  })
  .then(data => {
    renderSchedule(data);         // Vista escritorio (tabla)
    renderScheduleMobile(data);   // Vista móvil (lista)
  })
  .catch(error => {
    console.error("Hubo un problema con la solicitud:", error);
  });


const dias = ["Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"];
const horas = [
  "05:00", "06:00", "07:00", "08:00", "09:00",
  "10:00", "11:00", "12:00", "13:00", "14:00",
  "15:00", "16:00", "17:00", "18:00", "19:00"
];

function renderSchedule(data) {
  const tbody = document.getElementById("schedule-body");
  tbody.innerHTML = "";

  for (let i = 0; i < horas.length - 1; i++) {
    const row = document.createElement("tr");

    // Columna de hora
    const horaInicio = horas[i];
    const horaFin = horas[i + 1];
    const horaCell = document.createElement("td");
    horaCell.textContent = `${horaInicio} - ${horaFin}`;
    row.appendChild(horaCell);

    // Celdas por día
    for (let dia of dias) {
      const cell = document.createElement("td");
      
      const inicioCelda = toMinutes(horaInicio);
      const finCelda = toMinutes(horaFin);
    
      const actividades = data.filter(item => {
        const inicioClase = toMinutes(item.hora);
        const finClase = toMinutes(item.horaFin);
        return item.dia === dia && (
          inicioClase < finCelda && finClase > inicioCelda
        );
      });
    
      if (actividades.length > 0) {
        cell.classList.add("event");
        cell.innerHTML = actividades.map(actividad =>
          `<a>${actividad.nombre}</a><br>${actividad.hora} - ${actividad.horaFin}`
        ).join("<hr>");
      }
    
      row.appendChild(cell);
    }

    tbody.appendChild(row);
  }
}

function renderScheduleMobile(data) {
  const list = document.querySelector(".timetable_small .items_list");
  list.innerHTML = "";

  data.forEach(item => {
    const li = document.createElement("li");
    li.classList.add("item");

    li.innerHTML = `
      <strong>${item.nombre}</strong><br>
      <span>${item.dia} — ${item.hora} a ${item.horaFin}</span>
    `;

    list.appendChild(li);
  });
}


function toMinutes(hora) {
  const [h, m] = hora.split(":").map(Number);
  return h * 60 + m;
}