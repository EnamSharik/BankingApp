-- ============================================
-- SISTEMA DE GESTIÓN FINANCIERA
-- Base de Datos Oracle
-- ============================================

-- Tabla: MONEDA
-- Descripción: Catálogo de monedas disponibles
CREATE TABLE MONEDA (
    id_moneda NUMBER PRIMARY KEY,
    codigo VARCHAR2(3) NOT NULL UNIQUE,
    nombre VARCHAR2(50) NOT NULL,
    simbolo VARCHAR2(5),
    --estado VARCHAR2(1) DEFAULT 'A' CHECK (estado IN ('A', 'I')),
    fecha_creacion DATE DEFAULT SYSDATE
);

-- Tabla: USUARIO
-- Descripción: Usuarios del sistema con acceso a la aplicación
CREATE TABLE USUARIO (
    id_usuario NUMBER PRIMARY KEY,
    usuario VARCHAR2(50) NOT NULL UNIQUE,
    contrasena VARCHAR2(255) NOT NULL,
    fecha_creacion DATE DEFAULT SYSDATE,
    fecha_ultimo_acceso DATE
    --estado VARCHAR2(1) DEFAULT 'A' CHECK (estado IN ('A', 'I'))
);

-- Tabla: CLIENTE
-- Descripción: Inversionistas que colocan capital
CREATE TABLE CLIENTE (
    id_cliente NUMBER PRIMARY KEY,
    nombre VARCHAR2(100) NOT NULL,
    apellido VARCHAR2(100) NOT NULL,
    nit VARCHAR2(20),
    dpi VARCHAR2(20),
    telefono VARCHAR2(20),
    email VARCHAR2(100),
    direccion VARCHAR2(255),
    fecha_registro DATE DEFAULT SYSDATE,
    --estado VARCHAR2(1) DEFAULT 'A' CHECK (estado IN ('A', 'I'))
);

-- Tabla: ENTIDAD_FINANCIERA
-- Descripción: Instituciones bancarias del país
CREATE TABLE ENTIDAD_FINANCIERA (
    id_entidad NUMBER PRIMARY KEY,
    nombre VARCHAR2(150) NOT NULL,
    codigo VARCHAR2(20) UNIQUE,
    direccion VARCHAR2(255),
    telefono VARCHAR2(20),
    email VARCHAR2(100),
    fecha_registro DATE DEFAULT SYSDATE,
    --estado VARCHAR2(1) DEFAULT 'A' CHECK (estado IN ('A', 'I'))
);

-- Tabla: INVERSION
-- Descripción: Operaciones de inversión realizadas por clientes
CREATE TABLE INVERSION (
    id_inversion NUMBER PRIMARY KEY,
    id_cliente NUMBER NOT NULL,
    id_moneda NUMBER NOT NULL,
    monto_inversion NUMBER(15,2) NOT NULL,
    plazo_dias NUMBER NOT NULL,
    tasa_interes NUMBER(5,4) NOT NULL,
    modalidad_pago VARCHAR2(1) NOT NULL CHECK (modalidad_pago IN ('M', 'F')), -- M=Mensual, F=Final
    fecha_inversion DATE NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    monto_intereses NUMBER(15,2),
    --estado VARCHAR2(1) DEFAULT 'V' CHECK (estado IN ('V', 'C', 'A')), -- V=Vigente, C=Cancelado, A=Anulado
    fecha_creacion DATE DEFAULT SYSDATE,
    id_usuario_crea NUMBER NOT NULL,
    CONSTRAINT fk_inv_cliente FOREIGN KEY (id_cliente) REFERENCES CLIENTE(id_cliente),
    CONSTRAINT fk_inv_moneda FOREIGN KEY (id_moneda) REFERENCES MONEDA(id_moneda),
    CONSTRAINT fk_inv_usuario FOREIGN KEY (id_usuario_crea) REFERENCES USUARIO(id_usuario)
);

-- Tabla: PAGO_INVERSION
-- Descripción: Proyección y registro de pagos de intereses a inversionistas
CREATE TABLE PAGO_INVERSION (
    id_pago_inversion NUMBER PRIMARY KEY,
    id_inversion NUMBER NOT NULL,
    numero_pago NUMBER NOT NULL,
    fecha_pago_programada DATE NOT NULL,
    monto_interes NUMBER(15,2) NOT NULL,
    fecha_pago_real DATE,
    estado VARCHAR2(1) DEFAULT 'P' CHECK (estado IN ('P', 'C', 'A')), -- P=Pendiente, C=Cancelado, A=Anulado
    fecha_creacion DATE DEFAULT SYSDATE,
    CONSTRAINT fk_pago_inv FOREIGN KEY (id_inversion) REFERENCES INVERSION(id_inversion)
);

-- Tabla: PRESTAMO
-- Descripción: Operaciones de préstamo a entidades financieras
CREATE TABLE PRESTAMO (
    id_prestamo NUMBER PRIMARY KEY,
    id_entidad NUMBER NOT NULL,
    id_moneda NUMBER NOT NULL,
    monto_prestamo NUMBER(15,2) NOT NULL,
    plazo_dias NUMBER NOT NULL,
    tasa_interes NUMBER(5,4) NOT NULL,
    modalidad_pago VARCHAR2(1) NOT NULL CHECK (modalidad_pago IN ('M', 'F')), -- M=Mensual, F=Final
    fecha_prestamo DATE NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    monto_intereses NUMBER(15,2),
    --estado VARCHAR2(1) DEFAULT 'V' CHECK (estado IN ('V', 'C', 'A')), -- V=Vigente, C=Cancelado, A=Anulado
    fecha_creacion DATE DEFAULT SYSDATE,
    id_usuario_crea NUMBER NOT NULL,
    CONSTRAINT fk_prest_entidad FOREIGN KEY (id_entidad) REFERENCES ENTIDAD_FINANCIERA(id_entidad),
    CONSTRAINT fk_prest_moneda FOREIGN KEY (id_moneda) REFERENCES MONEDA(id_moneda),
    CONSTRAINT fk_prest_usuario FOREIGN KEY (id_usuario_crea) REFERENCES USUARIO(id_usuario)
);

-- Tabla: PAGO_PRESTAMO
-- Descripción: Proyección y registro de pagos de intereses recibidos
CREATE TABLE PAGO_PRESTAMO (
    id_pago_prestamo NUMBER PRIMARY KEY,
    id_prestamo NUMBER NOT NULL,
    numero_pago NUMBER NOT NULL,
    fecha_pago_programada DATE NOT NULL,
    monto_interes NUMBER(15,2) NOT NULL,
    fecha_pago_real DATE,
    --estado VARCHAR2(1) DEFAULT 'P' CHECK (estado IN ('P', 'C', 'A')), -- P=Pendiente, C=Cancelado, A=Anulado
    fecha_creacion DATE DEFAULT SYSDATE,
    CONSTRAINT fk_pago_prest FOREIGN KEY (id_prestamo) REFERENCES PRESTAMO(id_prestamo)
);