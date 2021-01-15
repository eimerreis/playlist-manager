<script>
    import Playlist from "./Playlist.svelte";
</script>

<style>
    .playlists {
        display: grid;
        grid-template-columns: auto auto auto;
        grid-template-rows: auto;
        display: grid;
    }
</style>

<section class="playlists">
    <Playlist />
</section>
