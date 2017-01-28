#region Copyright (C) Simon Bridewell
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Enums;
using SpriteVortex.Helpers.GifComponents.Types;

namespace SpriteVortex.Helpers.GifComponents.Components
{
	/// <summary>
	/// The image data for a table based image consists of a sequence of 
	/// sub-blocks, of size at most 255 bytes each, containing an index into 
	/// the active color table, for each pixel in the image.  
	/// Pixel indices are in order of left to right and from top to bottom.  
	/// Each index must be within the range of the size of the active color 
	/// table, starting at 0.
	/// See http://www.w3.org/Graphics/GIF/spec-gif89a.txt section 22
	/// 
	/// Adapted from John Cristy's ImageMagick.
	/// Decodes LZW image data into pixel array and returns table-based 
	/// image data - see http://www.w3.org/Graphics/GIF/spec-gif89a.txt
	/// section 22.
	/// Simon Bridewell - July-August 2009
	/// 	Extracted this logic from GifDecoder.cs
	/// 	Added logical properties
	/// 	Derive from GifComponent in order to make use of component status
	/// 	Use stronger types than just byte where appropriate
	/// </summary>
	public class TableBasedImageData : GifComponent
	{
		#region declarations
		private const int _maxStackSize = 4096; // max decoder pixel stack size
		private const int _nullCode = -1; // indicates no previous code has been read yet
		
		/// <summary>
		/// Data blocks read from the input stream.
		/// </summary>
		private Collection<DataBlock> _dataBlocks;
		
		/// <summary>
		/// An array of indices to colours in the active colour table,
		/// representing the pixels of a frame in a GIF data stream.
		/// </summary>
		private IndexedPixels _pixels;
		
		/// <summary>
		/// Determines the initial number of bits used for LZW codes 
		/// in the image data.
		/// </summary>
		private int _lzwMinimumCodeSize;
		#endregion
		
		#region constructor( Stream, int )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="inputStream">
		/// The stream from which the image data is to be read, starting with
		/// the LZW minimum code size, and ending with a block terminator.
		/// </param>
		/// <param name="pixelCount">
		/// Number of pixels in the image.
		/// </param>
		public TableBasedImageData( Stream inputStream, int pixelCount )
			: this( inputStream, pixelCount, false )
		{}
		#endregion
		
