using System;
using System.Collections.Generic;

namespace Boogle_V0
{
    class Program
    {
        static void Main(string[] args)
        {

            /*Plateau new_de = new Plateau();
            Console.WriteLine(new_de.ToString());*/

            // Dictionnaire dico = new Dictionnaire(Dictionnaire.readfile("y"), "français");
            // dico.deux();

            List<string> lmots = new List<string>();
            lmots=Dictionnaire.Creation_Dico(2);
            Dictionnaire dico = new Dictionnaire(lmots, 2, "français");
            Console.WriteLine(dico.ToString());

        }
    }
}
