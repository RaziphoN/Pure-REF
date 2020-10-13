using UnityEngine;

namespace REF.Runtime.Core
{
	public abstract class ServiceSO : ScriptableObject, IService
	{
		private bool initialized = false;

		public virtual bool IsSupported() { return true; }
		public virtual bool IsInitialized() { return initialized; }

		public virtual void PreInitialize(System.Action callback) 
		{ 
			SetInitialized(false); 
			callback?.Invoke(); 
		}

		public virtual void Initialize(System.Action callback) 
		{ 
			callback?.Invoke(); 
		}

		public virtual void PostInitialize(System.Action callback) 
		{ 
			SetInitialized(true); 
			callback?.Invoke(); 
		}

		public virtual void Release(System.Action callback) 
		{
			SetInitialized(false);
			callback?.Invoke(); 
		}

		public virtual void Update() { }

		public virtual void OnApplicationFocus(bool focus) { }
		public virtual void OnApplicationPause(bool pause) { }
		public virtual void OnApplicationQuit() { }

		protected void SetInitialized(bool state)
		{
			initialized = state;
		}
	}
}
