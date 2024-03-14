CREATE TABLE [usuario] (
  [id] varchar(8) PRIMARY KEY,
  [email] nvarchar(150) UNIQUE NOT NULL,
  [senhaHash] nvarchar(200) NOT NULL,
  [primeiroNome] nvarchar(100) NOT NULL,
  [ultimoSobrenome] nvarchar(100) NOT NULL,
  [codigoValidacao] varchar(6) NOT NULL,
  [limiteValidacao] datetime NULL,
  [validacaoRealizada] datetime NULL
);
GO

CREATE TABLE [credenciais] (
  [id] int PRIMARY KEY IDENTITY,
  [titulo] nvarchar(100) NOT NULL
)
GO

CREATE TABLE [usuario_credenciais] (
  [id] int PRIMARY KEY,
  [usuarioId] varchar(8),
  [credenciaisId] int,
  FOREIGN KEY ([usuarioId]) REFERENCES usuario([id]) ON DELETE CASCADE,
  FOREIGN KEY ([credenciaisId]) REFERENCES credenciais([id]) ON DELETE CASCADE
)
GO