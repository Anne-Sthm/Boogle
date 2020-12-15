using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle_V0
{
    class Joueur
    {
        List<string> found_words;
        int score;
        string nom;

        /// <summary>
        /// Constructeur de la classe Joueur
        /// </summary>
        /// <param name="found_words">mots trouvés par le joueur pendant la partie</param>
        /// <param name="score"> score du jour </param>
        /// <param name="nom">nom du joueur</param>
        public Joueur(List<string> found_words, int score, string nom)
        {
            this.found_words = found_words;
            this.score = score;
            this.nom = nom;
        }

        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        /// <summary>
        /// teste si le mot passé appartient déjà aux mots trouvés par le joueur pendant la partie
        /// </summary>
        /// <param name="mot">mot testé</param>
        /// <returns>True si le mot à déjà été trouvé, False sinon</returns>
        public bool Contain(string mot)
        {
            return this.found_words.Contains(mot);
        }


        /// <summary>
        /// ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie
        /// </summary>
        /// <param name="mot"> mot qui va être ajouté dans la liste</param>
        public void Add_Mot(string mot)
        {
            this.found_words.Add(mot);
        }


        /// <summary>
        /// retourne une chaîne de caractères qui décrit un joueur
        /// </summary>
        /// <returns> chaine de caractère décrivant le joueur </returns>
        public override string ToString()
        {
            string joueur = "";
            foreach (string mot in this.found_words)
            {
                joueur = String.Concat(joueur, mot, " ");
            }
            return ("Nom : " + this.nom + "\n Score : " + this.score + "\n Mots trouvés : " + joueur);
        }

    }
}
