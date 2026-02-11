-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 11-02-2026 a las 19:12:39
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `ventas_db`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ciudades_tb`
--

CREATE TABLE `ciudades_tb` (
  `id_ciudad` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `id_pais` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `ciudades_tb`
--

INSERT INTO `ciudades_tb` (`id_ciudad`, `nombre`, `id_pais`) VALUES
(1, 'Kabul', 1),
(2, 'Tirana', 2),
(3, 'Berlín', 3),
(4, 'Andorra la Vella', 4),
(5, 'Luanda', 5),
(6, 'Riad', 6),
(7, 'Argel', 7),
(8, 'Buenos Aires', 8),
(9, 'Ereván', 9),
(10, 'Canberra', 10),
(11, 'Viena', 11),
(12, 'Bakú', 12),
(13, 'Nasáu', 13),
(14, 'Daca', 14),
(15, 'Bridgetown', 15),
(16, 'Manama', 16),
(17, 'Bruselas', 17),
(18, 'Belmopán', 18),
(19, 'Porto-Novo', 19),
(20, 'Minsk', 20),
(21, 'Sucre', 21),
(22, 'Sarajevo', 22),
(23, 'Gaborone', 23),
(24, 'Brasilia', 24),
(25, 'Bandar Seri Begawan', 25),
(26, 'Sofía', 26),
(27, 'Uagadugú', 27),
(28, 'Gitega', 28),
(29, 'Timbu', 29),
(30, 'Praia', 30),
(31, 'Nom Pen', 31),
(32, 'Yaundé', 32),
(33, 'Ottawa', 33),
(34, 'Doha', 34),
(35, 'Yamena', 35),
(36, 'Santiago', 36),
(37, 'Pekín', 37),
(38, 'Nicosia', 38),
(39, 'Bogotá', 39),
(40, 'Moroni', 40),
(41, 'Pionyang', 41),
(42, 'Seúl', 42),
(43, 'Yamusukro', 43),
(44, 'San José', 44),
(45, 'Zagreb', 45),
(46, 'La Habana', 46),
(47, 'Copenhague', 47),
(48, 'Roseau', 48),
(49, 'Quito', 49),
(50, 'El Cairo', 50),
(51, 'San Salvador', 51),
(52, 'Abu Dabi', 52),
(53, 'Asmara', 53),
(54, 'Bratislava', 54),
(55, 'Liubliana', 55),
(56, 'Madrid', 56),
(57, 'Washington D.C.', 57),
(58, 'Tallinn', 58),
(59, 'Mbabane', 59),
(60, 'Adís Abeba', 60),
(61, 'Manila', 61),
(62, 'Helsinki', 62),
(63, 'Suva', 63),
(64, 'París', 64),
(65, 'Libreville', 65),
(66, 'Banjul', 66),
(67, 'Tiflis', 67),
(68, 'Acra', 68),
(69, 'Saint George’s', 69),
(70, 'Atenas', 70),
(71, 'Ciudad de Guatemala', 71),
(72, 'Georgetown', 72),
(73, 'Conakri', 73),
(74, 'Bisáu', 74),
(75, 'Malabo', 75),
(76, 'Puerto Príncipe', 76),
(77, 'Tegucigalpa', 77),
(78, 'Budapest', 78),
(79, 'Nueva Delhi', 79),
(80, 'Yakarta', 80),
(81, 'Bagdad', 81),
(82, 'Teherán', 82),
(83, 'Dublín', 83),
(84, 'Reikiavik', 84),
(85, 'Majuro', 85),
(86, 'Honiara', 86),
(87, 'Jerusalén', 87),
(88, 'Roma', 88),
(89, 'Kingston', 89),
(90, 'Tokio', 90),
(91, 'Amán', 91),
(92, 'Astaná', 92),
(93, 'Nairobi', 93),
(94, 'Bishkek', 94),
(95, 'Tarawa', 95),
(96, 'Kuwait City', 96),
(97, 'Vientián', 97),
(98, 'Maseru', 98),
(99, 'Riga', 99),
(100, 'Beirut', 100),
(101, 'Monrovia', 101),
(102, 'Trípoli', 102),
(103, 'Vaduz', 103),
(104, 'Vilna', 104),
(105, 'Luxemburgo', 105),
(106, 'Antananarivo', 106),
(107, 'Kuala Lumpur', 107),
(108, 'Lilongüe', 108),
(109, 'Malé', 109),
(110, 'Bamako', 110),
(111, 'La Valeta', 111),
(112, 'Rabat', 112),
(113, 'Port Louis', 113),
(114, 'Nuakchot', 114),
(115, 'Ciudad de México', 115),
(116, 'Palikir', 116),
(117, 'Chisináu', 117),
(118, 'Mónaco', 118),
(119, 'Ulán Bator', 119),
(120, 'Podgorica', 120),
(121, 'Maputo', 121),
(122, 'Naipyidó', 122),
(123, 'Windhoek', 123),
(124, 'Yaren', 124),
(125, 'Katmandú', 125),
(126, 'Managua', 126),
(127, 'Niamey', 127),
(128, 'Abuya', 128),
(129, 'Oslo', 129),
(130, 'Wellington', 130),
(131, 'Mascate', 131),
(132, 'Ámsterdam', 132),
(133, 'Islamabad', 133),
(134, 'Melekeok', 134),
(135, 'Ciudad de Panamá', 135),
(136, 'Port Moresby', 136),
(137, 'Asunción', 137),
(138, 'Lima', 138),
(139, 'Varsovia', 139),
(140, 'Lisboa', 140),
(141, 'Londres', 141),
(142, 'Bangui', 142),
(143, 'Praga', 143),
(144, 'Brazzaville', 144),
(145, 'Kinshasa', 145),
(146, 'Santo Domingo', 146),
(147, 'Kigali', 147),
(148, 'Bucarest', 148),
(149, 'Moscú', 149),
(150, 'Apia', 150),
(151, 'Basseterre', 151),
(152, 'San Marino', 152),
(153, 'Kingstown', 153),
(154, 'Castries', 154),
(155, 'Santo Tomé', 155),
(156, 'Dakar', 156),
(157, 'Belgrado', 157),
(158, 'Victoria', 158),
(159, 'Freetown', 159),
(160, 'Singapur', 160),
(161, 'Damasco', 161),
(162, 'Mogadiscio', 162),
(163, 'Sri Jayawardenepura Kotte', 163),
(164, 'Pretoria', 164),
(165, 'Jartum', 165),
(166, 'Yuba', 166),
(167, 'Estocolmo', 167),
(168, 'Berna', 168),
(169, 'Paramaribo', 169),
(170, 'Bangkok', 170),
(171, 'Dodoma', 171),
(172, 'Dusambé', 172),
(173, 'Díli', 173),
(174, 'Lomé', 174),
(175, 'Nukualofa', 175),
(176, 'Puerto España', 176),
(177, 'Túnez', 177),
(178, 'Aşgabat', 178),
(179, 'Ankara', 179),
(180, 'Funafuti', 180),
(181, 'Kiev', 181),
(182, 'Kampala', 182),
(183, 'Montevideo', 183),
(184, 'Taskent', 184),
(185, 'Port Vila', 185),
(186, 'Ciudad del Vaticano', 186),
(187, 'Caracas', 187),
(188, 'Hanói', 188),
(189, 'Saná', 189),
(190, 'Yibuti', 190),
(191, 'Lusaka', 191),
(192, 'Harare', 192);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes_tb`
--

