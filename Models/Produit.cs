using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FilterDemoApp.Models
{
    public class Produit
    {
        public string Id { get; set; }
        [Required]
        public string Libelle { get; set; }
        public int Quantite { get; set; }
        public float Prix { get; set; }
        public bool Etat { get; set; } = false;
    }
}
