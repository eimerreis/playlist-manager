import express from "express";
import superagent from "superagent";
import kleur from "kleur";
import SpotifyApi from "spotify-web-api-node";
import signale from "signale";
import { SpotifyClientId, SpotifyClientSecret } from "@env";
import opn from "opn";

import { ConfigurePlaylistManagement } from "../ConfigurePlaylistManagement";
import { AccessTokenResponse } from "./AccessTokenResponse";

//todo: move constants to config
const scopes = "playlist-modify-public playlist-modify-private playlist-read-private"
const RedirectUri = "http://localhost:3000/auth";

const SuccessMessage = `
    <html>
        <head>
            <style type="text/css">
                body {
                    display: flex;
                    align-items: center;
                    flex-direction: column;
                    font-family: Helvetica, Arial;
                }
            </style>
            <script type="text/javascript">
                setTimeout(function() {
                    window.close();
                }, 5000);
            </script>
        </head>
        <body>
            <h1>Login Successful</h1>
            <p> This window will close itself automatically in after seconds </p>
        </body>
    </html>

`

const app = express();
app.get('/auth', async (req, res) => {
    const { code } = req.query;

    if (!code) {
        //todo: throw unauthorized error
    }

    res.send(SuccessMessage);

    try {
        const tokenResponse = await superagent
            .post("https://accounts.spotify.com/api/token")
            .send(`grant_type=authorization_code&code=${code}&redirect_uri=${RedirectUri}&client_id=${SpotifyClientId}&client_secret=${SpotifyClientSecret}`)

        const body = tokenResponse.body as AccessTokenResponse;

        ConfigurePlaylistManagement(new SpotifyApi({
            accessToken: body.access_token,
            clientId: SpotifyClientId,
            clientSecret: SpotifyClientSecret,
            redirectUri: RedirectUri,
            refreshToken: body.refresh_token
        }));

    } catch (err) {
        console.error(err);
    } finally {
        server.close();
    }
});

const server = app.listen(3000, () => {
    console.log("server runs on port 3000")
});

export const AcquireToken = () => {
    signale.log(kleur.bold("Logging you in to your"), kleur.green("Spotify Account"));
    opn(`https://accounts.spotify.com/authorize?response_type=code&client_id=${SpotifyClientId}&scope=${encodeURIComponent(scopes)}&redirect_uri=${encodeURIComponent(RedirectUri)}`);
}
