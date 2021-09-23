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

        public virtual string Consignar(decimal valorConsignacion, DateTime fecha, string ciudadPerteneciente, string ciudadConsignacion)
        {
            if (valorConsignacion < 0)
            {
                return "El valor a consignar es incorrecto";
            }
            if (!_movimientos.Any() && valorConsignacion >= 50000)
            {
                AddMovimientoAumentarSaldo(valorConsignacion, fecha, "Consignacion");
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
                    AddMovimientoAumentarSaldo(valorConsignacion, fecha, "Consignacion");
                    AddMovimientoDisminuyeSaldo(10000, fecha, "Costo nacional");
                }
                else
                {
                    AddMovimientoAumentarSaldo(valorConsignacion, fecha, "Consignacion");
                }
                return $"Su Nuevo Saldo es de {Saldo} pesos m/c";
            }
            throw new NotImplementedException();
        }

        public override string Retirar(decimal valorRetiro, DateTime fecha)
        {
            if (valorRetiro > 0 && valorRetiro <= Saldo )
            {
                decimal saldoMinimo = Saldo - valorRetiro;
                if (saldoMinimo >= 20000)
                {
                    AddMovimientoDisminuyeSaldo(valorRetiro, fecha, "Retiro");
                    return $"El retiro ha sido exitoso, su nuevo saldo es de {Saldo} pesos m/c";
                }
                else
                {
                    return $"No puede retirar el monto. El saldo minimo de la cuenta debe ser de 20000 mil pesos m/c";
                }
            }
            else
            {
                return $"El retiro no puede ser negativo, ni puede exceder el saldo actual";
            }
            throw new NotImplementedException();
        }
    }

    
}
