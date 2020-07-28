- Everything should be controlled through json configuration file
  - Json file could be generated through cli tool in 2nd step

- Serverless Function takes json file as configuration
- How to intially provide an access token?
    - How could the API call look like?
      - /api/manage
        - takes all playlists as a payload including an access token? might be a good idea

POST /api/manage
{
    "accessToken": "ey123102387",
    "configuration": {
    }
}

- How to refresh access tokens?