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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using SpriteVortex.Helpers.GifComponents.Enums;

namespace SpriteVortex.Helpers.GifComponents.Components
{
	/// <summary>
	/// A global or local colour table which forms part of a GIF data stream.
	/// </summary>
	public class ColourTable : GifComponent
	{
		#region declarations
		/// <summary>
		/// The colours in the colour table.
		/// </summary>
		private Collection<Color> _colours;
		#endregion
		
		#region constructors

		#region default constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		public ColourTable()
		{
			_colours = new Collection<Color>();
		}
		#endregion
		
		#region constructor( Stream, int )
		/// <summary>
		/// Reads and returns a colour table from the supplied input stream.
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to read.
		/// </param>
		/// <param name="numberOfColours">
		/// The number of colours the colour table is expected to contain.
		/// </param>
		public ColourTable( Stream inputStream, int numberOfColours )
			: this( inputStream, numberOfColours, false )
		{}
		#endregion

		#region constructor( Stream, int, bool )
		/// <summary>
		/// Reads and returns a colour table from the supplied input stream.
		/// </summary>
		/// <param name="inputStream">
		/// The input stream to read.
		/// </param>
		/// <param name="numberOfColours">
		/// The number of colours the colour table is expected to contain.
		/// </param>
		/// <param name="xmlDebugging">
		/// Whether or not to create debug XML
		/// </param>
		public ColourTable( Stream inputStream, 
		                    int numberOfColours, 
		                    bool xmlDebugging )
			: base( xmlDebugging )
		{
			string message 
				= "The number of colours must be between 0 and 256. "
				+ "Number supplied: " + numberOfColours;
			
			if( numberOfColours < 0 )
			{
				throw new ArgumentOutOfRangeException( "numberOfColours",
				                                       message );
			}
			
			if( numberOfColours > 256 )
			{
				throw new ArgumentOutOfRangeException( "numberOfColours", 
				                                       message );
			}
			
			WriteDebugXmlAttribute( "ExpectedColours", numberOfColours );
			
			int bytesExpected = numberOfColours * 3; // expected length of Colour table
			byte[] buffer = new byte[bytesExpected];
			int bytesRead = 0;
			bytesRead = inputStream.Read( buffer, 0, buffer.Length );
			int coloursRead = bytesRead / 3;
			WriteDebugXmlAttribute( "ColoursRead", coloursRead );

			int i = 0;
			int j = 0;
			_colours = new Collection<Color>();
			while( i < coloursRead )
			{
				int r = ((int) buffer[j++]) & 0xff;
				int g = ((int) buffer[j++]) & 0xff;
				int b = ((int) buffer[j++]) & 0xff;
				_colours.Add( Color.FromArgb( 255, r, g, b ) );
				i++;
			}
			
			if( bytesRead < bytesExpected )
			{
				message
					= "Expected colour table size: " + bytesExpected
					+ ". Number of bytes read: " + bytesRead;
				SetStatus( ErrorState.ColourTableTooShort, message );
				if( xmlDebugging )
				{
					byte[] actualBytes = new byte[bytesRead];
					for( int n = 0; n < bytesRead; n++ )
					{
						actualBytes[n] = buffer[n];
					}
					WriteDebugXmlByteValues( "BytesRead", actualBytes );
				}
				
				for( int k = bytesRead; k < bytesExpected; k += 3 )
				{
					// Fill colour table with black
					_colours.Add( Color.FromArgb( 0 ) );
				}
			} 

			if( XmlDebugging )
			{
				WriteDebugXmlStartElement( "Colours" );
				foreach( Color c in _colours )
				{
					WriteDebugXmlStartElement( "Colour" );
					WriteDebugXmlAttribute( "R", c.R );
					WriteDebugXmlAttribute( "G", c.G );
					WriteDebugXmlAttribute( "B", c.B );
					WriteDebugXmlEndElement();
				}
			}
			WriteDebugXmlFinish();
		}
		#endregion

		#endregion

		#region properties

		#region Colours property
		/// <summary>
		/// Gets the colours in the colour table.
		/// This property is included in order to make the colours visible in
		/// a property sheet.
		/// </summary>
		/// <remarks>
		/// This property returns an array rather than a collection because it
		/// looks better in a property sheet.
		/// </remarks>
		[Description( "The colours in the colour table" )]
		[SuppressMessage("Microsoft.Performance", 
		                 "CA1819:PropertiesShouldNotReturnArrays")]
		public Color[] Colours
		{
			get
			{
				Color[] colours = new Color[Length];
				_colours.CopyTo( colours, 0 );
				return colours;
			}
		}
		#endregion
		
		#region Length property
		/// <summary>
		/// Gets the number of colours in the colour table.
		/// </summary>
		public int Length
		{
			get { return _colours.Count; }
		}
		#endregion
		
		#region SizeBits property
		/// <summary>
		/// Gets the number of bits required to hold the length of the colour
		/// table, minus 1.
		/// </summary>
		public int SizeBits
		{
			get
			{
				switch( Length )
				{
					case 256:
						return 7;
						
					case 128:
						return 6;
						
					case 64:
						return 5;
						
					case 32:
						return 4;
						
					case 16:
						return 3;
						
					case 8:
						return 2;
						
					case 4:
						return 1;
						
					default:
						// TESTME: SizeBits default condition
						return 0;
				}
			}
		}
		#endregion
		
		#endregion

		#region indexer
		/// <summary>
		/// Gets or sets the colour at the specified index in the colour table.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// The supplied index is outside the bounds of the array.
		/// </exception>
		public Color this[int index]
		{
			get
			{
				if( index >= _colours.Count || index < 0 )
				{
					string message
						= "Colour table size: " + _colours.Count
						+ ". Index: " + index;
					throw new ArgumentOutOfRangeException( "index", message );
				}
				return _colours[index];
			}
			set
			{
				if( index >= _colours.Count || index < 0 )
				{
					string message
						= "Colour table size: " + _colours.Count
						+ ". Index: " + index;
					throw new ArgumentOutOfRangeException( "index", message );
				}
				_colours[index] = value;
			}
		}
		#endregion
		
		#region methods

		#region Add method
		/// <summary>
		/// Adds the supplied colour to the table
		/// </summary>
		/// <param name="colourToAdd">
		/// The colour to add to the colour table.
		/// </param>
		public void Add( Color colourToAdd )
		{
			_colours.Add( colourToAdd );
		}
		#endregion
		
		#region Pad method
		/// <summary>
		/// Pads the colour table out to a length of an exact power of 2
		/// </summary>
		public void Pad()
		{
			while( IsPowerOf2( _colours.Count ) == false )
			{
				_colours.Add( Color.FromArgb( 0, 0, 0 ) );
			}
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
			foreach( Color c in _colours )
			{
				WriteByte( c.R, outputStream );
				WriteByte( c.G, outputStream );
				WriteByte( c.B, outputStream );
			}
		}
		#endregion

		#region private static IsPowerOf2 method
		/// <summary>
		/// Determines whether the supplied number is an exact power of 2 and
		/// therefore a suitable size for a colour table.
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		private static bool IsPowerOf2( int number )
		{
			switch( number )
			{
				case 4:
				case 8:
				case 16:
				case 32:
				case 64:
				case 128:
				case 256:
					return true;
					
				default:
					return false;
			}
		}
		#endregion

		#endregion

	}
}
