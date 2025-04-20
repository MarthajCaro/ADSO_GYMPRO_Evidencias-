document.addEventListener("DOMContentLoaded", () => {
    const tabla = document.getElementById("tabla-pagos");
  
    fetch("http://localhost:8080/api/pagos")
      .then(res => res.json())
      .then(pagos => {
        pagos.forEach(pago => {
          const fila = document.createElement("tr");
  
          fila.innerHTML = `
            <td>${pago.cliente}</td>
            <td>${pago.plan}</td>
            <td>${pago.vencimiento}</td>
            <td>$${pago.monto}</td>
            <td class="${pago.estado === 'Pagado' ? 'estado-pagado' : 'estado-pendiente'}">
              ${pago.estado}
            </td>
          `;
  
          tabla.appendChild(fila);
        });
      })
      .catch(err => {
        console.error("Error al cargar pagos:", err);
      });
  });
  