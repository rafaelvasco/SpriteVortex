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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Enums;
using SpriteVortex.Helpers.GifComponents.Types;

namespace SpriteVortex.Helpers.GifComponents.Components
{
	/// <summary>
	/// The Logical Screen Descriptor component of a Graphics Interchange Format
	/// stream.
	/// See http://www.w3.org/Graphics/GIF/spec-gif89a.txt section 18.
	/// </summary>
	/// <remarks>
	/// The Logical Screen Descriptor contains the parameters necessary to 
	/// define the area of the display device within which the images will be 
	/// rendered. The coordinates in this block are given with respect to the 
	/// top-left corner of the virtual screen; they do not necessarily refer to 
	/// absolute coordinates on the display device.  This implies that they 
	/// could refer to window coordinates in a window-based environment or 
	/// printer coordinates when a printer is used. 
	/// This block is REQUIRED; exactly one Logical Screen Descriptor must be
	/// present per Data Stream.
	/// </remarks>
	public class LogicalScreenDescriptor : GifComponent
	{
		#region declarations
		private Size _screenSize;
		private bool _hasGlobalColourTable;
		private int _colourResolution;
		private bool _gctIsSorted;
		private int _gctSizeBits;
		private int _backgroundColourIndex;
		private int _pixelAspectRatio;
		
		private const int _byteMax = byte.MaxValue;
		private const int _uShortMax = ushort.MaxValue;
		#endregion
		
		#region constructor( logical properties )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="logicalScreenSize">
		/// The width and height, in pixels, of the logical screen where
		/// the images will be rendered in the displaying device.
		/// </param>
		/// <param name="hasGlobalColourTable">
		/// Indicates the presence of a Global Color Table; if the flag is set, 
		/// the Global Color Table will immediately follow the Logical Screen 
		/// Descriptor. 
		/// This flag also selects the interpretation of the Background Color 
		/// Index; if the flag is set, the value of the Background Color Index 
		/// field should be used as the table index of the background color.
		/// </param>
		/// <param name="colourResolution">
		/// The number of bits per primary color available to the original 
		/// image, minus 1. 
		/// This value represents the size of the entire palette from which the 
		/// colors in the graphic were selected, not the number of colors 
		/// actually used in the graphic.
		/// For example, if the value in this field is 3, then the palette of
		/// the original image had 4 bits per primary color available to create
		/// the image.  This value should be set to indicate the richness of
		/// the original palette, even if not every color from the whole
		/// palette is available on the source machine.
		/// </param>
		/// <param name="globalColourTableIsSorted">
		/// Indicates whether the Global Color Table is sorted.
		/// If the flag is set, the Global Color Table is sorted, in order of
		/// decreasing importance. Typically, the order would be decreasing
		/// frequency, with most frequent color first. This assists a decoder,
		/// with fewer available colors, in choosing the best subset of colors;
		/// the decoder may use an initial segment of the table to render the
		/// </param>
		/// <param name="globalColourTableSizeBits">
		/// The number of bits required to hold the size of the global colour
		/// table.
		/// To determine the actual size of the color table, raise 2 to 
		/// [the value of the field + 1]. 
		/// E.g. if this parameter is 7 then the global colour table will be 
		/// 2^(7+1) = 256 bytes long.
		/// See also <see cref="GlobalColourTableSize"/>
		/// </param>
		/// <param name="backgroundColourIndex">
		/// Gets the index into the Global Color Table for the Background Color.
		/// The Background Color is the color used for those pixels on the 
		/// screen that are not covered by an image. 
		/// If the Global Color Table Flag is set to (zero), this field should 
		/// be zero and should be ignored.
		/// </param>
		/// <param name="pixelAspectRatio">
		/// Gets the factor used to compute an approximation of the aspect ratio 
		/// of the pixel in the original image.  
		/// </param>
		/// <returns></returns>
		public LogicalScreenDescriptor( Size logicalScreenSize,
		                                bool hasGlobalColourTable,
		                                int colourResolution,
		                                bool globalColourTableIsSorted,
		                                int globalColourTableSizeBits,
		                                int backgroundColourIndex,
		                                int pixelAspectRatio )
		{
			#region validation
			if( logicalScreenSize.Width > _uShortMax )
			{
				string message
					= "Logical screen width cannot be more than "
					+ _uShortMax + ". "
					+ "Supplied value: " + logicalScreenSize.Width;
				throw new ArgumentException( message, "logicalScreenSize" );
			}
			if( logicalScreenSize.Height > _uShortMax )
			{
				string message
					= "Logical screen height cannot be more than "
					+ _uShortMax + ". "
					+ "Supplied value: " + logicalScreenSize.Height;
				throw new ArgumentException( message, "logicalScreenSize" );
			}
			if( colourResolution > 7 )
			{
				string message
					= "Colour resolution cannot be more than 7. "
					+ "Supplied value: " + colourResolution;
				throw new ArgumentException( message, "colourResolution" );
			}
			if( globalColourTableSizeBits > 7 )
			{
				string message
					= "Global colour table size cannot be more than 7. "
					+ "Supplied value: " + globalColourTableSizeBits;
				throw new ArgumentException( message, 
				                             "globalColourTableSizeBits" );
			}
			if( backgroundColourIndex > _byteMax )
			{
				string message
					= "Background colour index cannot be more than "
					+ _byteMax + ". "
					+ "Supplied value: " + backgroundColourIndex;
				throw new ArgumentException( message, "backgroundColourIndex" );
			}
			if( pixelAspectRatio > _byteMax )
			{
				string message
					= "Pixel aspect ratio cannot be more than "
					+ _byteMax + ". "
					+ "Supplied value: " + pixelAspectRatio;
				throw new ArgumentException( message, "pixelAspectRatio" );
			}
			#endregion
			
			_screenSize = logicalScreenSize;
			_hasGlobalColourTable = hasGlobalColourTable;
			_colourResolution = colourResolution;
			_gctIsSorted = globalColourTableIsSorted;
			_gctSizeBits = globalColourTableSizeBits;
			_backgroundColourIndex = backgroundColourIndex;
			_pixelAspectRatio = pixelAspectRatio;
		}
		#endregion

