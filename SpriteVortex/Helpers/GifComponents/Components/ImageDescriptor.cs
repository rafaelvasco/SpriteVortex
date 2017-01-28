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

using System.Drawing;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Types;

namespace SpriteVortex.Helpers.GifComponents.Components
{
	/// <summary>
	/// Describes a single image within a Graphics Interchange Format data 
	/// stream.
	/// See http://www.w3.org/Graphics/GIF/spec-gif89a.txt section 20.
	/// </summary>
	/// <remarks>
	/// Each image in the Data Stream is composed of an Image Descriptor, an 
	/// optional Local Color Table, and the image data.  Each image must fit 
	/// within the boundaries of the Logical Screen, as defined in the 
	/// Logical Screen Descriptor.
	/// 
	/// The Image Descriptor contains the parameters necessary to process a 
	/// table based image. The coordinates given in this block refer to 
	/// coordinates within the Logical Screen, and are given in pixels. This 
	/// block is a Graphic-Rendering Block, optionally preceded by one or more 
	/// Control blocks such as the Graphic Control Extension, and may be 
	/// optionally followed by a Local Color Table; the Image Descriptor is 
	/// always followed by the image data.
	/// 
	/// This block is REQUIRED for an image.  Exactly one Image Descriptor must
	/// be present per image in the Data Stream.  An unlimited number of images
	/// may be present per Data Stream.
	/// 
	/// The scope of this block is the Table-based Image Data Block that 
	/// follows it. This block may be modified by the Graphic Control Extension.
	/// </remarks>
	public class ImageDescriptor : GifComponent
	{
		#region declarations
		private Point _position;
		private Size _size;
		private bool _hasLocalColourTable;
		private bool _isInterlaced;
		private bool _isSorted;
		private int _localColourTableSizeBits;
		#endregion
		
		#region constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="position">
		/// Sets the <see cref="Position"/>.
		/// </param>
		/// <param name="size">
		/// Sets the <see cref="Size"/>.
		/// </param>
		/// <param name="hasLocalColourTable">
		/// Sets the <see cref="HasLocalColourTable"/> flag.
		/// </param>
		/// <param name="isInterlaced">
		/// Sets the <see cref="IsInterlaced"/> flag.
		/// </param>
		/// <param name="isSorted">
		/// Sets the <see cref="IsSorted"/> flag.
		/// </param>
		/// <param name="localColourTableSizeBits">
		/// Sets the <see cref="LocalColourTableSizeBits"/>.
		/// </param>
		public ImageDescriptor( Point position,
		                        Size size,
		                        bool hasLocalColourTable,
		                        bool isInterlaced,
		                        bool isSorted,
		                        int localColourTableSizeBits )
		{
			_position = position;
			_size = size;
			_hasLocalColourTable = hasLocalColourTable;
			_isInterlaced = isInterlaced;
			_isSorted = isSorted;
			_localColourTableSizeBits = localColourTableSizeBits;
		}
		#endregion
		
		#region constructor( Stream )
		/// <summary>
		/// Reads and returns an image descriptor from the supplied stream.
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to read.
		/// </param>
		public ImageDescriptor( Stream inputStream ) : this( inputStream, false )
		{}
		#endregion
		
