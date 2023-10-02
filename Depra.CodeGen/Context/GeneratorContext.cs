// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;

namespace Depra.CodeGen.Context
{
	public sealed class GeneratorContext
	{
		private readonly List<CodeText> _codeList = new();

		public GeneratorContext(string folderPath) => FolderPath = folderPath;

		public IReadOnlyList<CodeText> CodeList => _codeList;

		public string FolderPath { get; private set; }

		public void AddCode(string fileName, string text) =>
			_codeList.Add(new CodeText(fileName, text));
	}
}