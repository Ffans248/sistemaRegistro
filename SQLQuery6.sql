SELECT * FROM tbProveedor;
-- ===========================================
INSERT INTO tbProveedor (nombre, nit, direccion, telefono, correo) VALUES
('Distribuidora El Buen Precio', '1234567-8', 'Zona 1, Ciudad de Guatemala', '50255510001', 'contacto@buenprecio.com'),
('Importadora San Juan', '2345678-9', 'Calle Principal, Cobán, Alta Verapaz', '50255520002', 'ventas@sanjuan.gt'),
('TecnoDistribuciones', '3456789-0', 'Zona 9, Guatemala', '50255530003', 'info@tecnodist.com');
GO

-- ===========================================
-- 2. Datos de prueba para tbCategoria
-- ===========================================
INSERT INTO tbCategoria (nombreCategoria, descripcion) VALUES
('Bebidas', 'Refrescos, jugos y bebidas energéticas'),
('Snacks', 'Productos como galletas, papas fritas y dulces'),
('Limpieza', 'Artículos de limpieza para el hogar'),
('Tecnología', 'Accesorios y dispositivos electrónicos');
GO

-- ===========================================
-- 3. Datos de prueba para tbProducto
-- ===========================================
INSERT INTO tbProducto (nombreProducto, descripcion, precioCosto, precioVenta, descuento, stockActual, stockMinimo) VALUES
('Coca-Cola 600ml', 'Bebida gaseosa', 4.50, 7.00, 0, 100, 20),
('Doritos Queso 40g', 'Snack de maíz sabor queso', 2.00, 3.50, 0, 150, 30),
('Detergente Ariel 1kg', 'Detergente en polvo', 20.00, 30.00, 5.00, 60, 10),
('Mouse Logitech M170', 'Mouse inalámbrico óptico', 85.00, 120.00, 10.00, 25, 5);
GO

-- ===========================================
-- 4. Relaciones producto-categoría (tbProductoCategoria)
-- ===========================================
INSERT INTO tbProductoCategoria (idProducto, idCategoria) VALUES
(1, 1), -- Coca-Cola → Bebidas
(2, 2), -- Doritos → Snacks
(3, 3), -- Ariel → Limpieza
(4, 4); -- Mouse → Tecnología
GO

-- ===========================================
-- 5. Datos de prueba para tbCompra
-- ===========================================
INSERT INTO tbCompra (idProveedor, numeroFactura, fechaCompra, totalCompra) VALUES
(1, 'FAC-001', '2025-10-10', 700.00),
(2, 'FAC-002', '2025-10-11', 450.00);
GO

-- ===========================================
-- 6. Datos de prueba para tbDetalleCompra
-- ===========================================
INSERT INTO tbDetalleCompra (idCompra, idProducto, cantidad, precioUnitario, descuento, subtotal) VALUES
(1, 1, 50, 4.50, 0, 225.00),   -- 50 Coca-Cola
(1, 2, 100, 2.00, 0, 200.00),  -- 100 Doritos
(1, 3, 10, 20.00, 5.00, 190.00), -- 10 Detergente
(2, 4, 5, 85.00, 10.00, 425.00); -- 5 Mouse Logitech
GO