		#region constructor( Stream, bool )
		/// <summary>
		/// Reads and returns an image descriptor from the supplied stream.
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to read.
		/// </param>
		/// <param name="xmlDebugging">Whether or not to create debug XML</param>
		public ImageDescriptor( Stream inputStream, bool xmlDebugging )
			: base( xmlDebugging )
		{
			int leftPosition = ReadShort( inputStream ); // (sub)image position & size
			int topPosition = ReadShort( inputStream );
			int width = ReadShort( inputStream );
			int height = ReadShort( inputStream );
			_position = new Point( leftPosition, topPosition );
			_size = new Size( width, height );

			PackedFields packed = new PackedFields( Read( inputStream ) );
			_hasLocalColourTable = packed.GetBit( 0 );
			_isInterlaced = packed.GetBit( 1 );
			_isSorted = packed.GetBit( 2 );
			_localColourTableSizeBits = packed.GetBits( 5, 3 );
			
			if( XmlDebugging )
			{
				WriteDebugXmlStartElement( "Position" );
				WriteDebugXmlAttribute( "X", _position.X );
				WriteDebugXmlAttribute( "Y", _position.Y );
				WriteDebugXmlEndElement();
				
				WriteDebugXmlStartElement( "Size" );
				WriteDebugXmlAttribute( "Width", _size.Width );
				WriteDebugXmlAttribute( "Height", _size.Height );
				WriteDebugXmlEndElement();
				
				WriteDebugXmlStartElement( "PackedFields" );
				WriteDebugXmlAttribute( "ByteRead", ToHex( packed.Byte ) );
				WriteDebugXmlAttribute( "HasLocalColourTable", 
				                        _hasLocalColourTable );
				WriteDebugXmlAttribute( "IsInterlaced", _isInterlaced );
				WriteDebugXmlAttribute( "LocalColourTableIsSorted", _isSorted );
				WriteDebugXmlAttribute( "LocalColourTableSizeBits", 
				                        _localColourTableSizeBits );
				WriteDebugXmlEndElement();
				
				WriteDebugXmlFinish();
			}
		}
		#endregion

		#region logical properties
		
		#region Position property
		/// <summary>
		/// Gets the position, in pixels, of the top-left corner of the image,
		/// with respect to the top-left corner of the logical screen.
		/// Top-left corner of the logical screen is 0,0.
		/// </summary>
		public Point Position
		{
			get { return _position; }
		}
		#endregion

		#region Size property
		/// <summary>
		/// Gets the size of the image in pixels.
		/// </summary>
		public Size Size
		{
			get { return _size; }
		}
		#endregion
		
		#region HasLocalColourTable property
		/// <summary>
		/// Gets a boolean value indicating the presence of a Local Color Table 
		/// immediately following this Image Descriptor.
		/// </summary>
		public bool HasLocalColourTable
		{
			get { return _hasLocalColourTable;; }
		}
		#endregion
		
		#region IsInterlaced property
		/// <summary>
		/// Gets a boolean value indicating whether the image is interlaced. An 
		/// image is interlaced in a four-pass interlace pattern; see Appendix E 
		/// for details.
		/// </summary>
		public bool IsInterlaced
		{
			get { return _isInterlaced; }
		}
		#endregion
		
		#region IsSorted property
		/// <summary>
		/// Gets a boolean value indicating whether the Local Color Table is
		/// sorted.  If the flag is set, the Local Color Table is sorted, in
		/// order of decreasing importance. Typically, the order would be
		/// decreasing frequency, with most frequent color first. This assists
		/// a decoder, with fewer available colors, in choosing the best subset
		/// of colors; the decoder may use an initial segment of the table to
		/// render the graphic.
		/// </summary>
		public bool IsSorted
		{
			get { return _isSorted; }
		}
		#endregion
		
		#region LocalColourTableSizeBits property
		/// <summary>
		/// If the Local Color Table Flag is set to 1, the value in this field 
		/// is used to calculate the number of bytes contained in the Local 
		/// Color Table. To determine that actual size of the color table, 
		/// raise 2 to the value of the field + 1. 
		/// This value should be 0 if there is no Local Color Table specified.
		/// </summary>
		public int LocalColourTableSizeBits
		{
			get { return _localColourTableSizeBits; }
		}
		#endregion
		
		#region LocalColourTableSize property
		/// <summary>
		/// Gets the actual size of the local colour table.
		/// </summary>
		public int LocalColourTableSize
		{
			get { return 2 << _localColourTableSizeBits; }
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
			// Position and size of the image in this frame
			WriteShort( _position.X, outputStream );
			WriteShort( _position.Y, outputStream );
			WriteShort( _size.Width, outputStream );
			WriteShort( _size.Height, outputStream );
			
			PackedFields packed = new PackedFields();
			packed.SetBit( 0, _hasLocalColourTable );
			packed.SetBit( 1, _isInterlaced );
			packed.SetBit( 2, _isSorted );
			packed.SetBits( 5, 3, _localColourTableSizeBits );
			WriteByte( packed.Byte, outputStream );
		}
		#endregion
	}
}
