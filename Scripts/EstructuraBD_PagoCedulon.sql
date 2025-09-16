-- =============================================
-- Script de Base de Datos para Sistema de Pago de Cedulones
-- =============================================

-- Tabla de Deudores
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Deudores' AND xtype='U')
BEGIN
    CREATE TABLE Deudores (
        id INT IDENTITY(1,1) PRIMARY KEY,
        cuit_cuil VARCHAR(20) NOT NULL UNIQUE,
        apellido VARCHAR(100) NOT NULL,
        nombre VARCHAR(100) NOT NULL,
        tipo_documento VARCHAR(10) NOT NULL,
        numero_documento VARCHAR(20) NOT NULL,
        fecha_nacimiento DATE,
        sexo CHAR(1),
        direccion VARCHAR(200),
        localidad VARCHAR(100),
        provincia VARCHAR(100),
        codigo_postal VARCHAR(10),
        telefono VARCHAR(50),
        email VARCHAR(150),
        fecha_creacion DATETIME DEFAULT GETDATE(),
        activo BIT DEFAULT 1
    );
    
    -- Índices
    CREATE INDEX IX_Deudores_CuitCuil ON Deudores(cuit_cuil);
    CREATE INDEX IX_Deudores_Documento ON Deudores(tipo_documento, numero_documento);
    
    PRINT 'Tabla Deudores creada exitosamente.';
END
ELSE
    PRINT 'Tabla Deudores ya existe.';

-- Tabla de Cedulones
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cedulones' AND xtype='U')
BEGIN
    CREATE TABLE Cedulones (
        id INT IDENTITY(1,1) PRIMARY KEY,
        numero_cedulon VARCHAR(50) NOT NULL UNIQUE,
        id_deudor INT NOT NULL,
        descripcion VARCHAR(500) NOT NULL,
        monto_original DECIMAL(18,2) NOT NULL,
        monto_intereses DECIMAL(18,2) DEFAULT 0,
        monto_total AS (monto_original + monto_intereses) PERSISTED,
        fecha_emision DATETIME NOT NULL DEFAULT GETDATE(),
        fecha_vencimiento DATETIME NOT NULL,
        fecha_pago DATETIME NULL,
        estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente', -- Pendiente, Pagado, Vencido, Anulado
        observaciones TEXT,
        fecha_creacion DATETIME DEFAULT GETDATE(),
        usuario_creacion VARCHAR(100),
        
        CONSTRAINT FK_Cedulones_Deudores FOREIGN KEY (id_deudor) REFERENCES Deudores(id),
        CONSTRAINT CK_Cedulones_Estado CHECK (estado IN ('Pendiente', 'Pagado', 'Vencido', 'Anulado')),
        CONSTRAINT CK_Cedulones_Montos CHECK (monto_original > 0 AND monto_intereses >= 0)
    );
    
    -- Índices
    CREATE INDEX IX_Cedulones_Numero ON Cedulones(numero_cedulon);
    CREATE INDEX IX_Cedulones_Estado ON Cedulones(estado);
    CREATE INDEX IX_Cedulones_Vencimiento ON Cedulones(fecha_vencimiento);
    CREATE INDEX IX_Cedulones_Deudor ON Cedulones(id_deudor);
    
    PRINT 'Tabla Cedulones creada exitosamente.';
END
ELSE
    PRINT 'Tabla Cedulones ya existe.';

-- Tabla de Tarjetas de Crédito
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TarjetasCredito' AND xtype='U')
BEGIN
    CREATE TABLE TarjetasCredito (
        id INT IDENTITY(1,1) PRIMARY KEY,
        nombre VARCHAR(100) NOT NULL,
        descripcion VARCHAR(200),
        logo VARCHAR(10), -- Texto corto para mostrar en el logo (ej: VISA, MC, etc.)
        color_primario VARCHAR(7), -- Color hexadecimal
        color_secundario VARCHAR(7), -- Color hexadecimal
        orden INT DEFAULT 0,
        activa BIT DEFAULT 1,
        fecha_creacion DATETIME DEFAULT GETDATE(),
        
        CONSTRAINT CK_TarjetasCredito_Colores CHECK (
            color_primario LIKE '#[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]' 
            OR color_primario IS NULL
        )
    );
    
    CREATE INDEX IX_TarjetasCredito_Activa ON TarjetasCredito(activa, orden);
    
    PRINT 'Tabla TarjetasCredito creada exitosamente.';
END
ELSE
    PRINT 'Tabla TarjetasCredito ya existe.';

