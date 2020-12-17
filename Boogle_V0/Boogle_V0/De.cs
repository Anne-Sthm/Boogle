using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Boogle_V0
{
    
    /// <summary>
    /// La classe Dé sert à construire les différents dés du Boogle.
    /// </summary>
    public class De
    {
        /// <summary>
        /// Variables d'instance
        /// </summary>
        List<char> faces; // liste des lettres sur les faces du dé courant
        char face_visible; // lettre de la face visible

        /// <summary>
        /// Variable de classe
        /// </summary>
        static List<char> liste_de_faces_visibles = new List<char>();


        /// <summary>
        /// Constructeur de la classe De.
        /// </summary>
        /// <param name="filename"> Chemin ramenant au dossier utilisé pour construire notre dé. </param>
        public De(string filename)
        {
            this.faces = Recup_De(filename);
            Side_Generator();
        }


        /// <summary>
        /// Réduire le nombre de lettres similaires sur les faces du plateau.
        /// </summary>
        public void Side_Generator()
        {
            // On regarde si la lettre est déjà une face visible d'un autre dé 
            bool exist = false;
            // Nombre de lancé qui ermet de sortir de la boucle si toutes les faces du dé courant sont déjà sorties
            int timeout = 0; 

            // Lance le dé une premièe fois (permet de ne pas avoir une liste vide si c'est
            // le premier dé lancé
            this.face_visible = this.faces[Lance()];

            // Tant que la face est dans la liste on relance le dé
            while (exist & liste_de_faces_visibles.Count != 0)
            {
                // Lance le dé
                this.face_visible = this.faces[Lance()];
                // On vérifie que le dé n'est pas dans la liste 
                exist = liste_de_faces_visibles.Contains(this.face_visible);

                // On incrémente le nombre de lancés afin de sortir de la boucle au bout de 10000 lancés
                timeout++;
                if (timeout > 10000) exist = false;

            }

            // Ajout du dé à la liste
            liste_de_faces_visibles.Add(this.face_visible);
        }

        /// <summary>
        /// Nous retourne la lettre d'une face visible.
        /// </summary>
        public char Face_Visible
        {
            get { return this.face_visible; }
        }

        /// <summary>
        /// Nous retoune la liste des faces visibles des dés.
        /// </summary>
        public static List<char> Faces_Visibles
        {
            get { return liste_de_faces_visibles; }
            set { liste_de_faces_visibles = value; }
        }

        /// <summary>
        /// Permet de tirer au hasard une des 6 faces du dés.
        /// </summary>
        /// <returns> L'index de la face du dé.</returns>
        public int Lance()
        {
            Random r = new Random();
            return r.Next(0,6);
        }

        /// <summary>
        /// Retourne une chaîne de caractères qui décrit un dé.
        /// </summary>
        /// <returns> Chaine de carcatères décrivant le dé en string. </returns>
        public override string ToString()
        {
            string string_liste_faces = "";
            foreach (char face in this.faces)
            {
                string_liste_faces = String.Concat(string_liste_faces, face, " ");
            }
            return ("Face visible : " + this.face_visible + "\n Faces : " + string_liste_faces);
        }

        /// <summary>
        /// Lecteur du fichier pour construire notre dé.
        /// </summary>
        /// <param name="filemane">Chemin ramenant au fichier utilisé pour construire notre dé.</param>
        /// <returns> Liste des faces du dé pour le constructeur.</returns>
        public List<char> Recup_De(string filename)
        {

            List<char> liste_faces = new List<char>();
            List<string> ligne_fichier = new List<string>();

            try
            {
                // Ouvre un flux en lecture du fichier et le lit
                StreamReader sr = new StreamReader(File.OpenRead(@filename));

                // Prend la première ligne du fichier et la stocke dans une liste 
                if (!sr.EndOfStream)
                {
                    liste_faces.Clear();
                    // Conversion de la ligne lue en tableau puis en liste de caractères et supprime le séparateur (;)
                    liste_faces = sr.ReadLine().ToCharArray().ToList();
                    liste_faces.RemoveAll(Is_Separator);
                }

                // Stocke les lignes restantes du fichier dans une liste et les réécrits
                ligne_fichier.Clear();
                while (!sr.EndOfStream)
                {
                    ligne_fichier.Add(sr.ReadLine());
                }
                sr.Close(); // Ferme le flux en lecture
                File.WriteAllText(@filename, String.Empty);
                StreamWriter writer = new StreamWriter(File.OpenWrite(@filename));
                foreach (string ligne in ligne_fichier)
                {
                    writer.WriteLine(ligne);
                }
                writer.Close(); // Ferme le flux en écriture

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return liste_faces;
        }

        /// <summary>
        /// Predicat qui vérifie si le caractère 'c' est un ';'.
        /// </summary>
        /// <param name="c">Caractère testé</param>
        /// <returns></returns>
        private static bool Is_Separator(char c)
        {
            return c.Equals(';');
        }
    }
}
