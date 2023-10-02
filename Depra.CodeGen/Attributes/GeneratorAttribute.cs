// SPDX-License-Identifier: Apache-2.0
// © 2023 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.CodeGen.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class GeneratorAttribute : Attribute { }
}