#region Copyright (C) Simon Bridewell, Kevin Weiner, Anthony Dekker
// 
// This file is part of the GifComponents library.
// GifComponents is free software; you can redistribute it and/or
// modify it under the terms of the Code Project Open License.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Code Project Open License for more details.
// 
// You can read the full text of the Code Project Open License at:
// http://www.codeproject.com/info/cpol10.aspx
//
// GifComponents is a derived work based on NGif written by gOODiDEA.NET
// and published at http://www.codeproject.com/KB/GDI-plus/NGif.aspx,
// with an enhancement by Phil Garcia published at
// http://www.thinkedge.com/blogengine/post/2008/02/20/Animated-GIF-Encoder-for-NET-Update.aspx
//
// Simon Bridewell makes no claim to be the original author of this library,
// only to have created a derived work.
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using SpriteVortex.Helpers.GifComponents.Components;

namespace SpriteVortex.Helpers.GifComponents.Tools
{
	/// <summary>
	/// NeuQuant Neural-Net Quantization Algorithm
	/// ------------------------------------------
	/// 
	/// Copyright (c) 1994 Anthony Dekker
	/// 
	/// NEUQUANT Neural-Net quantization algorithm by Anthony Dekker, 1994.
	/// See "Kohonen neural networks for optimal colour quantization"
	/// in "Network: Computation in Neural Systems" Vol. 5 (1994) pp 351-367.
	/// for a discussion of the algorithm.
	/// 
	/// http://members.ozemail.com.au/~dekker/NeuQuant.pdf
	/// 
	/// Any party obtaining a copy of these files from the author, directly or
	/// indirectly, is granted, free of charge, a full and unrestricted irrevocable,
	/// world-wide, paid up, royalty-free, nonexclusive right and license to deal
	/// in this software and documentation files (the "Software"), including without
	/// limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
	/// and/or sell copies of the Software, and to permit persons who receive
	/// copies from any such party to do so, with the only requirement being
	/// that this copyright notice remain intact.
	///
	/// Ported to Java 12/00 K Weiner
	/// 
	/// Modified by Simon Bridewell, June 2009 - April 2010:
	/// Downloaded from 
	/// http://www.thinkedge.com/BlogEngine/file.axd?file=NGif_src2.zip
	/// * Adapted for FxCop code analysis compliance and documentation comments 
	///   converted to .net XML comments.
	/// * Removed "len" parameter from constructor.
	/// * Added AnalysingPixel property.
	/// * Made Learn, UnbiasNetwork and BuildIndex methods private.
	/// * Derive from LongRunningProcess to allow use of ProgressCounters.
	/// * Renamed lots of private members, local variables and methods.
	/// * Refactored some code into separate methods:
	/// 	GetPixelIncrement
	/// 	ManhattanDistance
	/// 	MoveNeuron
	/// 	MoveNeighbouringNeurons
	/// 	MoveNeighbour
	/// 	SetNeighbourhoodAlphas
	/// 	IndexOfLeastGreenNeuron
	/// 	SwapNeurons
	/// 	SwapValues
	/// * Added lots of comments.
	/// TODO: consider a separate Neuron class - R, G, B, frequency and bias properties?
	/// TODO: consider a separate NeuralNetwork class, not specific to quantizing images
	/// TODO: consider an abstract Quantizer base class
	/// TODO: consider a NeuralNetworkParameters class to replace the constants defined here
	/// </summary>
	[SuppressMessage("Microsoft.Naming", 
	                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
	                 MessageId = "Neu")]
	public class NeuQuant 
	{
		#region constants
		
		/// <summary>
		/// Number of neurons in the neural network
		/// -- or (in this implementation) --
		/// Maximum number of colours in the output image.
		/// </summary>
		private const int _neuronCount = 256;
		
		// Four primes near 500 - assume no image has a length so large
		// that it is divisible by all four primes
		private const int _prime1 = 499;
		private const int _prime2 = 491;
		private const int _prime3 = 487;
		private const int _prime4 = 503;
		
		/// <summary>
		/// Minimum size for input image.
		/// If the image has fewer pixels than this then the learning loop will
		/// step through its pixels 3 at a time rather than using one of the
		/// four prime constants, and the sample factor supplied to the 
		/// constructor will be overridden by a value of 1.
		/// </summary>
		private const int _minPictureBytes = ( 3 * _prime4 );

		/* Network Definitions
		   ------------------- */
		
		/// <summary>
		/// Highest possible index for a neuron in the neural network
		/// (the network is a zero-based array).
		/// </summary>
		private const int _highestNeuronIndex = _neuronCount - 1;
		
		/// <summary>
		/// Bias for colour values.
		/// Controls the relationship between actual colour intensities (0 to 
		/// 255) and the colour values held in the network's neurons. 
		/// The larger this value, the larger the values held in the neurons 
		/// will be in comparison to the actual colour intensities.
		/// </summary>
		private const int _netBiasShift = 4; /* bias for colour values */
		
		/// <summary>
		/// Number of learning cycles. The greater this value, the more often
		/// the alpha values used to move neurons will be decremented.
		/// </summary>
		private const int _numberOfLearningCycles = 100; /* no. of learning cycles */

