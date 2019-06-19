CREATE TABLE contaspagar (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(80),
	valor DECIMAL(8,2),
	tipo VARCHAR(30),
	descricao VARCHAR(100),
	estatus VARCHAR(20)
);

DROP TABLE contasreceber
CREATE TABLE contasreceber (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	valor DECIMAL(8,2),
	tipo VARCHAR(50),
	descricao VARCHAR(500),
	estatus VARCHAR(60)
);

INSERT INTO contasreceber(nome, valor) VALUES 
('Conta de agua', 12.0);

SELECT * FROM contasreceber;

DELETE FROM contasreceber WHERE valor > 1;