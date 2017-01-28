#region Copyright (C) Simon Bridewell, Kevin Weiner, Phil Garcia
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

#region using directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Components;
using SpriteVortex.Helpers.GifComponents.Enums;
using SpriteVortex.Helpers.GifComponents.Pelettes;
using SpriteVortex.Helpers.GifComponents.Tools;
using SpriteVortex.Helpers.GifComponents.Types;

#endregion

namespace SpriteVortex.Helpers.GifComponents
{
	/// <summary>
	/// TODO: remove support for transparency until it's understood better?
	/// Class AnimatedGifEncoder - Encodes a GIF file consisting of one or
	/// more frames.
	/// Instantiate the encoder using the constructor, call the AddFrame to add
	/// as many GifFrames as desired, then call the WriteToStream or WriteToFile
	/// method to create the animation.
	/// 
	/// No copyright asserted on the source code of this class.  May be used
	/// for any purpose, however, refer to the Unisys LZW patent for restrictions
	/// on use of the associated LZWEncoder class.  Please forward any corrections
	/// to kweiner@fmsware.com.
	/// 
	/// @author Kevin Weiner, FM Software
	/// @version 1.03 November 2003
	/// 
	/// Modified by Phil Garcia (phil@thinkedge.com) 
	///		1. Add support to output the Gif to a MemoryStream (9/2/2005)
	/// 
	/// Modified by Simon Bridewell, June 2009 - April 2010:
	/// Downloaded from 
	/// http://www.thinkedge.com/BlogEngine/file.axd?file=NGif_src2.zip
	/// 	* Corrected FxCop code analysis errors.
	/// 	* Documentation comments converted to .net XML comments.
	/// 	* Refactored so that all properties are set in the constructor.
	/// 	* Writing of GIF components to the output stream delegated to the 
	/// 	  classes for those components.
	/// 	* Added option to use a global colour table instead of local colour tables.
	/// 	* Added support for colour tables with fewer than 256 colours
	/// 	* Colour quantization only performed for animations with more than 
	/// 	  256 colours.
	/// 	* User can now supply their own palette as a global or local colour
	/// 	  table.
	/// 	* New QuantizerType property - allows user to choose between NeuQuant
	/// 	  and Octree quantizers.
	/// 	* Add ProgressCounters to allow UI to provide feedback to the user.
	/// </summary>
	public class AnimatedGifEncoder : GifComponent
	{
		#region declarations
		/// <summary>
		/// The frames which make up the animation/
		/// </summary>
		private Collection<GifFrame> _frames;
		
		/// <summary>
		/// The ColourTableStrategy indicating whether a global colour table
		/// or local colour tables should be used.
		/// </summary>
		private ColourTableStrategy _strategy;
		
		/// <summary>
		/// The global colour table, if used.
		/// </summary>
		private ColourTable _globalColourTable;
		
		/// <summary>
		/// Size, in pixels, of the animated GIF file.
		/// </summary>
		private Size _logicalScreenSize;
		
		private Color _transparent = Color.Empty; // transparent color if given

		/// <summary>
		/// The number of times to repeat the animation.
		/// 0 to repeat indefinitely.
		/// -1 to not repeat.
		/// </summary>
		private int _repeatCount;
		
		private bool[] _usedEntry = new bool[256]; // active palette entries
		
		/// <summary>
		/// Quality of color quantization (conversion of images to the maximum 
		/// 256 colors allowed by the GIF specification).
		/// Lower values (minimum = 1) produce better colors, but slow 
		/// processing significantly.
		/// 10 is the default, and produces good color mapping at reasonable 
		/// speeds.
		/// Values greater than 20 do not yield significant improvements in 
		/// speed.
		/// </summary>
		private int _quality;
		
		/// <summary>
		/// Indicates the type of quantizer used to reduce the colour palette
		/// to 256 colours.
		/// </summary>
		private QuantizerType _quantizerType;
		
		private Palette _palette;
		
		private PixelAnalysis _pixelAnalysis;

		private int _encodingFrame;
		
		#endregion

