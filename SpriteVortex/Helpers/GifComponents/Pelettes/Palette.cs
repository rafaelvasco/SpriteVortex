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
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.IO;

namespace SpriteVortex.Helpers.GifComponents.Pelettes
{
	/// <summary>
	/// A palette of up to 256 colours.
	/// This class exposes methods to load and save the palette in Adobe Colour
	/// Table (.act) format, i.e. a series of red, green and blue intensity
	/// bytes.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", 
	                 "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	//[Editor( typeof( PaletteEditor ), typeof( UITypeEditor ) )]
	[TypeConverter( typeof( PaletteConverter ) )]
	public class Palette : Collection<Color>
	{
		#region declarations
		/// <summary>
		/// The maximum number of colours which can be held in a Palette.
		/// </summary>
		private const int _maxColours = 256;
		
		/// <summary>
		/// The number of bytes we expect in an Adobe Colour Tablefile.
		/// </summary>
		private const int _expectedBytes = _maxColours * 3;
		#endregion
		
		#region Validate method
		/// <summary>
		/// Throws an exception if the current Palette is not in a suitable
		/// state for encoding a GIF.
		/// </summary>
		public void Validate()
		{
			if( this.Count < 2 )
			{
				string message
					= "A palette with less than two colours is not valid.";
				throw new InvalidOperationException( message );
			}
		}
		#endregion
		
		#region static FromFile method
		/// <summary>
		/// Returns a Palette object read from the specified Adobe Colour Table
		/// file.
		/// </summary>
		/// <param name="fileName">
		/// Path to the Adobe Colour Table file
		/// </param>
		/// <returns>
		/// A Palette object as read from the specified file.
		/// </returns>
		public static Palette FromFile( string fileName )
		{
			Stream inputStream = File.OpenRead( fileName );
			Palette returnValue = FromStream( inputStream );
			inputStream.Close();
			return returnValue;
		}
		#endregion
		
		#region static FromStream method
		/// <summary>
		/// Returns a Palette object read from the supplied stream.
		/// </summary>
		/// <param name="inputStream">
		/// The stream containing the Palette's data.
		/// </param>
		/// <returns>
		/// A Palette object as read from the supplied stream.
		/// </returns>
		public static Palette FromStream( Stream inputStream )
		{
			if( inputStream == null )
			{
				throw new ArgumentNullException( "inputStream" );
			}
			Palette returnValue = new Palette();
			byte[] bytes = new byte[_expectedBytes];
			int bytesRead = inputStream.Read( bytes, 0, _expectedBytes );
			
			if( bytesRead != _expectedBytes )
			{
				string message
					= "Adobe Colour Table files should be exactly "
					+ _expectedBytes
					+ " bytes long, and the supplied stream is "
					+ bytesRead
					+ " bytes long";
				throw new ArgumentException( message, "inputStream" );
			}
			
			for( int i = 0; i < bytesRead; i+=3 )
			{
				Color c = Color.FromArgb( bytes[i], bytes[i+1], bytes[i+2] );
				returnValue.Add( c );
			}
			return returnValue;
		}
		#endregion
		
		#region WriteToFile method
		/// <summary>
		/// Writes the current instance to an Adobe Colour Table file.
		/// </summary>
		/// <param name="fileName">
		/// The name of the file to write.
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
		/// Writes the current instance to the supplied stream in Adobe Colour 
		/// Table format.
		/// </summary>
		/// <param name="outputStream">
		/// The stream to write to.
		/// </param>
		public void WriteToStream( Stream outputStream )
		{
			int colourCount = 0;
			foreach( Color c in this )
			{
				outputStream.WriteByte( c.R );
				outputStream.WriteByte( c.G );
				outputStream.WriteByte( c.B );
				colourCount++;
			}

			// If the palette contains less than 256 colours, pad it out to 256
			// with black.
			while( colourCount < 256 )
			{
				outputStream.WriteByte( 0 );
				outputStream.WriteByte( 0 );
				outputStream.WriteByte( 0 );
				colourCount++;
			}
		}
		#endregion

		#region Add method
		/// <summary>
		/// Adds the supplied colour to the palette.
		/// </summary>
		/// <param name="colourToAdd">
		/// The colour to add to the palette.
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// The palette already contains the maximum number of colours allowed.
		/// </exception>
		public new void Add( Color colourToAdd )
		{
			if( this.Contains( colourToAdd ) )
			{
				return;
			}
			
			if( this.Count >= _maxColours )
			{
				string message
					= "This palette already contains the maximum number of "
					+ "colours allowed.";
				throw new InvalidOperationException( message );
			}
			base.Add( colourToAdd );
		}
		#endregion

		#region override ToString method
		/// <summary>
		/// Gets a string representation of the current Palette.
		/// </summary>
		/// <returns>
		/// A string indicating the number of colours in the palette.
		/// </returns>
		public override string ToString()
		{
			return this.Count + " colours";
		}
		#endregion

		#region ToBitmap method
		/// <summary>
		/// Gets a <see cref="System.Drawing.Bitmap"/> representing the current
		/// palette.
		/// </summary>
		/// <returns>
		/// A 16x16 bitmap made up of blocks of colour including all the colours
		/// in the palette.
		/// </returns>
		public Bitmap ToBitmap()
		{
			Size brushSize = GetBrushSize( this.Count );
			
			Bitmap b = new Bitmap( 16, 16 );
			int colourIndex = 0;
			for( int y = 0; y < 16; y += brushSize.Height )
			{
				for( int x = 0; x < 16; x += brushSize.Width )
				{
					Color c;
					if( colourIndex < this.Count )
					{
						c = this[colourIndex];
					}
					else
					{
						c = Color.White;
					}
					for( int innerY = 0; innerY < brushSize.Height; innerY++ )
					{
						for( int innerX = 0; innerX < brushSize.Width; innerX++ )
						{
							b.SetPixel( x + innerX, y + innerY, c );
						}
					}
					colourIndex++;
				}
			}
			return b;
		}
		#endregion

		#region private static GetBrushSize method
		/// <summary>
		/// Works out the best brush size for filling a 16x16 bitmap with blocks
		/// of colour to show all the colours in the palette.
		/// </summary>
		/// <param name="numberOfColours">
		/// The number of colours in the palette.
		/// </param>
		/// <returns>
		/// A <see cref="System.Drawing.Size"/> representing the size of blocks
		/// needed to best fill a 16x16 bitmap with the supplied number of
		/// colours.
		/// </returns>
		private static Size GetBrushSize( int numberOfColours )
		{
			Size brushSize;
			if( numberOfColours > 128 )
			{
				brushSize = new Size( 1, 1 );
			}
			else if( numberOfColours > 64 )
			{
				brushSize = new Size( 2, 1 );
			}
			else if( numberOfColours > 32 )
			{
				brushSize = new Size( 2, 2 );
			}
			else if( numberOfColours > 16 )
			{
				brushSize = new Size( 4, 2 );
			}
			else if( numberOfColours > 8 )
			{
				brushSize = new Size( 4, 4 );
			}
			else if( numberOfColours > 4 )
			{
				brushSize = new Size( 8, 4 );
			}
			else if( numberOfColours > 2 )
			{
				brushSize = new Size( 8, 8 );
			}
			else
			{
				// 2 colours (1 and 0 colours not permitted)
				brushSize = new Size( 16, 8 );
			}
			return brushSize;
		}
		#endregion

	}
}