		#region constants for frequency and bias
		
		/// <summary>
		/// The larger this value is, the larger _intbias will be.
		/// Larger values will also make the bias of a neuron a more significant
		/// factor than the distance from the supplied colour when identifying
		/// the best neuron for a given colour.
		/// </summary>
		private const int _intBiasShift = 16; /* bias for fractions */
		
		/// <summary>
		/// The larger this value is, the higher the initial frequency will be
		/// for each neuron, and the more the bias and frequency of the closest
		/// neuron will be adjusted by during the learning loop.
		/// </summary>
		private const int _intBias = (((int) 1) << _intBiasShift);
		
		/// <summary>
		/// The larger this value is, the larger _beta will be.
		/// Larger values also result in the bias of all neurons being increased
		/// by a greater amount in each iteration through the learning process.
		/// </summary>
		private const int _gammaShift = 10; /* gamma = 1024 */
		
		/// <summary>
		/// The larger this value is, the smaller 
		/// _closestNeuronFrequencyIncrement and _closestNeuronBiasDecrement 
		/// will be.
		/// This means that larger values will also result in the frequency of 
		/// all neurons being decreased by less and the bias being increased by 
		/// less at each step of the learning loop.
		/// </summary>
		private const int _betaShift = 10;
		
		/// <summary>
		/// The larger this value is, the more the frequency of the closest 
		/// neuron will be increased by during the learning loop.
		/// </summary>
		/// <remarks>This member was originally called _beta</remarks>
		private const int _closestNeuronFrequencyIncrement = (_intBias >> _betaShift); /* beta = 1/1024 */
		
		/// <summary>
		/// The larger this value is, the more the bias of the closest neuron
		/// will be decreased by during the learning loop.
		/// </summary>
		/// <remarks>This member was originally called _betagamma</remarks>
		private const int _closestNeuronBiasDecrement =
			(_intBias << (_gammaShift - _betaShift));
		#endregion

		#region constants controlling radius factor / neighbourhood size
		/// <summary>
		/// Initial radius.
		/// The initial unbiased neuron neighbourhood size is set to this 
		/// multiplied by the neighbourhood size bias.
		/// This is also the size of the array of alphas for shifting 
		/// neighbouring neurons.
		/// </summary>
		private const int _initialRadius = (_neuronCount >> 3);
		
		/// <summary>
		/// The neuron neighbourhood size is set by shifting the unbiased 
		/// neighbourhood size this many bits to the right.
		/// </summary>
		private const int _neighbourhoodSizeBiasShift = 6; /* at 32.0 biased by 6 bits */
		
		/// <summary>
		/// Raduis bias.
		/// The initial unbiased neuron neighbourhood size is set to this
		/// multiplied by the initial radius.
		/// </summary>
		private const int _radiusBias = (((int) 1) << _neighbourhoodSizeBiasShift);
		
		/// <summary>
		/// The initial value for the unbiased size of a neuron neighbourhood.
		/// </summary>
		private const int _initialUnbiasedNeighbourhoodSize = (_initialRadius * _radiusBias); /* and decreases by a */

		/// <summary>
		/// Factor for reducing the unbiased neighbourhood size.
		/// </summary>
		private const int _unbiasedNeighbourhoodSizeDecrement = 30;
		#endregion

		#region constants for decreasing alpha factor
		/// <summary>
		/// The initial value of alpha will be set to 1, left shifted by this
		/// many bits.
		/// </summary>
		private const int _alphaBiasShift = 10;
		
		/// <summary>
		/// The starting value of alpha.
		/// Alpha is a factor which controls how far neurons are moved during
		/// the learning loop, and it decreases as learning proceeds.
		/// </summary>
		private const int _initialAlpha = (((int) 1) << _alphaBiasShift);

		/// <summary>
		/// Controls how much alpha decreases by at each step of the learning
		/// loop - alpha will be decremented by alpha divided by this value, so
		/// the larger this value, the more slowly alpha will be reduced.
		/// </summary>
		private int _alphaDecrement;
		#endregion

		#region constants for radius calculations
		/// <summary>
		/// The greater this value, the greater _radBias and _alphaRadBiasShift
		/// will be.
		/// </summary>
		private const int _radBiasShift = 8;

		/// <summary>
		/// The greater this value, the larger alpha will be, and the more 
		/// neighbouring neurons will be moved by during the learning process.
		/// </summary>
		private const int _radBias = (((int) 1) << _radBiasShift);

		/// <summary>
		/// The greater this value, the greater _alphaRadBias will be, and so
		/// the less neighbourint neurons will be moved by during the learning
		/// process.
		/// </summary>
		private const int _alphaRadBiasShift = (_alphaBiasShift + _radBiasShift);

		/// <summary>
		/// The greater this value, the less neighbouring neurons will be moved 
		/// by during the learning process.
		/// </summary>
		private const int _alphaRadBias = (((int) 1) << _alphaRadBiasShift);
		#endregion

		#endregion

		#region declarations
		
		/* Types and Global Variables
		-------------------------- */

		/// <summary>
		/// A collection of byte colour intensities, in the order red, green, 
		/// blue, representing the colours of each of the pixels in an image
		/// to be quantized.
		/// </summary>
		private byte[] _thePicture; /* the input image itself */
		
