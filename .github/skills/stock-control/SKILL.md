---
name: stock-control
description: Look up Stock.Mcp products, check low stock, and handle warehouse stock adjustments such as goods received, damaged stock, or exact counts. Use for stock-controller and warehouse-manager requests.
---

# Stock control

Use the connected stock MCP tools for current data and changes. Do not access SQLite directly or invent results. If the tools are unavailable, explain the connection requirement.

## Find products

- Use `search_products` with a product code or short description term.
- The demo catalogue is English. Keep product codes unchanged; translate description queries into English where needed. Search does not translate or handle plurals automatically.
- If no matches are returned, try a singular form, synonym, or broader term, such as `brake pad` instead of `brake pads`.
- Leave `query` and `maxStock` unset to list all products. For stock below a threshold, pass that threshold as `maxStock`; the comparison is strictly less than.
- Use only returned products. Search is a substring match; confirm the exact code before changing stock. If a description is ambiguous, ask the user to choose a code.

## Adjust stock

- Use `update_stock(productCode, quantity, mode)` for adjustments: `Add` for goods received, `Remove` for damaged stock, and `Set` for an absolute count.
- Quantities must be whole numbers zero or greater. Stock must remain between 0 and 2147483647.
- Resolve missing quantities or unclear modes with the user. Do not guess or silently change the requested operation.
- Check availability before removing stock. If the request would make stock negative, explain the available and requested quantities without proceeding. The server validates stock again transactionally.
- For a sale, use `record_sale`, not `update_stock`, so sales history is recorded.
- For new products, collect product code, description, cost price, and sell price before using `create_product`.

Show concise tables with product code, description, current stock, and a practical next action where relevant. After an adjustment, include previous stock, quantity changed, and remaining stock from the tool response.

If a tool reports `isError: true`, explain its JSON `error` message and do not claim success. For missing products, suggest a broader search. Do not retry a write with an uncertain outcome until current data establishes whether it succeeded.