		#region Constructor( Stream, int, bool )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="inputStream">
		/// The stream from which the image data is to be read, starting with
		/// the LZW minimum code size, and ending with a block terminator.
		/// </param>
		/// <param name="pixelCount">
		/// Number of pixels in the image.
		/// </param>
		/// <param name="xmlDebugging">Whether or not to create debug XML</param>
		/// <remarks>
		/// The input stream is read, first into the LZW minimum code size, then
		/// into data blocks. Bytes are extracted from the data blocks into a
		/// datum until the datum contains enough bits to form a code; this code
		/// is then extracted from the datum and decoded into a pixel index.
		/// Once all data has been read, or a block terminator, 
		/// end-of-information code or error condition is encountered, any
		/// remaining pixel indices not already populated default to zero.
		/// </remarks>
		public TableBasedImageData( Stream inputStream, 
		                            int pixelCount, 
		                            bool xmlDebugging )
			: base( xmlDebugging )
		{
			#region guard against silly image sizes
			if( pixelCount < 1 )
			{
				string message
					= "The pixel count must be greater than zero. "
					+ "Supplied value was " + pixelCount;
				throw new ArgumentOutOfRangeException( "pixelCount", message );
			}
			#endregion
			
			#region declare / initialise local variables
			_pixels = new IndexedPixels( pixelCount );
			_dataBlocks = new Collection<DataBlock>();
			int nextAvailableCode; // the next code to be added to the dictionary
			int	currentCodeSize;
			int	in_code;
			int	previousCode;
			int	code;
			int	datum = 0; // temporary storage for codes read from the input stream
			int	meaningfulBitsInDatum = 0; // number of bits of useful information held in the datum variable
			int	firstCode = 0; // first code read from the stream since last clear code
			int	indexInDataBlock = 0;
			int	pixelIndex = 0;
			
			// number of bytes still to be extracted from the current data block
			int bytesToExtract = 0; 

			short[] prefix = new short[_maxStackSize];
			byte[] suffix = new byte[_maxStackSize];
			Stack<byte> pixelStack = new Stack<byte>();
			#endregion

			//  Initialize GIF data stream decoder.
			_lzwMinimumCodeSize = Read( inputStream ); // number of bits initially used for LZW codes in image data
			WriteDebugXmlElement( "LzwMinimumCodeSize", _lzwMinimumCodeSize );
			nextAvailableCode = ClearCode + 2;
			previousCode = _nullCode;
			currentCodeSize = InitialCodeSize;
			
			#region guard against LZW code size being too large
			if( ClearCode >= _maxStackSize )
			{
				string message
					= "LZW minimum code size: " + _lzwMinimumCodeSize
					+ ". Clear code: " + ClearCode
					+ ". Max stack size: " + _maxStackSize;
				SetStatus( ErrorState.LzwMinimumCodeSizeTooLarge, message );
				WriteDebugXmlFinish();
				return;
			}
			#endregion
			
			// TODO: what are prefix and suffix and why are we initialising them like this?
			for( code = 0; code < ClearCode; code++ )
			{
				prefix[code] = 0;
				suffix[code] = (byte) code;
			}
			
			WriteDebugXmlStartElement( "DataBlocks" );

			#region decode LZW image data

			// Initialise block to an empty data block. This will be overwritten
			// first time through the loop with a data block read from the input
			// stream.
			DataBlock block = new DataBlock( 0, new byte[0] );
			
			for( pixelIndex = 0; pixelIndex < pixelCount; ) 
			{
				if( pixelStack.Count == 0 )
				{
					// There are no pixels in the stack at the moment, so...
					#region get some pixels and put them on the stack
					if( meaningfulBitsInDatum < currentCodeSize )
					{
						// Then we don't have enough bits in the datum to make
						// a code; we need to get some more from the current
						// data block, or we may need to read another data
						// block from the input stream
						#region get another byte from the current data block
						if( bytesToExtract == 0 )
						{
							// Then we've extracted all the bytes from the 
							// current data block, so...
							
							#region	read the next data block from the stream
							block = ReadDataBlock( inputStream );
							bytesToExtract = block.ActualBlockSize;
							
							// Point to the first byte in the new data block
							indexInDataBlock = 0;

							if( block.TestState( ErrorState.DataBlockTooShort ) )
							{
								// then we've reached the end of the stream
								// prematurely
								break;
							}
							
							if( bytesToExtract == 0 )
							{
								// then it's a block terminator, end of the
								// image data (this is a data block other than
								// the first one)
								break;
							}
							#endregion
						}
						// Append the contents of the current byte in the data 
						// block to the beginning of the datum
						int newDatum = block[indexInDataBlock] << meaningfulBitsInDatum;
						datum += newDatum;

						// so we've now got 8 more bits of information in the
						// datum.
						meaningfulBitsInDatum += 8;
						
						// Point to the next byte in the data block
						indexInDataBlock++;
						
						// We've one less byte still to read from the data block
						// now.
						bytesToExtract--;
						
						// and carry on reading through the data block
						continue;
						#endregion
					}

					#region get the next code from the datum
					// Get the least significant bits from the read datum, up
					// to the maximum allowed by the current code size.
					code = datum & GetMaximumPossibleCode( currentCodeSize );
					
					// Drop the bits we've just extracted from the datum.
					datum >>= currentCodeSize;
					
					// Reduce the count of meaningful bits held in the datum
					meaningfulBitsInDatum -= currentCodeSize;
					#endregion

					#region interpret the code

					#region end of information?
					if( code == EndOfInformation )
					{
						// We've reached an explicit marker for the end of the
						// image data.
						break;
					}
					#endregion

					#region code not in dictionary?
					if( code > nextAvailableCode )
					{
						// We expect the code to be either one which is already
						// in the dictionary, or the next available one to be
						// added. If it's neither of these then abandon 
						// processing of the image data.
						string message
							= "Next available code: " + nextAvailableCode
							+ ". Last code read from input stream: " + code;
						SetStatus( ErrorState.CodeNotInDictionary, message );
						break;
					}
					#endregion

					#region clear code?
					if( code == ClearCode )
					{
						// We can get a clear code at any point in the image
						// data, this is an instruction to reset the decoder
						// and empty the dictionary of codes.
						currentCodeSize = InitialCodeSize;
						nextAvailableCode = ClearCode + 2;
						previousCode = _nullCode;

						// Carry on reading from the input stream.
						continue;
					}
					#endregion
					
					#region first code since last clear code?
					if( previousCode == _nullCode )
					{
						// This is the first code read since the start of the
						// image data or the most recent clear code.
						// There's no previously read code in memory yet, so
						// get the pixel index for the current code and add it
						// to the stack.
						pixelStack.Push( suffix[code] );
						previousCode = code;
						firstCode = code;
						
						// and carry on to the next pixel
						continue;
					}
					#endregion

					in_code = code;
					if( code == nextAvailableCode )
					{
						pixelStack.Push( (byte) firstCode );
						code = previousCode;
					}
					
					while( code > ClearCode )
					{
						pixelStack.Push( suffix[code] );
						code = prefix[code];
					}

					#endregion

					firstCode = ((int) suffix[code]) & 0xff;

					#region add a new string to the string table
					if( nextAvailableCode >= _maxStackSize )
					{
						// TESTME: constructor - next available code >- _maxStackSize
						break;
					}
					pixelStack.Push( (byte) firstCode );
					prefix[nextAvailableCode] = (short) previousCode;
					suffix[nextAvailableCode] = (byte) firstCode;
					nextAvailableCode++;
					#endregion
					
					#region do we need to increase the code size?
					if( ( nextAvailableCode & GetMaximumPossibleCode( currentCodeSize ) ) == 0 )
					{
						// We've reached the largest code possible for this size
						if( nextAvailableCode < _maxStackSize )
						{
							// so increase the code size by 1
							currentCodeSize++;
						}
					}
					#endregion

					previousCode = in_code;
					#endregion
				}

				// Pop all the pixels currently on the stack off, and add them
				// to the return value.
				_pixels[pixelIndex] = pixelStack.Pop();
				pixelIndex++;
			}

			#endregion

			#region check input stream contains enough data to fill the image
			if( pixelIndex < pixelCount )
			{
				string message
					= "Expected pixel count: " + pixelCount
					+ ". Actual pixel count: " + pixelIndex;
				SetStatus( ErrorState.TooFewPixelsInImageData, message );
			}
			#endregion
			
			if( XmlDebugging )
			{
				WriteDebugXmlEndElement();
				byte[] bytes = new byte[_pixels.Count];
				_pixels.CopyTo( bytes, 0 );
				WriteDebugXmlByteValues( "IndexedPixels", bytes );
				WriteDebugXmlFinish();
			}
		}
		#endregion
		
