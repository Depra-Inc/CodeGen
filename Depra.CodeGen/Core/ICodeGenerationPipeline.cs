using System;
using System.Collections.Generic;

namespace Depra.CodeGen.Core
{
	public interface ICodeGenerationPipeline
	{
		bool IsGenerating { get; }

		void Generate(IEnumerable<Type> generatorTypes);
	}
}