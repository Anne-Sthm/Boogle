using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using System.Globalization;
using WMPLib;
using System.IO;


namespace Boogle_V0
{

    class Program
    {
        /// <summary>
        /// Constantes de temps 
        /// </summary>
        const int minute = 2000;
        const int seconde = 1000;

        /// <summary>
        /// Musiques possibles en fond du jeu
        /// </summary>
        const string musique_1 = "Musique_1.mp3";
        const string musique_2 = "Musique_2.mp3";
        const string musique_3 = "Musique_3.mp3";

        /// <summary>
        /// Paramètres par défaut
        /// </summary>
        static string first_player_ID = "Joueur 1";
        static string second_player_ID = "Joueur 2";
        static int volume = 40;
        static int nb_Tours = 6;
        static string musique = musique_1;

        /// <summary>
        /// Gestion du temps écoulé
        /// </summary>
        static int minutes_elapsed;
        static int secondes_elapsed;

        /// <summary>
        /// Variables des jeux et plateaux utilisés
        /// </summary>
        static Jeu first_game;
        static Jeu second_game;
        static Jeu current_game;
        static Plateau first_board;
        static Plateau second_board;

        /// <summary>
        /// Autres variables
        /// </summary>
        static bool draw; // Assure que le dessin du plateau se finisse en bonne et dûe forme
        static object consoleLock; // Bloque les valeurs des variables, utilisé car plusieurs threads
        static string submitted_word; // Mot soumis par l'utilisateur



