using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphVector
{
    class BFS
    {
        private Queue<int> VertexQueue = new Queue<int>(); // hang doi chua cac dinh, luu so dinh cua ma tran
        private List<List<int>> _adjacent; // danh sach canh ke , luu ma tran khi mo file 
        private List<int> _reportPath = new List<int>(); // danh sach dinh duong di, luu cac duong minh da di qua 

        public BFS(List<List<int>> adjacent)
        {
            this._adjacent = adjacent;

        }

        //find path
        // thuat toan tim duong di ngan nhat 
        // 14 diem -> di qua tung diem 1-> luu diem do vao 1 mảng,
        // start -> end tu dau la do minh cho 
        public List<int> findPathbyBfs(int tips, int start, int end)
        {
            if (this._adjacent[start] == null || this._adjacent[end] == null)
                return null;

            bool[] visited = new bool[tips]; // danh dau cac dinh da tham
            int[] previous = new int[tips + 1]; // luu dinh truoc, phần kê tiếp. 


            // khoi tao mang visited va previous
            for (int index = 0; index < tips; ++index)
            {
                visited[index] = false;
                previous[index] = -1;
            }
            // khi bat dau dau, minh se luu 
            this.VertexQueue.Enqueue(start);   // vertexqueue {1,2,3,4,....}
            visited[start] = true; // tri so start no bat dau dinh nao thi lay luon lam tri so. 
         
            // neu ma so dinh !0 
            while (this.VertexQueue.Count != 0)
            {
                int v = this.VertexQueue.Dequeue();
                
                List<int> row = new List<int>(this._adjacent[v]);
                foreach (var item in row) // duyet tung phan tu trong 1 hang 
                {
                    //em 
                    Console.WriteLine("row" + item); // debug code de xem ket qua duyet tung phan tu trong 1 hang
                }
                if (row != null) // hang bang 0 
                {
                    foreach (int col in row)
                    {
                        if (!visited[col])
                        {
                            this.VertexQueue.Enqueue(col);  
                            previous[col] = v;
                            visited[col] = true;
                            // neu diem ket thu duoc tham thi ket thuc thuat toan
                            if (visited[end]) break;
                        }
                    }
                }
                // diem cuoi duoc tham thi ket thuc thuat toan
                if (visited[end]) break;

            }
            // neu diem ket thuc khong duoc tham thi tra ve null
            if (!visited[end]) return null;

            // luu duong di vao mang report Path 
            int current = end;
            this._reportPath.Add(end);
            while (previous[current] != -1)
            {
                this._reportPath.Add(previous[current]);
                current = previous[current];
            }
            this._reportPath.Reverse();
            // tra ve danh sach cac diem da di qua tu diem from cho den to 
            return this._reportPath;
        }
    }
}
