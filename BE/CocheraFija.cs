using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CocheraFija : Espacio
    {
        public int IdCocheraFija { get; set; }
        public float ValorMes {  get; set; }
        public new int IdEspacio { get; set; }
        public CocheraFija() { }
        public CocheraFija(int id)
        {
            IdCocheraFija = id;
        }
    }
}
