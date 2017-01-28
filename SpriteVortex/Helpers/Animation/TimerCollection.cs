using System;

//-------------------------------------------------------------------------------------------------------------------//
// CODE ADAPTED FROM ORIGINAL CLASS WRITEN BY NICK GRAVELYN                                                          //
//-------------------------------------------------------------------------------------------------------------------//

namespace SpriteVortex.Helpers.Animation
{
	/// <summary>
	/// A managed collection of timers.
	/// </summary>
	public sealed class TimerCollection
	{
		private readonly Pool<Timer> _timers = new Pool<Timer>(10, t => t.IsActive);

		/// <summary>
		/// Creates a new Timer.
		/// </summary>
		/// <param name="tickLength">The amount of time between the timer's ticks.</param>
		/// <param name="repeats">Whether or not the timer repeats.</param>
		/// <param name="tick">An action to perform when the timer ticks.</param>
		/// <returns>The new Timer object or null if the timer pool is full.</returns>
		public Timer Create(float tickLength, bool repeats, Action<Timer> tick)
		{
			if (tickLength <= 0f)
				throw new ArgumentException("tickLength must be greater than zero.");
			if (tick == null)
				throw new ArgumentNullException("tick");

			lock (_timers)
			{
				// get a new timer from the pool
				Timer t = _timers.New();
				t.Reset(tickLength, repeats, tick);

				return t;
			}
		}

		/// <summary>
		/// Updates all timers in the collection.
		/// </summary>
		/// <param name="frameTime">The current game timestamp.</param>
		public void Update(float frameTime)
		{
			lock (_timers)
			{
				for (int i = 0; i < _timers.ValidCount; i++)
					_timers[i].Update(frameTime);
				_timers.CleanUp();
			}
		}
	}
}