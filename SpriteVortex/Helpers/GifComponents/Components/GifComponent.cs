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
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using SpriteVortex.Helpers.GifComponents.Enums;
using SpriteVortex.Helpers.GifComponents.Types;

namespace SpriteVortex.Helpers.GifComponents.Components
{
	/// <summary>
	/// The base class for a component of a Graphics Interchange File data 
	/// stream.
	/// </summary>
	[TypeConverter( typeof( ExpandableObjectConverter ) )]
	public abstract class GifComponent :  IDisposable
	{
		#region declarations
		/// <summary>
		/// The status of this component, consisting of its error state and any
		/// associated error messages.
		/// </summary>
		private GifComponentStatus _status;
		
		/// <summary>
		/// Set to true to store information in _debugXml about how the GIF
		/// stream was decoder. Set to false for production use.
		/// </summary>
		private bool _xmlDebugging;
		
		/// <summary>
		/// An <see cref="XmlTextWriter"/> which is used to write an XML 
		/// document showing how the GIF stream was decoded.
		/// For debugging use only.
		/// </summary>
		private System.Xml.XmlWriter _debugXmlWriter;
		
		/// <summary>
		/// Holds a stream of XML showing how the GIF stream was decoded.
		/// For debugging use only.
		/// </summary>
		private Stream _debugXmlStream;
		
		#endregion
		
		#region public constants
		
		/// <summary>
		/// Plain text label - identifies the current block as a plain text
		/// extension.
		/// Value 0x01.
		/// TODO: add see cref once PlainTextExtension class implemented
		/// </summary>
		public const byte CodePlaintextLabel = 0x01;
		
		/// <summary>
		/// Extension introducer - identifies the start of an extension block.
		/// Value 0x21.
		/// </summary>
		public const byte CodeExtensionIntroducer = 0x21;
		
		/// <summary>
		/// Image separator - identifies the start of an 
		/// <see cref="ImageDescriptor"/>.
		/// Value 0x2C.
		/// </summary>
		public const byte CodeImageSeparator = 0x2C;
		
		/// <summary>
		/// Trailer - This is a single-field block indicating the end of the GIF
		/// data stream.
		/// Value 0x3B.
		/// </summary>
		public const byte CodeTrailer = 0x3B;
		
		/// <summary>
		/// Graphic control label - identifies the current block as a
		/// <see cref="GraphicControlExtension"/>.
		/// Value 0xF9.
		/// </summary>
		public const byte CodeGraphicControlLabel = 0xF9;
		
		/// <summary>
		/// Comment label - identifies the current block as a comment extension.
		/// Value 0xFE.
		/// TODO: add see cref once CommentExtension class is implemented.
		/// </summary>
		public const byte CodeCommentLabel = 0xFE;
		
		/// <summary>
		/// Application extension label - identifies the current block as a
		/// <see cref="ApplicationExtension"/>.
		/// Value 0xFF.
		/// </summary>
		public const byte CodeApplicationExtensionLabel = 0xFF;
		
		#endregion
		
		#region protected default constructor
		/// <summary>
		/// Constructor.
		/// This is implicitly called by constructors of derived types.
		/// </summary>
		protected GifComponent()
		{
			_status = new GifComponentStatus( ErrorState.Ok, "" );
		}
		#endregion
		
		#region protected constructor( bool )
		/// <summary>
		/// Constructor.
		/// This should be called from the constructor of a derived type which
		/// accepts a Stream and a boolean as parameters.
		/// </summary>
		/// <param name="xmlDebugging">
		/// True: this component will create XML debug information whilst being 
		/// instantiated from a stream.
		/// False: this component will not create XML debug information.
		/// </param>
		protected GifComponent( bool xmlDebugging ) : this()
		{
			if( xmlDebugging )
			{
				WriteDebugXmlStart();
			}
		}
		#endregion

		#region public properties
		
