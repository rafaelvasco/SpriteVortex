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
using System.IO;

namespace SpriteVortex.Helpers.GifComponents.Components
{
	/// <summary>
	/// An application extension which controls the number of times an animation
	/// should be displayed.
	/// See http://www.let.rug.nl/~kleiweg/gif/netscape.html for format
	/// </summary>
	public class NetscapeExtension : ApplicationExtension
	{
		#region declarations
		private int _loopCount;
		#endregion
		
		#region constructor( int repeatCount )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="repeatCount">
		/// Number of times to repeat the animation.
		/// 0 to repeat indefinitely, -1 to not repeat.
		/// </param>
		public NetscapeExtension( int repeatCount )
			: this( new ApplicationExtension( GetIdentificationBlock(), GetApplicationData( repeatCount ) ) )
		{
			_loopCount = repeatCount;
		}
		#endregion
		
		#region constructor( ApplicationExtension )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="applicationExtension">
		/// The application extension to build the Netscape extension from.
		/// </param>
		public NetscapeExtension( ApplicationExtension applicationExtension )
			: base( applicationExtension.IdentificationBlock,
			        applicationExtension.ApplicationData )
		{
			#region guard against application extensions which are not Netscape extensions
			string message;
			if( applicationExtension.ApplicationIdentifier != "NETSCAPE" )
			{
				message = "The application identifier is not 'NETSCAPE' "
						+ "therefore this application extension is not a "
						+ "Netscape extension. Application identifier: "
						+ applicationExtension.ApplicationIdentifier;
				throw new ArgumentException( message, "applicationExtension" );
			}
			
			if( applicationExtension.ApplicationAuthenticationCode != "2.0" )
			{
				message = "The application authentication code is not '2.0' "
						+ "therefore this application extension is not a "
						+ "Netscape extension. Application authentication code: "
						+ applicationExtension.ApplicationAuthenticationCode;
				throw new ArgumentException( message, "applicationExtension" );
			}
			#endregion

			foreach( DataBlock block in ApplicationData )
			{
				if( block.ActualBlockSize == 0 )
				{
					// then we've found the block terminator
					break;
				}
				// The first byte in a Netscape application extension data
				// block should be 1. Ignore if anything else.
				if( block.ActualBlockSize > 2 && block[0] == 1 )
				{
					// The loop count is held in the second and third bytes
					// of the data block, least significant byte first.
					int byte1 = ( (int) block[1] ) & 0xff;
					int byte2 = ( (int) block[2] ) & 0xff;
					
					// String the two bytes together to make an integer,
					// with byte 2 coming first.
					_loopCount = (byte2 << 8) | byte1;
				}
			}
		}
		#endregion
		
		#region LoopCount property
		/// <summary>
		/// Number of times to repeat the frames of the animation.
		/// 0 to repeat indefinitely.
		/// </summary>
		public int LoopCount
		{
			get { return _loopCount; }
		}
		#endregion

		#region private static GetIdentificationBlock method
		private static DataBlock GetIdentificationBlock()
		{
			MemoryStream s = new MemoryStream();
			WriteString( "NETSCAPE2.0", s );
			s.Seek( 0, SeekOrigin.Begin );
			byte[] identificationData = new byte[11];
			s.Read( identificationData, 0, 11 );
			DataBlock identificationBlock = new DataBlock( 11, identificationData );
			return identificationBlock;
		}
		#endregion
		
		#region private static GetApplicationData method
		private static Collection<DataBlock> GetApplicationData( int repeatCount )
		{
			MemoryStream s = new MemoryStream();
			WriteByte( 1, s );
			WriteShort( repeatCount, s );
			s.Seek( 0, SeekOrigin.Begin );
			byte[] repeatData = new byte[3];
			s.Read( repeatData, 0, 3 );
			DataBlock repeatBlock = new DataBlock( 3, repeatData );
			
			byte[] terminatorData = new byte[0];
			DataBlock terminatorBlock = new DataBlock( 0, terminatorData );
			
			Collection<DataBlock> appData = new Collection<DataBlock>();
			appData.Add( repeatBlock );
			appData.Add( terminatorBlock );
			return appData;
		}
		#endregion
	}
}
