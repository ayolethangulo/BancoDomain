using System;
using System.Collections.Generic;
using System.Linq;

namespace BancoDomain.CuentaBancaria
{
    public abstract class CuentaBancariaBase
    {
        
        public string Numero { get; private set; }
        public string Nombre { get; private set; }
        public decimal Saldo { get; protected set; }
        public string Ciudad { get; private set; }

        protected List<Movimiento> _movimientos;
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

        public CuentaBancariaBase(string numero, string nombre, string ciudad)
        {
            Numero = numero;
            Nombre = nombre;
            Ciudad = ciudad;
            _movimientos = new List<Movimiento>();
        }

        public virtual string Consignar(decimal valorConsignacion, DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public abstract string Retirar(decimal valorRetiro, DateTime fecha);
    }
}