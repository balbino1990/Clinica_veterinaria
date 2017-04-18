using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vets.Models
{
    public class Veterinarios
    {
        public Veterinarios()
        {
            Consultas = new HashSet<Consultas>();
        }

        [Key] // Força a criação do PK
        public int VeterinarioID { get; set; }                  //int é um tipo de dados nativo

        // O nome é prechimento obrigatorio
        [Required]
        [StringLength(30)]      // O tamanho maximo do atributo nome é de 30 caracteres
        public string Nome { get; set; }

        [StringLength(50)]
        public string Rua { get; set; }

        [StringLength(10)]
        public string NumPorta { get; set; }

        [StringLength(10)]
        public string Andar { get; set; }

        [StringLength(30)]
        public string CodPostal { get; set; }

        [StringLength(9)]
        public string NIF { get; set; }

        [Column(TypeName = "date")] //formato o tipo de dados na BD
        public DateTime? DataEntradaClinica { get; set; } // o '?', significa não é obrogatorio o preenchimento

        [Required]
        [StringLength(30)]
        public string NumCedulaProf { get; set; }

        [Column(TypeName = "date")] 
        public DateTime? DataInscOrdem { get; set; } 

        [StringLength(50)]
        public string Faculdade { get; set; }

        public virtual ICollection<Consultas> Consultas { get; set; }
    }
}