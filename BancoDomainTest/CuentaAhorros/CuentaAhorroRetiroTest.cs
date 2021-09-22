using BancoDomain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDomainTest.CuentaAhorros
{
    public class CuentaAhorroRetiroTest
    {
        
        /*
        Escenario: Valor a retirar correcto
        HU 2.COMO Usuario QUIERO realizar retiros a una cuenta de ahorro 
        para obtener el dinero en efectivo

        Criterios de Aceptación:
        2.1 El valor a retirar se debe descontar del saldo de la cuenta.
        2.2 El saldo mínimo de la cuenta deberá ser de 20 mil pesos.

        Dado El cliente tiene una cuenta de ahorro
        Número 10001, Nombre “Cuenta ejemplo”, Saldo 50.000
        Cuando Va a retirar 30.000 mil pesos
        Entonces
        El sistema presentará el mensaje. 
        “El retiro a sido exitoso, su nuevo saldo es de 20000 pesos m/c”
         */

        [Test]
        public void PuedoHacerRetiroTreintaMilCorrecto()
        { 
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);

            decimal valorConsignacionInicial = 50000;
            string ciudadConsignacion = "Bogota";
            cuentaAhorro.Consignar(valorConsignacionInicial, fecha: new DateTime(2020, 2, 1), ciudadPerteneciente, ciudadConsignacion);

            decimal valorRetiro = 30000;
            string respuestaRetiro = cuentaAhorro.Retirar(valorRetiro, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(2, cuentaAhorro.Movimientos.Count);
            Assert.AreEqual("El retiro ha sido exitoso, su nuevo saldo es de 20000 pesos m/c", respuestaRetiro);

        }

        /*
        Escenario: Valor a retirar Incorrecto
        HU 2. COMO Usuario QUIERO realizar retiros a una cuenta de ahorro 
        para obtener el dinero en efectivo

        Criterios de Aceptación:
        2.1 El valor a retirar se debe descontar del saldo de la cuenta.
        2.2 El saldo mínimo de la cuenta deberá ser de 20 mil pesos.

        Dado El cliente tiene una cuenta de ahorro
        Número 10001, Nombre “Cuenta ejemplo”, Saldo 50.000
        Cuando Va a retirar 50.000 mil pesos
        Entonces
        El sistema presentará el mensaje. 
        “No puede retirar el monto. El saldo minimo de la cuenta debe ser de 20000 mil pesos m/c”
         */

        [Test]
        public void RetiroCincuentaMilIncorrecto()
        {
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);

            decimal valorConsignacionInicial = 50000;
            string ciudadConsignacion = "Bogota";
            cuentaAhorro.Consignar(valorConsignacionInicial, fecha: new DateTime(2020, 2, 1), ciudadPerteneciente, ciudadConsignacion);

            decimal valorRetiro = 50000;
            string respuestaRetiro = cuentaAhorro.Retirar(valorRetiro, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(1, cuentaAhorro.Movimientos.Count);
            Assert.AreEqual("No puede retirar el monto. El saldo minimo de la cuenta debe ser de 20000 mil pesos m/c", respuestaRetiro);

        }

        [Test]
        public void NoPuedoHacerRetiroMenosUno()
        {
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);

            decimal valorConsignacionInicial = 50000;
            string ciudadConsignacion = "Bogota";
            cuentaAhorro.Consignar(valorConsignacionInicial, fecha: new DateTime(2020, 2, 1), ciudadPerteneciente, ciudadConsignacion);

            decimal valorRetiro = -1;
            string respuestaRetiro = cuentaAhorro.Retirar(valorRetiro, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(1, cuentaAhorro.Movimientos.Count);
            Assert.AreEqual("El retiro no puede ser negativo, ni puede exceder el saldo actual", respuestaRetiro);

        }

        /*
        Escenario: Valor a retirar sin costo
        HU 2. COMO Usuario QUIERO realizar retiros a una cuenta de ahorro 
        para obtener el dinero en efectivo

        Criterios de Aceptación:
        2.3 Los primeros 3 retiros del mes no tendrán costo.

        Dado El cliente tiene una cuenta de ahorro
        Número 10001, Nombre “Cuenta ejemplo”, Saldo 50.000
        Cuando Va a retirar 30.000 mil pesos por primera vez en el mes
        Entonces
        El sistema presentará el mensaje. 
        “Retiro exitoso, los 3 primeros de cada mes no tendrán costo. Su nuevo saldo es de 20000 pesos m/c”
        

        [Test]
        public void PrimerRetiroDelMesSinCosto()
        {
            string ciudadPerteneciente = "Bogota";
            var cuentaAhorro = new CuentaAhorro(numero: "10001", nombre: "Cuenta Ejemplo", ciudadPerteneciente);

            decimal valorConsignacionInicial = 50000;
            string ciudadConsignacion = "Bogota";
            cuentaAhorro.Consignar(valorConsignacionInicial, fecha: new DateTime(2020, 2, 1), ciudadPerteneciente, ciudadConsignacion);

            decimal valorRetiro = 30000;
            string respuestaRetiro = cuentaAhorro.Retirar(valorRetiro, fecha: new DateTime(2020, 2, 1));

            Assert.AreEqual(2, cuentaAhorro.Movimientos.Count);
            Assert.AreEqual("El retiro ha sido exitoso, su nuevo saldo es de 20000 pesos m/c", respuestaRetiro);

        }*/
    }
}
