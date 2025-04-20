document.addEventListener("DOMContentLoaded", () => {
    const catalogo = document.querySelector(".catalogo");
  
    fetch("http://localhost:8080/api/suplementos")
      .then(response => response.json())
      .then(data => {
        data.forEach(producto => {
          const card = crearCardProducto(producto);
          catalogo.appendChild(card);
        });
      })
      .catch(error => {
        console.error("Error al cargar los productos:", error);
      });
  });
  
  function crearCardProducto(producto) {
    const div = document.createElement("div");
    div.classList.add("producto");
  
    div.innerHTML = `
      <img src="${producto.imagenUrl}" alt="${producto.nombre}">
      <div class="producto-info">
        <h2>${producto.nombre}</h2>
        <p>${producto.descripcion}</p>
        <div class="precio">$${producto.precio}</div>
        <div class="stock ${producto.stock > 0 ? "in-stock" : "out-stock"}">
          ${producto.stock > 0 ? "En stock" : "Sin stock"}
        </div>
        <div class="compra">
          <input type="number" min="1" max="${producto.stock}" value="1" ${producto.stock === 0 ? 'disabled' : ''}>
          <button ${producto.stock === 0 ? 'disabled' : ''}>Comprar</button>
        </div>
      </div>
    `;
  
    return div;
  }
  