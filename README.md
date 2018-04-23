# csharp-deep-learning-audio

Deep Learning for Audio in CSharp and Keras

# Environment Configuration Before Install 

The packages runs in x64 and built with .NET 4.6.1. Therefore you need to 

* Set the "Target framework" to ".NET Framework 4.6.1" in the Application tab of your project Properties
* Set the "Platform Target" to x64 in the Build tab of your project Properties
* Set the .NET configuration manager to x64 in Visual Studio IDE




# Install

```bash
Install-Package TensorFlow-Deep-Music -Version 1.0.1
```

The following dlls are also installed when installing the TensorFlow-Deep-Music:

* Bass.NET Version 1.0.0 (used to convert audio file to melgram)
* TensorFlowSharp Version 1.7.0 (used to run the tensorflow audio classifier trained models)
* System.ValueTuple Version 4.4.0 (required by TensorFlowSharp)

# Usage

Below is the [code](csharp-deep-learning-audio-samples/Program.cs) showing how to use [Cifar10AudioClassifier](csharp-deep-learning-audio/Cifar10AudioClassifier.cs)
or [ResNetV2AudioClassifier](csharp-deep-learning-audio/ResNetV2AudioClassifier.cs) to predict the genres of an audio file:





