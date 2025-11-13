CREATE DATABASE dbCentroDesarrollo2030;

USE dbCentroDesarrollo2030;


CREATE TABLE estado_solicitud (
    id INT NOT NULL IDENTITY(1,1),
    nombre_estado VARCHAR(50) NOT NULL,
    descripcion VARCHAR(100) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE tipo_usuario (
    id INT NOT NULL IDENTITY(1,1),
    nombre_tipo_usuario VARCHAR(50) NOT NULL,
    descripcion VARCHAR(100) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE tipo_servicio (
    id INT NOT NULL IDENTITY(1,1),
    nombre_tipo_servicio VARCHAR(50) NOT NULL,
    descripcion VARCHAR(100) NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE usuario (
    id_usuario INT NOT NULL IDENTITY(1,1),
    cedula INT NOT NULL,
    nombre VARCHAR(200) NOT NULL,
    correo VARCHAR(100) NOT NULL,
    password VARCHAR(30) NOT NULL,
    telefono INT,
    tipo_usuario INT NOT NULL,
    PRIMARY KEY (id_usuario),
    FOREIGN KEY (tipo_usuario) REFERENCES tipo_usuario(id)
);

CREATE TABLE solicitud (
    id_solicitud INT NOT NULL IDENTITY(1,1),
    asunto VARCHAR(100) NOT NULL,
    detalle VARCHAR(1000),
    tipo_servicio INT NOT NULL,
    estado_solicitud INT NOT NULL,
    fecha_inicio DATETIME,
    fecha_fin DATETIME,
    id_usuario INT NOT NULL,
    PRIMARY KEY (id_solicitud),
    FOREIGN KEY (tipo_servicio) REFERENCES tipo_servicio(id),
    FOREIGN KEY (estado_solicitud) REFERENCES estado_solicitud(id),
    FOREIGN KEY (id_usuario) REFERENCES usuario(id_usuario)
);

CREATE TABLE programa_servicio (
    id_servicio INT NOT NULL IDENTITY(1,1),
    nombre VARCHAR(100) NOT NULL,
    detalle VARCHAR(1000),
    tipo_servicio INT NOT NULL,
    fecha_inicio DATETIME,
    fecha_fin DATETIME,
    PRIMARY KEY (id_servicio),
    FOREIGN KEY (tipo_servicio) REFERENCES tipo_servicio(id)
);

CREATE TABLE usuarios_x_programa (
    id_servicio INT NOT NULL,
    id_usuario INT NOT NULL,
    asiste BIT DEFAULT 0,
    PRIMARY KEY (id_servicio, id_usuario),
    FOREIGN KEY (id_servicio) REFERENCES programa_servicio(id_servicio),
    FOREIGN KEY (id_usuario) REFERENCES usuario(id_usuario)
);

