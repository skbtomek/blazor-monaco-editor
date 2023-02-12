namespace BlazorMonacoEditor.TypeDefinitions;

public interface ITypescriptDefinitions
{
    string Content { get; }
    string RootTypeName { get; }
    string RootVariableName { get; }
}