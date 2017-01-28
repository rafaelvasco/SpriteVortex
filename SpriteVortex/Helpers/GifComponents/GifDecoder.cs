#region Copyright (C) Simon Bridewell, Kevin Weiner, John Christy
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Components;
using SpriteVortex.Helpers.GifComponents.Enums;

namespace SpriteVortex.Helpers.GifComponents
{
	/// <summary>
	/// Class GifDecoder - Decodes a GIF file into one or more frames and 
	/// exposes its properties, components and any error states.
	/// 
	/// No copyright asserted on the source code of this class.  May be used for
	/// any purpose, however, refer to the Unisys LZW patent for any additional
	/// restrictions.  Please forward any corrections to kweiner@fmsware.com.
	///
	/// @author Kevin Weiner, FM Software; LZW decoder adapted from John 
	/// 	Cristy's ImageMagick.
	/// @version 1.03 November 2003
	/// 
	/// Modified by Simon Bridewell, June 2009 - March 2010:
	/// Downloaded from 
	/// http://www.thinkedge.com/blogengine/post/2008/02/20/Animated-GIF-Encoder-for-NET-Update.aspx
	/// http://www.thinkedge.com/BlogEngine/file.axd?file=NGif_src2.zip
	/// 
	/// 1. Adapted for FxCop code analysis compliance and documentation 
	/// 	comments converted to .net XML comments.
	/// 2. Added comments relating the properties to data items specified in
	/// 	http://www.w3.org/Graphics/GIF/spec-gif89a.txt
	/// 3. Added property getters to expose the components of the GIF file.
	/// 4. Refactored large amounts of functionality into separate classes
	/// 	which encapsulate the types of the components of a GIF file.
	/// 5. Removed all private declarations which are not components of a GIF
	/// 	file.
	/// 6. Added State property to indicate whether decoding is not started,
	/// 	in progress or complete.
	/// 7. Added StreamLength and StreamPosition properties.
	/// 8. Added progress counters for use with LongRunningProcess and 
	///    ResponsiveForm.
	/// </summary>
	public class GifDecoder : GifComponent
	{

		#region declarations
		
		/// <summary>
		/// The header of the GIF file.
		/// </summary>
		private GifHeader _header;
		
		/// <summary>
		/// The Logical Screen Descriptor contains the parameters necessary to 
		/// define the area of the display device within which the images will 
		/// be rendered.
		/// The coordinates in this block are given with respect to the 
		/// top-left corner of the virtual screen; they do not necessarily 
		/// refer to absolute coordinates on the display device.
		/// This implies that they could refer to window coordinates in a 
		/// window-based environment or printer coordinates when a printer is 
		/// used. 
		/// This block is REQUIRED; exactly one Logical Screen Descriptor must be
		/// present per Data Stream.
		/// </summary>
		private LogicalScreenDescriptor _lsd;

		/// <summary>
		/// The global colour table, if present.
		/// </summary>
		private ColourTable _gct;

		/// <summary>
		/// Netscape extension, if present
		/// </summary>
		private NetscapeExtension _netscapeExtension;

		/// <summary>
		/// Collection of the application extensions in the file.
		/// </summary>
		private Collection<ApplicationExtension> _applicationExtensions;
		
		/// <summary>
		/// The frames which make up the GIF file.
		/// </summary>
		private Collection<GifFrame> _frames;
		
		
		/// <summary>
		/// An enum indicating whether the decoder has not started, is in 
		/// progress, or has finished.
		/// </summary>
		private GifDecoderState _state;

		/// <summary>
		/// Holds the <see cref="System.IO.Stream"/> from which the GIF is being
		/// read.
		/// </summary>
		private Stream _stream;
		#endregion
		
		#region constructors
		
		#region constructor( string )
		/// <summary>
		/// Reads a GIF file from specified file/URL source  
		/// (URL assumed if name contains ":/" or "file:")
		/// </summary>
		/// <param name="fileName">
		/// Path or URL of image file.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// The supplied filename is null.
		/// </exception>
		public GifDecoder( string fileName )
			: this( fileName, false )
		{
		}
		#endregion
		
		#region constructor( string, bool )
		/// <summary>
		/// Reads a GIF file from specified file/URL source  
		/// (URL assumed if name contains ":/" or "file:")
		/// </summary>
		/// <param name="fileName">
		/// Path or URL of image file.
		/// </param>
		/// <param name="xmlDebugging">
		/// A boolean value indicating whether or not an XML document should be 
		/// created showing how the GIF stream was decoded.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// The supplied filename is null.
		/// </exception>
		public GifDecoder( string fileName, bool xmlDebugging )
			: base( xmlDebugging )
		{
			if( fileName == null )
			{
				throw new ArgumentNullException( "fileName" );
			}
			else
			{
				fileName = fileName.Trim().ToLower( CultureInfo.InvariantCulture );
				Stream inputStream = new FileInfo( fileName ).OpenRead();
				_stream = inputStream;
			}
		}
		#endregion
		