		#region constructor( Stream )
		/// <summary>
		/// Constructor.
		/// Reads and returns a logical screen descriptor from the supplied
		/// input stream.
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to be read.
		/// </param>
		public LogicalScreenDescriptor( Stream inputStream )
			: this( inputStream, false ) {}
		#endregion
		
		#region constructor( Stream, bool )
		/// <summary>
		/// Constructor.
		/// Reads and returns a logical screen descriptor from the supplied
		/// input stream.
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to be read.
		/// </param>
		/// <param name="xmlDebugging">
		/// Whether or not to create debug XML.
		/// </param>
		public LogicalScreenDescriptor( Stream inputStream, bool xmlDebugging )
			: base( xmlDebugging )
		{
			// logical screen size
			int width = ReadShort( inputStream );
			int height = ReadShort( inputStream );
			_screenSize = new Size( width, height );

			PackedFields packed = new PackedFields( Read( inputStream ) );
			_hasGlobalColourTable = packed.GetBit( 0 );
			_colourResolution = packed.GetBits( 1, 3 );
			_gctIsSorted = packed.GetBit( 4 );
			_gctSizeBits = packed.GetBits( 5, 3 );

			_backgroundColourIndex = Read( inputStream );
			_pixelAspectRatio = Read( inputStream );
			
			if( XmlDebugging )
			{
				WriteDebugXmlStartElement( "LogicalScreenSize" );
				WriteDebugXmlAttribute( "Width", width );
				WriteDebugXmlAttribute( "Height", height );
				WriteDebugXmlEndElement();

				WriteDebugXmlStartElement( "PackedFields" );
				WriteDebugXmlAttribute( "ByteRead", ToHex( packed.Byte ) );
				WriteDebugXmlAttribute( "HasGlobalColourTable", _hasGlobalColourTable );
				WriteDebugXmlAttribute( "ColourResolution", _colourResolution );
				WriteDebugXmlAttribute( "GlobalColourTableIsSorted", _gctIsSorted );
				WriteDebugXmlAttribute( "GlobalColourTableSizeBits", _gctSizeBits );
				WriteDebugXmlEndElement();
				
				WriteDebugXmlElement( "BackgroundColourIndex", _backgroundColourIndex );
				WriteDebugXmlElement( "PixelAspectRatio", _pixelAspectRatio );
			}
			
			if( width < 0 || height < 0 || packed.Byte < 0 
			   || _backgroundColourIndex < 0 || _pixelAspectRatio < 0 )
			{
				SetStatus( ErrorState.EndOfInputStream, "" );
			}
			WriteDebugXmlFinish();
		}
		#endregion

