using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4fitClub.ViewModels
{
    public class CreateCategoriaViewModel : IValidatableObject
    {

        [Required]
        [RegularExpression("[A-ZÂÍ][a-záéíóúãõàèìòùâêîôûäëïöüç.]+",
            ErrorMessage = "A categoria deve começar com letra maiúscula, seguida de letra minúscula...")]
        [StringLength(25)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public HttpPostedFileBase Imagem { get; set; }

        /// <summary>
        /// Validação custom.
        /// 
        /// Aqui estou a fazer validação do tipo da imagem do agente.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Imagem.ContentType.Split('/').First() != "image")
            {
                yield return new ValidationResult(
                    "Só são aceites imagens.",
                    new[] { nameof(Imagem) } // O 'nameof(Variavel)' dá o nome da variável, como string.
                    );

            }

        }
    }
}