		#region constructor( stream )
		/// <summary>
		/// Reads a GIF file from the specified stream.
		/// </summary>
		/// <param name="inputStream">
		/// A stream to read the GIF data from.
		/// </param>
		public GifDecoder( Stream inputStream )
			: this( inputStream, false ) {}
		#endregion
		
		#region constructor( stream, bool )
		/// <summary>
		/// Reads a GIF file from the specified stream.
		/// </summary>
		/// <param name="inputStream">
		/// A stream to read the GIF data from.
		/// </param>
		/// <param name="xmlDebugging">
		/// A boolean value indicating whether or not an XML document should be 
		/// created showing how the GIF stream was decoded.
		/// </param>
		public GifDecoder( Stream inputStream, bool xmlDebugging )
			: base( xmlDebugging )
		{
			if( inputStream == null )
			{
				throw new ArgumentNullException( "inputStream" );
			}
			
			if( inputStream.CanRead == false )
			{
				string message
					= "The supplied stream cannot be read";
				throw new ArgumentException( message, "inputStream" );
			}
			_stream = inputStream;
			_state = GifDecoderState.NotStarted;
		}
		#endregion
		
		#endregion
		
		#region Decode method
		/// <summary>
		/// Decodes the supplied GIF stream.
		/// </summary>
		public void Decode()
		{
			_state = GifDecoderState.Decoding;
			ReadStream( _stream );
			_state = GifDecoderState.Done;
		}
		#endregion
		
		#region properties
		
		#region Header property
		/// <summary>
		/// Gets the header of the GIF stream, containing the signature and
		/// version of the GIF standard used.
		/// </summary>
		public GifHeader Header
		{
			get { return _header; }
		}
		#endregion
		
		#region Logical Screen descriptor property
		/// <summary>
		/// Gets the logical screen descriptor.
		/// </summary>
		/// <remarks>
		/// The Logical Screen Descriptor contains the parameters necessary to 
		/// define the area of the display device within which the images will 
		/// be rendered.
		/// The coordinates in this block are given with respect to the 
		/// top-left corner of the virtual screen; they do not necessarily 
		/// refer to absolute coordinates on the display device.
		/// This implies that they could refer to window coordinates in a 
		/// window-based environment or printer coordinates when a printer is 
		/// used.
		/// </remarks>
		[Description( "The Logical Screen Descriptor contains the parameters " +
		              "necessary to define the area of the display device " + 
		              "within which the images will be rendered. " + 
		              "The coordinates in this block are given with respect " + 
		              "to the top-left corner of the virtual screen; they do " + 
		              "not necessarily refer to absolute coordinates on the " + 
		              "display device. " + 
		              "This implies that they could refer to window " + 
		              "coordinates in a window-based environment or printer " + 
		              "coordinates when a printer is used." )]
		public LogicalScreenDescriptor LogicalScreenDescriptor
		{
			get { return _lsd; }
		}
		#endregion
		
		#region BackgroundColour property
		/// <summary>
		/// Gets the background colour.
		/// </summary>
		[Description( "The default background colour for this GIF file." + 
		             "This is derived using the background colour index in the " +
		             "Logical Screen Descriptor and looking up the colour " + 
		             "in the Global Colour Table." )]
		public Color BackgroundColour
		{
			get 
			{ 
				return _gct[_lsd.BackgroundColourIndex];
			}
		}
		#endregion
		
		#region ApplicationExtensions property
		/// <summary>
		/// Gets the application extensions contained within the GIF stream.
		/// This is an array rather than a property because it looks better in
		/// a property sheet control.
		/// </summary>
		[SuppressMessage("Microsoft.Performance", 
		                 "CA1819:PropertiesShouldNotReturnArrays")]
		public ApplicationExtension[] ApplicationExtensions
		{
			get
			{ 
				ApplicationExtension[] appExts 
					= new ApplicationExtension[_applicationExtensions.Count];
				_applicationExtensions.CopyTo( appExts, 0 );
				return appExts;; 
			}
		}
		#endregion

		#region NetscapeExtension property
		/// <summary>
		/// Gets the Netscape 2.0 application extension, if present.
		/// This contains the animation's loop count.
		/// </summary>
		[Description( "Gets the Netscape 2.0 application extension, if " + 
		              "present. This contains the animation's loop count." )]
		public NetscapeExtension NetscapeExtension
		{
			get { return _netscapeExtension; }
		}
		#endregion
		
