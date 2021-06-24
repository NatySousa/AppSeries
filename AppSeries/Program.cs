using AppSeries.Entities;
using AppSeries.Enum;
using System;
using System.Globalization;

namespace AppSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();

                }
                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }
        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.RetornaExcluido();

                Console.WriteLine("#Id {0} : {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? " - *Excluído*" : ""));
            }
            
        }
        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");
            
            foreach(int i in Genero.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Genero.GetName(typeof(Genero), i));
            }

            Console.WriteLine();
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(repositorio.ProximoId(), (Genero)entradaGenero, entradaTitulo, entradaDescricao, entradaAno);

            repositorio.Inserir(novaSerie);

        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("As melhores séries ao seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("----");
            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar a Tela");
            Console.WriteLine("X- Sair");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void AtualizarSerie()
        {
            var lista = repositorio.Lista();

            foreach (var serie in lista)
            {
                Console.WriteLine("#Id {0} : {1}", serie.retornaId(), serie.retornaTitulo());
            }

            Console.WriteLine();
            Console.Write("Digite o Id da série que será atualizada: ");

            int idSerie = int.Parse(Console.ReadLine());

            foreach (int i in Genero.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Genero.GetName(typeof(Genero), i));
            }

            Console.WriteLine();
            Console.Write("Digite o novo gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o novo Título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o novo ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a nova descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(idSerie, (Genero)entradaGenero, entradaTitulo, entradaDescricao, entradaAno);

            repositorio.Atualizar(idSerie, novaSerie);
        }

        private static void ExcluirSerie()
        {
            var lista = repositorio.Lista();

            foreach (var serie in lista)
            {
                Console.WriteLine("#Id {0} : {1}", serie.retornaId(), serie.retornaTitulo());
            }

            Console.WriteLine();
            Console.Write("Digite o Id da série que será excluída: ");

            int idSerie = int.Parse(Console.ReadLine());

            repositorio.Excluir(idSerie);
        }

        private static void VisualizarSerie()
        {
            var lista = repositorio.Lista();

            foreach (var serie in lista)
            {
                Console.WriteLine("#Id {0} : {1}", serie.retornaId(), serie.retornaTitulo());
            }

            Console.WriteLine();
            Console.Write("Digite o Id da série que será detalhada: ");

            int idSerie = int.Parse(Console.ReadLine());

            Serie serieDetalhada = repositorio.RetornaPorId(idSerie);
            Console.WriteLine();
            Console.WriteLine(serieDetalhada.ToString());
        }
    }
}
