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
        static List<string> prefixes;


        /// <summary>
        /// Constructeur de la classe dictionnaire.
        /// </summary>
        /// <param name="mots"> Liste des mots.</param>
        /// <param name="taille"> Taille des mots de la liste.</param>
        /// <param name="langue"> Langue du dictionnaire.</param>
        public Dictionnaire(List<string> mots, int taille, string langue)
        {
            this.langue = langue;
            this.taille = taille;
            this.mots = mots;
        }

        /// <summary>
        /// Creation de la liste de tous les mots d'une certaine taille.
        /// </summary>
        /// <param name="taille"> Taille des mots de la liste.</param>
        /// <returns> Liste de tous les mots francais d'une certaine taille.  </returns>
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
        /// Retourne la taille du dictionnaire. 
        /// </summary>
        public int Taille
        {
            get { return this.mots.Count; }
        }

        /// <summary>
        /// Retourne la liste des mots contenus dans ce dictionnaire. 
        /// </summary>
        public List<string> Mots
        {
            get { return this.mots; }
        }

        /// <summary>
        /// Retourne une chaîne de caractères qui décrit le dictionnaire à savoir ici le nombre de mots par longueur et la langue.
        /// </summary>
        /// <returns>Une chaîne de caractères qui décrit le dictionnaire.</returns>
        public override string ToString()
        {
            return ("Taille : " + this.mots.Count + "\n Langue : " + this.langue);
        }

        /// <summary>
        /// Vérifie le mot appartienne bien à un dictionnaire.
        /// </summary>
        /// <param name="debut"> Début de la zone du mot testé.</param>
        /// <param name="fin"> Fin de la zone du mot testé.</param>
        /// <param name="mot"> Mot testé.</param>
        /// <returns> True si le mot testé appartient au dictionnaire false sinon. </returns>
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

        // Recherche les préfixes possibles des mots dans le dictionnaire 
        // Un prefixe contient 3 lettres
        // Les prefixes seront stockés dans la liste prefixes

        public static List<string>  Recherche_Prefixes()
        {
            prefixes = new List<string>();
            for (int i = 3; i < 15; i++)
            {
                List<string> Dico = Creation_Dico(i);
                for (int j = 0; j < Dico.Count; j++)
                {
                    string str = "";
                    str += Dico[j][0];
                    str += Dico[j][1];
                    str += Dico[j][2];
                    if (!prefixes.Contains(str))
                    {
                        prefixes.Add(str);                     
                    }
                }
            }
            return prefixes;
        }


        // Une liste par préfixe
        // Les mots sont par ordre de taille 
        public static List<string> Mots_Prefixes(string prefixeunique)
        {
            List<string> List_mots_prefixe = new List<string>();
            for (int i = 3; i < 15; i++)
            {
                List<string> Dico = Creation_Dico(i);
                for (int j = 0; j < Dico.Count; j++)
                {
                    if(prefixeunique[0]== Dico[j][0]&& prefixeunique[1] == Dico[j][1] && prefixeunique[2] == Dico[j][2])
                    {
                        List_mots_prefixe.Add(Dico[j]);
                    }
                }
            }
            return List_mots_prefixe;
        }
    }
}
