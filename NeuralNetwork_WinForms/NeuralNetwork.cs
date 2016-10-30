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
        Bitmap bmp;

        int inputsCount=3 ;
        int outputsCount=1;
        int totalLayersCount=2;
        int perceptronsInHiddenLayer=0;
        float learningRate=0.3f;

        public Form1()
        {
            InitializeComponent();

            nn = new NeuralNetwork(inputsCount, perceptronsInHiddenLayer, outputsCount, totalLayersCount);
             bmp = new Bitmap(250, 250);

            pictureBoxResult.Refresh();
        }



        private void pictureBoxResult_Paint(object sender, PaintEventArgs e)
        {
            if (nn.InputMatrix != null && nn.InputMatrix.Count > 0)
                for(int i=0; i < nn.InputMatrix.Count;i++)
                {
                    if (nn.OutputMatrix[i][0] == 1)
                        bmp.SetPixel((int)((nn.InputMatrix[i][0] + 1) * 125), (int)((nn.InputMatrix[i][1] + 1) * 125), Color.Red);
                    else if (nn.OutputMatrix[i][0] == -1)
                        bmp.SetPixel((int)((nn.InputMatrix[i][0] + 1) * 125), (int)((nn.InputMatrix[i][1] + 1) * 125), Color.Green);
                    else
                        bmp.SetPixel((int)((nn.InputMatrix[i][0] + 1) * 125), (int)((nn.InputMatrix[i][1] + 1) * 125), Color.Blue);
                }

            e.Graphics.DrawImage((Image)bmp, new Point(0, 0));
        }
    
        private void pictureBoxResult_MouseClick(object sender, MouseEventArgs e)
        {
            float[] InputMatrix = new float[inputsCount];
            float[] OutputMatrix = new float[outputsCount];

            InputMatrix[0] = ((((float)e.X) / 250) * 2) - 1;
            InputMatrix[1] = ((((float)e.Y) / 250) * 2) - 1;

            if (e.Button == MouseButtons.Left)
                OutputMatrix[0] = 1;
            else if(e.Button == MouseButtons.Middle)
                OutputMatrix[0] = 0;
            else
                OutputMatrix[0] = -1;

            nn.InputMatrix.Add(InputMatrix);
            nn.OutputMatrix.Add(OutputMatrix);
            pictureBoxResult.Refresh();
        }




        private void buttonLearn_Click(object sender, EventArgs e)
        {
            nn.Learn(10, learningRate);
            int[] color = new int[250 * 250];
            float[] matrix = new float[inputsCount];
            int index = 0;
            for (int x = 0; x < 250; x++)
            {
                matrix[0] = ((((float)x) / 250) * 2) - 1;

                for (int y = 0; y < 250; y++)
                {
                    matrix[1] = ((((float)y) / 250) * 2) - 1;
                    color[index] = (int)((nn.CalculateOutputs(matrix)[0] + 1) * 127);
                    index++;
                }
            }


            index = 0;
            for (int i = 0; i < 250; i++)
                for (int j = 0; j < 250; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb(255, color[index], color[index], color[index]));
                    index++;
                }

            pictureBoxResult.Refresh();
        }





        private void buttonClear_Click(object sender, EventArgs e)
        {
            nn = new NeuralNetwork(inputsCount, perceptronsInHiddenLayer, outputsCount, totalLayersCount);
            bmp = new Bitmap(250, 250);
            pictureBoxResult.Refresh();
        }
    }


}
