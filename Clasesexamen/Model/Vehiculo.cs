using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clasesexamen.Model
{
    public class Vehiculo
    {
        public Vehiculo()
        {
            TipoVehiculos = new HashSet<TipoVehiculo>();
        }

        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<TipoVehiculo> TipoVehiculos { get; set; }
    }
}
