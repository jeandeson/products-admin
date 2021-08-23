CREATE TABLE Enderecos (
	[IdEndereco] bigint IDENTITY(1,1),
	[Estado] NVARCHAR(50) NOT NULL,
	[Cidade] NVARCHAR(50) NOT NULL,
	[Bairro] NVARCHAR(50) NOT NULL,
	[Rua] NVARCHAR(50) NOT NULL,
	[Cep] bigint NOT NULL,
);

ALTER TABLE Enderecos ADD CONSTRAINT PK_IdEndereco PRIMARY KEY(IdEndereco)

-----------------------------------------

CREATE TABLE Centros (
	[IdCentro] bigint IDENTITY(1,1),
	[CodigoCentro] bigint NOT NULL,
	[NomeCentro] NVARCHAR(50) NOT NULL,    	
	[IdEndereco] bigint NULL,
);

ALTER TABLE Centros ADD CONSTRAINT PK_IdCentro PRIMARY KEY(IdCentro)
ALTER TABLE Centros ADD CONSTRAINT FK_IdEndereco_Centro Foreign Key(IdEndereco) References Enderecos(IdEndereco)

-----------------------------------------

CREATE TABLE Clientes (
	[IdCliente] bigint IDENTITY(1,1),
	[NomeCliente] NVARCHAR(50) NOT NULL,
	[SobreNomeCliente] NVARCHAR(50) NOT NULL,
	[EmailCliente] NVARCHAR(50) NOT NULL,
	[IdEndereco] NVARCHAR(50) NULL,
);

ALTER TABLE Clientes ADD CONSTRAINT PK_IdCliente PRIMARY KEY(IdCliente)
ALTER TABLE Clientes ADD CONSTRAINT FK_IdEndereco_cliente Foreign Key(IdEndereco) References Enderecos(IdEndereco)

-----------------------------------------

CREATE TABLE Produtos (
	[IdProduto] bigint IDENTITY(1,1),
	[CodigoProduto] bigint NOT NULL,
	[NomeProduto] NVARCHAR(50) NOT NULL,
	[QuantidadeProduto] int NOT NULL,
	[ValorProduto] NVARCHAR(50) NOT NULL,
	[ImagemProduto] NVARCHAR(50) NOT NULL,
	[IdCentro] bigint not null,
);

ALTER TABLE Produtos ADD CONSTRAINT PK_IdProduto PRIMARY KEY(IdProduto)
ALTER TABLE Produtos ADD CONSTRAINT FK_IdCentro Foreign Key(idCentro) References Centros(IdCentro)

-----------------------------------------

