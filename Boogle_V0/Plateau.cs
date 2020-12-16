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

        public override string ToString()
        {
            return (de_plateau[0].Face_Visible + " "+de_plateau[1].Face_Visible + " " + de_plateau[2].Face_Visible + " " + de_plateau[3].Face_Visible + "\n" + 
                de_plateau[4].Face_Visible + " " + de_plateau[5].Face_Visible + " " + de_plateau[6].Face_Visible + " " + de_plateau[7].Face_Visible + "\n" +
                de_plateau[8].Face_Visible + " " + de_plateau[9].Face_Visible + " " + de_plateau[10].Face_Visible + " " + de_plateau[11].Face_Visible + "\n" +
                de_plateau[12].Face_Visible + " " + de_plateau[13].Face_Visible + " " + de_plateau[14].Face_Visible + " " + de_plateau[15].Face_Visible );
        } 

        public int y_pos(char face)
        {
            int compteur=0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4;j++)
                {
                    if (de_plateau[compteur].Face_Visible.Equals(face)) return j;
                    compteur++;
                }
                
                
            }
            return -10;
        }

        public int x_pos(char face)
        {
            int compteur = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (de_plateau[compteur].Face_Visible.Equals(face)) return i;
                    compteur++;
                }
            }
            return -10;
        }

        public bool Test_Plateau(int previous_x, int previous_y, int compteur, string mot, bool[,] used_de)
        {

            if (compteur < mot.Length)
            {
                int delta_x = x_pos(mot[compteur]) - previous_x;
                int delta_y = y_pos(mot[compteur]) - previous_y;

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
                        }
                            
                    }

                    return false;

                }

            }

            return true;
        }
    }

}
