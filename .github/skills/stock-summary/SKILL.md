---
name: stock-summary
description: Produce an executive or business-facing summary of current Stock.Mcp stock, low-stock items, recent sales, and recommended next actions. Use for stock overview reports rather than individual stock mutations.
---

# Executive stock summary

Use the connected stock MCP tools to ground the report in current data. This is a read-only workflow: do not create products, adjust stock, or record sales to produce a summary. If the tools are unavailable, report the limitation rather than inventing a report.

1. Call `search_products` without a query or stock filter for the current catalogue. If empty, report that there are no products.
2. Use the user's low-stock threshold, or state that you are using the demo threshold of fewer than 20 units. Call `search_products` with `maxStock` set to that threshold. The threshold is exclusive.
3. Use the requested sales period; if none is given, state that you are using the current calendar month to date. Resolve dates with the relevant time zone and use explicit ISO 8601 bounds.
4. Call `get_sales_history` for each product included in the sales summary, using the same period. There is no all-products sales-history tool. If coverage is partial, identify the included products instead of claiming a catalogue-wide total.
5. Write one short business-friendly paragraph and a compact table when useful. Include current stock highlights, low-stock items, recent quantities sold, and a practical recommended next action.

Report only values returned by the tools or clearly identified calculations from those values. Do not invent reorder quantities, historical prices, revenue, or profit. Current catalogue prices are not historical sale prices.

If a tool reports `isError: true`, explain the JSON `error` message and identify the missing part of the report. Never turn a failed lookup into a zero stock level or zero sales total.
