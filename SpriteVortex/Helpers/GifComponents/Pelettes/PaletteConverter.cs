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

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace SpriteVortex.Helpers.GifComponents.Pelettes
{
    /// <summary>
    /// Allows a string representation of a <see cref="Palette"/> to be 
    /// displayed in a <see cref="System.Windows.Forms.PropertyGrid"/>.
    /// </summary>
    internal class PaletteConverter : TypeConverter
    {
        #region public override CanConvertTo method
        /// <summary>
        /// Indicates whether a Palette can be converted to the supplied type.
        /// </summary>
        /// <param name="context">
        /// Contextual information.
        /// </param>
        /// <param name="destType">
        /// The type which we want to know whether a Palette can be converted to.
        /// </param>
        /// <returns>
        /// True: A Palette can be converted to the supplied type.
        /// False: A Palette cannot be converted to the supplied type.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context,
                                           Type destType)
        {
            if (destType == typeof(InstanceDescriptor)
               || destType == typeof(string)
               || destType == typeof(Palette))
            {
                return true;
            }
            return base.CanConvertTo(context, destType);
        }
        #endregion

        #region public override ConvertTo method
        /// <summary>
        /// Converts the supplied Palette to the supplied type.
        /// </summary>
        /// <param name="context">
        /// Contextual information.
        /// </param>
        /// <param name="culture">
        /// The culture to use for the conversion.
        /// </param>
        /// <param name="value">
        /// The Palette to convert.
        /// </param>
        /// <param name="destType">
        /// The type to convert to.
        /// </param>
        /// <returns>
        /// The supplied Palette, converted to the supplied type.
        /// </returns>
        public override object ConvertTo(ITypeDescriptorContext context,
                                          CultureInfo culture,
                                          object value,
                                          Type destType)
        {
            if (value == null)
            {
                return null;
            }

            Palette p = (Palette)value;
            if (destType == typeof(InstanceDescriptor))
            {
                Type[] constructorParams = new Type[] { typeof(int) };
                Type pt = typeof(Palette);
                ConstructorInfo ci = pt.GetConstructor(constructorParams);
                ICollection arguments = new object[] { p };
                return new InstanceDescriptor(ci, arguments);
            }
            else if (destType == typeof(string))
            {
                return p.ToString();
            }
            return base.ConvertTo(context, culture, value, destType);
        }
        #endregion
    }
}
