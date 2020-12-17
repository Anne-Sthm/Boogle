using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_V0
{
    /// <summary>
    /// 
    /// </summary>
    class Jeu
    {
        SortedList<int, Dictionnaire> mondico;
        Plateau monplateau;
        Joueur joueur;

        /// <summary>
        /// Constructeur associant le joueur, le plateau et les dictionnaires.
        /// </summary>
        /// <param name="monplateau">Plateau de la partie.</param>
        /// <param name="mondico">Dictionnaire.</param>
        /// <param name="joueur">Joueur.</param>
        public Jeu(Plateau monplateau, SortedList<int, Dictionnaire> mondico, Joueur joueur)
        {
            this.mondico = mondico;
            this.monplateau = monplateau;
            this.joueur = joueur;
        }

        /// <summary>
        /// 
        /// </summary>
        public Plateau Monplateau
        {
            get { return this.monplateau; }
        }
        // Teste si le mot est eligible 
        /// <summary>
        /// Verifie si le mot appartient au dictionnaire et s'il peut être formé dans cette configuration du plateau. 
        /// </summary>
        /// <param name="mot">mot comparé</param>
        /// <returns>True si les conditions sont completés, else sinon.</returns>
        public bool MotEligible(string mot)
        {
            if (mot.Length>=3 && mot.Length<=15){
                if (this.mondico[mot.Length].Mots.Contains(mot)) // Vérifie que le mot appartient au dictionnaire
                {
                    bool[,] valide = new bool[4, 4];
                    if(monplateau.Test_Plateau(monplateau.x_pos(mot[0]), monplateau.y_pos(mot[0]), 0, mot, valide)) return true; // Vérifie si les lettres du mots sont bien adjacentes 
                }
            }

            return false;
        }

        /// <summary>
        /// Verifie si la proposition du joueur est vrai, modifie l'affichage et le score en fonction.
        /// </summary>
        /// <param name="proposition">Mot proposé par le joueur.</param>
        public void Tour(string proposition)
        {

            Console.SetCursorPosition(0, 15);
            Console.Write(new string(' ', Console.WindowWidth));

            if (MotEligible(proposition))
            {
                Console.SetCursorPosition(0, 10);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Le mot " + proposition + " est correct");
                if (joueur.Contain(proposition))
                {
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Vous avez déjà joué le mot "+proposition+", vous ne gagnez aucun point ! :( ");
                } else
                {
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Vous avez gagné " + proposition.Length + " points !");
                    this.joueur.Add_Mot(proposition);
                    this.joueur.Score += proposition.Length;
                }

            } else
            {
                Console.SetCursorPosition(0, 10);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Le mot " + proposition + " est incorrect :(");
                Console.SetCursorPosition(0, 11);
                Console.Write(new string(' ', Console.WindowWidth));

            }
            
        }

        


    }
}
