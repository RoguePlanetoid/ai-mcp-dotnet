---
name: stock-create-product
description: Guide creation of a new Stock.Mcp catalogue product by collecting required fields and calling create_product. Use when a user asks to add a product, including incomplete requests such as creating a Premium Brake Disc.
---

# Create a stock product

Use the connected stock MCP tools, not direct database access. If the tools are unavailable, explain that the stock server must be running and connected.

1. Read the user's request and collect `productCode`, `description`, `costPrice`, and `sellPrice`. Ask for missing required values; do not invent them.
2. Use starting stock supplied by the user as `quantityStock`; otherwise use the tool's default of zero. Prices must be zero or greater, and stock must be a whole number between 0 and 2147483647.
3. Use `search_products` to check the proposed code. Search returns substring matches; compare exact codes, accounting for the server trimming and uppercasing product codes.
4. If that product already exists, report it and do not overwrite it or change its stock as a substitute for creation.
5. Call `create_product` with the collected values and respect client approval requirements.
6. Confirm the returned product code, description, cost price, sell price, and quantity in a compact table.

If a tool reports `isError: true`, explain its JSON `error` message instead of claiming success. Do not retry a creation with an uncertain outcome until a lookup establishes whether the product was created.

For example, "Create a product called Premium Brake Disc" supplies only a description. Ask for the code and prices before calling the create tool.
