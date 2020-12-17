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
    class Plateau
    {
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

        public void Shuffle()
        {
            foreach(De de in de_plateau)
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
            return (de_plateau[0].Face_Visible + " "+de_plateau[1].Face_Visible + " " + de_plateau[2].Face_Visible + " " + de_plateau[3].Face_Visible + "\n" + 
                de_plateau[4].Face_Visible + " " + de_plateau[5].Face_Visible + " " + de_plateau[6].Face_Visible + " " + de_plateau[7].Face_Visible + "\n" +
                de_plateau[8].Face_Visible + " " + de_plateau[9].Face_Visible + " " + de_plateau[10].Face_Visible + " " + de_plateau[11].Face_Visible + "\n" +
                de_plateau[12].Face_Visible + " " + de_plateau[13].Face_Visible + " " + de_plateau[14].Face_Visible + " " + de_plateau[15].Face_Visible );
        }

        /// <summary>
        /// Postition sur y.
        /// </summary>
        /// <param name="face">Face dont l'on cherche la position.</param>
        /// <returns>Position sur y en int.</returns>
        public int y_pos(char face)
        {
            int compteur=0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4;j++)
                {
                    if (this.de_plateau[compteur].Face_Visible.Equals(face)) return j;
                    compteur++;
                }
                
                
            }
            return -10;
        }

        /// <summary>
        /// Position sur x.
        /// </summary>
        /// <param name="face">Face dont l'on cherche la position.</param>
        /// <returns>Position sur x en int.</returns>
        public int x_pos(char face)
        {
            int compteur = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (this.de_plateau[compteur].Face_Visible.Equals(face)) return i;
                    compteur++;
                }
            }
            return -10;
        }



        public List<int> Nb_char(char lettre)
        {
            List<int> positions = new List<int>();
            foreach(De de in de_plateau)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (de.Face_Visible.Equals(lettre))
                        {
                            positions.Add(i); positions.Add(j);
                        }
                            
                    }
                }
                
            }
            return positions ;
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

            if (compteur < mot.Length)
            {
                int current_x_pos = x_pos(mot[compteur]);
                int current_y_pos = y_pos(mot[compteur]);

                int delta_x = current_x_pos - previous_x;
                int delta_y = current_y_pos - previous_y;

                if (x_pos(mot[compteur]) != -10 && y_pos(mot[compteur]) != -10)
                {
                    if (used_de[x_pos(mot[compteur]), y_pos(mot[compteur])])
                    {
                        return false;
                    }
                    else
                    {
                        if ((delta_x == 0 || delta_x == -1 || delta_x == 1) && (delta_y == 0 || delta_y == -1 || delta_y == 1))
                        {

                            used_de[x_pos(mot[compteur]), y_pos(mot[compteur])] = true;
                            return (Test_Plateau(x_pos(mot[compteur]), y_pos(mot[compteur]), compteur +1, mot, used_de));
                        } else if (Nb_char(mot[compteur]).Count > 2) {
                            
                        }
                            
                    }

                    return false;

                }

            }

            return true;
        }
    }

}
