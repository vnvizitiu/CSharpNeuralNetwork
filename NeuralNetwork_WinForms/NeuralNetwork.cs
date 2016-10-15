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


        List<double[]> Points;
        int inputsCount;
        int outputsCount;
        int totalLayersCount;
        int perceptronsInHiddenLayer;
        Random rnd;

        Perceptron[][] p;

        public Form1()
        {
            InitializeComponent();
            Points = new List<double[]>();
            rnd = new Random();

            inputsCount = 12;
            outputsCount = 1;
            totalLayersCount = 4;
            perceptronsInHiddenLayer =5;

            InitTopology(inputsCount, outputsCount, totalLayersCount, perceptronsInHiddenLayer, rnd);

            pictureBoxResult.Refresh();
        }

        public void InitTopology(int _inputs, int _outputs, int _totalLayersCount, int _perceptronsInHiddenLayer, Random _rnd)
        {
            p = new Perceptron[_totalLayersCount][];// 3 layers

            p[0] = new Perceptron[inputsCount];//input layer
            p[_totalLayersCount - 1] = new Perceptron[outputsCount];//output layer 

            for (int i = 1; i < _totalLayersCount - 1; i++) //hidden layers
                p[i] = new Perceptron[_perceptronsInHiddenLayer];


            //initialize weight and other stuff
            for (int layer = 0; layer < p.Length; layer++)
                for (int perceptron = 0; perceptron < p[layer].Length; perceptron++)
                    if (layer - 1 < 0)
                        p[layer][perceptron] = new Perceptron(2,rnd);
                    else
                        p[layer][perceptron] = new Perceptron(p[layer - 1].Length,rnd);
        }


        private void pictureBoxResult_Paint(object sender, PaintEventArgs e)
        {
            try
            {

                var pen = new Pen(Color.Black, 2);
                for (int x = 0; x < 250; x++)
                    for (int y = 0; y < 250; y++)
                    {
                        double[] matrix = new double[2];
                        matrix[0] = ((((double)x) / 250) * 2) - 1;
                        matrix[1] = ((((double)y) / 250) * 2) - 1;

                        CalculateOutputs(matrix);

                        int color = (int)((p[totalLayersCount - 1][0].Output+1) * 127);

                        pen = new Pen(Color.FromArgb(255, color, color, color), 1);
                        e.Graphics.DrawLine(pen, (float)x, (float)y, (float)x + 1, (float)y + 1);
                    }

                if (Points != null && Points.Count > 0)
                    foreach (double[] point in Points)
                    {
                        if (point[2] == 1)
                            pen = new Pen(Color.Red, 1);
                        else
                            pen = new Pen(Color.LightSkyBlue, 1);
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

            double[] matrix = new double[3];
            matrix[0] = ((((double)e.X) / 250)*2)-1;
            matrix[1] = ((((double)e.Y) / 250) * 2) - 1;
            if (e.Button == MouseButtons.Left)
                matrix[2] = 1;
            else
                matrix[2] = -1;

            Points.Add(matrix);
            pictureBoxResult.Refresh();
        }


        private void CalculateOutputs(double[] _point)
        {
            for (int i = 0; i < p[0].Length; i++)
                p[0][i].ShowMatrix(_point);//show data input layers

            for (int layers = 1; layers < totalLayersCount; layers++)  // show data rest layers
                for (int i = 0; i < p[layers].Length; i++)
                {
                    double[] matrix = new double[p[layers - 1].Length];
                    for (int j = 0; j < p[layers - 1].Length; j++)
                        matrix[j] = p[layers - 1][j].Output;

                    p[layers][i].ShowMatrix(matrix);
                }
        }

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            //iterate all
            for (int i = 0; i < 2000; i++)
                foreach (double[] point in Points)
                {
                    CalculateOutputs(new double[] { point[0], point[1] });
                    CalculateSigma(point[2]);
                    UpgradeWeight(point);
                }

            pictureBoxResult.Refresh();
        }

        private void UpgradeWeight(double[] _point)
        {
            for (int layer = 0; layer < totalLayersCount; layer++)
                for (int i = 0; i < p[layer].Length; i++)
                {
                    double input = 0;
                    for (int j = 0; j < p[layer][i].weight.Length; j++)
                    {
                       
                        if (layer == 0)
                            input = _point[j];///pomylonailosc wejsc ziloscia neuronow wejsciowych
                        else
                            input = p[layer - 1][j].Output;
                        
                        p[layer][i].weight[j] += 0.002 * p[layer][i].Sigma * (1- (p[layer][i].Output * p[layer][i].Output)) * input;
                      
                    }

                     p[layer][i].BiasWeight += 0.002 * p[layer][i].Sigma * (1 - (p[layer][i].Output * p[layer][i].Output));

                }
        }

        private void CalculateSigma(double _expectation)
        {
            for (int i = 0; i < p[totalLayersCount - 1].Length; i++)
                p[totalLayersCount - 1][i].Sigma = _expectation - p[totalLayersCount - 1][i].Output;


            for (int layer = totalLayersCount - 2; layer >= 0; layer--)
                for (int i = 0; i < p[layer].Length; i++)
                {
                    double sigmaSum = 0;
                    for (int j = 0; j < p[layer + 1].Length; j++)
                        sigmaSum += p[layer + 1][j].weight[i] * p[layer + 1][j].Sigma;

                    p[layer][i].Sigma = sigmaSum;
                }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            InitTopology(inputsCount, outputsCount, totalLayersCount, perceptronsInHiddenLayer, rnd);
            Points = new List<double[]>();
            pictureBoxResult.Refresh();
        }
    }


}