		/// <summary>
		/// Number of pixels in the input image.
		/// </summary>
		private int _pixelCount; /* lengthcount = H*W*3 */

		/// <summary>
		/// Sampling factor. Minimum of 1.
		/// Lower values mean more of the pixels of the image will be examined
		/// hence better image quality but slower processing.
		/// Higher values mean fewer of the pixels will be examined, so poorer
		/// image quality but faster processing.
		/// The larger _samplingFactor is, the fewer of the pixels in the input
		/// image will be examined, and the slower the value of alpha will be 
		/// reduced during the learning loop.
		/// </summary>
		private int _samplingFactor;

		/// <summary>
		/// The neural network used to quantize the image.
		/// This is a two-dimensional array, with each element of the first 
		/// dimension representing one of the colours in the colour table of 
		/// the quantized output image, and the elements of the second dimension 
		/// representing the blue, green and red components of those colours, 
		/// and the original index of the neuron in the network before the
		/// neurons are reordered in the BuildIndex method.
		/// </summary>
		private int[][] _network;

		/// <summary>
		/// Used for locating colours in the neural network - the index of this
		/// array is the green value of the colour to look for.
		/// </summary>
		private int[] _indexOfGreen = new int[_neuronCount];

		/* bias and freq arrays for learning */
		
		/// <summary>
		/// Array of biases for each neuron.
		/// For frequently chosen neurons this will be negative.
		/// </summary>
		private int[] _bias = new int[_neuronCount];

		/// <summary>
		/// Array indicating how frequently each neuron is chosen during the
		/// learning process as the closest neuron to a given colour.
		/// For frequently chosen neurons this will be high.
		/// </summary>
		private int[] _frequency = new int[_neuronCount];
		
		/// <summary>
		/// Alpha values controlling how far towards a target colour any 
		/// neighbouring are moved.
		/// </summary>
		private int[] _neighbourhoodAlphas = new int[_initialRadius];
		
		#endregion

		#region constructor
		/// <summary>
		/// Constructor.
		/// Initialise network in range (0,0,0) to (255,255,255) and set parameters
		/// TODO: consider accepting a collection / array of Colors instead of a byte array.
		/// </summary>
		/// <param name="thePicture">
		/// A collection of byte colour intensities, in the order red, green, 
		/// blue, representing the colours of each of the pixels in an image.
		/// </param>
		/// <param name="samplingFactor">
		/// Sampling factor. Minimum of 1.
		/// Lower values mean more of the pixels of the image will be examined
		/// hence better image quality but slower processing.
		/// Higher values mean fewer of the pixels will be examined, so poorer
		/// image quality but faster processing.
		/// Set to 1 to examine every pixel in the image.
		/// </param>
		public NeuQuant( byte[] thePicture, int samplingFactor )
		{
			if( thePicture == null )
			{
				throw new ArgumentNullException( "thePicture" );
			}
			
			int[] thisNeuron;

			_thePicture = thePicture;
			_pixelCount = thePicture.Length / 3;
			_samplingFactor = samplingFactor;

			// Initialise the number of neurons in the neural network to the 
			// maximum number of colours allowed in the output image.
			_network = new int[_neuronCount][];
			
			for( int neuronIndex = 0; neuronIndex < _neuronCount; neuronIndex++ ) 
			{
				// Initialise the size of the neuron to 4 - 3 for red, green and
				// blue, and one for the neuron's original index in the network.
				_network[neuronIndex] = new int[4];
				
				// Make a reference to this neuron
				thisNeuron = _network[neuronIndex];
				
				// Initialise the values of the neurons so that they lie on a 
				// diagonal line from 0,0,0 to 4080,4080,4080. They will be 
				// adjusted during the learning loop as the pixels of the image
				// are analysed.
				thisNeuron[0] = thisNeuron[1] = thisNeuron[2] 
					= (neuronIndex << (_netBiasShift + 8)) / _neuronCount;
				
				// Initialise the frequency and bias of each neuron.
				_frequency[neuronIndex] = _intBias / _neuronCount; /* 1/netsize */
				_bias[neuronIndex] = 0;
			}
		}
		#endregion

		#region Process method
		/// <summary>
		/// Calls the Learn, UnbiasNetwork and BuildIndex method and returns the
		/// ColourMap.
		/// </summary>
		/// <returns>
		/// The colour table containing the colours of the image after 
		/// quantization.
		/// </returns>
		public ColourTable Process()
		{
			Learn();
			UnbiasNetwork();
			BuildIndex();
			return ColourMap();
		}
		#endregion
	