		#region Frames property
		/// <summary>
		/// Gets the frames of the GIF file.
		/// </summary>
		public Collection<GifFrame> Frames
		{
			get { return _frames; }
		}
		#endregion

		#region GlobalColourTable property
		/// <summary>
		/// Gets the global colour table for this GIF data stream.
		/// </summary>
		public ColourTable GlobalColourTable
		{
			get { return _gct; }
		}
		#endregion
		
		#region StreamLength property
		/// <summary>
		/// Gets the length, in bytes, of the stream being decoder.
		/// </summary>
		[Browsable( false )]
		public long StreamLength
		{
			get
			{
				if( _stream == null )
				{
					return 0;
				}
				else
				{
					if( _stream.CanRead )
					{
						return _stream.Length;
					}
					else
					{
						return 0;
					}
				}
			}
		}
		#endregion
		
		#region StreamPosition property
		/// <summary>
		/// Gets the current position in the stream being decoded.
		/// </summary>
		[Browsable( false )]
		public long StreamPosition
		{
			get
			{
				if( _stream == null )
				{
					return 0;
				}
				else
				{
					if( _stream.CanRead )
					{
						return _stream.Position;
					}
					else
					{
						return 0;
					}
				}
			}
		}
		#endregion
		
		#region State property
		/// <summary>
		/// Gets one of the GifDecoderState values indicating whether the 
		/// GifDecoder has not started, is in progress, or has finished decoding.
		/// </summary>
		public GifDecoderState State
		{
			get { return _state; }
		}
		#endregion
		
		#endregion

		#region private methods
		
		#region private ReadStream method
		/// <summary>
		/// Reads GIF image from stream
		/// </summary>
		/// <param name="inputStream">
		/// Stream containing GIF file.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// The supplied stream is null.
		/// </exception>
		private void ReadStream( Stream inputStream ) 
		{
			_frames = new Collection<GifFrame>();
			_applicationExtensions = new Collection<ApplicationExtension>();
			_gct = null;
			
			_header = new GifHeader( inputStream, XmlDebugging );
			WriteDebugXmlNode( _header.DebugXmlReader );
			if( _header.ErrorState != ErrorState.Ok )
			{
				WriteDebugXmlFinish();
				return;
			}

			_lsd = new LogicalScreenDescriptor( inputStream, XmlDebugging );
			WriteDebugXmlNode( _lsd.DebugXmlReader );
			if( TestState( ErrorState.EndOfInputStream ) )
			{
				WriteDebugXmlFinish();
				return;
			}
			
			if( _lsd.HasGlobalColourTable )
			{
				_gct = new ColourTable( inputStream, 
				                        _lsd.GlobalColourTableSize, 
				                        XmlDebugging );
				WriteDebugXmlNode( _gct.DebugXmlReader );
			}
			
			if( ConsolidatedState == ErrorState.Ok )
			{
				ReadContents( inputStream );
			}
			inputStream.Close();
			WriteDebugXmlFinish();
		}
		#endregion
		
