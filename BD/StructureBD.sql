-- ---------- Database structure (SQL Server) ----------
-- Tables translated from the diagram:
-- usuario            -> Users
-- tipo_usuario       -> UserTypes
-- tipo_servicio      -> ServiceTypes
-- solicitud          -> Requests
-- estado_solicitud   -> RequestStatuses
-- programa_servicio  -> ServicePrograms
-- usuarios_x_programa-> ProgramUsers

SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE DATABASE Activo2030

USE Activo2030
-- Optional: create schema (use dbo if you prefer)
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'activo2030') 
    EXEC('CREATE SCHEMA activo2030');

-- 1) UserTypes
CREATE TABLE activo2030.UserTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL
);


-- 2) Users
CREATE TABLE activo2030.Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,              -- id_usuario
    NationalId NVARCHAR(50) NULL,                   -- cedula    
	Name NVARCHAR(100) NOT NULL,                -- nombre
	SurName1 NVARCHAR(200) NOT NULL,                -- nombre
	SurName2 NVARCHAR(200) NOT NULL,                -- nombre
    Email NVARCHAR(200) NULL,                       -- correo
    Password VARCHAR(MAX) NULL,                     -- password (store hash)
    Phone NVARCHAR(50) NULL,                        -- telefono
    UserTypeId INT NOT NULL,                        -- tipo_usuario FK
    CreatedAt DATETIME NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_Users_Email UNIQUE(Email),
    CONSTRAINT FK_Users_UserTypes FOREIGN KEY (UserTypeId)
        REFERENCES activo2030.UserTypes(Id)
);

CREATE INDEX IX_Users_UserTypeId ON activo2030.Users(UserTypeId);
CREATE INDEX IX_Users_NationalId ON activo2030.Users(NationalId);

-- 3) ServiceTypes
CREATE TABLE activo2030.ServiceTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,               -- tipo_servicio.id
    Name NVARCHAR(150) NOT NULL,
    Description NVARCHAR(500) NULL
);

-- 4) RequestStatuses
CREATE TABLE activo2030.RequestStatuses (
    Id INT IDENTITY(1,1) PRIMARY KEY,               -- estado_solicitud.id
    Name NVARCHAR(150) NOT NULL,
    Description NVARCHAR(500) NULL
);

-- 5) Requests (solicitud)
CREATE TABLE activo2030.Requests (
    Id INT IDENTITY(1,1) PRIMARY KEY,               -- id_solicitud
    Subject NVARCHAR(250) NOT NULL,                 -- asunto
    Details NVARCHAR(MAX) NULL,                     -- detalle
    ServiceTypeId INT NOT NULL,                     -- tipo_servicio FK
    StatusId INT NOT NULL,                          -- estado_solicitud FK
    StartDate DATETIME NULL,                       -- fecha_inicio
    EndDate DATETIME NULL,                         -- fecha_fin
    UserId INT NOT NULL,                            -- id_usuario FK (creator/owner)
    CreatedAt DATETIME NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Requests_ServiceTypes FOREIGN KEY (ServiceTypeId)
        REFERENCES activo2030.ServiceTypes(Id),

    CONSTRAINT FK_Requests_Status FOREIGN KEY (StatusId)
        REFERENCES activo2030.RequestStatuses(Id),
    CONSTRAINT FK_Requests_User FOREIGN KEY (UserId)
        REFERENCES activo2030.Users(Id));

CREATE INDEX IX_Requests_ServiceTypeId ON activo2030.Requests(ServiceTypeId);
CREATE INDEX IX_Requests_StatusId ON activo2030.Requests(StatusId);
CREATE INDEX IX_Requests_UserId ON activo2030.Requests(UserId);

-- 6) ServicePrograms (programa_servicio)
CREATE TABLE activo2030.ServicePrograms (
    Id INT IDENTITY(1,1) PRIMARY KEY,               -- id_servicio
    Name NVARCHAR(200) NOT NULL,
    Details NVARCHAR(MAX) NULL,
    ServiceTypeId INT NOT NULL,                     -- tipo_servicio
    StartDate DATETIME NULL,                       -- fecha_inicio
    EndDate DATETIME NULL,                         -- fecha_fin
    CreatedAt DATETIME NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_ServicePrograms_ServiceTypes FOREIGN KEY (ServiceTypeId)
        REFERENCES activo2030.ServiceTypes(Id)        );

CREATE INDEX IX_ServicePrograms_ServiceTypeId ON activo2030.ServicePrograms(ServiceTypeId);

-- 7) ProgramUsers (usuarios_x_programa) - junction table
CREATE TABLE activo2030.ProgramUsers (
    ServiceProgramId INT NOT NULL,                  -- id_servicio -> ServicePrograms.Id
    UserId INT NOT NULL,                            -- id_usuario -> Users.Id
    Attended BIT NOT NULL DEFAULT 0,                -- asiste
    RegisteredAt DATETIME NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_ProgramUsers PRIMARY KEY (ServiceProgramId, UserId),
    CONSTRAINT FK_ProgramUsers_ServicePrograms FOREIGN KEY (ServiceProgramId)
        REFERENCES activo2030.ServicePrograms(Id)
        ,
    CONSTRAINT FK_ProgramUsers_Users FOREIGN KEY (UserId)
        REFERENCES activo2030.Users(Id)        
);

CREATE INDEX IX_ProgramUsers_UserId ON activo2030.ProgramUsers(UserId);

-- Commit transaction
COMMIT TRANSACTION;
