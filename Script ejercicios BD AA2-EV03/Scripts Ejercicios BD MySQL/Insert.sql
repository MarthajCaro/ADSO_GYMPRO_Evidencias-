Insert

INSERT INTO `usuarios`(`id`, `usuario`, `correo`, `contrasena`, `fecha_nacimiento`, `edad`, `id_persona`, `id_rol`) VALUES
(NULL, 'Carlos_López', 'carlos.lopez@example.com', 'password1', '1990-05-10', 33, 1, 1),
(NULL, 'Ana_Martínez', 'ana.martinez@example.com', 'password2', '1985-07-20', 38, 2, 2),
(NULL, 'María_Gómez', 'maria.gomez@example.com', 'password3', '1992-09-15', 31, 3, 3),
(NULL, 'Pedro_Ramírez', 'pedro.ramirez@example.com', 'password4', '1988-11-23', 35, 4, 1),
(NULL, 'Laura_Torres', 'laura.torres@example.com', 'password5', '1995-03-30', 28, 5, 2),
(NULL, 'Luis_Pérez', 'luis.perez@example.com', 'password6', '1993-04-11', 30, 6, 3),
(NULL, 'Jorge_Rojas', 'jorge.rojas@example.com', 'password7', '1989-02-01', 34, 7, 4),
(NULL, 'Claudia_Lara', 'claudia.lara@example.com', 'password8', '1986-06-19', 37, 8, 1),
(NULL, 'Sofia_Castro', 'sofia.castro@example.com', 'password9', '1994-08-25', 29, 9, 3),
(NULL, 'Andrés_Mendoza', 'andres.mendoza@example.com', 'password10', '1991-10-12', 32, 10, 2),
(NULL, 'Juliana_Moreno', 'juliana.moreno@example.com', 'password11', '1997-01-05', 26, 11, 3),
(NULL, 'Felipe_Diaz', 'felipe.diaz@example.com', 'password12', '1992-12-18', 31, 12, 4),
(NULL, 'Lorena_Ruiz', 'lorena.ruiz@example.com', 'password13', '1996-05-25', 27, 13, 2),
(NULL, 'Natalia_Vargas', 'natalia.vargas@example.com', 'password14', '1990-09-09', 33, 14, 4);

INSERT INTO Persona (id, nombre, apellido, genero, fecha_nacimiento, telefono, correo, id_municipio, direccion, zip) VALUES
(1, 'Carlos', 'López', 'M', '1990-05-10', '3105551111', 'carlos.lopez@example.com', 1, 'Calle 123', '111111'),
(2, 'Ana', 'Martínez', 'F', '1985-07-20', '3205552222', 'ana.martinez@example.com', 1, 'Carrera 45', '111112'),
(3, 'María', 'Gómez', 'F', '1992-09-15', '3155553333', 'maria.gomez@example.com', 2, 'Avenida 9', '111113'),
(4, 'Pedro', 'Ramírez', 'M', '1988-11-23', '3105554444', 'pedro.ramirez@example.com', 3, 'Diagonal 50', '111114'),
(5, 'Laura', 'Torres', 'F', '1995-03-30', '3215555555', 'laura.torres@example.com', 2, 'Transversal 22', '111115'),
(6, 'Luis', 'Pérez', 'M', '1993-04-11', '3125556666', 'luis.perez@example.com', 1, 'Calle 80', '111116'),
(7, 'Jorge', 'Rojas', 'M', '1989-02-01', '3105557777', 'jorge.rojas@example.com', 3, 'Carrera 10', '111117'),
(8, 'Claudia', 'Lara', 'F', '1986-06-19', '3135558888', 'claudia.lara@example.com', 2, 'Calle 50', '111118'),
(9, 'Sofia', 'Castro', 'F', '1994-08-25', '3225559999', 'sofia.castro@example.com', 3, 'Avenida 68', '111119'),
(10, 'Andrés', 'Mendoza', 'M', '1991-10-12', '3165550000', 'andres.mendoza@example.com', 1, 'Carrera 13', '111120'),
(11, 'Juliana', 'Moreno', 'F', '1997-01-05', '3145551010', 'juliana.moreno@example.com', 3, 'Calle 7', '111121'),
(12, 'Felipe', 'Diaz', 'M', '1992-12-18', '3105552020', 'felipe.diaz@example.com', 1, 'Avenida 26', '111122'),
(13, 'Lorena', 'Ruiz', 'F', '1996-05-25', '3155553030', 'lorena.ruiz@example.com', 2, 'Carrera 66', '111123'),
(14, 'Natalia', 'Vargas', 'F', '1990-09-09', '3195554040', 'natalia.vargas@example.com', 3, 'Calle 19', '111124'),
(15, 'Raúl', 'Guerrero', 'M', '1987-07-13', '3105555050', 'raul.guerrero@example.com', 2, 'Carrera 20', '111125');


INSERT INTO pago (id, precio, fecha_pago, metodo_pago, fecha_vigencia, id_usuario) VALUES
(NULL, 50000, '2023-10-01', 'Tarjeta', '2024-10-01', 3),
(NULL, 70000, '2023-11-01', 'Efectivo', '2024-11-01', 5),
(NULL, 60000, '2023-09-01', 'Transferencia', '2024-09-01', 9),
(NULL, 80000, '2023-08-01', 'Tarjeta', '2024-08-01', 6),
(NULL, 90000, '2023-12-01', 'Efectivo', '2024-12-01', 11);

INSERT INTO Membresia (id, nombre_membresia, precio, duracion_membresia_en_meses, descripcion, id_pago) VALUES
(NULL, 'Mensual', 50000, 1, 'Acceso limitado', 1),
(NULL, 'Trimestral', 150000, 3, 'Acceso completo', 2),
(NULL, 'Anual', 600000, 12, 'Acceso VIP', 3),
(NULL, 'Semestral', 300000, 6, 'Acceso completo', 4),
(NULL, 'Bimensual', 100000, 2, 'Acceso regular', 5);

INSERT INTO Clase (id, nombre, duracion_en_minutos, descripcion,id_usuario) VALUES
(NULL, 'Yoga', 60, 'Clase de relajación y estiramiento', 2),
(NULL, 'Crossfit', 45, 'Entrenamiento de alta intensidad', 6);

INSERT INTO Inscripcion (id, fecha_inscripcion, estado, id_clase, id_usuario) VALUES
(NULL, '2023-10-01', 'Activo', 1, 3),
(NULL, '2023-11-01', 'Activo', 2, 5),
(NULL, '2023-09-01', 'Activo', 1, 9),
(NULL, '2023-08-01', 'Activo', 2, 11),
(NULL, '2023-12-01', 'Activo', 1, 6);

INSERT INTO Suplemento_deportivo (id, nombre, tipo, descripcion, precio, id_usuario) VALUES
(NULL, 'Proteína Whey', 'Proteína', 'Suplemento de proteína para ganar masa muscular', 70000, 3),
(NULL, 'Creatina', 'Creatina', 'Aumenta el rendimiento y la resistencia', 50000, 5);