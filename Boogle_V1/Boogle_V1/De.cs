using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Boogle
{
    class De
    {
        List<char> faces;
        char face_visible;
        public De(string filename)
        {
            this.faces = Recup_De(filename);
            Random r = new Random();
            this.face_visible = faces[Lance(r)];
        }

        public char Face_Visible{
            get {return this.face_visible;}
        }

        public int Lance(Random r)
        {
            return r.Next(6);
        }

        public override string ToString()
        {
            string description = "";
            foreach(char face in this.faces)
            {
                description = String.Concat(description, face, " ");
            }
            return ("Face visible : " + this.face_visible + "\n Faces : " + description);
        }
        
        public List<char> Recup_De(string filemane)
        {
            
            List<char> liste_de = new List<char>();
            List<string> fichier = new List<string>();
            try
            {
                StreamReader sr = new StreamReader(File.OpenRead(@"De.txt"));

                if (!sr.EndOfStream)
                {
                    liste_de.Clear();
                    liste_de = sr.ReadLine().ToCharArray().ToList();
                    liste_de.RemoveAll(separator);
                }
                sr.Close();

                while (!sr.EndOfStream)
                {
                    fichier.Clear();
                    fichier.Add(sr.ReadLine());
                }
                StreamWriter sw = new StreamWriter(File.OpenWrite(@"De.txt"));
                fichier[0] = "";
                foreach (string ligne in fichier)
                {
                    if(!ligne.Equals("")) sw.WriteLine(ligne);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return liste_de;
        }

        private static bool separator(char c)
        {
            return c.Equals(';');
        }
    }
}
