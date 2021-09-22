using BancoDomain.CuentaBancaria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoDomain
{
    public class CuentaAhorro: CuentaBancariaBase
    {

        public CuentaAhorro(string numero, string nombre, string ciudad): base(numero, nombre, ciudad)
        {

        }

        public string Consignar(decimal valorConsignacion, DateTime fecha, string ciudadPerteneciente, string ciudadConsignacion)
        {
            if (valorConsignacion < 0)
            {
                return "El valor a consignar es incorrecto";
            }
            if (!_movimientos.Any() && valorConsignacion >= 50000)
            {
                _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "Consignacion", valor: valorConsignacion));
                Saldo += valorConsignacion;
                return $"Su Nuevo Saldo es de {Saldo} pesos m/c";
            }
            else if (!_movimientos.Any() && valorConsignacion < 50000)
            {
                return $"El valor mínimo de la primera consignación debe ser de 50000 mil pesos. Su nuevo saldo es {Saldo} pesos";
            }
            if (_movimientos.Any())
            {
                if (ciudadPerteneciente != ciudadConsignacion)
                {
                    _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "Consignacion", valor: valorConsignacion));
                    Saldo += valorConsignacion-10000;
                }
                else
                {
                    _movimientos.Add(new Movimiento(cuentaBancaria: this, fecha: fecha, tipo: "Consignacion", valor: valorConsignacion));
                    Saldo += valorConsignacion;
                }
                return $"Su Nuevo Saldo es de {Saldo} pesos m/c";
            }
            throw new NotImplementedException();
        }

        public override string Retirar(decimal valorRetiro, DateTime fecha)
        {
            throw new NotImplementedException();
        }
    }

    
}
