using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace towerofannoy
{
    public partial class Form1 : Form
    {
        public class rectangle
        {
           public int X, Y;
           public int w, h;
            public int id;
            public int id2;
            public int id3;
            public int TNum;
        }
        List<int> lastid = new List<int>();
        List<int> lastid2 = new List<int>();
        List<int> lastid3 = new List<int>();
        Bitmap off;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.MouseDown += Form1_MouseDown;
            this.Paint += Form1_Paint;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
            this.KeyDown += Form1_KeyDown;
        }
        void Dubblebuffer(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            Drawscene(g2);
            g.DrawImage(off, 0, 0);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.M)
            {
                int X = 900;
                int Y = this.Height - 60;
                int W = 220;
                int H = 20;
                for (int i = 0; i < 6; i++)
                {
                    L[i].X = X;
                    L[i].Y = Y;
                    L[i].w = W;
                    L[i].h = H;
                    X += 20;
                    Y -= 20;
                    W -= 40;
                }
                Dubblebuffer(CreateGraphics());
                MessageBox.Show("you won");
                
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Dubblebuffer(e.Graphics);
        }

        int flagdrag = 0;
        List<rectangle> L = new List<rectangle>();
        int tile,oldx,oldy, ct=120,ct2=0,ct3=0;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            
            for (int i = 0; i < L.Count; i++)
            {
                if (e.X >= L[i].X && e.X <= (L[i].X + L[i].w)
                    && e.Y >= L[i].Y && e.Y <= (L[i].Y + L[i].h))
                {
                    if(L[i].TNum==1&&L[i].id2==ct/20)
                    {

                        flagdrag = 1;
                        
                    }
                    else if(L[i].TNum == 2 && L[i].id2 == ct2/20)
                    {
                        flagdrag = 1;
                        
                    }
                    else if (L[i].TNum == 3 && L[i].id2 == ct3/20)
                    {
                        flagdrag = 1;
                        
                    }
                    tile = i;
                    oldx = L[i].X;
                    oldy = L[i].Y;

                }
            }
           
            Dubblebuffer(CreateGraphics());
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (flagdrag == 1)
            {

                   L[tile].X = e.X;
                   L[tile].Y = e.Y;
                
                Dubblebuffer(CreateGraphics());
            }
            
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            flagdrag = 0;
           
            if (L[tile].X > 390 && L[tile].X < 420&& L[tile].id3 > lastid[ct/20])
            {
                
                L[tile].X = 400 - (L[tile].id * 20);
                L[tile].Y = this.Height - (60 + ct);
                if (L[tile].TNum == 2)
                {
                    ct2 -= 20;
                    ct += 20;
                    L[tile].id2 = ct / 20;
                    lastid2.RemoveAt((ct2 / 20) + 1);
                    lastid.Add(L[tile].id3);
                }
                else if (L[tile].TNum == 3)
                {
                    ct3 -= 20;
                    ct += 20;
                    L[tile].id2 = ct / 20;
                    lastid3.RemoveAt((ct3 / 20) + 1);
                    lastid.Add(L[tile].id3);
                }
                L[tile].TNum = 1;
               
            }
            else if (L[tile].X > 690 && L[tile].X < 720 && L[tile].id3 > lastid2[ct2/20])
            {
                
                L[tile].X = 700 - (L[tile].id * 20);
                L[tile].Y = this.Height-(60+ct2);
                if(L[tile].TNum==1)
                {
                    ct -= 20;
                    ct2 += 20;
                    L[tile].id2 = ct2 / 20;
                   
                    lastid.RemoveAt((ct/20)+1);
                   
                    lastid2.Add(L[tile].id3);
                    
                }
                else if(L[tile].TNum==3)
                {
                    ct3 -= 20;
                    ct2 += 20;
                    L[tile].id2 = ct2 / 20;
                    lastid3.RemoveAt((ct3 / 20) + 1);
                    lastid2.Add(L[tile].id3);
                }
                L[tile].TNum = 2;
                


            }
            else if(L[tile].X > 990 && L[tile].X < 1020 && L[tile].id3 > lastid3[ct3/20])
            {
                L[tile].X = 1000-(L[tile].id*20);
                L[tile].Y = this.Height - (60+ct3);
                if (L[tile].TNum == 1)
                {
                    ct -= 20;
                    ct3 += 20;
                    L[tile].id2 = ct3 / 20;
                    lastid.RemoveAt((ct / 20) + 1);
                    lastid3.Add(L[tile].id3);
                }
                else if (L[tile].TNum == 2)
                {
                    ct2 -= 20;
                    ct3 += 20;
                    L[tile].id2 = ct3 / 20;
                    lastid2.RemoveAt((ct2 / 20) + 1);
                    lastid3.Add(L[tile].id3);
                }
                L[tile].TNum = 3;
                if(ct3/20==6)
                {
                    MessageBox.Show("You Won");
                }
                
            }
            else
            {
                L[tile].X = oldx;
                L[tile].Y = oldy;
            }
            this.Text = ct/20 + " " + ct2/20 + " " + ct3/20;
            Dubblebuffer(CreateGraphics());
        }

        void Drawscene(Graphics g)
        {
            g.Clear(Color.White);
            int x = 400;
            for (int i = 0; i < 3; i++)
            {
                g.FillRectangle(Brushes.Gray, x, 400, 20, this.Height);
                g.DrawRectangle(Pens.Black, x, 400, 20, this.Height);
                x += 300;
            }
            for (int i=0;i<L.Count;i++)
            {
                g.FillRectangle(Brushes.Yellow, L[i].X, L[i].Y, L[i].w, L[i].h);
                g.DrawRectangle(Pens.Black, L[i].X, L[i].Y, L[i].w, L[i].h);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            int x = 400;
            Graphics g = CreateGraphics();
            for (int i = 0; i < 3; i++)
            {
                g.FillRectangle(Brushes.Gray, x, 400, 20, this.Height);
                g.DrawRectangle(Pens.Black, x, 400, 20, this.Height);
                x += 300;
            }
            ////////////////////////////////////////////////////////
            int X = 300;
            int Y = this.Height - 60;
            int W = 220;
            int H = 20;
            lastid2.Add(0);
            lastid3.Add(0);
            lastid.Add(0);
            for (int i = 0,J=5; i < 6; i++,J--)
            {

                rectangle pnn = new rectangle();
                pnn.X = X;
                pnn.Y = Y;
                pnn.w = W;
                pnn.h = H;
                pnn.id = J;
                pnn.TNum = 1;
                pnn.id2 = i + 1;
                pnn.id3 = i + 1;
                g.FillRectangle(Brushes.Yellow, pnn.X, pnn.Y, pnn.w, pnn.h);
                g.DrawRectangle(Pens.Black, pnn.X, pnn.Y, pnn.w, pnn.h);
                lastid.Add(pnn.id3);
                L.Add(pnn);
                X += 20;
                Y -= 20;
                W -= 40;

            }
        }
    }
}
