using BancoDomain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDomainTest.CuentaAhorros
{
    public class CuentaAhorroTest
    {
        /*
        Escenario: Valor de consignación -1
        H1: COMO Cajero del banco QUIERO realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        Criterio de Aceptación:
        1.2 El valor de la consignación no puede ser menor o igual a 0.
        Dado
        El cliente tiene una cuenta de ahorro
        Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
        Cuando
        Va a consignar un valor menor o igual a cero
        Entonces
        El sistema presentará el mensaje. “El valor a consignar es incorrecto”
         */

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NoPuedeConsignarValorDeMenosUno()
        {
            #region Dado El cliente tiene una cuenta de ahorro Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);
            #endregion

            #region Cuando Va a consignar un valor menor o igual a cero
            decimal valorConsignacion = -1;
            string ciudadConsignacion = "Bogota";
            string respuesta = cuentaAhorro.Consignar(valorConsignacion, fecha: new DateTime(2020,3,1), ciudadPerteneciente,ciudadConsignacion);
            #endregion

            #region Entonces El sistema presentará el mensaje. “El valor a consignar es incorrecto”
            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
            #endregion
        }

        /*[Test]
         public void NoPuedeConsignarValorDeMenosUnoVersionSimple()
         {
             var cuentaAhorro = new CuentaAhorros(numero: "10001", nombre: "Cuenta Ejemplo");
             var respuesta = cuentaAhorro.Consignar(-1);
             Assert.AreEqual("El valor a consignar es incorrecto", respuesta);

         }*/

        /*
            Escenario: Consignación Inicial Correcta
            HU: COMO Usuario QUIERO realizar consignaciones a una cuenta de ahorro 
            para salvaguardar el dinero.
            Criterio de Aceptación:

            1.1 La consignación inicial debe ser mayor o igual a 50 mil pesos
            1.3 El valor de la consignación se le adicionará al valor del saldo aumentará
            
            Dado El cliente tiene una cuenta de ahorro
            Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
            Cuando Va a consignar el valor inicial de 50 mil pesos
            Entonces El sistema registrará la consignación
            AND presentará el mensaje. “Su Nuevo Saldo es de $50.000,00 pesos m/c”.
         */

        [Test]
        public void PuedeHacerConsignacionInicialCorrecta()
        {
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);
            decimal valorConsignacion = 50000;
            string ciudadConsignacion = "Bogota";
            string respuesta = cuentaAhorro.Consignar(valorConsignacion, fecha: new DateTime(2020,2,1), ciudadPerteneciente,ciudadConsignacion);
            Assert.AreEqual(1, cuentaAhorro.Movimientos.Count);
            Assert.AreEqual("Su Nuevo Saldo es de 50000 pesos m/c", respuesta);

        }

        /*
        Escenario: Consignación Inicial Incorrecta
        HU: COMO Usuario QUIERO realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        Criterio de Aceptación:
        1.1 La consignación inicial debe ser mayor o igual a 50 mil pesos
        Dado El cliente tiene una cuenta de ahorro con
        Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
        Cuando Va a consignar el valor inicial de $49.950 pesos
        Entonces El sistema no registrará la consignación
        AND presentará el mensaje. “El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos”.
         */

        [Test]
        public void NoPuedeHacerConsignacionInicialMenorACincuenta()
        {
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);
            decimal valorConsignacion = 49950;
            string ciudadConsignacion = "Bogota";
            string respuesta = cuentaAhorro.Consignar(valorConsignacion, fecha: new DateTime(2020, 3, 1), ciudadPerteneciente, ciudadConsignacion);
            Assert.AreEqual(0, cuentaAhorro.Movimientos.Count);
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de 50000 mil pesos. Su nuevo saldo es 0 pesos", respuesta);

        }

        /*
        Escenario: Consignación posterior a la inicial correcta
        HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        Criterio de Aceptación:

        1.3 El valor de la consignación se le adicionará al valor del saldo aumentará
        
        Dado El cliente tiene una cuenta de ahorro con un saldo de 30.000 //50.000
        Cuando Va a consignar el valor  de $49.950 pesos
        Entonces El sistema registrará la consignación
        AND presentará el mensaje. “Su Nuevo Saldo es de $79.950,00 pesos m/c”. //99950
         */

        [Test]
        public void ConsignacionPosteriorAInicialCorrecta()
        {
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);
            decimal valorConsignacionInicial = 50000;
            string ciudadConsignacion = "Bogota";
            cuentaAhorro.Consignar(valorConsignacionInicial, fecha: new DateTime(2020, 2, 1), ciudadPerteneciente, ciudadConsignacion);

            decimal consignacion = 49950;
            string respuesta = cuentaAhorro.Consignar(consignacion, fecha: new DateTime(2020, 2, 1), ciudadPerteneciente, ciudadConsignacion);

            Assert.AreEqual(2, cuentaAhorro.Movimientos.Count);
            Assert.AreEqual("Su Nuevo Saldo es de 99950 pesos m/c", respuesta);

        }

        /*
        Escenario: Consignación posterior a la inicial correcta
        HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro 
        para salvaguardar el dinero.
        Criterio de Aceptación:

        1.4 La consignación nacional (a una cuenta de otra ciudad) tendrá un 
        costo de $10 mil pesos.
        
        Dado El cliente tiene una cuenta de ahorro con un saldo de 30.000 // 50000
        perteneciente a una sucursal de la ciudad de Bogotá y se realizará una 
        consignación desde una sucursal de la Valledupar.
        Cuando Va a consignar el valor inicial de $49.950 pesos.
        Entonces El sistema registrará la consignación restando el valor a 
        consignar los 10 mil pesos.
        AND presentará el mensaje. “Su Nuevo Saldo es de $69.950,00 pesos m/c”. //89.950
         */

         [Test]
         public void ConsignacionPosteriorAInicialCorrectaNacional()
         {
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);
            decimal valorConsignacionInicial = 50000;
            string ciudadConsignacion = "Bogota";
            cuentaAhorro.Consignar(valorConsignacionInicial, fecha: new DateTime(2020, 2, 1), ciudadPerteneciente, ciudadConsignacion);

            decimal consignacion = 49950;
            ciudadConsignacion = "Valledupar";
             string respuesta = cuentaAhorro.Consignar(consignacion, fecha: new DateTime(2020, 2, 1), ciudadPerteneciente, ciudadConsignacion);

             Assert.AreEqual(2, cuentaAhorro.Movimientos.Count);
             Assert.AreEqual("Su Nuevo Saldo es de 89950 pesos m/c", respuesta);

         }
    }

   
}
