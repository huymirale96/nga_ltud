using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace GraphVector
{
   public class checkConnect
    {
        public bool checkIsConnected(int[,]graph, int V)
        {
            //các bạn thấy nó quen không.Hehe coppy của bài DFS đó.hehe không chú thích nưa nha.
            int i = 0;
            int numVertex = V;
            List<int> listVS = new List<int>();
            int[] visit = new int[V];
            Stack<int> stack = new Stack<int>();
            listVS.Add(i);
            visit[i] = 1;
            stack.Push(i);
            
            while (stack.Count != 0)
            {
                i = stack.Peek();
                int count = 0;
                // duyet mang gia tri 
                for (int j = 0; j < visit.Length; j++)
                {
                   
                    if ((graph[i,j] > 0) && visit[j] != 1)
                    {
                        visit[j] = 1;
                        listVS.Add(j);
                        stack.Push(j);
                        break;
                    }
                    else
                    {
                        count++;
                        Console.WriteLine("tp lien thong"+ count);
                    }
                }
               // Form1 frm1 = new Form1();
               // frm1.lbLienThong.Text = count.ToString();
                if (count == visit.Length)
                {
                    stack.Pop();
                }
            }
           
            
            for (int k = 0; k < visit.Length; k++)
            {
                if (visit[k] != 1)
                    //nếu có một cái bất kỳ khác 1 là return.Còn ko thi liên thông .
                    return false;
            }
            return true;
        }
    }
}