		#region default constructor
		/// <summary>
		/// Default constructor.
		/// Sets repeat count to 0 (repeat indefinitely)
		/// Sets colour table strategy to UseGlobal
		/// Sets image quantization quality to 10.
		/// Sets quantizer type to NeuQuant.
		/// Screen size defaults to size of first frame.
		/// </summary>
		public AnimatedGifEncoder()
		{
			_frames = new Collection<GifFrame>();
			_strategy = ColourTableStrategy.UseGlobal;
			_quality = 10;
			_quantizerType = QuantizerType.NeuQuant;
			_logicalScreenSize = Size.Empty;
			_palette = new Palette();
		}
		#endregion
		
		#region properties
		
		#region Frames property
		/// <summary>
		/// Gets a collection of the GifFrames which make up the animation.
		/// </summary>
		[Browsable( false )]
		public Collection<GifFrame> Frames
		{
			get { return _frames; }
		}
		#endregion
		
		#region Transparent property
		/// <summary>
		/// Gets and sets the transparent color for the next added frame and 
		/// any subsequent frames.
		/// Since all colors are subject to modification in the quantization 
		/// process, the color in the final palette for each frame closest to 
		/// the given color becomes the transparent color for that frame.
		/// May be set to Color.Empty to indicate no transparent color.
		/// </summary>
		public Color Transparent
		{
			get{ return _transparent; }
			set{ _transparent = value; }
		}
		#endregion

		#region ColourTableStrategy property
		/// <summary>
		/// Indicates whether the animation will contain a single global colour
		/// table for all frames (UseGlobal) or a local colour table for each
		/// frame (UseLocal)
		/// </summary>
		[Description( "Indicates whether the animation will contain a single " +
		              "global colour table for all frames (UseGlobal) or a " +
		              "local colour table for each frame (UseLocal)" )]
		public ColourTableStrategy ColourTableStrategy
		{
			get { return _strategy; }
			set { _strategy = value; }
		}
		#endregion
		
		#region RepeatCount property
		/// <summary>
		/// The number of times to repeat the animation.
		/// 0 to repeat indefinitely.
		/// -1 to not repeat.
		/// Defaults to -1 if less than -1.
		/// </summary>
		[Description( "The number of times to repeat the animation. 0 to " +
		              "repeat indefinitely. -1 to not repeat. Defaults to -1 " +
		              "if less than -1." )]
		public int RepeatCount
		{
			get { return _repeatCount; }
			set
			{
				if( value < -1 )
				{
					_repeatCount = -1;
				}
				else
				{
					_repeatCount = value;
				}
			}
		}
		#endregion
		
		#region SamplingFactor property
		/// <summary>
		/// Sets the proportion of pixels in the input image to be examined
		/// when quantizing the image.
		/// Set to 1 to examine every pixel for better colour quality but slow
		/// processing.
		/// Set to 10 to examine one in 10 of the pixels for faster processing
		/// but poorer image quality.
		/// Only applicable if input image has more than 256 colours and the
		/// QuantizerType is set to NeuQuant.
		/// </summary>
		[Description( "Sets the proportion of pixels in the input image to be " +
		              "examined when quantizing the image. " +
		              "Set to 1 to examine every pixel for better colour " +
		              "quality but slow processing. " +
		              "Set to 10 to examine one in 10 of the pixels for faster " +
		              "processing but poorer image quality." +
		              "Only applicable if input image has more than 256 " + 
		              "colours and QuantizerType is set to NeuQuant.")]
		public int SamplingFactor
		{
			get { return _quality; }
			set
			{
				if( value < 1 )
				{
					_quality = 1;
				}
				else
				{
					_quality = value;
				}
			}
		}
		#endregion
		
		#region LogicalScreenSize property
		/// <summary>
		/// The size, in pixels, of the animated GIF file.
		/// If this property is not set before the animation is encoder, the 
		/// size of the first frame to be added will be used.
		/// </summary>
		[Description( "The size, in pixels, of the animated GIF file. If " +
		              "this property is not set before the animation is " +
		              "encoder, the size of the first frame to be added " +
		              "will be used." )]
		public Size LogicalScreenSize
		{
			get { return _logicalScreenSize; }
			set { _logicalScreenSize = value; }
		}
		#endregion
		
