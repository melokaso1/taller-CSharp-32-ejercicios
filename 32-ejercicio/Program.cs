using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteOptimizer
{
    // Representa el grafo ponderado
    public class Graph
    {
        // Diccionario: nodo -> lista de aristas (vecino, peso)
        private readonly Dictionary<string, List<Edge>> _adjacencyList = new();

        public void AddNode(string node)
        {
            if (!_adjacencyList.ContainsKey(node))
                _adjacencyList[node] = new List<Edge>();
        }

        public void AddEdge(string from, string to, int cost, bool bidirectional = false)
        {
            AddNode(from);
            AddNode(to);

            _adjacencyList[from].Add(new Edge(to, cost));

            if (bidirectional)
                _adjacencyList[to].Add(new Edge(from, cost));
        }

        public IEnumerable<string> Nodes => _adjacencyList.Keys;

        public IEnumerable<Edge> GetNeighbors(string node)
        {
            return _adjacencyList.TryGetValue(node, out var edges)
                ? edges
                : Enumerable.Empty<Edge>();
        }
    }

    // Arista: destino + costo de combustible
    public readonly struct Edge
    {
        public string Target { get; }
        public int Cost { get; }

        public Edge(string target, int cost)
        {
            Target = target;
            Cost = cost;
        }
    }

    public static class Dijkstra
    {
        public static (List<string> path, int totalCost) FindShortestPath(
            Graph graph,
            string start,
            string end
        )
        {
            // Distancias acumuladas desde start
            var distances = new Dictionary<string, int>();
            // Predecesores para reconstruir la ruta
            var previous = new Dictionary<string, string?>();
            // Conjunto de nodos sin visitar
            var unvisited = new HashSet<string>(graph.Nodes);

            // Inicializar distancias
            foreach (var node in graph.Nodes)
            {
                distances[node] = int.MaxValue;
                previous[node] = null;
            }
            distances[start] = 0;

            while (unvisited.Count > 0)
            {
                // 1. Elegir el nodo no visitado con menor distancia
                string? current = unvisited.OrderBy(n => distances[n]).FirstOrDefault();

                if (current is null || distances[current] == int.MaxValue)
                    break; // No hay más alcanzables

                // Si llegamos al destino, podemos salir
                if (current == end)
                    break;

                unvisited.Remove(current);

                // 2. Relajar las aristas desde current
                foreach (var edge in graph.GetNeighbors(current))
                {
                    if (!unvisited.Contains(edge.Target))
                        continue;

                    int newDist = distances[current] + edge.Cost;
                    if (newDist < distances[edge.Target])
                    {
                        distances[edge.Target] = newDist;
                        previous[edge.Target] = current;
                    }
                }
            }

            // Si no hay ruta posible
            if (!distances.ContainsKey(end) || distances[end] == int.MaxValue)
                return (new List<string>(), int.MaxValue);

            // 3. Reconstruir la ruta desde end hacia start usando previous
            var path = new List<string>();
            string? nodePath = end;
            while (nodePath is not null)
            {
                path.Add(nodePath);
                nodePath = previous[nodePath];
            }
            path.Reverse(); // Estaba al revés

            return (path, distances[end]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Construir el grafo con el ejemplo del enunciado
            var graph = new Graph();

            // A → B = 4
            // A → C = 2
            // B → D = 5
            // C → B = 1
            // C → D = 8
            // C → E = 10
            // D → E = 2
            // E → F = 3
            // D → F = 6

            graph.AddEdge("A", "B", 4);
            graph.AddEdge("A", "C", 2);
            graph.AddEdge("B", "D", 5);
            graph.AddEdge("C", "B", 1);
            graph.AddEdge("C", "D", 8);
            graph.AddEdge("C", "E", 10);
            graph.AddEdge("D", "E", 2);
            graph.AddEdge("E", "F", 3);
            graph.AddEdge("D", "F", 6);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== OPTIMIZACIÓN DE RUTAS (DIJKSTRA) =====\n");
                Console.WriteLine("Puntos disponibles: " + string.Join(", ", graph.Nodes));
                Console.Write("\nPunto de recogida (inicio): ");
                string? start = Console.ReadLine()?.Trim().ToUpperInvariant();

                Console.Write("Punto de entrega (destino): ");
                string? end = Console.ReadLine()?.Trim().ToUpperInvariant();

                if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end))
                {
                    Console.WriteLine(
                        "\nEntrada inválida. Presione una tecla para intentar de nuevo..."
                    );
                    Console.ReadKey();
                    continue;
                }

                // Validar que existan en el grafo
                if (!graph.Nodes.Contains(start) || !graph.Nodes.Contains(end))
                {
                    Console.WriteLine("\nUno de los puntos no existe en la red.");
                    Console.WriteLine("Presione una tecla para intentar de nuevo...");
                    Console.ReadKey();
                    continue;
                }

                // 2. Ejecutar Dijkstra
                var (path, totalCost) = Dijkstra.FindShortestPath(graph, start, end);

                Console.WriteLine();
                if (path.Count == 0 || totalCost == int.MaxValue)
                {
                    Console.WriteLine($"No existe ruta entre {start} y {end}.");
                }
                else
                {
                    Console.WriteLine("Ruta de menor consumo: " + string.Join(" -> ", path));
                    Console.WriteLine($"Consumo total estimado: {totalCost}");
                }

                Console.WriteLine("\n¿Desea calcular otra ruta? (S/N)");
                var key = Console.ReadKey(true);
                if (key.KeyChar is 'n' or 'N')
                    break;
            }
        }
    }
}
