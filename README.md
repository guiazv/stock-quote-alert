# Stock Quote Alert

This application provides stock trading advice based on predefined sell and buy prices for a specific stock. Every 15 seconds, the application checks the current price for the given stock and if it meets the sell or buy criteria, an email is sent with a buy or sell recommendation.

## Setup

Before running the application, make sure to set up the required environment variables to enable email notifications through the SMTP server. The following environment variables need to be configured:

- `SMTP_PORT`: The port number for the SMTP server. For example, `1025`.
- `SMTP_HOST`: The hostname or IP address of the SMTP server. For local development, it might be `localhost`.
- `SMTP_USERNAME`: The username for authenticating with the SMTP server. Leave it empty if no authentication is required.
- `SMTP_PASSWORD`: The password corresponding to the provided username. Leave it empty if no authentication is required.
- `SMTP_SSL`: A boolean value indicating whether SSL should be used for the SMTP connection. Set it to `false` if SSL/TLS is not required.
- `MAIL_TO`: The recipient email address where the notifications will be sent. For example, `test@example.com`.

### Setting Up Local SMTP Server

To enable email notifications in your development environment, you can run a Mailpit instance (an email testing tool) which provides a SMTP server and a web interface to view captured emails using Docker and the provided `compose.yml` file.

You can start it by running the following command at the root of the project:

```sh
docker compose up
```

This uses the `SMTP_PORT` environment variable as the SMTP server port (if there's a `.env` file, it will be read from that), but defaults to `1025` if the variable is not set.

Mailpit also provides a web interface that you can access to view sent emails. Open your web browser and go to http://localhost:8025 to access the Mailpit web interface.

## Usage

At the root of the project, run the following command:

```
dotnet run --project src/StockQuoteAlert <stock symbol> <sell price> <buy price>
```

Example:

```
dotnet run --project src/StockQuoteAlert petr4 36.50 36.33
```

## Testing

At the root of the project run the following command to run the included test suites:

```
dotnet test
```
