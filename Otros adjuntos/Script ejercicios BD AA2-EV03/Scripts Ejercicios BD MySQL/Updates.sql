Updates

UPDATE membresia 
SET precio = 100 
WHERE nombre_membresia = 'Gold';

UPDATE membresia 
SET duracion_membresia_en_meses = 12 
WHERE nombre_membresia = 'Silver';

UPDATE membresia 
SET descripcion = 'Acceso a clases básicas' 
WHERE nombre_membresia = 'Bronze';

UPDATE membresia 
SET precio = 200 
WHERE nombre_membresia = 'Diamond';

UPDATE membresia 
SET duracion_membresia_en_meses = 6 
WHERE nombre_membresia = 'Gold';

UPDATE membresia 
SET nombre_membresia = 'Basic', precio = 50.00, duracion_membresia_en_meses = 1, descripcion = 'Acceso básico al gimnasio', id_pago = 1
WHERE id = 1;

UPDATE membresia 
SET nombre_membresia = 'Standard', precio = 80.00, duracion_membresia_en_meses = 3, descripcion = 'Acceso a clases grupales', id_pago = 2
WHERE id = 2;

UPDATE membresia 
SET nombre_membresia = 'Premium', precio = 120.00, duracion_membresia_en_meses = 6, descripcion = 'Acceso a clases grupales y piscina', id_pago = 3
WHERE id = 3;

UPDATE membresia 
SET nombre_membresia = 'Gold', precio = 150.00, duracion_membresia_en_meses = 12, descripcion = 'Acceso a todas las instalaciones', id_pago = 4
WHERE id = 4;

UPDATE membresia 
SET nombre_membresia = 'VIP', precio = 200.00, duracion_membresia_en_meses = 12, descripcion = 'Acceso a todas las instalaciones y eventos especiales', id_pago = 5
WHERE id = 5;

UPDATE persona 
SET direccion = 'Calle 45 #20-30', telefono = '3219876543' 
WHERE id = 1;

UPDATE persona 
SET direccion = 'Carrera 10 #15-40', telefono = '3107654321' 
WHERE id = 2;

UPDATE persona 
SET direccion = 'Avenida Siempre Viva #742', telefono = '3151234567' 
WHERE id = 3;

UPDATE persona 
SET direccion = 'Calle 12A #55-10', telefono = '3123456789' 
WHERE id = 4;

UPDATE persona 
SET direccion = 'Carrera 4A #66-22', telefono = '3176543210' 
WHERE id = 5;

UPDATE suplemento_deportivo 
SET precio = 45000, descripcion = 'Suplemento de proteína avanzada' 
WHERE id = 1;

UPDATE suplemento_deportivo 
SET precio = 38000, descripcion = 'Vitaminas y minerales esenciales' 
WHERE id = 2;

UPDATE suplemento_deportivo 
SET precio = 29000, descripcion = 'Aminoácidos de cadena ramificada' 
WHERE id = 3;

UPDATE suplemento_deportivo 
SET precio = 50000, descripcion = 'Creatina para alto rendimiento' 
WHERE id = 4;

UPDATE suplemento_deportivo 
SET precio = 35000, descripcion = 'Suplemento de omega 3 concentrado' 
WHERE id = 5;

UPDATE inscripcion 
SET estado = 'Activo', fecha_inscripcion = '2024-10-30' 
WHERE id = 1;

UPDATE inscripcion 
SET estado = 'Inactivo', fecha_inscripcion = '2024-09-15' 
WHERE id = 2;

UPDATE inscripcion 
SET estado = 'Pendiente', fecha_inscripcion = '2024-08-12' 
WHERE id = 3;

UPDATE inscripcion 
SET estado = 'Cancelado', fecha_inscripcion = '2024-07-22' 
WHERE id = 4;

UPDATE inscripcion 
SET estado = 'Activo', fecha_inscripcion = '2024-06-10' 
WHERE id = 5;