-- Tabla de Planes de Cuotas
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PlanesCuotas' AND xtype='U')
BEGIN
    CREATE TABLE PlanesCuotas (
        id INT IDENTITY(1,1) PRIMARY KEY,
        id_tarjeta INT NOT NULL,
        cantidad_cuotas INT NOT NULL,
        costo_financiero DECIMAL(5,4) NOT NULL, -- Porcentaje como decimal (ej: 0.1250 = 12.50%)
        porcentaje_descuento DECIMAL(5,2) DEFAULT 0, -- Descuento sobre intereses para deudas vencidas
        aplica_descuento_vencido BIT DEFAULT 0, -- Si aplica descuento solo para deudas vencidas
        descripcion VARCHAR(200),
        orden INT DEFAULT 0,
        activo BIT DEFAULT 1,
        fecha_creacion DATETIME DEFAULT GETDATE(),
        
        CONSTRAINT FK_PlanesCuotas_Tarjetas FOREIGN KEY (id_tarjeta) REFERENCES TarjetasCredito(id),
        CONSTRAINT CK_PlanesCuotas_Cuotas CHECK (cantidad_cuotas > 0 AND cantidad_cuotas <= 60),
        CONSTRAINT CK_PlanesCuotas_Costo CHECK (costo_financiero >= 0 AND costo_financiero <= 1),
        CONSTRAINT CK_PlanesCuotas_Descuento CHECK (porcentaje_descuento >= 0 AND porcentaje_descuento <= 100),
        
        UNIQUE(id_tarjeta, cantidad_cuotas) -- No permitir planes duplicados
    );
    
    CREATE INDEX IX_PlanesCuotas_Tarjeta ON PlanesCuotas(id_tarjeta, activo, orden);
    
    PRINT 'Tabla PlanesCuotas creada exitosamente.';
END
ELSE
    PRINT 'Tabla PlanesCuotas ya existe.';

-- Tabla de Transacciones de Pago
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TransaccionesPago' AND xtype='U')
BEGIN
    CREATE TABLE TransaccionesPago (
        id INT IDENTITY(1,1) PRIMARY KEY,
        numero_cedulon VARCHAR(50) NOT NULL,
        id_tarjeta INT NOT NULL,
        id_plan INT NOT NULL,
        monto_original DECIMAL(18,2) NOT NULL,
        monto_intereses DECIMAL(18,2) NOT NULL,
        monto_total DECIMAL(18,2) NOT NULL,
        cantidad_cuotas INT NOT NULL,
        monto_cuota DECIMAL(18,2) NOT NULL,
        costo_financiero_aplicado DECIMAL(5,4) NOT NULL,
        descuento_aplicado DECIMAL(5,2) DEFAULT 0,
        fecha_creacion DATETIME DEFAULT GETDATE(),
        fecha_aprobacion DATETIME NULL,
        fecha_rechazo DATETIME NULL,
        estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente', -- Pendiente, Aprobado, Rechazado, Cancelado
        codigo_autorizacion VARCHAR(100),
        referencia_externa VARCHAR(200), -- ID del procesador de pagos
        ip_cliente VARCHAR(45),
        user_agent TEXT,
        observaciones TEXT,
        
        CONSTRAINT FK_TransaccionesPago_Tarjetas FOREIGN KEY (id_tarjeta) REFERENCES TarjetasCredito(id),
        CONSTRAINT FK_TransaccionesPago_Planes FOREIGN KEY (id_plan) REFERENCES PlanesCuotas(id),
        CONSTRAINT CK_TransaccionesPago_Estado CHECK (estado IN ('Pendiente', 'Aprobado', 'Rechazado', 'Cancelado')),
        CONSTRAINT CK_TransaccionesPago_Montos CHECK (monto_original > 0 AND monto_total > 0 AND monto_cuota > 0)
    );
    
    -- Índices
    CREATE INDEX IX_TransaccionesPago_Cedulon ON TransaccionesPago(numero_cedulon);
    CREATE INDEX IX_TransaccionesPago_Estado ON TransaccionesPago(estado, fecha_creacion);
    CREATE INDEX IX_TransaccionesPago_Fecha ON TransaccionesPago(fecha_creacion);
    CREATE INDEX IX_TransaccionesPago_Referencia ON TransaccionesPago(referencia_externa);
    
    PRINT 'Tabla TransaccionesPago creada exitosamente.';
END
ELSE
    PRINT 'Tabla TransaccionesPago ya existe.';

-- =============================================
-- DATOS DE PRUEBA
-- =============================================

-- Insertar tarjetas de crédito de ejemplo
IF NOT EXISTS (SELECT * FROM TarjetasCredito WHERE nombre = 'Visa')
BEGIN
    INSERT INTO TarjetasCredito (nombre, descripcion, logo, color_primario, color_secundario, orden, activa)
    VALUES 
    ('Visa', 'Tarjeta Visa - Todos los bancos', 'VISA', '#1A1F71', '#FFFFFF', 1, 1),
    ('Mastercard', 'Tarjeta Mastercard - Todos los bancos', 'MC', '#EB001B', '#FF5F00', 2, 1),
    ('American Express', 'American Express', 'AMEX', '#006FCF', '#FFFFFF', 3, 1),
    ('Cabal', 'Tarjeta Cabal', 'CABAL', '#E31837', '#FFFFFF', 4, 1),
    ('Naranja', 'Tarjeta Naranja', 'NAR', '#FF6900', '#FFFFFF', 5, 1);
    
    PRINT 'Tarjetas de crédito de ejemplo insertadas.';
