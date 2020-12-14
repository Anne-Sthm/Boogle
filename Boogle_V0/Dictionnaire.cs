using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Boogle_V0
{
    class Dictionnaire
    {
        SortedList<int, List<string>> mots;
        string langue;
        public Dictionnaire(SortedList<int, List<string>> mots,string langue)
        {
            this.langue = langue;
            this.mots = mots;
        }

        public static SortedList<int, List<string>> readfile(string filemane)
        {
            StreamReader flux = null;
            string line;
            int resultat;
            int i = 0;
            int key = 0;
            List<string> listedestring = new List<string>();
            SortedList<int, List<string>> des_mots = new SortedList<int, List<string>>();
            try
            {
                flux = new StreamReader("MotsPossibles - Copy.txt");
                while ((line = flux.ReadLine()) != null)
                {
                    i = 0;
                    listedestring.Clear();
                    string[] ligne = line.Split(" ");
                    if (Int32.TryParse(ligne[i], out resultat))
                    {
                        key = resultat;
                        
                    } else
                    {
                        while (i < ligne.Length - 1)
                        {
                            listedestring.Add(ligne[i]);
                            Console.WriteLine(ligne[i]);
                            i++;
                        }
                    }

                    des_mots.Add(key, listedestring);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                if ("MotsPossibles - Copy.txt" != null) { flux.Close(); }
            }

            return des_mots;
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
