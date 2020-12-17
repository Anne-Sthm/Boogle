using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle_V0
{
    class IA
    {
        List<Dictionnaire> liste_dico;
        Plateau plateau;
        public IA(List<Dictionnaire> liste_dico, Plateau plateau)
        {
            this.liste_dico = liste_dico;
            this.plateau = plateau;
        }

        public void Generate_prefixe()
        {
            // Cherche tous les groupes de 3 lettres possibles
        }

        public bool Test_Prefixe(string prefixe)
        {
            if (Dictionnaire.Recherche_Prefixes().Contains(prefixe)) return true;
            return false;
        }

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
