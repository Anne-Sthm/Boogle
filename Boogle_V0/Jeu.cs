using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle_V0
{
    class Jeu
    {
        List<Dictionnaire> mondico = new List<Dictionnaire>();
        Plateau monplateau;
        public Jeu(Plateau monplateau, List<Dictionnaire> mondico)
        {
            this.mondico = mondico;
            this.monplateau = monplateau;
        }


    }
}