		#region Map method
		/// <summary>
		/// Search for BGR values 0..255 (after net is unbiased) and return 
		/// the index in the colour table of the colour closest to the supplied
		/// colour.
		/// </summary>
		/// <param name="blue">
		/// The blue component of the input colour.
		/// </param>
		/// <param name="green">
		/// The green component of the input colour.
		/// </param>
		/// <param name="red">
		/// The red component of the input colour.
		/// </param>
		/// <returns>
		/// The index in the colour table of the colour closest to the supplied
		/// colour.
		/// </returns>
		public int Map( int blue, int green, int red )
		{

			int distance, distanceIncrement;
			int[] thisNeuron;
			int best;

			int bestDistance = 1000; /* biggest possible dist is 256*3 */
			best = -1;
			
			// Start searching the network at the neuron with a green value
			// closest to the supplied green value and work outwards
			int highNeuronIndex = _indexOfGreen[green];
			int lowNeuronIndex = highNeuronIndex - 1;

			while( (highNeuronIndex < _neuronCount) || (lowNeuronIndex >= 0) ) 
			{
				if( highNeuronIndex < _neuronCount ) 
				{
					thisNeuron = _network[highNeuronIndex];
					distance = thisNeuron[1] - green; /* inx key */
					if( distance >= bestDistance )
					{
						highNeuronIndex = _neuronCount; /* stop iter */
					}
					else 
					{
						highNeuronIndex++;
						if( distance < 0 )
						{
							distance = -distance;
						}
						distanceIncrement = Math.Abs( thisNeuron[0] - blue );
						distance += distanceIncrement;
						if( distance < bestDistance ) 
						{
							distanceIncrement = Math.Abs( thisNeuron[2] - red );
							distance += distanceIncrement;
							if( distance < bestDistance ) 
							{
								bestDistance = distance;
								best = thisNeuron[3];
							}
						}
					}
				}
				if( lowNeuronIndex >= 0 ) 
				{
					thisNeuron = _network[lowNeuronIndex];
					distance = green - thisNeuron[1];
					if( distance >= bestDistance )
					{
						lowNeuronIndex = -1; /* stop iter */
					}
					else 
					{
						lowNeuronIndex--;
						if( distance < 0 )
						{
							distance = -distance; // TESTME: Map - dist < 0
						}
						distanceIncrement = Math.Abs( thisNeuron[0] - blue );
						distance += distanceIncrement;
						if( distance < bestDistance ) 
						{
							distanceIncrement = Math.Abs( thisNeuron[2] - red );
							distance += distanceIncrement;
							if( distance < bestDistance ) 
							{
								bestDistance = distance;
								best = thisNeuron[3];
							}
						}
					}
				}
			}
			return best;
		}
		#endregion

		#region private methods
		
		#region Learn method and methods called by it
		
		#region private Learn method
		/// <summary>
		/// Main Learning Loop
		/// </summary>
		private void Learn() 
		{
			int closestNeuronIndex, blue, green, red;
			byte[] p;

			if( _pixelCount < _minPictureBytes )
			{
				_samplingFactor = 1;
			}
			
			// The larger _alphaDecrement is, the slower the value of alpha will 
			// be reduced during the learning process.
			// Therefore the larger _samplingFactor is, the slower the value of
			// aplha will be reduced.
			_alphaDecrement = 30 + ((_samplingFactor - 1) / 3);
			
			p = _thePicture;
			
			int pixelIndex = 0;
			
			// Set the number of pixels in the image to be examined during
			// the learning loop. 
			// If _sampleFactor is 1 then every pixel in the image will be 
			// examined. 
			// If _sampleFactor is 10 then one tenth of the pixels will be 
			// examined.
			int pixelsToExamine = _pixelCount / _samplingFactor;
			
			// Allows a ResponsiveForm to track the progress of this method
		    //AddCounter( learnCounterText, pixelsToExamine );
			
			// Set how often the alpha value for shifting neurons is updated.
			// 1 means it is updated once per pixel examined, 10 means it is 
			// updated every 10 pixels, and so on.
			int alphaUpdateFrequency = pixelsToExamine / _numberOfLearningCycles;
			
			// Alpha is a factor which controls how far neurons are moved during
			// the learning loop, and it decreases as learning proceeds.
			int alpha = _initialAlpha;
			
			// Set the size of the neighbourhood which makes up the neighbouring
			// neurons which also need to be moved when a neuron is moved.
			int unbiasedNeighbourhoodSize = _initialUnbiasedNeighbourhoodSize;
			int neighbourhoodSize 
				= unbiasedNeighbourhoodSize >> _neighbourhoodSizeBiasShift;
			if( neighbourhoodSize <= 1 )
			{
				neighbourhoodSize = 0; // TESTME: Learn - neighbourhoodSize <= 1
			}
			
			// Set the initial alpha values for neighbouring neurons.
			SetNeighbourhoodAlphas( neighbourhoodSize, alpha );

			int step = GetPixelIndexIncrement();

			int pixelsExamined = 0;
			while( pixelsExamined < pixelsToExamine ) 
			{
				// Update the progress counter for the benefit of the UI
				//MyProgressCounters[learnCounterText].Value = pixelsExamined;
				
				blue = (p[pixelIndex + 0] & 0xff) << _netBiasShift;
				green = (p[pixelIndex + 1] & 0xff) << _netBiasShift;
				red = (p[pixelIndex + 2] & 0xff) << _netBiasShift;
				closestNeuronIndex = FindClosestAndReturnBestNeuron( blue, green, red );

				// Move this neuron closer to the colour of the current pixel
				// by a factor of alpha.
				MoveNeuron( alpha, closestNeuronIndex, blue, green, red );
				
				// If appropriate, move neighbouring neurons closer to the 
				// colour of the current pixel.
				if( neighbourhoodSize != 0 )
				{
					MoveNeighbouringNeurons( neighbourhoodSize, 
					                         closestNeuronIndex, 
					                         blue, 
					                         green, 
					                         red );
				}

				// Move on to the next pixel to be examined
				pixelIndex += step;
				if( pixelIndex >= _pixelCount )
				{
					// We've gone past the end of the image, so wrap round to
					// the start again
					pixelIndex -= _pixelCount;
				}

				// Keep track of how many pixels have been examined so far
				pixelsExamined++;
				
				if( alphaUpdateFrequency == 0 )
				{
					// Ensure delta is greater than zero to avoid a 
					// divide-by-zero error.
					alphaUpdateFrequency = 1;
				}
				
				// Is it time to update the alpha values for moving neurons?
				if( pixelsExamined % alphaUpdateFrequency == 0 ) 
				{
					alpha -= alpha / _alphaDecrement;
					unbiasedNeighbourhoodSize -= unbiasedNeighbourhoodSize / _unbiasedNeighbourhoodSizeDecrement;
					neighbourhoodSize 
						= unbiasedNeighbourhoodSize >> _neighbourhoodSizeBiasShift;
					
					if( neighbourhoodSize <= 1 )
					{
						neighbourhoodSize = 0;
					}
					
					// Update the alpha values to be used for moving 
					// neighbouring neurons.
					SetNeighbourhoodAlphas( neighbourhoodSize, alpha );
				}
			}
			
			// This method is finished now so the UI doesn't need to monitor it
			// any more.
			//RemoveCounter( learnCounterText );
		}
		#endregion
		
