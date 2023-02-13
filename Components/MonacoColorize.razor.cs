using BlazorMonacoEditor.TypeDefinitions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorMonacoEditor.Components;

public partial class MonacoColorize : IAsyncDisposable
{

    [Inject]
    private IJSRuntime _jsRuntime { get; set; } = default!;

    [Inject]
    private ITypescriptDefinitions _typescriptDefinitions { get; set; } = null!;

    private IJSObjectReference? _jsModule;

    private IJSObjectReference? _jsComponent;

    private ElementReference _hostDomElement { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./Components/MonacoColorize.razor.js");

            await _jsModule.InvokeVoidAsync(
                "colorize", _hostDomElement, _typescriptDefinitions);
        }
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
    }
}