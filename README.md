# Hacker News Aggregator API

## Overview

This repository contains an ASP.NET Core REST API that retrieves the best stories from Hacker News and exposes them through a simplified response contract.

The API obtains the story identifiers from Hacker News, retrieves the details for the corresponding stories, orders them by score in descending order, and returns the requested number of stories.

The implementation is designed to provide value quickly while reducing the risk of overloading both the aggregator API and the third-party Hacker News API.

## How to Run

### Using Visual Studio

1. Open the solution in Visual Studio.
2. Set `HackerNewsAggregator` as the startup project.
3. Start the application using the `https` launch profile.
4. Open the Swagger UI shown by default to inspect and test the available endpoint.

### Using the .NET CLI

From the solution directory, run:

```bash
dotnet run --project src --launch-profile https
```

The application URL will be displayed in the console. Open the Swagger endpoint in a browser to test the API interactively.

## API Usage

The endpoint accepts the number of stories to return through the `top` query parameter.

If `top` is not provided, the API returns the top 10 stories by default.

The response contains the following fields:

- `title`: the story title.
- `uri`: the original story URL.
- `postedBy`: the Hacker News username of the author.
- `time`: the story publication time in ISO 8601 format.
- `score`: the Hacker News score.
- `commentCount`: the number of comments or descendants reported by Hacker News.

Example request:

```http
GET /api/hacker-news/stories/best?top=10
```

Example response:

```json
[
  {
    "title": "A uBlock Origin update was rejected from the Chrome Web Store",
    "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
    "postedBy": "ismaildonmez",
    "time": "2019-10-12T13:43:01+00:00",
    "score": 1716,
    "commentCount": 572
  }
]
```

The exact route can be confirmed in Swagger, which is the source of truth for the running application.

## External Hacker News API

The implementation uses the public Hacker News API:

- Best story identifiers: <https://hacker-news.firebaseio.com/v0/beststories.json>
- Individual story details: <https://hacker-news.firebaseio.com/v0/item/{id}.json>

## Design Decisions

The implementation priorities were:

1. Deliver functional value as early as possible.
2. Establish a testable base to support subsequent refinements and refactoring.
3. Protect the aggregator API with rate limiting to reduce the risk of overload.
4. Use caching to improve efficiency and reduce repeated requests to the third-party API.
5. Use controlled concurrent requests to retrieve story details efficiently without generating an uncontrolled request burst.
6. Centralise all tunable behaviour in `appsettings.json` through `HackerNewsOptions`.

### Controlled Concurrency

Story details are retrieved concurrently, but concurrency is bounded. This improves response time while preventing the application from creating an excessive number of simultaneous requests to Hacker News.

### Caching

Caching avoids repeatedly downloading data that is requested frequently. This improves the API response time and reduces traffic to the third-party service.

### Rate Limiting

ASP.NET Core's built-in rate limiter middleware protects the API from excessive inbound traffic. The fixed-window policy and its parameters (`PermitLimit`, `Window`, `QueueLimit`) are fully configurable through `HackerNewsOptions` in `appsettings.json`.

## Assumptions

- When the `top` query parameter is omitted, the default value is 10.
- The `top` value represents the maximum number of stories returned to the caller.
- Stories are sorted by their Hacker News score in descending order.
- The Hacker News API is the source of truth for story titles, URLs, authors, timestamps, scores and comment counts.
- A story without an available URL is returned with a null or empty `uri`, depending on the configured serialization behavior.
- The API may return fewer stories than requested when Hacker News does not provide enough valid story details.
- Cancellation is propagated when the client disconnects or the request is cancelled.

## Testing Strategy

The service is structured around testable components so that HTTP communication, caching, concurrency and response mapping can be tested independently.

The most important test cases are:

- The default value of `top` is 10.
- A requested number of stories is returned when enough valid stories are available.
- Results are ordered by descending score.
- Hacker News item data is mapped to the public response contract correctly.
- Invalid `top` values are rejected.
- Missing or invalid Hacker News items do not cause unrelated valid stories to be discarded.
- Concurrent requests remain within the configured limit.
- Cached responses avoid unnecessary calls to Hacker News.
- Cancellation is honored.

## Improvements with More Time

The next improvements would be addressed in this order:

1. Add a logger mechanism and global exception handler.
2. Add API versioning to allow future contract changes without breaking existing clients.
3. Introduce a hosted background service with queues for requests to the third-party API. This would smooth request bursts, provide better control over throughput and improve resilience when Hacker News is slow or temporarily unavailable.

Additional possible improvements include stronger resilience policies, more detailed observability, automated integration tests and more explicit configuration validation.

## Conclusion

The solution provides a focused REST API for retrieving the best Hacker News stories while keeping the external dependency behind a controlled and optimizable service boundary. The selected design leaves room for caching, bounded concurrency, routing protection and future asynchronous processing without changing the public response contract.
