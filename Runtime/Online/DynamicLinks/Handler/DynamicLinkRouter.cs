using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace REF.Runtime.Online.DynamicLinks
{
	public class DynamicLinkRouter : ILinkProcessor
	{
		private List<ILinkProcessor> processors = new List<ILinkProcessor>();
		private bool allowMultipleProcessorPerLink = true;

		public int Priority { get { return int.MaxValue; } }
		public string Pattern { get { return string.Empty; } }

		public void SetMultiProcessingEnabled(bool enabled)
		{
			allowMultipleProcessorPerLink = enabled;
		}

		public void Register(ILinkProcessor processor)
		{
			if (!processors.Contains(processor))
			{
				processors.Add(processor);
				ReOrderProcessors();
			}
		}

		public void Unregister(ILinkProcessor processor)
		{
			if (processors.Contains(processor))
				processors.Remove(processor);
		}

		private void ReOrderProcessors()
		{
			processors = processors.OrderByDescending(processor => processor.Priority).ToList();
		}

		public void Handle(Uri link)
		{
			if (link == null)
				return;

			foreach (ILinkProcessor processor in processors)
			{
				Regex regex = new Regex(processor.Pattern);

				if (regex.IsMatch(link.OriginalString))
				{
					processor.Handle(link);

					if (!allowMultipleProcessorPerLink)
						break;
				}
			}
		}
	}
}
