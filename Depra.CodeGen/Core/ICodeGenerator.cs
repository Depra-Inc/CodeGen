using Depra.CodeGen.Context;

namespace Depra.CodeGen.Core
{
	public interface ICodeGenerator
	{
		public void Execute(GeneratorContext context);
	}
}