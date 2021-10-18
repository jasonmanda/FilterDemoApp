using System;

namespace FilterDemoApp.Models
{
    public class Produit
    {
        public string Id { get; set; }
        public string Libelle { get; set; }
        public int Quantite { get; set; }
        public float Prix { get; set; }
        public bool Etat { get; set; } = false;
    }
}
