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

namespace SpriteVortex.Helpers.GifComponents.Enums
{
	/// <summary>
	/// Enumeration of strategies for including colour tables when encoding 
	/// a GIF file.
	/// </summary>
	public enum ColourTableStrategy
	{
		/// <summary>
		/// Add a local colour table to every frame. No global colour table.
		/// </summary>
		UseLocal,
		
		/// <summary>
		/// Use a global colour table for all frames.
		/// </summary>
		UseGlobal
	}
}
