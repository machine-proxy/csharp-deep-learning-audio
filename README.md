# csharp-deep-learning-audio

Deep Learning for Audio in CSharp and Keras

# Environment Configuration Before Install 

The packages runs in x64 and built with .NET 4.6.1. Therefore you need to 

* Set the "Target framework" to ".NET Framework 4.6.1" in the Application tab of your project Properties
* Set the "Platform Target" to x64 in the Build tab of your project Properties
* Set the .NET configuration manager to x64 in Visual Studio IDE


# Install

Make sure that you have the following installed:

* TensorFlowSharp Version 1.7.0 (used to run the tensorflow audio classifier trained models)

Run the following command in your nuget manager console:

```bash
Install-Package TensorFlow-Deep-Music -Version 1.0.2
```

The following dlls are also installed when installing the TensorFlow-Deep-Music:

* Bass.NET Version 1.0.0 (used to convert audio file to melgram)
* System.ValueTuple Version 4.4.0 (required by TensorFlowSharp)

# Usage

Below is the [code](csharp-deep-learning-audio-samples/Program.cs) showing how to use [Cifar10AudioClassifier](csharp-deep-learning-audio/Cifar10AudioClassifier.cs)
 to predict the genres of an audio file:

```cs
using System;
using csharp_deep_learning_audio;
using System.IO;

namespace csharp_deep_learning_audio_samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Cifar10AudioClassifier c = new Cifar10AudioClassifier();
            string dataDirPath = @"C:\Users\chen0\git\csharp-deep-learning-audio\gtzan\genres";

            string[] subDirectories = Directory.GetDirectories(dataDirPath);
            foreach(string subDirectory in subDirectories)
            {
                string[] files = Directory.GetFiles(subDirectory, "*.au");
                foreach(string file in files)
                {
                    Console.WriteLine("classifing: {0}", file);
                    Console.WriteLine("predicted: {0}", c.PredictLabel(file));
                    break;
                }
                
            }
        }
    }
}

```





