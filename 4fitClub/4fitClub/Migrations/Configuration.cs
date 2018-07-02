namespace _4fitClub.Migrations
{
    using _4fitClub.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_4fitClub.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(_4fitClub.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //******************************************************************************************
            //adiciona Categorias
            var categorias = new List<Categorias> {
                new Categorias {ID=1, Nome="Cardio/Resistência", Descricao="Exercicios aeróbicos, com foco em resistência",
                Imagem="Cardio.png"},
                new Categorias {ID=2, Nome="Musculação", Descricao="Exercícios com foco na estrutura muscular",
                Imagem="Musculacao.png"},
                new Categorias {ID=3, Nome="Flexibilidade", Descricao="Exercícios que ajudam a melhorar a flexibilidade",
                Imagem="Flexibilidade.png"},
                new Categorias {ID=4, Nome="Equilíbrio", Descricao="Exercícios para melhorar o equilibrio e a agilidade",
                Imagem="Equilibrio.png"},
                new Categorias {ID=5, Nome="Calístenia", Descricao="Exercícios que utilizam um grande conjuto de músculos do corpo",
                Imagem="Calistenia.png"}
            };
            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            //********************************************************************************************
            // adiciona Exercicios
            var exercicios = new List<Exercicios> {
                new Exercicios {ID=1, Nome="Flexão", Objetivo="Trabalhar a zona peitoral e triceps",
                Passos="1.Coloque o corpo numa posição de prancha, alinhado e com braços e pernas estendidos. " +
                       "2.Flita apenas os braços, até tocar com o peito e a cintura no chão." +
                       "3.Estenda de novo apenas os braços, até voltar à posição inicial.",
                CategoriaFK=2},
                 new Exercicios {ID=2, Nome="Shadow Box", Objetivo="Resistência",
                Passos="1.Faça movimentos de boxe no ar, pelo menos durante 30 segundos, e faça várias séries",
                CategoriaFK=1},
                new Exercicios {ID=3, Nome="Agachamento", Objetivo="Foco nos musculos superiores das pernas",
                Passos="1.Afaste os pés à largura dos ombros, apoiando totalmente no chão. " +
                       "2.Fletir os joelhos e baixar o corpo até ultrapassar o nível dos joelhos, mantendo sempre as costas direitas." +
                       "3.Estique as pernas para posição inicial.",
                CategoriaFK=2},
                new Exercicios {ID=4, Nome="Saltar à Corda ", Objetivo="Resistência e controlo da respiração",
                Passos="1.Salte à Corda, e mantenha o ritmo durante 40 segundos",
                CategoriaFK=1},
                new Exercicios {ID=5, Nome="Cobra Stretch", Objetivo="Treinar Flexibilidade e alongar",
                Passos="1.Coloque o corpo virado de barriga para baixo, com as palmas das mãos no chão." +
                       "2.Mantenha as pernas no chão e eleve o tronco até ao máximo que conseguir. " +
                       "3.Baixe o tronco de forma lenta, até regressar à posição inicial. " +
                       "4.Repitas os passos entre 30 segundos a 1 minuto",
                CategoriaFK=3},
                new Exercicios {ID=6, Nome="Elevação", Objetivo="Exercicio de Calistenia, com foco no conjuto de músculos superiores",
                Passos="1.Pendurar numa barra com braços estendido."+
                       "2.Elevar o corpo, com a força dos braços, até o queixo passar a barra",
                CategoriaFK=5}
            };
            exercicios.ForEach(ee => context.Exercicios.AddOrUpdate(e => e.Nome, ee));
            context.SaveChanges();


            //****************************************************************************************
            // adiciona Planos
            var planos = new List<Planos> {
                new Planos {ID=1, Nome="Projeto Verão", Descricao="É desta que fico Fit", UserName="david@mail.pt", ListaDeExercicios=new List<Exercicios>{ exercicios[0], exercicios[2], exercicios[3], exercicios[4] } }
            };
            planos.ForEach(pp => context.Planos.AddOrUpdate(p => p.Nome, pp));
            context.SaveChanges();

            //**********************************************************************************************
            //adicona Imagens - alterar para multimédia (o nome da classe)

            var imagens = new List<Imagens> {
                new Imagens {ID=1, Nome="Flexao1.jpg", Ordem=1, Tipo="Imagem", ExercicioFK=1},
                new Imagens {ID=2, Nome="Flexao2.jpg", Ordem=2, Tipo="Imagem", ExercicioFK=1},
                new Imagens {ID=3, Nome="ShadowBox1.jpg", Ordem=1, Tipo="Imagem", ExercicioFK=2},
                new Imagens {ID=4, Nome="ShadowBox2.jpg", Ordem=2, Tipo="Imagem", ExercicioFK=2},
                new Imagens {ID=5, Nome="Agachamento1.jpg", Ordem=1, Tipo="Imagem", ExercicioFK=3},
                new Imagens {ID=6, Nome="Agachamento2.jpg", Ordem=2, Tipo="Imagem", ExercicioFK=3},
                new Imagens {ID=7, Nome="SaltoCorda1.jpg", Ordem=1, Tipo="Imagem", ExercicioFK=4},
                new Imagens {ID=8, Nome="SaltoCorda2.jpg", Ordem=2, Tipo="Imagem", ExercicioFK=4},
                new Imagens {ID=9, Nome="CobraStretch1.jpg", Ordem=1, Tipo="Imagem", ExercicioFK=5},
                new Imagens {ID=10, Nome="CobraStretch2.jpg", Ordem=2, Tipo="Imagem", ExercicioFK=5},
                new Imagens {ID=11, Nome="PullUp1.jpg", Ordem=1, Tipo="Imagem", ExercicioFK=6},
                new Imagens {ID=11, Nome="PullUp2.jpg", Ordem=2, Tipo="Imagem", ExercicioFK=6}
            };
            imagens.ForEach(ii => context.Imagens.AddOrUpdate(i => i.Nome,ii));
            context.SaveChanges();

         
        }
    }
}
