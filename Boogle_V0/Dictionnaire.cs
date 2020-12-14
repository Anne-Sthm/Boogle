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

                while (((line = sr.ReadLine()) != Convert.ToString(taille + 1)) && line!=null)
                {
                    i = 0;
                    string[] ligne = line.Split(' ');
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
            if (debut == fin)
            {
                return false;
            }
            if (mots[debut].Equals(mot)||mots[fin].Equals(mot))
            {
                return true;
            }
            if (mots[(debut + fin) / 2 - 1].Equals(mot))
            {
                return true;
            }

            else if (String.Compare(mots[(debut + fin) / 2 - 1], mot, false) >= 1)
            {
                return (RechDichoRecursif(debut, (debut + fin) / 2  , mot));
            }
            else
            {
                return (RechDichoRecursif((debut + fin) / 2  , fin, mot));

            }
        }


    }
}
