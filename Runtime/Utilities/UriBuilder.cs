using System.Text;

namespace REF.Runtime.Utilities
{
	public class UriBuilder
	{
		private string url;
		private StringBuilder queryBuilder = new StringBuilder();

		public UriBuilder()
		{

		}

		public UriBuilder SetUrl(string url)
		{
			this.url = url;
			return this;
		}

		public void AddParameter(string paramName, string value)
		{
			if (queryBuilder.Length == 0)
				queryBuilder.Append('?');
			else
				queryBuilder.Append('&');

			queryBuilder.Append(System.Uri.EscapeDataString(paramName)).Append('=').Append(System.Uri.EscapeDataString(value));
		}

		public void RemoveParameter(string paramName)
		{
			string query = queryBuilder.ToString();
			int startIndex = query.IndexOf(paramName);
			int endIndex = query.IndexOf('&', startIndex);

			if (startIndex > 0)
			{
				if (endIndex > 0)
					queryBuilder.Remove(startIndex, endIndex - startIndex);
				else
					queryBuilder.Remove(startIndex, query.Length - startIndex);
			}
		}

		public override string ToString()
		{
			return url + queryBuilder.ToString();
		}
	}
}
