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
using SpriteVortex.Helpers.GifComponents.Enums;

namespace SpriteVortex.Helpers.GifComponents.Types
{
	/// <summary>
	/// The status of a component in a GIF data stream which is being decoded.
	/// Includes a member of the GifDecoderStatus enumeration, and some text
	/// to describe what (if anything) is wrong.
	/// </summary>
	[TypeConverter( typeof( ExpandableObjectConverter ) )]
	public class GifComponentStatus
	{
		#region declarations
		private ErrorState _errorState;
		private string _errorMessage;
		#endregion
		
		#region constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="errorState">
		/// A member of the Gif.Components.ErrorState enumeration.
		/// </param>
		/// <param name="errorMessage">
		/// Any error message associated with the error state.
		/// </param>
		public GifComponentStatus( ErrorState errorState, string errorMessage )
		{
			_errorState = errorState;
			_errorMessage = errorMessage;
		}
		#endregion

		#region public ErrorState property
		/// <summary>
		/// Gets a member of the Gif.Components.ErrorState enumeration 
		/// describing the error state of a component of a GIF data stream.
		/// </summary>
		public ErrorState ErrorState
		{
			get { return _errorState; }
		}
		#endregion
		
		#region ErrorMessage property
		/// <summary>
		/// Gets any error message associated with the status of a GIF 
		/// component.
		/// </summary>
		public string ErrorMessage
		{
			get { return _errorMessage; }
		}
		#endregion

		#region ToString method
		/// <summary>
		/// Gets a string representation of the GifComponentStatus's ErrorState
		/// property.
		/// </summary>
		/// <returns>
		/// A string representation of the ErrorState property.
		/// </returns>
		public override string ToString()
		{
			return ErrorState.ToString();
		}
		#endregion
	}
}
