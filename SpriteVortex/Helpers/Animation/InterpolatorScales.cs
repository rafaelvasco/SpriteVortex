//-------------------------------------------------------------------------------------------------------------------//
// ORIGINAL CLASS WRITEN BY NICK GRAVELYN                                                                            //
//-------------------------------------------------------------------------------------------------------------------//


namespace SpriteVortex.Helpers.Animation
{
	/// <summary>
	/// A static class that contains predefined scales for Interpolators.
	/// </summary>
	public static class InterpolatorScales
	{
		/// <summary>
		/// A linear interpolator scale. This is used by default by the Interpolator if no other scale is given.
		/// </summary>
		public static readonly InterpolatorScaleDelegate Linear = LinearInterpolation;

		/// <summary>
		/// A quadratic interpolator scale.
		/// </summary>
		public static readonly InterpolatorScaleDelegate Quadratic = QuadraticInterpolation;

		/// <summary>
		/// A cubic interpolator scale.
		/// </summary>
		public static readonly InterpolatorScaleDelegate Cubic = CubicInterpolation;

		/// <summary>
		/// A quartic interpolator scale.
		/// </summary>
		public static readonly InterpolatorScaleDelegate Quartic = QuarticInterpolation;

		private static float LinearInterpolation(float progress) 
		{
			return progress;
		}
		
		private static float QuadraticInterpolation(float progress)
		{
			return progress * progress;
		}

		private static float CubicInterpolation(float progress)
		{
			return progress * progress * progress;
		}

		private static float QuarticInterpolation(float progress)
		{
			return progress * progress * progress * progress;
		}
	}
}