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
         HU 3.
        Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero.
        Criterios de Aceptación
        3.1 La consignación inicial debe ser de mínimo 100 mil pesos.
        3.2 El valor consignado debe ser adicionado al saldo de la cuenta.
         */
        [Test]
        public void NoPuedoConsignarNoventaMilPesosIniciales()
        {
            var cuentaCorriente = new CuentaCorriente(numero: "10001", nombre: "Cuenta Ejemplo");
            decimal valorConsignacion = 100000;
            string respuesta = cuentaCorriente.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1));
        }

    }

    internal class CuentaCorriente
    {
        public string Numero;
        private string Nombre;

        public CuentaCorriente(string numero, string nombre)
        {
            this.Numero = numero;
            this.Nombre = nombre;
        }

        internal string Consignar(decimal valorConsignacion, DateTime fecha)
        {
            throw new NotImplementedException();
        }
    }
}
