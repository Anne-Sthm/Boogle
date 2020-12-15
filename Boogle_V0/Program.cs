using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;




namespace Boogle_V0
{
    class Program
    {
        static int minutes=0;
        static int secondes=10;

        public static void ChronoSecondes(Object source, ElapsedEventArgs e)
        {
            int colonne = Console.CursorLeft;
            int ligne = Console.CursorTop;
            Console.SetCursorPosition(0, 0);
            Console.Write("                                  ");
            Console.SetCursorPosition(0, 0);
            Console.Write("Temps restant : " +secondes);
            Console.SetCursorPosition(colonne, ligne);
            if (secondes > 0)
            {
                secondes--;
            }
            

            
        }

        public static void ChronoMinutes(Object source, ElapsedEventArgs e)
        {
            minutes++;
            secondes = 10;

        }


        static void Main(string[] args)
        {

            // Création des dictionnaires
            SortedList<int,Dictionnaire> liste_dico = new SortedList<int,Dictionnaire>();
            for(int i = 2; i < 16; i++)
            {
                List<string> mots_dico = Dictionnaire.Creation_Dico(i);
                Dictionnaire dico = new Dictionnaire(mots_dico, i, "français");
                liste_dico.Add(i, dico);
            }

            // Création des joueurs
            List<string> mots_trouves1 = new List<string>();
            Joueur j1 = new Joueur(mots_trouves1, 0, "Anne");
            List<string> mots_trouves2 = new List<string>();
            Joueur j2 = new Joueur(mots_trouves1, 0, "Victor");

            // Création du plateau
            Plateau plateau = new Plateau();

            // Création du jeu
            Jeu jeu = new Jeu(plateau, liste_dico);

            // Début du jeu
            Timer chrono_jeu = new System.Timers.Timer(10000); // Toutes les 10 secondes
            chrono_jeu.AutoReset = true;
            chrono_jeu.Enabled = true;
            chrono_jeu.Elapsed += ChronoMinutes; // Routine du timer 
            chrono_jeu.Start();

            Timer temps_restant = new System.Timers.Timer(1000); // Toutes les secondes
            temps_restant.AutoReset = true;
            temps_restant.Enabled = true;
            temps_restant.Elapsed += ChronoSecondes; // Routine du timer 
            temps_restant.Start();

            while (minutes < 6)
            {
                if (minutes % 2 == 0) 
                {
                    for (int i = 3; i < 8; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write("                                  ");
                    }
                    Console.SetCursorPosition(0, 2);
                    Console.WriteLine("Joueur 1");
                    Console.WriteLine("------------------");
                    Console.WriteLine(plateau.ToString());
                    jeu.Tour(j1, secondes);

                } else
                {
                    for (int i = 3; i < 8; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write("                                  ");
                    }
                    Console.SetCursorPosition(0, 2);
                    Console.WriteLine("Joueur 2");
                    Console.WriteLine("------------------");
                    Console.WriteLine(plateau.ToString());
                    jeu.Tour(j1,secondes);
                }

            }

            Console.Clear();
            Console.WriteLine("Le temps est écoulé !");
            Console.WriteLine(j1.ToString());
            Console.WriteLine(j2.ToString());

            chrono_jeu.Stop();
            

        }
    }
}
