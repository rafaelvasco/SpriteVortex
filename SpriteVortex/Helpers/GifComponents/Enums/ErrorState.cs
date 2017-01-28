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
using System.Diagnostics.CodeAnalysis;

namespace SpriteVortex.Helpers.GifComponents.Enums
{
	/// <summary>
	/// The error state of a GIF data stream, or of one of the components of
	/// the stream.
	/// </summary>
	[Flags]
	[SuppressMessage("Microsoft.Design", 
	                 "CA1008:EnumsShouldHaveZeroValue")]
	[SuppressMessage("Microsoft.Naming", 
	                 "CA1714:FlagsEnumsShouldHavePluralNames")]
	public enum ErrorState
	{
		/// <summary>
		/// OK - no errors.
		/// </summary>
		Ok = 0,
		
		/// <summary>
		/// The "GIF" signature at the start of the file could not be found.
		/// </summary>
		BadSignature = 1,
		
		/// <summary>
		/// Unable to open the file for reading. The file could already be open.
		/// </summary>
		CouldNotOpenFile = 1 << 1,
		
		/// <summary>
		/// A data block was read which was shorter than its declared length.
		/// </summary>
		DataBlockTooShort = 1 << 2,
		
		/// <summary>
		/// A colour table was shorter than its declared length.
		/// </summary>
		ColourTableTooShort = 1 << 3,
		
		/// <summary>
		/// One or more frames has no active colour table.
		/// </summary>
		FrameHasNoColourTable = 1 << 4,
		
		/// <summary>
		/// A data block begins with an invalid byte.
		/// Valid bytes are:
		/// 	0x2c - image separator
		/// 	0x21 - extension
		/// 	0x3b - terminator
		/// 	0x00 - actually invalid but supported by the decoder
		/// </summary>
		BadDataBlockIntroducer = 1 << 5,
		
		/// <summary>
		/// The decoder is still trying to read more data from the input stream
		/// but has reached the end of the stream.
		/// </summary>
		EndOfInputStream = 1 << 6,
		
		/// <summary>
		/// An image block contains a LZW minimum code size which would result
		/// in a clear code larger than the maximum stack size.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "Lzw")]
		LzwMinimumCodeSizeTooLarge = 1 << 7,
		
		/// <summary>
		/// An application extension declared itself as a Netscape extension
		/// but does not follow the format of a Netscape extension.
		/// </summary>
		NotANetscapeExtension = 1 << 8,
		
		/// <summary>
		/// The identification block is longer than 11 bytes.
		/// </summary>
		IdentificationBlockTooLong = 1 << 9,
		
		/// <summary>
		/// A data block was read which was longer than its declared length.
		/// </summary>
		DataBlockTooLong = 1 << 10,
		
		/// <summary>
		/// The table-based image data contained data for fewer pixels than
		/// expected given the supplied image size.
		/// </summary>
		TooFewPixelsInImageData = 1 << 11,
		
		/// <summary>
		/// The decoder's dictionary is full, it was not possible to increase
		/// the code size given the maximum possible size of the dictionary.
		/// </summary>
		UnableToIncreaseCodeSize = 1 << 12,
		
		/// <summary>
		/// A code was read which is not in the dictionary and is not the next
		/// code available to be added to the dictionary.
		/// </summary>
		CodeNotInDictionary = 1 << 13,
		
		/// <summary>
		/// The next available code is greater than or equal to the maximum
		/// stack size.
		/// </summary>
		StackSizeExceeded = 1 << 14,
		
		/// <summary>
		/// The frame's LZW image data decoded to a zero-length array.
		/// </summary>
		FrameHasNoImageData = 1 << 15,

		/// <summary>
		/// An unexpected block terminator was encountered.
		/// </summary>
		UnexpectedBlockTerminator = 1 << 16,
		
		/// <summary>
		/// The decoded image data contains an index which is outside the range
		/// of the active colour table.
		/// </summary>
		BadColourIndex = 1 << 17,
		
		/// <summary>
		/// The frame does not have a graphic control extension.
		/// </summary>
		NoGraphicControlExtension = 1 << 18
	}
}
