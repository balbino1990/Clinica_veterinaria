using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vets.Models
{
    public class Donos
    {
        public Donos()
        {
            //construtor da classe, que vai ser utilizado para inicializar o atributo 'ListaDeAnimais'
            ListaDeAnimais = new HashSet<Animais>();
        }

        //os atributos da tabela

        //[Key]
        public int DonosID { get; set; } //get e set====>para convocar os metodos getters e setters

        //[Required]
        public string Nome { set; get; }

        //[Required]
        public string NIF { get; set; }

        //relacionar os 'Donos' com os 'Animais'
        // 1 Dono tem Muitos Animais
        public virtual ICollection<Animais> ListaDeAnimais { get; set; }
    }
}