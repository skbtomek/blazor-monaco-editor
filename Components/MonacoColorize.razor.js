export function colorize(hostElementDOM, typescriptDefinitions) {

    const rootType = typescriptDefinitions.rootTypeName;
    const rootVariable = typescriptDefinitions.rootVariableName;
    const content = typescriptDefinitions.content;
    const libSource = `const ${rootVariable} = {} as ${rootType};\n${content}`;


    monaco.editor.colorize(libSource, "typescript", {tabSize: 2}).then((code) => {
        hostElementDOM.innerHTML = code;
    });
}