		#region properties

		#region DataBlocks property
		/// <summary>
		/// Gets the data blocks as read from the input stream.
		/// </summary>
		[SuppressMessage("Microsoft.Performance", 
		                 "CA1819:PropertiesShouldNotReturnArrays")]
		public DataBlock[] DataBlocks
		{
			get
			{
				DataBlock[] blocks = new DataBlock[_dataBlocks.Count];
				_dataBlocks.CopyTo( blocks, 0 );
				return blocks;
			}
		}
		#endregion
		
		#region Pixels property
		/// <summary>
		/// Gets an array of indices to colours in the active colour table,
		/// representing the pixels of a frame in a GIF data stream.
		/// </summary>
		public IndexedPixels Pixels
		{
			get { return _pixels; }
		}
		#endregion
		
		#region LzwMinimumCodeSize property
		/// <summary>
		/// Determines the initial number of bits used for LZW codes in the 
		/// image data.
		/// This is read from the first available byte in the input stream.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "Lzw")]
		public int LzwMinimumCodeSize
		{
			get { return _lzwMinimumCodeSize; }
		}
		#endregion

		#region ClearCode property
		/// <summary>
		/// A special Clear code is defined which resets all compression / 
		/// decompression parameters and tables to a start-up state. 
		/// The value of this code is 2 ^ code size. 
		/// For example if the code size indicated was 4 (image was 4 bits/pixel)
		/// the Clear code value would be 16 (10000 binary). 
		/// The Clear code can appear at any point in the image data stream and 
		/// therefore requires the LZW algorithm to process succeeding codes as 
		/// if a new data stream was starting. 
		/// Encoders should output a Clear code as the first code of each image 
		/// data stream.
		/// </summary>
		public int ClearCode
		{
			get { return 1 << _lzwMinimumCodeSize; }
		}
		#endregion
		
		#region InitialCodeSize property
		/// <summary>
		/// Gets the size in bits of the first code to add to the dictionary.
		/// </summary>
		public int InitialCodeSize
		{
			get { return _lzwMinimumCodeSize + 1; }
		}
		#endregion
		
		#region EndOfInformation property
		/// <summary>
		/// Gets the code which explicitly marks the end of the image data in
		/// the stream.
		/// </summary>
		public int EndOfInformation
		{
			get { return ClearCode + 1; }
		}
		#endregion
		
		#endregion
		
		#region private methods
		
		#region private static GetMaximumPossibleCode method
		/// <summary>
		/// Gets the highest possible code for the supplied code size - when
		/// all bits in the code are set to 1.
		/// This is used as a bitmask to extract the correct number of least 
		/// significant bits from the datum to form a code.
		/// </summary>
		/// <param name="currentCodeSize"></param>
		/// <returns></returns>
		private static int GetMaximumPossibleCode( int currentCodeSize )
		{
			return (1 << currentCodeSize) - 1;
		}
		#endregion
		
		#region private ReadDataBlock method
		private DataBlock ReadDataBlock( Stream inputStream )
		{
			DataBlock block = new DataBlock( inputStream, XmlDebugging );
			_dataBlocks.Add( block );
			WriteDebugXmlNode( block.DebugXmlReader );
			return block;
		}
		#endregion
		
		#endregion

		#region public WriteToStream method
		/// <summary>
		/// Writes this component to the supplied output stream.
		/// TODO: maybe use LzwEncoder to do this?
		/// </summary>
		/// <param name="outputStream">
		/// The output stream to write to.
		/// </param>
		public override void WriteToStream( Stream outputStream )
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
