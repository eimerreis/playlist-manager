<script>
    import { AuthenticationStore } from "../Stores/Authentication";
    import Spotify from "spotify-web-api-js";
    import Playlist from "../Components/Playlist.svelte";

    const client = new Spotify();
    client.setAccessToken($AuthenticationStore.accessToken);
    const playlistsPromise = client.getUserPlaylists($AuthenticationStore.user.id);
</script>

<section class="grid grid-cols-1 gap-8 sm:grid-cols-2 md:grid-cols-3">
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