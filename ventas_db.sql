-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost
-- Tiempo de generación: 04-02-2026 a las 16:30:16
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
-- Estructura de tabla para la tabla `clientes_tb`
--

CREATE TABLE `clientes_tb` (
  `id_cliente` int(11) NOT NULL,
  `nombres` varchar(100) NOT NULL,
  `apellidos` varchar(100) NOT NULL,
  `tipo_documento` varchar(50) NOT NULL,
  `numero_documento` varchar(20) NOT NULL,
  `correo_electronico` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  `estado` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `clientes_tb`
--
ALTER TABLE `clientes_tb`
  ADD PRIMARY KEY (`id_cliente`);

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
-- Indices de la tabla `productos_tb`
--
ALTER TABLE `productos_tb`
  ADD PRIMARY KEY (`id_producto`),
  ADD UNIQUE KEY `codigo del producto` (`codigo_producto`);

--
-- Indices de la tabla `ventas_tb`
--
ALTER TABLE `ventas_tb`
  ADD PRIMARY KEY (`id_venta`),
  ADD KEY `ventas_tb_ibfk_1` (`id_cliente`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `clientes_tb`
--
ALTER TABLE `clientes_tb`
  MODIFY `id_cliente` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `facturas_tb`
--
ALTER TABLE `facturas_tb`
  MODIFY `id_factura` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `productos_tb`
--
ALTER TABLE `productos_tb`
  MODIFY `id_producto` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `ventas_tb`
--
ALTER TABLE `ventas_tb`
  MODIFY `id_venta` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

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
  ADD CONSTRAINT `ventas_tb_ibfk_1` FOREIGN KEY (`id_cliente`) REFERENCES `clientes_tb` (`id_cliente`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
