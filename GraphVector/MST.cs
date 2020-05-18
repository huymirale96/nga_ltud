using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphVector
{
    class MST
    {
        static int minKey(int[] key, bool[] mstSet, int V)
        {

            // Initialize min value 
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }

            return min_index;
        }

      
        // parent[] 
        static List<int> printMST(int[] parent, int n, int[,] graph)
        {
            List<int> _reportPath = new List<int>();
            Form1 frm1 = new Form1();
            GraphicsTools g;
            Pen pLine;
            Console.WriteLine("Edge Weight");
            for (int i = 1; i < n; i++)
            {
                Console.WriteLine(parent[i] + " - " + i + "	" +
                                graph[i, parent[i]]);                 // test list
                _reportPath.Add(parent[i]);
                _reportPath.Add(i);
            }
            return _reportPath;
        }

 
        public List<int> primMST(int[,] graph, int V)
        {

            List<int> _reportPath = new List<int>();    // report duong ve
            int[] parent = new int[V];                  // nut cha

            
            int[] key = new int[V];

            
            bool[] mstSet = new bool[V];


            for (int i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

           
            key[0] = 0;
            parent[0] = -1;

            try
            {
                for (int count = 0; count < V - 1; count++)
                {

                    int u = minKey(key, mstSet, V);
                    Console.WriteLine("gia tri u" + u);
                    mstSet[u] = true;
                    Console.WriteLine(mstSet[u]);
                    for (int v = 0; v < V; v++)


                        if (graph[u, v] != 0 && mstSet[v] == false &&
                                                graph[u, v] < key[v])
                        {
                            parent[v] = u;
                            key[v] = graph[u, v];
                        }
                }
            }
            catch (Exception Ex)
            {
            }
                // xuat ra list MST 
                _reportPath = printMST(parent, V, graph);
            foreach (object o in _reportPath)
            {
                Console.WriteLine("data__reportPath: " + o);
            }
            return _reportPath;
        }

       
    }
}