		#region private SetNeighbourhoodAlphas method
		/// <summary>
		/// Sets the alpha values for moving neighbouring neurons.
		/// </summary>
		private void SetNeighbourhoodAlphas( int neighbourhoodSize, int alpha )
		{
			int neighbourhoodSizeSquared = neighbourhoodSize * neighbourhoodSize;
			for( int alphaIndex = 0; alphaIndex < neighbourhoodSize; alphaIndex++ )
			{
				_neighbourhoodAlphas[alphaIndex] =
					alpha 
					* 
					(
						(
							(
								neighbourhoodSizeSquared
								- 
								( alphaIndex * alphaIndex )
							) 
							* 
							_radBias
						) 
						/ 
						neighbourhoodSizeSquared
					);
			}
		}
		#endregion
		
		#region private GetPixelIndexIncrement method
		/// <summary>
		/// Calculates an increment to step through the pixels of the image, 
		/// such that all pixels will eventually be examined, but not 
		/// sequentially.
		/// This is required because the learning loop needs to examine the
		/// pixels in a psuedo-random order.
		/// </summary>
		/// <returns>The increment.</returns>
		private int GetPixelIndexIncrement()
		{
			int step;
			if( _pixelCount < _minPictureBytes )
			{
				step = 3;
			}
			else if( (_pixelCount % _prime1) != 0 )
			{
				// The number of pixels is not divisible by the first prime number
				step = 3 * _prime1;
			}
			else if( (_pixelCount % _prime2) != 0 )
			{
				// TESTME: GetPixelIndexIncrement - _lengthcount >= _minpicturebytes and divisible by _prime1
				// The number of pixels is not divisible by the second prime number
				step = 3 * _prime2;
			}
			else if( (_pixelCount % _prime3) != 0 )
			{
				// The number of pixels is not divisible by the third prime number
				step = 3 * _prime3;
			}
			else
			{
				// The number of pixels is divisible by the first, second and
				// third prime numbers.
				step = 3 * _prime4;
			}
			return step;
		}
		#endregion
		
		#region private FindClosestAndReturnBestNeuron method and methods called by it
		
