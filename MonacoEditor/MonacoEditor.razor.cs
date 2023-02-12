using BlazorMonacoEditor.TypeDefinitions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorMonacoEditor.MonacoEditor;

public sealed partial class MonacoEditor : IAsyncDisposable
{
    [Parameter]
    public string Value { get; set; } = default!;

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new();

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    [Inject]
    private ITypescriptDefinitions _typescriptDefinitions { get; set; } = null!;

    private string _internalValue = string.Empty;

    private ElementReference _hostDomElement { get; set; }

    private IJSObjectReference? _jsModule;

    private IJSObjectReference? _jsComponent;

    private DotNetObjectReference<MonacoEditor>? _blazorComponent;

    protected override void OnInitialized()
    {
        _blazorComponent = DotNetObjectReference.Create(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./MonacoEditor/MonacoEditor.razor.js");

            _jsComponent = await _jsModule.InvokeAsync<IJSObjectReference>(
                "initialize", _hostDomElement, _blazorComponent, Value, _typescriptDefinitions);
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (_internalValue == Value) return;

        _internalValue = Value;
        if (_jsModule is not null)
        {
            await _jsModule.InvokeVoidAsync("setValue", _jsComponent, Value);
        }
    }

    [JSInvokable]
    public async Task EditorValueChanged(string value)
    {
        if (value == _internalValue) return;

        _internalValue = value;
        await ValueChanged.InvokeAsync(value);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            if (_jsModule is not null)
            {
                await _jsModule.DisposeAsync();
                _jsModule = null;
            }

            if (_jsComponent is not null)
            {
                await _jsComponent.DisposeAsync();
                _jsComponent = null;
            }
        }
        catch (JSDisconnectedException)
        {
            // Circuit/SignalR is disconnected. Unable to execute javascript in client browser.
            _jsModule = null;
            _jsComponent = null;
        }

        if (_blazorComponent is not null)
        {
            _blazorComponent?.Dispose();
            _blazorComponent = null;
        }
    }
}