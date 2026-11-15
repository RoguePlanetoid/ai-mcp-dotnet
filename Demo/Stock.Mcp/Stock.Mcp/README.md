# MCP Server

This README was created using the C# MCP server project template.
It demonstrates how you can easily create an MCP server using C# and run it as an ASP.NET Core web application.

The MCP server is built as a self-contained application and does not require the .NET runtime to be installed on the target machine.
However, since it is self-contained, it must be built for each target platform separately.
By default, the template is configured to build for:
* `win-x64`
* `win-arm64`
* `osx-arm64`
* `linux-x64`
* `linux-arm64`
* `linux-musl-x64`

If you require more platforms to be supported, update the list of runtime identifiers in the project's `<RuntimeIdentifiers />` element.

## Developing locally

To test this MCP server from source code (locally), you can configure your IDE to connect to the server using localhost.

```json
{
  "servers": {
    "Stock.Mcp": {
      "type": "http",
      "url": "http://localhost:5050"
    }
  }
}
```

Refer to the VS Code or Visual Studio documentation for more information on configuring and using MCP servers:

- [Use MCP servers in VS Code](https://code.visualstudio.com/docs/copilot/chat/mcp-servers)
- [Use MCP servers in Visual Studio](https://learn.microsoft.com/visualstudio/ide/mcp-servers)

## Testing the MCP Server

Once configured, ask Copilot Chat:

```text
Use the stock MCP server tools to list all products and their current stock levels.
```

Copilot should call `search_products` without a search term or stock filter. Approve the tool call if prompted. The response should contain a `count` and a `products` array, which Copilot can present as a table.

A new database has no sample products, so `count: 0` and an empty `products` array are valid results. If you have created the sample products from the root `DEMO.md`, you can also ask `Find brake pads`; Copilot should use `search_products` with a term such as `brake pad` and return `BP123` and `BP124`.

## Known issues

1. When using VS Code, connecting to `https://` fails.
  * This is related to using a self-signed developer certificate, even when the certificate is trusted by the system.
  * Connecting with `http://localhost:5050` succeeds.
  * See [Cannot connect to MCP server via SSE using trusted developer certificate (microsoft/vscode#248170)](https://github.com/microsoft/vscode/issues/248170) for more information.

## More information

ASP.NET Core MCP servers use the [ModelContextProtocol.AspNetCore](https://www.nuget.org/packages/ModelContextProtocol.AspNetCore) package from the MCP C# SDK. For more information about MCP:

- [Official Documentation](https://modelcontextprotocol.io/)
- [Protocol Specification](https://spec.modelcontextprotocol.io/)
- [GitHub Organization](https://github.com/modelcontextprotocol)
- [MCP C# SDK](https://csharp.sdk.modelcontextprotocol.io/)
