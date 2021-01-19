import { readable } from "svelte/store";

const createStore = () => {
    console.log(__app);
    const config = {
        Spotify: {
            ClientId: __app.env.SPOTIFY_CLIENT_ID,
            RedirectUri: __app.env.SPOTIFY_REDIRECT_URI
        }
    }

    return readable(config);
}

export const ConfigurationStore = createStore();