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

IF OBJECT_ID('Pedido', 'U') IS NOT NULL DROP TABLE Pedido;
CREATE TABLE Pedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,          
    Fecha DATETIME NOT NULL,        
    Total DECIMAL(10, 2) NOT NULL,  
    Estado NVARCHAR(50) NOT NULL DEFAULT 'Confirmado', 
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);

IF OBJECT_ID('LineaPedido', 'U') IS NOT NULL DROP TABLE LineaPedido;
CREATE TABLE LineaPedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PedidoId INT NOT NULL,          
    ProductoNombre NVARCHAR(100) NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10, 2) NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL,
    ProductoIdOriginal INT NOT NULL,  
    TipoProductoOriginal NVARCHAR(50) NOT NULL,
    FOREIGN KEY (PedidoId) REFERENCES Pedido(Id) ON DELETE CASCADE
);

IF OBJECT_ID('Opinion', 'U') IS NOT NULL DROP TABLE Opinion;
CREATE TABLE Opinion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    UsuarioNombre NVARCHAR(100) NOT NULL,
    ProductoId INT NOT NULL,
    Texto NVARCHAR(500) NOT NULL,
    Puntuacion INT NOT NULL,
    Fecha DATETIME NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id) ON DELETE CASCADE
);