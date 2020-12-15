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

        /// <summary>
        /// Constructeur de la classe dictionnaire
        /// </summary>
        /// <param name="mots"> liste des mots</param>
        /// <param name="taille"> taille des mots de la liste</param>
        /// <param name="langue"> langue du dictionnaire</param>
        public Dictionnaire(List<string> mots, int taille, string langue)
        {
            this.langue = langue;
            this.taille = taille;
            this.mots = mots;
        }

        /// <summary>
        /// Creation de la liste de tous les mots d'une certaine taille
        /// </summary>
        /// <param name="taille"> taille des mots de la liste</param>
        /// <returns> liste de tous les mots francais d'une certaine taille  </returns>
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

                while (((line = sr.ReadLine()) != Convert.ToString(taille + 1)) && line != null)
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

        /// <summary>
        /// Retourne la taille du dictionnaire 
        /// </summary>
        public int Taille
        {
            get { return this.mots.Count; }
        }

        /// <summary>
        /// Retourne la liste des mots contenus dans ce dictionnaire 
        /// </summary>
        public List<string> Mots
        {
            get { return this.mots; }
        }

        /// <summary>
        /// retourne une chaîne de caractères qui décrit le dictionnaire à savoir ici le nombre de mots par longueur et la langue
        /// </summary>
        /// <returns> une chaîne de caractères qui décrit le dictionnaire</returns>
        public override string ToString()
        {
            return ("Taille : " + this.mots.Count + "\n Langue : " + this.langue);
        }


        /// <summary>
        /// Vérifie le mot appartienne bien à un dictionnaire
        /// </summary>
        /// <param name="debut"> début de la zone du mot testé</param>
        /// <param name="fin"> fin de la zone du mot testé</param>
        /// <param name="mot"> mot testé</param>
        /// <returns> true si le mot testé appartient au dictionnaire false sinon </returns>
        public bool RechDichoRecursif(int debut, int fin, string mot)
        {
            if (debut == fin)
            {
                return false;
            }
            if (mots[debut].Equals(mot) || mots[fin].Equals(mot))
            {
                return true;
            }
            if (mots[(debut + fin) / 2 - 1].Equals(mot))
            {
                return true;
            }

            else if (String.Compare(mots[(debut + fin) / 2 - 1], mot, false) >= 1)
            {
                return (RechDichoRecursif(debut, (debut + fin) / 2, mot));
            }
            else
            {
                return (RechDichoRecursif((debut + fin) / 2, fin, mot));

            }
        }

    }
}