		#region ComponentStatus property
		/// <summary>
		/// Gets the status of this component, consisting of its error state
		/// and any associated error message.
		/// </summary>
		[Category( "Status" )]
		[Description( "Gets the status of this component, consisting of its " +
		              "error state and any associated error message." )]
		public GifComponentStatus ComponentStatus
		{
			get { return _status; }
		}
		#endregion
		
		#region ErrorState property
		/// <summary>
		/// Gets the member of the Gif.Components.ErrorState enumeration held 
		/// within the ComponentStatus property.
		/// </summary>
		[Browsable( false )]
		[Category( "Status" )]
		[Description( "Gets the member of the Gif.Components.ErrorState " +
		              "enumeration held within the ComponentStatus property." )]
		public ErrorState ErrorState
		{
			get { return _status.ErrorState; }
		}
		#endregion
		
		#region ConsolidatedState property
		/// <summary>
		/// Gets the combined error states of this component and all its child
		/// components.
		/// </summary>
		/// <remarks>
		/// This property uses reflection to inspect the runtime type of the
		/// current instance and performs a bitwise or of the ErrorStates of
		/// the current instance and of any GifComponents within it.
		/// </remarks>
		[Category( "Status" )]
		[Description( "Gets the combined error states of this component and " +
		              "all its child components." )]
		public ErrorState ConsolidatedState
		{
			get
			{
				ErrorState state = this.ErrorState;
				GifComponent component;
				GifComponent[] componentArray;
				PropertyInfo[] properties = this.GetType().GetProperties();
				foreach( PropertyInfo property in properties )
				{
					// We don't want to inspect the ConsolidatedState property
					// else we get a StackOverflowException.
					if( property.Name == "ConsolidatedState" )
					{
						continue;
					}
					
					// Is this property an array?
					if( property.PropertyType.IsArray )
					{
						// Is this property an array of GifComponents?
						componentArray = property.GetValue( this, null ) 
							as GifComponent[];
						if( componentArray != null )
						{
							// It's an array of GifComponents, so inspect
							// their ConsolidatedState properties.
							foreach( GifComponent c in componentArray )
							{
								state |= c.ConsolidatedState;
							}
						}
						continue;
					}

					// Is this property an indexer?
					if( property.GetIndexParameters().Length > 0 )
					{
						// it's probably an indexer, so ignore it
						continue;
					}
					
					// Is this property of a type derived from GifComponent?
					if( property.PropertyType.IsSubclassOf( typeof( GifComponent ) ) )
					{
						// Yes, so it also has a ConsolidatedState property
						component = property.GetValue( this, null ) as GifComponent;
						if( component != null )
						{
							state |= component.ConsolidatedState;
						}
						continue;
					}
					
					// Is this property a generic type?
					if( property.PropertyType.IsGenericType )
					{
						IEnumerable objectCollection
							= property.GetValue( this, null )
							as IEnumerable;
						if( objectCollection != null )
						{
							// Yes, it's IEnumerable, so iterate through its members
							foreach( object o in objectCollection )
							{
								GifComponent c = o as GifComponent;
								if( c != null )
								{
									state |= c.ConsolidatedState;
								}
							}
						}
						continue;
					}

				}
				return state;
			}
		}
		#endregion
		
		#region ErrorMessage property
		/// <summary>
		/// Gets any error message associated with the component's error state.
		/// </summary>
		[Browsable( false )]
		[Category( "Status" )]
		[Description( "Gets any error message associated with the component's " +
		              "error state." )]
		public string ErrorMessage
		{
			get { return _status.ErrorMessage; }
		}
		#endregion

		#region public DebugXml property
		/// <summary>
		/// Gets the XML which provides insight into how the GIF stream or 
		/// component was interpreted by this library.
		/// For debugging use if the GIF stream cannot be decoded.
		/// TODO: view DebugXml in a multi-line text box in PropertyGrid
		/// </summary>
		[Category( "Status" )]
		public string DebugXml
		{
			get 
			{ 
				string xml = DebugXmlReader.ReadOuterXml();
				return xml;
			}
		}
		#endregion
		
