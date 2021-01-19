<script lang="typescript">
    import type { Playlist } from "../Types/Playlist";
    import { fade } from "svelte/transition";
    import { Link } from "svelte-navigator";

    export let playlist: Playlist;
    const image = playlist.images[0].url;
</script>

<style>
    .playlist-card {
        cursor: pointer;
    }
    img {
        width: 100%;
        object-fit: cover;
        height: 320px;
    }
</style>

<div transition:fade="{{ duration: 200 }}" class="playlist-card">
    <Link to="/playlists/{playlist.id}">
        <h1>{playlist.name}</h1>
        <img src={image} alt={playlist.name} />
    </Link>
</div>