END

-- Insertar planes de cuotas de ejemplo
IF NOT EXISTS (SELECT * FROM PlanesCuotas WHERE id_tarjeta = 1)
BEGIN
    DECLARE @visaId INT = (SELECT id FROM TarjetasCredito WHERE nombre = 'Visa');
    DECLARE @mastercardId INT = (SELECT id FROM TarjetasCredito WHERE nombre = 'Mastercard');
    DECLARE @amexId INT = (SELECT id FROM TarjetasCredito WHERE nombre = 'American Express');
    
    -- Planes para Visa
    INSERT INTO PlanesCuotas (id_tarjeta, cantidad_cuotas, costo_financiero, porcentaje_descuento, aplica_descuento_vencido, descripcion, orden, activo)
    VALUES 
    (@visaId, 1, 0.0000, 0, 0, 'Pago único sin interés', 1, 1),
    (@visaId, 3, 0.0850, 5, 1, '3 cuotas - 8.50% anual', 2, 1),
    (@visaId, 6, 0.1250, 10, 1, '6 cuotas - 12.50% anual', 3, 1),
    (@visaId, 12, 0.1850, 15, 1, '12 cuotas - 18.50% anual', 4, 1),
    (@visaId, 18, 0.2250, 20, 1, '18 cuotas - 22.50% anual', 5, 1),
    (@visaId, 24, 0.2650, 25, 1, '24 cuotas - 26.50% anual', 6, 1);
    
    -- Planes para Mastercard
    INSERT INTO PlanesCuotas (id_tarjeta, cantidad_cuotas, costo_financiero, porcentaje_descuento, aplica_descuento_vencido, descripcion, orden, activo)
    VALUES 
    (@mastercardId, 1, 0.0000, 0, 0, 'Pago único sin interés', 1, 1),
    (@mastercardId, 3, 0.0900, 5, 1, '3 cuotas - 9.00% anual', 2, 1),
    (@mastercardId, 6, 0.1300, 10, 1, '6 cuotas - 13.00% anual', 3, 1),
    (@mastercardId, 12, 0.1900, 15, 1, '12 cuotas - 19.00% anual', 4, 1),
    (@mastercardId, 18, 0.2300, 20, 1, '18 cuotas - 23.00% anual', 5, 1),
    (@mastercardId, 24, 0.2700, 25, 1, '24 cuotas - 27.00% anual', 6, 1);
    
    -- Planes para American Express
    INSERT INTO PlanesCuotas (id_tarjeta, cantidad_cuotas, costo_financiero, porcentaje_descuento, aplica_descuento_vencido, descripcion, orden, activo)
    VALUES 
    (@amexId, 1, 0.0000, 0, 0, 'Pago único sin interés', 1, 1),
    (@amexId, 3, 0.0750, 8, 1, '3 cuotas - 7.50% anual', 2, 1),
    (@amexId, 6, 0.1150, 12, 1, '6 cuotas - 11.50% anual', 3, 1),
    (@amexId, 12, 0.1750, 18, 1, '12 cuotas - 17.50% anual', 4, 1),
    (@amexId, 18, 0.2150, 22, 1, '18 cuotas - 21.50% anual', 5, 1);
    
    PRINT 'Planes de cuotas de ejemplo insertados.';
END

-- Insertar deudor de ejemplo
IF NOT EXISTS (SELECT * FROM Deudores WHERE cuit_cuil = '20123456789')
BEGIN
    INSERT INTO Deudores (cuit_cuil, apellido, nombre, tipo_documento, numero_documento, fecha_nacimiento, sexo, direccion, localidad, provincia, codigo_postal, telefono, email)
    VALUES 
    ('20123456789', 'González', 'Juan Carlos', 'DNI', '12345678', '1980-05-15', 'M', 'Av. Corrientes 1234', 'Buenos Aires', 'Ciudad Autónoma de Buenos Aires', '1043', '011-4567-8901', 'juan.gonzalez@email.com');
    
    PRINT 'Deudor de ejemplo insertado.';
END

