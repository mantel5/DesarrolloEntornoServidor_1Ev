CREATE DATABASE SuplementosDB;


USE SuplementosDB;


IF OBJECT_ID('Creatina', 'U') IS NOT NULL DROP TABLE Creatina;
CREATE TABLE Creatina (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Imagen NVARCHAR(MAX) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    Categoria NVARCHAR(50) NOT NULL,
    PesoKg FLOAT NOT NULL,
    Sabor NVARCHAR(50) NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    Formato NVARCHAR(50) NOT NULL,
    SelloCreapure BIT NOT NULL,
    EsMicronizada BIT NOT NULL,
    DosisDiariaGr INT NOT NULL
);

IF OBJECT_ID('Proteina', 'U') IS NOT NULL DROP TABLE Proteina;
CREATE TABLE Proteina (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Imagen NVARCHAR(MAX) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    Categoria NVARCHAR(50) NOT NULL,
    PesoKg FLOAT NOT NULL,
    Sabor NVARCHAR(50) NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    Porcentaje INT NOT NULL,
    EsSinLactosa BIT NOT NULL
);

IF OBJECT_ID('Omega3', 'U') IS NOT NULL DROP TABLE Omega3;
CREATE TABLE Omega3 (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Imagen NVARCHAR(MAX) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    Categoria NVARCHAR(50) NOT NULL,
    PesoKg FLOAT NOT NULL,
    Formato NVARCHAR(50) NOT NULL,
    Origen NVARCHAR(50) NOT NULL,
    MgEPA INT NOT NULL,
    MgDHA INT NOT NULL,
    CertificadoIFOS BIT NOT NULL
);

IF OBJECT_ID('PreEntreno', 'U') IS NOT NULL DROP TABLE PreEntreno;
CREATE TABLE PreEntreno (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Imagen NVARCHAR(MAX) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    Categoria NVARCHAR(50) NOT NULL,
    PesoKg FLOAT NOT NULL,
    Formato NVARCHAR(50) NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    Sabor NVARCHAR(50) NOT NULL,
    MgCafeina INT NOT NULL,
    TieneBetaAlanina BIT NOT NULL
);

IF OBJECT_ID('Salsa', 'U') IS NOT NULL DROP TABLE Salsa;
CREATE TABLE Salsa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Imagen NVARCHAR(MAX) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    Calorias FLOAT NOT NULL,
    Proteinas FLOAT NOT NULL,
    Carbohidratos FLOAT NOT NULL,
    Grasas FLOAT NOT NULL,
    Sabor NVARCHAR(50) NOT NULL,
    EsPicante BIT NOT NULL,
    EsZero BIT NOT NULL
);

IF OBJECT_ID('Tortitas', 'U') IS NOT NULL DROP TABLE Tortitas;
CREATE TABLE Tortitas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Imagen NVARCHAR(MAX) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    Calorias FLOAT NOT NULL,
    Proteinas FLOAT NOT NULL,
    Carbohidratos FLOAT NOT NULL,
    Grasas FLOAT NOT NULL,
    Sabor NVARCHAR(50) NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    PesoGr INT NOT NULL,
    EsSinGluten BIT NOT NULL
);

IF OBJECT_ID('Bebida', 'U') IS NOT NULL DROP TABLE Bebida;
CREATE TABLE Bebida (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Imagen NVARCHAR(MAX) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    Calorias FLOAT NOT NULL,
    Proteinas FLOAT NOT NULL,
    Carbohidratos FLOAT NOT NULL,
    Grasas FLOAT NOT NULL,
    Sabor NVARCHAR(50) NOT NULL,
    Mililitros INT NOT NULL,
    TieneGluten BIT NOT NULL,
    TieneGas BIT NOT NULL
);

IF OBJECT_ID('Packs', 'U') IS NOT NULL DROP TABLE Packs;
CREATE TABLE Packs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    Imagen NVARCHAR(MAX) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    Categoria NVARCHAR(50) NOT NULL,
    PesoKg FLOAT NOT NULL,
    ProteinaId INT NOT NULL,
    PreEntrenoId INT NOT NULL,
    CreatinaId INT NOT NULL,
    BebidaId INT NOT NULL,
    FOREIGN KEY (ProteinaId) REFERENCES Proteina(Id),
    FOREIGN KEY (PreEntrenoId) REFERENCES PreEntreno(Id),
    FOREIGN KEY (CreatinaId) REFERENCES Creatina(Id),
    FOREIGN KEY (BebidaId) REFERENCES Bebida(Id)
);

IF OBJECT_ID('Usuario', 'U') IS NOT NULL DROP TABLE Usuario;
CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE, 
    Password NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(20) NOT NULL DEFAULT 'Cliente',
    Direccion NVARCHAR(255) NULL,
    Telefono NVARCHAR(20) NULL
);