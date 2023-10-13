# Stock Quote Alert

This application provides stock trading advice based on predefined sell and buy prices for a specific stock. Every 15 seconds, the application checks the current price for the given stock and if it meets the sell or buy criteria, an email is sent with a buy or sell recommendation.

## Usage

```sh
dotnet run --project src/StockQuoteAlert <stock symbol> <sell price> <buy price>
```
