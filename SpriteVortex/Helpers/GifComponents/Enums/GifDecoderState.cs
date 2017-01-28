#region Copyright (C) Simon Bridewell
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 3
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

// You can read the full text of the GNU General Public License at:
// http://www.gnu.org/licenses/gpl.html

// See also the Wikipedia entry on the GNU GPL at:
// http://en.wikipedia.org/wiki/GNU_General_Public_License
#endregion

namespace SpriteVortex.Helpers.GifComponents.Enums
{
	/// <summary>
	/// A list of values indicating whether a GifDecoder has not started, is in
	/// the process of, or has finished, decoding a GIF stream.
	/// </summary>
	public enum GifDecoderState
	{
		/// <summary>
		/// The GifDecoder has not started decoding.
		/// </summary>
		NotStarted = 0,
		
		/// <summary>
		/// The GifDecoder is currently decoding a GIF stream.
		/// </summary>
		Decoding = 1,
		
		/// <summary>
		/// The GifDecoder has finished decoding the GIF stream.
		/// </summary>
		Done = 2,
	}
}
