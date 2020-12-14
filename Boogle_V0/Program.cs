using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;




namespace Boogle_V0
{
    class Program
    {
        static int minutes=0;
        static int secondes=0;
        static string lecture;


        public static void ChronoSecondes(Object source, ElapsedEventArgs e)
        {
            int ligne = Console.CursorLeft;
            int colonne = Console.CursorTop;
            Console.SetCursorPosition(0, 0);
            Console.Write(secondes);
            Console.SetCursorPosition(ligne, colonne);
            secondes++;

            
        }

        public static void ChronoMinutes(Object source, ElapsedEventArgs e)
        {
            Console.Clear();
            lecture = "";
            minutes++;

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
            Timer chrono_jeu = new System.Timers.Timer(10000);
            chrono_jeu.AutoReset = true;
            chrono_jeu.Enabled = true;
            chrono_jeu.Elapsed += ChronoMinutes;
            chrono_jeu.Start();

            Timer temps_restant = new System.Timers.Timer(1000);
            temps_restant.AutoReset = true;
            temps_restant.Enabled = true;
            temps_restant.Elapsed += ChronoSecondes;
            temps_restant.Start();

            while (minutes < 6)
            {
                if (minutes%2 == 0)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Joueur 1");
                    Console.Write(plateau.ToString());
                    lecture = Console.ReadLine();
                } else
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Joueur 2");
                    Console.Write(plateau.ToString());
                    lecture = Console.ReadLine();
                }

            }

            chrono_jeu.Stop();
            

        }
    }
}
