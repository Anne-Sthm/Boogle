using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle_V0
{
    class Jeu
    {
        SortedList<int, Dictionnaire> mondico;
        Plateau monplateau;
        public Jeu(Plateau monplateau, SortedList<int, Dictionnaire> mondico)
        {
            this.mondico = mondico;
            this.monplateau = monplateau;
        }

        public bool MotEligible(string mot)
        {
            if (mot.Length>=3){
                if (this.mondico[mot.Length].Mots.Contains(mot))
                {
                    bool[,] valide = new bool[4, 4];
                    monplateau.Test_Plateau(monplateau.x_pos(mot[0]), monplateau.y_pos(mot[0]), 0, mot, valide);
                }
            }

            return false;
        }

       /* public void Tour()
        {
            do
            {

            } while 
            Console.WriteLine("Saisir un mot");
            MotEligible(Console.ReadLine());
        }*/
        
    }
}
