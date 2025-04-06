Join

SELECT p.precio, m.nombre_membresia 
FROM pago p 
JOIN membresia m ON p.id = m.id_pago;

SELECT m.nombre_membresia, m.precio, p.fecha_pago
FROM membresia m
JOIN pago p ON m.id_pago = p.id;

SELECT p.nombre AS nombre_persona, m.nombre AS nombre_municipio
FROM persona p
JOIN municipio m ON p.id_municipio = m.id;

SELECT c.nombre AS nombre_clase, u.usuario AS nombre_usuario
FROM clase c
JOIN usuarios u ON c.id_usuario = u.id;

SELECT s.nombre AS nombre_suplemento, u.usuario AS usuario_comprador
FROM suplemento_deportivo s
JOIN usuarios u ON s.id_usuario = u.id;

SELECT r.nombre AS nombre_rol, u.usuario AS nombre_usuario
FROM rol r
JOIN usuarios u ON r.id = u.id_rol;

SELECT p.nombre AS nombre_persona, m.nombre_membresia, m.precio
FROM persona p
JOIN usuarios u ON p.id = u.id_persona
JOIN pago pg ON u.id = pg.id_usuario
JOIN membresia m ON pg.id = m.id_pago;

SELECT p.nombre AS nombre_persona, c.nombre AS nombre_clase, i.estado
FROM persona p
JOIN usuarios u ON p.id = u.id_persona
JOIN inscripcion i ON u.id = i.id_usuario
JOIN clase c ON i.id_clase = c.id;

SELECT m.nombre AS nombre_municipio, d.nombre AS nombre_departamento, p.nombre AS nombre_persona
FROM municipio m
JOIN departamento d ON m.id_departamento = d.id
JOIN persona p ON p.id_municipio = m.id;

SELECT s.nombre AS nombre_suplemento, u.usuario AS usuario_comprador, m.nombre_membresia
FROM suplemento_deportivo s
JOIN usuarios u ON s.id_usuario = u.id
JOIN pago p ON u.id = p.id_usuario
JOIN membresia m ON p.id = m.id_pago;

SELECT s.nombre AS nombre_suplemento, u.usuario AS usuario_comprador, m.nombre_membresia
FROM suplemento_deportivo s
JOIN usuarios u ON s.id_usuario = u.id
JOIN pago p ON u.id = p.id_usuario
JOIN membresia m ON p.id = m.id_pago;

SELECT m.nombre AS nombre_municipio, d.nombre AS nombre_departamento, p.nombre AS nombre_persona, c.nombre AS nombre_clase
FROM municipio m
JOIN departamento d ON m.id_departamento = d.id
JOIN persona p ON p.id_municipio = m.id
JOIN usuarios u ON p.id = u.id_persona
JOIN inscripcion i ON u.id = i.id_usuario
JOIN clase c ON i.id_clase = c.id;

SELECT u.usuario AS nombre_usuario, r.nombre AS nombre_rol, m.nombre_membresia, p.precio AS precio_pago, c.nombre AS nombre_clase
FROM usuarios u
JOIN rol r ON u.id_rol = r.id
JOIN pago p ON u.id = p.id_usuario
JOIN membresia m ON p.id = m.id_pago
JOIN inscripcion i ON u.id = i.id_usuario
JOIN clase c ON i.id_clase = c.id;

SELECT p.nombre AS nombre_persona, m.nombre AS nombre_municipio, d.nombre AS nombre_departamento, s.nombre AS nombre_suplemento, c.nombre AS nombre_clase
FROM persona p
JOIN municipio m ON p.id_municipio = m.id
JOIN departamento d ON m.id_departamento = d.id
JOIN usuarios u ON p.id = u.id_persona
LEFT JOIN suplemento_deportivo s ON u.id = s.id_usuario
LEFT JOIN inscripcion i ON u.id = i.id_usuario
LEFT JOIN clase c ON i.id_clase = c.id;

SELECT m.nombre_membresia, p.nombre AS nombre_persona, s.nombre AS nombre_suplemento, c.nombre AS nombre_clase
FROM membresia m
JOIN pago pg ON m.id_pago = pg.id
JOIN usuarios u ON pg.id_usuario = u.id
JOIN persona p ON u.id_persona = p.id
LEFT JOIN suplemento_deportivo s ON u.id = s.id_usuario
LEFT JOIN inscripcion i ON u.id = i.id_usuario
LEFT JOIN clase c ON i.id_clase = c.id;

SELECT c.nombre AS nombre_clase, 
       u.usuario AS nombre_usuario, 
       p.precio AS precio_pago, 
       m.nombre_membresia AS nombre_membresia
FROM clase c
JOIN usuarios u ON c.id_usuario = u.id
JOIN pago p ON p.id_usuario = u.id
JOIN membresia m ON m.id_pago = p.id
ORDER BY m.precio DESC;

