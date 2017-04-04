using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vets.Models
{
    public class Donos
    {
        //os atributos da tabela

        //[Key]
        public int DonosID { get; set; } //get e set====>para convocar os metodos getters e setters

        //[Required]
        public string Nome { set; get; }

        public string Morada { get; set; }

        public string CodPostal { get; set; }

        //[Required]
        public string NIF { get; set; }

        public double Altura { get; set; }

        public int Idade { get; set; }
    }
}