using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraphVector
{
    public partial class Form1 : Form
    {
        Color ClrGraph;
        List<List<int>> Adjacent;
        int Vertices; // so dinh
        private int[,] matrixMST;      // khai bao ma tran MST,co hang co cot 
        GraphicsTools g; // khai bao 1 graphicsTools
        List<Point> lstPointVertices; // danh sach toa do cac dinh
        string FileName;
        static int numberCheckClick = 0;
        public const int PicturePadding = 50; // picture padding
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOpenFile_Click_1(object sender, EventArgs e)
        {
            // mo file co san 
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "DoThi File(*.txt)|*.txt|All File(*.*)|*.*";
            // neu mo oK thi se ve do thi ban dau
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.lvMatrix.Items.Clear();
                    this.lvMatrix.Columns.Clear();
                    this.picGraphics.Image = null;
                    //
                    Matrix m = new Matrix();
                    this.FileName = dlg.FileName;
                   
                    this.Adjacent = m.LoadFile(this.FileName, this.lvMatrix, this.cboFrom, this.cboTo);//, this.cboFrom, this.cboTo);
                                                                                                       //Console.WriteLine(Adjacent);
                    Console.WriteLine("File name:  " + dlg.FileName + "  " + m);
                    matrixMST = m.MatrixArray(this.FileName);
                    Console.WriteLine(string.Join("\t Adjacent_", Adjacent));          // error
                    this.Vertices = m.Vertices; // loc ra so dinh 
                    // in ra so dinh la bao nhieu 
                    Console.WriteLine("SO Dinh"+ m.Vertices+ "  _Adjacent: " + this.Adjacent);
                    // khoi tao ra 1 picture box 
                    this.g = new GraphicsTools(this.picGraphics.Width, this.picGraphics.Height);// khoi tao graphics tool
                    // 
                    this.lstPointVertices = new Vector2D(this.Vertices, this.picGraphics.Width - Form1.PicturePadding,
                                                this.picGraphics.Height - Form1.PicturePadding).getRandomPoint();
                    Console.WriteLine("lstPointVertices : " + this.lstPointVertices.ToArray().ToString());
                    // tao mau theo y ng dung. 
                    if (numberCheckClick == 1)
                    {
                        this.picGraphics.Image = this.g.CreateGraphics(this.Adjacent, this.lstPointVertices,ClrGraph);
                    }
                    else
                    {
                        ClrGraph = Color.FromArgb(100, 149, 237);
                        this.picGraphics.Image = this.g.CreateGraphics(this.Adjacent, this.lstPointVertices, ClrGraph);
                    }
                    // end phan to mau. 
                    //else if (ClrGraph == null)
                    //{
                    //    Console.WriteLine("vao else !! ");
                    //    ClrGraph = Color.FromArgb(100, 149, 237);
                    //    this.picGraphics.Image = this.g.CreateGraphics(this.Adjacent, this.lstPointVertices, ClrGraph);
                    //}
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Lỗi");
                }
            }
            dlg.Dispose();
        }
        // end mo file va ve do thi. 

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            // bam find path thuc thi code 
            BFS bfs = new BFS(this.Adjacent);
            var result = string.Join(";", this.Adjacent);
            //Adjacent.ForEach(result);
            //Console.WriteLine(result);
            int start = this.cboFrom.SelectedIndex;
            int end = this.cboTo.SelectedIndex;

            // reset picGraphics va txtResult
            this.picGraphics.Image = this.g.Reset(this.Adjacent, this.lstPointVertices);

            if (start == end)
            {
                MessageBox.Show("Điểm trùng nhau. Vui lòng chọn lại!", "Error");
                return;
            }
            // res se danh sach report path tra ve 
            List<int> res = bfs.findPathbyBfs(this.Vertices, start, end);

            if (res == null)  // khong co duong 
            {
                string text = "Không thể tìm được đường từ {0} đến {1}.";
                MessageBox.Show(string.Format(text, start + 1, end + 1), "Find Path");
                return;
            }
            else
            {
                // neu co duong di se thuc hien hightlight duong ngan nhat len do thi 
                int index;
                //this.txtResult.Text = "";

                //// reset bit map

                //// xuat ket qua ra text box
                //for (index = 0; index < res.Count - 1; ++index)
                //    this.txtResult.Text += (1 + res[index]).ToString() + " ---> ";
                //this.txtResult.Text += (1 + res[index]).ToString();

                // ve duong di len bitmap
                this.picGraphics.Image = this.g.DrawPath(res, this.lstPointVertices);
            }
        }
        public void WriteToConsole(IEnumerable items)
        {
            foreach (object o in items)
            {
                Console.WriteLine(o);
            }
        }
        // lửu ảnh

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImgDialog = new SaveFileDialog();
            saveImgDialog.DefaultExt = "png";
            saveImgDialog.Filter = "Bitmap Image (*.png)|*.png|All File (*.*)|*.*";
            saveImgDialog.AddExtension = true;
            saveImgDialog.RestoreDirectory = true;
            saveImgDialog.Title = "Lưu ảnh đồ thị";
            string initFileName = System.IO.Path.GetFileNameWithoutExtension(this.FileName);
            saveImgDialog.FileName = initFileName;

            try
            {
                if (saveImgDialog.ShowDialog() == DialogResult.OK)
                {
                    string imgPath = saveImgDialog.FileName;

                    if (imgPath != null)
                    {
                        if (this.picGraphics.Image != null)
                        {
                            this.picGraphics.Image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            saveImgDialog.Dispose();
        }
        // tim khung cuc tieu bang thuat toan kruskal
        // 
        private void btnKruskal_Click(object sender, EventArgs e)
        {
            // List<int> list = new List<int>(new int[] { 7, 1, 1, 2, 4,3,0,4,3,5,5,6,6,7,3,8});
            // int count = 0;
            
            MST mst = new MST();

            List<int> res = mst.primMST(matrixMST, Vertices);
            this.picGraphics.Image = this.g.DrawMSTPath(res, this.lstPointVertices,Color.Yellow,Color.Red,Color.Green);
            foreach (object o in res)
            {
                Console.WriteLine("data_res: " + o);
            }
            int sum = 0;
            for (int index = 0; index < res.Count - 1; ++index)
            {
                Console.WriteLine("res _ index: " + res[index]+ res[index+1]);
                sum += matrixMST[res[index], res[index + 1]];
            }
            Console.WriteLine("duong min : : " + sum);
            label4.Text = "Đường đi ngắn nhất giá trị là: " + sum;
            //foreach (object o in this.lstPointVertices)
            //{
            //    Console.WriteLine("data_: " + o);
            //}
            // this.picGraphics.Image = this.g.DrawPath(list, this.lstPointVertices);
        }

        private void btnCheckConn_Click(object sender, EventArgs e)
        {
            checkConnect checkEvt = new checkConnect();
            bool check = checkEvt.checkIsConnected(matrixMST,Vertices);
            Console.WriteLine("bool" + check);
            if (check == true)
            {
                //  lbIsConnect
                lblnoti_.Text = "Đồ thị Liên Thông";
                //  MessageBox.Show("Đồ thị Liên Thông", "Thông Báo");
            }
            else if (check == false)
            {
                // lbIsConnect
                lblnoti_.Text = "Không Liên Thông";
               // MessageBox.Show("Đồ thị Không Liên Thông", "Thông Báo");
            }
                    
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                ClrGraph = colorDialog1.Color;
                numberCheckClick = 1;
                Console.WriteLine("color: " + ClrGraph);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void picGraphics_Click(object sender, EventArgs e)
        {

        }

        private void lvMatrix_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnHuongDan_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblnoti_.Text = " ";
            label4.Text = " ";
        }
    }
}