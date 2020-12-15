using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace Boogle_V0
{
    class ParamJeux
    {
        public List<Jeu> Jeux { get; set; }

        public ParamJeux(List<Jeu> jeux)
        {
            Jeux = jeux;
        }
    }
}