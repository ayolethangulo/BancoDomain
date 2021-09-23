using BancoDomain;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDomainTest.CuentaCorrientes
{
    
    public class CuentaCorrienteTest
    {
        /*
         Escenario: Consignacion inicial incorrecta
        HU 3. COMO Usuario QUIERO realizar consignaciones a una 
        cuenta corriente para salvaguardar el dinero.
        Criterios de Aceptación:
        3.1 La consignación inicial debe ser de mínimo 100 mil pesos.
        DADO 
        CUANDO va consignar un valor de 99.000
        ENTONCES El sistema presentará el mensaje. “El valor a consignar es incorrecto”
         */

        [Test]
        public void NoPuedoConsignarNoventaMilPesosIniciales()
        {
            var cuentaCorriente = new CuentaCorriente(numero: "10001", nombre: "Cuenta Ejemplo",sobregiro: 1000000, ciudad: "Bogota");

            decimal valorConsignacion = 90000;
            string respuesta = cuentaCorriente.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));
            
            Assert.AreEqual(0, cuentaCorriente.Movimientos.Count);
            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
        }

        /*
        HU 3.
        Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero.
        Criterios de Aceptación
        3.1 La consignación inicial debe ser de mínimo 100 mil pesos.
        3.2 El valor consignado debe ser adicionado al saldo de la cuenta.
         */
        [Test]
        public void PuedoHacerConsignacionCienMilPesosInicialesCorrecta()
        {
            #region DADO que el cliente tiene una nueva cuenta corriente
            var cuentaCorriente = new CuentaCorriente(numero: "10001", nombre: "Cuenta Ejemplo", sobregiro: 1000000, ciudad: "Bogota");
            #endregion

            #region CUANDO va consignar 100.000 pesos inicial
            decimal valorConsignacion = 100000;
            string respuesta = cuentaCorriente.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));
            #endregion

            #region ENTONCES el sistema presentará el mensaje. "Su Nuevo saldo es de $100.000,00 pesos m/c"
            Assert.AreEqual(1, cuentaCorriente.Movimientos.Count);
            Assert.AreEqual("Su Nuevo saldo es de 100000 pesos m/c", respuesta);
            #endregion

        }

        /*
         * Escenario: Retiros a una cuenta corriente
        HU 4.COMO Usuario QUIERO realizar retiros a una cuenta corriente para salvaguardar el dinero.
        Criterios de Aceptación:
        4.1 El valor a retirar se debe descontar del saldo de la cuenta.
        4.2 El saldo mínimo deberá ser mayor o igual al cupo de sobregiro.
        4.3 El retiro tendrá un costo del 4×Mil
         */

        [Test]
        public void PuedoRetirarCienMilIniciales4xMILSobregiro()
        {
            #region Dado el cliente tiene una cuenta corriente con un sobregiro permitido de 1.000.000
            var cuentaCorriente = new CuentaCorriente(numero: "10001", nombre: "Cuenta Ejemplo", sobregiro: 1000000, ciudad: "Bogota");
            #endregion

            #region Cuando retire 100.000 pesos
            decimal valorRetiro = 100000;
            string respuesta = cuentaCorriente.Retirar(valorRetiro: valorRetiro, fecha: new DateTime(2020, 2, 1));
            #endregion

            #region Entonces el sistema descontará el 4xmil de la transacción y el saldo de la cuenta será de 400
            cuentaCorriente.Movimientos.Count.Should().Be(2);

            var retiro = cuentaCorriente.Movimientos.FirstOrDefault(t => t.Tipo == "Retiro");
            retiro.Valor.Should().Be(100000);

            var cuatroXMil = cuentaCorriente.Movimientos.FirstOrDefault(t => t.Tipo == "Impuesto4xMil");
            cuatroXMil.Valor.Should().Be(400);

            respuesta.Should().Be("Su Nuevo Saldo es de -100400 pesos m/c");
            #endregion
        }
    }
}
