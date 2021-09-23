using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDomainTest.TarjetaDeCredito
{
    
    public class TarjetaDeCreditoTest
    {
        /*
       HU 5.
       Como Usuario quiero realizar consignaciones (abonos) a una 
       Tarjeta Crédito para abonar al saldo del servicio.
       Criterios de Aceptación:
       5.1 El valor a abono no puede ser menor o igual a 0.
       5.2 El abono podrá ser máximo el valor del saldo de la tarjeta de crédito.
       5.3 Al realizar un abono el cupo disponible aumentará con el mismo valor 
       que el valor del abono y reducirá de manera equivalente el saldo.
        */
        /*
         * Escenario: Valor de abono negativo
       HU 5. COMO Usuario QUIERO realizar consignaciones (abonos) a una 
       Tarjeta Crédito para abonar al saldo del servicio.
       Criterios de Aceptación:
       5.1 El valor a abono no puede ser menor o igual a 0.
        DADO El cliente tiene una Tarjeta de credito
            Número 10001, Nombre “Cuenta ejemplo”, con un preaprobado Cupo 2.000.000 de pesos 
        CUANDO Va a abonar un valor menor o igual a cero
        ENTONCES El sistema presentará el mensaje. “El valor a consignar es incorrecto”
        */

        [Test]
        public void NoPuedoAbonarMenosUno()
        {
            var tarjetaCredito = new TarjetaCredito(numero: "10001", nombre: "Cuenta Ejemplo", cupo: 2000000);

            decimal valorAbono = -1;
            string respuesta = tarjetaCredito.Consignar(valorAbono: valorAbono, fecha: new DateTime(2020, 2, 1));

            //Assert.AreEqual(0, tarjetaCredito.Movimientos.Count);
            Assert.AreEqual("El valor a consignar es incorrecto", respuesta);
        }

        /*
         * Escenario: Valor de abono correcto
       HU 5. COMO Usuario QUIERO realizar consignaciones (abonos) a una 
       Tarjeta Crédito para abonar al saldo del servicio.
       Criterios de Aceptación:
       5.2 El abono podrá ser máximo el valor del saldo de la tarjeta de crédito.
       5.3 Al realizar un abono el cupo disponible aumentará con el mismo valor 
       que el valor del abono y reducirá de manera equivalente el saldo.
        DADO El cliente tiene una Tarjeta de credito
            Número 10001, Nombre “Cuenta ejemplo”, con un preaprobado Cupo 2.000.000 de pesos
        y saldo de 500.000
        CUANDO Va a abonar 500.000
        ENTONCES El sistema presentará el mensaje. “Su saldo es de 0 y su cupo de 2.000.000”
        */
        [Test]
        public void PuedoHacerAbonoMaximo()
        {
            var tarjetaCredito = new TarjetaCredito(numero: "10001", nombre: "Cuenta Ejemplo", cupo: 1500000);

            decimal valorAbono = 500000;
            string respuesta = tarjetaCredito.Consignar(valorAbono: valorAbono, fecha: new DateTime(2020, 2, 1));

            //Assert.AreEqual(0, tarjetaCredito.Movimientos.Count);
            Assert.AreEqual("Su saldo es de 0 y su cupo de 2.000.000", respuesta);
        }
    }

    internal class TarjetaCredito
    {
        private string numero;
        private string nombre;
        private int cupo;

        public TarjetaCredito(string numero, string nombre, int cupo)
        {
            this.numero = numero;
            this.nombre = nombre;
            this.cupo = cupo;
        }

        internal string Consignar(decimal valorAbono, DateTime fecha)
        {
            if (valorAbono <= 0)
            {
                return "El valor a consignar es incorrecto";
            }
           
            throw new NotImplementedException();
        }
    }
}
