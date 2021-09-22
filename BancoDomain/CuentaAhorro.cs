using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoDomain
{
    public class CuentaAhorro
    {
        public string Numero { get; private set; } //Encapsulamiento
        public string Nombre { get; private set; }
        public decimal Saldo { get; private set; }
        public string Ciudad { get; private set; }
        private List<Movimiento> _movimientos;
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

        public CuentaAhorro(string numero, string nombre, string ciudad)
        {
            this.Numero = numero;
            this.Nombre = nombre;
            this.Ciudad = ciudad;
            _movimientos = new List<Movimiento>();
        }

        public string Consignar(decimal valorConsignacion, DateTime fecha, string ciudadPerteneciente, string ciudadConsignacion)
        {
            if (valorConsignacion < 0)
            {
                return "El valor a consignar es incorrecto";
            }
            if (!_movimientos.Any() && valorConsignacion >= 50000)
            {
                _movimientos.Add(new Movimiento(cuentaAhorro: this, fecha: fecha, tipo: "Consignacion", valor: valorConsignacion));
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
                    _movimientos.Add(new Movimiento(cuentaAhorro: this, fecha: fecha, tipo: "Consignacion", valor: valorConsignacion));
                    Saldo += valorConsignacion-10000;
                }
                else
                {
                    _movimientos.Add(new Movimiento(cuentaAhorro: this, fecha: fecha, tipo: "Consignacion", valor: valorConsignacion));
                    Saldo += valorConsignacion;
                }
                return $"Su Nuevo Saldo es de {Saldo} pesos m/c";
            }
            throw new NotImplementedException();
        }
    }

    public class Movimiento
    {
        public Movimiento(CuentaAhorro cuentaAhorro, DateTime fecha, string tipo, decimal valor)
        {
            CuentaAhorro = cuentaAhorro;
            Fecha = fecha;
            Tipo = tipo;
            Valor = valor;
        }

        public CuentaAhorro CuentaAhorro { get; private set; }

        public DateTime Fecha { get; private set; }
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }
    }
}
