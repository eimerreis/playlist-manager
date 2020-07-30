<div align="center">
<h1>Playlist Manager</h1>
<p>Automate your spotify playlists with ease</p>

![.github/workflows/deploy-backend.yml](https://github.com/eimerreis/playlist-manager/workflows/.github/workflows/deploy-backend.yml/badge.svg?event=status)
[![GitHub](https://img.shields.io/github/license/eimerreis/playlist-manager?color=brightgreen)](https://github.com/eimerreis/playlist-manager/blob/master/LICENSE)

---

![Playlist Manager](/assets/terminal.png)

</div>

## Features

Playlist Manager is a background service, which organizes your spotify playlists

- **Always have the latest additions to a playlist at the top**
- **Define a limit of tracks for your playlists**
- **Move "old" tracks automatically to an archive playlists**

## Motivation

I always struggled to keep my playlists up to date altough they had quite an audience. As it seemed quite easy for me to create a background job, which does this for me, I started this as a small project on vacation.

## Roadmap

### Functional

- [ ] Support Playlists without a "max item" count
  - Spotify's track limit for playlists is 10.000 items
- [ ] Retrieve Existing Configurations for that user from Backend
  - [ ] Need to Check the users identity via /me route in backend to do so

### Technical

- [ ] Use Code Grant + PKCE Flow to not need ClientSecret in CLI
- [ ] Write Unit Tests for shared functions
- [ ] Write loglevel plugin which logs to application insights
- [ ] Enable seamless debugging of backend

## Components

- `packages/cli/` is a command line interface to initially sort your playlists the way you configure it, and send the configuration to the management service
- `packages/backend` contains an express based API, which receives configurations for playlist management and schedules cron jobs to manage the playlists.
- `packages/shared` contains shared code and type definitions that are used by both the CLI and the backend.

## Contributing
todo
