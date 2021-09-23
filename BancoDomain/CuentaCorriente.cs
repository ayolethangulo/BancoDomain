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

        public override string Consignar(decimal valorConsignacion, DateTime fecha)
        {
            if (!_movimientos.Any() && valorConsignacion < 100000)
            {
                return "El valor a consignar es incorrecto";
            }
            else if(!_movimientos.Any() && valorConsignacion >= 100000)
            {
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "Consignacion", valor: valorConsignacion));
                Saldo += valorConsignacion;
                return $"Su Nuevo saldo es de {Saldo} pesos m/c";
            }
            throw new NotImplementedException();
        }

        public override string Retirar(decimal valorRetiro, DateTime fecha)
        {
            var cuatroXMil = valorRetiro * 4 / 1000;
            var saldoTemporal = Saldo - valorRetiro - cuatroXMil;
            if (saldoTemporal > Sobregiro)
            {
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "Retiro", valor: valorRetiro));
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "Impuesto4xMil", valor: cuatroXMil));
                Saldo = saldoTemporal;            
                return $"Su Nuevo Saldo es de {Saldo} pesos m/c";
            }
            throw new NotImplementedException();
        }
    }
}
