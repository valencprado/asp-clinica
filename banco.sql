create database bdClinica;
Use bdClinica;

create table tbTipoUsuario(
codTipoUsuario int primary key auto_increment,
usuario varchar(50)
);

create table tbLogin(
usuario varchar(50) primary key,
senha varchar(10),
codTipoUsuario int,
foreign key (codTipoUsuario) references tbTipoUsuario(codTipoUsuario)
);

insert into tbTipoUsuario values(default,'admin'),
(default,'comum');


insert into tbLogin values ('Rovilson','123456',1),
('Maria','123456',2);

select * from tbTipoUsuario;
select * from tblogin;

create table tbEspecialidade(
codEspecialidade int primary key auto_increment,
Especialidade varchar(50)
);


 

create table tbMedico(
codMedico int primary key auto_increment,
nomeMedico varchar(50),
codEspecialidade int,
foreign key(codEspecialidade) references tbEspecialidade(codEspecialidade)
);


 

create table tbPaciente(
codPac int primary key auto_increment,
nomePaciente varchar(50),
telPaciente varchar(50),
emailPaciente varchar(50)
);