		#region public DebugXmlReader property
		/// <summary>
		/// Gets the XML which provides insight into how the GIF stream or 
		/// component was interpreted by this library.
		/// For debugging use if the GIF stream cannot be decoded.
		/// </summary>
		[Browsable( false )]
		public XmlReader DebugXmlReader
		{
			get 
			{
				XmlReader xr;
				if( _xmlDebugging )
				{
					_debugXmlStream.Position = 0;
					xr = new XmlTextReader( _debugXmlStream );
				}
				else
				{
					string message
						= "<Message>There is no DebugXml because XML debugging "
						+ "has not been enabled for this "
						+ this.GetType().Name
						+ " instance.</Message>";
					TextReader tr = new StringReader( message );
					xr = new XmlTextReader( tr );
				}
				xr.MoveToContent();
				return xr;
			}
		}
		#endregion
		
		#endregion
		
		#region protected properties
		
		#region protected XmlDebugging property
		/// <summary>
		/// Gets a value indicating whether or not this component builds an xml
		/// document during decoding for debugging purposes.
		/// </summary>
		protected bool XmlDebugging
		{
			get { return _xmlDebugging; }
		}
		#endregion
		
		#endregion
		
		#region methods
		
		#region public methods
		
		#region override ToString method
		/// <summary>
		/// Gets a string representation of the error status of this component
		/// and its subcomponents.
		/// </summary>
		/// <returns>
		/// A string representation of the error status of this component and
		/// its subcomponents.
		/// </returns>
		public override string ToString()
		{
			return this.ConsolidatedState.ToString();
		}
		#endregion
		
		#region public TestState method
		/// <summary>
		/// Tests whether the error state of this component or any of its member
		/// components contains the supplied member of the ErrorState 
		/// enumeration.
		/// </summary>
		/// <param name="state">
		/// The error state to look for in the current instance's state.
		/// </param>
		/// <returns>
		/// True if the current instance's error state includes the supplied
		/// error state, otherwise false.
		/// </returns>
		public bool TestState( ErrorState state )
		{
			return( ConsolidatedState & state ) == state;
		}
		#endregion

		#endregion
		
		#region protected SetStatus method
		/// <summary>
		/// Sets the ComponentStatus property of thie GifComponent.
		/// </summary>
		/// <param name="errorState">
		/// A member of the Gif.Components.ErrorState enumeration.
		/// </param>
		/// <param name="errorMessage">
		/// An error message associated with the error state.
		/// </param>
		protected void SetStatus( ErrorState errorState, string errorMessage )
		{
			ErrorState newState = _status.ErrorState | errorState;
			string newMessage = _status.ErrorMessage;
			if( !String.IsNullOrEmpty( newMessage ) )
			{
				newMessage += Environment.NewLine;
			}
			newMessage += errorMessage;
			_status = new GifComponentStatus( newState, newMessage );

			if( _xmlDebugging )
			{
				WriteDebugXmlErrorState( errorState, errorMessage );
			}
		}
		#endregion

		#region protected static ToHex method
		/// <summary>
		/// Converts the supplied integer to a 2-character hexadecimal value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		protected static string ToHex( int value )
		{
			return string.Format( CultureInfo.InvariantCulture, 
			                      "{0:X2}",
			                      value );
		}
		#endregion
		
		#region methods for reading the GIF stream
		
		#region protected static Read method
		/// <summary>
		/// Reads a single byte from the input stream and advances the position
		/// within the stream by one byte, or returns -1 if at the end of the
		/// stream.
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to read.
		/// </param>
		/// <returns>
		/// The unsigned byte, cast to an Int32, or -1 if at the end of the 
		/// stream.
		/// </returns>
		protected static int Read( Stream inputStream ) 
		{
			if( inputStream == null )
			{
				throw new ArgumentNullException( "inputStream" );
			}
			int curByte = 0;
			curByte = inputStream.ReadByte();
			return curByte;
		}
		#endregion

