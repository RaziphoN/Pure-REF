using UnityEngine;

namespace REF.Runtime.Utilities
{
	[System.Serializable]
	public class Loop
	{
		[SerializeField] private float tickInterval = 1f;
		private float timer = 0f;
		
		public System.Action OnTick { get; set; }

		public Loop()
		{
			timer = tickInterval;
		}

		public Loop(float tickInterval)
		{
			this.tickInterval = tickInterval;
			timer = tickInterval;
		}

		public void SetTickInterval(float seconds)
		{
			tickInterval = seconds;
			timer = tickInterval;
		}

		public float GetTickInterval()
		{
			return tickInterval;
		}

		public void Update()
		{
			timer -= Time.deltaTime;

			if (timer <= 0f)
			{
				OnTick?.Invoke();
				timer = tickInterval;
			}
		}
	}
}
