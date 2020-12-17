using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle_V0
{
    /// <summary>
    /// La classe plateau représente le plateau de dés sur lequel le joueur va chercher des mots.
    /// </summary>
    /// <remarks>
    /// La face visible de chaque dé est différente.
    /// </remarks>
    public class Plateau
    {
        /// <summary>
        /// Variable d'instance
        /// </summary>
        List<De> de_plateau;

        /// <summary>
        /// Le constructeur de la classe remplit le plateau.
        /// </summary>
        public Plateau()
        {
            this.de_plateau = Remplir_Plateau();
        }

        /// <summary>
        /// Rempli le plateau de dés.
        /// </summary>
        /// <returns>Plateau rempli par la liste de dés.</returns>
        public List<De> Remplir_Plateau()
        {
            List<De> des = new List<De>();
            for (int i = 0; i < 16; i++)
            {
                De un_de = new De("Des.txt");
                des.Add(un_de);

            }
            return des;
        }

        /// <summary>
        /// Remélange les dés
        /// </summary>
        public void Shuffle()
        {
            List<char> liste = De.Faces_Visibles;
            liste.Clear();
            De.Faces_Visibles = liste;
            foreach (De de in de_plateau)
            {
                de.Side_Generator();
            }
        }

        /// <summary>
        /// Retourne une chaîne de caractères qui décrit un plateau.
        /// </summary>
        /// <returns>Chaîne de caractères le plateau en string.</returns>
        public override string ToString()
        {
            return (de_plateau[0].Face_Visible + " " + de_plateau[1].Face_Visible + " " + de_plateau[2].Face_Visible + " " + de_plateau[3].Face_Visible + "\n" +
                de_plateau[4].Face_Visible + " " + de_plateau[5].Face_Visible + " " + de_plateau[6].Face_Visible + " " + de_plateau[7].Face_Visible + "\n" +
                de_plateau[8].Face_Visible + " " + de_plateau[9].Face_Visible + " " + de_plateau[10].Face_Visible + " " + de_plateau[11].Face_Visible + "\n" +
                de_plateau[12].Face_Visible + " " + de_plateau[13].Face_Visible + " " + de_plateau[14].Face_Visible + " " + de_plateau[15].Face_Visible);
        }


        /// <summary>
        /// Trouver les positions d'une lettre dans le plateau.
        /// </summary>
        /// <param name="lettre"></param>
        /// <returns>Retourne une liste des positions des lettres dans le plateau.</returns>
        public List<int> Nb_char(char lettre)
        {
            int compteur = 0;
            List<int> positions = new List<int>();

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                         if (this.de_plateau[compteur].Face_Visible.Equals(lettre))
                         {
                             positions.Add(i); positions.Add(j);
                         }

                    compteur++;
                    }
                }
            
            return positions;
        }

        /// <summary>
        /// Teste si le mot passé en paramètre est un mot éligible c’est-à-dire qu’il respecte la contrainte d’adjacence décrite ci-dessus.
        /// </summary>
        /// <param name="previous_x">Position précédente sur x.</param>
        /// <param name="previous_y">Position précédente sur y.</param>
        /// <param name="compteur">Compteur d'itération.</param>
        /// <param name="mot">Mot testé.</param>
        /// <param name="used_de">Tableau des dés utilisés.</param>
        /// <returns>True si le mot passé en paramètre est un mot éligible, false sinon.</returns>

        public bool Test_Plateau(int previous_x, int previous_y, int compteur, string mot, bool[,] used_de)
        {
            // Permet d'arrêter la récursivité si on a testé toutes les lettres
            if (compteur < mot.Length)
            {
                // On récupère les positions d'une lettre
                List<int> positions = Nb_char(mot[compteur]);

                // Index de départ de la liste des positions d'une lettre
                int index_depart = 0;

                // Position d'une lettre et sa distance par rapport à la précédente
                int current_x_pos;
                int current_y_pos;
                int delta_x;
                int delta_y;

                // On vérifie que la lettre est présente i.e elle possède une ou plusieurs valeurs de position
                // dans la liste
                if (!(positions.Count == 0))
                {
                    current_x_pos = positions[index_depart];
                    current_y_pos = positions[index_depart + 1];

                    // La première lettre est forcément bonne donc on initialise la position précédente 
                    // à la même position ainsi la condition de distance sera validée et on fait avancer la récursivité
                    if (compteur == 0)
                    {
                        previous_x = current_x_pos;
                        previous_y = current_y_pos;
                    }

                    // Calcul des distances
                    delta_x = current_x_pos - previous_x;
                    delta_y = current_y_pos - previous_y;

                    while (index_depart < positions.Count - 1)
                    {
                        // On recalcule les distances et les positions (car si l'index change tout change)
                        current_x_pos = positions[index_depart];
                        current_y_pos = positions[index_depart + 1];

                        delta_x = current_x_pos - previous_x;
                        delta_y = current_y_pos - previous_y;

                        // On vérifie que la lettre n'a pas déjà été utilisée
                        if (used_de[current_x_pos, current_y_pos])
                        {
                            // Si la lettre est utilisée, on passe à la lettre suivante sinon on retourne false
                            if (Nb_char(mot[compteur]).Count > 2)
                            {
                                index_depart += 2;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            // On vérifie que la lettre est adjacente grâce à la position
                            if ((delta_x == 0 || delta_x == -1 || delta_x == 1) && (delta_y == 0 || delta_y == -1 || delta_y == 1))
                            {

                                used_de[current_x_pos, current_y_pos] = true;
                                return (Test_Plateau(current_x_pos, current_y_pos, compteur + 1, mot, used_de));

                            }
                            // Sinon on passe à le prochaine occurence de la lettre dans le plateau si elle n'existe pas on retourne false
                            
                            else if (Nb_char(mot[compteur]).Count > 2)
                            {
                                index_depart += 2;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    return false;
                }
                return false;
            }
            return true;
        }

      
    }
}