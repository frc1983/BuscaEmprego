begin transaction

CREATE DATABASE BuscaEmprego
GO
-- tables
-- Table: Empresa
CREATE TABLE Empresa (
    Id int NOT NULL IDENTITY(1,1),
    Endereco_Id int NOT NULL,
    CNPJ bigint NOT NULL,
	Email varchar(255) NOT NULL,
    Nome varchar(255) NOT NULL,
	Senha varchar(255) NOT NULL,
    CONSTRAINT PK_Id PRIMARY KEY (Id)
);

-- Table: Endereco
CREATE TABLE Endereco (
    Id int NOT NULL IDENTITY(1,1),
    Logradouro varchar(255) NOT NULL,
    Complemento varchar(255) NULL,
    CEP int NOT NULL,
    Tipo varchar(255) NULL,
    Telefone bigint NOT NULL,
    CONSTRAINT Endereco_pk PRIMARY KEY (Id)
);

-- Table: Perfil
CREATE TABLE Perfil (
    Id int NOT NULL IDENTITY(1,1),
    Nome varchar(255) NOT NULL,
    CONSTRAINT Perfil_pk PRIMARY KEY (Id)
);

-- Table: Tipo
CREATE TABLE Tipo (
    Id int NOT NULL IDENTITY(1,1),
    Tipo varchar(255) NOT NULL,
    CONSTRAINT Tipo_pk PRIMARY KEY (Id)
);

-- Table: Usuario
CREATE TABLE Usuario (
    Id int NOT NULL IDENTITY(1,1),
    Endereco_Id int NOT NULL,
    CPF bigint NOT NULL,
	Email varchar(255) NOT NULL,
    Nome varchar(255) NOT NULL,
	Senha varchar(255) NOT NULL,
    CONSTRAINT Usuario_pk PRIMARY KEY (Id)
);

-- Table: Usuario_Perfil
CREATE TABLE Usuario_Perfil (
    Perfil_Id int NOT NULL,
    Usuario_Id int NOT NULL,
    CONSTRAINT Usuario_Perfil_pk PRIMARY KEY (Perfil_Id,Usuario_Id)
);

-- Table: Vaga
CREATE TABLE Vaga (
    Id int NOT NULL IDENTITY(1,1),
    Empresa_Id int NOT NULL,
    Tipo_Id int NOT NULL,
    Descricao text NOT NULL,
    Salario decimal(6,2) NOT NULL,
    Beneficios text NOT NULL,
    Ativa bit NOT NULL,
    Data_Cadastro date NOT NULL,
    Data_Preenchimento_Vaga date NULL,
    Data_Cancelamento date NULL,
    Data_Ativacao date NULL,
    CONSTRAINT Vaga_pk PRIMARY KEY (Id)
);

-- Table: Vaga_Perfil
CREATE TABLE Vaga_Perfil (
    Vaga_Id int NOT NULL,
    Perfil_Id int NOT NULL,
    CONSTRAINT Vaga_Perfil_pk PRIMARY KEY (Vaga_Id,Perfil_Id)
);

-- Table: Vaga_Usuario
CREATE TABLE Vaga_Usuario (
    Vaga_Id int NOT NULL,
    Usuario_Id int NOT NULL,
    Aprovado bit NOT NULL DEFAULT 0,
    Data_Candidatura date NOT NULL,
    Data_Aprovacao date NOT NULL,
    CONSTRAINT Vaga_Usuario_pk PRIMARY KEY (Vaga_Id,Usuario_Id)
);

-- foreign keys
-- Reference: Empresa_Endereco (table: Empresa)
ALTER TABLE Empresa ADD CONSTRAINT Empresa_Endereco FOREIGN KEY (Endereco_Id)
    REFERENCES Endereco (Id)
    ON DELETE CASCADE;

-- Reference: Usuario_Endereco (table: Usuario)
ALTER TABLE Usuario ADD CONSTRAINT Usuario_Endereco FOREIGN KEY (Endereco_Id)
    REFERENCES Endereco (Id)
	ON DELETE CASCADE;

-- Reference: Usuario_Perfil_Perfil (table: Usuario_Perfil)
ALTER TABLE Usuario_Perfil ADD CONSTRAINT Usuario_Perfil_Perfil FOREIGN KEY (Perfil_Id)
    REFERENCES Perfil (Id);

-- Reference: Usuario_Perfil_Usuario (table: Usuario_Perfil)
ALTER TABLE Usuario_Perfil ADD CONSTRAINT Usuario_Perfil_Usuario FOREIGN KEY (Usuario_Id)
    REFERENCES Usuario (Id)
    ON DELETE CASCADE;

-- Reference: Vaga_Empresa (table: Vaga)
ALTER TABLE Vaga ADD CONSTRAINT Vaga_Empresa FOREIGN KEY (Empresa_Id)
    REFERENCES Empresa (Id);

-- Reference: Vaga_Perfil_Perfil (table: Vaga_Perfil)
ALTER TABLE Vaga_Perfil ADD CONSTRAINT Vaga_Perfil_Perfil FOREIGN KEY (Perfil_Id)
    REFERENCES Perfil (Id);

-- Reference: Vaga_Perfil_Vaga (table: Vaga_Perfil)
ALTER TABLE Vaga_Perfil ADD CONSTRAINT Vaga_Perfil_Vaga FOREIGN KEY (Vaga_Id)
    REFERENCES Vaga (Id)
    ON DELETE CASCADE;

-- Reference: Vaga_Usuario_Usuario (table: Vaga_Usuario)
ALTER TABLE Vaga_Usuario ADD CONSTRAINT Vaga_Usuario_Usuario FOREIGN KEY (Usuario_Id)
    REFERENCES Usuario (Id);

-- Reference: Vaga_Usuario_Vaga (table: Vaga_Usuario)
ALTER TABLE Vaga_Usuario ADD CONSTRAINT Vaga_Usuario_Vaga FOREIGN KEY (Vaga_Id)
    REFERENCES Vaga (Id);

-- Reference: Vaga_Vaga_Tipo (table: Vaga)
ALTER TABLE Vaga ADD CONSTRAINT Vaga_Vaga_Tipo FOREIGN KEY (Tipo_Id)
    REFERENCES Tipo (Id);

INSERT INTO Tipo (Tipo) VALUES ('Empresa')
INSERT INTO Tipo (Tipo) VALUES ('Estágio')

INSERT INTO Perfil (Nome) VALUES ('TI')
INSERT INTO Perfil (Nome) VALUES ('Serviços Gerais')
INSERT INTO Perfil (Nome) VALUES ('Engenharia Elétrica')
INSERT INTO Perfil (Nome) VALUES ('Telecomunicações')
INSERT INTO Perfil (Nome) VALUES ('Eletrônica')
-- End of file.
rollback
