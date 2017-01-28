using System;

//-------------------------------------------------------------------------------------------------------------------//
 // CODE ADAPTED FROM ORIGINAL CLASS WRITEN BY NICK GRAVELYN                                                          //
//-------------------------------------------------------------------------------------------------------------------//


namespace SpriteVortex.Helpers.Animation
{
	/// <summary>
	/// A delegate used by Interpolators to scale their progress and generate their current value.
	/// </summary>
	/// <param name="progress">The current progress of the Interpolator in the range [0, 1].</param>
	/// <returns>A value representing the scaled progress used to generate the Interpolator's Value.</returns>
	public delegate float InterpolatorScaleDelegate(float progress);

	public sealed class Interpolator
	{
		private bool _valid;
		private float _progress;
		private float _start;
		private float _end;
		private float _range;
		private float _speed;
		private float _value;
		private InterpolatorScaleDelegate _scale;
		private Action<Interpolator> _step;
		private Action<Interpolator> _completed;

		/// <summary>
		/// Gets whether or not the interpolator is active.
		/// </summary>
		public bool IsActive { get { return _valid; } }

		/// <summary>
		/// Gets the interpolator's progress in the range of [0, 1].
		/// </summary>
		public float Progress { get { return _progress; } }

		/// <summary>
		/// Gets the interpolator's starting value.
		/// </summary>
		public float Start { get { return _start; } }

		/// <summary>
		/// Gets the interpolator's ending value.
		/// </summary>
		public float End { get { return _end; } }

		/// <summary>
		/// Gets the interpolator's current value.
		/// </summary>
		public float Value { get { return _value; } }

		/// <summary>
		/// Gets or sets some extra data to the timer.
		/// </summary>
		public object Tag { get; set; }

		/// <summary>
		/// Internal constructor used by InterpolatorCollection
		/// </summary>
		internal Interpolator() { }

		/// <summary>
		/// Creates a new Interpolator.
		/// </summary>
		/// <param name="startValue">The starting value.</param>
		/// <param name="endValue">The ending value.</param>
		/// <param name="step">An optional delegate to invoke each update.</param>
		/// <param name="completed">An optional delegate to invoke upon completion.</param>
		public Interpolator(float startValue, float endValue, Action<Interpolator> step, Action<Interpolator> completed)
			: this(startValue, endValue, 1f, InterpolatorScales.Linear, step, completed)
		{
		}

		/// <summary>
		/// Creates a new Interpolator.
		/// </summary>
		/// <param name="startValue">The starting value.</param>
		/// <param name="endValue">The ending value.</param>
		/// <param name="interpolationLength">The amount of time, in seconds, for the interpolation to occur.</param>
		/// <param name="step">An optional delegate to invoke each update.</param>
		/// <param name="completed">An optional delegate to invoke upon completion.</param>
		public Interpolator(float startValue, float endValue, float interpolationLength, Action<Interpolator> step, Action<Interpolator> completed)
			: this(startValue, endValue, interpolationLength, InterpolatorScales.Linear, step, completed)
		{
		}

		/// <summary>
		/// Creates a new Interpolator.
		/// </summary>
		/// <param name="startValue">The starting value.</param>
		/// <param name="endValue">The ending value.</param>
		/// <param name="interpolationLength">The amount of time, in seconds, for the interpolation to occur.</param>
		/// <param name="scale">A custom delegate to use for scaling the Interpolator's value.</param>
		/// <param name="step">An optional delegate to invoke each update.</param>
		/// <param name="completed">An optional delegate to invoke upon completion.</param>
		public Interpolator(float startValue, float endValue, float interpolationLength, InterpolatorScaleDelegate scale, Action<Interpolator> step, Action<Interpolator> completed)
		{
			Reset(startValue, endValue, interpolationLength, scale, step, completed);
		}

		/// <summary>
		/// Stops the Interpolator.
		/// </summary>
		public void Stop()
		{
			_valid = false;
		}

		/// <summary>
		/// Forces the interpolator to set itself to its final position and fire off its delegates before invalidating itself.
		/// </summary>
		public void ForceFinish()
		{
			if (_valid)
			{
				_valid = false;
				_progress = 1;
				float scaledProgress = _scale(_progress);
				_value = _start + _range * scaledProgress;

				if (_step != null)
					_step(this);

				if (_completed != null)
					_completed(this);
			}
		}

		internal void Reset(float s, float e, float l, InterpolatorScaleDelegate scaleFunc, Action<Interpolator> stepFunc, Action<Interpolator> completedFunc)
		{
			if (l <= 0f)
				throw new ArgumentException("length must be greater than zero");

			if (scaleFunc == null)
				throw new ArgumentNullException("scaleFunc");

			_valid = true;
			_progress = 0f;

			_start = s;
			_end = e;
			_range = e - s;
			_speed = 1f / l;

			_scale = scaleFunc;
			_step = stepFunc;
			_completed = completedFunc;
		}

		/// <summary>
		/// Updates the Interpolator.
		/// </summary>
        /// <param name="frameTime"></param>
		public void Update(float frameTime)
		{
			if (!_valid)
				return;

			// update the progress, clamping at 1f
			_progress = Math.Min(_progress + _speed * frameTime, 1f);

			// get the scaled progress and use that to generate the value
			float scaledProgress = _scale(_progress);
			_value = _start + _range * scaledProgress;

			// invoke the step callback
			if (_step != null)
				_step(this);

			// if the progress is 1...
			if (_progress == 1f)
			{
				// the interpolator is done
				_valid = false;

				// invoke the completed callback
				if (_completed != null)
					_completed(this);

				Tag = null;
				_scale = null;
				_step = null;
				_completed = null;
			}
		}
	}
}