-- Insertar cedulón de ejemplo
IF NOT EXISTS (SELECT * FROM Cedulones WHERE numero_cedulon = 'CEd-2025-001')
BEGIN
    DECLARE @deudorId INT = (SELECT id FROM Deudores WHERE cuit_cuil = '20123456789');
    
    INSERT INTO Cedulones (numero_cedulon, id_deudor, descripcion, monto_original, monto_intereses, fecha_emision, fecha_vencimiento, estado, usuario_creacion)
    VALUES 
    ('CED-2025-001', @deudorId, 'Impuesto Inmobiliario - Período 2024', 45000.00, 2250.00, GETDATE(), DATEADD(day, -30, GETDATE()), 'Pendiente', 'SISTEMA'),
    ('CED-2025-002', @deudorId, 'Tasa de Servicios Urbanos - 1er Trimestre 2025', 12500.00, 0.00, GETDATE(), DATEADD(day, 15, GETDATE()), 'Pendiente', 'SISTEMA');
    
    PRINT 'Cedulones de ejemplo insertados.';
END

-- =============================================
-- VISTAS ÚTILES
-- =============================================

-- Vista para consulta completa de cedulones
IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_CedulonesCompletos')
BEGIN
    EXEC('CREATE VIEW vw_CedulonesCompletos AS
    SELECT 
        c.id,
        c.numero_cedulon,
        c.descripcion,
        c.monto_original,
        c.monto_intereses,
        c.monto_total,
        c.fecha_emision,
        c.fecha_vencimiento,
        c.fecha_pago,
        c.estado,
        c.observaciones,
        d.cuit_cuil,
        d.apellido,
        d.nombre,
        d.apellido + '', '' + d.nombre AS nombre_completo,
        d.tipo_documento,
        d.numero_documento,
        d.direccion,
        d.localidad,
        d.provincia,
        d.codigo_postal,
        d.telefono,
        d.email,
        CASE WHEN c.fecha_vencimiento < GETDATE() AND c.estado = ''Pendiente'' THEN 1 ELSE 0 END AS esta_vencido,
        DATEDIFF(day, c.fecha_vencimiento, GETDATE()) AS dias_vencido
    FROM Cedulones c
    INNER JOIN Deudores d ON c.id_deudor = d.id');
    
    PRINT 'Vista vw_CedulonesCompletos creada exitosamente.';
END

-- Vista para transacciones con detalles
IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_TransaccionesCompletas')
BEGIN
    EXEC('CREATE VIEW vw_TransaccionesCompletas AS
    SELECT 
        t.id,
        t.numero_cedulon,
        t.monto_original,
        t.monto_intereses,
        t.monto_total,
        t.cantidad_cuotas,
        t.monto_cuota,
        t.costo_financiero_aplicado,
        t.descuento_aplicado,
        t.fecha_creacion,
        t.fecha_aprobacion,
        t.fecha_rechazo,
        t.estado,
        t.codigo_autorizacion,
        t.referencia_externa,
        tc.nombre AS tarjeta_nombre,
        tc.descripcion AS tarjeta_descripcion,
        pc.descripcion AS plan_descripcion,
        c.descripcion AS cedulon_descripcion,
        d.nombre_completo AS deudor_nombre
    FROM TransaccionesPago t
    INNER JOIN TarjetasCredito tc ON t.id_tarjeta = tc.id
    INNER JOIN PlanesCuotas pc ON t.id_plan = pc.id
    INNER JOIN vw_CedulonesCompletos c ON t.numero_cedulon = c.numero_cedulon
    INNER JOIN (SELECT cuit_cuil, apellido + '', '' + nombre AS nombre_completo FROM Deudores) d ON c.cuit_cuil = d.cuit_cuil');
    
    PRINT 'Vista vw_TransaccionesCompletas creada exitosamente.';
END

-- =============================================
-- STORED PROCEDURES ÚTILES
-- =============================================

-- Procedure para actualizar intereses de cedulones vencidos
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_ActualizarInteresesVencidos')
BEGIN
    EXEC('CREATE PROCEDURE sp_ActualizarInteresesVencidos
    AS
    BEGIN
        SET NOCOUNT ON;
        
        DECLARE @tasa_interes DECIMAL(5,4) = 0.0005; -- 0.05% diario
        
        UPDATE Cedulones 
        SET 
            monto_intereses = monto_original * @tasa_interes * DATEDIFF(day, fecha_vencimiento, GETDATE()),
            estado = ''Vencido''
        WHERE fecha_vencimiento < GETDATE() 
        AND estado = ''Pendiente''
        AND DATEDIFF(day, fecha_vencimiento, GETDATE()) > 0;
        
        SELECT @@ROWCOUNT AS CedulonesActualizados;
    END');
    
    PRINT 'Stored Procedure sp_ActualizarInteresesVencidos creado exitosamente.';
END

PRINT '=== SCRIPT COMPLETADO EXITOSAMENTE ===';
PRINT 'Estructura de base de datos para Sistema de Pago de Cedulones creada.';
PRINT 'Datos de ejemplo insertados.';
PRINT 'Para probar el sistema, use el cedulón: CED-2025-001';