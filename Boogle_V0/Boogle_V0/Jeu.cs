using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_V0
{
    /// <summary>
    /// La classe Jeu fait le lien entre les dictionnaires, les joueurs et le plateau.
    /// </summary>
    public class Jeu
    {
        /// <summary>
        /// Variables d'instance
        /// </summary>
        SortedList<int, Dictionnaire> mondico;
        Plateau monplateau;
        Joueur joueur;

        static int WHOLE = 1400;
        static int HALF = WHOLE / 2;
        static int QUARTER = HALF / 2;
        static int EIGHTH = QUARTER / 2;
        static int SIXTENTH = EIGHTH / 2;

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
        /// Retourne le plateau.
        /// </summary>
        public Plateau Monplateau
        {
            get { return this.monplateau; }
        }

        // Teste si le mot est eligible 
        /// <summary>
        /// Verifie si le mot appartient au dictionnaire et s'il peut être formé dans cette configuration du plateau, joue une suite de notes différentes selon l'échec ou la reussite. 
        /// </summary>
        /// <param name="mot">mot comparé</param>
        /// <returns>True si les conditions sont completés, else sinon.</returns>
        public bool MotEligible(string mot)
        {


            if (mot.Length >= 3 && mot.Length <= 15)
            {
                // Vérifie que le mot appartient au dictionnaire
                if (this.mondico[mot.Length].Words_list.Contains(mot))
                {
                    bool[,] valide = new bool[4, 4];
                    // Vérifie si les lettres du mots sont bien adjacentes 
                    if (monplateau.Test_Plateau(0, 0, 0, mot, valide))
                    {
                        return true;
                    } 

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
                Console.Beep(1760, SIXTENTH);
                Console.Beep(1975, SIXTENTH);
                Console.Beep(2349, SIXTENTH);
                if (joueur.Contain(proposition))
                {
                    Console.SetCursorPosition(0, 11);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Vous avez déjà joué le mot " + proposition + ", vous ne gagnez aucun point ! :( ");
                }
                else
                {
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Vous avez gagné " + proposition.Length + " points !");
                    this.joueur.Add_Mot(proposition);
                    this.joueur.Score += proposition.Length;
                }

            }
            else
            {
                Console.SetCursorPosition(0, 10);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Le mot " + proposition + " est incorrect :(");
                Console.SetCursorPosition(0, 11);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.Beep(220, EIGHTH);
                Console.Beep(207, EIGHTH);
                Console.Beep(196, EIGHTH);
                Console.Beep(185, HALF + QUARTER);

            }

        }




    }
}