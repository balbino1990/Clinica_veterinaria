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
        [RegularExpression("[A-Z][a-záéíóúâêîôûàèìòùãõäëïöüçñ]+((( )|(-)|((e|de|da|dos) )|( d'))|[A-Z][a-záéíóúâêîôûàèìòùãõäëïöüçñ]+){1,3}", ErrorMessage ="Deve escrever o {0} só com letras")]  //Expressão regular é um filtro que válida o atributo.........[] é para agrupar os simbolos aceitaveis
        //    \w significa aceitar um valor alfa-númerico (letras ou algarismos)
        //      ? significa zero ou uma, + significa uma ou mais, * significa zero ou mais
        public string Nome { set; get; }


        [RegularExpression("[0-9]{9}", ErrorMessage = "O campo {0} só aceitar 9 algarismos")]
        [Required(ErrorMessage = "O {0} é preenchimento obrigatoria....")]
        [Display(Name = "NIF do Dono")]
        public string NIF { get; set; }


        // criar uma 'ponte' entre a BD do negocio e a BD de autenticaçaõ
        public string NomeUtilizadores { get; set; }




        // especificar que um DONO tem muitos ANIMAIS
        public ICollection<Animais> ListaDeAnimais { get; set; }
    }
}