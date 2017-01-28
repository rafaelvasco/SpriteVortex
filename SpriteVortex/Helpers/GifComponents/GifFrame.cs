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
using System.Drawing;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Components;

namespace SpriteVortex.Helpers.GifComponents
{
	/// <summary>
	/// A single image frame from a GIF file.
	/// </summary>
	public class GifFrame : GifComponents.Components.GifFrame
	{
		#region Constructor( Image )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="theImage">
		/// The image held in this frame of the GIF file
		/// </param>
		public GifFrame( Image theImage ) : base( theImage )
		{}
		#endregion
		
		#region constructor( Stream, , , , ,  )
		/// <summary>
		/// Creates and returns a GifFrame by reading its data from the supplied
		/// input stream.
		/// </summary>
		/// <param name="inputStream">
		/// A stream containing the data which makes the GifStream, starting 
		/// with the image descriptor for this frame.
		/// </param>
		/// <param name="lsd">
		/// The logical screen descriptor for the GIF stream.
		/// </param>
		/// <param name="gct">
		/// The global colour table for the GIF stream.
		/// </param>
		/// <param name="gce">
		/// The graphic control extension, if any, which precedes this image in
		/// the input stream.
		/// </param>
		/// <param name="previousFrame">
		/// The frame which precedes this one in the GIF stream, if present.
		/// </param>
		/// <param name="previousFrameBut1">
		/// The frame which precedes the frame before this one in the GIF stream,
		/// if present.
		/// </param>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "2#gct")]
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "3#gce")]
		public GifFrame( Stream inputStream,
		                 LogicalScreenDescriptor lsd,
		                 ColourTable gct,
		                 GraphicControlExtension gce,
		                 GifFrame previousFrame,
		                 GifFrame previousFrameBut1 )
			: base( inputStream, lsd, gct, gce, previousFrame, previousFrameBut1 )
		{}
		#endregion
		
		#region constructor( Stream, , , , , bool )
		/// <summary>
		/// Creates and returns a GifFrame by reading its data from the supplied
		/// input stream.
		/// </summary>
		/// <param name="inputStream">
		/// A stream containing the data which makes the GifStream, starting 
		/// with the image descriptor for this frame.
		/// </param>
		/// <param name="lsd">
		/// The logical screen descriptor for the GIF stream.
		/// </param>
		/// <param name="gct">
		/// The global colour table for the GIF stream.
		/// </param>
		/// <param name="gce">
		/// The graphic control extension, if any, which precedes this image in
		/// the input stream.
		/// </param>
		/// <param name="previousFrame">
		/// The frame which precedes this one in the GIF stream, if present.
		/// </param>
		/// <param name="previousFrameBut1">
		/// The frame which precedes the frame before this one in the GIF stream,
		/// if present.
		/// </param>
		/// <param name="xmlDebugging">Whether or not to create debug XML</param>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "2#gct")]
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "3#gce")]
		public GifFrame( Stream inputStream,
		                 LogicalScreenDescriptor lsd,
		                 ColourTable gct,
		                 GraphicControlExtension gce,
		                 GifFrame previousFrame,
		                 GifFrame previousFrameBut1, 
		                 bool xmlDebugging )
			: base( inputStream, lsd, gct, gce, previousFrame, previousFrameBut1, 
			        xmlDebugging )
		{}
		#endregion
	}
}
