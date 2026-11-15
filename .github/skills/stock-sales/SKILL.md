---
name: stock-sales
description: Record Stock.Mcp product sales and report sales history over optional date ranges. Use for sales-administrator requests, completed sales, availability checks before selling, and questions about quantities sold.
---

# Stock sales

Use the connected stock MCP tools for live data. Do not edit SQLite, invent sales, or report a successful write without a successful tool response. Explain if the server tools are unavailable.

## Record a sale

1. Obtain the product and quantity sold from the user. Quantities must be positive whole numbers.
2. Resolve the product with `search_products`. Keep codes unchanged; use short English catalogue terms for descriptions. If no matches are found, try singular forms or broader terms. Confirm an exact returned code and ask the user to resolve ambiguity.
3. Check available stock. If insufficient, state the requested and available quantities and do not continue. The server validates availability again transactionally.
4. Call `record_sale(productCode, quantitySold, date)`. Omit `date` to use the server's current time unless the user specifies a sale date. Use an ISO 8601 date-time with a known offset when supplying a date; clarify ambiguous dates or time zones.
5. Show the returned code, description, quantity sold, sale date, and remaining stock. Do not use `update_stock` for sales because it does not create sales history.

## Report sales history

1. Obtain or resolve the product code from the conversation; ask if missing or ambiguous.
2. Translate requested relative dates into explicit `fromDate` and `toDate` values for `get_sales_history`, using the relevant time zone. Clarify "last month" when the distinction between the previous calendar month and a rolling period matters.
3. Bounds are inclusive. Include the whole requested end day rather than accidentally ending the range at its start. Leave dates unset when the user requests all history.
4. Report the date range used, `totalQuantitySold`, `count`, and a concise table of returned sales. An empty history means no matching sales, not a tool failure. Do not infer revenue: sales records contain quantities, not historical sale prices.

For `isError: true`, explain the JSON `error` message and do not pretend the operation succeeded. Do not retry a sale with an uncertain outcome automatically: a repeated sale can reduce stock twice. Check stock and history first, and ask the user if the outcome remains unclear.
