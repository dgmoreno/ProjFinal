using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _4fitClub.ViewModels
{
    public class EditCategoriaViewModel : IValidatableObject
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Descricao { get; set; }

        public HttpPostedFileBase Imagem { get; set; }

        public string ImagemAtual { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Imagem != null && 
                Imagem.ContentLength > 0 && 
                Imagem.ContentType.Split('/').First() != "image")
            {
                yield return new ValidationResult(
                    "Só são aceites imagens.",
                    new[] { nameof(Imagem)}
                    );
            }
        }
    }
}