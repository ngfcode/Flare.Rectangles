using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flare.Rectangles.Application.Common.Interfaces;
using Flare.Rectangles.Infrastructure.Services;

namespace Flare.Rectangles.Unit.Test.GridServiceTest;

[Collection("GridServiceTest")]
public abstract class BaseGridServiceTest : IDisposable
{
	protected readonly IGridService _sut;

	public BaseGridServiceTest()
	{
		_sut = new GridService();
	}

	public void Dispose()
	{
		if (_sut is not null)
		{
			_sut.ClearData();
		}
	}
}
