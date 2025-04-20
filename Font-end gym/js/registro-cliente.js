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