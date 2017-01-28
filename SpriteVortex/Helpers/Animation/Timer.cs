using System;

//-------------------------------------------------------------------------------------------------------------------//
// CODE ADAPTED FROM ORIGINAL CLASS WRITEN BY NICK GRAVELYN                                                          //
//-------------------------------------------------------------------------------------------------------------------//

namespace SpriteVortex.Helpers.Animation
{
	/// <summary>
	/// An object that invokes an action after an amount of time has elapsed and
	/// optionally continues repeating until told to stop.
	/// </summary>
	public sealed class Timer
	{
		private bool _valid;
		private float _time;
		private float _tickLength;
		private bool _repeats;
		private Action<Timer> _tick;

		/// <summary>
		/// Gets whether or not the timer is active.
		/// </summary>
		public bool IsActive { get { return _valid; } }

		/// <summary>
		/// Gets or sets some extra data to the timer.
		/// </summary>
		public object Tag { get; set; }

		/// <summary>
		/// Gets whether or not this timer repeats.
		/// </summary>
		public bool Repeats { get { return _repeats; } }

		/// <summary>
		/// Gets the length of time (in seconds) between ticks of the timer.
		/// </summary>
		public float TickLength { get { return _tickLength; } }

		internal Timer() { }

		/// <summary>
		/// Creates a new Timer.
		/// </summary>
		/// <param name="length">The length of time between ticks.</param>
		/// <param name="repeats">Whether or not the timer repeats.</param>
		/// <param name="tick">The delegate to invoke when the timer ticks.</param>
		public Timer(float length, bool repeats, Action<Timer> tick)
		{
			if (length <= 0f)
				throw new ArgumentException("length must be greater than 0");
			if (tick == null)
				throw new ArgumentNullException("tick");

			Reset(length, repeats, tick);
		}

		/// <summary>
		/// Stops the timer.
		/// </summary>
		public void Stop()
		{
			_valid = false;
		}

		/// <summary>
		/// Forces the timer to fire its tick event, invalidating the timer unless it is set to repeat.
		/// </summary>
		public void ForceTick()
		{
			if (!_valid)
				return;

			_tick(this);
			_time = 0f;

			_valid = Repeats;

			if (!_valid)
			{
				_tick = null;
				Tag = null;
			}
		}

		internal void Reset(float l, bool r, Action<Timer> t)
		{
			_valid = true;
			_time = 0f;
			_tickLength = l;
			_repeats = r;
			_tick = t;
		}

		/// <summary>
		/// Updates the timer.
		/// </summary>
		/// <param name="frameTime">The current game timestamp.</param>
		public void Update(float frameTime)
		{
			// if a timer is stopped manually, it may not
			// be valid at this point so we skip i
			if (!_valid)
				return;

			// update the timer's time
			_time += frameTime;

			// if the timer passed its tick length...
			if (_time >= _tickLength)
			{
				// perform the action
				_tick(this);

				// subtract the tick length in case we need to repeat
				_time -= _tickLength;

				// if the timer doesn't repeat, it is no longer valid
				_valid = _repeats;

				if (!_valid)
				{
					_tick = null;
					Tag = null;
				}
			}
		}
	}
}