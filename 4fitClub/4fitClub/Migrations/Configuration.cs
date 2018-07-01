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
                new Categorias {ID=1, Nome="Cardio/Resist�ncia", Descricao="Exercicios aer�bicos, com foco em resist�ncia",
                Imagem="Cardio.png"},
                new Categorias {ID=2, Nome="Muscula��o", Descricao="Exerc�cios com foco na estrutura muscular",
                Imagem="Musculacao.png"},
                new Categorias {ID=3, Nome="Flexibilidade", Descricao="Exerc�cios que ajudam a melhorar a flexibilidade",
                Imagem="Flexibilidade.png"},
                new Categorias {ID=4, Nome="Equil�brio", Descricao="Exerc�cios para melhorar o equilibrio e a agilidade",
                Imagem="Equilibrio.png"},
                new Categorias {ID=5, Nome="Cal�stenia", Descricao="Exerc�cios que utilizam um grande conjuto de m�sculos do corpo",
                Imagem="Calistenia.png"}
            };
            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            //********************************************************************************************
            // adiciona Exercicios
            var exercicios = new List<Exercicios> {
                new Exercicios {ID=1, Nome="Flex�o", Objetivo="Trabalhar a zona peitoral e triceps",
                Passos="1.Coloque o corpo numa posi��o de prancha, alinhado e com bra�os e pernas estendidos. " +
                       "2.Flita apenas os bra�os, at� tocar com o peito e a cintura no ch�o." +
                       "3.Estenda de novo apenas os bra�os, at� voltar � posi��o inicial.",
                CategoriaFK=2},
                 new Exercicios {ID=2, Nome="Shadow Box", Objetivo="Resist�ncia",
                Passos="1.Fa�a movimentos de boxe no ar, pelo menos durante 30 segundos, e fa�a v�rias s�ries",
                CategoriaFK=1},
                new Exercicios {ID=3, Nome="Agachamento", Objetivo="Foco nos musculos superiores das pernas",
                Passos="1.Afaste os p�s � largura dos ombros, apoiando totalmente no ch�o. " +
                       "2.Fletir os joelhos e baixar o corpo at� ultrapassar o n�vel dos joelhos, mantendo sempre as costas direitas." +
                       "3.Estique as pernas para posi��o inicial.",
                CategoriaFK=2},
                new Exercicios {ID=4, Nome="Saltar � Corda ", Objetivo="Resist�ncia e controlo da respira��o",
                Passos="1.Salte � Corda, e mantenha o ritmo durante 40 segundos",
                CategoriaFK=1},
                new Exercicios {ID=5, Nome="Cobra Stretch", Objetivo="Treinar Flexibilidade e alongar",
                Passos="1.Coloque o corpo virado de barriga para baixo, com as palmas das m�os no ch�o." +
                       "2.Mantenha as pernas no ch�o e eleve o tronco at� ao m�ximo que conseguir. " +
                       "3.Baixe o tronco de forma lenta, at� regressar � posi��o inicial. " +
                       "4.Repitas os passos entre 30 segundos a 1 minuto",
                CategoriaFK=3},
                new Exercicios {ID=6, Nome="Eleva��o", Objetivo="Exercicio de Calistenia, com foco no conjuto de m�sculos superiores",
                Passos="1.Pendurar numa barra com bra�os estendido."+
                       "2.Elevar o corpo, com a for�a dos bra�os, at� o queixo passar a barra",
                CategoriaFK=5}
            };
            exercicios.ForEach(ee => context.Exercicios.AddOrUpdate(e => e.Nome, ee));
            context.SaveChanges();


            //****************************************************************************************
            // adiciona Planos
            var planos = new List<Planos> {
                new Planos {ID=1, Nome="Projeto Ver�o", Descricao="� desta que fico Fit", UserName="david@mail.pt", ListaDeExercicios=new List<Exercicios>{ exercicios[0], exercicios[2], exercicios[3], exercicios[4] } }
            };
            planos.ForEach(pp => context.Planos.AddOrUpdate(p => p.Nome, pp));
            context.SaveChanges();

            //**********************************************************************************************
            //adicona Imagens - alterar para multim�dia (o nome da classe)

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
