using NJsonSchema.CodeGeneration.TypeScript;

namespace BlazorMonacoEditor.TypeDefinitions;

public class WeatherReportDefinitions : ITypescriptDefinitions
{
    public string Content { get; }

    public string RootTypeName => nameof(WeatherReport);

    public string RootVariableName => "report";

    public WeatherReportDefinitions()
    {
        var schema = NJsonSchema.JsonSchema.FromType<WeatherReport>();
        var generator = new TypeScriptGenerator(schema, new TypeScriptGeneratorSettings
        {
            TypeStyle = TypeScriptTypeStyle.Interface,
            ExportTypes = false,
            GenerateConstructorInterface = false,
            GenerateDefaultValues = false,
            GenerateCloneMethod = false,
            UseLeafType = true,
            NullValue = TypeScriptNullValue.Null
        });
        Content = generator.GenerateFile();
    }

}