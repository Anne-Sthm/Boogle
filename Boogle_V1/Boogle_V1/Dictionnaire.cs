using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle
{
    class Dictionnaire
    {
        List<string> mots;
        string langue;
        public Dictionnaire(List<string> mots, string langue)
        {

        }
        public int Taille
        {
            get { return this.mots.Count; }
        }

        public override string ToString()
        {
            return ("Taille : " + this.mots.Count + "\n Langue : " + this.langue);
        }

        public bool RechDichoRecursif(int debut, int fin, string mot)
        {
            for (int i = debut; i < fin; i++){
                if (mots[i].Equals(mot)) return true;
            }
            return false;
        }


    }
}