		#region private ReadContents method
		/// <summary>
		/// Main file parser. Reads GIF content blocks.
		/// </summary>
		/// <param name="inputStream">
		/// Input stream from which the GIF data is to be read.
		/// </param>
		private void ReadContents( Stream inputStream ) 
		{
			// read GIF file content blocks
			bool done = false;
			GraphicControlExtension lastGce = null;
			string message; // for error conditions
			while( !done && ConsolidatedState == ErrorState.Ok )
			{
				int code = Read( inputStream );
				WriteCodeToDebugXml( code );
				
				switch( code )
				{

					case CodeImageSeparator:
						WriteDebugXmlComment( "0x2C - image separator" );
						AddFrame( inputStream, lastGce );
						break;

					case CodeExtensionIntroducer:
						WriteDebugXmlComment( "0x21 - extension introducer" );
						code = Read( inputStream );
						WriteCodeToDebugXml( code );
						switch( code )
						{
							case CodePlaintextLabel:
								// FEATURE: handle plain text extension
								WriteDebugXmlComment( "0x01 - plain text extension" );
								SkipBlocks( inputStream );
								break;

							case CodeGraphicControlLabel:
								WriteDebugXmlComment( "0xF9 - graphic control label" );
								lastGce = new GraphicControlExtension( inputStream, 
								                                       XmlDebugging );
								WriteDebugXmlNode( lastGce.DebugXmlReader );
								break;
								
							case CodeCommentLabel:
								// FEATURE: handle comment extension
								WriteDebugXmlComment( "0xFE - comment extension" );
								SkipBlocks( inputStream );
								break;

							case CodeApplicationExtensionLabel:
								WriteDebugXmlComment( "0xFF - application extension label" );
								ApplicationExtension ext 
									= new ApplicationExtension( inputStream, 
									                            XmlDebugging );
								WriteDebugXmlNode( ext.DebugXmlReader );
								if( ext.ApplicationIdentifier == "NETSCAPE"
								    && ext.ApplicationAuthenticationCode == "2.0" )
								{
									_netscapeExtension = new NetscapeExtension( ext );
								}
								else
								{
									// TESTME: ReadContents - non-Netscape application extension
									_applicationExtensions.Add( ext );
								}
								break;
	
							default : // uninteresting extension
								// TESTME: ReadContents - uninteresting extension
								WriteDebugXmlComment( "Ignoring this extension" );
								SkipBlocks( inputStream );
								break;
						}
						break;

					case CodeTrailer:
						// We've reached an explicit end-of-data marker, so stop
						// processing the stream.
						WriteDebugXmlComment( "0x3B - end of data" );
						done = true;
						break;

					case 0x00 : // bad byte, but keep going and see what happens
						WriteDebugXmlComment( "0x00 - unexpected code" );
						message
							= "Unexpected block terminator encountered at "
							+ "position " + inputStream.Position
							+ " after reading " + _frames.Count + " frames.";
						SetStatus( ErrorState.UnexpectedBlockTerminator, 
						           message );
						break;
						
					case -1: // reached the end of the input stream
						WriteDebugXmlComment( "-1 - end of input stream" );
						message
							= "Reached the end of the input stream without "
							+ "encountering trailer 0x3b";
						SetStatus( ErrorState.EndOfInputStream, message );
						break;

					default :
						WriteDebugXmlComment( "Not a recognised code" );
						message 
							= "Bad data block introducer: 0x"
							+ code.ToString( "X", CultureInfo.InvariantCulture ).PadLeft( 2, '0' )
							+ " (" + code + ") at position " + inputStream.Position
							+ " (" 
							+ inputStream.Position.ToString( "X", 
							                               CultureInfo.InvariantCulture )
							+ " hex) after reading "
							+ _frames.Count + " frames.";
						SetStatus( ErrorState.BadDataBlockIntroducer, message );
						break;
				}
			}
		}
		#endregion
		
		#region private WriteCodeToDebugXml method
		private void WriteCodeToDebugXml( int code )
		{
			if( XmlDebugging )
			{
				WriteDebugXmlStartElement( "Code" );
				WriteDebugXmlAttribute( "Value", ToHex( code ) );
				WriteDebugXmlAttribute( "FrameCount", _frames.Count );
				WriteDebugXmlEndElement();
			}
		}
		#endregion
		
		#region private AddFrame method
		/// <summary>
		/// Reads a frame from the input stream and adds it to the collection
		/// of frames.
		/// </summary>
		/// <param name="inputStream">
		/// Input stream from which the frame is to be read.
		/// </param>
		/// <param name="lastGce">
		/// The graphic control extension most recently read from the input
		/// stream.
		/// </param>
		private void AddFrame( Stream inputStream,
		                       GraphicControlExtension lastGce )
		{
			GifFrame previousFrame;
			GifFrame previousFrameBut1;
			if( _frames.Count > 0 )
			{
				previousFrame = _frames[_frames.Count - 1];
			}
			else
			{
				previousFrame = null;
			}
			if( _frames.Count > 1 )
			{
				previousFrameBut1 = _frames[_frames.Count - 2];
			}
			else
			{
				previousFrameBut1 = null;
			}
			GifFrame frame = new GifFrame( inputStream, 
			                               _lsd,
			                               _gct,
			                               lastGce,
			                               previousFrame,
			                               previousFrameBut1,
			                               XmlDebugging );
			//MyProgressCounters[_readStreamCounterText].Value 
				//= (int) inputStream.Position;
			_frames.Add( frame );
			WriteDebugXmlNode( frame.DebugXmlReader );
		}
		#endregion

		#region private WriteToStream method
		/// <summary>
		/// Throws a NotSupportedException.
		/// GifDecoders are only intended to read from, and decode streams, not 
		/// to write to them.
		/// </summary>
		/// <param name="outputStream">
		/// The output stream to write to.
		/// </param>
		public override void WriteToStream( Stream outputStream )
		{
			throw new NotSupportedException();
		}
		#endregion

		#endregion

	}
}
