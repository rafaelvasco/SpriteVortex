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

using System.Diagnostics.CodeAnalysis;

namespace SpriteVortex.Helpers.GifComponents.Enums
{
	/// <summary>
	/// An enumeration of colour quality quantizers available to reduce the 
	/// palette of an image to 256 colours.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", 
	                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
	                 MessageId = "Quantizer")]
	public enum QuantizerType
	{
		/// <summary>
		/// Represents the NewQuant quantizer as originally included in NGif
		/// </summary>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "Neu")]
		NeuQuant,
		
		/// <summary>
		/// Represents the Octree quantizer from 
		/// http://msdn.microsoft.com/en-us/library/aa479306.aspx
		/// </summary>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "Octree")]
		Octree,
		
		/// <summary>
		/// Do not call a quantizer, instead use the user-supplied palette as
		/// the colour table, and find the closest colour within that palette
		/// for each pixel
		/// </summary>
		UseSuppliedPalette
	}
}
