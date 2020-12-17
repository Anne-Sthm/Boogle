using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle_V0
{
    /// <summary>
    /// La classe Joueur construit les joueurs du Boogle.
    /// </summary>
    public class Joueur
    {
        List<string> found_words;
        int score;
        string nom;

        /// <summary>
        /// Constructeur de la classe Joueur.
        /// </summary>
        /// <param name="found_words">Mots trouvés par le joueur pendant la partie.</param>
        /// <param name="score">Score du jour. </param>
        /// <param name="nom">Nom du joueur.</param>
        public Joueur(List<string> found_words, int score, string nom)
        {
            this.found_words = found_words;
            this.score = score;
            this.nom = nom;
        }

        /// <summary>
        /// Retourne le score du joueur.
        /// </summary>
        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }
        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }
        /// <summary>
        /// Teste si le mot passé appartient déjà aux mots trouvés par le joueur pendant la partie.
        /// </summary>
        /// <param name="mot">mot testé</param>
        /// <returns>True si le mot à déjà été trouvé, False sinon.</returns>
        public bool Contain(string mot)
        {
            return this.found_words.Contains(mot);
        }


        /// <summary>
        /// Ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie.
        /// </summary>
        /// <param name="mot"> Mot qui va être ajouté dans la liste.</param>
        public void Add_Mot(string mot)
        {
            this.found_words.Add(mot);
        }


        /// <summary>
        /// Retourne une chaîne de caractères qui décrit un joueur.
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
