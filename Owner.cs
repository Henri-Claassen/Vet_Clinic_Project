using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    internal class Owner //Dante:Removed the abstract keyword because I couldn't create an instance of an abstract type
    //2nd option : was to add a subclass 
    {
        private string ownerName;
        private int ownerID;
        private int ownerCoverageNr;
        private string ownerCoverageName;
        private string ownerCoveragePackage;

        public List<Animal> Pets { get; set; }//List so each owner can have many pets if they choose 

        public Owner()
        {
            
        }
        public Owner(string ownerName, int ownerID, int ownerCoverageNr, string ownerCoverageName, string ownerCoveragePackage)
        {
            this.ownerName = ownerName;
            this.ownerID = ownerID;
            this.ownerCoverageNr = ownerCoverageNr;
            this.ownerCoverageName = ownerCoverageName;
            this.ownerCoveragePackage = ownerCoveragePackage;
        }

        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }

        public int OwnerID
        {
            get { return ownerID; }
            set { ownerID = value; }
        }

        public int OwnerCoverageNr
        {
            get { return ownerCoverageNr; }
            set { ownerCoverageNr = value; }
        }

        public string OwnerCoverageName
        {
            get { return ownerCoverageName; }
            set { ownerCoverageName = value; }
        }

        public string OwnerCoveragePackage
        {
            get { return ownerCoveragePackage; }
            set { ownerCoveragePackage = value; }
        }

    }//End of owner class
}//End of namespace
