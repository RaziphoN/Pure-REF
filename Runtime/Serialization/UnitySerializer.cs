using UnityEngine;

using System.Text;

namespace REF.Runtime.Serialization
{
	[CreateAssetMenu(fileName = "UnitySerializer", menuName = "REF/Game Data/Default Unity Serializer")]
	public class UnitySerializer : Serializer
	{
		public override byte[] Serialize(object obj)
		{
			var json = JsonUtility.ToJson(obj);
			return Encoding.Default.GetBytes(json);
		}

		public override void Deserialize(byte[] data, object obj)
		{
			var json = Encoding.Default.GetString(data);
			JsonUtility.FromJsonOverwrite(json, obj);
		}

		public override T Deserialize<T>(byte[] data)
		{
			var json = Encoding.Default.GetString(data);
			return JsonUtility.FromJson<T>(json);
		}
	}
}
