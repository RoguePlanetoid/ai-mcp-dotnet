---
name: stock-demo-setup
description: Set up the six sample products for the Stock.Mcp demo. Use when asked to prepare or seed the stock demo catalogue, not for general product creation or database resets.
---

# Stock demo setup

Use the connected stock MCP server tools, not SQL, local database files, or fabricated results. If the tools are unavailable, explain that the server must be running and connected before continuing.

Create these sample products using `create_product`:

| ProductCode | Description | CostPrice | SellPrice | QuantityStock |
|---|---|---:|---:|---:|
| BP123 | Premium Brake Pad | 18.50 | 39.99 | 100 |
| BP124 | Economy Brake Pad | 9.75 | 24.99 | 15 |
| OF456 | Oil Filter | 4.20 | 11.99 | 50 |
| SP789 | Spark Plug | 2.10 | 6.99 | 8 |
| FLT001 | HVAC Filter Cartridge | 12.50 | 29.99 | 30 |
| CAB005 | Sensor Cable 5m | 6.75 | 14.99 | 12 |

1. Use `search_products` to check for existing sample product codes. Search results are substring matches, so compare returned product codes exactly.
2. Create only missing products, passing all five fields from the table.
3. Leave existing products and sales unchanged. Report existing products whose fields differ from the sample values. Setup is not a reset; never delete the database or adjust stock to hide a difference.
4. If a tool reports `isError: true`, explain its JSON `error` message and do not claim that operation succeeded.
5. Search again to confirm the actual products and stock. Show a compact table identifying created, existing, differing, or failed products.

Creating these products changes the database. Respect the client's tool approvals. Do not retry a write with an uncertain outcome until a lookup establishes whether it succeeded.
