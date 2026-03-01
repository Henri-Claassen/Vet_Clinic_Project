using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    internal interface ICalcInventory
    {
        void addStock(Medication med, int count);
        void removeStock(Medication med, int count);
        void displayStock();
    }//End of interface ICalcInventory
}//End of namespace
