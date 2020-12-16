using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_V0
{
    class Jeu
    {
        SortedList<int, Dictionnaire> mondico;
        Plateau monplateau;
        Joueur joueur;
        public Jeu(Plateau monplateau, SortedList<int, Dictionnaire> mondico, Joueur joueur)
        {
            this.mondico = mondico;
            this.monplateau = monplateau;
            this.joueur = joueur;
        }

        public Plateau Monplateau
        {
            get { return this.monplateau; }
        }
        // Teste si le mot est ligible 
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


        
        public void Tour(int time, string proposition)
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
