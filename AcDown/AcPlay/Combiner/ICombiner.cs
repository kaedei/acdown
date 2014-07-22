using System.Collections.Generic;

namespace Kaedei.AcPlay.Combiner
{
	public interface ICombiner
	{
		string Combine(List<StandardItem> addItems);
	}
}
