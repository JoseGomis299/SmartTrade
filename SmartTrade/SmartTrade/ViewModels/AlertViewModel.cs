using System;
using System.Collections.Generic;
using ReactiveUI;
using SmartTradeDTOs;

namespace SmartTrade.ViewModels
{
	public class AlertViewModel : ReactiveObject
	{
		private ICollection<SimplePostDTO> _postDTOs;
		public AlertViewModel(ICollection<SimplePostDTO> posts) 
		{
			_postDTOs = posts;
		}
	}
}