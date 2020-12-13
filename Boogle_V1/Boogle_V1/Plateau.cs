using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle
{
    // CONSOLIDATION DE FACES DIFF
    class Plateau
    {
        List<De> de_plateau;
        public Plateau(List<De> de_plateau)
        {
            this.de_plateau = de_plateau;
        }

        public override string ToString()
        {
            return (de_plateau[0].Face_Visible + " "+de_plateau[1].Face_Visible + " " + de_plateau[2].Face_Visible + " " + de_plateau[3].Face_Visible + " " +
                de_plateau[4].Face_Visible + " " + de_plateau[5].Face_Visible + " " + de_plateau[6].Face_Visible + " " + de_plateau[7].Face_Visible + " " +
                de_plateau[8].Face_Visible + " " + de_plateau[9].Face_Visible + " " + de_plateau[10].Face_Visible + " " + de_plateau[11].Face_Visible + " " +
                de_plateau[12].Face_Visible + " " + de_plateau[13].Face_Visible + " " + de_plateau[14].Face_Visible + " " + de_plateau[15].Face_Visible );
        } 

        public int y_pos(char face)
        {
            int compteur=0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4;j++)
                {
                    if (de_plateau[compteur].Equals(face)) return j;
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
                    if (de_plateau[compteur].Equals(face)) return i;
                    compteur++;
                }
            }
            return -10;
        }
    

        public bool Test_Plateau(int previous_x, int previous_y, int compteur, string mot)
        {
            if (compteur < 0)
            {

                int delta_x = x_pos(mot[compteur]) - previous_x;
                int delta_y = y_pos(mot[compteur]) - previous_y;

                if (x_pos(mot[compteur]) != -10 && y_pos(mot[compteur]) != -10)
                {
                    if (delta_x == 0 && (delta_y == delta_x))
                    {
                        return false;
                    }
                    else
                    {
                        if ((delta_x == 0 || delta_x == -1 || delta_x == 1) && (delta_y == 0 || delta_y == -1 || delta_y == 1)) return (Test_Plateau(x_pos(mot[compteur]), y_pos(mot[compteur]), compteur - 1, mot));
                    }

                    return false;

                }

            }

            return true;
        }
    }

}
