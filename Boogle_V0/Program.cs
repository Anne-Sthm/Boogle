using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Globalization;





namespace Boogle_V0
{

    class Program
    {
        static int minutes=0;
        static int secondes=60;
        static Jeu jeu_1;
        static Jeu jeu_2;
        static Jeu jeu_courant;
        static Plateau plateau;
        static bool plateauFini;
        static object consoleLock;

        public static void ChronoSecondes(Object source, ElapsedEventArgs e)
        {
            lock(consoleLock)
            {
                if (plateauFini)
                {
                    int colonne = Console.CursorLeft;
                    int ligne = Console.CursorTop;
                    Console.SetCursorPosition(0, 0);
                    Console.Write("                                  ");
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Temps restant : " + secondes + plateauFini);
                    Console.SetCursorPosition(colonne, ligne);
                    if (secondes > 0)
                    {
                        secondes--;
                    }
                }
            }
            
        }

        public static void ChronoMinutes(Object source, ElapsedEventArgs e)
        {
            lock (consoleLock)
            {
                minutes++;
                secondes = 60;
                Console.Clear();
                plateauFini = false;
                if (minutes < 6)
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
                        //jeu_1.Tour(secondes);
                        jeu_courant = jeu_1;

                    }
                    else
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
                        //jeu_2.Tour(secondes);
                        jeu_courant = jeu_2;
                    }
                    Console.WriteLine("Saisir un mot");
                    plateauFini = true;
                }
            }
        }

        static void Main(string[] args)
        {
            consoleLock = new object();
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
            Joueur j2 = new Joueur(mots_trouves2, 0, "Victor");

            // Création du plateau
            plateau = new Plateau();

            // Création du jeu
            jeu_1 = new Jeu(plateau, liste_dico, j1);
            jeu_2 = new Jeu(plateau, liste_dico, j2);

            jeu_courant = jeu_1;

            // Début du jeu

            Timer chrono_jeu = new System.Timers.Timer(60000); // Toutes les 10 secondes
            chrono_jeu.AutoReset = true;
            chrono_jeu.Enabled = true;
            chrono_jeu.Elapsed += ChronoMinutes; // Routine du timer

            Timer temps_restant = new System.Timers.Timer(1000); // Toutes les secondes
            temps_restant.AutoReset = true;
            temps_restant.Enabled = true;
            temps_restant.Elapsed += ChronoSecondes; // Routine du timer

            plateauFini = false;
            chrono_jeu.Start();
            temps_restant.Start();

            DateTime start = DateTime.Now;
            TimeSpan tps_ecoule;
            tps_ecoule = start - DateTime.Now;

            for (int i = 3; i < 8; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("                                  ");
            }
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Joueur 1");
            Console.WriteLine("------------------");
            Console.WriteLine(plateau.ToString());
            Console.WriteLine("Saisir un mot");
            plateauFini = true;
            //while (minutes < 6)
            //{

            //    while (minutes % 2 == 0) 
            //    {
            //        for (int i = 3; i < 8; i++)
            //        {
            //            Console.SetCursorPosition(0, i);
            //            Console.Write("                                  ");
            //        }
            //        Console.SetCursorPosition(0, 2);
            //        Console.WriteLine("Joueur 1");
            //        Console.WriteLine("------------------");
            //        Console.WriteLine(plateau.ToString());
            //        jeu_1.Tour(secondes);

            //    } 
            //    while(minutes % 2 == 1) { 
            //    }
            //    {
            //        for (int i = 3; i < 8; i++)
            //        {
            //            Console.SetCursorPosition(0, i);
            //            Console.Write("                                  ");
            //        }
            //        Console.SetCursorPosition(0, 2);
            //        Console.WriteLine("Joueur 2");
            //        Console.WriteLine("------------------");
            //        Console.WriteLine(plateau.ToString());
            //        jeu_2.Tour(secondes);
            //    }
            //}
            while (minutes < 6)
            {
                string proposition = Console.ReadLine();
                jeu_courant.Tour(secondes, proposition);
            } 

            chrono_jeu.Stop();
            temps_restant.Stop();
            Console.Clear();
            Console.WriteLine("Le temps est écoulé !");
            Console.WriteLine(j1.ToString());
            Console.WriteLine(j2.ToString());


            

        }
    }
}
