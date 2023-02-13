using Microsoft.AspNetCore.Components;

namespace BlazorMonacoEditor.Components;

public partial class Editors
{
    private string? Editor1Value { get; set; }
    private string? Editor2Value { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Editor1Value = @"report.Type = ReportType.Weekly
report.Measurements = {
    TemperatureC: 10,
    TemperatureF: 40,
    WindSpeed: {
        Measure: 20,
        Unit: Speed.KmH
    }
};";

        Editor2Value = @"report.Title == 'Some title'";

        await Task.CompletedTask;
    }
}