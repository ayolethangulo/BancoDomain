using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDomain
{
    public class CuentaCorriente
    {
        public string Numero { get; private set; }
        public string Nombre { get; private set; }
        public decimal Saldo { get; private set; }
        public decimal Sobregiro { get; private set; }
        public string Ciudad { get; private set; }
        private List<CuentaCorrienteMovimiento> _movimientos;
        public IReadOnlyCollection<CuentaCorrienteMovimiento> Movimientos => _movimientos.AsReadOnly();

        public CuentaCorriente(string numero, string nombre, decimal sobregiro, string ciudad)
        {
            this.Numero = numero;
            this.Nombre = nombre;
            this.Sobregiro = -sobregiro;
            this.Ciudad = ciudad;
            _movimientos = new List<CuentaCorrienteMovimiento>();
        }

        public string Consignar(decimal valorConsignacion, DateTime fecha)
        {
            if (!_movimientos.Any() && valorConsignacion < 100000)
            {
                return "El valor a consignar es incorrecto";
            }
            throw new NotImplementedException();
        }

        public string Retirar(decimal valorRetiro, DateTime fecha)
        {
            var saldoTemporal = Saldo - valorRetiro - valorRetiro * 4 / 1000;
            if (saldoTemporal > Sobregiro)
            {
                Saldo = saldoTemporal;
                _movimientos.Add(new CuentaCorrienteMovimiento(cuentaCorriente: this, fecha: fecha, tipo: "Retiro", valor: valorRetiro));
                return $"Su Nuevo Saldo es de {Saldo} pesos m/c";
            }
            throw new NotImplementedException();
        }
    }

    public class CuentaCorrienteMovimiento
    {
        public CuentaCorrienteMovimiento(CuentaCorriente cuentaCorriente, DateTime fecha, string tipo, decimal valor)
        {
            Cuenta = cuentaCorriente;
            Fecha = fecha;
            Tipo = tipo;
            Valor = valor;
        }

        public CuentaCorriente Cuenta { get; private set; }

        public DateTime Fecha { get; private set; }
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }
    }
}
