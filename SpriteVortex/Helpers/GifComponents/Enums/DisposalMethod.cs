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
	/// Enumeration of disposal methods that can be found in a Graphic Control
	/// Extension.
	/// See http://www.w3.org/Graphics/GIF/spec-gif89a.txt section 23.
	/// </summary>
	public enum DisposalMethod
	{
		/// <summary>
		/// 0 - No disposal specified. The decoder is not required to take any 
		/// action.
		/// </summary>
		NotSpecified = 0,
		
		/// <summary>
		/// 1 - Do not dispose. The graphic is to be left in place.
		/// </summary>
		DoNotDispose = 1,
		
		/// <summary>
		/// 2 - Restore to background color. The area used by the graphic must 
		/// be restored to the background color.
		/// </summary>
		RestoreToBackgroundColour = 2,
		
		/// <summary>
		/// 3 - Restore to previous. The decoder is required to restore the 
		/// area overwritten by the graphic with what was there prior to 
		/// rendering the graphic.
		/// </summary>
		RestoreToPrevious = 3,
		
		/// <summary>
		/// 4 - To be defined.
		/// </summary>
		ToBeDefined4,
		
		/// <summary>
		/// 5 - To be defined.
		/// </summary>
		ToBeDefined5,
		
		/// <summary>
		/// 6 - To be defined.
		/// </summary>
		ToBeDefined6,
		
		/// <summary>
		/// 7 - To be defined.
		/// </summary>
		ToBeDefined7
	}
}
