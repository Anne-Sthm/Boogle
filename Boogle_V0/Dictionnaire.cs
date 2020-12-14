using System;
using System.Collections.Generic;
using System.IO;

namespace Boogle_V0
{
    class Dictionnaire
    {
        List<string> mots;
        string langue;
        int taille;
        public Dictionnaire(List<string> mots, int taille, string langue)
        {
            this.langue = langue;
            this.taille = taille;
            this.mots = mots;
        }

        public static List<string> Creation_Dico(int taille)
        {
            List<string> lmots = new List<string>();
            int i;
            string line;
            try
            {
                StreamReader sr = new StreamReader(File.OpenRead(@"MotsPossibles - Copy.txt"));

                while ((line = sr.ReadLine()) != Convert.ToString(taille))
                {

                }

                while ((line = sr.ReadLine()) != Convert.ToString(taille + 1))
                {
                    i = 0;
                    string[] ligne = line.Split(" ");
                    while (i < ligne.Length)
                    {
                        lmots.Add(ligne[i]);
                        i++;
                    }

                }

                sr.Close();

                return lmots;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return lmots;

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
            for (int i = debut; i < fin; i++)
            {

                if (mots[i].Equals(mot)) return true;
            }
            return false;
        }


    }
}
