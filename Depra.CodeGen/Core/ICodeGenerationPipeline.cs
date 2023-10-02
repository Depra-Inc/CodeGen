// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

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