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

namespace SpriteVortex.Helpers.GifComponents.Types
{
	/// <summary>
	/// Represents an image in terms of the indices into its associated
	/// <see cref="GifComponents.Components.ColourTable"/> which relate to the 
	/// colours of the individual pixels in the image.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", 
	                 "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	[TypeConverter( typeof( ExpandableObjectConverter ) )]
	public class IndexedPixels : Collection<byte>
	{
		/// <summary>
		/// Indicates whether the collection was instantiated with a fixed
		/// size.
		/// </summary>
		private bool _isFixedSize;
		
		#region constructors
		/// <summary>
		/// Constructor.
		/// </summary>
		public IndexedPixels() : base() {}
		
		/// <summary>
		/// Constructor.
		/// Initialises the pixel collection to the specified size.
		/// </summary>
		/// <param name="capacity">
		/// The fixed capacity to which the collection should be initialsed.
		/// </param>
		public IndexedPixels( int capacity ) : base( new byte[capacity] ) 
		{
			_isFixedSize = true;
		}
		#endregion
		
		#region indexer
		/// <summary>
		/// Gets or sets the byte at the specified index in the collection.
		/// </summary>
		/// <remarks>
		/// Note the use of base.Items[index] rather than base[index].
		/// This is becase base[index] is read-only when the instance has been
		/// instantiated with a default size, whereas the protected Items
		/// property is not read-only.
		/// </remarks>
		public new byte this[int index]
		{
			get
			{
				ValidateIndex( index );
				return base.Items[index];
			}
			set
			{
				ValidateIndex( index );
				base.Items[index] = value;
			}
		}
		#endregion
		
		#region Add method
		/// <summary>
		/// Adds a pixel index to the collection of pixel indices.
		/// </summary>
		/// <param name="pixelIndex">
		/// The index within a colour table of the colour of the pixel to add
		/// to the collection.
		/// </param>
		/// <remarks>
		/// This method is provided because the Add method of 
		/// <see cref="System.Collections.ObjectModel.Collection&lt;T&gt;"/>
		/// throws a misleading exception when the collection has been 
		/// instantiated with a fixed size, implying that the collection is
		/// read-only rather than that items cannot be added to it.
		/// </remarks>
		/// <exception cref="NotSupportedException">
		/// The collection was instantiated with a fixed size, therefore no new
		/// items can be added to it.
		/// </exception>
		public new void Add( byte pixelIndex )
		{
			if( _isFixedSize )
			{
				string message
					= "You cannot add pixels to this instance because it was "
					+ "instantiated with a fixed size.";
				throw new NotSupportedException( message );
			}
			else
			{
				base.Add( pixelIndex );
			}
		}
		#endregion
		
		#region private ValidateIndex method
		private void ValidateIndex( int index )
		{
			if( index < 0 || index >= this.Count )
			{
				string message
					= "Collection size: " + this.Count
					+ ". Supplied index: " + index
					+ ". ";
				throw new ArgumentOutOfRangeException( "index", message );
			}
		}
		#endregion
	}
}