CREATE TABLE `clientes_tb` (
  `id_cliente` int(11) NOT NULL,
  `nombres` varchar(100) NOT NULL,
  `apellidos` varchar(100) NOT NULL,
  `tipo_documento` varchar(50) NOT NULL,
  `numero_documento` varchar(20) NOT NULL,
  `correo_electronico` varchar(200) NOT NULL,
  `id_pais` int(11) NOT NULL,
  `id_ciudad` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `clientes_tb`
--

INSERT INTO `clientes_tb` (`id_cliente`, `nombres`, `apellidos`, `tipo_documento`, `numero_documento`, `correo_electronico`, `id_pais`, `id_ciudad`) VALUES
(2, 'Michael', 'Sanchez Guerrero', 'Cédula', '402-2871667-2', 'Michael.s14070@gmail.com', 146, 146);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `facturas_tb`
--

CREATE TABLE `facturas_tb` (
  `id_factura` int(11) NOT NULL,
  `id_venta` int(11) NOT NULL,
  `id_cliente` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `numero_factura` varchar(30) NOT NULL,
  `fecha_emision` datetime NOT NULL,
  `rnc_empresa` varchar(30) NOT NULL,
  `nombre_empresa` varchar(50) NOT NULL,
  `direccion_empresa` varchar(100) NOT NULL,
  `ncf` varchar(20) NOT NULL,
  `tipo_comprobante_fiscal` varchar(20) NOT NULL,
  `estado` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `paises_tb`
--

CREATE TABLE `paises_tb` (
  `id_pais` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `paises_tb`
--

INSERT INTO `paises_tb` (`id_pais`, `nombre`) VALUES
(1, 'Afganistán'),
(2, 'Albania'),
(3, 'Alemania'),
(4, 'Andorra'),
(5, 'Angola'),
(6, 'Arabia Saudita'),
(7, 'Argelia'),
(8, 'Argentina'),
(9, 'Armenia'),
(10, 'Australia'),
(11, 'Austria'),
(12, 'Azerbaiyán'),
(13, 'Bahamas'),
(14, 'Bangladés'),
(15, 'Barbados'),
(16, 'Baréin'),
(17, 'Bélgica'),
(18, 'Belice'),
(19, 'Benín'),
(20, 'Bielorrusia'),
(21, 'Bolivia'),
(22, 'Bosnia y Herzegovina'),
(23, 'Botsuana'),
(24, 'Brasil'),
(25, 'Brunéi'),
(26, 'Bulgaria'),
(27, 'Burkina Faso'),
(28, 'Burundi'),
(29, 'Bután'),
(30, 'Cabo Verde'),
(31, 'Camboya'),
(32, 'Camerún'),
(33, 'Canadá'),
(34, 'Catar'),
(35, 'Chad'),
(36, 'Chile'),
(37, 'China'),
(38, 'Chipre'),
(39, 'Colombia'),
(40, 'Comoras'),
(41, 'Corea del Norte'),
(42, 'Corea del Sur'),
(43, 'Costa de Marfil'),
(44, 'Costa Rica'),
(45, 'Croacia'),
(46, 'Cuba'),
(47, 'Dinamarca'),
(48, 'Dominica'),
(49, 'Ecuador'),
(50, 'Egipto'),
(51, 'El Salvador'),
(52, 'Emiratos Árabes Unidos'),
(53, 'Eritrea'),
(54, 'Eslovaquia'),
(55, 'Eslovenia'),
(56, 'España'),
(57, 'Estados Unidos'),
(58, 'Estonia'),
(59, 'Esuatini'),
(60, 'Etiopía'),
(61, 'Filipinas'),
(62, 'Finlandia'),
(63, 'Fiyi'),
(64, 'Francia'),
(65, 'Gabón'),
(66, 'Gambia'),
(67, 'Georgia'),
(68, 'Ghana'),
(69, 'Granada'),
(70, 'Grecia'),
(71, 'Guatemala'),
(72, 'Guyana'),
(73, 'Guinea'),
(74, 'Guinea-Bisáu'),
(75, 'Guinea Ecuatorial'),
(76, 'Haití'),
(77, 'Honduras'),
(78, 'Hungría'),
(79, 'India'),
(80, 'Indonesia'),
(81, 'Irak'),
(82, 'Irán'),
(83, 'Irlanda'),
(84, 'Islandia'),
(85, 'Islas Marshall'),
(86, 'Islas Salomón'),
(87, 'Israel'),
(88, 'Italia'),
(89, 'Jamaica'),
(90, 'Japón'),
(91, 'Jordania'),
(92, 'Kazajistán'),
(93, 'Kenia'),
(94, 'Kirguistán'),
(95, 'Kiribati'),
(96, 'Kuwait'),
(97, 'Laos'),
(98, 'Lesoto'),
(99, 'Letonia'),
(100, 'Líbano'),
(101, 'Liberia'),
(102, 'Libia'),
(103, 'Liechtenstein'),
(104, 'Lituania'),
(105, 'Luxemburgo'),
(106, 'Madagascar'),
(107, 'Malasia'),
(108, 'Malaui'),
(109, 'Maldivas'),
(110, 'Malí'),
(111, 'Malta'),
(112, 'Marruecos'),
(113, 'Mauricio'),
(114, 'Mauritania'),
(115, 'México'),
(116, 'Micronesia'),
(117, 'Moldavia'),
(118, 'Mónaco'),
(119, 'Mongolia'),
(120, 'Montenegro'),
(121, 'Mozambique'),
(122, 'Myanmar'),
(123, 'Namibia'),
(124, 'Nauru'),
(125, 'Nepal'),
(126, 'Nicaragua'),
(127, 'Níger'),
(128, 'Nigeria'),
(129, 'Noruega'),
(130, 'Nueva Zelanda'),
(131, 'Omán'),
(132, 'Países Bajos'),
(133, 'Pakistán'),
(134, 'Palaos'),
(135, 'Panamá'),
(136, 'Papúa Nueva Guinea'),
(137, 'Paraguay'),
(138, 'Perú'),
(139, 'Polonia'),
(140, 'Portugal'),
(141, 'Reino Unido'),
(142, 'República Centroafricana'),
(143, 'República Checa'),
(144, 'República del Congo'),
(145, 'República Democrática del Congo'),
(146, 'República Dominicana'),
(147, 'Ruanda'),
(148, 'Rumanía'),
(149, 'Rusia'),
(150, 'Samoa'),
(151, 'San Cristóbal y Nieves'),
(152, 'San Marino'),
(153, 'San Vicente y las Granadinas'),
(154, 'Santa Lucía'),
(155, 'Santo Tomé y Príncipe'),
(156, 'Senegal'),
(157, 'Serbia'),
(158, 'Seychelles'),
(159, 'Sierra Leona'),
(160, 'Singapur'),
(161, 'Siria'),
(162, 'Somalia'),
(163, 'Sri Lanka'),
(164, 'Sudáfrica'),
(165, 'Sudán'),
(166, 'Sudán del Sur'),
(167, 'Suecia'),
(168, 'Suiza'),
(169, 'Surinam'),
(170, 'Tailandia'),
(171, 'Tanzania'),
(172, 'Tayikistán'),
(173, 'Timor Oriental'),
(174, 'Togo'),
(175, 'Tonga'),
(176, 'Trinidad y Tobago'),
(177, 'Túnez'),
(178, 'Turkmenistán'),
(179, 'Turquía'),
(180, 'Tuvalu'),
(181, 'Ucrania'),
(182, 'Uganda'),
(183, 'Uruguay'),
(184, 'Uzbekistán'),
(185, 'Vanuatu'),
(186, 'Vaticano'),
(187, 'Venezuela'),
(188, 'Vietnam'),
(189, 'Yemen'),
(190, 'Yibuti'),
(191, 'Zambia'),
(192, 'Zimbabue');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos_tb`
--

CREATE TABLE `productos_tb` (
  `id_producto` int(11) NOT NULL,
  `codigo_producto` varchar(100) NOT NULL,
  `nombre_producto` varchar(100) NOT NULL,
  `descripcion` text NOT NULL,
  `categoria` varchar(100) NOT NULL,
  `precio_compra` decimal(10,2) NOT NULL,
  `precio_venta` decimal(10,2) NOT NULL,
  `impuesto` decimal(5,2) DEFAULT NULL,
  `estado` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `productos_tb`
--

INSERT INTO `productos_tb` (`id_producto`, `codigo_producto`, `nombre_producto`, `descripcion`, `categoria`, `precio_compra`, `precio_venta`, `impuesto`, `estado`) VALUES
(1, 'PROD-01', 'MacBook Pro', 'Macbook Pro 2017 Usada', 'Electrónica', 20000.00, 40000.00, 18.00, 'Activo');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios_tb`
--

CREATE TABLE `usuarios_tb` (
  `id_usuarios` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellidos` varchar(50) NOT NULL,
  `tipo_documento` varchar(50) NOT NULL,
  `numero_documento` varchar(20) NOT NULL,
  `numero_telefono` varchar(20) NOT NULL,
  `numero_celular` varchar(20) NOT NULL,
  `username` varchar(50) NOT NULL,
  `clave` varchar(255) NOT NULL,
  `email` varchar(100) NOT NULL,
  `estado` varchar(20) NOT NULL,
  `rol` varchar(20) NOT NULL,
  `fecha_registro` timestamp NOT NULL DEFAULT current_timestamp(),
  `ultimo_acceso` datetime NOT NULL,
  `intentos_fallidos` int(11) NOT NULL,
  `bloqueado_hasta` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas_tb`
--

CREATE TABLE `ventas_tb` (
  `id_venta` int(11) NOT NULL,
  `fecha_venta` datetime NOT NULL,
  `id_cliente` int(11) NOT NULL,
  `subtotal` decimal(10,2) NOT NULL,
  `itbis` decimal(10,2) NOT NULL,
  `total` decimal(10,2) NOT NULL,
  `metodo_pago` varchar(30) NOT NULL,
  `tipo_comprobante` varchar(30) NOT NULL,
  `numero_comprobante` varchar(20) NOT NULL,
  `estado` varchar(20) NOT NULL,
  `descuento` decimal(10,2) DEFAULT 0.00,
  `tipo_descuento` varchar(20) DEFAULT 'Monto',
  `tipo_venta` varchar(20) DEFAULT 'Contado',
  `notas` varchar(500) DEFAULT '',
  `id_usuarios` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `ciudades_tb`
--
ALTER TABLE `ciudades_tb`
  ADD PRIMARY KEY (`id_ciudad`),
  ADD KEY `id_pais` (`id_pais`);

--
-- Indices de la tabla `clientes_tb`
--
ALTER TABLE `clientes_tb`
  ADD PRIMARY KEY (`id_cliente`),
  ADD KEY `id_pais` (`id_pais`),
  ADD KEY `id_ciudad` (`id_ciudad`);

--
-- Indices de la tabla `facturas_tb`
--
ALTER TABLE `facturas_tb`
  ADD PRIMARY KEY (`id_factura`),
  ADD UNIQUE KEY `Numero de la factura` (`numero_factura`),
  ADD KEY `id_cliente` (`id_cliente`),
  ADD KEY `id_producto` (`id_producto`),
  ADD KEY `id_venta` (`id_venta`);

--
-- Indices de la tabla `paises_tb`
--
ALTER TABLE `paises_tb`
  ADD PRIMARY KEY (`id_pais`);

--
-- Indices de la tabla `productos_tb`
--
ALTER TABLE `productos_tb`
  ADD PRIMARY KEY (`id_producto`),
  ADD UNIQUE KEY `codigo del producto` (`codigo_producto`);

--
-- Indices de la tabla `usuarios_tb`
--
ALTER TABLE `usuarios_tb`
  ADD PRIMARY KEY (`id_usuarios`);

--
-- Indices de la tabla `ventas_tb`
--
ALTER TABLE `ventas_tb`
  ADD PRIMARY KEY (`id_venta`),
  ADD KEY `ventas_tb_ibfk_1` (`id_cliente`),
  ADD KEY `id_usuarios` (`id_usuarios`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `ciudades_tb`
--
ALTER TABLE `ciudades_tb`
  MODIFY `id_ciudad` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=193;

--
-- AUTO_INCREMENT de la tabla `clientes_tb`
--
ALTER TABLE `clientes_tb`
  MODIFY `id_cliente` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `facturas_tb`
--
ALTER TABLE `facturas_tb`
  MODIFY `id_factura` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `paises_tb`
--
ALTER TABLE `paises_tb`
  MODIFY `id_pais` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=193;

--
-- AUTO_INCREMENT de la tabla `productos_tb`
--
ALTER TABLE `productos_tb`
  MODIFY `id_producto` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `usuarios_tb`
--
ALTER TABLE `usuarios_tb`
  MODIFY `id_usuarios` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `ventas_tb`
--
ALTER TABLE `ventas_tb`
  MODIFY `id_venta` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `ciudades_tb`
--
ALTER TABLE `ciudades_tb`
  ADD CONSTRAINT `ciudades_tb_ibfk_1` FOREIGN KEY (`id_pais`) REFERENCES `paises_tb` (`id_pais`);

--
-- Filtros para la tabla `clientes_tb`
--
ALTER TABLE `clientes_tb`
  ADD CONSTRAINT `clientes_tb_ibfk_1` FOREIGN KEY (`id_pais`) REFERENCES `paises_tb` (`id_pais`),
  ADD CONSTRAINT `clientes_tb_ibfk_2` FOREIGN KEY (`id_ciudad`) REFERENCES `ciudades_tb` (`id_ciudad`);

--
-- Filtros para la tabla `facturas_tb`
--
ALTER TABLE `facturas_tb`
  ADD CONSTRAINT `facturas_tb_ibfk_1` FOREIGN KEY (`id_cliente`) REFERENCES `clientes_tb` (`id_cliente`),
  ADD CONSTRAINT `facturas_tb_ibfk_2` FOREIGN KEY (`id_producto`) REFERENCES `productos_tb` (`id_producto`),
  ADD CONSTRAINT `facturas_tb_ibfk_3` FOREIGN KEY (`id_venta`) REFERENCES `ventas_tb` (`id_venta`);

--
-- Filtros para la tabla `ventas_tb`
--
ALTER TABLE `ventas_tb`
  ADD CONSTRAINT `ventas_tb_ibfk_1` FOREIGN KEY (`id_cliente`) REFERENCES `clientes_tb` (`id_cliente`),
  ADD CONSTRAINT `ventas_tb_ibfk_2` FOREIGN KEY (`id_usuarios`) REFERENCES `usuarios_tb` (`id_usuarios`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