		#region logical properties
		
		#region LogicalScreenSize
		/// <summary>
		/// Gets the width and height, in pixels, of the logical screen where
		/// the images will be rendered in the displaying device.
		/// </summary>
		[Description( "Width and height, in pixels, of the Logical Screen " +
		              "where the images will be rendered in the displaying " +
		              "device." )]
		public Size LogicalScreenSize
		{
			get { return _screenSize; }
		}
		#endregion
		
		#region HasGlobalColourTable property
		/// <summary>
		/// Gets a flag indicating the presence of a Global Color Table; if the 
		/// flag is set, the Global Color Table will immediately follow the 
		/// Logical Screen Descriptor. This flag also selects the interpretation 
		/// of the Background Color Index; if the flag is set, the value of the 
		/// Background Color Index field should be used as the table index of 
		/// the background color.
		/// </summary>
		[Description( "Flag indicating the presence of a Global Color Table; " +
		              "if the flag is set, the Global Color Table will " +
		              "immediately follow the Logical Screen Descriptor. " +
		              "This flag also selects the interpretation of the " +
		              "Background Color Index; if the flag is set, the value " +
		              "of the Background Color Index field should be used as " +
		              "the table index of the background color. " +
		              "(This field is the most significant bit of the byte.)" )]
		public bool HasGlobalColourTable
		{
			get { return _hasGlobalColourTable; }
		}
		#endregion
		
		#region ColourResolution property
		/// <summary>
		/// Gets the number of bits per primary color available to the original 
		/// image, minus 1. This value represents the size of the entire palette 
		/// from which the colors in the graphic were selected, not the number 
		/// of colors actually used in the graphic.
		/// For example, if the value in this field is 3, then the palette of
		/// the original image had 4 bits per primary color available to create
		/// the image.  This value should be set to indicate the richness of
		/// the original palette, even if not every color from the whole
		/// palette is available on the source machine.
		/// </summary>
		[Description( "Number of bits per primary color available to the " +
		              "original image, minus 1. This value represents the size " +
		              "of the entire palette from which the colors in the " +
		              "graphic were selected, not the number of colors actually " +
		              "used in the graphic. For example, if the value in this " +
		              "field is 3, then the palette of the original image had " +
		              "4 bits per primary color available to create the image. " +
		              "This value should be set to indicate the richness of the " +
		              "original palette, even if not every color from the whole " +
		              "palette is available on the source machine" )]
		public int ColourResolution
		{
			get { return _colourResolution; }
		}
		#endregion
		
		#region GlobalColourTableIsSorted property
		/// <summary>
		/// Indicates whether the Global Color Table is sorted.
		/// If the flag is set, the Global Color Table is sorted, in order of
		/// decreasing importance. Typically, the order would be decreasing
		/// frequency, with most frequent color first. This assists a decoder,
		/// with fewer available colors, in choosing the best subset of colors;
		/// the decoder may use an initial segment of the table to render the
		/// graphic.
		/// </summary>
		[Description( "Indicates whether the Global Color Table is sorted. If " +
		             "the flag is set, the Global Color Table is sorted, in " +
		             "order of decreasing importance. " +
		             "Typically, the order would be decreasing frequency, " +
		             "with most frequent color first. " +
		             "This assists a decoder, with fewer available colors, " +
		             "in choosing the best subset of colors; the decoder may " +
		             "use an initial segment of the table to render the " +
		             "graphic." )]
		public bool GlobalColourTableIsSorted
		{
			get { return _gctIsSorted; }
		}
		#endregion
		
