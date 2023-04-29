use dbclinica
CREATE TABLE `Users` (
	`ID` int NOT NULL AUTO_INCREMENT,
	`User` varchar(255) NOT NULL,
	`Rol` smallint NOT NULL,
	`Correo` varchar(255) NOT NULL,
	`Contraseña` varchar(255) NOT NULL,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `Roles` (
	`ID` smallint NOT NULL AUTO_INCREMENT,
	`Description` varchar(255) NOT NULL,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `Paciente` (
	`ID` int NOT NULL AUTO_INCREMENT,
	`PNombre` varchar(255) NOT NULL,
	`SNombre` varchar(255) NOT NULL,
	`PApellido` varchar(255) NOT NULL,
	`SApellido` varchar(255) NOT NULL,
	`Edad` int NOT NULL,
	`Telefono` int NOT NULL,
	`FechaDeNacimiento` DATETIME NOT NULL,
	`Email` varchar(255) NOT NULL,
	`Sexo` varchar(255) NOT NULL,
	`Nombre_Responsable` varchar(255) NOT NULL,
	`Tel_Responsable` int NOT NULL,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `Caso` (
	`ID` int NOT NULL,
	`FechaDeApertura` DATETIME NOT NULL,
	`UsuarioCrea` int NOT NULL,
	`IDPaciente` int NOT NULL,
	`Motivo_Consulta` TEXT(255) NOT NULL,
	`Antecedentes` TEXT(255) NOT NULL,
	`Diagnostico` TEXT(255) NOT NULL,
	`ReferidoPor` TEXT(255) NOT NULL,
	`Estado` varchar(255) NOT NULL,
	`FechaDeCierre` DATETIME NOT NULL,
	`MotivoDeCierre` TEXT(255) NOT NULL,
	PRIMARY KEY (`ID`)
);

CREATE TABLE `Consulta` (
	`ID` int NOT NULL AUTO_INCREMENT,
	`IDCaso` int NOT NULL,
	`FechaDeConsulta` DATETIME NOT NULL,
	`Datos_Subjetivos` TEXT(255) NOT NULL,
	`Datos_Objetivos` TEXT(255) NOT NULL,
	`Nuevos_Datos` TEXT(255) NOT NULL,
	`Plan_Terapuetico` TEXT(255) NOT NULL,
	`Estado` TEXT(255) NOT NULL,
	PRIMARY KEY (`ID`)
);

ALTER TABLE `Users` ADD CONSTRAINT `Users_fk0` FOREIGN KEY (`Rol`) REFERENCES `Roles`(`ID`);

ALTER TABLE `Caso` ADD CONSTRAINT `Caso_fk0` FOREIGN KEY (`UsuarioCrea`) REFERENCES `Users`(`ID`);

ALTER TABLE `Caso` ADD CONSTRAINT `Caso_fk1` FOREIGN KEY (`IDPaciente`) REFERENCES `Paciente`(`ID`);

ALTER TABLE `Consulta` ADD CONSTRAINT `Consulta_fk0` FOREIGN KEY (`IDCaso`) REFERENCES `Caso`(`ID`);