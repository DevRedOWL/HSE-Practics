﻿using System;
using System.Collections.Generic;

namespace Task10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите список вершин");
            Vertex.VList.Add(new Vertex(0, new List<int> { 1, 3 } )); // 0
            Vertex.VList.Add(new Vertex(6, new List<int> { 4 } )); // 1
            Vertex.VList.Add(new Vertex(7, new List<int> { 0 } )); // 2
            Vertex.VList.Add(new Vertex(7, new List<int> { 2, 4 } )); // 3
            Vertex.VList.Add(new Vertex(7, new List<int> { 2 } )); // 4
            Vertex.VList.Add(new Vertex(9, new List<int> { })); // 4

            Vertex.Stretch();

            Console.Read();
        }
    }
}
