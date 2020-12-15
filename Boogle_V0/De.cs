using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Boogle_V0
{
    class De
    {

        List<char> faces; // liste de lettre des faces du dé
        char face_visible; // lettre de la face visible
        static List<char> faces_visibles = new List<char>();

        /// <summary>
        /// Constructeur de la classe De
        /// </summary>
        /// <param name="filename"> chemin ramenant au dossier utilisé pour construire notre dé </param>
        public De(string filename)
        {
            this.faces = Recup_De(filename);
            testFaces();
        }


        /// <summary>
        /// reduire le nombre de lettres similaires sur une face
        /// </summary>
        public void testFaces()
        {
            bool exist = true;
            int timeout = 0;
            this.face_visible = this.faces[Lance()];
            while (exist & faces_visibles.Count != 0)
            {

                this.face_visible = this.faces[Lance()];
                exist = faces_visibles.Contains(this.face_visible);
                timeout++;
                if (timeout > 10000) exist = false;

            }

            faces_visibles.Add(this.face_visible);
        }

        /// <summary>
        /// Nous retourne la lettre d'une face visible en char
        /// </summary>
        public char Face_Visible
        {
            get { return this.face_visible; }
        }

        /// <summary>
        /// permet de tirer au hasard une lettre parmi les 6
        /// </summary>
        /// <returns> le numéro de la lettre retouné en int</returns>
        public int Lance()
        {
            Random r = new Random();
            return r.Next(0, 36) / 6;
        }

        /// <summary>
        /// retourne une chaîne de caractères qui décrit un dé.
        /// </summary>
        /// <returns> chaine de carcatères décrivant le dé en string </returns>
        public override string ToString()
        {
            string description = "";
            foreach (char face in this.faces)
            {
                description = String.Concat(description, face, " ");
            }
            return ("Face visible : " + this.face_visible + "\n Faces : " + description);
        }

        /// <summary>
        /// lecteur du dossier pour construire notre dé
        /// </summary>
        /// <param name="filemane">chemin ramenant au dossier utilisé pour construire notre dé</param>
        /// <returns> liste faces_visibles pour le constructeur</returns>
        public List<char> Recup_De(string filemane)
        {
            
            List<char> liste_de = new List<char>();
            List<string> fichier = new List<string>();

            try
            {
                StreamReader sr = new StreamReader(File.OpenRead(@"Des.txt"));

                if (!sr.EndOfStream)
                {
                    liste_de.Clear();
                    liste_de = sr.ReadLine().ToCharArray().ToList();
                    liste_de.RemoveAll(separator);
                }


                fichier.Clear();
                while (!sr.EndOfStream)
                {
                    fichier.Add(sr.ReadLine());
                }
                sr.Close();
                File.WriteAllText(@"Des.txt", String.Empty);
                StreamWriter sw = new StreamWriter(File.OpenWrite(@"Des.txt"));
                foreach (string ligne in fichier)
                {
                    sw.WriteLine(ligne);
                }
                sw.Close();
                
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
