using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_WinForms
{

    class Perceptron
    {
        public float[] weight;
        public float BiasWeight;
        public float Output;
        public float Sigma;

        public Perceptron(int _inputsCount, Random _rnd)
        {
            weight = new float[_inputsCount];
            for (int i = 0; i < weight.Length; i++)
                weight[i] = (float)(_rnd.NextDouble()-0.5);

            BiasWeight =2;
        }

        public float ShowMatrix(float[] _normalizedInputsMatrix)
        {
            float sum = BiasWeight;
            for (int i = 0; i < _normalizedInputsMatrix.Length; i++)
                sum += (_normalizedInputsMatrix[i] * weight[i]);

            Output = (float)Math.Tanh(sum); //2 / (1 + Math.Exp(-sum)) - 1;

            return Output;
        }



    }
    class NeuralNetwork
    {
        public List<float[]> InputWithOneOutputMatrix;
        int inputsCount;
        int outputsCount;
        int totalLayersCount;
        int perceptronsInHiddenLayer;
        float learningRate;
        Random rnd;

        Perceptron[][] p;


        public NeuralNetwork(int _inputs, int _outputs, int _totalLayersCount, int _perceptronsInHiddenLayer)
        {
            inputsCount = _inputs;
            outputsCount = _outputs;
            totalLayersCount = _totalLayersCount;
            perceptronsInHiddenLayer = _perceptronsInHiddenLayer;
            rnd = new Random();
            InputWithOneOutputMatrix = new List<float[]>();

            InitTopology();          
        }

        private void InitTopology()
        {
            p = new Perceptron[totalLayersCount][];// 3 layers

            p[0] = new Perceptron[inputsCount];//input layer
            p[totalLayersCount - 1] = new Perceptron[outputsCount];//output layer 

            for (int i = 1; i < totalLayersCount - 1; i++) //hidden layers
                p[i] = new Perceptron[perceptronsInHiddenLayer];


            //initialize weight and other stuff
            for (int layer = 0; layer < p.Length; layer++)
                for (int perceptron = 0; perceptron < p[layer].Length; perceptron++)
                    if (layer - 1 < 0)
                        p[layer][perceptron] = new Perceptron(2, rnd);
                    else
                        p[layer][perceptron] = new Perceptron(p[layer - 1].Length, rnd);
        }

        public float CalculateOutputs(float[] _point)
        {
            for (int i = 0; i < p[0].Length; i++)
                p[0][i].ShowMatrix(_point);//show data input layers

            for (int layers = 1; layers < totalLayersCount; layers++)  // show data rest layers
                for (int i = 0; i < p[layers].Length; i++)
                {
                    float[] matrix = new float[p[layers - 1].Length];
                    for (int j = 0; j < p[layers - 1].Length; j++)
                        matrix[j] = p[layers - 1][j].Output;

                    p[layers][i].ShowMatrix(matrix);
                }

            return p[totalLayersCount - 1][0].Output;
        }

        public  void UpgradeWeight(float[] _point)
        {
            for (int layer = 0; layer < totalLayersCount; layer++)
                for (int i = 0; i < p[layer].Length; i++)
                {
                    float input = 0;
                    float func = learningRate * p[layer][i].Sigma * (1 - (p[layer][i].Output * p[layer][i].Output));


                    for (int j = 0; j < p[layer][i].weight.Length; j++)
                    {

                        if (layer == 0)
                            input = _point[j];///pomylonailosc wejsc ziloscia neuronow wejsciowych
                        else
                            input = p[layer - 1][j].Output;

                        p[layer][i].weight[j] += func * input;

                    }

                    p[layer][i].BiasWeight += func;

                }
        }

        public void CalculateSigma(float _expectation)
        {
            for (int i = 0; i < p[totalLayersCount - 1].Length; i++)
                p[totalLayersCount - 1][i].Sigma = _expectation - p[totalLayersCount - 1][i].Output;


            for (int layer = totalLayersCount - 2; layer >= 0; layer--)
                for (int i = 0; i < p[layer].Length; i++)
                {
                    float sigmaSum = 0;
                    for (int j = 0; j < p[layer + 1].Length; j++)
                        sigmaSum += p[layer + 1][j].weight[i] * p[layer + 1][j].Sigma;

                    p[layer][i].Sigma = sigmaSum;
                }
        }

        public void Learn(int epoch = 2000, double _learningRate = 0.02)
        {
            learningRate = (float)_learningRate;
            for (int i = 0; i < epoch; i++)
                foreach (float[] point in InputWithOneOutputMatrix)
                {
                    CalculateOutputs(new float[] { point[0], point[1] });
                    CalculateSigma(point[2]);
                    UpgradeWeight(point);
                }
        }

    }
}
