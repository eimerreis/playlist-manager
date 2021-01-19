<script lang="typescript">
    import { AuthenticationStore } from "../Stores/Authentication";
    import type { Playlist } from "../Types/Playlist";
    import Spotify from "spotify-web-api-js";
import SettingCard from "../Components/SettingCard.svelte";

    export let params;
    console.log(params);
    let playlistPromise: Promise<SpotifyApi.SinglePlaylistResponse>;
    if (params.id) {
        const client = new Spotify();
        client.setAccessToken($AuthenticationStore.accessToken);
        playlistPromise = client.getPlaylist(params.id);
        playlistPromise.then((x) => console.log(x));
    }
</script>

<style>
    .playlist-detail {
        display: grid;
        grid-template-columns: 33% 33% 33%;
        width: 100%;
        gap: 12px;
    }

    h2 {
        text-transform: uppercase;
        font-weight: 300;
        letter-spacing: 3px;
        grid-column: 1 / -1;
    }

    .status-card {
        cursor: pointer;
        background: #fff;
        box-shadow: 1px 1px 4px -2px rgba(0, 0, 0, 0.8);
        border-radius: 5px;
        padding: 12px;

        display: grid;
        grid-template-columns: auto auto;
    }

    .status-card .label {
        align-self: center;
        justify-self: center;
        font-weight: 300;
        font-size: 1.1rem;
        text-transform: uppercase;
        grid-column: 1 / 2;
    }

    .status-card .number {
        font-size: 4rem;
        justify-self: center;
        font-weight: 200;
    }
</style>

{#await playlistPromise then playlist}
    <section class="playlist-detail">
        <h2>{playlist.name}</h2>
        <SettingCard Label="Maximum Tracks" Value="60" />
        <SettingCard Label="Sorted" Value="New First" />
        <SettingCard Label="Archive List" Value="Yes" />
        <div class="status-card">
            <span class="label">Status</span>
            <span class="number">Running</span>
        </div>
    </section>
{/await}
