namespace BlazorMonacoEditor.Components;

public partial class Editors
{
    private string? Editor1Value { get; set; }
    private string? Editor2Value { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Editor1Value = @"report.Type = ReportType.Weekly";

        Editor2Value = @"report.Title == 'Some title'";

        await Task.CompletedTask;
    }
}