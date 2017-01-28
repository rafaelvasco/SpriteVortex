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

using System.ComponentModel;
using System.IO;
using System.Text;
using SpriteVortex.Helpers.GifComponents.Enums;

namespace SpriteVortex.Helpers.GifComponents.Components
{
	/// <summary>
	/// The header section of a Graphics Interchange Format stream.
	/// See http://www.w3.org/Graphics/GIF/spec-gif89a.txt section 17.
	/// </summary>
	/// <remarks>
	/// The Header identifies the GIF Data Stream in context. The Signature 
	/// field marks the beginning of the Data Stream, and the Version field 
	/// identifies the set of capabilities required of a decoder to fully 
	/// process the Data Stream.
	/// This block is REQUIRED; exactly one Header must be present per Data 
	/// Stream.
	/// </remarks>
	public class GifHeader : GifComponent
	{
		private string _signature;
		private string _gifVersion;
		
		#region constructor( logical properties )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="signature">
		/// The GIF signature which identifies a GIF stream.
		/// Should contain the fixed value "GIF".
		/// </param>
		/// <param name="gifVersion">
		/// The version of the GIF standard used by this stream.
		/// </param>
		public GifHeader( string signature, string gifVersion )
		{
			_signature = signature;
			_gifVersion = gifVersion;

			if( _signature != "GIF" )
			{
				string errorInfo = "Bad signature: " + _signature;
				ErrorState status = ErrorState.BadSignature;
				SetStatus( status, errorInfo );
			}
		}
		#endregion

		#region public constructor( Stream )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="inputStream">
		/// A <see cref="System.IO.Stream"/> containing the data to create the
		/// GifHeader.
		/// </param>
		public GifHeader( Stream inputStream ) 
			: this( inputStream, false ) {}
		#endregion
		
		#region constructor( Stream, bool )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="inputStream">
		/// A <see cref="System.IO.Stream"/> containing the data to create the
		/// GifHeader.
		/// </param>
		/// <param name="xmlDebugging">
		/// A boolean value indicating whether or not an XML document should be 
		/// created showing how the GIF stream was decoded.
		/// </param>
		public GifHeader( Stream inputStream, bool xmlDebugging )
			: base( xmlDebugging )
		{
			
			StringBuilder sb = new StringBuilder();
			int[] bytesRead = new int[6];
			// Read 6 bytes from the GIF stream
			// These should contain the signature and GIF version.
			bool endOfFile = false;
			for( int i = 0; i < 6; i++ ) 
			{
				int nextByte = Read( inputStream );
				if( nextByte == -1 )
				{
					if( endOfFile == false )
					{
						SetStatus( ErrorState.EndOfInputStream, 
						           "Bytes read: " + i );
						endOfFile = true;
					}
					nextByte = 0;
				}
				sb.Append( (char) nextByte );
				if( this.XmlDebugging )
				{
					bytesRead[i] = nextByte;
				}
			}
			
			string headerString = sb.ToString();

			WriteDebugXmlByteValues( "BytesRead", bytesRead );
			
			_signature = headerString.Substring( 0, 3 );
			WriteDebugXmlElement( "Signature", _signature );
			_gifVersion = headerString.Substring( 3, 3 );
			WriteDebugXmlElement( "GifVersion", _gifVersion );
			if( _signature != "GIF" )
			{
				string errorInfo = "Bad signature: " + _signature;
				ErrorState status = ErrorState.BadSignature;
				SetStatus( status, errorInfo );
			}

			WriteDebugXmlFinish();
		}
		#endregion
		
		#region Signature property
		/// <summary>
		/// Gets the signature which introduces the GIF stream.
		/// This should contain the fixed value "GIF".
		/// </summary>
		[Description( "The signature which introduces the GIF stream. " + 
		             "This should contain the fixed value \"GIF\"." )]
		public string Signature
		{
			get { return _signature; }
		}
		#endregion
		
		#region Version property
		/// <summary>
		/// Gets the version of the Graphics Interchange Format used by the GIF 
		/// stream which contains this header.
		/// </summary>
		[Description( "The version of the Graphics Interchange Format used " + 
		             "by the GIF stream which contains this header." )]
		public string Version
		{
			get { return _gifVersion; }
		}
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
			WriteString( _signature, outputStream );
			WriteString( _gifVersion, outputStream );
		}
		#endregion
	}
}
