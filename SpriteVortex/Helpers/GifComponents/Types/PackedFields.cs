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

namespace SpriteVortex.Helpers.GifComponents.Types
{
	/// <summary>
	/// Represents a byte of data in a GIF data stream which contains a number
	/// of data items.
	/// </summary>
	public class PackedFields
	{
		private bool[] _bits;
		
		#region default constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		public PackedFields()
		{
			_bits = new bool[8];
		}
		#endregion
		
		#region constructor( int )
		/// <summary>
		/// Constructor.
		/// Sets the bits in the packed fields to the corresponding bits from
		/// the supplied byte.
		/// </summary>
		/// <param name="data">
		/// A single byte of data, consisting of fields which may be of one or
		/// more bits.
		/// </param>
		public PackedFields( int data ) : this()
		{
			int bitShift;
			int bitValue;
			for( int i = 0; i < 8; i++ )
			{
				bitShift = 7 - i;
				bitValue = (data >> bitShift) & 1; // get just this bit
				bool bit = bitValue == 1;
				_bits[i] = bit;
			}
		}
		#endregion
		
		#region Byte property
		/// <summary>
		/// Gets the byte which represents the data items held in the packed 
		/// fields.
		/// </summary>
		public int Byte
		{
			get
			{
				int returnValue = 0;
				int bitValue;
				int bitShift = 7;
				foreach( bool bit in _bits )
				{
					if( bit == true )
					{
						bitValue = 1 << bitShift;
					}
					else
					{
						bitValue = 0;
					}
					returnValue |= bitValue;
					bitShift--;
				}
				return returnValue;
			}
		}
		#endregion

		#region SetBit method
		/// <summary>
		/// Sets the specified bit within the packed fields to the supplied 
		/// value.
		/// </summary>
		/// <param name="index">
		/// The zero-based index within the packed fields of the bit to set.
		/// </param>
		/// <param name="valueToSet">
		/// The value to set the bit to.
		/// </param>
		public void SetBit( int index, bool valueToSet )
		{
			if( index < 0 || index > 7 )
			{
				string message 
					= "Index must be between 0 and 7. Supplied index: "
					+ index;
				throw new ArgumentOutOfRangeException( "index", message );
			}
			_bits[index] = valueToSet;
		}
		#endregion
		
		#region SetBits method
		/// <summary>
		/// Sets the specified bits within the packed fields to the supplied 
		/// value.
		/// </summary>
		/// <param name="startIndex">
		/// The zero-based index within the packed fields of the first bit to 
		/// set.
		/// </param>
		/// <param name="length">
		/// The number of bits to set.
		/// </param>
		/// <param name="valueToSet">
		/// The value to set the bits to.
		/// </param>
		public void SetBits( int startIndex, int length, int valueToSet )
		{
			if( startIndex < 0 || startIndex > 7 )
			{
				string message
					= "Start index must be between 0 and 7. Supplied index: "
					+ startIndex;
				throw new ArgumentOutOfRangeException( "startIndex", message );
			}
			
			if( length < 1 || startIndex + length > 8 )
			{
				string message
					= "Length must be greater than zero and the sum of length "
					+ "and start index must be less than 8. Supplied length: "
					+ length
					+ ". Supplied start index: "
					+ startIndex;
				throw new ArgumentOutOfRangeException( "length", message );
			}
			
			int bitShift = length - 1;
			int bitValue;
			int bitValueIfSet;
			int bitIsSet;
			for( int i = startIndex; i < startIndex + length; i++ )
			{
				bitValueIfSet = (1 << bitShift);
				bitValue = (valueToSet & bitValueIfSet);
				bitIsSet = (bitValue >> bitShift);
				_bits[i] = (bitIsSet == 1);
				bitShift--;
			}
		}
		#endregion
		
		#region GetBit method
		/// <summary>
		/// Gets the value of the specified bit within the byte.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the bit to get.
		/// </param>
		/// <returns>
		/// The value of the specified bit within the byte.
		/// </returns>
		public bool GetBit( int index )
		{
			if( index < 0 || index > 7 )
			{
				string message 
					= "Index must be between 0 and 7. Supplied index: "
					+ index;
				throw new ArgumentOutOfRangeException( "index", message );
			}
			return _bits[index];
		}
		#endregion
		
		#region GetBits method
		/// <summary>
		/// Gets the value of the specified bits within the byte.
		/// </summary>
		/// <param name="startIndex">
		/// The zero-based index of the first bit to get.
		/// </param>
		/// <param name="length">
		/// The number of bits to get.
		/// </param>
		/// <returns>
		/// The value of the specified bits within the byte.
		/// </returns>
		public int GetBits( int startIndex, int length )
		{
			if( startIndex < 0 || startIndex > 7 )
			{
				string message
					= "Start index must be between 0 and 7. Supplied index: "
					+ startIndex;
				throw new ArgumentOutOfRangeException( "startIndex", message );
			}
			
			if( length < 1 || startIndex + length > 8 )
			{
				string message
					= "Length must be greater than zero and the sum of length "
					+ "and start index must be less than 8. Supplied length: "
					+ length
					+ ". Supplied start index: "
					+ startIndex;
				throw new ArgumentOutOfRangeException( "length", message );
			}
			
			int returnValue = 0;
			int bitShift = length - 1;
			int bitValue;
			for( int i = startIndex; i < startIndex + length; i++ )
			{
				bitValue = (_bits[i] ? 1 : 0) << bitShift;
				returnValue += bitValue;
				bitShift--;
			}
			return returnValue;
		}
		#endregion
	}
}
