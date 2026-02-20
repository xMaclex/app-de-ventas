-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 20-02-2026 a las 21:56:49
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
  `estado` varchar(20) NOT NULL,
  `motivo_anulacion` varchar(500) DEFAULT '',
  `fecha_anulacion` datetime DEFAULT NULL,
  `monto_total` decimal(10,2) DEFAULT 0.00,
  `monto_itbis` decimal(10,2) DEFAULT 0.00,
  `id_usuarios` int(11) NOT NULL
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
-- Estructura de tabla para la tabla `roles_tb`
--

CREATE TABLE `roles_tb` (
  `id_roles` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `secuencias_ncf_tb`
--

CREATE TABLE `secuencias_ncf_tb` (
  `id_secuencia` int(11) NOT NULL,
  `tipo_comprobante` varchar(3) NOT NULL,
  `numero_inicial` bigint(20) NOT NULL,
  `numero_final` bigint(20) NOT NULL,
  `numero_actual` bigint(20) NOT NULL,
  `fecha_vencimiento` date NOT NULL,
  `activa` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `secuencias_ncf_tb`
--

INSERT INTO `secuencias_ncf_tb` (`id_secuencia`, `tipo_comprobante`, `numero_inicial`, `numero_final`, `numero_actual`, `fecha_vencimiento`, `activa`) VALUES
(1, 'B01', 1, 10000, 1, '2026-12-31', 1),
(2, 'B02', 1, 50000, 1, '2026-12-31', 1),
(3, 'B14', 1, 5000, 1, '2026-12-31', 1),
(4, 'B15', 1, 5000, 1, '2026-12-31', 1);

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
  `id_roles` int(11) NOT NULL,
  `fecha_registro` timestamp NOT NULL DEFAULT current_timestamp(),
  `ultimo_acceso` datetime DEFAULT current_timestamp(),
  `intentos_fallidos` int(11) NOT NULL,
  `bloqueado_hasta` datetime DEFAULT NULL,
  `foto_perfil` longblob DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios_tb`
--

INSERT INTO `usuarios_tb` (`id_usuarios`, `nombre`, `apellidos`, `tipo_documento`, `numero_documento`, `numero_telefono`, `numero_celular`, `username`, `clave`, `email`, `estado`, `id_roles`, `fecha_registro`, `ultimo_acceso`, `intentos_fallidos`, `bloqueado_hasta`, `foto_perfil`) VALUES
(12, 'Michael', 'Sanchez Guerrero', 'Cédula', '402-2871667-2', '809-361-0349', '8496575458', 'xMaclex', 'mH/4UGG/UlDcdSd7TjBXdBZ4nwfsD+D5Xtx+dg7l0rQ=', 'michael.s140701@gmail.com', 'Activo', 0, '2026-02-13 19:37:54', '2026-02-20 16:03:35', 0, NULL, 0x52494646b25c00005745425056503820a65c0000d0d0019d012a800280023e5126914623a221ada2b23991b00a09676e8f0e6e2839f075532d6e976cbbecae0ced71e213251e2a6404e3bba0079dffd8133dca7dc2e94da00f3fe7f7634ffc5ec7ffacef46f36fe680f9b7ef97a23e9c78cfd7afdcade66d49be6bf7cbf71fe13dacf6d3fb0ff51ff33d42ff28fe95fe6ff3478c5e723d423ddafa7ffb8fed1f96df07df89ff63fc87acdfbdff9cffb7ee03fac5ff13cb47c4ebf1dff2bf6cfe017fa1ff86fd9cf773ff13ff9ffb4f4c7fa1ffa8ffd9fea7e04ff99ff72ffa5fe1fb717a5bb0776119212ffefeac0b97c8fdb7db731dbc4cb2267ed3f9e6135dc34c27b7e0df76d31668129bfb2b1658099ba440da6e85b96f4ba791d36ecde9666aaa8d9151f8cdfdec1330de35953067f9392934b7e519283ef0d18f469b45c27bcffa40c896e36afff7c245e4e12e7002e716f48defd99b7b9ce1ffef63ef111cc8b4fabfae0d281fd8858b7fc902486da00f1b28a4e30879811a53196a223c4fdd8b8fa0eca3f97b8269668eee12651f642fa5c9dada305af5d2b5980ef0b07cdb8747256823f8211d2d97145ed9c4e3d9b74a3f00adc23e8a42dca6d1dd1509f0d5223fcd2b2f9fe1b8186c085b50152008c37f5ed8fa8aee712a3e6fc7e1155813f373b1ba10ddbec7f6d2cfe372d0f78f8b9aae96dacef81aac95ada1d1317549bb6d37157736247b30481befcf67c7cdce6cfafd21b5b906b3f47c6e90a80d845a85bcc61bb3f1d223da80b9333f606d8f0ad55da18b190eafc402e6481c69cdebfd8508d400d601b47d9b1075b3427347711c912255767142f9105a212c7f2a917d3fb2c1bf0d21a2e193cfbef40840ae6676d3d862285405abf77bb57eb51d33fd08f81ca04a06534cdfadd1fa707c2071dddd91ce7ea7e238f4b965331def232958d7866ed68a4d5a1fb7364b7c3f76695fa23c1dcb83f2a375330051a1c3ac3e9b929a64be4b65c3ca186fb507c52be51ef5b7cedebae2cd1aac37eff51cad659d40b43b6591055cc0d6af2e6edb973b3d4e752788733f24080b3ed79ef03e9db14b398e51c18e9fc5e011d9a7bb09684b87845778fc077f232d08152321f3ee5ecdcfca6bc2a224c999d608db8671b5f896b34a544d1a97be7d71808773e98a39143565a7810c8a23628387f0091210eef363583ade8a21508e8c27c2e6c38f2cbb6497190ec46b7a4fca49eda081cade6d90d148e131bd70d8184c09ef04b972dcab2c3592de1b136564badeb329c4dcc1ab7be3b059f91532cea0ba886a017453ae4c6f22843dff6df90216790a9883f98c16908f4a0ffae48bc6a02e8447adbcc91432138e9c72a1e800ec34765932b0d5dcedeb416b6e412dde16c99c2f503c9bb22b6592dfd2a11dac39996f8ca820af0911c204103aa3419cc968a611e65f39832213ee2f013e0dce773f9332663e303140dcb0eeb3e9ba6916cf8fb715510340ab2469257eff0f5938cb7668fc2618ea2a392a1f61956a906bd21d8c80f5a471c8addf2b1deb0d626193705ad84a7a4b64f70976afc7b30725ffd4cc3540bfb31a33be2b544c247e39feb91535f68a8817b1e575f7ceaffd73d7ce534e51bf3152fdc8fcb25d0785191be12d922cb6d08e2c2a073ae5ec479d53e507c4771bb95dfb16e5388a44c12b40337170cec3ab2c19acee28b5dd79b877c2adb8606d8a54d15a6f5c7f8e14d54c527294e41d4f88306b6c2c47e65f299044fd404fd014a09b2a6adc670498874e879f7b55d342ff2bfcf9e88c6ba44f467bf96799950a6e6b8a7c7c89a341af017b5023f04263442477cdaeaad2ecaeca6362b6d0a7112df6b1d0fbfe8f3fc2b864796d0491d8f9cdb9239ecdd6849a333d12a25af30a0283fed2bf66cb63ee7794ad1646448473c03e26816bfc6bf7da7827b205d84e0e0fbef70f7ebdd6781136c1fe10d90a4db1051fef49dcef2a6be47fdc118143d07a1d404f67013a6a63fdcc4ff0b7f58cdc7a7b23f74cd00781580b697421e9bba4b6a7a98ac3b899826f826b65e13f717e21658d45eb07fc1916cda2f6262f1b0ca0e8070b606c625680ce456416cfe00b95c05de0071d753d1d30f8b2f346d282eafe6bc2b7afb214e31bd841e890b291bb61a24e6390213e738939e798228938179ec518a82026fb6961eb6d09abb5a2f6c7bc4cd5c7731cb6edbf4da3b2d25409de4438ffb3c3e040e499b486f1e3918b5b35dae3219fe88af4199fd6bf0c6661d1d8eea5f7e3afa0983ad6255c4a456006e2528bea35d90a1da6b6ff15d73983efa5abcaead7f05058bdc80c8ccfb62a92c804361c58ab2789fff3840985117ea0d2fd9c5395a98e4d79ba14dc8b94960b49374164e097183ab537b6266e8237658ca8a787ff6a24c64155d5cc3c38e8e9a863837c060bf0a9314a9fc4727f424e5532323934cb41112881b2f4cc2389087df54c612b239a10f7be02d40ed824c44701d50884759302d8281f9dc50ed4a88c4e0f091eef0a85c1210aafffff1153b9d30e34faab90d343eaaddfb4859d8cfd277c25c96594eb297fc5129802acada5ab4707d486db2374f19f66cab171c5a9851458dd15cd5fbe8ab366daf73c3b9476d616701154d9b2499b32860ba2b8a1a02e1a8b24bd0785babc2c40c532234ebc05c21691dc658aa5cac299a6e9cdb25a640aef75aa60ebd7477450e45b876591eb502c0a6b3909a6563874b1a8833b87130cedc13a7490f22a78485d506b3857506221541d28c02f0005aef6ae6e414999b5eaebe43278dd533ccf35d88803d15947064e9d8405f0bc6e49abf26b511608738b9af6b1ec70ff65fb4fc9ebb06ad1d0a8727b1f212fe41df48950b09e477bbb61a1c74f8815ba99c9f9d87c1d35c3f2cd657c3c8f96b13a9461446ef1a21d32b5dff8153d01827fb073eec4268ba704595c789b8b78d1029aa15e4489a9d15ae37bc174b909e578cdea81cd2b54d4ce46b6a5916eab2e9e8167d48799ab88db7dd47c6302cb7c48f22f870aea9f726a4e898575e0387bb2a21fa5ca719302cc6ae5ad125852bd715c7920e290611616386c00e01c7ebfd50045bbee9c1fa74467d35b3b174c16fa6188e19596e9bfa4db40e1b4dc039f1a34965ab374fc35fb70a0f2bbf6264cac1cbc369ce35412ccf55c367ca06a5514cf52c17860ac90ab945dedc92c98220a74c61492e2ba030194b40512b99bb12b79dcf25fe97285ed89508d3c3b4c7a2158c04c421d74d3a412300c16beb4ef6888a79cb78504d962a79a9667c09bebbdd012fb47268bbeee08c7c5bf769cabf52c74e1ff8852c183195e41fd3cde38431b15acb51b46c4c22e348e68320090a2bb3e69496f4f55a3b5bd6fc7158d368a6942502d13c2a94e863fe185d0c74745217c6db1f1a4c22e2c67fd9663dadba98af543cb4d6e200abca9495e57ec3ea503bac7cd5ed73e567135e7f539455d6227121d8580c359eb6a755e4be08be6b4ef7f635758518f9a5964f1f6fc5b5f09e53205b1152f9d42f9063e00382db2b58c7549768b432e3b1933e531f0bdc0bd88499ab65eff23e78ed17728e69bba7e3a2a56a43c09e95932c76d4ff0f982a8c0a5bd7feac65ca98214246a59deb15520940362f06ae06651e534abf2ac66d7acf64b3e625e4329a4352769c6d80a64936d3f6f6178c5f8f882bd9a54847a7ab4ef082f81e9ee25534bc4a6737cc906697fe9f4ab0341902fa895c29cdf88f75553b97f21b851ac33cbf450bc9c19f5aee78817ce3170760ecd75a2cac63c29f2fce66241988548fcf282e811b5ff0450b413c7b887f871a87d26d870c8c4c60b22d9d86c87e1b2977bd1ecc8a31ef4b15e5b0469925b624ab2bd15a2715a58a20b7c1ca2518215af48bd5259933b84f903d25863aa56c30d53fc40036a9bac208bc247b72ad63db49303d3b385204b5f85f93ebbd54d2e959adfaf61a5ea08a79644f46904fa29cc87f45ed01bca4a76be08eaac778e63aff44ae3cc6cc23f9a97533a730a7180002d3797dec0378479ebf14909ef476a866bd93f7e60805e3616b6cc78102d770ae1c669d9bb8df84305803d071fea8c8171e40ac308c9208ed9fc545b7b723257a08ed0570d511e150d830b91fd5dfbaf135647ebb43935eff4c3b639c44998b755fa1892b74389fb3855c19de55d59ff6f3397c7fe5d8d1b4ab0f5794f15900dd16e91ba0b73b479666fd2490ce3f07d6d45e90027bb49fd2dce4b0f610d7b062d0b3cc88e0c0cd280d9151e552d5424548f16adb140f74818a0f9d3672544b6b39edf2b23a81b58516b1016a81272cf4ce9bc141571e28afacff6124e70f39c2a54c20a10902ea4c03c24a91afee061ee69ee44ce602feb8223bd7ea6509a7da29b3c7c01ae1dac28d80807d8ca6052e32b08c2e6550f4d3f16174dc88ff42365da155a5ded79ed3dadc208dc5b2e01191b16584418694a31fc5b62c55d84f50591cc883099c2aaab6309ef1730ad16d7c9c5648fa40264b57d104c02098cf9289013f159cc571b645e7105bbd2a5c29a9dee5d94dcb45b3702d75df703590f5ae82a1815fbd8aef20c6e756575a2be98d8f81770cfd190cfa3c8fc376759c5c0971a8b6a3d01f59afb513163a75f644a695c590c252e39e08d2ef3ee10f18c49a4b132355e108b2f231e49e4b041098c2919b79dd22724a3eb03a412b8902168dcad38d7accb9f3b3d2a6b31551244f29d32f102f7bb8312479d3b9dbe99014add2db78a16489f92442511c36c6bbfc8f097f328376d301c7bb96915e831dd3811584472f7e3dbcde03a93d91a0c66ca71d9fbc3f155b31ae762808633b66fd825d32437cdcf0e55fd74a3f64b5865464dd30e7ff414fc82de319952dc131d6784b840ecfc466ac4dab7cfe5bba3f4cf152ec1989960c7f958583fa0af59382a8ad9725d29d743a9de51a931ca17a9ae6d3379f4fa34382a3c54c6d16220bf504726696552279c4af5044c09b0c64f5fb272f62771c7ba684dd0ddffb06bfe535bf18daf426e188d736e28260fe13735b4f6deaa047d3d89d59bebb3e5f3925938b9ddfbf5ccd156f17abcee8a3efad4c3ffae265642824f2213481df1418402e0dda6b7e1a54cd6297f8e40fea777f9294ac0339f5133ecaf29f5bb96d6e68ce590d24b9628f20f0989e9799dff64fe258ee2a7d2a26ddcab4e10607a35f0bd0bb1ed184fdd30238a43b0650a82d928b62f49763a2313d5cfe33263e1c7db5d3adc4183de552242994b740a712c8000feee97bffeaf7aa50be90cc3a0a859d99fdb3b0f1e29853ae845cb41c9b7056b91b6c2d8907b8b3764e1ad552ada88496a16a338abc67d3234f7e49c76320c33d90490f06e475d5001c2684a63f31af5cc52fe2a088156b6d24a6b8164af62946c540fa572b03fd5837adb1c247d82dd59d5a9710bb10109babf57868ea8efce5fb83cfcff5caed8dba5134e23e4dc1d141f4d463b0d668b702e7001e61ba95f619d167250658fb5351432a342dd16c663fb2e2a8ad5fd8e42d0dff7839cee7c7b201589c47e4dea5ed98c9e19d7840b439f9001916599d8287d90d7114655166a2081573a891810d1e9475639749925c3442dbe8f3e779d1ed117ba7535f42e64b283194a749f985cbf018af1b69f1471a54479bc878cfa2ef2a7b20033fa4d1892f191c9d8ecd4d1828f30c0e9747945c7ebdb4aaace7c0160af75a333d5a976ed9da52b2e797d2d68372c88595014b652c786d164c0306195f3e327fa5f28061b89df06c1154c9cfd194781991b8e62c02704ef3c659bfc07161dffb22db9f894c4e1420d3ac700bd79abb95975b3cc681f1947ff56cb084d74be5e0d4ca3b7a808319d6a89e4ce8d99e48027f469d0735382e070260c9b9d9e97b803c89a4d45c1fa01a3e138370def3ddf593c828bdc2763c0b826492808d50a7d2ef0de01e61f11b7eeb1eb01ca0140f09899a261f004e857c6214c6679a18853b6eddeb886bebf06c1c481d437eb2c33dae33ac6a1957e35b06901a524bcbc29d1fe7bba8d1f880ca57d6c10737001c8c7375f00d6f26b740adfaa31d21098317cbf25e4b326a3e7c14d6e0ca6f3c27bc54e555ea792187e79ef6dd3b15d8ddfa8df08246321537669b6ed7f4505d7bdc0038b359837acf50eff1421085b2a1ccc3e67c13102df35f571ac8edd17ba18cba6f3b714b66a2226c4f45f2acfef915e2aa801d62fc67e5de376e743c8144b905574728abacaab86d3e9fc8a810fb8a0169e10b31b47e78de0eb6e74ee7c0f8da928bbae30fed880386f46f78160ae75dd22a19421e5309d716a8379201722bcc15c85fe82469cfbb74054a81eb38a1eb2ebf7004742ae35f1380ac1238d7054bef5a9640c3f8614f2c6b957419a92a725045296da5ad4706037c017bfc57ea65d95aa515c472b081c9c727b7ab06e4cfbe137c1a279e904ce18510969f8234faa56eb76fbe1708518808f50b5c611b15cbe5758582849613cec527a38eeee9e29464061bfe59d89c09970305de81c548c6ec4212727888be8e855b5c5ec7087ba36f17d88a4ddb3d15aa8c4d1dc7bba9cffe59ac79c7199bee444c2018f1c0b1b8dd47b2c356850e75c77daa1f25c4f756c16d3e12d4d729d62ce3f3885bccc34e7f8daa4cc2cc380ee96e7d44eb9562f897ddfe9a1a13e9c1515ad03a9a8a2cd91ea14df6e709b7e9ccc8b58f1f1e6e2f09b0391e6c33fc6337823cfb9679e75a005818b7e62757db3ff439193bc903e110f0c8ba84b23477717f9a0f10d54b6f323cdb8c8817e186b4bdc18580b8330896dd8c57b5f9684b3b27bb4b51ebd868b82089386958f3dbe3004d32a48bfcef0ace3a3af4cc025aea91776fab0addea7eaa04c8ce7389f621251083fa839b1ac4e620b35000c2d53f9ccda7b0334f6af6c6bafc87a0fbfb7b5473af8afeba712aed1dbfaf5ac7ae9864f528e4115c3c4b2bb134f390a7ea9d96dcfa53c7477e08a4f427c32da516bddcfa1110368ce94297a7a8d7b3b5ca56bac805fd3049385ad27e5b79f5fc2edbfccd140129708822d26eafbf5246d236727bb859f664a5b54d53f4a5a0072ae11b5bfde181fe573dcbbfc2e1af8b07d65d1a7c6c17e3a7cf63ad83c3aa60b8d477d5b500aa53a42061584ddd2f237f3c37f6f4914bd016924cbb1f962028fdcf6425bd8899f025237bc31986db2007e294ff1ef17d3b16e84f0967b016749ced91a1bc256a6a3dfc04f82784687692a32f868a7104320b9a143138d789f5a14ac225fd9343dc1ec783b30f222fdf2b095841a13abb8cbf661a05b925dc4833e79727c5ed593ed332c1eafd4c37d7e89d674d30b4fe6fc61b8e6f86d798d9508aad8ffbc5b2e8b2aaed77484e791d30a5d6230044ba579d02342bebbc98b4cda52662744d958cf1cf0a6773bbe8d6c7725846a879bba4f7523607f37ff7881cc4837449338f7b2dc65742e1b60d77fb171e8c015771b1999ee606363d062ea27bbed1cd68bb6b6c546df23cd05e3222bccedafc0cb16da7d8f12a2c77423d90a1debde2d1af382c5be8e7d3e2f30cec918687d9694955a3a1135cf093b5efeabe35a4d5633f484c0b0d0b59865902e3ad6a70740035cb8a9d646a72fec9be055ca709378b0435d1c8373fc13566d4964e6a4047379978d9a543cc133d5ec9944693ada41e1e07d17979ab77dec8648843b86c67b81a8ecf69e8e4d92bf6ea819099d056f497039be872ac92bd77983e81fe86ba19a6a2d920515aa023b385cefce9e50f38c6fe01ab3a148fc2ad8b4d921984fdaf156644b4e50e48a9fb0f573100d6cfd0f4f9367e6284e4a85aa1aef7abfe9fbc10fec7a22ee5021d9e57135281fe0232b201e95a74b87bcbbb385530495433b2c38daf1c4ae1b7b3003af340a6480d9ae3a70a648b39130bd18c21f38fad96f8a5df66593ed1e89f4b14bcf835c344d125cc79b306ae7dfc6217aae7072c2ad2dd67cefd6c33f4549a02f2f939a0b30bd605e4a4b79bf5960435b3d20e6e4ab9ba7f57132ec801929aab18ecd249ac0ad391a76a1f590ce7c83342e9fb71c9176ae930f9872bc4c02cbd193b2b5ab7c8bc8c2754aa5751a34d3865d644bd5dba343825eac4d35cefaa3524054ecdcce424e32e9a7111db3111e2c4e2448a51430a1df172168708b954fdc3b0198a9e160ba2720c09287a26bb5ef89a8f408a01decf83bc9a7164e7d8e1194526c20f9978f49c3d2fbfc358ac813224c78ce3bb99d727e66d8481ac3038a08bab7e767679c1751247688e56f5f1c0381ad034378efbfbd66f72dcd6136fb2bc8cece60ecd83fafc3087625ad2767c108d4f7254a666de7d6e6482bda77ffc9b90cd6cd275ef463d1c1646ad92942f7a49baf328a5d63f395986d8c7b51231c18c0361a7e6bc6fc0d3cff0be934c3ea53fe56796a74121aa10fedcffc5dbfaba806af91d51eaeb493edce0290127e06db40bcb9d9f6d6d39b40b944ce0612866ba29ca393d38404ea400f6ac7c125c432eb6c8b72571fd12c1bcbb71da738de9ed0ce2e57c5e649308a3840593954cd145284a86ce904f3495c543fea23c243b349b5b9bf77b0e42faa6e9c8f59c1394b62eef5d995689854814b49b549969147ecf4b2a33d299388622b97a987518649ccd9af781b9fcb4e7ac247f3165e67655b45171d9a2e22da640142403c770005feb5bfce6472e653f3b7e11cdae2ebc0aa7a536a168ea7611cbca513e2f93b788a9134e6705f59ac4e9a59a1256312c486a54f33350491e841256dc3a6df525f9d5c1033daa80c354eb7b1aa36fd485835ec0f44f3da50866d594e2a21e7c919a63a97dfc7606596ed8436ec2050b0a5908320211b0dae0c63eda846f955fa3d0bfd88279da902b868c69bfab2ef25fe1ea885a1825cf861e7086a7ad5132ff220ec31c4ff367f7221fcf6c42c90f0549d4cbe112d77328078323b94eec9592587cd23d8477b2a93a91b26a40c620bd4cf3fb8ffbefcd46fa0744f7db87a199132f8ea6aa6a9393a3027a2357e1b66085d317f31042aadf00826703734e5cdd51822805bae4d9ef8c5a90f4e3099c9a0de236322cd401b450e3668bbd64d58e0f4cee61ed4879af9ece5011d7420d46720818f06a3778d861c08a81963810cc6117d10170d0bf10d3aece6133008e1c2286a60a836f439946423fc9f7b84e4f2bc6081eef79ef6e5182bbd0d7d178765250af0276644f07df8b564b3f7cf0855365185c8b02ff495f2b61dbf448317c5e61cb78e982ed387aa6448fce545dea43a42f05d6740744e23b64b719b2dc5772d99288ad99823aa47d35b5e4aac249d73ec5447def72ff905c02ac1c0a4eef6a24c0faba7f9707e688acbb5f95dfb619dd94544640e6bc9be48039c688513fb538739594858623300076f62021f7c6b7e9728d3c47b9db431f498f2dfa083741d76dceda7ea4de2bda9ec9d8c0db038034786594dc7a5693cae626ba373a889895bceb44f21a27e0b2f952f9f6110720bd589cfddd8d7ec27ca881cc8ef8fb995098d1dfa8e009adadba17970815405122d2e5119bf0b488d7217ba8e0fea92d6c405284ad5d6c12e26ec2c7777d120004f92cbf7328aedba487ef3b9ede69591808086cfabcf184c32ae119d3bb1e91cc309e8fd7dda5bc1dcfdd825707dc6ef8f53677ca7cee568f1fa885a48e040a11db3c40ba5773452ee2d2ccb4bd23beb2142618082a6acdc7c89bfcce2ecbce440a092065747ace84e206ae51577e2c8a13c73f066a598691a84fae93b9cd5ac4d85ae3c1173422726fafb96b22ee8b87a04d25d05e00448f947bd4adf2fc5ed942be1a367607d20f920f18941e2b9b07edac06cd434504f527d92a74084402a78f47d1aec445d69ea84104edd194a2caaa6fe036ee79f5586ee4a1b8f830613fa600e40ededcb26fba62a02730b3a6debd7e7d54d77844507b5c1d7c49cfdb86bb0c852b4d919cc12dda8d4c178896ca7777a422446170a545c4da59fc680ef55ebb15efb5d8a9d2bd404dcec7974388134d6d3043734c981cd4e7ec83e1d080deabc18bf2c98494b077100b3e8fefe273569e940387cddc7914c52fa0574f6e58975803d14ec84f6ec62da0a41bfa310ee3dcda62c6c069d77e0763f5d76440e3c7f3145287bd6fa252a5233ed95bf24197c35cc097aaa2b5f05c1e3b17f97f02158401cb9ff392826575fe34a82bcd70978e9b62f5400fa14e0a592dbe0edeb6fe1f1983ffc92bcb5c99c3fb78a8c7a6c1b201f3c73f16ec40b3cf7ec7b6e3959dda6eb2b454d015b0c0371967fb73824f88cdde1631930a12ed689ebcfa3f9866291dda7eb5f6c1d527390c9daad0531e58bfbe64ddbd3bb325d6cd098e74a6df10a86b1ab1f1d8badc844673c3bec43dd56f47bbe96bf42e7873a5d25711c956cd9e84384853850f51dd30f47e2dfd53fe612ac1a746f61e0a8d80bfed778966648a94ffb575d75f667a0904867411bf2923ce7832c3ec5e8bac26041d84dadd555c3e455084b453879d38a62358b84f00afe29472d7491bb37cd6ca0f0bc75eb45d8f249c29cae137460dc424e7e37f6e372697c440c7353e034a8c89bd109503be4f681a18c240abfb08ecb53eb6f0f61f4f18c3b5ae6c97decf8ac08b282de5a8b281bec4d86b77b856655d0caa1c6c293d7908e7b1453de0c1bce22a6dcc50053669ae9605708544344c96509e9d4cd544eb02fa4ed256ba15e546addaa82b249d5f80a438f0fcd59b10d5a16d5a731bff9616cee12e066cd52971fd6c8647f016b2ac0a31b92990eb2583aebff8439e085925de6a677369ae074450dfdf603af5c9c1ec42ceed9f76c0f084b3af497cfdf316ad684596ad6f5b60c6f82c5ae76e89d4396d8ddeb300ba98117bc717d84203f05b87bb62f571291641d2db010997b8b8ee834c33805200977a93de99e737609f8415d5ee5177a361706ceb08c347dbdcb649db85eeac1dc305d44807810e4cb3916951bc090054797f7acc7452b6fd5a44494e81dadd9e56a7acc96a6ca002537d15417a276fd638bcc748dcd00f78acff2b7690b59f3d30186a7d74ab8fbb37e41a0294c4b46eeb1de9230d42ef70728a8c2862f487730acb1c1713f1840dc2fe706121656e2a9beeadbffc8a9e0a21a6d08db7d1e19f9b7ed04b2037b200f85caa00b5a4c5fd5b80c48971b45f56176c4242d7982a490d8af77830645ba3267034212354c8932334dfd33d67d77bb346c16f996917f3160e9228c67a4c56dccb4ec97fb7bfca8281a129600043b6414a485962322aa5106794aab9ddb523badb86ee4bf57fd9e431734e43c8a0d167c8befa69d96cfc0c45b08ba61f43166e308f25f9afd0e70fc6835afa82d48576aefc3e4e209bca29fe3ee942fa84f6ac3ef95bcf6bf1e7d41269749ba021c03e303b948a2cf97ed673383fbb8101032d6f4193ec5fdd61266969e8cc417a3dc33edeb11c3c6f3448a725ed8a33575bc796ef73c601012c9e179310fb8f2e4f8eacb053553730a575d6e2e25e8c62d548ecf3ca0c8c71e7511e760f91a04fb97667f4eee3866129a74f1fbac63e7676c639cd384435ac347aca238af2d12d1e139ddc1431dd9592414d36d243fae9fc94e1615a82df52b88105f9dd907b42818d669d942f6d40917b4ab40815e2be84cf6a57590fc4b98a1cbbd40aab998ae010652a01b27d6562f6457ec0bf81bdfdd29a0b70d2cb315baf60e71017f7dc469a4f2db2b7d3ecd006507e637c0ada6a16245928e86af575b79710788ba415cd6e005c26fe34b85cdc8d0007c1aac702a962683e54ae7c9eadc6100a0108515fad7bf21af6e4194fa0e8639332950dc9c955622341ed1e6831e8095b9162b025f458da24d7cc38ef437d5b928dc4fe96125f7a5e86f170bbb6b9ba19a95b62d091143e6d666b96f3bcb15006c68ba91f1b3c1356eef1880fd33040f92891ae6166ac63081dbae14acf57ca4ed5e55ff03ff51d43e9ab738228a83db62352caac15bf4bc0aed826b7f65e2d7bc8fcc7c0a2572c99353be68c54b3247e63bb4a96a39f27eac9f0b3f0b016faf22858f52353ab83c56356571ca4a7843641c07a2e17abd42e75c8bc872312402014c9a6aa88ae25feb2d320dd9b5f0156284fbdeb7b7eed1f4a51030e4dbc3098a4179cbda61a2d5c63edccb18863b61b79190327c9077c8baf2a179cc014c931d0a213cbfac16dd1010bc4861b3e8ae0d2949e07577de9210fd9b5460ce17a7443ee39fabf417567b300a7a19821a746deb66d3d830c1a11b4b925f6e1874d67f2421993d2b0c2dcef5a3a8bbb02464345d92d609c63a507091ef4ca7be57c7547d24467c3bb22e0022d7e2f6fb5b3b2904a934d65a18ca6012f638e1e832ab24f1499961bf45578b50c98da5aab193f72610fb3f1188ac073d631de21ac80c912b1cb98964605239608970adadd57f3cb55c06bc47217fa85b65abbc8062d8ae1d5655ee1e9ffb0333cf134b45660279e347d5dc812276cf477859f4004bea3e6da07d7e328ec2e3ecda67db6860179891c05c198641834cdd0d291c41f08f1964622efb2a396eef4d2305d223182b1075296b01fca8dde94a9dbf0cc4261af1b0a36ae32ca84d39d683e135b9e8445799b167cdfc52ce02a079fb8ea6f1df7d541b5ab4158f73d8994a4f581a9c3d8cd3923a7cb8a91e7c49ec26247d69645f96a4ccaa33b17da3ab3bd6e1189f8364b9154a0cf5f22b642db2295a461290b47b1c20c8fb2ca4a74d12f6a51fb427ca72be70496a8a3da1df67297b75ce6dfffa7c79f7c0571ada5d9c60da85a98e5fee7a8c11bf6bf1097e629e54d418973028b7c58eaf3311121e0554abc0ec41b99b9755b804a3a8653d993848871b314ef9ecb26af304eeceea1205e5e86c8216d60f80b44fa5966526c50845057a8ba6fba8240705ba10d69a8cf55fd4f90a463971b46929636bd1734bae2c1b64ff1cb2b251351db0879209a0148ada4a5374ffb4b264bb187d3c009ed9ec26f6c075e684dea94115bb7a0a7b69822897a3adaafe57930a46fbb34f22fc277574563f16704c03db1d226e297afd07a8a8a8f40b9e2059b26e422e1180fa2ad892ee07514abda52845a2d8b23ac65e2c676c8fd378b415e46a06fd41fd15cd9d1449c03cfe98ce308f4844434ca2239f471553a2f9eab8327a0d12dbe0776ee15dcc5c10accc7ff9ed728431815487b2a90c75fcde3f404fd57b533d2304dc25534c259bb52012b391000fa7312eb2311d043d623339d83dad164d72985b523569c84653aed0e0811768bf08339afdd6651bbd3ec52b85d487b0aac1493ebead769648cd104829f466e0bbd2cf5eb4477ea4951f919308c0034a2b74e53664df97ff5f6490c681bac648b35d87ae25c86d168bf6a47c4227854f1f753d429fa54766da79240c8ce7d38302d12bcae9dbf3fc82bd42ab50b78c33291d1093ac2b3e51e5a565ad98a80939d1b63f240142b1b2df2c9d4837ba4f5743d684f883295339aa174c5dbe0b9f6df7f9e6a3d85c3d9eac4468099aeb18481edd2d91d282e1bce497d370ad662d5d36a17d5a6a4cd35fe080c1bc1d3d356cafdd02426da1595ead82194ba53409113b61cd6797545fbbafd653551449fdc7725d37bb175c21c6fb4d6b2efdb28306ea20e0cb73d75422080b737ebd173daff9afebd947ee8781112c9efd15a0909d9d55c1ba91fbbacc44f66f560eefddf2b466d29cb1a056d0fc37645623a55b608a4d334f76e885bb0a8695cea639c7626383eb5668670f9efd0030435043ae60360b9017bc3fc32aabab0755305c59f87c632d5e39c946d292ac8db8463954f06e4e89e90b4ff3bafae48a3ccdcac04ce2a1e32194fa83aaa07da55febf582eb8420868de26af5e81dd97bfee368af85ecc77d30494207870dbdf9c9f432191b7329132b65f127267279bbb545e95cce106a3485becaf6789e5dd7074dd079af31da774993138219c41f5c8eef66cb50537a4c4112a7fe61e3961d441ae176aeb2d66e068ac59c40210ad8b34bfa3d6f12ee4edefe465615267a2f831efe32ec4e08d458056a8b0bbffd035d971a5cc6bc0bebb3fcf862472b7ec7082d98dda83295d04f2da94c05c8f321508fa4407d09b1054e6ae0c559ce506fcff43d42aeeca7bdb654929dadf265e508736691ffd89b6d4f8503533ca941ee37c52a36112a2fe626b2f43779154c5921b4cd6c8adac02fddfceb031a51d2f6b92baeb72761f17a8d537c69d369fb695ed293b80ddca6304b16eca902ddae20257eba6e40e01243910fca1ddc90b978d76b02ae1aac7c56b80d6ea913481fb69132079c72fc96579aa26eb764614f5ac03e9b175b97622f04753db3d503c82833386737cee2cabbbf69d9f778e9d60dab9e1a85ae04aecf32e5c003aa0f2177791f6805fda7e5074341111fe845b948343f13c54a1127f54e19a218817bd5ad0a24d9e1d122d6a582473bead859cfce2ca6ddd7b74883dd018ab115bb9433b18d301bb7c102935792c67565668c183fde9fd661b7855c5f9af150960d062dda6f4b55d8869eba604869b03501b71cb8ab34e1a728ed78f8ea4f34a69737cea121c56f56e25ef02856eb22addbe3be2cdf814ac4ee60257533179353d7312fd427c57d8c63b7aa95d364000680780bc9c01371b6d02db1d3dfa259a2c0f81e66fc86c9c7e715b1cad0e49d8d2b0650b61e831de4e4fe16777c961a389cdd813afd822fcae742bc2df846ab547fd4a63e09aff460208851ddd9c9857ff0edde8b218567340604cc3060d3e567439555322fb9c7b2122b90a787c450fb01d760cdf8f6acdd32aa7141dfaf054710684297a7f93f8754f3fbb7ee257fd6af2650e4c59765b5fc6ba88080f9d24ee2ac0033d953c00a6fccb0d7b4b271715d33f49d8e2f9f48dcd2b38df387e22673e46341cb8a6b9008d00c6c7401350f6a7d4d9e0b3beb9f10fb9034b8842019cb39b562045984e6bdb69fc3eaf0f2be0df09e87f6234d1cf17f5b742ce7df19404e43a29c40182db90a8adc28c3685c6a45794d469cb3cf146882b1f2fa9b6c42b0b76aef63fe3a8427e865c73133ceaa1a379f7d8de4132238b2803c96e0980bcddf6151a8ca09c3004ba45e1fa9e4ea7da954721412e24482700142bf9a437fe09bf62f2e85b41ec7797633b226acaea025b98a16d20c061f434f9bf823e7d062af1f93c961f15f31f992bae1c6c6f7b5d4fc9c46012a25a8a301b4f8d25f093e2b176c58a65c1b4b4b72e737a3f3d68f037d6d71b2006c70b9577460e550868e601b2ab6d8380acdf40859828ae7bc60b4aaf6c58003ea9266536cddd2b5ac6c48278435e71066f0a6c41b9d5c6608b6753196189282aad1e7b2ad7ff5d87f7d3738ace21ea8e636d1fee0aecd6b2f9b520130ea6b2f64e8e9a808634f4c8a80093342205002020cd4912c4cb0c3b0d17f47ffdfd91ac32bb8ad306ab03e54a90c0e4bfb1a29238bd7d104b07c4070936864399b6414ba24b57096a831511f0b607d1eeb98df3676dc01db47db9e35b2e4b36a504d96c65d678fda2afe17b5ceac32c677df08f3597516679d42ec5b1728c042e77da0793221522c1fb5784425a9b80a455a9c642f7827006d847b0094a3bf0cb7474c786a2f817ade82ce7f8477c96048e9582424da38670146505ad1f5bae3dd633cd64235739d21c74780dd35f3ddb0425ccf2cd755dde9c196886e57e9803fc1ac6e5edffee8e8600220839bcf79c072628a41fb2ad33d3af0bd13e47795e2b2594790b46a5ca7317a50255b9be457d0d0294af9cc659dce39b9436b91e7cc3da4405daa6da3303cd9e6450967128f2890ed6c2aaff408ccbd1c27ca3bda00ae6e1ffc9133ef75043655a80dce69516528923c7af0700f2ed339247713f26459bf029c89791e9a32f1d9e5d7487152eba6734327efc3f0bbe940b31586d1d199e11e1100c8aab53b719f226ce59ac6392c6deaa228a1d01b3d687b92a8077701c18b9febbe33bb1132fd982f9d9529657c29be3c53b8c632f6783219d602d963b4ccb706bf929b885b75fefbd8c66fbb7f370a92022b9f59446d4f336d23f15d5bd2f43dd442b3b5a26737b9313418878ba9dae83c32841bf6d5a6f1abd635f9856f0e4560a8bd2e7527f864030ca6179ad473a2cefd5c6615228334c4e79022bd149b6e70511871116ba0bab758d5d8ff9a9a1a4aaeba6d08a6bcd14536f3bb9c1be809e91a15347cb6f5444f873b8b42c8dd73c32dc6c21ea76e627fc5f55130991643c7c685ada862ea89b9153d478d683ef7e65fa6183647adac364821e07039f0dff241b766d0f5bd2525182001342ad770cdea4f3dc6809c2f6f3c610c27fefca98e6aa7a8bc176d0e4bb48d9b6056adfd137fd13c396f1f4c1b4b547a3bf6d338c895ac4654ec9eabf93c64cf28ba39bc5c663f327ce6933ec149378c130ceb2b1c35775805bd18808feb052f3ef9aa78ab453965cc6efdcd99f40467946b6213b751304eeb987dfdca499f460ded8389a9598d21ad13512bc2bdad6d4b9c76d310d200623b9666c0d9be8f83c562835598169e24778624601910b534b441667f574dffec8925aa7a384f52c8487b25499f32d56e2d28d21b7120076bbd67c0514c2561ec5f541c005b1a071409c09f685d5e9b4fcd291df329a14d36ce648a0e1fb2ef5ab837b93b78115bf51c34b01801b7bf26553b344c863c203f7b6ac184192ca4c2f49def99d66348ccf1a083af118ec540502fb0443d95f39a9259e22e975041edbd6b3f13a9e267b6c08864d2fcd43db5d8b9490c02f0da9fdfe9b108044661565deab1425e259bd28bb0f94906dbe7eb97e1d2ca649bacfe10db98db44dfd77f18e919151d09bc9f831649fe2b9b330286bf84de45b0d71e7228d08d011eb3f618cf890d5b0208e12ffa2f129dbe8b851269a14d678ed06def9ab0960875618a5f9e1df0c29c342d4936db71d2bab09f4d9da407ff4583754d1b3f121a10a488a46b7e63bce30eaafedb55e69375cadb1b6149bea3761f0cce7a8d42f42cbc3fcb148a61ca5d7dce1aa77a78e5c11cba6eb16375a8ba0156fbdcd432f4537dfb326599981b08b96e3d9ee1ce8272a9a2da0590f3343f3a3117b92749b6d4da8c944fc6c89e3db27bb851ec405304d19946599d467bc4056af072bd027c890794dac0e3e6af8af6bb34e8efd323065e398b7eb73335fe1b8d20cfbed955ceb5f7dddba541b447e82df6063c90150816e97335701c53c9ebbb0e521469b55faf000a5cf6a8811e00ef663d40ee00f84ed4c71a22cb04974922e6556292729c76075e4114d3707890095c688e6f072f661793c7eed2dcf6e3cf3caa345a712c55036c85f280fbd557d7068d5e13747f8614d282c2f0c20089fcd9116bfc3377daa5ed3aa202d62f4fb48d882eb42979513f4810869fa6a548ed4171db90fd25445fece762570d357bd122455ff4f0b69013c6a4ee819101ebabbf0d6beb9a821663eb57001823736e659fa672dc6bec965c6d6062776f40517ebba47c76c277afcbd68b14a5e7a0d37329e100a24ec72d1d37d8d6d4e4d8ca41c74e895142d0561744f3abc71ff5d268ed59398d5e3d5593feb297cdea203a375058bed5198e8f2af21f087e29d1049fc924d74476f7f4d783ad49ddedb2aa9414742857f783bccf587698848e84ebf02d59b70e37482e2748a7266e1b270c12bda87f14232fe19498bfd2b378a839b232cba295226e435ab783594ae8d54e08f9d88b3b4e084d61d5ad6b762c0adadc7044d666439e9879c406792022c9ff427577cc056b89e39b8c5fd28993cd377348ecbb84c40f459046d76ff2c346ed59d262361fca8698c84c3a0841a8518d873fdb2d3d5abccd197756a87be13a23c7b1bd2e4f2c5f1a9b7efe8dae772a70545be5a8aebd1e159a87812f04c74bbeca9ea4a00e3ffbbabc75003a9139736ac5a85ff93bf4714500f6d039fd87e40a99d1f54e0c13240cf5600e67c8f3b30036e4e0b0a9de69076f86b4cd6ab6bbca9daaa786d1505eda39094b3a522149a30eb423409af1375d23c760dc789c01479d92973294e070d4f4a77d2c419bc475bb1644c531e04b71508e4a062f67fb3939edd4b0ee5404ae62366b3b9af5de3f785ce176d09a4fa1941bf6207404b67ed81b17db6c5ff80c50eedf9efef1fbb97731b22aa6ef2f3274c07d92d065651ae8abf0bdafe0e62dc7ce512a45ea3490e630a798e436019f1366d92a6156185c8b872c4bdd3e26691ae16e99fffa72d04da9e78fa72914d3eeeead35452a25ff9992a8a654ea5c5245ea0282336c94e26717d83ea6e19acbf46f8cd934653b15acfecc541daff8d577ec19975247771644f3ec8b65b6f0e846d3010fad0c21929b707177434ee55ee44e242bb1a483ac062e7cc5bf87438d37bfaf6e4a64c0035b99580ec8b57b3732e6371daa4c8a5ab7ecef4578df8e58c725ae11f44c1c0ad4ca968cf150467ad85c63c601c4f843a09e6777bc1a79f78eb8ba406d813fceada50b7950e802e5fac21792ecd8882b9c51a63ebdb371bdfe20596dd1fb13654414e276e70a65db103070956069a06769a081e50a180f4aac897ee333150ae3a1bc388e8de7635b1e8b411d76616f344ab5f8408856b9524bf33e9b446df732470cb00447695d6e3abefadc784f86071a73f20deb9855821122ba0895aa6fcbc77b5c9386ab085f975250994112f775ddf96d148d631f87251939e0b012f1beb7504cd9d85b7e12a4033ff6e19d3aea13b0a84493f3010cee66b7b3717cb56857430ce25859744ace89a687cf5c265f4327ded7680baee419e603bcb36f2b8dddab7bc4be47cb52c76b60fdaf8e6aec3c159fe8a71e47efa6c5d29078f5a8ccb973558f4cc45b12920b068e7b96edc2e330957e1322d2be71ae3f4ba2b748692b13ec351307096d2b97109b051d41a55f2435ca73f1e37c7b2840fa6b0a4bf8b8d2d05d882e6bd98062e7cc780c27b45aaaad130c4e1c792d1dbc535dc48f28becef57e8c8d9a26fd33b1b357bff7e1bc736e8bdedaf8222cac3bc12cd83638f9bb9b3dbdf9ff46f2646348052eff61a809f4afef0ec6b472e9663cbe3d72560a03b68c8721239c3b5129a449db1b7e3b663f5ecefda9f1eac5957d9f45055bb8ce0390a821a285e425a297cb2744fc00219ceec3d43b4f2905ff773e25e231f48f32bcb67890f482c1ba55085a7799da1a4058df140c40b82f8b21b30984680dca184b053f69bbe07bb023bd95e04f1cc5c8d6d9dc40c95bd115756f5dbbc7518455c755ed3e8fd673ecae859a71d3c974e38da3e54e21dd22c6910291a3c679dcd318d2c5c8b218686414b06fe103667d04d4b8300d043fe9d2be37a753b24237874a8f81d82a1927806e1f8bb2c0fbfd30a0af1540f247aaa48e610be154bb6c1526c2e16cabddc9f5e7f46fb29b95da139d7e5c3e9979abfff4caaeab8d7680b4e1e9387051e94253f6ab70970665763bd412a51c60598492842ea17f172135f9da4245ba05853731a1c3bae670914ff6da64d213834369df7150f6bde9d4806c82ae2195ef684fd7ed3a8f0604e2a9c02bfd57bcdce6157c300a37def02fa75167f594a24e76fffe2c521b09448a901ae4e79f33e0560679e01051ef92a0e871aff3b6c047837ac14de8af8601f9f6f254a2a648fb8f082bed0a59098497166bc5563d7804ea7e04d18eccc3f7ace379cdcecbb6a85535b2da9cd5ee7a3a9996cf52a44eff80a312d4b689ee499217c1332f1c772aaaa8bc0b0f7f53bf3de0145740991a6d27bda944072961dd4e2010854c9764dc31045d4801bbcdee83d60a61c34558a7fb30588057cbbe3ffdf2cc89b6ec9943c974976d26236ec00200f28049b54fe68f78cc28f64bc8aac467cd6c12e2c04037a930494e04d2fa6c17d3f7e838cc7d7d8bec41b556908f42662c4a4879b75cd8a78b6e0159d47fc21fe1dc1039d333e7d17a69400b4d8e865b25ae5295132d9e2cd5560b57c8ca7eae057e45c7703f8368abd822074c57ac250a0a257dc684f517cc6434813c91497feb2f31e3433cf6c31d41b444558de034d1af59615d001df5724c8411726cc64f8cc70207db72684edb2519fd8a9438d72db88aaf19101d05af0db3119faf93739aeb49e8bd9e29a20d63d171dbe14ec13e657e9b1c225422a281508ab718b01be7bbcfc3a149c1760c8d283e0033c5be529692a667a7df4311abf7ce5aacee7266727d1e798fd04b70db3186f7fca93793f2abbe5a835072a40cf3d66989cfcf51d610b2d393e4009f777fa3bc95029f16afd1421220cba9062ba49f968ba17b14d614ce4295732e9d1261ef98ebb3a537832cd7ae63f8bcb8f02d8337f29b946fc9494ced621f8d9a89d606db5debb3630ce74bd2ac56aef87273bff2e8bf06ef478c59d10896ee272a28e761c7e879e32b0ab0ea7802ac17caa8e38a0a5da1bc5b95f87d55624193e81a95ca4f66262a40337bad1046a32065be74d7986c2850056e55a804705420bb2a9399ece8d81f29fce4cbc19a87d52f028a566a1851db476998badb8b4f9e39e769b37ec80cc4c54491f9037ee7afa32fa730eea0ce0c1c364149ab593e66b355ede689ee23067ca94d62c6f73578d116559aa63ac19f8d0766f3166b0269062d753243db9dcace405fdaa3be4cd0cc0fa42688c7e1cc3e0da94574c14a806e4ff726dbc206526d1421d13e3e9a8f5a7e9f7796292e881d0a40f87b2e2b717fbf3d9b2ec194c9a53fa54ca9ddab479ebf38fb2c90cea9a08cb17a700254455bee9827ff62c3a6237abfa8ac72278431ed43eabb484bdfe28253d4720dc6c6ff32c966a80f5be0e30ca8ccc4b01592f02644c1af1b23d5523f7521eb6456ee3f61d2d4ffdce49155118d0a0cf83b1ab0fb5effbb92066a7b1990855eee3e6801401a2a142b841291ac3195f3a92de7085114f40c7ea7ddfc70701577c1098a26dc85c1fdc4775daea201d2532452a3a5f00a45054b794510cd2bf275152a2d1a130143891d8a4845b5f1b3365fa1df9029f27179fec1101f718d6de55b2d5896382a696382b0ffe6e561f592c532080ae0609dfccd25e0d34f4b8c372e5009de80d6b923cad37c703d8491bab8714aafd3e0275d505cd7cf3641373c1ce9e0630e16d8a4d01ce6ecbe3c8f956c6e9cc72eb2d745d76db21af0f7397564c0b3b97fa0856dc2644edf11b817d647ce1c7608dfe6ec085ca775ef104b310e9c572d2094a9337a1f56cdd40f2e0d80fac082f540cf6af480f5e9d6bb072121c91abd1882aef8d1ef95661ef4efb6c4255ba23f5e49352088fb851f1dabc945b13e3a89f9c0cf0e6e04685958ae1d331da5bc21e644c50bf25da93c3d525a2f00bab46470ec2a7db01c9c0d97c6136499238dba08bde2b8c6445a357ea52c8bf37eb9187651646d33d56d5ec8f893a2a797c2d15a26bde745de28e2311f367e7086c73bd3c3e9d7ad554eb9efe0f404488d430954711aedb71947fdad3e501fbdeeba8d212689438e0760ace5994885194056b2312463463b249978b6aecb454b033bad11ec5bdc35f6deb6a4df8709d0fb91bbb5d949f7d9f2b3a1bf3244315638ec694335be88e89408717d8071cff3e7a528579915ccfb745dbea41d8de23f4ebee03337e0f8244fa61d43e297273577458ab2b87fc7e8f08d7df0819f2cb3c0f7b6c7a98063c779012d4215e985a8a1632dff2f65743f94c013f1e31a0a65715efd3bfd302dffbf9cd46bc6273a018dbbb3dea5d4dc89ae9bf63fdd2857c87e2c1ff9ee89e9bcddffc7ca7f56897ef3f27880e2dc57f82d358206bb525df234ef446014eab7c74b5c7145989f4140ad1f938211a47250581dd22e7b4d90452caa42ace93ae9fb9f10384d9a9a1fa2d75ffc14fba70b0c33cc298a717108d8d33e8f533ca5a71aee919f11858843cc5fe3057b3dbfa8971ee49dc7a200d40a5b65deb792e0786399fcc4b7cdb2fe1234c638ea4ea16ab4d54cb3c7971beab45ff1f29c6b5f89d3389fcb50c2bdd8a0cd4d676bd51d920db18f2da6663f520253e2fa411611a0edd06ea5fc660bb24daae6b09ace0a3075d4b1490587f94eed19799867532b1396cc53f831864eca1d3e507b3df101c4c81c6375ac9fcb2c9f1a433e76963c512d9e44ff7f201f9ec8b08e0d4ead34e18a3fd3ef84f8497cf3065b7f283a7bfb71ea2c495baf0015e219fd00e8021194cca82b44b2b62d978730e70663cc559c450b58cdcb943628b930021f928222c805e3c9ec10e25577420d99c504d7f9a384230e1af063e930fbaa707c28b6ff0d2688146e4ef4605adea9cab8f7d833c317a8c16ddbd93e5ba0eecf365f5a964958fcc152f7261c92269596d7a6c9b0c450e97e3618673a0026ce4595d4900cf9bfd56321937b0f478ddf2e3c3bc6dbf0d211e38fd72a02cdcbeaa5124e063239e42eb2223f7c3fbcc6fd5c4f3655342da74bafb2efbda126950f008c3d5ff04f6c15aa8c49630845cd2a5fad0209a14af6ee53c43d5181122e78b9bd1bae1d14f02dd1d1b90edda59c3a4e7ed711192debbc9d4c40fc4f0ef68726f8969e39a6d80ac81241ba800639f4669e622e85908ca09638b4a765e3b8071171c390b33a0e7beb767ea34af8918f6e66cee6eef64f69711037b8333dddb4f99b44c8f3ad099f7ebecbf00e70e5659183510deb27dd5cbd9bb91b878c589024d588a81dc38f19c60f495d5f8c822b77df9abbca1b195dc9204b2d26463c4f949ed554c99b7d45a23d25a3cb6f516b153d2f98823dafa5585315548e3428ea2d8adc07393a8c5e3cf2f2d568ff2ea7e2d9e3e605121e8bece06dff983e0574456039d1bdf89f9b1f25f3ba2c9fafb4f4f08d59760f9baabc388072d8e034140cfb2adb0c9b9a9d886933499e512313e0b70a6d0f7d6163b3051e32ead00e4be5d7f0d9628890929690f8d9e70592557f134380413ee1c7597736ea28cd183d9b49098a7e98c1d805efefbc2aecf524a36a02d7a3078c734682862f755d2116f95e984b1e28deec95f1443113776a681c54402664dbf9f5f1d5894e6bfaf4141f992d3e6a4d745ae6247c31fa0b17930803c1cccecdd366730832281b5f66f3f28b1d83fd0b095cb47b367952fe3b9bc1059ca7373d7d292fb4af47ca628f3acc23e5eccc4771b86167a9b6809446814a10caad10432123096b4c8cf649affcb6a567ff7ad68c0220580ebd565c5ff52d6f58c9e661193bc0849c29da99e0a63f4b0c0c2706b4fdd66ce8167f4d9f3f26eac9128cca0cc274ccc2dfda9b20fd984b8c0bfe81e3cf39b8a9578f33131cd38024b047d49798e6b4911e99562155db5cc18a72d70237d6c0af1a9d883acfe0186a5bd532e0752c9e876e54815d8f80ea5312d7bf39b57b8f323d46395eb594b31dc415ace739251676a098585f3bdc6d82c3cff2b25032b11a2c042e49f83acb084eb121144d4a34ed7a4ffcbd8ece7f0bed35082ae92ad2c1585da78c0c1927d79dfbdfc5da241560d603e4dfa97ef39f8993684f2af4f062cb1b8811dc31b4643dffd5ccafca95632d72de454db6d2832c5eb0a32a1edff09192c6995bf24d02b3eafc2ee89c747b095f9abbeb1246abec073017aaa7b70ee3e129acab5b3de6524f961b86b4abadd1a3f2956644529efd52c74dac560223de4a8c1de1bf72d784b2337c8009d4f4ee2e4acca38ae0b104b4f54dbd835d447e1154df50a39910480b39aba788875505e378e78f5c2d66dabd0c99a7d5ff0db2058ca9ff33522eace12f06270f5e2fd82872ae344f637b5125701b372f1810979467a76f319dec3b9b4e0f7b563231f8c7d035811a1e7d4130e821e012574e61d3ff7c61bf2ea0072cb42a3cda86e60573bea731f8d0c9c423deb9a2d98d563987438f54688e5fd7b9fdf096dbedc29b542e225a19cf682a1f737708d06d83c6000c4ff0037672c96ae2f4364cc59c1c290a21d1b6e02c9ea8e4e4237536aabde8c4cac465e3e013a40ee4bcd2ab36307e6f36baa019314d0a62d5261b02dccfe29acfd425e42bb63ef540d6bac514a81aa72cc0aaa80e439a13093867cb5a89777cffdfe7b421565a112f8cca1f2b1d41cbc488dc7b7cedd22d4a70e10d1021eff01201818356f8e7c643a72df5f20c42c49f9334321abb1dad29e0863473184e315fc0473cb783aec7f7e1cabfc064bc06065d2b72bd51df5cc33ed7970c9b06c0b07b614148c78a789586f43fe0f33b31b969a336b98586689798f4ea7d2bba0ff898edccecf9c871fd39d3f36df8dbc1b68be7a4edea7081c81f631ff652e7faa483238b109fe464ed83bb72551fb581325de68a6267a0a2a65219393a01f157887620761809fafef29d06d1f3befbd34e2a7631697364e2d39b8749b26b5dfb3aba1e50309f4957cc2f1f0110fc9d2375edd34e397a22eb5f648aca8d215cfcc10ca85e2c679ba73ccd491c24b791253adfdb27748bfd3f5e14c49c40c1dd306b113e3108bba3e49020513e3a78b8ac9ef1eb86bad7357c450ad1687d49a36c4439b8045c66d7c1f2d7802e2b18c01f26499402b6672cd169eeecaa12700544e48d178935edb0688c429c0f25c1d5e43d341f75cd67f1afcd903ddc03f998643b347d97729258f80b5b8ffb40e62e10f701c9108e5822a22c004dca96ca561fccdbb8d49da5731c02fc8eca1804e8eb0fbfc3d4766c378fd0443df8295c1abb219984f98a55ffef5901faf5a833549f4e3bc6ad4745a025421cfc35d3eeeb271a269a84934a7f851dfc00a7666dc3aa3e31c5deb265594fe05f21be49b1ac5a20d25a94ca0dd7298476f26114f57cd54123ec620919540f12057c3604e559fe776c7dc6ea26fae0add5ce847cc2f2fffe538d6a9c68de3e99982d15e7c09abdb6c00ec5691bae85a95b384a69a1d427bdccf35d57f5a5250ab9cb81311142a691e0c75e76183b49e23f3230001290609acdf119b4300d678a57eed17cbb52ebc74ea4c8de01fe53f5c0b7e516145c2478c5a972297869beb6fbf1e7c77b77313d2ab1678f0b74f10218d7dfbad9c112354a9732c8fc13bca9d888ad1cba6a8e4fea0caaa0ce5d22e1877eab48682bacc99705f3a761e92f5581c12d5a5022a42041b607194a5b0cc40e29ba52df8fedf375c92aa8de71bdf00cfaddf03d54347ce0fb3458604513e58f1d00dc89804fc17cd1d23b6f283a5d314ea596ddef4e7349085e72170978cbf8178dc42f47ada79fa10ec8f1148886137c31fcaa2e606db18d08f9ab7742cf32ab6084a2454bafadb78819df289f7ce2ff4779d47b0af47aeafe9cd1151c0681f128734590543c0b7d40389449f1020c108f7936bcb6cae16b5c713b1dd2055ca56d6bb0a7f064144016993c3cb1e01358aed9fa867f722c05e01f28407d8f0bf03e45e7b2d26d814aec5169921715a14804d886aff348c4b2acb3e2bb6b9a8811cf0f60293b5cd6cc59f9af1eb5005fccf81ac7d4e0d752be62c4eff4b64a8da6bf8e19d081c138137d50c8af18b4fc00f413a33ec2471a8f0bab1dfe7c4acb3a58342b06aafd2f616c9d87c4def9edeef3fb57e141775b7ccf0ffbd43313f442be229d07f09a650c80108657e20ecef5ef95fa9b66cd0cfbe982354d4f76059289b72f2fba07ed325253c4058069d82a01a96561db2dc07bb033d89d3b7c03afcace780fe8c51c28511737675b7bc5a7775f742fd056f85f09e38bf6959fbbadc3149827fdaa9402d0212b829873ae81d4f4cf1ee3d01c894425f25d4f920e1c0c4e2d6054952ecde94d83b423e22295b8fa9427a8fda742524052fef987a692fdbeb5f81051a44dafd1b1f825bbd8e235159bdc56ff87c87f381cd753cbbcea7440858f441dd0b774995ee61ef18240cadef18fef5b2fbc080b33786efef2b01c9ff1c33bacfc1e188206d80fc5041136f15dc527f174e1716a97a1ea8c32d8b4f121d3e41087fee517eb65a9c39cc1cb01083f021620c50ab650ae5554af14961964157f7ac4acfd70a2e238d8c9b54c0531a15cca1142072e3ae4b4c4dcf07b39175e0cf726aed5d128152a44f2a527799e3e3849aa72d74ada6df2ebf5186b2ec965cf5af9e6d18d7e544546e9923516ac6727cbcce11d991fbec172f1baeda86399e438596c572d18f59bc39bedb28bc44a7a8a31a0de14210eea274e84f35d0f69286f63f5533e92f0dceefc00be9bf257c4ddfd7f8f22e2574a327025cdad9ef0493ef3f1ac6dd34d8834168d38aa979c3a6b1ec0f722cdbed42269299547472c83b40c3a1e554d22775f3ecda33b052dc1dda075cf3a8bf40735892a8509ac1fde4297605610414acc4ba2cabf371d72e0c3ebe32bf3cb7d9c78e3134928b4011240708b4524e248ffb96c43323723fb6ca7bce925856473512b89a64571b8536a7d6eac238399629a45862348867c36f61945328207669fda9027a39928bdbde3f78046ba0bf1778ef2922fe04bb3a77f281524f42b3a09b7c7da5840dabef3adc8887351fc6804e7afe6b3e159fad94c4f90809bb07cf40a11036ea9485172ddf4b9a04d96a491adcdbc1fd3fb25444cc1fed7b6f33c4a34d016fcc2f30a848fbe062448c4e345310e6e42ad993b66cc485d0e7676ca5ae5540ae3880c4881d1986026650863d2982e593892540938e9a3a5eea0592ced6a861787daea701c6bf81153c52d2911fe3ac0fe00cbc082c38f96e2b7fdd3418daef773eba186379685ce9302e21b0b32e52ec1626ff2a37d4b16fbd8a7e095c170d9d7ba926275863e1e61a98cc3454b1c8e7ec95e56a21a17e718c3ab154210495cd551c22289663fa5629ad9ca256fbc57f4ac28f7725b3ca69280aee5a69cd2f0b2dd9631ee0aa3fd35c70078e1c534df5bc230f44c0b37ce93f28d258c3c43d43c0ff857f9b90b2528450d00982757a81d12100776200a6841c349185ff18929e252a48a4828748d7a1a563281e1b9f0cbcfca25128d2ccce11066571a537c9656360864b5790a0d3ece7118630102495ca4f90757702a3af74295af3adab5782c1fc646587d7c54bb4f4b79d8fd3ace7b7a2964d38256e3ea22d507d62f25b15e481b6f007f3efe7e543eed5f225757d2c694eef07738b08e8ce9f66b4c58c89f8f5851a689efdb6179233ddfe2d73b3cb6ae650a523644756c8f2e0146e3a2495b12994d00f5e4b338b49e0e9a14e827ddc27653c8d27fd28a27b5f09d45f7341ac8d6fe581762d7ee9081b1da38a35053f0a45c466cd02658fa1c42fa0be0307ad4105b0427985f1148b3be55b706004338f7dac39900e1615f1148da9e6fb863c2620325771ae779d167349ed3750da141efee61cf5fa7e9779d9a81e0387ee4fbb508a612075934fa2cfa7ce35a76817dc12df0226fcd48466f963100d98b85bce76cc90d421e8ae1559fe13e14e03190dd7fff5ace09486eb90aad6ee9dd8efc0e0ae15ca61964f889d16c1435d77d04a528b51525c195a425325813a72c22a314320b37797806067f2be10241bc14befd9d1fb32d05d35b139d0b5b3ac15fd719d09c899860a29d3469a30c636bf3c6a89e7b426de83a60818d1adc6bd1ef9fa9e36ce1eefa5c5265137944d3e243c6d140cd0f5b14fa076c0e76c4f405cd164e5741509ed9fe74dc40d22c763bfaf01409cc1a872a3739d225f4ea4ee5530de213096a3d249f5812d8878eccaf1344f32e28898115b40f46f01534f51c076baf9c24988bee2d1599b81d3e0158d0a4e2a908d36bfd3d2e85abf718fe00b57308a3d3fb45cf65f86481007a8253561357d2457331f4da5d472d3afc63c9bffd89b3dca247ab60beda091acb2bbda31b7ead61767debebc48e4b1fec8c71e1a0e5ac3644580bc5ee63ce1b7b7149ffd6dfa23f6dc3745c53bcd6d2689bed6585dc303320a6cf687d008483e82febd3b90f38ea76a56038f0c49d7c264b8bacdff321c497375fde2431a1d22016117203c3576022ac1edf3077349db58198e060fe64019c418d875fa09a9cb83bcb889bebf6f9d7fea4d3bb13f852f2018864096df209609e9a8e05592b561c848b3131388b59c1de25814fb706b8567f5aba6e79df895fc74ddac6cf0690f1c527b1199165e23d4de39c22fe33403eb2b2d1afea9c30ae744f49aec2099c0c584fb9dedb9ebbf2f01107495c3334db53c06231780fdd18226b79e31fb1562460bdbbb051e7692279a2a9aa57b4095dd2fc1f073b58f997a28e5130172a4d95ea69606b5fa9b34a0b37f79fe81c2431a689a697e806b916f48429a38b90aa597e1709c9f27656071d669ccd78625ee988a5e9a32e26e0413e06605cefe92f2d92fdabb3cfafa591060442fb3503eb9520a7f28d95c4bc99da4d956e725f20f9b6e03530f41859a886bde702d48e135c535d71c078a0143c005d2e0789dfa4fcdebbb47ab98c2f9fcc7e64853dbf0abe9d13a46a56d8b9ab32f76db67bb7869f65055b156306bc6fc2836d8ddd92138c6d0ab36fc8854f4797cd552ef6ea1dc62bfe665ab95629623e1ce35a73d9ef4fd13fbdc54cde0b8f9ee9c2aee3bb2aba4111122e71d30eed9325e6cfa3dc4bb6657f50e8bed795c04d4acdc1c3a462356c66e113f26a304a69751d21444e29a276cc25b53d56969549e488442d622979ec7e62f2425c42024c6934484cf6739dece7b541e2d635a8a7723b6b46cc83390db9bc3d7ad0dbce1ce243db9a3cdd881cf660d14633115d4e916136e289a78a11d331275e82e172db84303fad763eb3775684c8afb5684fe94b9a2bfb78001d035ecac9778b96a2f936779c2111d7b60ec7da65ee10014d72259bb04fe1367013108f7034255d9fb65e38fe35ddf3739e0f2611f4fb0a3d7bda837f8cb7900365e10bef6724180adc21c7bf1e8754fc49d988a5b95946255ee2a0a7777d0bddfe12aac7a55e96921aa1e74c141921d96298d87211ae9ee25a54e42d1d97369a3f1d57aa93dba307ee2512c44dcb16b8048d098cfa52671455cc64d30640f04a5c175c974f9c78dc9663ac3e1639b220e49042ab8d8a4023b47a837a2ce41a658f08812eca1c8fe50411d1b56da52562416467048aff33482d40d5678bef387a2f101e9906e3e067eea587ec89933b26211e88a6f3c90adda2fcef2af8caefc6f8e716d0abb32f3059e65f344be3d947dee93fce21b2a93e4b1ec5576b03a3148ee472743b9e72f8d4f4a452cfc8fd181e8dae7e14daa491d301fed4cdcd294e8240e7ba817bd692ff25d6998cbefbafb2571030fe6faf4ca3bbfe1577d7641684ed86c64f34511f4c016848fd9a36dd362c11e7f33fd4f9ce06b6b262fc2fbabba7d9a25cf0164f23fdbbb81c218aea144b09327691685dbf25d5a2d8d4abdd833220820fb6e1f4af9ed56c1b8df58b1e7463f057df07767ee46d18d540b8fbec53fab2922e795a70c4bac085f9999f1c387ff79c618c1ed346d492425decd62ff18af22df801c470773a06926e5c3f4870340d14b938c7bd551dd68189dc5b9d931089bf2b82e0d9ef283d26ebf057192f443e73489f5a34dbd19d749dfe2155d215feb60deac12a10593cf378e76562c41ba4c1f90b6e14e0e66b189ad13764f98d49405aed2f99051c1d9f83e78fde0396fd32df5d153e5fc85476fe3db7f894e53eb169735fece335a7420371ff3de466bf893328dff1bf11d8b3c125bc089b8cad412a9250a8ce438fbcb918aee8575677994e03fc75335bc27d0aa4240032f99577727a77a25153de09ab01d7cd34fa7044d2ea1ccd59d65baeb27f62ef751eeda6778f9750b386b534ce8408990cd8393eae27b78c271123f6dc4d7c8baa10cdb7a22c46f006eaed4e1072cfedcae20519135343e8502167187f5f14cdfc0abc18ae856a0d75924f25a889e9c8876c098a2621946c3abe85a68531622bb900f72d8ba29c2cf03d04c03311f06401031b4d764370245fa5e3d44f5d1fee53f89194267ba26540190bb9939ebe07cb3e61d61631ee149a7c1abe83850f88031b8a55cf05099974e160d329463a39363fe21b4763ee474471ffc85f312109d1a045e072d93425bd2ceca925bb52f0adbeb61ec65019eea3adfdd7a2647a47df811ee9ffb184427bca90bf556869731663629657bf3a79e3a1ddae7fd8abd272037ebb9f4ecb9324e5cfd5645c0c95317195c50cadee6c6cd82ef7142645927587d6e0266cb1b3e8b8587806981ad17b9fa511f88ebeccd475109da7fe3b4d2f84432c7a70bed0db4c20ce56ebc5efc231c8c0375785732ccf58c0873092824b4207d94583b2189dbfd713d4887e4229b52f893239a394c780105561867a457dbed3b5ff7133c62d4338112fc2ea4690da583d3c2e1350df36089fab61a5f83ce683c2fbccf8a198a86582f0d9f83c7df0e4d625022a24ff66cd7565fe69bc8cf94ad4adb05956de7eb67c377d7c563069a229207e047ca6bd9eff174586d3276f7e9f2f967eb92aa08e0a4dfbfc01bc4e246438ab4abc8ece8cb8f943fb5e08388873605bdaa0a6a5a9bf19a0f1769fee39371e2802d517113a1229e896054499cbad02d240d09d0e3e1d3fdac31fd221dcf2b19f7ed5ec5f3b1a0e57e1e2842e3a04dd7fe4e44a955f34c5241103a0b76cc769f1c6cbfd0ad00ca8174ec00463b4021ebc28f819433c88d521f2595479378ca2c42218db563a96eda31a89ffd718b3bba56812c04fe0047f7513ae8daf0d2a214721eb6ec6414c3124e2bdeb7bb5beabb681661402e4dd997aa64308b05acfe984be75815c39f72ef4a5b9847b6f28a3e24a4ad8c9dfe997e83dad7de482c4c9107b706ceec7e0f771815cdd14c59110a5ba6740262435e3514aecd4d02a74deb3acb9059b490d5f318b1084d61d0ff16fa0068219dfae3308b1aedea69d000cd3b63df8f938f2f26fd228eebc37410023579082d16518f243205a72050db234efda1758ba912baca5271042d0d5ca20b903dbdc7faa8f1ee95820399bf0352fca26b366e7ff8d50d1bcfcceb9f567ccedf08fd99619f72d8041e4b0f5c0335efbb946a41a4f930808ae7778876266a4749e6def0949c7e1f07989e9fe74e11c17a24318f596f1f74d4fa4c821b5bb9ad68024b589f36b22ef858d8e38aa8eef032608191f46ab03b48f1ca235c24e028afeb5f51de4a379947cfd12b69df39fc6c5ec0f329ba445d5cb3e27d2c5ca3cddd7664d66f482ef37320001cc861a5c20a72a9232873f8a507512db4240c2ea25b0b2faf04bff604e9ddf9281b01049d225bbb98e968f0909eab69d6f0e5bb7c37720be14156b7665c120e0bbaa0510a4fea7adbf516ebf521b34ddaeecd3807097be27d9437bc04ef5bcb90eca9c1c02d03736409f7d78342180415babab0ea32173125729a635b135f5896b40f13abcaa94ca3860adde1d1d4107b503c5a87a56ffa5f1c2d1880cdc430132c2014a28e8c797ccf1e7cf693ac80d7f2587ad608fb87803ed66843d7211b8550818798909bcab10ac391bab01f618c69c5afa42610b921925d6e483015402d186c19a08b7adb842e449e731fc8c4bfada32e683072d921e38334a01b3b598bafae7de1c3ebd45108eac6f52983cb4b6cd785dfff15219b5efa93710dbade7ccc83b3ce8abc34d7ba21e4125e9f2614f334564f5b3db0b030b19808ee886c543c01c08406b25cdc462799d7e6c15ae48b04e95688b6e52e5e976a976c04472f7c736527c6f5a74fe8589b8d74355e7c7c945955fd241cd87b8efcdc4a4d61ba7c6a17679e185be3bde15a229fee8bea5e585d1268392e69f9fba276aee77c1751078e5ff50909570941dcc529481b09bb929297d8001383857083e260b63a7c2c5a38f6c8e864e212751b23b7371f35f942a21a3fa3dbf4d9c235ce6e435a7df3e4ae4fc00e862f8dcfb708bb4457a8890083fc7f5c7ba42547fddb1421c4d6bcdc4ce33a3c12fa78f93ff942452674dc9b342d3defed91bfa1b8757d20d29ca0123b6efe20014a67c4bed7646a061369327972c824c0ee6608fb8b40e85c4b695f986fdbbd5ed8e6859ebbd3da255b15befd668b43927353ce7d7c195111d9e37bcba9202d38322504dd7ca1e33f42f50ceb44ebcbf7fb22adbff9f7ead3922616eeb1b00a1ed3ecfe6435e3cc31dad6170cdfe4c97034db7448a51cf856b014f8396a137d866e04dea091faee8ac6dc0f9313dedb4c9560d0703a9efbb1187181922a92ba6a776d6e0423291553b71a1cfe31b84c4e450f0c78bc57df64ada5fab7b96006e99e061db40ce19c5abde51ceb5263579eea07d2399a1e8f2fca615e48c6d13b399e528f669da8ff93e2dedd8d6268945309bef071cf65469cbb84434a999ffa6114c41c2e35ea1beb581c12888eecfa1b642554851db8c1e8670a15a95be53e1496bc10654f926b8313a6730719a3e47563efb33abe0cee9c1588a66330debe4589b22f3de675722b9bd7fd8496332d21740eab0117ba596e226ff2714ef4123549a974531cfa80aaa4a2205c867409b9c622bea03b400e47bceca5a50a0a43cd4fe41ad62e54c60aa848b607d7f4a4ece7dd28c58d574c069c4a0947731ac9128ecd69b82c154bd30251375b638094f5192a869de207dfaf03aa0193619ce552c0200eb88fd19ee50c4604dd2807ba7c4bc30078e5abaf99af808c4f27307f85082a59ca919397c93d5bb2c2a05d7b0143f4243e3f0f3b5cc9d8a8f1d361d1586f734103ad78e27026f2636c46d77a55ad1d8ca823be3c5d9dab31ba995632d0fb44b164f7666476f4d066ced1545a82fd7f7dc86cd479384b6f1416c4180778d9a399f83c3cc77866957d050c94a98b20d5411c2c8363405c42f0299584a7a2519b2f18cc2ffcb5149d12083af7248e15fc5bf08f00e10732b04523388f9447a878fc3954e478ccb2632bc597b1b94240050f4a971968672da682144a1da75976346040229b2e4d85e4f6dcaba097803795dd11bc1ff7891c66a0e67fc1b5db9730e94e84955b94c498642d24ef46bb725f1ba65bdedcdd0d5aa579cd5938c30e7c236e29e2b8ab1bccc00e561199d33c38a456104887a5a93d7029c9cb0f384fbab98305070ded74ff9e811a5b37cbedbadea0e05d127c998e3975e5d43192ef4889800b0a2c3a9f351480f336493123345529174b063fdd8572c03c9c41c9b6d3376f5bfb1cb7b48f098cc0f4029b814097e6a746a3e9c9281c64a7880200e3fdb830e21d2f5bdc67e95dfa28346d118495d14094e319339d9999f7589eaaa12484081217dec90521f5720abb3b62813233a34e827c27cdc54ecd336b7adabeb0fd97cec7f282241fe9c541926d0324bb5b076587828ee90d9c03280744e39fca9af4aea90372b046e04a189be7e8c6bae762ec724b0cb3e072233919f68405d39276a2b171aa6043f7840458c2f597075de128212517ab525c72631a995522fef71843e8ec4aee29fe2ff5fe4ba125cb2e3b732b08d0d725479ca624015b8ba92f37dc470000000);

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
  ADD KEY `id_venta` (`id_venta`),
  ADD KEY `id_usuarios` (`id_usuarios`);

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
-- Indices de la tabla `roles_tb`
--
ALTER TABLE `roles_tb`
  ADD PRIMARY KEY (`id_roles`);

--
-- Indices de la tabla `secuencias_ncf_tb`
--
ALTER TABLE `secuencias_ncf_tb`
  ADD PRIMARY KEY (`id_secuencia`),
  ADD KEY `idx_tipo` (`tipo_comprobante`),
  ADD KEY `idx_activa` (`activa`);

--
-- Indices de la tabla `usuarios_tb`
--
ALTER TABLE `usuarios_tb`
  ADD PRIMARY KEY (`id_usuarios`),
  ADD KEY `id_roles` (`id_roles`);

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
-- AUTO_INCREMENT de la tabla `roles_tb`
--
ALTER TABLE `roles_tb`
  MODIFY `id_roles` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `secuencias_ncf_tb`
--
ALTER TABLE `secuencias_ncf_tb`
  MODIFY `id_secuencia` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usuarios_tb`
--
ALTER TABLE `usuarios_tb`
  MODIFY `id_usuarios` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

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
  ADD CONSTRAINT `facturas_tb_ibfk_3` FOREIGN KEY (`id_venta`) REFERENCES `ventas_tb` (`id_venta`),
  ADD CONSTRAINT `facturas_tb_ibfk_4` FOREIGN KEY (`id_usuarios`) REFERENCES `usuarios_tb` (`id_usuarios`);

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
