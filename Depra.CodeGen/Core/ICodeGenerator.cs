// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using Depra.CodeGen.Context;

namespace Depra.CodeGen.Core
{
	public interface ICodeGenerator
	{
		public void Execute(GeneratorContext context);
	}
}