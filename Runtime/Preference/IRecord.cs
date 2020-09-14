using System.Collections.Generic;

namespace REF.Runtime.Serialization
{
	//public interface IRecord
	//{
	//	void SetKey(string key);
	//	string GetKey();

	//	void Add(IPreference preference);
	//	void Remove(IPreference preference);

	//	IPreference GetPrefByKey(string key);
	//	IPreference GetPrefByIdx(int idx);
	//	int GetPrefCount();
	//}

	//public class Record : IRecord
	//{
	//	private string key;
	//	private List<IPreference> preferences = new List<IPreference>();

	//	public void SetKey(string key)
	//	{
	//		this.key = key;
	//	}

	//	public string GetKey()
	//	{
	//		return key;
	//	}

	//	public void Add(IPreference preference)
	//	{
	//		preferences.Add(preference);
	//	}

	//	public void Remove(IPreference preference)
	//	{
	//		if (preferences.Contains(preference))
	//		{
	//			preferences.Remove(preference);
	//		}
	//	}

	//	public IPreference GetPrefByIdx(int idx)
	//	{
	//		return preferences[idx];
	//	}

	//	public IPreference GetPrefByKey(string key)
	//	{
	//		var foundPref = preferences.Find((pref) => { return pref.GetKey().Equals(key); });
	//		return foundPref;
	//	}

	//	public int GetPrefCount()
	//	{
	//		return preferences.Count;
	//	}
	//}
}
