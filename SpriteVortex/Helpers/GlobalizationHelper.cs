#region License
/**
     * Copyright (C) 2010 Rafael Vasco (rafaelvasco87@gmail.com)
     * 
     *
     * This program is free software; you can redistribute it and/or
     * modify it under the terms of the GNU General Public License
     * as published by the Free Software Foundation; either version 2
     * of the License, or (at your option) any later version.

     * This program is distributed in the hope that it will be useful,
     * but WITHOUT ANY WARRANTY; without even the implied warranty of
     * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
     * GNU General Public License for more details.

     * You should have received a copy of the GNU General Public License
     * along with this program; if not, write to the Free Software
     * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
     */
#endregion

using System;
using System.Globalization;

namespace SpriteVortex.Helpers
{
    public static class GlobalizationHelper
    {
        public static string FormatNumber(float number)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            NumberFormatInfo format = culture.NumberFormat;
            return Convert.ToString(number, format);
        }
    }
}