        /// <summary>
        /// Menu général
        /// </summary>
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Menu :\n"
                                 + "1 : Lancer le Boogle \n"
                                 + "2 : Règles \n"
                                 + "3 : Paramètres \n"
                                 + "4 : Historique des parties \n"
                                 + "Saisissez le nombre correspondant à l'option que vous désirez ou escape pour quitter ");
        }

        /// <summary>
        /// Menu des paramètres
        /// </summary>
        static void Menu_Settings()
        {
            Console.Clear();
            Console.WriteLine("Menu :\n"
                                 + "1 : Saisir les noms \n"
                                 + "2 : Changer le nombre de tours \n"
                                 + "3 : Activer/désactiver la musique \n"
                                 + "4 : Changer de musique \n"
                                 + "Saisissez le nombre correspondant à l'option que vous désirez ou autre chose pour sortir de ce menu ");
        }

        /// <summary>
        /// Musique de fin
        /// </summary>
        static void Ending()
        {
            Console.Clear();
            Console.WriteLine("Calcul des résultats, patientez en musique :)");
            WindowsMediaPlayer player_ending = new WindowsMediaPlayer();
            player_ending.URL = "BRADAFRAMANADAMADA.mp3";
            player_ending.settings.volume = volume;
            player_ending.controls.currentPosition = 13;
            player_ending.controls.play();
            Thread.Sleep(2000);
            Console.Write(" :)");
            Thread.Sleep(2000);
            Console.Write(" :) ");
            Thread.Sleep(2000);
            player_ending.controls.stop();
            
        }
        /// <summary>
        /// Chronomètre du temps restant avant de changer de joueur.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        static void ChronoSecondes(Object source, ElapsedEventArgs e)
        {
            lock (consoleLock)
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

        /// <summary>
        /// Affiche les plateaux pour les différents joueur, récupère leur réponses et annonce quand la partie est finie.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void ChronoMinutes(Object source, ElapsedEventArgs e)
        {
            lock (consoleLock)
            {
                minutes_elapsed++;
                secondes_elapsed = 60;
                Console.Clear();
                draw = false;
                if (minutes_elapsed < nb_Tours)
                {

                    if (minutes_elapsed % 2 == 0)
                    {
                        for (int ligne = 3; ligne < 8; ligne++)
                        {
                            Console.SetCursorPosition(0, ligne);
                            Console.Write(new string(' ', Console.WindowWidth));
                        }
                        Console.SetCursorPosition(0, 2);
                        Console.WriteLine(first_player_ID);
                        Console.WriteLine(new string('-', Console.WindowWidth));
                        first_board.Shuffle();
                        Console.WriteLine(first_board.ToString());
                        current_game = first_game;
                    }
                    else
                    {
                        for (int ligne = 3; ligne < 8; ligne++)
                        {
                            Console.SetCursorPosition(0, ligne);
                            Console.Write(new string(' ', Console.WindowWidth));
                        }
                        Console.SetCursorPosition(0, 2);
                        Console.WriteLine(second_player_ID);
                        Console.WriteLine(new string('-', Console.WindowWidth));
                        second_board.Shuffle();
                        Console.WriteLine(second_board.ToString());
                        current_game = second_game;
                    }
                    Console.WriteLine("Saisir un mot");
                    Console.SetCursorPosition(0, 15);
                    draw = true;
                }
                else
                {
                    Console.SetWindowSize(75, 7);
                    Console.WriteLine("La partie est finie, appuyez sur une touche pour connaître les résultats");
                }
            }
        }
        static void Main(string[] args)
        {

            
            do
            {
                try
                {
                    Console.Clear();
                    Menu();
                    Console.SetWindowSize(85, 25);
                    switch (Int32.Parse(Console.ReadLine()))
                    {
                        case 1: // jeu

                            Console.Clear();
                            consoleLock = new object();

                            // Musique de fond
                            WindowsMediaPlayer player_background = new WindowsMediaPlayer();
                            player_background.URL = musique;
                            player_background.controls.play();
                            player_background.settings.volume = volume;

                            // Création des dictionnaires
                            SortedList<int, Dictionnaire> liste_dico = new SortedList<int, Dictionnaire>();
                            for (int taille_dico = 2; taille_dico < 16; taille_dico++)
                            {
                                List<string> dico_words = Dictionnaire.Creation_Dico(taille_dico);
                                Dictionnaire dico = new Dictionnaire(dico_words, taille_dico, "français");
                                liste_dico.Add(taille_dico, dico);
                            }

                            // Création des joueurs
                            List<string> found_words_first_player = new List<string>();
                            Joueur first_player = new Joueur(found_words_first_player, 0, first_player_ID);
                            List<string> found_words_second_player = new List<string>();
                            Joueur second_player = new Joueur(found_words_second_player, 0, second_player_ID);

                            // Création du plateau
                            first_board = new Plateau();
                            second_board = new Plateau();

                            // Création du jeu
                            first_game = new Jeu(first_board, liste_dico, first_player);
                            second_game = new Jeu(second_board, liste_dico, second_player);
                            current_game = first_game;

                            // Début du jeu

                            System.Timers.Timer chrono_jeu = new System.Timers.Timer(minute); // Toutes les 60 secondes
                            chrono_jeu.AutoReset = true;
                            chrono_jeu.Enabled = true;
                            chrono_jeu.Elapsed += ChronoMinutes; // Routine du timer

                            System.Timers.Timer temps_restant = new System.Timers.Timer(seconde); // Toutes les secondes
                            temps_restant.AutoReset = true;
                            temps_restant.Enabled = true;
                            temps_restant.Elapsed += ChronoSecondes; // Routine du timer

                            
                            draw = false;
                            minutes_elapsed = 0;
                            secondes_elapsed = 60;

                            Console.SetWindowSize(30, 18);

                            chrono_jeu.Start();
                            temps_restant.Start();
                           
                            for (int ligne = 3; ligne < 8; ligne++)
                            {
                                Console.SetCursorPosition(0, ligne);
                                Console.Write(new string(' ', Console.WindowWidth));
                            }
                            Console.SetCursorPosition(0, 2);
                            Console.WriteLine(first_player_ID);
                            Console.WriteLine(new string('-', Console.WindowWidth));
                            Console.WriteLine(first_board.ToString());
                            Console.WriteLine("Saisir un mot");
                            draw = true;

                            while (minutes_elapsed < nb_Tours)
                            {
                                Console.SetCursorPosition(0, 15);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 15);
                                submitted_word = Console.ReadLine().ToUpper();
                                current_game.Tour(submitted_word);
                            }

                            chrono_jeu.Stop();
                            temps_restant.Stop();
                            Console.Clear();
                            player_background.controls.stop();
                            Ending();
                            Console.Clear();
                            Console.SetWindowSize(85, 25);
                            Console.WriteLine(first_player.ToString());
                            Console.WriteLine(second_player.ToString());
                            Console.WriteLine("LE GAGNANT EST : " + (first_player.Score == second_player.Score ? "Aucun (égalité)" : (first_player.Score > second_player.Score ? first_player_ID : second_player_ID))+" !!!");


                            // On écrit dans l'historique les données de la partie qui vient de se terminer
                            StreamWriter writer = new StreamWriter(File.OpenWrite(@"Historique.txt"));
                            try
                            {
                                DateTime date_ajd = DateTime.Now;
                                if (first_player.Score > second_player.Score)
                                {
                                    writer.WriteLine(date_ajd);
                                    writer.WriteLine(first_player.ToString());
                                    writer.WriteLine(second_player.ToString());
                                    writer.WriteLine(first_player.Nom);
                                    writer.WriteLine("----------------");
                                }

                                else if (first_player.Score == second_player.Score)
                                {
                                    writer.WriteLine(date_ajd);
                                    writer.WriteLine(first_player.ToString());
                                    writer.WriteLine(second_player.ToString());
                                    writer.WriteLine("egalité");
                                    writer.WriteLine("----------------");
                                }
                                else
                                {
                                    writer.WriteLine(date_ajd);
                                    writer.WriteLine(first_player.ToString());
                                    writer.WriteLine(second_player.ToString());
                                    writer.WriteLine(second_player.Nom);
                                    writer.WriteLine("----------------");
                                }
                                writer.Close();

                                Console.WriteLine("Appuyez sur une touche pour sortir");
                                Console.ReadKey();

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.ReadKey();
                            }
                           
                            break;

                        case 2: // Règles
                            Console.Clear();
                            Console.WriteLine("Règles : ");
                            Console.WriteLine(" Vous devez trouver un maximum de mots en formant des chaînes de  lettres contiguës." +
                                "\nPlus le mot est long, plus les points qu'il vous rapporte sont importants." +
                                "\nVous pouvez passer d'une lettre à la suivante située directement à gauche, à droite, en haut, en bas, ou sur l'une des quatres cases diagonales." +
                                "\nUne lettre ne peut pas être utilisée plus d'une fois pour un même mot." +
                                "\nSeuls les mots de trois lettres ou plus comptent." +
                                "\nLes accents ne sont pas importants. E peut être utilisé comme Ê, É, È etc.");
                            Console.WriteLine("\nAppuyez sur une touche pour sortir");
                            Console.ReadKey();
                            break;

                        case 3: // Paramètes
                            Console.Clear();
                            Menu_Settings();
                            switch (Int32.Parse(Console.ReadLine()))
                            {
                                case 1: // Changer le nom des joueurs
                                    Console.Clear();
                                    Console.WriteLine("Le nom actuel du joueur 1 est : " + first_player_ID + "\nEntrez le nouveau nom du joueur 1 : ");
                                    first_player_ID = Console.ReadLine();
                                    Console.Clear();
                                    Console.WriteLine("Le nom actuel du joueur 2 est : " + second_player_ID + "\nEntrez le nouveau nom du joueur 2 : ");
                                    second_player_ID = Console.ReadLine();

                                    break;
                                case 2: // Chenger le nombre de tours
                                    Console.Clear();
                                    Console.WriteLine("Le nombre de tours actuel par personne est de : " + nb_Tours / 2);
                                    Console.WriteLine("Saisissez le nouveau nombre de tours par personnes");
                                    nb_Tours = Int32.Parse(Console.ReadLine()) * 2;
                                    break;
                                case 3: // Activer/Désactiver musique
                                    Console.Clear();
                                    Console.WriteLine("Paramètre son :");
                                    Console.WriteLine("1 : Activer le son");
                                    Console.WriteLine("2 : Désactiver le son ");
                                    Console.WriteLine("Autre : Sortir");
                                    int Choix = Convert.ToInt32(Console.ReadLine());
                                    if (Choix == 1) volume = 30;
                                    if (Choix == 2) volume = 0;

                                    break;
                                case 4: // Changer de musique
                                    Console.Clear();
                                    Console.WriteLine("Musique : ");
                                    Console.WriteLine("1 : Musique par défaut");
                                    Console.WriteLine("2 : Electro ");
                                    Console.WriteLine("3 : Jazz ");
                                    Console.WriteLine("Autre : Sortir");
                                    int Music_Choice = Convert.ToInt32(Console.ReadLine());
                                    if (Music_Choice == 1) musique = musique_1;
                                    if (Music_Choice == 2) musique = musique_2;
                                    if (Music_Choice == 3) musique = musique_3;
                                    
                                    break;
                                default:
                                    Console.WriteLine("Il n'y a pas d'options correspondant à ce nombre");
                                    break;

                            }
                            break;
                        case 4:

                            Console.Clear();
                            Console.WriteLine("Historique :");
                            try
                            {
                                StreamReader reader = new StreamReader(File.OpenRead(@"Historique.txt"));
                                while (!reader.EndOfStream)
                                {
                                    Console.WriteLine(reader.ReadLine());
                                    
                                }

                                reader.Close();
                                
                            }
                            catch (FileNotFoundException)
                            {
                                Console.WriteLine("Le fichier n'existe pas encore, veuillez au préalable lancer une partie.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            Console.WriteLine("Appuyez sur une touche pour sortir");
                            Console.ReadKey();
                            break;

                        default:

                            Console.WriteLine("Il n'y a pas d'options correspondant à ce nombre");
                            break;
                    }
                    Console.Clear();
                    Console.WriteLine("Tapez escape pour sortir ou sur une autre touche pour retourner au menu principal");


                }
                // IO Exception lors de la saisie
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(" Erreur de saisie, veuillez saisir ce qui vous est demandé ");
                    Console.WriteLine("Détails de l'exception : \n" + e);
                }

            } while (Console.ReadKey().Key != ConsoleKey.Escape);

        }
    }
}