		#region private FindClosestAndReturnBestNeuron method
		/// <summary>
		/// Search for biased BGR values
		/// Finds the neuron which is closest to the supplied colour, increases
		/// its frequency and decreases its bias.
		/// Finds the best neuron (close to the supplied colour but not already
		/// chosen too many times) and returns its index in the neural network.
		/// </summary>
		/// <param name="blue">The blue component of the colour</param>
		/// <param name="green">The green component of the colour</param>
		/// <param name="red">The red component of the colour</param>
		/// <returns>
		/// The index in the neural network of a neuron which is close to the 
		/// supplied colour but which hasn't already been chosen too many times.
		/// </returns>
		/// <remarks>
		/// This method was originally called Contest.
		/// </remarks>
		private int FindClosestAndReturnBestNeuron( int blue, int green, int red ) 
		{

			/* finds closest neuron (min dist) and updates freq */
			/* finds best neuron (min dist-bias) and returns position */
			/* for frequently chosen neurons, freq[i] is high and bias[i] is negative */
			/* bias[i] = gamma*((1/netsize)-freq[i]) */

			int distance, biasDistance, betafreq;
			int[] thisNeuron;

			// Initialise closest neuron distance and its index in the network
			int closestDistance = ~(((int) 1) << 31); // bitwise complement of 2^31 - returns 2147483647
			int bestBiasDistance = closestDistance;
			int closestNeuronIndex = -1;
			int bestBiasNeuronIndex = closestNeuronIndex;

			for( int neuronIndex = 0; neuronIndex < _neuronCount; neuronIndex++ ) 
			{
				// How close is this neuron to the supplied colour?
				thisNeuron = _network[neuronIndex];
				distance = ManhattanDistance( thisNeuron, red, green, blue );
				
				// If it's closer than the closest one found so far, then it's
				// now the closest one.
				if( distance < closestDistance ) 
				{
					closestDistance = distance;
					closestNeuronIndex = neuronIndex;
				}
				
				// Bias distance takes into account the distance between the
				// neuron and the colour, and also the neuron's bias.
				// The more frequently a neuron has already been chosen, the
				// lower its bias, so less frequently-chosen neurons have a 
				// better chance of being returned by this method.
				// This ensures that the distribution of neurons is densest in
				// areas of the spectrum which have most colours in the input
				// image.
				biasDistance = distance - ((_bias[neuronIndex]) >> (_intBiasShift - _netBiasShift));
				if( biasDistance < bestBiasDistance ) 
				{
					bestBiasDistance = biasDistance;
					bestBiasNeuronIndex = neuronIndex;
				}
				
				// Decrease the frequency and increase the bias for all neurons.
				betafreq = _frequency[neuronIndex] >> _betaShift;
				_frequency[neuronIndex] -= betafreq;
				_bias[neuronIndex] += (betafreq << _gammaShift);
			}
			
			// Increase the frequency and decrease the bias for just the closest
			// neuron.
			_frequency[closestNeuronIndex] += _closestNeuronFrequencyIncrement;
			_bias[closestNeuronIndex] -= _closestNeuronBiasDecrement;
			return bestBiasNeuronIndex;
		}
		#endregion
		
		#region private static ManhattanDistance method
		/// <summary>
		/// Calculates how close the colour represented by the supplied red,
		/// green and blue values is to the colour held in the supplied neuron.
		/// </summary>
		/// <param name="neuron">
		/// A neuron with blue, green and red values held in its first three
		/// elements.
		/// </param>
		/// <param name="red">The red intensity of the supplied colour</param>
		/// <param name="green">The green intensity of the supplied colour</param>
		/// <param name="blue">The blue intensity of the supplied colour</param>
		/// <returns>The distance between the two colours</returns>
		private static int ManhattanDistance( int[] neuron, int red, int green, int blue )
		{
			int distance 
				= Math.Abs( neuron[0] - blue )
				+ Math.Abs( neuron[1] - green )
				+ Math.Abs( neuron[2] - red );
			return distance;
		}
		#endregion
		
		#endregion
		
		#region private MoveNeuron method
		/// <summary>
		/// Moves the neuron at the supplied index in the neural network closer
		/// to the target colour by a factor of alpha.
		/// </summary>
		/// <param name="alpha">
		/// The greater this parameter, the more the neuron will be moved in
		/// the direction of the target colour.
		/// </param>
		/// <param name="neuronIndex">
		/// Index in the neural network of the neuron to be moved.
		/// </param>
		/// <param name="blue">The blue component of the target colour</param>
		/// <param name="green">The green component of the target colour</param>
		/// <param name="red">The red component of the target colour</param>
		/// <remarks>This method was originally called Altersingle</remarks>
		private void MoveNeuron( int alpha, 
		                         int neuronIndex,
		                         int blue,
		                         int green,
		                         int red )
		{
			int[] thisNeuron = _network[neuronIndex];
			thisNeuron[0] -= (alpha * (thisNeuron[0] - blue)) / _initialAlpha;
			thisNeuron[1] -= (alpha * (thisNeuron[1] - green)) / _initialAlpha;
			thisNeuron[2] -= (alpha * (thisNeuron[2] - red)) / _initialAlpha;
		}
		#endregion
	
		#region private MoveNeighbouringNeurons method
		/// <summary>
		/// Moves neighbours of the neuron at the supplied index in the network
		/// closer to the supplied target colour.
		/// </summary>
		/// <param name="neighbourhoodSize">
		/// Size of the neighbourhood which makes up the neurons to be moved.
		/// For example, if this parameter is set to 3 then the 2 neurons before
		/// and the 2 after the supplied neuron index will be moved.
		/// </param>
		/// <param name="neuronIndex">
		/// The index in the network of the neuron whose neighbours need moving.
		/// </param>
		/// <param name="blue">
		/// The blue component of the target colour.
		/// </param>
		/// <param name="green">
		/// The green component of the target colour.
		/// </param>
		/// <param name="red">
		/// The red component of the target colour.
		/// </param>
		/// <remarks>This method was originally called Alterneigh.</remarks>
		private void MoveNeighbouringNeurons( int neighbourhoodSize, 
		                                      int neuronIndex,
		                                      int blue,
		                                      int green,
		                                      int red )
		{
			int lowNeuronIndexLimit = neuronIndex - neighbourhoodSize;
			if( lowNeuronIndexLimit < -1 )
			{
				// lowNeuronIndexLimit cannot be less than -1 as it is the lower 
				// limit for an array index.
				lowNeuronIndexLimit = -1;
			}
			int highNeuronIndexLimit = neuronIndex + neighbourhoodSize;
			if( highNeuronIndexLimit > _neuronCount )
			{
				// highNeuronIndexLimit cannot be more than the size of the array 
				// being accessed.
				highNeuronIndexLimit = _neuronCount;
			}

			int highNeuronIndex = neuronIndex + 1;
			int lowNeuronIndex = neuronIndex - 1;
			int neighbourAlphaIndex = 1;
			
			// Loop through the neighbouring neurons, starting with those 
			// on either side of the supplied neuron, moving them closer to the
			// target colour.
			while( (highNeuronIndex < highNeuronIndexLimit) || (lowNeuronIndex > lowNeuronIndexLimit) ) 
			{
				int neighbourAlpha = _neighbourhoodAlphas[neighbourAlphaIndex++];
				if( highNeuronIndex < highNeuronIndexLimit ) 
				{
					MoveNeighbour( highNeuronIndex++, neighbourAlpha, blue, green, red );
				}
				if( lowNeuronIndex > lowNeuronIndexLimit ) 
				{
					MoveNeighbour( lowNeuronIndex--, neighbourAlpha, blue, green, red );
				}
			}
		}
		#endregion
		