		#region protected static ReadShort method
		/// <summary>
		/// Reads next 16-bit value, least significant byte first, and advances 
		/// the position within the stream by two bytes.
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to read.
		/// </param>
		/// <returns>
		/// The next two bytes in the stream, cast to an Int32, or -1 if at the 
		/// end of the stream.
		/// </returns>
		protected static int ReadShort( Stream inputStream )
		{
			// Least significant byte is first in the stream
			int leastSignificant = Read( inputStream );
			
			// Most significant byte is next - shift its value left by 8 bits
			int mostSignificant = Read( inputStream ) << 8;
			
			// Use bitwise or to combine them to a short return value
			int returnValue = leastSignificant | mostSignificant;
			
			// Ensure the return value is -1 if the end of stream has been 
			// reached (if the first byte wasn't the end of stream then we'd
			// get a different negative number instead).
			if( returnValue < 0 )
			{
				returnValue = -1;
			}
			
			return returnValue;
		}
		#endregion

		#region protected SkipBlocks method
		/// <summary>
		/// Skips variable length blocks up to and including next zero length 
		/// block (block terminator).
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to read.
		/// </param>
		protected void SkipBlocks( Stream inputStream )
		{
			DataBlock block;
			do 
			{
				block = new DataBlock( inputStream, _xmlDebugging );
			} 
			while( block.DeclaredBlockSize > 0 && block.ErrorState == ErrorState.Ok );
		}
		#endregion

		#endregion

		#region methods for writing the GIF stream
		
		#region protected static WriteString method
		/// <summary>
		/// Writes the supplied string to the supplied output stream
		/// </summary>
		/// <param name="textToWrite">
		/// The string to be written to the output stream
		/// </param>
		/// <param name="outputStream">
		/// The stream to write the string to.
		/// </param>
		protected static void WriteString( String textToWrite, 
		                                   Stream outputStream )
		{
			if( outputStream == null )
			{
				throw new ArgumentNullException( "outputStream" );
			}

			// if textToWrite is null then write nothing
			if( textToWrite == null )
			{
				return;
			}
			
			char[] chars = textToWrite.ToCharArray();
			for( int i = 0; i < chars.Length; i++ )
			{
				outputStream.WriteByte( (byte) chars[i] );
			}
		}
		#endregion

		#region protected static WriteShort method
		/// <summary>
		/// Writes a 16-bit value to the supplied output stream, 
		/// least-significant byte first.
		/// The first two bytes in the supplied value are discarded.
		/// </summary>
		/// <param name="valueToWrite">
		/// The value to write to the output stream.
		/// </param>
		/// <param name="outputStream">
		/// The stream to write to.
		/// </param>
		protected static void WriteShort( int valueToWrite, Stream outputStream )
		{
			if( outputStream == null )
			{
				throw new ArgumentNullException( "outputStream" );
			}
			
			// Write least significant byte
			outputStream.WriteByte( Convert.ToByte( valueToWrite & 0xff) );
			// Write second-least significant byte
			outputStream.WriteByte( Convert.ToByte( (valueToWrite >> 8) & 0xff ) );
		}
		#endregion
		
		#region protected static WriteByte method
		/// <summary>
		/// Writes the least significant byte of the supplied value to the 
		/// supplied stream.
		/// The first 3 bytes of the supplied value are discarded.
		/// </summary>
		/// <param name="valueToWrite">
		/// The value to write to the output stream.
		/// </param>
		/// <param name="outputStream">
		/// The stream to write to.
		/// </param>
		protected static void WriteByte( int valueToWrite, Stream outputStream )
		{
			if( outputStream == null )
			{
				throw new ArgumentNullException( "outputStream" );
			}
			
			outputStream.WriteByte( Convert.ToByte( valueToWrite & 0xFF ) );
		}
		#endregion
	
