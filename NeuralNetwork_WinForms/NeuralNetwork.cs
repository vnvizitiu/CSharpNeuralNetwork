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

            nn= new NeuralNetwork(9, 1, 3, 5);

            pictureBoxResult.Refresh();
        }



        private void pictureBoxResult_Paint(object sender, PaintEventArgs e)
        {
            try
            {

                var pen = new Pen(Color.Black, 2);
                for (int x = 0; x < 250; x++)
                    for (int y = 0; y < 250; y++)
                    {
                        float[] matrix = new float[2];
                        matrix[0] = ((((float)x) / 250) * 2) - 1;
                        matrix[1] = ((((float)y) / 250) * 2) - 1;

                       

                        int color = (int)((nn.CalculateOutputs(matrix)+1) * 127);

                        pen = new Pen(Color.FromArgb(255, color, color, color), 1);
                        e.Graphics.DrawLine(pen, (float)x, (float)y, (float)x + 1, (float)y + 1);
                    }

                if (nn.InputWithOneOutputMatrix != null && nn.InputWithOneOutputMatrix.Count > 0)
                    foreach (float[] point in nn.InputWithOneOutputMatrix)
                    {
                        if (point[2] == 1)
                            pen = new Pen(Color.Red, 1);
                        else if(point[2] == -1)
                            pen = new Pen(Color.LightSkyBlue, 1);
                        else
                            pen = new Pen(Color.GreenYellow, 1);
                        e.Graphics.DrawEllipse(pen, ((float)point[0] +1)*125, ((float)point[1] + 1) * 125, 3, 3);
                    }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }

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
            nn = new NeuralNetwork(9, 1, 3, 5);
            pictureBoxResult.Refresh();
        }
    }


}
