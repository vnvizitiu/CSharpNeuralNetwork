using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork_WinForms
{

    class Perceptron
    {
        public double[] weight;
        public double BiasWeight;
        public double Output;
        public double Sigma;

        public Perceptron(int _inputsCount, Random _rnd)
        {
            weight = new double[_inputsCount];
            for (int i = 0; i < weight.Length; i++)
                weight[i] = _rnd.NextDouble()-0.5;

            BiasWeight =2;
        }

        public double ShowMatrix(double[] _normalizedInputsMatrix)
        {
            double sum = BiasWeight;
            for (int i = 0; i < _normalizedInputsMatrix.Length; i++)
                sum += (_normalizedInputsMatrix[i] * weight[i]);

            Output = Math.Tanh(sum); //2 / (1 + Math.Exp(-sum)) - 1;

            return Output;
        }



    }
}
