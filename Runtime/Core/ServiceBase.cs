namespace REF.Runtime.Core
{
	public class ServiceBase : IService
	{
		protected IApp app = null;
		private bool initialized = false;

		public virtual bool IsSupported() { return true; }
		public virtual bool IsInitialized() { return initialized; }

		public virtual void Construct(IApp app)
		{
			this.app = app;
		}

		public virtual void PreInitialize(System.Action callback) 
		{ 
			SetInitialized(false); 
			callback?.Invoke();
		}

		public virtual void Configure(IConfiguration config)
		{

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

		public virtual void Suspend() { }
		public virtual void Resume() { }

		protected void SetInitialized(bool state)
		{
			initialized = state;
		}
	}
}
