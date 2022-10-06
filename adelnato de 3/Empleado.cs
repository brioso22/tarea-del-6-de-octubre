using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adelnato_de_3
{
    class Empleado
    {
        private string nombre;
        private int dui;
        private double salario;
        private double afp;


        public string Nombre {
            get { return nombre; }
            set { nombre = value; }

        }

        public int Dui {
            get { return dui; }
            set { dui = value; }
        }
        public double Salario {
            get { return salario; }
            set { salario = value; }
        
        }
        public double Afp {
            get { return afp; }
            set { afp = value; }
        }

        public double AFP(double salario) {
            double calAFP = salario * 0.0625;
            return calAFP;
        
        }
    }
}

