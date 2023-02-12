export function initialize(hostElementDOM, blazorEditorComponent, value, typescriptDefinitions) {
    if (typescriptDefinitions){
        const rootType = typescriptDefinitions.rootTypeName;
        const rootVariable = typescriptDefinitions.rootVariableName;
        const content = typescriptDefinitions.content;
        const libSource = `const ${rootVariable} = {} as ${rootType}; ${content}`;
        monaco.languages.typescript.typescriptDefaults.addExtraLib(libSource);
    }

    const editor = monaco.editor.create(hostElementDOM, {
        language: 'typescript',
        automaticLayout: true,
        value: value,
        lineNumbers: 'off',
        folding: false,
        glyphMargin: false,
        lineDecorationsWidth: 1,
        minimap: {enabled: false}
    });

    editor.onDidChangeModelContent(e => {
        blazorEditorComponent.invokeMethodAsync('EditorValueChanged', editor.getValue());
    });

    return editor;
}

export function setValue(editor, value) {
    if (editor && editor.getValue() !== value) {
        editor.setValue(value);
    }
}