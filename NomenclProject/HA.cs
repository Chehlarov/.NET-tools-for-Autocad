using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomproject
{
    public class HA
    {

        static HA()
        {
        }

        //za presmqtane na dalginata, v cm
        public double dlepingle(Int16 ha)
        {
            switch (ha)
            {
                case 6: return 13;
                case 8: return 15;
                case 10: return 20;
                case 12: return 22;
                case 14: return 33;
                case 16: return 42;
                default: throw new System.NotSupportedException();
            }
        }
        public double dlcadre(Int16 ha)
        {
            switch (ha)
            {
                case 6: return 13;
                case 8: return 16;
                case 10: return 20;
                case 12: return 23;
                case 14: return 30;
                case 16: return 33;
                default: throw new System.NotSupportedException();
            }
        }
        public double dlcr45(Int16 ha)
        {
            switch (ha)
            {
                case 6: return 10;
                case 8: return 14;
                case 10: return 17;
                case 12: return 21;
                case 14: return 24;
                case 16: return 28;
                case 20: return 34;
                case 25: return 43;
                case 32: return 60;
                default: throw new System.NotSupportedException();
            }
        }
        public double dlcr90(Int16 ha)
        {
            switch (ha)
            {
                case 6: return 0;
                case 8: return 0;
                case 10: return -4;
                case 12: return -5;
                case 14: return -6;
                case 16: return -7;
                case 20: return -9;
                case 25: return -12;
                case 32: return -18;
                default: throw new System.NotSupportedException();
            }
        }

        //radiusi na ogyvane, v cm 
        public double rcadr(Int16 ha)
        {
            switch (ha)
            {
                case 6: return 1.5;
                case 8: return 1.5;
                case 10: return 2.0;
                case 12: return 2.5;
                case 14: return 3.5;
                case 16: return 5.0;
                default: throw new System.NotSupportedException();
            }
        }
        public double rancr(Int16 ha)
        {
            switch (ha)
            {
                case 6: return 3.5;
                case 8: return 3.5;
                case 10: return 5.0;
                case 12: return 5.0;
                case 14: return 7.5;
                case 16: return 7.5;
                case 20: return 10;
                case 25: return 12.5;
                case 32: return 15;
                case 40: return 20;
                default: throw new System.NotSupportedException();
            }
        }
        public double rcoud(Int16 ha)
        {
            switch (ha)
            {
                case 6: return 3.5;
                case 8: return 3.5;
                case 10: return 7.5;
                case 12: return 10.0;
                case 14: return 10.0;
                case 16: return 12.5;
                case 20: return 15.0;
                case 25: return 20.0;
                case 32: return 25.0;
                case 40: return 25.0;
                default: throw new System.NotSupportedException();
            }
        }

        //teglo
        public double linweight(Int16 ha)
        {
            switch (ha)
            {
                case 6: return 0.222;
                case 8: return 0.395;
                case 10: return 0.617;
                case 12: return 0.888;
                case 14: return 1.208;
                case 16: return 1.578;
                case 20: return 2.466;
                case 25: return 3.854;
                case 32: return 6.313;
                case 40: return 9.864;
                default: throw new System.NotSupportedException();
            }
        }
    }
}