		#endregion
		
		#region methods for writing the debug XML

		#region private WriteDebugXmlStart method
		/// <summary>
		/// Starts the process of creating debug XML during decoding.
		/// This method is called by the GifComponent constructor if the supplied
		/// xmlDebugging parameter is set to true.
		/// Sets the private member _xmlDebugging to true.
		/// Creates a new XmlWriter with an underlying MemoryStream and writes
		/// a start element to it, named after the derived type.
		/// </summary>
		private void WriteDebugXmlStart()
		{
			_xmlDebugging = true;
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			_debugXmlStream = new MemoryStream();
			_debugXmlWriter = XmlWriter.Create( _debugXmlStream, settings );
			_debugXmlWriter.WriteStartDocument();
			WriteDebugXmlStartElement( this.GetType().Name );
		}
		#endregion

		#region private WriteDebugXmlErrorState method
		/// <summary>
		/// Writes the latest error state and error message to the debug XML
		/// stream.
		/// </summary>
		/// <param name="errorState">The component's latest error state</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <remarks>
		/// There is no need to call this from derived classes as it is called
		/// by the SetStatus method.
		/// </remarks>
		private void WriteDebugXmlErrorState( ErrorState errorState, 
		                                      string errorMessage )
		{
			if( _xmlDebugging )
			{
				WriteDebugXmlElement( "ErrorState", errorState.ToString() );
				WriteDebugXmlElement( "ErrorMessage", errorMessage );
			}
		}
		#endregion

		#region protected WriteDebugXmlByteValues( int[] ) method
		/// <summary>
		/// If the XmlDebugging property is set to true, writes a BytesRead
		/// node to the DebugXmlWriter containing a hexadecimal representation
		/// of the supplied byte array.
		/// If the XmlDebugging property is set to false, does nothing.
		/// </summary>
		/// <param name="elementName">The name of the element to write</param>
		/// <param name="bytes">The byte array to write</param>
		/// <exception cref="ArgumentNullException">
		/// The supplied byte array is null.
		/// </exception>
		protected void WriteDebugXmlByteValues( string elementName, int[] bytes )
		{
			if( bytes == null )
			{
				throw new ArgumentNullException( "bytes" );
			}
			
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteStartElement( elementName );
				StringBuilder sb = new StringBuilder();
				for( int i = 0; i < bytes.Length; i++ )
				{
					sb.Append( ToHex( bytes[i] ) + " " );
					int result;
					Math.DivRem( i + 1, 16, out result );
					if( result == 0 )
					{
						_debugXmlWriter.WriteStartElement( "ByteGroup" );
						_debugXmlWriter.WriteString( sb.ToString() );
						_debugXmlWriter.WriteEndElement();
						sb = new StringBuilder();
					}
				}
				
				_debugXmlWriter.WriteStartElement( "ByteGroup" );
				_debugXmlWriter.WriteString( sb.ToString() );
				_debugXmlWriter.WriteEndElement();
				_debugXmlWriter.WriteEndElement();
			}
		}
		#endregion
		
