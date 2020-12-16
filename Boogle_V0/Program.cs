using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Globalization;





namespace Boogle_V0
{

    class Program
    {
        const int minute = 60000;
        const int seconde = 1000;

        static int minutes_elapsed = 0;
        static int secondes_elapsed = 60;
        static Jeu game_player1;
        static Jeu game_player2;
        static Jeu current_game;
        static Plateau plateau;
        static bool draw;
        static object consoleLock;
        static string word;

        public static void Ending()
        {
            int WHOLE = 1600;
            int HALF = WHOLE / 2;
            int QUARTER = HALF / 2;
            int EIGHTH = QUARTER / 2;


            int A = 440;
            int Bbas = 246;
            int B = 493;
            int D = 294;
            int E = 330;
            int Fsharp = 370;
            int Gsharp = 415;

            Console.Write("Veuillez patientez ( avec notre super musique de fin )");
            Console.Beep(Fsharp, EIGHTH);
            Console.Beep(Fsharp, EIGHTH);
            Console.Beep(D, EIGHTH);
            Console.Beep(Bbas, EIGHTH);
            Console.Beep(Bbas, QUARTER);
            Console.Beep(E, QUARTER);
            Console.Beep(E, QUARTER);
            Console.Beep(E, EIGHTH);
            Console.Write(" :) ");
            Console.Beep(Gsharp, EIGHTH);
            Console.Beep(Gsharp, EIGHTH);
            Console.Beep(A, EIGHTH);
            Console.Beep(B, EIGHTH);
            Console.Beep(A, EIGHTH);
            Console.Beep(A, EIGHTH);
            Console.Write(" :) ");
            Console.Beep(A, EIGHTH);
            Console.Beep(E, QUARTER);
            Console.Beep(D, QUARTER);
            Console.Beep(Fsharp, QUARTER);
            Console.Beep(Fsharp, QUARTER);
            Console.Beep(Fsharp, EIGHTH);
            Console.Beep(E, EIGHTH);
            Console.Beep(E, EIGHTH);
            Console.Beep(Fsharp, EIGHTH);
            Console.Beep(E, EIGHTH);
            Console.Beep(Fsharp, EIGHTH);
        }
        public static void ChronoSecondes(Object source, ElapsedEventArgs e)
        {
            lock(consoleLock)
            {
                if (draw)
                {
                    int colonne = Console.CursorLeft;
                    int ligne = Console.CursorTop;
                    Console.SetCursorPosition(0, 0);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Temps restant : " + secondes_elapsed);
                    Console.SetCursorPosition(colonne, ligne);
                    if (secondes_elapsed > 0)
                    {
                        secondes_elapsed--;
                    }
                }
            }
            
        }

        public static void ChronoMinutes(Object source, ElapsedEventArgs e)
        {
            lock (consoleLock)
            {
                minutes_elapsed++;
                secondes_elapsed = 60;
                Console.Clear();
                draw = false;
                if (minutes_elapsed < 6)
                {

                    if (minutes_elapsed % 2 == 0)
                    {
                        for (int i = 3; i < 8; i++)
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write(new string(' ', Console.WindowWidth));
                        }
                        Console.SetCursorPosition(0, 2);
                        Console.WriteLine("Joueur 1");
                        Console.WriteLine("------------------");
                        Console.WriteLine(plateau.ToString());
                        //jeu_1.Tour(secondes);
                        current_game = game_player1;

                    }
                    else
                    {
                        for (int i = 3; i < 8; i++)
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write(new string(' ', Console.WindowWidth));
                        }
                        Console.SetCursorPosition(0, 2);
                        Console.WriteLine("Joueur 2");
                        Console.WriteLine("------------------");
                        Console.WriteLine(plateau.ToString());
                        //jeu_2.Tour(secondes);
                        current_game = game_player2;
                    }
                    Console.WriteLine("Saisir un mot");
                    draw = true;
                } else
                {
                    Console.WriteLine("La partie est finie, appuyez sur une touche pour connaître les résultats");
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
            Joueur player1 = new Joueur(mots_trouves1, 0, "Anne");
            List<string> mots_trouves2 = new List<string>();
            Joueur player2 = new Joueur(mots_trouves2, 0, "Victor");

            // Création du plateau
            plateau = new Plateau();

            // Création du jeu
            game_player1 = new Jeu(plateau, liste_dico, player1);
            game_player2 = new Jeu(plateau, liste_dico, player2);

            current_game = game_player1;

            // Début du jeu

            Timer chrono_jeu = new System.Timers.Timer(minute); // Toutes les 10 secondes
            chrono_jeu.AutoReset = true;
            chrono_jeu.Enabled = true;
            chrono_jeu.Elapsed += ChronoMinutes; // Routine du timer

            Timer temps_restant = new System.Timers.Timer(seconde); // Toutes les secondes
            temps_restant.AutoReset = true;
            temps_restant.Enabled = true;
            temps_restant.Elapsed += ChronoSecondes; // Routine du timer

            draw = false;
            chrono_jeu.Start();
            temps_restant.Start();


            for (int i = 3; i < 8; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Joueur 1");
            Console.WriteLine("------------------");
            Console.WriteLine(plateau.ToString());
            Console.WriteLine("Saisir un mot");
            draw = true;

            while (minutes_elapsed < 6)
            {
                Console.SetCursorPosition(0, 15);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 15);
                word = Console.ReadLine().ToUpper();
                current_game.Tour(secondes_elapsed, word);
            } 

            chrono_jeu.Stop();
            temps_restant.Stop();
            Console.Clear();
            Ending();
            Console.WriteLine(player1.ToString());
            Console.WriteLine(player2.ToString());


            

        }
    }
}
