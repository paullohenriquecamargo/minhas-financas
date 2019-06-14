CREATE TABLE contaspagar (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(80),
	valor DECIMAL(8,2),
	tipo VARCHAR(30),
	descricao VARCHAR(100),
	estatus VARCHAR(20)
);

INSERT INTO contaspagar (nome, valor) VALUES 
('Conta de agua', 12.0);

SELECT nome, valor FROM contaspagar;