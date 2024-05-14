using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoModoConectado
{
    public class Product
    {
        public int ProductID { set; get; }
        public string ProductName { get; set; }
        public  double Unitprice { get; set; }
        public int UnitintStock { get; set; }
    }
}
