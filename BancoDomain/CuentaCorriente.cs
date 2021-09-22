using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoDomain.CuentaBancaria;

namespace BancoDomain
{
    public class CuentaCorriente : CuentaBancariaBase
    {
        public decimal Sobregiro { get; private set; }

        public CuentaCorriente(string numero, string nombre, decimal sobregiro, string ciudad): base(numero, nombre, ciudad)
        {
            this.Sobregiro = -sobregiro;
        }

        public override string Retirar(decimal valorRetiro, DateTime fecha)
        {
            var saldoTemporal = Saldo - valorRetiro - valorRetiro * 4 / 1000;
            if (saldoTemporal > Sobregiro)
            {
                Saldo = saldoTemporal;
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "Retiro", valor: valorRetiro));
                return $"Su Nuevo Saldo es de {Saldo} pesos m/c";
            }
            throw new NotImplementedException();
        }
    }
}
