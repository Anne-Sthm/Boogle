using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle_V0
{
    /// <summary>
    /// La classe IA construit l'intelligence artificielle du jeu.
    /// </summary>

    public class IA
    {
        List<Dictionnaire> liste_dico;
        Plateau plateau;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="liste_dico"></param>
        /// <param name="plateau"></param>
        public IA(List<Dictionnaire> liste_dico, Plateau plateau)
        {
            this.liste_dico = liste_dico;
            this.plateau = plateau;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Generate_prefixe()
        {
            // Cherche tous les groupes de 3 lettres possibles
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefixe"></param>
        /// <returns></returns>
        public bool Test_Prefixe(string prefixe)
        {
            if (Dictionnaire.Recherche_Prefixes().Contains(prefixe)) return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefixe"></param>
        // Mettre en récursif
        public void NextLetteer(string prefixe)
        {
            string create_word = prefixe;
            // Ajouter une lettre adjacente au plateau
            foreach (string mot in Dictionnaire.Mots_Prefixes(prefixe))
            {
                mot.StartsWith(create_word);
                // retourner la fonction avec prefixe = create_word
            }
        }
    }
}
