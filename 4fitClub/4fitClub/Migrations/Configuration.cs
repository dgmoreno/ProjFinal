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
                new Categorias {ID=1, Nome="Cardio/Resist�ncia", Descricao="Exercicios aer�bicos, com foco em resist�ncia",
                Imagem="Cardio.jpg"},
                new Categorias {ID=2, Nome="Muscula��o", Descricao="Exerc�cios com foco na estrutura muscular",
                Imagem="Musculacao.jpg"},
                new Categorias {ID=3, Nome="Flexibilidade", Descricao="Exerc�cios que ajudam a melhorar a flexibilidade",
                Imagem="flexibilidade.jpg"},
                new Categorias {ID=4, Nome="Equil�brio", Descricao="Exerc�cios para melhorar o equilibrio e a agilidade",
                Imagem="Equilibrio.jpg"},
                new Categorias {ID=5, Nome="Cal�stenia", Descricao="Exerc�cios que utilizam um grande conjuto de m�sculos do corpo",
                Imagem="Calistenia.jpg"},
            };
            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            //****************************************************************************************
            // adiciona Planos
            var planos = new List<Planos> {
                new Planos {ID=1, Nome="Projeto Ver�o", Descricao="� desta que fico Fit"},
            };
            planos.ForEach(pp => context.Planos.AddOrUpdate(p => p.Nome, pp));
            context.SaveChanges();

            //********************************************************************************************
            // adiciona Exercicios
            var exercicios = new List<Exercicios> {
                new Exercicios {ID=1, Nome="Flex�o", Objetivo="Trabalhar a zona peitoral e triceps",
                Passos="Coloque o corpo numa posi��o de prancha, alinhado e com bra�os e pernas estendidos. Flete apenas os bra�os, at� tocar com o peito e a cintura no ch�o. Estenda de novo apenas os bra�os, at� voltar � posi��o inicial.",
                , CategoriaFK=2},
                 new Exercicios {ID=2, Nome="Shadow Box", Objetivo="Resist�ncia",
                Passos="Fa�a movimentos de boxe no ar, pelo menos durante 30 segundos, e fa�a v�rias s�ries",
                Imagem="ShadowBox.jpg", CategoriaFK=1},
                new Exercicios {ID=3, Nome="Agachamento", Objetivo="Foco nos musculos superiores das pernas",
                Passos="Afastar os p�s � largura dos ombros e apoi�-los totalmente no ch�o. Fletir os joelhos e baixar o corpo at� ultrapassar o n�vel dos joelhos, mantendo sempre as costas direitas.Esticar as pernas para posi��o inicial.",
                Imagem="Agachamento.jpg", CategoriaFK=2},
                new Exercicios {ID=4, Nome="Saltar � Corda ", Objetivo="Resist�ncia e controlo da respira��o",
                Passos="Saltar a Corda, e aguentar o m�ximo poss�vel",
                Imagem="SCorda.jpg", CategoriaFK=1},
                new Exercicios {ID=5, Nome="Cobra Stretch", Objetivo="Treinar Flexibilidade e alongar",
                Passos="Coloque o corpo virado de barriga para baixo, com as palmas das m�os no ch�o. Mantenha as pernas no ch�o e eleve o tronco at� ao m�ximo que conseguir. Baixo o tronco de forma lenta, at� regressar � posi��o inicial. Repitas os passos entre 30 segundos a 1 minuto",
                Imagem="CStretch.jpg", CategoriaFK=3},
                new Exercicios {ID=6, Nome="Eleva��o", Objetivo="Exercicio de Calistenia, com foco no conjuto de m�sculos superior",
                Passos="Pendurar numa barra com bra�os estendido. Elevar o corpo, com a for�a dos bra�os, at� o queixo passar a barra",
                Imagem="Elevacao.jpg", CategoriaFK=5},
            };
            exercicios.ForEach(ee => context.Exercicios.AddOrUpdate(e => e.Nome, ee));
            context.SaveChanges();*/


        }
    }
}
