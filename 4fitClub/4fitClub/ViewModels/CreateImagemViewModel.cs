using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4fitClub.ViewModels
{
    public class CreateImagemViewModel : IValidatableObject
    {
        /// <summary>
        /// Nome da imagem, ou seja é o nome da imagem a ser carregada
        /// NOTA: Inicialmente era para ser só o nome mas decidi colocar 
        /// a imagem  
        /// </summary>
        
        [Required]
        public HttpPostedFileBase Nome { get; set; }

        [Required]
        public int Ordem { get; set; }

        [Required]
        public string Tipo { get; set; }

        public int ExercicioFK { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Nome.ContentType.Split('/').First() != "image")
            {
                yield return new ValidationResult(
                    "Só são aceites imagens.",
                    new[] { nameof(Nome) } // O 'nameof(Variavel)' dá o nome da variável, como string.
                    );

            }            
        }
    }
}