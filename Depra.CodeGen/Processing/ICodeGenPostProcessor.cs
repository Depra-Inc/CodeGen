// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.CodeGen.Processing
{
	public interface ICodeGenPostProcessor
	{
		public void Execute();
	}
}