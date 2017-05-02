using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vets.Models
{
    public class Donos
    {
        public Donos()
        {
            ListaDeAnimais = new HashSet<Animais>();
        }


        [Key]   //indica que o atributo é o PK
        [DatabaseGenerated(DatabaseGeneratedOption.None)]  //quando usado, inibe o atributo de ser Auto Number
        [Display(Name = "Identificador do Dono")]
        public int DonoID { get; set; }

        [Required(ErrorMessage = "O {0} é preenchimento obrigatorio...")]
        [Display(Name = "Nome do Dono")]
        public string Nome { set; get; }

        [Required(ErrorMessage = "O {0} é preenchimento obrigatoria....")]
        [Display(Name = "NIF do Dono")]
        public string NIF { get; set; }

        // especificar que um DONO tem muitos ANIMAIS
        public ICollection<Animais> ListaDeAnimais { get; set; }
    }
}