		#region QuantizerType property
		/// <summary>
		/// Gets and sets the type of quantizer used to reduce the colour
		/// palette to 256 colours, if required.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "Quantizer")]
		public QuantizerType QuantizerType
		{
			get { return _quantizerType; }
			set { _quantizerType = value; }
		}
		#endregion

		#region Palette property
		/// <summary>
		/// Gets and sets a user-defined palette to use as a global colour table.
		/// When setting this property, the ColourTableStrategy property is set
		/// to UseGlobal, and the QuantizerType property is set to 
		/// UseSuppliedPalette.
		/// </summary>
		[SuppressMessage("Microsoft.Usage", 
		                 "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[Description( "A user-defined palette to use as a global colour table" )]
		public Palette Palette
		{
			get { return _palette; }
			set
			{
				_palette = value;
				_quantizerType = QuantizerType.UseSuppliedPalette;
				_strategy = ColourTableStrategy.UseGlobal;
			}
		}
		#endregion
		
		#region PixelAnalysis property
		/// <summary>
		/// Gets the PixelAnalyis used to quantize the images
		/// </summary>
		[Browsable(false)]
		public PixelAnalysis PixelAnalysis
		{
			get { return _pixelAnalysis; }
		}
		#endregion
		
		#endregion

		#region methods
		
		#region WriteToFile method
		/// <summary>
		/// Writes an animated GIF file to the supplied file name.
		/// </summary>
		/// <param name="fileName">
		/// The file to write the animation to.
		/// </param>
		public void WriteToFile( string fileName )
		{
			Stream outputStream = File.Create( fileName );
			WriteToStream( outputStream );
			outputStream.Close();
		}
		#endregion
		
		#region WriteToStream method
		/// <summary>
		/// Writes the GIF animation to the supplied stream.
		/// </summary>
		/// <param name="outputStream">
		/// The stream to write the animation to.
		/// </param>
		public override void WriteToStream( Stream outputStream )
		{
			if( _frames.Count == 0 )
			{
				string message
					= "The AnimatedGifEncoder has no frames to write!";
				throw new InvalidOperationException( message );
			}
			
			if( _logicalScreenSize == Size.Empty )
			{
				// use first frame's size if logical screen size hasn't been set
				_logicalScreenSize = _frames[0].TheImage.Size;
			}
			
			if( _quantizerType == QuantizerType.UseSuppliedPalette )
			{
				if( _strategy == ColourTableStrategy.UseGlobal )
				{
					if( _palette == null )
					{
						// TESTME: WriteToStream - UseSuppliedPalette, no palette supplied
						string message
							= "You have chosen to encode this animation using "
							+ "a global colour table and a supplied palette, "
							+ "but you have not supplied a palette!";
						throw new InvalidOperationException( message );
					}
				}
			}
			
			_encodingFrame = 0;
			
			WriteGifHeader( outputStream );
			
			WriteLogicalScreenDescriptor( outputStream );
			
			WriteNetscapeExtension( outputStream );

		    //AddCounter( encodingFrameText, _frames.Count );
			
			for( _encodingFrame = 0; 
			     _encodingFrame < _frames.Count; 
			     _encodingFrame++ )
			{
				//MyProgressCounters[encodingFrameText].Value = _encodingFrame + 1;
				WriteFrame( outputStream );
			}
			
			//RemoveCounter( encodingFrameText );
			
			// GIF trailer
			WriteByte( CodeTrailer, outputStream );
		}
		#endregion
		
		#region AddFrame( GifFrame ) method
		/// <summary>
		/// Adds a frame to the animation.
		/// </summary>
		/// <param name="frame">
		/// The frame to add to the animation.
		/// </param>
		public void AddFrame( GifFrame frame )
		{
			_frames.Add( frame );
		}
		#endregion
	
		#region protected / private methods
	
		#region private static FindClosest method
		/// <summary>
		/// Returns the index within the supplied colour table of the colour 
		/// closest to the supplied colour.
		/// </summary>
		/// <param name="colourToFind">
		/// The colour to find the closest match for.
		/// </param>
		/// <param name="colourTable">
		/// The active colour table.
		/// </param>
		/// <returns>
		/// Returns -1 if the supplied colour is null.
		/// </returns>
		private static int FindClosest( Color colourToFind, 
		                                ColourTable colourTable )
		{
			if( colourTable == null )
			{
				// TESTME: FindClosest - null colour table
				return -1;
			}
			int r = colourToFind.R;
			int g = colourToFind.G;
			int b = colourToFind.B;
			int minpos = 0;
			int dmin = 256 * 256 * 256;
			int len = colourTable.Length;
			for( int i = 0; i < len; i++ ) 
			{
				int dr = r - colourTable[i].R;
				int dg = g - colourTable[i].G;
				int db = b - colourTable[i].B;
				int d = dr * dr + dg * dg + db * db;
				if( d < dmin )
				{
					dmin = d;
					minpos = i;
				}
			}
			return minpos;
		}
		#endregion

		#region private static WriteGifHeader method
		private static void WriteGifHeader( Stream outputStream )
		{
			GifHeader header = new GifHeader( "GIF", "89a" );
			header.WriteToStream( outputStream );
		}
		#endregion

		#region private WriteFrame method
		private void WriteFrame( Stream outputStream )
		{
			GifFrame thisFrame = _frames[_encodingFrame];
			Image thisImage = thisFrame.TheImage;
			ColourTable act = SetActiveColourTable();
			int transparentColourIndex;
			if( _transparent == Color.Empty )
			{
				transparentColourIndex = 0;
			}
			else
			{
				// TESTME: WriteFrame - _transparent != Color.Empty
				transparentColourIndex = FindClosest( _transparent, act );
			}
			WriteGraphicCtrlExt( thisFrame, 
			                     transparentColourIndex, 
			                     outputStream );
			ColourTable lct;
			if( _strategy == ColourTableStrategy.UseLocal )
			{
				lct = act;
			}
			else
			{
				lct = null;
			}
			WriteImageDescriptor( thisImage.Size, 
			                      thisFrame.Position,
			                      lct, 
			                      outputStream );
			
			// Write a local colour table if the strategy is to do so
			if( _strategy == ColourTableStrategy.UseLocal )
			{
				act.WriteToStream( outputStream );
				IndexedPixels ip;
				if( _quantizerType == QuantizerType.UseSuppliedPalette )
				{
					ip = MakeIndexedPixels( act, thisImage );
				}
				else
				{
					ip = _pixelAnalysis.IndexedPixels;
				}
				WritePixels( ip, outputStream );
			}
			else // global colour table
			{
				IndexedPixels ip;
				if( _quantizerType == QuantizerType.UseSuppliedPalette )
				{
					ip = MakeIndexedPixels( act, thisImage );
				}
				else
				{
					ip = _pixelAnalysis.IndexedPixelsCollection[_encodingFrame];
				}
				WritePixels( ip, outputStream );
			}
		}
		#endregion
		
		#region private MakeIndexedPixels method
		/// <summary>
		/// Converts the supplied image to a collection of pixel indices using
		/// the supplied colour table.
		/// Only used when the QuantizerType is set to UseSuppliedPalette
		/// </summary>
		/// <param name="act">The active colour table</param>
		/// <param name="image">The image</param>
		/// <returns></returns>
		private IndexedPixels MakeIndexedPixels( ColourTable act, Image image )
		{
			
		    //AddCounter( counterText, pixelCount );
			Bitmap bitmap = (Bitmap) image;
			IndexedPixels ip = new IndexedPixels();
			for( int y = 0; y < image.Height; y++ )
			{
				for( int x = 0; x < image.Width; x++ )
				{
					Color c = bitmap.GetPixel( x, y );
					int index = FindClosest( c, act );
					ip.Add( (byte) index );
					//MyProgressCounters[counterText].Value = ip.Count;
				}
			}
			//RemoveCounter( counterText );
			return ip;
		}
		#endregion
		
		#region private SetActiveColourTable method
		private ColourTable SetActiveColourTable()
		{
			ColourTable act; // active colour table
			if( _strategy == ColourTableStrategy.UseLocal )
			{
				if( _quantizerType == QuantizerType.UseSuppliedPalette )
				{
					if( _frames[_encodingFrame].Palette == null )
					{
						// TESTME: SetActiveColourTable - SetActiveColourTable, UseSuppliedPalette, no palette supplied
						string message
							= "You have opted to use a local colour table built "
							+ "from a supplied palette, but frame "
							+ _encodingFrame
							+ "does not have a palette.";
						throw new InvalidOperationException( message );
					}
					else
					{
						// Build local colour table from colours in the frame's
						// supplied palette.
						act = new ColourTable();
						foreach( Color c in _frames[_encodingFrame].Palette )
						{
							act.Add( c );
						}
						act.Pad();
					}
				}
				else
				{
					// Build local colour table based on colours in the image.
					Image thisImage = _frames[_encodingFrame].TheImage;
					_pixelAnalysis = new PixelAnalysis( thisImage, 
					                                    _quantizerType );
					_pixelAnalysis.ColourQuality = _quality;
					_pixelAnalysis.Analyse();
					// make local colour table active
					act = _pixelAnalysis.ColourTable;
				}
			}
			else
			{
				// make global colour table active
				act = _globalColourTable;
			}
			return act;
		}
		#endregion
		
		#region private WriteGraphicCtrlExt method
		/// <summary>
		/// Writes a Graphic Control Extension to the supplied output stream.
		/// </summary>
		/// <param name="frame">
		/// The GifFrame to which this graphic control extension relates.
		/// </param>
		/// <param name="transparentColourIndex">
		/// The index within the active colour table of the transparent colour.
		/// </param>
		/// <param name="outputStream">
		/// The stream to write to.
		/// </param>
		private void WriteGraphicCtrlExt( GifFrame frame,
		                                  int transparentColourIndex, 
		                                  Stream outputStream )
		{
			outputStream.WriteByte( GifComponent.CodeExtensionIntroducer );
			outputStream.WriteByte( GifComponent.CodeGraphicControlLabel );
			
			// The frame doesn't have have a graphic control extension yet, so we
			// need to work out what it would contain.
			DisposalMethod disposalMethod;
			bool hasTransparentColour;
			if( _transparent == Color.Empty ) // TODO: remove reference to _transparent - parameterise?
			{
				hasTransparentColour = false;
				disposalMethod = DisposalMethod.NotSpecified; // dispose = no action
			} 
			else 
			{
				// TESTME: WriteGraphicCtrlExt - _transparent != Color.Empty
				hasTransparentColour = true;
				disposalMethod = DisposalMethod.RestoreToBackgroundColour; // force clear if using transparent color
			}
			int blockSize = 4;
			GraphicControlExtension gce
				= new GraphicControlExtension( blockSize, 
				                               disposalMethod, 
				                               frame.ExpectsUserInput, 
				                               hasTransparentColour, 
				                               frame.Delay, 
				                               transparentColourIndex );
			gce.WriteToStream( outputStream );
		}
		#endregion
	
		#region private static WriteImageDescriptor method
		/// <summary>
		/// Writes an image descriptor to the supplied stream.
		/// </summary>
		/// <param name="imageSize">
		/// The size, in pixels, of the image in this frame.
		/// </param>
		/// <param name="position">
		/// The position of this image within the logical screen.
		/// </param>
		/// <param name="localColourTable">
		/// The local colour table for this frame.
		/// Supply null if the global colour table is to be used for this frame.
		/// </param>
		/// <param name="outputStream">
		/// The stream to write to.
		/// </param>
		private static void WriteImageDescriptor( Size imageSize,
		                                          Point position,
		                                          ColourTable localColourTable,
		                                          Stream outputStream )
		{
			bool hasLocalColourTable;
			int localColourTableSize;
			if( localColourTable == null )
			{
				hasLocalColourTable = false;
				localColourTableSize = 0;
			}
			else
			{
				hasLocalColourTable = true;
				localColourTableSize = localColourTable.SizeBits;
			}
			
			bool isInterlaced = false; // encoding of interlaced images not currently supported
			bool localColourTableIsSorted = false; // sorting of colour tables not currently supported
			ImageDescriptor id = new ImageDescriptor( position, 
			                                          imageSize, 
			                                          hasLocalColourTable, 
			                                          isInterlaced, 
			                                          localColourTableIsSorted, 
			                                          localColourTableSize );
			outputStream.WriteByte( GifComponent.CodeImageSeparator );
			id.WriteToStream( outputStream );
		}
		#endregion
	
		#region private WriteLogicalScreenDescriptor method
		/// <summary>
		/// Writes a Logical Screen Descriptor to the supplied stream.
		/// Also writes a global colour table if required.
		/// </summary>
		/// <param name="outputStream">
		/// The stream to write to.
		/// </param>
		private void WriteLogicalScreenDescriptor( Stream outputStream )
		{
			bool hasGlobalColourTable = _strategy == ColourTableStrategy.UseGlobal;
			int colourResolution = 7; // TODO: parameterise colourResolution?
			bool globalColourTableIsSorted = false; // Sorting of colour tables is not currently supported
			int backgroundColorIndex = 0; // TODO: parameterise backgroundColourIndex?
			int pixelAspectRatio = 0; // TODO: parameterise pixelAspectRatio?
			if( _strategy == ColourTableStrategy.UseGlobal )
			{
				if( _quantizerType == QuantizerType.UseSuppliedPalette )
				{
					// use supplied palette
					_globalColourTable = new ColourTable();
				    //AddCounter( buildColourTableCounterText, 
					         //   _palette.Count );
					int paletteIndex = 0;
					foreach( Color c in _palette )
					{
						_globalColourTable.Add( c );
						//MyProgressCounters[buildColourTableCounterText].Value 
							//= paletteIndex;
						paletteIndex++;
					}
					_globalColourTable.Pad();
					//RemoveCounter( buildColourTableCounterText );
				}
				else
				{
					// Analyse the pixels in all the images to build the
					// global colour table.
					Collection<Image> images = new Collection<Image>();
					foreach( GifFrame thisFrame in _frames )
					{
						Image thisImage = thisFrame.TheImage;
						images.Add( thisImage );
					}
					_pixelAnalysis = new PixelAnalysis( images );
					_pixelAnalysis.ColourQuality = _quality;
					_pixelAnalysis.Analyse();
					_globalColourTable = _pixelAnalysis.ColourTable;
				}
				LogicalScreenDescriptor lsd = 
					new LogicalScreenDescriptor( _logicalScreenSize, 
					                             hasGlobalColourTable, 
					                             colourResolution, 
					                             globalColourTableIsSorted, 
					                             _globalColourTable.SizeBits,
					                             backgroundColorIndex, 
					                             pixelAspectRatio );
				lsd.WriteToStream( outputStream );
				_globalColourTable.WriteToStream( outputStream );
			}
			else
			{
				LogicalScreenDescriptor lsd = 
					new LogicalScreenDescriptor( _logicalScreenSize, 
					                             hasGlobalColourTable, 
					                             colourResolution, 
					                             globalColourTableIsSorted, 
					                             7, 
					                             backgroundColorIndex, 
					                             pixelAspectRatio );
				lsd.WriteToStream( outputStream );
			}
		}
		#endregion
	
		#region private WriteNetscapeExtension method
		/// <summary>
		/// Writes a Netscape application extension defining the repeat count
		/// to the supplied output stream, if the repeat count is greater than
		/// or equal to zero.
		/// </summary>
		/// <param name="outputStream">
		/// The stream to write to.
		/// </param>
		private void WriteNetscapeExtension( Stream outputStream )
		{
			// Repeat count -1 means don't repeat the animation, so don't add
			// a Netscape extension.
			if( _repeatCount >= 0 ) 
			{
				outputStream.WriteByte( GifComponent.CodeExtensionIntroducer );
				outputStream.WriteByte( GifComponent.CodeApplicationExtensionLabel );
				NetscapeExtension ne = new NetscapeExtension( _repeatCount );
				ne.WriteToStream( outputStream );
			}
		}
		#endregion
	
		#region private static WritePixels method
		/// <summary>
		/// Encodes and writes pixel data to the supplied stream
		/// </summary>
		/// <param name="indexedPixels">
		/// Collection of indices of the pixel colours within the active colour 
		/// table.
		/// </param>
		/// <param name="outputStream">
		/// The stream to write to.
		/// </param>
		private static void WritePixels( IndexedPixels indexedPixels,
		                                 Stream outputStream )
		{
			LzwEncoder encoder = new LzwEncoder( indexedPixels );
			encoder.Encode( outputStream );
		}
		#endregion

		#endregion

		#endregion


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Frames.Clear();


        }
	}
}