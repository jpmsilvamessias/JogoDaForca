using System;
using System.Collections.Generic;

namespace JogoForca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int chances = 6;
            List<string> palavras = new List<string> { "ameixa", "batata", "amora", "sergio", "carro" };
            List<char> digitadas = new List<char>();
            Random rdn = new Random();
            string palavra = palavras[rdn.Next(palavras.Count)].ToUpper();
            char[] palavraEscondida = new string('_', palavra.Length).ToCharArray();

            Console.WriteLine("Bem-vindo ao jogo da forca!");

            while (chances > 0)
            {
                ExibirPalavra(palavraEscondida);
                Console.WriteLine($"Chances restantes: {chances}");

                try
                {
                    char letra = PedirLetra();

                    if (digitadas.Contains(letra))
                    {
                        Console.WriteLine("Você já digitou essa letra!");
                        continue;
                    }

                    digitadas.Add(letra);

                    if (palavra.Contains(letra))
                    {
                        for (int i = 0; i < palavra.Length; i++)
                        {
                            if (palavra[i] == letra)
                            {
                                palavraEscondida[i] = letra;
                            }
                        }
                    }
                    else
                    {
                        chances--;
                        Console.WriteLine("Letra incorreta!");
                    }

                    if (VerificarVitoria(palavraEscondida))
                    {
                        Console.WriteLine($"\nParabéns! Você acertou a palavra: {palavra}");
                        return;
                    }
                }
                catch (InvalidProgramException e)
                {
                    Console.WriteLine($"Erro: {e.Message}");
                }
            }

            Console.WriteLine($"\nGame Over! A palavra era: {palavra}");
        }

        static char PedirLetra()
        {
            Console.Write("Digite uma letra: ");
            string entrada = Console.ReadLine().ToUpper();

            if (string.IsNullOrEmpty(entrada) || entrada.Length > 1 || !char.IsLetter(entrada[0]))
            {
                throw new InvalidProgramException("Insira apenas uma letra válida!");
            }

            return entrada[0];
        }

        static void ExibirPalavra(char[] palavraEscondida)
        {
            Console.WriteLine("\nPalavra: " + string.Join(" ", palavraEscondida));
        }

        static bool VerificarVitoria(char[] palavraEscondida)
        {
            return !new string(palavraEscondida).Contains('_');
        }
    }
}