		#region GlobalColourTableSizeBits property
		/// <summary>
		/// If the Global Color Table Flag is set to 1, the value in this field 
		/// is used to calculate the number of bytes contained in the Global 
		/// Color Table. To determine that actual size of the color table, 
		/// raise 2 to [the value of the field + 1].  
		/// Even if there is no Global Color Table specified, set this field 
		/// according to the above formula so that decoders can choose the best 
		/// graphics mode to display the stream in.
		/// </summary>
		/// <remarks>
		/// This is the value of the globalColourTableSizeBits parameter passed
		/// to the constructor.
		/// </remarks>
		[Description( "If the Global Color Table Flag is set to 1, the " + 
		             "value in this field is used to calculate the number " + 
		             "of bytes contained in the Global Color Table. To " + 
		             "determine that actual size of the color table, raise 2 " + 
		             "to [the value of the field + 1]. " + 
		             "Even if there is no Global Color Table specified, set " + 
		             "this field according to the above formula so that " + 
		             "decoders can choose the best graphics mode to display " + 
		             "the stream in." )]
		public int GlobalColourTableSizeBits
		{
			get { return _gctSizeBits; }
		}
		#endregion
		
		#region GlobalColourTableSize property
		/// <summary>
		/// Gets the number of colours in the global colour table.
		/// </summary>
		public int GlobalColourTableSize
		{
			get { return 2 << _gctSizeBits; }
		}
		#endregion
		
		#region BackgroundColourIndex property
		/// <summary>
		/// Gets the index into the Global Color Table for the Background Color.
		/// The Background Color is the color used for those pixels on the 
		/// screen that are not covered by an image. 
		/// If the Global Color Table Flag is set to (zero), this field should 
		/// be zero and should be ignored.
		/// </summary>
		[Description( "Index into the Global Color Table for the Background " +
		              "Color. The Background Color is the color used for those " +
		              "pixels on the screen that are not covered by an image. " +
		              "If the Global Color Table Flag is set to (zero), this " +
		              "field should be zero and should be ignored." )]
		public int BackgroundColourIndex
		{
			get { return _backgroundColourIndex; }
		}
		#endregion
		
		#region PixelAspectRatio property
		/// <summary>
		/// Gets the factor used to compute an approximation of the aspect ratio 
		/// of the pixel in the original image.  If the value of the field is 
		/// not 0, this approximation of the aspect ratio is computed based on 
		/// the formula:
		/// 
		/// Aspect Ratio = (Pixel Aspect Ratio + 15) / 64
		/// 
		/// The Pixel Aspect Ratio is defined to be the quotient of the pixel's
		/// width over its height.  The value range in this field allows
		/// specification of the widest pixel of 4:1 to the tallest pixel of
		/// 1:4 in increments of 1/64th.
		/// </summary>
		[Description( "Pixel Aspect Ratio - Factor used to compute an " +
		              "approximation of the aspect ratio of the pixel in the " +
		              "original image.  " +
		              "If the value of the field is not 0, this approximation " +
		              "of the aspect ratio is computed based on the formula: " +
		              "Aspect Ratio = (Pixel Aspect Ratio + 15) / 64. " +
		              "The Pixel Aspect Ratio is defined to be the quotient " +
		              "of the pixel's width over its height.  " +
		              "The value range in this field allows specification of " +
		              "the widest pixel of 4:1 to the tallest pixel of 1:4 " +
		              "in increments of 1/64th." )]
		public int PixelAspectRatio
		{
			get { return _pixelAspectRatio; }
		}
		#endregion
		
		#endregion

		#region public WriteToStream method
		/// <summary>
		/// Writes this component to the supplied output stream.
		/// </summary>
		/// <param name="outputStream">
		/// The output stream to write to.
		/// </param>
		public override void WriteToStream( Stream outputStream )
		{
			// logical screen size
			WriteShort( _screenSize.Width, outputStream );
			WriteShort( _screenSize.Height, outputStream );

			// Packed fields
			PackedFields packed = new PackedFields();
			packed.SetBit( 0, _hasGlobalColourTable );
			packed.SetBits( 1, 3, _colourResolution );
			packed.SetBit( 4, _gctIsSorted );
			packed.SetBits( 5, 3, _gctSizeBits );
			WriteByte( packed.Byte, outputStream );
			
			WriteByte( _backgroundColourIndex, outputStream );
			WriteByte( _pixelAspectRatio, outputStream );
		}
		#endregion
	}
}
