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
        public Joueur(List<string> found_words, int score, string nom)
        {
            this.found_words = found_words;
            this.score = score;
            this.nom = nom;
        }

        public bool Contain(string mot)
        {
            return this.found_words.Contains(mot);
        }

        public void Add_Mot(string mot)
        {
            this.found_words.Add(mot);
        }

        public override string ToString()
        {
            string joueur="";
            foreach(string mot in this.found_words)
            {
                joueur=String.Concat(joueur, mot, " ");
            }
            return ("Nom : "+this.nom+"\n Score : "+this.score+"\n Mots trouvés : " + joueur);
        }

    }
}
