using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Boogle_V0;
using System.Linq;



namespace Boogle_V0
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Creation_Dico()
        {
            // creation de la liste attendu
            List<string> mots = new List<string> { "AA", "AH", "AI", "AN", "AS", "AU", "AY", "BI", "BU", "CA", "CE", "CI", "DA", "DE", "DO", "DU", "EH", "EN", "ES", "ET", "EU", "EX", "FA", "FI", "GO", "HA", "HE", "HI", "HO", "IF", "IL", "IN", "JE", "KA", "LA", "LE", "LI", "LU", "MA", "ME", "MI", "MU", "NA", "NE", "NI", "NO", "NU", "OC", "OH", "ON", "OR", "OS", "OU", "PI", "PU", "RA", "RE", "RI", "RU", "SA", "SE", "SI", "SU", "TA", "TE", "TU", "UN", "US", "UT", "VA", "VE", "VS", "VU", "WU", "XI" };
            List<string> result = Dictionnaire.Creation_Dico(2);
            Assert.AreEqual(mots, result);
        }
        public void RechDichoRecursif()
        {
            //Recherche si un mot appartient au dictionnaire
            bool vrai = true;
            string mot = "ACE";
            bool result = Dictionnaire.RechDichoRecursif(0, 100, mot);
            Assert.AreEqual(vrai, result);
        }
        public void Recup_De()
        {

            List<char> liste = new List<char> { 'R', 'E', 'N', 'I', 'S', 'H', 'T', 'I', 'E', 'A', 'A', 'O', 'D', 'O', 'N', 'E' };
            List<char> result = De.Recup_De(@"Des.txt");
            Assert.AreEqual(liste, result);
        }
        public void Contain()
        {
            List<string> found_words = new List<string> { "ACE", "CASSE" };
            Joueur joueur = new Joueur(found_words, 7, "VICTOR");
            bool vrai = true;
            bool result = Joueur.joueur.Contain("ACE");
            Assert.AreEqual(vrai, result);
        }
        public void Add_Mot()
        {
            List<string> vrai = new List<string> { "ACE", "CASSE" };
            List<string> result = new List<string> { "ACE" };
            result.Add("CASSE");
            Assert.AreEqual(vrai, result);
        }
    }
}