		#region protected WriteDebugXmlByteValues( byte[] ) method
		/// <summary>
		/// If the XmlDebugging property is set to true, writes a BytesRead
		/// node to the DebugXmlWriter containing a hexadecimal representation
		/// of the supplied byte array.
		/// If the XmlDebugging property is set to false, does nothing.
		/// </summary>
		/// <param name="elementName">The name of the element to write</param>
		/// <param name="bytes">The byte array to write</param>
		/// <exception cref="ArgumentNullException">
		/// The supplied byte array is null.
		/// </exception>
		protected void WriteDebugXmlByteValues( string elementName, byte[] bytes )
		{
			if( bytes == null )
			{
				throw new ArgumentNullException( "bytes" );
			}
			
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteStartElement( elementName );
				StringBuilder sb = new StringBuilder();
				for( int i = 0; i < bytes.Length; i++ )
				{
					sb.Append( ToHex( bytes[i] ) + " " );
					int result;
					Math.DivRem( i + 1, 16, out result );
					if( result == 0 )
					{
						_debugXmlWriter.WriteStartElement( "ByteGroup" );
						_debugXmlWriter.WriteString( sb.ToString() );
						_debugXmlWriter.WriteEndElement();
						sb = new StringBuilder();
					}
				}
				
				_debugXmlWriter.WriteStartElement( "ByteGroup" );
				_debugXmlWriter.WriteString( sb.ToString() );
				_debugXmlWriter.WriteEndElement();
				_debugXmlWriter.WriteEndElement();
			}
		}
		#endregion
		
		#region protected WriteDebugXmlComment method
		/// <summary>
		/// Writes a comment to the debug XML.
		/// </summary>
		/// <param name="comment">The comment to write</param>
		protected void WriteDebugXmlComment( string comment )
		{
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteComment( comment );
			}
		}
		#endregion
		
		#region protected WriteDebugXmlNode method
		/// <summary>
		/// Useful for writing the debug XML of a child component to its parent
		/// component's debug XML stream.
		/// </summary>
		protected void WriteDebugXmlNode( XmlReader reader )
		{
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteNode( reader, false );
			}
		}
		#endregion
		
		#region protected WriteDebugXmlRaw method
		/// <summary>
		/// Writes raw markup to the debug XML stream from the supplied string.
		/// </summary>
		/// <param name="text"></param>
		protected void WriteDebugXmlRaw( string text )
		{
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteRaw( text );
			}
		}
		#endregion
		
		#region protected WriteDebugXmlElement methods

		#region WriteDebugXmlElement( string, bool )
		/// <summary>
		/// Writes an element to the DebugXml with the supplied element name
		/// and a boolean value as the inner text.
		/// </summary>
		/// <param name="nodeName">
		/// The name of the element to write.
		/// </param>
		/// <param name="innerText">
		/// The inner text of the element to write.
		/// </param>
		protected void WriteDebugXmlElement( string nodeName, bool innerText )
		{
			string boolean = innerText.ToString( CultureInfo.InvariantCulture );
			WriteDebugXmlElement( nodeName, boolean );
		}
		#endregion
		
		#region WriteDebugXmlElement( string, int )
		/// <summary>
		/// Writes an element to the DebugXml with the supplied element name
		/// and inner text.
		/// </summary>
		/// <param name="nodeName">
		/// The name of the element to write.
		/// </param>
		/// <param name="innerText">
		/// The inner text of the element to write.
		/// </param>
		protected void WriteDebugXmlElement( string nodeName, int innerText )
		{
			string text = innerText.ToString( CultureInfo.InvariantCulture );
			WriteDebugXmlElement( nodeName, text );
		}
		#endregion
		
		#region WriteDebugXmlElement( string, string )
		/// <summary>
		/// Writes an element to the DebugXml with the supplied element name
		/// and inner text.
		/// </summary>
		/// <param name="nodeName">
		/// The name of the element to write.
		/// </param>
		/// <param name="innerText">
		/// The inner text of the element to write.
		/// </param>
		/// <remarks>
		/// Call this method in preference to _debugXmlWriter.WriteElementString
		/// because this method removes null characters from the inner text 
		/// before attempting to write.
		/// </remarks>
		protected void WriteDebugXmlElement( string nodeName, string innerText )
		{
			if( innerText == null )
			{
				innerText = string.Empty;
			}
			
			if( _xmlDebugging )
			{
				// remove nulls from the innerText before attempting to write
				innerText = innerText.Replace( "\0", "" );
				_debugXmlWriter.WriteElementString( nodeName, innerText );
			}
		}
		#endregion

		#endregion
		
		#region protected WriteDebugXmlAttribute methods
		/// <summary>
		/// Adds an attribute to the current element. Call after 
		/// WriteDebugXmlStartElement and before WriteDebugXmlEndElement.
		/// </summary>
		/// <param name="name">The attribute name</param>
		/// <param name="value">The attribute value</param>
		protected void WriteDebugXmlAttribute( string name, bool value )
		{
			if( _xmlDebugging )
			{
				string text = value.ToString( CultureInfo.InvariantCulture );
				WriteDebugXmlAttribute( name, text );
			}
		}
		
		/// <summary>
		/// Adds an attribute to the current element. Call after 
		/// WriteDebugXmlStartElement and before WriteDebugXmlEndElement.
		/// </summary>
		/// <param name="name">The attribute name</param>
		/// <param name="value">The attribute value</param>
		protected void WriteDebugXmlAttribute( string name, int value )
		{
			if( _xmlDebugging )
			{
				string text = value.ToString( CultureInfo.InvariantCulture );
				WriteDebugXmlAttribute( name, text );
			}
		}
		
		/// <summary>
		/// Adds an attribute to the current element. Call after 
		/// WriteDebugXmlStartElement and before WriteDebugXmlEndElement.
		/// </summary>
		/// <param name="name">The attribute name</param>
		/// <param name="value">The attribute value</param>
		protected void WriteDebugXmlAttribute( string name, string value )
		{
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteAttributeString( name, value );
			}
		}
		#endregion
		
		#region protected WriteDebugXmlStartElement method
		/// <summary>
		/// Writes out a start tag with the specified name to the debug XML
		/// stream.
		/// </summary>
		/// <param name="name"></param>
		protected void WriteDebugXmlStartElement( string name )
		{
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteStartElement( name );
			}
		}
		#endregion
		
		#region protected WriteDebugXmlEndElement method
		/// <summary>
		/// Writes out the closing tag of the current element to the debug XML
		/// stream.
		/// </summary>
		protected void WriteDebugXmlEndElement()
		{
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteEndElement();
			}
		}
		#endregion
		
		#region protected WriteDebugXmlFinish method
		/// <summary>
		/// Finishes off writing the debug XML.
		/// </summary>
		protected void WriteDebugXmlFinish()
		{
			if( _xmlDebugging )
			{
				_debugXmlWriter.WriteRaw( Environment.NewLine );
				_debugXmlWriter.WriteEndElement();
				_debugXmlWriter.WriteEndDocument();
				_debugXmlWriter.Flush();
				_debugXmlWriter.Close();
			}
		}
		#endregion

		#endregion

		#region abstract WriteToStream method
		/// <summary>
		/// Appends the current GifComponent to the supplied output stream.
		/// </summary>
		/// <param name="outputStream">
		/// The stream to which the component is to be written.
		/// </param>
		public abstract void WriteToStream( Stream outputStream );
		#endregion

		#endregion

		#region IDisposable implementation
		/// <summary>
		/// Indicates whether or not the Dispose( bool ) method has already been 
		/// called.
		/// </summary>
		bool _disposed;

		/// <summary>
		/// Finalzer.
		/// </summary>
		~GifComponent()
		{
			Dispose( false );
		}

		/// <summary>
		/// Disposes resources used by this class.
		/// </summary>
		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		/// <summary>
		/// Disposes resources used by this class.
		/// </summary>
		/// <param name="disposing">
		/// Indicates whether this method is being called by the class's Dispose
		/// method (true) or by the garbage collector (false).
		/// </param>
		protected virtual void Dispose( bool disposing )
		{
			if( !_disposed )
			{
				if( disposing )
				{
					// dispose-only, i.e. non-finalizable logic
					//_debugXmlStream.Dispose();
				}

				// new shared cleanup logic
				_disposed = true;
			}

			// Uncomment if the base type also implements IDisposable
//			base.Dispose( disposing );
		}
		#endregion
	}
}
