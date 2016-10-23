using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork_WinForms
{
    public partial class Form1 : Form
    {

        NeuralNetwork nn;



        public Form1()
        {
            InitializeComponent();

            nn = new NeuralNetwork(9, 5, 1, 4);

            pictureBoxResult.Refresh();
        }



        private void pictureBoxResult_Paint(object sender, PaintEventArgs e)
        {
            float[] matrix = new float[2];
            Bitmap bmp = new Bitmap(250, 250);
            var pen = new Pen(Color.Black, 2);

            for (int x = 0; x < 250; x++)
            {
                matrix[0] = ((((float)x) / 250) * 2) - 1;

                for (int y = 0; y < 250; y++)
                {
                    matrix[1] = ((((float)y) / 250) * 2) - 1;
                    int color = (int)((nn.CalculateOutputs(matrix) + 1) * 127);

                    bmp.SetPixel(x, y, Color.FromArgb(255, color, color, color));
                }
            }



            if (nn.InputWithOneOutputMatrix != null && nn.InputWithOneOutputMatrix.Count > 0)
                foreach (float[] point in nn.InputWithOneOutputMatrix)
                {
                    if (point[2] == 1)
                        bmp.SetPixel((int)((float)point[0] + 1) * 125, (int)((float)point[1] + 1) * 125, Color.Red);
                    else if (point[2] == -1)
                        bmp.SetPixel((int)((float)point[0] + 1) * 125, (int)((float)point[1] + 1) * 125, Color.Green);
                    else
                        bmp.SetPixel((int)((float)point[0] + 1) * 125, (int)((float)point[1] + 1) * 125, Color.Blue);
                }

            e.Graphics.DrawImage((Image)bmp, new Point(0, 0));
        }
    


        private void pictureBoxResult_MouseClick(object sender, MouseEventArgs e)
        {

            float[] matrix = new float[3];
            matrix[0] = ((((float)e.X) / 250)*2)-1;
            matrix[1] = ((((float)e.Y) / 250) * 2) - 1;

            if (e.Button == MouseButtons.Left)
                matrix[2] = 1;
            else if(e.Button == MouseButtons.Middle)
                matrix[2] = 0;
            else
                matrix[2] = -1;

            nn.InputWithOneOutputMatrix.Add(matrix);
            pictureBoxResult.Refresh();
        }




        private void buttonLearn_Click(object sender, EventArgs e)
        {
            nn.Learn();
            pictureBoxResult.Refresh();
        }





        private void buttonClear_Click(object sender, EventArgs e)
        {
            nn = new NeuralNetwork(9, 5, 1, 4);
            pictureBoxResult.Refresh();
        }
    }


}
