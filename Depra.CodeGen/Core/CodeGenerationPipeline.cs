// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Collections.Generic;
using System.IO;
using Depra.CodeGen.Context;
using Depra.CodeGen.Processing;

namespace Depra.CodeGen.Core
{
	public sealed class CodeGenerationPipeline : ICodeGenerationPipeline
	{
		private readonly string _folderPath;
		private readonly IEnumerable<ICodeGenPreProcessor> _preProcessors;
		private readonly IEnumerable<ICodeGenPostProcessor> _postProcessors;

		public CodeGenerationPipeline(string folderPath,
			IEnumerable<ICodeGenPreProcessor> preProcessors,
			IEnumerable<ICodeGenPostProcessor> postProcessors)
		{
			_folderPath = folderPath;
			_preProcessors = preProcessors;
			_postProcessors = postProcessors;
		}

		public bool IsGenerating { get; private set; }

		public void Generate(IEnumerable<Type> generatorTypes)
		{
			if (IsGenerating)
			{
				return;
			}

			IsGenerating = true;
			ExecutePreProcess();

			var changed = false;
			foreach (var generatorType in generatorTypes)
			{
				var context = ExecuteGeneration(generatorType);
				if (GenerateScriptFromContext(context))
				{
					changed = true;
				}
			}

			if (changed)
			{
				ExecutePostProcess();
			}
		}

		private void ExecutePreProcess()
		{
			foreach (var processor in _preProcessors)
			{
				processor.Execute();
			}
		}

		private GeneratorContext ExecuteGeneration(Type generatorType)
		{
			var generator = (ICodeGenerator) Activator.CreateInstance(generatorType);
			var context = new GeneratorContext(_folderPath);
			generator.Execute(context);

			return context;
		}

		private void ExecutePostProcess()
		{
			foreach (var processor in _postProcessors)
			{
				processor.Execute();
			}
		}

		private static bool GenerateScriptFromContext(GeneratorContext context)
		{
			var changed = false;
			var folderPath = context.FolderPath;

			if (Directory.Exists(folderPath) == false)
			{
				Directory.CreateDirectory(folderPath);
			}

			foreach (var code in context.CodeList)
			{
				var hierarchy = code.FileName.Split('/');
				var path = folderPath;
				for (var index = 0; index < hierarchy.Length; index++)
				{
					path += "/" + hierarchy[index];
					if (index == hierarchy.Length - 1)
					{
						break;
					}

					if (Directory.Exists(path) == false)
					{
						Directory.CreateDirectory(path);
					}
				}

				if (File.Exists(path))
				{
					if (File.ReadAllText(path) == code.Text)
					{
						continue;
					}
				}

				File.WriteAllText(path, code.Text);
				changed = true;
			}

			return changed;
		}
	}
}