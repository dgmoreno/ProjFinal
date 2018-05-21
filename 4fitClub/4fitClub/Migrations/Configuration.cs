namespace _4fitClub.Migrations
{
    using _4fitClub.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_4fitClub.Models.ExercicioDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(_4fitClub.Models.ExercicioDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //******************************************************************************************
            //adiciona Categorias
            /*var categorias = new List<Categorias> {
                new Categorias {ID=1, Nome="Cardio/Resistência", Descricao="Exercicios aeróbicos, com foco em resistência",
                Imagem="Cardio.jpg"},
                new Categorias {ID=2, Nome="Musculação", Descricao="Exercícios com foco na estrutura muscular",
                Imagem="Musculacao.jpg"},
                new Categorias {ID=3, Nome="Flexibilidade", Descricao="Exercícios que ajudam a melhorar a flexibilidade",
                Imagem="flexibilidade.jpg"},
                new Categorias {ID=4, Nome="Equilíbrio", Descricao="Exercícios para melhorar o equilibrio e a agilidade",
                Imagem="Equilibrio.jpg"},
                new Categorias {ID=5, Nome="Calístenia", Descricao="Exercícios que utilizam um grande conjuto de músculos do corpo",
                Imagem="Calistenia.jpg"},
            };
            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            //****************************************************************************************
            // adiciona Planos
            var planos = new List<Planos> {
                new Planos {ID=1, Nome="Projeto Verão", Descricao="É desta que fico Fit"},
            };
            planos.ForEach(pp => context.Planos.AddOrUpdate(p => p.Nome, pp));
            context.SaveChanges();

            //********************************************************************************************
            // adiciona Exercicios
            var exercicios = new List<Exercicios> {
                new Exercicios {ID=1, Nome="Flexão", Objetivo="Trabalhar a zona peitoral e triceps",
                Passos="Coloque o corpo numa posição de prancha, alinhado e com braços e pernas estendidos. Flete apenas os braços, até tocar com o peito e a cintura no chão. Estenda de novo apenas os braços, até voltar à posição inicial.",
                , CategoriaFK=2},
                 new Exercicios {ID=2, Nome="Shadow Box", Objetivo="Resistência",
                Passos="Faça movimentos de boxe no ar, pelo menos durante 30 segundos, e faça várias séries",
                Imagem="ShadowBox.jpg", CategoriaFK=1},
                new Exercicios {ID=3, Nome="Agachamento", Objetivo="Foco nos musculos superiores das pernas",
                Passos="Afastar os pés à largura dos ombros e apoiá-los totalmente no chão. Fletir os joelhos e baixar o corpo até ultrapassar o nível dos joelhos, mantendo sempre as costas direitas.Esticar as pernas para posição inicial.",
                Imagem="Agachamento.jpg", CategoriaFK=2},
                new Exercicios {ID=4, Nome="Saltar à Corda ", Objetivo="Resistência e controlo da respiração",
                Passos="Saltar a Corda, e aguentar o máximo possível",
                Imagem="SCorda.jpg", CategoriaFK=1},
                new Exercicios {ID=5, Nome="Cobra Stretch", Objetivo="Treinar Flexibilidade e alongar",
                Passos="Coloque o corpo virado de barriga para baixo, com as palmas das mãos no chão. Mantenha as pernas no chão e eleve o tronco até ao máximo que conseguir. Baixo o tronco de forma lenta, até regressar à posição inicial. Repitas os passos entre 30 segundos a 1 minuto",
                Imagem="CStretch.jpg", CategoriaFK=3},
                new Exercicios {ID=6, Nome="Elevação", Objetivo="Exercicio de Calistenia, com foco no conjuto de músculos superior",
                Passos="Pendurar numa barra com braços estendido. Elevar o corpo, com a força dos braços, até o queixo passar a barra",
                Imagem="Elevacao.jpg", CategoriaFK=5},
            };
            exercicios.ForEach(ee => context.Exercicios.AddOrUpdate(e => e.Nome, ee));
            context.SaveChanges();*/


        }
    }
}