		#region private MoveNeighbour method
		/// <summary>
		/// Moves an individual neighbouring neuron closer to the supplied 
		/// target colour by a factor of alpha.
		/// </summary>
		/// <param name="neuronIndex">
		/// The index in the network of the neighbour to be moved.
		/// </param>
		/// <param name="alpha">
		/// Controls how far towards the target colour the neuron is to be moved.
		/// </param>
		/// <param name="blue">The blue component of the target colour.</param>
		/// <param name="green">The green component of the target colour.</param>
		/// <param name="red">The red component of the target colour.</param>
		private void MoveNeighbour( int neuronIndex, int alpha, int blue, int green, int red )
		{
			int[] thisNeuron = _network[neuronIndex];
			thisNeuron[0] -= (alpha * (thisNeuron[0] - blue)) / _alphaRadBias;
			thisNeuron[1] -= (alpha * (thisNeuron[1] - green)) / _alphaRadBias;
			thisNeuron[2] -= (alpha * (thisNeuron[2] - red)) / _alphaRadBias;
		}
		#endregion
		
		#endregion
		
		#region private UnbiasNetwork method
		/// <summary>
		/// Shift the colour values in the network to bring them back within
		/// the range 0 to 255.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "Unbias")]
		private void UnbiasNetwork() 
		{
			// Allows a ResponsiveForm to track the progress of this method
		    //AddCounter( unbiasCounterText, _neuronCount );
			for( int thisNeuronIndex = 0; thisNeuronIndex < _neuronCount; thisNeuronIndex++ ) 
			{
				// Update the progress counter for the benefit of the UI
				//MyProgressCounters[unbiasCounterText].Value = thisNeuronIndex;
				
				// Shift the values of this neuron's r, g, b values right to 
				// bring them back within the range 0 to 255.
				_network[thisNeuronIndex][0] >>= _netBiasShift;
				_network[thisNeuronIndex][1] >>= _netBiasShift;
				_network[thisNeuronIndex][2] >>= _netBiasShift;
				
				// Record this neuron's index in the third element of its array
				// because the neurons will be reordered later during the
				// BuildIndex method.
				_network[thisNeuronIndex][3] = thisNeuronIndex; /* record colour no */
			}
			
			// This method has finished so remove its progress counter
			//RemoveCounter( unbiasCounterText );
		}
		#endregion

		#region private BuildIndex method and methods called by it
		
		#region BuildIndex method
		/// <summary>
		/// Insertion sort of network and building of netindex[0..255] (to do 
		/// after unbias)
		/// Populates the _indexOfGreen array with the indices in the network
		/// of colours with green values closest to 0 to 255.
		/// TODO: would this be better as a .net collection sort?
		/// </summary>
		private void BuildIndex() 
		{
			// Allows a ResponsiveForm to track the progress of this method
		    //AddCounter( buildIndexCounterText, _neuronCount );

			int indexOfLeastGreenNeuron;
			int greenValueOfLeastGreenNeuron;
			int[] thisNeuron;
			int previousLeastGreenValue = 0;
			int startingGreenValue = 0;

			for( int thisNeuronIndex = 0;
			     thisNeuronIndex < _neuronCount; 
			     thisNeuronIndex++ )
			{
				// Update the progress counter for the benefit of the UI
				//MyProgressCounters[buildIndexCounterText].Value = thisNeuronIndex;
				
				thisNeuron = _network[thisNeuronIndex];
				
				// Find the least green neuron between the current neuron and
				// the end of the network.
				indexOfLeastGreenNeuron 
					= IndexOfLeastGreenNeuron( thisNeuronIndex );
				greenValueOfLeastGreenNeuron 
					= _network[indexOfLeastGreenNeuron][1];
				
				int[] leastGreenNeuron = _network[indexOfLeastGreenNeuron];
				if( thisNeuronIndex != indexOfLeastGreenNeuron )
				{
					// Move the neuron with the lowest index towards the 
					// beginning of the array.
					SwapNeurons( thisNeuron, leastGreenNeuron );
				}
				
				if( greenValueOfLeastGreenNeuron != previousLeastGreenValue ) 
				{
					// then we've found a new least green neuron so update the
					// array of green indices accordingly
					_indexOfGreen[previousLeastGreenValue] 
						= (startingGreenValue + thisNeuronIndex) >> 1;
					for( int greenValue = previousLeastGreenValue + 1; 
					     greenValue < greenValueOfLeastGreenNeuron; 
					     greenValue++ )
					{
						_indexOfGreen[greenValue] = thisNeuronIndex;
					}
					previousLeastGreenValue = greenValueOfLeastGreenNeuron;
					startingGreenValue = thisNeuronIndex;
				}
			} // end of thisNeuronIndex loop
			
			_indexOfGreen[previousLeastGreenValue] 
				= (startingGreenValue + _highestNeuronIndex) >> 1;
			
			// Fill the remainder of the _indexOfGreen array with the index
			// of the last neuron in the network.
			for( int greenValue = previousLeastGreenValue + 1; 
			     greenValue < _neuronCount;
			     greenValue++)
			{
				_indexOfGreen[greenValue] = _highestNeuronIndex; /* really 256 */
			}

			// This method has finished so remove its progress counter
			//RemoveCounter( buildIndexCounterText );
		}
		
