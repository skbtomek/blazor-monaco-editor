# Blazor Monaco Editor

### Example of Blazor component that:
- Wraps [monaco-editor](https://microsoft.github.io/monaco-editor/) JavaScript library
- Implements two-way binding
- Imports extra JS library, that consists of typescript definitions generated from .NET/C# types

### Usage:
``` html
<MonacoEditor @bind-Value="Text"/>

@code {    
    [Parameter]
    public string Text { get; set; }
}
```

### Configure Monaco editor:
- Directly in component JS file: [MonacoEditor.razor.js](Components/MonacoEditor.razor.js)

### Generate Typescript definitions from .NET/C# types
- NJsonSchema library creates JsonSchema from .NET types
- Based on that, typescript definitions are generated with `TypeScriptGenerator` 
- Example: [WeatherReportDefinitions](TypeDefinitions/WeatherReportDefinitions.cs)

# How you can use it in your Blazor project
1. Install monaco-editor npm package
    - install libman: `dotnet tool install -g Microsoft.Web.LibraryManager.Cli`
    - install npm package: `libman install monaco-editor` (will create libman.json config)
    - if you use SASS, then add the following exception to .gitignore: `!editor.main.css`
2. Update `_Host.cshtml`:
```html
// Add styles to head section
<head>
   //...
    <link rel="stylesheet" href="lib/monaco-editor/min/vs/editor/editor.main.css"/>
   //...
</head>

// Add scripts to body section
<body>
//...
    <script>
        var require = { paths: { vs: 'lib/monaco-editor/min/vs' } };
    </script>
    <script src="lib/monaco-editor/min/vs/loader.js"></script>
    <script src="lib/monaco-editor/min/vs/editor/editor.main.js"></script>
//...
</body>
```
3. Copy and use [MonacoEditor.razor](Components/MonacoEditor.razor) (cs/css/js) in your project

# Demo

https://blazor-monaco-editor.azurewebsites.net

