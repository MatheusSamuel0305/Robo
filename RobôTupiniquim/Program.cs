using System;
using System.Collections.Generic;

class Robo {
    public int X { get; set; }
    public int Y { get; set; }
    public char Direcao { get; set; }

    private readonly Dictionary<char, (int dx, int dy)> movimentos = new Dictionary<char, (int, int)>
    {
        { 'N', (0, 1) },
        { 'S', (0, -1) },
        { 'L', (1, 0) },
        { 'O', (-1, 0) }
    };

    private readonly char[] direcoes = { 'N', 'L', 'S', 'O' };

    public Robo(int x, int y, char direcao) {
        X = x;
        Y = y;
        Direcao = direcao;
    }

    public void ExecutarComandos(string comandos, int maxX, int maxY) {
        foreach (char comando in comandos) {
            switch (comando) {
                case 'E':
                    GirarEsquerda();
                    break;
                case 'D':
                    GirarDireita();
                    break;
                case 'M':
                    Mover(maxX, maxY);
                    break;
            }
        }
    }

    private void GirarEsquerda() {
        int indice = (Array.IndexOf(direcoes, Direcao) + 3) % 4;
        Direcao = direcoes[indice];
    }

    private void GirarDireita() {
        int indice = (Array.IndexOf(direcoes, Direcao) + 1) % 4;
        Direcao = direcoes[indice];
    }

    private void Mover(int maxX, int maxY) {
        var movimento = movimentos[Direcao];
        int novoX = X + movimento.dx;
        int novoY = Y + movimento.dy;

        // Verifica se o movimento está dentro dos limites do plano
        if (novoX >= 0 && novoX <= maxX && novoY >= 0 && novoY <= maxY) {
            X = novoX;
            Y = novoY;
        }
    }
}

class Programa {
    static void Main() {
        Console.WriteLine("Informe o tamanho do plano de coordenadas (formato: X Y):");
        string[] tamanhoPlano = Console.ReadLine().Split();
        int maxX = int.Parse(tamanhoPlano[0]);
        int maxY = int.Parse(tamanhoPlano[1]);

        Console.WriteLine("Informe o número de robôs:");
        int numRobos = int.Parse(Console.ReadLine());

        for (int i = 1; i <= numRobos; i++) {
            Console.WriteLine($"Robô {i}:");
            Console.WriteLine("Informe a posição inicial e a direção (formato: X Y D):");
            string[] entradaInicial = Console.ReadLine().Split();
            int x = int.Parse(entradaInicial[0]);
            int y = int.Parse(entradaInicial[1]);
            char direcao = char.Parse(entradaInicial[2]);

            Console.WriteLine("Informe as ordens de movimento (formato: E, D ou M):");
            string comandos = Console.ReadLine();

            Robo robo = new Robo(x, y, direcao);
            robo.ExecutarComandos(comandos, maxX, maxY);

            Console.WriteLine($"Posição final do robô {i}: {robo.X},{robo.Y},{robo.Direcao}");
        }
    }
}
