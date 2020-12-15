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

        // Console.Readline

 
        
        public static string TryRL(int time)
        {
            Task<string> task = Task.Factory.StartNew(Console.ReadLine);
            //string r = task.Wait(time*1000) ? task.Result : "      "; // Si le Readline ne s'est pas effectué dans le temps imparti on renvoie un string vide 
            return task.Result;
        }

       
        
        public void Tour(int time, string proposition)
        {
            //string proposition;
            //Console.WriteLine("Saisir un mot");
            Console.SetCursorPosition(0, 10);
            //proposition = TryRL(time); // Readline
            //  string proposition = Console.ReadLine();
            /*try
            {
                proposition = Reader.ReadLine(time * 1000 -1);

            } catch (TimeoutException)
            {
                proposition = "      ";
            }*/
            
            
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
                    Console.WriteLine(joueur.ToString() +" : " + proposition.Length + " points !");
                    this.joueur.Add_Mot(proposition);
                    this.joueur.Score += proposition.Length;
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
