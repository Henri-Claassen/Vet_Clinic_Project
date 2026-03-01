using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PRG_281_Project
{
    internal class Medication : ICalcInventory
    {
        private string medName;
        private string medCode;
        private double dosagePerKG;
        private string medDesc;
        private int medsInStock;

        public static List<Medication> medicineList = new List<Medication>()
              {
                new Medication("Penicillin","ABC123",10,"Used for bacterial infections",10)
              };

        public delegate void PopupMessageEventHandler(string itemID);
        public event PopupMessageEventHandler PopupMessage;

        public Medication()
        {

        }
        public Medication(string medName, string medCode, double dosagePerKG, string medDesc, int medsInStock)
        {
            this.medName = medName;
            this.medCode = medCode;
            this.dosagePerKG = dosagePerKG;
            this.medDesc = medDesc;
            this.medsInStock = medsInStock;
        }

        public List<Medication> MedicineList
        {
            get {  return medicineList; }
            set { medicineList = value; }
        }

        public string MedName
        {
            get { return medName; }
            set { medName = value; }
        }
        public string MedCode
        {
            get { return medCode; }
            set { medCode = value; }
        }
        public double DosagePerKG
        {
            get { return dosagePerKG; }
            set { dosagePerKG = value; }
        }
        public string MedDesc
        {
            get { return medDesc; }
            set { medDesc = value; }
        }

        public int MedsInStock
        {
            get { return medsInStock; }
            set { medsInStock = value; }
        }
        #region Manipulate stock
        public void addStock(Medication med, int count)
        {
            foreach (var item in medicineList)
            {
                if (item.medCode == med.medCode)
                {
                    item.MedsInStock += count;
                    OnPopupMessage(med.medCode);
                }
            }
        }
        public void removeStock(Medication med, int count)
        {
            bool tooMuchRemoval = false;
            foreach (var item in medicineList)
            {
                if (item.medCode == med.medCode)
                {
                    if (count > item.medsInStock)
                    {
                        Console.WriteLine("Not enough items are in stock");
                        tooMuchRemoval = true;
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        return;
                    }
                    if (tooMuchRemoval == false)
                    {
                        item.MedsInStock -= count;
                        if (item.medsInStock == 0)
                        {
                            PopupMessage -= stockDisplay;
                            PopupMessage -= stockRunOutDisplay;
                            PopupMessage += stockRunOutDisplay;
                        }
                        OnPopupMessage(med.medCode);
                    } 
                }
            }
        }

        public void displayStock()
        {
            Console.Clear();
            int i = 1;
            //Inbuilt Interface used
            using (IEnumerator<Medication> enumerator = medicineList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Medication item = enumerator.Current;
                    Console.WriteLine($"=========Item:{i}=========");
                    Console.WriteLine($"Medicine Name: {item.MedName}");
                    Console.WriteLine($"Medicine code: {item.MedCode}");
                    Console.WriteLine($"Medicine dosage per kg: {item.MedDesc}");
                    Console.WriteLine($"Medicine description: {item.DosagePerKG}");
                    Console.WriteLine($"Medicine in stock: {item.MedsInStock}");
                    i++;
                }
            }
        }
        #endregion


        #region Popup Displays
        protected virtual void OnPopupMessage(string itemID)
        {
            PopupMessage?.Invoke(itemID);
        }
        public void stockDisplay(string itemID)
        {
            int itemsInStock = 0;
            string itemName = "";
            //LINQ
            Medication match = medicineList.FirstOrDefault(m => m.MedCode == itemID);

            if (match != null)
            {
                itemsInStock = match.MedsInStock;
                itemName = match.MedName;
            }

            MessageBox.Show($"{itemName} has {itemsInStock} items in stock");
        }

        public void stockRunOutDisplay(string itemID)
        {
            string itemName = "";
            Medication match = medicineList.FirstOrDefault(m => m.MedCode == itemID);

            if (match != null)
            {
                itemName = match.MedName;
            }
            MessageBox.Show($"{itemName} has run out of items in stock");
        }
        #endregion
    }//End of medication class
}//End of namespace
