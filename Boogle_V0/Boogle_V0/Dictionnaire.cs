using System;
using System.Collections.Generic;
using System.IO;

namespace Boogle_V0
{
    /// <summary>
    /// La classe dictionnaire construit les dictionnaires utilisés pour vérifier si les mots proposer par le joueur existent.
    /// </summary>
    public class Dictionnaire
    {
        /// <summary>
        /// Variables d'instance
        /// </summary>
        List<string> words_list;
        string langue;
        int taille;

        /// <summary>
        /// Variable de classe
        /// </summary>
        static List<string> prefixes;


        /// <summary>
        /// Constructeur de la classe dictionnaire.
        /// </summary>
        /// <param name="words_list"> Liste des mots.</param>
        /// <param name="taille"> Taille des mots de la liste.</param>
        /// <param name="langue"> Langue du dictionnaire.</param>
        public Dictionnaire(List<string> words_list, int taille, string langue)
        {
            this.langue = langue;
            this.taille = taille;
            this.words_list = words_list;
        }

        /// <summary>
        /// Creation de la liste de tous les mots d'une certaine taille.
        /// </summary>
        /// <param name="taille"> Taille des mots de la liste.</param>
        /// <returns> Liste de tous les mots francais d'une certaine taille.  </returns>
        public static List<string> Creation_Dico(int taille)
        {
            List<string> words_in_dico = new List<string>();
            int word_index_in_line;
            string current_line;

            try
            {
                // Ouvre un flux en lecture du fichier et le lit
                StreamReader reader = new StreamReader(File.OpenRead(@"MotsPossibles - Copy.txt"));

                // On lit le fichier jusqu'à la ligne avec la taille souhaitée du mot 
                while ((current_line = reader.ReadLine()) != Convert.ToString(taille))
                {

                }

                // On lit jusqu'à la prochaine taille de mot sauf si le fichier est fini et  
                // on ajoute les mots de la ou les lignes à la liste des mots de ce dictionnaire
                while (((current_line = reader.ReadLine()) != Convert.ToString(taille + 1)) && current_line != null)
                {
                    word_index_in_line = 0;
                    string[] split_current_line = current_line.Split(' ');
                    while (word_index_in_line < split_current_line.Length)
                    {
                        words_in_dico.Add(split_current_line[word_index_in_line]);
                        word_index_in_line++;
                    }
                }

                reader.Close(); // On ferme le flux de lecture
                return words_in_dico;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return words_in_dico;
        }


        /// <summary>
        /// Retourne la liste des mots contenus dans ce dictionnaire. 
        /// </summary>
        public List<string> Words_list
        {
            get { return this.words_list; }
        }

        /// <summary>
        /// Retourne une chaîne de caractères qui décrit le dictionnaire à savoir ici la longueur des mots et la langue.
        /// </summary>
        /// <returns>Une chaîne de caractères qui décrit le dictionnaire.</returns>
        public override string ToString()
        {
            return ("Taille des mots : " + this.taille + "\n Langue : " + this.langue);
        }

        /// <summary>
        /// Vérifie le mot appartienne bien à un dictionnaire.
        /// </summary>
        /// <param name="debut_dico"> Début de la zone du mot testé.</param>
        /// <param name="fin"> Fin de la zone du mot testé.</param>
        /// <param name="mot"> Mot testé.</param>
        /// <returns> True si le mot testé appartient au dictionnaire false sinon. </returns>
        public bool RechDichoRecursif(int debut_dico, int fin, string mot)
        {
            if (debut_dico == fin)
            {
                return false;
            }
            if (words_list[debut_dico].Equals(mot) || words_list[fin].Equals(mot))
            {
                return true;
            }
            if (words_list[(debut_dico + fin) / 2 - 1].Equals(mot))
            {
                return true;
            }

            else if (String.Compare(words_list[(debut_dico + fin) / 2 - 1], mot, false) >= 1)
            {
                return (RechDichoRecursif(debut_dico, (debut_dico + fin) / 2, mot));
            }
            else
            {
                return (RechDichoRecursif((debut_dico + fin) / 2, fin, mot));
            }
        }


        /// <summary>
        /// Recherche_Préfixe recherche les préfixes possibles des mots dans le dictionnaire, un préfixe contient 3 lettres.
        /// </summary>
        /// <returns>Elle retourne la liste de tous les préfixes du dictionnaire</returns>
        public static List<string> Recherche_Prefixes()
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

        /// <summary>
        /// Créer la liste des mots pouvant être construit à partir d'un préfixe.
        /// </summary>
        /// <param name="prefixeunique">Un préfixe, soit une liste de mots de 3 lettres.</param>
        /// <returns>La liste des mots pouvant être costruit à partir de ce préfixe</returns>
        public static List<string> Mots_Prefixes(string prefixeunique)
        {
            List<string> List_mots_prefixe = new List<string>();
            for (int i = 3; i < 15; i++)
            {
                List<string> Dico = Creation_Dico(i);
                for (int j = 0; j < Dico.Count; j++)
                {
                    if (prefixeunique[0] == Dico[j][0] && prefixeunique[1] == Dico[j][1] && prefixeunique[2] == Dico[j][2])
                    {
                        List_mots_prefixe.Add(Dico[j]);
                    }
                }
            }
            return List_mots_prefixe;
        }
    }
}