		#endregion

		#region private IndexOfLeastGreenNeuron method
		/// <summary>
		/// Gets the index in the network of the neuron with the lowest green
		/// value, between the supplied index and the end of the network.
		/// </summary>
		/// <param name="startNeuronIndex">
		/// The index in the network to start searching at.
		/// </param>
		/// <returns>
		/// The index of the least green neuron.
		/// </returns>
		private int IndexOfLeastGreenNeuron( int startNeuronIndex )
		{
			// Start with the current neuron, its index and green value.
			int indexOfLeastGreenNeuron = startNeuronIndex;
			int greenValueOfLeastGreenNeuron = _network[startNeuronIndex][1];
			int[] otherNeuron;
			
			// And compare it with the remaining neurons.
			for( int otherNeuronIndex = startNeuronIndex + 1; 
			     otherNeuronIndex < _neuronCount; 
			     otherNeuronIndex++ )
			{
				otherNeuron = _network[otherNeuronIndex];
				// TODO: there's a case here for making neuron a separate class
				if( otherNeuron[1] < greenValueOfLeastGreenNeuron ) 
				{
					// The green value of otherNeuron is lower than that of 
					// the least green neuron seen so far, so otherNeuron
					// becomes the least green.
					indexOfLeastGreenNeuron = otherNeuronIndex;
					greenValueOfLeastGreenNeuron = otherNeuron[1];
				}
			}
			return indexOfLeastGreenNeuron;
		}
		#endregion
		
		#region private static SwapNeurons method
		/// <summary>
		/// Swaps the values of the two supplied neurons.
		/// </summary>
		/// <param name="neuron1">
		/// One of the neurons whose value should be swapped with the other
		/// neuron.
		/// </param>
		/// <param name="neuron2">
		/// The other neuron, whose value should be swapped with the first
		/// neuron.
		/// </param>
		private static void SwapNeurons( int[] neuron1, int[] neuron2 )
		{
			// Swap the blue values
			SwapValues( ref neuron1[0], ref neuron2[0] );
			
			// Swap the green values
			SwapValues( ref neuron1[1], ref neuron2[1] );
			
			// Swap the red values
			SwapValues( ref neuron1[2], ref neuron2[2] );
			
			// Swap the original indices so that the neuron remembers what its
			// original index was before the network was sorted.
			SwapValues( ref neuron1[3], ref neuron2[3] );
		}
		#endregion
		
		#region private static SwapValues method
		/// <summary>
		/// Swaps the values of the two supplied integers.
		/// </summary>
		/// <param name="value1">The first integer</param>
		/// <param name="value2">The second integer</param>
		private static void SwapValues( ref int value1, ref int value2 )
		{
			int temp = value1;
			value1 = value2;
			value2 = temp;
		}
		#endregion
		
		#endregion

		#region private ColourMap method
		/// <summary>
		/// Returns a colour table containing the colours of the quantized image.
		/// </summary>
		/// <returns>
		/// A colour table containing up to 256 colours, being the colours of
		/// the image after quantization.
		/// </returns>
		private ColourTable ColourMap()
		{
			ColourTable map = new ColourTable();
			int[] originalIndices = new int[_neuronCount];
			int[] thisNeuron;
			
			// Build an array of the original indices of the neurons in the
			// network, before the BuildIndex method reordered them.
			for( int neuronIndex = 0; neuronIndex < _neuronCount; neuronIndex++ )
			{
				thisNeuron = _network[neuronIndex];
				originalIndices[thisNeuron[3]] = neuronIndex;
			}
			
			for( int i = 0; i < _neuronCount; i++ ) 
			{
				int indexInNetwork = originalIndices[i];
				map.Add( Color.FromArgb( 255, 
				                         _network[indexInNetwork][0], 
				                         _network[indexInNetwork][1], 
				                         _network[indexInNetwork][2] ) );
			}
			return map;
		}
		#endregion
	
		#endregion
	}
}