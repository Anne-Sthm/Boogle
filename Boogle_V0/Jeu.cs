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
        public Jeu(Plateau monplateau, SortedList<int, Dictionnaire> mondico)
        {
            this.mondico = mondico;
            this.monplateau = monplateau;
        }

        // Teste si le mot est ligible 
        public bool MotEligible(string mot)
        {
            if (mot.Length>=3){
                if (this.mondico[mot.Length].Mots.Contains(mot)) // Vérifie que le mot appartient au dictionnaire
                {
                    bool[,] valide = new bool[4, 4];
                    if(monplateau.Test_Plateau(monplateau.x_pos(mot[0]), monplateau.y_pos(mot[0]), 0, mot, valide)) return true; // Vérifie si les lettres du mots sont bien adjacentes 
                }
            }

            return false;
        }

        // Console.Readline
        public string TryRL(int time)
        {
            Task<string> task = Task.Factory.StartNew(Console.ReadLine);
            string r = task.Wait(time*1000) ? task.Result : "      "; // Si le Readline ne s'est pas effectué dans le temps imparti on renvoie un string vide 
            return r;
        }
        
        public void Tour(Joueur joueur, int time)
        {
            
            Console.WriteLine("Saisir un mot");
            Console.SetCursorPosition(0, 10);
            string proposition = TryRL(time); // Readline
            
            
            if (MotEligible(proposition))
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Le mot " + proposition + " est correct");
                if (joueur.Contain(proposition))
                {
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Vous avez déjà joué le mot "+proposition+", vous ne gagnez aucun point ! :( ");
                } else
                {
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Vous gagnez : " + proposition.Length + " points !");
                    joueur.Add_Mot(proposition);
                    joueur.Score += proposition.Length;
                }

            } else
            {
                if (!proposition.Equals("      "))
                {
                    Console.SetCursorPosition(0, 9);
                    Console.WriteLine("Le mot " + proposition + " est incorrect :(");
                }
                
            }
        }
        
    }
}
