using System;
using System.Collections.Generic;

namespace Boogle_V0
{
    class Program
    {
        static void Main(string[] args)
        {
            /* for(int i = 2; i < 16; i++)
             {
                 List<string> lmots = new List<string>();
                 lmots = Dictionnaire.Creation_Dico(i);
                 Dictionnaire dico = new Dictionnaire(lmots, i, "français");
             }*/


            List<string> lmots = new List<string>();
            lmots = Dictionnaire.Creation_Dico(15);
            Dictionnaire dico = new Dictionnaire(lmots, 15, "français");
            Console.WriteLine(dico.RechDichoRecursif(0, lmots.Count-1, "INNOCENTERAIENT"));


             


        }
    }
}
