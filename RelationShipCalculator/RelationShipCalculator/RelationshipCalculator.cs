using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelationShipCalculator
{
    class RelationshipCalculator
    {
        string FullName;
        string NumString;
        public RelationshipCalculator(string firstName, string lastName)
        {
            this.FullName = (firstName + lastName).Replace(" ", "").ToLower();
        }
        public string RelationshipChance
        {
            get { return this.CalculateRelationship(); }
        }
        private string CalculateRelationship()
        {
            //Phase 1 create string
            NumString = "";
            CreateNumberString();
            while (NumString.Length > 2) ReduceNumString();
            return NumString;

        }
        private void CreateNumberString()
        {
            while (FullName.Length > 0)
            {
                char firstChar = FullName[0];
                int charCount = 0;
                while (FullName.Contains(firstChar))
                {
                    charCount++;
                    int charIndex = FullName.IndexOf(firstChar);
                    FullName = FullName.Remove(charIndex, 1);
                }
                NumString += charCount;
            }
        }
        private void ReduceNumString()
        {
            string nextNumString = string.Empty;
            while (NumString.Length > 0)
            {
                int firstNum = int.Parse(NumString[0].ToString());
                if (NumString.Length > 1)
                {

                    int lastNum = int.Parse(NumString[NumString.Length - 1].ToString());

                    //Remove Numbers
                    NumString = NumString.Remove(0, 1);
                    NumString = NumString.Remove(NumString.Length - 1, 1);
                    nextNumString += (firstNum + lastNum);
                }
                else
                {
                    NumString = NumString.Remove(0, 1);
                    nextNumString += firstNum;
                }
            }
            NumString = nextNumString;

        }
    }
}
