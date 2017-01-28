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

namespace SpriteVortex.Helpers.GifComponents.Tools
{
	/// <summary>
	/// Utility class containing various static methods.
	/// TODO: remove Utils from the project.
	/// </summary>
	internal static class Utils
	{
		/// <summary>
		/// Exception handler.
		/// Writes details of the exception to the console and to the debug 
		/// stream.
		/// </summary>
		/// <param name="ex"></param>
		public static void Handle( Exception ex )
		{
			if( ex == null )
			{
				return;
			}
			System.Diagnostics.Debug.WriteLine( ex.ToString() );
			Console.WriteLine( ex.ToString() );
		}
	}
}
