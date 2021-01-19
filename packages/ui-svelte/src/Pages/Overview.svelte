<script>
    import { AuthenticationStore } from "../Stores/Authentication";
    import Spotify from "spotify-web-api-js";
    import Playlist from "../Components/Playlist.svelte";

    const client = new Spotify();
    client.setAccessToken($AuthenticationStore.accessToken);
    const playlistsPromise = client.getUserPlaylists($AuthenticationStore.user.id);
</script>

<style>
    .playlists {
        width: 100%;
        display: grid;
        grid-template-columns: 33% 33% 33%;
        gap: 24px;
    }
</style>

<section class="playlists">
    {#if playlistsPromise}
        {#await playlistsPromise}
            <span />
        {:then playlistResponse}
            {#each playlistResponse.items as playlist}
                <Playlist {playlist} />
            {/each}
        {:catch error}
            <p>Error while retrieving Playlists...</p>
        {/await}
    {/if}
</section>
